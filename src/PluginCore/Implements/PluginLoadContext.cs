//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================

using PluginCore.Interfaces;

namespace PluginCore.Implements;

/// <summary>
/// LazyPluginLoadContext
/// </summary>
public class PluginLoadContext : LazyPluginLoadContext, IPluginContext
{
    public PluginLoadContext(string pluginId, string pluginMainDllFilePath) : base(pluginId, pluginMainDllFilePath)
    {
    }
}
