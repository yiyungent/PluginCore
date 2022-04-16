using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PluginCore.AspNetCore.Interfaces
{
    public interface IPluginControllerManager
    {
        void AddControllers(Assembly assembly);

        void RemoveControllers(string pluginId);
    }
}
