using System;
using System.Collections.Generic;
using System.Linq;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using DevExpress.Utils;
using HKInc.Utils.Class;
using System.Data.SqlClient;

namespace HKInc.Ui.View.REPORT
{
    /// <summary>
    /// 20220413 오세완 차장 
    /// 매출관리
    /// </summary>
    public partial class XFR5000 : HKInc.Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_STD1400> ModelService = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");
        #endregion

        public XFR5000()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            dpe_Date.DateFrEdit.EditValue = DateTime.Now.AddMonths(-1);
            dpe_Date.DateToEdit.EditValue = DateTime.Now;
        }

        protected override void InitCombo()
        {
            List<TN_STD1100> item_Arr = ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.Topcategory_Final_Product &&
                                                                                   p.UseYn == "Y");
            slup_Itemcode.SetDefault(true, "ItemCode", "ItemNm", item_Arr);

            List<TN_STD1400> cust_Arr = ModelService.GetList(p => p.UseFlag == "Y");
            slup_Custcode.SetDefault(true, "CustomerCode", "CustomerName", cust_Arr);
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("OUT_DATE", "출고일", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("CUSTOMER_NAME", "거래처명");
            GridExControl.MainGrid.AddColumn("ITEM_CODE","품목코드");          
            GridExControl.MainGrid.AddColumn("ITEM_NM", "품번");
            GridExControl.MainGrid.AddColumn("ITEM_NM1", "품명");

            GridExControl.MainGrid.AddColumn("COST", "단가", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            GridExControl.MainGrid.AddColumn("OUT_QTY", "수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            GridExControl.MainGrid.AddColumn("SALES", "매출액", HorzAlignment.Far, FormatType.Numeric, "#,###.##");

            GridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;
            GridExControl.MainGrid.MainView.Columns["OUT_QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["OUT_QTY"].SummaryItem.FieldName = "OUT_QTY";
            GridExControl.MainGrid.MainView.Columns["OUT_QTY"].SummaryItem.DisplayFormat = "{0:#,###.##}";
            GridExControl.MainGrid.MainView.Columns["SALES"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["SALES"].SummaryItem.FieldName = "SALES";
            GridExControl.MainGrid.MainView.Columns["SALES"].SummaryItem.DisplayFormat = "{0:#,###.##}";

        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            InitCombo();

            string sDatefrom = Convert.ToDateTime(dpe_Date.DateFrEdit.EditValue).ToShortDateString();
            string sDateto = Convert.ToDateTime(dpe_Date.DateToEdit.EditValue).ToShortDateString();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Datefrom = new SqlParameter("@DATE_FROM", sDatefrom);
                SqlParameter sp_Dateto = new SqlParameter("@DATE_TO", sDateto);
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", slup_Itemcode.EditValue.GetNullToEmpty());
                SqlParameter sp_Custcode = new SqlParameter("@CUST_CODE", slup_Custcode.EditValue.GetNullToEmpty());

                var vResult = context.Database.SqlQuery<TP_XFR5000_LIST>("USP_GET_XFR5000_LIST @DATE_FROM, @DATE_TO, @ITEM_CODE, @CUST_CODE", sp_Datefrom, sp_Dateto, sp_Itemcode, sp_Custcode).ToList();
                GridBindingSource.DataSource = vResult;
            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }
        
    }
}