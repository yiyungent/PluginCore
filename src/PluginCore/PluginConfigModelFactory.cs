using System.Linq;
//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using PluginCore.Models;

namespace PluginCore
{
    public class PluginConfigModelFactory
    {
        #region 即时读取
        public static PluginConfigModel Create()
        {
            PluginConfigModel pluginConfigModel = new PluginConfigModel();
            string pluginConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "plugin.config.json");
            string pluginConfigJsonStr = File.ReadAllText(pluginConfigFilePath, Encoding.UTF8);
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.PropertyNameCaseInsensitive = true;
            pluginConfigModel = JsonSerializer.Deserialize<PluginConfigModel>(pluginConfigJsonStr, jsonSerializerOptions);
            pluginConfigModel = EnabledPluginsSort(pluginConfigModel);

            return pluginConfigModel;
        } 
        #endregion

        #region 保存
        public static void Save(PluginConfigModel pluginConfigModel)
        {
            if (pluginConfigModel == null)
            {
                throw new ArgumentNullException(nameof(pluginConfigModel));
            }
            try
            {
                pluginConfigModel = EnabledPluginsSort(pluginConfigModel);
                string pluginConfigJsonStr = JsonSerializer.Serialize(pluginConfigModel);
                string pluginConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "plugin.config.json");
                File.WriteAllText(pluginConfigFilePath, pluginConfigJsonStr, Encoding.UTF8);
            }
            catch (Exception ex)
            { }

        } 
        #endregion

        #region 确保建立正确的依赖顺序
        public static PluginConfigModel EnabledPluginsSort(PluginConfigModel pluginConfigModel) {
            var dependencySorter = new PluginCore.Utils.DependencySorter<string>();
            dependencySorter.AddObjects(pluginConfigModel.EnabledPlugins.ToArray());
            foreach (var plugin in pluginConfigModel.EnabledPlugins)
            {
                try
                {
                    IList<string> dependPlugins = PluginInfoModelFactory.Create(plugin).DependPlugins;
                    if (dependPlugins != null && dependPlugins.Count >= 1) {
                        dependencySorter.SetDependencies(obj: plugin, dependsOnObjects: dependPlugins.ToArray());
                    }
                }
                catch (System.Exception ex)
                {
                }
            }
            try
            {
                var sortedPlugins = dependencySorter.Sort(); 
                if (sortedPlugins != null && sortedPlugins.Length >= 1) {
                    pluginConfigModel.EnabledPlugins = sortedPlugins;
                }
            }
            catch (System.Exception ex)
            {
            }

            return pluginConfigModel;
        }
        #endregion
    }
}
