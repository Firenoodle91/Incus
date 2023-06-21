using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;

namespace HKInc.Service.Factory
{
    public class PopupFactory
    {
        public static IPopupCallbackForm GetPopupCallbackForm(PopupScreen screen, PopupDataParam map, PopupCallback callback)
        {
            return new HKInc.Service.Forms.MemoPopupForm(map, callback);                        
        }        
    }
}
