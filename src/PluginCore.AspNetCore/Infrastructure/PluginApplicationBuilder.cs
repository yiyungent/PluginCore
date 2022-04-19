//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Internal;

namespace PluginCore.AspNetCore.Infrastructure
{
    /// <summary>
    /// Default implementation for <see cref="IApplicationBuilder"/>.
    ///
    /// https://github.com/dotnet/aspnetcore/blob/main/src/Http/Http/src/Builder/ApplicationBuilder.cs
    /// </summary>
    public class PluginApplicationBuilder : IApplicationBuilder
    {
        private const string ServerFeaturesKey = "server.Features";
        private const string ApplicationServicesKey = "application.Services";

        private readonly List<Func<RequestDelegate, RequestDelegate>> _components = new List<Func<RequestDelegate, RequestDelegate>>();

        private Action _reachEndAction;

        public Action ReachEndAction
        {
            get
            {
                return this._reachEndAction;
            }
            set
            {
                this._reachEndAction = value;
            }
        }

        public PluginApplicationBuilder()
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationBuilder"/>.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> for application services.</param>
        public PluginApplicationBuilder(IServiceProvider serviceProvider, Action reachEndAction)
        {
            Properties = new Dictionary<string, object?>(StringComparer.Ordinal);
            ApplicationServices = serviceProvider;

            _reachEndAction = reachEndAction;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationBuilder"/>.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> for application services.</param>
        /// <param name="server">The server instance that hosts the application.</param>
        public PluginApplicationBuilder(IServiceProvider serviceProvider, Action reachEndAction, object server)
            : this(serviceProvider, reachEndAction)
        {
            SetProperty(ServerFeaturesKey, server);
        }

        private PluginApplicationBuilder(PluginApplicationBuilder builder)
        {
            Properties = new CopyOnWriteDictionary<string, object?>(builder.Properties, StringComparer.Ordinal);
        }

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> for application services.
        /// </summary>
        public IServiceProvider ApplicationServices
        {
            get
            {
                //return GetProperty<IServiceProvider>(ApplicationServicesKey)!;
                return null;
            }
            set
            {
                SetProperty<IServiceProvider>(ApplicationServicesKey, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="IFeatureCollection"/> for server features.
        /// </summary>
        public IFeatureCollection ServerFeatures
        {
            get
            {
                // ! （null 包容）运算符（C# 参考）
                // https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/operators/null-forgiving
                // TODO: 报错
                //var result = GetProperty<IFeatureCollection>(ServerFeaturesKey)!;

                IFeatureCollection rtn = null;

                return rtn;
            }
        }

        /// <summary>
        /// Gets a set of properties for <see cref="ApplicationBuilder"/>.
        /// </summary>
        public IDictionary<string, object?> Properties { get; }

        //private T? GetProperty<T>(string key)
        //{

        //    return Properties.TryGetValue(key, out var value) ? (T?)value : default(T);
        //}

        private void SetProperty<T>(string key, T value)
        {
            Properties[key] = value;
        }

        /// <summary>
        /// Adds the middleware to the application request pipeline.
        /// </summary>
        /// <param name="middleware">The middleware.</param>
        /// <returns>An instance of <see cref="IApplicationBuilder"/> after the operation has completed.</returns>
        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _components.Add(middleware);
            return this;
        }

        /// <summary>
        /// Creates a copy of this application builder.
        /// <para>
        /// The created clone has the same properties as the current instance, but does not copy
        /// the request pipeline.
        /// </para>
        /// </summary>
        /// <returns>The cloned instance.</returns>
        public IApplicationBuilder New()
        {
            return new PluginApplicationBuilder(this);
        }

        /// <summary>
        /// Produces a <see cref="RequestDelegate"/> that executes added middlewares.
        /// </summary>
        /// <returns>The <see cref="RequestDelegate"/>.</returns>
        public RequestDelegate Build()
        {
            RequestDelegate app = context =>
            {
                #region Old
                // If we reach the end of the pipeline, but we have an endpoint, then something unexpected has happened.
                // This could happen if user code sets an endpoint, but they forgot to add the UseEndpoint middleware.
                //var endpoint = context.GetEndpoint();
                //var endpointRequestDelegate = endpoint?.RequestDelegate;
                //if (endpointRequestDelegate != null)
                //{
                //    var message =
                //        $"The request reached the end of the pipeline without executing the endpoint: '{endpoint!.DisplayName}'. " +
                //        $"Please register the EndpointMiddleware using '{nameof(IApplicationBuilder)}.UseEndpoints(...)' if using " +
                //        $"routing.";
                //    throw new InvalidOperationException(message);
                //} 
                #endregion

                this._reachEndAction();

                //context.Response.StatusCode = StatusCodes.Status404NotFound;
                return Task.CompletedTask;
            };

            for (var c = _components.Count - 1; c >= 0; c--)
            {
                app = _components[c](app);
            }

            return app;
        }
    }



    /// <summary>
    /// https://github.com/dotnet/aspnetcore/blob/8b30d862de6c9146f466061d51aa3f1414ee2337/src/Shared/CopyOnWriteDictionary/CopyOnWriteDictionary.cs
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    internal class CopyOnWriteDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : notnull
    {
        private readonly IDictionary<TKey, TValue> _sourceDictionary;
        private readonly IEqualityComparer<TKey> _comparer;
        private IDictionary<TKey, TValue>? _innerDictionary;

        public CopyOnWriteDictionary(
            IDictionary<TKey, TValue> sourceDictionary,
            IEqualityComparer<TKey> comparer)
        {
            if (sourceDictionary == null)
            {
                throw new ArgumentNullException(nameof(sourceDictionary));
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            _sourceDictionary = sourceDictionary;
            _comparer = comparer;
        }

        private IDictionary<TKey, TValue> ReadDictionary
        {
            get
            {
                return _innerDictionary ?? _sourceDictionary;
            }
        }

        private IDictionary<TKey, TValue> WriteDictionary
        {
            get
            {
                if (_innerDictionary == null)
                {
                    _innerDictionary = new Dictionary<TKey, TValue>(_sourceDictionary,
                                                                    _comparer);
                }

                return _innerDictionary;
            }
        }

        public virtual ICollection<TKey> Keys
        {
            get
            {
                return ReadDictionary.Keys;
            }
        }

        public virtual ICollection<TValue> Values
        {
            get
            {
                return ReadDictionary.Values;
            }
        }

        public virtual int Count
        {
            get
            {
                return ReadDictionary.Count;
            }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public virtual TValue this[TKey key]
        {
            get
            {
                return ReadDictionary[key];
            }
            set
            {
                WriteDictionary[key] = value;
            }
        }

        public virtual bool ContainsKey(TKey key)
        {
            return ReadDictionary.ContainsKey(key);
        }

        public virtual void Add(TKey key, TValue value)
        {
            WriteDictionary.Add(key, value);
        }

        public virtual bool Remove(TKey key)
        {
            return WriteDictionary.Remove(key);
        }

        public virtual bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return ReadDictionary.TryGetValue(key, out value);
        }

        public virtual void Add(KeyValuePair<TKey, TValue> item)
        {
            WriteDictionary.Add(item);
        }

        public virtual void Clear()
        {
            WriteDictionary.Clear();
        }

        public virtual bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ReadDictionary.Contains(item);
        }

        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ReadDictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return WriteDictionary.Remove(item);
        }

        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ReadDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }



}
