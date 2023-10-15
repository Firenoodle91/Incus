using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Interface.Forms
{
    public interface IFormControlChanged
    {
        void SetIsFormControlChanged(bool changed);
        bool GetIsFormControlChanged();
    }
}
