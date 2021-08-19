using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubApiPlugin.ResponseModel
{
    public class CommonResponseModel
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public Object Data { get; set; }
    }
}
