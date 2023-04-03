//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PluginCore;
using PluginCore.IPlugins.Infrastructure;

namespace HelloWorldPlugin.Controllers;

/// <summary>
/// 其实也可以不写这个, 直接访问 Plugins/HelloWorldPlugin/index.html
/// 
/// 下面的方法, 是去掉 index.html
/// 
/// 若 wwwroot 下有其它需要访问的文件, 如何 css, js, 而你又不想每次新增 action 指定返回, 则 Route 必须 Plugins/{PluginId},
/// 这样访问 Plugins/HelloWorldPlugin/css/main.css 就会访问到你插件下的 wwwroot/css/main.css
/// </summary>
[Route("Plugins/HelloWorldPlugin")]
public class HomeController : Controller
{
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        string indexFilePath = System.IO.Path.Combine(PluginPathProvider.PluginWwwRootDir("HelloWorldPlugin"), "index.html");

        return PhysicalFile(indexFilePath, "text/html");
    }
}
