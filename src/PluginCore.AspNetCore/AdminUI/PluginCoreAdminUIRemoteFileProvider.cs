//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace PluginCore.AspNetCore.AdminUI
{
    public class PluginCoreAdminUIRemoteFileProvider : IFileProvider
    {
        protected string RootUrl { get; set; }

        public PluginCoreAdminUIRemoteFileProvider(string rootUrl)
        {
            this.RootUrl = rootUrl;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return (IDirectoryContents)NotFoundDirectoryContents.Singleton;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            if (string.IsNullOrEmpty(subpath))
                return (IFileInfo)new NotFoundFileInfo(subpath);

            IFileInfo fileInfo = new PluginCoreAdminUIFileInfo(this.RootUrl, subpath);

            return fileInfo;
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }


        public class PluginCoreAdminUIFileInfo : IFileInfo
        {
            protected string RootUrl { get; set; }

            protected string SubPath { get; set; }

            private string _name;

            public PluginCoreAdminUIFileInfo(string rootUrl, string subpath)
            {
                this.RootUrl = rootUrl;
                this.SubPath = subpath;
                this._name = this.SubPath.Substring(this.SubPath.LastIndexOf("/") + 1);
            }

            public Stream CreateReadStream()
            {
                HttpClient httpClient = new HttpClient();

                return httpClient.GetStreamAsync($"{this.RootUrl}/{this.SubPath}").Result;
            }

            public bool Exists
            {
                get
                {
                    bool isExist = false;
                    if (this.Name == "index.html")
                    {
                        isExist = true;
                    }

                    return isExist;
                }
            }
            public bool IsDirectory
            {
                get
                {
                    return false;
                }
            }

            public DateTimeOffset LastModified
            {
                get
                {
                    return new DateTimeOffset(DateTime.Now);
                }
            }

            public long Length
            {
                get
                {
                    return 111;
                }
            }

            public string Name
            {
                get
                {
                    return this._name;
                }
            }

            public string PhysicalPath
            {
                get
                {
                    return "";
                }
            }
        }

    }



}
