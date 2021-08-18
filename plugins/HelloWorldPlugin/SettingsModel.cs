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

        public GitHubOAuthModel GitHubOAuth { get; set; }

        public sealed class GitHubOAuthModel
        {
            public string AppName { get; set; }

            public string ClientId { get; set; }

            public string ClientSecret { get; set; }

            public string RedirectUri { get; set; }

            public IList<string> Scopes { get; set; }

            public GitHubOAuthModel()
            {
                this.Scopes = new string[] { "user", "notifications" };
                this.RedirectUri = "/api/GitHub/Authorize";
            }
        }
    }
}
