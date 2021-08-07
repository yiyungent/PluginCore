using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginCore
{
    /// <summary>
    /// <para>启用插件时加载进来, 禁用插件时移除释放</para>
    /// <para>只有已启用的插件才有上下文</para>
    /// https://www.cnblogs.com/lwqlun/p/11395828.html
    /// 1.当加载插件的时候，我们需要将当前插件的程序集加载上下文放到_pluginContexts字典中。字典的key是插件的名称，字典的value是插件的程序集加载上下文。
    /// 2.当移除一个插件的时候，我们需要使用Unload方法，来释放当前的程序集加载上下文。
    /// </summary>
    public static class PluginsLoadContexts
    {
        #region Fields

        private static Dictionary<string, CollectibleAssemblyLoadContext>
            _pluginContexts; 

        #endregion

        #region Ctor
        static PluginsLoadContexts()
        {
            _pluginContexts = new Dictionary<string, CollectibleAssemblyLoadContext>();
        }
        #endregion

        #region Methods
        public static List<CollectibleAssemblyLoadContext> All()
        {
            return _pluginContexts.Select(p => p.Value).ToList();
        }

        public static bool Any(string pluginId)
        {
            return _pluginContexts.ContainsKey(pluginId);
        }

        public static void Remove(string pluginId)
        {
            if (_pluginContexts.ContainsKey(pluginId))
            {
                _pluginContexts[pluginId].Unload();
                _pluginContexts.Remove(pluginId);
            }
        }

        public static CollectibleAssemblyLoadContext Get(string pluginId)
        {
            return _pluginContexts[pluginId];
        }

        public static void Add(string pluginId,
             CollectibleAssemblyLoadContext context)
        {
            _pluginContexts.Add(pluginId, context);
        } 
        #endregion

    }
}
