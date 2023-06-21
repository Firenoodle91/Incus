using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Service.Factory
{
    public class MenuFactory
    {
        public static HKInc.Utils.Interface.Helper.IUserRight GetUserRight(decimal menuId, decimal userId)
        {
            return new HKInc.Service.Helper.UserRight(menuId, userId);
        }

        public static HKInc.Utils.Interface.Service.IMenuService GetMenuService()
        {
            return new HKInc.Service.Service.MenuService();
        }
    }
}
