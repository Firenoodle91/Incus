using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Enum
{
    public enum ToolbarButton : int
    {
        Domain = 1000,
        Setting = 2000,
        Home = 3000,
        Logout = 4000,
        Refresh = 5000,        
        Save = 6000,
        Export = 7000,
        Print = 8000,
        Close = 9000,
        Pop=8888,
        Dashboard = 9999, // Dashboar Visible처리를 위해서
        Confirm = 9998 // Popup select confirm
    }
}
