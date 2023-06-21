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
            {"ParameterInfo", () => new SystemService<ParameterInfo>()},

            {"Screen", () => new SystemService<Screen>()},

            {"CultureField", () => new ProductionService<CultureField>()},
            {"StandardMessage", () => new SystemService<StandardMessage>()},
            {"SystemLog", () => new SystemService<SystemLog>()},
            {"UserGroup", () => new SystemService<UserGroup>()},
            {"UserUserGroup", () => new SystemService<UserUserGroup>()},
    
            {"Notice", () => new SystemService<Notice>()},
        
            {"Department", () => new SystemService<UserDepartment>()},
            {"GridLayout", () => new SystemService<GridLayout>()},
            {"MENUTOTABLE", () => new SystemService<MENUTOTABLE>()}, // 20220421 오세완 차장 TP보고용 테이블 추가 
            {"VI_TABLESIZE", () => new SystemService<VI_TABLESIZE>()} // 20220421 오세완 차장 TP보고용 뷰 추가 
        };

        public static object GetDomainService(string domainName)
        {
            return DomainList[domainName]();
        }
        
    }
}
