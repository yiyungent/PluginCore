//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using Microsoft.AspNetCore.Mvc;
using PluginCore;

namespace ExamplePlugin.Controllers
{
    [Route("api/plugins/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {

        public ActionResult Get()
        {
            // 注意: 请将此处 修改为 你的 PluginId
            SettingsModel settingsModel = PluginSettingsModelFactory.Create<SettingsModel>("ExamplePlugin");
            string str = $"Hello PluginCore ! {settingsModel.Hello}";

            return Ok(str);
        }

    }
}
