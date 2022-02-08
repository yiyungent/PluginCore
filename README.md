<p align="center">
<!-- <img src="docs/_images/logo.png" alt="daylib"> -->
</p>
<h1 align="center">daylib</h1>

> :cake: 日常开发前端库

[![repo size](https://img.shields.io/github/repo-size/yiyungent/daylib.svg?style=flat)]()
[![LICENSE](https://img.shields.io/github/license/yiyungent/daylib.svg?style=flat)](https://github.com/yiyungent/daylib/blob/master/LICENSE)
[![NPM version](https://img.shields.io/npm/v/daylib.svg)](https://www.npmjs.com/package/daylib)
[![NPM downloads](https://img.shields.io/npm/dt/daylib)](https://www.npmjs.com/package/daylib)
[![jsDelivr](https://img.shields.io/jsdelivr/npm/hy/daylib)](https://www.jsdelivr.com/package/npm/daylib)


## 介绍

日常开发前端库
 + **简单** - 简单易用.
 + **免费** - MIT协议 发布

## 安装

#### CDN
```html
<script src="https://cdn.jsdelivr.net/npm/daylib/dist/daylib.min.js"></script>
```

#### npm
```bash
npm install daylib --save
```

## 使用

### 使用模块管理器
```js
import DayLib from 'daylib';

const daylib = new DayLib();
```
### 在页面中使用
```html
    <body>
        <div id="app">
        </div>
        <script src="https://cdn.jsdelivr.net/npm/daylib/dist/daylib.min.js"></script>
        <script>
			var daylib = new DayLib();
			// 发送 get 请求
			daylib.http.get("", function() {
				// ...
			})
        </script>
    </body>
```

## 相关项目

 
## 鸣谢



## Donate

daylib is an MIT licensed open source project and completely free to use. However, the amount of effort needed to maintain and develop new features for the project is not sustainable without proper financial backing.

We accept donations through these channels:
- <a href="https://afdian.net/@yiyun" target="_blank">爱发电</a>

## Author

**daylib** © [yiyun](https://github.com/yiyungent), Released under the [MIT](./LICENSE) License.<br>
Authored and maintained by yiyun with help from contributors ([list](https://github.com/yiyungent/daylib/contributors)).

> GitHub [@yiyungent](https://github.com/yiyungent)

