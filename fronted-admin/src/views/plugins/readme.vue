<template>
  <div class="app-container">
    <el-page-header
      title="返回"
      content="插件文档"
      class="rem-page-header"
      @back="goBack"
    />
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>{{ item.pluginId }}</span>
        <span style="float: right; padding: 3px 0" type="text">{{
          item.content == "" ? "无文档" : ""
        }}</span>
      </div>
      <markdown-preview :initial-value="item.content" />
    </el-card>
  </div>
</template>
<script>
import { MarkdownPreview } from "vue-meditor";
import { readmeAction } from "@/api/admin/plugins";
import { showMessage } from "@/utils/tools";

export default {
  components: {
    MarkdownPreview
  },
  data() {
    return {
      item: { pluginId: "", content: "" }
    };
  },
  created() {
    this.loadContent();
  },
  methods: {
    async loadContent() {
      var pluginId = this.$route.params.pluginId;
      var res = await readmeAction(pluginId);
      if (res.code < 0) {
        showMessage(res);
      } else {
        this.item = res.data;
      }
    },
    goBack() {
      this.$router.back();
    }
  }
};
</script>
<style lang="scss" scoped>
.rem-page-header {
  margin-bottom: 24px;
}
</style>
