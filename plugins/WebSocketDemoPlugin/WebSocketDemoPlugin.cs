using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PluginCore.IPlugins;

namespace WebSocketDemoPlugin
{
    public class WebSocketDemoPlugin : BasePlugin, IStartupXPlugin
    {
        public override (bool IsSuccess, string Message) AfterEnable()
        {
            Console.WriteLine($"{nameof(WebSocketDemoPlugin)}: {nameof(AfterEnable)}");
            return base.AfterEnable();
        }

        public override (bool IsSuccess, string Message) BeforeDisable()
        {
            Console.WriteLine($"{nameof(WebSocketDemoPlugin)}: {nameof(BeforeDisable)}");
            return base.BeforeDisable();
        }

        public void ConfigureServices(IServiceCollection services)
        {

        }

        public void Configure(IApplicationBuilder app)
        {

        }

        public int ConfigureOrder
        {
            get
            {
                return 2;
            }
        }

        public int ConfigureServicesOrder
        {
            get
            {
                return 2;
            }
        }
    }
}
