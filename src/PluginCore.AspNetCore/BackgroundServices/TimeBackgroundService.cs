//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PluginCore.AspNetCore.BackgroundServices
{
    public abstract class TimeBackgroundService : IHostedService, IDisposable
    {
        protected Timer _timer;
        protected TimeSpan _timerPeriod;

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, _timerPeriod);

            return Task.CompletedTask;
        }

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        protected abstract void DoWork(object state);

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
