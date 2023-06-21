using System;
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
    public partial class ListMasterDetailDetailDetailSubTreeFormTemplate : BaseForm 
    {
        // Grid에 사용할 BindingSource
        protected BindingSource MasterGridBindingSource = new BindingSource();
        protected BindingSource DetailGridBindingSource = new BindingSource();
        protected BindingSource SubDetailGridBindingSource = new BindingSource();
        protected BindingSource TreeDetailGridBindingSource = new BindingSource();
        protected BindingSource DetailTreeDetailGridBindingSource = new BindingSource();
        protected BindingSource SubTreeDetailGridBindingSource = new BindingSource();

        protected HKInc.Service.Controls.GridEx MasterGridExControl;
        protected HKInc.Service.Controls.GridEx DetailGridExControl;
        protected HKInc.Service.Controls.GridEx SubDetailGridExControl;
        protected HKInc.Service.Controls.GridEx TreeDetailGridExControl;
        protected HKInc.Service.Controls.GridEx DetailTreeDetailGridExControl;
        protected HKInc.Service.Controls.GridEx SubTreeDetailGridExControl;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator GridRowLocator;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator DetailGridRowLocator;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator SubDetailGridRowLocator;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator TreeDetailGridRowLocator;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator DetailTreeDetailGridRowLocator;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator SubTreeDetailGridRowLocator;

        private RadioGroup _outputRadioGroup;
        private RadioGroupType _radioGroupType;

        
        // Grid Toolbar에 사용된 버턴 권한 설정
        private bool _IsMasterGridButtonExportEnabled;
        private bool _IsMasterGridButtonFileChooseEnabled;
        private bool _IsDetailGridButtonExportEnabled;
        private bool _IsDetailGridButtonFileChooseEnabled;
        private bool _IsSubDetailGridButtonExportEnabled;
        private bool _IsSubDetailGridButtonFileChooseEnabled;
        private bool _IsTreeDetailGridButtonExportEnabled;
        private bool _IsTreeDetailGridButtonFileChooseEnabled;
        private bool _IsDetailTreeDetailGridButtonExportEnabled;
        private bool _IsDetailTreeDetailGridButtonFileChooseEnabled;
        private bool _IsSubTreeDetailGridButtonExportEnabled;
        private bool _IsSubTreeDetailGridButtonFileChooseEnabled;

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
                DetailGridExControl.SetToolbarShotKeyChange(GridToolbarButton.Export, new BarShortcut((Keys.Alt | Keys.E)));
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
                DetailGridExControl.SetToolbarShotKeyChange(GridToolbarButton.FileChoose, new BarShortcut((Keys.Alt | Keys.R)));
                DetailGridExControl.ActFileChooseClicked += GridEx2_ActFileChooseClicked;
            }
        }

        protected bool IsSubDetailGridButtonExportEnabled
        {
            get { return _IsSubDetailGridButtonExportEnabled; }
            set
            {
                _IsSubDetailGridButtonExportEnabled = value;
                SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                SubDetailGridExControl.SetToolbarShotKeyChange(GridToolbarButton.Export, new BarShortcut((Keys.Alt | Keys.D)));
                SubDetailGridExControl.ActExportClicked += GridEx3_ActExportClicked;
            }
        }

        protected bool IsSubDetailGridButtonFileChooseEnabled
        {
            get { return _IsSubDetailGridButtonFileChooseEnabled; }
            set
            {
                _IsSubDetailGridButtonFileChooseEnabled = value;
                SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                SubDetailGridExControl.SetToolbarShotKeyChange(GridToolbarButton.FileChoose, new BarShortcut((Keys.Alt | Keys.F)));
                SubDetailGridExControl.ActFileChooseClicked += GridEx3_ActFileChooseClicked;
            }
        }

        protected bool IsTreeDetailGridButtonExportEnabled
        {
            get { return _IsTreeDetailGridButtonExportEnabled; }
            set
            {
                _IsTreeDetailGridButtonExportEnabled = value;
                TreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                TreeDetailGridExControl.SetToolbarShotKeyChange(GridToolbarButton.Export, new BarShortcut((Keys.Alt | Keys.C)));
                TreeDetailGridExControl.ActExportClicked += GridEx4_ActExportClicked;
            }
        }

        protected bool IsTreeDetailGridButtonFileChooseEnabled
        {
            get { return _IsTreeDetailGridButtonFileChooseEnabled; }
            set
            {
                _IsTreeDetailGridButtonFileChooseEnabled = value;
                TreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                TreeDetailGridExControl.SetToolbarShotKeyChange(GridToolbarButton.FileChoose, new BarShortcut((Keys.Alt | Keys.V)));
                TreeDetailGridExControl.ActFileChooseClicked += GridEx4_ActFileChooseClicked;
            }
        }

        protected bool IsDetailTreeDetailGridButtonExportEnabled
        {
            get { return _IsDetailTreeDetailGridButtonExportEnabled; }
            set
            {
                _IsDetailTreeDetailGridButtonExportEnabled = value;
                DetailTreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                DetailTreeDetailGridExControl.ActExportClicked += GridEx5_ActExportClicked;
            }
        }

        protected bool IsDetailTreeDetailGridButtonFileChooseEnabled
        {
            get { return _IsDetailTreeDetailGridButtonFileChooseEnabled; }
            set
            {
                _IsDetailTreeDetailGridButtonFileChooseEnabled = value;
                DetailTreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                DetailTreeDetailGridExControl.ActFileChooseClicked += GridEx5_ActFileChooseClicked;
            }
        }

        protected bool IsSubTreeDetailGridButtonExportEnabled
        {
            get { return _IsSubTreeDetailGridButtonExportEnabled; }
            set
            {
                _IsSubTreeDetailGridButtonExportEnabled = value;
                SubTreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                SubTreeDetailGridExControl.ActExportClicked += GridEx6_ActExportClicked;
            }
        }

        protected bool IsSubTreeDetailGridButtonFileChooseEnabled
        {
            get { return _IsSubTreeDetailGridButtonFileChooseEnabled; }
            set
            {
                _IsSubTreeDetailGridButtonFileChooseEnabled = value;
                SubTreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                SubTreeDetailGridExControl.ActFileChooseClicked += GridEx6_ActFileChooseClicked;
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
        public ListMasterDetailDetailDetailSubTreeFormTemplate()
        {
            InitializeComponent();            
        }        
        
        #endregion
        protected override void InitControls()
        {
            if(FormMenu != null)
                UserRight = MenuFactory.GetUserRight(FormMenu.MenuId, GlobalVariable.UserId);

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
                DetailGridExControl.MainGrid.MainView.FocusedRowChanged += DetailView_FocusedRowChanged;

                DetailGridRowLocator = HelperFactory.GetGridRowLocator(DetailGridExControl.MainGrid.MainView);
            }
            if (SubDetailGridExControl != null)
            {
                SubDetailGridExControl.MainGrid.Init();
                SubDetailGridExControl.ActAddRowClicked += GridEx3_ActAddRowClicked;
                SubDetailGridExControl.ActDeleteRowClicked += GridEx3_ActDeleteRowClicked;
                SubDetailGridExControl.MainGrid.MainView.FocusedRowChanged += SubDetailView_FocusedRowChanged;

                SubDetailGridRowLocator = HelperFactory.GetGridRowLocator(SubDetailGridExControl.MainGrid.MainView);
            }
            if (TreeDetailGridExControl != null)
            {
                TreeDetailGridExControl.MainGrid.Init();
                TreeDetailGridExControl.ActAddRowClicked += GridEx4_ActAddRowClicked;
                TreeDetailGridExControl.ActDeleteRowClicked += GridEx4_ActDeleteRowClicked;

                TreeDetailGridRowLocator = HelperFactory.GetGridRowLocator(TreeDetailGridExControl.MainGrid.MainView);
            }

            if (DetailTreeDetailGridExControl != null)
            {
                DetailTreeDetailGridExControl.MainGrid.Init();
                DetailTreeDetailGridExControl.ActAddRowClicked += GridEx5_ActAddRowClicked;
                DetailTreeDetailGridExControl.ActDeleteRowClicked += GridEx5_ActDeleteRowClicked;

                TreeDetailGridRowLocator = HelperFactory.GetGridRowLocator(DetailTreeDetailGridExControl.MainGrid.MainView);
            }

            if (SubTreeDetailGridExControl != null)
            {
                SubTreeDetailGridExControl.MainGrid.Init();
                SubTreeDetailGridExControl.ActAddRowClicked += GridEx6_ActAddRowClicked;
                SubTreeDetailGridExControl.ActDeleteRowClicked += GridEx6_ActDeleteRowClicked;

                SubTreeDetailGridRowLocator = HelperFactory.GetGridRowLocator(SubTreeDetailGridExControl.MainGrid.MainView);
            }
        }

        protected override void InitToolbarButton()
        {
            // Grid Toolbar UserRight이용 권한 설정
            if(UserRight != null && MasterGridExControl != null)
            {
                MasterGridExControl.SetToolbarVisible(UserRight.HasEdit);
                MasterGridExControl.SetToolbarButtonVisible(false);
                MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
                //IsMasterGridButtonExportEnabled = true;
            }
            if (UserRight != null && DetailGridExControl != null)
            {
                DetailGridExControl.SetToolbarVisible(UserRight.HasEdit);
                DetailGridExControl.SetToolbarButtonVisible(false);
                DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
                //IsDetailGridButtonExportEnabled = true;
            }
            if (UserRight != null && SubDetailGridExControl != null)
            {
                SubDetailGridExControl.SetToolbarVisible(UserRight.HasEdit);
                SubDetailGridExControl.SetToolbarButtonVisible(false);
                SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
                //IsSubDetailGridButtonExportEnabled = true;
            }
            if (UserRight != null && TreeDetailGridExControl != null)
            {
                TreeDetailGridExControl.SetToolbarVisible(UserRight.HasEdit);
                TreeDetailGridExControl.SetToolbarButtonVisible(false);
                TreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                TreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
                //IsTreeDetailGridButtonExportEnabled = true;
            }
            if (UserRight != null && DetailTreeDetailGridExControl != null)
            {
                DetailTreeDetailGridExControl.SetToolbarVisible(UserRight.HasEdit);
                DetailTreeDetailGridExControl.SetToolbarButtonVisible(false);
                DetailTreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                DetailTreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
                //IsDetailTreeDetailGridButtonExportEnabled = true;
            }
            if (UserRight != null && SubTreeDetailGridExControl != null)
            {
                SubTreeDetailGridExControl.SetToolbarVisible(UserRight.HasEdit);
                SubTreeDetailGridExControl.SetToolbarButtonVisible(false);
                SubTreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                SubTreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
                //IsSubTreeDetailGridButtonExportEnabled = true;
            }
    }

    protected override void InitGridLayoutRestore()
        {
            if (MasterGridExControl != null)
                MasterGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★

            if (DetailGridExControl != null)
                DetailGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★

            if (SubDetailGridExControl != null)
                SubDetailGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★

            if (TreeDetailGridExControl != null)
                TreeDetailGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★

            if (DetailTreeDetailGridExControl != null)
                DetailTreeDetailGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★

            if (SubTreeDetailGridExControl != null)
                SubTreeDetailGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★
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

        private void DetailView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                bool tempIsFormControlChanged = IsFormControlChanged;

                DetailFocusedRowChanged();

                IsFormControlChanged = tempIsFormControlChanged ? true : false;
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
        }

        private void SubDetailView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                bool tempIsFormControlChanged = IsFormControlChanged;

                SubDetailFocusedRowChanged();

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
            try { AddRowClicked(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }

            //NewRowFlag Mapping
            NewRowFlagMapping(MasterGridExControl);
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
            try { DetailAddRowClicked(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }

            //NewRowFlag Mapping
            NewRowFlagMapping(DetailGridExControl);
        }

        private void GridEx3_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (SubDetailGridExControl != null) SubDetailGridExControl.MainGrid.PostEditor();
            try { SubDetailAddRowClicked(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }

            //NewRowFlag Mapping
            NewRowFlagMapping(SubDetailGridExControl);
        }

        private void GridEx3_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try{ DeleteSubDetailRow(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx4_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (TreeDetailGridExControl != null) TreeDetailGridExControl.MainGrid.PostEditor();
            try { TreeDetailAddRowClicked(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }

            //NewRowFlag Mapping
            NewRowFlagMapping(TreeDetailGridExControl);
        }
        private void GridEx4_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try { DeleteTreeDetailRow(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx5_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (DetailTreeDetailGridExControl != null) DetailTreeDetailGridExControl.MainGrid.PostEditor();
            try { DetailTreeDetailAddRowClicked(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }

            //NewRowFlag Mapping
            NewRowFlagMapping(DetailTreeDetailGridExControl);
        }
        private void GridEx5_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try { DeleteDetailTreeDetailRow(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx6_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (SubTreeDetailGridExControl != null) SubTreeDetailGridExControl.MainGrid.PostEditor();
            try { SubTreeDetailAddRowClicked(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }

            //NewRowFlag Mapping
            NewRowFlagMapping(SubTreeDetailGridExControl);
        }
        private void GridEx6_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try { DeleteSubTreeDetailRow(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx2_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (DetailGridExControl != null) DetailGridExControl.MainGrid.PostEditor();
            try { DetailFileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }
        private void GridEx2_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (DetailGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(DetailGridExControl.MainGrid.MainView);
        }

        private void GridEx3_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (SubDetailGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(SubDetailGridExControl.MainGrid.MainView);
        }
        private void GridEx3_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (SubDetailGridExControl != null) SubDetailGridExControl.MainGrid.PostEditor();
            try { SubDetailFileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx4_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (TreeDetailGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(TreeDetailGridExControl.MainGrid.MainView);
        }
        private void GridEx4_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (TreeDetailGridExControl != null) TreeDetailGridExControl.MainGrid.PostEditor();
            try { TreeDetailFileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }
        private void GridEx5_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (DetailTreeDetailGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(DetailTreeDetailGridExControl.MainGrid.MainView);
        }
        private void GridEx5_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (DetailTreeDetailGridExControl != null) DetailTreeDetailGridExControl.MainGrid.PostEditor();
            try { DetailTreeDetailFileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }
        private void GridEx6_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (SubTreeDetailGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(SubTreeDetailGridExControl.MainGrid.MainView);
        }
        private void GridEx6_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (SubTreeDetailGridExControl != null) SubTreeDetailGridExControl.MainGrid.PostEditor();
            try { SubTreeDetailFileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }
        #endregion

        #region Function                

        protected virtual void AddRowClicked() { }
        protected virtual void DeleteRow() { }
        protected virtual void MasterFocusedRowChanged() { }
        protected virtual void FileChooseClicked() { }

        protected virtual void DetailAddRowClicked() { }
        protected virtual void DeleteDetailRow() { }
        protected virtual void DetailFocusedRowChanged() { }
        protected virtual void DetailFileChooseClicked() { }

        protected virtual void SubDetailAddRowClicked() { }
        protected virtual void DeleteSubDetailRow() { }
        protected virtual void SubDetailFocusedRowChanged() { }
        protected virtual void SubDetailFileChooseClicked() { }

        protected virtual void TreeDetailAddRowClicked() { }
        protected virtual void DeleteTreeDetailRow() { }
        protected virtual void TreeDetailFileChooseClicked() { }

        protected virtual void DetailTreeDetailAddRowClicked() { }
        protected virtual void DeleteDetailTreeDetailRow() { }
        protected virtual void DetailTreeDetailFileChooseClicked() { }

        protected virtual void SubTreeDetailAddRowClicked() { }
        protected virtual void DeleteSubTreeDetailRow() { }
        protected virtual void SubTreeDetailFileChooseClicked() { }
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

        protected void SetSubDetailGridSortInfo(params string[] sortingColumns)
        {
            if (SubDetailGridExControl != null)
            {
                GridView gv = SubDetailGridExControl.MainGrid.MainView as GridView;
                if (gv == null) return;

                SetGridViewSortInfo(gv, sortingColumns);
            }
        }

        protected void SetTreeDetailGridSortInfo(params string[] sortingColumns)
        {
            if (TreeDetailGridExControl != null)
            {
                GridView gv = TreeDetailGridExControl.MainGrid.MainView as GridView;
                if (gv == null) return;

                SetGridViewSortInfo(gv, sortingColumns);
            }
        }
        protected void SetDetailTreeDetailGridSortInfo(params string[] sortingColumns)
        {
            if (DetailTreeDetailGridExControl != null)
            {
                GridView gv = DetailTreeDetailGridExControl.MainGrid.MainView as GridView;
                if (gv == null) return;

                SetGridViewSortInfo(gv, sortingColumns);
            }
        }
        protected void SetSubTreeDetailGridSortInfo(params string[] sortingColumns)
        {
            if (SubTreeDetailGridExControl != null)
            {
                GridView gv = SubTreeDetailGridExControl.MainGrid.MainView as GridView;
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


    }
}