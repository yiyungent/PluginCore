//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace PluginCore.Utils
{
    public class LogUtil
    {
        public const string Sign = "PluginCore";

        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{Sign}: ");
            Console.ResetColor();
            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine(message);
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Sign}: ");
            Console.ResetColor();
            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine(message);
        }

        public static void Exception(Exception ex)
        {
            Error(ex.ToString());
            Exception exception = ex;
            while (exception.InnerException != null)
            {
                exception = ex.InnerException;
                Error(exception.Message);
            }
        }


        public static void PluginBehavior<T>(T plugin, Type iplugin, string methodName)
            where T : IPlugins.IPlugin
        {
            // TODO: Bug: 无法区别 相同方法名, 参数不同的 重载方法
            MethodInfo behavior = iplugin.GetMethods().FirstOrDefault(m => m.Name == methodName);

            ParameterInfo[] pars = behavior.GetParameters();
            string parsStr = string.Empty;
            if (pars != null && pars.Length > 0)
            {
                var parTypes = pars.OrderBy(m => m.Position).Select(m => m.ParameterType.Name).ToArray();
                parsStr = string.Join(", ", parTypes);
            }

            // A程序集.APlugin
            string pluginStr = plugin.GetType().ToString();
            // A程序集.AClass.AMethod()
            string message = $"{behavior.DeclaringType.ToString()}.{behavior.Name}({parsStr})";


            // TODO: 1.写入日志文件


            // 2.控制台输出
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{Sign}: {pluginStr}: ");
            Console.ResetColor();
            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss"));

            Console.WriteLine(message);
        }

    }
}
