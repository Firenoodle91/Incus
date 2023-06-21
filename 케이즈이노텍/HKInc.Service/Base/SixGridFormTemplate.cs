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
    /// <summary>
    /// Grid 6개 템플릿
    /// </summary>
    public partial class SixGridFormTemplate : BaseForm 
    {
        // Grid에 사용할 BindingSource
        protected BindingSource OneGridBindingSource = new BindingSource();
        protected BindingSource TwoGridBindingSource = new BindingSource();
        protected BindingSource ThreeGridBindingSource = new BindingSource();
        protected BindingSource FourGridBindingSource = new BindingSource();
        protected BindingSource FiveGridBindingSource = new BindingSource();
        protected BindingSource SixGridBindingSource = new BindingSource();

        protected HKInc.Service.Controls.GridEx OneGridExControl;
        protected HKInc.Service.Controls.GridEx TwoGridExControl;
        protected HKInc.Service.Controls.GridEx ThreeGridExControl;
        protected HKInc.Service.Controls.GridEx FourGridExControl;
        protected HKInc.Service.Controls.GridEx FiveGridExControl;
        protected HKInc.Service.Controls.GridEx SixGridExControl;

        protected HKInc.Utils.Interface.Helper.IGridRowLocator OneGridRowLocator;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator TwoGridRowLocator;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator ThreeGridRowLocator;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator FourGridRowLocator;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator FiveGridRowLocator;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator SixGridRowLocator;

        private RadioGroup _outputRadioGroup;
        private RadioGroupType _radioGroupType;
                
        // Grid Toolbar에 사용된 버턴 권한 설정
        private bool _IsOneGridButtonExportEnabled;
        private bool _IsOneGridButtonFileChooseEnabled;
        private bool _IsTwoGridButtonExportEnabled;
        private bool _IsTwoGridButtonFileChooseEnabled;
        private bool _IsThreeGridButtonExportEnabled;
        private bool _IsThreeGridButtonFileChooseEnabled;
        private bool _IsFourGridButtonExportEnabled;
        private bool _IsFourGridButtonFileChooseEnabled;
        private bool _IsFiveGridButtonExportEnabled;
        private bool _IsFiveGridButtonFileChooseEnabled;
        private bool _IsSixGridButtonExportEnabled;
        private bool _IsSixGridButtonFileChooseEnabled;

        protected bool IsOneGridButtonExportEnabled
        {
            get { return _IsOneGridButtonExportEnabled; }
            set
            {
                _IsOneGridButtonExportEnabled = value;
                OneGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                OneGridExControl.ActExportClicked += GridEx1_ActExportClicked;
            }
        }

        protected bool IsOneGridButtonFileChooseEnabled
        {
            get { return _IsOneGridButtonFileChooseEnabled; }
            set
            {
                _IsOneGridButtonFileChooseEnabled = value;
                OneGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                OneGridExControl.ActFileChooseClicked += GridEx1Control_ActFileChooseClicked;                
            }
        }

        protected bool IsTwoGridButtonExportEnabled
        {
            get { return _IsTwoGridButtonExportEnabled; }
            set
            {
                _IsTwoGridButtonExportEnabled = value;
                TwoGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                TwoGridExControl.ActExportClicked += GridEx2_ActExportClicked;
            }
        }

        protected bool IsTwoGridButtonFileChooseEnabled
        {
            get { return _IsTwoGridButtonFileChooseEnabled; }
            set
            {
                _IsTwoGridButtonFileChooseEnabled = value;
                TwoGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                TwoGridExControl.ActFileChooseClicked += GridEx2_ActFileChooseClicked;
            }
        }

        protected bool IsThreeGridButtonExportEnabled
        {
            get { return _IsThreeGridButtonExportEnabled; }
            set
            {
                _IsThreeGridButtonExportEnabled = value;
                ThreeGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                ThreeGridExControl.ActExportClicked += GridEx3_ActExportClicked;
            }
        }

        protected bool IsThreeGridButtonFileChooseEnabled
        {
            get { return _IsThreeGridButtonFileChooseEnabled; }
            set
            {
                _IsThreeGridButtonFileChooseEnabled = value;
                ThreeGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                ThreeGridExControl.ActFileChooseClicked += GridEx3_ActFileChooseClicked;
            }
        }

        protected bool IsFourGridButtonExportEnabled
        {
            get { return _IsFourGridButtonExportEnabled; }
            set
            {
                _IsFourGridButtonExportEnabled = value;
                FourGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                FourGridExControl.ActExportClicked += GridEx4_ActExportClicked;
            }
        }

        protected bool IsFourGridButtonFileChooseEnabled
        {
            get { return _IsFourGridButtonFileChooseEnabled; }
            set
            {
                _IsFourGridButtonFileChooseEnabled = value;
                FourGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                FourGridExControl.ActFileChooseClicked += GridEx4_ActFileChooseClicked;
            }
        }

        protected bool IsFiveGridButtonExportEnabled
        {
            get { return _IsFiveGridButtonExportEnabled; }
            set
            {
                _IsFiveGridButtonExportEnabled = value;
                FiveGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                FiveGridExControl.ActExportClicked += GridEx5_ActExportClicked;
            }
        }

        protected bool IsFiveGridButtonFileChooseEnabled
        {
            get { return _IsFiveGridButtonFileChooseEnabled; }
            set
            {
                _IsFiveGridButtonFileChooseEnabled = value;
                FiveGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                FiveGridExControl.ActFileChooseClicked += GridEx5_ActFileChooseClicked;
            }
        }
        protected bool IsSixGridButtonExportEnabled
        {
            get { return _IsSixGridButtonExportEnabled; }
            set
            {
                _IsSixGridButtonExportEnabled = value;
                SixGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                SixGridExControl.ActExportClicked += GridEx6_ActExportClicked;
            }
        }

        protected bool IsSixGridButtonFileChooseEnabled
        {
            get { return _IsSixGridButtonFileChooseEnabled; }
            set
            {
                _IsSixGridButtonFileChooseEnabled = value;
                SixGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                SixGridExControl.ActFileChooseClicked += GridEx6_ActFileChooseClicked;
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
        public SixGridFormTemplate()
        {
            InitializeComponent();            
        }                
        #endregion

        protected override void InitControls()
        {
            if(FormMenu != null)
                UserRight = MenuFactory.GetUserRight(FormMenu.MenuId, GlobalVariable.UserId);

            // Print, Export에 사용될 오브젝트 설정            
            if (OneGridExControl != null)
            {
                PrintGridControl = OneGridExControl.MainGrid;

                OneGridExControl.MainGrid.Init();
                OneGridExControl.ActAddRowClicked += GridEx1_ActAddRowClicked;
                OneGridExControl.ActDeleteRowClicked += GridEx1_ActDeleteRowClicked;
                OneGridExControl.MainGrid.MainView.FocusedRowChanged += OneView_FocusedRowChanged;
                OneGridExControl.Menu = FormMenu == null ? 0 : FormMenu.MenuId.GetNullToZero();

                OneGridRowLocator = HelperFactory.GetGridRowLocator(OneGridExControl.MainGrid.MainView);
            }

            if (TwoGridExControl != null)
            {                
                TwoGridExControl.MainGrid.Init();
                TwoGridExControl.ActAddRowClicked += GridEx2_ActAddRowClicked;
                TwoGridExControl.ActDeleteRowClicked += GridEx2_ActDeleteRowClicked;
                TwoGridExControl.Menu = FormMenu == null ? 0 : FormMenu.MenuId.GetNullToZero();

                TwoGridRowLocator = HelperFactory.GetGridRowLocator(TwoGridExControl.MainGrid.MainView);
            }

            if (ThreeGridExControl != null)
            {
                ThreeGridExControl.MainGrid.Init();
                ThreeGridExControl.ActAddRowClicked += GridEx3_ActAddRowClicked;
                ThreeGridExControl.ActDeleteRowClicked += GridEx3_ActDeleteRowClicked;
                ThreeGridExControl.MainGrid.MainView.FocusedRowChanged += ThreeView_FocusedRowChanged;
                ThreeGridExControl.Menu = FormMenu == null ? 0 : FormMenu.MenuId.GetNullToZero();

                ThreeGridRowLocator = HelperFactory.GetGridRowLocator(ThreeGridExControl.MainGrid.MainView);
            }

            if (FourGridExControl != null)
            {
                FourGridExControl.MainGrid.Init();
                FourGridExControl.ActAddRowClicked += GridEx4_ActAddRowClicked;
                FourGridExControl.ActDeleteRowClicked += GridEx4_ActDeleteRowClicked;
                FourGridExControl.Menu = FormMenu == null ? 0 : FormMenu.MenuId.GetNullToZero();

                FourGridRowLocator = HelperFactory.GetGridRowLocator(FourGridExControl.MainGrid.MainView);
            }

            if (FiveGridExControl != null)
            {
                FiveGridExControl.MainGrid.Init();
                FiveGridExControl.ActAddRowClicked += GridEx5_ActAddRowClicked;
                FiveGridExControl.ActDeleteRowClicked += GridEx5_ActDeleteRowClicked;
                FiveGridExControl.MainGrid.MainView.FocusedRowChanged += FiveView_FocusedRowChanged;
                FiveGridExControl.Menu = FormMenu == null ? 0 : FormMenu.MenuId.GetNullToZero();

                FiveGridRowLocator = HelperFactory.GetGridRowLocator(FiveGridExControl.MainGrid.MainView);
            }

            if (SixGridExControl != null)
            {
                SixGridExControl.MainGrid.Init();
                SixGridExControl.ActAddRowClicked += GridEx6_ActAddRowClicked;
                SixGridExControl.ActDeleteRowClicked += GridEx6_ActDeleteRowClicked;
                SixGridExControl.Menu = FormMenu == null ? 0 : FormMenu.MenuId.GetNullToZero();

                SixGridRowLocator = HelperFactory.GetGridRowLocator(SixGridExControl.MainGrid.MainView);
            }
        }

        protected override void InitToolbarButton()
        {
            // Grid Toolbar UserRight이용 권한 설정
            if(UserRight != null && OneGridExControl != null)
            {
                OneGridExControl.SetToolbarVisible(UserRight.HasEdit);
                OneGridExControl.SetToolbarButtonVisible(false);
                OneGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                OneGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            }
            if (UserRight != null && TwoGridExControl != null)
            {
                TwoGridExControl.SetToolbarVisible(UserRight.HasEdit);
                TwoGridExControl.SetToolbarButtonVisible(false);
                TwoGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                TwoGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            }
            if (UserRight != null && ThreeGridExControl != null)
            {
                ThreeGridExControl.SetToolbarVisible(UserRight.HasEdit);
                ThreeGridExControl.SetToolbarButtonVisible(false);
                ThreeGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                ThreeGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            }
            if (UserRight != null && FourGridExControl != null)
            {
                FourGridExControl.SetToolbarVisible(UserRight.HasEdit);
                FourGridExControl.SetToolbarButtonVisible(false);
                FourGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                FourGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            }
            if (UserRight != null && FiveGridExControl != null)
            {
                FiveGridExControl.SetToolbarVisible(UserRight.HasEdit);
                FiveGridExControl.SetToolbarButtonVisible(false);
                FiveGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                FiveGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            }
            if (UserRight != null && SixGridExControl != null)
            {
                SixGridExControl.SetToolbarVisible(UserRight.HasEdit);
                SixGridExControl.SetToolbarButtonVisible(false);
                SixGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                SixGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            }
        }

        protected override void InitGridLayoutRestore()
        {
            if (OneGridExControl != null)
                OneGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★

            if (TwoGridExControl != null)
                TwoGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★

            if (ThreeGridExControl != null)
                ThreeGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★

            if (FourGridExControl != null)
                FourGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★

            if (FiveGridExControl != null)
                FiveGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★

            if (SixGridExControl != null)
                SixGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★
        }

        #region Event Handler
        private void OneView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                bool tempIsFormControlChanged = IsFormControlChanged;

                OneViewFocusedRowChanged();

                IsFormControlChanged = tempIsFormControlChanged ? true : false;
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
        }

        private void ThreeView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                bool tempIsFormControlChanged = IsFormControlChanged;

                ThreeViewFocusedRowChanged();

                IsFormControlChanged = tempIsFormControlChanged ? true : false;
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
        }

        private void FiveView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                bool tempIsFormControlChanged = IsFormControlChanged;

                FiveViewFocusedRowChanged();

                IsFormControlChanged = tempIsFormControlChanged ? true : false;
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
        }

        private void GridEx1_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (!IsFirstLoaded) ActRefresh();
            if (OneGridExControl != null) OneGridExControl.MainGrid.PostEditor();
            try { OneGridAddRowClicked(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }

            //NewRowFlag Mapping
            NewRowFlagMapping(OneGridExControl);
        }

        private void GridEx1_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try { OneGridDeleteRow(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx1_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (OneGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(OneGridExControl.MainGrid.MainView);
        }

        private void GridEx1Control_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (OneGridExControl != null) OneGridExControl.MainGrid.PostEditor();
            try { OneGridFileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }
        
        private void GridEx2_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (TwoGridExControl != null) TwoGridExControl.MainGrid.PostEditor();
            try { TwoGridAddRowClicked(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }

            //NewRowFlag Mapping
            NewRowFlagMapping(TwoGridExControl);
        }

        private void GridEx2_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try { TwoGridDeleteRow(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx2_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (TwoGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(TwoGridExControl.MainGrid.MainView);
        }

        private void GridEx2_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (TwoGridExControl != null) TwoGridExControl.MainGrid.PostEditor();
            try { TwoGridFileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx3_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (ThreeGridExControl != null) ThreeGridExControl.MainGrid.PostEditor();
            try { ThreeGridAddRowClicked(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }

            //NewRowFlag Mapping
            NewRowFlagMapping(ThreeGridExControl);
        }

        private void GridEx3_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try{ ThreeGridDeleteRow(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx3_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (ThreeGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(ThreeGridExControl.MainGrid.MainView);
        }

        private void GridEx3_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (ThreeGridExControl != null) ThreeGridExControl.MainGrid.PostEditor();
            try { ThreeGridFileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx4_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (FourGridExControl != null) FourGridExControl.MainGrid.PostEditor();
            try { FourGridAddRowClicked(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }

            //NewRowFlag Mapping
            NewRowFlagMapping(FourGridExControl);
        }

        private void GridEx4_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try { FourGridDeleteRow(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx4_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (FourGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(FourGridExControl.MainGrid.MainView);
        }

        private void GridEx4_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (FourGridExControl != null) FourGridExControl.MainGrid.PostEditor();
            try { FourGridFileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx5_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (FiveGridExControl != null) FiveGridExControl.MainGrid.PostEditor();
            try { FiveGridAddRowClicked(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }

            //NewRowFlag Mapping
            NewRowFlagMapping(FiveGridExControl);
        }

        private void GridEx5_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try { FiveGridDeleteRow(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx5_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (FiveGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(FiveGridExControl.MainGrid.MainView);
        }

        private void GridEx5_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (FiveGridExControl != null) FiveGridExControl.MainGrid.PostEditor();
            try { FiveGridFileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx6_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (SixGridExControl != null) SixGridExControl.MainGrid.PostEditor();
            try { SixGridAddRowClicked(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }

            //NewRowFlag Mapping
            NewRowFlagMapping(SixGridExControl);
        }

        private void GridEx6_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try { SixGridDeleteRow(); IsFormControlChanged = true; } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void GridEx6_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (SixGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(SixGridExControl.MainGrid.MainView);
        }

        private void GridEx6_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (SixGridExControl != null) SixGridExControl.MainGrid.PostEditor();
            try { SixGridFileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }
        #endregion

        #region Function                

        protected virtual void OneGridAddRowClicked() { }
        protected virtual void OneGridDeleteRow() { }
        protected virtual void OneGridFileChooseClicked() { }
        protected virtual void OneViewFocusedRowChanged() { }

        protected virtual void TwoGridAddRowClicked() { }
        protected virtual void TwoGridDeleteRow() { }
        protected virtual void TwoGridFileChooseClicked() { }

        protected virtual void ThreeGridAddRowClicked() { }
        protected virtual void ThreeGridDeleteRow() { }
        protected virtual void ThreeGridFileChooseClicked() { }
        protected virtual void ThreeViewFocusedRowChanged() { }

        protected virtual void FourGridAddRowClicked() { }
        protected virtual void FourGridDeleteRow() { }
        protected virtual void FourGridFileChooseClicked() { }

        protected virtual void FiveGridAddRowClicked() { }
        protected virtual void FiveGridDeleteRow() { }
        protected virtual void FiveGridFileChooseClicked() { }
        protected virtual void FiveViewFocusedRowChanged() { }

        protected virtual void SixGridAddRowClicked() { }
        protected virtual void SixGridDeleteRow() { }
        protected virtual void SixGridFileChooseClicked() { }
        #endregion        
    }
}