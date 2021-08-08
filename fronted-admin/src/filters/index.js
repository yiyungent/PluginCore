import moment from "moment";

const dateFormat = (input, fmtstring) => {
  // 使用momentjs这个日期格式化类库实现日期的格式化功能
  // 注意：统一使用 utc 时间
  return moment(input)
    .utc()
    .format(fmtstring);
};

const moneyFormat = value => {
  if (!value) return "0.00";
  // /原来用的是Number(value).toFixed(0)，这样取整时有问题，例如0.51取整之后为1，感谢Nils指正/
  var intPart = Math.floor(value); // 获取整数部分
  var intPartFormat = intPart.toString().replace(/(\d)(?=(?:\d{3})+)/g, "1,"); // 将整数部分逢三一断
  var floatPart = ".00"; // 预定义小数部分
  var value2Array = value.split(".");
  // =2表示数据有小数位
  if (value2Array.length == 2) {
    floatPart = value2Array[1].toString(); // 拿到小数部分
    if (floatPart.length == 1) {
      // 补0,实际上用不着
      return intPartFormat + "." + floatPart + "0";
    } else {
      return intPartFormat + "." + floatPart;
    }
  } else {
    return intPartFormat + floatPart;
  }
};

const subStrPretty = (value, length) => {
  if (value) {
    if (value.length > length) {
      return value.substr(0, length) + "...";
    } else {
      return value;
    }
  }
  return "";
};

const numPretty = value => {
  if (value) {
    if (value >= 10000) {
      return Math.round((value / 10000) * 100) / 100;
    } else {
      return value;
    }
  }
  return 0;
};

export { dateFormat, moneyFormat, subStrPretty, numPretty };
