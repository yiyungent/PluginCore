# Changelog

All notable changes to this project will be documented in this file.

## [unreleased]

### 📚 Documentation

- *(Releases.md)* PluginCore.AspNetCore-v1.4.3

## [PluginCore.AspNetCore-v1.4.3] - 2024-08-31

### 🚀 Features

- *(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)* 插件 wwwroot 默认页 指定, 无需再手动

### 🐛 Bug Fixes

- *(PluginCore.AspNetCore/Controllers/PluginsController)* Swagger [FromForm]

### 📚 Documentation

- *(releases.md)* PluginCore-v2.2.5, PluginCore.AspNetCore-v1.4.2
- *(readme)* 关联线上产品
- *(README.md)* 爱发电赞助

### ⚙️ Miscellaneous Tasks

- *(PluginCore.AspNetCore.csproj)* 1.4.2 -> 1.4.3

## [PluginCore.AspNetCore-v1.4.2] - 2024-04-06

### Build

- *(src/plugincore.aspnetcore/plugincore.aspnetcore.csproj)* 1.4.1 -> 1.4.2

## [PluginCore-v2.2.5] - 2024-04-06

### 🚀 Features

- *(src/plugincore.aspnetcore/backgroundservices/plugintimejobbackgroundservice.cs)* Log
- *(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)* Plugin:startup->appstart

### 🐛 Bug Fixes

- *(src/plugincore/lmplements/lazypluginloadcontext.cs)* Dll 忽略版本搜索

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore-v2.2.4, PluginCore.AspNetCore-v1.4.1
- *(readme)* 关联线上产品

### Build

- *(src/plugincore/plugincore.csproj)* 2.2.4 -> 2.2.5

## [PluginCore.AspNetCore-v1.4.1] - 2024-03-14

### Build

- *(plugincore.aspnetcore.csproj)* 1.4.0 -> 1.4.1

## [PluginCore-v2.2.4] - 2024-03-14

### 🚀 Features

- *(src/plugincore/utils/logutil.cs)* Add LogCategoryName

### 🐛 Bug Fixes

- *(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)* Log, _serviceProvider

### 📚 Documentation

- *(readme,releases)* Add: PluginCore.AspNetCore-v1.4.0
- *(README.md)* 爱发电赞助
- *(readme.md,readme_zh.md)* Update
- *(readme)* README,plugincore-js-sdk/README.md: badge,link
- *(readme.md,readme_zh.md)* 技术栈
- *(readme.md,readme_zh.md)* 技术栈: & Element UI

### Build

- *(src/plugincore/plugincore.csproj)* 2.2.3 -> 2.2.4

## [PluginCore.AspNetCore-v1.4.0] - 2024-02-15

### 🚀 Features

- *(src/plugincore.aspnetcore)* Authentication & Authorize
- *(plugins.json)* Add
- Dist, dist-cdn
- *(dist-cdn)* Dist-cdn, vue.config.js
- *(vue.config.js)* Fronted -> frontend
- *(views/plugins/upload.vue)* Upload nupkg
- *(src/main.js)* Add copyright: plugincore-admin-frontend
- *(dist,dist-cdn)* Update
- *(myenv)* MyEnv get version from package.json
- 国际化: 多语言
- 国际化: route, menu, some plugin details
- *(dist,dist-cdn,package.json)* Change version to 0.3.0; build
- Publish-npm.ps1
- *(src/lang)* 移除: 插件上传大小限制
- Copy: daylib
- 初步完成: 未测试
- 初步完成: 测试通过
- Npm 包名改为: @yiyungent/plugincore
- *(package.json,src/js/plugincore.js)* 从 document 开始搜索扩展点: 支持 <head></head> 埋点
- *(.babelrc,package-lock.json,package.json,webpack.config.js)* Add: babel 转换
- *(plugincore.js,dist/plugincore.min.js)* Start(eachFinishCallback),console.log->console.info
- 0.4.0, script src 加载,p.addEventListener("load",) 支持
- *(src/js/plugincore.js)* ProcessLink,eachLink
- *(plugincore.js,utils.js)* 加载js,加载css 整理
- *(src/js/plugincore.js)* Window.plugincore.addEventListener = addEventListener;
- 支持 `headers` 自定义请求头
- *(docs/docs/.vuepress/components/pluginstore.vue)* 插件商店: 中间加宽并居中
- *(src/plugincore.aspnetcore)* 认证与授权: 优化,分离, PluginCoreStartupExtensions 优化
- *(src/plugincore.aspnetcore)* AccountManager 部分方法静态化, 提供 HttpContext 传入方式, 相关引用处更新调用

### 🐛 Bug Fixes

- 401 error tip
- Avatar: use local frontend, add: BASE_URL
- Avatar: const -> let
- 国际化: 插件子页面
- 国际化: 插件子页面, 导航
- *(src/utils/validate.js)* 用户名验证错误,v0.3.1
- *(index.html,package.json,src/js/plugincore.js)* 插件加载的 script 内容不会执行
- 0.4.0 -> 0.3.0
- *(src/js/plugincore.js)* Link 需要rel,type, 浏览器才会动态加载
- *(src/js/utils.js)* Npm run build error

### 📚 Documentation

- *(releases.md)* PluginCore.AspNetCore-v1.3.4
- *(readme.md,readme_zh.md)* 版本依赖: update
- *(README.md)* 爱发电赞助
- *(docs)* 插件商店: 初步
- *(docs)* 插件商店: update
- *(releases.md)* Plugincore-admin-frontend-v0.3.2
- *(readme.md)* Update badge: npm: @yiyungent/plugincore
- *(readme.md)* Update badge: npm: @yiyungent/plugincore
- *(readme.md,screenshots)* Update
- *(readme.md)* Install: CDN
- *(readme.md)* Usage
- *(readme.md)* 在页面中使用: url 错误
- *(releases.md,dist/plugincore.min.js,package.json)* 0.4.0
- *(releases.md)* Update
- *(plugincore-admin-frontend,plugincore-js-sdk)* README.md: update
- *(readme.md)* Update
- *(plugins.json)* Add some plugins
- *(readme_zh.md)* Https://moeci.com/ -> http://yiyungent.github.io/

