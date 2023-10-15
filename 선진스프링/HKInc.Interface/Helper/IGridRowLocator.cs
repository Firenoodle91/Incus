using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Interface.Helper
{
    public interface IGridRowLocator
    {
        void GetCurrentRow();
        void SetCurrentRow();

        void SetCurrentKeyValue(string fieldName, object value);
        void GetCurrentRow(string fieldName);
        void GetCurrentRow(string fieldName, object value);
    }
}
