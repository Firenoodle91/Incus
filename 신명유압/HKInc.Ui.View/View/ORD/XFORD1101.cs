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
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 일일출고관리
    /// </summary>
    public partial class XFORD1101 : Service.Base.ListFormTemplate
    {
        IService<TN_ORD1101> ModelService = (IService<TN_ORD1101>)ProductionFactory.GetDomainService("TN_ORD1101");
        private List<VI_PROD_STOCK_ITEM> StockList = new List<VI_PROD_STOCK_ITEM>();
        public XFORD1101()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
        }

        protected override void InitCombo()
        {
            dt_OutDatePlan.DateTime = DateTime.Today;
            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.CheckBoxMultiSelect(true, "DelivNo", true);
            IsGridButtonFileChooseEnabled = true;
            GridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("ShipmentCardPrint") + "[F10]", IconImageList.GetIconImage("print/printer"));
            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Print"));
            GridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            GridExControl.MainGrid.AddColumn("OrderSeq", LabelConvert.GetLabelText("OrderSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("DelivNo", LabelConvert.GetLabelText("DelivNo"));
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CarType", LabelConvert.GetLabelText("CarType"));
            GridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            GridExControl.MainGrid.AddColumn("TN_ORD1100.DelivQty", LabelConvert.GetLabelText("DelivQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            GridExControl.MainGrid.AddColumn("TN_ORD1100.DelivDate", LabelConvert.GetLabelText("DelivDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("OutDate", LabelConvert.GetLabelText("OutDatePlan"));
            GridExControl.MainGrid.AddColumn("OutId", LabelConvert.GetLabelText("OutId"), false);
            //GridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OuQtyPlan"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"), false);
            GridExControl.MainGrid.SetEditable("_Check");
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutDate", "OutId",  "Memo");

            var barText1 = new DevExpress.XtraBars.BarEditItem(GridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barText1.Id = 4;
            barText1.Name = "barText1";
            barText1.Edit.NullText = LabelConvert.GetLabelText("OutDatePlan") + ":";
            barText1.EditWidth = 120;
            barText1.Enabled = false;
            barText1.Edit.AppearanceDisabled.ForeColor = Color.Black;
            barText1.Edit.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Far;
            barText1.Edit.AppearanceDisabled.BackColor = Color.Transparent;
            barText1.Edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barText1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barText1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;

            var barDateEdit1 = new DevExpress.XtraBars.BarEditItem(GridExControl.BarTools.Manager, new RepositoryItemDateEdit());
            barDateEdit1.Id = 5;
            barDateEdit1.Enabled = UserRight.HasEdit;
            barDateEdit1.Name = "barDateEdit1";
            
            barDateEdit1.EditWidth = 100;
            barDateEdit1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            //DetailGridExControl.BarTools.AddItem(barTextEditBarCode);
            
            var barOutDatePlanInsert = new DevExpress.XtraBars.BarButtonItem();
            barOutDatePlanInsert.Id = 6;
            barOutDatePlanInsert.ImageOptions.Image = IconImageList.GetIconImage("actions/apply");
            barOutDatePlanInsert.Name = "barOutDatePlanInsert";
            barOutDatePlanInsert.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barOutDatePlanInsert.Caption = LabelConvert.GetLabelText("BatchApply");
            barOutDatePlanInsert.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            barOutDatePlanInsert.ItemClick += BarOutDatePlanInsert_ItemClick; ; 

            GridExControl.BarTools.ItemLinks.Insert(4, barText1);
            GridExControl.BarTools.ItemLinks.Insert(5, barDateEdit1);
            GridExControl.BarTools.ItemLinks.Insert(6, barOutDatePlanInsert);

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1101>(GridExControl);
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);
            GridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.CarType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.CarType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSpinEdit("OutQty", DefaultBoolean.Default, MasterCodeSTR.Numeric_N2);
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("DelivNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);

            GridExControl.MainGrid.Clear();
            StockList.Clear();

            ModelService.ReLoad();

            var outDate = dt_OutDatePlan.DateTime;
            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var customerCode = lup_Customer.EditValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => p.OutDate == outDate
                                                                    && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                    && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                               )
                                                               .ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            //재고량 가져오기
            StockList.AddRange(ModelService.GetChildList<VI_PROD_STOCK_ITEM>(p => true).ToList());
        }

        protected override void AddRowClicked()
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Constraint, "XFORD1101");
            param.SetValue(PopupParameter.Value_1, GridBindingSource.List);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_ORD1100, param, AddRowCallBack);
            form.ShowPopup(true);
        }

        private void AddRowCallBack(object sender, PopupArgument e)
        {
            if (e == null) return;

            List<TN_ORD1100> returnList = (List<TN_ORD1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            var outDate = DateTime.Today;

            foreach (var v in returnList)
            {
                var newObj = new TN_ORD1101();
                newObj.OutDate = outDate;
                newObj.OrderNo = v.OrderNo;
                newObj.OrderSeq = v.OrderSeq;
                newObj.DelivNo = v.DelivNo;
                newObj.ItemCode = v.ItemCode;
                newObj.CustomerCode = v.CustomerCode;
                //newObj.OutQty = v.DelivQty;
                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).First();
                newObj.TN_ORD1100 = ModelService.GetChildList<TN_ORD1100>(p => p.OrderNo == v.OrderNo && p.OrderSeq == v.OrderSeq && p.DelivNo == v.DelivNo).First();
                GridBindingSource.Add(newObj);
                GridBindingSource.MoveLast();
                ModelService.Insert(newObj);
                PopupDataParam.SetValue(PopupParameter.GridRowId_1, newObj.DelivNo);
            }
            GridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }

        protected override void DeleteRow()
        {
            var obj = GridBindingSource.Current as TN_ORD1101;
            if (obj == null) return;

            GridBindingSource.RemoveCurrent();
            ModelService.Delete(obj);
            GridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }

        protected override void FileChooseClicked()
        {
            if (GridBindingSource == null) return;

            var list = GridBindingSource.List as List<TN_ORD1101>;
            if (list == null || list.Count == 0) return;

            var checkList = list.Where(p => p._Check == "Y").ToList();
            if (checkList.Count == 0) return;

            var listCount = checkList.Count;

            int printRowCnt = 10;

            var valueCount = listCount / printRowCnt;
            var modCount = listCount % printRowCnt;

            if (modCount == 0)
            {
                ReportCreateToPrint(checkList);
            }
            else
            {
                var checkCount = (valueCount == 0 ? 1 : valueCount + 1) * printRowCnt;
                while (true)
                {
                    checkList.Add(new TN_ORD1101()
                    {
                        Temp2 = "-1"
                    });
                    if (checkList.Count == checkCount) break;
                }
                ReportCreateToPrint(checkList);
            }
        }

        private void ReportCreateToPrint(List<TN_ORD1101> checkList)
        {
            try
            {
                WaitHandler.ShowWait();
                var report = new REPORT.XRORD1101(checkList);
                report.CreateDocument();

                report.PrintingSystem.ShowMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                report.ShowPreview();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally
            {
                foreach (var v in checkList.Where(p => p.Temp2 == "-1").ToList())
                    checkList.Remove(v);
                WaitHandler.CloseWait();
            }
        }

        /// <summary> 출하예정일 일괄입력 </summary>
        private void BarOutDatePlanInsert_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (GridBindingSource == null) return;

            var list = GridBindingSource.List as List<TN_ORD1101>;
            if (list == null || list.Count == 0) return;

            var dateEdit = GridExControl.BarTools.ItemLinks[5].Item as BarEditItem;
            if (dateEdit == null) return;

            var value = dateEdit.EditValue;
            if (value == null) return;

            var msgValue = MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_75), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo);
            if (msgValue != DialogResult.Yes) return;

            foreach (var v in list)
            {
                v.OutDate = (DateTime)value;
            }

            GridExControl.BestFitColumns();
            dateEdit.EditValue = null;
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();

            ModelService.Save();

            DataLoad();
        }

        protected override void GridRowDoubleClicked(){}

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "StockQty")
            {
                var itemCode = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ItemCode").GetNullToEmpty();
                if (!itemCode.IsNullOrEmpty())
                {
                    var stockObj = StockList.Where(p => p.ItemCode == itemCode).FirstOrDefault();
                    e.DisplayText = stockObj == null ? "0" : stockObj.SumStockQty.ToString("#,#.##");
                }
            }
        }

    }
}