//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
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
