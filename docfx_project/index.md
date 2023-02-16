# This is the **HOMEPAGE**.
Refer to [Markdown](http://daringfireball.net/projects/markdown/) for how to write markdown files.
## Quick Start Notes:
1. Add images to the *images* folder if the file is referencing an image.


## API Docs Dev

- [.NET API Docs | docfx](https://dotnet.github.io/docfx/docs/dotnet-api-docs.html)

Docfx generates .NET API docs in 2 stages:

1.  The *metadata* stage uses the `metadata` config to produce [.NET API YAML files](https://dotnet.github.io/docfx/docs/dotnet-yaml-format.html) at the `metadata.dest` directory.

2.  The *build* stage transforms the generated .NET API YAML files specified in `build.content` config into HTML files.

These 2 stages can run independently with the `docfx metadata` command and the `docfx build` command. The `docfx` root command runs both `metadata` and `build`.


```bash
dotnet tool update -g docfx

dotnet build src/PluginCore.IPlugins/PluginCore.IPlugins.csproj --configuration Release
dotnet build src/PluginCore/PluginCore.csproj --configuration Release
dotnet build src/PluginCore.IPlugins.AspNetCore/PluginCore.IPlugins.AspNetCore.csproj --configuration Release
dotnet build src/PluginCore.AspNetCore/PluginCore.AspNetCore.csproj --configuration Release

cd docfx_project

docfx metadata

docfx build

docfx docfx.json --serve

# To preview your local changes, save changes then run this command in a new terminal to rebuild the website:
docfx docfx.json
```

