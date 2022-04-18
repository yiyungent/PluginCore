//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PluginCore.IPlugins
{
    public interface IWidgetPlugin : IPlugin
    {
        // 这种方式限定: 不合适, 一个插件, 可能需要注入多个地方
        //string WidgetKey { get; }

        Task<string> Widget(string widgetKey, params string[] extraPars);
    }
}
