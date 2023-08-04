//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================


using Microsoft.AspNetCore.Mvc;
using PluginCore;
using PluginCore.IPlugins.Infrastructure;

namespace HelloWorldPlugin.Controllers;

/// <summary>
/// Hello World Plugin
/// </summary>
[Route("api/plugins/[controller]")]
[ApiController]
public class HelloController : ControllerBase
{

    [HttpGet]
    public ActionResult Get()
    {
        SettingsModel settingsModel = PluginSettingsModelFactory.Create<SettingsModel>("HelloWorldPlugin");
        string str = $"Hello PluginCore ! {settingsModel.Hello}";

        return Ok(str);
    }

}
