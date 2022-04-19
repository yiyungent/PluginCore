//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PluginCore.IPlugins;

namespace WebSocketDemoPlugin
{
    public class WebSocketDemoPlugin : BasePlugin, /*IStartupXPlugin,*/ IStartupPlugin
    {
        public override (bool IsSuccess, string Message) AfterEnable()
        {
            Console.WriteLine($"{nameof(WebSocketDemoPlugin)}: {nameof(AfterEnable)}");
            return base.AfterEnable();
        }

        public override (bool IsSuccess, string Message) BeforeDisable()
        {
            Console.WriteLine($"{nameof(WebSocketDemoPlugin)}: {nameof(BeforeDisable)}");
            return base.BeforeDisable();
        }

        public void ConfigureServices(IServiceCollection services)
        {

        }

        public void Configure(IApplicationBuilder app)
        {
            // 1. 首先必须 添加 WebSocketMiddleware
            var webSocketOptions = new WebSocketOptions()
            {
                // KeepAliveInterval - 向客户端发送 "ping" 帧的频率，以确保代理保持连接处于打开状态。 默认值为 2 分钟。
                KeepAliveInterval = TimeSpan.FromSeconds(120),
            };
            app.UseWebSockets(webSocketOptions);


            #region 接受 WebSocket 请求: 方式2: 通过 Middleware
            //app.Use(async (context, next) =>
            //    {
            //        if (context.Request.Path == "/plugins/ws")
            //        {
            //            if (context.WebSockets.IsWebSocketRequest)
            //            {
            //                using (WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
            //                {
            //                    await Echo(context, webSocket);
            //                }
            //            }
            //            else
            //            {
            //                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //            }
            //        }
            //        else
            //        {
            //            await next();
            //        }

            //    }); 
            #endregion
        }

        #region 方式2: 通过 Middleware
        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
        #endregion

        public int ConfigureOrder
        {
            get
            {
                return 2;
            }
        }

        public int ConfigureServicesOrder
        {
            get
            {
                return 2;
            }
        }
    }
}
