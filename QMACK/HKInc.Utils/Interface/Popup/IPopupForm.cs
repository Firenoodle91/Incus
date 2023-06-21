using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Interface.Popup
{
    public interface IPopupForm
    {        
        void ShowPopup(bool isDialog);

        void SetGridFilter(string filterString);
    }
}
