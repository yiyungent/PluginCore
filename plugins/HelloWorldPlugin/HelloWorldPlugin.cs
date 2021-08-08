using System;
using PluginCore.IPlugins;

namespace HelloWorldPlugin
{
    public class HelloWorldPlugin : BasePlugin
    {
        public override (bool IsSuccess, string Message) AfterEnable()
        {
            Console.WriteLine($"{nameof(HelloWorldPlugin)}: {nameof(AfterEnable)}");
            return base.AfterEnable();
        }

        public override (bool IsSuccess, string Message) BeforeDisable()
        {
            Console.WriteLine($"{nameof(HelloWorldPlugin)}: {nameof(BeforeDisable)}");
            return base.BeforeDisable();
        }
    }
}
