using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.Interfaces
{
    public interface IPluginManager
    {
        void LoadPlugin(string pluginId);

        void UnloadPlugin(string pluginId);
    }
}
