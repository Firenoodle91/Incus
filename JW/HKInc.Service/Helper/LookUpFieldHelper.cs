using System;
using System.Collections.Generic;
using System.Linq;

using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Repository;
using HKInc.Utils.Interface.Repository;

namespace HKInc.Service.Helper
{
    public class LookUpFieldHelper
    {
        private static IEnumerable<CultureField> CultureFieldList;

        private static void GetCultureFieldList()
        {
            try
            {
                IRepository<CultureField> repository = new SystemRepository<CultureField>(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase));
                CultureFieldList = repository.GetAll();
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }           
        }

        public static void Reset() { GetCultureFieldList(); }

        public static string GetCultureFieldName(string entityName)
        {
            if (CultureFieldList == null) GetCultureFieldList();

            try
            {
                IEnumerable<CultureField> idr = CultureFieldList.Where(p => p.EntityName.Equals(entityName));
                if (idr.Count() > 0)
                    return GlobalVariable.IsDefaultCulture ? idr.First().DefaultField : (GlobalVariable.IsSecondCulture ? idr.First().SecondField : idr.First().ThirdField);
                else
                    return string.Format("{0}Name{1}", entityName, GlobalVariable.IsDefaultCulture ? "" : (GlobalVariable.IsSecondCulture ? "2" : "3"));
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
                return string.Format("{0}Name{1}", entityName, GlobalVariable.IsDefaultCulture ? "" : (GlobalVariable.IsSecondCulture ? "2" : "3"));
            }
        }
    }
}
