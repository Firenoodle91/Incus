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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.BandedGrid;

using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Enum;


namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 월별납품원가현황
    /// </summary>
    public partial class XRREP5010 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_STD1105> ModelService = (IService<TN_STD1105>)ProductionFactory.GetDomainService("TN_STD1105");

        public XRREP5010()
        {   
            InitializeComponent();

            MasterGridExControl = gridEx1;

            MasterGridExControl.MainGrid.MainView = new BandedGridView();
            MasterGridExControl.MainGrid.ViewType = GridViewType.BandedGridView;

            dt_YYYY.DateTime = DateTime.Today;
            dt_MM.DateTime = DateTime.Today;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(false, true, "ItemCode", "ItemName", ModelService.GetChildList<TN_STD1100>
                (p => p.UseFlag == "Y"
                && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)
                ).ToList());
            lup_Customer.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && p.CustomerType == "A00").ToList());
            lup_SrcTexture.SetDefault(true, "CodeVal", "CodeName", DbRequestHandler.GetCommTopCode(MasterCodeSTR.SrcTexture));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);

            MasterGridExControl.MainGrid.AddColumn("OrderNo", "수주번호", false);
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "거래처코드");
            MasterGridExControl.MainGrid.AddColumn("CustomerName", "거래처명");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("CarType", "차종");
            MasterGridExControl.MainGrid.AddColumn("ItemName", "품명");
            MasterGridExControl.MainGrid.AddColumn("OutDate", "납품일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd", false);
            MasterGridExControl.MainGrid.AddColumn("OutQty", "납품\n수량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("ItemCost", "납품\n금액", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("SrcCode", "원자재코드", false);
            MasterGridExControl.MainGrid.AddColumn("SrcName", "원자재명");
            MasterGridExControl.MainGrid.AddColumn("Texture", "재종");

            MasterGridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            MasterGridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            MasterGridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            MasterGridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));

            MasterGridExControl.MainGrid.AddColumn("SrcWeight", "원자재\n단위중량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("SumSrcWeight", "원자재\n소요량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("Weight", "제품중량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("SumScrap", "스크랩", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}"); 
            MasterGridExControl.MainGrid.AddColumn("CalSrcCost", "원자재\n단가", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("SrcCost", "원자재\n기준금액", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("SumSrcCost", "원자재\n금액", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");

            MasterGridExControl.MainGrid.AddColumn("BarfeederCNCcycleTime", "1개당\n소요시간", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("BfCNCManHour", "공수", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("BfCNCProcessCost", "공정비", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("BfCNCValueAdded", "부가가치", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");

            MasterGridExControl.MainGrid.AddColumn("CNC1cycleTime", "1차 1개당\n소요시간", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("CNC2cycleTime", "2차 1개당\n소요시간", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("CNC3cycleTime", "3차 1개당\n소요시간", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("CNCManHour", "공수", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("CNCProcessCost", "공정비", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("CNCValueAdded", "부가가치", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");

            MasterGridExControl.MainGrid.AddColumn("MCTcycleTime", "1개당\n소요시간", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("MCTManHour", "공수", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("MCTProcessCost", "공정비", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("MCTValueAdded", "부가가치", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");

            MasterGridExControl.MainGrid.AddColumn("TappingcycleTime", "개당\n소요시간", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("TappingManHour", "공수", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("TappingProcessCost", "공정비", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("TappingValueAdded", "부가가치", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");

            BandedGridView view = MasterGridExControl.MainGrid.MainView as BandedGridView;

            BandedGridEx.SetGridBandAddedEx(view, " ", " ", new string[] 
            {"CustomerCode","CustomerName","CarType","ItemName" ,"OutDate"
            ,"OutQty","ItemCost","SrcName","Texture","Spec1","Spec2","Spec3","Spec4"
            ,"SrcWeight","SumSrcWeight","Weight","SumScrap","CalSrcCost","SrcCost","SumSrcCost" });

            BandedGridEx.SetGridBandAddedEx(view, "BarfeederCNC", "BarfeederCNC", new string[] { "BarfeederCNCcycleTime", "BfCNCManHour", "BfCNCProcessCost", "BfCNCValueAdded" });
            BandedGridEx.SetGridBandAddedEx(view, "CNC", "CNC", new string[] { "CNC1cycleTime", "CNC2cycleTime", "CNC3cycleTime", "CNCManHour", "CNCProcessCost", "CNCValueAdded" });
            BandedGridEx.SetGridBandAddedEx(view, "MCT", "MCT", new string[] { "MCTcycleTime", "MCTManHour", "MCTProcessCost", "MCTValueAdded" });
            BandedGridEx.SetGridBandAddedEx(view, "Tapping", "Tapping", new string[] { "TappingcycleTime", "TappingManHour", "TappingProcessCost", "TappingValueAdded" });            
            
            MasterGridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;

            //그리드 합계 FOOTER 설정
            MasterGridExControl.MainGrid.MainView.Columns["SumSrcWeight"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["SumSrcWeight"].SummaryItem.DisplayFormat = "{0:#,##0.#####}";
            MasterGridExControl.MainGrid.MainView.Columns["BfCNCValueAdded"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["BfCNCValueAdded"].SummaryItem.DisplayFormat = "{0:#,##0.#####}";
            MasterGridExControl.MainGrid.MainView.Columns["CNCValueAdded"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["CNCValueAdded"].SummaryItem.DisplayFormat = "{0:#,##0.#####}";
            MasterGridExControl.MainGrid.MainView.Columns["MCTValueAdded"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["MCTValueAdded"].SummaryItem.DisplayFormat = "{0:#,##0.#####}";
            MasterGridExControl.MainGrid.MainView.Columns["TappingValueAdded"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["TappingValueAdded"].SummaryItem.DisplayFormat = "{0:#,##0.#####}";
        }

        protected override void InitRepository()
        {
            //SubDetailGridExControl.MainGrid.MainView.ColumnPanelRowHeight = 100;
            MasterGridExControl.MainGrid.MainView.OptionsView.ColumnAutoWidth = false;
            MasterGridExControl.MainGrid.MainView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();

            InitCombo();  
            InitRepository(); 

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                //var MonthDate = new SqlParameter("@MonthDate", dt_YearMonth.DateTime);
                //var MachineCode = new SqlParameter("@MachineCode", masterObj.MachineCode);

                var yyyy = new SqlParameter("@YYYY", dt_YYYY.DateTime.ToString("yyyy"));
                var mm = new SqlParameter("@MM", dt_MM.DateTime.ToString("MM"));

                var customercode = new SqlParameter("@CustomerCode", lup_Customer.EditValue.GetNullToEmpty());
                var itemcode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
                var srctexture = new SqlParameter("@SrcTexture", lup_SrcTexture.EditValue.GetNullToEmpty());

                var result = context.Database.SqlQuery<TEMP_XRREP5010>
                    ("USP_GET_XRREP5010 @YYYY, @MM, @CustomerCode, @ItemCode, @SrcTexture", yyyy, mm, customercode, itemcode, srctexture).ToList();

                MasterGridBindingSource.DataSource = result.ToList();
                MasterGridExControl.DataSource = MasterGridBindingSource;
                MasterGridExControl.BestFitColumns();
            }

            SetRefreshMessage(MasterGridExControl);
            GridRowLocator.SetCurrentRow();
        }


    }


}