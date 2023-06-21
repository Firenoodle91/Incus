using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Context;
using HKInc.Service.Repository;
using HKInc.Utils.Interface.Repository;
using HKInc.Utils.Interface.Service;

namespace HKInc.Service.Service
{
    public class MenuService : IMenuService
    {
        public IEnumerable<MenuUserList> GetMainMenuList(decimal userId)
        {
            IRepository<MenuUserList> repository = new SystemRepository<MenuUserList>(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase));
            return repository.Find(p => p.UserId == userId).OrderBy(p => p.SortOrder).ToList();                       
        }

        public IEnumerable<MenuBookmark> GetBookmarkMenuList(decimal userId)
        {
            IRepository<MenuBookmark> repository = new SystemRepository<MenuBookmark>(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase));
            return repository.Find(p => p.UserId == userId).OrderBy(p => p.Menu.SortOrder).ToList();
        }

        public IEnumerable<MenuFavorite> GetFavoriteMenuList(decimal userId)
        {
            IRepository<MenuFavorite> repository = new SystemRepository<MenuFavorite>(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase));
            return repository.Find(p => p.UserId == userId).OrderBy(p => p.Menu.SortOrder).ToList();
        }

        public void AddMenuToBookmarkList(HKInc.Ui.Model.Domain.MenuUserList menuUserList)
        {
            // 존재하는지 먼저 확인 해야 된다.
            IRepository<MenuBookmark> repository = new SystemRepository<MenuBookmark>(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase));
            
            IEnumerable<MenuBookmark> idr = repository.Find(p => p.UserId == menuUserList.UserId && p.MenuId == menuUserList.MenuId);
            if (idr.Count() > 0) return;

            MenuBookmark bookmark = new MenuBookmark() { MenuId = menuUserList.MenuId, UserId = menuUserList.UserId };
            repository.Insert(bookmark);
            repository.Save();            
        }

        public void RemoveFromBookmarkList(HKInc.Ui.Model.Domain.MenuBookmark menuBookmark)
        {
            IRepository<MenuBookmark> repository = new SystemRepository<MenuBookmark>(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase));
            MenuBookmark bookmark = repository.Find(p => p.MenuBookmarkId == menuBookmark.MenuBookmarkId).FirstOrDefault();       
            repository.Delete(bookmark);
            repository.Save();
            
        }
    }
}
