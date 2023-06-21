using System;
using System.Linq;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using DevExpress.XtraSplashScreen;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;

using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Interface.Handler;
using HKInc.Utils.Interface.Forms;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using HKInc.Service.Forms;
using HKInc.Utils.Images;

namespace HKInc.Service.Base
{
    public partial class BaseForm : XtraForm, IBaseForm, IFormControlChanged
    {
        private IUserRight _UserRight;
        private HKInc.Ui.Model.Domain.Menu _Menu;
        private Controls.GridControlEx _GridControl;
        protected Dictionary<ToolbarButton, BarButton> ButtonList = new Dictionary<ToolbarButton, BarButton>();
        protected HKInc.Service.Handler.LayoutControlHandler LayoutControlHandler;

        protected IMenuOpenLogService MenuOpenLogService = LogFactory.GetMenuOpenLogService();
        protected IStandardMessage MessageHelper = HelperFactory.GetStandardMessage();
        protected ILabelConvert LabelConvert = HelperFactory.GetLabelConvert();
        protected IMasterCode MasterCode = HelperFactory.GetMasterCode();
        protected WaitHandler WaitHandler = new WaitHandler();

        // 2021-11-15 김진우 주임 추가
        protected IconImageCollection IconImageList = new HKInc.Utils.Images.IconImageCollection();

        protected bool IsFormControlChanged = false;
        protected bool IsFirstLoaded = false;

        protected bool SetSaveMessageCheck = true;

        private PopupDataParam _PopupDataParam = new PopupDataParam();

        public BaseForm() 
        {
            InitializeComponent();

            InitForm();
        }

        void InitForm()
        {            
            this.FormClosed += BaseForm_FormClosed;
            this.FormClosing += BaseForm_FormClosing;            

            SetToolBar();
        }
        
        #region SetToolbar
        void SetToolBar()
        {
            ButtonList.Add(ToolbarButton.Refresh, new BarButton() { ToolbarButton = barButtonRefresh, ClickAction = ActRefresh, ToolbarButtonCaption = "Refresh" });
            ButtonList.Add(ToolbarButton.Save, new BarButton() { ToolbarButton = barButtonSave, ClickAction = ActSave, ToolbarButtonCaption = "Save" });
            ButtonList.Add(ToolbarButton.Export, new BarButton() { ToolbarButton = barButtonExport, ClickAction = DataExport, ToolbarButtonCaption = "Export" });
            ButtonList.Add(ToolbarButton.Print, new BarButton() { ToolbarButton = barButtonPrint, ClickAction = DataPrint, ToolbarButtonCaption = "Print" });
            ButtonList.Add(ToolbarButton.Close, new BarButton() { ToolbarButton = barButtonClose, ClickAction = ActClose, ToolbarButtonCaption = "Close" });
            ButtonList.Add(ToolbarButton.Confirm, new BarButton() { ToolbarButton = barButtonConfirm, ClickAction = ActConfirm, ToolbarButtonCaption = "Confirm" });

            foreach (ToolbarButton toolbarButton in ButtonList.Keys)
            {
                ButtonList[toolbarButton].ToolbarButton.Tag = (int)toolbarButton;
                ButtonList[toolbarButton].ToolbarButton.ItemClick += ToolbarButtonClick;
            }
        }

        void ToolbarButtonClick(object sender, ItemClickEventArgs e)
        {
            ToolbarButtonClicked((ToolbarButton)(e.Item.Tag.GetIntNullToZero()));
        }

