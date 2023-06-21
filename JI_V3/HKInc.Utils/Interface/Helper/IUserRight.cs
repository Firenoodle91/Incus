using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Interface.Helper
{
    public interface IUserRight
    {
        bool HasSelect { get; }
        bool HasEdit { get; }        
        bool HasPrint { get; }
        bool HasExport { get; }
        bool HasInsert { get; }
        bool HasReload { get; }
    }
}
