using PluginCore.lmplements;
using System;
using System.Collections.Generic;
using System.Runtime.Loader;
using System.Text;

namespace PluginCore.Interfaces
{
    public interface IPluginContextManager
    {
        List<IPluginContext> All();

        bool Any(string pluginId);

        void Remove(string pluginId);

        IPluginContext Get(string pluginId);

        void Add(string pluginId, IPluginContext context);
    }
}
