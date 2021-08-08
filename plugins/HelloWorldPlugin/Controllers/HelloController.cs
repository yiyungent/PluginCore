using System;
using System.Collections.Generic;
using System.Text;
using HelloWorldPlugin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace PluginCore.IPlugins.Controllers
{
    [Route("api/plugins/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {

        public ActionResult Get()
        {
            SettingsModel settingsModel = PluginSettingsModelFactory.Create<SettingsModel>("HelloWorldPlugin");
            string str = $"Hello PluginCore ! {settingsModel.Hello}";

            return Ok(str);
        }

    }
}
