(window.webpackJsonp=window.webpackJsonp||[]).push([[17,22],{271:function(e,t,s){},287:function(e,t,s){"use strict";s(271)},298:function(e,t,s){},303:function(e,t,s){"use strict";s.r(t);var r=s(295),a=s(286),o=s(296),i=s(297);a.c.add(o.a,i.a);var n={components:{FontAwesomeIcon:r.a},props:{plugin:{type:Object,required:!0,default:function(){return{}}}},data:()=>({}),computed:{isNuget(){return!!this.plugin.nugetPackageId},nugetUrl(){return"https://www.nuget.org/packages/"+this.plugin.nugetPackageId},githubOwner(){return this.plugin.github&&this.plugin.github.split("/").length>=2&&this.plugin.github.split("/")[0]}},created(){},methods:{homepageUrlIsGitHub(){return/^https:\/\/github.com\/[^/]+\/[^/]+/.test(this.plugin.homepageUrl)}}},c=(s(287),s(10)),u=Object(c.a)(n,(function(){var e=this,t=e._self._c;return t("div",{staticClass:"resource-card-container"},[t("div",{staticClass:"resource-card-header"},[t("div",{staticClass:"resource-card-header-title"},[e._v("\n      "+e._s(e.plugin.displayName)+"\n      "),t("FontAwesomeIcon",{staticClass:"resource-card-header-icon-check",attrs:{icon:["fas","circle-check"]}})],1),e._v(" "),t("div",{staticClass:"resource-card-header-expand"},[t("FontAwesomeIcon",{staticClass:"resource-card-header-icon-expand",attrs:{icon:["fas","expand"]}})],1)]),e._v(" "),t("div",{staticClass:"resource-card-desc"},[e._v(e._s(e.plugin.description))]),e._v(" "),t("div",{staticClass:"resource-card-footer"},[t("div",{staticClass:"resource-card-footer-tags"},e._l(e.plugin.tags,(function(s){return t("span",{key:s,staticClass:"resource-card-footer-tag"},[e._v(e._s(s))])})),0),e._v(" "),t("div",{staticClass:"resource-card-footer-divider"}),e._v(" "),t("div",{staticClass:"resource-card-footer-info"},[t("div",{staticClass:"resource-card-footer-group"},[t("a",{directives:[{name:"show",rawName:"v-show",value:!!e.plugin.homepageUrl,expression:"!!plugin.homepageUrl"}],attrs:{href:e.plugin.homepageUrl,target:"_blank",rel:"noopener noreferrer"}},[t("FontAwesomeIcon",{directives:[{name:"show",rawName:"v-show",value:e.homepageUrlIsGitHub(),expression:"homepageUrlIsGitHub()"}],staticClass:"resource-card-footer-icon",attrs:{icon:["fab","github"]}}),e._v(" "),t("FontAwesomeIcon",{directives:[{name:"show",rawName:"v-show",value:!e.homepageUrlIsGitHub(),expression:"!homepageUrlIsGitHub()"}],staticClass:"resource-card-footer-icon",attrs:{icon:["fas","link"]}})],1),e._v(" "),t("a",{directives:[{name:"show",rawName:"v-show",value:e.isNuget,expression:"isNuget"}],attrs:{href:e.nugetUrl,target:"_blank",alt:"NuGet",rel:"noopener noreferrer"}},[t("FontAwesomeIcon",{staticClass:"resource-card-footer-icon",attrs:{icon:["fas","cube"]}})],1)]),e._v(" "),t("div",{staticClass:"resource-card-footer-group"},[t("div",{directives:[{name:"show",rawName:"v-show",value:!!e.githubOwner,expression:"!!githubOwner"}],staticClass:"avatar"},[t("div",{staticClass:"resource-card-footer-avatar"},[t("a",{attrs:{href:"https://github.com/"+e.githubOwner,target:"_blank",rel:"noopener noreferrer"}},[t("img",{attrs:{src:`https://github.com/${e.githubOwner}.png?size=80`}})])])]),e._v(" "),t("span",{staticClass:"resource-card-footer-author"},[e._v(e._s(e.plugin.author))])])])])])}),[],!1,null,"6217f8a5",null);t.default=u.exports},314:function(e,t,s){"use strict";s(298)},325:function(e,t,s){"use strict";s.r(t);var r=s(303);"undefined"!=typeof window&&(window.global=window);const a=s(315).default;var o={components:{PluginStoreItem:r.default},data:()=>({plugins:[]}),created(){console.log("PluginStore");a.get("https://raw.githubusercontent.com/yiyungent/PluginCore/main/plugins.json").then(e=>{console.log(e.data),this.plugins=e.data.plugins})}},i=(s(314),s(10)),n=Object(i.a)(o,(function(){var e=this._self._c;return e("div",[e("div",{staticClass:"store-container"},this._l(this.plugins,(function(t,s){return e("PluginStoreItem",{key:s,attrs:{plugin:t}})})),1)])}),[],!1,null,"465db761",null);t.default=n.exports}}]);