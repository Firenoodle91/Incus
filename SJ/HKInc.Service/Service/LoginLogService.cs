using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

using HKInc.Utils.Common;
using HKInc.Utils.Interface.Repository;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;

namespace HKInc.Service.Service
{    
    class LoginLogService : ILoginLogService
    {
        IRepository<LoginLog> Repository;        

        public LoginLogService(IRepository<LoginLog> repository)
        {
            Repository = repository;
        }

        public void SetLoginLog(DateTime loginTime)
        {
            LoginLog LoginLogInfo = new LoginLog()
            {
                LoginTime = loginTime,
                UserId =  GlobalVariable.UserInfo.UserId,
                IpAddress = GetLocalIPAddress(), 
                PcName = Dns.GetHostName()
            };

            Repository.Insert(LoginLogInfo);
            Repository.Save();

            GlobalVariable.LoginLogInfo = Repository.Detached(LoginLogInfo);
        }

        public void SetLogoutLog(DateTime logoutTime)
        {
            GlobalVariable.LoginLogInfo.LogoutTime = logoutTime;

            Repository.Update(GlobalVariable.LoginLogInfo);
            Repository.Save();
        }

        private string GetLocalIPAddress()
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
        
    }
}
