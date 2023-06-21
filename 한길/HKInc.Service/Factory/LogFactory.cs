using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Context;
using HKInc.Service.Repository;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Handler;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Interface.Service;


namespace HKInc.Service.Factory
{
    public class LogFactory
    {
        private static readonly DatabaseCategory database = GlobalVariable.LogInDataBase;

        public static ILoginLogService GetLoginLogService()
        {
            return new LoginLogService(new SystemRepository<LoginLog>(ServerInfo.GetConnectString(database)));
        }

        public static IMenuOpenLogService GetMenuOpenLogService()
        {
            return new MenuOpenLogService(new SystemRepository<MenuLog>(ServerInfo.GetConnectString(database)));
        }
    }
}
