//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Text;
using PluginCore.IPlugins;

namespace PluginCore.Interfaces
{
    public interface IPluginFinder
    {
        #region 实现了指定接口或类型 的启用插件

        /// <summary>
        /// 实现了指定接口或类型 的启用插件
        /// </summary>
        /// <typeparam name="TPlugin">可以是一个接口，一个抽象类，一个普通实现类, 只要实现了 <see cref="IPlugin"/>即可</typeparam>
        /// <returns></returns>
        IEnumerable<TPlugin> EnablePlugins<TPlugin>()
           where TPlugin : IPlugin; // BasePlugin
        #endregion

        #region 所有启用的插件

        /// <summary>
        /// 所有启用的插件
        /// </summary>
        /// <returns></returns>
        IEnumerable<IPlugin> EnablePlugins();
        #endregion

        #region 获取指定 pluginId 的启用插件

        /// <summary>
        /// 获取指定 pluginId 的启用插件
        /// </summary>
        /// <param name="pluginId"></param>
        /// <returns>1.插件未启用返回null, 2.找不到此插件上下文返回null 3.找不到插件主dll返回null 4.插件主dll中找不到实现了IPlugin的Type返回null, 5.无法实例化插件返回null</returns>
        IPlugin Plugin(string pluginId);
        #endregion

    }
}
