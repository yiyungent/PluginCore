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
      // responseType: "document" // 不适合: 会自动将内容用 <html><head></head><body>内容</body></html> 包裹, 而且 注释节点位置不一定在 body 里面
      responseType: "text"
    })
      .then((res) => {

        // console.log("requestHtml.res", res);

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
