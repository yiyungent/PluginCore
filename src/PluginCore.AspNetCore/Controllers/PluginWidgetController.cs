//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PluginCore;
using PluginCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PluginCore.AspNetCore.Authorization;
using PluginCore.AspNetCore.ResponseModel;
using PluginCore.IPlugins;
using System.Text;
using PluginCore.Interfaces;

namespace PluginCore.AspNetCore.Controllers
{
    [Route("api/plugincore/[controller]/[action]")]
    [ApiController]
    public class PluginWidgetController : ControllerBase
    {
        #region Fields
        private readonly IPluginFinder _pluginFinder;
        #endregion

        #region Ctor
        public PluginWidgetController(IPluginFinder pluginFinder)
        {
            _pluginFinder = pluginFinder;
        }
        #endregion

        #region Actions

        #region Widget
        /// <summary>
        /// Widget
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost]
        //public async Task<ActionResult<CommonResponseModel>> Widget(string widgetKey, string extraPars = "")
        public async Task<ActionResult> Widget(string widgetKey, string extraPars = "")
        {
            BaseResponseModel responseModel = new ResponseModel.BaseResponseModel();
            string responseData = "";
            widgetKey = widgetKey.Trim('"', '\'');
            string[] extraParsArr = null;
            if (!string.IsNullOrEmpty(extraPars))
            {
                extraParsArr = extraPars.Split(",", StringSplitOptions.RemoveEmptyEntries);
                extraParsArr = extraParsArr.Select(m => m.Trim('"', '\'')).ToArray();
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"<!-- start:PluginCore.IPlugins.IWidgetPlugin.Widget({widgetKey},{extraPars}) -->");
            try
            {
                List<IWidgetPlugin> plugins = this._pluginFinder.EnablePlugins<IWidgetPlugin>().ToList();
                foreach (var item in plugins)
                {
                    string widgetStr = await item.Widget(widgetKey, extraParsArr);
                    if (!string.IsNullOrEmpty(widgetStr))
                    {
                        // TODO: 配合 PluginCoreConfig.PluginWidgetDebug
                        // TODO: PluginCoreConfig 改为 Options 模式, 避免手动反复读取文件 效率低
                        //sb.AppendLine($"<!-- {item.GetType().ToString()}: -->");

                        sb.AppendLine(widgetStr);
                    }
                }

            }
            catch (Exception ex)
            {
                Utils.LogUtil.Exception(ex);
                sb.AppendLine($"<!-- Exception: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}, Details: Console -->");
            }
            sb.AppendLine($"<!-- end:PluginCore.IPlugins.IWidgetPlugin.Widget({widgetKey},{extraPars}) -->");
            responseData = sb.ToString();

            responseModel.Code = 1;
            responseModel.Message = "Load Widget Success";
            responseModel.Data = responseData;

            //return await Task.FromResult(responseModel);
            return Content(responseData, "text/html;charset=utf-8");
        }
        #endregion

        #endregion

    }
}
