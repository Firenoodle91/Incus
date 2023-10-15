using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Class
{
    public static class DatabaseIpFunction
    {
        public static string DefaultServerName()
        {
            return Properties.Settings.Default.DefaultServerName;
        }
        public static string setDefaultServerName(string ip)
        {
             Properties.Settings.Default.DefaultServerName=ip;
            return Properties.Settings.Default.DefaultServerName;
        }
    }
}
