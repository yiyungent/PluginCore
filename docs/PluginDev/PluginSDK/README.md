# 插件SDK

> 插件开发 - 插件SDK


## IStartupPlugin

```C#
namespace PluginCore.IPlugins
{
    /// <summary>
    /// 实验阶段
    /// <para>无法热插拔: 需要启用插件后, 再 重启站点</para>
    /// </summary>
    public interface IStartupPlugin : IPlugin
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        void ConfigureServices(IServiceCollection services);

        int ConfigureServicesOrder { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        void Configure(IApplicationBuilder app);

        int ConfigureOrder { get; }
    }
}
```



## IStartupXPlugin

```C#
namespace PluginCore.IPlugins
{
    /// <summary>
    /// 实验阶段
    /// <para>热插拔: 已有效化</para>
    /// </summary>
    public interface IStartupXPlugin : IPlugin
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// <para>未有效化</para>
        /// </summary>
        /// <param name="services"></param>
        void ConfigureServices(IServiceCollection services);


        int ConfigureServicesOrder { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        void Configure(IApplicationBuilder app);

        int ConfigureOrder { get; }
    }
}
```


## ITimeJobPlugin

```C#
namespace PluginCore.IPlugins
{
    /// <summary>
    /// 定时任务
    /// </summary>
    public interface ITimeJobPlugin : IPlugin
    {
        /// <summary>
        /// 间隔秒数
        /// </summary>
        long SecondsPeriod { get; }

        Task ExecuteAsync();
    }
}
```