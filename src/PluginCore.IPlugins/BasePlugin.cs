//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================

using System.Collections.Generic;

namespace PluginCore.IPlugins;

public abstract class BasePlugin : IPlugin
{
    /// <summary>
    /// 在启用插件之后: 这时插件Assemblies已被加载(插件上下文已load)
    /// </summary>
    /// <returns>
    /// <para>当 IsSuccess 为 False, 主程序之后会回滚插件状态: (1)unload插件上下文 (2)更新plugin.config.json,标记为禁用状态</para>
    /// </returns>
    public virtual (bool IsSuccess, string Message) AfterEnable()
    {
        return (true, "启用成功");
    }

    /// <summary>
    /// 在禁用插件之前: 这时插件Assemblies还未被释放(插件上下文未Unload)
    /// </summary>
    /// <returns>
    /// <para>只有当 IsSuccess 为 True, 主程序之后才会释放插件上下文, 并标记为已禁用</para>
    /// <para>当 IsSuccess 为 False, 主程序不会释放插件上下文，也不会标记为禁用, 插件维持原状态</para>
    /// </returns>
    public virtual (bool IsSuccess, string Message) BeforeDisable()
    {
        return (true, "禁用成功");
    }

    /// <summary>
    /// TODO: 更新未做
    /// </summary>
    /// <param name="currentVersion"></param>
    /// <param name="targetVersion"></param>
    /// <returns></returns>
    public virtual (bool IsSuccess, string Message) Update(string currentVersion, string targetVersion)
    {
        return (true, "更新成功");
    }

    public virtual void AppStart() 
    {
            
    }

    public virtual List<string> AppStartOrderDependPlugins
    {
        get
        {
            return new List<string>();
        }
    }
}
