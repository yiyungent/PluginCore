//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Threading.Tasks;
using HelloWorldPlugin.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PluginCore.IPlugins;

namespace HelloWorldPlugin
{
    public class HelloWorldPlugin : BasePlugin, IStartupXPlugin, IWidgetPlugin
    {
        public override (bool IsSuccess, string Message) AfterEnable()
        {
            Console.WriteLine($"{nameof(HelloWorldPlugin)}: {nameof(AfterEnable)}");
            return base.AfterEnable();
        }

        public override (bool IsSuccess, string Message) BeforeDisable()
        {
            Console.WriteLine($"{nameof(HelloWorldPlugin)}: {nameof(BeforeDisable)}");
            return base.BeforeDisable();
        }

        public void ConfigureServices(IServiceCollection services)
        {

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<SayHelloMiddleware>();
        }

        public int ConfigureOrder
        {
            get
            {
                return 2;
            }
        }


        public int ConfigureServicesOrder
        {
            get
            {
                return 2;
            }
        }

        public async Task<string> Widget(string widgetKey, params string[] extraPars)
        {
            string rtnStr = null;
            if (widgetKey == "PluginCore.Admin.Footer")
            {
                if (extraPars != null)
                {
                    Console.WriteLine(string.Join(",", extraPars));
                }
                rtnStr = @"<div style=""border:1px solid green;width:300px;"">
                                <h3>HelloWorldPlugin 注入</h3>
                                <div>HelloWorldPlugin 挂件</div>
                           </div>";

            }

            return await Task.FromResult(rtnStr);
        }
    }
}
