using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Common
{
    public delegate void PopupCallback(object sender, PopupArgument e);

    public class PopupArgument : EventArgs
    {
        public Class.PopupDataParam Map { get; private set; }
        public PopupArgument(Class.PopupDataParam map)
        {
            this.Map = map;
        }
    }
}
