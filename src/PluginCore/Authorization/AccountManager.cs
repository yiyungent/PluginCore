using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using PluginCore.Utils;

namespace PluginCore.Authorization
{
    public class AccountManager
    {
        public Microsoft.AspNetCore.Http.HttpContext HttpContext { get; set; }

        public AccountManager(IHttpContextAccessor httpContextAccessor)
        {
            HttpContext = ((HttpContextAccessor)httpContextAccessor).HttpContext;
        }

        public Config.PluginCoreConfig.AdminModel Admin
        {
            get
            {
                return Config.PluginCoreConfigFactory.Create().Admin;
            }
            set
            {
                var sourceModel = Config.PluginCoreConfigFactory.Create();
                sourceModel.Admin = value;
                Config.PluginCoreConfigFactory.Save(sourceModel);
            }
        }

        public string CurrentToken()
        {
            string token = null;
            HttpRequest request = HttpContext.Request;
            try
            {
                // header -> cookie
                try
                {
                    // header 中找 token
                    string authHeader = request.Headers["Authorization"];
                    if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer"))
                    {
                        token = authHeader.Substring("Bearer ".Length).Trim();
                    }
                }
                catch (Exception ex)
                { }
                if (string.IsNullOrEmpty(token))
                {
                    // cookie 中找 token
                    string tokenCookieName = "token";
                    if (request.Cookies.Keys.Contains(tokenCookieName))
                    {
                        if (request.Cookies[tokenCookieName] != null && string.IsNullOrEmpty(request.Cookies[tokenCookieName]) == false)
                        {
                            token = request.Cookies[tokenCookieName];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return token;
        }

        public string CreateToken()
        {
            return CreateToken(Admin.UserName, Admin.Password);
        }

        public string CreateToken(string userName, string password)
        {
            string token = $"UserName={userName}&Password={password}";
            token = Md5Helper.MD5Encrypt32(token);

            return token;
        }

        public bool IsAdminToken(string token)
        {
            bool isAdmin = false;
            isAdmin = CreateToken().Equals(token);

            return isAdmin;
        }


        public bool IsAdmin()
        {
            bool isAdmin = false;
            try
            {
                string currentToken = CurrentToken();
                isAdmin = IsAdminToken(currentToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isAdmin;
        }

    }
}
