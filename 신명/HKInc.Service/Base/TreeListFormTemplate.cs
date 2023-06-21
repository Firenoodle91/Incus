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
    public partial class TreeListFormTemplate : BaseForm 
    {
        // Grid에 사용할 BindingSource
        protected BindingSource TreeListBindingSource = new BindingSource();
        protected TreeListEx TreeListExControl;
        private RadioGroup _outputRadioGroup;
        private RadioGroupType _radioGroupType;

        // Grid Toolbar에 사용된 버턴 권한 설정
        private bool _IsButtonExportEnabled;
        private bool _IsButtonFileChooseEnabled;

        protected bool IsGridButtonExportEnabled
        {
            get { return _IsButtonExportEnabled; }
            set
            {
                _IsButtonExportEnabled = value;
                TreeListExControl.SetToolbarButtonVisible(GridToolbarButton.Export, value);
                TreeListExControl.ActExportClicked += TreeListExControl_ActExportClicked;
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
        #region Construct
        public TreeListFormTemplate()
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
                //IsGridButtonExportEnabled = true;
            }                      
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

        private void TreeListExControl_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            if (TreeListExControl != null)
                HKInc.Service.Helper.ExcelExport.ExportToExcel(TreeListExControl.TreeList);
        }

        private void TreeListExControl_ActFileChooseClicked(object sender, ItemClickEventArgs e)
        {
            if (TreeListExControl != null) TreeListExControl.TreeList.PostEditor();
            try { FileChooseClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
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
        protected virtual void FileChooseClicked() { }
        protected virtual IPopupForm GetPopupForm(PopupDataParam param) { return new PopupCallbackFormTemplate(); }
        protected virtual PopupDataParam AddServiceToPopupDataParam(PopupDataParam param) { return new PopupDataParam(); }

        #endregion
    }
}