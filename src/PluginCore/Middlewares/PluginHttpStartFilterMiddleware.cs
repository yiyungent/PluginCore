using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PluginCore.Middlewares
{
    /// <summary>
    /// 一定在 PluginCore 添加的中间件中 第一个
    /// </summary>
    public class PluginHttpStartFilterMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly PluginFinder _pluginFinder;

        public PluginHttpStartFilterMiddleware(
            RequestDelegate next,
            IWebHostEnvironment hostingEnv,
            ILoggerFactory loggerFactory,
            PluginFinder pluginFinder)
        {
            _next = next;
            _pluginFinder = pluginFinder;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            var httpMethod = httpContext.Request.Method;
            var path = httpContext.Request.Path.Value;

            // 在请求下一个 middleware 前过滤
            await Filter(httpContext);

            // Call the next delegate/middleware in the pipeline
            await _next(httpContext);

            // middleware 回退时 过滤
        }

        private async Task Filter(HttpContext httpContext)
        {
            var plugins = this._pluginFinder.EnablePlugins<PluginCore.IPlugins.IHttpFilterPlugin>().ToList();

            foreach (var item in plugins)
            {
                // 调用
                await item?.HttpStartFilter(httpContext);
            }
        }



    }
}
