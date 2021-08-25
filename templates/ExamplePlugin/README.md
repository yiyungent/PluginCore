

# NuGet 打包 nupkg

```shell
nuget pack ExamplePlugin.nuspec
```

# 上传 nupkg 到 nuget 后

# 安装 插件模板

```shell
dotnet new -i PluginCore.Template
```

> 这将从 nuget 上下载安装 `PluginCore.Template`


> 补充:    
> 也可以直接指定 本地 nupkg
> 
> ```shell
> dotnet new -i ExamplePlugin.0.1.0.nupkg
> ```
> 
> 卸载模板
> 
> ```shell
> dotnet new -u PluginCore.Template
> ```

# 安装完成后, 使用此模板创建项目

```shell
dotnet new plugincore -n MyFirstPlugin
```

> 通过 `-n` 指定插件项目名

> `plugincore` 为 模板 `shortname`
