using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using HKInc.Ui.Model.Domain;

namespace HKInc.Utils.Interface.Helper
{
    public interface IMasterCode
    {
        IEnumerable<CodeMaster> GetMasterCode(int groupCode);
        IEnumerable<CodeMaster> GetActiveMasterCode(int groupCode);        
        IEnumerable<CodeMaster> GetMasterCodeFind(Expression<Func<CodeMaster, bool>> condition);
        IEnumerable<CodeMaster> GetMasterCodeFindCodeName(string CodeName);
        
        string GetCodeName(int code);
        void Reset();
    }
}
