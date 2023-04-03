//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================


using PluginCore.IPlugins.Models;

namespace HexoPlugin;

public class SettingsModel : PluginSettingsModel
{
    public string[] Origins { get; set; }

    public string Head { get; set; }

    public string Footer { get; set; }

}
