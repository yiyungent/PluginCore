//===================================================
//  License: GNU LGPLv3
//  Contributors: yiyungent@gmail.com
//  Project: https://yiyungent.github.io/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace PluginCore.Utils
{
    public class LogUtil
    {
        private static IServiceScopeFactory _serviceScopeFactory;

        public static void Initialize(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public static void Info<T>(string message)
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ILogger<T>? service = scope.ServiceProvider.GetService<ILogger<T>>();
                if (service != null)
                {
                    service.LogInformation(message);
                }
            }
        }

        public static void Error<T>(string message)
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ILogger<T>? service = scope.ServiceProvider.GetService<ILogger<T>>();
                if (service != null)
                {
                    service.LogError(message);
                }
            }
        }

        public static void Exception<T>(Exception ex)
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ILogger<T>? service = scope.ServiceProvider.GetService<ILogger<T>>();
                if (service != null)
                {
                    service.LogError(ex, ex.Message);
                }
            }
        }

        public static void PluginBehavior<T>(T plugin, Type iplugin, string methodName)
            where T : IPlugins.IPlugin
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
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

            // 2. 日志输出
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ILogger<T>? service = scope.ServiceProvider.GetService<ILogger<T>>();
                if (service != null)
                {
                    service.LogInformation(message);
                }
            }
        }

    }
}


