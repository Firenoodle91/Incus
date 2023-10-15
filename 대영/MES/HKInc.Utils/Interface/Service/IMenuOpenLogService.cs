using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Interface.Service
{
    public interface IMenuOpenLogService
    {
        void SetOpenMenuLog(DateTime openTime, int menuId);
        void SetCloseMenuLog(DateTime closeTime);
    }
}
