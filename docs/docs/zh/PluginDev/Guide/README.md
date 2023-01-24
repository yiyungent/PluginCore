<!--
 * @Author: yiyun
 * @Description: 
-->

> 插件开发 - 快速上手

> - 2022年4月25日 更新   
> 已适配最新 `PluginCore.AspNetCore` 插件加载机制
>    
> **注意**: 很多时候插件没有加载成功 / 出现异常, 都是由于没有正确的打包   


# 快速上手

## 方式1 (推荐): 使用 `dotnet cli` 快速安装插件模板, 并创建插件项目

> 安装插件模板

```shell
dotnet new --install PluginCore.Template
```

> 使用此插件模板, 创建插件项目

```shell
dotnet new plugincore -n MyFirstPlugin
```

> 通过 `-n` 指定插件项目名, 这里我的是 `MyFirstPlugin`

> `plugincore` 为 插件模板 `shortname`


## 方式2: 手动创建插件项目

#### 1. 引入插件SDK

[NuGet](https://www.nuget.org/packages/PluginCore.IPlugins.AspNetCore/)

> 方式1: NuGet Package Manager

```shell
Install-Package PluginCore.IPlugins.AspNetCore
```

> 方式2: .NET CLI

```shell
dotnet add package PluginCore.IPlugins.AspNetCore
```

> 方式3: PackageReference ( 在你的 `HelloWorldPlugin.csproj` `<ItemGroup>` 中 添加 )

```xml
<PackageReference Include="PluginCore.IPlugins.AspNetCore" Version="0.0.1">
  <ExcludeAssets>runtime</ExcludeAssets>
</PackageReference>
```


> 一个示例插件项目结构 如下图

<!-- ![](/images/plugin-structure.png) -->
<img :src="$withBase('/images/plugin-project-structure.png')">

#### 2. 添加 `HelloWorldPlugin` 类 继承 `BasePlugin`

> 或则你可以直接实现 `IPlugin`

> 可通过预先定义框架行为钩子，插件再实现接口，将插件行为加入框架，如实现  `IStartupXPlugin`

> 支持插件 `构造器注入` 框架预先注入的服务等

```csharp
public class HelloWorldPlugin : BasePlugin, IStartupXPlugin
{
    public override (bool IsSuccess, string Message) AfterEnable()
    {
        Console.WriteLine($"{nameof(HelloWorldPlugin)}: {nameof(AfterEnable)}");
        return base.AfterEnable();
    }

    public override (bool IsSuccess, string Message) BeforeDisable()
    {
        Console.WriteLine($"{nameof(HelloWorldPlugin)}: {nameof(BeforeDisable)}");
        return base.BeforeDisable();
    }

    public void ConfigureServices(IServiceCollection services)
    {

    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseMiddleware<SayHelloMiddleware>();
    }

    public int ConfigureOrder
    {
        get
        {
            return 2;
        }
    }


    public int ConfigureServicesOrder
    {
        get
        {
            return 2;
        }
    }
}
```

> `SayHelloMiddleware.cs`

```csharp
public class SayHelloMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// 在 <see cref="PluginApplicationBuilder"/> Build 时, 将会 new Middleware(), 最终将所有 Middleware 包装为一个 <see cref="RequestDelegate"/>
    /// </summary>
    /// <param name="next"></param>
    public SayHelloMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="pluginFinder">测试，是否运行时添加的Middleware，是否可以依赖注入</param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext, IPluginFinder pluginFinder)
    {
        // 测试: 成功
        List<IPlugin> plugins = pluginFinder.EnablePlugins()?.ToList();

        bool isMatch = false;

        isMatch = httpContext.Request.Path.Value.StartsWith("/SayHello");

        if (isMatch)
        {
            await httpContext.Response.WriteAsync($"Hello World! {DateTime.Now:yyyy-MM-dd HH:mm:ss} <br>" +
                                                  $"{httpContext.Request.Path} <br>" +
                                                  $"{httpContext.Request.QueryString.Value}", Encoding.UTF8);
        }
        else
        {
            // Call the next delegate/middleware in the pipeline
            await _next(httpContext);
        }
    }

}
```


#### 3. 插件其他配置

> 支持 动态扩展 WebAPI，和普通WebAPI 项目相同，直接创建 Controller 即可

> 注意: 这里的 `IUserInfoService` 是集成了 `PluginCore ` 的项目里的服务接口

```csharp
[Route("api/plugins/[controller]")]
[ApiController]
public class UserHelloController : ControllerBase
{
    private readonly IUserInfoService _userInfoService;

    public UserHelloController(IUserInfoService userInfoService)
    {
        this._userInfoService = userInfoService;
    }

    public ActionResult Get()
    {
        UserInfo userInfo = _userInfoService.FirstOrDefaultAsync(m => !m.IsDeleted).Result;
        SettingsModel settingsModel = PluginSettingsModelFactory.Create<SettingsModel>("HelloWorldPlugin");
        string rtn = $"用户名: {userInfo.UserName}, 创建时间: {userInfo.CreateTime.ToString()}, Hello: {settingsModel.Hello}";

        return Ok(rtn);
    }
}
```

> 插件设置（可选）, Json Model 类 继承 `PluginSettingsModel`

```csharp
public class SettingsModel : PluginSettingsModel
{
    public string Hello { get; set; }
}
```

文件名必须 `settings.json`

```json
{
	"Hello": "哈哈哈哈哈或或或或或或" 
}
```

> 插件描述 `info.json` （必需）

```json
{
	"PluginId": "HelloWorldPlugin",
	"DisplayName": "获取一个用户",
	"Description": "这是一个示例插件2号。",
	"Author": "yiyun",
	"Version": "0.1.0",
	"SupportedVersions": [ "0.0.1" ]
}
```

> 插件文档 `README.md`（可选）, 文件名必须 README.md

```markdown
## 说明文档（可选）

- [] 这是一个示例插件
- [x] 感谢使用
```

#### 3. 插件发布打包

> **注意:**
> > 此项尤其重要, 很多时候插件没有加载成功 / 出现异常, 都是由于没有正确的打包   
> 正确的打包:   
> 1. `<TargetFramework>net6.0</TargetFramework>` 其中 `net6.0` 替换为你的 `PluginCore.AspNetCore` 所在主程序的 框架版本
> 2. 使用 `dotnet build` / `Visual Studio` 编译后, 注意查看生成了哪些dll，是否有依赖遗漏
> 3. 哪些 dll 使用主程序提供的？这些 dll 就不要打包进 插件, 例如 `Microsoft.AspNetCore.Mvc` 等 就不要打包进插件



> 右键选择插件项目，点击 Build（Build），再将发布后的插件文件夹打包为 `xxx.zip` 即可     
> 注意: 进入有 `HelloWorldPlugin.dll` 的文件夹内，再打包, (压缩包直接点击开, 就可以看到 `HelloWorldPlugin.dll`)    

> 压缩包名可随意，框架将以 `info.json` 中 `PluginId` 作为插件标识    
> 注意: `PluginId` 一定要与程序集名相同, 例如 `PluginId` 为 `HelloWorldPlugin`, 那么最后打包里一定有 `HelloWorldPlugin.dll`

> 打包后的插件，即可通过 上传本地插件 载入框架

> `HelloWorldPlugin.csproj` 参考

- `<TargetFramework>net6.0</TargetFramework>` 最好是填写 `PluginCore.AspNetCore` 所在主程序的框架版本, 而不是 `netstandard2.0`, 如果非要使用, 则使用 `dotnet publish` 来发布插件，这样才能生成依赖的dll到输出目录, 然后输出目录中删除主程序已有的 dll, 即这些 dll 由主程序提供, 例如 `PluginCore.IPlugins`, `PluginCore.IPlugins.AspNetCore`

- `<EnableDynamicLoading>true</EnableDynamicLoading>` 会在 `dotnet build` 时将所有依赖 dll 一起复制到输出目录

- `<ExcludeAssets>runtime</ExcludeAssets>` 排除此包, 在 `dotnet build` 时不复制此 dll 到输出目录, 即使用主程序提供的 dll

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <EnableDynamicLoading>true</EnableDynamicLoading>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.2.0">
			<ExcludeAssets>runtime</ExcludeAssets>
		</PackageReference>
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="PluginCore.IPlugins.AspNetCore" Version="0.0.1">
			<ExcludeAssets>runtime</ExcludeAssets>
		</PackageReference>
  </ItemGroup>

  <!-- 发布插件相关文件 -->
  <ItemGroup>
    <Content Include="info.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
    <Content Include="README.md">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
    <Content Include="settings.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

  <!-- 发布 wwwroot -->
  <ItemGroup>
    <Content Include="wwwroot\*">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
    <Content Include="wwwroot\*\*">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

</Project>
```


<!-- Matomo Image Tracker-->
<img referrerpolicy="no-referrer-when-downgrade" src="https://matomo.moeci.com/matomo.php?idsite=2&amp;rec=1&amp;action_name=GitHub.PluginCore.docs.zh.PluginDev.Guide.README.md" style="border:0" alt="" />
<!-- End Matomo -->

