using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Utils.Common;
using HKInc.Utils.Interface.Repository;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;

namespace HKInc.Service.Service
{
    public class MenuEventLogService : IMenuEventService
    {
        IRepository<MenuEventLog> Repository;
        MenuEventLog MenuEventLogInfo;

        public MenuEventLogService(IRepository<MenuEventLog> repository)
        {
            Repository = repository;
        }

        public void SetMenuEventLog(int menuId, string state)
        {
            MenuEventLogInfo = new MenuEventLog();
            MenuEventLogInfo.Date = DateTime.Today;
            MenuEventLogInfo.UserId = GlobalVariable.UserId.GetIntNullToZero();
            MenuEventLogInfo.MenuId = menuId;

            switch (state)
            {
                case "Refresh":
                    MenuEventLogInfo.SearchCnt = 1;
                    break;
                case "barButtonAdd":
                    MenuEventLogInfo.AddCnt = 1;
                    break;
                case "Save":
                    MenuEventLogInfo.UpdateCnt = 1;
                    break;
                case "barButtonDelete":
                    MenuEventLogInfo.DeleteCnt = 1;
                    break;
            }

            Repository.Insert(MenuEventLogInfo);
            Repository.Save();
        }

        /// <summary>
        /// TP 보고용 로그 기록 추가
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="state">Refresh - 새로고침, barButtonAdd - 데이터 추가, Save - 데이터 저장, barButtonDelete - 데이터 삭제 </param>
        public void UpdateMenuEventLog(int menuId, string state)
        {
            MenuEventLogInfo = Repository.Find(x => x.Date == DateTime.Today && x.UserId == GlobalVariable.UserId && x.MenuId == menuId).FirstOrDefault();
            if (MenuEventLogInfo == null)
            {
                SetMenuEventLog(menuId, state);
            }
            else
            {
                switch (state)
                {
                    case "Refresh":
                        MenuEventLogInfo.SearchCnt = MenuEventLogInfo.SearchCnt + 1;
                        break;
                    case "barButtonAdd":
                        MenuEventLogInfo.AddCnt = MenuEventLogInfo.AddCnt + 1;
                        break;
                    case "Save":
                        MenuEventLogInfo.UpdateCnt = MenuEventLogInfo.UpdateCnt + 1;
                        break;
                    case "barButtonDelete":
                        MenuEventLogInfo.DeleteCnt = MenuEventLogInfo.DeleteCnt + 1;
                        break;
                }
            }

            Repository.Update(MenuEventLogInfo);
            Repository.Save();
        }
    }
}
