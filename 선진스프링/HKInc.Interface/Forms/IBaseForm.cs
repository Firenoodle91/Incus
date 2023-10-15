using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HKInc.Interface.Forms
{
    public interface IBaseForm : IToolBar
    {
        Helper.IUserRight UserRight { get; set; }

        // Menu class to open this form
        HKInc.Ui.Model.Domain.Menu FormMenu { get; set; }

        Image MdiTabImage { get; set; }        

        // MainForm Toolbar button click event시 호출
        // Refresh, Save, Export, Print
        void ToolbarButtonClicked(Enum.ToolbarButton toobarButton);

        bool isFormOpen { get; set; }
    }
}
