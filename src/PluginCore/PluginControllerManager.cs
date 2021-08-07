using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PluginCore
{
    public class PluginControllerManager
    {
        private readonly ApplicationPartManager _applicationPartManager;

        public PluginControllerManager(ApplicationPartManager applicationPartManager)
        {
            _applicationPartManager = applicationPartManager;
        }

        /// <summary>
        /// 从指定<see cref="Assembly"/> 中获取 Controller, 并添加进来
        /// </summary>
        /// <param name="assembly"></param>
        public void AddControllers(Assembly assembly)
        {
            AssemblyPart assemblyPart = new AssemblyPart(assembly);
            _applicationPartManager.ApplicationParts.Add(assemblyPart);

            ResetControllActions();
        }

        public void RemoveControllers(string pluginId)
        {
            ApplicationPart last = _applicationPartManager.ApplicationParts.First(m => m.Name == pluginId);
            _applicationPartManager.ApplicationParts.Remove(last);

            ResetControllActions();
        }

        /// <summary>
        /// 通知应用(主程序)Controller.Action 已发生变化
        /// </summary>
        private void ResetControllActions()
        {
            PluginActionDescriptorChangeProvider.Instance.HasChanged = true;
            // TokenSource 为 null
            // 注意: 在程序刚启动时, 未抵达 Controller 时不会触发 IActionDescriptorChangeProvider.GetChangeToken(), 也就会导致 TokenSource 为 null, 在启动时，同时在启动时，插件Controller.Action和主程序一起被添加，此时无需通知改变
            if (PluginActionDescriptorChangeProvider.Instance.TokenSource != null)
            {
                // 只有在插件列表控制启用，禁用时才需通知改变
                PluginActionDescriptorChangeProvider.Instance.TokenSource.Cancel();
            }
        }
    }
}
