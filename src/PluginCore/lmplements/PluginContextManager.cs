//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using PluginCore.Interfaces;
using PluginCore.lmplements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;

namespace PluginCore.lmplements
{
    /// <summary>
    /// <para>启用插件时加载进来, 禁用插件时移除释放</para>
    /// <para>只有已启用的插件才有上下文</para>
    /// https://www.cnblogs.com/lwqlun/p/11395828.html
    /// 1.当加载插件的时候，我们需要将当前插件的程序集加载上下文放到_pluginContexts字典中。字典的key是插件的名称，字典的value是插件的程序集加载上下文。
    /// 2.当移除一个插件的时候，我们需要使用Unload方法，来释放当前的程序集加载上下文。
    /// </summary>
    public class PluginContextManager : IPluginContextManager
    {
        #region Fields

        private static int _newCount;

        //private Dictionary<string, IPluginContext>
        //    _pluginContexts;

        private Dictionary<string, IPluginContext> _pluginContexts
        {
            get
            {
                return PluginContextStore.PluginContexts;
            }
        }

        #endregion

        #region Ctor
        public PluginContextManager()
        {
            //_pluginContexts = new Dictionary<string, IPluginContext>();

            #region 计数
            _newCount++;
            if (_newCount > 1)
            {
#if DEBUG
                Utils.LogUtil.Error($"警告: {nameof(PluginContextManager)} 被 new {_newCount} 次");
#endif
            }
            #endregion
        }
        #endregion

        #region Methods

        public List<IPluginContext> All()
        {
            return _pluginContexts.Select(p => p.Value).ToList();
        }

        public bool Any(string pluginId)
        {
            return _pluginContexts.ContainsKey(pluginId);
        }

        public void Remove(string pluginId)
        {
            if (_pluginContexts.ContainsKey(pluginId))
            {
                _pluginContexts[pluginId].Unload();
                _pluginContexts.Remove(pluginId);
            }
        }

        public IPluginContext Get(string pluginId)
        {
            return _pluginContexts[pluginId];
        }

        public void Add(string pluginId, IPluginContext context)
        {
            _pluginContexts.Add(pluginId, context);
        }
        #endregion

    }

    /// <summary>
    /// fixed: 由于 <see cref="IPluginContextManager"/> 单例失败, 因此临时解决方案
    /// </summary>
    public static class PluginContextStore
    {
        public static Dictionary<string, IPluginContext> PluginContexts = new Dictionary<string, IPluginContext>();
    }



}
