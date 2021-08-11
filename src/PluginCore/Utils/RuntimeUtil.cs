using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.Utils
{
    public class RuntimeUtil
    {
        public static Version RuntimeNetVersion
        {
            get
            {
                return Environment.Version;
            }
        }
    }
}
