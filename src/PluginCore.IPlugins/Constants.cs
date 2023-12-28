using System;

namespace PluginCore.IPlugins
{
    public class Constants
    {
        public const string AspNetCoreAuthenticationClaimType = "PluginCore.Admin.Token";

        public const string AspNetCoreAuthenticationScheme = "PluginCore.Admin.Authentication";

        public const string AspNetCoreAuthorizationPolicyName = "PluginCore.Admin";

        public const string AspNetCoreAuthorizationTokenCookieName = "PluginCore.Admin.Token";

        /// <summary>
        /// zh
        /// en
        /// </summary>
        public const string AspNetCoreLanguageCookieName = "language";

        /// <summary>
        /// httpContext.Items[Constants.AspNetCoreLanguageKey]
        /// </summary>
        public const string AspNetCoreLanguageKey = "PluginCore.Admin.Language";
    }
}
