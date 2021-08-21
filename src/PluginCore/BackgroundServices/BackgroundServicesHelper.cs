using Microsoft.Extensions.DependencyInjection;

namespace PluginCore.BackgroundServices
{
    public static class BackgroundServicesHelper
    {
        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IHostedService), typeof(TimeBackgroundService));
            services.AddHostedService<PluginTimeJobBackgroundService>(); // 以这种方式注入就是单例

            return services;
        }
    }
}
