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
      <el-table-column align="center" label="ID" width="50">
        <template slot-scope="scope">
          {{ scope.row.id }}
        </template>
      </el-table-column>
      <el-table-column align="center" label="标题" width="110">
        <template slot-scope="scope">
          {{ scope.row.title }}
        </template>
      </el-table-column>
      <el-table-column align="center" label="作者" width="110">
        <template slot-scope="scope">
          {{ scope.row.author.userName }}
        </template>
      </el-table-column>
      <el-table-column label="创建时间" width="140" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.createTime | dateFormat("YYYY-MM-DD HH:mm") }}</span>
        </template>
      </el-table-column>
      <el-table-column label="最后更新" width="140" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.lastUpdateTime | dateFormat("YYYY-MM-DD HH:mm") }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="操作"
        align="center"
        class-name="small-padding fixed-width"
      >
        <template slot-scope="{ row }">
          <el-button
            size="mini"
            type="success"
            @click="goUrlClick(row.customUrl)"
          >
            查看
          </el-button>
          <el-button size="mini" type="info" @click="editClick(row.id)">
            编辑
          </el-button>
          <el-button size="mini" type="danger" @click="deleteClick(row.id)">
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { listAction, deleteAction } from "@/api/admin/article";
// import { Message } from "element-ui";
import { showMessage } from "@/utils/tools";

export default {
  filters: {},
  data() {
    return {
      list: null,
      listLoading: true,
      page: {
        pageIndex: 1,
        pageSize: 5,
        totalCount: 0
      }
    };
  },
  created() {
    this.loadList();
  },
  methods: {
    loadList() {
      this.listLoading = true;
      listAction(this.page).then(response => {
        this.list = response.data.list;
        this.page.pageIndex = response.pageIndex;
        this.page.pageSize = response.pageSize;
        this.page.totalCount = response.totalCount;
        this.listLoading = false;
      });
    },
    goUrlClick(url) {
      window.open(url, "_blank");
    },
    editClick(id) {
      this.$router.push({
        name: "Article_Edit",
        params: { id: id }
      });
    },
    deleteClick(id) {
      deleteAction(id).then(res => {
        showMessage(res, this.loadList);
      });
    }
  }
};
</script>
