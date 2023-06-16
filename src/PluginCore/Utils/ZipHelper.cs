//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;

namespace PluginCore.Utils
{
    public class ZipHelper
    {
        /// <summary>
        /// 缓存字节数
        /// </summary>
        private const int BufferSize = 4096;

        /// <summary>
        /// 压缩最小等级
        /// </summary>
        public const int CompressionLevelMin = 0;

        /// <summary>
        /// 压缩最大等级
        /// </summary>
        public const int CompressionLevelMax = 9;

        /// <summary>
        /// 获取所有文件系统对象
        /// </summary>
        /// <param name="source">源路径</param>
        /// <param name="topDirectory">顶级文件夹</param>
        /// <returns>字典中Key为完整路径，Value为文件(夹)名称</returns>
        private static Dictionary<string, string> GetAllFileSystemEntities(string source, string topDirectory)
        {
            Dictionary<string, string> entitiesDictionary = new Dictionary<string, string>();
            entitiesDictionary.Add(source, source.Replace(topDirectory, ""));

            if (Directory.Exists(source))
            {
                //一次性获取下级所有目录，避免递归
                string[] directories = Directory.GetDirectories(source, "*.*", SearchOption.AllDirectories);
                foreach (string directory in directories)
                {
                    entitiesDictionary.Add(directory, directory.Replace(topDirectory, ""));
                }

                string[] files = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    entitiesDictionary.Add(file, file.Replace(topDirectory, ""));
                }
            }

            return entitiesDictionary;
        }

        /// <summary>
        /// 校验压缩等级
        /// </summary>
        /// <param name="compressionLevel"></param>
        /// <returns></returns>
        private static int CheckCompressionLevel(int compressionLevel)
        {
            compressionLevel = compressionLevel < CompressionLevelMin ? CompressionLevelMin : compressionLevel;
            compressionLevel = compressionLevel > CompressionLevelMax ? CompressionLevelMax : compressionLevel;
            return compressionLevel;
        }

