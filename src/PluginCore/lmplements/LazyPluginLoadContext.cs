//===================================================
//  License: GNU LGPLv3
//  Contributors: yiyungent@gmail.com
//  Project: https://yiyungent.github.io/PluginCore
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
    public class LazyPluginLoadContext : CollectibleAssemblyLoadContext, IPluginContext
    {
        private AssemblyDependencyResolver _resolver;

        /// <summary>
        /// 加了一个可回收
        /// </summary>
        /// <param name="pluginMainDllFilePath"></param>
        public LazyPluginLoadContext(string pluginId, string pluginMainDllFilePath) : base(name: pluginId)
        {
            _resolver = new AssemblyDependencyResolver(pluginMainDllFilePath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            // 1. 先尝试 从本插件文件夹中搜索
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
            // 2. 再尝试 从其他启用的插件文件夹中搜索
            // 实测: 可在下方搜索, 主程序包括的 assemblyName 与 启用插件加载的 assemblyName 都会位于其中
            var assList = AppDomain.CurrentDomain.GetAssemblies();
            var temp = assList.FirstOrDefault(m => m.GetName().FullName == assemblyName.FullName);
            if (temp != null)
            {
                return temp;
            }
            // 尝试忽略版本搜索
            var temp2 = assList.FirstOrDefault(m => m.GetName().Name == assemblyName.Name);
            if (temp2 != null)
            {
                return temp2;
            }

            // 由于是懒加载, 若 A 插件依赖 B 插件, 而此时在 A 插件调用 B.Test(),
            // 而 B.Test() 与 C.dll 有关, 而之前 B 插件未触发 C.dll 中类型, 于是这时还没有加载 C.dll, 于是 assList 这时其中无 C.dll
            // 由于 B.Test() 需在 A 中调用, 于是为公用, 因此 C.dll 需在 A 中排除, 不然会由于重复加载-类型不一致而 Method Missing
            // 在 A 中 C.dll 与 在 B 中 C.dll 会认为其中类型不一致
            // TODO: 主动扫描一次
            //foreach (var pluginContextKeyValue in PluginContextStore.PluginContexts)
            //{
            //    //pluginContextKeyValue.Value.Load(assemblyName);
            //    string pluginId = pluginContextKeyValue.Key;
                
            //}

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


