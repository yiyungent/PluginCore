


# PluginCore.AspNetCore-v1.2.0

## Updated

- `DebugController`: `PluginContexts:PluginId`
- `<PackageReference Include="PluginCore" Version="2.1.0" />`

# PluginCore-v2.1.0

## Fixed

- 修复解压时没有子目录导致解压失败的问题

## Updated

- `IPluginContext.PluginId`
- `LazyPluginLoadContext`, `PositivePluginLoadContext`

# PluginCore.AspNetCore-v1.1.0

## Updated

- `api/PluginCore/Admin/Debug`    
  - `PluginContexts`     
  - `AssemblyLoadContexts`    
  - `Assemblies`     
  - `Services`   

# PluginCore.AspNetCore-v1.0.4

## Updated

- 使用 `PluginCore-v2.0.2`



# PluginCore-v2.0.2

## Fixed

- B 插件依赖 A 插件时, B 插件无法启用
  - 1.null异常: B 插件的 LoadContext 没有搜索到 A 插件的 assemblyName 
  - 2.经过测试启用, 不同版本 dll 依然可以在不同插件中共存 (由于1插件一个LoadContext, 区分采用 `AssemblyName.FullName`)



# PluginCore.AspNetCore-v1.0.3

## Fixed

- 内存溢出
  - 后台定时任务频率太高 (1s), GC 没有及时回收, 内存++ -> 每次任务完成 `GC.Collect()` 


# PluginCore.AspNetCore-v1.0.2


## Updated

- 后端: 移除: 插件上传大小限制       
- 前端: 使用 `plugincore-admin-frontend-v0.3.2`
  - 移除: 插件上传大小限制



# PluginCore.AspNetCore-v1.0.1

## Updated

- 更新到 `PluginCore-v2.0.1`


# PluginCore-v2.0.1

## Fixed

- 修复 插件dll 被锁定 导致的删除失败



# PluginCore.AspNetCore-v1.0.0

## Refactor

- 默认 使用 最新 微软推荐的 插件加载方案 `PluginLoadContext`
- 大量接口变更, 类名变更



# PluginCore-v2.0.0

## Refactor

- 默认 使用 最新 微软推荐的 插件加载方案 `PluginLoadContext`
- 大量接口变更, 类名变更


# PluginCore.AspNetCore-v0.0.5

## Fixed

- 修复由于 `GitHub Action` 缺少 `npm install` 引起的 没有将前端文件打包进入 dll



# PluginCore.AspNetCore-v0.0.4

## Fixed

- 修复由于 `GitHub Action` 引起 没有安装 `Node.js` 导致没有成功 `npm install` 最终导致没有将前端文件成功打包进入 dll



# PluginCore.AspNetCore-v0.0.3

## Fixed

- `services.TryAddTransient<IPluginFinder, PluginFinder>();`


# PluginCore.AspNetCore-v0.0.2

- 仅 `ASP.NET Core` 相关

> PS: 注意: 之前的 `PluginCore.AspNetCore-0.0.1` 无用, 只是为了占用住 在 NuGet 的 PackageId

# PluginCore.IPlugins.AspNetCore-v0.0.1


- 仅 `ASP.NET Core` 相关

# PluginCore-v1.0.0

## Refactor

- 重大更新: 重构
  - 在 `ASP.NET Core` 中的内容移动到 `PluginCore.AspNetCore`
  - 接口更新

## Added

- 开放更多接口, 可自由替换内部实现
  - `services.TryAddTransient<IPluginControllerManager, PluginControllerManager>();`
  - `services.TryAddTransient<IPluginApplicationBuilderManager, PluginApplicationBuilderManager>();`
  - `services.TryAddTransient<IPluginManager, AspNetCorePluginManager>();`
  - `services.AddTransient<IPluginFinder, PluginFinder>();`
    - **注意:** 此项忘记 用 `TryAdd` 了, 在 `AddPluginCore()` 前 添加会导致无法替换内部, 下个版本修复

# PluginCore.IPlugins-v0.8.0

## Refactor

- 重大更新: 重构
  - 在 `ASP.NET Core` 中的内容移动到 `PluginCore.IPlugins.AspNetCore`



---



# PluginCore-v0.9.3

## Fixed

- 更新 PluginCore Admin 前端: `plugincore-admin-frontend-v0.3.1`
  - Fixed: 用户名验证错误


# PluginCore-v0.9.2

## Fixed

- `tokenCookieName = "PluginCore.Admin.Token"` 与 `PluginCore Admin` 前端一致, 而不是后端检索 `tokenCookieName = "token"`
  - 插件可在 `Controller,Action` 上使用 `[Authorize("PluginCoreAdmin")]`, 来达到与 `PluginCore Admin` 相同的权限策略



# PluginCore-v0.9.1

## Fixed

- `ITimeJobPlugin` 多线程定时任务 执行问题
  - 当上一个任务未完成, 下个任务就开始时导致, 修复: 加锁, 下个任务线程阻塞等待

# PluginCore-v0.9.0

## Added

- 挂件 (Plugin Widget) 相关


# PluginCore.IPlugins-v0.7.0

