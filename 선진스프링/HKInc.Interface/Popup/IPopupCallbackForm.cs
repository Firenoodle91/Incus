using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Interface.Popup
{
    public interface IPopupCallbackForm : IPopupForm
    {
        void SetPopupCallback(Common.PopupCallback callback);
    }
}
