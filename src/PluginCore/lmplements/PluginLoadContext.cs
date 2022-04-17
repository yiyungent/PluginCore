using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace PluginCore.lmplements
{
    /// <summary>
    /// 修改自下方
    /// https://docs.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support
    /// 此方法依赖于 插件项目生成的 HelloWorldPlugin.deps.json 与 runtimes 文件夹,
    /// 虽然官方文档没写, 但最好还带上 HelloWorldPlugin.runtimeconfig.json
    /// 插件项目 .csproj 其它注意, 看文档
    /// </summary>
    public class PluginLoadContext : CollectibleAssemblyLoadContext, IPluginContext
    {
        private AssemblyDependencyResolver _resolver;

        /// <summary>
        /// 加了一个可回收
        /// </summary>
        /// <param name="pluginMainDllFilePath"></param>
        public PluginLoadContext(string pluginMainDllFilePath) /*: base(isCollectible: true)*/
        {
            _resolver = new AssemblyDependencyResolver(pluginMainDllFilePath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                //return LoadFromAssemblyPath(assemblyPath);
                using (var fs = new FileStream(assemblyPath, FileMode.Open))
                {
                    // 使用此方法, 就不会导致dll被锁定
                    // 锁定dll 会导致: 1. 无法通过复制粘贴替换 更新 2. 无法删除
                    return LoadFromStream(fs);
                }
            }

            return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return IntPtr.Zero;
        }
    }
}
