(window.webpackJsonp=window.webpackJsonp||[]).push([[12],{374:function(e,n,a){"use strict";a.r(n);var t=a(45),r=Object(t.a)({},(function(){var e=this,n=e.$createElement,a=e._self._c||n;return a("ContentSlotsDistributor",{attrs:{"slot-key":e.$parent.slotKey}},[a("h1",{attrs:{id:"插件sdk"}},[a("a",{staticClass:"header-anchor",attrs:{href:"#插件sdk"}},[e._v("#")]),e._v(" 插件SDK")]),e._v(" "),a("blockquote",[a("p",[e._v("插件开发 - 插件SDK")])]),e._v(" "),a("h2",{attrs:{id:"istartupplugin"}},[a("a",{staticClass:"header-anchor",attrs:{href:"#istartupplugin"}},[e._v("#")]),e._v(" IStartupPlugin")]),e._v(" "),a("div",{staticClass:"language-C# extra-class"},[a("pre",{pre:!0,attrs:{class:"language-text"}},[a("code",[e._v('namespace PluginCore.IPlugins\n{\n    /// <summary>\n    /// 实验阶段\n    /// <para>无法热插拔: 需要启用插件后, 再 重启站点</para>\n    /// </summary>\n    public interface IStartupPlugin : IPlugin\n    {\n        /// <summary>\n        /// This method gets called by the runtime. Use this method to add services to the container.\n        /// </summary>\n        /// <param name="services"></param>\n        void ConfigureServices(IServiceCollection services);\n\n        int ConfigureServicesOrder { get; }\n\n        /// <summary>\n        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.\n        /// </summary>\n        /// <param name="app"></param>\n        /// <param name="env"></param>\n        void Configure(IApplicationBuilder app);\n\n        int ConfigureOrder { get; }\n    }\n}\n')])])]),a("h2",{attrs:{id:"istartupxplugin"}},[a("a",{staticClass:"header-anchor",attrs:{href:"#istartupxplugin"}},[e._v("#")]),e._v(" IStartupXPlugin")]),e._v(" "),a("div",{staticClass:"language-C# extra-class"},[a("pre",{pre:!0,attrs:{class:"language-text"}},[a("code",[e._v('namespace PluginCore.IPlugins\n{\n    /// <summary>\n    /// 实验阶段\n    /// <para>热插拔: 已有效化</para>\n    /// </summary>\n    public interface IStartupXPlugin : IPlugin\n    {\n        /// <summary>\n        /// This method gets called by the runtime. Use this method to add services to the container.\n        /// <para>未有效化</para>\n        /// </summary>\n        /// <param name="services"></param>\n        void ConfigureServices(IServiceCollection services);\n\n\n        int ConfigureServicesOrder { get; }\n\n        /// <summary>\n        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.\n        /// </summary>\n        /// <param name="app"></param>\n        /// <param name="env"></param>\n        void Configure(IApplicationBuilder app);\n\n        int ConfigureOrder { get; }\n    }\n}\n')])])]),a("h2",{attrs:{id:"itimejobplugin"}},[a("a",{staticClass:"header-anchor",attrs:{href:"#itimejobplugin"}},[e._v("#")]),e._v(" ITimeJobPlugin")]),e._v(" "),a("div",{staticClass:"language-C# extra-class"},[a("pre",{pre:!0,attrs:{class:"language-text"}},[a("code",[e._v("namespace PluginCore.IPlugins\n{\n    /// <summary>\n    /// 定时任务\n    /// </summary>\n    public interface ITimeJobPlugin : IPlugin\n    {\n        /// <summary>\n        /// 间隔秒数\n        /// </summary>\n        long SecondsPeriod { get; }\n\n        Task ExecuteAsync();\n    }\n}\n")])])])])}),[],!1,null,null,null);n.default=r.exports}}]);