using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
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

        public IList<string> SkipDllKeys { get; set; }

        public PluginManager(PluginControllerManager pluginControllerManager)
        {
            _pluginControllerManager = pluginControllerManager;
            SkipDllKeys = new List<string>();

            #region 跳过1: 程序目录下 以单独dll 出现
            // 获取主程序 已经存在的（不许在再加载的 dll）
            string basePath = AppContext.BaseDirectory;
            //Console.WriteLine($"PluginCore.PluginManager: basePath: {basePath}");
            // { "Core.dll", "Domain.dll", "Framework.dll", "Services.dll", "Repositories.dll", "PluginCore.dll", ... }

            // 为保证万无一失
            SkipDllKeys = DllKeysInDir(new DirectoryInfo(basePath)).Select(m => m.DllKey).ToList();
            #endregion

            #region 跳过2: 打包进入1个dll 或 打包进 1个exe
            // 注意: 用户可能将 dll 打包在 一个dll中, 或打包进 exe, 因此 通过此方式 确保跳过
            // 主程序所有 位于 AssemblyLoadContext.Default
            // 注意: 这样获取到的 会漏一些，不知道为什么, 可能将其算在了 assembly.GetReferencedAssemblies() 里
            List<AssemblyName> skipAssembliesName = AssemblyLoadContext.Default.Assemblies
                .Select(m => m.GetName())
                //.Select(m => m.Name).ToList();
                .ToList();
            //foreach (var name in skipAssembliesName)
            foreach (var name in skipAssembliesName)
            {
                // 只区分 名、版本
                // driver, Version=1.5.77
                //this.SkipDlls.Add($"{name.Name}, Version={name.Version}");

                // 全名
                if (!SkipDllKeys.Contains(name.FullName))
                {
                    this.SkipDllKeys.Add(name.FullName);
                }
            }
            #endregion
        }

        /// <summary>
        /// 加载插件程序集
        /// </summary>
        /// <param name="pluginId"></param>
        public void LoadPlugin(string pluginId)
        {
            // 此插件的 加载上下文
            var context = new CollectibleAssemblyLoadContext();

            // TODO:未测试 加载插件引用的dll: 方法二: 
            //AssemblyName[] referenceAssemblyNames = pluginMainAssembly.GetReferencedAssemblies();
            //foreach (var assemblyName in referenceAssemblyNames)
            //{
            //    context.LoadFromAssemblyName(assemblyName);
            //}

            // 跳过不需要加载的 dll, eg: ASP.NET Core Shared Framework, 主程序中已有dll
            string[] skipDllKeys = SkipDllKeys.ToArray(); //new string[] { "Core.dll", "Domain.dll", "Framework.dll", "Services.dll", "Repositories.dll", "PluginCore.dll" };

            // 注意: 先加载插件引用的dll, 因为可能在插件主dll的Controller中立即使用了引用的dll

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

            var allReferenceFileInfos = DllKeysInDir(pluginDir).Where(m =>
                  m.DllFile.Name != $"{pluginId}.dll"
                  &&
                  (
                      !skipDllKeys.Contains(m.DllKey))
                      //||
                      //!m.DllKey.StartsWith("PluginCore")
                  )
                  .Select(m => m.DllFile).ToList();

            foreach (FileInfo file in allReferenceFileInfos)
            {
                using (var sr = new StreamReader(file.OpenRead()))
                {
                    context.LoadFromStream(sr.BaseStream);
                }
            }
            #endregion

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


            // 这个插件加载上下文 放入 集合中
            PluginsLoadContexts.Add(pluginId, context);
        }

        public void UnloadPlugin(string pluginId)
        {
            PluginsLoadContexts.Remove(pluginId);

            // 移除其中的控制器
            _pluginControllerManager.RemoveControllers(pluginId);
        }

        /// <summary>
        /// 某个目录下的所有 dll 的信息标记
        /// </summary>
        /// <returns></returns>
        private IEnumerable<(FileInfo DllFile, string DllKey)> DllKeysInDir(DirectoryInfo dllDir)
        {
            var alldllFileInfos = dllDir.GetFiles("*.dll");

            // 注意: 专门用一个临时 ALC 来获取当前插件下的 dll 信息
            var contextTemp = new CollectibleAssemblyLoadContext();
            foreach (FileInfo file in alldllFileInfos)
            {
                using (var sr = new StreamReader(file.OpenRead()))
                {
                    Assembly assembly = contextTemp.LoadFromStream(sr.BaseStream);
                    //string dllKey = $"{assembly.GetName().Name}, Version={assembly.GetName().Version}";
                    // 直接用全名
                    string dllKey = assembly.FullName;

                    yield return (file, dllKey);
                }
            }

            contextTemp.Unload();
        }
    }
}
