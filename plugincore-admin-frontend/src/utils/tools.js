import { Message } from "element-ui";

export function showMessage(res, callback) {
  if (res.code > 0) {
    Message({
      message: res.message,
      type: "success",
      duration: 5 * 1000
    });
  } else {
    Message({
      message: res.message,
      type: "error",
      duration: 5 * 1000
    });
  }

  if (callback) {
    callback();
  }
}
