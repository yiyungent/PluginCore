using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.Utils
{
    public class LogUtil
    {
        public const string Sign = "PluginCore";

        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{Sign}: ");
            Console.ResetColor();
            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine(message);
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Sign}: ");
            Console.ResetColor();
            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine(message);
        }

    }
}
