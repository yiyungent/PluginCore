using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PluginCore.IPlugins
{
    public interface IContentFilterPlugin : IPlugin
    {
        Task<string> RequestBodyFilter(string urlPath, string content);

        Task<string> ReponseBodyFilter(string urlPath, string content);
    }
}
