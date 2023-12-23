//===================================================
//  License: GNU LGPLv3
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.Models
{
    /// <summary>
    /// 所有插件的配置信息模型
    /// <para>对应 WebApi/App_Data/plugin.config.json</para>
    /// <para>-------------</para>
    /// <para> Plugins = 已启用 + 已禁用 </para>
    /// <para> 上传放入 Plugins 后, 默认为 已禁用 </para>
    /// </summary>
    public class PluginConfigModel
    {
        /// <summary>
        /// 启用的插件列表: PluginID
        /// <para>属于 插件 已安装</para>
        /// </summary>
        public List<string> EnabledPlugins { get; set; }

        #region ctor
        public PluginConfigModel()
        {
            this.EnabledPlugins = new List<string>();
        }
        #endregion
    }
}

