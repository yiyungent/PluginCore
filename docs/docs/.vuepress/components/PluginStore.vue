<template>
  <div>
    <div class="store-container">
      <PluginStoreItem
        v-for="(plugin, index) in plugins"
        :key="index"
        :plugin="plugin"
      />
    </div>
  </div>
</template>
<script>
// https://nonebot.dev/store/plugins

if (typeof window !== "undefined") window.global = window;

// import axios from "axios";
const axios = require("axios").default;

import PluginStoreItem from "./PluginStoreItem.vue";

export default {
  components: {
    PluginStoreItem,
  },

  data() {
    return {
      plugins: [],
    };
  },

  created() {
    console.log("PluginStore");

    // const registryUrl =
    //   "https://ghproxy.net/https://raw.githubusercontent.com/yiyungent/PluginCore/main/plugins.json";
    const registryUrl =
      "https://raw.githubusercontent.com/yiyungent/PluginCore/main/plugins.json";
    axios.get(registryUrl).then((res) => {
      console.log(res.data);
      this.plugins = res.data.plugins;
    });
  },
};
</script>
<style scoped>
.store-container {
  display: grid;
  gap: 1.5rem;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  margin-bottom: 2rem;
  margin-top: 1rem;
}
</style>
