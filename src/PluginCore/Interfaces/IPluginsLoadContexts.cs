using PluginCore.lmplements;
using System;
using System.Collections.Generic;
using System.Runtime.Loader;
using System.Text;

namespace PluginCore.Interfaces
{
    //public interface IPluginsLoadContexts : IPluginsLoadContexts<CollectibleAssemblyLoadContext>
    //{
    //    // 不在这里使用默认 泛型, 尽量将 修改 统一到 IOC 容器管理 那里
    //}

    public interface IPluginsLoadContexts<TAssemblyLoadContext>
        where TAssemblyLoadContext : AssemblyLoadContext
    {
        List<TAssemblyLoadContext> All();

        bool Any(string pluginId);

        void Remove(string pluginId);

        TAssemblyLoadContext Get(string pluginId);

        void Add(string pluginId, TAssemblyLoadContext context);
    }
}
