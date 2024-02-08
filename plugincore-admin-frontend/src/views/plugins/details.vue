<template>
  <div class="app-container">
    <div class="rem-plugin-details">
      <el-page-header
        :title="$t('common.back')"
        :content="$t('pluginDetails.title')"
        class="rem-page-header"
        @back="goBack"
      />
      <el-card class="box-card">
        <div class="rem-item">
          <span class="rem-label">PluginId:</span><span>{{ info.pluginId }}</span>
        </div>
        <div class="rem-item">
          <span class="rem-label">{{ $t("pluginDetails.displayName") }}:</span><span>{{ info.displayName }}</span>
        </div>
        <div class="rem-item">
          <span class="rem-label">{{ $t("pluginDetails.desc") }}:</span><span>{{ info.description }}</span>
        </div>
        <div class="rem-item">
          <span class="rem-label">{{ $t("pluginDetails.author") }}:</span><span>{{ info.author }}</span>
        </div>
        <div class="rem-item">
          <span class="rem-label">{{ $t("pluginDetails.version") }}:</span><span><el-tag>{{ info.version }}</el-tag></span>
        </div>
        <div class="rem-item">
          <span
            class="rem-label"
          >{{ $t("pluginDetails.supportedVersions") }}:</span><span><el-tag
            v-for="(v, index) in info.supportedVersions"
            :key="index"
          >{{ v }}</el-tag></span>
        </div>
        <div class="rem-item">
          <span class="rem-label">{{ $t("pluginDetails.status") }}:</span><span><el-tag>{{ getStatus(info.status) }}</el-tag></span>
        </div>
      </el-card>
    </div>
  </div>
</template>

<script>
import { detailsAction } from '@/api/plugins'
import { showMessage } from '@/utils/tools'

export default {
  filters: {},
  data() {
    return {
      info: {
        pluginId: '',
        displayName: '',
        description: '',
        author: '',
        version: '',
        supportedVersions: [],
        status: 0
      }
    }
  },
  created() {
    this.loadInfo()
  },
  methods: {
    getStatus(status) {
      const pluginStatusOptions = [
        { key: '-1', display_name: this.$t('pluginList.statusInstall') },
        { key: '0', display_name: this.$t('pluginList.statusEnable') },
        { key: '1', display_name: this.$t('pluginList.statusDisable') },
        { key: '2', display_name: this.$t('pluginList.statusUninstall') }
      ]
      // arr to obj, such as { CN : "China", US : "USA" }
      const pluginStatusKeyValue = pluginStatusOptions.reduce((acc, cur) => {
        acc[cur.key] = cur.display_name
        return acc
      }, {})

      return pluginStatusKeyValue[status]
    },
    async loadInfo() {
      var pluginId = this.$route.params.pluginId
      var res = await detailsAction(pluginId)
      if (res.code < 0) {
        showMessage(res)
      } else {
        this.info = res.data
      }
    },
    goBack() {
      this.$router.back()
    }
  }
}
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
