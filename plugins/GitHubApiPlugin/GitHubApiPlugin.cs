using System;
using Microsoft.Extensions.DependencyInjection;
using PluginCore.IPlugins;

namespace GitHubApiPlugin
{
    public class GitHubApiPlugin : BasePlugin
    {

        public GitHubApiPlugin(IServiceProvider serviceProvider)
        {

        }

        public override (bool IsSuccess, string Message) AfterEnable()
        {
            Console.WriteLine($"{nameof(GitHubApiPlugin)}: {nameof(AfterEnable)}");
            return base.AfterEnable();
        }

        public override (bool IsSuccess, string Message) BeforeDisable()
        {
            Console.WriteLine($"{nameof(GitHubApiPlugin)}: {nameof(BeforeDisable)}");
            return base.BeforeDisable();
        }
    }
}
