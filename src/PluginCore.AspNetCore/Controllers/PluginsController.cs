//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PluginCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PluginCore.AspNetCore.Authorization;
using PluginCore.Infrastructure;
using PluginCore.IPlugins;
using PluginCore.AspNetCore.ResponseModel;
using PluginCore.Interfaces;
using PluginCore.AspNetCore.Interfaces;
using PluginCore.IPlugins.Infrastructure;
using PluginCore.IPlugins.Interfaces;

namespace PluginCore.AspNetCore.Controllers;

[Route("api/plugincore/admin/[controller]/[action]")]
[PluginCoreAdminAuthorize]
[ApiController]
public class PluginsController : ControllerBase
{
    #region Fields
    private readonly IPluginManager _pluginManager;
    private readonly IPluginFinder _pluginFinder;
    private readonly IPluginApplicationBuilderManager _pluginApplicationBuilderManager;
    #endregion

    #region Ctor
    public PluginsController(IPluginManager pluginManager, IPluginFinder pluginFinder, IPluginApplicationBuilderManager pluginApplicationBuilderManager)
    {
        _pluginManager = pluginManager;
        _pluginFinder = pluginFinder;
        _pluginApplicationBuilderManager = pluginApplicationBuilderManager;
    }
    #endregion

    #region Actions

    #region 插件列表
    /// <summary>
    /// 加载插件列表
    /// </summary>
    /// <param name="status">插件状态</param>
    /// <returns></returns>
    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> List(string status = "all")
    {
        BaseResponseModel responseData = new ResponseModel.BaseResponseModel();
        var pluginConfigModel = PluginConfigModelFactory.Create();

        // 获取所有插件信息
        IList<PluginInfoModel> pluginInfoModels = PluginInfoModelFactory.CreateAll();
        IList<PluginInfoResponseModel> responseModels = new List<PluginInfoResponseModel>();
        string[] enablePluginIds = _pluginFinder.EnablePluginIds().ToArray();

        // 添加插件状态
        responseModels = PluginInfoModelToResponseModel(pluginInfoModels, pluginConfigModel, enablePluginIds);
        #region 筛选插件状态
        switch (status.ToLower())
        {
            case "all":
                break;
            case "enabled":
                responseModels = responseModels.Where(m => m.Status == PluginStatus.Enabled).ToList();
                break;
            case "disabled":
                responseModels = responseModels.Where(m => m.Status == PluginStatus.Disabled).ToList();
                break;
            default:
                break;
        }
        #endregion

        responseData.Code = 1;
        responseData.Message = "加载插件列表成功";
        responseData.Data = responseModels;

        return await Task.FromResult(responseData);
    }
    #endregion

    #region 卸载插件
    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> Uninstall(string pluginId)
    {
        BaseResponseModel responseData = new BaseResponseModel();
        pluginId = pluginId.Trim();
        var pluginConfigModel = PluginConfigModelFactory.Create();
        // 卸载插件 必须 先禁用插件
        #region 效验
        if (pluginConfigModel.EnabledPlugins.Contains(pluginId))
        {
            responseData.Code = -1;
            responseData.Message = "卸载失败: 请先禁用此插件";
            return await Task.FromResult(responseData);
        }
        string pluginDirStr= Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId);
        string pluginWwwrootDirStr = Path.Combine(PluginPathProvider.PluginsWwwRootDir(), pluginId);
        if (!Directory.Exists(pluginDirStr) && !Directory.Exists(pluginWwwrootDirStr))
        {
            responseData.Code = -2;
            responseData.Message = "卸载失败: 此插件不存在";
            return await Task.FromResult(responseData);
        }
        #endregion

        try
        {
            // PS:卸载插件必须先禁用插件，所以此时插件LoadContext已被移除释放(插件Assemblies已被释放), 此处不需移除LoadContext
                
            // 1.删除物理文件
            var pluginDir = new DirectoryInfo(pluginDirStr);
            if (pluginDir.Exists) {
                pluginDir.Delete(true);
            }
            // 虽然 已禁用 时 pluginWwwrootDirStr/pluginId 已删除, 但为确保, 还是再删除一次
            var pluginWwwrootDir = new DirectoryInfo(pluginWwwrootDirStr);
            if (pluginWwwrootDir.Exists) {
                pluginWwwrootDir.Delete(true);
            }

            responseData.Code = 1;
            responseData.Message = "卸载成功";
        }
        catch (Exception ex)
        {
            responseData.Code = -1;
            responseData.Message = "卸载失败: " + ex.Message;
        }

