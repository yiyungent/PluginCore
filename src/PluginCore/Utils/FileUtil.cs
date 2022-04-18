//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PluginCore.Utils
{
    public class FileUtil
    {
        #region 复制大文件
        public static void CopyBigFile(string originalFilePath, string destFilePath)
        {
            // 定义读文件流
            using (FileStream fsr = new FileStream(originalFilePath, FileMode.Open))
            {
                // 定义写文件流
                using (FileStream fsw = new FileStream(destFilePath, FileMode.OpenOrCreate))
                {
                    // 申请1M内存空间
                    byte[] buffer = new byte[1024 * 1024];
                    // 无限循环中反复读写，直到读完写完
                    while (true)
                    {
                        int readCount = fsr.Read(buffer, 0, buffer.Length);
                        fsw.Write(buffer, 0, readCount);
                        if (readCount < buffer.Length)
                        {
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        #region 移动大文件
        public static void MoveBigFile(string originalFilePath, string destFilePath)
        {
            try
            {
                CopyBigFile(originalFilePath, destFilePath);
                File.Delete(originalFilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 复制文件夹 
        /// <summary>
        /// 复制文件夹及文件
        /// (不包括源文件夹根目录名称, 只是复制其中内容到目标文件夹)
        /// https://www.cnblogs.com/wangjianhui008/p/3234519.html
        /// </summary>
        /// <param name="sourceFolder">原文件路径</param>
        /// <param name="destFolder">目标文件路径</param>
        /// <returns></returns>
        public static bool CopyFolder(string sourceFolder, string destFolder)
        {
            try
            {
                // 如果目标路径不存在,则创建目标路径
                if (!System.IO.Directory.Exists(destFolder))
                {
                    System.IO.Directory.CreateDirectory(destFolder);
                }
                // 得到原文件根目录下的所有文件
                string[] files = System.IO.Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    string name = System.IO.Path.GetFileName(file);
                    string dest = System.IO.Path.Combine(destFolder, name);
                    System.IO.File.Copy(file, dest);//复制文件
                }
                // 得到原文件根目录下的所有文件夹
                string[] folders = System.IO.Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders)
                {
                    string name = System.IO.Path.GetFileName(folder);
                    string dest = System.IO.Path.Combine(destFolder, name);
                    CopyFolder(folder, dest);//构建目标路径,递归复制文件
                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }

        }
        #endregion

        #region 计算MD5


        /// <summary>
        /// 获取文件的MD5码
        /// </summary>
        /// <param name="fileName">传入的文件名（含路径及后缀名）</param>
        /// <returns></returns>
        public string GetMD5HashFromFile(string filePath)
        {
            try
            {
                FileStream file = new FileStream(filePath, System.IO.FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }


        public static string GetFileMD5(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            int bufferSize = 1048576;
            byte[] buff = new byte[bufferSize];
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            md5.Initialize();
            long offset = 0;
            while (offset < fs.Length)
            {
                long readSize = bufferSize;
                if (offset + readSize > fs.Length)
                    readSize = fs.Length - offset;
                fs.Read(buff, 0, Convert.ToInt32(readSize));
                if (offset + readSize < fs.Length)
                    md5.TransformBlock(buff, 0, Convert.ToInt32(readSize), buff, 0);
                else
                    md5.TransformFinalBlock(buff, 0, Convert.ToInt32(readSize));
                offset += bufferSize;
            }
            if (offset >= fs.Length)
            {
                fs.Close();
                byte[] result = md5.Hash;
                md5.Clear();
                StringBuilder sb = new StringBuilder(32);
                for (int i = 0; i < result.Length; i++)
                    sb.Append(result[i].ToString("X2"));
                return sb.ToString();
            }
            else
            {
                fs.Close();
                return null;
            }
        }

        #endregion


    }
}
