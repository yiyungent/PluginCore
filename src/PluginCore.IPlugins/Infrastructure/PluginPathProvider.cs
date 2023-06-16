//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PluginCore
{
    public class PluginPathProvider
    {

        static PluginPathProvider()
        {
            // 初始化插件目录
            string appDataDir = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
            if (!Directory.Exists(appDataDir))
            {
                Directory.CreateDirectory(appDataDir);
            }

            string pluginConfigJsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "plugin.config.json");
            string pluginConfigJson = "{\"EnabledPlugins\":[],\"DisabledPlugins\":[],\"UninstalledPlugins\":[]}";
            if (!File.Exists(pluginConfigJsonFilePath))
            {
                File.WriteAllText(pluginConfigJsonFilePath, pluginConfigJson, System.Text.Encoding.UTF8);
            }

            string tempPluginUploadDir = TempPluginUploadDir();
            if (!Directory.Exists(tempPluginUploadDir))
            {
                Directory.CreateDirectory(tempPluginUploadDir);
            }

            string pluginsDir = PluginsRootPath();
            if (!Directory.Exists(pluginsDir))
            {
                Directory.CreateDirectory(pluginsDir);
            }

            string pluginsWwwRootDir = PluginsWwwRootDir();
            if (!Directory.Exists(pluginsWwwRootDir))
            {
                Directory.CreateDirectory(pluginsWwwRootDir);
            }

            string pluginCoreAdminDir = PluginCoreAdminDir();
            if (!Directory.Exists(pluginCoreAdminDir))
            {
                Directory.CreateDirectory(pluginCoreAdminDir);
            }
        }

        /// <summary>
        /// 临时插件上传目录路径
        /// eg: F:\Com\me\Repos\Remember.Core\src\Presentation\WebApi\App_Data\TempPluginUpload
        /// </summary>
        /// <returns></returns>
        public static string TempPluginUploadDir()
        {
            string tempPluginUploadDir = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "TempPluginUpload");
            return tempPluginUploadDir;
        }

        /// <summary>
        /// 获取 Plugins 的路径
        /// </summary>
        /// <returns></returns>
        public static string PluginsRootPath()
        {
            string pluginRootPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");

            return pluginRootPath;
        }

        /// <summary>
        /// 获取目标插件文件夹名
        /// </summary>
        /// <param name="pluginDir">目标插件完整目录路径</param>
        /// <returns></returns>
        public static string GetPluginFolderNameByDir(string pluginDir)
        {
            string pluginRootPath = PluginsRootPath();
            string pluginFolderName = pluginDir.Replace(pluginRootPath + Path.DirectorySeparatorChar, "");

            return pluginFolderName;
        }

        /// <summary>
        /// 所有插件的完整目录路径
        /// </summary>
        /// <returns></returns>
        public static IList<string> AllPluginDir()
        {
            string pluginRootPath = PluginsRootPath();
            string[] pluginDirs = Directory.GetDirectories(pluginRootPath, "*");

            return pluginDirs;
        }

        /// <summary>
        /// 所有插件的文件夹名
        /// </summary>
        /// <returns></returns>
        public static IList<string> AllPluginFolderName()
        {
            IList<string> pluginFolderNames = new List<string>();
            IList<string> pluginDirs = AllPluginDir();
            foreach (var dir in pluginDirs)
            {
                string pluginFolderName = GetPluginFolderNameByDir(dir);
                pluginFolderNames.Add(pluginFolderName);
            }

            return pluginFolderNames;
        }

        /// <summary>
        /// Plugins/{pluginId}/wwwroot
        /// </summary>
        /// <returns></returns>

        public static string WwwRootDir(string pluginId)
        {
            string wwwrootDir = Path.Combine(PluginsRootPath(), pluginId, "wwwroot");

            return wwwrootDir;
        }

        /// <summary>
        /// Plugins/{currentPluginId}/wwwroot
        /// </summary>
        /// <returns></returns>
        //public static string CurrentWwwRootDir()
        //{
        //    string wwwrootDir = Path.Combine(PluginsRootPath(), CurrentPluginId(), "wwwroot");

        //    return wwwrootDir;
        //}


        /// <summary>
        /// Plugins_wwwroot
        /// </summary>
        /// <returns></returns>
        public static string PluginsWwwRootDir()
        {
            string pluginsWwwRootDir = Path.Combine(Directory.GetCurrentDirectory(), "Plugins_wwwroot");

            return pluginsWwwRootDir;
        }

        /// <summary>
        /// Plugins_wwwroot/pluginId
        /// </summary>
        /// <param name="pluginId"></param>
        /// <returns></returns>
        public static string PluginWwwRootDir(string pluginId)
        {
            string pluginWwwRootDir = Path.Combine(PluginsWwwRootDir(), pluginId);

            return pluginWwwRootDir;
        }

        /// <summary>
        /// Plugins_wwwroot/currentPluginId
        /// </summary>
        /// <returns></returns>
        //public static string CurrentPluginWwwRootDir()
        //{
        //    string pluginId = CurrentPluginId();

        //    string pluginWwwRootDir = PluginWwwRootDir(pluginId);

        //    return pluginWwwRootDir;
        //}

        public static string PluginCoreAdminDir()
        {
            string pluginCoreAdminDir = Path.Combine(Directory.GetCurrentDirectory(), "PluginCoreAdmin");

            return pluginCoreAdminDir;
        }


        /// <summary>
        /// 规范:
        /// PluginId = 插件程序集名 (PluginId.dll)
        ///
        /// TODO: 无法获取当前 PluginId
        /// </summary>
        /// <returns></returns>
        //public static string CurrentPluginId()
        //{
        //    //var ass = Assembly.GetExecutingAssembly(); // 错误: PluginCore.IPlugins
        //    //var ass = Assembly.GetCallingAssembly(); // 错误: PluginCore.IPlugins
        //    //var ass = Assembly.GetEntryAssembly(); // 错误: AspNetCore3_1
            

        //    string pluginId = ass.GetName().Name;

        //    return pluginId;
        //}


    }
}
