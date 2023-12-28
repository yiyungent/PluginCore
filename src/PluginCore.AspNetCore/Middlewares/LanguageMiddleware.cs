//===================================================
//  License: GNU LGPLv3
//  Contributors: yiyungent@gmail.com
//  Project: https://yiyungent.github.io/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PluginCore.IPlugins;

namespace PluginCore.AspNetCore.Middlewares;

public class LanguageMiddleware
{
    private readonly RequestDelegate _next;

    public LanguageMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        // 从 Cookie 中获取语言标识
        string language = httpContext.Request.Cookies[Constants.AspNetCoreLanguageCookieName];

        // 存储当前语言到 HttpContext.Items
        httpContext.Items[Constants.AspNetCoreLanguageKey] = language;

        // 调用下一个中间件
        await _next(httpContext);
    }
}

public static class LanguageMiddlewareExtensions
{
    public static IApplicationBuilder UseLanguageMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LanguageMiddleware>();
    }
}
