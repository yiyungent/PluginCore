//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================

using System.Collections.Generic;

namespace PluginCore.Interfaces;

public interface IPluginContextManager
{
    List<IPluginContext> All();

    bool Any(string pluginId);

    void Remove(string pluginId);

    IPluginContext Get(string pluginId);

    void Add(string pluginId, IPluginContext context);
}
