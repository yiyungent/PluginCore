//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketDemoPlugin.Infrastructure
{
    public class WebSocketConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public int Count
        {
            get
            {
                return _sockets.Count;
            }
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return _sockets;
        }

        public WebSocket GetWebSocket(string key)
        {
            WebSocket _socket;
            _sockets.TryGetValue(key, out _socket);

            return _socket;
        }

        public string GetKey(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }

        public void AddSocket(WebSocket socket, string key)
        {
            if (GetWebSocket(key) != null)
            {
                _sockets.TryRemove(key, out WebSocket destoryWebsocket);
            }
            _sockets.TryAdd(key, socket);
        }

        public async Task RemoveSocket(string key)
        {
            try
            {
                WebSocket socket;

                _sockets.TryRemove(key, out socket);

                await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
            }
            catch (Exception)
            {

            }
        }

        public async Task CloseSocket(WebSocket socket)
        {
            await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
        }
    }
}
