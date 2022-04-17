using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
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
                return LoadFromAssemblyPath(assemblyPath);
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
