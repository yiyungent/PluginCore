//===================================================
//  License: GNU LGPLv3
//  Contributors: yiyungent@gmail.com
//  Project: https://yiyungent.github.io/PluginCore
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


