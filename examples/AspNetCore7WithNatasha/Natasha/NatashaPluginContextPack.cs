using PluginCore.Interfaces;

namespace AspNetCore7WithNatasha.Natasha;

public class NatashaPluginContextPack : IPluginContextPack
{
    public IPluginContext Pack(string pluginId)
    {
        var context = new NatashaPluginContext(pluginId);

        return context;
    }
}
