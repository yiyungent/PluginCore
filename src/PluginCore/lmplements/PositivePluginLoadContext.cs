using System.Reflection.Metadata;
//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Linq;

namespace PluginCore.lmplements
{
    /// <summary>
    /// 修改自下方
    /// https://docs.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support
    /// 此方法依赖于 插件项目生成的 HelloWorldPlugin.deps.json 与 runtimes 文件夹,
    /// 虽然官方文档没写, 但最好还带上 HelloWorldPlugin.runtimeconfig.json
    /// 插件项目 .csproj 其它注意, 看文档
    /// </summary>
    public class PositivePluginLoadContext : CollectibleAssemblyLoadContext, IPluginContext
    {
        private AssemblyDependencyResolver _resolver;

         /// <summary>
        /// 即 插件的 Entrypoint Assembly
        /// </summary>
        public AssemblyName MainAssemblyName{ get; protected set; }

        public IEnumerable<AssemblyName> ReferencedAssemblyNames { get; protected set; }

        /// <summary>
        /// 加了一个可回收
        /// </summary>
        /// <param name="pluginMainDllFilePath"></param>
        public PositivePluginLoadContext(string pluginId, string pluginMainDllFilePath) : base(name: pluginId)
        {
            _resolver = new AssemblyDependencyResolver(pluginMainDllFilePath);

            // 主动加载 存在此插件文件夹的 依赖项
            using (var mainFs = new FileStream(pluginMainDllFilePath, FileMode.Open))
            {
                // 使用此方法, 就不会导致dll被锁定
                // 锁定dll 会导致: 1. 无法通过复制粘贴替换 更新 2. 无法删除
                Assembly mainAssembly = LoadFromStream(mainFs);
                // 上面 FileStream 处于打开 pluginMainDllFilePath 状态, 不能使用下方方法
                //this.MainAssemblyName = AssemblyName.GetAssemblyName(pluginMainDllFilePath);
                this.MainAssemblyName = mainAssembly.GetName();
                AssemblyName[] referencedAssemblies = mainAssembly.GetReferencedAssemblies();
                this.ReferencedAssemblyNames = referencedAssemblies.AsEnumerable();
                
                // TODO:
                AssemblyFile assemblyFile = new AssemblyFile();
                
                foreach (var referencedAssembly in referencedAssemblies)
                {
                    string assemblyPath = _resolver.ResolveAssemblyToPath(referencedAssembly);
                    if (assemblyPath != null)
                    {
                        //return LoadFromAssemblyPath(assemblyPath);
                        using (var fs = new FileStream(assemblyPath, FileMode.Open))
                        {
                            // 使用此方法, 就不会导致dll被锁定
                            // 锁定dll 会导致: 1. 无法通过复制粘贴替换 更新 2. 无法删除
                            LoadFromStream(fs);
                        }
                    }
                }
            }
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            // 1. 先尝试 从本插件文件夹中搜索 (无需, 因为前面已先主动加载, 再次被调用此方法只能是其它情况: 有不存在于此插件文件夹下的依赖项 被需要)

            // 2. 再尝试 从其他启用的插件文件夹中搜索
            // 实测: 可在下方搜索, 主程序包括的 assemblyName 与 启用插件加载的 assemblyName 都会位于其中
            var assList = AppDomain.CurrentDomain.GetAssemblies();
            var temp = assList.FirstOrDefault(m => m.GetName().FullName == assemblyName.FullName);
            if (temp != null)
            {
                return temp;
            }

            // 3. 最后搜索不到, 返回 null, 即代表使用主程序提供, 如果最后几次都为 null, 则会报错
            // 当启用本插件/触碰到本插件中的一些类型时, 而当主程序中没有提供相关的此 assemblyName 时, 也会触发此方法 来尝试加载

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
