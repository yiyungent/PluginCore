using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using Core.Common;
//using Framework.Authorization;
using PluginCore;
using PluginCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PluginCore.IPlugins;
using PluginCore.ResponseModel;

//using ResponseModel;

namespace PluginCore.Controllers
{
    [Route("api/plugincore/admin/[controller]/[action]")]
    //[WebApiAuthorize]
    [ApiController]
    public class PluginsController : ControllerBase
    {
        #region Fields
        private readonly PluginManager _pluginManager;
        private readonly PluginFinder _pluginFinder;
        #endregion

        #region Ctor
        public PluginsController(PluginManager pluginManager, PluginFinder pluginFinder)
        {
            _pluginManager = pluginManager;
            _pluginFinder = pluginFinder;
        }
        #endregion

        #region Actions

        #region 插件列表
        /// <summary>
        /// 加载插件列表
        /// </summary>
        /// <param name="status">插件状态</param>
        /// <returns></returns>
        public async Task<ActionResult<CommonResponseModel>> List(string status = "all")
        {
            CommonResponseModel responseData = new ResponseModel.CommonResponseModel();
            var pluginConfigModel = PluginConfigModelFactory.Create();

            // 获取所有插件信息
            IList<PluginInfoModel> pluginInfoModels = PluginInfoModelFactory.CreateAll();
            IList<PluginInfoResponseModel> responseModels = new List<PluginInfoResponseModel>();
            // 添加插件状态
            responseModels = PluginInfoModelToResponseModel(pluginInfoModels, pluginConfigModel);
            #region 筛选插件状态
            switch (status.ToLower())
            {
                case "all":
                    break;
                case "installed":
                    responseModels = responseModels.Where(m => m.Status == PluginStatus.Enabled || m.Status == PluginStatus.Disabled).ToList();
                    break;
                case "enabled":
                    responseModels = responseModels.Where(m => m.Status == PluginStatus.Enabled).ToList();
                    break;
                case "disabled":
                    responseModels = responseModels.Where(m => m.Status == PluginStatus.Disabled).ToList();
                    break;
                case "uninstalled":
                    responseModels = responseModels.Where(m => m.Status == PluginStatus.Uninstalled).ToList();
                    break;
                default:
                    break;
            }
            #endregion

            responseData.code = 1;
            responseData.message = "加载插件列表成功";
            responseData.data = responseModels;

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 安装插件
        public async Task<ActionResult<ResponseModel.CommonResponseModel>> Install(string pluginId)
        {
            CommonResponseModel responseData = new CommonResponseModel();
            PluginConfigModel pluginConfigModel = PluginConfigModelFactory.Create();
            // TODO: 效验
            #region 效验
            if (string.IsNullOrEmpty(pluginId))
            {
                responseData.code = -1;
                responseData.message = "安装失败, pluginId不能为空";
                return await Task.FromResult(responseData);
            }
            #endregion

            try
            {
                // 1. 从 pluginConfigModel.UninstalledPlugins 移除, 添加到 pluginConfigModel.DisabledPlugins
                pluginConfigModel.UninstalledPlugins.Remove(pluginId);
                pluginConfigModel.DisabledPlugins.Add(pluginId);
                // 2.保存到 plugin.config.json
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseData.code = 1;
                responseData.message = "安装成功";
            }
            catch (Exception ex)
            {
                responseData.code = -1;
                responseData.message = "安装失败: " + ex.Message;
                return await Task.FromResult(responseData);
            }

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 删除插件
        public async Task<ActionResult<CommonResponseModel>> Delete(string pluginId)
        {
            CommonResponseModel responseData = new CommonResponseModel();
            var pluginConfigModel = PluginConfigModelFactory.Create();
            // 效验是否存在于 已卸载插件列表
            if (!pluginConfigModel.UninstalledPlugins.Contains(pluginId))
            {
                responseData.code = -1;
                responseData.message = "删除失败: 此插件不存在, 或未卸载";
                return await Task.FromResult(responseData);
            }

            try
            {
                // 1.删除物理文件
                string pluginPath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId);
                var directory = new DirectoryInfo(pluginPath);
                directory.Delete(true);
                // 2.从 pluginConfigModel.UninstalledPlugins 移除
                pluginConfigModel.UninstalledPlugins.Remove(pluginId);
                // 3.保存到 plugin.config.json
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseData.code = 1;
                responseData.message = "删除成功";
            }
            catch (Exception ex)
            {
                responseData.code = -2;
                responseData.message = "删除失败: " + ex.Message;
            }

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 卸载插件
        public async Task<ActionResult<CommonResponseModel>> Uninstall(string pluginId)
        {
            CommonResponseModel responseData = new CommonResponseModel();
            var pluginConfigModel = PluginConfigModelFactory.Create();
            // 卸载插件 必须 先禁用插件
            #region 效验
            if (pluginConfigModel.UninstalledPlugins.Contains(pluginId))
            {
                responseData.code = -3;
                responseData.message = "卸载失败: 此插件已卸载";
                return await Task.FromResult(responseData);
            }
            if (pluginConfigModel.EnabledPlugins.Contains(pluginId))
            {
                responseData.code = -1;
                responseData.message = "卸载失败: 请先禁用此插件";
                return await Task.FromResult(responseData);
            }
            if (!pluginConfigModel.DisabledPlugins.Contains(pluginId))
            {
                responseData.code = -2;
                responseData.message = "卸载失败: 此插件不存在";
                return await Task.FromResult(responseData);
            }
            #endregion

            try
            {
                // PS:卸载插件必须先禁用插件，所以此时插件LoadContext已被移除释放(插件Assemblies已被释放), 此处不需移除LoadContext
                // 1.从 pluginConfigModel.DisabledPlugins 移除, 添加到 pluginConfigModel.UninstalledPlugins
                pluginConfigModel.DisabledPlugins.Remove(pluginId);
                pluginConfigModel.UninstalledPlugins.Add(pluginId);
                // 2.保存到 plugin.config.json
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseData.code = 1;
                responseData.message = "卸载成功";
            }
            catch (Exception ex)
            {
                responseData.code = -1;
                responseData.message = "卸载失败: " + ex.Message;
            }

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 启用插件
        public async Task<ActionResult<CommonResponseModel>> Enable(string pluginId)
        {
            CommonResponseModel responseData = new CommonResponseModel();
            var pluginConfigModel = PluginConfigModelFactory.Create();
            // 效验是否存在于 已禁用插件列表
            #region 效验
            if (!pluginConfigModel.DisabledPlugins.Contains(pluginId))
            {
                responseData.code = -1;
                responseData.message = "启用失败: 此插件不存在, 或未安装";
                return await Task.FromResult(responseData);
            }
            #endregion

            try
            {
                // 1. 创建插件程序集加载上下文, 添加到 PluginsLoadContexts
                _pluginManager.LoadPlugin(pluginId);
                // 2.从 pluginConfigModel.DisabledPlugins 移除
                pluginConfigModel.DisabledPlugins.Remove(pluginId);
                // 3. 添加到 pluginConfigModel.EnabledPlugins
                pluginConfigModel.EnabledPlugins.Add(pluginId);
                // 4.保存到 plugin.config.json
                PluginConfigModelFactory.Save(pluginConfigModel);

                // 5. 找到此插件实例
                IPlugin plugin = _pluginFinder.Plugin(pluginId);
                if (plugin == null)
                {
                    responseData.code = -1;
                    responseData.message = "启用失败: 此插件不存在, 或未安装";
                    return await Task.FromResult(responseData);
                }
                // 6.调取插件的 AfterEnable(), 插件开发者可在此回收资源
                var pluginEnableResult = plugin.AfterEnable();
                if (!pluginEnableResult.IsSuccess)
                {
                    // 7.启用不成功, 回滚插件状态: (1)释放插件上下文 (2)更新 plugin.config.json
                    _pluginManager.UnloadPlugin(pluginId);
                    // 从 pluginConfigModel.EnabledPlugins 移除
                    pluginConfigModel.EnabledPlugins.Remove(pluginId);
                    // 添加到 pluginConfigModel.DisabledPlugins
                    pluginConfigModel.DisabledPlugins.Add(pluginId);
                    // 保存到 plugin.config.json
                    PluginConfigModelFactory.Save(pluginConfigModel);

                    responseData.code = -1;
                    responseData.message = "启用失败: 来自插件的错误信息: " + pluginEnableResult.Message;
                    return await Task.FromResult(responseData);
                }

                responseData.code = 1;
                responseData.message = "启用成功";
            }
            catch (Exception ex)
            {
                responseData.code = -2;
                responseData.message = "启用失败: " + ex.Message;
            }

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 禁用插件
        public async Task<ActionResult<CommonResponseModel>> Disable(string pluginId)
        {
            CommonResponseModel responseData = new CommonResponseModel();
            var pluginConfigModel = PluginConfigModelFactory.Create();
            // 效验是否存在于 已启用插件列表
            #region 效验
            if (!pluginConfigModel.EnabledPlugins.Contains(pluginId))
            {
                responseData.code = -1;
                responseData.message = "禁用失败: 此插件不存在, 或未安装";
                return await Task.FromResult(responseData);
            }
            #endregion

            try
            {
                // 1. 找到此插件实例
                IPlugin plugin = _pluginFinder.Plugin(pluginId);
                if (plugin == null)
                {
                    responseData.code = -1;
                    responseData.message = "禁用失败: 此插件不存在, 或未启用";
                    return await Task.FromResult(responseData);
                }
                try
                {
                    // 2.调取插件的 BeforeDisable(), 插件开发者可在此回收资源
                    var pluginDisableResult = plugin.BeforeDisable();
                    if (!pluginDisableResult.IsSuccess)
                    {
                        responseData.code = -1;
                        responseData.message = "禁用失败: 来自插件的错误信息: " + pluginDisableResult.Message;
                        return await Task.FromResult(responseData);
                    }
                    // 3.移除插件对应的程序集加载上下文
                    _pluginManager.UnloadPlugin(pluginId);
                    // 4.从 pluginConfigModel.EnabledPlugins 移除
                    pluginConfigModel.EnabledPlugins.Remove(pluginId);
                    // 5. 添加到 pluginConfigModel.DisabledPlugins
                    pluginConfigModel.DisabledPlugins.Add(pluginId);
                    // 6.保存到 plugin.config.json
                    PluginConfigModelFactory.Save(pluginConfigModel);
                }
                catch (Exception ex)
                {
                    responseData.code = -1;
                    responseData.message = "禁用失败: 此插件不存在, 或未启用";
                    return await Task.FromResult(responseData);
                }


                responseData.code = 1;
                responseData.message = "禁用成功";
            }
            catch (Exception ex)
            {
                responseData.code = -2;
                responseData.message = "禁用失败: " + ex.Message;
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
        public async Task<ActionResult<CommonResponseModel>> Upload([FromForm] IFormFile file)
        {
            CommonResponseModel responseData = new CommonResponseModel();

            #region 效验
            if (file == null)
            {
                responseData.code = -1;
                responseData.message = "上传的文件不能为空";
                return responseData;
            }
            //文件后缀
            string fileExtension = Path.GetExtension(file.FileName);//获取文件格式，拓展名
            if (fileExtension != ".zip")
            {
                responseData.code = -1;
                responseData.message = "只能上传zip格式文件";
                return responseData;
            }
            //判断文件大小
            var fileSize = file.Length;
            if (fileSize > 1024 * 1024 * 5) // 5M
            {
                responseData.code = -1;
                responseData.message = "上传的文件不能大于5MB";
                return responseData;
            }
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
                bool isDecomparessSuccess = Utils.ZipHelper.DecomparessFile(tempZipFilePath, tempZipFilePath.Replace(".zip", ""));
                // 3.删除原压缩包
                System.IO.File.Delete(tempZipFilePath);
                if (!isDecomparessSuccess)
                {
                    responseData.code = -1;
                    responseData.message = "解压插件压缩包失败";
                    return responseData;
                }
                // 4.读取其中的info.json, 获取 PluginId 值
                PluginInfoModel pluginInfoModel = PluginInfoModelFactory.ReadPluginDir(tempZipFilePath.Replace(".zip", ""));
                if (pluginInfoModel == null || string.IsNullOrEmpty(pluginInfoModel.PluginId))
                {
                    // 记得删除已不再需要的临时插件文件夹
                    Directory.Delete(tempZipFilePath.Replace(".zip", ""), true);

                    responseData.code = -1;
                    responseData.message = "不合法的插件";
                    return responseData;
                }
                string pluginId = pluginInfoModel.PluginId;
                // 5.检索 此 PluginId 是否本地插件已存在
                var pluginConfigModel = PluginConfigModelFactory.Create();
                // 本地已经存在的 PluginId
                IList<string> localExistPluginIds = pluginConfigModel.EnabledPlugins.Concat(pluginConfigModel.DisabledPlugins).Concat(pluginConfigModel.UninstalledPlugins).ToList();
                if (localExistPluginIds.Contains(pluginId))
                {
                    // 记得删除已不再需要的临时插件文件夹
                    Directory.Delete(tempZipFilePath.Replace(".zip", ""), true);

                    responseData.code = -1;
                    responseData.message = $"本地已有此插件 (PluginId: {pluginId}), 请前往插件列表删除后, 再上传";
                    return responseData;
                }
                // 6.本地无此插件 -> 移动插件文件夹到 Plugins 下, 并以 PluginId 为插件文件夹名
                string pluginsRootPath = PluginPathProvider.PluginsRootPath();
                string newPluginDir = Path.Combine(pluginsRootPath, pluginId);
                Directory.Move(tempZipFilePath.Replace(".zip", ""), newPluginDir);

                // 7. 加入 PluginConfigModel.UninstalledPlugins
                pluginConfigModel.UninstalledPlugins.Add(pluginId);
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseData.code = 1;
                responseData.message = $"上传插件成功 (PluginId: {pluginId})";
            }
            catch (Exception ex)
            {
                responseData.code = -1;
                responseData.message = "上传插件失败: " + ex.Message;
            }

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 查看详细
        public async Task<ActionResult<CommonResponseModel>> Details(string pluginId)
        {
            CommonResponseModel responseData = new CommonResponseModel();

            try
            {
                var pluginConfigModel = PluginConfigModelFactory.Create();
                var allPluginConfigModels = pluginConfigModel.EnabledPlugins.Concat(pluginConfigModel.DisabledPlugins)
                        .Concat(pluginConfigModel.UninstalledPlugins).ToList();
                #region 效验

                if (!allPluginConfigModels.Contains(pluginId))
                {
                    responseData.code = -1;
                    responseData.message = $"查看详细失败: 不存在 {pluginId} 插件";
                    return await Task.FromResult(responseData);
                }

                #endregion

                PluginInfoModel pluginInfoModel = PluginInfoModelFactory.Create(pluginId);
                PluginInfoResponseModel pluginInfoResponseModel = PluginInfoModelToResponseModel(new List<PluginInfoModel>() { pluginInfoModel }, pluginConfigModel).FirstOrDefault();


                responseData.code = 1;
                responseData.message = "查看详细成功";
                responseData.data = pluginInfoResponseModel;
            }
            catch (Exception ex)
            {
                responseData.code = -1;
                responseData.message = "查看详细失败: " + ex.Message;
            }

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 查看文档
        public async Task<ActionResult<CommonResponseModel>> Readme(string pluginId)
        {
            CommonResponseModel responseData = new CommonResponseModel();

            try
            {
                var pluginConfigModel = PluginConfigModelFactory.Create();
                var allPluginConfigModels = pluginConfigModel.EnabledPlugins.Concat(pluginConfigModel.DisabledPlugins)
                    .Concat(pluginConfigModel.UninstalledPlugins).ToList();
                #region 效验

                if (!allPluginConfigModels.Contains(pluginId))
                {
                    responseData.code = -1;
                    responseData.message = $"查看文档失败: 不存在 {pluginId} 插件";
                    return await Task.FromResult(responseData);
                }

                #endregion

                PluginReadmeModel readmeModel = PluginReadmeModelFactory.Create(pluginId);
                PluginReadmeResponseModel readmeResponseModel = new PluginReadmeResponseModel();
                readmeResponseModel.Content = readmeModel?.Content ?? "";
                readmeResponseModel.PluginId = pluginId;

                responseData.code = 1;
                responseData.message = "查看文档成功";
                responseData.data = readmeResponseModel;
            }
            catch (Exception ex)
            {
                responseData.code = -1;
                responseData.message = "查看文档失败: " + ex.Message;
            }

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 设置
        [HttpGet]
        public async Task<ActionResult<CommonResponseModel>> Settings(string pluginId)
        {
            CommonResponseModel responseData = new CommonResponseModel();

            try
            {
                #region 效验
                var pluginConfigModel = PluginConfigModelFactory.Create();
                var allPluginConfigModels = pluginConfigModel.EnabledPlugins.Concat(pluginConfigModel.DisabledPlugins)
                    .Concat(pluginConfigModel.UninstalledPlugins).ToList();

                if (!allPluginConfigModels.Contains(pluginId))
                {
                    responseData.code = -1;
                    responseData.message = $"查看设置失败: 不存在 {pluginId} 插件";
                    return await Task.FromResult(responseData);
                }

                #endregion

                string settingsJsonStr = PluginSettingsModelFactory.Create(pluginId);


                responseData.code = 1;
                responseData.message = "查看设置成功";
                responseData.data = settingsJsonStr ?? "无设置项";
            }
            catch (Exception ex)
            {
                responseData.code = -1;
                responseData.message = "查看设置失败: " + ex.Message;
            }

            return await Task.FromResult(responseData);
        }

        [HttpPost]
        public async Task<ActionResult<CommonResponseModel>> Settings(PluginSettingsInputModel inputModel)
        {
            CommonResponseModel responseData = new CommonResponseModel();

            try
            {
                #region 效验
                var pluginConfigModel = PluginConfigModelFactory.Create();
                var allPluginConfigModels = pluginConfigModel.EnabledPlugins.Concat(pluginConfigModel.DisabledPlugins)
                    .Concat(pluginConfigModel.UninstalledPlugins).ToList();

                if (!allPluginConfigModels.Contains(inputModel.PluginId))
                {
                    responseData.code = -1;
                    responseData.message = $"设置失败: 不存在 {inputModel.PluginId} 插件";
                    return await Task.FromResult(responseData);
                }

                #endregion

                inputModel.Data = inputModel.Data ?? "";
                PluginSettingsModelFactory.Save(pluginSettingsJsonStr: inputModel.Data, pluginId: inputModel.PluginId);


                responseData.code = 1;
                responseData.message = "设置成功";
                responseData.data = inputModel.Data;
            }
            catch (Exception ex)
            {
                responseData.code = -1;
                responseData.message = "设置失败: " + ex.Message;
            }

            return await Task.FromResult(responseData);
        }
        #endregion

        #endregion


        #region Helpers

        [NonAction]
        private IList<PluginInfoResponseModel> PluginInfoModelToResponseModel(IList<PluginInfoModel> pluginInfoModels, PluginConfigModel pluginConfigModel)
        {
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

                if (pluginConfigModel.EnabledPlugins.Contains(model.PluginId))
                {
                    responseModel.Status = PluginStatus.Enabled;
                }
                else if (pluginConfigModel.DisabledPlugins.Contains(model.PluginId))
                {
                    responseModel.Status = PluginStatus.Disabled;
                }
                else if (pluginConfigModel.UninstalledPlugins.Contains(model.PluginId))
                {
                    responseModel.Status = PluginStatus.Uninstalled;
                }
                responseModels.Add(responseModel);
            }
            #endregion

            return responseModels;
        }

        #endregion
    }
}
