using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Interface.Handler
{
    public interface IUserLogin
    {
        string UserId { get; set; }
        string MachineCode { get; set; }
        string Culture { get; set; }
        string Password { set; }

        bool IsValidUser();

        /// <summary>
        /// 20210818 오세완 차장 비밀번호 글자수 파악 버전
        /// </summary>
        /// <returns>0 - 예외사항 발생, 1 - id틀림, 2 - 비번틀림, 3 - 비번이 7글자 이하, 4 - 정상</returns>
        short IsValidUser_WithLength();
        void SaveUserId(bool saveUserId);
    }
}
