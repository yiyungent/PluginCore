//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;


namespace WebSocketDemoPlugin.Controllers
{
    /// <summary>
    /// 接受 WebSocket 请求: 方式1: 通过 Controller
    /// </summary>
    public class WebSocketController : ControllerBase
    {
        [HttpGet("/plugins/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using WebSocket webSocket = await
                    HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(HttpContext, webSocket);

                //using (WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync())
                //{
                //    await Echo(HttpContext, webSocket);
                //}
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        private async Task Echo(HttpContext httpContext, WebSocket webSocket)
        {
            var receiveBuffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                byte[] myBuffer = System.Text.Encoding.UTF8.GetBytes("哈哈哈哈!");
                await webSocket.SendAsync(new ArraySegment<byte>(myBuffer), WebSocketMessageType.Text, false, CancellationToken.None);

                
                await webSocket.SendAsync(new ArraySegment<byte>(receiveBuffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                // 继续接收
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }




    }
}
