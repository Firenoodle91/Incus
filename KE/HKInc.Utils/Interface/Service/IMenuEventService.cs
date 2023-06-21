using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Interface.Service
{
    public interface IMenuEventService
    {
        void SetMenuEventLog(int menuId, string state);

        void UpdateMenuEventLog(int menuId, string state);
    }
}
