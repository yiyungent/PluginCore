//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
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
    public class PluginFinder : IPluginFinder
    {
        #region Fields

        /// <summary>
        /// 用来解析插件构造函数需要的服务
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// TODO: 未使用
        /// Key: 实现了 IPlugin 的插件类
        /// Value: 此插件类 允许的 行为(众多实现了 IPlugin 的钩子接口)
        /// </summary>
        private static ConcurrentDictionary<Type, List<Type>> _pluginAllowedBehavior;

        #endregion

        public IPluginContextManager PluginContextManager { get; set; }

        #region Ctor
        public PluginFinder(IPluginContextManager pluginContextManager, IServiceProvider serviceProvider)
        {
            this.PluginContextManager = pluginContextManager;
            _serviceProvider = serviceProvider;
        }

        static PluginFinder()
        {
            // TODO: 初始化: 默认为信任模式: 插件允许所有行为
            _pluginAllowedBehavior = new ConcurrentDictionary<Type, List<Type>>();
        }
        #endregion


        #region 允许激活行为的 启用插件
        /// <summary>
        /// TODO: 实现了指定接口或类型 的启用插件 并且 允许激活此行为(接口)
        ///
        /// <para>TODO: 考虑后，还是没办法限制住插件恶意行为, 因为无法限制插件 修改配置文件 </para>
        /// </summary>
        /// <typeparam name="TPlugin"></typeparam>
        /// <returns></returns>
        public IEnumerable<TPlugin> ActivatedPlugins<TPlugin>()
            where TPlugin : IPlugin
        {
            var enablePlugins = EnablePlugins<TPlugin>();
            foreach (var plugin in enablePlugins)
            {
                // TODO: 检查是否允许 此插件行为


                yield return plugin;
            }
        }
        #endregion

        #region 实现了指定接口或类型 的启用插件
        /// <summary>
        /// 实现了指定接口或类型 的启用插件
        /// </summary>
        /// <typeparam name="TPlugin">可以是一个接口，一个抽象类，一个普通实现类, 只要实现了 <see cref="IPlugin"/>即可</typeparam>
        /// <returns></returns>
        public IEnumerable<TPlugin> EnablePlugins<TPlugin>()
            where TPlugin : IPlugin // BasePlugin
        {
            // TODO: 目前这里还有问题, 不应该写为 BasePlugin, 不利于扩展, 不利于插件开发者自己实现 Install , Uninstall等

            // 1.所有启用的插件 PluginId
            var pluginConfigModel = PluginConfigModelFactory.Create();
            IList<string> enablePluginIds = pluginConfigModel.EnabledPlugins;
            foreach (var pluginId in enablePluginIds)
            {
                if (this.PluginContextManager.Any(pluginId))
                {
                    // 2.找到插件对应的Context
                    var context = this.PluginContextManager.Get(pluginId);
                    // 3.找插件 主 Assembly
                    // Assembly.FullName: HelloWorld, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
                    Assembly pluginMainAssembly = context.Assemblies.Where(m => m.FullName.StartsWith($"{pluginId}, Version=")).FirstOrDefault();
                    if (pluginMainAssembly == null)
                    {
                        continue;
                    }
                    // 4.从插件主Assembly中 找实现了 TPlugin 接口的 Type, 若有多个，只要一个
                    Type pluginType = pluginMainAssembly.ExportedTypes.Where(m =>
                        (m.BaseType == typeof(TPlugin) || m.GetInterfaces().Contains(typeof(TPlugin)))
                        &&
                        !m.IsInterface
                        &&
                        !m.IsAbstract
                    ).FirstOrDefault();
                    if (pluginType == null)
                    {
                        continue;
                    }
                    // 5.实例化插件 Type
                    //(TPlugin)Activator.CreateInstance(pluginType,);
                    //try to resolve plugin as unregistered service
                    //object instance = EngineContext.Current.ResolveUnregistered(pluginType);
                    object instance = ResolveUnregistered(pluginType);
                    //try to get typed instance
                    TPlugin typedInstance = (TPlugin)instance;
                    if (typedInstance == null)
                    {
                        continue;
                    }

                    yield return typedInstance;
                }
            }

        }
        #endregion

        #region 所有启用的插件
        /// <summary>
        /// 所有启用的插件
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IPlugin> EnablePlugins()
        {
            return EnablePlugins<IPlugin>();
        }
        #endregion

        #region 获取指定 pluginId 的启用插件
        /// <summary>
        /// 获取指定 pluginId 的启用插件
        /// </summary>
        /// <param name="pluginId"></param>
        /// <returns>1.插件未启用返回null, 2.找不到此插件上下文返回null 3.找不到插件主dll返回null 4.插件主dll中找不到实现了IPlugin的Type返回null, 5.无法实例化插件返回null</returns>
        public IPlugin Plugin(string pluginId)
        {
            // 1.所有启用的插件 PluginId
            var pluginConfigModel = PluginConfigModelFactory.Create();
            IList<string> enablePluginIds = pluginConfigModel.EnabledPlugins;
            // 插件未启用返回null
            if (!enablePluginIds.Contains(pluginId))
            {
                return null;
            }

            // 找不到此插件上下文返回null
            if (!this.PluginContextManager.Any(pluginId))
            {
                return null;
            }

            // 2.找到插件对应的Context
            var context = this.PluginContextManager.Get(pluginId);
            // 3.找插件 主 Assembly
            // Assembly.FullName: HelloWorld, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            Assembly pluginMainAssembly = context.Assemblies.Where(m => m.FullName.StartsWith($"{pluginId}, Version=")).FirstOrDefault();
            // 找不到插件主dll返回null
            if (pluginMainAssembly == null)
            {
                return null;
            }
            // 4.从插件主Assembly中 找实现了 TPlugin 接口的 Type, 若有多个，只要一个
            Type pluginType = pluginMainAssembly.ExportedTypes.Where(m =>
                (m.BaseType == typeof(IPlugin) || m.GetInterfaces().Contains(typeof(IPlugin)))
                &&
                !m.IsInterface
                &&
                !m.IsAbstract
            ).FirstOrDefault();
            // 插件主dll中找不到实现了IPlugin的Type返回null
            if (pluginType == null)
            {
                return null;
            }
            // 5.实例化插件 Type
            //(TPlugin)Activator.CreateInstance(pluginType,);
            //try to resolve plugin as unregistered service
            //object instance = EngineContext.Current.ResolveUnregistered(pluginType);
            object instance = ResolveUnregistered(pluginType);
            //try to get typed instance
            IPlugin typedInstance = (IPlugin)instance;
            // 无法实例化插件返回null
            if (typedInstance == null)
            {
                return null;
            }

            return typedInstance;
        }
        #endregion

        #region 获取未IOC注册的类型实例
        /// <summary>
        /// 获取未IOC注册的类型实例
        /// <para>此类型的构造函数可以依赖注入, 将通过ASP.NET Core 自带依赖注入系统进行注入</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual object ResolveUnregistered(Type type)
        {

            Exception innerException = null;
            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    //try to resolve constructor parameters
                    var parameters = constructor.GetParameters().Select(parameter =>
                    {
                        //var service = Resolve(parameter.ParameterType);
                        var service = _serviceProvider.GetService(parameter.ParameterType);
                        if (service == null)
                            throw new Exception("Unknown dependency");
                        return service;
                    });

                    //all is ok, so create instance
                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (Exception ex)
                {
                    innerException = ex;
                }
            }

            throw new Exception("No constructor was found that had all the dependencies satisfied.", innerException);

        }
        #endregion

    }
}
