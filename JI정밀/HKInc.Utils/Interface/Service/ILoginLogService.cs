using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Interface.Service
{
    public interface ILoginLogService
    {
        void SetLoginLog(DateTime loginTime);
        void SetLogoutLog(DateTime logoutTime);
    }
}
