<p align="center">
<!-- <img src="docs/_images/logo.png" alt="plugincore-js-sdk"> -->
</p>
<h1 align="center">plugincore-js-sdk</h1>

> :cake: PluginCore JavaScript SDK

[![repo size](https://img.shields.io/github/repo-size/yiyungent/plugincore-js-sdk.svg?style=flat)]()
[![LICENSE](https://img.shields.io/github/license/yiyungent/plugincore-js-sdk.svg?style=flat)](https://github.com/yiyungent/plugincore-js-sdk/blob/master/LICENSE)
[![NPM version](https://img.shields.io/npm/v/@yiyungent/plugincore.svg)](https://www.npmjs.com/package/@yiyungent/plugincore)
[![NPM downloads](https://img.shields.io/npm/dt/@yiyungent/plugincore)](https://www.npmjs.com/package/@yiyungent/plugincore)
[![jsDelivr](https://img.shields.io/jsdelivr/npm/hy/@yiyungent/plugincore)](https://www.jsdelivr.com/package/npm/@yiyungent/plugincore)


## Introduce

PluginCore JavaScript SDK

 + **简单** - 简单易用.
 + **免费** - MIT协议 发布

## Screenshots

![](screenshots/2022-02-09-15-17-47.png)

## Install

#### CDN
```html
<script src="https://cdn.jsdelivr.net/npm/plugincore-js-sdk/dist/PluginCore.min.js"></script>
```

#### npm
```bash
npm install @yiyungent/plugincore --save
```

## Usage

## 首先需要先在 扩展的前端页面 埋点 (扩展点 / Plugin Widget)

> 例如下方: 第一个扩展点    
> 在 `body` 最后 插入扩展点, 并且 `widgetKey`: `PluginCore.Admin.Footer`, 传递额外参数: `a,b,c`      
> 启用 `HelloWorldPlugin` 插件后, 即会插入相应挂件  

```html
<!DOCTYPE html>
<html>
    <head></head>
    <body>
        <div id="app">
            <h2 style="margin: 0 auto;text-align: center;">plugincore-js-sdk</h2>
            <div style="margin: 0 auto;text-align: center;">尝试使用插件 注入/修改 页面</div>
        </div>
        <!-- PluginCore.IPlugins.IWidgetPlugin.Widget(PluginCore.Admin.Footer,a,b,c) -->
        <!-- PluginCore.IPlugins.IWidgetPlugin.Widget(PluginCore.Admin.Footer,d,e) -->
        <!-- PluginCore.IPlugins.IWidgetPlugin.Widget(PluginCore.Admin.Footer,f,j) -->
    </body>
</html>
```

### 使用模块管理器
```js
import PluginCore from '@yiyungent/plugincore';

const p = new PluginCore();
```
### 在页面中使用
```html
    <body>
        <div id="app">
            <h2 style="margin: 0 auto;text-align: center;">plugincore-js-sdk</h2>
            <div style="margin: 0 auto;text-align: center;">尝试使用插件 注入/修改 页面</div>
        </div>
        <script src="https://cdn.jsdelivr.net/npm/plugincore-js-sdk/dist/PluginCore.min.js"></script>
        <script>
			var p = new PluginCore({
                baseUrl: "your PluginCore url"
            });
			
            p.start();
        </script>
    </body>
```

## Related Project

- [yiyungent/PluginCore: ASP.NET Core lightweight plugin framework. ASP.NET Core 轻量级 插件框架 - 一分钟集成](https://github.com/yiyungent/PluginCore)
- [yiyungent/plugincore-admin-frontend: PluginCore 的 Admin 前端 ( Vue.js )](https://github.com/yiyungent/plugincore-admin-frontend)
 
## 鸣谢



## Donate

plugincore-js-sdk is an MIT licensed open source project and completely free to use. However, the amount of effort needed to maintain and develop new features for the project is not sustainable without proper financial backing.

We accept donations through these channels:
- <a href="https://afdian.net/@yiyun" target="_blank">爱发电</a>

## Author

**plugincore-js-sdk** © [yiyun](https://github.com/yiyungent), Released under the [MIT](./LICENSE) License.<br>
Authored and maintained by yiyun with help from contributors ([list](https://github.com/yiyungent/plugincore-js-sdk/contributors)).

> GitHub [@yiyungent](https://github.com/yiyungent)

