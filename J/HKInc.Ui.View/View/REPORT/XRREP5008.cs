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
    /// 스크랩 발생내역
    /// </summary>
    public partial class XRREP5008 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        //IService<TN_STD1105> ModelService = (IService<TN_STD1105>)ProductionFactory.GetDomainService("TN_STD1105");

        IService<TN_STD1000> CommonModel = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");
        IService<TN_STD1100> ItemModel = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        IService<TN_STD1400> CustModel = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");

        public XRREP5008()
        {   
            InitializeComponent();

            MasterGridExControl = gridEx1;

            //dt_OrderDate.SetTodayIsMonth();

            ////월초~월말(개월 수)
            //dt_OrderDate.SetMonth(1);

            dt_YYYY.DateTime = DateTime.Today;
            dt_MM.DateTime = DateTime.Today;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(false, true, "ItemCode", "ItemName", ItemModel.GetList(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_MAT)).ToList());
            lup_Customer.SetDefault(true, "CustomerCode", "CustomerName", CustModel.GetList(p => p.UseFlag == "Y").ToList());

            //lup_SrcTexture.SetDefault(true, "CodeTop", "CodeName", CommonModel.GetList(p => p.CodeTop == MasterCodeSTR.SrcTexture && p.UseYN == "Y").ToList());
            lup_SrcTexture.SetDefault(true, "CodeVal", "CodeName", DbRequestHandler.GetCommTopCode(MasterCodeSTR.SrcTexture));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);

            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "거래처코드", false);
            MasterGridExControl.MainGrid.AddColumn("CustomerName", "거래처명", false);
            //MasterGridExControl.MainGrid.AddColumn("OutDate", "납품일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd", false);
            MasterGridExControl.MainGrid.AddColumn("OutQty", "납품\n수량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}", false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("CarType", "차종", false);
            MasterGridExControl.MainGrid.AddColumn("ItemName", "품명", false);
            MasterGridExControl.MainGrid.AddColumn("SrcCode", "원자재코드", false);
            MasterGridExControl.MainGrid.AddColumn("SrcName", "원자재명");
            MasterGridExControl.MainGrid.AddColumn("Texture", "재종");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "자재규격(1)");
            MasterGridExControl.MainGrid.AddColumn("Spec2", "자재규격(2)");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "자재규격(3)");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "자재규격(4)");
            MasterGridExControl.MainGrid.AddColumn("Unit", "단위");

            MasterGridExControl.MainGrid.AddColumn("SumSrcWeight", "원자재\n소요량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("SumWeight", "완제품\n소요량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            MasterGridExControl.MainGrid.AddColumn("SumScrap", "스크랩중량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");

            MasterGridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;
            MasterGridExControl.MainGrid.MainView.Columns["SumSrcWeight"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["SumSrcWeight"].SummaryItem.DisplayFormat = "{0:#,##0.#####}";
            MasterGridExControl.MainGrid.MainView.Columns["SumWeight"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["SumWeight"].SummaryItem.DisplayFormat = "{0:#,##0.#####}";
            MasterGridExControl.MainGrid.MainView.Columns["SumScrap"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["SumScrap"].SummaryItem.DisplayFormat = "{0:#,##0.#####}";
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

            //var itemCode = lup_Item.EditValue.GetNullToEmpty();
            //var customerCode = lup_Customer.EditValue.GetNullToEmpty();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;

                var yyyy = new SqlParameter("@YYYY", dt_YYYY.DateTime.ToString("yyyy"));
                var mm = new SqlParameter("@MM", dt_MM.DateTime.ToString("MM"));
                var customercode = new SqlParameter("@CustomerCode", lup_Customer.EditValue.GetNullToEmpty());
                var itemcode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
                var srctexture = new SqlParameter("@SrcTexture", lup_SrcTexture.EditValue.GetNullToEmpty());

                var result = context.Database.SqlQuery<TEMP_XRREP5008>
                    ("USP_GET_XRREP5008 @YYYY, @MM, @CustomerCode, @ItemCode, @SrcTexture", yyyy, mm, customercode, itemcode, srctexture).ToList();

                MasterGridBindingSource.DataSource = result.ToList();
                MasterGridExControl.DataSource = MasterGridBindingSource;
                MasterGridExControl.BestFitColumns();

            }

            SetRefreshMessage(MasterGridExControl);
            GridRowLocator.SetCurrentRow();
        }


    }


}