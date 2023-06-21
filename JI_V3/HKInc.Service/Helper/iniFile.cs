﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Service.Helper
{
    public class iniFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        public iniFile(string path, string file)
        {
            //string iniFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)+ @"\mando";
            string iniFolder = path;
            if (!System.IO.Directory.Exists(iniFolder))
            {
                System.IO.Directory.CreateDirectory(iniFolder);
            }

            //path = INIPath;
            path = file;
        }

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <param name="path"></param>
        public void IniWriteValue(string Section, string Key, string Value, string path)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            try
            {
                StringBuilder temp = new StringBuilder(255);
                int i = GetPrivateProfileString(Section, Key, "", temp,
                                                255, this.path);
                return temp.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key, string path)
        {
            try
            {
                StringBuilder temp = new StringBuilder(255);
                int i = GetPrivateProfileString(Section, Key, "", temp,
                                                255, path);
                return temp.ToString();
            }
            catch
            {
                return "";
            }
        }


    }
}
