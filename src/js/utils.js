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

export default utils;
