(window.webpackJsonp=window.webpackJsonp||[]).push([[22],{271:function(e,r,t){},287:function(e,r,t){"use strict";t(271)},303:function(e,r,t){"use strict";t.r(r);var s=t(295),a=t(286),o=t(296),i=t(297);a.c.add(o.a,i.a);var c={components:{FontAwesomeIcon:s.a},props:{plugin:{type:Object,required:!0,default:function(){return{}}}},data:()=>({}),computed:{isNuget(){return!!this.plugin.nugetPackageId},nugetUrl(){return"https://www.nuget.org/packages/"+this.plugin.nugetPackageId},githubOwner(){return this.plugin.github&&this.plugin.github.split("/").length>=2&&this.plugin.github.split("/")[0]}},created(){},methods:{homepageUrlIsGitHub(){return/^https:\/\/github.com\/[^/]+\/[^/]+/.test(this.plugin.homepageUrl)}}},n=(t(287),t(10)),u=Object(n.a)(c,(function(){var e=this,r=e._self._c;return r("div",{staticClass:"resource-card-container"},[r("div",{staticClass:"resource-card-header"},[r("div",{staticClass:"resource-card-header-title"},[e._v("\n      "+e._s(e.plugin.displayName)+"\n      "),r("FontAwesomeIcon",{staticClass:"resource-card-header-icon-check",attrs:{icon:["fas","circle-check"]}})],1),e._v(" "),r("div",{staticClass:"resource-card-header-expand"},[r("FontAwesomeIcon",{staticClass:"resource-card-header-icon-expand",attrs:{icon:["fas","expand"]}})],1)]),e._v(" "),r("div",{staticClass:"resource-card-desc"},[e._v(e._s(e.plugin.description))]),e._v(" "),r("div",{staticClass:"resource-card-footer"},[r("div",{staticClass:"resource-card-footer-tags"},e._l(e.plugin.tags,(function(t){return r("span",{key:t,staticClass:"resource-card-footer-tag"},[e._v(e._s(t))])})),0),e._v(" "),r("div",{staticClass:"resource-card-footer-divider"}),e._v(" "),r("div",{staticClass:"resource-card-footer-info"},[r("div",{staticClass:"resource-card-footer-group"},[r("a",{directives:[{name:"show",rawName:"v-show",value:!!e.plugin.homepageUrl,expression:"!!plugin.homepageUrl"}],attrs:{href:e.plugin.homepageUrl,target:"_blank",rel:"noopener noreferrer"}},[r("FontAwesomeIcon",{directives:[{name:"show",rawName:"v-show",value:e.homepageUrlIsGitHub(),expression:"homepageUrlIsGitHub()"}],staticClass:"resource-card-footer-icon",attrs:{icon:["fab","github"]}}),e._v(" "),r("FontAwesomeIcon",{directives:[{name:"show",rawName:"v-show",value:!e.homepageUrlIsGitHub(),expression:"!homepageUrlIsGitHub()"}],staticClass:"resource-card-footer-icon",attrs:{icon:["fas","link"]}})],1),e._v(" "),r("a",{directives:[{name:"show",rawName:"v-show",value:e.isNuget,expression:"isNuget"}],attrs:{href:e.nugetUrl,target:"_blank",alt:"NuGet",rel:"noopener noreferrer"}},[r("FontAwesomeIcon",{staticClass:"resource-card-footer-icon",attrs:{icon:["fas","cube"]}})],1)]),e._v(" "),r("div",{staticClass:"resource-card-footer-group"},[r("div",{directives:[{name:"show",rawName:"v-show",value:!!e.githubOwner,expression:"!!githubOwner"}],staticClass:"avatar"},[r("div",{staticClass:"resource-card-footer-avatar"},[r("a",{attrs:{href:"https://github.com/"+e.githubOwner,target:"_blank",rel:"noopener noreferrer"}},[r("img",{attrs:{src:`https://github.com/${e.githubOwner}.png?size=80`}})])])]),e._v(" "),r("span",{staticClass:"resource-card-footer-author"},[e._v(e._s(e.plugin.author))])])])])])}),[],!1,null,"6217f8a5",null);r.default=u.exports}}]);