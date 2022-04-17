using Microsoft.AspNetCore.Mvc;
using PluginCore.AspNetCore.Authorization;
using PluginCore.AspNetCore.ResponseModel;
using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace PluginCore.AspNetCore.Controllers
{
    [Route("api/plugincore/admin/[controller]/[action]")]
    [PluginCoreAdminAuthorize]
    [ApiController]
    public class DebugController : ControllerBase
    {
        #region Fields
        private readonly IPluginContextManager _pluginContextManager;
        #endregion

        #region Ctor
        public DebugController(IPluginContextManager pluginContextManager)
        {
            _pluginContextManager = pluginContextManager;
        }
        #endregion

        #region Actions

        [HttpGet, HttpPost]
        public async Task<ActionResult<CommonResponseModel>> PluginContext()
        {
            CommonResponseModel responseModel = new CommonResponseModel();
            try
            {
                var pluginContextList = _pluginContextManager.All();
                Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();
                foreach (var pluginContext in pluginContextList)
                {
                    keyValuePairs.Add($"{pluginContext.GetType().ToString()} - {pluginContext.GetHashCode()}", pluginContext.Assemblies.Select(m => m.FullName).ToList());
                }

                responseModel.code = 1;
                responseModel.message = "success";
                responseModel.data = keyValuePairs;
            }
            catch (Exception ex)
            {
                responseModel.code = -1;
                responseModel.message = "error";
                responseModel.data = ex.ToString();
            }

            return await Task.FromResult(responseModel);
        }

        #endregion

    }

}
