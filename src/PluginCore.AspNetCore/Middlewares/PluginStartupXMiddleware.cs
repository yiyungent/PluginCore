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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PluginCore.AspNetCore.Interfaces;
using PluginCore.Infrastructure;

namespace PluginCore.AspNetCore.Middlewares
{
    public class PluginStartupXMiddleware
    {
        private readonly RequestDelegate _next;

        public PluginStartupXMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public static Action ReachedEndAction { get; set; } = () => { _isReachedEnd = true; };

        private static bool _isReachedEnd;

        public async Task InvokeAsync(HttpContext httpContext, IPluginApplicationBuilderManager pluginApplicationBuilderManager)
        {
            //bool isReachedEnd = false;
            _isReachedEnd = false;

            try
            {
                RequestDelegate requestDelegate = pluginApplicationBuilderManager.GetBuildResult();

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

            if (_isReachedEnd)
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
