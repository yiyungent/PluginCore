using System.Reflection;
using PluginCore;
using PluginCore.Interfaces;

namespace AspNetCore6WithNatasha.Natasha
{
    public class NatashaPluginContext : IPluginContext
    {

        public NatashaReferenceDomain Domain
        {
            get; set;
        }

        public NatashaPluginContext(string pluginId)
        {
            this.Domain = DomainManagement.Create(pluginId);
            // 插件的主dll, 不包括插件项目引用的dll
            string pluginMainDllFilePath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId, $"{pluginId}.dll");
            this.Domain.LoadPluginWithAllDependency(path: pluginMainDllFilePath);
        }

        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                return this.Domain.Assemblies;
            }
        }

        public Assembly LoadFromAssemblyName(AssemblyName assemblyName)
        {
            var ass = this.Domain.LoadFromAssemblyName(assemblyName);

            return ass;
        }

        public Assembly LoadFromStream(Stream assembly)
        {
            throw new NotImplementedException();
        }

        public void Unload()
        {
            this.Domain.Unload();
        }
    }
}
