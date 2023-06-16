//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PluginCore.AspNetCore.Authorization;
using PluginCore.Config;
using PluginCore.AspNetCore.RequestModel.User;
using PluginCore.AspNetCore.ResponseModel;

namespace PluginCore.AspNetCore.Controllers
{
    [Route("api/plugincore/admin/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AccountManager _accountManager;

        public string RemoteFronted
        {
            get
            {
                return PluginCore.Config.PluginCoreConfigFactory.Create().RemoteFrontend;
            }
        }

        public UserController(AccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpGet, HttpPost]
        public async Task<ActionResult<BaseResponseModel>> Login([FromBody] LoginRequestModel requestModel)
        {
            BaseResponseModel responseModel = new BaseResponseModel();

            try
            {
                string token = this._accountManager.CreateToken(requestModel.UserName, requestModel.Password);
                bool isAdmin = this._accountManager.IsAdminToken(token);
                if (!isAdmin)
                {
                    responseModel.Code = -1;
                    responseModel.Message = "用户名或密码不正确";

                    return await Task.FromResult(responseModel);
                }

                responseModel.Code = 1;
                responseModel.Message = "登录成功";
                responseModel.Data = new
                {
                    token = token,
                    userName = requestModel.UserName
                };
            }
            catch (Exception ex)
            {
                responseModel.Code = -1;
                responseModel.Message = "失败: " + ex.Message;
            }

            return await Task.FromResult(responseModel);
        }

        [HttpGet, HttpPost]
        public async Task<ActionResult<BaseResponseModel>> Logout()
        {
            BaseResponseModel responseModel = new BaseResponseModel()
            {
                Code = 1,
                Message = "退出登录成功"
            };

            return await Task.FromResult(responseModel);
        }

        [PluginCoreAdminAuthorize]
        [HttpGet, HttpPost]
        public async Task<ActionResult<BaseResponseModel>> Info()
        {
            BaseResponseModel responseModel = new BaseResponseModel();

            try
            {
                string adminUserName = PluginCoreConfigFactory.Create().Admin.UserName;

                responseModel.Code = 1;
                responseModel.Message = "成功";
                responseModel.Data = new
                {
                    name = adminUserName,
                    //avatar = this.RemoteFronted + "/images/avatar.gif"
                    avatar = ""
                };
            }
            catch (Exception ex)
            {
                responseModel.Code = -1;
                responseModel.Message = "失败: " + ex.Message;
            }

            return await Task.FromResult(responseModel);
        }

        [PluginCoreAdminAuthorize]
        [HttpGet, HttpPost]
        public async Task<ActionResult<BaseResponseModel>> Update([FromBody] UpdateRequestModel requestModel)
        {
            BaseResponseModel responseModel = new BaseResponseModel();

            try
            {
                PluginCoreConfig pluginCoreConfig = PluginCoreConfigFactory.Create();
                pluginCoreConfig.Admin.UserName = requestModel.UserName;
                pluginCoreConfig.Admin.Password = requestModel.Password;
                PluginCoreConfigFactory.Save(pluginCoreConfig);

                responseModel.Code = 1;
                responseModel.Message = "修改成功, 需要重新登录";
            }
            catch (Exception ex)
            {
                responseModel.Code = -1;
                responseModel.Message = "失败: " + ex.Message;
            }

            return await Task.FromResult(responseModel);
        }

    }
}
