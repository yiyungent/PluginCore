using System.Runtime.CompilerServices;
//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



using Microsoft.AspNetCore.Mvc;
using PluginCore.AspNetCore.Authorization;
using PluginCore.AspNetCore.ResponseModel;
using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using PluginCore.AspNetCore.Extensions;

namespace PluginCore.AspNetCore.Controllers
{
    /// <summary>
    /// [ASP.NET Core — 依赖注入\_啊晚的博客-CSDN博客\_asp.net core 依赖注入](https://blog.csdn.net/weixin_37648525/article/details/127942292)
    /// [ASP.NET Core中的依赖注入（3）: 服务的注册与提供 - Artech - 博客园](https://www.cnblogs.com/artech/p/asp-net-core-di-register.html)
    /// [ASP.NET Core中的依赖注入（5）: ServiceProvider实现揭秘 【总体设计 】 - Artech - 博客园](https://www.cnblogs.com/artech/p/asp-net-core-di-service-provider-1.html)
    /// [dotnet/ServiceProvider.cs at main · dotnet/dotnet](https://github.com/dotnet/dotnet/blob/main/src/runtime/src/libraries/Microsoft.Extensions.DependencyInjection/src/ServiceProvider.cs)
    /// [Net6 DI源码分析Part2 Engine,ServiceProvider - 一身大膘 - 博客园](https://www.cnblogs.com/hts92/p/15800990.html)
    /// [【特别的骚气】asp.net core运行时注入服务，实现类库热插拔 - 四处观察 - 博客园](https://www.cnblogs.com/1996-Chinese-Chen/p/16154218.html)
    /// 
    /// ActivatorUtilities.CreateInstance<PluginCore.IPlugins.IPlugin>(serviceProvider, "test");
    /// ActivatorUtilities.GetServiceOrCreateInstance<PluginCore.IPlugins.IPlugin>(serviceProvider);
    /// 
    /// </summary>
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
        public async Task<ActionResult<BaseResponseModel>> PluginContexts()
        {
            BaseResponseModel responseModel = new BaseResponseModel();
            try
            {
                var pluginContextList = _pluginContextManager.All();
                Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();
                foreach (var pluginContext in pluginContextList)
                {
                    keyValuePairs.Add($"{pluginContext.GetType().ToString()} - {pluginContext.PluginId} - {pluginContext.GetHashCode()}", pluginContext.Assemblies.Select(m => m.FullName).ToList());
                }

                responseModel.Code = 1;
                responseModel.Message = "success";
                responseModel.Data = keyValuePairs;
            }
            catch (Exception ex)
            {
                responseModel.Code = -1;
                responseModel.Message = "error";
                responseModel.Data = ex.ToString();
            }

            return await Task.FromResult(responseModel);
        }

        [HttpGet, HttpPost]
        public async Task<ActionResult<BaseResponseModel>> AssemblyLoadContexts()
        {
            BaseResponseModel responseModel = new BaseResponseModel();
            try
            {
                var assemblyLoadContextDefault = AssemblyLoadContext.Default;
                var assemblyLoadContextAll = AssemblyLoadContext.All;
                var responseDataModel = new AssemblyLoadContextsResponseDataModel();
                responseDataModel.Default = new AssemblyLoadContextsResponseDataModel.AssemblyLoadContextModel
                {
                    Name = assemblyLoadContextDefault.Name,
                    Type = assemblyLoadContextDefault.GetType().ToString(),
                    Assemblies = assemblyLoadContextDefault.Assemblies.Select(m => new AssemblyModel { FullName = m.FullName, DefinedTypes = m.DefinedTypes.Select(m => m.FullName).ToList() }).ToList()
                };
                responseDataModel.All = assemblyLoadContextAll.Select(item => new AssemblyLoadContextsResponseDataModel.AssemblyLoadContextModel
                {
                    Name = item.Name,
                    Type = item.GetType().ToString(),
                    Assemblies = item.Assemblies.Select(m => new AssemblyModel { FullName = m.FullName, DefinedTypes = m.DefinedTypes.Select(m => m.FullName).ToList() }).ToList()
                }).ToList();

                responseModel.Code = 1;
                responseModel.Message = "success";
                responseModel.Data = responseDataModel;
            }
            catch (Exception ex)
            {
                responseModel.Code = -1;
                responseModel.Message = "error";
                responseModel.Data = ex.ToString();
            }

            return await Task.FromResult(responseModel);
        }

