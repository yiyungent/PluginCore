<template>
  <div class="app-container">
    <div class="rem-plugin-details">
      <el-page-header
        title="返回"
        content="插件详细"
        class="rem-page-header"
        @back="goBack"
      />
      <el-card class="box-card">
        <div class="rem-item">
          <span class="rem-label">PluginId:</span><span>{{ info.pluginId }}</span>
        </div>
        <div class="rem-item">
          <span class="rem-label">显示名:</span><span>{{ info.displayName }}</span>
        </div>
        <div class="rem-item">
          <span class="rem-label">描述:</span><span>{{ info.description }}</span>
        </div>
        <div class="rem-item">
          <span class="rem-label">作者:</span><span>{{ info.author }}</span>
        </div>
        <div class="rem-item">
          <span class="rem-label">版本:</span><span><el-tag>{{ info.version }}</el-tag></span>
        </div>
        <div class="rem-item">
          <span class="rem-label">支持版本:</span><span><el-tag v-for="(v, index) in info.supportedVersions" :key="index">{{
            v
          }}</el-tag></span>
        </div>
        <div class="rem-item">
          <span class="rem-label">状态:</span><span><el-tag>{{ info.status | statusFilter }}</el-tag></span>
        </div>
      </el-card>
    </div>
  </div>
</template>

<script>
import { detailsAction } from "@/api/admin/plugins";
import { showMessage } from "@/utils/tools";

const pluginStatusOptions = [
  { key: "-1", display_name: "已安装" },
  { key: "0", display_name: "已启用" },
  { key: "1", display_name: "未启用" },
  { key: "2", display_name: "未安装" }
];

// arr to obj, such as { CN : "China", US : "USA" }
const pluginStatusKeyValue = pluginStatusOptions.reduce((acc, cur) => {
  acc[cur.key] = cur.display_name;
  return acc;
}, {});

export default {
  filters: {
    statusFilter(status) {
      return pluginStatusKeyValue[status];
    }
  },
  data() {
    return {
      info: {
        pluginId: "",
        displayName: "",
        description: "",
        author: "",
        version: "",
        supportedVersions: [],
        status: 0
      }
    };
  },
  created() {
    this.loadInfo();
  },
  methods: {
    async loadInfo() {
      var pluginId = this.$route.params.pluginId;
      var res = await detailsAction(pluginId);
      if (res.code < 0) {
        showMessage(res);
      } else {
        this.info = res.data;
      }
    },
    goBack() {
      this.$router.back();
    }
  }
};
</script>

<style lang="scss" scoped>
.rem-plugin-details {
//   margin: 26px 26px 14px;
  .rem-page-header {
    margin-bottom: 24px;
  }
  .rem-item {
    margin-bottom: 8px;
    .rem-label {
      padding-right: 20px;
    }
  }
}
</style>
