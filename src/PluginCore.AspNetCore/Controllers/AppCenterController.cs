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
//using ResponseModel;


namespace PluginCore.AspNetCore.Controllers
{
    /// <summary>
    /// 应用中心
    /// <para>插件</para>
    /// </summary>
    [Route("api/plugincore/admin/[controller]/[action]")]
    [PluginCoreAdminAuthorize]
    [ApiController]
    public class AppCenterController : ControllerBase
    {
        #region Fields

        private static Dictionary<string, Task> _pluginDownloadTasks;

        #endregion

        #region Ctor

        static AppCenterController()
        {
            _pluginDownloadTasks = new Dictionary<string, Task>();
        }

        public AppCenterController()
        {

        }
        #endregion

        #region Actions

        #region 插件列表
        /// <summary>
        /// 插件
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet, HttpPost]
        public async Task<ActionResult<BaseResponseModel>> Plugins(string query = "")
        {
            BaseResponseModel responseDTO = new BaseResponseModel();
            IList<PluginRegistryResponseModel> pluginRegistryModels = new List<PluginRegistryResponseModel>();
            try
            {
                // 1. TODO: 从json文件中读取插件订阅源 registry url
                string registryUrl = "";
                // 2. TODO: 向订阅源发送 http get 获取插件列表信息  eg: http://rem-core-plugins-registry.moeci.com/?query=xxx
                IList<string> remotePluginIds = new List<string>();

                // 3. 根据本地已有 PluginId 插件情况 状态赋值
                PluginConfigModel pluginConfigModel = PluginConfigModelFactory.Create();
                IList<string> localPluginIds = pluginConfigModel.EnabledPlugins.Concat(pluginConfigModel.DisabledPlugins).Concat(pluginConfigModel.UninstalledPlugins).ToList();



                responseDTO.Code = 1;
                responseDTO.Message = "获取远程插件数据成功";
                responseDTO.Data = pluginRegistryModels;
            }
            catch (Exception ex)
            {
                responseDTO.Code = -1;
                responseDTO.Message = "获取远程插件数据失败: " + ex.Message;
                responseDTO.Data = pluginRegistryModels;
            }

            return await Task.FromResult(responseDTO);
        }
        #endregion

        #region 下载插件
        [HttpGet, HttpPost]
        public async Task<ActionResult<BaseResponseModel>> DownloadPlugin(string pluginDownloadUrl = "")
        {
            BaseResponseModel responseDTO = new BaseResponseModel();

            #region 效验
            if (string.IsNullOrEmpty(pluginDownloadUrl))
            {
                responseDTO.Code = -1;
                responseDTO.Message = "插件下载地址不正确";
                return responseDTO;
            }
            // TODO: 效验是否本地已经存在相同pluginId的插件

            #endregion

            try
            {
                // 1.执行下载操作, TODO:存在问题，阻塞对性能不好，但不阻塞又不好通知用户插件下载进度，以及可能存在在插件下载过程中，用户再次点击下载
                WebClient webClient = new WebClient();
                // TODO: 插件下载文件路径
                string pluginDownloadFilePath = "";
                //webClient.DownloadFileAsync(new Uri(pluginDownloadFilePath), "");
                Task task = webClient.DownloadFileTaskAsync(pluginDownloadUrl, pluginDownloadFilePath);

                _pluginDownloadTasks.Add(pluginDownloadUrl, task);

                webClient.DownloadFileCompleted += Plugin_DownloadFileCompleted;
                webClient.DownloadProgressChanged += Plugin_DownloadProgressChanged;
                webClient.Disposed += WebClient_Disposed;

                responseDTO.Code = 1;
                responseDTO.Message = "开始下载插件";
            }
            catch (Exception ex)
            {
                responseDTO.Code = -1;
                responseDTO.Message = "下载插件失败: " + ex.Message;
            }

            return await Task.FromResult(responseDTO);
        }




        #endregion

        #region 获取插件下载进度
        [HttpGet, HttpPost]
        public async Task<ActionResult<BaseResponseModel>> DownloadPluginProgress()
        {
            BaseResponseModel responseDTO = new BaseResponseModel();
            try
            {
                responseDTO.Data = new { };



                responseDTO.Code = 1;
                responseDTO.Message = "获取插件下载进度成功";
            }
            catch (Exception ex)
            {
                responseDTO.Code = -1;
                responseDTO.Message = "获取插件下载进度失败: " + ex.Message;
            }

            return await Task.FromResult(responseDTO);
        }
        #endregion

        #endregion

        #region Helpers

        /// <summary>
        /// 插件下载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Plugin_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Console.WriteLine("插件下载完成");
            // 1.从 _pluginDownloadTasks 中移除
            //_pluginDownloadTasks.Remove();
            // 2. 解压插件

        }

        private void Plugin_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine($"插件下载进度改变: {e.ProgressPercentage}% {e.BytesReceived}/{e.TotalBytesToReceive}");
        }

        private void WebClient_Disposed(object sender, EventArgs e)
        {
            if (sender is WebClient webClient)
            {
                Console.WriteLine(webClient.BaseAddress);
            }

            Console.WriteLine(nameof(WebClient_Disposed) + ": " + sender.ToString());
        }

        #endregion

    }
}
