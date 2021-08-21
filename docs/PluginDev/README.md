<!--
 * @Author: yiyun
 * @Description: 
-->
# 插件开发

#### 1. 引入插件框架dll（PluginCore 必须，其它根据需要）

> 一个示例插件项目结构 如下图

<!-- ![](/images/plugin-structure.png) -->
<img :src="$withBase('/images/plugin-structure.png')">

#### 2. 添加 `GetUserInfoPlugin` 类 继承 `BasePlugin`

> 或则你可以直接实现 `IPlugin`

> 可通过预先定义框架行为钩子，插件再实现接口，将插件行为加入框架，如实现  `ITestPlugin`

> 支持插件 `构造器注入` 框架预先注入的服务等

```c#
public class GetUserInfoPlugin : BasePlugin, ITestPlugin
{
    private readonly IUserInfoService _userInfoService;

    public GetUserInfoPlugin(IUserInfoService userInfoService)
    {
        this._userInfoService = userInfoService;
    }

    public string Say()
    {
        UserInfo userInfo = _userInfoService.FirstOrDefaultAsync(m => !m.IsDeleted).Result;
        string rtn = $"用户名: {userInfo.UserName}, 创建时间: {userInfo.CreateTime.ToString()}";

        return rtn;
    }

    public override (bool IsSuccess, string Message) AfterEnable()
    {
        Console.WriteLine($"{nameof(GetUserInfoPlugin)}: {nameof(AfterEnable)}");
        return base.AfterEnable();
    }

    public override (bool IsSuccess, string Message) BeforeDisable()
    {
        Console.WriteLine($"{nameof(GetUserInfoPlugin)}: {nameof(BeforeDisable)}");
        return base.BeforeDisable();
    }
}
```

#### 3. 插件其他配置

> 支持 动态扩展 WebAPI，和普通WebAPI 项目相同，直接创建 Controller 即可

```C#
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
        SettingsModel settingsModel = PluginSettingsModelFactory.Create<SettingsModel>("GetUserInfo");
        string rtn = $"用户名: {userInfo.UserName}, 创建时间: {userInfo.CreateTime.ToString()}, Hello: {settingsModel.Hello}";

        return Ok(rtn);
    }
}
```

> 插件设置（可选）, Json Model 类 继承 `PluginSettingsModel`

```C#
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
	"PluginId": "GetUserInfo",
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

> 右键选择插件项目，点击发布（Publish），再将发布后的插件文件夹打包为 `GetUserInfo.zip` 即可  
> 压缩包名可随意，框架将以 `info.json` 中 `PluginId` 作为插件标识

> 打包后的插件，即可通过 上传本地插件 载入框架

> GetUserInfo.csproj 参考

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netcoreapp3.1</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\Framework\Libraries\Domain\Domain.csproj" />
    <ProjectReference Include="..\..\src\Framework\Libraries\Framework\Framework.csproj" />
    <ProjectReference Include="..\..\src\Framework\Libraries\PluginCore\PluginCore.csproj" />
    <ProjectReference Include="..\..\src\Framework\Libraries\Services\Services.csproj" />
  </ItemGroup>

  <ItemGroup>
    <None Update="info.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
    <None Update="README.md">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
    <None Update="settings.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
  </ItemGroup>

</Project>
```