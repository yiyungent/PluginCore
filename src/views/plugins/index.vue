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
      <el-table-column
        align="center"
        :label="$t('pluginList.displayName')"
        width="110"
      >
        <template slot-scope="scope">
          {{ scope.row.displayName }}
        </template>
      </el-table-column>
      <el-table-column align="center" :label="$t('pluginList.desc')">
        <template slot-scope="scope">
          {{ scope.row.description }}
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('pluginList.author')"
        width="110"
        align="center"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.author }}</span>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('pluginList.status')"
        width="110"
        align="center"
      >
        <template slot-scope="scope">
          <el-tag>{{ getStatus(scope.row.status) }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('pluginList.version')"
        width="110"
        align="center"
      >
        <template slot-scope="scope">
          <el-tag>{{ scope.row.version }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('pluginList.operation')"
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
            {{ $t("pluginList.clickInstall") }}
          </el-button>
          <el-button
            v-if="row.status == '0'"
            size="mini"
            type="success"
            @click="disableClick(row.pluginId)"
          >
            {{ $t("pluginList.clickDisable") }}
          </el-button>
          <el-button
            v-if="row.status == '1'"
            size="mini"
            @click="enableClick(row.pluginId)"
          >
            {{ $t("pluginList.clickEnable") }}
          </el-button>
          <el-button
            v-if="row.status == '1'"
            size="mini"
            type="danger"
            @click="uninstallClick(row.pluginId)"
          >
            {{ $t("pluginList.clickUninstall") }}
          </el-button>
          <el-button
            v-if="row.status == '2'"
            size="mini"
            type="danger"
            @click="deleteClick(row.pluginId)"
          >
            {{ $t("pluginList.clickDelete") }}
          </el-button>
          <el-button
            v-if="row.status == '0' || row.status == '1'"
            size="mini"
            type="info"
            @click="settingsClick(row.pluginId)"
          >
            {{ $t("pluginList.clickSettings") }}
          </el-button>
          <el-button
            size="mini"
            type="info"
            @click="detailsClick(row.pluginId)"
          >
            {{ $t("pluginList.clickDetails") }}
          </el-button>
          <el-button size="mini" type="info" @click="readmeClick(row.pluginId)">
            {{ $t("pluginList.clickReadme") }}
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
} from "@/api/plugins";
import { Message } from "element-ui";

export default {
  filters: {},
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
    getStatus(status) {
      let pluginStatusOptions = [
        { key: "-1", display_name: this.$t("pluginList.statusInstall") },
        { key: "0", display_name: this.$t("pluginList.statusEnable") },
        { key: "1", display_name: this.$t("pluginList.statusDisable") },
        { key: "2", display_name: this.$t("pluginList.statusUninstall") }
      ];
      // arr to obj, such as { CN : "China", US : "USA" }
      let pluginStatusKeyValue = pluginStatusOptions.reduce((acc, cur) => {
        acc[cur.key] = cur.display_name;
        return acc;
      }, {});

      return pluginStatusKeyValue[status];
    },
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
