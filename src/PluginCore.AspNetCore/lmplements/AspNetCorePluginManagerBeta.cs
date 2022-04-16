using PluginCore.AspNetCore.Interfaces;
using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using PluginCore.lmplements;
using PluginCore.Infrastructure;

namespace PluginCore.AspNetCore.lmplements
{
    /// <summary>
    /// 一个插件的所有dll由 一个 <see cref="CollectibleAssemblyLoadContext"/> 管理
    /// <see cref="PluginsLoadContexts"/> 记录管理了所有 插件的<see cref="CollectibleAssemblyLoadContext"/>
    /// <see cref="AspNetCorePluginManager"/> 是对 <see cref="PluginsLoadContexts"/>的封装, 使其更好管理插件加载释放的行为
    /// </summary>
    public class AspNetCorePluginManagerBeta : IPluginManager
    {
        private readonly IPluginControllerManager _pluginControllerManager;

        public AspNetCorePluginManagerBeta(IPluginControllerManager pluginControllerManager)
        {
            _pluginControllerManager = pluginControllerManager;
        }

        /// <summary>
        /// 加载插件程序集
        /// </summary>
        /// <param name="pluginId"></param>
        public void LoadPlugin(string pluginId)
        {

            #region 加载插件主dll

            // 插件的主dll, 不包括插件项目引用的dll
            string pluginMainDllFilePath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId, $"{pluginId}.dll");
            // 此插件的 加载上下文
            var context = new PluginLoadContext(pluginMainDllFilePath);
            Assembly pluginMainAssembly;
            // 微软文档推荐 LoadFromAssemblyName
            pluginMainAssembly = context.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginMainDllFilePath)));
            // 加载其中的控制器
            _pluginControllerManager.AddControllers(pluginMainAssembly);

            #region 第2种方法: 未在这种情况下测试
            //using (var fs = new FileStream(pluginMainDllFilePath, FileMode.Open))
            //{
            //    // 使用此方法, 就不会导致dll被锁定
            //    pluginMainAssembly = context.LoadFromStream(fs);

            //    // 加载其中的控制器
            //    _pluginControllerManager.AddControllers(pluginMainAssembly);
            //} 
            #endregion

            #endregion


            // 这个插件加载上下文 放入 集合中
            PluginsLoadContexts<PluginLoadContext>.Add(pluginId, context);
        }

        public void UnloadPlugin(string pluginId)
        {
            PluginsLoadContexts<PluginLoadContext>.Remove(pluginId);

            // 移除其中的控制器
            _pluginControllerManager.RemoveControllers(pluginId);
        }

    }
}
