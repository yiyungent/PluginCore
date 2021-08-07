using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.Models
{
    /// <summary>
    /// 所有插件的配置信息模型
    /// <para>对应 WebApi/App_Data/plugin.config.json</para>
    /// <para>-------------</para>
    /// <para>已安装的插件才在 Assemblies 中</para>
    /// <para>上传(添加)插件: 1.将临时上传目录下的插件.zip 移动到 plugins 2.解压插件.zip 3.加入<see cref="UninstalledPlugins"/></para>
    /// <para>安装插件: 1.加入<see cref="DisabledPlugins"/> 2.Assembly.Load()插件目录下的所有Assembly</para>
    /// <para>卸载插件: 1.加入<see cref="UninstalledPlugins"/> 2.重启应用程序域, 重新加载所有在<see cref="EnabledPlugins"/>和<see cref="DisabledPlugins"/>中的插件Assemblies</para>
    /// <para>删除插件: 从磁盘中删除plugins目录下的对应插件文件夹</para>
    /// </summary>
    public class PluginConfigModel
    {
        /// <summary>
        /// 启用的插件列表: PluginID
        /// <para>属于 插件 已安装</para>
        /// </summary>
        public IList<string> EnabledPlugins { get; set; }

        /// <summary>
        /// 禁用的插件列表: PluginID
        /// <para>属于 插件 已安装</para>
        /// </summary>
        public IList<string> DisabledPlugins { get; set; }

        /// <summary>
        /// 卸载的插件列表: PluginID
        /// </summary>
        public IList<string> UninstalledPlugins { get; set; }

        #region ctor
        public PluginConfigModel()
        {
            this.EnabledPlugins = new List<string>();
            this.DisabledPlugins = new List<string>();
            this.UninstalledPlugins = new List<string>();
        }
        #endregion
    }
}
