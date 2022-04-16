using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.AspNetCore.RequestModel.User
{
    public class LoginRequestModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
