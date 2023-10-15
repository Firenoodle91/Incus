using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Utils.Common;
using HKInc.Utils.Interface.Repository;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;

namespace HKInc.Service.Service
{
    public class MenuOpenLogService : IMenuOpenLogService
    {
        IRepository<MenuLog> Repository;
        MenuLog MenuLogInfo;

        public MenuOpenLogService(IRepository<MenuLog> repository)
        {
            Repository = repository;
        }

        public void SetOpenMenuLog(DateTime openTime, int menuId)
        {
            MenuLogInfo = new MenuLog()
            {
                LoginLogId = GlobalVariable.LoginLogInfo.LoginLogId,
                MenuId = menuId,
                OpenTime = openTime
            };

            Repository.Insert(MenuLogInfo);
            Repository.Save();
        }

        public void SetCloseMenuLog(DateTime closeTime)
        {
            MenuLogInfo.CloseTime = closeTime;
            Repository.Update(MenuLogInfo);
            Repository.Save();
        }
    }
}
