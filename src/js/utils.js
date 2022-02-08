/*
 * @Author: yiyun
 * @Description:
 */
/**
 * 发送http GET
 * @param {String} url 请求url
 * @param {Function} callback 请求成功回调函数
 */

function httpGet(url, callback) {
  // XMLHttpRequest对象用于在后台与服务器交换数据
  var xhr = new XMLHttpRequest();
  xhr.open("GET", url, true);
  xhr.onreadystatechange = function () {
    // readyState == 4说明请求已完成
    if ((xhr.readyState == 4 && xhr.status == 200) || xhr.status == 304) {
      // 从服务器获得数据
      callback.call(this, JSON.parse(xhr.responseText));
    }
  };
  xhr.send();
}

/**
 * 发送http POST
 * @param {String} url 请求url
 * @param {Ojbect} data 将要发送的数据包装为对象
 * @param {Function} callback 请求成功回调函数
 */
function httpPost(url, data, callback) {
  var xhr = new XMLHttpRequest();
  xhr.open("POST", url, true);
  // 添加http头，发送信息至服务器时内容编码类型
  // xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
  xhr.setRequestHeader("Content-Type", "application/json");
  xhr.onreadystatechange = function () {
    if (xhr.readyState == 4 && (xhr.status == 200 || xhr.status == 304)) {
      callback.call(this, JSON.parse(xhr.responseText));
    }
  };
  xhr.send(JSON.stringify(data));
}

/**
 * 获取目标html元素相对于整个html文档的位置(y轴)(px)
 * @param {HTMLElement} obj
 */
function getOffsetTop(obj) {
  var tmp = obj.offsetTop;
  var val = obj.offsetParent;
  while (val != null) {
    tmp += val.offsetTop;
    val = val.offsetParent;
  }
  return tmp;
}

/**
 * 获取目标html元素相对于整个html文档的位置(x轴)(px)
 * @param {HTMLElement} obj
 */
function getOffsetLeft(obj) {
  var tmp = obj.offsetLeft;
  var val = obj.offsetParent;
  while (val != null) {
    tmp += val.offsetLeft;
    val = val.offsetParent;
  }
  return tmp;
}

/**
 * 移除目标元素
 * @param {HTMLElement} _element 目标元素
 */
function removeElement(_element) {
  var _parentElement = _element.parentNode;
  if (_parentElement) {
    _parentElement.removeChild(_element);
  }
}

export { httpGet, httpPost, removeElement, getOffsetLeft, getOffsetTop };
