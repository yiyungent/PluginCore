using System;
using System.Collections.Generic;
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
        public CollectibleAssemblyLoadContext()
             : base(isCollectible: true)
        {
        }

        protected override Assembly Load(AssemblyName name)
        {
            return null;
        }
    }
}
