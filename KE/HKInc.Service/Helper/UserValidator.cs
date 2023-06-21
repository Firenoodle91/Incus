using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Enum;

namespace HKInc.Service.Helper
{
    class UserValidator : IUserValidator
    {
        private ILoginService UserService;        

        public UserValidator(ILoginService userService)
        {
            this.UserService = userService;
        }

        public bool IsValidUser(string userId, string password, DatabaseCategory databaseCategory = DatabaseCategory.DefaultApplication)
        {
            return this.UserService.IsValidLogin(userId, password, databaseCategory);
        }

        /// <summary>
        /// 20210818 오세완 차장 비밀번호 7자리 이하 검증 버전
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="databaseCategory"></param>
        /// <returns>0 - 예외사항 발생, 1 - id틀림, 2 - 비번틀림, 3 - 비번이 7글자 이하, 4 - 정상</returns>
        public short IsValidUser_WithLength(string userId, string password, DatabaseCategory databaseCategory = DatabaseCategory.DefaultApplication)
        {
            return this.UserService.IsValidLogin_V2(userId, password, databaseCategory);
        }

        public HKInc.Ui.Model.Domain.User UserInfo { get { return this.UserService.GetUserInfo(); } }

        public string GetServerIpAddress()
        { 
            return this.UserService.GetServerIpAddress(); 
        }
    }
}
