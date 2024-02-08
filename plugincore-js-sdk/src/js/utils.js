// import { util } from "webpack"; // 不要导入这个, 会报错

var utils = {};

// 字符串格式化
utils.format = function (src) {
  if (arguments.length == 0) return null;
  let args = Array.prototype.slice.call(arguments, 1);
  return src.replace(/\{(\d+)\}/g, function (m, i) {
    return args[i];
  });
};

// 设置cookie的函数  （名字，值，过期时间（天））
utils.setCookie = function (cname, cvalue, exdays) {
  let d = new Date();
  d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
  let expires = "expires=" + d.toUTCString();
  document.cookie = cname + "=" + cvalue + "; " + expires;
};

//获取cookie
//取cookie的函数(名字) 取出来的都是字符串类型 子目录可以用根目录的cookie，根目录取不到子目录的 大小4k左右
utils.getCookie = function (cname) {
  let name = cname + "=";
  let ca = document.cookie.split(";");
  for (let i = 0; i < ca.length; i++) {
    let c = ca[i].trim();
    if (c.indexOf(name) === 0) return c.substring(name.length, c.length);
  }
  return "";
};

utils.percent = function (v) {
  let n = parseFloat(v);
  return utils.format("{0}%", n.toFixed(2));
};

utils.str2Int = function (v) {
  return parseInt(v);
};

utils.unit = new Array("B", "KB", "MB", "GB");

utils.b2string = function (v, rate) {
  let n = v;
  let i = 0;
  for (; n > rate; ) {
    n /= rate;
    i++;
    if (i === utils.unit.length) {
      break;
    }
  }
  return utils.format("{0}{1}", Math.round(n), utils.unit[i]);
};

utils.parseDOM = function (arg) {
  var objE = document.createElement("div");
  objE.innerHTML = arg;
  return objE.childNodes;
};

utils.cloneNodes = function (nodeList) {
  // 注意: NodeList 不是数组, 实际上这里有问题
  let rtn = [];
  nodeList.forEach(node => {
    rtn.push(node.cloneNode(true));
  });

  return rtn;
}

utils.dynamicLoad = {
  css: (href, onloadCallbackStr) => {
    let loadStr = `
      (function () {
          var hm = document.createElement("link");
          hm.href = "${href}";
          // 注意: 需要设置以下两项, 浏览器才会加载
          hm.setAttribute("rel", "stylesheet");
          hm.setAttribute("type", "text/css");
          hm.onload = () => {
            ${onloadCallbackStr};

            // 加载完就删除
            // hm.remove();
          };
          var heads = document.getElementsByTagName("head"); 
          if(heads.length) 
            heads[0].appendChild(hm); 
          else 
            document.documentElement.appendChild(hm);
      })();`;

    return loadStr;
  },

  js: (src, onloadCallbackStr) => {
    let loadStr = `
      (function () {
          var hm = document.createElement("script");
          hm.src = "${src}";
          hm.onload = () => {
            ${onloadCallbackStr};

            // 加载完就删除
            // hm.remove();
          };
          var heads = document.getElementsByTagName("head"); 
          if(heads.length) 
            heads[0].appendChild(hm); 
          else 
            document.documentElement.appendChild(hm);
      })();`;

    return loadStr;
  }
};

export default utils;
