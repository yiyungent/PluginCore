//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PluginCore.AspNetCore.Extensions;

[assembly: HostingStartup(typeof(PluginCore.AspNetCore.Infrastructure.PluginCoreHostingStartup))]
namespace PluginCore.AspNetCore.Infrastructure
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/platform-specific-configuration?view=aspnetcore-5.0
    /// </summary>
    public class PluginCoreHostingStartup : IHostingStartup
    {
        public PluginCoreHostingStartup()
        {

        }

        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureAppConfiguration(config =>
            //{

            //});

            // 注意: 无论是通过 Program.cs 中 webBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "PluginCore");
            // 还是 通过环境变量 指定承载启动程序集, 都是先执行 外部的承载启动程序集, 再执行主程序的 Startup.cs, 因此在这时, 有些 service 还没有注册

            // TODO: 不知道, 重复 Add, Use 会导致什么, 没有做防止重复
            builder.ConfigureServices(services =>
            {
                // fixed: https://github.com/yiyungent/PluginCore/issues/1
                // System.InvalidOperationException: 'Unable to resolve service for type 'Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPartManager'
                // TODO: 不确定, 这样是否可行, 事实上之后主程序还会 Add 一次, 不知道是否会导致存在多个 实例
                // 失败: 不是一个实例, 导致无法改变 Controller
                //Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPartManager applicationPartManager =
                //    new Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPartManager();
                //services.AddSingleton<Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPartManager>(applicationPartManager);

                services.AddPluginCore();
            });

            builder.Configure(app =>
            {
                app.UsePluginCore();
            });
        }
    }
}
