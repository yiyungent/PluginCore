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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using PluginCore.AspNetCore.Authorization;
using PluginCore.AspNetCore.AdminUI;
using PluginCore.Infrastructure;
using PluginCore.Interfaces;
using PluginCore.AspNetCore.Middlewares;
using PluginCore.AspNetCore.BackgroundServices;
using PluginCore.IPlugins;
using PluginCore.AspNetCore.Interfaces;
using PluginCore.lmplements;
using PluginCore.AspNetCore.lmplements;

namespace PluginCore.AspNetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class PluginCoreStartupExtensions
    {
        private static IWebHostEnvironment _webHostEnvironment;

        private static IServiceCollection _services;

        /// <summary>
        /// 若需要替换默认实现, 请在 <see cref="AddPluginCore(IServiceCollection)"/> 之前, 若在之后, 则无法影响 主程序启动时默认行为
        /// </summary>
        /// <param name="services"></param>
        public static void AddPluginCore(this IServiceCollection services)
        {
            #region 仅适用于 ASP.NET Core
            // start: 仅用于 ASP.NET Core
            // 用于添加插件Controller 时，通知Controller.Action发生变化
            services.AddSingleton<IActionDescriptorChangeProvider>(PluginActionDescriptorChangeProvider.Instance);
            services.AddSingleton(PluginActionDescriptorChangeProvider.Instance);

            services.TryAddTransient<PluginControllerManager>();
            services.TryAddTransient<IPluginControllerManager, PluginControllerManager>();

            services.TryAddTransient<PluginApplicationBuilderManager>();
            services.TryAddTransient<IPluginApplicationBuilderManager, PluginApplicationBuilderManager>();
            // end: 仅用于 ASP.NET Core 
            #endregion

            #region 通用
            services.TryAddTransient<AspNetCorePluginManager>();
            // 使用 AspNetCorePluginManager
            services.TryAddTransient<IPluginManager, AspNetCorePluginManager>();
            // 上方等同下方
            //services.TryAddTransient<IPluginManager, AspNetCorePluginManager<CollectibleAssemblyLoadContext>>();

            services.TryAddTransient<PluginFinder>();
            services.TryAddTransient<IPluginFinder, PluginFinder>();
            #endregion


            #region ASP.NET Core
            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    IPluginManager pluginManager = scope.ServiceProvider.GetService<IPluginManager>();

                    // 初始化 PluginCore 相关目录
                    PluginPathProvider.PluginsRootPath();

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
                    IPluginControllerManager pluginControllerManager = scope.ServiceProvider.GetService<IPluginControllerManager>();
                    pluginControllerManager.AddControllers(ass);


                    // IWebHostEnvironment
                    _webHostEnvironment = scope.ServiceProvider.GetService<IWebHostEnvironment>();

                }
            }
            #endregion

            #region PluginCore Admin 权限
            // 1. 先 Authentication (我是谁) 2. 再 Authorization (我能做什么)

            // fixed: https://github.com/yiyungent/PluginCore/issues/4
            // System.InvalidOperationException: No authenticationScheme was specified, and there was no DefaultChallengeScheme found. The default schemes can be set using either AddAuthentication(string defaultScheme) or AddAuthentication(Action<AuthenticationOptions> configureOptions).
            #region 添加认证 Authentication
            // 没通过 Authentication: 401 Unauthorized
            services.AddAuthentication("PluginCore.Authentication")
                .AddScheme<Authentication.PluginCoreAuthenticationSchemeOptions,
                    Authentication.PluginCoreAuthenticationHandler>("PluginCore.Authentication", "PluginCore.Authentication",
                    options => { });
            #endregion

            #region 添加授权策略-所有标记 [PluginCoreAdminAuthorize] 都需要权限检查
            services.AddSingleton<IAuthorizationHandler, PluginCoreAdminAuthorizationHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("PluginCore.Admin", policy =>
                {
                    // 无法满足 下方任何一项：HTTP 403 错误
                    // 3.需要 检查是否拥有当前请求资源的权限
                    policy.Requirements.Add(new PluginCoreAdminRequirement());
                });
            });
            #endregion


            // AccountManager
            services.AddTransient<AccountManager>();
            // HttpContext.Current
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddHttpContextAccessor(); 
            #endregion

            IPluginFinder pluginFinder = services.BuildServiceProvider().GetService<IPluginFinder>();

            #region IStartupPlugin

            var plugins = pluginFinder.EnablePlugins<IStartupPlugin>()?.OrderBy(m => m.ConfigureServicesOrder)?.ToList();

            foreach (var item in plugins)
            {
                // 调用
                Utils.LogUtil.Info($"{item.GetType().ToString()} {nameof(IStartupPlugin)}.{nameof(IStartupPlugin.ConfigureServices)}");
                item?.ConfigureServices(services);
            }

            #endregion

            // AddBackgroundServices
            services.AddBackgroundServices();

            // 一定要在最后
            _services = services;
        }

        public static IApplicationBuilder UsePluginCore(this IApplicationBuilder app)
        {
            // 一定在 PluginCore 添加的中间件中 第一个
            app.UseMiddleware<PluginHttpStartFilterMiddleware>();

            app.UsePluginCoreAdminUI();

            // 由于没办法在运行时, 动态 UseStaticFiles(), 因此不再为每一个插件都 UseStaticFiles(),
            // 而是统一在一个文件夹下, 插件启用时, 将插件的wwwroot复制到 Plugins_wwwroot/{PluginId}, 禁用时, 再删除
            string pluginwwwrootDir = PluginPathProvider.PluginsWwwRootDir();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    pluginwwwrootDir),
                RequestPath = "/Plugins"
            });


            // 发现 UseAuthentication 认证中间件重复添加, 也只会执行一次 认证
            // 但 UseAuthorization 重复添加2次, 则会执行 2次 授权
            app.UseAuthentication();
            app.UseAuthorization();

            #region Plugin Middleware
            // Plugin Middleware
            //app.UseMiddleware<PluginContentFilterMiddleware>();


            // 一定在 PluginCore 添加的中间件中 最后一个
            app.UseMiddleware<PluginHttpEndFilterMiddleware>();
            #endregion


            IPluginFinder pluginFinder = _services.BuildServiceProvider().GetService<IPluginFinder>();

            #region IStartupPlugin

            var plugins = pluginFinder.EnablePlugins<IStartupPlugin>()?.OrderBy(m => m.ConfigureOrder)?.ToList();

            foreach (var item in plugins)
            {
                // 调用
                Utils.LogUtil.PluginBehavior(item, typeof(IStartupPlugin), nameof(IStartupPlugin.Configure));

                item?.Configure(app);
            }

            #endregion


            app.UseMiddleware<PluginStartupXMiddleware>();


            #region 启动 Log
            Config.PluginCoreConfig pluginCoreConfig = Config.PluginCoreConfigFactory.Create();

            Utils.LogUtil.Info("启动成功:");
            Utils.LogUtil.Info($"前端模式: {pluginCoreConfig.FrontendMode}");
            Utils.LogUtil.Info($"注意: 更新前端模式 需要 重启站点");
            #endregion

            return app;
        }


    }
}
