<template>
  <div class="app-container">
    <el-page-header
      :title="$t('common.back')"
      :content="$t('pluginSettings.title')"
      class="rem-page-header"
      @back="goBack"
    />
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>{{ item.pluginId }}</span>
        <span style="float: right; padding: 3px 0" type="text">{{
          item.content == "" ? $t('pluginSettings.noSettings') : ""
        }}</span>
      </div>
      <div class="editor-container">
        <json-editor ref="jsonEditor" v-model="item.content" />
      </div>
    </el-card>
    <el-button size="mini" type="primary" @click="settingsSaveClick()">
      {{ $t('pluginSettings.save') }}
    </el-button>
  </div>
</template>
<script>
import { settingsAction } from "@/api/plugins";
import { showMessage } from "@/utils/tools";
import JsonEditor from "@/components/JsonEditor";
import { settingsSaveAction } from "@/api/plugins";

export default {
  components: { JsonEditor },
  data() {
    return {
      item: { pluginId: this.$route.params.pluginId, content: "" }
    };
  },
  created() {
    this.loadContent();
  },
  methods: {
    async loadContent() {
      var pluginId = this.$route.params.pluginId;
      var res = await settingsAction(pluginId);
      if (res.code < 0) {
        showMessage(res);
      } else {
        this.item.content = JSON.parse(res.data);
      }
    },
    async settingsSaveClick() {
      var res = await settingsSaveAction(this.item.pluginId, this.item.content);
      showMessage(res);
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
.editor-container {
  position: relative;
  height: 100%;
}
</style>
