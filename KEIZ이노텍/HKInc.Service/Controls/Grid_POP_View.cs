using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Scrolling;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Service.Controls
{
    public class Grid_POP_View : GridView
    {
        public Grid_POP_View() : base()
        {
            this.OptionsView.ColumnAutoWidth = false;
        }
        public Grid_POP_View(GridControl grid) : base(grid) { }

        internal const string MyGridViewName = "GridPopView";
        protected override string ViewName { get { return MyGridViewName; } }

        protected override ScrollInfo CreateScrollInfo()
        {
            return new MyScrollInfo(this);
        }
    }

    public class Grid_POP_ViewInfoRegistrator : GridInfoRegistrator
    {
        public Grid_POP_ViewInfoRegistrator() : base() { }

        public override string ViewName { get { return Grid_POP_View.MyGridViewName; } }

        public override BaseView CreateView(GridControl grid)
        {
            return new Grid_POP_View(grid);
        }
    }
}
