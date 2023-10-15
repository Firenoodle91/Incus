using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Class;
using System.Data.SqlClient;
using DevExpress.XtraCharts;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;

namespace HKInc.Ui.View.View.REPORT
{
    public partial class XRMOLD1700 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_MOLD1700> ModelService = (IService<TN_MOLD1700>)ProductionFactory.GetDomainService("TN_MOLD1700");

        public XRMOLD1700()
        {
            InitializeComponent();

            DataLoad();
            SetCellData();
        }

        private void DataLoad()
        {

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                //SqlParameter param = new SqlParameter("@MoldCode", string.Empty);

                List<XRMOLD1700_LISTDATA_A> result = context.Database.SqlQuery<XRMOLD1700_LISTDATA_A>("EXEC USP_GET_XRMOLD1700_A").ToList();
                List<XRMOLD1700_LISTDATA_B> result2 = context.Database.SqlQuery<XRMOLD1700_LISTDATA_B>("EXEC USP_GET_XRMOLD1700_B").ToList();
                List<XRMOLD1700_LISTDATA_C> result3 = context.Database.SqlQuery<XRMOLD1700_LISTDATA_C>("EXEC USP_GET_XRMOLD1700_C").ToList();

                SetChart(xrChart1, result);
                SetChart(xrChart2, result2);

                SetCellData1(result);
                SetCellData2(result2);
                SetCellData3(result3);
            }
        }

        private void SetChart(XRChart chart, List<XRMOLD1700_LISTDATA_A> list)
        {
            Series series1 = new Series("Series1", ViewType.Pie3D);
            series1.LabelsVisibility = DefaultBoolean.True;

            chart.Series.Add(series1);
            chart.Series[0].LegendTextPattern = "{A}";
            //chart.Series[0].Label.TextPattern = "{V:N0}";

            foreach (var s in list)
            {
                chart.Series[0].Points.Add(new SeriesPoint(s.ClassName, new double[] { Convert.ToDouble(s.MoldCnt) }));
            }
        }

        private void SetChart(XRChart chart, List<XRMOLD1700_LISTDATA_B> list)
        {
            Series series1 = new Series("A", ViewType.Pie3D);
            series1.LabelsVisibility = DefaultBoolean.True;

            chart.Series.Add(series1);
            chart.Series[0].LegendTextPattern = "{A}";
            //chart.Series[0].Label.TextPattern = "{V:N0}";
            

            foreach (var s in list)
            {
                chart.Series[0].Points.Add(new SeriesPoint(s.QuarterName, new double[] { Convert.ToDouble(s.MoldCnt) }));
            }
        }

        private void SetCellData()
        {
            cell_DpmNo.Text = DbRequestHandler.GetSeqStandard("DPM01").ToString();
            cell_PrintDate.Text = DateTime.Today.ToShortDateString();
            cell_Year.Text = DateTime.Today.Year.ToString();
            cell_Year2.Text = DateTime.Today.Year.ToString();
        }

        private void SetCellData1(List<XRMOLD1700_LISTDATA_A> list)
        {
            cell_GradeAQty.Text = list[0].MoldCnt.ToString();
            cell_GradeAQty2.Text = list[0].MoldCnt.ToString();

            cell_GradeBQty.Text = list[1].MoldCnt.ToString();
            cell_GradeBQty2.Text = list[1].MoldCnt.ToString();

            cell_GradeCQty.Text = list[2].MoldCnt.ToString();
            cell_GradeCQty2.Text = list[2].MoldCnt.ToString();

            cell_GradeDQty.Text = list[3].MoldCnt.ToString();
            cell_GradeDQty2.Text = list[3].MoldCnt.ToString();

            cell_GradeETCQty.Text = list[4].MoldCnt.ToString();
            cell_GradeETCQty2.Text = list[4].MoldCnt.ToString();

            cell_GradeSumQty.Text = list[4].SumMoldCnt.ToString();

            cell_GradeAPer.Text = list[0].MoldCntPerStr;
            cell_GradeBPer.Text = list[1].MoldCntPerStr;
            cell_GradeCPer.Text = list[2].MoldCntPerStr;
            cell_GradeDPer.Text = list[3].MoldCntPerStr;
            cell_GradeETCPer.Text = list[4].MoldCntPerStr;
        }

        private void SetCellData2(List<XRMOLD1700_LISTDATA_B> list)
        {
            cell_Quarter1.Text = list[0].MoldCnt.ToString();
            cell_Quarter2.Text = list[1].MoldCnt.ToString();
            cell_Quarter3.Text = list[2].MoldCnt.ToString();
            cell_Quarter4.Text = list[3].MoldCnt.ToString();

            cell_Quarter1Per.Text = list[0].MoldCntPerStr;
            cell_Quarter2Per.Text = list[1].MoldCntPerStr;
            cell_Quarter3Per.Text = list[2].MoldCntPerStr;
            cell_Quarter4Per.Text = list[3].MoldCntPerStr;
        }

        private void SetCellData3(List<XRMOLD1700_LISTDATA_C> list)
        {
            cell_ScoreA.Text = list[0].Score.GetNullToEmpty();
            cell_ScoreB.Text = list[1].Score.GetNullToEmpty();
            cell_ScoreC.Text = list[2].Score.GetNullToEmpty();
            cell_ScoreD.Text = list[3].Score.GetNullToEmpty();

            cell_ContentA.Text = list[0].Contents.GetNullToEmpty();
            cell_ContentB.Text = list[1].Contents.GetNullToEmpty();
            cell_ContentC.Text = list[2].Contents.GetNullToEmpty();
            cell_ContentD.Text = list[3].Contents.GetNullToEmpty();
        }

        #region 테스트
        private void TEST2()
        {
            List<AAAA> list = TESTDATA();

            Series series1 = new Series("gd", ViewType.Pie3D);
            series1.LabelsVisibility = DefaultBoolean.True;
            //series1.ArgumentDataMember = "b";

            xrChart1.Series.Add(series1);
            xrChart1.Series[0].LegendTextPattern = "{A}";
            xrChart1.Series[0].Label.TextPattern = "{V:N0}";


            //CustomLegendItem clItem = new CustomLegendItem("A", "A등급");
            //CustomLegendItem clItem2 = new CustomLegendItem("B", "B등급");
            //CustomLegendItem clItem3 = new CustomLegendItem("C", "C등급");
            //CustomLegendItem clItem4 = new CustomLegendItem("D", "D등급");
            //CustomLegendItem clItem5 = new CustomLegendItem("ETC", "기타");

            //Legend legend = new Legend();
            //legend.CustomItems.AddRange(new CustomLegendItem[] { clItem, clItem2, clItem3, clItem4, clItem5});

            //xrChart1.Legends.Clear();
            //xrChart1.Legends.Add(legend);


            //xrChart2.Series[0].LegendTextPattern = "{S} - {A} - {V} - {VP}";
            xrChart2.Series[0].LegendTextPattern = "{A}";

            foreach (var s in list)
            {
                //xrChart1.Series[0].Points.Add(new SeriesPoint(s.cc));
                xrChart1.Series[0].Points.Add(new SeriesPoint(s.dd + "", new double[] { Convert.ToDouble(s.cc) }));
                xrChart2.Series[0].Points.Add(new SeriesPoint(s.dd + "", new double[] { Convert.ToDouble(s.cc) }));
            }
        }
        private void TEST()
        {
            //xrChart1.Series

            //List<AAA> list = new List<AAA>();

            AAAA a = new AAAA("A", "A등급", 10, 1);
            AAAA b = new AAAA("B", "B등급", 20, 2);
            AAAA c = new AAAA("C", "C등급", 30, 3);
            AAAA d = new AAAA("D", "D등급", 40, 4);
            AAAA e = new AAAA("E", "기타", 50, 5);

            List<AAAA> list = new List<AAAA> { a, b, c, d, e };

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
            }

            xrChart1.DataSource = list;

            xrChart1.SeriesDataMember = "ProcessName";
            xrChart1.SeriesTemplate.ArgumentDataMember = "ItemName";
            //Chart1.SeriesTemplate.ValueDataMembers.AddRange("ResultQty");
            //Chart1.SeriesTemplate.ValueDataMembers.AddRange("FQty");
            xrChart1.SeriesTemplate.ValueDataMembers.AddRange("BadRate");
            xrChart1.SeriesTemplate.Label.TextPattern = "{S}:{V:n2}%";
            //xrChart1.CrosshairEnabled = DefaultBoolean.True;
            //xrChart1.CrosshairOptions.ShowValueLabels = true;

            XYDiagram diagram = (XYDiagram)xrChart1.Diagram;

            diagram.AxisY.WholeRange.Auto = true;      // y축 범위 자동변경 설정 
            diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f);
            diagram.AxisY.Label.TextPattern = "{V:n2}%";

            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisYScrolling = true;
            diagram.AxisX.VisualRange.Auto = true;
            diagram.AxisY.VisualRange.Auto = true;
            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Visibility = DefaultBoolean.True;
        }
        private List<AAAA> TESTDATA()
        {
            AAAA a = new AAAA("A", "A등급", 10, 1);
            AAAA b = new AAAA("B", "B등급", 20, 2);
            AAAA c = new AAAA("C", "C등급", 30, 3);
            AAAA d = new AAAA("D", "D등급", 40, 4);
            AAAA e = new AAAA("E", "기타", 50, 5);

            List<AAAA> list = new List<AAAA> { a, b, c, d, e };

            return list;
        }
        #endregion

    }

    public class AAAA
    {
        public string aa { get; set; }
        public string dd { get; set; }
        public int bb { get; set; }
        public int cc { get; set; }
        
        public AAAA(string a, string d, int b, int c)
        {
            this.aa = a;
            this.bb = b;
            this.cc = c;
            this.dd = d;
        }

        public AAAA() { }
    }
    
    public class XRMOLD1700_LISTDATA_A
    {
        public string ClassName { get; set; }
        public int MoldCnt { get; set; }
        public int SumMoldCnt { get; set; }
        public double MoldCntPer { get; set; }
        public string MoldCntPerStr { get; set; }
    }

    public class XRMOLD1700_LISTDATA_B
    {
        public string QuarterCode { get; set; }
        public string QuarterName { get; set; }
        public int MoldCnt { get; set; }
        public double MoldCntPer { get; set; }
        public string MoldCntPerStr { get; set; }
    }

    public class XRMOLD1700_LISTDATA_C
    {
        public string ClassName { get; set; }
        public string Contents { get; set; }
        public string Score { get; set; }
    }
    
}
