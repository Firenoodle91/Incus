using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKInc.Ui.Model.Context;
using HKInc.Utils.Common;

namespace HKInc.Service.Handler
{
    public static class SystemLogHandler
    {
        public static void SendSystemLog(int errorCode, string message, string message2 = "", string message3 = "", string message4 = "", string message5 = "")
        {
            using (ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase)))
            {
                db.MakeSystemLogEntry(0, GlobalVariable.CurrentInstance, errorCode, message, message2, message3, message4, message5, GlobalVariable.LoginId, GlobalVariable.CurrentInstance);
            }
        }
    }
}
