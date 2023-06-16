//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace PluginCore.AspNetCore.AdminUI
{
    public static class PluginCoreAdminUIBuilderExtensions
    {

        /// <summary>
        /// Register the SwaggerUI middleware with provided options
        /// </summary>
        public static IApplicationBuilder UsePluginCoreAdminUI(this IApplicationBuilder app, PluginCoreAdminUIOptions options)
        {
            #region Old - 区分
            //Config.PluginCoreConfig pluginCoreConfig = Config.PluginCoreConfigFactory.Create();

            //switch (pluginCoreConfig.FrontendMode?.ToLower())
            //{
            //    case "localembedded":
            //        app.UseMiddleware<PluginCoreAdminUIMiddleware>(options);
            //        break;
            //    case "localfolder":

            //        #region LocalFolder
            //        //string contentRootPath = Directory.GetCurrentDirectory();

            //        // https://docs.microsoft.com/zh-CN/aspnet/core/fundamentals/static-files?view=aspnetcore-5.0
            //        //var options = new DefaultFilesOptions()
            //        //{
            //        //    RequestPath = "/PluginCore/Admin",
            //        //};
            //        //// TODO: 404: 无效, 失败, 改为使用 Controller 手动指定
            //        ////options.DefaultFileNames.Add("PluginCoreAdmin/index.html");
            //        //app.UseDefaultFiles(options);

            //        // 注意: 为了无需重启Web，而更新是否本地前端配置, 因此此项保持常驻开启
            //        // 因此, 需要保证 PluginCoreAdmin 文件夹存在
            //        string pluginCoreAdminDir = PluginPathProvider.PluginCoreAdminDir();
            //        app.UseStaticFiles(new StaticFileOptions
            //        {
            //            FileProvider = new PhysicalFileProvider(
            //                 pluginCoreAdminDir),
            //            RequestPath = "/PluginCore/Admin"
            //        });
            //        #endregion

            //        break;
            //    case "remotecdn":

            //        break;
            //    default:
            //        app.UseMiddleware<PluginCoreAdminUIMiddleware>(options);
            //        break;
            //}

            //return app; 
            #endregion

            
            return app.UseMiddleware<PluginCoreAdminUIMiddleware>(options);
        }

        /// <summary>
        /// Register the SwaggerUI middleware with optional setup action for DI-injected options
        /// </summary>
        public static IApplicationBuilder UsePluginCoreAdminUI(
            this IApplicationBuilder app)
        {
            PluginCoreAdminUIOptions options = new PluginCoreAdminUIOptions()
            {

            };

            return app.UsePluginCoreAdminUI(options);
        }


    }
}
