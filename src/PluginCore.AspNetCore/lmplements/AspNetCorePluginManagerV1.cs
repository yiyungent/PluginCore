//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using PluginCore.AspNetCore.Interfaces;
using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using PluginCore.lmplements;
using PluginCore.Infrastructure;

namespace PluginCore.AspNetCore.lmplements
{

    // ********* 虽然 看上去和 AspNetCorePluginManager 一样, 但特别保留, 防止需要不一样的处理, 后续更新维护方便

    /// <summary>
    /// 一个插件的所有dll由 一个 <see cref="IPluginContext"/> 管理
    /// <see cref="PluginContextManager"/> 记录管理了所有 插件的<see cref="CollectibleAssemblyLoadContext"/>
    /// <see cref="AspNetCorePluginManagerV1"/> 是对 <see cref="PluginContextManager"/>的封装, 使其更好管理插件加载释放的行为
    /// </summary>
    public class AspNetCorePluginManagerV1 : IPluginManager
    {
        private readonly IPluginControllerManager _pluginControllerManager;

        public IPluginContextManager PluginContextManager { get; set; }

        public IPluginContextPack PluginContextPack { get; set; }

        public AspNetCorePluginManagerV1(IPluginContextManager pluginContextManager, IPluginContextPack pluginContextPack, IPluginControllerManager pluginControllerManager)
        {
            this.PluginContextManager = pluginContextManager;
            this.PluginContextPack = pluginContextPack;
            _pluginControllerManager = pluginControllerManager;
        }

        /// <summary>
        /// 加载插件程序集
        /// </summary>
        /// <param name="pluginId"></param>
        public void LoadPlugin(string pluginId)
        {
            // 此插件的 加载上下文
            IPluginContext context = this.PluginContextPack.Pack(pluginId);

            // TODO: 注意: 未测试: 不清除 对于 旧版加载 dll 方式, 再结合 LoadFromAssemblyName 是否有效
            Assembly pluginMainAssembly = context.LoadFromAssemblyName(new AssemblyName(pluginId));
            // 加载其中的控制器
            _pluginControllerManager.AddControllers(pluginMainAssembly);

            // 这个插件加载上下文 放入 集合中
            this.PluginContextManager.Add(pluginId, context);
        }

        public void UnloadPlugin(string pluginId)
        {
            this.PluginContextManager.Remove(pluginId);

            // 移除其中的控制器
            _pluginControllerManager.RemoveControllers(pluginId);
        }
    }
}
