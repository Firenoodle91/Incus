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
    /// 매출현황(월별)
    /// </summary>
    public partial class XRREP5015 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XRREP5015()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            dt_YYYY.DateTime = DateTime.Today;

        }

        protected override void InitCombo()
        {
            lup_ItemCode.SetDefault(false, true, "ItemCode", "ItemName", ModelService.GetChildList<TN_STD1100>
                (p => p.UseFlag == "Y"
                && (p.TopCategory != MasterCodeSTR.TopCategory_WAN && p.TopCategory != MasterCodeSTR.TopCategory_BAN)
                ).ToList());
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && p.CustomerType == "A01").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;

            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCode"), false);
            GridExControl.MainGrid.AddColumn("CustomerName", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), false);
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            //GridExControl.MainGrid.AddColumn("CarType", LabelConvert.GetLabelText("CarType"));

            string ColName = "";

            for (int i = 1; i < 13; i++)
            {
                if (i.ToString().Length == 1)
                    ColName = "0" + i.ToString();
                else
                    ColName = i.ToString();


                GridExControl.MainGrid.AddColumn("M" + ColName, ColName, HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.###}");
                GridExControl.MainGrid.MainView.Columns["M" + ColName].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                GridExControl.MainGrid.MainView.Columns["M" + ColName].SummaryItem.DisplayFormat = "{0:#,##0}";
            }

            GridExControl.MainGrid.AddColumn("TOTAL", "계", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.###}");

            GridExControl.MainGrid.MainView.Columns["TOTAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["TOTAL"].SummaryItem.DisplayFormat = "{0:#,##0.###}";
        }

        protected override void InitRepository()
        {


        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var yyyy = new SqlParameter("@YYYY", dt_YYYY.DateTime.ToString("yyyy"));
                var ItemCode = new SqlParameter("@ItemCode", lup_ItemCode.EditValue.GetNullToEmpty());
                var CustomerCode = new SqlParameter("@CustomerCode", lup_CustomerCode.EditValue.GetNullToEmpty());

                var result = context.Database
                      .SqlQuery<DATA>("USP_GET_XRREP5015 @YYYY, @CustomerCode, @ItemCode", yyyy, CustomerCode, ItemCode).ToList();

                GridBindingSource.DataSource = result;
            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            SetRefreshMessage(GridExControl);
        }
        public class DATA
        {
            public string CustomerName { get; set; }
            public string ItemName { get; set; }
            //public string CarType { get; set; }

            public decimal M01 { get; set; }
            public decimal M02 { get; set; }
            public decimal M03 { get; set; }
            public decimal M04 { get; set; }
            public decimal M05 { get; set; }
            public decimal M06 { get; set; }
            public decimal M07 { get; set; }
            public decimal M08 { get; set; }
            public decimal M09 { get; set; }
            public decimal M10 { get; set; }
            public decimal M11 { get; set; }
            public decimal M12 { get; set; }
            public decimal TOTAL { get; set; }


        }
    }
}