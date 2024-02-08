import "./css/plugincore.css";

import PluginCore from "./js/plugincore.js";
import env from "./env.js";

const VERSION = env.version;
const GIT_HASH = "";
console.log(
  `${"\n"} %c plugincore-js-sdk v${VERSION} ${GIT_HASH} %c https://github.com/yiyungent/plugincore-js-sdk ${"\n"}${"\n"}`,
  "color: #fff; background: #030307; padding:5px 0;",
  "background: #ff80ab; padding:5px 0;"
);

export default PluginCore;
