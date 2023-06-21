using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;

using HKInc.Service.Handler;
using HKInc.Utils.Enum;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Service.Factory;

namespace HKInc.Service.Base
{
    /// <summary>
    /// Grid 1개 템플릿
    /// </summary>
    public partial class ListFormTemplate : BaseForm 
    {
        // Grid에 사용할 BindingSource
        protected BindingSource GridBindingSource = new BindingSource();
        protected HKInc.Service.Controls.GridEx GridExControl;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator GridRowLocator;

        private RadioGroup _outputRadioGroup;
        private RadioGroupType _radioGroupType;

        protected Dictionary<string, Control> ControlEnableList = new Dictionary<string, Control>(); // 권한에 따라 Enable, Disable할 Control list 

        // Grid Toolbar에 사용된 버턴 권한 설정
        private bool _IsGridButtonExportEnabled;
        private bool _IsGridButtonFileChooseEnabled;

        protected bool IsGridButtonExportEnabled
        {
            get { return _IsGridButtonExportEnabled; }
            set
            {
                _IsGridButtonExportEnabled = value;
                GridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                GridExControl.ActExportClicked += GridEx1_ActExportClicked;
            }
        }

        protected bool IsGridButtonFileChooseEnabled
        {
            get { return _IsGridButtonFileChooseEnabled; }
            set
            {
                _IsGridButtonFileChooseEnabled = value;
                GridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                GridExControl.ActFileChooseClicked += GridEx1_ActFileChooseClicked;
            }
        }

        protected RadioGroup OutPutRadioGroup
        {
            get { return _outputRadioGroup; }
            set
            {
                _outputRadioGroup = value;
                if (_radioGroupType > 0)
                    HKInc.Service.Handler.RadioGroupHandler.SetRadioGroup(_outputRadioGroup, _radioGroupType);                
            }
        }

        protected RadioGroupType RadioGroupType
        {
            get { return _radioGroupType; }
            set
            {
                _radioGroupType = value;
                if(_outputRadioGroup != null )
                    HKInc.Service.Handler.RadioGroupHandler.SetRadioGroup(_outputRadioGroup, _radioGroupType);
            }
        }
        #region Construct
        public ListFormTemplate()
        {
            InitializeComponent();            
        }        
        
        #endregion
        protected override void InitControls()
        {
            AddControlList();

            if (FormMenu != null)
                UserRight = MenuFactory.GetUserRight(FormMenu.MenuId, GlobalVariable.UserId);

            // UserRight권한에 따라 Control 설정            
            foreach (var controlName in ControlEnableList.Keys)
            {
                ControlEnableList[controlName].Enabled = UserRight.HasEdit;
                if(UserRight.HasEdit)
                    ((BaseEdit)ControlEnableList[controlName]).EditValueChanged += ListFormTemplate_EditValueChanged;
            }
            // Print, Export에 사용될 오브젝트 설정            
            if (GridExControl != null)
            {
                PrintGridControl = GridExControl.MainGrid;

                GridExControl.MainGrid.Init();
                GridExControl.ActAddRowClicked += GridEx1_ActAddRowClicked;
                GridExControl.ActDeleteRowClicked += GridEx1_ActDeleteRowClicked;
                GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;

                // 20210825 오세완 차장 최종검사 팝업에서 오류가 발생해서 일단 회피 코드를 추가
                if(FormMenu != null)
                    GridExControl.Menu = FormMenu.MenuId;

                GridRowLocator = HelperFactory.GetGridRowLocator(GridExControl.MainGrid.MainView);
            }
        }

        protected override void InitToolbarButton()
        {
            // Grid Toolbar UserRight이용 권한 설정
            if(UserRight != null && GridExControl != null)
            {
                GridExControl.SetToolbarVisible(UserRight.HasEdit);
                GridExControl.SetToolbarButtonVisible(false);
                GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
                //IsGridButtonExportEnabled = true;
            }
        }

        protected override void InitGridLayoutRestore()
        {
            if (GridExControl != null)
                GridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★
        }
        #region Event Handler
        private void GridEx1_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try
            {
                DeleteRow();
                IsFormControlChanged = true;
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }       
        }

        private void GridEx1_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (!IsFirstLoaded) ActRefresh();
            if (GridExControl != null) GridExControl.MainGrid.PostEditor();
            try{ AddRowClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }

            //NewRowFlag Mapping
            NewRowFlagMapping(GridExControl);
        }

        protected void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView == null) return;

            if (e.Clicks == 2)
            {
                try{ GridRowDoubleClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
            }
        }

        private void GridEx1_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (GridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(GridExControl.MainGrid.MainView);
        }

        private void GridEx1_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (GridExControl != null) GridExControl.MainGrid.PostEditor();
            try { FileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        #endregion

        #region Function        
        protected virtual void OpenPopup(PopupDataParam param)
        {            
            param.SetValue(PopupParameter.UserRight, UserRight);
            param = AddServiceToPopupDataParam(param);
            if (param.Count == 0) return;
            IPopupForm form = GetPopupForm(param);
            try
            {
                ((Form)form).Text = GlobalVariable.IsDefaultCulture ? this.FormMenu.MenuName : (GlobalVariable.IsSecondCulture ? this.FormMenu.MenuName2 : this.FormMenu.MenuName3);
            
            ((Form)form).Icon = Icon.FromHandle(new Bitmap(MdiTabImage).GetHicon());
            }
            catch { }
            form.ShowPopup(true);
        }

        protected virtual void GridRowDoubleClicked()
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.EditMode, UserRight.HasEdit ? PopupEditMode.Update : PopupEditMode.ReadOnly);
            param.SetValue(PopupParameter.KeyValue, GridBindingSource.Current);

            OpenPopup(param);
        }

        protected virtual void AddRowClicked()
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);

            OpenPopup(param);
        }

        protected virtual void DeleteRow() { }
        protected virtual IPopupForm GetPopupForm(PopupDataParam param) { return new PopupCallbackFormTemplate(); }
        protected virtual PopupDataParam AddServiceToPopupDataParam(PopupDataParam param) { return new PopupDataParam(); }
        protected virtual void FileChooseClicked() { }

        // Userright권한에 따라 Enable, Disable할 Control List를 설정한다.
        protected virtual void AddControlList() { }
        #endregion

        protected void SetGridSortInfo(params string[] sortingColumns)
        {
            if(GridExControl != null)
            {
                GridView gv = GridExControl.MainGrid.MainView as GridView;
                if (gv == null) return;

                GridColumnSortInfo[] gridColumnSortInfo = new GridColumnSortInfo[sortingColumns.Length];
                for(int i = 0; i < sortingColumns.Length; i++)                
                    gridColumnSortInfo[i] = new GridColumnSortInfo(gv.Columns[sortingColumns[i]], ColumnSortOrder.Ascending);

                gv.SortInfo.ClearAndAddRange(gridColumnSortInfo);
            }            
        }

        protected void SetControlList(bool enabled)
        {
            foreach (var controlName in ControlEnableList.Keys)
                ControlEnableList[controlName].Enabled = enabled;
        }

        private void ListFormTemplate_EditValueChanged(object sender, EventArgs e)
        {
            if (!IsFirstLoaded) return;

            BaseEdit edit = sender as BaseEdit;
            if (edit == null) return;

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