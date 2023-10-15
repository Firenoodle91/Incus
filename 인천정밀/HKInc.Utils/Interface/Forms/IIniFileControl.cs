using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace HKInc.Utils.Interface.Forms
{
    public class IIniFileControl
    {
        private static string sIniFileName = "PLC_Info.ini";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder reVal, int size, string filePath);

        public static void WriteIniFile(string section, string key, string value)
        {
            //string sPath = System.Windows.Forms.Application.StartupPath + "\\" + sIniFileName;
            string sPath = "C:\\IncusSoft\\" + sIniFileName; // 20200921 오세완 차장 실행파일에 저장을 하면 다음 버전 업데이트 후에 해당 파일을 찾지 못하기 때문에 폴더를 지정 처리
            WritePrivateProfileString(section, key, value, sPath);
        }

        public static string ReadIniFile(string section, string key)
        {
            //string sPath = System.Windows.Forms.Application.StartupPath + "\\" + sIniFileName;
            string sPath = "C:\\IncusSoft\\" + sIniFileName; // 20200921 오세완 차장 실행파일에 저장을 하면 다음 버전 업데이트 후에 해당 파일을 찾지 못하기 때문에 폴더를 지정 처리
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", sb, sb.Capacity, sPath);
            return sb.ToString();
        }
    }
}
