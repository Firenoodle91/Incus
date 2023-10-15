using System;
using DevExpress.XtraGrid.Columns;

using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;

using DevExpress.XtraTreeList.Columns;

namespace HKInc.Service.Forms
{
    public partial class MemoPopupForm : Base.PopupCallbackFormTemplate
    {
        private int focusedRow;
        private GridColumn focusedGridColumn;
        private DataParam dataParam;

        /// <summary>
        /// 20210118 오세완 차장
        /// TreeListEx에서 Memo popup을 사용하는 경우 Column 저장용
        /// </summary>
        private TreeListColumn focusedTreeColumn;

        /// <summary>
        /// 20210118 오세완 차장
        /// TreeListEx에서 Memo popup을 사용하는 경우 해당 column으로 초기화를 구분하기 위해서 사용
        /// </summary>
        public bool UseTreeColumn { get; set; }

        public MemoPopupForm(PopupDataParam parameter, PopupCallback callback) : base(parameter, callback)
        {
            InitializeComponent();

            this.focusedRow = PopupParam.GetValue(PopupParameter.FocusedRow).GetIntNullToZero();
            this.focusedGridColumn = PopupParam.GetValue(PopupParameter.FocusedGridColumn) as GridColumn;
            this.dataParam = PopupParam.GetValue(PopupParameter.DataParam) as DataParam;
        }

        /// <summary>
        /// 20210119 오세완 차장
        /// 트리컨트롤에서 사용하는 경우는 초기화를 다르게 하기 위하여 생성
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="callback"></param>
        /// <param name="bUseTree">true로 설정하여 트리컨트롤로 사용한다고 표시함</param>
        public MemoPopupForm(PopupDataParam parameter, PopupCallback callback, bool bUseTree) : base(parameter, callback)
        {
            InitializeComponent();
            this.focusedRow = PopupParam.GetValue(PopupParameter.FocusedRow).GetIntNullToZero();
            UseTreeColumn = bUseTree;
            if (UseTreeColumn)
            {
                this.focusedTreeColumn = PopupParam.GetValue(PopupParameter.FocusedGridColumn) as TreeListColumn;
            }
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
            memoEdit.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            DataParam map = PopupParam.GetValue(PopupParameter.DataParam) as DataParam;

            // 20210118 오세완 차장 tree도 공용으로 사용하게 수정처리
            //if (map != null) memoEdit.EditValue = map.GetValue(focusedGridColumn.FieldName);
            if (map != null)
            {
                if (UseTreeColumn)
                {
                    memoEdit.EditValue = map.GetValue(focusedTreeColumn.FieldName);
                }
                else
                {
                    memoEdit.EditValue = map.GetValue(focusedGridColumn.FieldName);
                }
            }

            if ((PopupEditMode)PopupParam.GetValue(PopupParameter.EditMode) != PopupEditMode.ReadOnly)
            {
                SetToolbarButtonVisible(ToolbarButton.Save, true);
                memoEdit.ReadOnly = false;
            }
        }

        protected override void DataSave()
        {
            DataParam param = new DataParam();
            // 20210119 오세완 차장 tree구조도 공용으로 사용해야 해서 변경
            //param.SetValue(focusedGridColumn.FieldName, memoEdit.EditValue);
            if (UseTreeColumn)
            {
                param.SetValue(focusedTreeColumn.FieldName, memoEdit.EditValue);
            }
            else
            {
                param.SetValue(focusedGridColumn.FieldName, memoEdit.EditValue);
            }

            PopupParam.SetValue(PopupParameter.DataParam, param);

            if (Callback != null) Callback(this, new PopupArgument(PopupParam));

            Close();
        }
    }
}