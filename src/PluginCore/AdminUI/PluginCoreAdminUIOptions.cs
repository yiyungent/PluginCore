using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace PluginCore.AdminUI
{
    public class PluginCoreAdminUIOptions
    {
        /// <summary>
        /// Gets or sets a route prefix for accessing the swagger-ui
        /// </summary>
        public string RoutePrefix { get; set; } = "PluginCore/Admin";

        /// <summary>
        /// Gets or sets a Stream function for retrieving the swagger-ui page
        /// </summary>
        public Func<Stream> IndexStream
        {
            get
            {
                Func<Stream> funcStream = null;
                ;
                Config.PluginCoreConfig pluginCoreConfig = Config.PluginCoreConfigFactory.Create();
                switch (pluginCoreConfig.FrontendMode?.ToLower())
                {
                    case "localembedded":
                        funcStream = () => typeof(PluginCoreAdminUIOptions).GetTypeInfo().Assembly
                            .GetManifestResourceStream("PluginCore.PluginCoreAdmin.index.html");
                        break;
                    case "localfolder":
                        string absoluteRootPath = PluginPathProvider.PluginCoreAdminDir();
                        string indexFilePath = Path.Combine(absoluteRootPath, "index.html");

                        funcStream = () => (Stream)new FileStream(indexFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1, FileOptions.Asynchronous | FileOptions.SequentialScan);
                        break;
                    case "remotecdn":
                        string remoteFrontendRootPath = pluginCoreConfig.RemoteFrontend;
                        string indexFileRemotePath = remoteFrontendRootPath + "/" + "index.html";

                        funcStream = () => new HttpClient().GetStreamAsync(indexFileRemotePath).Result;
                        break;
                    default:
                        funcStream = () => typeof(PluginCoreAdminUIOptions).GetTypeInfo().Assembly
                             .GetManifestResourceStream("PluginCore.PluginCoreAdmin.index.html");
                        break;
                }

                return funcStream;
            }
        }


    }
}
