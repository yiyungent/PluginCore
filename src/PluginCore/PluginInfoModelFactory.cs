using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using PluginCore.Models;

namespace PluginCore
{
    public class PluginInfoModelFactory
    {
        private const string InfoJson = "info.json";

        #region 即时读取指定 plugin info.json
        public static PluginInfoModel Create(string pluginId)
        {
            PluginInfoModel pluginInfoModel = new PluginInfoModel();
            string pluginDir = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId);
            string pluginInfoFilePath = Path.Combine(pluginDir, InfoJson);

            if (!File.Exists(pluginInfoFilePath))
            {
                return null;
            }
            try
            {
                string pluginInfoJsonStr = File.ReadAllText(pluginInfoFilePath, Encoding.UTF8);
                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
                jsonSerializerOptions.PropertyNameCaseInsensitive = true;
                pluginInfoModel = JsonSerializer.Deserialize<PluginInfoModel>(pluginInfoJsonStr, jsonSerializerOptions);
            }
            catch (Exception ex)
            {
                pluginInfoModel = null;
            }

            return pluginInfoModel;
        }
        #endregion

        #region 即时读取插件目录下所有 plugin info.json
        public static IList<PluginInfoModel> CreateAll()
        {
            IList<PluginInfoModel> pluginInfoModels = new List<PluginInfoModel>();
            IList<string> pluginDirs = PluginPathProvider.AllPluginDir();
            foreach (var dir in pluginDirs)
            {
                // 从 dir 中解析出 pluginId
                // 约定: 插件文件夹名=PluginID=插件主.dll
                string pluginId = PluginPathProvider.GetPluginFolderNameByDir(dir);
                PluginInfoModel model = Create(pluginId);
                pluginInfoModels.Add(model);
            }
            // 去除为 null: 目标插件信息不存在，或者格式错误的
            pluginInfoModels = pluginInfoModels.Where(m => m != null).ToList();

            return pluginInfoModels;
        }
        #endregion

        #region 从指定插件目录读取插件信息
        /// <summary>
        /// 从指定插件目录读取插件信息
        /// 可以用于读取临时插件上传目录中的插件信息
        /// </summary>
        /// <param name="pluginDir"></param>
        /// <returns></returns>
        public static PluginInfoModel ReadPluginDir(string pluginDir)
        {
            PluginInfoModel pluginInfoModel = new PluginInfoModel();
            string pluginInfoFilePath = Path.Combine(pluginDir, InfoJson);

            if (!File.Exists(pluginInfoFilePath))
            {
                return null;
            }
            try
            {
                string pluginInfoJsonStr = File.ReadAllText(pluginInfoFilePath, Encoding.UTF8);
                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
                jsonSerializerOptions.PropertyNameCaseInsensitive = true;
                pluginInfoModel = JsonSerializer.Deserialize<PluginInfoModel>(pluginInfoJsonStr, jsonSerializerOptions);
            }
            catch (Exception ex)
            {
                pluginInfoModel = null;
            }

            return pluginInfoModel;
        }  
        #endregion

    }

    
}
