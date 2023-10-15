using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors;

using HKInc.Utils.Enum;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Service.Factory;
using HKInc.Service.Controls;
using HKInc.Service.Handler;

namespace HKInc.Service.Base
{
    public partial class TreeListMasterDetailFormTemplate : BaseForm 
    {
        // Grid에 사용할 BindingSource
        protected BindingSource TreeListBindingSource = new BindingSource();
        protected BindingSource DetailGridBindingSource = new BindingSource();
        protected TreeListEx TreeListExControl;
        protected HKInc.Service.Controls.GridEx DetailGridExControl;

        private RadioGroup _outputRadioGroup;
        private RadioGroupType _radioGroupType;

        // TreeList Toolbar에 사용된 버턴 권한 설정
        private bool _IsTreeListButtonExportEnabled;
        private bool _IsTreeListButtonFileChooseEnabled;

        protected bool IsTreeListButtonExportEnabled
        {
            get { return _IsTreeListButtonExportEnabled; }
            set
            {
                _IsTreeListButtonExportEnabled = value;
                TreeListExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                TreeListExControl.ActExportClicked += TreeListExControl_ActExportClicked; ;
            }
        }


        protected bool IsTreeListButtonFileChooseEnabled
        {
            get { return _IsTreeListButtonFileChooseEnabled; }
            set { _IsTreeListButtonFileChooseEnabled = value; TreeListExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value); }
        }

        // Grid Toolbar에 사용된 버턴 권한 설정
        private bool _IsGridButtonExportEnabled;
        private bool _IsGridButtonFileChooseEnabled;

        protected bool IsGridButtonExportEnabled
        {
            get { return _IsGridButtonExportEnabled; }
            set { _IsGridButtonExportEnabled = value; TreeListExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value); }
        }

        protected bool IsGridButtonFileChooseEnabled
        {
            get { return _IsGridButtonFileChooseEnabled; }
            set { _IsGridButtonFileChooseEnabled = value; TreeListExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, value); }
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
        public TreeListMasterDetailFormTemplate()
        {
            InitializeComponent();            
        }        
        
        #endregion
        protected override void InitControls()
        {
            if(FormMenu != null)
                UserRight = MenuFactory.GetUserRight(FormMenu.MenuId, GlobalVariable.UserId);

            // Print, Export에 사용될 오브젝트 설정            
            if (TreeListExControl != null)
            {               
                TreeListExControl.ActAddRowClicked += TreeListEx_ActAddRowClicked;
                TreeListExControl.ActDeleteRowClicked += TreeListEx_ActDeleteRowClicked;
                TreeListExControl.TreeList.MouseDoubleClick += TreeList_MouseDoubleClick;
                TreeListExControl.TreeList.FocusedNodeChanged += TreeList_FocusedNodeChanged;
            }

            if (DetailGridExControl != null)
            {
                DetailGridExControl.MainGrid.Init();
                DetailGridExControl.ActAddRowClicked += DetailGrid_ActAddRowClicked;
                DetailGridExControl.ActDeleteRowClicked += DetailGrid_ActDeleteRowClicked;
                DetailGridExControl.ActExportClicked += DetailGrid_ActExportClicked;
            }
        }
        
        protected override void InitToolbarButton()
        {
            // Grid Toolbar UserRight이용 권한 설정
            if(UserRight != null && TreeListExControl != null)
            {
                TreeListExControl.SetToolbarVisible(UserRight.HasEdit);
                TreeListExControl.SetToolbarButtonVisible(false);
                TreeListExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                TreeListExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
                //IsTreeListButtonExportEnabled = true;
            }
            if (UserRight != null && DetailGridExControl != null)
            {
                DetailGridExControl.SetToolbarVisible(UserRight.HasEdit);
                DetailGridExControl.SetToolbarButtonVisible(false);
                DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
                DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
                //IsGridButtonExportEnabled = true;
            }
        }
        protected override void InitGridLayoutRestore()
        {
            if (DetailGridExControl != null)
                DetailGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★
        }

        #region Event Handler
        protected virtual void TreeListEx_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try { DeleteRow(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }


        protected virtual void TreeListEx_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (!IsFirstLoaded) ActRefresh();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);

            OpenPopup(param);
        }

        protected virtual void TreeList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeList tree = sender as TreeList;
                TreeListHitInfo info = tree.CalcHitInfo(tree.PointToClient(MousePosition));
                TreeListNode node = info.Node;
                if (info.HitInfoType == HitInfoType.Cell)
                {
                    if (node != null)
                    {
                        PopupDataParam param = new PopupDataParam();
                        param.SetValue(PopupParameter.EditMode, UserRight.HasEdit ? PopupEditMode.Update : PopupEditMode.ReadOnly);
                        param.SetValue(PopupParameter.KeyValue, TreeListBindingSource.Current);

                        OpenPopup(param);
                    }
                }
            }
        }

        private void TreeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
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

        protected override void DataExport()
        {
            if (TreeListExControl != null)
                TreeListExControl.Export();
        }

        protected override void DataPrint()
        {
            if (TreeListExControl != null)
            {
                TreeListExControl.TreeList.AppearancePrint.HeaderPanel.Options.UseTextOptions = false;
                TreeListExControl.TreeList.ShowRibbonPrintPreview();
            }
        }

        private void TreeListExControl_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (TreeListExControl != null)
                TreeListExControl.Export();
        }

        private void DetailGrid_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            try { DeleteDetailRow(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void DetailGrid_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (DetailGridExControl != null) DetailGridExControl.MainGrid.PostEditor();
            try { DetailAddRowClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
        }

        private void DetailGrid_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (DetailGridExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(DetailGridExControl.MainGrid.MainView);
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

        protected virtual void DeleteRow() { }
        protected virtual IPopupForm GetPopupForm(PopupDataParam param) { return new PopupCallbackFormTemplate(); }
        protected virtual PopupDataParam AddServiceToPopupDataParam(PopupDataParam param) { return new PopupDataParam(); }
        protected virtual void MasterFocusedRowChanged() { }

        protected virtual void DetailAddRowClicked() { }
        protected virtual void DeleteDetailRow() { }

        #endregion
    }
}