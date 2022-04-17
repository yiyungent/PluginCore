using PluginCore.Infrastructure;
using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace PluginCore.lmplements
{
    /// <summary>
    /// 一个插件的所有dll由 一个 <see cref="CollectibleAssemblyLoadContext"/> 管理
    /// <see cref="PluginsLoadContexts"/> 记录管理了所有 插件的<see cref="CollectibleAssemblyLoadContext"/>
    /// <see cref="PluginManager"/> 是对 <see cref="PluginsLoadContexts"/>的封装, 使其更好管理插件加载释放的行为
    /// </summary>
    public class PluginManager<TAssemblyLoadContext> : IPluginManager
        where TAssemblyLoadContext : AssemblyLoadContext
    {
        public IPluginsLoadContexts<TAssemblyLoadContext> PluginsLoadContexts { get; set; }

        public IAssemblyLoadContextPack AssemblyLoadContextPack { get; set; }

        public PluginManager(IPluginsLoadContexts<TAssemblyLoadContext> pluginsLoadContexts, IAssemblyLoadContextPack assemblyLoadContextPack)
        {
            this.PluginsLoadContexts = pluginsLoadContexts;
            this.AssemblyLoadContextPack = assemblyLoadContextPack;
        }

        /// <summary>
        /// 加载插件程序集
        /// </summary>
        /// <param name="pluginId"></param>
        public void LoadPlugin(string pluginId)
        {
            #region 方案1
            //AssemblyLoadContext context = this.AssemblyLoadContextPack.Pack(pluginId);
            //// 父类 -> 子类
            //TAssemblyLoadContext newContext = (TAssemblyLoadContext)context;
            #endregion

            #region 方案2
            TAssemblyLoadContext context = (TAssemblyLoadContext)this.AssemblyLoadContextPack.Pack(pluginId);
            #endregion

            // 这个插件加载上下文 放入 集合中
            this.PluginsLoadContexts.Add(pluginId, context);
        }

        public void UnloadPlugin(string pluginId)
        {
            this.PluginsLoadContexts.Remove(pluginId);
        }
    }
}
