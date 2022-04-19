//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubApiPlugin.ResponseModel
{
    public class CommonResponseModel
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public Object Data { get; set; }
    }
}
