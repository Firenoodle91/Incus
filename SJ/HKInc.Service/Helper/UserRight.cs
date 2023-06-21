using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Context;
using HKInc.Service.Repository;
using HKInc.Utils.Interface.Repository;
using HKInc.Utils.Interface.Helper;

namespace HKInc.Service.Helper
{
    class UserRight : IUserRight
    {
        public UserRight(decimal menuId, decimal userId)
        {
            try
            {                
                IRepository<MenuUserRight> repository = new SystemRepository<MenuUserRight>(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase));

                IEnumerable<MenuUserRight> idr = repository.Find(p => p.MenuId == menuId && p.UserId == userId);
                if (idr.Count() > 0)
                {
                    MenuUserRight menuUserRight = idr.First();
                    HasEdit = menuUserRight.Write.Equals("Y") ? true : false;
                    HasSelect = menuUserRight.Read.Equals("Y") ? true : false;
                    HasInsert = menuUserRight.Insert.Equals("Y") ? true : false;
                    HasPrint = menuUserRight.Print.Equals("Y") ? true : false;
                    HasExport = menuUserRight.Export.Equals("Y") ? true : false;
                    HasReload = menuUserRight.Reload.Equals("Y") ? true : false;
                }
                else
                {
                    HasEdit = false;
                    HasSelect = false;
                    HasPrint = false;
                    HasInsert = false;
                    HasExport = false;
                    HasReload = false;
                }
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }

        public bool HasEdit { get; }
        public bool HasSelect { get; }
        public bool HasPrint { get; }
        public bool HasInsert { get; }
        public bool HasExport { get; }
        public bool HasReload { get; }
    }
}
