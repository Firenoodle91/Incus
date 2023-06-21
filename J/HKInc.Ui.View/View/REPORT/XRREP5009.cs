using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Entity;

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using DevExpress.XtraBars;

using HKInc.Service.Service;
using HKInc.Service.Factory;

using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.View.PopupFactory;

using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Enum;


namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 월별납품관리
    /// </summary>
    public partial class XRREP5009 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {

        IService<TN_ORD1700> ModelService = (IService<TN_ORD1700>)ProductionFactory.GetDomainService("TN_ORD1700");
                             

        public XRREP5009()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;

            dt_YYYY.DateTime = DateTime.Today;
            dt_MM.DateTime = DateTime.Today;
        }        

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "거래처명");
            //MasterGridExControl.MainGrid.AddColumn("CustomerName", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품명");
            //MasterGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.CarType", LabelConvert.GetLabelText("CarType"));
            MasterGridExControl.MainGrid.AddColumn("YYYY", "YYYY", false);
            MasterGridExControl.MainGrid.AddColumn("MM", "MM", false);
            
            MasterGridExControl.MainGrid.AddColumn("PlanQty", "계획수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("Cost", "단가", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DelivQty", "납품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DueMQty", "미납수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DuePlanCost", "계획금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DueCost", "납품금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DueMCost", "미납금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DueRate", "달성율", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##}");

            string ColName = "";

            for(int i = 1 ; i < 32; i++)
            {
                if (i.ToString().Length == 1)
                    ColName = "0" + i.ToString();
                else
                    ColName = i.ToString();


                MasterGridExControl.MainGrid.AddColumn("D"+ColName, ColName, HorzAlignment.Far, FormatType.Numeric, "n0");
            }

            /*
            MasterGridExControl.MainGrid.AddColumn("D01", "01", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D02", "02", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D03", "03", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D04", "04", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D05", "05", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D06", "06", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D07", "07", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D08", "08", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D09", "09", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D10", "10", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D11", "11", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D12", "12", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D13", "13", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D14", "14", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D15", "15", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D16", "16", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D17", "17", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D18", "18", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D19", "19", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D20", "20", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D21", "21", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D22", "22", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D23", "23", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D24", "24", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D25", "25", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D26", "26", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D27", "27", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D28", "28", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D29", "29", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D30", "30", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("D31", "31", HorzAlignment.Far, FormatType.Numeric, "n0");
            */

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PlanQty", "Cost"
                                                     ,"D01", "D02", "D03", "D04", "D05", "D06", "D07", "D08", "D09", "D10"
                                                     ,"D11", "D12", "D13", "D14", "D15", "D16", "D17", "D18", "D19", "D20"
                                                     ,"D21", "D22", "D23", "D24", "D25", "D26", "D27", "D28", "D29", "D30", "D31");

            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1300>(MasterGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemCode", Service.Helper.DataConvert.GetCultureDataFieldName("ItemName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));

            MasterGridExControl.MainGrid.MainView.FixedLineWidth = 10;
            MasterGridExControl.MainGrid.MainView.Columns["CustomerCode"].Fixed = FixedStyle.Left;
            MasterGridExControl.MainGrid.MainView.Columns["ItemCode"].Fixed = FixedStyle.Left;
            MasterGridExControl.MainGrid.MainView.Columns["TN_STD1100.CarType"].Fixed = FixedStyle.Left;
            MasterGridExControl.MainGrid.MainView.Columns["PlanQty"].Fixed = FixedStyle.Left;
            MasterGridExControl.MainGrid.MainView.Columns["Cost"].Fixed = FixedStyle.Left;
            MasterGridExControl.MainGrid.MainView.Columns["DelivQty"].Fixed = FixedStyle.Left;
            MasterGridExControl.MainGrid.MainView.Columns["DueMQty"].Fixed = FixedStyle.Left;
            MasterGridExControl.MainGrid.MainView.Columns["DuePlanCost"].Fixed = FixedStyle.Left;
            MasterGridExControl.MainGrid.MainView.Columns["DueCost"].Fixed = FixedStyle.Left;
            MasterGridExControl.MainGrid.MainView.Columns["DueMCost"].Fixed = FixedStyle.Left;
            MasterGridExControl.MainGrid.MainView.Columns["DueRate"].Fixed = FixedStyle.Left;

            MasterGridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;
            MasterGridExControl.MainGrid.MainView.Columns["DelivQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["DelivQty"].SummaryItem.DisplayFormat = "{0:#,#0.#####}";
            MasterGridExControl.MainGrid.MainView.Columns["DueMQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["DueMQty"].SummaryItem.DisplayFormat = "{0:#,#0.#####}";
            MasterGridExControl.MainGrid.MainView.Columns["DuePlanCost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["DuePlanCost"].SummaryItem.DisplayFormat = "{0:#,#0.#####}";
            MasterGridExControl.MainGrid.MainView.Columns["DueCost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["DueCost"].SummaryItem.DisplayFormat = "{0:#,#0.#####}";
            MasterGridExControl.MainGrid.MainView.Columns["DueMCost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["DueMCost"].SummaryItem.DisplayFormat = "{0:#,#0.#####}";
            /*
            MasterGridExControl.MainGrid.MainView.OptionsView.ColumnAutoWidth = false;
            MasterGridExControl.MainGrid.MainView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

            MasterGridExControl.MainGrid.MainView.Columns["DelivQty"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("ItemCost");
            */

            MasterGridExControl.BestFitColumns();
        }

        protected override void InitCombo()
        {
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_ItemCode.SetDefault(true, "ItemCode", "ItemName", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            //InitCombo();
            //InitRepository();

            //string YYYY = DateTime.Now.ToString("yyyy");
            //string MM = DateTime.Now.ToString("MM");


            string YYYY = dt_YYYY.DateTime.ToString("yyyy");
            string MM = dt_MM.DateTime.ToString("MM");
            string CustomerCode = lup_CustomerCode.EditValue.GetNullToEmpty();
            string ItemCode = lup_ItemCode.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => p.YYYY == YYYY
                                                                        && p.MM == MM
                                                                        && (string.IsNullOrEmpty(CustomerCode) ? true : p.CustomerCode == CustomerCode)
                                                                        && (string.IsNullOrEmpty(ItemCode) ? true : p.ItemCode == ItemCode)
                                                                     )
                                                                     .OrderBy(o => o.CustomerCode).ThenBy(o => o.ItemCode)
                                                                     .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void AddRowClicked()
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Constraint, MasterCodeSTR.Contraint_ItemWAN);
            param.SetValue(PopupParameter.Value_1, null);
            param.SetValue(PopupParameter.Value_5, "Y");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_STD1100, param, AddMasterCallback);
            form.ShowPopup(true);
        }

        private void AddMasterCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            string YYYY = DateTime.Now.ToString("yyyy");
            string MM = DateTime.Now.ToString("MM");

            var returnList = (List<TN_STD1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var v in returnList)
            {

                var newObj = new TN_ORD1700();
                newObj.ItemCode = v.ItemCode;
                newObj.CustomerCode = v.MainCustomerCode;
                newObj.NewRowFlag = "Y";
                newObj.YYYY = YYYY;
                newObj.MM = MM;

                MasterGridBindingSource.Add(newObj);
            }

            InitRepository();

            if (returnList.Count > 0) SetIsFormControlChanged(true);

            MasterGridExControl.MainGrid.BestFitColumns();
        }

        private void  DetailMainView_RowStyle(object sender, RowStyleEventArgs e)
        {
            /*
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                var ttt = View.GetRowCellValue(e.RowHandle, View.Columns["ItemCost"]);

                int ItemCost = View.GetRowCellValue(e.RowHandle, View.Columns["ItemCost"]).GetIntNullToZero();
                int OutQty = View.GetRowCellValue(e.RowHandle, View.Columns["OutQty"]).GetIntNullToZero();

                int DueCost = ItemCost * OutQty;

                View.SetRowCellValue(e.RowHandle, View.Columns["DueCost"], "111");
            }
            */
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();

            List<TN_ORD1700> List = MasterGridBindingSource.DataSource as List<TN_ORD1700>;

            foreach (var v in List)
            {
                //추가
                if (v.NewRowFlag == "Y")
                {
                    ModelService.Insert(v);
                }
                //수정
                else if (v.NewRowFlag == "N" && v.EditRowFlag == "Y")
                {
                    ModelService.Update(v);
                }
            }

            ModelService.Save();

            DataLoad();
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1700;

            if (masterObj == null) return;

            MasterGridBindingSource.RemoveCurrent();
            ModelService.Delete(masterObj);

        }

    }
}
