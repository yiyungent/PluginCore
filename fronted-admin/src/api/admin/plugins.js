import request from "@/utils/request";

export function listAction(status) {
  return request({
    url: "/admin/plugins/list",
    method: "get",
    params: { status: status }
  });
}

export function installAction(pluginId) {
  return request({
    url: "/admin/plugins/install",
    method: "post",
    params: { pluginId: pluginId }
  });
}

export function uninstallAction(pluginId) {
  return request({
    url: "/admin/plugins/uninstall",
    method: "post",
    params: { pluginId: pluginId }
  });
}

export function deleteAction(pluginId) {
  return request({
    url: "/admin/plugins/delete",
    method: "post",
    params: { pluginId: pluginId }
  });
}

export function enableAction(pluginId) {
  return request({
    url: "/admin/plugins/enable",
    method: "post",
    params: { pluginId: pluginId }
  });
}

export function disableAction(pluginId) {
  return request({
    url: "/admin/plugins/disable",
    method: "post",
    params: { pluginId: pluginId }
  });
}

export function detailsAction(pluginId) {
  return request({
    url: "/admin/plugins/details",
    method: "get",
    params: { pluginId: pluginId }
  });
}

export function readmeAction(pluginId) {
  return request({
    url: "/admin/plugins/readme",
    method: "get",
    params: { pluginId: pluginId }
  });
}

export function settingsAction(pluginId) {
  return request({
    url: "/admin/plugins/settings",
    method: "get",
    params: { pluginId: pluginId }
  });
}

export function settingsSaveAction(pluginId, data) {
  return request({
    url: "/admin/plugins/settings",
    method: "post",
    data: { pluginId: pluginId, data: data }
  });
}