        #region 字节压缩与解压

        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="sourceBytes">源字节数组</param>
        /// <param name="compressionLevel">压缩等级</param>
        /// <param name="password">密码</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] CompressBytes(byte[] sourceBytes, string password = null, int compressionLevel = 6)
        {
            byte[] result = new byte[] { };

            if (sourceBytes.Length > 0)
            {
                try
                {
                    using (MemoryStream tempStream = new MemoryStream())
                    {
                        using (MemoryStream readStream = new MemoryStream(sourceBytes))
                        {
                            using (ZipOutputStream zipStream = new ZipOutputStream(tempStream))
                            {
                                zipStream.Password = password;//设置密码
                                zipStream.SetLevel(CheckCompressionLevel(compressionLevel));//设置压缩等级

                                ZipEntry zipEntry = new ZipEntry("ZipBytes");
                                zipEntry.DateTime = DateTime.Now;
                                zipEntry.Size = sourceBytes.Length;
                                zipStream.PutNextEntry(zipEntry);
                                int readLength = 0;
                                byte[] buffer = new byte[BufferSize];

                                do
                                {
                                    readLength = readStream.Read(buffer, 0, BufferSize);
                                    zipStream.Write(buffer, 0, readLength);
                                } while (readLength == BufferSize);

                                readStream.Close();
                                zipStream.Flush();
                                zipStream.Finish();
                                result = tempStream.ToArray();
                                zipStream.Close();
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    throw new Exception("压缩字节数组发生错误", ex);
                }
            }

            return result;
        }

        /// <summary>
        /// 解压字节数组
        /// </summary>
        /// <param name="sourceBytes">源字节数组</param>
        /// <param name="password">密码</param>
        /// <returns>解压后的字节数组</returns>
        public static byte[] DecompressBytes(byte[] sourceBytes, string password = null)
        {
            byte[] result = new byte[] { };

            if (sourceBytes.Length > 0)
            {
                try
                {
                    using (MemoryStream tempStream = new MemoryStream(sourceBytes))
                    {
                        using (MemoryStream writeStream = new MemoryStream())
                        {
                            using (ZipInputStream zipStream = new ZipInputStream(tempStream))
                            {
                                zipStream.Password = password;
                                ZipEntry zipEntry = zipStream.GetNextEntry();

                                if (zipEntry != null)
                                {
                                    byte[] buffer = new byte[BufferSize];
                                    int readLength = 0;

                                    do
                                    {
                                        readLength = zipStream.Read(buffer, 0, BufferSize);
                                        writeStream.Write(buffer, 0, readLength);
                                    } while (readLength == BufferSize);

                                    writeStream.Flush();
                                    result = writeStream.ToArray();
                                    writeStream.Close();
                                }
                                zipStream.Close();
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    throw new Exception("解压字节数组发生错误", ex);
                }
            }
            return result;
        }

        #endregion

        #region 文件压缩与解压

        /// <summary>
        /// 为压缩准备文件系统对象
        /// </summary>
        /// <param name="sourceFileEntityPathList"></param>
        /// <returns></returns>
        private static Dictionary<string, string> PrepareFileSystementities(IEnumerable<string> sourceFileEntityPathList)
        {
            Dictionary<string, string> fileEntityDictionary = new Dictionary<string, string>();//文件字典
            string parentDirectoryPath = "";
            foreach (string fileEntityPath in sourceFileEntityPathList)
            {
                string path = fileEntityPath;
                //保证传入的文件夹也被压缩进文件
                if (path.EndsWith(@"\"))
                {
                    path = path.Remove(path.LastIndexOf(@"\"));
                }

                parentDirectoryPath = Path.GetDirectoryName(path) + @"\";

                if (parentDirectoryPath.EndsWith(@":\\"))//防止根目录下把盘符压入的错误
                {
                    parentDirectoryPath = parentDirectoryPath.Replace(@"\\", @"\");
                }

                //获取目录中所有的文件系统对象
                Dictionary<string, string> subDictionary = GetAllFileSystemEntities(path, parentDirectoryPath);

                //将文件系统对象添加到总的文件字典中
                foreach (string key in subDictionary.Keys)
                {
                    if (!fileEntityDictionary.ContainsKey(key))//检测重复项
                    {
                        fileEntityDictionary.Add(key, subDictionary[key]);
                    }
                }
            }
            return fileEntityDictionary;
        }

        /// <summary>
        /// 压缩单个文件/文件夹
        /// </summary>
        /// <param name="sourceList">源文件/文件夹路径列表</param>
        /// <param name="zipFilePath">压缩文件路径</param>
        /// <param name="comment">注释信息</param>
        /// <param name="password">压缩密码</param>
        /// <param name="compressionLevel">压缩等级，范围从0到9，可选，默认为6</param>
        /// <returns></returns>
        public static bool CompressFile(string path, string zipFilePath,
            string comment = null, string password = null, int compressionLevel = 6)
        {
            return CompressFile(new string[] { path }, zipFilePath, comment, password, compressionLevel);
        }

        /// <summary>
        /// 压缩多个文件/文件夹
        /// </summary>
        /// <param name="sourceList">源文件/文件夹路径列表</param>
        /// <param name="zipFilePath">压缩文件路径</param>
        /// <param name="comment">注释信息</param>
        /// <param name="password">压缩密码</param>
        /// <param name="compressionLevel">压缩等级，范围从0到9，可选，默认为6</param>
        /// <returns></returns>
        public static bool CompressFile(IEnumerable<string> sourceList, string zipFilePath,
             string comment = null, string password = null, int compressionLevel = 6)
        {
            bool result = false;

            try
            {
                //检测目标文件所属的文件夹是否存在，如果不存在则建立
                string zipFileDirectory = Path.GetDirectoryName(zipFilePath);
                if (!Directory.Exists(zipFileDirectory))
                {
                    Directory.CreateDirectory(zipFileDirectory);
                }

                Dictionary<string, string> dictionaryList = PrepareFileSystementities(sourceList);

                using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    zipStream.Password = password;//设置密码
                    zipStream.SetComment(comment);//添加注释
                    zipStream.SetLevel(CheckCompressionLevel(compressionLevel));//设置压缩等级

                    foreach (string key in dictionaryList.Keys)//从字典取文件添加到压缩文件
                    {
                        if (File.Exists(key))//判断是文件还是文件夹
                        {
                            FileInfo fileItem = new FileInfo(key);

                            using (FileStream readStream = fileItem.Open(FileMode.Open,
                                FileAccess.Read, FileShare.Read))
                            {
                                ZipEntry zipEntry = new ZipEntry(dictionaryList[key]);
                                zipEntry.DateTime = fileItem.LastWriteTime;
                                zipEntry.Size = readStream.Length;
                                zipStream.PutNextEntry(zipEntry);
                                int readLength = 0;
                                byte[] buffer = new byte[BufferSize];

                                do
                                {
                                    readLength = readStream.Read(buffer, 0, BufferSize);
                                    zipStream.Write(buffer, 0, readLength);
                                } while (readLength == BufferSize);

                                readStream.Close();
                            }
                        }
                        else//对文件夹的处理
                        {
                            ZipEntry zipEntry = new ZipEntry(dictionaryList[key] + "/");
                            zipStream.PutNextEntry(zipEntry);
                        }
                    }

                    zipStream.Flush();
                    zipStream.Finish();
                    zipStream.Close();
                }

                result = true;
            }
            catch (System.Exception ex)
            {
                throw new Exception("压缩文件失败", ex);
            }

            return result;
        }

        /// <summary>
        /// 解压文件到指定文件夹
        /// </summary>
        /// <param name="sourceFile">压缩文件</param>
        /// <param name="destinationDirectory">目标文件夹，如果为空则解压到当前文件夹下</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static bool DecomparessFile(string sourceFile, string destinationDirectory = null, string password = null)
        {
            bool result = false;

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
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                using (ZipInputStream zipStream = new ZipInputStream(File.Open(sourceFile, FileMode.Open,
                    FileAccess.Read, FileShare.Read)))
                {
                    zipStream.Password = password;
                    ZipEntry zipEntry = zipStream.GetNextEntry();

                    while (zipEntry != null)
                    {
                        if (zipEntry.IsDirectory)//如果是文件夹则创建
                        {
                            Directory.CreateDirectory(Path.Combine(destinationDirectory,
                                Path.GetDirectoryName(zipEntry.Name)));
                        }
                        else
                        {
                            string fileName = Path.GetFileName(zipEntry.Name);
                            if (!string.IsNullOrEmpty(fileName) && fileName.Trim().Length > 0)
                            {
                                FileInfo fileItem = new FileInfo(Path.Combine(destinationDirectory, zipEntry.Name));
                                if (fileItem.Directory is null)
                                    continue;
                                if (!Directory.Exists(fileItem.Directory.FullName))
                                    Directory.CreateDirectory(fileItem.Directory.FullName);
                                using (FileStream writeStream = fileItem.Create())
                                {
                                    byte[] buffer = new byte[BufferSize];
                                    int readLength = 0;

                                    do
                                    {
                                        readLength = zipStream.Read(buffer, 0, BufferSize);
                                        writeStream.Write(buffer, 0, readLength);
                                    } while (readLength == BufferSize);

                                    writeStream.Flush();
                                    writeStream.Close();
                                }
                                fileItem.LastWriteTime = zipEntry.DateTime;
                            }
                        }
                        zipEntry = zipStream.GetNextEntry();//获取下一个文件
                    }

                    zipStream.Close();
                }
                result = true;
            }
            catch (System.Exception ex)
            {
                throw new Exception("文件解压发生错误", ex);
            }

            return result;
        }


        public static bool FastDecomparessFile(string sourceFile, string destinationDirectory = null)
        {
            bool result = false;

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
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                FastZip fastZip = new FastZip();
                string fileFilter = null;

                fastZip.ExtractZip(sourceFile, destinationDirectory, fileFilter);

                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("文件解压发生错误", ex);
            }

            return result;
        }

        #endregion
    }
}
