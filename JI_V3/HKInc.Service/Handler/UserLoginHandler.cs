using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Handler;
using HKInc.Utils.Interface.Helper;

namespace HKInc.Service.Handler
{
    class UserLoginHandler : IUserLogin
    {
        private string culture;
        private string userId;
        private string machinecode;
        private string password;
        private bool saveUserId = false;
        private DatabaseCategory loginDatabase;
        private IUserValidator UserValidator;

        public UserLoginHandler(IUserValidator userValidator)
        {
            this.UserValidator = userValidator;
            this.Culture = GetCultureFromRegistry();
            this.UserId = GetUserIdFromRegistry();
            this.machinecode = GetMachineCodeFromRegistry();
        }

        #region IUserLogin Interface
        public string Culture
        {
            get { return culture; }
            set
            {
                culture = value;
                SetCultureToGlobalVar();
                SetCultureToRegistry();
            }
        }

        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                if (!string.IsNullOrEmpty(userId))                
                    this.loginDatabase = GetLoginDatabaseFromRegistry(this.userId);                                    
                else                
                    this.loginDatabase = GlobalVariable.DefaultLogInDataBase;                                    
            }
        }
        public string MachineCode
        {
            get { return machinecode; }
            set
            {
                machinecode = value;
                SetMachineCodeToRegistry(this.machinecode);

            }
        }

        public string Password { protected get { return password; } set { password = value; } }

        public bool IsValidUser()
        {
            if (this.UserValidator.IsValidUser(UserId, Password, this.loginDatabase))
            {
                GlobalVariable.SetUser(this.UserValidator.UserInfo);
                GlobalVariable.ServerIp = this.UserValidator.GetServerIpAddress(); 
                SetUserIdToRegistry(userId);
                SetMachineCodeToRegistry(machinecode);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 20210818 오세완 차장 비밀번호 글자수 파악 버전
        /// </summary>
        /// <returns>0 - 예외사항 발생, 1 - id틀림, 2 - 비번틀림, 3 - 비번이 7글자 이하, 4 - 정상</returns>
        public short IsValidUser_WithLength()
        {
            short sReturn = this.UserValidator.IsValidUser_WithLength(UserId, Password, this.loginDatabase);
            if(sReturn == 4)
            {
                // 20210825 오세완 차장 정상인 경우 사용자 저장같은 기능을 동작하게 수정
                GlobalVariable.SetUser(this.UserValidator.UserInfo);
                GlobalVariable.ServerIp = this.UserValidator.GetServerIpAddress();
                SetUserIdToRegistry(userId);
                SetMachineCodeToRegistry(machinecode);
            }
            return sReturn;
        }

        public void SaveUserId(bool saveUserId)
        {
            this.saveUserId = saveUserId;
        }
        #endregion


        private DatabaseCategory LoginDatabase { get { return loginDatabase; } set { this.loginDatabase = value; } }

        //
        private string GetUserIdFromRegistry()
        {
            string uid = RegistryHandler.GetValue(GlobalVariable.LoginInfoPath, "UID");

            if (!String.IsNullOrEmpty(uid))
                return uid;
            else
                return String.Empty;
        }

        private void SetUserIdToRegistry(string userId)
        {
            if (saveUserId)
                RegistryHandler.SetValue(GlobalVariable.LoginInfoPath, "UID", userId);
            else
                RegistryHandler.SetValue(GlobalVariable.LoginInfoPath, "UID", string.Empty);
        }
        //로그인 창 설비코드 선택 검색
        private string GetMachineCodeFromRegistry()
        {
            string machine = RegistryHandler.GetValue(GlobalVariable.MachineInfoPath, "MACHINE");

            if (!String.IsNullOrEmpty(machine))
                return machine;
            else
                return String.Empty;
        }
        //로그인 창 설비코드 선택 저장
        private void SetMachineCodeToRegistry(string machinecode)
        {
            RegistryHandler.SetValue(GlobalVariable.MachineInfoPath, "MACHINE", machinecode);
        }

        //
        private string GetCultureFromRegistry()
        {
            string cul = RegistryHandler.GetValue(GlobalVariable.CulturePath, "Culture");

            if (!String.IsNullOrEmpty(cul))
                return cul;
            else
                return GlobalVariable.DefaultCulture;
        }

        //
        private void SetCultureToRegistry()
        {
            RegistryHandler.SetValue(GlobalVariable.CulturePath, "Culture", GlobalVariable.Culture);
        }

        //
        private void SetCultureToGlobalVar()
        {
            GlobalVariable.Culture = this.culture;

            Thread.CurrentThread.CurrentCulture = new CultureInfo(GlobalVariable.Culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(GlobalVariable.Culture);
        }

        // userId can be Domain Userid or SIGN user ID
        private DatabaseCategory GetLoginDatabaseFromRegistry(string userId)
        {
            const string key = "LogInDataBase";
            string registryPath = string.Format(@"{0}\{1}", GlobalVariable.ServerConfigPath, userId);
            DatabaseCategory decriptedDataBase = DatabaseCategory.DefaultApplication;

            string registryDataBase = RegistryHandler.GetValue(registryPath, key);

            // Servercofig\UserID key로 먼저 찾고
            // Servercofig\UserID key가 없으면 ServerConfig\ key에서 찾는다
            if (String.IsNullOrEmpty(registryDataBase))
            {
                registryDataBase = RegistryHandler.GetValue(GlobalVariable.ServerConfigPath, key);  // 없으면 default path에서 registry값을 가져온다
                if (String.IsNullOrEmpty(registryDataBase))
                    decriptedDataBase = GlobalVariable.DefaultLogInDataBase;   // 그래도 없으면 디폴트값사용
                else
                    decriptedDataBase = ServerInfo.GetDatabaseCategory(registryDataBase);
            }
            else
            {
                decriptedDataBase = ServerInfo.GetDatabaseCategory(registryDataBase);
            }
            return decriptedDataBase;
        }

        private void SetLoginDatabaseToRegistry()
        {
            ////////////////////////////////////////////////////////////////////////
            // Login Database to login & Sercurity
            // User Id or AD user ID 별로 구분Setting.
            // ServerConfig/UserID key에 저장 한다.
            ////////////////////////////////////////////////////////////////////////                  
            this.loginDatabase = GlobalVariable.LogInDataBase;

            SetLoginDatabaseToRegistry(GlobalVariable.LoginId);
        }

        private void SetLoginDatabaseToRegistry(string agUserId)
        {
            const string key = "LogInDataBase";
            string registryPath = string.Format(@"{0}\{1}", GlobalVariable.ServerConfigPath, agUserId);

            RegistryHandler.SetValue(registryPath, key, this.loginDatabase.ToString());
        }

        private string GetUpdateServer()
        {
            if (string.IsNullOrEmpty(GlobalVariable.UpdateServer))
                return string.Empty;
            else
                return GlobalVariable.UpdateServer;
        }        
    }
}
