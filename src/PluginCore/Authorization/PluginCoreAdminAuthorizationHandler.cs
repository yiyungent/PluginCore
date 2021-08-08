using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PluginCore.Authorization
{
    public class PluginCoreAdminAuthorizationHandler : AuthorizationHandler<PluginCoreAdminRequirement>
    {
        private readonly AccountManager _accountManager;

        public PluginCoreAdminAuthorizationHandler(AccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        /// <summary>
        /// 必须在其中呼叫一次 <see cref="AuthorizationHandlerContext.Succeed(IAuthorizationRequirement)"/> 代表满足 <see cref="PluginCoreAdminRequirement"/>，否则皆为 不满足此 Requirement
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PluginCoreAdminRequirement requirement)
        {
            bool isAdmin = this._accountManager.IsAdmin();
            if (!isAdmin)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }

            await Task.CompletedTask;
        }
    }
}
