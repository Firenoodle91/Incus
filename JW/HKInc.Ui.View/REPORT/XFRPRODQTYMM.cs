using System;

using System.Data;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Service.Service;
using DevExpress.XtraCharts;
using System.Drawing;
using System.Linq;
using HKInc.Utils.Class;

namespace HKInc.Ui.View.REPORT
{
    public partial class XFRPRODQTYMM :  HKInc.Service.Base.ListFormTemplate
    {
       
        public XFRPRODQTYMM()
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
            GridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            GridExControl.MainGrid.AddColumn("ResultQty", "생산수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("FalilQty", "불량수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("OkQty", "양품수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.SummaryItemAddNew(5);
            GridExControl.MainGrid.SummaryItemAddNew(6);
            GridExControl.MainGrid.SummaryItemAddNew(7);
        }
       
        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            string yymmf = datePeriodEditEx1.DateFrEdit.DateTime.ToString("yyyy-MM");
            string yymmt = datePeriodEditEx1.DateToEdit.DateTime.ToString("yyyy-MM");
            string sql= "select * from VI_YYYYMM_QTY where YYYYMM between '"+ yymmf + "' and '"+ yymmt + "' order by 1,2";
            DataSet ds = DbRequesHandler.GetDataQury(sql);
            if (ds != null)
            {
                GridBindingSource.DataSource = ds.Tables[0];
                GridExControl.DataSource = GridBindingSource;

                GridChart(ds);
            }

            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        private void GridChart(DataSet ds)
        {
            DataRow[] list = ds.Tables[0].Select();
            chartControl1.DataSource = list.Select(s => new PRODQTYMM_DATA
            {
                ItemCode = s.ItemArray[1].ToString()
                , ItemNm = s.ItemArray[2].ToString()
                , ItemNm1 = s.ItemArray[3].ToString()
                , Qty = s.ItemArray[5].GetNullToZero()
            })
            .GroupBy(g => new { g.ItemCode, g.ItemNm, g.ItemNm1 })
            .Select(s => new PRODQTYMM_DATA { ItemCode = s.Key.ItemCode, ItemNm = s.Key.ItemNm, ItemNm1 = s.Key.ItemNm1, SumQty = s.Sum(a => a.Qty).GetNullToZero() });

            //X축설정 (Series)
            //chartControl1.DataSource = ds;
            chartControl1.SeriesDataMember = "ItemCode";
            chartControl1.SeriesTemplate.ArgumentDataMember = "ItemNm1";
            chartControl1.SeriesTemplate.ValueDataMembers.AddRange("SumQty");
            chartControl1.SeriesTemplate.Label.TextPattern = "{S}:{V:n0}";
            chartControl1.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
            ((BarSeriesLabel)chartControl1.SeriesTemplate.Label).Position = BarSeriesLabelPosition.Top;
            ((BarSeriesLabel)chartControl1.SeriesTemplate.Label).ShowForZeroValues = true;
            chartControl1.CrosshairEnabled = DefaultBoolean.True;
            chartControl1.CrosshairOptions.ShowValueLabels = true;
            

            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

            diagram.AxisY.WholeRange.Auto = true;      // y축 범위 자동변경 설정 
            diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f);
            diagram.AxisY.Label.TextPattern = "{V:n0}";

            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisYScrolling = true;
            diagram.AxisX.VisualRange.Auto = true;
            diagram.AxisY.VisualRange.Auto = true;

            chartControl1.Legend.Visibility = DefaultBoolean.False;
        }
    }

    class PRODQTYMM_DATA
    {
        public string ItemCode { get; set; }
        public string ItemNm { get; set; }
        public string ItemNm1 { get; set; }
        public decimal Qty { get; set; }
        public decimal SumQty { get; set; }
    }
}
