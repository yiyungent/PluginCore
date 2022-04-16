using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.AspNetCore.Interfaces
{
    public interface IPluginApplicationBuilderManager
    {
        void ReBuild();

        RequestDelegate GetBuildResult();
    }
}
