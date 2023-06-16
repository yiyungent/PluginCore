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

namespace PluginCore.Config
{
    public class PluginCoreConfigFactory
    {
        private const string FileName = "PluginCore.Config.json";

        #region 即时读取
        public static PluginCoreConfig Create()
        {
            PluginCoreConfig pluginCoreConfig = new PluginCoreConfig();
            string pluginCoreConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", FileName);
            string pluginCoreConfigJsonStr = string.Empty;
            if (!File.Exists(pluginCoreConfigFilePath))
            {
                // 不存在, 则新建初始化默认
                pluginCoreConfigJsonStr = JsonSerializer.Serialize(pluginCoreConfig);
                File.WriteAllText(pluginCoreConfigFilePath, pluginCoreConfigJsonStr, Encoding.UTF8);

                return pluginCoreConfig;
            }

            pluginCoreConfigJsonStr = File.ReadAllText(pluginCoreConfigFilePath, Encoding.UTF8);
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.PropertyNameCaseInsensitive = true;
            pluginCoreConfig = JsonSerializer.Deserialize<PluginCoreConfig>(pluginCoreConfigJsonStr, jsonSerializerOptions);

            return pluginCoreConfig;
        }
        #endregion

        #region 保存
        public static void Save(PluginCoreConfig pluginCoreConfig)
        {
            if (pluginCoreConfig == null)
            {
                throw new ArgumentNullException(nameof(pluginCoreConfig));
            }
            try
            {
                string pluginCoreConfigJsonStr = JsonSerializer.Serialize(pluginCoreConfig);
                string pluginCoreConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", FileName);
                File.WriteAllText(pluginCoreConfigFilePath, pluginCoreConfigJsonStr, Encoding.UTF8);
            }
            catch (Exception ex)
            { }

        }
        #endregion


    }
}
