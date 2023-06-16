//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace PluginCore.lmplements
{
    public class PluginContextPackV1 : IPluginContextPack
    {
        public IList<string> SkipDlls { get; set; }

        public IPluginContext Pack(string pluginId)
        {
            SkipDlls = new List<string>();

            #region 跳过1: 程序目录下 以单独dll 出现
            // 获取主程序 已经存在的（不许在再加载的 dll）
            string basePath = AppContext.BaseDirectory;
            //Console.WriteLine($"PluginCore.PluginManager: basePath: {basePath}");
            // { "Core.dll", "Domain.dll", "Framework.dll", "Services.dll", "Repositories.dll", "PluginCore.dll", ... }
            SkipDlls = new DirectoryInfo(basePath).GetFiles("*.dll").Select(m => m.Name).ToList();
            #endregion

            #region 跳过2: 打包进入1个dll 或 打包进 1个exe
            // 注意: 用户可能将 dll 打包在 一个dll中, 或打包进 exe, 因此 通过此方式 确保跳过
            // 主程序所有 位于 AssemblyLoadContext.Default
            List<string> skipAssembliesName = AssemblyLoadContext.Default.Assemblies
                .Select(m => m.GetName())
                .Select(m => m.Name).ToList();
            foreach (var name in skipAssembliesName)
            {
                this.SkipDlls.Add($"{name}.dll");
            }
            #endregion


            // 此插件的 加载上下文
            var context = new CollectibleAssemblyLoadContext(pluginId);

            // TODO:未测试 加载插件引用的dll: 方法二: 
            //AssemblyName[] referenceAssemblyNames = pluginMainAssembly.GetReferencedAssemblies();
            //foreach (var assemblyName in referenceAssemblyNames)
            //{
            //    context.LoadFromAssemblyName(assemblyName);
            //}

            // 跳过不需要加载的 dll, eg: ASP.NET Core Shared Framework, 主程序中已有dll
            string[] skipDlls = SkipDlls.ToArray(); //new string[] { "Core.dll", "Domain.dll", "Framework.dll", "Services.dll", "Repositories.dll", "PluginCore.dll" };

            // 注意: 先加载插件引用的dll, 因为可能在插件主dll的Controller中立即使用了引用的dll

            #region 加载插件引用的dll
            // 加载插件引用的dll
            // eg: xxx/Plugins/HelloWorld
            string pluginDirPath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId);
            var pluginDir = new DirectoryInfo(pluginDirPath);
            // 插件引用的所有dll (排除 主dll 和 skipDlls )
            // 注意: 主程序中已有dll 必须跳过, 应为这些默认Context中已经加载, 而如果插件Context再次加载, 则认为这两个是不同Assembly, 导致其中的Type转换失败
            // 这里简单来说，意思就是当在一个自定义LoadContext中加载程序集的时候，如果找不到这个程序集，程序会自动去默认LoadContext中查找，如果默认LoadContext中都找不到，就会返回null。
            // 这里我突然想到会不会是因为DemoPlugin1、DemoPlugin2以及主站点的AssemblyLoadContext都加载了Mystique.Core.dll程序集的缘故，虽然他们加载的是同一个程序集，但是因为LoadContext不同，所以系统认为它是2个程序集。
            // 参考： https://www.cnblogs.com/lwqlun/p/12930713.html
            var allReferenceFileInfos = pluginDir.GetFiles("*.dll").Where(p =>
            p.Name != $"{pluginId}.dll"
            &&
            !skipDlls.Contains(p.Name));
            foreach (FileInfo file in allReferenceFileInfos)
            {
                using (var sr = new StreamReader(file.OpenRead()))
                {
                    context.LoadFromStream(sr.BaseStream);
                }
            }
            #endregion

            #region 加载插件主dll
            // 插件的主dll, 不包括插件项目引用的dll
            string pluginMainDllFilePath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId, $"{pluginId}.dll");
            Assembly pluginMainAssembly;
            using (var fs = new FileStream(pluginMainDllFilePath, FileMode.Open))
            {
                // 使用此方法, 就不会导致dll被锁定
                pluginMainAssembly = context.LoadFromStream(fs);
            }
            #endregion


            return context;
        }
    }
}
