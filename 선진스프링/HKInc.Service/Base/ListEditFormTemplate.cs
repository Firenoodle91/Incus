using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;

using HKInc.Service.Handler;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Popup;

namespace HKInc.Service.Base
{
    public partial class ListEditFormTemplate : BaseForm, IPopupCallbackForm
    {
        protected PopupCallback Callback;            // Callback함수 부모윈도우에서 넘어 온다
        protected PopupDataParam PopupParam;         // Parameter
        protected PopupArgument ReturnPopupArgument; // Callback에 같이 보낼 parameter
        protected PopupEditMode EditMode;            // ReadOnly, new
        protected BindingSource ModelBindingSource = new BindingSource();  // Control에 사용한 BindingSource 다자인때 설정한 것이므로 컨스트럭터에서 설정한다.
        protected Dictionary<string, Control> ControlEnableList = new Dictionary<string, Control>(); // 권한에 따라 Enable, Disable할 Control list 
        protected HKInc.Service.Controls.GridEx GridExControl;
        protected string ActiveFilter = string.Empty;

        public ListEditFormTemplate()
        {
            InitializeComponent();
        }

        public ListEditFormTemplate(PopupDataParam parameter, PopupCallback callback) :this()
        {            
            this.PopupParam = parameter;
            this.Callback = callback;
        }

        protected override void InitControls()
        {
            AddControlList();            

            // UserRight에 값이 들어갈대 Toolbar가 설정된다.
            if (PopupParam != null)
            {
                UserRight = (IUserRight)PopupParam.GetValue(PopupParameter.UserRight);
                EditMode = (PopupEditMode)PopupParam.GetValue(PopupParameter.EditMode);

                // UserRight권한에 따라 Control 설정
                foreach (var controlName in ControlEnableList.Keys)
                {
                    ControlEnableList[controlName].Enabled = UserRight.HasEdit;
                    if (UserRight.HasEdit)
                        ((BaseEdit)ControlEnableList[controlName]).EditValueChanged += ListFormTemplate_EditValueChanged;
                }
            }
        }
        
        protected override void InitDataLoad()
        {
            ActRefresh();
            SetToolbarButtonVisible(ToolbarButton.Refresh, false);
            SetToolbarButtonVisible(ToolbarButton.Export, false);
            SetToolbarButtonVisible(ToolbarButton.Print, false);
        }

        protected override void InitToolbarButton()
        {
            // Grid Toolbar UserRight이용 권한 설정
            if (UserRight != null && GridExControl != null)
            {
                PrintGridControl = GridExControl.MainGrid;

                GridExControl.SetToolbarVisible(UserRight.HasEdit);
                GridExControl.SetToolbarButtonVisible(false);
                GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
           
                GridExControl.ActAddRowClicked += GridEx1_ActAddRowClicked;
                GridExControl.ActDeleteRowClicked += GridEx1_ActDeleteRowClicked;                
            }
        }
        
        protected new virtual void ActSave()
        {
            try
            {
                WaitHandler.ShowWait();
                if (ModelBindingSource != null) ModelBindingSource.EndEdit();
                    
                DataSave();
                SetSaveMessage();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
            finally
            {
                WaitHandler.CloseWait();
            }
        }

        // Callback실행한다. Form_Closed Event에서  Call한다. ReturnPopupArgument를 인수로 넘긴다.
        protected virtual void ExecuteCallback(PopupArgument arg) { if (Callback != null) Callback(this, arg); }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            ExecuteCallback(ReturnPopupArgument); // Callback함수 실행
        }
        
        #region IPopupCallbackForm 구현
        public virtual void ShowPopup(bool isDialog = true)
        {
            try
            {
                if (isDialog)
                    ShowDialog();
                else
                    Show();
            }
            catch(Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
        }
        public virtual void SetGridFilter(string filterString)
        {
            ActiveFilter = filterString;
        }
        //Callback설정
        public virtual void SetPopupCallback(PopupCallback callback) { this.Callback = callback; }
        #endregion

        // Userright권한에 따라 Enable, Disable할 Control List를 설정한다.
        protected virtual void AddControlList() { }


        #region GridToolbar Button Click Event
        protected virtual void GridEx1_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            try { DeleteGridRow(); }
            catch (Exception ex) { HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex); }
        }

        protected virtual void GridEx1_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            try{ AddGridRow(); }
            catch (Exception ex) { HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex); }
        }

        protected virtual void DeleteGridRow() { }
        protected virtual void AddGridRow() { }
        #endregion

        #region Function OpenPopup
        protected virtual void OpenPopup(PopupDataParam param)
        {
            param.SetValue(PopupParameter.UserRight, UserRight);
            param = AddServiceToPopupDataParam(param);

            IPopupForm form = GetPopupForm(param);
            ((Form)form).Text = this.Text;

            form.ShowPopup(true);
        }
        protected virtual IPopupForm GetPopupForm(PopupDataParam param) { return new PopupCallbackFormTemplate(); }
        protected virtual PopupDataParam AddServiceToPopupDataParam(PopupDataParam param) { return new PopupDataParam(); }
        #endregion

        protected void SetGridSortInfo(params string[] sortingColumns)
        {
            if (GridExControl != null)
            {
                GridView gv = GridExControl.MainGrid.MainView as GridView;
                if (gv == null) return;

                GridColumnSortInfo[] gridColumnSortInfo = new GridColumnSortInfo[sortingColumns.Length];
                for (int i = 0; i < sortingColumns.Length; i++)
                    gridColumnSortInfo[i] = new GridColumnSortInfo(gv.Columns[sortingColumns[i]], ColumnSortOrder.Ascending);

                gv.SortInfo.ClearAndAddRange(gridColumnSortInfo);
            }
        }

        private void ListFormTemplate_EditValueChanged(object sender, EventArgs e)
        {
            if (!IsFirstLoaded) return;

            BaseEdit edit = sender as BaseEdit;
            if (edit == null) return;

            if (edit.DataBindings["EditValue"] == null) return;

            BindingSource CurrentBindingSoruce = edit.DataBindings["EditValue"].DataSource as BindingSource;
            if (CurrentBindingSoruce.Current == null) return;

            object currentObj = CurrentBindingSoruce.Current;
            Type type = currentObj.GetType();

            PropertyInfo updateId = type.GetProperty("UpdateId");
            PropertyInfo updateTime = type.GetProperty("UpdateTime");

            updateId.SetValue(currentObj, HKInc.Utils.Common.GlobalVariable.LoginId);
            updateTime.SetValue(currentObj, DateTime.Now);

            ((HKInc.Utils.Interface.Forms.IFormControlChanged)this.FindForm()).SetIsFormControlChanged(true);

            IsFormControlChanged = true;
        }
    }
}