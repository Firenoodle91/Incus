using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Interface.Forms
{
    public interface IToolBar
    {        
        void SetToolbarVisible(bool visible);

        void SetToolbarButtonVisible(bool visible);

        void SetToolbarButtonVisible(Enum.ToolbarButton button, bool visible);

        // void SetToolbarPermission();

        // MainForm에서 Child form의 UserRight로 설정할때 사용
        void SetToolbarPermission(Helper.IUserRight userRight); 
    }
}
