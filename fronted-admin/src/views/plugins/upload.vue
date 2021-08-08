<template>
  <div class="app-container">
    <el-upload
      drag
      :action="uploadAction"
      accept=".zip"
      auto-upload
      :on-success="uploadSuccess"
      :headers="headers"
    >
      <i class="el-icon-upload" />
      <div class="el-upload__text">
        将插件压缩包拖到此处，或<em>点击上传</em>
      </div>
      <div slot="tip" class="el-upload__tip">
        只能上传zip文件，且不超过5MB
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
      uploadAction: process.env.VUE_APP_BASE_API + "/admin/plugins/upload",
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
