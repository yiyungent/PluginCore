<template>
  <div>
    <span>登录成功, 即将跳转页面。</span>
  </div>
</template>

<script>
// 登陆成功回调页面: 在授权中心登陆后, 它会跳转到此页面
import oidcService from "@/open-id-connect/oidcService";
import storage from "@/storage";

export default {
  data() {
    return {
      redirect: storage.get("redirect")
    };
  },
  created() {
    oidcService
      .signinRedirectCallback()
      .then(user => {
        // 存到 vuex中
        this.$store
          .dispatch("user/login", user.access_token)
          .then(() => {
            setTimeout(() => {
              // 登录后在此处读出之前存起来的 redirectUrl, 如果有的话就跳转过去，否则首页
              this.$router.push({ path: this.redirect || "/" });
            }, 3000);
          })
          .catch(() => {});
      })
      .catch(err => {
        console.log(err);
      });
  }
};
</script>

<style></style>
