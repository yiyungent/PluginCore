
<h1 align="center">vue-sim-captcha</h1>

> :cake: 一个简单易用的点触验证码, SimCaptcha 前端的 Vue.js 实现

[![repo size](https://img.shields.io/github/repo-size/yiyungent/vue-sim-captcha.svg?style=flat)]()
[![LICENSE](https://img.shields.io/github/license/yiyungent/vue-sim-captcha.svg?style=flat)](https://github.com/yiyungent/vue-sim-captcha/blob/master/LICENSE)
[![NPM version](https://img.shields.io/npm/v/vue-sim-captcha.svg)](https://www.npmjs.com/package/vue-sim-captcha)


<!-- [English](README_en.md) -->

## 介绍

轻松在 Vue.js 下使用点触验证码
 + **简单** - 简单易用.
 + **免费** - MIT协议 发布

## 安装

```bash
PM> npm install vue-sim-captcha --save
```

## 使用
```js
// main.js 或单个组件中 Login.vue
import Vue from 'vue'
import VueSimCaptcha from 'vue-sim-captcha'

Vue.use(VueSimCaptcha)
```

```html
<!-- Login.vue -->
<template>
    <div>
        <div>
             <input type="text" v-model="account" />
             <input type="password" v-model="password" />
        </div>
        <div>
            <button @click="login">登陆</button>
        </div>
        <vue-sim-captcha
        v-model="captcha.showCaptcha"
        :appId="captcha.appId"
        :callback="saveTicket"
        :source="captcha.source"
        ></vue-sim-captcha>
    </div>
</template>
```

```js
// Login.vue
// 以下仅适用于 v0.1.1 及以上
<script>
export default {
    data() {
        account: "",
        password: "",
        // 验证码相关信息
        captcha: {
            appId: "132132",
            source: {
                baseUrl: "https://captcha.moeci.com", // 请替换为你自己的验证服务端url
                imgUrl: "/api/vCode/VCodeImg",
                checkUrl: "/api/vCode/VCodeCheck"
            },
            showCaptcha: false, // 控制显示隐藏验证码层
            ticket: "", // 来自验证服务端
            userId: ""  // 来自验证服务端
      }
    },
    methods:{
        // 验证服务端确认验证通过后,将回调此函数,无需关注内部实现,将由vue-sim-captcha.js自动完成与验证服务端captcha.moeci.com的交互
        saveTicket(res) {
            if (res.code === 0) {
                // 验证码服务端-验证通过
                console.log("第一次验证通过");
                // 第一次验证通过
                // 准备 业务后台 第二次验证
                this.captcha.ticket = res.ticket;
                this.captcha.userId = res.userId;
                console.log("第一次验证 结束");
            }
        },
        // 登陆
        login() {
            if (this.captcha.ticket == "" || this.captcha.userId == "") {
                // 弹出验证码
                this.captcha.showCaptcha = true;
                return;
            }
            // 发送http请求，到业务后台验证
            // 传 ticket, userId 用作第二次验证
            apiLogin(
                this.account,
                this.password,
                this.captcha.ticket,
                this.captcha.userId
            ).then(res => {
                if (res.code > 0) {
                    // 登陆成功
                } else {
                    // 登录失败
                    alert(res.message);
                    this.captcha.ticket = "";
                    this.captcha.userId = "";
                }
            });
        },


    }
}
</script>
```

