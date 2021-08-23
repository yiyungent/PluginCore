using System;
using System.Collections.Generic;
using System.Text;

namespace DemoModePlugin.ResponseModel
{
    public class CommonResponseModel
    {
        public int code { get; set; }

        public string message { get; set; }

        public Object data { get; set; }
    }
}
