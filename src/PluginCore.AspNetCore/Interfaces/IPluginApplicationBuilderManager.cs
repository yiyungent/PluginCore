//===================================================
//  License: GNU LGPLv3
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.AspNetCore.Interfaces
{
    public interface IPluginApplicationBuilderManager
    {
        void ReBuild();

        RequestDelegate GetBuildResult();
    }
}

