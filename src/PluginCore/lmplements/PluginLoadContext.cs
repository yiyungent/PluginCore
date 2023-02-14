//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Linq;

namespace PluginCore.lmplements
{
    /// <summary>
    /// LazyPluginLoadContext
    /// </summary>
    public class PluginLoadContext : LazyPluginLoadContext, IPluginContext
    {
        public PluginLoadContext(string pluginId, string pluginMainDllFilePath) : base(pluginId, pluginMainDllFilePath)
        {
        }
    }
}
