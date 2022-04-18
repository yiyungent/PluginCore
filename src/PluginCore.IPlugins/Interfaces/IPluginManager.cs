//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
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
