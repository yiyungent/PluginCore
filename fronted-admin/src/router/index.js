import Vue from "vue";
import Router from "vue-router";

Vue.use(Router);

/* Layout */
import Layout from "@/layout";

/**
 * Note: sub-menu only appear when route children.length >= 1
 * Detail see: https://panjiachen.github.io/vue-element-admin-site/guide/essentials/router-and-nav.html
 *
 * hidden: true                   if set true, item will not show in the sidebar(default is false)
 * alwaysShow: true               if set true, will always show the root menu
 *                                if not set alwaysShow, when item has more than one children route,
 *                                it will becomes nested mode, otherwise not show the root menu
 * redirect: noRedirect           if set noRedirect will no redirect in the breadcrumb
 * name:'router-name'             the name is used by <keep-alive> (must set!!!)
 * meta : {
    roles: ['admin','editor']    control the page roles (you can set multiple roles)
    title: 'title'               the name show in sidebar and breadcrumb (recommend set)
    icon: 'svg-name'/'el-icon-x' the icon show in the sidebar
    breadcrumb: false            if set false, the item will hidden in breadcrumb(default is true)
    activeMenu: '/example/list'  if set path, the sidebar will highlight the path you set
  }
 */

/**
 * constantRoutes
 * a base page that does not have permission requirements
 * all roles can be accessed
 */
export const constantRoutes = [
  // 登录回调
  {
    path: "/oidc/callback",
    component: () => import("@/views/oidc/callback"),
    hidden: true
  },

  // 退出登录回调
  {
    path: "/oidc/logoutCallback",
    component: () => import("@/views/oidc/logoutCallback"),
    hidden: true
  },

  // 静默登录刷新access_token
  {
    path: "/oidc/silentRenew",
    component: () => import("@/views/oidc/silentRenew"),
    hidden: true
  },

  {
    path: "/404",
    component: () => import("@/views/404"),
    hidden: true
  },

  {
    path: "/",
    component: Layout,
    redirect: "/dashboard",
    children: [
      {
        path: "dashboard",
        name: "Dashboard",
        component: () => import("@/views/dashboard/index"),
        meta: { title: "首页", icon: "dashboard" }
      }
    ]
  },

  {
    path: "/plugins",
    component: Layout,
    redirect: "/plugins/list",
    name: "Plugins",
    meta: { title: "插件管理", icon: "el-icon-s-help" },
    children: [
      {
        path: "list",
        name: "Plugins_List",
        component: () => import("@/views/plugins/index"),
        meta: { title: "插件列表", icon: "table" }
      },
      {
        path: "upload",
        name: "Plugins_Upload",
        component: () => import("@/views/plugins/upload"),
        meta: { title: "上传插件", icon: "tree" }
      },
      {
        path: "details/:pluginId",
        name: "Plugins_Details",
        component: () => import("@/views/plugins/details"),
        meta: { title: "插件详细", icon: "tree" },
        hidden: true
      },
      {
        path: "readme/:pluginId",
        name: "Plugins_Readme",
        component: () => import("@/views/plugins/readme"),
        meta: { title: "插件文档", icon: "tree" },
        hidden: true
      },
      {
        path: "settings/:pluginId",
        name: "Plugins_Settings",
        component: () => import("@/views/plugins/settings"),
        meta: { title: "插件设置", icon: "tree" },
        hidden: true
      }
    ]
  },

  {
    path: "/article",
    component: Layout,
    redirect: "/article/list",
    name: "Article",
    meta: { title: "文章管理", icon: "el-icon-s-help" },
    children: [
      {
        path: "list",
        name: "Article_List",
        component: () => import("@/views/article/index"),
        meta: { title: "文章列表", icon: "table" }
      },
      {
        path: "create",
        name: "Article_Create",
        component: () => import("@/views/article/create"),
        meta: { title: "添加文章", icon: "table" }
      }
    ]
  },

  // 404 page must be placed at the end !!!
  { path: "*", redirect: "/404", hidden: true }
];

const createRouter = () =>
  new Router({
    mode: "history", // require service support
    scrollBehavior: () => ({ y: 0 }),
    routes: constantRoutes
    // mode: "hash" // 注意: 由于 oidc-client 找回调地址中的id_token等不是使用query?id_token=xxx, 而是取 #之后, eg: #id_token
    // http://localhost:9528/oidc/callback#id_token=eyJhbGciOiJSUzI1NiIsImt
    // 使用 授权码code模式，就会采用query?方式，而使用implicit模式，则会采用#方式
  });

const router = createRouter();

// Detail see: https://github.com/vuejs/vue-router/issues/1234#issuecomment-357941465
export function resetRouter() {
  const newRouter = createRouter();
  router.matcher = newRouter.matcher; // reset router
}

export default router;
