//===================================================
//  License: GNU LGPLv3
//  Contributors: yiyungent@gmail.com
//  Project: https://yiyungent.github.io/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.Utils
{
    public class RuntimeUtil
    {
        public static Version RuntimeNetVersion
        {
            get
            {
                return Environment.Version;
            }
        }
    }
}


