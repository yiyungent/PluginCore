//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================


using System.Threading;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;

namespace PluginCore.AspNetCore.Implements
{
    /// <summary>
    /// 目前采用的第一种方案
    /// 方案一: https://www.codetd.com/article/461093
    /// 方案二: https://www.cnblogs.com/artech/p/dynamic-controllers.html
    /// </summary>
    public class PluginActionDescriptorChangeProvider : IActionDescriptorChangeProvider
    {
        public static PluginActionDescriptorChangeProvider Instance { get; } = new PluginActionDescriptorChangeProvider();

        public CancellationTokenSource TokenSource { get; private set; }

        public bool HasChanged { get; set; }

        public IChangeToken GetChangeToken()
        {
            TokenSource = new CancellationTokenSource();
            return new CancellationChangeToken(TokenSource.Token);
        }
    }
}
