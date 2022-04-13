using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace PluginCore
{
    /// <summary>
    /// 一个可回收的程序集加载上下文
    /// 在整个插件加载上下文的设计上，每个插件都使用一个单独的CollectibleAssemblyLoadContext来加载，所有插件的CollectibleAssemblyLoadContext都放在一个PluginsLoadContext对象中
    /// </summary>
    public class CollectibleAssemblyLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;

        public CollectibleAssemblyLoadContext(string name)
             : base(name: name, isCollectible: true)
        {
            string dllFilePath = Path.Combine(PluginPathProvider.PluginsRootPath(), this.Name, $"{this.Name}.dll");
            _resolver = new AssemblyDependencyResolver(dllFilePath);
        }

        protected override Assembly Load(AssemblyName name)
        {
#if DEBUG
            Console.WriteLine($"{this.Name} Load({name})");
#endif

            #region 方案1
            //string assemblyPath = _resolver.ResolveAssemblyToPath(name);
            //if (assemblyPath != null)
            //{
            //    return LoadFromAssemblyPath(assemblyPath);
            //}

            //return null;
            #endregion

            #region 方案2
            //string dllFilePath = Path.Combine(PluginPathProvider.PluginsRootPath(), this.Name, $"{name.Name}.dll");
            //Assembly dllAssembly = null;
            //if (File.Exists(dllFilePath))
            //{
            //    using (var fs = new FileStream(dllFilePath, FileMode.Open))
            //    {
            //        // 使用此方法, 就不会导致dll被锁定
            //        dllAssembly = this.LoadFromStream(fs);
            //    }
            //} 

            //return dllAssembly;
            #endregion

            #region 方案3
            string assemblyPath = _resolver.ResolveAssemblyToPath(name);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            string dllFilePath = Path.Combine(PluginPathProvider.PluginsRootPath(), this.Name, $"{name.Name}.dll");
            Assembly dllAssembly = null;
            if (File.Exists(dllFilePath))
            {
                using (var fs = new FileStream(dllFilePath, FileMode.Open))
                {
                    // 使用此方法, 就不会导致dll被锁定
                    dllAssembly = this.LoadFromStream(fs);

                    return dllAssembly;
                }
            }

            return null;
            #endregion

            //return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
#if DEBUG
            Console.WriteLine($"{this.Name} LoadUnmanagedDll({unmanagedDllName})");
#endif

            return base.LoadUnmanagedDll(unmanagedDllName);
        }





    }
}
