//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PluginCore.Utils
{
    public class Md5Helper
    {
        #region MD5加密为32位16进制字符串
        /// <summary>
        /// MD5加密为32位16进制字符串
        /// </summary>
        /// <param name="source">原输入字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string MD5Encrypt32(string source)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(source));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                // "x2" 转换为 16进制
                sb.Append(buffer[i].ToString("x2"));
            }
            return sb.ToString();
        }
        #endregion
    }
}
