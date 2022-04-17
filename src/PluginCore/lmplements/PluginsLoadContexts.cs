using PluginCore.Interfaces;
using PluginCore.lmplements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;

namespace PluginCore.lmplements
{
    /// <summary>
    /// 由 IOC 来维护 单例
    /// </summary>
    //public class PluginsLoadContexts : PluginsLoadContexts<CollectibleAssemblyLoadContext>
    //{
    //    // 不在这里使用默认 泛型, 尽量将 修改 统一到 IOC 容器管理 那里
    //}

    /// <summary>
    /// <para>启用插件时加载进来, 禁用插件时移除释放</para>
    /// <para>只有已启用的插件才有上下文</para>
    /// https://www.cnblogs.com/lwqlun/p/11395828.html
    /// 1.当加载插件的时候，我们需要将当前插件的程序集加载上下文放到_pluginContexts字典中。字典的key是插件的名称，字典的value是插件的程序集加载上下文。
    /// 2.当移除一个插件的时候，我们需要使用Unload方法，来释放当前的程序集加载上下文。
    /// </summary>
    public class PluginsLoadContexts<TAssemblyLoadContext> : IPluginsLoadContexts<TAssemblyLoadContext>
        where TAssemblyLoadContext : AssemblyLoadContext
    {
        #region Fields

        private Dictionary<string, TAssemblyLoadContext>
            _pluginContexts;

        #endregion

        #region Ctor
        public PluginsLoadContexts()
        {
            _pluginContexts = new Dictionary<string, TAssemblyLoadContext>();
        }
        #endregion

        #region Methods

        public List<TAssemblyLoadContext> All()
        {
            return _pluginContexts.Select(p => p.Value).ToList();
        }

        public bool Any(string pluginId)
        {
            return _pluginContexts.ContainsKey(pluginId);
        }

        public void Remove(string pluginId)
        {
            if (_pluginContexts.ContainsKey(pluginId))
            {
                _pluginContexts[pluginId].Unload();
                _pluginContexts.Remove(pluginId);
            }
        }

        public TAssemblyLoadContext Get(string pluginId)
        {
            return _pluginContexts[pluginId];
        }

        public void Add(string pluginId, TAssemblyLoadContext context)
        {
            _pluginContexts.Add(pluginId, context);
        }
        #endregion

    }
}