## Added

- `PluginCore.IPlugins.IWidgetPlugin.Widget` 前端挂件接口



# PluginCore-v0.8.6

## Fixed

- 若 `PluginCore` + `Swashbuckle.AspNetCore` 配合使用, 导致 `SwaggerGeneratorException: Ambiguous HTTP method for action`


# PluginCore-v0.8.5

## Updated

- Reference: plugincore-admin-frontend v0.3.0

# PluginCore-v0.8.4

## Updated

- PackageReference: PluginCore.IPlugins: 0.6.1

# PluginCore.IPlugins-v0.6.1

## Updated

- remove: `Newtonsoft.Json`
  - 设置的json格式化 使用 `System.Text.json`


# PluginCore-v0.8.3

## Fixed

- 当 `主程序 打包进入 1个dll 或 1个exe` 可能导致 插件重复引入 主程序已引入Assembly

# PluginCore-v0.8.2

## Fixed

- nuget 包中 `PluginCore.dll` 缺失 `Resources` 导致 #7 


# PluginCore-v0.8.1

## Fixed

- 用户头像 在某些时候url错误: 改为完全由前端提供

- RemoteFrontend 已更新


# PluginCore-v0.8.0

## Added

- `ITimeJobPlugin` 定时任务 激活

## Updated

- 命名空间 整理




# PluginCore.IPlugins-v0.6.0

## Added

- `ITimeJobPlugin` 定时任务

## Updated

- 命名空间 整理



# PluginCore.IPlugins-v0.5.0

## Added

- `IPluginFinder` 添加，并注入服务
- `ConfigureServicesOrder`, `ConfigureOrder` 添加，`ConfigureOrder` 应用

# PluginCore-v0.7.0

## Added

- `IPluginFinder` 注入服务

## Updated

-  `PluginApplicationBuilderManager`
   - 性能提升: 不再是每次在 middleware.invoke 时 build插件的middleware, 而是启用禁用时rebuild



# PluginCore.IPlugins-v0.4.0


## Added

- `IStartupXPlugin` 支持在运行时添加 `请求管道Middleware` , 热插拔 ( 实验阶段 )
   - 不同于 `IStartupPlugin` 必须启用后，重启站点


# PluginCore-v0.6.0

## Added

- 激活 `IStartupXPlugin`  的 `Configure(IApplicationBuilder app)`
   - 支持在运行时添加 `请求管道Middleware` , 热插拔 ( 实验阶段 )
   - 不同于 `IStartupPlugin` 必须启用后，重启站点
   - 暂不支持 `ConfigureServices(IServiceCollection services)`

# PluginCore-v0.5.1

## Fixed

- 当插件引用dll时, 插件Controller立即使用引用dll时，报错:找不到引用dll
  - 改变加载dll顺序: 先加载插件引用的dll, 再加载插件主dll

- 插件启用: 内部顺序 引起的插件未成功启用，但配置文件却已改变

## Updated

- IStartupPlugin 激活，进入实验阶段
  - 实现 `IStartupPlugin` 的插件 安装后，需先启用，再重启站点，即可激活 `IStartupPlugin`, 此类插件无法热插拔



# PluginCore.IPlugins-v0.3.0

## Added

- 插件sdk
  - 新的api


# PluginCore-v0.5.0

## Added

- 插件sdk
  - 新的api

## Updated

- 前端内嵌式资源 改为使用 npm, 不再使用 `PluginCoreAdmin`, 只有 `LocalFolder` 模式才使用此文件夹



# PluginCore-v0.4.0

## Added

- 支持 嵌入式资源 方式引入前端

## Updated

- PluginCore.Config.json 部分配置属性 改变
   - PluginCore.Config.json 前端配置 改为 前端模式 ( `FrontendMode` )




# PluginCore.IPlugins-v0.2.0

## Added

- plugin 支持加载插件 wwwroot 文件夹下的 html前端等




# PluginCore-v0.3.1

## Fixed

- System.InvalidOperationException: No authenticationScheme was specified, and there was no DefaultChallengeScheme found. The default schemes can be set using either AddAuthentication(string defaultScheme) or AddAuthentication(Action<AuthenticationOptions> configureOptions).
  - Fixed #4 


# PluginCore-v0.3.0

## Added

- plugin 支持加载插件 wwwroot 文件夹下的 html前端等



# PluginCore-v0.2.0


## Added

- 支持加载远程前端, 使用 jsdelivr
   - https://cdn.jsdelivr.net/gh/yiyungent/plugincore-admin-fronted@0.1.2/dist-cdn/



# PluginCore.IPlugins-v0.1.0

## 基本的 Plugin 接口

- IPlugin
- BasePlugin
- 基本 辅助类


# PluginCore-v0.1.0

## Added

- 加载本地 前端


<!-- Matomo Image Tracker-->
<img referrerpolicy="no-referrer-when-downgrade" src="https://matomo.moeci.com/matomo.php?idsite=2&amp;rec=1&amp;action_name=GitHub.PluginCore.Releases.md" style="border:0" alt="" />
<!-- End Matomo -->
