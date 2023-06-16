//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PluginCore.Registry.ResponseModel;
using PluginCore.Registry.Utils;

namespace PluginCore.Registry.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NuGetController : ControllerBase
    {
        public NuGetController()
        {

        }

        [HttpGet]
        public async Task<ActionResult<CommonResponseModel>> List(int pageIndex = 1, string tagName = "PluginCore.Plugin")
        {
            CommonResponseModel responseModel = new ResponseModel.CommonResponseModel();
            // TODO: 没有使用 ResponseModel
            IList<Models.NuGet.NuGetPackageItemModel> responseData = new List<Models.NuGet.NuGetPackageItemModel>();

            // https://www.nuget.org/packages?page=2&q=Tags:"EF"&sortBy=relevance
            // https://www.nuget.org/packages?page=2&q=Tags%3A%22EF%22&sortBy=relevance
            string nugetUrl = string.Format("https://www.nuget.org/packages?page={0}&q=Tags%3A%22{1}%22&sortBy=relevance", pageIndex, tagName);

            string htmlStr;
            using (HttpClient httpClient = new HttpClient())
            {
                htmlStr = await httpClient.GetStringAsync(nugetUrl);
            }

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlStr);
            HtmlNode listPackagesNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='list-packages']");

            //if (!listPackagesNode.HasChildNodes) // 不行, 就算里面为空还是 HasChildNodes 返回 True
            if (string.IsNullOrEmpty(listPackagesNode.InnerHtml?.Trim()))
            {
                responseModel.code = 1;
                responseModel.message = "加载列表成功";
                responseModel.data = responseData;

                return await Task.FromResult(responseModel);
            }

            HtmlNodeCollection packageNodes = listPackagesNode.SelectNodes("./article[@class='package']");


            foreach (var packageNode in packageNodes)
            {
                Models.NuGet.NuGetPackageItemModel model = new Models.NuGet.NuGetPackageItemModel();

                model.PackageIcon = packageNode.SelectSingleNode("./div[@class='row']/div[@class='col-sm-1 hidden-xs hidden-sm col-package-icon']/img[@class='package-icon img-responsive']").Attributes["src"].Value;
                model.PackageId = packageNode.SelectSingleNode("./div[@class='row']/div[@class='col-sm-11']/div[@class='package-header']/a[@class='package-title']").Attributes["data-package-id"].Value;
                model.PackageVersion = packageNode.SelectSingleNode("./div[@class='row']/div[@class='col-sm-11']/div[@class='package-header']/a[@class='package-title']").Attributes["data-package-version"].Value;
                var packageByNodes = packageNode
                    .SelectSingleNode(
                        "./div[@class='row']/div[@class='col-sm-11']/div[@class='package-header']/span[@class='package-by']")
                    .SelectNodes("./a");
                foreach (var node in packageByNodes)
                {
                    model.PackageBy.Add(node.InnerText);
                }

                var packageListNode = packageNode.SelectSingleNode(
                     "./div[@class='row']/div[@class='col-sm-11']/ul[@class='package-list']");
                var liNodes = packageListNode.SelectNodes("./li");
                model.PackageStat.DownloadCount = Convert.ToInt32(liNodes[0].SelectSingleNode("./span").InnerText.Trim().Replace("total downloads", "").Replace(",", ""));
                // TODO: 居然是 9/21/2021, 不是 5 days ago
                model.PackageStat.LastUpdatedText = liNodes[1].SelectSingleNode("./span/span").InnerText;
                model.PackageStat.LastUpdated = Convert.ToDateTime(liNodes[1].SelectSingleNode("./span/span").Attributes["data-datetime"].Value).ToTimeStamp10();

                var tagNodes = liNodes[3].SelectNodes("./span/a");
                foreach (var tagNode in tagNodes)
                {
                    model.PackageStat.Tags.Add(tagNode.InnerText);
                }

                model.PackageDetail = packageNode
                    .SelectSingleNode(
                        "./div[@class='row']/div[@class='col-sm-11']/div[@class='package-details']").InnerText;

                responseData.Add(model);
            }

            responseModel.code = 1;
            responseModel.message = "加载列表成功";
            responseModel.data = responseData;

            return await Task.FromResult(responseModel);
        }



    }
}
