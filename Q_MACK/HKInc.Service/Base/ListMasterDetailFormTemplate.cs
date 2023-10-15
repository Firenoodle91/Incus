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

using HKInc.Utils.Enum;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Service.Factory;
using HKInc.Service.Handler;

namespace HKInc.Service.Base
{
    public partial class ListMasterDetailFormTemplate : BaseForm
    {
        // Grid에 사용할 BindingSource
        protected BindingSource MasterGridBindingSource = new BindingSource();
        protected BindingSource DetailGridBindingSource = new BindingSource();
        protected HKInc.Service.Controls.GridEx MasterGridExControl;
        protected HKInc.Service.Controls.GridEx DetailGridExControl;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator GridRowLocator;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator DetailGridRowLocator;


        protected Dictionary<string, Control> ControlEnableList = new Dictionary<string, Control>(); // 권한에 따라 Enable, Disable할 Control list 

        private RadioGroup _outputRadioGroup;
        private RadioGroupType _radioGroupType;


        // Grid Toolbar에 사용된 버턴 권한 설정
        private bool _IsMasterGridButtonExportEnabled;
        private bool _IsMasterGridButtonFileChooseEnabled;
        private bool _IsDetailGridButtonExportEnabled;
        private bool _IsDetailGridButtonFileChooseEnabled;

        protected bool IsMasterGridButtonExportEnabled
        {
            get { return _IsMasterGridButtonExportEnabled; }
            set
            {
                _IsMasterGridButtonExportEnabled = value;
                MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                MasterGridExControl.ActExportClicked += GridEx1_ActExportClicked;
            }
        }

        protected bool IsMasterGridButtonFileChooseEnabled
        {
            get { return _IsMasterGridButtonFileChooseEnabled; }
            set
            {
                _IsMasterGridButtonFileChooseEnabled = value;
                MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                MasterGridExControl.ActFileChooseClicked += GridEx1Control_ActFileChooseClicked;
            }
        }

        
        protected bool IsDetailGridButtonExportEnabled
        {
            get { return _IsDetailGridButtonExportEnabled; }
            set
            {
                _IsDetailGridButtonExportEnabled = value;
                DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                DetailGridExControl.ActExportClicked += GridEx2_ActExportClicked;
            }
        }
        
        protected bool IsDetailGridButtonFileChooseEnabled
        {
            get { return _IsDetailGridButtonFileChooseEnabled; }
            set
            {
                _IsDetailGridButtonFileChooseEnabled = value;
                DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                DetailGridExControl.ActFileChooseClicked += GridEx2_ActFileChooseClicked;
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
                if (_outputRadioGroup != null)
                    HKInc.Service.Handler.RadioGroupHandler.SetRadioGroup(_outputRadioGroup, _radioGroupType);
            }
        }
        #region Construct
        public ListMasterDetailFormTemplate()
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
                if (UserRight.HasEdit)
                    ((BaseEdit)ControlEnableList[controlName]).EditValueChanged += ListFormTemplate_EditValueChanged;
            }
            // Print, Export에 사용될 오브젝트 설정            
            if (MasterGridExControl != null)
            {
                PrintGridControl = MasterGridExControl.MainGrid;

                MasterGridExControl.MainGrid.Init();
                MasterGridExControl.ActAddRowClicked += GridEx1_ActAddRowClicked;
                MasterGridExControl.ActDeleteRowClicked += GridEx1_ActDeleteRowClicked;
                MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;                

                GridRowLocator = HelperFactory.GetGridRowLocator(MasterGridExControl.MainGrid.MainView);
            }
            if (DetailGridExControl != null)
            {
                DetailGridExControl.MainGrid.Init();
                DetailGridExControl.ActAddRowClicked += GridEx2_ActAddRowClicked;
                DetailGridExControl.ActDeleteRowClicked += GridEx2_ActDeleteRowClicked;

                DetailGridRowLocator = HelperFactory.GetGridRowLocator(DetailGridExControl.MainGrid.MainView);
            }
        }

        protected override void InitToolbarButton()
        {
            // Grid Toolbar UserRight이용 권한 설정
            if (UserRight != null && MasterGridExControl != null)
            {
                MasterGridExControl.SetToolbarVisible(UserRight.HasEdit);
                MasterGridExControl.SetToolbarButtonVisible(false);
                MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
                IsMasterGridButtonExportEnabled = true;
            }
            if (UserRight != null && DetailGridExControl != null)
            {
                DetailGridExControl.SetToolbarVisible(UserRight.HasEdit);
                DetailGridExControl.SetToolbarButtonVisible(false);
                DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
                IsDetailGridButtonExportEnabled = true;
            }
    }

        protected override void InitGridLayoutRestore()
        {
            if (MasterGridExControl != null)
                MasterGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★

            if (DetailGridExControl != null)
                DetailGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★
        }
        #region Event Handler

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                bool tempIsFormControlChanged = IsFormControlChanged;

                MasterFocusedRowChanged();

                IsFormControlChanged = tempIsFormControlChanged ? true : false;
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
        }


        private void GridEx1_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try { DeleteRow(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx1_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (!IsFirstLoaded) ActRefresh();
            if (MasterGridExControl != null) MasterGridExControl.MainGrid.PostEditor();
            try { AddRowClicked();  } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx1_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (MasterGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(MasterGridExControl.MainGrid.MainView);
        }

        private void GridEx1Control_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (MasterGridExControl != null) MasterGridExControl.MainGrid.PostEditor();
            try { FileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx2_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try { DeleteDetailRow(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx2_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (DetailGridExControl != null) DetailGridExControl.MainGrid.PostEditor();
            try { DetailAddRowClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx2_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (DetailGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(DetailGridExControl.MainGrid.MainView);
        }

        private void GridEx2_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (DetailGridExControl != null) DetailGridExControl.MainGrid.PostEditor();
            try { DetailFileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        #endregion

        #region Function                

        protected virtual void AddRowClicked() { }
        protected virtual void DeleteRow() { }
        protected virtual void MasterFocusedRowChanged() { }
        protected virtual void FileChooseClicked() { }

        protected virtual void DetailAddRowClicked() { }
        protected virtual void DeleteDetailRow() { }
        protected virtual void DetailFileChooseClicked() { }

        protected virtual void AddControlList() { }
        #endregion
        
        protected void SetGridSortInfo(params string[] sortingColumns)
        {
            if (MasterGridExControl != null)
            {
                GridView gv = MasterGridExControl.MainGrid.MainView as GridView;
                if (gv == null) return;

                SetGridViewSortInfo(gv, sortingColumns);
            }
        }

        protected void SetDetailGridSortInfo(params string[] sortingColumns)
        {
            if (DetailGridExControl != null)
            {
                GridView gv = DetailGridExControl.MainGrid.MainView as GridView;
                if (gv == null) return;

                SetGridViewSortInfo(gv, sortingColumns);
            }
        }

        private void SetGridViewSortInfo(GridView gv, string[] sortingColumns)
        {

            GridColumnSortInfo[] gridColumnSortInfo = new GridColumnSortInfo[sortingColumns.Length];
            for (int i = 0; i < sortingColumns.Length; i++)
                gridColumnSortInfo[i] = new GridColumnSortInfo(gv.Columns[sortingColumns[i]], ColumnSortOrder.Ascending);

            gv.SortInfo.ClearAndAddRange(gridColumnSortInfo);
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
            
            BindingSource CurrentBindingSoruce = edit.DataBindings[0].DataSource as BindingSource;
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