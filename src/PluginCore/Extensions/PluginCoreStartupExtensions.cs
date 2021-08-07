using PluginCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace PluginCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class PluginCoreStartupExtensions
    {
        private static IWebHostEnvironment _webHostEnvironment;

        public static void AddPluginCore(this IServiceCollection services)
        {
            // 用于添加插件Controller 时，通知Controller.Action发生变化
            services.AddSingleton<IActionDescriptorChangeProvider>(PluginActionDescriptorChangeProvider.Instance);
            services.AddSingleton(PluginActionDescriptorChangeProvider.Instance);
            services.AddTransient<PluginControllerManager>();
            services.AddTransient<PluginManager>();
            services.AddTransient<PluginFinder>();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    PluginManager pluginManager = scope.ServiceProvider.GetService<PluginManager>();

                    // 在程序启动时加载所有 已安装的插件

                    // 获取PluginConfigModel
                    #region 获取 获取PluginConfigModel
                    PluginConfigModel pluginConfigModel = PluginConfigModelFactory.Create();
                    #endregion

                    // 已启用的插件
                    #region 加载 已启用插件的Assemblies
                    IList<string> enabledPluginIds = pluginConfigModel.EnabledPlugins;
                    foreach (var pluginId in enabledPluginIds)
                    {
                        pluginManager.LoadPlugin(pluginId);
                    }
                    #endregion



                    // 将本 Assembly 内的 Controller 添加
                    var ass = Assembly.GetExecutingAssembly();
                    PluginControllerManager pluginControllerManager = scope.ServiceProvider.GetService<PluginControllerManager>();
                    pluginControllerManager.AddControllers(ass);


                    // IWebHostEnvironment
                    _webHostEnvironment = scope.ServiceProvider.GetService<IWebHostEnvironment>();
                }
            }



        }

        public static void UsePluginCore(this IApplicationBuilder app)
        {
            //string contentRootPath = Directory.GetCurrentDirectory();

            // https://docs.microsoft.com/zh-CN/aspnet/core/fundamentals/static-files?view=aspnetcore-5.0
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(_webHostEnvironment.ContentRootPath, "PluginCoreAdmin")),
                RequestPath = "/PluginCore/Admin"
            });
        }


    }
}
