import request from "@/utils/request";

export function listAction(status) {
  return request({
    url: "/plugincore/admin/plugins/list",
    method: "get",
    params: { status: status }
  });
}

export function installAction(pluginId) {
  return request({
    url: "/plugincore/admin/plugins/install",
    method: "post",
    params: { pluginId: pluginId }
  });
}

export function uninstallAction(pluginId) {
  return request({
    url: "/plugincore/admin/plugins/uninstall",
    method: "post",
    params: { pluginId: pluginId }
  });
}

export function deleteAction(pluginId) {
  return request({
    url: "/plugincore/admin/plugins/delete",
    method: "post",
    params: { pluginId: pluginId }
  });
}

export function enableAction(pluginId) {
  return request({
    url: "/plugincore/admin/plugins/enable",
    method: "post",
    params: { pluginId: pluginId }
  });
}

export function disableAction(pluginId) {
  return request({
    url: "/plugincore/admin/plugins/disable",
    method: "post",
    params: { pluginId: pluginId }
  });
}

export function detailsAction(pluginId) {
  return request({
    url: "/plugincore/admin/plugins/details",
    method: "get",
    params: { pluginId: pluginId }
  });
}

export function readmeAction(pluginId) {
  return request({
    url: "/plugincore/admin/plugins/readme",
    method: "get",
    params: { pluginId: pluginId }
  });
}

export function settingsAction(pluginId) {
  return request({
    url: "/plugincore/admin/plugins/settings",
    method: "get",
    params: { pluginId: pluginId }
  });
}

export function settingsSaveAction(pluginId, data) {
  return request({
    url: "/plugincore/admin/plugins/settings",
    method: "post",
    data: { pluginId: pluginId, data: data }
  });
}
