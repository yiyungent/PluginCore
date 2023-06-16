//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace PluginCore.AspNetCore.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        #region Old
        //private readonly IWebHostEnvironment _webHostEnvironment;

        //public bool IsLocalFronted
        //{
        //    get
        //    {
        //        return PluginCore.Config.PluginCoreConfigFactory.Create().IsLocalFrontend;
        //    }
        //}

        //public string RemoteFronted
        //{
        //    get
        //    {
        //        return PluginCore.Config.PluginCoreConfigFactory.Create().RemoteFrontend;
        //    }
        //}

        //public HomeController(IWebHostEnvironment webHostEnvironment)
        //{
        //    this._webHostEnvironment = webHostEnvironment;
        //}

        //[Route("PluginCore/Admin")]
        //public async Task<ActionResult> Home()
        //{
        //    if (this.IsLocalFronted)
        //    {
        //        var localIndexFilePath = Path.Combine(
        //            this._webHostEnvironment.ContentRootPath, "PluginCoreAdmin", "index.html");

        //        return PhysicalFile(localIndexFilePath, "text/html");
        //    }
        //    else
        //    {
        //        string htmlStr = string.Empty;
        //        HttpClient httpClient = new HttpClient();
        //        htmlStr = await httpClient.GetStringAsync(this.RemoteFronted + "/index.html");

        //        return Content(htmlStr, "text/html", Encoding.UTF8);
        //    }
        //} 
        #endregion

    }
}
