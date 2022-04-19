//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using Microsoft.AspNetCore.Mvc;
using PluginCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HexoPlugin.Controllers
{
    /// <summary>
    /// ******  注意: 请将 HexoPlugin 修改为你的 PluginId ************
    /// 
    /// 其实也可以不写这个, 直接访问 Plugins/HexoPlugin/index.html
    /// 
    /// 下面的方法, 是去掉 index.html
    /// 
    /// 若 wwwroot 下有其它需要访问的文件, 如何 css, js, 而你又不想每次新增 action 指定返回, 则 Route 必须 Plugins/{PluginId},
    /// 这样访问 Plugins/HexoPlugin/css/main.css 就会访问到你插件下的 wwwroot/css/main.css
    /// </summary>
    [Route("Plugins/HexoPlugin")]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Get()
        {
            string indexFilePath = System.IO.Path.Combine(PluginPathProvider.PluginWwwRootDir("HexoPlugin"), "index.html");

            return PhysicalFile(indexFilePath, "text/html");
        }
    }
}