### ⚙️ Miscellaneous Tasks

- *(package.json,license)* Update project info
- *(.github/workflows)* 1.npm-push.yml 2.npm-release.yml
- *(.github/workflows)* Fix: job 之间不能共享数据, 因此合并为一个job
- *(.github/workflows/npm-push.yml)* Push 404
- *(.github/workflows)* 1.codeql-analysis.yml 2.sync-gitee.yml
- *(.github/workflows/sync-gitee.yml)* Vant -> plugincore-admin-frontend
- *(npm-push.yml)* Order: Publish to GitHub Package
- *(npm-push.yml)* Publishing packages to npm and GitHub Packages
- *(.github/workflows)* 1.npm-push.yml 2.npm-release.yml 3.sync-gitee.yml
- *(.github/workflows/sync-gitee.yml)* GITEE_SSH_PRIVATE_KEY
- *(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)* 代码缩进: 美化

### Build

- *(package.json)* "version": "0.3.2"
- *(dist-cdn,dist)* Build: 0.3.2
- *(package-lock.json)* Npm install 后
- *(package-lock.json,package.json)* Npm install axios
- *(plugincore.aspnetcore.csproj)* <Version>1.3.4</Version> -> <Version>1.4.0</Version>

## [PluginCore.AspNetCore-v1.3.4-re-nuget-push] - 2023-12-30

### ⚙️ Miscellaneous Tasks

- *(plugincore.aspnetcore-nuget-push.yml)* Dotnet nuget push github

## [PluginCore.AspNetCore-v1.3.4] - 2023-12-30

### 📚 Documentation

- *(releases.md)* Add releases
- *(src/plugincore.aspnetcore/readme.txt)* Update

### ⚙️ Miscellaneous Tasks

- *(.github/workflows)* PluginCore.AspNetCore-*.yml: update

### Build

- *(plugincore.aspnetcore.csproj)* 1.3.3 -> 1.3.4

## [PluginCore-v2.2.3] - 2023-12-30

### ⚙️ Miscellaneous Tasks

- *(.github/workflows)* PluginCore-*.yml: update

### Build

- *(plugincore.csproj)* 2.2.2 -> 2.2.3

## [PluginCore.IPlugins.AspNetCore-v0.1.1] - 2023-12-30

### ⚙️ Miscellaneous Tasks

- *(.github/workflows)* PluginCore.IPlugins.AspNetCore-*.yml: update

### Build

- *(plugincore.iplugins.aspnetcore.csproj)* 0.1.0 -> 0.1.1

## [PluginCore.IPlugins-v0.9.1-re-release] - 2023-12-30

### 📚 Documentation

- *(src/plugincore.aspnetcore/readme.txt)* Zh -> EN

### ⚙️ Miscellaneous Tasks

- *(.github/workflows/plugincore.iplugins-release.yml)* Zip release

## [PluginCore.IPlugins-v0.9.1] - 2023-12-30

### 🚀 Features

- *(src/plugincore.iplugins/constants.cs)* Add
- *(src/plugincore.aspnetcore)* Use Constants
- *(src/**/*.cs)* //  License: Apache-2.0 -> //  License: GNU LGPLv3
- *(src/**/*.cs)* //  Project: https://moeci.com/PluginCore -> //  Project: https://yiyungent.github.io/PluginCore
- *(utils/add-header-batch.ps1)* Add
- *(src/plugincore.iplugins/constants.cs)* Add: AspNetCoreAuthenticationScheme
- *(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)* Use Constants
- *(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)* Use constants
- *(src/plugincore)* Utils/LogUtil.cs, PluginCore.csproj: 与 ILogger 结合, FrameworkReference
- *(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)* 初始化: Logger
- *(src/plugincore/utils/logutil.cs)* Add 非泛型
- *(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)* Log 输出 -> 英文
- *(src/plugincore.iplugins/constants.cs)* Add: AspNetCoreLanguageCookieName = "language"
- *(src/plugincore.iplugins/constants.cs)* Add: AspNetCoreLanguageKey = "PluginCore.Admin.Language"
- *(src/plugincore.aspnetcore)* LanguageMiddleware: 当前 Language
- *(src/plugincore.aspnetcore/controllers/pluginscontroller.cs)* 启用,禁用: Message: 使用 BasePlugin 源
- *(src/plugincore/utils/logutil.cs)* 非泛型: 需指定 categoryName
- *(src/plugincore)* 适配: LogUtil
- *(src/plugincore.aspnetcore)* 适配: LogUtil
- *(src/plugincore/utils/logutil.cs)* Add: Warn, support: (Exception ex, string message)
- *(src/plugincore/lmplements/plugincontextmanager.cs)* LogUtil.Error -> LogUtil.Warn
- *(src/plugincore.aspnetcore)* 认证与授权: 日志输出: 中文->英文

### 🐛 Bug Fixes

- *(docs)* Config.js,package.json: @lukemnet/vuepress-plugin-matomo -> vuepress-plugin-matomo
- *(src/plugincore.aspnetcore)* 转向/适配 LogUtil
- *(src/plugincore.aspnetcore)* 适配: LogUtil
- *(src/plugincore.aspnetcore/middlewares/languagemiddleware.cs)* Namespace: 语法降级
- *(src/plugincore/infrastructure/nupkgservice.cs)* 适配 LogUtil.Error
- *(src/plugincore.aspnetcore)* 适配 LogUtil.Error

### 📚 Documentation

