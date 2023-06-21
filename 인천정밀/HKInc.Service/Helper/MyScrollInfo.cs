using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Scrolling;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKInc.Service.Helper
{
    /// <summary>
    /// Grid 스크롤 너비
    /// </summary>
    public class MyScrollInfo : ScrollInfo
    {
        public MyScrollInfo(BaseView view) : base(view) { }

        protected override int HScrollHeight
        {
            get
            {
                return SystemInformation.VerticalScrollBarWidth * 2;
            }
        }

        protected override VCrkScrollBar CreateVScroll()
        {
            return new MyVCrkScrollBar(this);
        }

        protected override HCrkScrollBar CreateHScroll()
        {
            return new MyHCrkScrollBar(this);
        }

        public override int VScrollSize
        {
            get { return SystemInformation.VerticalScrollBarWidth * 2; }
        }


    }

    public class MyVCrkScrollBar : VCrkScrollBar
    {
        public MyVCrkScrollBar(ScrollInfo scrollInfo) : base(scrollInfo) { }

        protected override ScrollBarViewInfo CreateScrollBarViewInfo()
        {
            return new MyScrollBarViewinfo(this);
        }
    }

    public class MyHCrkScrollBar : HCrkScrollBar
    {
        public MyHCrkScrollBar(ScrollInfo scrollInfo) : base(scrollInfo) { }

        protected override ScrollBarViewInfo CreateScrollBarViewInfo()
        {
            return new MyScrollBarViewinfo(this);
        }
    }

    public class MyScrollBarViewinfo : ScrollBarViewInfo
    {
        public MyScrollBarViewinfo(IScrollBar scrollBar) : base(scrollBar) { }

        public override int ButtonWidth
        {
            get { return SystemInformation.VerticalScrollBarArrowHeight * 2; }
        }
    }
}
