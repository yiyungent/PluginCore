//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PluginCore;
using PluginCore.IPlugins;

namespace TestTimeJobPlugin
{
    public class TestTimeJobPlugin : BasePlugin, ITimeJobPlugin
    {
        public override (bool IsSuccess, string Message) AfterEnable()
        {
            Console.WriteLine($"{nameof(TestTimeJobPlugin)}: {nameof(AfterEnable)}");
            return base.AfterEnable();
        }

        public override (bool IsSuccess, string Message) BeforeDisable()
        {
            Console.WriteLine($"{nameof(TestTimeJobPlugin)}: {nameof(BeforeDisable)}");
            return base.BeforeDisable();
        }

        public long SecondsPeriod
        {
            get
            {
                return 10;
            }
        }

        public static int Count { get; set; }

        public async Task ExecuteAsync()
        {
            SettingsModel model = PluginSettingsModelFactory.Create<SettingsModel>("TestTimeJobPlugin");
            Count++;
            model.Hello = $"{model.Hello} - {Count}";

            PluginSettingsModelFactory.Save<SettingsModel>(model, "TestTimeJobPlugin");
        }




    }
}
