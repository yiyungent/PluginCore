using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PluginCore.Models;


namespace PluginCore
{
    public class PluginSettingsModelFactory
    {
        // TODO: Linux 文件名下区分大小写, windows不区分, 目前必须为 README.md
        private const string SettingsFile = "settings.json";

        #region 即时读取
        public static T Create<T>
            (string pluginId)
            where T : PluginSettingsModel
        {
            PluginSettingsModel rtnModel = new PluginSettingsModel();
            string pluginDir = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId);
            string pluginSettingsFilePath = Path.Combine(pluginDir, SettingsFile);

            if (!File.Exists(pluginSettingsFilePath))
            {
                return null;
            }
            try
            {
                string settingsStr = File.ReadAllText(pluginSettingsFilePath, Encoding.UTF8);
                rtnModel = System.Text.Json.JsonSerializer.Deserialize<T>(settingsStr);
            }
            catch (Exception ex)
            {
                rtnModel = null;
            }

            return rtnModel as T;
        }

        public static string Create
            (string pluginId)
        {
            string rtnStr = string.Empty;
            string pluginDir = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId);
            string pluginSettingsFilePath = Path.Combine(pluginDir, SettingsFile);

            if (!File.Exists(pluginSettingsFilePath))
            {
                return null;
            }
            try
            {
                rtnStr = File.ReadAllText(pluginSettingsFilePath, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                rtnStr = null;
            }

            return rtnStr;
        }
        #endregion

        #region 保存
        public static void Save<T>(T pluginSettingsModel, string pluginId)
        where T : PluginSettingsModel
        {
            if (pluginSettingsModel == null)
            {
                throw new ArgumentNullException(nameof(pluginSettingsModel));
            }
            try
            {
                string pluginSettingsJsonStr = System.Text.Json.JsonSerializer.Serialize<T>(pluginSettingsModel);
                string pluginSettingsFilePath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId, SettingsFile);
                //File.WriteAllText(pluginSettingsFilePath, pluginSettingsJsonStr, Encoding.UTF8);
                // 写的时候加缩进
                File.WriteAllText(pluginSettingsFilePath, ConvertJsonString(pluginSettingsJsonStr), Encoding.UTF8);
            }
            catch (Exception ex)
            { }

        }

        public static void Save(string pluginSettingsJsonStr, string pluginId)
        {
            if (pluginSettingsJsonStr == null)
            {
                throw new ArgumentNullException(nameof(pluginSettingsJsonStr));
            }
            try
            {
                string pluginSettingsFilePath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId, SettingsFile);
                //File.WriteAllText(pluginSettingsFilePath, pluginSettingsJsonStr, Encoding.UTF8);
                // 写的时候加缩进
                File.WriteAllText(pluginSettingsFilePath, ConvertJsonString(pluginSettingsJsonStr), Encoding.UTF8);
            }
            catch (Exception ex)
            { }

        }
        #endregion

        #region 使用Newtonsoft.Json格式化JSON文档
        private static string ConvertJsonString(string str)
        {
            //格式化json字符串
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            TextReader tr = new StringReader(str);
            Newtonsoft.Json.JsonTextReader jtr = new Newtonsoft.Json.JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(textWriter)
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
        #endregion

    }
}
