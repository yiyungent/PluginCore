//===================================================
//  License: GNU LGPLv3
//  Contributors: yiyungent@gmail.com
//  Project: https://yiyungent.github.io/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace PluginCore.AspNetCore.Authorization
{
    /// <summary>
    /// 注意: PluginCoreAdmin -> PluginCore.Admin
    /// </summary>
    public class PluginCoreAdminAuthorizeAttribute : AuthorizeAttribute
    {
        // public PluginCoreAdminAuthorizeAttribute() : base("PluginCore.Admin")
        public PluginCoreAdminAuthorizeAttribute() : base(IPlugins.Constants.AspNetCoreAuthorizationPolicyName)
        {

        }
    }
}


