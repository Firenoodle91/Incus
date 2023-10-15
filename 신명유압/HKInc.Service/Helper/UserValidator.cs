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

        public HKInc.Ui.Model.Domain.User UserInfo { get { return this.UserService.GetUserInfo(); } }

        public string GetServerIpAddress()
        { 
            return this.UserService.GetServerIpAddress(); 
        }
    }
}
