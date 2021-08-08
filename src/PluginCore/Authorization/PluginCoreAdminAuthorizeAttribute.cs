using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace PluginCore.Authorization
{
    public class PluginCoreAdminAuthorizeAttribute : AuthorizeAttribute
    {
        public PluginCoreAdminAuthorizeAttribute() : base("PluginCoreAdmin")
        {

        }
    }
}
