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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 수주관리
    /// </summary>
    public partial class XFORD1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1000> ModelService = (IService<TN_ORD1000>)ProductionFactory.GetDomainService("TN_ORD1000");
        private List<VI_PROD_STOCK_ITEM> StockList = new List<VI_PROD_STOCK_ITEM>();
        List<Holiday> holidayList;

        public XFORD1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            dt_OrderDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dt_OrderDate.DateToEdit.DateTime = DateTime.Today;

            MasterGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;

            btn_OrderStatus.Click += Btn_OrdStatus_Click;
        }
        
        protected override void InitCombo()
        {
            lup_Manager.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());  //ModelService.GetChildList<VI_BUSINESS_MANAGEMENT_USER>(p => true).ToList());
            lup_Manager.EditValueChanged += Lup_Manager_EditValueChanged;

            lup_OrderCustomerCode.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Sales || p.CustomerType == null)).ToList());
            lup_OrderCustomerCode.Popup += Lup_OrderCustomerCode_Popup;

            btn_OrderStatus.Text = LabelConvert.GetLabelText("OrderStatus") + "(&O)";
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            MasterGridExControl.MainGrid.AddColumn("OrderDate", LabelConvert.GetLabelText("OrderDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderCustomerCode", LabelConvert.GetLabelText("OrderCustomerCode"), false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("OrderCustomer"));
            MasterGridExControl.MainGrid.AddColumn("OrderManagerName", LabelConvert.GetLabelText("OrderManagerName"));
            MasterGridExControl.MainGrid.AddColumn("OrderDueDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderId", LabelConvert.GetLabelText("OrderId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            //DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "OrderSeq", UserRight.HasEdit);
            //IsDetailGridButtonFileChooseEnabled = UserRight.HasEdit;
            //DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("WorkOrderAdd") + "[Alt+R]", IconImageList.GetIconImage("miscellaneous/wizard"));
            //DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            DetailGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"), false);
            DetailGridExControl.MainGrid.AddColumn("OrderSeq", LabelConvert.GetLabelText("OrderSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CarType", LabelConvert.GetLabelText("CarType"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N0);
            DetailGridExControl.MainGrid.AddColumn("OrderQty", LabelConvert.GetLabelText("OrderQty"));
            DetailGridExControl.MainGrid.AddColumn("OrderCost", LabelConvert.GetLabelText("Cost"));
            DetailGridExControl.MainGrid.AddUnboundColumn("OrderAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([OrderQty],0) * ISNULL([OrderCost],0)", FormatType.Numeric, "#,###,###,###.##");
            
            //DetailGridExControl.MainGrid.AddColumn("WorkNoDate", LabelConvert.GetLabelText("PublishDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            //DetailGridExControl.MainGrid.AddColumn("DlvDate", LabelConvert.GetLabelText("StartDueDate"));
            //DetailGridExControl.MainGrid.AddColumn("PlanStartDate", LabelConvert.GetLabelText("PlanStartDate"));
            //DetailGridExControl.MainGrid.AddColumn("PlanEndDate", LabelConvert.GetLabelText("FinishDueDate"));
            //DetailGridExControl.MainGrid.AddColumn("PlanWorkQty", LabelConvert.GetLabelText("PlanWorkQty"));
            //DetailGridExControl.MainGrid.AddColumn("OutConfirmFlag", LabelConvert.GetLabelText("OutConfirmFlag"));
            //DetailGridExControl.MainGrid.AddColumn("TurnKeyFlag", LabelConvert.GetLabelText("TurnKeyFlag"));
            //DetailGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            //DetailGridExControl.MainGrid.AddColumn("WorkPrintFlag", LabelConvert.GetLabelText("WorkPrintFlag"));

            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OrderQty","OrderCost", "Memo");

            //var barButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            //barButtonPrint.Id = 4;
            //barButtonPrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            //barButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            //barButtonPrint.Name = "barButtonPrint";
            //barButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            //barButtonPrint.ShortcutKeyDisplayString = "Alt+P";
            //barButtonPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            //barButtonPrint.Caption = LabelConvert.GetLabelText("WorkOrderPrint") + "[Alt+P]";
            //barButtonPrint.ItemClick += BarButtonPrint_ItemClick;
            //barButtonPrint.Alignment = BarItemLinkAlignment.Right;
            //DetailGridExControl.BarTools.AddItem(barButtonPrint);

            //var barButtonWorkOrderDelete = new DevExpress.XtraBars.BarButtonItem();
            //barButtonWorkOrderDelete.Id = 5;
            //barButtonWorkOrderDelete.ImageOptions.Image = IconImageList.GetIconImage("spreadsheet/hidedetail");
            //barButtonWorkOrderDelete.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.T));
            //barButtonWorkOrderDelete.Name = "barButtonWorkOrderDelete";
            //barButtonWorkOrderDelete.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            //barButtonWorkOrderDelete.ShortcutKeyDisplayString = "Alt+T";
            //barButtonWorkOrderDelete.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            //barButtonWorkOrderDelete.Caption = LabelConvert.GetLabelText("WorkOrderDelete") + "[Alt+T]";
            //barButtonWorkOrderDelete.ItemClick += BarButtonWorkOrderDelete_ItemClick;
            //DetailGridExControl.BarTools.AddItem(barButtonWorkOrderDelete);

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1001>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OrderId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.CarType", DbRequestHandler.GetCommCode(MasterCodeSTR.CarType, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderQty", DefaultBoolean.Default, "n0");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderCost", DefaultBoolean.Default, "n2");

            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            //DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanStartDate");
            //DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanEndDate");
            //DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("DlvDate");
            //DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("PlanWorkQty", DefaultBoolean.Default, "n0");
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutConfirmFlag", "N");
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("TurnKeyFlag", "N");
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("WorkPrintFlag", "N");

            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit, 100, 30);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            if (!IsFirstLoaded)
            {
                holidayList = ModelService.GetChildList<Holiday>(p => true).ToList();
            }

            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("OrderNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            StockList.Clear();
            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            string customerCode = lup_OrderCustomerCode.EditValue.GetNullToEmpty();
            string manager = lup_Manager.EditValue.GetNullToEmpty();


            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OrderDate >= dt_OrderDate.DateFrEdit.DateTime
                                                                         && p.OrderDate <= dt_OrderDate.DateToEdit.DateTime)
                                                                         && (string.IsNullOrEmpty(customerCode) ? true : p.OrderCustomerCode == customerCode)
                                                                         && (string.IsNullOrEmpty(manager) ? true : p.OrderId == manager)
                                                                         && (p.OrderType == "양산")
                                                                      )
                                                                      .OrderBy(p => p.OrderNo)
                                                                      .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);

            //생산테이블 부재로 임시 주석처리 20211021
            ////재고량 가져오기
            //StockList.AddRange(ModelService.GetChildList<VI_PROD_STOCK_ITEM>(p => true).ToList());
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

            //#region 품목단가이력 I/F 

            //if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            //{
            //    var masterList = MasterGridBindingSource.List as List<TN_ORD1000>;
            //    if (masterList.Count > 0)
            //    {
            //        foreach (var v in masterList.Where(p => p.TN_ORD1001List.Any(c => c.NewRowFlag == "Y" || c.EditRowFlag == "Y")).ToList())
            //        {
            //            foreach (var d in v.TN_ORD1001List.Where(c => c.NewRowFlag == "Y" || c.EditRowFlag == "Y").ToList())
            //            {
            //                if (d.OrderCost > 0)
            //                {
            //                    TN_STD1103 old = ModelService.GetChildList<TN_STD1103>(p => p.CustomerCode == v.OrderCustomerCode && p.ItemCode == d.ItemCode && p.ChangeDate == DateTime.Today).FirstOrDefault();
            //                    if (old == null)
            //                    {
            //                        var newObj = new TN_STD1103()
            //                        {
            //                            ItemCode = d.ItemCode,
            //                            Seq = d.TN_STD1100.TN_STD1103List.Count == 0 ? 1 : d.TN_STD1100.TN_STD1103List.Max(p => p.Seq) + 1,
            //                            CustomerCode = v.OrderCustomerCode,
            //                            ChangeDate = DateTime.Today,
            //                            ChangeCost = d.OrderCost
            //                        };
            //                        d.TN_STD1100.TN_STD1103List.Add(newObj);
            //                    }
            //                    else
            //                    {
            //                        old.ChangeCost = d.OrderCost;
            //                        d.TN_STD1100.TN_STD1103List.Remove(old);
            //                        d.TN_STD1100.TN_STD1103List.Add(old);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //#endregion

            //#region 품목단가이력 I/F 
            //if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            //{
            //    var masterList = MasterGridBindingSource.List as List<TN_ORD1000>;
            //    if (masterList.Count > 0)
            //    {
            //        foreach (var v in masterList.Where(p => p.TN_ORD1001List.Any(c => 1==1)).ToList())
            //        {
            //            foreach (var d in v.TN_ORD1001List.Where(p => 1==1))
            //            {
            //                TN_STD1103 old = ModelService.GetChildList<TN_STD1103>(p =>p.CustomerCode== v.OrderCustomerCode && p.ItemCode == d.ItemCode && p.ChangeDate == DateTime.Today).FirstOrDefault();
            //                if (old == null)
            //                {

            //                    var newObj = new TN_STD1103()
            //                    {
            //                        ItemCode = d.ItemCode,
            //                        Seq = d.TN_STD1100.TN_STD1103List.Count == 0 ? 1 : d.TN_STD1100.TN_STD1103List.Max(p => p.Seq) + 1,
            //                        CustomerCode = v.OrderCustomerCode,
            //                        ChangeDate = DateTime.Today,
            //                        ChangeCost = d.OrderCost
            //                    };
            //                    d.TN_STD1100.TN_STD1103List.Add(newObj);
            //                }
            //                else {
            //                    old.ChangeCost = d.OrderCost;
            //                    d.TN_STD1100.TN_STD1103List.Remove(old);
            //                    d.TN_STD1100.TN_STD1103List.Add(old);
            //                }
            //            }
            //        }
            //    }
            //}
            //#endregion

            //#region 품목단가이력 I/F 
            //if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            //{
            //    var masterList = MasterGridBindingSource.List as List<TN_ORD1000>;
            //    if (masterList.Count > 0)
            //    {
            //        foreach (var v in masterList.Where(p => p.TN_ORD1001List.Any(c => c.NewRowFlag == "Y")).ToList())
            //        {
            //            foreach (var d in v.TN_ORD1001List.Where(p => p.NewRowFlag == "Y"))
            //            {
            //                var newObj = new TN_STD1103()
            //                {
            //                    ItemCode = d.ItemCode,
            //                    Seq = d.TN_STD1100.TN_STD1103List.Count == 0 ? 1 : d.TN_STD1100.TN_STD1103List.Max(p => p.Seq) + 1,
            //                    CustomerCode = v.OrderCustomerCode,
            //                    ChangeDate = DateTime.Today,
            //                    ChangeCost = d.OrderCost
            //                };
            //                d.TN_STD1100.TN_STD1103List.Add(newObj);
            //            }
            //        }
            //    }
            //}
            //#endregion

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
            param.SetValue(PopupParameter.Value_1, "양산");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000, param, AddRowPopupCallback);

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
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("OrderMasterInfo"), LabelConvert.GetLabelText("OrderDetailInfo"), LabelConvert.GetLabelText("OrderDetailInfo")));
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
            param.SetValue(PopupParameter.Value_1, masterObj.OrderCustomerCode);
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
                TN_STD1103 obj1 = ModelService.GetChildList<TN_STD1103>(p => p.CustomerCode == masterObj.OrderCustomerCode && p.ItemCode ==  v.ItemCode).OrderByDescending(o => o.ChangeDate).FirstOrDefault();
                var newObj = new TN_ORD1001();
                newObj.OrderNo = masterObj.OrderNo;
                newObj.OrderSeq = masterObj.TN_ORD1001List.Count == 0 ? 1 : masterObj.TN_ORD1001List.Max(p => p.OrderSeq) + 1;
                newObj.ItemCode = item.ItemCode;
                newObj.TN_STD1100 = item;
                newObj.OrderCost = obj1 == null ? item.Cost.GetDecimalNullToZero() : obj1.ChangeCost;
                newObj.ProductionFlag = "N";
                newObj.OutConfirmFlag = "Y";
                newObj.TurnKeyFlag = "N";
                newObj.NewRowFlag = "Y";
                newObj.DlvDate = masterObj.OrderDueDate;
                masterObj.TN_ORD1001List.Add(newObj);
                DetailGridBindingSource.Add(newObj);
            }
            if (returnList.Count > 0) SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (detailObj == null) return;

            if(detailObj.TN_ORD1100List.Count > 0)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_102));
                return;
            }

            masterObj.TN_ORD1001List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.BestFitColumns();

            //var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            //if (masterObj == null) return;

            //var detailList = DetailGridBindingSource.List as List<TN_ORD1001>;
            //if (detailList == null) return;

            //var checkList = detailList.Where(p => p._Check == "Y").ToList();
            //if (checkList.Count == 0) return;

            //if (checkList.Any(p => !p.WorkNo.IsNullOrEmpty()))
            //{
            //    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_100));
            //    return;
            //}

            //if (checkList.Any(p => p.TN_ORD1100List.Any(c => c.TN_ORD1101List.Count > 0)))
            //{
            //    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_102));
            //    return;
            //}

            //if (checkList.Any(p => p.TN_ORD1100List.Any(c => c.TN_ORD1102List.Count > 0)))
            //{
            //    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_103));
            //    return;
            //}

            //if (checkList.Any(p => p.TN_ORD1100List.Any(c => c.TN_ORD1200List.Count > 0)))
            //{
            //    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_104));
            //    return;
            //}

            //if (checkList.Any(p => p.TN_ORD1100List.Any(c => c.TN_PUR1600List.Count > 0)))
            //{
            //    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_105));
            //    return;
            //}

            //foreach (var v in checkList)
            //{
            //    masterObj.TN_ORD1001List.Remove(v);
            //    DetailGridBindingSource.Remove(v);
            //    DetailGridExControl.BestFitColumns();
            //}

            //var detailObj = DetailGridBindingSource.Current as TN_ORD1001;
            //if (detailObj == null) return;

            //if (detailObj.TN_ORD1100List.Count > 0)
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("OrderDetailInfo"), LabelConvert.GetLabelText("DelivInfo"), LabelConvert.GetLabelText("DelivInfo")));
            //    return;
            //}

            //masterObj.TN_ORD1001List.Remove(detailObj);
            //DetailGridBindingSource.RemoveCurrent();
            //DetailGridExControl.BestFitColumns();
        }

        ///// <summary>
        ///// 작업지시서생성 이벤트
        ///// </summary>
        //protected override void DetailFileChooseClicked()
        //{
        //    var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
        //    if (masterObj == null) return;

        //    var detailList = DetailGridBindingSource.List as List<TN_ORD1001>;
        //    if (detailList == null) return;

        //    var checkList = detailList.Where(p => p._Check == "Y").ToList();
        //    if (checkList.Count == 0) return;

        //    if (checkList.Any(p => p.NewRowFlag == "Y"))
        //    {
        //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_99));
        //        return;
        //    }

        //    if (checkList.Any(p => !p.WorkNo.IsNullOrEmpty()))
        //    {
        //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_100));
        //        return;
        //    }

        //    if (checkList.Any(p => p.TurnKeyFlag == "Y"))
        //    {
        //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_106));
        //        return;
        //    }

        //    if (checkList.Any(p => p.PlanWorkQty.GetDecimalNullToZero() == 0))
        //    {
        //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_107));
        //        return;
        //    }

        //    if (checkList.Any(p => p.PlanStartDate == null || p.PlanEndDate == null))
        //    {
        //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_108));
        //        return;
        //    }

        //    foreach (var v in checkList)
        //    {
        //        var TN_MPS1000List = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == v.ItemCode && p.UseFlag == "Y").OrderBy(o => o.ProcessSeq).ToList();
        //        if (TN_MPS1000List.Count == 0)
        //        {
        //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemProcess")));
        //            return;
        //        }

        //        #region 반제품 Process Check
        //        var wanBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == v.ItemCode).FirstOrDefault();
        //        if (wanBomObj != null)
        //        {
        //            var banBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ParentBomCode == wanBomObj.BomCode).FirstOrDefault();
        //            if (banBomObj != null)
        //            {
        //                var TN_MPS1000List_BAN = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == banBomObj.ItemCode && p.UseFlag == "Y").OrderBy(o => o.ProcessSeq).ToList();
        //                if (TN_MPS1000List_BAN.Count == 0)
        //                {
        //                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_78)));
        //                    return;
        //                }
        //            }
        //        }
        //        #endregion
        //    }

        //    foreach (var v in checkList)
        //    {
        //        v.ProductionFlag = "Y";
        //        v.TN_ORD1100List.First().ProductionFlag = "Y";
        //        v.WorkNoDate = DateTime.Today;

        //        //생산계획생성
        //        var TN_MPS1100_NEW = new TN_MPS1100()
        //        {
        //            PlanNo = DbRequestHandler.GetSeqMonth("WP"),
        //            OrderNo = v.OrderNo,
        //            OrderSeq = v.OrderSeq,
        //            DelivNo = v.TN_ORD1100List.First().DelivNo,
        //            ItemCode = v.ItemCode,
        //            CustomerCode = masterObj.OrderCustomerCode,
        //            PlanQty = v.PlanWorkQty.GetDecimalNullToZero(),
        //            PlanStartDate = (DateTime)v.PlanStartDate,
        //            PlanEndDate = (DateTime)v.PlanEndDate,
        //            TN_ORD1100 = v.TN_ORD1100List.First(),
        //        };
        //        v.TN_ORD1100List.First().TN_MPS1100List.Add(TN_MPS1100_NEW);

        //        //작업지시생성
        //        var TN_MPS1000List = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == v.ItemCode && p.UseFlag == "Y").OrderBy(o => o.ProcessSeq).ToList();
        //        if (TN_MPS1000List.Count == 0)
        //        {
        //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemProcess")));
        //            return;
        //        }

        //        #region 반제품 Process Check
        //        var wanBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == v.ItemCode).FirstOrDefault();
        //        if (wanBomObj != null)
        //        {
        //            var banBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ParentBomCode == wanBomObj.BomCode).FirstOrDefault();
        //            if (banBomObj != null)
        //            {
        //                var TN_MPS1000List_BAN = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == banBomObj.ItemCode && p.UseFlag == "Y").OrderBy(o => o.ProcessSeq).ToList();
        //                if (TN_MPS1000List_BAN.Count == 0)
        //                {
        //                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_78)));
        //                    return;
        //                }
        //                AddBanProcess(TN_MPS1100_NEW, banBomObj.ItemCode, TN_MPS1000List_BAN, TN_MPS1000List, (DateTime)v.WorkNoDate);
        //            }
        //            else
        //            {
        //                AddProcess(TN_MPS1100_NEW, v.ItemCode, TN_MPS1000List, (DateTime)v.WorkNoDate);
        //            }
        //        }
        //        else
        //        {
        //            AddProcess(TN_MPS1100_NEW, v.ItemCode, TN_MPS1000List, (DateTime)v.WorkNoDate);
        //        }
        //        #endregion


        //    }

        //    ActSave();
        //}

        ///// <summary>
        ///// 작업지시서삭제 이벤트
        ///// </summary>
        //private void BarButtonWorkOrderDelete_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
        //    if (masterObj == null) return;

        //    var detailList = DetailGridBindingSource.List as List<TN_ORD1001>;
        //    if (detailList == null) return;

        //    var checkList = detailList.Where(p => p._Check == "Y").ToList();
        //    if (checkList.Count == 0) return;

        //    if (checkList.Any(p => p.NewRowFlag == "Y"))
        //    {
        //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_99));
        //        return;
        //    }

        //    if (checkList.Any(p => p.WorkNo.IsNullOrEmpty()))
        //    {
        //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_109));
        //        return;
        //    }

        //    if (checkList.Any(p => p.TN_ORD1100List.Any(c => c.TN_MPS1100List.Any(x => x.TN_MPS1200List.Any(z => z.WorkNo == p.WorkNo && z.JobStates != MasterCodeSTR.JobStates_Wait)))))
        //    {
        //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_110));
        //        return;
        //    }

        //    if (checkList.Any(p => p.TN_ORD1100List.Any(c => c.TN_MPS1100List.Any(x => x.TN_MPS1200List.Any(z => z.WorkNo == p.WorkNo && z.JobStates != MasterCodeSTR.JobStates_Wait)))))
        //    {
        //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_110));
        //        return;
        //    }

        //    if (checkList.Any(p => p.WorkPrintFlag == "Y"))
        //    {
        //        if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_101), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.No) 
        //            return;
        //    }

        //    foreach (var v in checkList)
        //    {
        //        var TN_MPS1200List = v.TN_ORD1100List.First().TN_MPS1100List.First().TN_MPS1200List.ToList();
        //        foreach (var d in TN_MPS1200List)
        //        {
        //            var delete_TN_MPS1200List = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == d.WorkNo).ToList();
        //            foreach (var c in delete_TN_MPS1200List)
        //            {
        //                if (c.Temp.IsNullOrEmpty())
        //                {
        //                    ModelService.RemoveChild(c);
        //                }
        //                else
        //                {
        //                    //반제품이 있을 경우
        //                    bool banDeleteFlag = false;
        //                    if (!banDeleteFlag)
        //                    {
        //                        var delete_Ban_TN_MPS1200List = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == c.Temp).ToList();
        //                        foreach (var x in delete_Ban_TN_MPS1200List)
        //                        {
        //                            if (x.JobStates != MasterCodeSTR.JobStates_Wait)
        //                            {                                        
        //                                //if (MessageBoxHandler.Show("반제품 작업지시에 대하여 진행된 항목이 존재합니다. 반제품을 제외하고 삭제하시겠습니까?", LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.No)
        //                                if (MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_114), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.No)
        //                                {
        //                                    ActRefresh();
        //                                    return;
        //                                }
        //                            }
        //                            else
        //                                ModelService.RemoveChild(x);
        //                        }
        //                        banDeleteFlag = true;
        //                    }
        //                    ModelService.RemoveChild(c);
        //                }
        //            }
        //        }

        //        var planNo = v.TN_ORD1100List.First().TN_MPS1100List.First().PlanNo;
        //        ModelService.RemoveChild(ModelService.GetChildList<TN_MPS1100>(p => p.PlanNo == planNo).First());

        //        //var TN_MPS1100List = v.TN_ORD1100List.First().TN_MPS1100List.ToList();
        //        //foreach (var d in TN_MPS1100List)
        //        //    v.TN_ORD1100List.First().TN_MPS1100List.Remove(d);

        //        v.WorkNo = null;
        //        v.WorkPrintFlag = "N";
        //        v.ProductionFlag = "N";
        //        v.TN_ORD1100List.First().ProductionFlag = "N";
        //        v.WorkNoDate = null;
        //    }

        //    ActSave();
        //}

        ///// <summary>
        ///// 작업지시서출력 이벤트
        ///// </summary>
        //private void BarButtonPrint_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
        //    if (masterObj == null) return;

        //    var detailList = DetailGridBindingSource.List as List<TN_ORD1001>;
        //    if (detailList == null) return;

        //    var checkList = detailList.Where(p => p._Check == "Y").ToList();
        //    if (checkList.Count == 0) return;

        //    if (checkList.Any(p => p.NewRowFlag == "Y"))
        //    {
        //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_99));
        //        return;
        //    }

        //    if (checkList.Any(p => p.WorkNo.IsNullOrEmpty()))
        //    {
        //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_109));
        //        return;
        //    }
        //    try
        //    {
        //        WaitHandler.ShowWait();

        //        var mainReport = new REPORT.XRMPS1200();

        //        foreach (var v in checkList)
        //        {
        //            var report = new REPORT.XRMPS1200(ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == v.WorkNo).OrderBy(p => p.ProcessSeq).First());
        //            report.CreateDocument();
        //            mainReport.Pages.AddRange(report.Pages);
        //            v.WorkPrintFlag = "Y";
        //        }
        //        mainReport.PrintingSystem.ShowMarginsWarning = false;
        //        mainReport.ShowPrintStatusDialog = false;
        //        mainReport.ShowPreview();
        //    }
        //    catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
        //    finally
        //    {
        //        WaitHandler.CloseWait();
        //    }
        //    ActSave();
        //}

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

                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000, param, PopupRefreshCallback);

                form.ShowPopup(true);
            }
        }

        /// <summary> 수주현황 </summary>
        private void Btn_OrdStatus_Click(object sender, EventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                PopupDataParam param = new PopupDataParam();
                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_ORD_STATUS, param, null);
                form.ShowPopup(false);
            }
            finally { WaitHandler.CloseWait(); }
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = sender as GridView;
            var detailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (e.Column.FieldName == "OrderCost")
                detailObj.EditRowFlag = "Y";
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            var FieldName = view.FocusedColumn.FieldName;
            var SubObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (FieldName == "ProductionFlag")
            {
                if (SubObj.TurnKeyFlag == "Y")
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_55), LabelConvert.GetLabelText("TurnKeyFlag")));
                    e.Cancel = true;
                }
            }
            //else if (FieldName == "TurnKeyFlag")
            //{
            //    if (SubObj.ProductionFlag == "Y")
            //    {
            //        //MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_55), LabelConvert.GetLabelText("ProductionFlag")));
            //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_100));
            //        e.Cancel = true;
            //    }
            //    else if (SubObj.TN_ORD1100List.First().TN_PUR1600List.Count > 0)
            //    {
            //        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_52), LabelConvert.GetLabelText("TurnKeyInfo")));
            //        e.Cancel = true;
            //    }
            //}
        }

        //private void AddProcess(TN_MPS1100 masterObj, string itemCode, List<TN_MPS1000> TN_MPS1000List, DateTime planStartDate)
        //{
        //    string WorkNo = DbRequestHandler.GetSeqMonth("WNO");
        //    DateTime dt = planStartDate;

        //    bool FirstInsertFlag = true;
        //    foreach (var v in TN_MPS1000List)
        //    {
        //        if (!FirstInsertFlag)
        //            dt = dt.AddDays(v.StdWorkDay.GetIntNullToZero());

        //        dt = CheckHolidayDate(dt.AddDays(-1), 1);

        //        var newObj = new TN_MPS1200()
        //        {
        //            WorkNo = WorkNo,
        //            ProcessCode = v.ProcessCode,
        //            ProcessSeq = v.ProcessSeq,
        //            PlanNo = masterObj.PlanNo,
        //            ItemCode = itemCode,
        //            MachineGroupCode = v.MachineGroupCode,
        //            CustomerCode = masterObj.CustomerCode,
        //            EmergencyFlag = "N",
        //            WorkDate = dt,
        //            WorkQty = masterObj.PlanQty,
        //            OutProcFlag = v.OutProcFlag,
        //            MachineFlag = v.MachineFlag,
        //            ToolUseFlag = v.ToolUseFlag,
        //            JobSettingFlag = v.JobSettingFlag,
        //            JobStates = MasterCodeSTR.JobStates_Wait,
        //            TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itemCode).First(),
        //            NewRowFlag = "Y"
        //        };
        //        masterObj.TN_MPS1200List.Add(newObj);
        //        FirstInsertFlag = false;
        //    }

        //    masterObj.TN_ORD1100.TN_ORD1001.WorkNo = WorkNo;
        //}

        //private void AddBanProcess(TN_MPS1100 masterObj, string itemCode, List<TN_MPS1000> TN_MPS1000List_BAN, List<TN_MPS1000> TN_MPS1000List, DateTime planStartDate)
        //{
        //    string WorkNo = DbRequestHandler.GetSeqMonth("WNO");
        //    DateTime dt = planStartDate;

        //    bool FirstInsertFlag = true;
        //    foreach (var v in TN_MPS1000List_BAN)
        //    {
        //        if (!FirstInsertFlag)
        //            dt = dt.AddDays(v.StdWorkDay.GetIntNullToZero());

        //        dt = CheckHolidayDate(dt.AddDays(-1), 1);

        //        var newObj = new TN_MPS1200()
        //        {
        //            WorkNo = WorkNo,
        //            ProcessCode = v.ProcessCode,
        //            ProcessSeq = v.ProcessSeq,
        //            PlanNo = masterObj.PlanNo,
        //            ItemCode = itemCode,
        //            MachineGroupCode = v.MachineGroupCode,
        //            CustomerCode = masterObj.CustomerCode,
        //            EmergencyFlag = "N",
        //            WorkDate = dt,
        //            WorkQty = masterObj.PlanQty,
        //            OutProcFlag = v.OutProcFlag,
        //            MachineFlag = v.MachineFlag,
        //            ToolUseFlag = v.ToolUseFlag,
        //            JobSettingFlag = v.JobSettingFlag,
        //            JobStates = MasterCodeSTR.JobStates_Wait,
        //            TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itemCode).First(),
        //            NewRowFlag = "Y"
        //        };
        //        masterObj.TN_MPS1200List.Add(newObj);
        //        FirstInsertFlag = false;
        //    }

        //    var banWorkNo = WorkNo;
        //    WorkNo = DbRequestHandler.GetSeqMonth("WNO");
        //    dt = planStartDate;
        //    FirstInsertFlag = true;

        //    foreach (var v in TN_MPS1000List)
        //    {
        //        if (!FirstInsertFlag)
        //            dt = dt.AddDays(v.StdWorkDay.GetIntNullToZero());

        //        dt = CheckHolidayDate(dt.AddDays(-1), 1);

        //        var newObj = new TN_MPS1200()
        //        {
        //            WorkNo = WorkNo,
        //            ProcessCode = v.ProcessCode,
        //            ProcessSeq = v.ProcessSeq,
        //            PlanNo = masterObj.PlanNo,
        //            ItemCode = masterObj.ItemCode,
        //            MachineGroupCode = v.MachineGroupCode,
        //            CustomerCode = masterObj.CustomerCode,
        //            EmergencyFlag = "N",
        //            WorkDate = dt,
        //            WorkQty = masterObj.PlanQty,
        //            OutProcFlag = v.OutProcFlag,
        //            MachineFlag = v.MachineFlag,
        //            ToolUseFlag = v.ToolUseFlag,
        //            JobSettingFlag = v.JobSettingFlag,
        //            JobStates = MasterCodeSTR.JobStates_Wait,
        //            TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == masterObj.ItemCode).First(),
        //            NewRowFlag = "Y",
        //            Temp = banWorkNo,
        //        };
        //        masterObj.TN_MPS1200List.Add(newObj);
        //        FirstInsertFlag = false;
        //    }

        //    masterObj.TN_ORD1100.TN_ORD1001.WorkNo = WorkNo;
        //}

        ///// <summary>
        ///// 휴일체크 재귀함수
        ///// </summary>
        //private DateTime CheckHolidayDate(DateTime date, int changeQty)
        //{
        //    var addDate = date.AddDays(changeQty);

        //    if (changeQty > 0)
        //    {
        //        if (holidayList.Any(p => (p.HolidayFlag == "Y" && p.OvertimeFlag == "N") && p.Date == addDate))
        //            return CheckHolidayDate(addDate, 1);
        //        else
        //            return addDate;
        //    }
        //    else if (changeQty < 0)
        //    {
        //        if (holidayList.Any(p => (p.HolidayFlag == "Y" && p.OvertimeFlag == "N") && p.Date == addDate))
        //            return CheckHolidayDate(addDate, -1);
        //        else
        //            return addDate;
        //    }
        //    else
        //    {
        //        return addDate;
        //    }
        //}

        private void Lup_Manager_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            lup_OrderCustomerCode.EditValue = null;
        }

        private void Lup_OrderCustomerCode_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var manager = lup_Manager.EditValue.GetNullToNull();

            if (manager.IsNullOrEmpty())
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            else
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[BusinessManagementId] = '" + manager + "'";
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "StockQty")
            {
                var itemCode = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ItemCode").GetNullToEmpty();
                if (!itemCode.IsNullOrEmpty())
                {
                    var stockObj = StockList.Where(p => p.ItemCode == itemCode).FirstOrDefault();
                    e.DisplayText = stockObj == null ? "0" : stockObj.SumStockQty.ToString("#,#.##");
                }
            }
        }


    }
}