using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PluginCore.Infrastructure;

namespace PluginCore.Middlewares
{
    public class PluginStartupXMiddleware
    {
        private readonly RequestDelegate _next;

        public PluginStartupXMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IServiceProvider serviceProvider, PluginFinder pluginFinder)
        {
            bool isReachedEnd = false;
            //IApplicationBuilder applicationBuilder = new ApplicationBuilder(serviceProvider);
            IApplicationBuilder applicationBuilder = new PluginApplicationBuilder(serviceProvider, () =>
            {
                isReachedEnd = true;
            });
            var plugins = pluginFinder.EnablePlugins<PluginCore.IPlugins.IStartupXPlugin>().ToList();
            foreach (var item in plugins)
            {
                // 调用
                Utils.LogUtil.Info($"{item.GetType().ToString()} 运行时 Configure(IApplicationBuilder app) 激活管道Middleware");

                item.Configure(applicationBuilder);
            }

            try
            {
                #region Old - Use ApplicationBuilder
                // 迫不得已又得重新 UseRouting(), 不然会报错, 因为内部检查 末端Middleware
                //applicationBuilder.UseRouting();
                //applicationBuilder.UseEndpoints(configure =>
                //{
                //    //isReachedEnd = true;
                //});
                //applicationBuilder.Run(async (HttpContext httpContext) =>
                //{
                //    isReachedEnd = true;

                //    await Task.FromResult(0);
                //}); 
                #endregion

                RequestDelegate requestDelegate = applicationBuilder.Build();

                await requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                // InvalidOperationException: The request reached the end of the pipeline without executing the endpoint: 'AspNetCore3_1.Controllers.WeatherForecastController.Get (AspNetCore3_1)'. Please register the EndpointMiddleware using 'IApplicationBuilder.UseEndpoints(...)' if using routing.
                Utils.LogUtil.Error(ex.ToString());
                if (ex.InnerException != null)
                {
                    Utils.LogUtil.Error(ex.InnerException.ToString());
                }
            }

            if (isReachedEnd)
            {
                // Call the next delegate/middleware in the pipeline
                await _next(httpContext);
            }
            else
            {
                // 没有抵达 End, 说明在插件的 middleware 中已堵塞, 准备返回 响应
            }


        }

    }
}
