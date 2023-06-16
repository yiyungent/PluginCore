//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PluginCore.IPlugins
{
    /// <summary>
    /// 目前未有效化，占坑
    /// </summary>
    public interface IContentFilterPlugin : IPlugin
    {
        Task<string> RequestBodyFilter(string urlPath, string content);

        Task<string> ReponseBodyFilter(string urlPath, string content);
    }
}
