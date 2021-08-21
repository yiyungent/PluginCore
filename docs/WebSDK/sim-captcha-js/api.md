<!--
 * @Author: yiyun
 * @Description: 
-->
# API

## show() 显示验证码

显示验证码层

```js
show()
```

## hidden() 隐藏验证码

隐藏当前验证码弹出层，下次show 将使用当前验证码图片base64
用于用户手动点击关闭按钮

```js
hidden()
```

## destroy() 摧毁验证码

摧毁当前验证码（隐藏验证码弹出层，清除验证码图片base64），下次show 将请求新验证码图片base64

```js
destroy()
```

## getTicket() 获取票据

return: Object:{"appId":"","ticket":""}

```js
getTicket()
```




