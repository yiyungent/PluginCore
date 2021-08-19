

## API


  - GitHub 登录 [/api/GitHub/Login](/api/GitHub/Login)

  - 我的 GitHub 仓库 (默认授权登录后跳转到此处) [/api/GitHub/Index](/api/GitHub/Index)




  > 无需关注回调, 回调地址 (`RedirectUrl`) 固定: /api/GitHub/Callback

  > 你的 `GitHub OAuth Apps` 中 `Authorization callback URL` 填写 `https://localhost:5001/api/GitHub/Callback`      
  > 其中的 `https://localhost:5001` 需改为你自己的地址



  > 插件 设置 填写相对url (/xxx/xxx), 会自动转为当前请求的绝对url

  > 例如: 在第三方登录区域, GitHub登录 按钮 超链接到 /api/GitHub/Login  ,         
  > 授权登录成功后跳转到 /Home/Last, 那么就将 `settings.json` 中的 `HomePageUrl` 改为 /Home/Last


  ## 参考

- [Scopes for OAuth Apps - GitHub Docs](https://docs.github.com/en/developers/apps/building-oauth-apps/scopes-for-oauth-apps)
- [Authorizing OAuth Apps - GitHub Docs](https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps#redirect-urls)