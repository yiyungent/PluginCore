//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using PluginCore.Models;

namespace GitHubApiPlugin
{
    public class SettingsModel : PluginSettingsModel
    {
        /// <summary>
        /// 当GitHub授权登录完成后, 跳转到的地址
        /// </summary>
        public string HomePageUrl { get; set; }

        public GitHubOAuthModel GitHubOAuth { get; set; }


        public SettingsModel()
        {
            this.HomePageUrl = "/api/GitHub/Index";
        }

        public sealed class GitHubOAuthModel
        {
            public string AppName { get; set; }

            public string ClientId { get; set; }

            public string ClientSecret { get; set; }

            public string RedirectUrl { get; set; }

            public IList<string> Scopes { get; set; }

            public GitHubOAuthModel()
            {
                this.Scopes = new string[] { "user", "notifications", "repo" };
                this.RedirectUrl = "/api/GitHub/Callback";
            }
        }
    }
}