        return await Task.FromResult(responseData);
    }
    #endregion

    #region 启用插件
    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> Enable(string pluginId)
    {
        BaseResponseModel responseData = new BaseResponseModel();
        var pluginConfigModel = PluginConfigModelFactory.Create();
        // 效验是否存在于 已禁用插件列表
        #region 效验
        pluginId = pluginId.Trim();
        var pluginDir = new DirectoryInfo(Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId));
        if (pluginDir != null && !pluginDir.Exists)
        {
            responseData.Code = -1;
            responseData.Message = "启用失败: 此插件不存在";
            return await Task.FromResult(responseData);
        }
        string[] enablePluginIds = _pluginFinder.EnablePluginIds().ToArray();
        if (enablePluginIds.Contains(pluginId)) {
            responseData.Code = -2;
            responseData.Message = "启用失败: 此插件已启用";
            return await Task.FromResult(responseData);
        }
        #endregion

        try
        {
            // 1. 创建插件程序集加载上下文, 添加到 PluginsLoadContexts
            _pluginManager.LoadPlugin(pluginId);
            // 2. 添加到 pluginConfigModel.EnabledPlugins
            pluginConfigModel.EnabledPlugins.Add(pluginId);
            // 4.保存到 plugin.config.json
            PluginConfigModelFactory.Save(pluginConfigModel);

            // 5. 找到此插件实例
            IPlugin plugin = _pluginFinder.Plugin(pluginId);
            if (plugin == null)
            {
                // 7.启用不成功, 回滚插件状态: (1)释放插件上下文 (2)更新 plugin.config.json
                try
                {
                    _pluginManager.UnloadPlugin(pluginId);
                }
                catch (Exception ex)
                { }

                // 从 pluginConfigModel.EnabledPlugins 移除
                pluginConfigModel.EnabledPlugins.Remove(pluginId);
                // 保存到 plugin.config.json
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseData.Code = -1;
                responseData.Message = "启用失败: 此插件不存在";
                return await Task.FromResult(responseData);
            }
            // 6.调取插件的 AfterEnable(), 插件开发者可在此回收资源
            var pluginEnableResult = plugin.AfterEnable();
            if (!pluginEnableResult.IsSuccess)
            {
                // 7.启用不成功, 回滚插件状态: (1)释放插件上下文 (2)更新 plugin.config.json
                try
                {
                    _pluginManager.UnloadPlugin(pluginId);
                }
                catch (Exception ex)
                { }

                // 从 pluginConfigModel.EnabledPlugins 移除
                pluginConfigModel.EnabledPlugins.Remove(pluginId);
                // 保存到 plugin.config.json
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseData.Code = -1;
                responseData.Message = "启用失败: 来自插件的错误信息: " + pluginEnableResult.Message;
                return await Task.FromResult(responseData);
            }

            // 7. ReBuild
            this._pluginApplicationBuilderManager.ReBuild();

            // 8. 尝试复制 插件下的 wwwroot 到 Plugins_wwwroot
            string wwwRootDir = PluginPathProvider.WwwRootDir(pluginId);
            if (Directory.Exists(wwwRootDir))
            {
                string targetDir = PluginPathProvider.PluginWwwRootDir(pluginId);
                Utils.FileUtil.CopyFolder(wwwRootDir, targetDir);
            }

            responseData.Code = 1;
            responseData.Message = "启用成功";
        }
        catch (Exception ex)
        {
            responseData.Code = -2;
            responseData.Message = "启用失败: " + ex.Message;
        }

        return await Task.FromResult(responseData);
    }
    #endregion

    #region 禁用插件
    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> Disable(string pluginId)
    {
        BaseResponseModel responseData = new BaseResponseModel();
        #region 效验
        pluginId = pluginId.Trim();
        var pluginConfigModel = PluginConfigModelFactory.Create();
        // string[] enablePluginIds = _pluginFinder.EnablePluginIds().ToArray();
        // // 效验是否存在于 已启用插件列表
        // if (!enablePluginIds.Contains(pluginId))
        // {
        //     responseData.Code = -1;
        //     responseData.Message = "禁用失败: 此插件不存在, 或未启用";
        //     return await Task.FromResult(responseData);
        // }
        #endregion

        try
        {
            // 1. 找到此插件实例
            IPlugin plugin = _pluginFinder.Plugin(pluginId);
            if (plugin == null)
            {
                responseData.Code = -1;
                responseData.Message = "禁用失败: 此插件不存在, 或未启用";
                return await Task.FromResult(responseData);
            }
            try
            {
                // 2.调取插件的 BeforeDisable(), 插件开发者可在此回收资源
                var pluginDisableResult = plugin.BeforeDisable();
                if (!pluginDisableResult.IsSuccess)
                {
                    responseData.Code = -1;
                    responseData.Message = "禁用失败: 来自插件的错误信息: " + pluginDisableResult.Message;
                    return await Task.FromResult(responseData);
                }
                // 3.移除插件对应的程序集加载上下文
                _pluginManager.UnloadPlugin(pluginId);
                // 3.1. ReBuild
                this._pluginApplicationBuilderManager.ReBuild();
                if (pluginConfigModel.EnabledPlugins.Contains(pluginId)) {
                    // 4.从 pluginConfigModel.EnabledPlugins 移除
                    pluginConfigModel.EnabledPlugins.Remove(pluginId);
                    // 5.保存到 plugin.config.json
                    PluginConfigModelFactory.Save(pluginConfigModel);
                }
            }
            catch (Exception ex)
            {
                Utils.LogUtil.Error(ex.ToString());
                responseData.Code = -1;
                responseData.Message = "禁用失败: 此插件不存在, 或未启用";
                return await Task.FromResult(responseData);
            }

            // 7. 尝试移除 Plugins_wwwroot/PluginId
            string pluginWwwRootDir = PluginPathProvider.PluginWwwRootDir(pluginId);
            if (Directory.Exists(pluginWwwRootDir))
            {
                Directory.Delete(pluginWwwRootDir, true);
            }


            responseData.Code = 1;
            responseData.Message = "禁用成功";
        }
        catch (Exception ex)
        {
            responseData.Code = -2;
            responseData.Message = "禁用失败: " + ex.Message;
        }

        return await Task.FromResult(responseData);
    }
    #endregion

    #region 上传插件
    /// <summary>
    /// 上传插件
    /// </summary>
    /// <param name="file">注意: 参数名一定为 file， 对应前端传过来时以 file 为名</param>
    /// <returns></returns>
    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> Upload([FromForm] IFormFile file)
    {
        BaseResponseModel responseData = new BaseResponseModel();

        #region 效验
        if (file == null)
        {
            responseData.Code = -1;
            responseData.Message = "上传的文件不能为空";
            return responseData;
        }
        //文件后缀
        string fileExtension = Path.GetExtension(file.FileName);//获取文件格式，拓展名
        // 类型标记
        UploadFileType uploadFileType = UploadFileType.NoAllowedType;
        switch (fileExtension)
        {
            case ".zip":
                uploadFileType = UploadFileType.Zip;
                break;
            case ".nupkg":
                uploadFileType = UploadFileType.Nupkg;
                break;
        }

        if (fileExtension != ".zip" && fileExtension != ".nupkg")
        {
            responseData.Code = -1;
            // nupkg 其实就是 zip
            responseData.Message = "只能上传 zip 或 nupkg 格式文件";
            return responseData;
        }
        // PluginCore.AspNetCore-v1.0.2 起 不再限制插件上传大小
        //判断文件大小
        //var fileSize = file.Length;
        //if (fileSize > 1024 * 1024 * 5) // 5M
        //{
        //    responseData.Code = -1;
        //    responseData.Message = "上传的文件不能大于5MB";
        //    return responseData;
        //}
        #endregion

        try
        {
            // 1.先上传到 临时插件上传目录, 用Guid.zip作为保存文件名
            string tempZipFilePath = Path.Combine(PluginPathProvider.TempPluginUploadDir(), Guid.NewGuid() + ".zip");
            using (var fs = System.IO.File.Create(tempZipFilePath))
            {
                file.CopyTo(fs); //将上传的文件文件流，复制到fs中
                fs.Flush();//清空文件流
            }
            // 2.解压
            bool isDecomparessSuccess = false;
            if (uploadFileType == UploadFileType.Zip)
            {
                isDecomparessSuccess = Utils.ZipHelper.DecomparessFile(tempZipFilePath, tempZipFilePath.Replace(".zip", ""),null!);
            }
            else if (uploadFileType == UploadFileType.Nupkg)
            {
                isDecomparessSuccess = NupkgService.DecomparessFile(tempZipFilePath, tempZipFilePath.Replace(".zip", ""));
            }

            // 3.删除原压缩包
            System.IO.File.Delete(tempZipFilePath);
            if (!isDecomparessSuccess)
            {
                responseData.Code = -1;
                responseData.Message = "解压插件压缩包失败";
                return responseData;
            }
            // 4.读取其中的info.json, 获取 PluginId 值
            PluginInfoModel pluginInfoModel = PluginInfoModelFactory.ReadPluginDir(tempZipFilePath.Replace(".zip", ""));
            if (pluginInfoModel == null || string.IsNullOrEmpty(pluginInfoModel.PluginId))
            {
                // 记得删除已不再需要的临时插件文件夹
                Directory.Delete(tempZipFilePath.Replace(".zip", ""), true);

                responseData.Code = -1;
                responseData.Message = "不合法的插件";
                return responseData;
            }
            string pluginId = pluginInfoModel.PluginId;
            // 5.检索 此 PluginId 是否本地插件已存在
            var pluginConfigModel = PluginConfigModelFactory.Create();
            // 本地已经存在的 PluginId
            IList<string> localExistPluginIds = PluginPathProvider.AllPluginFolderName();
            if (localExistPluginIds.Contains(pluginId))
            {
                // 记得删除已不再需要的临时插件文件夹
                Directory.Delete(tempZipFilePath.Replace(".zip", ""), true);

                responseData.Code = -1;
                responseData.Message = $"本地已有此插件 (PluginId: {pluginId}), 请前往插件列表删除后, 再上传";
                return responseData;
            }
            // 6.本地无此插件 -> 移动插件文件夹到 Plugins 下, 并以 PluginId 为插件文件夹名
            string pluginsRootPath = PluginPathProvider.PluginsRootPath();
            string newPluginDir = Path.Combine(pluginsRootPath, pluginId);
            Directory.Move(tempZipFilePath.Replace(".zip", ""), newPluginDir);

            // 7. 放入 Plugins 中, 默认为 已禁用

            responseData.Code = 1;
            responseData.Message = $"上传插件成功 (PluginId: {pluginId})";
        }
        catch (Exception ex)
        {
            responseData.Code = -1;
            responseData.Message = "上传插件失败: " + ex.Message;
            ex = ex.InnerException;
            while (ex != null)
            {
                responseData.Message += " - " + ex.InnerException.Message;
                ex = ex.InnerException;
            }
        }

        return await Task.FromResult(responseData);
    }
    #endregion

    #region 查看详细
    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> Details(string pluginId)
    {
        BaseResponseModel responseData = new BaseResponseModel();

        try
        {
            #region 效验
            pluginId = pluginId.Trim();
            var pluginConfigModel = PluginConfigModelFactory.Create();
            string[] localPluginIds = PluginPathProvider.AllPluginFolderName().ToArray();

            if (!localPluginIds.Contains(pluginId))
            {
                responseData.Code = -1;
                responseData.Message = $"查看详细失败: 不存在 {pluginId} 插件";
                return await Task.FromResult(responseData);
            }

            #endregion

            PluginInfoModel pluginInfoModel = PluginInfoModelFactory.Create(pluginId);
            string[] enablePluginIds = _pluginFinder.EnablePluginIds().ToArray();
            PluginInfoResponseModel pluginInfoResponseModel = PluginInfoModelToResponseModel(new List<PluginInfoModel>() { pluginInfoModel }, pluginConfigModel, enablePluginIds).FirstOrDefault();


            responseData.Code = 1;
            responseData.Message = "查看详细成功";
            responseData.Data = pluginInfoResponseModel;
        }
        catch (Exception ex)
        {
            responseData.Code = -1;
            responseData.Message = "查看详细失败: " + ex.Message;
        }

        return await Task.FromResult(responseData);
    }
    #endregion

    #region 查看文档
    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> Readme(string pluginId)
    {
        BaseResponseModel responseData = new BaseResponseModel();

        try
        {
            #region 效验
            pluginId = pluginId.Trim();
            // var pluginConfigModel = PluginConfigModelFactory.Create();
            string[] localPluginIds = PluginPathProvider.AllPluginFolderName().ToArray();

            if (!localPluginIds.Contains(pluginId))
            {
                responseData.Code = -1;
                responseData.Message = $"查看文档失败: 不存在 {pluginId} 插件";
                return await Task.FromResult(responseData);
            }

            #endregion

            PluginReadmeModel readmeModel = PluginReadmeModelFactory.Create(pluginId);
            PluginReadmeResponseModel readmeResponseModel = new PluginReadmeResponseModel();
            readmeResponseModel.Content = readmeModel?.Content ?? "";
            readmeResponseModel.PluginId = pluginId;

            responseData.Code = 1;
            responseData.Message = "查看文档成功";
            responseData.Data = readmeResponseModel;
        }
        catch (Exception ex)
        {
            responseData.Code = -1;
            responseData.Message = "查看文档失败: " + ex.Message;
        }

        return await Task.FromResult(responseData);
    }
    #endregion

    #region 设置
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel>> Settings(string pluginId)
    {
        BaseResponseModel responseData = new BaseResponseModel();

        try
        {
            #region 效验
            pluginId = pluginId.Trim();
            // var pluginConfigModel = PluginConfigModelFactory.Create();
            string[] localPluginIds = PluginPathProvider.AllPluginFolderName().ToArray();

            if (!localPluginIds.Contains(pluginId))
            {
                responseData.Code = -1;
                responseData.Message = $"查看设置失败: 不存在 {pluginId} 插件";
                return await Task.FromResult(responseData);
            }

            #endregion

            string settingsJsonStr = PluginSettingsModelFactory.Create(pluginId);


            responseData.Code = 1;
            responseData.Message = "查看设置成功";
            responseData.Data = settingsJsonStr ?? "无设置项";
        }
        catch (Exception ex)
        {
            responseData.Code = -1;
            responseData.Message = "查看设置失败: " + ex.Message;
        }

        return await Task.FromResult(responseData);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponseModel>> Settings(PluginSettingsInputModel inputModel)
    {
        BaseResponseModel responseData = new BaseResponseModel();

        try
        {
            #region 效验
            inputModel.PluginId = inputModel.PluginId.Trim();
            // var pluginConfigModel = PluginConfigModelFactory.Create();
            string[] localPluginIds = PluginPathProvider.AllPluginFolderName().ToArray();

            if (!localPluginIds.Contains(inputModel.PluginId))
            {
                responseData.Code = -1;
                responseData.Message = $"设置失败: 不存在 {inputModel.PluginId} 插件";
                return await Task.FromResult(responseData);
            }

            #endregion

            inputModel.Data = inputModel.Data ?? "";
            PluginSettingsModelFactory.Save(pluginSettingsJsonStr: inputModel.Data, pluginId: inputModel.PluginId);


            responseData.Code = 1;
            responseData.Message = "设置成功";
            responseData.Data = inputModel.Data;
        }
        catch (Exception ex)
        {
            responseData.Code = -1;
            responseData.Message = "设置失败: " + ex.Message;
        }

        return await Task.FromResult(responseData);
    }
    #endregion

    #endregion


    #region Helpers

    [NonAction]
    private IList<PluginInfoResponseModel> PluginInfoModelToResponseModel(IList<PluginInfoModel> pluginInfoModels, PluginConfigModel pluginConfigModel, string[] enablePluginIds)
    {
        // 获取 Plugins 下所有插件
        // DirectoryInfo pluginsDir = new DirectoryInfo(PluginPathProvider.PluginsRootPath());
        // List<string> pluginIds = pluginsDir?.GetDirectories()?.Select(m => m.Name)?.ToList() ?? new List<string>();

        IList<PluginInfoResponseModel> responseModels = new List<PluginInfoResponseModel>();
        #region 添加插件状态信息
        foreach (var model in pluginInfoModels)
        {
            PluginInfoResponseModel responseModel = new PluginInfoResponseModel();
            responseModel.Author = model.Author;
            responseModel.Description = model.Description;
            responseModel.DisplayName = model.DisplayName;
            responseModel.PluginId = model.PluginId;
            responseModel.SupportedVersions = model.SupportedVersions;
            responseModel.Version = model.Version;
            responseModel.DependPlugins = model.DependPlugins;

            if (pluginConfigModel.EnabledPlugins.Contains(model.PluginId) && !enablePluginIds.Contains(model.PluginId)) {
                // 错误情况: 配置 标识 已启用, 但实际没有启用成功
                pluginConfigModel.EnabledPlugins.Remove(model.PluginId);
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseModel.Status = PluginStatus.Disabled;
            } else if(!pluginConfigModel.EnabledPlugins.Contains(model.PluginId) && enablePluginIds.Contains(model.PluginId))
            {
                // 错误情况: 配置没有标识 已启用, 但实际 已启用
                pluginConfigModel.EnabledPlugins.Add(model.PluginId);
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseModel.Status = PluginStatus.Enabled;
            } else if (pluginConfigModel.EnabledPlugins.Contains(model.PluginId) && enablePluginIds.Contains(model.PluginId))
            {
                responseModel.Status = PluginStatus.Enabled;
            }
            else
            {
                responseModel.Status = PluginStatus.Disabled;
            }
            responseModels.Add(responseModel);
        }
        #endregion

        return responseModels;
    }

    public enum UploadFileType
    {
        NoAllowedType = 0,
        Zip = 1,
        Nupkg = 2
    }

    #endregion
}
