using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKInc.Utils.Interface.Repository;

namespace HKInc.Utils.Interface.Service
{
    public interface ILoginService : IIpAddress
    {
        bool IsValidLogin(string userId, string password, Enum.DatabaseCategory databaseCategory);

        /// <summary>
        /// 20210818 오세완 차장 비번 글자수 확인 하는 버전
        /// </summary>
        /// <param name="userId">id</param>
        /// <param name="password">password</param>
        /// <param name="databaseCategory">db</param>
        /// <returns>0 - 예외사항 발생, 1 - id틀림, 2 - 비번틀림, 3 - 비번이 7글자 이하, 4 - 정상</returns>
        short IsValidLogin_V2(string userId, string password, Enum.DatabaseCategory databaseCategory);

        HKInc.Ui.Model.Domain.User GetUserInfo();        
    }
}
