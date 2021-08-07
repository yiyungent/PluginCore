using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.Models
{
    /// <summary>
    /// 插件信息模型
    /// <para>对应插件目录下 info.json</para>
    /// <para>约定: 插件文件夹名=PluginID</para>
    /// <para>约定: 插件文件夹名=插件主程序集(Assembly)名 .dll</para>
    /// <para>eg: plugins/payment  payment.dll</para>
    /// <para>约定: 插件文件夹下 logo.png 为插件图标</para>
    /// <para>约定: 插件文件夹下 README.md 为插件说明文件</para>
    /// <para>约定: 插件文件夹下 settings.json 为插件设置文件</para>
    /// </summary>
    public class PluginInfoModel
    {
        public string PluginId { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string Version { get; set; }

        public IList<string> SupportedVersions { get; set; }

        #region Ctor
        public PluginInfoModel()
        {
            this.SupportedVersions = new List<string>();
        }
        #endregion
    }
}
