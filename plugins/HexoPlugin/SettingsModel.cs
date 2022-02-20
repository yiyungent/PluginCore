using PluginCore.Models;

namespace HexoPlugin
{
    public class SettingsModel : PluginSettingsModel
    {
        public string[] Origins { get; set; }

        public string Head { get; set; }

        public string Footer { get; set; }

    }
}
