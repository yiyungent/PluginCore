//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
// Project: SimCaptcha
// https://github.com/yiyungent/SimCaptcha
// Author: yiyun <yiyungent@gmail.com>

/// <summary>
/// JavaScript时间戳：是指格林威治时间1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的总毫秒数。（13 位数字）
/// 
/// Unix时间戳：是指格林威治时间1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的总秒数。（10 位数字）
/// </summary>
namespace PluginCore.Registry.Utils
{
    public static class DateTimeUtil
    {
        public static DateTime DateTime1970 = new DateTime(1970, 1, 1).ToLocalTime();

        #region Unix 10位时间戳-总秒数
        /// <summary>
        /// C# DateTime转换为Unix时间戳
        /// </summary>
        public static long ToTimeStamp10(this DateTime dateTime)
        {
            // 相差秒数
            long timeStamp = (long)(dateTime.ToLocalTime() - DateTime1970).TotalSeconds;

            return timeStamp;
        }

        /// <summary>
        /// C# DateTime转换为Unix时间戳
        /// </summary>
        public static long ToTimeStamp10(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return 0;
            }
            // 相差秒数
            long timeStamp = ToTimeStamp10((DateTime)dateTime);

            return timeStamp;
        }

        /// <summary>
        /// Unix时间戳转换为C# DateTime 
        /// </summary>
        public static DateTime ToDateTime10(this long timeStamp10)
        {
            DateTime dateTime = DateTime1970.AddSeconds(timeStamp10).ToLocalTime();

            return dateTime;
        }
        #endregion

        #region JavaScript 13位时间戳-总毫秒数
        /// <summary>
        /// C# DateTime转换为JavaScript时间戳
        /// </summary>
        public static long ToTimeStamp13(this DateTime dateTime)
        {
            // 相差毫秒数
            long timeStamp = (long)(dateTime.ToLocalTime() - DateTime1970).TotalMilliseconds;

            return timeStamp;
        }

        /// <summary>
        /// C# DateTime转换为JavaScript时间戳
        /// </summary>
        public static long ToTimeStamp13(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return 0;
            }
            // 相差秒数
            long timeStamp = ToTimeStamp13((DateTime)dateTime);

            return timeStamp;
        }

        /// <summary>
        /// JavaScript时间戳转换为C# DateTime
        /// </summary>
        public static DateTime ToDateTime13(this long timeStamp13)
        {
            DateTime dateTime = DateTime1970.AddMilliseconds(timeStamp13).ToLocalTime();

            return dateTime;
        }
        #endregion

        #region 获取当前Unix时间戳
        public static long NowTimeStamp10()
        {
            return ToTimeStamp10(DateTime.Now);
        }
        #endregion

        #region 获取当前JavaScript时间戳
        public static long NowTimeStamp13()
        {
            return ToTimeStamp13(DateTime.Now);
        }
        #endregion
    }
}
