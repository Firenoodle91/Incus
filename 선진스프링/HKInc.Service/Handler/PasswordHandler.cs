using System;
using System.Linq;
using System.Text.RegularExpressions;
using HKInc.Utils.Interface.Handler;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;

namespace HKInc.Service.Handler
{
    public class PasswordHandler : IPasswordHandler
    {
        public void UpdatePassword(string password)
        {            
            IService<User> UserService = (IService<User>)ServiceFactory.GetDomainService("User");
            User obj = UserService.GetList(p => p.UserId == GlobalVariable.UserId).First();
            if(obj != null)
            {
                //obj.Password = HKInc.Utils.Encrypt.AESEncrypt256.Encrypt(password);
                //TCK AES 관리 X
                obj.Password = password;
                UserService.Update(obj);
                UserService.Save();
            }
        }

        public bool IsValidFormat(string password, out string ErrorMessage)
        {
            IStandardMessage MessageHelper = Factory.HelperFactory.GetStandardMessage();

            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@GlobalVariable.PasswordNumberReg);
            var hasUpperChar = new Regex(@GlobalVariable.PasswordUpperCharReg);
            var hasMiniMaxChars = new Regex(@GlobalVariable.PasswordMinMaxCharReg);
            var hasLowerChar = new Regex(@GlobalVariable.PasswordLowerCharReg);
            var hasSymbols = new Regex(@GlobalVariable.PasswordSymbolReg);

            //if (!hasLowerChar.IsMatch(input))
            //{
            //    ErrorMessage = MessageHelper.GetStandardMessage(11);
            //    return false;
            //}
            //else if (!hasUpperChar.IsMatch(input))
            //{
            //    ErrorMessage = MessageHelper.GetStandardMessage(12);
            //    return false;
            //}
            //else if (!hasMiniMaxChars.IsMatch(input))
            //{
            //    ErrorMessage = MessageHelper.GetStandardMessage(13);
            //    return false;
            //}
            //else if (!hasNumber.IsMatch(input))
            //{
            //    ErrorMessage = MessageHelper.GetStandardMessage(14);
            //    return false;
            //}
            //else if (!hasSymbols.IsMatch(input))
            //{
            //    ErrorMessage = MessageHelper.GetStandardMessage(15);
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}

            //if (!hasMiniMaxChars.IsMatch(input))
            //{
            //    ErrorMessage = MessageHelper.GetStandardMessage(13);
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
            return true;
        }
    }
}
