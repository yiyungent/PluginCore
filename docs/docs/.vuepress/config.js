/*
 * @Author: yiyun
 * @Description:
 */
module.exports = {
  title: "PluginCore",
  description: "ASP.NET Core 轻量级 插件框架",
  base: "/PluginCore/",
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
        items: [
          { text: "插件", link: "/Community/Plugins/" },
        ],
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

    }

  },
};
