using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using HKInc.Utils.Common;
using HKInc.Utils.Encrypt;

namespace HKInc.Service.Handler
{
    public static class LicenseKeyHandler
    {
        public static string GetGeneratedLicenseKey(string macAddress)
        {            
            string textToEncrypt = HKInc.Utils.Common.GlobalVariable.LicenseEncryptionKey;
            return AESEncrypt256.Encrypt(textToEncrypt, macAddress.Replace("-","").Replace(":",""));
        }        

        public static bool IsLicensedMachine()
        {
            // License Check
            // Key Registry 확인 없거나 불일치
            // Key입력 Popup 표시
            // Key입력 받아 Registry에저장
            if (IsLicenseKeyMached())
                return true;

            // Make Popup
            HKInc.Service.Forms.LicenseKeyInput form = new HKInc.Service.Forms.LicenseKeyInput();
            if(form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (IsLicenseKeyMached(form.KeyInput))
                {
                    RegistryHandler.SetValue(GlobalVariable.ServerConfigPath, "LicenseKey", form.KeyInput);
                    return true;
                }                    
            }
            return false;
        }

        private static bool IsLicenseKeyMached(string licenseKey)
        {            
            if (string.IsNullOrEmpty(licenseKey)) return false;

            string[] macAddress = GetLocalMacAddress();
            if (macAddress == null || macAddress.Length == 0) return false;

            foreach (var addr in macAddress)
            {
                string encryptedString = GetGeneratedLicenseKey(addr);
                if (licenseKey.Equals(encryptedString)) return true;
            }
            return false;
        }

        private static bool IsLicenseKeyMached()
        {            
            // GlobalVariable.LicenseEncryptionKey 를 Mac Address로 Encrypt 한다.
            string licenseKey = RegistryHandler.GetValue(GlobalVariable.ServerConfigPath, "LicenseKey");
            if (string.IsNullOrEmpty(licenseKey)) return false;

            return IsLicenseKeyMached(licenseKey);
        }

        private static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        private static string[] GetLocalMacAddress()
        {
            var networks = NetworkInterface.GetAllNetworkInterfaces();
            var activeNetworks = networks.Where(ni => ni.OperationalStatus == OperationalStatus.Up && 
                                                      ni.NetworkInterfaceType != NetworkInterfaceType.Loopback);
            string[] macAddress = new string[activeNetworks.Count()];
            int i = 0;
            foreach (var nic in activeNetworks)
                macAddress[i++] = nic.GetPhysicalAddress().ToString();

            return macAddress;           
        }
    }
}
