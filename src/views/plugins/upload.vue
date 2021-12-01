<template>
  <div class="app-container">
    <el-upload
      drag
      :action="uploadAction"
      auto-upload
      :on-success="uploadSuccess"
      :headers="headers"
    >
      <i class="el-icon-upload" />
      <div class="el-upload__text">
        {{ $t("pluginUpload.uploadTip") }}<em>{{ $t("pluginUpload.clickUpload") }}</em>
      </div>
      <div slot="tip" class="el-upload__tip">
        {{ $t("pluginUpload.uploadLimit") }}
      </div>
    </el-upload>
  </div>
</template>

<script>
import { Message } from "element-ui";
import { getToken } from "@/utils/auth";

export default {
  data() {
    return {
      uploadAction: process.env.VUE_APP_BASE_API + "/plugincore/admin/plugins/upload",
      headers: { Authorization: "Bearer " + getToken() }
    };
  },
  methods: {
    uploadSuccess(res, file, fileList) {
      if (res.code < 0) {
        Message({
          message: res.message,
          type: "error",
          duration: 5 * 1000
        });
      } else {
        Message({
          message: res.message,
          type: "success",
          duration: 5 * 1000
        });
      }
    }
  }
};
</script>

<style></style>
