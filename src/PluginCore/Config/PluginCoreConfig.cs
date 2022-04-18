//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.Config
{
    public class PluginCoreConfig
    {
        public PluginCoreConfig()
        {
            this.Admin = new AdminModel();
        }

        public AdminModel Admin { get; set; }

        /// <summary>
        /// LocalEmbedded
        /// LocalFolder
        /// RemoteCDN
        /// </summary>
        public string FrontendMode { get; set; } = "LocalEmbedded";

        public string RemoteFrontend { get; set; } = "https://cdn.jsdelivr.net/gh/yiyungent/plugincore-admin-frontend@0.3.1/dist-cdn";

        /// <summary>
        /// 开启后:
        /// 1. 页面的 Widget 会显示插件的详细插入点
        /// </summary>
        public bool PluginWidgetDebug { get; set; } = false;

        public sealed class AdminModel
        {
            public string UserName { get; set; } = "admin";

            public string Password { get; set; } = "ABC12345";
        }

    }
}
