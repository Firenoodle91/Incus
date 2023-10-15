using System;
using DevExpress.XtraGrid.Columns;

using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;

namespace HKInc.Service.Forms
{
    public partial class MemoPopupForm : Base.PopupCallbackFormTemplate
    {
        private int focusedRow;        
        private GridColumn focusedGridColumn;
        private DataParam dataParam;

        public MemoPopupForm(PopupDataParam parameter, PopupCallback callback) :base(parameter, callback)
        {
            InitializeComponent();

            this.focusedRow = PopupParam.GetValue(PopupParameter.FocusedRow).GetIntNullToZero();            
            this.focusedGridColumn = PopupParam.GetValue(PopupParameter.FocusedGridColumn) as GridColumn;
            this.dataParam = PopupParam.GetValue(PopupParameter.DataParam) as DataParam;            
        }

        protected override void InitControls() { }

        protected override void ExecuteCallback(PopupArgument arg) { }

        protected override void AddControlList()
        {
            ControlEnableList.Add("Memo", memoEdit);
        }

        protected override void InitToolbarButton()
        {                        
            SetToolbarVisible(true);
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
            memoEdit.ReadOnly = true;

            DataParam map = PopupParam.GetValue(PopupParameter.DataParam) as DataParam;
 
            if (map != null) memoEdit.EditValue = map.GetValue(focusedGridColumn.FieldName);
            if ((PopupEditMode)PopupParam.GetValue(PopupParameter.EditMode) != PopupEditMode.ReadOnly)
            {
                SetToolbarButtonVisible(ToolbarButton.Save, true);                
                memoEdit.ReadOnly = false;
            }            
        }

        protected override void DataSave()
        {
            DataParam param = new DataParam();
            param.SetValue(focusedGridColumn.FieldName, memoEdit.EditValue);

            PopupParam.SetValue(PopupParameter.DataParam, param);

            if (Callback != null) Callback(this, new PopupArgument(PopupParam));

            Close();
        }
    }
}