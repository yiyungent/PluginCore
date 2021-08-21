# 指南

## 项目简介

一个轻量的 Web 应用框架, 具有优雅、高效、简洁、富于表达力等优点。采用 前后端分离 设计，是崇尚开发效率的全栈框架

- **简洁友好** - 统一的设计规范，精心打磨的操作界面回应你的期待。
- **易扩展** - 一套完整的插件机制，以 约定优于配置 为中心的项目结构，无论是对开发者还是使用者都如此友好。

前端 基于 `vue-element-admin`，后端 基于 `.NET Core3.1` ， RESTful + Semantic WebAPI 设计，采用 `UHub（IdentityServer4）` 完成认证授权。

Remember.Core 目前仅提供了插件框架，若你需要实现一个Web插件系统，或许 Remember.Core 是不错的参考，这也是本项目的目的，作为 插件系统实现的参考。

## 框架技术栈

<!-- ![](/images/Remember.Core生态.png) -->
<img :src="$withBase('/images/Remember.Core生态.png')">

## 项目分层

<!-- ![](/images/project-structure.png) -->
<img :src="$withBase('/images/project-structure.png')">

<!-- ![](/images/PluginCore.png) -->
<img :src="$withBase('/images/PluginCore.png')">

## 功能一览

- **上传本地插件** - 热插拔：无论是加载，卸载都无需重启你的站点

- **放置钩子** - 让插件行为加入框架

- **全程 `依赖注入`** - 你可在插件生命周期获取你注入的任何服务

<!-- ![](/images/pluginDependence.png) -->
<img :src="$withBase('/images/pluginDependence.png')">

- **以 `约定优于配置`** 为中心的项目结构 - 只需关注你的业务

<!-- ![](/images/plugin-structure.png) -->
<img :src="$withBase('/images/plugin-structure.png')">

- **一插件一 LoadContext** - 插件间彼此隔离

- **Framework 域共享机制** - 免去重复加载

- **简单易用** - `PluginFinder`、`PluginManager` 或许你仅仅需要它们

<!-- ![](/images/PluginFinder.png) -->
<!-- ![](/images/PluginManager.png) -->
<img :src="$withBase('/images/PluginFinder.png')">
<img :src="$withBase('/images/PluginManager.png')">

- **一套完整的 插件生命周期** - 在需要时做你想做

<!-- ![](/images/screenshot/2020-10-29-18-33-40.png) -->
<img :src="$withBase('/images/screenshot/2020-10-29-18-33-40.png')">

- **动态扩展 WebAPI** - 每个插件都是一个 WebAPI

<!-- ![](/images/screenshot/2020-10-29-18-40-28.png) -->
<img :src="$withBase('/images/screenshot/2020-10-29-18-40-28.png')">

- **完整插件的机制** - 从上传，设置，禁用再到卸载，一次打通

<!-- ![](/images/screenshot/2020-10-29-18-41-59.png) -->
<img :src="$withBase('/images/screenshot/2020-10-29-18-41-59.png')">

<!-- ![](/images/screenshot/2020-10-29-18-42-27.png) -->
<img :src="$withBase('/images/screenshot/2020-10-29-18-42-27.png')">

<!-- ![](/images/screenshot/2020-10-29-18-44-05.png) -->
<img :src="$withBase('/images/screenshot/2020-10-29-18-44-05.png')">

- **多数据库切换** - 让EF做它该做的事


- **轻量的插件框架** - 易用不过如此

<!-- ![](/images/screenshot/2020-11-24-19-49-49.png) -->
<img :src="$withBase('/images/screenshot/2020-11-24-19-49-49.png')">


## 补充

- 作者博客: https://moeci.com

- 作者邮箱: i@moeci.com

- Remember.Core 的前生 是 在线学习系统 [remember](https://github.com/yiyungent/remember)

- Remember 系列 是作者在大学的作品，写的不好，还请见谅

- 插件系统设计参考自: https://github.com/lamondlu/Mystique，本框架去除了一些个人认为不必要的，再进行了一些高度封装、扩展
