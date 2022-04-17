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
    /// <see cref="AspNetCorePluginManagerV1"/> 是对 <see cref="PluginsLoadContexts"/>的封装, 使其更好管理插件加载释放的行为
    /// </summary>
    public class AspNetCorePluginManager : IPluginManager
    {
        private readonly IPluginControllerManager _pluginControllerManager;

        public IPluginContextManager PluginsLoadContexts { get; set; }

        public IPluginContextPack AssemblyLoadContextPack { get; set; }

        public AspNetCorePluginManager(IPluginContextManager pluginsLoadContexts, IPluginContextPack assemblyLoadContextPack, IPluginControllerManager pluginControllerManager)
        {
            this.PluginsLoadContexts = pluginsLoadContexts;
            this.AssemblyLoadContextPack = assemblyLoadContextPack;
            _pluginControllerManager = pluginControllerManager;
        }

        /// <summary>
        /// 加载插件程序集
        /// </summary>
        /// <param name="pluginId"></param>
        public void LoadPlugin(string pluginId)
        {
            IPluginContext context = this.AssemblyLoadContextPack.Pack(pluginId);
            Assembly pluginMainAssembly = context.LoadFromAssemblyName(new AssemblyName(pluginId));

            // 加载其中的控制器
            _pluginControllerManager.AddControllers(pluginMainAssembly);

            // 这个插件加载上下文 放入 集合中
            this.PluginsLoadContexts.Add(pluginId, context);
        }

        public void UnloadPlugin(string pluginId)
        {
            this.PluginsLoadContexts.Remove(pluginId);

            // 移除其中的控制器
            _pluginControllerManager.RemoveControllers(pluginId);
        }

    }
}
