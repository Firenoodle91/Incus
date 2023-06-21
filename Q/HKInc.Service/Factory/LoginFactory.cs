using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Context;
using HKInc.Service.Repository;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Handler;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Interface.Service;

namespace HKInc.Service.Factory
{
    public class LoginFactory
    {
        private static readonly DatabaseCategory database = GlobalVariable.LogInDataBase;

        public static IUserLogin GetUserLogin()
        {
            return new Handler.UserLoginHandler(GetUserValidator());
        }

        private static IUserValidator GetUserValidator()
        {
            return new Helper.UserValidator(GetLoginService());
        }   

        private static ILoginService GetLoginService()
        {                        
            return new LoginService(new SystemRepository<User>(ServerInfo.GetConnectString(database)));
        }   
        
        public static IPasswordHandler GetPasswordHandler()
        {
            return new HKInc.Service.Handler.PasswordHandler();
        }
    }
}
