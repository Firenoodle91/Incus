using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using HKInc.Ui.Model.Domain;
using HKInc.Utils.Encrypt;
using HKInc.Utils.Enum;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Repository;
using HKInc.Utils.Interface.Service;

namespace HKInc.Service.Service
{
    public class LoginService : ILoginService
    {
        private User UserInfo;
        private IRepository<User> UserRepository;

        public LoginService(IRepository<User> repository)
        {
            UserRepository = repository;
        }

        public bool IsValidLogin(string userId, string password, DatabaseCategory databaseCategory)
        {
            try
            {
                IEnumerable<User> userList = UserRepository.Find(p => p.LoginId.Equals(userId) && p.Active == "Y").ToList();
                if (userList.Count() > 0)
                {
                    this.UserInfo = userList.First();
                    // AES 관리 X
                    //if (UserInfo.Password.Equals(AESEncrypt256.Encrypt(password)))
                    //    return true;
                    if (UserInfo.Password.Equals(password))
                        return true;
                }
            }
            catch(Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
            return false;
        }

        /// <summary>
        /// 20210817 오세완 차장
        /// 시정조치에 따라서 비번은 대소문 구분안하게 하고 초기 비밀번호 설정이 8자리 이하인 경우를 분간하기 위한 버전
        /// </summary>
        /// <param name="userId">id</param>
        /// <param name="password">password</param>
        /// <param name="databaseCategory">db</param>
        /// <returns>0 - 예외사항 발생, 1 - id틀림, 2 - 비번틀림, 3 - 비번이 7글자 이하, 4 - 정상</returns>
        public short IsValidLogin_V2(string userId, string password, DatabaseCategory databaseCategory)
        {
            short sResult = 0;
            try
            {
                IEnumerable<User> userList = UserRepository.Find(p => p.LoginId.Equals(userId) && p.Active == "Y").ToList();
                if (userList.Count() > 0)
                {
                    this.UserInfo = userList.First();
                    if(this.UserInfo == null)
                    {
                        sResult = 1;
                    }
                    else
                    {
                        if (UserInfo.Password.ToUpper().Equals(password.ToUpper()))
                        {
                            if (UserInfo.Password.Length < 8)
                                sResult = 3;
                            else
                                sResult = 4;
                        }
                        else
                            sResult = 2;
                    }
                    
                }
                else
                    sResult = 1;

            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }

            return sResult;
        }

        public User GetUserInfo()
        {
            //return UserRepository.Detached(UserInfo);
            return UserInfo;
        }

        public string GetServerIpAddress()
        {
            return UserRepository.GetDbContext().Database.SqlQuery<string>(GlobalVariable.ServerIpAddressQuery).ToList().FirstOrDefault();            
        }
    }
}
