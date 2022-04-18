//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using PluginCore.Infrastructure;
using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace PluginCore.lmplements
{
    /// <summary>
    /// 一个插件的所有dll由 一个 <see cref="IPluginContext"/> 管理
    /// <see cref="PluginContextManager"/> 记录管理了所有 插件的<see cref="IPluginContext"/>
    /// <see cref="PluginManager"/> 是对 <see cref="PluginContextManager"/>的封装, 使其更好管理插件加载释放的行为
    /// </summary>
    public class PluginManager : IPluginManager
    {
        public IPluginContextManager PluginContextManager { get; set; }

        public IPluginContextPack PluginContextPack { get; set; }

        public PluginManager(IPluginContextManager pluginContextManager, IPluginContextPack pluginContextPack)
        {
            this.PluginContextManager = pluginContextManager;
            this.PluginContextPack = pluginContextPack;
        }

        /// <summary>
        /// 加载插件程序集
        /// </summary>
        /// <param name="pluginId"></param>
        public void LoadPlugin(string pluginId)
        {
            IPluginContext context = this.PluginContextPack.Pack(pluginId);

            // 这个插件加载上下文 放入 集合中
            this.PluginContextManager.Add(pluginId, context);
        }

        public void UnloadPlugin(string pluginId)
        {
            this.PluginContextManager.Remove(pluginId);
        }
    }
}
