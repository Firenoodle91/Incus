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
        private bool hasEdit;
        private bool hasSelect;
        private bool hasPrint;        
        private bool hasInsert;
        private bool hasExport;

        public UserRight(decimal menuId, decimal userId)
        {
            try
            {                
                IRepository<MenuUserRight> repository = new SystemRepository<MenuUserRight>(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase));

                IEnumerable<MenuUserRight> idr = repository.Find(p => p.MenuId == menuId && p.UserId == userId);
                if (idr.Count() > 0)
                {
                    MenuUserRight menuUserRight = idr.First();
                    this.hasEdit = menuUserRight.Write.Equals("Y") ? true : false;
                    this.hasSelect = menuUserRight.Read.Equals("Y") ? true : false;
                    this.hasInsert = menuUserRight.Insert.Equals("Y") ? true : false;
                    this.hasPrint = menuUserRight.Print.Equals("Y") ? true : false;
                    this.hasExport = menuUserRight.Export.Equals("Y") ? true : false;
                }
                else
                {
                    this.hasEdit = false;
                    this.hasSelect = false;
                    this.hasPrint = false;
                    this.hasInsert = false;
                    this.hasExport = false;
                }
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }

        public bool HasEdit { get { return hasEdit; } }
        public bool HasSelect { get { return hasSelect; } }        
        public bool HasPrint { get { return hasPrint; } }
        public bool HasInsert { get { return hasInsert; } }
        public bool HasExport { get { return hasExport; } }
    }
}
