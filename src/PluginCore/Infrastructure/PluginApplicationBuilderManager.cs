using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PluginCore.Interfaces;
using PluginCore.IPlugins;
using PluginCore.Middlewares;

namespace PluginCore.Infrastructure
{
    public class PluginApplicationBuilderManager
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
            PluginApplicationBuilder applicationBuilder = new PluginApplicationBuilder();
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
