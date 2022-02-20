import handleOption from "./options.js";
import utils from "./utils.js";
import requestHtml from "./requestHtml.js";


let _options = {};

let _eachFinishCallback = (pluginPars)=>{
  if(_options.debug) {
    console.info(`plugincore-js-sdk: eachFinish: ${pluginPars.widgetKey},${pluginPars.extraPars}`);
  }
};

//#region Event
let _eventSet = {};

function addEventListener(eventKey, callback) {
  if (!_eventSet[eventKey]) {
    _eventSet[eventKey] = []
  }
  _eventSet[eventKey].push(callback);
}

function invokeEvent(eventKey, pars) {
  let callbacks = _eventSet[eventKey];
  callbacks.forEach(c => {
    c(pars);
  });
}
//#endregion


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
      eachComment(child, callback);
    }
  }
}

function processScript(scriptNode) {
  if (scriptNode.text == "" && scriptNode.src != "") {
      // src
      // 注意: 此种方法, 需要 至少当前页面已经存在一个 <script></script>
      let loadSrcStr = `var _hmt = _hmt || [];
      (function () {
          var hm = document.createElement("script");
          hm.src = "${scriptNode.src}";
          hm.onload = () => {
            if (${_options.debug}) {
              console.log("load finished: ${scriptNode.src}");
            }
            window.plugincore.invokeEvent("load", "${scriptNode.src}");

            // 加载完就删除, 因为在 widgetHtml 中有这个内容了
            hm.remove();
          };
          var s = document.getElementsByTagName("script")[0];
          s.parentNode.insertBefore(hm, s);
      })();`;

      if (_options.debug) {
        console.info("scriptStr", loadSrcStr);
      }

      // return loadSrcStr;
      eval(loadSrcStr);
  } else {
      // script 内容
      // return scriptNode.text;
      if (_options.debug) {
        console.info("scriptStr", scriptNode.text);
      }
      eval(scriptNode.text);
  }
}

/**
 * 搜索 <script></script>
 * @param {Element} ele 需要多次, 对 插件返回的每一个节点搜索
 * @param {Function} callback script节点 回调函数
 */
function eachScript(ele, callback) {
  // 注意: 当前节点也要算
  if (ele.nodeType == 1 && ele.nodeName == "SCRIPT") {
    callback(ele);
  } else {

    for (var i = 0; i < ele.childNodes.length; i++) {
      var child = ele.childNodes[i];
      if (child.nodeType == 1 && child.nodeName == "SCRIPT") {
        // console.log(child.text); // 注意: 这里需要通过 .text 获取 script 内容, nodeValue 为 null
        callback(child);
      } else if (child.childNodes) {
        eachScript(child, callback);
      }
    }

  }
}

/**
 * 处理插件返回的 html 字符串
 * @param {Element} node 
 * @param {String} res 
 */
function processHtml(node, res) {
  // 用 widget html 替换注释节点
  let widgetHtml = utils.parseDOM(res);

  if (_options.debug) {
    console.info("widgetHtml", widgetHtml);
  }

  // 注意: 深度克隆, 不要赋值传地址
  let tempWidgetHtml = utils.cloneNodes(widgetHtml);

  // 1. 替换 html
  node.replaceWith(...widgetHtml);

  // 注意: widgetHtml 被 node.replaceWith 后 为 NodeList()[], 因此需要先存起来

  if (_options.debug) {
    console.info("tempWidgetHtml", tempWidgetHtml);
  }

  // let scriptStr = "";
  // 对 widgetHtml 搜索 script
  tempWidgetHtml.forEach(
    tempNode => { 
      eachScript(tempNode, scriptNode => {
        // 末尾加个 ; 防止有不规范的代码 影响之后的执行
        // scriptStr += processScript(scriptNode) + ";";
        processScript(scriptNode);
      });          
    }
  );

  // if (_options.debug) {
  //   console.info("scriptStr", scriptStr);
  // }

  // 1. 替换 html
  // node.replaceWith(...widgetHtml);
  // 取消: 不再一个扩展点, 一次执行, 而是一个扩展点内的内容一步步执行
  // 2. 解析 执行 js
  // eval(scriptStr);
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
    requestHtml(_options.baseUrl + "/api/plugincore/PluginWidget/Widget", {
      widgetKey: widgetKey,
      extraPars: extraPars,
    }).then((res) => {

      processHtml(node, res);
      _eachFinishCallback({
        widgetKey: widgetKey,
        extraPars: extraPars,
      });

    });

    
  }
}


function start(eachFinishCallback) {
  _eachFinishCallback = eachFinishCallback || _eachFinishCallback;
  console.info("plugincore-js-sdk: start");
  // let rootElement = document.getElementsByTagName("body")[0];
  // 直接从 document 开始搜索, 这样 可以在 <head></head> 中插入扩展点
  let rootElement = document;
  eachComment(rootElement, processComment);
}

function PluginCore(options) {
  // 处理 options
  this.options = handleOption(options);
  _options = this.options;

  this.utils = utils;
}
PluginCore.prototype = {
  constructor: PluginCore,

  // 以下只会出现在 __proto__ 中

  start: start,

  addEventListener: addEventListener,

};

String.prototype.format = function () {
  var args = arguments;
  return this.replace(/\{(\d+)\}/g, function (s, i) {
    return args[i];
  });
};

window.plugincore = {};
window.plugincore.invokeEvent = invokeEvent;

export default PluginCore;
