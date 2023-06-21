using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Context;
using HKInc.Service.Repository;
using HKInc.Utils.Interface.Repository;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Class;


namespace HKInc.Service.Helper
{
    class MasterCode : IMasterCode
    {
        private static IEnumerable<CodeMaster> MasterCodeList;

        public static void GetMasterCodeList()
        {
            try
            {                
                IRepository<CodeMaster> repository = new SystemRepository<CodeMaster>(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase));
                MasterCodeList = repository.GetAll().Select(p => new CodeMaster()
                {
                    CodeId = p.CodeId,
                    CodeGroup = p.CodeGroup,
                    GroupDescription = p.GroupDescription,
                    CodeName = GlobalVariable.IsDefaultCulture ? p.CodeName : (GlobalVariable.IsSecondCulture ? p.CodeName2 : p.CodeName3),
                    CodeName2 = p.CodeName2,
                    CodeName3 = p.CodeName3,
                    DisplayOrder = p.DisplayOrder,
                    Property1 = p.Property1,
                    Property2 = p.Property2,
                    Property3 = p.Property3,
                    Property4 = p.Property4,
                    Property5 = p.Property5,
                    Property6 = p.Property6,
                    Property7 = p.Property7,
                    Property8 = p.Property8,
                    Property9 = p.Property9,
                    Property10 = p.Property10,
                    Active = p.Active,
                    DeActiveDate = p.DeActiveDate
                });
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }

        public void Reset() { GetMasterCodeList(); }

        public static void ResetCode() { GetMasterCodeList(); }

        public IEnumerable<CodeMaster> GetMasterCode(int groupCode)
        {
            if (MasterCodeList == null || MasterCodeList.Count() == 0) GetMasterCodeList();

            return MasterCodeList.Where(p => p.CodeGroup == groupCode && (p.Active.GetNullToEmpty().Equals("Y") || string.IsNullOrEmpty(p.Active)))
                                 .OrderBy(p=>p.DisplayOrder).ThenBy(p=>p.CodeName);
        }

        public IEnumerable<CodeMaster> GetActiveMasterCode(int groupCode)
        {
            if (MasterCodeList == null || MasterCodeList.Count() == 0) GetMasterCodeList();

            return MasterCodeList.Where(p => p.CodeGroup == groupCode && (p.Active.GetNullToEmpty().Equals("Y") || string.IsNullOrEmpty(p.Active)))
                                 .OrderBy(p => p.DisplayOrder).ThenBy(p => p.CodeName);
        }
        
        public IEnumerable<CodeMaster> GetMasterCodeFind(Expression<Func<CodeMaster, bool>> condition)
        {
            IRepository<CodeMaster> repository = new SystemRepository<CodeMaster>(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase));
            return repository.Find(condition);
        }

        public IEnumerable<CodeMaster> GetMasterCodeFindCodeName(string CodeName)
        {
            if (MasterCodeList == null || MasterCodeList.Count() == 0) GetMasterCodeList();

            return MasterCodeList.Where(p => p.CodeName == CodeName && (p.Active.GetNullToEmpty().Equals("Y") || string.IsNullOrEmpty(p.Active)))
                                 .OrderBy(p => p.DisplayOrder).ThenBy(p => p.CodeName).ToList();
        }

        public string GetCodeName(int code)
        {
            CodeMaster codeMaster = MasterCodeList.FirstOrDefault(p => p.CodeId == code);

            return GlobalVariable.IsDefaultCulture ? codeMaster.CodeName : (GlobalVariable.IsSecondCulture ? codeMaster.CodeName2 : codeMaster.CodeName3);
        }        
    }
}
