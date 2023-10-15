using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Repository;

namespace HKInc.Utils.Interface.Helper
{
    public interface IUserValidator : IIpAddress
    {
        bool IsValidUser(string userId, string password, DatabaseCategory databaseCategory = DatabaseCategory.DefaultApplication);

        /// <summary>
        /// 20210818 오세완 차장 비밀번호 글자수 7글자 이하면 오류를 감지하는 버전
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="databaseCategory"></param>
        /// <returns>0 - 예외사항 발생, 1 - id틀림, 2 - 비번틀림, 3 - 비번이 7글자 이하, 4 - 정상</returns>
        short IsValidUser_WithLength(string userId, string password, DatabaseCategory databaseCategory = DatabaseCategory.DefaultApplication);

        User UserInfo { get;}
    }
}
