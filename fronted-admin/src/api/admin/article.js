import request from "@/utils/request";

export function listAction(page) {
  return request({
    url: "/admin/article/list",
    method: "get",
    params: { pageIndex: page.pageIndex, pageSize: page.pageSize }
  });
}

export function deleteAction(id) {
  return request({
    url: "/admin/article/delete",
    method: "post",
    params: { id: id }
  });
}
