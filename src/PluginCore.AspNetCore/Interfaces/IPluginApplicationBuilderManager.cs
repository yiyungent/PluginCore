//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================

using Microsoft.AspNetCore.Http;

namespace PluginCore.AspNetCore.Interfaces;

public interface IPluginApplicationBuilderManager
{
    void ReBuild();

    RequestDelegate GetBuildResult();
}
