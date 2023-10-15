using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Service.Controls
{
    [ToolboxItem(true)]
    public class Gird_POP_Default : GridControl
    {
        public Gird_POP_Default() : base() { }

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new Grid_POP_ViewInfoRegistrator());
        }
    }
}
