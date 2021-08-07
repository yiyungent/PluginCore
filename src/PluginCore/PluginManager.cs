using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PluginCore
{
    /// <summary>
    /// 一个插件的所有dll由 一个 <see cref="CollectibleAssemblyLoadContext"/> 管理
    /// <see cref="PluginsLoadContexts"/> 记录管理了所有 插件的<see cref="CollectibleAssemblyLoadContext"/>
    /// <see cref="PluginManager"/> 是对 <see cref="PluginsLoadContexts"/>的封装, 使其更好管理插件加载释放的行为
    /// </summary>
    public class PluginManager
    {
        private readonly PluginControllerManager _pluginControllerManager;

        public IList<string> SkipDlls { get; set; }

        public PluginManager(PluginControllerManager pluginControllerManager)
        {
            _pluginControllerManager = pluginControllerManager;
            SkipDlls = new List<string>();
            // 获取主程序 已经存在的（不许在再加载的 dll）
            string basePath = AppContext.BaseDirectory;
            //Console.WriteLine($"PluginCore.PluginManager: basePath: {basePath}");
            // { "Core.dll", "Domain.dll", "Framework.dll", "Services.dll", "Repositories.dll", "PluginCore.dll", ... }
            SkipDlls = new DirectoryInfo(basePath).GetFiles("*.dll").Select(m => m.Name).ToList();
        }

        /// <summary>
        /// 加载插件程序集
        /// </summary>
        /// <param name="pluginId"></param>
        public void LoadPlugin(string pluginId)
        {
            // 此插件的 加载上下文
            var context = new CollectibleAssemblyLoadContext();

            #region 加载插件主dll
            // 插件的主dll, 不包括插件项目引用的dll
            string pluginMainDllFilePath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId, $"{pluginId}.dll");
            Assembly pluginMainAssembly;
            using (var fs = new FileStream(pluginMainDllFilePath, FileMode.Open))
            {
                // 使用此方法, 就不会导致dll被锁定
                pluginMainAssembly = context.LoadFromStream(fs);

                // 加载其中的控制器
                _pluginControllerManager.AddControllers(pluginMainAssembly);
            }
            #endregion

            // TODO:未测试 加载插件引用的dll: 方法二: 
            //AssemblyName[] referenceAssemblyNames = pluginMainAssembly.GetReferencedAssemblies();
            //foreach (var assemblyName in referenceAssemblyNames)
            //{
            //    context.LoadFromAssemblyName(assemblyName);
            //}

            // 跳过不需要加载的 dll, eg: ASP.NET Core Shared Framework, 主程序中已有dll
            string[] skipDlls = SkipDlls.ToArray(); //new string[] { "Core.dll", "Domain.dll", "Framework.dll", "Services.dll", "Repositories.dll", "PluginCore.dll" };

            #region 加载插件引用的dll
            // 加载插件引用的dll
            // eg: xxx/Plugins/HelloWorld
            string pluginDirPath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId);
            var pluginDir = new DirectoryInfo(pluginDirPath);
            // 插件引用的所有dll (排除 主dll 和 skipDlls )
            // 注意: 主程序中已有dll 必须跳过, 应为这些默认Context中已经加载, 而如果插件Context再次加载, 则认为这两个是不同Assembly, 导致其中的Type转换失败
            // 这里简单来说，意思就是当在一个自定义LoadContext中加载程序集的时候，如果找不到这个程序集，程序会自动去默认LoadContext中查找，如果默认LoadContext中都找不到，就会返回null。
            // 这里我突然想到会不会是因为DemoPlugin1、DemoPlugin2以及主站点的AssemblyLoadContext都加载了Mystique.Core.dll程序集的缘故，虽然他们加载的是同一个程序集，但是因为LoadContext不同，所以系统认为它是2个程序集。
            // 参考： https://www.cnblogs.com/lwqlun/p/12930713.html
            var allReferenceFileInfos = pluginDir.GetFiles("*.dll").Where(p =>
            p.Name != $"{pluginId}.dll"
            &&
            !skipDlls.Contains(p.Name));
            foreach (FileInfo file in allReferenceFileInfos)
            {
                using (var sr = new StreamReader(file.OpenRead()))
                {
                    context.LoadFromStream(sr.BaseStream);
                }
            }
            #endregion

            // 这个插件加载上下文 放入 集合中
            PluginsLoadContexts.Add(pluginId, context);
        }

        public void UnloadPlugin(string pluginId)
        {
            PluginsLoadContexts.Remove(pluginId);

            // 移除其中的控制器
            _pluginControllerManager.RemoveControllers(pluginId);
        }
    }
}
