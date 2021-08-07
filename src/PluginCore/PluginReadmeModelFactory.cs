using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using PluginCore.Models;

namespace PluginCore
{
    /// <summary>
    /// TODO: 目前这样读取出来的包含了 windows 换行符 \r\n
    /// </summary>
    public class PluginReadmeModelFactory
    {
        // TODO: Linux 文件名下区分大小写, windows不区分, 目前必须为 README.md
        private const string ReadmeFile = "README.md";

        #region 即时读取
        public static PluginReadmeModel Create(string pluginId)
        {
            PluginReadmeModel readmeModel = new PluginReadmeModel();
            string pluginDir = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId);
            string pluginReadmeFilePath = Path.Combine(pluginDir, ReadmeFile);

            if (!File.Exists(pluginReadmeFilePath))
            {
                return null;
            }
            try
            {
                string readmeStr = File.ReadAllText(pluginReadmeFilePath, Encoding.UTF8);
                readmeModel.PluginId = pluginId;
                readmeModel.Content = readmeStr;
            }
            catch (Exception ex)
            {
                readmeModel = null;
            }

            return readmeModel;
        }
        #endregion
    }
}
