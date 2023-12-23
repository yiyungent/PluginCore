//===================================================
//  License: GNU LGPLv3
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PluginCore.Infrastructure;
using PluginCore.Interfaces;
using PluginCore.IPlugins;

namespace PluginCore.lmplements
{

    /// <summary>
    /// 插件发现者: 找启用的插件(1.plugin.config.json中启用 2. 有插件上下文)
    /// TODO: 其实是没必要再效验plugin.config.json的，因为只有启用的插件才有上下文, 为了保险，暂时这么做
    /// 注意: 这意味着一个启用的插件需同时满足这两个条件
    /// </summary>
    /// <remarks>
    /// 依赖解析: IServiceScopeFactory
    /// </remarks>
    public class PluginFinder : PluginFinderV2
    {
        public PluginFinder(IPluginContextManager pluginContextManager, IServiceScopeFactory serviceScopeFactory) : base(pluginContextManager, serviceScopeFactory)
        {
        }
    }
}