        protected void ActRefresh()
        {
            try
            {
                WaitHandler.ShowWait();
                DataLoad();
                IsFormControlChanged = false;
                IsFirstLoaded = true;
            }
            catch(Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
            finally
            {
                WaitHandler.CloseWait();
            }
        }

        protected void ActSave()
        {
            try
            {
                WaitHandler.ShowWait();
                EndEditOnAllBindingSources();

                DataSave();
                // 20220428 오세완 차장 대영 스타일 대로 수정
                //if(SetSaveMessageCheck)
                //    SetSaveMessage();
            }
            catch (DbEntityValidationException ex)
            {
                string dispalyMessage = string.Empty;                
                foreach (var eve in ex.EntityValidationErrors)
                {
                    dispalyMessage += string.Format(MessageHelper.GetStandardMessage(21), LabelConvert.GetLabelText(eve.Entry.Entity.GetType().Name), LabelConvert.GetLabelText(eve.Entry.State.GetNullToEmpty()), Environment.NewLine);
                    foreach (var ve in eve.ValidationErrors)                    
                        dispalyMessage += string.Format(MessageHelper.GetStandardMessage(20), LabelConvert.GetLabelText(ve.PropertyName), ve.ErrorMessage, Environment.NewLine);                    
                }
                MessageBoxHandler.Show(dispalyMessage);
                SetSaveMessageCheck = false; // 20220428 오세완 차장 대영 스타일 대로 수정
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
                SetSaveMessageCheck = false;
            }
            finally
            {
                WaitHandler.CloseWait();

                // 20220428 오세완 차장 대영 스타일 대로 수정
                if (SetSaveMessageCheck)
                    SetSaveMessage();
            }
        }

        private void EndEditOnAllBindingSources()
        {
            // 20220428 오세완 차장 대영 스타일 대로 componet 조건 추가 
            if(this.components != null)
            {
                var BindingSourcesQuery =
                from Component bindingSources in this.components.Components
                where bindingSources is BindingSource
                select bindingSources;

                foreach (BindingSource bindingSource in BindingSourcesQuery)
                    bindingSource.EndEdit();
            }
            
        }

        protected virtual void DataLoad() { }
        protected virtual void DataSave() { }

        protected virtual void DataExport()
        {
            if (_GridControl != null)            
                HKInc.Service.Helper.ExcelExport.ExportToExcel(_GridControl.MainView);            
        }

        protected virtual void DataPrint()
        {
            if(_GridControl != null)
            {
                _GridControl.MainView.AppearancePrint.HeaderPanel.Options.UseTextOptions = false;
                _GridControl.MainGrid.ShowRibbonPrintPreview();
            }
        }

        protected virtual void ActClose() { this.Close(); }
        protected virtual void ActConfirm() { Confirm(); }
        protected virtual void Confirm() { }
        #endregion

        #region IFormControlchanged
        public void SetIsFormControlChanged(bool changed)
        {
            IsFormControlChanged = changed;
        }

        public bool GetIsFormControlChanged()
        {
            return IsFormControlChanged;
        }
        #endregion

        #region IBaseForm Interface구현        
        public Image MdiTabImage { get; set; }

        public IUserRight UserRight
        {
            get { return this._UserRight; }
            set { this._UserRight = value;  SetToolbarPermission(this._UserRight); }
        }

        public PopupDataParam PopupDataParam
        {
            get { return this._PopupDataParam; }
            set { this._PopupDataParam = value; }
        }

        public HKInc.Ui.Model.Domain.Menu FormMenu
        {
            get { return this._Menu; }
            set { this._Menu = value; MenuOpenLogService.SetOpenMenuLog(DateTime.Now, this._Menu.MenuId.GetIntNullToZero()); }
        }

        public Controls.GridControlEx PrintGridControl { get { return _GridControl; } set { _GridControl = value; } }
        
        // MainForm Toolbar button click event시 호출
        // Refresh, Save, Export, Print
        public void ToolbarButtonClicked(ToolbarButton toolbarButton)
        {
            ButtonList[toolbarButton].ClickAction();

            // 20220224 오세완 차장 TP보고용 로그 기록 추가 
            if (this._Menu != null)
                LogFactory.GetMenuEventService().UpdateMenuEventLog(this._Menu.MenuId.GetIntNullToZero(), toolbarButton.GetNullToEmpty());
        }

        public void SetToolbarVisible(bool visible)
        {
            this.barTools.Visible = visible;
        }

        public void SetToolbarButtonVisible(bool visible)
        {
            foreach (ToolbarButton button in ButtonList.Keys)
                SetToolbarButtonVisible(button, visible);
        }

        public void SetToolbarButtonVisible(ToolbarButton button, bool visible)
        {
            ButtonList[button].ToolbarButton.Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        /// <summary>
        /// 20220321 오세완 차장 
        /// XFSMPS1000에서 사용
        /// </summary>
        /// <param name="button"></param>
        /// <param name="caption"></param>
        /// <param name="image"></param>
        public void SetToolbarButtonCaption(ToolbarButton button, string caption, Image image = null)
        {
            ButtonList[button].ToolbarButton.Caption = caption;
            if (image != null)
            {
                ButtonList[button].ToolbarButton.ImageOptions.Image = image;
            }
        }

        public void SetToolbarPermission(IUserRight userRight)
        {
            ButtonList[ToolbarButton.Refresh].ToolbarButton.Visibility = userRight.HasSelect ? BarItemVisibility.Always : BarItemVisibility.Never;
            ButtonList[ToolbarButton.Save].ToolbarButton.Visibility = userRight.HasEdit ? BarItemVisibility.Always : BarItemVisibility.Never;
            ButtonList[ToolbarButton.Export].ToolbarButton.Visibility = userRight.HasExport ? BarItemVisibility.Always : BarItemVisibility.Never;
            ButtonList[ToolbarButton.Print].ToolbarButton.Visibility = userRight.HasPrint ? BarItemVisibility.Always : BarItemVisibility.Never;
            ButtonList[ToolbarButton.Confirm].ToolbarButton.Visibility = BarItemVisibility.Never;
        }
        #endregion

        #region Form OnLoad                
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                SetLayoutControl();

                InitControls();
                InitBindingSource(); // Event는 DataLoad후 처리한다.
                InitToolbarButton();
                InitCombo();
                InitGrid();
                InitGridLayoutRestore();
                InitRepository();
                InitDataLoad2();
                InitBindingSourceEvent();
                
                barStaticItemName.Caption = this.Name;
            }
            catch(Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }                        
        }

