//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PluginCore.AspNetCore.Authorization;

namespace PluginCore.AspNetCore.Authentication
{
    /// <summary>
    /// https://stackoverflow.com/questions/52287542/invalidoperationexception-no-authenticationscheme-was-specified-and-there-was
    /// </summary>
    public class PluginCoreAuthenticationHandler : AuthenticationHandler<PluginCoreAuthenticationSchemeOptions>
    {
        private readonly AccountManager _accountManager;

        public PluginCoreAuthenticationHandler(IOptionsMonitor<PluginCoreAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, AccountManager accountManager) : base(options, logger, encoder, clock)
        {
            this._accountManager = accountManager;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string token = this._accountManager.CurrentToken();
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.NoResult();
            }

            bool isAdmin = this._accountManager.IsAdminToken(token);

            if (!isAdmin)
            {
                return AuthenticateResult.Fail($"token is not admin");
            }
            else
            {
                var id = new ClaimsIdentity(
                    new Claim[] { new Claim("PluginCore.Token", token) },  // not safe , just as an example , should custom claims on your own
                    Scheme.Name
                );
                ClaimsPrincipal principal = new ClaimsPrincipal(id);
                var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(), Scheme.Name);

                Utils.LogUtil.Info($"通过 Authentication: token: {token}");

                return AuthenticateResult.Success(ticket);
            }

        }
    }
}
