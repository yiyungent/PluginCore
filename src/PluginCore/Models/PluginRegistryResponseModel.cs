using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.Models
{
    public class PluginRegistryResponseModel : PluginInfoModel
    {
        public string DownloadUrl { get; set; }

        /// <summary>
        /// 此属性值由获取后根据本地插件情况赋值
        /// </summary>
        public PluginStatus Status { get; set; }

        public enum PluginStatus
        {
            /// <summary>
            /// 本地无此 PluginId 的插件
            /// </summary>
            LocalWithout,

            /// <summary>
            /// 本地已存在此 PluginId 的插件
            /// </summary>
            LocalExist
        }
    }

    public class PluginRegistryDTO
    { 
        
    }
}
