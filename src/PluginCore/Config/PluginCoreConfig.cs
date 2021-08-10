using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.Config
{
    public class PluginCoreConfig
    {
        public PluginCoreConfig()
        {
            this.Admin = new AdminModel();
        }

        public AdminModel Admin { get; set; }

        public bool IsLocalFrontend { get; set; } = false;

        public string RemoteFrontend { get; set; } =
            "https://cdn.jsdelivr.net/gh/yiyungent/plugincore-admin-frontend@0.1.3/dist-cdn";

        public sealed class AdminModel
        {
            public string UserName { get; set; } = "admin";

            public string Password { get; set; } = "ABC12345";
        }
    }
}
