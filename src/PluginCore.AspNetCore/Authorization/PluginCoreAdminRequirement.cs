//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================

using Microsoft.AspNetCore.Authorization;

namespace PluginCore.AspNetCore.Authorization;

public class PluginCoreAdminRequirement : IAuthorizationRequirement
{
    public PluginCoreAdminRequirement()
    {

    }
}
