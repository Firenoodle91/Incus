using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.XtraEditors;

namespace HKInc.Interface.Helper
{
    public interface IDateEditMask
    {
        void SetEditMask(DateEdit editor, HKInc.Utils.Enum.DateFormat dateFormat);
        void SetMaskFormat(HKInc.Utils.Enum.DateFormat dateFormat);
    }
}
