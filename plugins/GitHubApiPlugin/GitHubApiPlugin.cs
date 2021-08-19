using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PluginCore.IPlugins;

namespace GitHubApiPlugin
{
    public class GitHubApiPlugin : BasePlugin, IStartupPlugin
    {

        public GitHubApiPlugin(IServiceProvider serviceProvider)
        {

        }

        public override (bool IsSuccess, string Message) AfterEnable()
        {
            Console.WriteLine($"{nameof(GitHubApiPlugin)}: {nameof(AfterEnable)}");
            return base.AfterEnable();
        }

        public override (bool IsSuccess, string Message) BeforeDisable()
        {
            Console.WriteLine($"{nameof(GitHubApiPlugin)}: {nameof(BeforeDisable)}");
            return base.BeforeDisable();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Microsoft.AspNetCore.Session
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".GitHubApiPlugin.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(60 * 60 * 24);
                options.Cookie.IsEssential = true;
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            // Microsoft.AspNetCore.Session
            app.UseSession();
        }
    }
}
