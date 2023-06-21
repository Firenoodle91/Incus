using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;
using HKInc.Ui.Model.BaseDomain;
using HKInc.Service.Helper;
using System.Collections.Generic;
using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain.TEMP;
using System.Data.SqlClient;
using DevExpress.XtraBars;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 마감등록관리
    /// </summary>
    public partial class XFORD1500 : Service.Base.SixGridFormTemplate
    {
        //반제품 탭 첫번재로 선택여부
        private bool BanDeadTabFirstClickFlag = false;

        //자재 탭 첫번재로 선택여부
        private bool MatDeadTabFirstClickFlag = false;

        //제품탭 모델서비스
        IService<TN_PROD_DEAD_MST> ModelService_PROD = (IService<TN_PROD_DEAD_MST>)ProductionFactory.GetDomainService("TN_PROD_DEAD_MST");

        //반제품탭 모델서비스
        IService<TN_BAN_DEAD_MST> ModelService_BAN = (IService<TN_BAN_DEAD_MST>)ProductionFactory.GetDomainService("TN_BAN_DEAD_MST");

        //자재탭 모델서비스
        IService<TN_MAT_DEAD_MST> ModelService_MAT = (IService<TN_MAT_DEAD_MST>)ProductionFactory.GetDomainService("TN_MAT_DEAD_MST");

        //완제품 탭 마감확정/취소 버튼
        BarButtonItem barButtonDeadConfirmWAN = new BarButtonItem();

        //반제품 탭 마감확정/취소 버튼
        BarButtonItem barButtonDeadConfirmBAN = new BarButtonItem();

        //자재 탭 마감확정/취소 버튼
        BarButtonItem barButtonDeadConfirmMAT = new BarButtonItem();

        public XFORD1500()
        {
            InitializeComponent();

            OneGridExControl = gridEx1;
            TwoGridExControl = gridEx2;

            ThreeGridExControl = gridEx3;
            FourGridExControl = gridEx4;

            FiveGridExControl = gridEx5;
            SixGridExControl = gridEx6;

            OneGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            ThreeGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            FiveGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;

            xtraTabControl1.SelectedPageChanged += XtraTabControl1_SelectedPageChanged;
        }

        protected override void InitCombo()
        {
            lcProdDead.Text = LabelConvert.GetLabelText("Prod");
            lcBanDead.Text = LabelConvert.GetLabelText("Ban");
            lcMatDead.Text = LabelConvert.GetLabelText("Mat");
            
            lcProdDeadLine.Text = LabelConvert.GetLabelText("ProdDeadLine");
            lcProdDeadLineDetail.Text = LabelConvert.GetLabelText("ProdDeadLineDetail");

            lcBanDeadLine.Text = LabelConvert.GetLabelText("BanDeadLine");
            lcBanDeadLineDetail.Text = LabelConvert.GetLabelText("BanDeadLineDetail");

            lcMatDeadLine.Text = LabelConvert.GetLabelText("MatDeadLine");
            lcMatDeadLineDetail.Text = LabelConvert.GetLabelText("MatDeadLineDetail");
            
            lcProdDead.Appearance.HeaderActive.BackColor = System.Drawing.Color.White;
            lcBanDead.Appearance.HeaderActive.BackColor = System.Drawing.Color.White;
            lcMatDead.Appearance.HeaderActive.BackColor = System.Drawing.Color.White;

            lcProdDead.Appearance.HeaderActive.Font = new System.Drawing.Font(lcProdDead.Appearance.Header.Font, System.Drawing.FontStyle.Bold);
            lcBanDead.Appearance.HeaderActive.Font = new System.Drawing.Font(lcProdDead.Appearance.Header.Font, System.Drawing.FontStyle.Bold);
            lcMatDead.Appearance.HeaderActive.Font = new System.Drawing.Font(lcMatDead.Appearance.Header.Font, System.Drawing.FontStyle.Bold);

            dt_Year.SetFormat(DateFormat.Year, true);
            dt_Year.DateTime = DateTime.Today;
        }

        protected override void InitGrid()
        {
            //완제품 마스터
            IsOneGridButtonFileChooseEnabled = UserRight.HasEdit;
            OneGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));
            OneGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
            OneGridExControl.MainGrid.AddColumn("DeadLineDate", LabelConvert.GetLabelText("DeadLineMonth"));
            OneGridExControl.MainGrid.AddColumn("RealDeadLineDate", LabelConvert.GetLabelText("DeadLineDate"));
            OneGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"));
            OneGridExControl.MainGrid.AddColumn("DeadLineId", LabelConvert.GetLabelText("DeadLineId"), false);
            OneGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "RealDeadLineDate", "DeadLineId");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PROD_DEAD_MST>(OneGridExControl);

            if (UserRight.HasEdit)
            {
                barButtonDeadConfirmWAN.Id = 4;
                barButtonDeadConfirmWAN.ImageOptions.Image = IconImageList.GetIconImage("actions/apply");
                barButtonDeadConfirmWAN.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.O));
                barButtonDeadConfirmWAN.Name = "barButtonDeadConfirmWAN";
                barButtonDeadConfirmWAN.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
                barButtonDeadConfirmWAN.ShortcutKeyDisplayString = "Alt+O";
                barButtonDeadConfirmWAN.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
                barButtonDeadConfirmWAN.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
                barButtonDeadConfirmWAN.ItemClick += BarButtonDeadConfirmWAN_ItemClick;
                barButtonDeadConfirmWAN.Enabled = false;
                OneGridExControl.BarTools.AddItem(barButtonDeadConfirmWAN);
            }

            //완제품 디테일(이력)
            TwoGridExControl.SetToolbarVisible(false);
            TwoGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"));
            TwoGridExControl.MainGrid.AddColumn("DeadLineDate", LabelConvert.GetLabelText("DeadLineMonth"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM");
            TwoGridExControl.MainGrid.AddColumn("RealDeadLineDate", LabelConvert.GetLabelText("DeadLineDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            TwoGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"));
            TwoGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"));

            //반제품 마스터
            IsThreeGridButtonFileChooseEnabled = UserRight.HasEdit;
            ThreeGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("actions/apply"));
            ThreeGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
            ThreeGridExControl.MainGrid.AddColumn("DeadLineDate", LabelConvert.GetLabelText("DeadLineMonth"));
            ThreeGridExControl.MainGrid.AddColumn("RealDeadLineDate", LabelConvert.GetLabelText("DeadLineDate"));
            ThreeGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"));
            ThreeGridExControl.MainGrid.AddColumn("DeadLineId", LabelConvert.GetLabelText("DeadLineId"), false);
            ThreeGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "RealDeadLineDate", "DeadLineId");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_BAN_DEAD_MST>(ThreeGridExControl);
            
            //반제품 디테일(이력)
            FourGridExControl.SetToolbarVisible(false);
            FourGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"));
            FourGridExControl.MainGrid.AddColumn("DeadLineDate", LabelConvert.GetLabelText("DeadLineMonth"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM");
            FourGridExControl.MainGrid.AddColumn("RealDeadLineDate", LabelConvert.GetLabelText("DeadLineDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            FourGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"));
            FourGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"));

            //자재 마스터
            IsFiveGridButtonFileChooseEnabled = UserRight.HasEdit;
            FiveGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));
            FiveGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
            FiveGridExControl.MainGrid.AddColumn("DeadLineDate", LabelConvert.GetLabelText("DeadLineMonth"));
            FiveGridExControl.MainGrid.AddColumn("RealDeadLineDate", LabelConvert.GetLabelText("DeadLineDate"));
            FiveGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"));
            FiveGridExControl.MainGrid.AddColumn("DeadLineId", LabelConvert.GetLabelText("DeadLineId"), false);
            FiveGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "RealDeadLineDate", "DeadLineId");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MAT_DEAD_MST>(FiveGridExControl);

            //자재 디테일(이력)
            SixGridExControl.SetToolbarVisible(false);
            SixGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"));
            SixGridExControl.MainGrid.AddColumn("DeadLineDate", LabelConvert.GetLabelText("DeadLineMonth"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM");
            SixGridExControl.MainGrid.AddColumn("RealDeadLineDate", LabelConvert.GetLabelText("DeadLineDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            SixGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"));
            SixGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"));
        }
        
        protected override void InitRepository()
        {
            var userList = ModelService_PROD.GetChildList<User>(p => p.Active == "Y").ToList();
            var deadLineDivision = DbRequestHandler.GetCommTopCode(MasterCodeSTR.DeadLineDivision);

            OneGridExControl.MainGrid.SetRepositoryItemDateEdit("DeadLineDate", DateFormat.Month);
            OneGridExControl.MainGrid.SetRepositoryItemDateEdit("RealDeadLineDate");
            OneGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", deadLineDivision, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            OneGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DeadLineId", userList, "LoginId", "UserName");

            TwoGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", deadLineDivision, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            TwoGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", userList, "LoginId", "UserName");

            ThreeGridExControl.MainGrid.SetRepositoryItemDateEdit("DeadLineDate", DateFormat.Month);
            ThreeGridExControl.MainGrid.SetRepositoryItemDateEdit("RealDeadLineDate");
            ThreeGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", deadLineDivision, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            ThreeGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DeadLineId", userList, "LoginId", "UserName");

            FourGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", deadLineDivision, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            FourGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", userList, "LoginId", "UserName");

            FiveGridExControl.MainGrid.SetRepositoryItemDateEdit("DeadLineDate", DateFormat.Month);
            FiveGridExControl.MainGrid.SetRepositoryItemDateEdit("RealDeadLineDate");
            FiveGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", deadLineDivision, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            FiveGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DeadLineId", userList, "LoginId", "UserName");

            SixGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", deadLineDivision, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            SixGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", userList, "LoginId", "UserName");

            OneGridExControl.BestFitColumns();
            TwoGridExControl.BestFitColumns();
            ThreeGridExControl.BestFitColumns();
            FourGridExControl.BestFitColumns();
            FiveGridExControl.BestFitColumns();
            SixGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            var selectedPageName = xtraTabControl1.SelectedTabPage.Name;
            var yearDate = dt_Year.DateTime.GetNullToDateTime();
                        
            if (selectedPageName == "lcProdDead") //완제품 탭
            {
                OneGridRowLocator.GetCurrentRow();

                OneGridExControl.MainGrid.Clear();
                TwoGridExControl.MainGrid.Clear();

                ModelService_PROD.ReLoad();

                OneGridBindingSource.DataSource = ModelService_PROD.GetList(p => yearDate == null ? true : p.DeadLineDate.Year == ((DateTime)yearDate).Year).OrderBy(p => p.DeadLineDate).ToList();

                OneGridExControl.DataSource = OneGridBindingSource;
                OneGridExControl.BestFitColumns();
                OneGridRowLocator.SetCurrentRow();
            }
            else if (selectedPageName == "lcBanDead") //반제품 탭
            {
                ThreeGridRowLocator.GetCurrentRow();

                ThreeGridExControl.MainGrid.Clear();
                FourGridExControl.MainGrid.Clear();

                ModelService_BAN.ReLoad();

                ThreeGridBindingSource.DataSource = ModelService_BAN.GetList(p => yearDate == null ? true : p.DeadLineDate.Year == ((DateTime)yearDate).Year).OrderBy(p => p.DeadLineDate).ToList();

                ThreeGridExControl.DataSource = ThreeGridBindingSource;
                ThreeGridExControl.BestFitColumns();
                ThreeGridRowLocator.SetCurrentRow();
            }
            else if (selectedPageName == "lcMatDead") //자재 탭
            {
                FiveGridRowLocator.GetCurrentRow();

                FiveGridExControl.MainGrid.Clear();
                SixGridExControl.MainGrid.Clear();

                ModelService_MAT.ReLoad();

                FiveGridBindingSource.DataSource = ModelService_MAT.GetList(p => yearDate == null ? true : p.DeadLineDate.Year == ((DateTime)yearDate).Year).OrderBy(p => p.DeadLineDate).ToList();

                FiveGridExControl.DataSource = FiveGridBindingSource;
                FiveGridExControl.BestFitColumns();
                FiveGridRowLocator.SetCurrentRow();
            }
        }

        /// <summary> 완제품 행 변경 시 </summary>
        protected override void OneViewFocusedRowChanged()
        {
            var masterObj = OneGridBindingSource.Current as TN_PROD_DEAD_MST;
            if (masterObj == null)
            {
                OneGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));
                OneGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
                barButtonDeadConfirmWAN.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
                barButtonDeadConfirmWAN.Enabled = false;
                TwoGridExControl.MainGrid.Clear();
                return;
            }

            OneGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
            barButtonDeadConfirmWAN.Enabled = false;

            if (masterObj.Division.IsNullOrEmpty()) //구분이 없을 경우
            {
                OneGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                OneGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));

                barButtonDeadConfirmWAN.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinish) //마감처리일 경우
            {
                OneGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                OneGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinishCancel") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));

                barButtonDeadConfirmWAN.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
                barButtonDeadConfirmWAN.Enabled = true;
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinishCancel) //마감처리취소일 경우
            {
                OneGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                OneGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));

                barButtonDeadConfirmWAN.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirm) //마감확정일 경우
            {
                OneGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("actions/cancel"));

                barButtonDeadConfirmWAN.Caption = LabelConvert.GetLabelText("DeadConfirmCancel") + "[Alt+O]";
                barButtonDeadConfirmWAN.Enabled = true;
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirmCancel) //마감확정취소일 경우
            {
                OneGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                OneGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinishCancel") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));

                barButtonDeadConfirmWAN.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
                barButtonDeadConfirmWAN.Enabled = true;
            }

            //완제품 마감 이력 Load
            TwoGridBindingSource.DataSource = ModelService_PROD.GetChildList<TN_PROD_DEAD_HISTORY>(p => p.DeadLineDate == masterObj.DeadLineDate).OrderBy(p => p.CreateTime).ToList();
            TwoGridExControl.DataSource = TwoGridBindingSource;
            TwoGridExControl.BestFitColumns();
        }

        /// <summary> 반제품 행 변경 시 </summary>
        protected override void ThreeViewFocusedRowChanged()
        {
            var masterObj = ThreeGridBindingSource.Current as TN_BAN_DEAD_MST;
            if (masterObj == null)
            {
                ThreeGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));
                ThreeGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
                barButtonDeadConfirmBAN.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
                barButtonDeadConfirmBAN.Enabled = false;
                FourGridExControl.MainGrid.Clear();
                return;
            }

            ThreeGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
            barButtonDeadConfirmBAN.Enabled = false;

            if (masterObj.Division.IsNullOrEmpty()) //구분이 없을 경우
            {
                ThreeGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                ThreeGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));

                barButtonDeadConfirmBAN.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinish) //마감처리일 경우
            {
                ThreeGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                ThreeGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinishCancel") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));

                barButtonDeadConfirmBAN.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
                barButtonDeadConfirmBAN.Enabled = true;
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinishCancel) //마감처리취소일 경우
            {
                ThreeGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                ThreeGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));

                barButtonDeadConfirmBAN.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirm) //마감확정일 경우
            {
                ThreeGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("actions/cancel"));

                barButtonDeadConfirmBAN.Caption = LabelConvert.GetLabelText("DeadConfirmCancel") + "[Alt+O]";
                barButtonDeadConfirmBAN.Enabled = true;
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirmCancel) //마감확정취소일 경우
            {
                ThreeGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                ThreeGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinishCancel") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));

                barButtonDeadConfirmBAN.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
                barButtonDeadConfirmBAN.Enabled = true;
            }

            //반제품 마감 이력 Load
            FourGridBindingSource.DataSource = ModelService_BAN.GetChildList<TN_BAN_DEAD_HISTORY>(p => p.DeadLineDate == masterObj.DeadLineDate).OrderBy(p => p.CreateTime).ToList();
            FourGridExControl.DataSource = FourGridBindingSource;
            FourGridExControl.BestFitColumns();
        }

        /// <summary> 자재 행 변경 시 </summary>
        protected override void FiveViewFocusedRowChanged()
        {
            var masterObj = FiveGridBindingSource.Current as TN_MAT_DEAD_MST;
            if (masterObj == null)
            {
                FiveGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));
                FiveGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
                barButtonDeadConfirmMAT.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
                barButtonDeadConfirmMAT.Enabled = false;
                SixGridExControl.MainGrid.Clear();
                return;
            }

            FiveGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
            barButtonDeadConfirmMAT.Enabled = false;

            if (masterObj.Division.IsNullOrEmpty()) //구분이 없을 경우
            {
                FiveGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                FiveGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));

                barButtonDeadConfirmMAT.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinish) //마감처리일 경우
            {
                FiveGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                FiveGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinishCancel") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));

                barButtonDeadConfirmMAT.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
                barButtonDeadConfirmMAT.Enabled = true;
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinishCancel) //마감처리취소일 경우
            {
                FiveGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                FiveGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));

                barButtonDeadConfirmMAT.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirm) //마감확정일 경우
            {
                FiveGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinish") + "[F10]", IconImageList.GetIconImage("actions/cancel"));

                barButtonDeadConfirmMAT.Caption = LabelConvert.GetLabelText("DeadConfirmCancel") + "[Alt+O]";
                barButtonDeadConfirmMAT.Enabled = true;
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirmCancel) //마감확정취소일 경우
            {
                FiveGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                FiveGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("DeadFinishCancel") + "[F10]", IconImageList.GetIconImage("conditional%20formatting/iconsetsymbols3"));

                barButtonDeadConfirmMAT.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
                barButtonDeadConfirmMAT.Enabled = true;
            }

            //자재 마감 이력 Load
            SixGridBindingSource.DataSource = ModelService_MAT.GetChildList<TN_MAT_DEAD_HISTORY>(p => p.DeadLineDate == masterObj.DeadLineDate).OrderBy(p => p.CreateTime).ToList();
            SixGridExControl.DataSource = SixGridBindingSource;
            SixGridExControl.BestFitColumns();
        }

        /// <summary> 완제품 마감 추가 </summary>
        protected override void OneGridAddRowClicked()
        {
            if (OneGridBindingSource == null || OneGridBindingSource.DataSource == null) ActRefresh();
            var list = OneGridBindingSource.List as List<TN_PROD_DEAD_MST>;

            var newObjCheck = list.Where(p => p.NewRowFlag == "Y").FirstOrDefault();
            if (newObjCheck != null) //이미 추가된 마감이 있을 경우 그 위치로 이동
            {
                OneGridExControl.MainGrid.MainView.FocusedRowHandle = OneGridExControl.MainGrid.MainView.LocateByValue("DeadLineDate", newObjCheck.DeadLineDate);
            }
            else
            {
                //마지막으로 추가된 마감을 불러옴
                var lastObj = ModelService_PROD.GetList(p => true).OrderBy(p => p.RowId).LastOrDefault();
                if (lastObj != null && lastObj.Division != MasterCodeSTR.DeadLineDivision_DeadConfirm) //마지막으로 된 마감이 있고, 해당 마감이 확정이 되지 않은 경우
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_76), LabelConvert.GetLabelText("AddRow")));
                    return;
                }

                var today = DateTime.Today;
                var todayPreviousMonth = DateTime.Today.AddMonths(-1);
                var deadLineDate = lastObj == null ? todayPreviousMonth.AddDays(1 - today.Day) : lastObj.DeadLineDate.AddMonths(1); //첫 데이터일 경우 전 달을 불러옴, 데이터가 있을 경우 제일 마지막 +1달을 가져옴
                if (today <= deadLineDate) //마감될 항목이 당월보다 낮지 않을 경우 예외처리 (당월 마감은 익월 가능)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_82));
                    return;
                }

                var newObj = new TN_PROD_DEAD_MST()
                {
                    DeadLineDate = deadLineDate,
                    RealDeadLineDate = DateTime.Today,
                    //Division = MasterCodeSTR.DeadLineDivision_DeadConfirm,
                    DeadLineId = GlobalVariable.LoginId,
                    NewRowFlag = "Y"
                };

                ModelService_PROD.Insert(newObj);
                OneGridBindingSource.Add(newObj);
                OneGridExControl.BestFitColumns();
            }
        }
        
        /// <summary> 반제품 마감 추가 </summary>
        protected override void ThreeGridAddRowClicked()
        {
            if (ThreeGridBindingSource == null || ThreeGridBindingSource.DataSource == null) ActRefresh();
            var list = ThreeGridBindingSource.List as List<TN_BAN_DEAD_MST>;

            var newObjCheck = list.Where(p => p.NewRowFlag == "Y").FirstOrDefault();
            if (newObjCheck != null) //이미 추가된 마감이 있을 경우 그 위치로 이동
            {
                ThreeGridExControl.MainGrid.MainView.FocusedRowHandle = ThreeGridExControl.MainGrid.MainView.LocateByValue("DeadLineDate", newObjCheck.DeadLineDate);
            }
            else
            {
                //마지막으로 추가된 마감을 불러옴
                var lastObj = ModelService_BAN.GetList(p => true).OrderBy(p => p.RowId).LastOrDefault();
                if (lastObj != null && lastObj.Division != MasterCodeSTR.DeadLineDivision_DeadConfirm) //마지막으로 된 마감이 있고, 해당 마감이 확정이 되지 않은 경우
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_76), LabelConvert.GetLabelText("AddRow")));
                    return;
                }

                var today = DateTime.Today;
                var todayPreviousMonth = DateTime.Today.AddMonths(-1);
                var deadLineDate = lastObj == null ? todayPreviousMonth.AddDays(1 - today.Day) : lastObj.DeadLineDate.AddMonths(1); //첫 데이터일 경우 전 달을 불러옴, 데이터가 있을 경우 제일 마지막 +1달을 가져옴
                if (today <= deadLineDate) //마감될 항목이 당월보다 낮지 않을 경우 예외처리 (당월 마감은 익월 가능)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_82));
                    return;
                }

                var newObj = new TN_BAN_DEAD_MST()
                {
                    DeadLineDate = deadLineDate,
                    RealDeadLineDate = DateTime.Today,
                    //Division = MasterCodeSTR.DeadLineDivision_DeadConfirm,
                    DeadLineId = GlobalVariable.LoginId,
                    NewRowFlag = "Y"
                };

                ModelService_BAN.Insert(newObj);
                ThreeGridBindingSource.Add(newObj);
                ThreeGridExControl.BestFitColumns();
            }
        }
        
        /// <summary> 자재 마감 추가 </summary>
        protected override void FiveGridAddRowClicked()
        {
            if (FiveGridBindingSource == null || FiveGridBindingSource.DataSource == null) ActRefresh();
            var list = FiveGridBindingSource.List as List<TN_MAT_DEAD_MST>;

            var newObjCheck = list.Where(p => p.NewRowFlag == "Y").FirstOrDefault();
            if (newObjCheck != null) //이미 추가된 마감이 있을 경우 그 위치로 이동
            {
                FiveGridExControl.MainGrid.MainView.FocusedRowHandle = FiveGridExControl.MainGrid.MainView.LocateByValue("DeadLineDate", newObjCheck.DeadLineDate);
            }
            else
            {
                //마지막으로 추가된 마감을 불러옴
                var lastObj = ModelService_MAT.GetList(p => true).OrderBy(p => p.RowId).LastOrDefault();
                if (lastObj != null && lastObj.Division != MasterCodeSTR.DeadLineDivision_DeadConfirm) //마지막으로 된 마감이 있고, 해당 마감이 확정이 되지 않은 경우
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_76), LabelConvert.GetLabelText("AddRow")));
                    return;
                }

                var today = DateTime.Today;
                var todayPreviousMonth = DateTime.Today.AddMonths(-1);
                var deadLineDate = lastObj == null ? todayPreviousMonth.AddDays(1 - today.Day) : lastObj.DeadLineDate.AddMonths(1); //첫 데이터일 경우 전 달을 불러옴, 데이터가 있을 경우 제일 마지막 +1달을 가져옴
                if (today <= deadLineDate) //마감될 항목이 당월보다 낮지 않을 경우 예외처리 (당월 마감은 익월 가능)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_82));
                    return;
                }

                var newObj = new TN_MAT_DEAD_MST()
                {
                    DeadLineDate = deadLineDate,
                    RealDeadLineDate = DateTime.Today,
                    //Division = MasterCodeSTR.DeadLineDivision_DeadConfirm,
                    DeadLineId = GlobalVariable.LoginId,
                    NewRowFlag = "Y"
                };

                ModelService_MAT.Insert(newObj);
                FiveGridBindingSource.Add(newObj);
                FiveGridExControl.BestFitColumns();
            }
        }
        
        /// <summary> 완제품 마감 삭제 </summary>
        protected override void OneGridDeleteRow()
        {
            var masterObj = OneGridBindingSource.Current as TN_PROD_DEAD_MST;
            if (masterObj == null) return;

            if (!masterObj.Division.IsNullOrEmpty() && masterObj.Division != MasterCodeSTR.DeadLineDivision_DeadFinishCancel) //마감처리취소가 아닐 경우 예외처리
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_89));
                //MessageBoxHandler.Show("마감 처리 취소가 아니므로 삭제할 수 없습니다.");
                return;
            }

            ModelService_PROD.Delete(masterObj);
            OneGridBindingSource.RemoveCurrent();
            OneGridExControl.BestFitColumns();
        }

        /// <summary> 반제품 마감 삭제 </summary>
        protected override void ThreeGridDeleteRow()
        {
            var masterObj = ThreeGridBindingSource.Current as TN_BAN_DEAD_MST;
            if (masterObj == null) return;

            if (!masterObj.Division.IsNullOrEmpty() && masterObj.Division != MasterCodeSTR.DeadLineDivision_DeadFinishCancel) //마감처리취소가 아닐 경우 예외처리
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_89));
                //MessageBoxHandler.Show("마감 처리 취소가 아니므로 삭제할 수 없습니다.");
                return;
            }

            ModelService_BAN.Delete(masterObj);
            ThreeGridBindingSource.RemoveCurrent();
            ThreeGridExControl.BestFitColumns();
        }

        /// <summary> 자재 마감 삭제 </summary>
        protected override void FiveGridDeleteRow()
        {
            var masterObj = FiveGridBindingSource.Current as TN_MAT_DEAD_MST;
            if (masterObj == null) return;

            if (!masterObj.Division.IsNullOrEmpty() && masterObj.Division != MasterCodeSTR.DeadLineDivision_DeadFinishCancel) //마감처리취소가 아닐 경우 예외처리
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_89));
                //MessageBoxHandler.Show("마감 처리 취소가 아니므로 삭제할 수 없습니다.");
                return;
            }

            ModelService_MAT.Delete(masterObj);
            FiveGridBindingSource.RemoveCurrent();
            FiveGridExControl.BestFitColumns();
        }

        /// <summary> 완제품 마감처리/취소 이벤트</summary>
        protected override void OneGridFileChooseClicked()
        {
            var masterObj = OneGridBindingSource.Current as TN_PROD_DEAD_MST;
            if (masterObj == null) return;

            //마지막으로 추가된 마감을 불러옴
            var lastObj = ModelService_PROD.GetList(p => true).OrderBy(p => p.RowId).LastOrDefault();

            if (masterObj.NewRowFlag != "Y" && lastObj != null && lastObj.DeadLineDate != masterObj.DeadLineDate) //추가된 행이 아니고, 마지막 마감이 아닐 경우
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_90));
                //MessageBoxHandler.Show("마지막 마감 항목부터 처리할 수 있습니다.");
                return;
            }
                       
            //현재 구분이 없을 경우 => 마감처리 , 현재 구분이 마감처리 시 => 마감처리취소, 현재 구분이 마감처리취소 시 => 마감처리, 현재 구분이 마감확정취소 시 => 마감처리취소
            var division = string.Empty;
            if (masterObj.Division.IsNullOrEmpty())
                division = MasterCodeSTR.DeadLineDivision_DeadFinish;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinish)
                division = MasterCodeSTR.DeadLineDivision_DeadFinishCancel;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinishCancel)
                division = MasterCodeSTR.DeadLineDivision_DeadFinish;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirmCancel)
                division = MasterCodeSTR.DeadLineDivision_DeadFinishCancel;

            masterObj.Division = division;
            masterObj.UpdateId = GlobalVariable.LoginId;
            masterObj.UpdateTime = DateTime.Now;

            if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinish) //마감처리 시
            {
                //재고 이월 데이터 생성
                if (masterObj.TN_PROD_DEAD_DTL_List.Count == 0)
                {
                    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                    {
                        context.Database.CommandTimeout = 0;
                        var Date = new SqlParameter("@Date", masterObj.DeadLineDate);
                        var result = context.Database.SqlQuery<TEMP_PROD_DEAD_DTL>("USP_GET_PROD_DEAD_DTL_TEMP_ADD_LIST @Date", Date).ToList();
                        foreach (var v in result)
                        {
                            var newObj = new TN_PROD_DEAD_DTL();
                            newObj.DeadLineDate = masterObj.DeadLineDate;
                            newObj.ProductLotNo = v.ProductLotNo;
                            newObj.CarryOverQty = v.CarryOverQty;
                            newObj.InQty = v.InQty;
                            newObj.OutQty = v.OutQty;
                            newObj.AdjustQty = 0;
                            newObj.StockQty = v.StockQty;
                            newObj.ItemCode = v.ItemCode;
                            masterObj.TN_PROD_DEAD_DTL_List.Add(newObj);
                        }

                        if (result.Count > 0)
                            IsFormControlChanged = true;
                    }
                }
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinishCancel) //마감처리취소 시 재고 이월데이터 삭제
            {
                //재고 이월 데이터 삭제
                var removeList = masterObj.TN_PROD_DEAD_DTL_List.ToList();
                foreach (var v in removeList)
                {
                    masterObj.TN_PROD_DEAD_DTL_List.Remove(v);
                }

                if (removeList.Count > 0)
                    IsFormControlChanged = true;
            }

            OneViewFocusedRowChanged();
            OneGridExControl.BestFitColumns();
        }

        /// <summary> 반제품 마감처리/취소 이벤트 </summary>
        protected override void ThreeGridFileChooseClicked()
        {
            var masterObj = ThreeGridBindingSource.Current as TN_BAN_DEAD_MST;
            if (masterObj == null) return;

            //마지막으로 추가된 마감을 불러옴
            var lastObj = ModelService_BAN.GetList(p => true).OrderBy(p => p.RowId).LastOrDefault();

            if (masterObj.NewRowFlag != "Y" && lastObj != null && lastObj.DeadLineDate != masterObj.DeadLineDate) //추가된 행이 아니고, 마지막 마감이 아닐 경우
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_90));
                //MessageBoxHandler.Show("마지막 마감 항목부터 처리할 수 있습니다.");
                return;
            }

            //현재 구분이 없을 경우 => 마감처리 , 현재 구분이 마감처리 시 => 마감처리취소, 현재 구분이 마감처리취소 시 => 마감처리, 현재 구분이 마감확정취소 시 => 마감처리취소
            var division = string.Empty;
            if (masterObj.Division.IsNullOrEmpty())
                division = MasterCodeSTR.DeadLineDivision_DeadFinish;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinish)
                division = MasterCodeSTR.DeadLineDivision_DeadFinishCancel;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinishCancel)
                division = MasterCodeSTR.DeadLineDivision_DeadFinish;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirmCancel)
                division = MasterCodeSTR.DeadLineDivision_DeadFinishCancel;

            masterObj.Division = division;
            masterObj.UpdateId = GlobalVariable.LoginId;
            masterObj.UpdateTime = DateTime.Now;

            if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinish) //마감처리 시
            {
                //재고 이월 데이터 생성
                if (masterObj.TN_BAN_DEAD_DTL_List.Count == 0)
                {
                    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                    {
                        context.Database.CommandTimeout = 0;
                        var Date = new SqlParameter("@Date", masterObj.DeadLineDate);
                        var result = context.Database.SqlQuery<TEMP_BAN_DEAD_DTL>("USP_GET_BAN_DEAD_DTL_TEMP_ADD_LIST @Date", Date).ToList();
                        foreach (var v in result)
                        {
                            var newObj = new TN_BAN_DEAD_DTL();
                            newObj.DeadLineDate = masterObj.DeadLineDate;
                            newObj.ProductLotNo = v.ProductLotNo;
                            newObj.CarryOverQty = v.CarryOverQty;
                            newObj.InQty = v.InQty;
                            newObj.OutQty = v.OutQty;
                            newObj.AdjustQty = 0;
                            newObj.StockQty = v.StockQty;
                            newObj.ItemCode = v.ItemCode;
                            masterObj.TN_BAN_DEAD_DTL_List.Add(newObj);
                        }

                        if (result.Count > 0)
                            IsFormControlChanged = true;
                    }
                }
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinishCancel) //마감처리취소 시 재고 이월데이터 삭제
            {
                //재고 이월 데이터 삭제
                var removeList = masterObj.TN_BAN_DEAD_DTL_List.ToList();
                foreach (var v in removeList)
                {
                    masterObj.TN_BAN_DEAD_DTL_List.Remove(v);
                }

                if (removeList.Count > 0)
                    IsFormControlChanged = true;
            }

            ThreeViewFocusedRowChanged();
            ThreeGridExControl.BestFitColumns();
        }

        /// <summary> 자재 마감처리/취소 이벤트 </summary>
        protected override void FiveGridFileChooseClicked()
        {
            var masterObj = FiveGridBindingSource.Current as TN_MAT_DEAD_MST;
            if (masterObj == null) return;

            //마지막으로 추가된 마감을 불러옴
            var lastObj = ModelService_MAT.GetList(p => true).OrderBy(p => p.RowId).LastOrDefault();

            if (masterObj.NewRowFlag != "Y" && lastObj != null && lastObj.DeadLineDate != masterObj.DeadLineDate) //추가된 행이 아니고, 마지막 마감이 아닐 경우
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_90));
                //MessageBoxHandler.Show("마지막 마감 항목부터 처리할 수 있습니다.");
                return;
            }

            //현재 구분이 없을 경우 => 마감처리 , 현재 구분이 마감처리 시 => 마감처리취소, 현재 구분이 마감처리취소 시 => 마감처리, 현재 구분이 마감확정취소 시 => 마감처리취소
            var division = string.Empty;
            if (masterObj.Division.IsNullOrEmpty())
                division = MasterCodeSTR.DeadLineDivision_DeadFinish;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinish)
                division = MasterCodeSTR.DeadLineDivision_DeadFinishCancel;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinishCancel)
                division = MasterCodeSTR.DeadLineDivision_DeadFinish;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirmCancel)
                division = MasterCodeSTR.DeadLineDivision_DeadFinishCancel;

            masterObj.Division = division;
            masterObj.UpdateId = GlobalVariable.LoginId;
            masterObj.UpdateTime = DateTime.Now;

            if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinish) //마감처리 시
            {
                //재고 이월 데이터 생성
                if (masterObj.TN_MAT_DEAD_DTL_List.Count == 0)
                {
                    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                    {
                        context.Database.CommandTimeout = 0;
                        var Date = new SqlParameter("@Date", masterObj.DeadLineDate);
                        var result = context.Database.SqlQuery<TEMP_MAT_DEAD_DTL>("USP_GET_MAT_DEAD_DTL_TEMP_ADD_LIST @Date", Date).ToList();
                        foreach (var v in result)
                        {
                            var newObj = new TN_MAT_DEAD_DTL();
                            newObj.DeadLineDate = masterObj.DeadLineDate;
                            newObj.InLotNo = v.InLotNo;
                            newObj.CarryOverQty = v.CarryOverQty;
                            newObj.InQty = v.InQty;
                            newObj.OutQty = v.OutQty;
                            newObj.AdjustQty = 0;
                            newObj.StockQty = v.StockQty;
                            newObj.ItemCode = v.ItemCode;
                            masterObj.TN_MAT_DEAD_DTL_List.Add(newObj);
                        }

                        if (result.Count > 0)
                            IsFormControlChanged = true;
                    }
                }
            }
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinishCancel) //마감처리취소 시 재고 이월데이터 삭제
            {
                //재고 이월 데이터 삭제
                var removeList = masterObj.TN_MAT_DEAD_DTL_List.ToList();
                foreach (var v in removeList)
                {
                    masterObj.TN_MAT_DEAD_DTL_List.Remove(v);
                }

                if (removeList.Count > 0)
                    IsFormControlChanged = true;
            }

            FiveViewFocusedRowChanged();
            FiveGridExControl.BestFitColumns();
        }
        
        /// <summary> 완제품 마감확정/취소 이벤트 </summary>
        private void BarButtonDeadConfirmWAN_ItemClick(object sender, ItemClickEventArgs e)
        {
            var masterObj = OneGridBindingSource.Current as TN_PROD_DEAD_MST;
            if (masterObj == null) return;

            //마지막으로 추가된 마감을 불러옴
            var lastObj = ModelService_PROD.GetList(p => true).OrderBy(p => p.RowId).LastOrDefault();

            if (masterObj.NewRowFlag != "Y" && lastObj != null && lastObj.DeadLineDate != masterObj.DeadLineDate) //추가된 행이 아니고, 마지막 마감이 아닐 경우
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_90));
                //MessageBoxHandler.Show("마지막 마감 항목부터 처리할 수 있습니다.");
                return;
            }

            //현재 구분이 마감처리 시 => 마감확정, 현재 구분이 마감확정 시 => 마감확정취소, 현재 구분이 마감확정취소 시 => 마감확정
            var division = string.Empty;
            if (masterObj.Division.IsNullOrEmpty())
                return;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinish)
                division = MasterCodeSTR.DeadLineDivision_DeadConfirm;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirm)
                division = MasterCodeSTR.DeadLineDivision_DeadConfirmCancel;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirmCancel)
                division = MasterCodeSTR.DeadLineDivision_DeadConfirm;

            masterObj.Division = division;
            masterObj.UpdateId = GlobalVariable.LoginId;
            masterObj.UpdateTime = DateTime.Now;
            IsFormControlChanged = true;

            OneViewFocusedRowChanged();
            OneGridExControl.BestFitColumns();
        }

        /// <summary> 반제품 마감확정/취소 이벤트 </summary>
        private void BarButtonDeadConfirmBAN_ItemClick(object sender, ItemClickEventArgs e)
        {
            var masterObj = ThreeGridBindingSource.Current as TN_BAN_DEAD_MST;
            if (masterObj == null) return;

            //마지막으로 추가된 마감을 불러옴
            var lastObj = ModelService_BAN.GetList(p => true).OrderBy(p => p.RowId).LastOrDefault();

            if (masterObj.NewRowFlag != "Y" && lastObj != null && lastObj.DeadLineDate != masterObj.DeadLineDate) //추가된 행이 아니고, 마지막 마감이 아닐 경우
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_90));
                //MessageBoxHandler.Show("마지막 마감 항목부터 처리할 수 있습니다.");
                return;
            }

            //현재 구분이 마감처리 시 => 마감확정, 현재 구분이 마감확정 시 => 마감확정취소, 현재 구분이 마감확정취소 시 => 마감확정
            var division = string.Empty;
            if (masterObj.Division.IsNullOrEmpty())
                return;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinish)
                division = MasterCodeSTR.DeadLineDivision_DeadConfirm;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirm)
                division = MasterCodeSTR.DeadLineDivision_DeadConfirmCancel;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirmCancel)
                division = MasterCodeSTR.DeadLineDivision_DeadConfirm;

            masterObj.Division = division;
            masterObj.UpdateId = GlobalVariable.LoginId;
            masterObj.UpdateTime = DateTime.Now;
            IsFormControlChanged = true;

            ThreeViewFocusedRowChanged();
            ThreeGridExControl.BestFitColumns();
        }

        /// <summary> 자재 마감확정/취소 이벤트 </summary>
        private void BarButtonDeadConfirmMAT_ItemClick(object sender, ItemClickEventArgs e)
        {
            var masterObj = FiveGridBindingSource.Current as TN_MAT_DEAD_MST;
            if (masterObj == null) return;

            //마지막으로 추가된 마감을 불러옴
            var lastObj = ModelService_MAT.GetList(p => true).OrderBy(p => p.RowId).LastOrDefault();

            if (masterObj.NewRowFlag != "Y" && lastObj != null && lastObj.DeadLineDate != masterObj.DeadLineDate) //추가된 행이 아니고, 마지막 마감이 아닐 경우
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_90));
                //MessageBoxHandler.Show("마지막 마감 항목부터 처리할 수 있습니다.");
                return;
            }

            //현재 구분이 마감처리 시 => 마감확정, 현재 구분이 마감확정 시 => 마감확정취소, 현재 구분이 마감확정취소 시 => 마감확정
            var division = string.Empty;
            if (masterObj.Division.IsNullOrEmpty())
                return;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadFinish)
                division = MasterCodeSTR.DeadLineDivision_DeadConfirm;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirm)
                division = MasterCodeSTR.DeadLineDivision_DeadConfirmCancel;
            else if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirmCancel)
                division = MasterCodeSTR.DeadLineDivision_DeadConfirm;

            masterObj.Division = division;
            masterObj.UpdateId = GlobalVariable.LoginId;
            masterObj.UpdateTime = DateTime.Now;
            IsFormControlChanged = true;

            FiveViewFocusedRowChanged();
            FiveGridExControl.BestFitColumns();
        }
        
        protected override void DataSave()
        {
            var selectedPageName = xtraTabControl1.SelectedTabPage.Name;

            if (selectedPageName == "lcProdDead") //완제품 탭
            {
                OneGridExControl.MainGrid.PostEditor();

                ModelService_PROD.Save();
                DataLoad();
            }
            else if (selectedPageName == "lcBanDead") //반제품 탭
            {
                ThreeGridExControl.MainGrid.PostEditor();

                ModelService_BAN.Save();
                DataLoad();
            }
            else if (selectedPageName == "lcMatDead") //자재 탭
            {
                FiveGridExControl.MainGrid.PostEditor();

                ModelService_MAT.Save();
                DataLoad();
            }
        }

        //마감확정취소가 아닌 경우 Edit 막기
        private void MainView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var view = sender as GridView;
            var division = view.GetFocusedRowCellValue("Division").GetNullToEmpty();
            if (division != MasterCodeSTR.DeadLineDivision_DeadConfirmCancel)
            {
                e.Cancel = true;
            }
        }

        private void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            var selectedPageName = xtraTabControl1.SelectedTabPage.Name;
            if (selectedPageName == "lcProdDead")
            {

            }
            else if (selectedPageName == "lcBanDead")
            {
                if (UserRight.HasEdit)
                {
                    if (!BanDeadTabFirstClickFlag)
                    {
                        barButtonDeadConfirmBAN.Id = 4;
                        barButtonDeadConfirmBAN.ImageOptions.Image = IconImageList.GetIconImage("actions/apply");
                        barButtonDeadConfirmBAN.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.O));
                        barButtonDeadConfirmBAN.Name = "barButtonDeadConfirmBAN";
                        barButtonDeadConfirmBAN.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
                        barButtonDeadConfirmBAN.ShortcutKeyDisplayString = "Alt+O";
                        barButtonDeadConfirmBAN.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
                        barButtonDeadConfirmBAN.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
                        barButtonDeadConfirmBAN.ItemClick += BarButtonDeadConfirmBAN_ItemClick;
                        barButtonDeadConfirmBAN.Alignment = BarItemLinkAlignment.Default;
                        barButtonDeadConfirmBAN.Enabled = false;
                        ThreeGridExControl.BarTools.AddItem(barButtonDeadConfirmBAN);
                        BanDeadTabFirstClickFlag = true;
                    }
                }
            }
            else if (selectedPageName == "lcMatDead")
            {
                if (UserRight.HasEdit)
                {
                    if (!MatDeadTabFirstClickFlag)
                    {
                        barButtonDeadConfirmMAT.Id = 4;
                        barButtonDeadConfirmMAT.ImageOptions.Image = IconImageList.GetIconImage("actions/apply");
                        barButtonDeadConfirmMAT.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.O));
                        barButtonDeadConfirmMAT.Name = "barButtonDeadConfirmMAT";
                        barButtonDeadConfirmMAT.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
                        barButtonDeadConfirmMAT.ShortcutKeyDisplayString = "Alt+O";
                        barButtonDeadConfirmMAT.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
                        barButtonDeadConfirmMAT.Caption = LabelConvert.GetLabelText("DeadConfirm") + "[Alt+O]";
                        barButtonDeadConfirmMAT.ItemClick += BarButtonDeadConfirmMAT_ItemClick;
                        barButtonDeadConfirmMAT.Enabled = false;
                        FiveGridExControl.BarTools.AddItem(barButtonDeadConfirmMAT);
                        MatDeadTabFirstClickFlag = true;
                    }
                }
            }
        }

    }
}
