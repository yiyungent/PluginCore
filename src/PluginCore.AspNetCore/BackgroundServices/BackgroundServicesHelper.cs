//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using Microsoft.Extensions.DependencyInjection;

namespace PluginCore.AspNetCore.BackgroundServices
{
    public static class BackgroundServicesHelper
    {
        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IHostedService), typeof(TimeBackgroundService));
            // AddHostedService: Microsoft.AspNetCore.App
            services.AddHostedService<PluginTimeJobBackgroundService>(); // 以这种方式注入就是单例

            return services;
        }
    }
}
