using System;
using DevExpress.XtraBars;
using HKInc.Service.Factory;

namespace HKInc.Service.Forms
{
    public class BarButton
    {                
        private BarButtonItem _toolbarButton;

        public BarButtonItem ToolbarButton
        {
            get { return _toolbarButton; }
            set { _toolbarButton = value; }
        }

        public Action<object, DevExpress.XtraBars.ItemClickEventArgs> OnClicked { get; set; }

        public Action ClickAction { get; set; }

        public string ToolbarButtonCaption
        {
            get { return _toolbarButton.Caption; }
            //set { this._toolbarButton.Caption = HelperFactory.GetLabelConvert().GetLabelText(value);}
            set { this._toolbarButton.Caption = string.Format("{0}[{1}]", HelperFactory.GetLabelConvert().GetLabelText(value), _toolbarButton.ShortcutKeyDisplayString); }
        }
    }
}
