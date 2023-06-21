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
