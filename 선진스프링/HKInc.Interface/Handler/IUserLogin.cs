using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Interface.Handler
{
    public interface IUserLogin
    {
        string UserId { get; set; }
        string Culture { get; set; }
        string Password { set; }

        bool IsValidUser();
        void SaveUserId(bool saveUserId);
    }
}
