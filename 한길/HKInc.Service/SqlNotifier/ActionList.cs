using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Utils.Interface.Handler;

namespace HKInc.Service.SqlNotifier
{
    public class ActionList
    {           
        private readonly Dictionary<string, Action> DicCacheResetActionList = new Dictionary<string, Action>
        {
            // 처리해야할 Action Deligate 작성
            {"CodeMaster",      () => Helper.MasterCode.ResetCode()},
            {"FieldLabel",       () => Helper.LabelConvert.ResetCode()},
            {"StandardMessage", () => Helper.MessageHelper.ResetCode()}
        };
                        
        public ActionList(IReloadDashboard form)
        {              
            // Thread 이므로 Ivoke처리 해야 됨
            // Dashboard처리 actino추가
            Action actionDashboard = () => { form.ReloadDashboard(); };
            Action actionNotice = () => { form.ReloadNotice(); };
                        
            DicCacheResetActionList.Add("Notice", () => { ((System.Windows.Forms.Control)form).Invoke(actionNotice); });            
        }

        public bool GetResetAction(string queTable, out Action resetAction)
        {
            if (DicCacheResetActionList.TryGetValue(queTable, out resetAction))
                return true;
            else
                return false;
        }
    }
}
