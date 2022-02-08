// import handleOption from "./options.js";
import {
  httpGet,
  httpPost,
  getOffsetTop,
  getOffsetLeft,
  removeElement,
} from "./utils.js";

import { util } from "./utils2.js"


function DayLib() {
  this.util = util;
}
DayLib.prototype = {
  constructor: DayLib,

  // 以下只会出现在 __proto__ 中

  http: {
    get: httpGet,
    post: httpPost
  },

  dom: {
    getOffsetTop: getOffsetTop,
    getOffsetLeft: getOffsetLeft,
    removeElement: removeElement
  },


};

String.prototype.format = function () {
  var args = arguments;
  return this.replace(/\{(\d+)\}/g, function (s, i) {
    return args[i];
  });
};


export default DayLib;
