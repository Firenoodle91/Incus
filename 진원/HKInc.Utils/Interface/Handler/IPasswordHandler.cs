using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Interface.Handler
{
    public interface IPasswordHandler
    {
        bool IsValidFormat(string password, out string ErrorMessage);
        void UpdatePassword(string password);
    }
}
