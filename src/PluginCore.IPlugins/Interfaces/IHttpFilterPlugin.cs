using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PluginCore.IPlugins
{
    public interface IHttpFilterPlugin : IPlugin
    {
        Task HttpEndFilter(HttpContext httpContext);

        Task HttpStartFilter(HttpContext httpContext);
    }
}
