//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Runtime.Loader;
using System.Text;

namespace PluginCore.Interfaces
{
    public interface IPluginContextPack
    {
        /// <summary>
        /// 将 此插件 打包 到一个 <see cref="IPluginContext"/> 中
        /// </summary>
        /// <param name="pluginId"></param>
        /// <returns></returns>
        IPluginContext Pack(string pluginId);
    }
}
