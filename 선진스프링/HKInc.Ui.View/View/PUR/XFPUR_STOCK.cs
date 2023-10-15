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
using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.PUR
{
    /// <summary>
    /// 자재재고관리
    /// </summary>
    public partial class XFPUR_STOCK : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PUR_STOCK_ITEM> ModelService = (IService<VI_PUR_STOCK_ITEM>)ProductionFactory.GetDomainService("VI_PUR_STOCK_ITEM");
        private string searchDivision;
        private DateTime? searchYearMonthDate;
        private string searchItemCode;

     //   List<VI_PUR_STOCK_IN_LOT_NO> VI_PUR_STOCK_IN_LOT_NO_LIST = new List<VI_PUR_STOCK_IN_LOT_NO>();

        public XFPUR_STOCK()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            
            lup_Item.Popup += Lup_MiddleCategory_Popup;

            btn_PurchaseStatus.Click += Btn_PurchaseStatus_Click;

            DetailGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
        }
        private void Lup_MiddleCategory_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var value = lup_TopCategory.EditValue.GetNullToEmpty();

            if (value.IsNullOrEmpty())
            {
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            }
            else
            {
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[TopCategory] = '" + value + "'";
            }
        }
        private void MainView_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "StockQty")
            {
                var division = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "Division").GetNullToEmpty();
                if (division == "이월")
                {
                    var inqty = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "InOutQty").GetNullToZero();
                    var outqty = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "OutQty").GetNullToZero();
                    e.DisplayText = (inqty-outqty) == 0 ? "0" : (inqty - outqty).ToString("#,0.##");

                }
                else {
                    var inLotNo = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "InOutLotNo").GetNullToEmpty();
                    var stockObj = ModelService.GetChildList<VI_PUR_STOCK_IN_LOT_NO>(p => p.InLotNo == inLotNo).FirstOrDefault();
                    e.DisplayText = stockObj == null ? "0" : stockObj.StockQty.ToString("#,0.##");
                }
                //if (division == "입고")
                //{
                //    var inLotNo = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "InOutLotNo").GetNullToEmpty();
                //    var stockObj = ModelService.GetChildList<VI_PUR_STOCK_IN_LOT_NO>(p => p.InLotNo == inLotNo).FirstOrDefault();
                //    e.DisplayText = stockObj == null ? "0" : stockObj.StockQty.ToString("#,0.##");
                //}
                //if (division == "재입고")
                //{
                //    var inLotNo = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "InOutLotNo").GetNullToEmpty();
                //    var stockObj = ModelService.GetChildList<VI_PUR_STOCK_IN_LOT_NO>(p => p.InLotNo == inLotNo).FirstOrDefault(); //;VI_PUR_STOCK_IN_LOT_NO_LIST.Where(p => p.InLotNo == inLotNo).FirstOrDefault();
                //    e.DisplayText = stockObj == null ? "0" : stockObj.StockQty.ToString("#,0.##");
                //}

            }
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_MAT || p.TopCategory == MasterCodeSTR.TopCategory_BU)).ToList());

            btn_PurchaseStatus.Text = LabelConvert.GetLabelText("PurchaseStatus") + "(&F)";

            cbo_YearMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            lup_TopCategory.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1).Where(p => p.CodeVal != MasterCodeSTR.TopCategory_SPARE && p.CodeVal != MasterCodeSTR.TopCategory_TOOL && p.CodeVal != MasterCodeSTR.TopCategory_BAN && p.CodeVal != MasterCodeSTR.TopCategory_WAN).ToList());
            var nowDate = DateTime.Today.Day >= 20 ? DateTime.Today.AddMonths(1) : DateTime.Today;
            //cbo_YearMonth.Properties.Items.Add("당월");
            cbo_YearMonth.Properties.Items.Add(nowDate.ToString("yyyy-MM"));

            var deadLineDateList = ModelService.GetChildList<TN_MAT_DEAD_MST>(p => p.Division == MasterCodeSTR.DeadLineDivision_DeadConfirm).Select(p => p.DeadLineDate).ToList();
            foreach (var v in deadLineDateList.OrderByDescending(p => p.Date)) 
            {
                cbo_YearMonth.Properties.Items.Add(v.ToString("yyyy-MM"));
            }

            cbo_YearMonth.EditValue = nowDate.ToString("yyyy-MM");// "당월";
            searchDivision = cbo_YearMonth.EditValue.GetNullToEmpty();
            
            MasterGridExControl.SetToolbarVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(false);
            IsDetailGridButtonFileChooseEnabled = UserRight.HasEdit;
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, UserRight.HasEdit);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("ReturnAdjust") + "[F10]", IconImageList.GetIconImage("spreadsheet/showdetail"));
            DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);

            var barButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonPrint.Id = 5;
            barButtonPrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            barButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barButtonPrint.Name = "barButtonPrint";
            barButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonPrint.ShortcutKeyDisplayString = "Alt+P";
            barButtonPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonPrint.Caption = LabelConvert.GetLabelText("BarcodePrint") + "[Alt+P]";
            barButtonPrint.ItemClick += BarButtonPrint_ItemClick;
            barButtonPrint.Alignment = BarItemLinkAlignment.Right;
            DetailGridExControl.BarTools.AddItem(barButtonPrint);
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));

            var nowDate = DateTime.Today.Day >= 20 ? DateTime.Today.AddMonths(1) : DateTime.Today;
            if (searchDivision == nowDate.ToString("yyyy-MM"))//"당월")
            {
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"));
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100.SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "N0");
            }
            else
            {
                MasterGridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
                MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
                MasterGridExControl.MainGrid.AddColumn("Spec", LabelConvert.GetLabelText("Spec"));
                MasterGridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));
                MasterGridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "N0");
            }
                
            MasterGridExControl.MainGrid.AddColumn("SumInQty", LabelConvert.GetLabelText("SumInQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumOutQty", LabelConvert.GetLabelText("SumOutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumCarryOverQty", LabelConvert.GetLabelText("SumCarryOverQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            if (searchDivision != nowDate.ToString("yyyy-MM"))//"당월")
                MasterGridExControl.MainGrid.AddColumn("SumAdjustQty", LabelConvert.GetLabelText("SumAdjustQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumStockQty", LabelConvert.GetLabelText("SumStockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            if (searchDivision == nowDate.ToString("yyyy-MM"))//"당월")
            {
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Cost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
                MasterGridExControl.MainGrid.AddUnboundColumn("StockAmt", "재고금액", DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([TN_STD1100.Cost],0) * SumStockQty", FormatType.Numeric, "#,0.##");
            }
            else
            {
                MasterGridExControl.MainGrid.AddColumn("Cost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
                MasterGridExControl.MainGrid.AddUnboundColumn("StockAmt", "재고금액", DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([Cost],0) * SumStockQty", FormatType.Numeric, "#,0.##");
            }

            MasterGridExControl.MainGrid.ShowFooter = true;
            MasterGridExControl.MainGrid.MainView.Columns["SumStockQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["SumStockQty"].SummaryItem.FieldName = "SumStockQty";
            MasterGridExControl.MainGrid.MainView.Columns["SumStockQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            MasterGridExControl.MainGrid.MainView.Columns["StockAmt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["StockAmt"].SummaryItem.FieldName = "StockAmt";
            MasterGridExControl.MainGrid.MainView.Columns["StockAmt"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasSelect, "RowIndex", true);
            DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            DetailGridExControl.MainGrid.AddColumn("RowIndex", LabelConvert.GetLabelText("RowIndex"), HorzAlignment.Far, FormatType.Numeric, "#,0.##", false);
            DetailGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("InOutDate", LabelConvert.GetLabelText("InOutDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("InOutLotNo", LabelConvert.GetLabelText("InOutLotNo"));
            DetailGridExControl.MainGrid.AddColumn("InOutQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("InOutId", LabelConvert.GetLabelText("InOutId"));
            DetailGridExControl.MainGrid.AddColumn("InWhCode", LabelConvert.GetLabelText("InWhCode"));
            DetailGridExControl.MainGrid.AddColumn("InWhPosition", LabelConvert.GetLabelText("InWhPosition"));
            DetailGridExControl.MainGrid.AddColumn("InInspectionState", "수입검사", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("ProductItemCode", LabelConvert.GetLabelText("ProductItemCode"));
            DetailGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("UseMachine"));
            DetailGridExControl.MainGrid.AddColumn("SpendQty", LabelConvert.GetLabelText("UseQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasSelect, "_Check");

        }

        protected override void InitRepository()
        {
            var nowDate = DateTime.Today.Day >= 20 ? DateTime.Today.AddMonths(1) : DateTime.Today;
            if (searchDivision == nowDate.ToString("yyyy-MM"))//"당월")
            {
                MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
                MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            }
            else
            {
                MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
                MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            }
            
       
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InOutId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            searchItemCode = lup_Item.EditValue.GetNullToEmpty();
            searchDivision = cbo_YearMonth.EditValue.GetNullToEmpty();
            string topcatgory = lup_TopCategory.EditValue.GetNullToEmpty();
            MasterGridExControl.MainGrid.Columns.Clear();
            DetailGridExControl.MainGrid.Columns.Clear();
            InitGrid();
            InitRepository();

            var nowDate = DateTime.Today.Day >= 20 ? DateTime.Today.AddMonths(1) : DateTime.Today;
            if (searchDivision == nowDate.ToString("yyyy-MM"))//"당월")
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(searchItemCode) ? true : p.ItemCode == searchItemCode)
                                                                        &&(string.IsNullOrEmpty(topcatgory) ? true : p.TN_STD1100.TopCategory == topcatgory) )
                                                                         .OrderBy(p => p.ItemCode)
                                                                         .ToList();
            }
            else
            {
                searchYearMonthDate = new DateTime(searchDivision.Left(4).GetIntNullToZero(), searchDivision.Substring(5).GetIntNullToZero(), 1);
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var Date = new SqlParameter("@Date", searchYearMonthDate);
                    var ItemCode = new SqlParameter("@ItemCode", searchItemCode);
                    var TopCategory = new SqlParameter("@topcategory", topcatgory);
                    MasterGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PUR_STOCK_ITEM>("USP_GET_PUR_STOCK_MASTER_NEW @Date,@ItemCode,@topcategory", Date, ItemCode, TopCategory).OrderBy(p => p.ItemCode).ToList();
                }
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var nowDate = DateTime.Today.Day >= 20 ? DateTime.Today.AddMonths(1) : DateTime.Today;
            if (searchDivision == nowDate.ToString("yyyy-MM"))//"당월")
            {
                var masterObj = MasterGridBindingSource.Current as VI_PUR_STOCK_ITEM;
                if (masterObj == null)
                {
                    DetailGridExControl.MainGrid.Clear();
                    return;
                }

                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var Date = new SqlParameter("@Date", nowDate);// DateTime.Today);
                    var ItemCode = new SqlParameter("@ItemCode", masterObj.ItemCode);
                    DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PUR_STOCK_DETAIL>("USP_GET_PUR_STOCK_DETAIL_NEW @Date,@ItemCode", Date, ItemCode)
                        .OrderBy(p => p.InOutLotNo)
                        .ThenBy(p=>p.UpdateTime)
                        .ToList();
                }

                DetailRowChange();
            }
            else
            {
                var masterObj = MasterGridBindingSource.Current as TEMP_PUR_STOCK_ITEM;
                if (masterObj == null)
                {
                    DetailGridExControl.MainGrid.Clear();
                    return;
                }

                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var Date = new SqlParameter("@Date", searchYearMonthDate);
                    var ItemCode = new SqlParameter("@ItemCode", masterObj.ItemCode);
                    DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PUR_STOCK_DETAIL>("USP_GET_PUR_STOCK_DETAIL_NEW @Date,@ItemCode", Date, ItemCode)
                        .OrderBy(p => p.InOutLotNo)
                        .ThenBy(p => p.UpdateTime)
                        .ToList();
                }
            }
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        private void MainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DetailRowChange();
        }

        private void DetailRowChange()
        {
            var detailObj = DetailGridBindingSource.Current as TEMP_PUR_STOCK_DETAIL;
            if (detailObj == null)
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
            else
            {
                var nowDate = DateTime.Today.Day >= 20 ? DateTime.Today.AddMonths(1) : DateTime.Today;
                if (searchDivision == nowDate.ToString("yyyy-MM"))//"당월")
                {
                    if(detailObj.Division == "재입고")
                        DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                    else
                        DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
                }
                else
                {
                    DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
                }
            }
        }

        /// <summary> 구매현황 </summary>
        private void Btn_PurchaseStatus_Click(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PUR_STATUS, param, null);
            form.ShowPopup(false);
        }

        /// <summary> 재입고 수정 </summary>
        protected override void DetailFileChooseClicked()
        {
            var detailObj = DetailGridBindingSource.Current as TEMP_PUR_STOCK_DETAIL;
            if (detailObj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.KeyValue, detailObj.InOutLotNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFPUR1302, param, PopupRefreshCallback);
            form.ShowPopup(true);
        }

        /// <summary> 현품표출력버튼 </summary>
        private void BarButtonPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (DetailGridBindingSource == null) return;
                
                WaitHandler.ShowWait();

                var detailList = DetailGridBindingSource.List as List<TEMP_PUR_STOCK_DETAIL>;
                var printList = detailList.Where(p => p._Check == "Y").ToList();
                if (printList.Count == 0) return;

                var mainReport = new REPORT.XRPUR1201();
                foreach (var v in printList.OrderByDescending(p => p.RowIndex).ToList())
                {
                    var printQty = 1;
                    if (printQty <= 1)
                    {
                        var report = new REPORT.XRPUR1201(v);
                        report.CreateDocument();
                        mainReport.Pages.AddRange(report.Pages);
                    }
                    else
                    {
                        for (int i = 0; i < printQty; i++)
                        {
                            var report = new REPORT.XRPUR1201(v);
                            report.CreateDocument();
                            mainReport.Pages.AddRange(report.Pages);
                        }
                    }
                    v._Check = "N";
                }
                DetailGridExControl.BestFitColumns();
                mainReport.PrintingSystem.ShowMarginsWarning = false;
                mainReport.ShowPrintStatusDialog = false;
                mainReport.ShowPreview();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }
    }
}