        private void SetLayoutControl()
        {
            LayoutControlHandler = new LayoutControlHandler(this.Controls);

            LayoutControlHandler.SetLayoutControlPadding();

            this.SuspendLayout();

            LayoutControlHandler.SetLabelText();            

            this.ResumeLayout(true);            
        }               

        protected virtual void InitControls() { }
        protected virtual void InitBindingSource() { }
        protected virtual void InitToolbarButton() { }
        protected virtual void InitCombo() { }
        protected virtual void InitGrid() { }
        protected virtual void InitGridLayoutRestore() { }
        protected virtual void InitRepository() { }
        protected virtual void InitDataLoad() { }
        protected virtual void InitDataLoad2()
        {
            try
            {
                WaitHandler.ShowWait();
                InitDataLoad();
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
            finally
            {
                WaitHandler.CloseWait();
            }

        }
        protected virtual void InitBindingSourceEvent() { }

        #endregion

        #region FormClosed
        protected virtual void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Close Query
                if (IsFormControlChanged)
                {
                    DialogResult result = MessageBoxHandler.Show(MessageHelper.GetStandardMessage(1), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Cancel)
                        e.Cancel = true;
                    else if (result == DialogResult.Yes)
                        ActSave();
                }
            }
            catch(Exception ex)
            {
               // HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }

        protected virtual void BaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //Popup은 기록하지 않는다.
                if (_Menu != null)
                    MenuOpenLogService.SetCloseMenuLog(DateTime.Now);
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }            
        }
        #endregion

        #region SetMessage to MainForm message bar        
        protected void SetRefreshMessage(int rowCount)
        {
            IsFormControlChanged = false;
            SetMessage(string.Format(MessageHelper.GetStandardMessage(2), rowCount));
        }
        protected void SetRefreshMessage(string MasterCaption, int MasterRowCount, string DetailCaption, int DetailRowCount)
        {
            IsFormControlChanged = false;
            
            string Msg = string.Format("{0} [{1}]건의 데이터가 조회되었습니다. | {2} [{3}]건의 데이터가 조회되었습니다."
                , LabelConvert.GetLabelText(MasterCaption), MasterRowCount
                , LabelConvert.GetLabelText(DetailCaption), DetailRowCount);
            SetMessage(Msg);
        }

        protected void SetSaveMessage()
        {
            IsFormControlChanged = false;
            SetMessage(MessageHelper.GetStandardMessage(3));
        }

        protected void SetMessage(string message, bool isMainForm = false)
        {
            if (isMainForm) ((IMainFormMessage)this.MdiParent).SetMessage(message);
            else barStaticMessage.Caption = message;
        }
        
        #endregion

        #region PopupCallback Method
        protected virtual void PopupRefreshCallback(object sender, PopupArgument e)
        {
            ActRefresh();
        }
        #endregion
        protected void SetPopupFormMessage(string message)
        {
            barStaticMessage.Caption = message;
        }
    }

}