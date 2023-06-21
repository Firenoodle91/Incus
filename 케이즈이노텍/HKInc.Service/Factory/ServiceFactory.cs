using System;
using System.Collections;
using System.Collections.Generic;

using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;

namespace HKInc.Service.Factory
{
    public class ServiceFactory
    {
        private static Dictionary<string, Func<object>> DomainList = new Dictionary<string,Func<object>>()
        {
            {"Module", () => new SystemService<Module>()},
            {"User", () => new SystemService<User>()},          
            {"CodeMaster", () => new SystemService<CodeMaster>()},       
            {"GroupMenu", () => new SystemService<GroupMenu>()},
            {"FieldLabel", () => new SystemService<FieldLabel>()},
            {"LoginLog", () => new SystemService<LoginLog>()},
            {"Menu", () => new SystemService<Menu>()},
            {"MenuBookmark", () =>  new SystemService<MenuBookmark>()},
            {"MenuFavorite", () => new SystemService<MenuFavorite>()},
            {"MenuLog", () => new SystemService<MenuLog>()},
            {"MenuUserList", () => new SystemService<MenuUserList>()},      
            {"Screen", () => new SystemService<Screen>()},         
            {"StandardMessage", () => new SystemService<StandardMessage>()},
            {"SystemLog", () => new SystemService<SystemLog>()},
            {"UserGroup", () => new SystemService<UserGroup>()},
            {"UserUserGroup", () => new SystemService<UserUserGroup>()},    
            {"Notice", () => new SystemService<Notice>()},        
            {"GridLayout", () => new SystemService<GridLayout>()},
            {"Holiday", () => new SystemService<Holiday>()},
            {"VI_TABLESIZE", () => new SystemService<VI_TABLESIZE>()},
            {"MENUTOTABLE", () => new SystemService<MENUTOTABLE>()}
        };

        public static object GetDomainService(string domainName)
        {
            return DomainList[domainName]();
        }
        
    }
}
