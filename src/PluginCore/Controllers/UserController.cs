using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PluginCore.Authorization;
using PluginCore.Config;
using PluginCore.RequestModel.User;
using PluginCore.ResponseModel;

namespace PluginCore.Controllers
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
                return PluginCore.Config.PluginCoreConfigFactory.Create().RemoteFronted;
            }
        }

        public UserController(AccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public async Task<ActionResult<CommonResponseModel>> Login([FromBody] LoginRequestModel requestModel)
        {
            CommonResponseModel responseModel = new CommonResponseModel();

            try
            {
                string token = this._accountManager.CreateToken(requestModel.UserName, requestModel.Password);
                bool isAdmin = this._accountManager.IsAdminToken(token);
                if (!isAdmin)
                {
                    responseModel.code = -1;
                    responseModel.message = "用户名或密码不正确";

                    return await Task.FromResult(responseModel);
                }

                responseModel.code = 1;
                responseModel.message = "登录成功";
                responseModel.data = new
                {
                    token = token,
                    userName = requestModel.UserName
                };
            }
            catch (Exception ex)
            {
                responseModel.code = -1;
                responseModel.message = "失败: " + ex.Message;
            }

            return await Task.FromResult(responseModel);
        }

        public async Task<ActionResult<CommonResponseModel>> Logout()
        {
            CommonResponseModel responseModel = new CommonResponseModel()
            {
                code = 1,
                message = "退出登录成功"
            };

            return await Task.FromResult(responseModel);
        }

        [PluginCoreAdminAuthorize]
        public async Task<ActionResult<CommonResponseModel>> Info()
        {
            CommonResponseModel responseModel = new CommonResponseModel();

            try
            {
                string adminUserName = PluginCoreConfigFactory.Create().Admin.UserName;

                responseModel.code = 1;
                responseModel.message = "成功";
                responseModel.data = new
                {
                    name = adminUserName,
                    avatar = this.RemoteFronted + "/images/avatar.gif"
                };
            }
            catch (Exception ex)
            {
                responseModel.code = -1;
                responseModel.message = "失败: " + ex.Message;
            }

            return await Task.FromResult(responseModel);
        }

        [PluginCoreAdminAuthorize]
        public async Task<ActionResult<CommonResponseModel>> Update([FromBody] UpdateRequestModel requestModel)
        {
            CommonResponseModel responseModel = new CommonResponseModel();

            try
            {
                PluginCoreConfig pluginCoreConfig = PluginCoreConfigFactory.Create();
                pluginCoreConfig.Admin.UserName = requestModel.UserName;
                pluginCoreConfig.Admin.Password = requestModel.Password;
                PluginCoreConfigFactory.Save(pluginCoreConfig);

                responseModel.code = 1;
                responseModel.message = "修改成功, 需要重新登录";
            }
            catch (Exception ex)
            {
                responseModel.code = -1;
                responseModel.message = "失败: " + ex.Message;
            }

            return await Task.FromResult(responseModel);
        }

    }
}
