using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using DevExpress.XtraExport;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 개발의뢰관리
    /// </summary>
    public partial class XFORD1000_DEV : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1000> ModelService = (IService<TN_ORD1000>)ProductionFactory.GetDomainService("TN_ORD1000");

        public XFORD1000_DEV()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            dt_OrderDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dt_OrderDate.DateToEdit.DateTime = DateTime.Today;

            MasterGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {
            AddGridButton();

            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("ReqDevelopmentNo"));
            MasterGridExControl.MainGrid.AddColumn("OrderDate", LabelConvert.GetLabelText("ReqDevelopmentDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderCustomerCode", LabelConvert.GetLabelText("CustomerCode"), false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("OrderManagerName", LabelConvert.GetLabelText("OrderManagerName"));
            MasterGridExControl.MainGrid.AddColumn("OrderDueDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderId", LabelConvert.GetLabelText("OrderId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.AddColumn("ConfirmState", LabelConvert.GetLabelText("ConfirmState"));

            var userInfo = ModelService.GetChildList<User>(p => p.UserId == GlobalVariable.UserId).FirstOrDefault();
            if(userInfo.DepartmentCode == MasterCodeSTR.DeptRsrch || userInfo.DepartmentCode == MasterCodeSTR.DeptSales 
               || userInfo.DepartmentCode == MasterCodeSTR.DeptProd || userInfo.DepartmentCode == MasterCodeSTR.DeptPur)
                IsDetailGridButtonFileChooseEnabled = true;
            else
                IsDetailGridButtonFileChooseEnabled = false;

            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("Approval") + "[F10]", IconImageList.GetIconImage("actions/apply"));
            DetailGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"), false);
            DetailGridExControl.MainGrid.AddColumn("OrderSeq", LabelConvert.GetLabelText("Seq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CarType", LabelConvert.GetLabelText("CarType"));
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("OrderQty", LabelConvert.GetLabelText("ReqDevelopmentQty"));
            DetailGridExControl.MainGrid.AddColumn("OrderCost", LabelConvert.GetLabelText("Cost"));
            DetailGridExControl.MainGrid.AddUnboundColumn("OrderAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([OrderQty],0) * ISNULL([OrderCost],0)", FormatType.Numeric, "#,###,###,###.##");
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("ConfirmReqDate", LabelConvert.GetLabelText("ConfirmReqDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("DrawSubmitDate", LabelConvert.GetLabelText("DrawSubmitDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("ConfirmDate", LabelConvert.GetLabelText("ConfirmDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("RfqDate", LabelConvert.GetLabelText("RfqDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("QuoteSubmitDate", LabelConvert.GetLabelText("QuoteSubmitDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("ResrchConfirmDate", LabelConvert.GetLabelText("ResrchConfirm"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("ResrchConfirmId", LabelConvert.GetLabelText("ResrchConfirmId"));
            DetailGridExControl.MainGrid.AddColumn("SalesConfirmDate", LabelConvert.GetLabelText("SalesConfirm"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("SalesConfirmId", LabelConvert.GetLabelText("SalesConfirmId"));
            DetailGridExControl.MainGrid.AddColumn("ProdConfirmDate", LabelConvert.GetLabelText("ProdConfirm"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("ProdConfirmId", LabelConvert.GetLabelText("ProdConfirmId"));
            DetailGridExControl.MainGrid.AddColumn("PurConfirmDate", LabelConvert.GetLabelText("PurConfirm"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("PurConfirmId", LabelConvert.GetLabelText("PurConfirmId"));
            DetailGridExControl.MainGrid.AddColumn("ConfirmState", LabelConvert.GetLabelText("ConfirmState"),false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OrderQty", "OrderCost", "Memo", "ConfirmReqDate", "DrawSubmitDate", "ConfirmDate", "RfqDate", "QuoteSubmitDate");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1001>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OrderId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ConfirmState", DbRequestHandler.GetCommTopCode(MasterCodeSTR.ApprovalState), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.CarType", DbRequestHandler.GetCommCode(MasterCodeSTR.CarType, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderQty", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderCost", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ResrchConfirmId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SalesConfirmId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProdConfirmId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PurConfirmId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ConfirmState", DbRequestHandler.GetCommTopCode(MasterCodeSTR.ApprovalState), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
        }

        protected override void InitCombo()
        {
            lup_OrderCustomerCode.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("OrderNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string customerCode = lup_OrderCustomerCode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OrderDate >= dt_OrderDate.DateFrEdit.DateTime
                                                                         && p.OrderDate <= dt_OrderDate.DateToEdit.DateTime)
                                                                         && (string.IsNullOrEmpty(customerCode) ? true : p.OrderCustomerCode == customerCode)
                                                                         && (p.OrderType == "개발")
                                                                      )
                                                                      .OrderBy(p => p.OrderNo)
                                                                      .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = masterObj.TN_ORD1001List.OrderBy(p => p.OrderSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            MasterGridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);
            param.SetValue(PopupParameter.Value_1, "개발");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000_DEV, param, AddRowPopupCallback);

            form.ShowPopup(true);
        }

        private void AddRowPopupCallback(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null || e.Map == null)
            {
                ActRefresh();
            }
            else
            {
                PopupDataParam.SetValue(PopupParameter.GridRowId_1, e.Map.GetValue(PopupParameter.GridRowId_1));
                ActRefresh();
            }
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            if (masterObj.TN_ORD1001List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("ReqDevelopmentMasterInfo"), LabelConvert.GetLabelText("ReqDevelopmentDetailInfo"), LabelConvert.GetLabelText("ReqDevelopmentDetailInfo")));
                return;
            }
            ModelService.Delete(masterObj);
            MasterGridBindingSource.RemoveCurrent();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Constraint, MasterCodeSTR.Contraint_ItemWAN);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_STD1100, param, AddOrderDetailCallback);
            form.ShowPopup(true);
        }

        private void AddOrderDetailCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            var returnList = (List<TN_STD1100>)e.Map.GetValue(PopupParameter.ReturnObject);
            foreach (var v in returnList)
            {
                var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();
                var newObj = new TN_ORD1001();
                newObj.OrderNo = masterObj.OrderNo;
                newObj.OrderSeq = masterObj.TN_ORD1001List.Count == 0 ? 1 : masterObj.TN_ORD1001List.Max(p => p.OrderSeq) + 1;
                newObj.ItemCode = item.ItemCode;
                newObj.ProductionFlag = "N";
                newObj.OutConfirmFlag = "Y";
                newObj.TurnKeyFlag = "N";
                newObj.NewRowFlag = "Y";
                newObj.TN_STD1100 = item;
                masterObj.TN_ORD1001List.Add(newObj);
                DetailGridBindingSource.Add(newObj);
            }

            //마스터 승인상태 변경
            if (masterObj.TN_ORD1001List.Any(p => (p.ConfirmState == MasterCodeSTR.ApprovalIng)) || masterObj.TN_ORD1001List.Any(p => !(p.ConfirmState == MasterCodeSTR.ApprovalWait)))
            {
                masterObj.ConfirmState = MasterCodeSTR.ApprovalIng;
            }
            else if (masterObj.TN_ORD1001List.All(p => (p.ConfirmState == MasterCodeSTR.ApprovalWait)))
            {
                masterObj.ConfirmState = MasterCodeSTR.ApprovalWait;
            }
            else if (masterObj.TN_ORD1001List.All(p => p.ConfirmState == MasterCodeSTR.ApprovalFinish))
            {
                masterObj.ConfirmState = MasterCodeSTR.ApprovalFinish;
            }
            else
            {
                masterObj.ConfirmState = MasterCodeSTR.ApprovalWait;
            }

            if (returnList.Count > 0) SetIsFormControlChanged(true);
            MasterGridExControl.MainGrid.BestFitColumns();
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (detailObj == null) return;

            if (detailObj.TN_ORD1100List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("ReqDevelopmentDetailInfo"), LabelConvert.GetLabelText("DevelopmentDelivInfo"), LabelConvert.GetLabelText("DevelopmentDelivInfo")));
                return;
            }

            if (detailObj.ConfirmState != MasterCodeSTR.ApprovalWait)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_51), LabelConvert.GetLabelText("ApprovalOK")));
                return;
            }
            
            masterObj.TN_ORD1001List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.BestFitColumns();

            if(masterObj.TN_ORD1001List.Any(p => !(p.ConfirmState == MasterCodeSTR.ApprovalWait || p.ConfirmState == MasterCodeSTR.ApprovalIng)))
            {
                masterObj.ConfirmState = MasterCodeSTR.ApprovalFinish;
                MasterGridExControl.BestFitColumns();
            }
        }

        /// <summary>승인버튼 클릭</summary>
        protected override void DetailFileChooseClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            var detailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (detailObj == null) return;

            var userInfo = ModelService.GetChildList<User>(p => p.UserId == GlobalVariable.UserId).FirstOrDefault();
            if (userInfo.IsNullOrEmpty()) return;

            //연구소
            if(detailObj.ResrchConfirmId.IsNullOrEmpty() && userInfo.DepartmentCode == MasterCodeSTR.DeptRsrch)
            {
                detailObj.ResrchConfirmDate = DateTime.Now;
                detailObj.ResrchConfirmId = GlobalVariable.LoginId;
            }
            //영업부
            else if (detailObj.SalesConfirmId.IsNullOrEmpty() && userInfo.DepartmentCode == MasterCodeSTR.DeptSales)
            {
                detailObj.SalesConfirmDate = DateTime.Now;
                detailObj.SalesConfirmId = GlobalVariable.LoginId;
            }
            //생산부
            else if (detailObj.ProdConfirmId.IsNullOrEmpty() && userInfo.DepartmentCode == MasterCodeSTR.DeptProd)
            {
                detailObj.ProdConfirmDate = DateTime.Now;
                detailObj.ProdConfirmId = GlobalVariable.LoginId;
            }
            //구매부
            else if (detailObj.PurConfirmId.IsNullOrEmpty() && userInfo.DepartmentCode == MasterCodeSTR.DeptPur)
            {
                detailObj.PurConfirmDate = DateTime.Now;
                detailObj.PurConfirmId = GlobalVariable.LoginId;
            }

            //마스터 승인상태 변경
            if (masterObj.TN_ORD1001List.All(p => (p.ConfirmState == MasterCodeSTR.ApprovalWait)))
            {
                masterObj.ConfirmState = MasterCodeSTR.ApprovalWait;
            }
            else if (masterObj.TN_ORD1001List.All(p => p.ConfirmState == MasterCodeSTR.ApprovalFinish))
            {
                masterObj.ConfirmState = MasterCodeSTR.ApprovalFinish;
            }
            else if (masterObj.TN_ORD1001List.All(p => p.ConfirmState == MasterCodeSTR.ApprovalIng))
            {
                masterObj.ConfirmState = MasterCodeSTR.ApprovalIng;
            }
            else
            {
                masterObj.ConfirmState = MasterCodeSTR.ApprovalIng;
            }
            MasterGridExControl.MainGrid.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        private void MainView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
                if (masterObj == null) return;

                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Service, ModelService);
                param.SetValue(PopupParameter.KeyValue, masterObj);
                param.SetValue(PopupParameter.UserRight, UserRight);
                param.SetValue(PopupParameter.EditMode, PopupEditMode.Update);

                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000_DEV, param, PopupRefreshCallback);

                form.ShowPopup(true);
            }
        }

        private void MasterGrid_PrintClickEvent(object sender, ItemClickEventArgs e)
        {
            TN_ORD1000 obj = MasterGridBindingSource.Current as TN_ORD1000;
            if (obj == null) return;

            var report = new REPORT.XRORD1000_DEV(obj);
            report.CreateDocument();

            report.PrintingSystem.ShowMarginsWarning = false;
            report.ShowPrintStatusDialog = false;
            report.ShowPreview();
        }

        private void AddGridButton()
        {
            var printButton = new DevExpress.XtraBars.BarButtonItem();
            printButton.Id = 4;
            printButton.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            printButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            printButton.Name = "PrintButton";
            printButton.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            printButton.ShortcutKeyDisplayString = "Alt+P";
            printButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            printButton.Caption = LabelConvert.GetLabelText("DevelopmentListPrint") + "[Alt+P]";
            printButton.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            printButton.ItemClick += MasterGrid_PrintClickEvent;
            MasterGridExControl.BarTools.AddItem(printButton);
        }
    }
}