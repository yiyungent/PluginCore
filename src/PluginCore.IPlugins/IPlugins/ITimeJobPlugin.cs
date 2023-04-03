//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================


using System.Threading.Tasks;

namespace PluginCore.IPlugins.IPlugins;

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
