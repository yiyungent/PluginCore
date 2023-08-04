//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================


using System.Linq;
using Microsoft.AspNetCore.Http;
using PluginCore.AspNetCore.Infrastructure;
using PluginCore.AspNetCore.Interfaces;
using PluginCore.AspNetCore.Middlewares;
using PluginCore.Interfaces;
using PluginCore.IPlugins.AspNetCore.IPlugins;
using PluginCore.IPlugins.Interfaces;

namespace PluginCore.AspNetCore.Implements
{
    public class PluginApplicationBuilderManager : PluginApplicationBuilderManager<PluginApplicationBuilder>
    {
        public PluginApplicationBuilderManager(IPluginFinder pluginFinder) : base(pluginFinder)
        {
        }
    }

    public class PluginApplicationBuilderManager<TPluginApplicationBuilder> : IPluginApplicationBuilderManager
        where TPluginApplicationBuilder : PluginApplicationBuilder, new()
    {
        private readonly IPluginFinder _pluginFinder;

        public PluginApplicationBuilderManager(IPluginFinder pluginFinder)
        {
            _pluginFinder = pluginFinder;
        }

        public static RequestDelegate RequestDelegateResult { get; set; }


        /// <summary>
        /// 插件 启用, 禁用 时: 重新 Build
        /// </summary>
        public void ReBuild()
        {
            TPluginApplicationBuilder applicationBuilder = new TPluginApplicationBuilder();
            applicationBuilder.ReachEndAction = PluginStartupXMiddleware.ReachedEndAction;

            var plugins = this._pluginFinder.EnablePlugins<IStartupXPlugin>()?.OrderBy(m => m.ConfigureOrder)?.ToList();
            foreach (var item in plugins)
            {
                // 调用
                Utils.LogUtil.Info($"{item.GetType().ToString()} {nameof(IStartupXPlugin)}.{nameof(IStartupXPlugin.Configure)}");

                item.Configure(applicationBuilder);
            }

            RequestDelegateResult = applicationBuilder.Build();
        }


        public RequestDelegate GetBuildResult()
        {
            if (RequestDelegateResult == null)
            {
                ReBuild();
            }

            return RequestDelegateResult;
        }

    }
}
