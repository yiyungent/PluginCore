//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using PluginCore.IPlugins;

namespace ExamplePlugin
{

    // ☆★★★☆★★★☆★★★☆☆★★★☆★★★☆★★★☆  注意: 打包插件使用 dotnet build , 不要将 PluginCore.IPlugins 等主程序已有 dll 打包入 ExamplePlugin.zip   ★★★☆★★★☆★★★☆★★★☆

    public class ExamplePlugin : BasePlugin
    {
        public override (bool IsSuccess, string Message) AfterEnable()
        {
            Console.WriteLine($"{nameof(ExamplePlugin)}: {nameof(AfterEnable)}");
            return base.AfterEnable();
        }

        public override (bool IsSuccess, string Message) BeforeDisable()
        {
            Console.WriteLine($"{nameof(ExamplePlugin)}: {nameof(BeforeDisable)}");
            return base.BeforeDisable();
        }

    }
}
