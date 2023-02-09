/*
 * @Author: yiyun
 * @Description:
 */
module.exports = {
  title: "PluginCore",
  description: "ASP.NET Core 轻量级 插件框架",
  base: "/PluginCore/",
  plugins: [
    // https://github.com/eFrane/vuepress-plugin-mermaidjs
    'vuepress-plugin-mermaidjs',
    // "mermaidjs"
    [
      // https://github.com/lukemnet/vuepress-plugin-matomo
      "@lukemnet/vuepress-plugin-matomo",
      {
        'siteId': 4,
        'trackerUrl': "https://matomo.moeci.com/"
      }
    ]
  ],
  // 多语言配置: https://vuepress.vuejs.org/zh/guide/i18n.html#站点多语言配置
  locales: {
    // 键名是该语言所属的子路径
    // 作为特例，默认语言可以使用 '/' 作为其路径。
    "/": {
      lang: "en-US", // 将会被设置为 <html> 的 lang 属性
      title: "PluginCore",
      description: "ASP.NET Core lightweight plugin framework",
    },
    "/zh/": {
      lang: "zh-CN",
      title: "PluginCore",
      description: "ASP.NET Core 轻量级 插件框架",
    },
  },
  themeConfig: {
    // 假定是 GitHub. 同时也可以是一个完整的 GitLab URL
    repo: "yiyungent/PluginCore",
    // 自定义仓库链接文字。默认从 `themeConfig.repo` 中自动推断为
    // "GitHub"/"GitLab"/"Bitbucket" 其中之一，或是 "Source"。
    repoLabel: "GitHub",

    // 以下为可选的编辑链接选项

    // 假如你的文档仓库和项目本身不在一个仓库：
    //docsRepo: 'vuejs/vuepress',
    // 假如文档不是放在仓库的根目录下：
    docsDir: "docs/docs",
    // 假如文档放在一个特定的分支下：
    docsBranch: "main",
    // 默认是 false, 设置为 true 来启用
    editLinks: true,
    // 默认为 "Edit this page"
    editLinkText: "帮助我们改善此页面！",
    lastUpdated: "Last Updated", // string | boolean

    nav: [
      { text: "首页", link: "/" },
      { text: "指南", link: "/Guide/" },
      {
        text: "插件开发",
        items: [
          { text: "快速上手", link: "/PluginDev/Guide/" },
          { text: "插件SDK", link: "/PluginDev/PluginSDK/" },
        ],
      },
      {
        text: "社区",
        items: [{ text: "插件", link: "/Community/Plugins/" }],
      },
      {
        text: "高级",
        items: [
          { text: "系统设计", link: "/Advanced/Design/" },
          // { text: "vue-sim-captcha", link: "/WebSDK/vue-sim-captcha/" },
        ],
      },
    ],

    sidebarDepth: 2,

    sidebar: {
      "/PluginDev/PluginSDK/": [
        {
          title: "插件SDK", // 必要的
          path: "/PluginDev/PluginSDK/",
          collapsable: false, // 可选的, 默认值是 true,
          children: [
            { title: "IStartupPlugin", path: "IStartupPlugin" },
            { title: "IStartupXPlugin", path: "IStartupXPlugin" },
            { title: "ITimeJobPlugin", path: "ITimeJobPlugin" },
          ],
        },
      ],
    },

    // 多语言配置: https://vuepress.vuejs.org/zh/guide/i18n.html#站点多语言配置
    locales: {
      "/": {
        selectText: "Languages",
        label: "English",
        ariaLabel: "Languages",
        editLinkText: "Edit this page on GitHub",
        serviceWorker: {
          updatePopup: {
            message: "New content is available.",
            buttonText: "Refresh",
          },
        },
        algolia: {},

        nav: [
          { text: "Home", link: "/" },
          { text: "Guide", link: "/Guide/" },
          {
            text: "Plugin development",
            items: [
              { text: "Quick Start", link: "/PluginDev/Guide/" },
              { text: "Plugin SDK", link: "/PluginDev/PluginSDK/" },
            ],
          },
          {
            text: "Community",
            items: [{ text: "Plugins", link: "/Community/Plugins/" }],
          },
          {
            text: "Advanced",
            items: [
              { text: "System design", link: "/Advanced/Design/" },
              // { text: "vue-sim-captcha", link: "/WebSDK/vue-sim-captcha/" },
            ],
          },
        ],

        sidebarDepth: 2,

        sidebar: {
          "/PluginDev/PluginSDK/": [
            {
              title: "插件SDK", // 必要的
              path: "/PluginDev/PluginSDK/",
              collapsable: false, // 可选的, 默认值是 true,
              children: [
                { title: "IStartupPlugin", path: "IStartupPlugin" },
                { title: "IStartupXPlugin", path: "IStartupXPlugin" },
                { title: "ITimeJobPlugin", path: "ITimeJobPlugin" },
              ],
            },
          ],
        },
        
      },
      "/zh/": {
        // 多语言下拉菜单的标题
        selectText: "选择语言",
        // 该语言在下拉菜单中的标签
        label: "简体中文",
        // 编辑链接文字
        editLinkText: "在 GitHub 上编辑此页",
        // Service Worker 的配置
        serviceWorker: {
          updatePopup: {
            message: "发现新内容可用.",
            buttonText: "刷新",
          },
        },
        // 当前 locale 的 algolia docsearch 选项
        algolia: {},

        nav: [
          { text: "首页", link: "/zh/" },
          { text: "指南", link: "/zh/Guide/" },
          {
            text: "插件开发",
            items: [
              { text: "快速上手", link: "/zh/PluginDev/Guide/" },
              { text: "插件SDK", link: "/zh/PluginDev/PluginSDK/" },
            ],
          },
          {
            text: "社区",
            items: [{ text: "插件", link: "/zh/Community/Plugins/" }],
          },
          {
            text: "高级",
            items: [
              { text: "系统设计", link: "/zh/Advanced/Design/" },
              // { text: "vue-sim-captcha", link: "/WebSDK/vue-sim-captcha/" },
            ],
          },
        ],

        sidebarDepth: 2,

        sidebar: {
          "/zh/PluginDev/PluginSDK/": [
            {
              title: "插件SDK", // 必要的
              path: "/zh/PluginDev/PluginSDK/",
              collapsable: false, // 可选的, 默认值是 true,
              children: [
                { title: "IStartupPlugin", path: "IStartupPlugin" },
                { title: "IStartupXPlugin", path: "IStartupXPlugin" },
                { title: "ITimeJobPlugin", path: "ITimeJobPlugin" },
              ],
            },
          ],
        },

      },
    },
  },
};
