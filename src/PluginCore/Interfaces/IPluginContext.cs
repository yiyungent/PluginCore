//===================================================
//  License: GNU LGPLv3
//  Contributors: yiyungent@gmail.com
//  Project: https://yiyungent.github.io/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace PluginCore.Interfaces
{
    /// <summary>
    /// 每个插件的所有 Assembly 打包到此中
    /// </summary>
    public interface IPluginContext
    {
        string PluginId { get; }

        IEnumerable<Assembly> Assemblies { get; }

        Assembly LoadFromAssemblyName(AssemblyName assemblyName);

        void Unload();


        /// <summary>
        /// 暂时用不到
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        Assembly LoadFromStream(Stream assembly);
    }
}


