//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using Microsoft.AspNetCore.Http;
using PluginCore;
using PluginCore.IPlugins;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace HexoPlugin
{
    public class HexoPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HexoPlugin(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override (bool IsSuccess, string Message) AfterEnable()
        {
            Console.WriteLine($"{nameof(HexoPlugin)}: {nameof(AfterEnable)}");
            return base.AfterEnable();
        }

        public override (bool IsSuccess, string Message) BeforeDisable()
        {
            Console.WriteLine($"{nameof(HexoPlugin)}: {nameof(BeforeDisable)}");
            return base.BeforeDisable();
        }

        public Task<string> Widget(string widgetKey, params string[] extraPars)
        {
            string rtn = string.Empty;
            if (_httpContextAccessor?.HttpContext?.Request != null)
            {
                _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("origin", out Microsoft.Extensions.Primitives.StringValues originValue);
                SettingsModel settingsModel = PluginSettingsModelFactory.Create<SettingsModel>("HexoPlugin");
                string[] origins = settingsModel.Origins?.Select(m => m.Trim())?.ToArray();
                if (origins == null || origins.Length <= 0)
                {
                    return Task.FromResult("");
                }
                if (origins.Contains(originValue.ToString().Trim()))
                {
                    switch (widgetKey)
                    {
                        case "Hexo.Head":
                            // 1.
                            rtn += settingsModel.Head;

                            // 2.
                            string tokenJsonFilePath = System.IO.Path.Combine(PluginPathProvider.PluginsRootPath(), "HexoPlugin", "token.json");
                            string tokenJsonStr = System.IO.File.ReadAllText(tokenJsonFilePath, System.Text.Encoding.UTF8);
                            // 注意: 不要再对 tokenJson 使用 JSON.stringify
                            rtn += $@"<script>
                                        var storage = window.localStorage;
                                        var storageName = 'hexo-encrypt-token'; 
                                        storage.setItem(storageName, `{tokenJsonStr}`);
                                      </script>";

                            // 3.
                            string headFilePath = System.IO.Path.Combine(PluginPathProvider.PluginsRootPath(), "HexoPlugin", "head.html");
                            string headStr = System.IO.File.ReadAllText(headFilePath, System.Text.Encoding.UTF8);
                            rtn += headStr;

                            break;
                        case "Hexo.Footer":
                            // 1.
                            rtn += settingsModel.Footer;

                            // 2.
                            string footerFilePath = System.IO.Path.Combine(PluginPathProvider.PluginsRootPath(), "HexoPlugin", "footer.html");
                            string footerStr = System.IO.File.ReadAllText(footerFilePath, System.Text.Encoding.UTF8);
                            rtn += footerStr;

                            break;
                        default:
                            break;
                    }
                }
            }

            return Task.FromResult(rtn);
        }
    }
}
