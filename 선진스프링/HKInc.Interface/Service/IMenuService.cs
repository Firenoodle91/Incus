using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKInc.Ui.Model;
namespace HKInc.Interface.Service
{
    public interface IMenuService
    {
        IEnumerable<HKInc.Ui.Model.Domain.MenuUserList> GetMainMenuList(decimal userId);

        IEnumerable<HKInc.Ui.Model.Domain.MenuBookmark> GetBookmarkMenuList(decimal userId);

        void AddMenuToBookmarkList(HKInc.Ui.Model.Domain.MenuUserList menuUserList);

        void RemoveFromBookmarkList(HKInc.Ui.Model.Domain.MenuBookmark menuBookmark);

        IEnumerable<HKInc.Ui.Model.Domain.MenuFavorite> GetFavoriteMenuList(decimal userId);
    }
}
