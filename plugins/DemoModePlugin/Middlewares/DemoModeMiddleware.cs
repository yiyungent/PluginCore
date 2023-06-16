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
using DemoModePlugin.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using PluginCore.Interfaces;
using PluginCore.IPlugins;

namespace DemoModePlugin.Middlewares
{
    public class DemoModeMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 在 <see cref="PluginApplicationBuilder"/> Build 时, 将会 new Middleware(), 最终将所有 Middleware 包装为一个 <see cref="RequestDelegate"/>
        /// </summary>
        /// <param name="next"></param>
        public DemoModeMiddleware(RequestDelegate next)
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
            CommonResponseModel responseModel = new CommonResponseModel();

            #region 特殊可用

            isMatch = httpContext.Request.Path.Value.StartsWith("/api/plugincore/admin/Plugins/Disable", StringComparison.OrdinalIgnoreCase)
                      || httpContext.Request.Path.Value.StartsWith("/api/plugincore/admin/Plugins/Enable", StringComparison.OrdinalIgnoreCase)
                      || httpContext.Request.Path.Value.StartsWith("/api/plugincore/admin/Plugins/Uninstall", StringComparison.OrdinalIgnoreCase)
                      || httpContext.Request.Path.Value.StartsWith("/api/plugincore/admin/Plugins/Install", StringComparison.OrdinalIgnoreCase);
            if (isMatch)
            {
                KeyValuePair<string, StringValues> pluginIdPair = httpContext.Request.Query
                    .FirstOrDefault(m => m.Key.Equals("pluginId", StringComparison.OrdinalIgnoreCase));
                string pluginId = pluginIdPair.Value.FirstOrDefault();
                if (!string.IsNullOrEmpty(pluginId) && pluginId == "HelloWorldPlugin")
                {
                    await _next(httpContext);
                    return;
                }
            }

            #endregion

            // 注意: 忽略大小写
            #region 禁用
            isMatch = httpContext.Request.Path.Value.StartsWith("/api/plugincore/admin/Plugins/Upload", StringComparison.OrdinalIgnoreCase);
            if (isMatch)
            {
                responseModel.code = -1;
                responseModel.message = "演示模式: 禁止上传插件";
                string jsonStr = System.Text.Json.JsonSerializer.Serialize(responseModel);

                await httpContext.Response.WriteAsync(jsonStr, Encoding.UTF8);
                return;
            }
            isMatch = httpContext.Request.Path.Value.StartsWith("/api/plugincore/admin/Plugins/Uninstall", StringComparison.OrdinalIgnoreCase);
            if (isMatch)
            {
                responseModel.code = -1;
                responseModel.message = "演示模式: 禁止卸载插件";
                string jsonStr = System.Text.Json.JsonSerializer.Serialize(responseModel);

                await httpContext.Response.WriteAsync(jsonStr, Encoding.UTF8);
                return;
            }
            isMatch = httpContext.Request.Path.Value.StartsWith("/api/plugincore/admin/Plugins/Disable", StringComparison.OrdinalIgnoreCase);
            if (isMatch)
            {
                responseModel.code = -1;
                responseModel.message = "演示模式: 禁止禁用插件";
                string jsonStr = System.Text.Json.JsonSerializer.Serialize(responseModel);

                await httpContext.Response.WriteAsync(jsonStr, Encoding.UTF8);
                return;
            }
            isMatch = httpContext.Request.Path.Value.StartsWith("/api/plugincore/admin/Plugins/Delete", StringComparison.OrdinalIgnoreCase);
            if (isMatch)
            {
                responseModel.code = -1;
                responseModel.message = "演示模式: 禁止删除插件";
                string jsonStr = System.Text.Json.JsonSerializer.Serialize(responseModel);

                await httpContext.Response.WriteAsync(jsonStr, Encoding.UTF8);
                return;
            }
            isMatch = httpContext.Request.Path.Value.StartsWith("/api/plugincore/admin/Plugins/Settings", StringComparison.OrdinalIgnoreCase);
            string httpMethod = httpContext.Request.Method;
            if (isMatch && httpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                responseModel.code = -1;
                responseModel.message = "演示模式: 禁止设置插件";
                string jsonStr = System.Text.Json.JsonSerializer.Serialize(responseModel);

                await httpContext.Response.WriteAsync(jsonStr, Encoding.UTF8);
                return;
            }
            isMatch = httpContext.Request.Path.Value.StartsWith("/api/plugincore/admin/User/Update", StringComparison.OrdinalIgnoreCase);
            if (isMatch)
            {
                responseModel.code = -1;
                responseModel.message = "演示模式: 禁止修改用户";
                string jsonStr = System.Text.Json.JsonSerializer.Serialize(responseModel);

                await httpContext.Response.WriteAsync(jsonStr, Encoding.UTF8);
                return;
            }
            #endregion


            // Call the next delegate/middleware in the pipeline
            await _next(httpContext);
        }

    }
}

