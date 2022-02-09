import handleOption from "./options.js";
import { util } from "./utils.js";
import requestHtml from "./requestHtml.js";

/**
 * 搜索注释节点
 * @param {Element} ele 最好从 body element 开始搜索
 * @param {Function} callback 注释节点 回调函数
 */
function eachComment(ele, callback) {
  for (var i = 0; i < ele.childNodes.length; i++) {
    var child = ele.childNodes[i];
    if (child.nodeType == 8) {
      // console.log(child.nodeValue);
      callback(child);
    } else if (child.childNodes) {
      eachComment(child);
    }
  }
}

function start() {
  console.log("plugincore-js-sdk: start");
  let bodyElement = document.getElementsByTagName("body")[0];
  eachComment(bodyElement, processComment);
}

function processComment(node) {
  const pluginWidgetFlag = "PluginCore.IPlugins.IWidgetPlugin.Widget";
  // <!-- PluginCore.IPlugins.IWidgetPlugin.Widget(PluginCore.Admin.Footer,a,b,c) -->
  if (node.nodeValue.indexOf(pluginWidgetFlag) != -1) {
    // (PluginCore.Admin.Footer,a,b,c)
    let temp1 = node.nodeValue.trim().replaceAll(pluginWidgetFlag, "");
    if (temp1.indexOf("(") != 0 || temp1.lastIndexOf(")") != temp1.length - 1) {
      // 外面没有 (), 非法格式
      return;
    }
    // PluginCore.Admin.Footer,a,b,c
    let temp2 = temp1.substring(1, temp1.length - 1);
    // PluginCore.Admin.Footer
    let widgetKey = temp2.substring(0, temp2.indexOf(","));
    // a,b,c
    let extraPars = temp2.substring(temp2.indexOf(",") + 1);
    // 发送请求, 获取 html
    requestHtml(this.options.baseUrl + "/api/plugincore/PluginWidget/Widget", {
      widgetKey: widgetKey,
      extraPars: extraPars,
    }).then((res) => {
      // 用 widget html 替换注释节点
      let widgetHtml = util.parseDOM(res.data);
      console.log("widgetHtml", widgetHtml);
      node.replaceWith(widgetHtml);
    });

    
  }
}

function PluginCore(options) {
  // 处理 options
  this.options = handleOption(options);

  this.util = util;
}
PluginCore.prototype = {
  constructor: PluginCore,

  // 以下只会出现在 __proto__ 中

  start: start,
};

String.prototype.format = function () {
  var args = arguments;
  return this.replace(/\{(\d+)\}/g, function (s, i) {
    return args[i];
  });
};

export default PluginCore;
