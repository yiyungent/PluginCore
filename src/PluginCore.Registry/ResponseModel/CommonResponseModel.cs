//===================================================
//  License: GNU LGPLv3
//  Contributors: yiyungent@gmail.com
//  Project: https://yiyungent.github.io/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿namespace PluginCore.Registry.ResponseModel
{
    /// <summary>
    /// 其实没必要小写 属性名, ApiController 会自动转首字母小写
    /// </summary>
    public class CommonResponseModel 
    {
        public int code { get; set; }

        public string message { get; set; }

        public object data { get; set; }
    }
}


