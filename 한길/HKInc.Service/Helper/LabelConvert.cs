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
using HKInc.Utils.Class;


namespace HKInc.Service.Helper
{
    class LabelConvert : ILabelConvert
    {
        private static List<FieldLabel> LabelTextList = new List<FieldLabel>();

        public static void GetLabelTextList()
        {
            try
            {
                LabelTextList.Clear();

                IRepository<FieldLabel> repository = new SystemRepository<FieldLabel>(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase));
                IEnumerable<FieldLabel> idr = repository.Find(p=>p.Active == "Y");
                foreach (FieldLabel label in idr)
                    LabelTextList.Add(repository.Detached(label));
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);            
            }
        }

        public void Reset() { GetLabelTextList(); }
        public static void ResetCode() { GetLabelTextList(); }

        public string GetLabelText(string key)
        {
            if (LabelTextList.Count == 0) GetLabelTextList();

            try
            {
                IEnumerable<FieldLabel> idr = LabelTextList.Where(p => p.FieldName.Equals(key));
                if (idr.Count() > 0)
                    return GlobalVariable.IsDefaultCulture ? idr.First().LabelText : (GlobalVariable.IsSecondCulture ? idr.First().LabelText2 : idr.First().LabelText3);
                else
                    return key.UpperToSpace();
            }
            catch(Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
                return key.UpperToSpace();
            }

        }                      
    }
}
