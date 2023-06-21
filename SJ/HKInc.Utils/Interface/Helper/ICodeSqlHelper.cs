using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HKInc.Utils.Enum;

namespace HKInc.Utils.Interface.Helper
{
    public interface ICodeSqlHelper
    {
        DataTable GetCodeSqlDataTable(string sqlId, List<object> parameterList, bool isPopupForm = false,  DatabaseCategory database = DatabaseCategory.DefaultProduction);
    }
}
