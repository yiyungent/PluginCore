using System;
using PluginCore.IPlugins;

namespace ExamplePlugin
{
    public class ExamplePlugin : BasePlugin
    {
        public override (bool IsSuccess, string Message) AfterEnable()
        {
            Console.WriteLine($"{nameof(ExamplePlugin)}: {nameof(AfterEnable)}");
            return base.AfterEnable();
        }

        public override (bool IsSuccess, string Message) BeforeDisable()
        {
            Console.WriteLine($"{nameof(ExamplePlugin)}: {nameof(BeforeDisable)}");
            return base.BeforeDisable();
        }

    }
}
