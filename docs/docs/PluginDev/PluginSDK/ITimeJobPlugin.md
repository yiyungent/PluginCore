
## ITimeJobPlugin



```csharp
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


