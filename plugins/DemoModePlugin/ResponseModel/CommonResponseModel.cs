//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DemoModePlugin.ResponseModel
{
    public class CommonResponseModel
    {
        public int code { get; set; }

        public string message { get; set; }

        public Object data { get; set; }
    }
}
