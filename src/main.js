import Vue from 'vue'

import 'normalize.css/normalize.css' // A modern alternative to CSS resets

import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
// import locale from 'element-ui/lib/locale/lang/en' // lang i18n
import i18n from './lang' // internationalization
import Cookies from 'js-cookie'

import '@/styles/index.scss' // global css

import App from './App'
import store from './store'
import router from './router'

import '@/icons' // icon
import '@/permission' // permission control

import myEnv from "@/env"

/**
 * If you don't want to use mock-server
 * you want to use MockJs for mock api
 * you can execute: mockXHR()
 *
 * Currently MockJs will be used in the production environment,
 * please remove it before going online ! ! !
 */
if (process.env.NODE_ENV === 'production') {
  const { mockXHR } = require('../mock')
  mockXHR()
}

// set ElementUI lang to EN
// Vue.use(ElementUI, { locale })
// 如果想要中文版 element-ui，按如下方式声明
// Vue.use(ElementUI);
Vue.use(ElementUI, {
  size: Cookies.get('size') || 'medium', // set element-ui default size
  i18n: (key, value) => i18n.t(key, value)
})

Vue.config.productionTip = false

const VERSION = myEnv.version;
const GIT_HASH = "";
console.log(
  `${"\n"} %c plugincore-admin-frontend v${VERSION} ${GIT_HASH} %c https://github.com/yiyungent/plugincore-admin-frontend ${"\n"}${"\n"}`,
  "color: #fff; background: #030307; padding:5px 0;",
  "background: #ff80ab; padding:5px 0;"
);

new Vue({
  el: '#app',
  router,
  store,
  i18n,
  render: h => h(App)
})
