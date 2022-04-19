//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PluginCore.Infrastructure
{
    public class NupkgService
    {
        /// <summary>
        /// 将 xxx.nupkg 根据当前环境 解压成 对应 插件目录结构
        /// xxx.zip 形式的插件安装包: 直接解压即可
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationDirectory"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool DecomparessFile(string sourceFile, string destinationDirectory = null)
        {
            bool isDecomparessSuccess = false;

            if (!File.Exists(sourceFile))
            {
                throw new FileNotFoundException("要解压的文件不存在", sourceFile);
            }

            if (string.IsNullOrWhiteSpace(destinationDirectory))
            {
                destinationDirectory = Path.GetDirectoryName(sourceFile);
            }

            try
            {
                // 注意: 之前重命名过: Guid.zip
                destinationDirectory = destinationDirectory.Replace(".zip", "");

                isDecomparessSuccess = Utils.ZipHelper.FastDecomparessFile(sourceFile, destinationDirectory);
                if (!isDecomparessSuccess)
                {
                    return isDecomparessSuccess;
                }
                // 到这里已经解压完成, 开始解析

                var netVersion = Utils.RuntimeUtil.RuntimeNetVersion;

                // netcoreapp3.1  net5.0
                // dll 文件等
                string libDirPath = Path.Combine(destinationDirectory, "lib");
                string netFolderName = string.Empty;
                if (netVersion >= new Version("3.1") && Directory.Exists(Path.Combine(libDirPath, $"netcoreapp{netVersion.Major}.{netVersion.Minor}")))
                {
                    netFolderName = $"netcoreapp{netVersion.Major}.{netVersion.Minor}";
                }
                else if (netVersion.Major >= 5 && Directory.Exists(Path.Combine(libDirPath, $"net{netVersion.Major}.{netVersion.Minor}")))
                {
                    netFolderName = $"net{netVersion.Major}.{netVersion.Minor}";
                }
                else if (Directory.Exists(Path.Combine(libDirPath, "netstandard2.0")))
                {
                    netFolderName = $"netstandard2.0";
                }
                else if (Directory.Exists(Path.Combine(libDirPath, "netstandard2.1")))
                {
                    netFolderName = $"netstandard2.1";
                }
                else
                {
                    throw new Exception("暂不支持 .NET Core 3.1 以下版本, 也不支持 .NET Framework ");
                }
                // 1. ./lib/netcoreapp3.1
                string libNetDirPath = Path.Combine(libDirPath, netFolderName);
                // 移动到插件根目录
                //Directory.Move(libNetDirPath, destinationDirectory); // 错误: 这样移动会导致 包含根目录文件夹名
                Utils.FileUtil.CopyFolder(libNetDirPath, destinationDirectory);
                // 只需要这些, 其他删除
                Directory.Delete(libDirPath, true);

                Utils.LogUtil.Info($"加载 nupkg 中 dll: {sourceFile} ; {libNetDirPath}");

                // 2. 普通文件: 例如 wwwroot
                // 2.1 ./content
                string contentDirPath = Path.Combine(destinationDirectory, "content");
                bool isExistContentDir = Directory.Exists(contentDirPath);
                bool isFinishedContent = false;
                if (isExistContentDir)
                {
                    DirectoryInfo dir = new DirectoryInfo(contentDirPath);
                    bool isExistFile = dir.GetFiles()?.Length >= 1 || dir.GetDirectories()?.Length >= 1;
                    if (isExistFile)
                    {
                        Utils.FileUtil.CopyFolder(contentDirPath, destinationDirectory);
                        isFinishedContent = true;

                        Utils.LogUtil.Info($"加载 nupkg 中 非dll: ./content : {sourceFile} ; {contentDirPath}");
                    }
                }
                else
                {
                    // 2.2 ./contentFiles/any/netFolderName
                    // 在 ./content 中没有找到文件, 再尝试 此文件夹
                    if (!isFinishedContent)
                    {
                        string contentFilesDirPath = Path.Combine(destinationDirectory, "contentFiles");
                        DirectoryInfo dir = new DirectoryInfo(contentFilesDirPath);
                        int? childDirLength = dir.GetDirectories()?.Length;
                        if (childDirLength >= 1)
                        {
                            DirectoryInfo anyDir = dir.GetDirectories()[0];
                            DirectoryInfo netFolderDir = anyDir.GetDirectories(netFolderName)?.FirstOrDefault();
                            if (netFolderDir != null)
                            {
                                Utils.FileUtil.CopyFolder(netFolderDir.FullName, destinationDirectory);

                                Utils.LogUtil.Info($"加载 nupkg 中 非dll: ./contentFiles/any/netFolderName : {sourceFile} ; {netFolderDir.FullName}");
                            }
                        }
                    }
                }





            }
            catch (Exception ex)
            {
                Utils.LogUtil.Error(ex.ToString());
                Utils.LogUtil.Error(ex.InnerException?.ToString() ?? "");

                throw ex;
            }

            return isDecomparessSuccess;
        }
    }
}
