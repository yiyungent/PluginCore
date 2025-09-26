# Changelog

All notable changes to this project will be documented in this file. See [conventional commits](https://www.conventionalcommits.org/) for commit guidelines.

---
## [unreleased]

### Documentation

- **(CHANGELOG.md)** update - ([85bf0d9](https://github.com/yiyungent/PluginCore/commit/85bf0d9bd8e2f9efa7e8c0db3127ec8aaa054bc8)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([a329e4e](https://github.com/yiyungent/PluginCore/commit/a329e4e7ec4048ec445e826f4536402abd832e46)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([0bbfc89](https://github.com/yiyungent/PluginCore/commit/0bbfc8955b7f6338db2125c78ec250e9eeeadcce)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([4f6b47b](https://github.com/yiyungent/PluginCore/commit/4f6b47b3f86bfce4a8f660166837a7322c568d78)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([82fd182](https://github.com/yiyungent/PluginCore/commit/82fd182007362e8877c03ae2e1a496ed762825ae)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([c3b1db5](https://github.com/yiyungent/PluginCore/commit/c3b1db5b918b6342d5a730ce2eac7552d39e3715)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([7fd216c](https://github.com/yiyungent/PluginCore/commit/7fd216cde264fb9f0481db81117ece8fe374d5ab)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([62e2bc7](https://github.com/yiyungent/PluginCore/commit/62e2bc766ce85f51459ae18c29c3bfef5c7ee64f)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([cc9f0c0](https://github.com/yiyungent/PluginCore/commit/cc9f0c01cf176eae07c24ee1cfc9fa96778be614)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([0d72cda](https://github.com/yiyungent/PluginCore/commit/0d72cdae09f22c50b2a64a4e14ae3bf16e2fdfc8)) - github-actions[bot]

### Miscellaneous Chores

- **(src/plugincore.aspnetcore)** cliff.toml, CHANGELOG.md - ([bf95c28](https://github.com/yiyungent/PluginCore/commit/bf95c287ae9902608c4711e4dec05575c2d0e794)) - yiyun

---
## [PluginCore.AspNetCore-v1.4.3](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.4.2..PluginCore.AspNetCore-v1.4.3) - 2024-08-31

### Bug Fixes

- **(PluginCore.AspNetCore/Controllers/PluginsController)** swagger [FromForm] - ([241d9a7](https://github.com/yiyungent/PluginCore/commit/241d9a72973d9cf1a11c11264a91e9370b2a6cda)) - yiyun

### Features

- **(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)** 插件 wwwroot 默认页 指定, 无需再手动 - ([469ae81](https://github.com/yiyungent/PluginCore/commit/469ae81fc31adce0c0b0701e2dfa1d98634ee184)) - yiyun

### Miscellaneous Chores

- **(PluginCore.AspNetCore.csproj)** 1.4.2 -> 1.4.3 - ([1924e0c](https://github.com/yiyungent/PluginCore/commit/1924e0c179aaac1a1d1f57a8bdebc7578d0b7650)) - yiyun

---
## [PluginCore.AspNetCore-v1.4.2](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.4.1..PluginCore.AspNetCore-v1.4.2) - 2024-04-06

### Features

- **(src/plugincore.aspnetcore/backgroundservices/plugintimejobbackgroundservice.cs)** log - ([5da04c2](https://github.com/yiyungent/PluginCore/commit/5da04c20b57a166811ef8af828c5fd0bafab844c)) - yiyun
- **(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)** plugin:startup->appstart - ([71cafe7](https://github.com/yiyungent/PluginCore/commit/71cafe7fa7348b4d8ccb0b342f31ff848c6e77e9)) - yiyun

### Build

- **(src/plugincore.aspnetcore/plugincore.aspnetcore.csproj)** 1.4.1 -> 1.4.2 - ([75f17be](https://github.com/yiyungent/PluginCore/commit/75f17becb01b6d833759811e65cf464ea7a745b1)) - yiyun

---
## [PluginCore.AspNetCore-v1.4.1](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.4.0..PluginCore.AspNetCore-v1.4.1) - 2024-03-14

### Bug Fixes

- **(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)** log, _serviceProvider - ([7d7a904](https://github.com/yiyungent/PluginCore/commit/7d7a904fd1794e26f278655f6f3f286b92bd1492)) - yiyun

### Build

- **(plugincore.aspnetcore.csproj)** 1.4.0 -> 1.4.1 - ([393d861](https://github.com/yiyungent/PluginCore/commit/393d861870c45a70f63421b5451df76e4ed9c808)) - yiyun

---
## [PluginCore.AspNetCore-v1.4.0](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.3.4..PluginCore.AspNetCore-v1.4.0) - 2024-02-15

### Features

- **(src/plugincore.aspnetcore)** authentication & Authorize - ([73a673e](https://github.com/yiyungent/PluginCore/commit/73a673e06d2a8d662a44323cf7401e91d814ae90)) - yiyun
- **(src/plugincore.aspnetcore)** 认证与授权: 优化,分离, PluginCoreStartupExtensions 优化 - ([ebab6d8](https://github.com/yiyungent/PluginCore/commit/ebab6d888a303f221e4a45ddaa28107ff4a012c4)) - yiyun
- **(src/plugincore.aspnetcore)** accountManager 部分方法静态化, 提供 HttpContext 传入方式, 相关引用处更新调用 - ([491f334](https://github.com/yiyungent/PluginCore/commit/491f334baabb28e1736b5cc1bd19a0081c54949c)) - yiyun

### Miscellaneous Chores

- **(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)** 代码缩进: 美化 - ([6a03628](https://github.com/yiyungent/PluginCore/commit/6a036289ba6f3ae1592b4e4ed58b4fb1bcdc1306)) - yiyun

### Build

- **(plugincore.aspnetcore.csproj)** <Version>1.3.4</Version> -> <Version>1.4.0</Version> - ([f786f5e](https://github.com/yiyungent/PluginCore/commit/f786f5eeb5e19558e3e73890a42f56ce812784cc)) - yiyun

---
## [PluginCore.AspNetCore-v1.3.4](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.3.3..PluginCore.AspNetCore-v1.3.4) - 2023-12-30

### Bug Fixes

- **(src/plugincore.aspnetcore)** 转向/适配 LogUtil - ([1a7c71d](https://github.com/yiyungent/PluginCore/commit/1a7c71d748fd228f1053de67572a04f466012ab8)) - yiyun
- **(src/plugincore.aspnetcore)** 适配: LogUtil - ([f150d07](https://github.com/yiyungent/PluginCore/commit/f150d07b55326133afdfe2c156464605483feb05)) - yiyun
- **(src/plugincore.aspnetcore)** 适配 LogUtil.Error - ([9c82b16](https://github.com/yiyungent/PluginCore/commit/9c82b161c5507ad1a7b78b1191dba208e8f4be45)) - yiyun
- **(src/plugincore.aspnetcore/middlewares/languagemiddleware.cs)** namespace: 语法降级 - ([a1f83fc](https://github.com/yiyungent/PluginCore/commit/a1f83fce8f1dcf2e5ead3b8a15eae006053af4de)) - yiyun

### Documentation

- **(src/plugincore.aspnetcore/readme.txt)** zh -> EN - ([f822b0b](https://github.com/yiyungent/PluginCore/commit/f822b0b79d1afca536bb7e10814226fb65988365)) - yiyun
- **(src/plugincore.aspnetcore/readme.txt)** update - ([0a3210b](https://github.com/yiyungent/PluginCore/commit/0a3210bc073252a1f18d0910dc80aabd1061a66f)) - yiyun

### Features

- **(src/**/*.cs)** //  License: Apache-2.0 -> //  License: GNU LGPLv3 - ([57366d3](https://github.com/yiyungent/PluginCore/commit/57366d3e2afdb8e20e94851aa8a09f1ee61b6d7e)) - yiyun
- **(src/**/*.cs)** //  Project: https://moeci.com/PluginCore -> //  Project: https://yiyungent.github.io/PluginCore - ([7420480](https://github.com/yiyungent/PluginCore/commit/742048065978c1b8597fab3d52f011db4247fbda)) - yiyun
- **(src/plugincore.aspnetcore)** use Constants - ([6cd128a](https://github.com/yiyungent/PluginCore/commit/6cd128a2ce6da83f8cfee46ae03a7af44380e791)) - yiyun
- **(src/plugincore.aspnetcore)** languageMiddleware: 当前 Language - ([b0d79e7](https://github.com/yiyungent/PluginCore/commit/b0d79e7a0ae469cc295d87ed8a7c97a355cbc7a1)) - yiyun
- **(src/plugincore.aspnetcore)** 适配: LogUtil - ([b09da39](https://github.com/yiyungent/PluginCore/commit/b09da39199d614f40ec533d7a472069c40121fae)) - yiyun
- **(src/plugincore.aspnetcore)** 认证与授权: 日志输出: 中文->英文 - ([1470a99](https://github.com/yiyungent/PluginCore/commit/1470a9913fff6e0df37495d1143f5a55eed995fd)) - yiyun
- **(src/plugincore.aspnetcore/controllers/pluginscontroller.cs)** 启用,禁用: Message: 使用 BasePlugin 源 - ([d6c8a33](https://github.com/yiyungent/PluginCore/commit/d6c8a3361a7573c68031ad6e8bd8aa35c65035d4)) - yiyun
- **(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)** use Constants - ([68952a1](https://github.com/yiyungent/PluginCore/commit/68952a13613d469cefe9fc9fd13ab7525dd93f78)) - yiyun
- **(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)** use constants - ([37798ef](https://github.com/yiyungent/PluginCore/commit/37798efc92f82e5e5f4d696b6de5466809a18d48)) - yiyun
- **(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)** 初始化: Logger - ([6e24a5a](https://github.com/yiyungent/PluginCore/commit/6e24a5a391af60f8ec0ba85e8ea86ea23c49f522)) - yiyun
- **(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)** log 输出 -> 英文 - ([e41b73a](https://github.com/yiyungent/PluginCore/commit/e41b73adda0ca5d9f1f60109ee8b6fe7de39deda)) - yiyun

### Build

- **(plugincore.aspnetcore.csproj)** 1.3.3 -> 1.3.4 - ([84550cc](https://github.com/yiyungent/PluginCore/commit/84550cc9159c387cedbc8beda282dfcfea9cdcb1)) - yiyun

---
## [PluginCore.AspNetCore-v1.3.3](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.3.2..PluginCore.AspNetCore-v1.3.3) - 2023-12-14

### Build

- **(src/plugincore.aspnetcore/plugincore.aspnetcore.csproj)** packageReference update, 1.3.2->1.3.3 - ([6fc3d1a](https://github.com/yiyungent/PluginCore/commit/6fc3d1ad6359a5f8690babfc6949e0821d5bd6c5)) - yiyun

---
## [PluginCore.AspNetCore-v1.3.2](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.3.1..PluginCore.AspNetCore-v1.3.2) - 2023-08-21

### Features

- **(src/plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)** services: PluginFinder - ([144db05](https://github.com/yiyungent/PluginCore/commit/144db05576001a72f9cbd6beea0b3b5baa6a082c)) - yiyun

### Build

- **(src/plugincore.aspnetcore/plugincore.aspnetcore.csproj)** <Version>1.3.2</Version> - ([dfdd17d](https://github.com/yiyungent/PluginCore/commit/dfdd17dbcb2fc636cc102ab76497ff5768c64a4e)) - yiyun

---
## [PluginCore.AspNetCore-v1.3.1](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.3.0..PluginCore.AspNetCore-v1.3.1) - 2023-02-15

### Build

- **(plugincore.aspnetcore.csproj)** `<Version>1.3.1</Version>` - ([39e0d03](https://github.com/yiyungent/PluginCore/commit/39e0d037c6f3e0dcf4845b5ca8ae2aa41142a474)) - yiyun

---
## [PluginCore.AspNetCore-v1.3.0](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.2.0..PluginCore.AspNetCore-v1.3.0) - 2023-02-15

### Bug Fixes

- **(plugincore.aspnetcore,plugincore)** iList<string> EnabledPlugins->List<string>,IList不支持Remove - ([4d5d30e](https://github.com/yiyungent/PluginCore/commit/4d5d30e66c4c28998a7a6ac96bf3ffb25e4872b4)) - yiyun

### Features

- **(plugincore.aspnetcore,plugincore.iplugins,plugincore)** 仅保留已启用/已禁用 状态, IPlugin新方法 - ([e843a5b](https://github.com/yiyungent/PluginCore/commit/e843a5ba9fad4e88290c09bb3282b730c44c5a06)) - yiyun

### Build

- **(src/plugincore.aspnetcore/plugincore.aspnetcore.csproj)** `<Version>1.3.0</Version>` - ([64b9775](https://github.com/yiyungent/PluginCore/commit/64b977569312539aa2775235ae1f0fca5517ddcd)) - yiyun

---
## [PluginCore.AspNetCore-v1.2.0](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.1.0..PluginCore.AspNetCore-v1.2.0) - 2023-02-14

### Features

- **(src/plugincore.aspnetcore/controllers/debugcontroller.cs)** pluginContexts:PluginId - ([f82b7b1](https://github.com/yiyungent/PluginCore/commit/f82b7b199420b15fc6f38e8f26c8094ee4be1b88)) - yiyun

### Build

- **(src/plugincore.aspnetcore/plugincore.aspnetcore.csproj)** <Version>1.2.0</Version> - ([fe418e0](https://github.com/yiyungent/PluginCore/commit/fe418e0f67dd8ed1fd2d78dc3f4106c8dc1a396b)) - yiyun

---
## [PluginCore.AspNetCore-v1.1.0](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.0.4..PluginCore.AspNetCore-v1.1.0) - 2023-02-10

### Features

- **(src/plugincore.aspnetcore/)** update - ([52ee48f](https://github.com/yiyungent/PluginCore/commit/52ee48fe234040fd0cc0a4d21f3c2e1c9483735e)) - yiyun
- **(src/plugincore.aspnetcore/controllers/debugcontroller.cs)** add - ([196823d](https://github.com/yiyungent/PluginCore/commit/196823d6e3b39eb70d1d4bb55d7d4c8433ee64fc)) - yiyun
- **(src/plugincore.aspnetcore/controllers/debugcontroller.cs)** 完成 - ([2a2e213](https://github.com/yiyungent/PluginCore/commit/2a2e2131ee4b25912c16ca0d01ef408702cbfc39)) - yiyun
- **(src/plugincore.aspnetcore/controllers/debugcontroller.cs)** services - ([22edc22](https://github.com/yiyungent/PluginCore/commit/22edc229c1bedc4667a920ef62c11c8e002ba9d2)) - yiyun

### Build

- **(src/plugincore.aspnetcore/plugincore.aspnetcore.csproj)** <Version>1.1.0</Version> - ([3bdb176](https://github.com/yiyungent/PluginCore/commit/3bdb17602c3258386711b200917e567c82dc442b)) - yiyun

---
## [PluginCore.AspNetCore-v1.0.4](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.0.3..PluginCore.AspNetCore-v1.0.4) - 2023-01-12

### Build

- **(plugincore.aspnetcore.csproj)** <Version>1.0.4</Version> - ([46cf5dd](https://github.com/yiyungent/PluginCore/commit/46cf5dd5f955f6b648323e5216af97dd177e5a4b)) - yiyun

---
## [PluginCore.AspNetCore-v1.0.3](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.0.2..PluginCore.AspNetCore-v1.0.3) - 2022-06-03

### Bug Fixes

- **(backgroundservices/plugintimejobbackgroundservice.cs)** 定时任务:强制GC回收,抑制内存++ - ([434e824](https://github.com/yiyungent/PluginCore/commit/434e82403b6aa2050945eeaa1131ea7965bdbc5f)) - yiyun

### Build

- **(plugincore.aspnetcore.csproj)** <Version>1.0.3</Version> - ([7da5638](https://github.com/yiyungent/PluginCore/commit/7da5638672571ca0ef057ebf73493260886b903e)) - yiyun

---
## [PluginCore.AspNetCore-v1.0.2](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.0.1..PluginCore.AspNetCore-v1.0.2) - 2022-04-19

### Features

- **(plugincore.aspnetcore)** pluginsController: 移除: 插件上传大小限制 - ([90f8d67](https://github.com/yiyungent/PluginCore/commit/90f8d671a840ae8ca3688f4a5cbc22e568a6159e)) - yiyun

### Style

- add: copyright: *.cs - ([9643dce](https://github.com/yiyungent/PluginCore/commit/9643dce112861a440d63306cb555accbed3d5111)) - yiyun

### Build

- **(plugincore.aspnetcore.csproj)** <Version>1.0.2</Version> - ([b4eb1cc](https://github.com/yiyungent/PluginCore/commit/b4eb1cca9039ff95a516ba6d4000f10d156e03d0)) - yiyun
- **(plugincore.aspnetcore/package.json,package-lock.json)** "plugincore-admin-frontend": "0.3.2" - ([7a70b20](https://github.com/yiyungent/PluginCore/commit/7a70b2003128bf569b4fdd4b7eaa662a92da8ee8)) - yiyun

---
## [PluginCore.AspNetCore-v1.0.1](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.0.0..PluginCore.AspNetCore-v1.0.1) - 2022-04-17

### Build

- **(plugincore.aspnetcore.csproj)** <Version>1.0.1</Version> - ([ab348ce](https://github.com/yiyungent/PluginCore/commit/ab348ceca937f1b1f4ba8c83f4816b9a0ae7ea3e)) - yiyun

---
## [PluginCore.AspNetCore-v1.0.0](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v0.0.5..PluginCore.AspNetCore-v1.0.0) - 2022-04-17

### Bug Fixes

- **(plugincore)** 临时修复由于 PluginContextManager 单例失败 导致的插件信息丢失 - ([fa613b4](https://github.com/yiyungent/PluginCore/commit/fa613b4c46e41c906fe955eb8f62c3f4937795bc)) - yiyun

### Features

- **(plugincore,plugincore.aspnetcore)** aspNetCorePluginManagerBeta,PluginLoadContext,PluginFinder - ([9d65a59](https://github.com/yiyungent/PluginCore/commit/9d65a590e3e0850251f6d815c322c7c5d9c7cf3f)) - yiyun
- **(plugincore.aspnetcore)** add:DebugController.PluginContext - ([cd8de63](https://github.com/yiyungent/PluginCore/commit/cd8de636c8d7e2b125e11c6ce4091567cb97d4bc)) - yiyun
- **(plugincore.aspnetcore)** commonResponseModel -> BaseResponseModel - ([1a0e834](https://github.com/yiyungent/PluginCore/commit/1a0e834b7dfdb45c45770d42b1733f1cb449a6ca)) - yiyun

### Refactoring

- **(plugincore.aspnetcore,plugincore)** 未完成 - ([a151bcd](https://github.com/yiyungent/PluginCore/commit/a151bcda125cb7e9b5fe11d44e1389afa7a1db5e)) - yiyun
- **(plugincore.aspnetcore,plugincore)** 重构v2: 未测试 - ([53dde31](https://github.com/yiyungent/PluginCore/commit/53dde31116bd6455d33f7d7006b6fd1430f3694b)) - yiyun
- **(plugincore.aspnetcore,plugincore)** 变量名,属性名,类名规范化 - ([eaadabf](https://github.com/yiyungent/PluginCore/commit/eaadabfd759228da245af1d9bd5b86e557540d28)) - yiyun

### Build

- **(plugincore.aspnetcore.csproj)** <Version>1.0.0</Version> - ([9b5caf4](https://github.com/yiyungent/PluginCore/commit/9b5caf4c4cdc543025c5493d35275a836cae61a9)) - yiyun

---
## [PluginCore.AspNetCore-v0.0.5](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v0.0.4..PluginCore.AspNetCore-v0.0.5) - 2022-04-16

### Build

- **(plugincore.aspnetcore.csproj)** <Version>0.0.5</Version> - ([3e5cb09](https://github.com/yiyungent/PluginCore/commit/3e5cb0921a9faa33f93169c2d983011a1e8c5b5f)) - yiyun

---
## [PluginCore.AspNetCore-v0.0.4](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v0.0.3..PluginCore.AspNetCore-v0.0.4) - 2022-04-16

### Build

- **(plugincore.aspnetcore.csproj)** <Version>0.0.4</Version> - ([adffab3](https://github.com/yiyungent/PluginCore/commit/adffab3fecee6696709c1d325b8c11e5a5389031)) - yiyun

---
## [PluginCore.AspNetCore-v0.0.3](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v0.0.2..PluginCore.AspNetCore-v0.0.3) - 2022-04-16

### Bug Fixes

- **(plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)** pluginFinder:TryAddTransient - ([03e9235](https://github.com/yiyungent/PluginCore/commit/03e9235c3c7fe68a3209cb1109792869e78aaa4e)) - yiyun
- **(plugincore.aspnetcore/extensions/plugincorestartupextensions.cs)** 注释错误: 在程序启动时加载所有 已安装并启用 的插件 - ([844826a](https://github.com/yiyungent/PluginCore/commit/844826a630f99c2d866a98e6556404d4b1857e52)) - yiyun

### Build

- **(plugincore.aspnetcore.csproj)** <Version>0.0.3</Version> - ([ee81a45](https://github.com/yiyungent/PluginCore/commit/ee81a4536af17a85bbebc07bc0c980af8c00ad63)) - yiyun

---
## [PluginCore.AspNetCore-v0.0.2](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v0.0.1..PluginCore.AspNetCore-v0.0.2) - 2022-04-16

### Refactoring

- 1.提取出 PluginCore.AspNetCore,PluginCore.IPlugins.AspNetCore 2.提取出更多接口,可自由替换 - ([fffd8d9](https://github.com/yiyungent/PluginCore/commit/fffd8d91c23fd6e4a4d09cbf91975beb3cf7acf0)) - yiyun

### Build

- **(plugincore.aspnetcore.csproj)** <Version>0.0.2</Version> - ([ba7b274](https://github.com/yiyungent/PluginCore/commit/ba7b27496a4ba784c271305489b9f2f8e6f74ad3)) - yiyun

---
## [PluginCore.AspNetCore-v0.0.1](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.9.3..PluginCore.AspNetCore-v0.0.1) - 2022-03-26

### Features

- **(plugincore.aspnetcore)** pluginCore.AspNetCore,PluginCore.AspNetCore-nuget-push.yml - ([491f63e](https://github.com/yiyungent/PluginCore/commit/491f63e3362129a2239d87b090aa04cc2e414e9a)) - yiyun

<!-- generated by git-cliff -->
