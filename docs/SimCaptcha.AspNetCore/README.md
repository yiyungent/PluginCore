<!--
 * @Author: yiyun
 * @Description: 
-->
# SimCaptcha.AspNetCore

#### 在 ASP.NET Core 下使用

```bash
PM> Install-Package SimCaptcha.AspNetCore
```

## 快速开始

#### 在 ASP.NET Core 下 三步搭建验证服务端

```csharp
// Startup.cs 
// 注意: 省略了部分代码, 只保留主要部分, 详见示例(/examples/EasyAspNetCoreService)
// 仅适用于 SimCaptcha.AspNetCore v0.2.0+
public void ConfigureServices(IServiceCollection services)
{
    // 1.重要: 注册验证码配置
    services.Configure<SimCaptchaOptions>(Configuration.GetSection(SimCaptchaOptions.SimCaptcha));

    // 2.添加 SimCaptcha
    services.AddSimCaptcha();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // 3.启用 SimCaptcha 中间件
    app.UseSimCaptcha();

    // 现在
    // "https://yourdomain.com/api/vCode/VCodeImg", "https://yourdomain.com/api/vCode/VCodeCheck", "https://yourdomain.com/api/vCode/TicketVerify"
    // 将开始工作
}
```