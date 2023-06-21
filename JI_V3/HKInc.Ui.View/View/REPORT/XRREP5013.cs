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
    /// 월별매입관리
    /// </summary>
    public partial class XRREP5013 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {

        IService<TN_PUR1700> ModelService = (IService<TN_PUR1700>)ProductionFactory.GetDomainService("TN_PUR1700");
                             

        public XRREP5013()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            dt_YYYY.DateTime = DateTime.Today;
            dt_MM.DateTime = DateTime.Today;

            lup_CustomerCode.EditValueChanged += lup_CustomerCode_EditValueChanged;
            lup_ItemCode.EditValueChanged += lup_ItemCode_EditValueChanged;
        }        

        public void lup_CustomerCode_EditValueChanged(object sender, EventArgs e)
        {
            DataLoad();
        }
        public void lup_ItemCode_EditValueChanged(object sender, EventArgs e)
        {
            MasterFocusedRowChanged();
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            //MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
            //MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "거래처명");

            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품명");
            DetailGridExControl.MainGrid.AddColumn("YYYY", "YYYY", false);
            DetailGridExControl.MainGrid.AddColumn("MM", "MM", false);
            DetailGridExControl.MainGrid.AddColumn("PlanQty", "계획량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Cost", "단가", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.###}");
            DetailGridExControl.MainGrid.AddColumn("InQty", "입고량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("DueMQty", "미입고량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("DuePlanCost", "계획금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("DueCost", "입고금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("DueMCost", "미납금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("DueRate", "달성율", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##}");

            string ColName = "";

            for(int i = 1 ; i < 32; i++)
            {
                if (i.ToString().Length == 1)
                    ColName = "0" + i.ToString();
                else
                    ColName = i.ToString();


                DetailGridExControl.MainGrid.AddColumn("D"+ColName, ColName, HorzAlignment.Far, FormatType.Numeric, "n0");
            }


            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PlanQty", "Cost"
                                                     ,"D01", "D02", "D03", "D04", "D05", "D06", "D07", "D08", "D09", "D10"
                                                     ,"D11", "D12", "D13", "D14", "D15", "D16", "D17", "D18", "D19", "D20"
                                                     ,"D21", "D22", "D23", "D24", "D25", "D26", "D27", "D28", "D29", "D30", "D31");

            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1300>(MasterGridExControl);

            //픽스 취소
            /*
            DetailGridExControl.MainGrid.MainView.FixedLineWidth = 10;
            DetailGridExControl.MainGrid.MainView.Columns["ItemCode"].Fixed = FixedStyle.Left;
            DetailGridExControl.MainGrid.MainView.Columns["TN_STD1100.CarType"].Fixed = FixedStyle.Left;
            DetailGridExControl.MainGrid.MainView.Columns["PlanQty"].Fixed = FixedStyle.Left;
            DetailGridExControl.MainGrid.MainView.Columns["Cost"].Fixed = FixedStyle.Left;
            DetailGridExControl.MainGrid.MainView.Columns["DelivQty"].Fixed = FixedStyle.Left;
            DetailGridExControl.MainGrid.MainView.Columns["DueMQty"].Fixed = FixedStyle.Left;
            DetailGridExControl.MainGrid.MainView.Columns["DuePlanCost"].Fixed = FixedStyle.Left;
            DetailGridExControl.MainGrid.MainView.Columns["DueCost"].Fixed = FixedStyle.Left;
            DetailGridExControl.MainGrid.MainView.Columns["DueMCost"].Fixed = FixedStyle.Left;
            DetailGridExControl.MainGrid.MainView.Columns["DueRate"].Fixed = FixedStyle.Left;
            */

            DetailGridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;
            DetailGridExControl.MainGrid.MainView.Columns["InQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            DetailGridExControl.MainGrid.MainView.Columns["InQty"].SummaryItem.DisplayFormat = "{0:#,#0.#####}";
            DetailGridExControl.MainGrid.MainView.Columns["DueMQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            DetailGridExControl.MainGrid.MainView.Columns["DueMQty"].SummaryItem.DisplayFormat = "{0:#,#0.#####}";
            DetailGridExControl.MainGrid.MainView.Columns["DuePlanCost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            DetailGridExControl.MainGrid.MainView.Columns["DuePlanCost"].SummaryItem.DisplayFormat = "{0:#,#0.#####}";
            DetailGridExControl.MainGrid.MainView.Columns["DueCost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            DetailGridExControl.MainGrid.MainView.Columns["DueCost"].SummaryItem.DisplayFormat = "{0:#,#0.#####}";
            DetailGridExControl.MainGrid.MainView.Columns["DueMCost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            DetailGridExControl.MainGrid.MainView.Columns["DueMCost"].SummaryItem.DisplayFormat = "{0:#,#0.#####}";
            /*
            MasterGridExControl.MainGrid.MainView.OptionsView.ColumnAutoWidth = false;
            MasterGridExControl.MainGrid.MainView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

            MasterGridExControl.MainGrid.MainView.Columns["DelivQty"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("ItemCost");
            */

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit
                ("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit
                ("ItemCode", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemCode", Service.Helper.DataConvert.GetCultureDataFieldName("ItemName"));
            

        }

        protected override void InitCombo()
        {
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && p.CustomerType == "A01").ToList());
            lup_ItemCode.SetDefault(false, true, "ItemCode", "ItemName", ModelService.GetChildList<TN_STD1100>
                (p => p.UseFlag == "Y" 
             //&& p.TopCategory == MasterCodeSTR.TopCategory_MAT
             && (p.TopCategory != MasterCodeSTR.TopCategory_WAN && p.TopCategory != MasterCodeSTR.TopCategory_BAN)
             ).ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("CustomerCode");

            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string CustomerCode = lup_CustomerCode.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y"
                                                                                   && (string.IsNullOrEmpty(CustomerCode) ? true : p.CustomerCode == CustomerCode)
                                                                                   && p.CustomerType == "A01"
                                                                                   )
                                                                     .OrderBy(o => o.CustomerCode)
                                                                     .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            //GridRowLocator.GetCurrentRow("ItemCode");
            DataSave1();
            DetailGridExControl.MainGrid.Clear();

            //ModelService.ReLoad();

            //InitCombo();
            //InitRepository();

            //string YYYY = DateTime.Now.ToString("yyyy");
            //string MM = DateTime.Now.ToString("MM");

            var masterObj = MasterGridBindingSource.Current as TN_STD1400;

            if (masterObj == null) return;


            string YYYY = dt_YYYY.DateTime.ToString("yyyy");
            string MM = dt_MM.DateTime.ToString("MM");
            string CustomerCode = masterObj.CustomerCode.GetNullToEmpty();
            string ItemCode = lup_ItemCode.EditValue.GetNullToEmpty();

            DetailGridBindingSource.DataSource = ModelService.GetList(p => p.YYYY == YYYY
                                                                        && p.MM == MM
                                                                        && p.CustomerCode == CustomerCode
                                                                        && (string.IsNullOrEmpty(ItemCode) ? true : p.ItemCode == ItemCode)
                                                                     )
                                                                     .OrderBy(o => o.CustomerCode).ThenBy(o => o.ItemCode)
                                                                     .ToList();

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();

            //GridRowLocator.SetCurrentRow();
            //SetRefreshMessage(DetailGridExControl);
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1400;
            if (masterObj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Constraint, MasterCodeSTR.Contraint_ItemMAT_BU);
            param.SetValue(PopupParameter.Value_1, masterObj.CustomerCode);
            param.SetValue(PopupParameter.Value_5, "Y");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_STD1100, param, AddDetailCallback);
            form.ShowPopup(true);
        }

        private void AddDetailCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            string YYYY = dt_YYYY.DateTime.ToString("yyyy");
            string MM = dt_MM.DateTime.ToString("MM");
            var masterObj = MasterGridBindingSource.Current as TN_STD1400;

            var returnList = (List<TN_STD1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var v in returnList)
            {
                //차종 바로 보이게 하기 위함
                var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();

                var newObj = new TN_PUR1700();
                newObj.ItemCode = v.ItemCode;
                newObj.TN_STD1100 = item; //차종 바로 보이게 하기 위함
                newObj.CustomerCode = masterObj.CustomerCode;
                newObj.Cost = DbRequestHandler.GetCustItemCost(masterObj.CustomerCode, v.ItemCode, YYYY+"-"+MM, "M");
                newObj.NewRowFlag = "Y";
                newObj.YYYY = YYYY;
                newObj.MM = MM;
                
                DetailGridBindingSource.Add(newObj);
                ModelService.Insert(newObj);        // 2022-10-19 김진우 추가
            }

            //InitRepository();

            if (returnList.Count > 0) SetIsFormControlChanged(true);

            DetailGridExControl.MainGrid.BestFitColumns();
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
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();


                ModelService.Save();
            
            DataLoad();
        }
        private void DataSave1()
        {
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

        
                ModelService.Save();
           
        }

        protected override void DeleteDetailRow()
        {
            var DetailObj = DetailGridBindingSource.Current as TN_PUR1700;

            if (DetailObj == null) return;

            DetailGridBindingSource.RemoveCurrent();
           ModelService.Delete(DetailObj);
        }

    }
}
