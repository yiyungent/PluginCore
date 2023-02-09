(window.webpackJsonp=window.webpackJsonp||[]).push([[18],{284:function(n,t,i){"use strict";i.r(t);var e=i(10),l=function(n){n.options.__data__block__={mermaid_1a96284d:"classDiagram\ndirection LR\n\nclass IPluginContext {\n    <<interface>>\n    +IEnumerable<\\Assembly> Assemblies \n\n    +Assembly LoadFromAssemblyName(AssemblyName assemblyName)\n    +void Unload()\n}\n\nclass IPluginContextPack {\n    <<interface>>\n    // 将 此插件 打包 到一个 IPluginContext 中\n    +IPluginContext Pack(string pluginId)\n}\n\nclass IPluginContextManager {\n    <<interface>>\n    +List<\\IPluginContext> All()\n    +bool Any(string pluginId)\n    +void Remove(string pluginId)\n    +IPluginContext Get(string pluginId)\n    +void Add(string pluginId, IPluginContext context)\n}\n\nclass IPluginFinder {\n    <<interface>>\n    +IEnumerable<\\TPlugin> EnablePlugins<TPlugin>()\n    +IEnumerable<\\IPlugin> EnablePlugins()\n    +IPlugin Plugin(string pluginId)\n}\n\nclass IPluginManager {\n    <<interface>>\n    +void LoadPlugin(string pluginId)\n    +void UnloadPlugin(string pluginId)\n}\n\nIPluginContextManager <|.. PluginContextManager\nPluginContextStore <-- PluginContextManager\nclass PluginContextManager {\n    -Dictionary<\\string, IPluginContext> _pluginContexts : PluginContextStore.PluginContexts\n\n    +List<\\IPluginContext> All()\n    +bool Any(string pluginId)\n    +void Remove(string pluginId)\n    +IPluginContext Get(string pluginId)\n    +void Add(string pluginId, IPluginContext context)\n}\n\nclass PluginContextStore {\n    <<static>>\n    // fixed: 由于 IPluginContextManager 单例失败, 因此临时解决方案\n    +static Dictionary<\\string, IPluginContext> PluginContexts = new Dictionary<\\string, IPluginContext>()\n}\n\nIPluginContextPack <|.. PluginContextPack\nclass PluginContextPack {\n    // 将 此插件 打包 到一个 IPluginContext 中\n    +IPluginContext Pack(string pluginId)\n}\n\nIPluginFinder <|.. PluginFinder\nclass PluginFinder {\n    +IPluginContextManager PluginContextManager\n\n    +IEnumerable<\\TPlugin> EnablePlugins<\\TPlugin>()\n    +IEnumerable<\\IPlugin> EnablePlugins()\n    +IPlugin Plugin(string pluginId)\n}\n\nIPluginContext <|.. PluginLoadContext\nCollectibleAssemblyLoadContext <|-- PluginLoadContext\nclass PluginLoadContext {\n    -AssemblyDependencyResolver _resolver\n\n    #Assembly Load(AssemblyName assemblyName)\n    #IntPtr LoadUnmanagedDll(string unmanagedDllName)\n}\n\nIPluginManager <|.. PluginManager\nclass PluginManager {\n    +IPluginContextManager PluginContextManager\n    +IPluginContextPack PluginContextPack\n    \n    +void LoadPlugin(string pluginId)\n    +void UnloadPlugin(string pluginId)\n}\n\nIPluginManager <|.. AspNetCorePluginManager\nclass AspNetCorePluginManager {\n    +IPluginContextManager PluginContextManager\n    +IPluginContextPack PluginContextPack\n\n    +void LoadPlugin(string pluginId)\n    +void UnloadPlugin(string pluginId)\n}\n\n"}},g=Object(e.a)({},(function(){var n=this._self._c;return n("ContentSlotsDistributor",{attrs:{"slot-key":this.$parent.slotKey}},[n("h1",{attrs:{id:"系统设计"}},[n("a",{staticClass:"header-anchor",attrs:{href:"#系统设计"}},[this._v("#")]),this._v(" 系统设计")]),this._v(" "),n("Mermaid",{attrs:{id:"mermaid_1a96284d",graph:this.$dataBlock.mermaid_1a96284d}})],1)}),[],!1,null,null,null);"function"==typeof l&&l(g);t.default=g.exports}}]);