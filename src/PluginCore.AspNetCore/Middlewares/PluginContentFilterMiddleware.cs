//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PluginCore.Interfaces;

namespace PluginCore.AspNetCore.Middlewares
{
    /// <summary>
    /// TODO: 未测试
    /// </summary>
    public class PluginContentFilterMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IPluginFinder _pluginFinder;

        public PluginContentFilterMiddleware(
            RequestDelegate next,
            IWebHostEnvironment hostingEnv,
            ILoggerFactory loggerFactory,
            IPluginFinder pluginFinder)
        {
            _next = next;
            _pluginFinder = pluginFinder;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            var httpMethod = httpContext.Request.Method;
            var path = httpContext.Request.Path.Value;

            // 在请求下一个 middleware 前过滤
            await RequestBodyFilter(httpContext);

            // Call the next delegate/middleware in the pipeline
            await _next(httpContext);

            if (httpMethod == "GET" && path.EndsWith(".css") || path.EndsWith(".js") || path.EndsWith(".html"))
            {
                // middleware 回退时 过滤
                await ResponseBodyFilter(httpContext);
            }
        }

        private async Task RequestBodyFilter(HttpContext httpContext)
        {
            string content = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                // Response.Body
                await httpContext.Request.Body.CopyToAsync(memoryStream);

                long pos = httpContext.Request.Body.Position;

                using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    content = await reader.ReadToEndAsync();
                }
            }

            var plugins = this._pluginFinder.EnablePlugins<PluginCore.IPlugins.IContentFilterPlugin>().ToList();

            foreach (var item in plugins)
            {
                // 调用
                content = await item?.RequestBodyFilter(httpContext.Request.Path.Value, content);
            }

            // 更新 Request.Body

            #region 方式1
            //var requestStream = new MemoryStream();
            //using (StreamWriter writer = new StreamWriter(requestStream, Encoding.UTF8))
            //{
            //    await writer.WriteAsync(content);
            //}
            //httpContext.Request.Body = requestStream;
            #endregion


            #region 方式2
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            await httpContext.Request.Body.WriteAsync(buffer, 0, buffer.Length);
            #endregion
        }

        private async Task ResponseBodyFilter(HttpContext httpContext)
        {
            string content = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                // Response.Body
                await httpContext.Response.Body.CopyToAsync(memoryStream);

                long pos = httpContext.Response.Body.Position;

                using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    content = await reader.ReadToEndAsync();
                }
            }

            var plugins = this._pluginFinder.EnablePlugins<PluginCore.IPlugins.IContentFilterPlugin>().ToList();

            foreach (var item in plugins)
            {
                // 调用
                content = await item?.ReponseBodyFilter(httpContext.Request.Path.Value, content);
            }

            // 更新 ResponseBody

            #region 方式1
            //var responseStream = new MemoryStream();
            //using (StreamWriter writer = new StreamWriter(responseStream, Encoding.UTF8))
            //{
            //    await writer.WriteAsync(content);
            //}
            //httpContext.Response.Body = responseStream;
            #endregion


            #region 方式2
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            await httpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            #endregion
        }

    }
}
