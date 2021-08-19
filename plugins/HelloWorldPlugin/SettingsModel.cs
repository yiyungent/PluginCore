using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using PluginCore.Models;

namespace HelloWorldPlugin
{
    public class SettingsModel : PluginSettingsModel
    {
        public string Hello { get; set; }
    }
}
