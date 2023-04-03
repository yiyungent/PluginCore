//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================

using PluginCore.IPlugins.Models;

namespace HelloWorldPlugin;

public class SettingsModel : PluginSettingsModel
{
    public string Hello { get; set; }
}