- *(docs/docs)* Update link, deps
- *(readme*.md)* Apache-2.0 -> GNU LGPLv3
- *(docs/docs/*readme.md)* Apache-2.0 -> GNU LGPLv3
- *(readme*.md)* Badges: LICENSE: update

### ⚙️ Miscellaneous Tasks

- *(license)* Apache-2.0 -> GNU LGPLv3
- *(utils/add-header-template.txt)* License,Project: update
- *(utils/replace-batch.ps1)* Add
- *(.vscode/settings.json)* Add
- *(utils/replace-batch.ps1)* Replace: Project
- *(.github/workflows)* PluginCore.IPlugins-*.yml: nuget -> dotnet nuget, zip
- *(.github/workflows)* PluginCore.IPlugins-*.yml: shell: bash
- *(src/plugincore.iplugins/plugincore.iplugins.csproj)* 0.9.0 -> 0.9.1

## [PluginCore.Template-v0.2.2] - 2023-12-16

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore.AspNetCore-v1.3.3
- *(readme.md)* Https://moeci.com/PluginCore -> http://yiyungent.github.io/PluginCore

### ⚙️ Miscellaneous Tasks

- *(exampleplugin.csproj)* 0.1.1
- *(exampleplugin)* 0.2.2

## [PluginCore.AspNetCore-v1.3.3] - 2023-12-14

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore-v2.2.2

### Build

- *(src/plugincore.aspnetcore/plugincore.aspnetcore.csproj)* PackageReference update, 1.3.2->1.3.3

## [PluginCore-v2.2.2] - 2023-12-14

### 🐛 Bug Fixes

- *(templates/exampleplugin/*.cs)* 编码 utf-8: 前面多了个未知符号
- *(src/plugincore/lmplements/positivepluginloadcontext.cs)* PluginMainDllFilePath 被打开状态即锁定

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore-v2.2.1, PluginCore.AspNetCore-v1.3.2
- *(README.md)* 爱发电赞助

### ⚙️ Miscellaneous Tasks

- *(.github/workflows)* PluginCore*-release.yml
- *(src/plugincore/plugincore.csproj)* 2.2.1 -> 2.2.2

### Build

- *(exampleplugin/exampleplugin.csproj)* Package,config: update

## [PluginCore.AspNetCore-v1.3.2] - 2023-08-21

### 🚀 Features

- *(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)* Services: PluginFinder

### Build

- *(src/plugincore.aspnetcore/plugincore.aspnetcore.csproj)* <Version>1.3.2</Version>

## [PluginCore-v2.2.1] - 2023-08-21

### 🚀 Features

- *(docfx_project/)* Init
- *(.gitmodules,docfx_project/)* Update
- *(.editorconfig)* Code files:charset = utf-8-bom
- *(src/plugincore/lmplements/pluginfinder*)* PluginFinder.cs, PluginFinderV1.cs
- *(src/plugincore/lmplements/pluginfinder*)* PluginFinderV2,PluginFinder:PluginFinderV2

### 🐛 Bug Fixes

- *(utils/generate-release-note.ps1)* --sort=committerdate 按对应提交时间 顺序排序: old->new

### 📚 Documentation

- *(readme,releases)* Update
- *(docfx_project/index.md)* Update
- *(docfx_project/index.md)* Dotnet tool update -g docfx
- *(readme)* API Docs
- *(docs/docs/)* Add API 文档
- *(docfx_project/)* Api/index.md,articles/intro.md,index.md:Matomo Tag Manager
- *(readme.md,readme_zh.md)* FOSSA Status
- *(README.md)* 爱发电赞助
- *(readme.md,readme_zh.md)* Updated:demo
- *(docs/docs/zh/)* Info.json:DependPlugins etc
- *(readme,docs/docs/readme)* Update
- *(readme)* 增加3点特性介绍
- *(readme.md,jetbrains-variant-3.png)* 特别鸣谢
- *(README.md)* 爱发电赞助
- *(readme)* 🔌 `ASP.NET Core` lightweight plugin framework

### 🎨 Styling

- *(.editorconfig)* Charset = utf-8

### ⚙️ Miscellaneous Tasks

- *(deploy-docs-api.yml)* Add
- *(deploy-docs-api.yml)* An error in your yaml syntax
- *(deploy-docs.yml)* Deleted:deploy-docs-api.yml
- *(deploy-docs.yml)* Gh-pages-folder

### Build

- *(src/plugincore/plugincore.csproj)* <Version>2.2.1</Version>

## [PluginCore.AspNetCore-v1.3.1] - 2023-02-15

### Build

- *(plugincore.aspnetcore.csproj)* `<Version>1.3.1</Version>`

## [PluginCore.IPlugins.AspNetCore-v0.1.0] - 2023-02-15

### Build

- *(plugincore.iplugins.aspnetcore.csproj)* `<Version>0.1.0</Version>`

## [PluginCore.AspNetCore-v1.3.0] - 2023-02-15

### Build

- *(src/plugincore.aspnetcore/plugincore.aspnetcore.csproj)* `<Version>1.3.0</Version>`

## [PluginCore-v2.2.0] - 2023-02-15

### Build

- *(src/plugincore/plugincore.csproj)* `<Version>2.2.0</Version>`

## [PluginCore.IPlugins-v0.9.0] - 2023-02-15

### Build

- *(src/plugincore.iplugins/plugincore.iplugins.csproj)* <Version>0.9.0</Version>

## [PluginCore.Template-v0.2.1] - 2023-02-15

### 🚀 Features

- *(plugincore.aspnetcore,plugincore.iplugins,plugincore)* 仅保留已启用/已禁用 状态, IPlugin新方法
- *(plugincore)* PluginInfoModel,PluginConfigModelFactory:前置依赖插件:DependPlugins:建立依赖顺序
- *(templates/exampleplugin/)* Info.json:"DependPlugins": [ ];<version>0.2.1</version>

### 🐛 Bug Fixes

- *(plugincore.aspnetcore,plugincore)* IList<string> EnabledPlugins->List<string>,IList不支持Remove

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore.AspNetCore-v1.2.0,PluginCore-v2.1.0
- *(readme.md,readme_zh.md)* CLA assistant badge
- *(readme.md,readme_zh.md)* Licene badge
- *(readme.md,readme_zh.md)* Remove:[LICENSE] badge

### ⚙️ Miscellaneous Tasks

- *(.github/workflows/sync-*.yml)* Sync-bitbucket,sync-gitea,sync-gitlab
- *(.github/workflows/sync-bitbucket.yml)* Gitea->Bitbucket
- *(src/plugincore/utils/dependencysorter.cs)* // Debug.Assert

## [PluginCore.AspNetCore-v1.2.0] - 2023-02-14

### Build

- *(src/plugincore.aspnetcore/plugincore.aspnetcore.csproj)* <Version>1.2.0</Version>

## [PluginCore-v2.1.0] - 2023-02-14

### 🚀 Features

- *(src/plugincore/lmplements/)* LazyPluginLoadContext,PositivePluginLoadContext
- *(src/plugincore/lmplements/)* Base(name: pluginId),MainAssemblyName,ReferencedAssemblyNames
- *(src/plugincore/)* IPluginContext.PluginId
- *(src/plugincore.aspnetcore/controllers/debugcontroller.cs)* PluginContexts:PluginId
- *(src/plugincore/lmplements/pluginloadcontext.cs)* PluginLoadContext : LazyPluginLoadContext

### 📚 Documentation

- *(readme,releases)* PluginCore.AspNetCore-v1.1.0

### ⚙️ Miscellaneous Tasks

- *(.github/workflows/,utils/generate-release-note.ps1)* Create temp-release-note

### Build

- *(src/plugincore/plugincore.csproj)* <Version>2.1.0</Version>

## [PluginCore.AspNetCore-v1.1.0] - 2023-02-10

### 🚀 Features

- *(docs/)* .gitignore,package-lock.json
- *(src/plugincore.aspnetcore/controllers/debugcontroller.cs)* Add
- *(src/plugincore.aspnetcore/)* Update
- *(src/plugincore.aspnetcore/controllers/debugcontroller.cs)* 完成
- *(src/plugincore.aspnetcore/controllers/debugcontroller.cs)* Services

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore-v2.0.2,PluginCore.AspNetCore-v1.0.4
- *(readme.md)* 本列表由 afdian-action 自动更新
- *(README.md)* 爱发电赞助
- *(readme.md,readme_zh.md)* 相关项目:update
- *(readme.md,readme_zh.md)* Matomo Image Tracker
- *(releases.md)* Matomo Image Tracker
- *(docs/docs/zh/plugindev/guide/readme.md)* Matomo Image Tracker
- *(readme)* Project structure
- *(docs/)* Mermaid:系统设计:类图
- *(docs/docs/zh/advanced/design/readme.md)* Update
- *(docs/docs/zh/advanced/design/readme.md)* 顺序调整
- *(docs/)* Vuepress-plugin-matomo
- *(readme.md,readme_zh.md)* 版本依赖:plugincore-admin-frontend,plugincore-js-sdk
- *(readme,docs/)* 模块化

### ⚙️ Miscellaneous Tasks

- 自动更新爱发电赞助列表

### Build

- *(src/plugincore.aspnetcore/plugincore.aspnetcore.csproj)* <Version>1.1.0</Version>

## [PluginCore.AspNetCore-v1.0.4] - 2023-01-12

### Build

- *(plugincore.aspnetcore.csproj)* <Version>1.0.4</Version>

## [PluginCore-v2.0.2] - 2023-01-12

### 🐛 Bug Fixes

- *(pluginloadcontext.cs)* B插件依赖A插件时,B插件无法启用

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore.AspNetCore-v1.0.3
- *(readme.md,readme_zh.md)* 注释:hits badge

### ⚙️ Miscellaneous Tasks

- *(utils/)* 将 copyright 字样文件放到 utils/ 中,防止误识别
- *(utils/)* Copyright -> banquan
- *(utils/)* Banquan -> add-header
- *(plugincore.csproj)* <Version>2.0.2</Version>

## [PluginCore.AspNetCore-v1.0.3] - 2022-06-03

### 🚀 Features

- *(examples/aspnetcore6withnatasha)* Init
- *(examples/aspnetcore6withnatasha/)* Add Natasha

### 🐛 Bug Fixes

- *(backgroundservices/plugintimejobbackgroundservice.cs)* 定时任务:强制GC回收,抑制内存++

### 📚 Documentation

- *(docs/docs/plugindev/guide/readme.md)* Update
- *(docs/docs/)* Update
- *(readme_zh.md)* 相关项目:add:QQBotHub
- *(readme.md,readme_zh.md)* Add badge: Telegram Group

### ⚙️ Miscellaneous Tasks

- *(deploy-docs.yml)* Add:Upyun Refresh
- *(copyright-batch.bat,copyright.txt)* Renamed
- *(plugins)* <TargetFrameworks>netstandard2.0;netcoreapp3.1;net5.0;net6.0</TargetFrameworks>

### Build

- *(plugincore.aspnetcore.csproj)* <Version>1.0.3</Version>

## [GitHubApiPlugin-v0.2.0] - 2022-04-23

### 🚀 Features

- *(examples)* Update: PluginCore.AspNetCore-v1.0.2

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore.AspNetCore-v1.0.2

### ⚙️ Miscellaneous Tasks

- *(githubapiplugin-release.yml)* 使用 dotnet build

### Build

- *(plugins/*.csproj)* 使用最新插件加载机制对应插件配置,PluginCore.IPlugins.AspNetCore

## [PluginCore.AspNetCore-v1.0.2] - 2022-04-19

### 🚀 Features

- *(plugincore.aspnetcore)* PluginsController: 移除: 插件上传大小限制
- *(copyright-batch.bat,copyright.txt)* Add

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore-v2.0.1, PluginCore.AspNetCore-v1.0.1

### 🎨 Styling

- Add: copyright: *.cs

### ⚙️ Miscellaneous Tasks

- *(.editorconfig,plugincore.sln)* Add: Solution Items/.editorconfig
- *(copyright.txt)* Add empty line: 4
- *(plugincore.sln)* Add project.csproj: PluginCore.AspNetCore,PluginCore.IPlugins.AspNetCore

### Build

- *(plugincore.aspnetcore/package.json,package-lock.json)* "plugincore-admin-frontend": "0.3.2"
- *(plugincore.aspnetcore.csproj)* <Version>1.0.2</Version>

## [PluginCore.AspNetCore-v1.0.1] - 2022-04-17

### Build

- *(plugincore.aspnetcore.csproj)* <Version>1.0.1</Version>

## [PluginCore-v2.0.1] - 2022-04-17

### 🐛 Bug Fixes

- *(plugincore)* PluginLoadContext: LoadFromStream: 使用此方法, 就不会导致dll被锁定

### Build

- *(plugincore.csproj)* <Version>2.0.1</Version>

## [PluginCore.Template-v0.2.0] - 2022-04-17

### 🚀 Features

- *(templates/exampleplugin)* 适配最新 PluginCore v2

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore.AspNetCore-v1.0.0

## [PluginCore.AspNetCore-v1.0.0] - 2022-04-17

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore-v2.0.0

### ⚙️ Miscellaneous Tasks

- *(plugincore-release.yml)* Remove: npm install

### Build

- *(plugincore.aspnetcore.csproj)* <Version>1.0.0</Version>

## [PluginCore-v2.0.0] - 2022-04-17

### 🚀 Features

- *(plugincore,plugincore.aspnetcore)* AspNetCorePluginManagerBeta,PluginLoadContext,PluginFinder
- *(plugincore.aspnetcore)* Add:DebugController.PluginContext
- *(plugincore.aspnetcore)* CommonResponseModel -> BaseResponseModel

### 🐛 Bug Fixes

- *(plugincore)* 临时修复由于 PluginContextManager 单例失败 导致的插件信息丢失

### 🚜 Refactor

- *(plugincore.aspnetcore,plugincore)* 未完成
- *(plugincore.aspnetcore,plugincore)* 重构v2: 未测试
- *(plugincore.aspnetcore,plugincore)* 变量名,属性名,类名规范化

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore.AspNetCore-v0.0.5 相关

### ⚙️ Miscellaneous Tasks

- *(plugincore-release.yml)* Remove: useless: Setup Node.js
- *(plugincore,plugincore.aspnetcore)* Add dotnet6.0 support

### Build

- *(plugincore.csproj)* Add:net6.0 ; <Version>2.0.0</Version>

## [PluginCore.AspNetCore-v0.0.5] - 2022-04-16

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore.AspNetCore-v0.0.4,README.md:update

### ⚙️ Miscellaneous Tasks

- *(plugincore-nuget-push.yml,plugincore.aspnetcore-nuget-push.yml)* 缺少 npm install

### Build

- *(plugincore.aspnetcore.csproj)* <Version>0.0.5</Version>

## [PluginCore.AspNetCore-v0.0.4] - 2022-04-16

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* PluginCore.AspNetCore-0.0.3 相关更新
- *(releases.md)* Add 缺失的 v

### ⚙️ Miscellaneous Tasks

- *(plugincore-nuget-push,plugincore.aspnetcore-nuget-push.yml)* Setup Node.js: 打包前端文件进入 dll

### Build

- *(plugincore.aspnetcore.csproj)* <Version>0.0.4</Version>

## [PluginCore.AspNetCore-v0.0.3] - 2022-04-16

### 🐛 Bug Fixes

- *(plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)* PluginFinder:TryAddTransient
- *(plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)* 注释错误: 在程序启动时加载所有 已安装并启用 的插件

### 📚 Documentation

- *(releases.md)* Update
- *(readme.md,readme_zh.md)* 版本依赖 等

### Build

- *(plugincore.aspnetcore.csproj)* <Version>0.0.3</Version>

## [PluginCore.AspNetCore-v0.0.2] - 2022-04-16

### ⚙️ Miscellaneous Tasks

- *(plugincore.aspnetcore-release.yml)* Add

### Build

- *(plugincore.aspnetcore.csproj)* <Version>0.0.2</Version>

## [PluginCore.IPlugins.AspNetCore-v0.0.1] - 2022-04-16

### ⚙️ Miscellaneous Tasks

- *(plugincore.iplugins.aspnetcore)* Add

### Build

- *(plugincore.iplugins.aspnetcore.csproj)* <Version>0.0.1</Version>

## [PluginCore-v1.0.0] - 2022-04-16

### Build

- *(plugincore.csproj)* <Version>1.0.0</Version>

## [PluginCore.IPlugins-v0.8.0] - 2022-04-16

### 🚜 Refactor

- 1.提取出 PluginCore.AspNetCore,PluginCore.IPlugins.AspNetCore 2.提取出更多接口,可自由替换

### 📚 Documentation

- *(readme.md,readme_zh.md)* Add badge: hits
- *(readme.md,readme_zh.md)* Hits: link -> img

### ⚙️ Miscellaneous Tasks

- *(plugincore.iplugins.csproj)* <Version>0.8.0</Version>

## [PluginCore.AspNetCore-v0.0.1] - 2022-03-26

### 🚀 Features

- *(plugincore.aspnetcore)* PluginCore.AspNetCore,PluginCore.AspNetCore-nuget-push.yml

## [PluginCore-v0.9.3] - 2022-03-15

### 🐛 Bug Fixes

- *(plugincore,docs)* 更新 PluginCore Admin 前端: `plugincore-admin-frontend-v0.3.1`

### 📚 Documentation

- *(readme.md)* 相关项目:add:Dragonfly

## [GitHubApiPlugin-v0.1.0] - 2022-03-09

### ⚙️ Miscellaneous Tasks

- *(.github/workflows/githubapiplugin-release.yml)* 插件引用的第三方包dll没有被打包进去

## [PluginCore-v0.9.2] - 2022-03-09

### 🐛 Bug Fixes

- *(authorization/accountmanager.cs)* TokenCookieName = "PluginCore.Admin.Token"

## [PluginCore-v0.9.1] - 2022-03-08

### 🚀 Features

- *(hexoplugin)* Add
- *(plugins/hexoplugin/footer.html)* Hexo-encrypt-token: 用于第一次访问时, 在获取到 password 后, 自动解密

### 🐛 Bug Fixes

- *(readme.md,readme_zh.md,releases.md,plugintimejobbackgroundservice.cs,plugincore.csproj)* Lock 锁

### 📚 Documentation

- *(readme.md,readme_zh.md,releases.md)* Update

## [PluginCore-v0.9.0] - 2022-02-09

### Build

- *(plugincore.csproj)* 0.9.0

## [PluginCore.IPlugins-v0.7.0] - 2022-02-09

### 🚀 Features

- *(helloworldplugin.cs,iwidgetplugin.cs,plugincore)* Add: Plugin Widget
- *(pluginwidgetcontroller.cs)* 1.widgetKey.Trim('"', '\'') 2.Content:text/html
- *(helloworldplugin.cs,helloworldplugin/readme.md)* 美化挂件

### ⚙️ Miscellaneous Tasks

- *(plugincore.iplugins.csproj)* 0.7.0

## [PluginCore-v0.8.6] - 2022-02-07

### 🚀 Features

- *(plugincore.sln,examples/aspnetcore6)* New
- *(.gitignore,examples/aspnetcore6)* Add: PluginCore

### 🐛 Bug Fixes

- *(appcentercontroller.cs,pluginscontroller.cs,usercontroller.cs)* Add: [HttpGet, HttpPost]

### 📚 Documentation

- *(readme)* 版本依赖
- *(releases.md)* Add: PluginCore-v0.8.5
- *(readme.md,readme_zh.md)* Donate: add 面包多
- *(readme.md,readme_zh.md)* Donate: add: (￥5.00 起), (￥1.00 起)
- *(readme.md,readme_zh.md,releases.md,src/plugincore/plugincore.csproj)* PluginCore-v0.8.6

## [PluginCore-v0.8.5] - 2021-12-16

### 🚀 Features

- *(examples/aspnetcore3_1/startup.cs)* 跨域配置

### 📚 Documentation

- *(readme.md,releases.md)* 版本依赖, release
- *(docs/docs/.vuepress/config.js)* DocsDir: "docs/docs"
- *(readme.md)* 版本依赖: PluginCore.Template
- *(readme)* 多语言
- 多语言配置 初步
- *(readme,readme_zh)* Badages, English, 中文
- *(readme)* Badge: QQ Group

### ⚙️ Miscellaneous Tasks

- *(examples/aspnetcore3_1,plugincore)* PluginsController.cs,DemoModePlugin.csproj

### Build

- *(plugincore)* Plugincore-admin-frontend: v0.3.0; PluginCore-v0.8.5

## [PluginCore-v0.8.4] - 2021-09-01

### Build

- *(plugincore.csproj)* 0.8.4 ; PackageReference: PluginCore.IPlugins: 0.6.1

## [PluginCore.IPlugins-v0.6.1] - 2021-09-01

### 🚀 Features

- *(pluginsettingsmodelfactory.cs,plugincore.iplugins.csproj)* Remove: Newtonsoft.Json

### 📚 Documentation

- *(plugindev/guide/readme.md)* 插件开发: 使用 dotnet new 模板
- *(readme.md)* 赞助者

### ⚙️ Miscellaneous Tasks

- *(plugincore.iplugins.csproj)* 0.6.1

## [PluginCore.Template-v0.1.0] - 2021-08-25

### 🚀 Features

- *(templates)* ExamplePlugin

### 📚 Documentation

- *(readme.md)* 介绍

### ⚙️ Miscellaneous Tasks

- *(.github)* Templates.ExamplePlugin-nuget-push.yml

## [AspNetCore3_1-v0.4.7] - 2021-08-25

### 📚 Documentation

- *(readme.md,releases.md)* PluginCore-v0.8.3
- Docs folder: docs 相关文件全放在 docs文件夹

### ⚙️ Miscellaneous Tasks

- *(.github/workflows)* AspNetCore3_1-release.yml

### Build

- *(aspnetcore3_1.csproj)* PackageReference: PluginCore: 0.8.3

## [PluginCore-v0.8.3] - 2021-08-25

### 🐛 Bug Fixes

- *(plugincore)* PluginManager.cs: SkipDlls: 跳过2: 打包进入1个dll 或 打包进 1个exe

### ⚙️ Miscellaneous Tasks

- *(plugincore.csproj)* 0.8.3

## [AspNetCore3_1-v0.4.6] - 2021-08-24

### 🚀 Features

- *(demomodeplugin: demomodemiddleware.cs)* 演示模式: 禁止设置插件
- *(demomodeplugin:demomodemiddleware.cs)* 特殊可用: HelloWorldPlugin

### 🐛 Bug Fixes

- *(demomodeplugin: demomodemiddleware.cs)* User.Update

### 📚 Documentation

- *(readme.md)* Badge: CodeFactor, GitHub all releases
- *(plugindev,config.js)* Add sidebar
- *(plugindev,guide)* Code block: C# -> csharp

### ⚙️ Miscellaneous Tasks

- *(FUNDING.yml)* Add

### Build

- *(githubapiplugin.csproj)* PackageReference: PluginCore.IPlugins: 0.6.0

## [AspNetCore3_1-v0.4.5] - 2021-08-23

### ⚙️ Miscellaneous Tasks

- *(docker-compose.yml,readme.md)* Linux 区分大小写; README: 在线演示

## [AspNetCore3_1-v0.4.4] - 2021-08-23

### ⚙️ Miscellaneous Tasks

- *(aspnetcore3_1-docker-push-release.yml)* Wget: gitee

## [AspNetCore3_1-v0.4.3] - 2021-08-23

### 📚 Documentation

- *(readme.md,releases.md)* PluginCore-v0.8.2

### Build

- *(examples)* Csproj: PackageReference: PluginCore: 0.8.2

## [PluginCore-v0.8.2] - 2021-08-23

### ⚙️ Miscellaneous Tasks

- *(plugincore)* Npm install
- *(plugincore.csproj)* 0.8.2

## [AspNetCore3_1-v0.4.2] - 2021-08-23

### 🚀 Features

- *(examples/aspnetcore3_1/startup.cs)* Remove: app.UseHttpsRedirection()

## [AspNetCore3_1-v0.4.1] - 2021-08-23

### ⚙️ Miscellaneous Tasks

- *(aspnetcore3_1-docker-push-release.yml)* Deploy-docker

## [AspNetCore3_1-v0.4.0] - 2021-08-23

### 📚 Documentation

- *(readme.md,releases.md)* Release: PluginCore-v0.8.1

### Build

- *(aspnetcore3_1.csproj)* PackageReference: PluginCore: 0.8.1

## [PluginCore-v0.8.1] - 2021-08-23

### 🚀 Features

- *(plugins)* WebSocketDemoPlugin: 测试成功
- *(plugins/websocketdemoplugin)* WebSocketController.cs,WebSocketConnectionManager.cs
- *(pluginfinder.cs)* ActivatedPlugins
- *(plugins)* DemoModePlugin

### 🐛 Bug Fixes

- *(plugins/helloworldplugin/middlewares/sayhellomiddleware.cs)* Using namespace error
- *(plugincorestartupextensions.cs,logutil.cs)* Utils.LogUtil.PluginBehavior, apply: IStartupPlugin
- *(demomodeplugin: demomodemiddleware.cs)* StartsWith("", StringComparison.OrdinalIgnoreCase)
- UserController.cs: avatar url error; upgrade: frontend

### 📚 Documentation

- *(changelog.md,readme.md)* Update
- *(docs/)* Guide,PluginDev,Community,Advanced: project page init
- *(changelog.md,development.md,releases.md)* Update
- *(plugins/websocketdemoplugin/readme.md)* Url: remove http

### ⚙️ Miscellaneous Tasks

- *(plugincore.csproj)* 0.8.1

## [PluginCore-v0.8.0] - 2021-08-21

### Build

- *(plugincore.csproj)* PluginCore: 0.8.0; PluginCore.IPlugins: 0.6.0

## [PluginCore.IPlugins-v0.6.0] - 2021-08-21

### 🚀 Features

- *(testtimejobplugin,plugincore.iplugins,plugincore)* TimeJobPlugin 相关
- *(plugincore)* Utils.LogUtil.Info

### 📚 Documentation

- *(docs,package.json,readme.md,changelog.md)* Update
- *(changelog.md)* Update
- *(readme.md,docs)* Update

### ⚙️ Miscellaneous Tasks

- *(.github/workflows/*-nuget-push.yml)* Nuget Symbol package
- *(.github/workflows)* GitHubApiPlugin-release.yml
- *(.github/workflows)* Deploy-docs.yml
- *(plugincore.iplugins.csproj)* 0.6.0

## [PluginCore-v0.7.0] - 2021-08-21

### Build

- *(plugincore.csproj)* 0.7.0, PluginCore.IPlugins: 0.5.0

## [PluginCore.IPlugins-v0.5.0] - 2021-08-21

### 🚀 Features

- *(plugins/websocketdemoplugin)* New empty plugin project
- *(pluginserviceprovide.cs)* Add
- *(plugins,plugincore.iplugins,plugincore)* Add: order, add: PluginApplicationBuilderManager

### 📚 Documentation

- *(readme.md)* 1.介绍: 热插拔 2.版本依赖

### ⚙️ Miscellaneous Tasks

- *(plugincore-release.yml,plugincore.iplugins-release.yml)* Release files: add: README.md
- *(plugincore.iplugins.csproj)* 0.5.0

### Build

- *(githubapiplugin.csproj,helloworldplugin.csproj)* PackageReference: PluginCore.IPlugins: 0.4.0

## [AspNetCore3_1-v0.3.0] - 2021-08-20

### Build

- *(examples/aspnetcore3_1/aspnetcore3_1.csproj)* PluginCore: 0.6.0

## [PluginCore-v0.6.0] - 2021-08-20

### Build

- *(plugincore.csproj)* 0.6.0

## [PluginCore.IPlugins-v0.4.0] - 2021-08-20

### 🚀 Features

- *(plugincore,plugincore.iplugins,helloworldplugin)* IStartupXPlugin: 运行时 Configure(app)
- *(istartupplugin.cs,istartupxplugin.cs)* 添加注释

### 📚 Documentation

- *(readme.md)* 版本依赖

### ⚙️ Miscellaneous Tasks

- *(plugincore.iplugins.csproj)* 0.4.0

## [PluginCore-v0.5.1] - 2021-08-19

### 🚀 Features

- *(examples/aspnetcore3_1,plugins/helloworldplugin)* HellowWorldPlugin: GitHub Login
- *(examples/aspnetcore3_1,plugins/githubapiplugin)* GitHubApiPlugin
- *(plugins/helloworldplugin)* Remove GitHub API
- *(plugins/githubapiplugin)* README.md,SettingsModel.cs,settings.json
- Update: IStartupPlugin success, fix: Plugin.Enable

### 🐛 Bug Fixes

- *(src/plugincore/pluginmanager.cs)* 当插件引用dll时, 插件Controller立即使用引用dll时，报错:找不到引用dll
- *(src/plugincore/controllers/pluginscontroller.cs)* 启用插件: 启用失败时 回滚

### 📚 Documentation

- *(readme.md)* 版本依赖

### ⚙️ Miscellaneous Tasks

- *(.gitignore)* Plugins in examples
- 将要忽略的文件从索引中删除
- *(src/plugincore/plugincore.csproj)* 0.5.1

## [PluginCore-v0.5.0] - 2021-08-18

### Build

- *(plugins/helloworldplugin/helloworldplugin.csproj)* 方便开发debug,与发布到nuget

## [PluginCore.IPlugins-v0.3.0] - 2021-08-18

### 🚀 Features

- *(examples/aspnetcore5)* Add PluginCore
- *(plugincore.iplugins)* IStartupPlugin.cs, PluginCore.IPlugins.csproj
- *(plugincore)* PluginContentFilterMiddleware, IContentFilterPlugin
- *(plugincore,/plugincore.iplugins)* PluginHttpEndFilter
- LocalEmbedded: PluginCoreAdmin -> package.json
- 生成注释xml: PluginCore.IPlugins,PluginCore
- *(plugincore.iplugins.csproj)* 0.3.0

### 📚 Documentation

- *(readme.md)* 补充
- *(readme.md)* 注意
- *(readme.md)* 版本依赖,自定义前端

### ⚙️ Miscellaneous Tasks

- *(.github/workflows)* Sync-gitee.yml
- *(plugincore.csproj)* 0.4.0 -> 0.5.0
- *(plugincore.iplugins)* 注释
- *(.github/workflows)* Release: dotnet build all -> dotnet build single project

### Build

- *(plugincore.csproj)* PluginCore.IPlugins: 0.3.0

## [AspNetCore3_1-v0.2.0] - 2021-08-16

### 📚 Documentation

- *(readme.md)* 自定义前端

### Build

- *(aspnetcore3_1.csproj)* <PackageReference Include="PluginCore" Version="0.4.0" />

## [PluginCore-v0.4.0] - 2021-08-16

### 🚀 Features

- *(src/plugincore.registry)* Empty project template
- *(/plugincore.registry)* NuGetController.List
- 支持嵌入式 前端 (打包进dll)
- *(plugincore.csproj)* 0.4.0

### 📚 Documentation

- *(readme.md)* 介绍: 插件前后端分离, 依赖注入
- *(readme.md)* 1.介绍 2.规范
- *(readme.md)* 一分钟集成 -> 补充
- *(readme.md)* 规范: add lines
- *(readme.md)* 1.相关项目 2.鸣谢
- *(readme.md)* 介绍: plugindId -> pluginId

## [AspNetCore3_1-v0.1.0] - 2021-08-14

### 🚀 Features

- 支持 nupkg 格式插件

### 📚 Documentation

- *(readme.md)* 版本依赖
- *(readme)* Add logo.png
- *(readme.md)* Docker 体验

### ⚙️ Miscellaneous Tasks

- *(.github/workflows)* UserName error: dotnet-campus -> yiyungent
- Examples/AspNetCore3_1
- *(examples/aspnetcore3_1/dockerfile)* Add annotation
- *(.github/workflows)* 1.PluginCore-release.yml 2.PluginCore.IPlugins-release.yml
- *(.github/workflows)* An error in your yaml syntax on line 39
- *(.github/workflows)* Syntax error
- *(.github/workflows)* Release: remove restore
- *(.github/workflows)* Release tag name error
- *(.github/workflows)* Action package change
- *(.github/workflows)* Release: file path error
- *(.github/workflows)* Docker-push: ghcr.io
- *(aspnetcore3_1-docker-push-beta.yml)* VERSION=${{ steps.last_release.outputs.tag_name }}-beta
- *(aspnetcore3_1-docker-push-release.yml)* On: push: tags: 'AspNetCore3_1-v*'
- *(aspnetcore3_1-docker-push-release.yml)* RELEASE_VERSION: 去掉前面的 refs/tags/AspNetCore3_1-

## [PluginCore-v0.3.1] - 2021-08-10

### 🚀 Features

- LogUtil and apply
- *(plugincoreconfig.cs)* @0.1.3/dist-cdn
- *(plugincore.csproj)* 0.3.1

### 🐛 Bug Fixes

- Authentication: 401

### 📚 Documentation

- *(readme.md)* 1.介绍 2.添加插件钩子, 并应用 3.版本依赖

## [PluginCore-v0.3.0] - 2021-08-10

### 🚀 Features

- *(plugincore.csproj)* 0.3.0

### ⚙️ Miscellaneous Tasks

- *(.github/workflows)* Cd ./src/PluginCore.IPlugins
- *(.github/workflows)* Ls ./bin/Release/

## [PluginCore.IPlugins-v0.2.0] - 2021-08-10

### 🚀 Features

- Plugin 支持加载插件 wwwroot 文件夹下的 html前端等
- *(plugincore.iplugins.csproj)* 0.2.0

### 🐛 Bug Fixes

- *(examples/aspnetcore3_1)* /plugins/HelloWorldPlugin/css/main.css

### 📚 Documentation

- *(readme,license)* Add
- *(readme.md)* 介绍

### ⚙️ Miscellaneous Tasks

- *(.github/workflows)* Dotnet-version: 5.0.102
- *(.github/workflows)* Ls ./src/PluginCore/bin/Release/
- *(.github/workflows)* Nuget push path error: / -> \

## [PluginCore-v0.2.0] - 2021-08-09

### 🚀 Features

- *(fronted-admin)* Remove
- RemoteFronted, remove PluginCoreAdmin
- 保证 PluginCoreAdmin 文件夹存在
- *(plugincorehostingstartup)* Failure
- *(github action)* Nuget-push
- *(plugincore.csproj)* 0.2.0

### 🐛 Bug Fixes

- PluginCore Admin: avatar url 404: dist-cdn
- Fronted -> frontend
- *(.github/workflows)* Remove branches: main

## [PluginCore.IPlugins-v0.1.0] - 2021-08-08

### 🚀 Features

- Add controllers, examples
- 自动初始化插件目录
- *(pluginframeworkstartupextensions.cs)* UseStaticFiles: PluginCoreAdmin
- *(pluginframeworkstartupextensions.cs)* PluginFramework -> PluginCore, app.UseDefaultFiles();
- PluginCore.IPlugins, plugins: HelloWorldPlugin
- PluginCoreConfig, PluginCoreConfigFactory
- *(authorization)* Authorization, Login
- *(fronted-admin)* Fronted-admin
- *(fronted-admin,examples/aspnetcore3_1)* For PluginCore
- *(fronted-admin/src/views/login/index.vue)* Pretty
- *(examples/aspnetcore3_1/plugincoreadmin)* Update Login
- PluginCore, plugins/HelloWorldPlugin
- Logout, Login: pretty
- *(plugincore/plugincoreadmin)* Add
- Nuget config, v0.1.0

### 🐛 Bug Fixes

- Api url error, config file with init etc

<!-- generated by git-cliff -->
