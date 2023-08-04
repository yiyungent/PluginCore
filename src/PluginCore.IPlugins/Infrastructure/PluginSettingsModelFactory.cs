//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================


using System;
using System.IO;
using System.Text;
using PluginCore.IPlugins.Models;

namespace PluginCore.IPlugins.Infrastructure;

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
        catch
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
        catch
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
        catch
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
        catch
        { }

    }
    #endregion

    #region 格式化JSON字符串
    private static string ConvertJsonString(string str)
    {
        //格式化json字符串
        #region 使用Newtonsoft.Json格式化 JSON字符串
        //Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
        //TextReader tr = new StringReader(str);
        //Newtonsoft.Json.JsonTextReader jtr = new Newtonsoft.Json.JsonTextReader(tr);
        //object obj = serializer.Deserialize(jtr);
        //if (obj != null)
        //{
        //    StringWriter textWriter = new StringWriter();
        //    Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(textWriter)
        //    {
        //        Formatting = Newtonsoft.Json.Formatting.Indented,
        //        Indentation = 4,
        //        IndentChar = ' '
        //    };
        //    serializer.Serialize(jsonWriter, obj);
        //    return textWriter.ToString();
        //}
        //else
        //{
        //    return str;
        //} 
        #endregion

        #region 使用 System.Text.Json 格式化 JSON字符串
        // https://blog.csdn.net/essity/article/details/84644510
        System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions();
        // 设置支持中文的unicode编码: 这样就不会自动转码，而是原样展现
        options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        // 启用缩进设置
        options.WriteIndented = true;

        // 注意: object 不会丢失json数据, 但不能使用 dynamic, 会报编译错误
        object jsonObj = System.Text.Json.JsonSerializer.Deserialize<object>(str);

        // Error CS0656  Missing compiler required member 'Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create' 
        // dynamic jsonObj = System.Text.Json.JsonSerializer.Deserialize<dynamic>(str);

        string rtnStr = System.Text.Json.JsonSerializer.Serialize(jsonObj, options);

        return rtnStr;
        #endregion
    }
    #endregion

}
