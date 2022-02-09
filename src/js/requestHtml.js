import axios from "axios";

// https://github.com/yiyungent/hexo-asset-img
// document.location.origin: https://github.com
// axios.defaults.baseURL = document.location.origin;

const requestHtml = function (url, params) {
  return new Promise((resolve, reject) => {
    axios({
      url: url,
      method: "get",
      params: params,
      responseType: "document"
    })
      .then((res) => {
        console.log("requestHtml.res", res);
        // res.data.status = res.status;
        resolve(res.data);
      })
      .catch((err) => {
        // err.data.status = err.status;
        reject(err.data);
      });
  });
};

export default requestHtml;
