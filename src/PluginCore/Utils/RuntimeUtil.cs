//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================

using System;

namespace PluginCore.Utils;

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
