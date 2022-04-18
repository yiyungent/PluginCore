//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PluginCore.Registry.Models.NuGet
{
    public class NuGetPackageItemModel
    {
        public string PackageIcon { get; set; }

        public string PackageId { get; set; }

        public string PackageVersion { get; set; }

        public IList<string> PackageBy { get; set; }

        public string PackageDetail { get; set; }

        public PackageStatModel PackageStat { get; set; }

        public NuGetPackageItemModel()
        {
            this.PackageBy = new List<string>();
            this.PackageStat = new PackageStatModel();
            this.PackageStat.Tags = new List<string>();
        }


        public sealed class PackageStatModel
        {
            public int DownloadCount { get; set; }

            public string LastUpdatedText { get; set; }

            //public DateTime LastUpdated { get; set; }

            /// <summary>
            /// 10位 Unix 时间戳
            /// </summary>
            public long LastUpdated { get; set; }

            public IList<string> Tags { get; set; }
        }

    }
}
