using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using HKInc.Utils.Enum;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Service.Factory;
using HKInc.Service.Controls;
using HKInc.Service.Handler;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;


namespace HKInc.Service.Base
{
    public partial class ListMasterTreeFormTemplate : BaseForm
    {
        protected BindingSource MasterGridBindingSource = new BindingSource();
        protected BindingSource TreeListBindingSource = new BindingSource();

        protected HKInc.Service.Controls.GridEx MasterGridExControl;
        protected TreeListEx TreeListExControl;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator GridRowLocator;

        private RadioGroup _outputRadioGroup;
        private RadioGroupType _radioGroupType;

        // Grid Toolbar에 사용된 버턴 권한 설정
        private bool _IsButtonExportEnabled;
        private bool _IsButtonFileChooseEnabled;

        /// <summary>
        /// 20211102 오세완 차장 
        /// 거래처 정보는 마스터 그리드에서 더블클릭을 지원해서 bom 타입에서는 사용하지 않을거라 분리 로직 추가
        /// </summary>
        private bool gb_MasterGridDoubleClick = false;

        /// <summary>
        /// 20211102 오세완 차장 
        /// 거래처 정보는 마스터 그리드에서 팝업 출력을 지원해서 bom 타입에서는 사용하지 않을거라 분리 로직 추가
        /// </summary>
        private bool gb_MasterGridAddPopup = false;

        public ListMasterTreeFormTemplate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 20211102 오세완 차장 
        /// 마스터 그리드 더블클릭 이벤트 제어
        /// </summary>
        protected bool IsMasterGridDoubleClick
        {
            get
            {
                return gb_MasterGridDoubleClick;
            }
            set
            {
                gb_MasterGridDoubleClick = value;
            }
        }

        /// <summary>
        /// 20211102 오세완 차장 
        /// 거래처 정보는 마스터 그리드에서 팝업 출력을 지원해서 bom 타입에서는 사용하지 않을거라 분리 로직 추가
        /// </summary>
        protected bool IsMasterGridAddPopup
        {
            get
            {
                return gb_MasterGridAddPopup;
            }

            set
            {
                gb_MasterGridAddPopup = value;
            }
        }

        protected bool IsGridButtonFileChooseEnabled
        {
            get { return _IsButtonFileChooseEnabled; }
            set
            {
                _IsButtonFileChooseEnabled = value;
                TreeListExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value);
                TreeListExControl.ActFileChooseClicked += TreeListExControl_ActFileChooseClicked;
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

        protected override void InitControls()
        {
            if (FormMenu != null)
                UserRight = MenuFactory.GetUserRight(FormMenu.MenuId, GlobalVariable.UserId);

            if (MasterGridExControl != null)
            {
                PrintGridControl = MasterGridExControl.MainGrid;

                MasterGridExControl.MainGrid.Init();
                MasterGridExControl.ActAddRowClicked += Grid_AddRowClickEvent;
                MasterGridExControl.ActDeleteRowClicked += Grid_DeleteRowClickEvent;
                MasterGridExControl.MainGrid.MainView.RowClick += Grid_RowClickEvent;
                //MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
                MasterGridExControl.Menu = FormMenu == null ? 0 : FormMenu.MenuId.GetNullToZero();

                GridRowLocator = HelperFactory.GetGridRowLocator(MasterGridExControl.MainGrid.MainView);
            }

            if (TreeListExControl != null)
            {
                TreeListExControl.ActAddRowClicked += TreeListEx_AddRowClickEvent;
                TreeListExControl.ActDeleteRowClicked += TreeListEx_DeleteRowClickEvent;
                //TreeListExControl.TreeList.MouseDoubleClick += TreeList_MouseDoubleClick;
                //TreeListExControl.TreeList.FocusedNodeChanged += TreeList_FocusedNodeChanged;
                TreeListExControl.Menu = FormMenu == null ? 0 : FormMenu.MenuId.GetNullToZero();
            }
        }

        protected override void InitToolbarButton()
        {
            // Grid Toolbar UserRight이용 권한 설정
            if (UserRight != null && TreeListExControl != null)
            {
                TreeListExControl.SetToolbarVisible(UserRight.HasEdit);
                TreeListExControl.SetToolbarButtonVisible(false);
                TreeListExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                TreeListExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
                //IsTreeListButtonExportEnabled = true;
            }
            if (UserRight != null && MasterGridExControl != null)
            {
                MasterGridExControl.SetToolbarVisible(UserRight.HasEdit);
                MasterGridExControl.SetToolbarButtonVisible(false);
                MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
                //IsGridButtonExportEnabled = true;
            }
        }

        protected override void InitGridLayoutRestore()
        {
            if (MasterGridExControl != null)
                MasterGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★
        }

        #region Event
        private void TreeListExControl_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (TreeListExControl != null) TreeListExControl.TreeList.PostEditor();
            try { FileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }
        protected virtual void Grid_AddRowClickEvent(object sender, ItemClickEventArgs e)
        {
            if (!IsFirstLoaded)
                ActRefresh();

            if(gb_MasterGridAddPopup)
            {
                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.EditMode, PopupEditMode.New);

                OpenPopup(param);
            }
            else
            {
                try
                {
                    MasterGridAddRow();
                }
                catch (Exception ex)
                {
                    MessageBoxHandler.ErrorShow(ex);
                }
            }
            
        }
        protected virtual void Grid_DeleteRowClickEvent(object sender, ItemClickEventArgs e)
        {
            try { MasterGridDeleteRow(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }
        protected virtual void Grid_RowClickEvent(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                try
                {
                    // 20211102 오세완 차장 bom 타입에서는 더블클릭시 팝업을 출력하기 싫어서 수정
                    if(gb_MasterGridDoubleClick)
                        GridRowDoubleClicked();

                } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
            }
        }
        protected virtual void TreeListEx_AddRowClickEvent(object sender, ItemClickEventArgs e)
        {
            try { TreeAddRow(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }
        protected virtual void TreeListEx_DeleteRowClickEvent(object sender, ItemClickEventArgs e)
        {
            try { TreeDeleteRow(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        #endregion

        #region Function
        protected virtual void OpenPopup(PopupDataParam param)
        {
            param.SetValue(PopupParameter.UserRight, UserRight);
            param = AddServiceToPopupDataParam(param);

            IPopupForm form = GetPopupForm(param);
            ((Form)form).Text = GlobalVariable.IsDefaultCulture ? this.FormMenu.MenuName : (GlobalVariable.IsSecondCulture ? this.FormMenu.MenuName2 : this.FormMenu.MenuName3);
            ((Form)form).Icon = Icon.FromHandle(new Bitmap(MdiTabImage).GetHicon());

            form.ShowPopup(true);
        }
        protected virtual IPopupForm GetPopupForm(PopupDataParam param) { return new PopupCallbackFormTemplate(); }
        protected virtual PopupDataParam AddServiceToPopupDataParam(PopupDataParam param) { return new PopupDataParam(); }
        protected virtual void GridRowDoubleClicked()
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.EditMode, UserRight.HasEdit ? PopupEditMode.Update : PopupEditMode.ReadOnly);
            param.SetValue(PopupParameter.KeyValue, MasterGridBindingSource.Current);

            OpenPopup(param);
        }
        protected virtual void MasterFocusedRowChanged() { }
        protected virtual void FileChooseClicked() { }
        protected virtual void TreeAddRow() { }
        protected virtual void TreeDeleteRow() { }
        protected virtual void MasterGridAddRow() { }
        protected virtual void MasterGridDeleteRow() { }
        #endregion
    }
}