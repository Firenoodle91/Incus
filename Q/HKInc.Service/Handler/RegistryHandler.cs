using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Utils.Common;

namespace HKInc.Service.Handler
{
    public class RegistryHandler
    {
        public static string GetValue(string path, string key)
        {
            try
            {
                string keyPath = String.Format(@"{0}\{1}", GlobalVariable.MainPath, path);
                RegistryKey reg = Registry.CurrentUser.OpenSubKey(keyPath, true);
                if (reg == null)
                {
                    reg = Registry.CurrentUser.CreateSubKey(keyPath);
                }
                return reg.GetValue(key).ToString();
            }
            catch
            {
                return null;
            }
        }

        public static void SetValue(string path, string key, string value)
        {
            string keyPath = String.Format(@"{0}\{1}", GlobalVariable.MainPath, path);
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(keyPath, true);
            if (reg == null)
            {
                reg = Registry.CurrentUser.CreateSubKey(keyPath);
            }
            reg.SetValue(key, value);
        }
    }
}
