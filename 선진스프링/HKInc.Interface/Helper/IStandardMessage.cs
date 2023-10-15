using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Interface.Helper
{
    public interface IStandardMessage
    {
        string GetStandardMessage(int messageId);
        void Reset();
    }
}
