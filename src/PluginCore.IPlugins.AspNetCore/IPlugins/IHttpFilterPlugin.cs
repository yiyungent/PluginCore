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
    /// 实验性: 不确定有效, 发现问题，请 new issue
    /// </summary>
    public interface IHttpFilterPlugin : IPlugin
    {
        Task HttpEndFilter(HttpContext httpContext);

        Task HttpStartFilter(HttpContext httpContext);
    }
}
