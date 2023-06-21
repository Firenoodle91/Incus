using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HKInc.Service.Handler;
using System.IO;

namespace HKInc.Service.Helper
{
    public  class iniFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        //string aa = "C:\\qmack\\Serial.ini";      path
        //string ab = "C:\\qmack";                  file
        //  "C:\\qmack\\Serial.ini";
        public iniFile(string path, string file)
        {
            //var userprofile_location = "C:\\qmack";
            //var userprofile_location = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) /*+ @"\Appdata\Roaming\GameCentral"*/;
            //var userprofile_location = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) /*+ @"\Appdata\Roaming\GameCentral"*/;
            Directory.CreateDirectory(@"C:\qmack");
            //if (!Directory.Exists(userprofile_location + @"\Serial.ini"))
            //if (!Directory.Exists(@"C:\qmack\Serial.ini"))
            DirectoryInfo a = new DirectoryInfo(@"C:\qmack\Serial.ini");

            if (!a.Exists)
            {
                File.Create(@"C:\qmack\Serial.ini");
                //File.Create(userprofile_location + @"\Serial.ini");

                //WritePrivateProfileString("RS232", "WorkNo", "", userprofile_location + @"\Serial.ini");
                //WritePrivateProfileString("RS232", "Process", "", userprofile_location + @"\Serial.ini");
                //WritePrivateProfileString("RS232", "Port", "", userprofile_location + @"\Serial.ini");
                //WritePrivateProfileString("RS232", "Machine", "", userprofile_location + @"\Serial.ini");

                //StringBuilder WorkNo = new StringBuilder();
                //StringBuilder Process = new StringBuilder();
                //StringBuilder Port = new StringBuilder();
                //StringBuilder Machine = new StringBuilder();

                //GetPrivateProfileString("RS232", "WorkNo", "", WorkNo, WorkNo.Capacity, userprofile_location + @"\Serial.ini");
                //GetPrivateProfileString("RS232", "Process", "", Process, Process.Capacity, userprofile_location + @"\Serial.ini");
                //GetPrivateProfileString("RS232", "Port", "", Port, Port.Capacity, userprofile_location + @"\Serial.ini");
                //GetPrivateProfileString("RS232", "Machine", "", Machine, Machine.Capacity, userprofile_location + @"\Serial.ini");
            }
            //File.Create("C:\\qmack\\Serial.ini");
            //iniFile settings = new iniFile(userprofile_location + @"\Serial.ini", "");
            //settings.IniWriteValue("1", "PFAD", "Icons");


            //var userprofile_location = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Appdata\Roaming\GameCentral";
            //Directory.CreateDirectory(userprofile_location);
            //File.Create(userprofile_location + @"\settings.ini");
            //iniFile settings = new iniFile(userprofile_location + @"\settings.ini", "");
            //settings.IniWriteValue("1", "PFAD", "Icons");

        }

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        //public iniFile(string path, string file)
        //{
        //    //string iniFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)+ @"\mando";
        //    string iniFolder = path;
        //    if (!System.IO.Directory.Exists(iniFolder))
        //    {
        //        System.IO.Directory.CreateDirectory(iniFolder);
        //        //System.IO.Directory.CreateDirectory(iniFolder);
        //    }

        //    //path = INIPath;
        //    //path = file;
        //}

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
