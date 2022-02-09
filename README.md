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


## 介绍

PluginCore JavaScript SDK

 + **简单** - 简单易用.
 + **免费** - MIT协议 发布

## 安装

#### CDN
```html
<script src="https://cdn.jsdelivr.net/npm/plugincore-js-sdk/dist/PluginCore.min.js"></script>
```

#### npm
```bash
npm install @yiyungent/plugincore --save
```

## 使用

### 使用模块管理器
```js
import PluginCore from '@yiyungent/plugincore';

const p = new PluginCore();
```
### 在页面中使用
```html
    <body>
        <div id="app">
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

## 相关项目

 
## 鸣谢



## Donate

plugincore-js-sdk is an MIT licensed open source project and completely free to use. However, the amount of effort needed to maintain and develop new features for the project is not sustainable without proper financial backing.

We accept donations through these channels:
- <a href="https://afdian.net/@yiyun" target="_blank">爱发电</a>

## Author

**plugincore-js-sdk** © [yiyun](https://github.com/yiyungent), Released under the [MIT](./LICENSE) License.<br>
Authored and maintained by yiyun with help from contributors ([list](https://github.com/yiyungent/plugincore-js-sdk/contributors)).

> GitHub [@yiyungent](https://github.com/yiyungent)

