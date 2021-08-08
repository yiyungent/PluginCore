<template>
  <div class="app-container">
    <el-table
      v-loading="listLoading"
      :data="list"
      element-loading-text="Loading"
      border
      fit
      highlight-current-row
    >
      <el-table-column align="center" label="PluginId" width="110">
        <template slot-scope="scope">
          {{ scope.row.pluginId }}
        </template>
      </el-table-column>
      <el-table-column align="center" label="插件名" width="110">
        <template slot-scope="scope">
          {{ scope.row.displayName }}
        </template>
      </el-table-column>
      <el-table-column align="center" label="描述">
        <template slot-scope="scope">
          {{ scope.row.description }}
        </template>
      </el-table-column>
      <el-table-column label="作者" width="110" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.author }}</span>
        </template>
      </el-table-column>
      <el-table-column label="状态" width="110" align="center">
        <template slot-scope="scope">
          <el-tag>{{ scope.row.status | statusFilter }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="版本" width="110" align="center">
        <template slot-scope="scope">
          <el-tag>{{ scope.row.version }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column
        label="操作"
        align="center"
        class-name="small-padding fixed-width"
      >
        <template slot-scope="{ row }">
          <el-button
            v-if="row.status == '2'"
            size="mini"
            type="success"
            @click="installClick(row.pluginId)"
          >
            安装
          </el-button>
          <el-button
            v-if="row.status == '0'"
            size="mini"
            type="success"
            @click="disableClick(row.pluginId)"
          >
            禁用
          </el-button>
          <el-button
            v-if="row.status == '1'"
            size="mini"
            @click="enableClick(row.pluginId)"
          >
            启用
          </el-button>
          <el-button
            v-if="row.status == '1'"
            size="mini"
            type="danger"
            @click="uninstallClick(row.pluginId)"
          >
            卸载
          </el-button>
          <el-button
            v-if="row.status == '2'"
            size="mini"
            type="danger"
            @click="deleteClick(row.pluginId)"
          >
            删除
          </el-button>
          <el-button
            v-if="row.status == '0' || row.status == '1'"
            size="mini"
            type="info"
            @click="settingsClick(row.pluginId)"
          >
            设置
          </el-button>
          <el-button
            size="mini"
            type="info"
            @click="detailsClick(row.pluginId)"
          >
            详细
          </el-button>
          <el-button size="mini" type="info" @click="readmeClick(row.pluginId)">
            文档
          </el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import {
  listAction,
  installAction,
  deleteAction,
  uninstallAction,
  enableAction,
  disableAction
} from "@/api/admin/plugins";
import { Message } from "element-ui";

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
      list: null,
      listLoading: true
    };
  },
  created() {
    this.loadList();
  },
  methods: {
    loadList() {
      this.listLoading = true;
      listAction().then(response => {
        this.list = response.data;
        this.listLoading = false;
      });
    },
    installClick(pluginId) {
      installAction(pluginId).then(this.showMessage);
    },
    uninstallClick(pluginId) {
      uninstallAction(pluginId).then(this.showMessage);
    },
    enableClick(pluginId) {
      enableAction(pluginId).then(this.showMessage);
    },
    disableClick(pluginId) {
      disableAction(pluginId).then(this.showMessage);
    },
    deleteClick(pluginId) {
      deleteAction(pluginId).then(this.showMessage);
    },
    detailsClick(pluginId) {
      this.$router.push({
        name: "Plugins_Details",
        params: { pluginId: pluginId }
      });
    },
    readmeClick(pluginId) {
      this.$router.push({
        name: "Plugins_Readme",
        params: { pluginId: pluginId }
      });
    },
    settingsClick(pluginId) {
      this.$router.push({
        name: "Plugins_Settings",
        params: { pluginId: pluginId }
      });
    },
    showMessage(res) {
      if (res.code > 0) {
        Message({
          message: res.message,
          type: "success",
          duration: 5 * 1000
        });
      } else {
        Message({
          message: res.message,
          type: "error",
          duration: 5 * 1000
        });
      }

      this.loadList();
    }
  }
};
</script>
