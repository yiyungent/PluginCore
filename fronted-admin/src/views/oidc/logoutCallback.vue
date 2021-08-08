<template>
  <div>
    <span>退出成功, 即将跳转页面。</span>
  </div>
</template>

<script>
// 退出登陆成功回调页面: 在授权中心退出登录后, 它会跳转到此页面
// import oidcService from "@/open-id-connect/oidcService";
import storage from "@/storage";

export default {
  data() {
    return {
      redirect: storage.get("redirect")
    };
  },
  created() {
    setTimeout(() => {
      // 登录后在此处读出之前存起来的 redirect, 如果有的话就跳转过去，否则首页
      this.$router.push({ path: this.redirect || "/" });
    }, 3000);
  }
};
</script>

<style></style>
