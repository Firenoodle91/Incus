using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Interface.Handler
{
    public interface IMainFormMessage
    {
        void SetMessage(string message);

        System.Windows.Forms.Form FlyoutMainForm { get; }  // flyout을 소유하고 있는 form
        DevExpress.Utils.FlyoutPanel FlyMsg { get; }
        DevExpress.XtraEditors.LabelControl LabelTitle { get; }
        DevExpress.XtraEditors.LabelControl LabelText { get; }
    }
}
