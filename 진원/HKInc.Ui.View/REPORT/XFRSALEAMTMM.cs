using System;

using System.Data;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Service.Service;
using HKInc.Utils.Class;

namespace HKInc.Ui.View.REPORT
{
    public partial class XFRSALEAMTMM :  HKInc.Service.Base.ListFormTemplate
    {
       
        public XFRSALEAMTMM()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            datePeriodEditEx1.DateFrEdit.SetFormat(DateFormat.Month);
            datePeriodEditEx1.DateToEdit.SetFormat(DateFormat.Month);
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today;
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today;
        }
        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.MainGrid.ShowFooter = true;
            GridExControl.MainGrid.AddColumn("YYYYMM", "년월");
            GridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            GridExControl.MainGrid.AddColumn("ItemNm", "품목명");
            GridExControl.MainGrid.AddColumn("ItemNm1", "품번");                      
            GridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("SaleAmt", "매출액", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.SummaryItemAddNew(4);
            GridExControl.MainGrid.SummaryItemAddNew(5);

        }
       
        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            string yymmf = datePeriodEditEx1.DateFrEdit.DateTime.ToString("yyyy-MM");
            string yymmt = datePeriodEditEx1.DateToEdit.DateTime.ToString("yyyy-MM");
          
            string sql = "select * from VI_YYYYMM_SALEAMT where YYYYMM between '" + yymmf + "' and '" + yymmt + "' order by 1,2";
            
            DataSet ds = DbRequesHandler.GetDataQury(sql);
            if (ds != null)
            {
                GridBindingSource.DataSource = ds.Tables[0];


                GridExControl.DataSource = GridBindingSource;
            }
            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }
    }
}
