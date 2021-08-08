<template>
  <div></div>
</template>

<script>
// 静默刷新 access_token: 重新获取登录状态
import Oidc from "oidc-client";

export default {
  mounted() {
    console.log("trying to renew the acess_token");
    new Oidc.UserManager().signinSilentCallback();
  }
};
</script>

<style></style>
