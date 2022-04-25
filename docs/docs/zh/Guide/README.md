# 指南


## 一分钟集成

推荐使用 [NuGet](https://www.nuget.org/packages/PluginCore), 在你项目的根目录 执行下方的命令, 如果你使用 Visual Studio, 这时依次点击 **Tools** -> **NuGet Package Manager** -> **Package Manager Console** , 确保 "Default project" 是你想要安装的项目, 输入下方的命令进行安装.

### 在你的 ASP.NET Core 项目中集成

```bash
PM> Install-Package PluginCore.AspNetCore
```

> 在你的 ASP.NET Core 应用程序中修改代码
>
> Startup.cs

```C#
using PluginCore.AspNetCore.Extensions;

// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    // 1. 添加 PluginCore
    services.AddPluginCore();
}

// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();

    app.UseRouting();

    // 2. 使用 PluginCore
    app.UsePluginCore();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```

> 现在访问 https://localhost:5001/PluginCore/Admin 即可进入 PluginCore Admin  
> https://localhost:5001 需改为你的地址

### 注意

请登录 `PluginCore Admin` 后，为了安全，及时修改默认用户名，密码:

`App_Data/PluginCore.Config.json`     

```json
{
	"Admin": {
		"UserName": "admin",
		"Password": "ABC12345"
	},
	"FrontendMode": "LocalEmbedded",
	"RemoteFrontend": "https://cdn.jsdelivr.net/gh/yiyungent/plugincore-admin-frontend@0.1.2/dist-cdn"
}
```

修改后，立即生效，无需重启站点，需重新登录 `PluginCore Admin`


## Docker 体验

如果你需要在本地体验 PluginCore, 那么这里有一个 [例子(/examples)](https://github.com/yiyungent/PluginCore/tree/main/examples)

```bash
docker run -d -p 5004:80 -e ASPNETCORE_URLS="http://*:80" --name plugincore-aspnetcore3-1 yiyungent/plugincore-aspnetcore3-1
```

现在你可以访问 http://localhost:5004/PluginCore/Admin

> 补充:     
> 若使用 `Docker Compose`, 可参考仓库根目录下的 `docker-compose.yml`     

> 补充:   
> 使用 `ghcr.io`     
> 
> ```bash
> docker run -d -p 5004:80 -e ASPNETCORE_URLS="http://*:80" --name plugincore-aspnetcore3-1 ghcr.io/yiyungent/plugincore-aspnetcore3-1
> ```

## 使用

- [详细文档(/docs)](https://moeci.com/PluginCore "在线文档") 文档构建中
- [见示例(/examples)](https://github.com/yiyungent/PluginCore/tree/main/examples)


### 添加插件钩子, 并应用

> 1.例如，自定义插件钩子: `ITestPlugin`

```C#
using PluginCore.IPlugins;

namespace PluginCore.IPlugins
{
    public interface ITestPlugin : IPlugin
    {
        string Say();
    }
}
```

> 2.在需要激活的地方，应用钩子，这样所有启用的插件中，实现了 `ITestPlugin` 的插件，都将调用 `Say()`

```C#
using PluginCore;
using PluginCore.IPlugins;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly PluginFinder _pluginFinder;

        public TestController(PluginFinder pluginFinder)
        {
            _pluginFinder = pluginFinder;
        }

        public ActionResult Get()
        {
            //var plugins = PluginFinder.EnablePlugins<BasePlugin>().ToList();
            // 所有实现了 ITestPlugin 的已启用插件
            var plugins2 = _pluginFinder.EnablePlugins<ITestPlugin>().ToList();

            foreach (var item in plugins2)
            {
                // 调用
                string words = item.Say();
                Console.WriteLine(words);
            }

            return Ok("");
        }
    }
}
```

### 自定义前端

PluginCore 支持3种前端文件加载方式

> 配置文件 `App_Data/PluginCore.Config.json` 中 `FrontendMode`

1. LocalEmbedded
  - 默认, 嵌入式资源，前端文件打包进dll, 此模式下, 不容易自定义前端文件，需要修改 `PluginCore` 源代码，重新编译，不建议

2. LocalFolder
  - 在集成了 `PluginCore` 的 ASP.NET Core 项目中, 新建 `PluginCoreAdmin`, 将前端文件放入此文件夹

3. RemoteCDN
  - 使用远程cdn资源, 可通过 配置文件中 `RemoteFrontend` 指定url

> **注意:**    
> 更新 `FrontendMode`, 需重启站点后, 才能生效



### 补充

> **补充**
>
> 开发插件只需要, 添加对 `PluginCore.IPlugins` 包 (插件sdk) 的引用即可，        
>
> 当然如果你需要 `PluginCore` ,  也可以添加引用



> **规范**
>
> 1. 插件sdk
>
> 插件接口应当位于 `PluginCore.IPlugins` 命名空间，这是规范，不强求，但建议这么做，      
>
> 程序集名不一定要与命名空间名相同，你完全在你的插件sdk程序集中，使用 `PluginCore.IPlugins` 命名空间。
>
> 2. 插件
>
> 插件程序集名(一般=项目(Project)名) 与 插件 `info.json` 中 `PluginId` 一致, 例如: Project: `HelloWorldPlugin`, PluginId: `HelloWorldPlugin`,  此项必须，否则插件无法加载
> `PluginId` 为插件唯一标识


