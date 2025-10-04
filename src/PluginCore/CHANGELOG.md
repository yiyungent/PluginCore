# Changelog

All notable changes to this project will be documented in this file. See [conventional commits](https://www.conventionalcommits.org/) for commit guidelines.

---
## [unreleased]

### Documentation

- **(CHANGELOG.md)** update - ([0bbfc89](https://github.com/yiyungent/PluginCore/commit/0bbfc8955b7f6338db2125c78ec250e9eeeadcce)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([4f6b47b](https://github.com/yiyungent/PluginCore/commit/4f6b47b3f86bfce4a8f660166837a7322c568d78)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([82fd182](https://github.com/yiyungent/PluginCore/commit/82fd182007362e8877c03ae2e1a496ed762825ae)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([c3b1db5](https://github.com/yiyungent/PluginCore/commit/c3b1db5b918b6342d5a730ce2eac7552d39e3715)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([7fd216c](https://github.com/yiyungent/PluginCore/commit/7fd216cde264fb9f0481db81117ece8fe374d5ab)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([62e2bc7](https://github.com/yiyungent/PluginCore/commit/62e2bc766ce85f51459ae18c29c3bfef5c7ee64f)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([cc9f0c0](https://github.com/yiyungent/PluginCore/commit/cc9f0c01cf176eae07c24ee1cfc9fa96778be614)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([0d72cda](https://github.com/yiyungent/PluginCore/commit/0d72cdae09f22c50b2a64a4e14ae3bf16e2fdfc8)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([868a40b](https://github.com/yiyungent/PluginCore/commit/868a40b1b79ab35967fdf46d34a6c6c8f2a33ba1)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([8fcfc13](https://github.com/yiyungent/PluginCore/commit/8fcfc13f80d00b12989cc4981a26d36f3baaccef)) - github-actions[bot]
- **(CHANGELOG.md)** update - ([8cdc35d](https://github.com/yiyungent/PluginCore/commit/8cdc35dcfa3029cbbde84dcc481a301c38e1a152)) - github-actions[bot]

### Miscellaneous Chores

- **(cliff.toml)** add - ([5614ef0](https://github.com/yiyungent/PluginCore/commit/5614ef024d644349095e19a0016bb23d989b0c90)) - yiyun

### Ci

- **(changelog.yml)** changelog.yml, PluginCore/CHANGELOG.md - ([34ab464](https://github.com/yiyungent/PluginCore/commit/34ab46467101933f3b4b966fbe9c7a21f510494e)) - yiyun

---
## [PluginCore-v2.2.5](https://github.com/yiyungent/PluginCore/compare/PluginCore-v2.2.4..PluginCore-v2.2.5) - 2024-04-06

### Bug Fixes

- **(src/plugincore/lmplements/lazypluginloadcontext.cs)** dll 忽略版本搜索 - ([af8e6e2](https://github.com/yiyungent/PluginCore/commit/af8e6e299726abacdfc0eb125339235a5ee2823d)) - yiyun

### Build

- **(src/plugincore/plugincore.csproj)** 2.2.4 -> 2.2.5 - ([f2dad72](https://github.com/yiyungent/PluginCore/commit/f2dad724d336902e64cc5d07c7974fe3d08bb0d7)) - yiyun

---
## [PluginCore-v2.2.4](https://github.com/yiyungent/PluginCore/compare/PluginCore-v2.2.3..PluginCore-v2.2.4) - 2024-03-14

### Features

- **(src/plugincore/utils/logutil.cs)** add LogCategoryName - ([9e66363](https://github.com/yiyungent/PluginCore/commit/9e66363b207e1b6b10e11e8e934097530280c37f)) - yiyun

### Build

- **(src/plugincore/plugincore.csproj)** 2.2.3 -> 2.2.4 - ([0c20c18](https://github.com/yiyungent/PluginCore/commit/0c20c18c452333540cde8a6036b0c2a8f805b376)) - yiyun

---
## [PluginCore-v2.2.3](https://github.com/yiyungent/PluginCore/compare/PluginCore-v2.2.2..PluginCore-v2.2.3) - 2023-12-30

### Bug Fixes

- **(src/plugincore/infrastructure/nupkgservice.cs)** 适配 LogUtil.Error - ([1bbd0ad](https://github.com/yiyungent/PluginCore/commit/1bbd0ad8c2b69accf8f19d28c47857844b42f527)) - yiyun

### Features

- **(src/**/*.cs)** //  License: Apache-2.0 -> //  License: GNU LGPLv3 - ([57366d3](https://github.com/yiyungent/PluginCore/commit/57366d3e2afdb8e20e94851aa8a09f1ee61b6d7e)) - yiyun
- **(src/**/*.cs)** //  Project: https://moeci.com/PluginCore -> //  Project: https://yiyungent.github.io/PluginCore - ([7420480](https://github.com/yiyungent/PluginCore/commit/742048065978c1b8597fab3d52f011db4247fbda)) - yiyun
- **(src/plugincore)** utils/LogUtil.cs, PluginCore.csproj: 与 ILogger 结合, FrameworkReference - ([d638a76](https://github.com/yiyungent/PluginCore/commit/d638a76fbc5733ec19c7dc24450e18b1c5803f70)) - yiyun
- **(src/plugincore)** 适配: LogUtil - ([7e0de48](https://github.com/yiyungent/PluginCore/commit/7e0de488c570ff869948160c8bee9edfb8f4192f)) - yiyun
- **(src/plugincore/lmplements/plugincontextmanager.cs)** logUtil.Error -> LogUtil.Warn - ([d438fad](https://github.com/yiyungent/PluginCore/commit/d438fadc70749cdcff8d22a50c004541d15cb010)) - yiyun
- **(src/plugincore/utils/logutil.cs)** add 非泛型 - ([e108632](https://github.com/yiyungent/PluginCore/commit/e108632414809f496b9bbc9cd45ac73e58d53d36)) - yiyun
- **(src/plugincore/utils/logutil.cs)** 非泛型: 需指定 categoryName - ([3aa0410](https://github.com/yiyungent/PluginCore/commit/3aa0410b97727651659e15e786572bd7335d5963)) - yiyun
- **(src/plugincore/utils/logutil.cs)** add: Warn, support: (Exception ex, string message) - ([897a910](https://github.com/yiyungent/PluginCore/commit/897a910028fd7dce1410c5ea4ad38b2b2a6a7c03)) - yiyun

### Build

- **(plugincore.csproj)** 2.2.2 -> 2.2.3 - ([b0838bc](https://github.com/yiyungent/PluginCore/commit/b0838bc9175c1739972f89fb8c65342d16b112c9)) - yiyun

---
## [PluginCore-v2.2.2](https://github.com/yiyungent/PluginCore/compare/PluginCore-v2.2.1..PluginCore-v2.2.2) - 2023-12-14

### Bug Fixes

- **(src/plugincore/lmplements/positivepluginloadcontext.cs)** pluginMainDllFilePath 被打开状态即锁定 - ([4c81402](https://github.com/yiyungent/PluginCore/commit/4c814028fdaa377608cf9eb849bc732a1fcc70cc)) - yiyun

### Miscellaneous Chores

- **(src/plugincore/plugincore.csproj)** 2.2.1 -> 2.2.2 - ([dcb3dff](https://github.com/yiyungent/PluginCore/commit/dcb3dffb3062f62ae0ba1e3c8115ae8300aaf9ce)) - yiyun

---
## [PluginCore-v2.2.1](https://github.com/yiyungent/PluginCore/compare/PluginCore-v2.2.0..PluginCore-v2.2.1) - 2023-08-21

### Features

- **(src/plugincore/lmplements/pluginfinder*)** pluginFinder.cs, PluginFinderV1.cs - ([721ae36](https://github.com/yiyungent/PluginCore/commit/721ae36750281731f3ae10dfed63c44d013cdc1e)) - yiyun
- **(src/plugincore/lmplements/pluginfinder*)** pluginFinderV2,PluginFinder:PluginFinderV2 - ([6c7ad5f](https://github.com/yiyungent/PluginCore/commit/6c7ad5f56a8649bad33779e6fcc9d2a4aa06271a)) - yiyun

### Build

- **(src/plugincore/plugincore.csproj)** <Version>2.2.1</Version> - ([12b322a](https://github.com/yiyungent/PluginCore/commit/12b322a58a148dc908fb3924df9bf8a7fbd129f7)) - yiyun

---
## [PluginCore-v2.2.0](https://github.com/yiyungent/PluginCore/compare/PluginCore-v2.1.0..PluginCore-v2.2.0) - 2023-02-15

### Bug Fixes

- **(plugincore.aspnetcore,plugincore)** iList<string> EnabledPlugins->List<string>,IList不支持Remove - ([4d5d30e](https://github.com/yiyungent/PluginCore/commit/4d5d30e66c4c28998a7a6ac96bf3ffb25e4872b4)) - yiyun

### Features

- **(plugincore)** pluginInfoModel,PluginConfigModelFactory:前置依赖插件:DependPlugins:建立依赖顺序 - ([f5841ce](https://github.com/yiyungent/PluginCore/commit/f5841ce42c296b93e1e34a6ed9c8a84767beb795)) - yiyun
- **(plugincore.aspnetcore,plugincore.iplugins,plugincore)** 仅保留已启用/已禁用 状态, IPlugin新方法 - ([e843a5b](https://github.com/yiyungent/PluginCore/commit/e843a5ba9fad4e88290c09bb3282b730c44c5a06)) - yiyun

### Miscellaneous Chores

- **(src/plugincore/utils/dependencysorter.cs)** // Debug.Assert - ([52421fd](https://github.com/yiyungent/PluginCore/commit/52421fd4d150c8c568c63e8d053fceb66d0a7ff2)) - yiyun

### Build

- **(src/plugincore/plugincore.csproj)** `<Version>2.2.0</Version>` - ([11a2043](https://github.com/yiyungent/PluginCore/commit/11a20435ec261dcbb4b10e7a99ec10d2c80743c7)) - yiyun

---
## [PluginCore-v2.1.0](https://github.com/yiyungent/PluginCore/compare/PluginCore-v2.0.2..PluginCore-v2.1.0) - 2023-02-14

### Features

- **(src/plugincore/)** iPluginContext.PluginId - ([45b22e0](https://github.com/yiyungent/PluginCore/commit/45b22e006879043a68dc61ad33800c149784d257)) - yiyun
- **(src/plugincore/lmplements/)** lazyPluginLoadContext,PositivePluginLoadContext - ([d7b9918](https://github.com/yiyungent/PluginCore/commit/d7b9918ff86bded2ad65ae3a2b6251f88a3df185)) - yiyun
- **(src/plugincore/lmplements/)** base(name: pluginId),MainAssemblyName,ReferencedAssemblyNames - ([98be798](https://github.com/yiyungent/PluginCore/commit/98be798bfbd3462d19d96f3dd301d2b916240f70)) - yiyun
- **(src/plugincore/lmplements/pluginloadcontext.cs)** pluginLoadContext : LazyPluginLoadContext - ([8e957ab](https://github.com/yiyungent/PluginCore/commit/8e957ab53b9446eab35e6664f2b4397ff07cc950)) - yiyun

### Build

- **(src/plugincore/plugincore.csproj)** <Version>2.1.0</Version> - ([1a24c03](https://github.com/yiyungent/PluginCore/commit/1a24c03b5fa28eb0008e0a82d2e9feaba2d22042)) - yiyun

---
## [PluginCore-v2.0.2](https://github.com/yiyungent/PluginCore/compare/PluginCore.AspNetCore-v1.0.2..PluginCore-v2.0.2) - 2023-01-12

### Bug Fixes

- **(pluginloadcontext.cs)** b插件依赖A插件时,B插件无法启用 - ([4eb8dac](https://github.com/yiyungent/PluginCore/commit/4eb8daca89d23602b58b104162fc54910fc39f76)) - yiyun

### Miscellaneous Chores

- **(plugincore.csproj)** <Version>2.0.2</Version> - ([b3e5522](https://github.com/yiyungent/PluginCore/commit/b3e5522f02e02f66fcdeed35aed3613b78637ab6)) - yiyun

---
## [PluginCore.AspNetCore-v1.0.2](https://github.com/yiyungent/PluginCore/compare/PluginCore-v2.0.1..PluginCore.AspNetCore-v1.0.2) - 2022-04-19

### Style

- add: copyright: *.cs - ([9643dce](https://github.com/yiyungent/PluginCore/commit/9643dce112861a440d63306cb555accbed3d5111)) - yiyun

---
## [PluginCore-v2.0.1](https://github.com/yiyungent/PluginCore/compare/PluginCore-v1.0.0..PluginCore-v2.0.1) - 2022-04-17

### Bug Fixes

- **(plugincore)** 临时修复由于 PluginContextManager 单例失败 导致的插件信息丢失 - ([fa613b4](https://github.com/yiyungent/PluginCore/commit/fa613b4c46e41c906fe955eb8f62c3f4937795bc)) - yiyun
- **(plugincore)** pluginLoadContext: LoadFromStream: 使用此方法, 就不会导致dll被锁定 - ([8af287a](https://github.com/yiyungent/PluginCore/commit/8af287a44dc0d61db1cb449b7b3225595ac07f03)) - yiyun

### Features

- **(plugincore,plugincore.aspnetcore)** aspNetCorePluginManagerBeta,PluginLoadContext,PluginFinder - ([9d65a59](https://github.com/yiyungent/PluginCore/commit/9d65a590e3e0850251f6d815c322c7c5d9c7cf3f)) - yiyun

### Refactoring

- **(plugincore.aspnetcore,plugincore)** 未完成 - ([a151bcd](https://github.com/yiyungent/PluginCore/commit/a151bcda125cb7e9b5fe11d44e1389afa7a1db5e)) - yiyun
- **(plugincore.aspnetcore,plugincore)** 重构v2: 未测试 - ([53dde31](https://github.com/yiyungent/PluginCore/commit/53dde31116bd6455d33f7d7006b6fd1430f3694b)) - yiyun
- **(plugincore.aspnetcore,plugincore)** 变量名,属性名,类名规范化 - ([eaadabf](https://github.com/yiyungent/PluginCore/commit/eaadabfd759228da245af1d9bd5b86e557540d28)) - yiyun

### Build

- **(plugincore.csproj)** add:net6.0 ; <Version>2.0.0</Version> - ([3608906](https://github.com/yiyungent/PluginCore/commit/3608906e4860f2514541d7fc0a9d187b2bcd3076)) - yiyun
- **(plugincore.csproj)** <Version>2.0.1</Version> - ([ce4cfa8](https://github.com/yiyungent/PluginCore/commit/ce4cfa87525fb6a2fc4c0f87943213ef13fd34bd)) - yiyun

---
## [PluginCore-v1.0.0](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.9.3..PluginCore-v1.0.0) - 2022-04-16

### Refactoring

- 1.提取出 PluginCore.AspNetCore,PluginCore.IPlugins.AspNetCore 2.提取出更多接口,可自由替换 - ([fffd8d9](https://github.com/yiyungent/PluginCore/commit/fffd8d91c23fd6e4a4d09cbf91975beb3cf7acf0)) - yiyun

### Build

- **(plugincore.csproj)** <Version>1.0.0</Version> - ([96f6b02](https://github.com/yiyungent/PluginCore/commit/96f6b028b958f415b8013d9786d361f902fa3bea)) - yiyun

---
## [PluginCore-v0.9.3](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.9.2..PluginCore-v0.9.3) - 2022-03-15

### Bug Fixes

- **(plugincore,docs)** 更新 PluginCore Admin 前端: `plugincore-admin-frontend-v0.3.1` - ([a9772bb](https://github.com/yiyungent/PluginCore/commit/a9772bb971d19ec05b982d3f2ef2ddfcbc377e6e)) - yiyun

---
## [PluginCore-v0.9.2](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.9.1..PluginCore-v0.9.2) - 2022-03-09

### Bug Fixes

- **(authorization/accountmanager.cs)** tokenCookieName = "PluginCore.Admin.Token" - ([c643c3b](https://github.com/yiyungent/PluginCore/commit/c643c3bcee7555118f5004aed64c1f73664ada9c)) - yiyun

---
## [PluginCore-v0.9.1](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.9.0..PluginCore-v0.9.1) - 2022-03-08

### Bug Fixes

- **(readme.md,readme_zh.md,releases.md,plugintimejobbackgroundservice.cs,plugincore.csproj)** lock 锁 - ([d233779](https://github.com/yiyungent/PluginCore/commit/d23377993838cdd4fc616b13823964d043b2a526)) - yiyun

---
## [PluginCore-v0.9.0](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.8.6..PluginCore-v0.9.0) - 2022-02-09

### Features

- **(helloworldplugin.cs,iwidgetplugin.cs,plugincore)** add: Plugin Widget - ([0f010e9](https://github.com/yiyungent/PluginCore/commit/0f010e9cb9b11c4ccda51c40656dc5fd82a16a01)) - yiyun
- **(pluginwidgetcontroller.cs)** 1.widgetKey.Trim('"', '\'') 2.Content:text/html - ([27ae842](https://github.com/yiyungent/PluginCore/commit/27ae8422c6105328635328cd9170e5aa13243ad1)) - yiyun

### Build

- **(plugincore.csproj)** 0.9.0 - ([8f1ca64](https://github.com/yiyungent/PluginCore/commit/8f1ca6470c8c55eb63930e418a6866cc29a5005e)) - yiyun

---
## [PluginCore-v0.8.6](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.8.5..PluginCore-v0.8.6) - 2022-02-07

### Bug Fixes

- **(appcentercontroller.cs,pluginscontroller.cs,usercontroller.cs)** add: [HttpGet, HttpPost] - ([85d4f1d](https://github.com/yiyungent/PluginCore/commit/85d4f1d60585173158981dd4f0b2c75dbd43bbe2)) - yiyun

### Documentation

- **(readme.md,readme_zh.md,releases.md,src/plugincore/plugincore.csproj)** pluginCore-v0.8.6 - ([345f0f3](https://github.com/yiyungent/PluginCore/commit/345f0f32d3896db03efafc3cad06967fd32d40a2)) - yiyun

---
## [PluginCore-v0.8.5](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.8.4..PluginCore-v0.8.5) - 2021-12-16

### Miscellaneous Chores

- **(examples/aspnetcore3_1,plugincore)** pluginsController.cs,DemoModePlugin.csproj - ([f43286a](https://github.com/yiyungent/PluginCore/commit/f43286a10116217d8cb05e1e7dc875669a1159d3)) - yiyun

### Build

- **(plugincore)** plugincore-admin-frontend: v0.3.0; PluginCore-v0.8.5 - ([17bdfdb](https://github.com/yiyungent/PluginCore/commit/17bdfdb6026fd8f983a2b685eb9bf03c9504854c)) - yiyun

---
## [PluginCore-v0.8.4](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.8.3..PluginCore-v0.8.4) - 2021-09-01

### Build

- **(plugincore.csproj)** 0.8.4 ; PackageReference: PluginCore.IPlugins: 0.6.1 - ([d359350](https://github.com/yiyungent/PluginCore/commit/d359350e7fa9cad5dbacee1d0207926291a2d4f7)) - yiyun

---
## [PluginCore-v0.8.3](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.8.2..PluginCore-v0.8.3) - 2021-08-25

### Bug Fixes

- **(plugincore)** pluginManager.cs: SkipDlls: 跳过2: 打包进入1个dll 或 打包进 1个exe - ([c0aa1f5](https://github.com/yiyungent/PluginCore/commit/c0aa1f595e33061f33a1bd6fe89d63dbeadbd0f5)) - yiyun

### Miscellaneous Chores

- **(plugincore.csproj)** 0.8.3 - ([0904cdc](https://github.com/yiyungent/PluginCore/commit/0904cdc2267bae2671a9e2bd3b08831a4c2eb4b4)) - yiyun

---
## [PluginCore-v0.8.2](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.8.1..PluginCore-v0.8.2) - 2021-08-23

### Miscellaneous Chores

- **(plugincore.csproj)** 0.8.2 - ([11f7757](https://github.com/yiyungent/PluginCore/commit/11f7757747f8990916d60224a8824d54da08d6ef)) - yiyun

---
## [PluginCore-v0.8.1](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.8.0..PluginCore-v0.8.1) - 2021-08-23

### Bug Fixes

- **(plugincorestartupextensions.cs,logutil.cs)** utils.LogUtil.PluginBehavior, apply: IStartupPlugin - ([00bfa94](https://github.com/yiyungent/PluginCore/commit/00bfa94a4de5de34bda970ed4524718f250d0fdf)) - yiyun
- userController.cs: avatar url error; upgrade: frontend - ([c842cb7](https://github.com/yiyungent/PluginCore/commit/c842cb7cce2967c37b80a891689ebad610ae2d62)) - yiyun

### Features

- **(pluginfinder.cs)** activatedPlugins - ([9076b29](https://github.com/yiyungent/PluginCore/commit/9076b290bfe365b3e111d2280349f62c37aa5402)) - yiyun

### Miscellaneous Chores

- **(plugincore.csproj)** 0.8.1 - ([9f37bbf](https://github.com/yiyungent/PluginCore/commit/9f37bbf7fd8fffcb626d7bb5bb419dc551e91dcc)) - yiyun

---
## [PluginCore-v0.8.0](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.7.0..PluginCore-v0.8.0) - 2021-08-21

### Features

- **(plugincore)** utils.LogUtil.Info - ([4163a2a](https://github.com/yiyungent/PluginCore/commit/4163a2ac7938ac6fc0741bdf7c1b967d72b55ed0)) - yiyun
- **(testtimejobplugin,plugincore.iplugins,plugincore)** timeJobPlugin 相关 - ([55d4f4c](https://github.com/yiyungent/PluginCore/commit/55d4f4ca7ddd9738216b9434ad1c30ef75f06471)) - yiyun

### Build

- **(plugincore.csproj)** pluginCore: 0.8.0; PluginCore.IPlugins: 0.6.0 - ([7213c6d](https://github.com/yiyungent/PluginCore/commit/7213c6d716b8d2191eb102aed268cc3c788a8721)) - yiyun

---
## [PluginCore-v0.7.0](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.6.0..PluginCore-v0.7.0) - 2021-08-21

### Features

- **(plugins,plugincore.iplugins,plugincore)** add: order, add: PluginApplicationBuilderManager - ([5e4a5f4](https://github.com/yiyungent/PluginCore/commit/5e4a5f46a4eb3aaca5d978fc1e695d0849e11e5c)) - yiyun
- **(pluginserviceprovide.cs)** add - ([0eb5a28](https://github.com/yiyungent/PluginCore/commit/0eb5a284f89cdca374660623a937178cfec6ebf1)) - yiyun

### Build

- **(plugincore.csproj)** 0.7.0, PluginCore.IPlugins: 0.5.0 - ([2992023](https://github.com/yiyungent/PluginCore/commit/2992023e688016d23f99ccf57e3086ff9c9af338)) - yiyun

---
## [PluginCore-v0.6.0](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.5.1..PluginCore-v0.6.0) - 2021-08-20

### Features

- **(plugincore,plugincore.iplugins,helloworldplugin)** iStartupXPlugin: 运行时 Configure(app) - ([0d18a6f](https://github.com/yiyungent/PluginCore/commit/0d18a6f9949faa1e92f1d20da35689e8e153bac1)) - yiyun

### Build

- **(plugincore.csproj)** 0.6.0 - ([98a4c70](https://github.com/yiyungent/PluginCore/commit/98a4c708234b2379cbcd00868b11e209c275baa0)) - yiyun

---
## [PluginCore-v0.5.1](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.4.0..PluginCore-v0.5.1) - 2021-08-19

### Bug Fixes

- **(src/plugincore/controllers/pluginscontroller.cs)** 启用插件: 启用失败时 回滚 - ([49d46c7](https://github.com/yiyungent/PluginCore/commit/49d46c79c3601df3b4899aee38296500c942854b)) - yiyun
- **(src/plugincore/pluginmanager.cs)** 当插件引用dll时, 插件Controller立即使用引用dll时，报错:找不到引用dll - ([d8c79c5](https://github.com/yiyungent/PluginCore/commit/d8c79c5681dc0a2e4a3d36565075c243b6ce44c7)) - yiyun

### Features

- **(plugincore)** pluginContentFilterMiddleware, IContentFilterPlugin - ([2597e9c](https://github.com/yiyungent/PluginCore/commit/2597e9c054bde134f9f250071347990be59e8d37)) - yiyun
- **(plugincore,/plugincore.iplugins)** pluginHttpEndFilter - ([c0cd458](https://github.com/yiyungent/PluginCore/commit/c0cd4581df72cdb9f4f678a531e7f04980c9695d)) - yiyun
- localEmbedded: PluginCoreAdmin -> package.json - ([f8be0d2](https://github.com/yiyungent/PluginCore/commit/f8be0d2ce86b26d9f00cf67845daed2853f285f6)) - yiyun
- 生成注释xml: PluginCore.IPlugins,PluginCore - ([5878148](https://github.com/yiyungent/PluginCore/commit/5878148244344f412e75fe9446824dd99ca2de47)) - yiyun
- update: IStartupPlugin success, fix: Plugin.Enable - ([ad950b2](https://github.com/yiyungent/PluginCore/commit/ad950b2802f60da3f950fd3eaf7bf1eee24c84b6)) - yiyun

### Miscellaneous Chores

- **(plugincore.csproj)** 0.4.0 -> 0.5.0 - ([658dde4](https://github.com/yiyungent/PluginCore/commit/658dde4156316f340fe4a28df5a8d76c895de872)) - yiyun
- **(src/plugincore/plugincore.csproj)** 0.5.1 - ([f337786](https://github.com/yiyungent/PluginCore/commit/f337786fa11addfbaed948085a946fb1205c4e8e)) - yiyun

### Build

- **(plugincore.csproj)** pluginCore.IPlugins: 0.3.0 - ([f29b998](https://github.com/yiyungent/PluginCore/commit/f29b998c926a09fddbfe6c47208b624a87b94b0e)) - yiyun

---
## [PluginCore-v0.4.0](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.3.1..PluginCore-v0.4.0) - 2021-08-16

### Features

- **(plugincore.csproj)** 0.4.0 - ([2ef0a92](https://github.com/yiyungent/PluginCore/commit/2ef0a924d5ca4c21245add4d3c05c13909100551)) - yiyun
- 支持 nupkg 格式插件 - ([1aa1d5f](https://github.com/yiyungent/PluginCore/commit/1aa1d5f45208fe273637548cd69f96a770c32c28)) - yiyun
- 支持嵌入式 前端 (打包进dll) - ([7e08cb3](https://github.com/yiyungent/PluginCore/commit/7e08cb33868890227f11645c2b8d4dd022318c94)) - yiyun

---
## [PluginCore-v0.3.1](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.3.0..PluginCore-v0.3.1) - 2021-08-10

### Bug Fixes

- authentication: 401 - ([01c5d04](https://github.com/yiyungent/PluginCore/commit/01c5d04e0a11e53c402076a25c1780fc728ccbc3)) - yiyun

### Features

- **(plugincore.csproj)** 0.3.1 - ([001ef67](https://github.com/yiyungent/PluginCore/commit/001ef674190418354bf964d7bf042d66717a0828)) - yiyun
- **(plugincoreconfig.cs)** @0.1.3/dist-cdn - ([da3fb7d](https://github.com/yiyungent/PluginCore/commit/da3fb7d5986582c1fde3d27e31fd8b135cd881c4)) - yiyun
- logUtil and apply - ([f0ee2e8](https://github.com/yiyungent/PluginCore/commit/f0ee2e8df4ec3ec17048a0f718340b6e0adb7360)) - yiyun

---
## [PluginCore-v0.3.0](https://github.com/yiyungent/PluginCore/compare/PluginCore-v0.2.0..PluginCore-v0.3.0) - 2021-08-10

### Features

- **(plugincore.csproj)** 0.3.0 - ([627ef86](https://github.com/yiyungent/PluginCore/commit/627ef866f464edddb1f836fde83e4cac04d6f4a3)) - yiyun
- plugin 支持加载插件 wwwroot 文件夹下的 html前端等 - ([273f9a4](https://github.com/yiyungent/PluginCore/commit/273f9a44c8727675f60d364fcf59a373958b3575)) - yiyun

---
## [PluginCore-v0.2.0](https://github.com/yiyungent/PluginCore/compare/PluginCore.IPlugins-v0.1.0..PluginCore-v0.2.0) - 2021-08-09

### Bug Fixes

- pluginCore Admin: avatar url 404: dist-cdn - ([38ca90c](https://github.com/yiyungent/PluginCore/commit/38ca90c5ff8b5138e1751887b9f60a376158eaad)) - yiyun
- fronted -> frontend - ([c41cfdb](https://github.com/yiyungent/PluginCore/commit/c41cfdbfba02fcfe37981d9aa4c8c05b194363de)) - yiyun

### Features

- **(plugincore.csproj)** 0.2.0 - ([4a10e5d](https://github.com/yiyungent/PluginCore/commit/4a10e5d5d5a45a3a763569bb6ef2c46d04a373fe)) - yiyun
- **(plugincorehostingstartup)** failure - ([0803d76](https://github.com/yiyungent/PluginCore/commit/0803d7679313ffa0e9c583d0923bff3412f265d5)) - yiyun
- remoteFronted, remove PluginCoreAdmin - ([81f6982](https://github.com/yiyungent/PluginCore/commit/81f698213cee6383da9d8035165d3881b88bc709)) - yiyun
- 保证 PluginCoreAdmin 文件夹存在 - ([2bf2c0e](https://github.com/yiyungent/PluginCore/commit/2bf2c0e42cbafb03af00ff324f1fb637238de441)) - yiyun

---
## [PluginCore.IPlugins-v0.1.0] - 2021-08-08

### Bug Fixes

- api url error, config file with init etc - ([9adb655](https://github.com/yiyungent/PluginCore/commit/9adb6551650b8ede28bec086df13023a2b7d9bf6)) - yiyun

### Features

- **(authorization)** authorization, Login - ([5b6f9fa](https://github.com/yiyungent/PluginCore/commit/5b6f9fa989d9739c30ef0d1f0186b876cddc5890)) - yiyun
- **(plugincore/plugincoreadmin)** add - ([802ad74](https://github.com/yiyungent/PluginCore/commit/802ad74efd013f34e9c5f7d5ed3eef8574f2c20b)) - yiyun
- **(pluginframeworkstartupextensions.cs)** useStaticFiles: PluginCoreAdmin - ([b0adb8e](https://github.com/yiyungent/PluginCore/commit/b0adb8e1f31912472946c8c76fe05b5ff85a77b4)) - yiyun
- **(pluginframeworkstartupextensions.cs)** pluginFramework -> PluginCore, app.UseDefaultFiles(); - ([17e1587](https://github.com/yiyungent/PluginCore/commit/17e15879076d36d3d2cf6891181cf823fb78c66d)) - yiyun
- add controllers, examples - ([c7e8553](https://github.com/yiyungent/PluginCore/commit/c7e8553b9bbb6d45eac251e1060acb719fd3dac9)) - yiyun
- 自动初始化插件目录 - ([fe3d162](https://github.com/yiyungent/PluginCore/commit/fe3d162e3a5455d308db55f9b260301f10ff4eee)) - yiyun
- pluginCore.IPlugins, plugins: HelloWorldPlugin - ([1e81de2](https://github.com/yiyungent/PluginCore/commit/1e81de2107394f527a94ec5d4c2ae6853d2d5526)) - yiyun
- pluginCoreConfig, PluginCoreConfigFactory - ([6a0dae2](https://github.com/yiyungent/PluginCore/commit/6a0dae2d222d9f0464b8d158eb87f674529af56e)) - yiyun
- pluginCore, plugins/HelloWorldPlugin - ([5141afd](https://github.com/yiyungent/PluginCore/commit/5141afded8feba94af581d6132fccb87aafa516c)) - yiyun
- logout, Login: pretty - ([5fac6a3](https://github.com/yiyungent/PluginCore/commit/5fac6a3939436d58d860e2529be08a26c7a79946)) - yiyun
- nuget config, v0.1.0 - ([fffc419](https://github.com/yiyungent/PluginCore/commit/fffc419480481b632340eb4e42a0b608c5fff144)) - yiyun

<!-- generated by git-cliff -->