        [HttpGet, HttpPost]
        public async Task<ActionResult<BaseResponseModel>> Assemblies()
        {
            BaseResponseModel responseModel = new BaseResponseModel();
            try
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                List<AssemblyModel> assemblyModels = new List<AssemblyModel>();
                foreach (var item in assemblies)
                {
                    assemblyModels.Add(new AssemblyModel
                    {
                        FullName = item.FullName,
                        DefinedTypes = item.DefinedTypes.Select(m => m.FullName).ToList()
                    });
                }

                responseModel.Code = 1;
                responseModel.Message = "success";
                responseModel.Data = assemblyModels;
            }
            catch (Exception ex)
            {
                responseModel.Code = -1;
                responseModel.Message = "error";
                responseModel.Data = ex.ToString();
            }

            return await Task.FromResult(responseModel);
        }

        [HttpGet, HttpPost]
        public async Task<ActionResult<BaseResponseModel>> Services([FromServices] IServiceProvider serviceProvider)
        {
            BaseResponseModel responseModel = new BaseResponseModel();
            try
            {
                //IServiceProvider serviceProvider = HttpContext.RequestServices;
                //var provider = serviceProvider.GetType().GetProperty("RootProvider", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                //var serviceField = provider.GetType().GetField("_realizedServices", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                //var serviceValue = serviceField.GetValue(provider);
                //var funcType = serviceField.FieldType.GetGenericArguments()[1].GetGenericArguments()[0];
                //ConcurrentDictionary<Type, Func<ServiceProviderEngineScope, object?>> realizedServices = (ConcurrentDictionary<Type, Func<ServiceProviderEngineScope, object?>>)serviceValue;

                // 获取所有已经注册的服务
                var allService = serviceProvider.GetAllServiceDescriptors();

                List<ServiceModel> serviceModels = new List<ServiceModel>();
                foreach (var item in allService)
                {
                    serviceModels.Add(new ServiceModel
                    {
                        Type = item.Key.ToString(),
                        ImplementationType = item.Value.ImplementationType?.ToString() ?? "",
                        Lifetime = item.Value.Lifetime.ToString(),
                        TypeAssembly = new AssemblyModel
                        {
                            FullName = item.Key.Assembly.FullName,
                        },
                        ImplementationTypeAssembly = new AssemblyModel
                        {
                            FullName = item.Value.ImplementationType?.Assembly?.FullName ?? ""
                        }
                    });
                }

                responseModel.Code = 1;
                responseModel.Message = "success";
                responseModel.Data = serviceModels;
            }
            catch (Exception ex)
            {
                responseModel.Code = -1;
                responseModel.Message = "error";
                responseModel.Data = ex.ToString();
            }

            return await Task.FromResult(responseModel);
        }

        #endregion

        public sealed class AssemblyLoadContextsResponseDataModel
        {
            public AssemblyLoadContextModel Default
            {
                get; set;
            }

            public List<AssemblyLoadContextModel> All
            {
                get; set;
            }

            public sealed class AssemblyLoadContextModel
            {
                public string Name
                {
                    get; set;
                }
                public string Type
                {
                    get; set;
                }
                public List<AssemblyModel> Assemblies
                {
                    get; set;
                }
            }
        }

        public sealed class AssembliesResponseDataModel
        {

        }

        public sealed class ServiceModel
        {
            public string Type
            {
                get; set;
            }

            public string ImplementationType
            {
                get; set;
            }

            public string Lifetime
            {
                get; set;
            }

            public AssemblyModel TypeAssembly
            {
                get; set;
            }

            public AssemblyModel ImplementationTypeAssembly
            {
                get; set;
            }
        }

        public sealed class AssemblyModel
        {
            public string FullName
            {
                get; set;
            }

            public List<string> DefinedTypes
            {
                get; set;
            }
        }

    }
}
