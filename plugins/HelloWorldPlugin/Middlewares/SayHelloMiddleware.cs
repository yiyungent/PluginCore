//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PluginCore.Interfaces;
using PluginCore.IPlugins;

namespace HelloWorldPlugin.Middlewares
{
    public class SayHelloMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 在 <see cref="PluginApplicationBuilder"/> Build 时, 将会 new Middleware(), 最终将所有 Middleware 包装为一个 <see cref="RequestDelegate"/>
        /// </summary>
        /// <param name="next"></param>
        public SayHelloMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="pluginFinder">测试，是否运行时添加的Middleware，是否可以依赖注入</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext, IPluginFinder pluginFinder)
        {
            // 测试: 成功
            List<IPlugin> plugins = pluginFinder.EnablePlugins()?.ToList();

            bool isMatch = false;

            isMatch = httpContext.Request.Path.Value.StartsWith("/SayHello");

            if (isMatch)
            {
                await httpContext.Response.WriteAsync($"Hello World! {DateTime.Now:yyyy-MM-dd HH:mm:ss} <br>" +
                                                      $"{httpContext.Request.Path} <br>" +
                                                      $"{httpContext.Request.QueryString.Value}", Encoding.UTF8);
            }
            else
            {
                // Call the next delegate/middleware in the pipeline
                await _next(httpContext);
            }
        }

    }
}
