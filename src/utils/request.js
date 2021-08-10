import axios from "axios";
import { MessageBox, Message } from "element-ui";
import store from "@/store";
import { getToken } from "@/utils/auth";

// create an axios instance
const service = axios.create({
  baseURL: process.env.VUE_APP_BASE_API, // url = base url + request url
  // withCredentials: true, // send cookies when cross-domain requests
  timeout: 5000 // request timeout
});

// request interceptor
service.interceptors.request.use(
  config => {
    // do something before request is sent

    if (store.getters.token) {
      // let each request carry token
      // ['X-Token'] is a custom headers key
      // please modify it according to the actual situation
      config.headers["Authorization"] = "Bearer " + getToken();
    }
    return config;
  },
  error => {
    // do something with request error
    console.log(error); // for debug
    return Promise.reject(error);
  }
);

// response interceptor
service.interceptors.response.use(
  /**
   * If you want to get http information such as headers or status
   * Please return  response => response
   */

  /**
   * Determine the request status by custom code
   * Here is just an example
   * You can also judge the status by HTTP Status Code
   */
  response => {
    // status: 2xx

    const res = response.data;
    res.status = response.status;

    console.log("PluginCore.Admin: res", res);

    // 不拦截: 由 具体的 view 自己处理
    // if the custom code < 0, it is judged as an error.
    // if (res.code < 0) {

    //   // Message({
    //   //   message: res.message || "Error",
    //   //   type: "error",
    //   //   duration: 5 * 1000
    //   // });
      
    //   return Promise.reject(new Error(res.message || "Error"));
    // } else {
    //   return res;
    // }

    return res;
  },
  error => {
    // 请求已经发出, 但 status 不在 2xx 范围内
    // 【务必注意】这里的error输出的不是一个对象【error.response才是一个对象】
    // eg: error是字符串: Error: Request failed with status code 401

    if (error.response) {
      const res = error.response.data || { };
      res.status = error.response.status;

      console.log("PluginCore.Admin: res", res);

      // 401: Illegal token; 401: Other clients logged in; 401: Token expired;
      if (res.status === 401) {
        // to re-login
        MessageBox.confirm(
          "401: 没有权限, 你需要重新登录",
          "确认退出登录",
          {
            confirmButtonText: "重新登录",
            cancelButtonText: "取消",
            type: "warning"
          }
        ).then(() => {
          store.dispatch("user/resetToken").then(() => {
            location.reload();
          });
        });
      } else {

        Message({
          message: res.message || "Error",
          type: "error",
          duration: 5 * 1000
        });

      }

    } else {
      // Something happened in setting up the request that triggered an Error
      console.log( "Error", error.message);
    }

    return Promise.reject(res);
  }
);

export default service;
