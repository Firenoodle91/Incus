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

    /// <summary>
    /// 2021-06-21 김진우 주임 추가
    /// 설비등급평가현황 리포트 1
    /// </summary>
    public partial class XRMEA1300 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_MEA1300> ModelService = (IService<TN_MEA1300>)ProductionFactory.GetDomainService("TN_MEA1300");

        public XRMEA1300()
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

                List<XRMEA1300_LISTDATA_A> result = context.Database.SqlQuery<XRMEA1300_LISTDATA_A>("EXEC USP_GET_XRMEA1300_A").ToList();
                List<XRMEA1300_LISTDATA_B> result2 = context.Database.SqlQuery<XRMEA1300_LISTDATA_B>("EXEC USP_GET_XRMEA1300_B").ToList();
                List<XRMEA1300_LISTDATA_C> result3 = context.Database.SqlQuery<XRMEA1300_LISTDATA_C>("EXEC USP_GET_XRMEA1300_C").ToList();

                SetChart(xrChart1, result);
                SetChart(xrChart2, result2);

                SetCellData1(result);
                SetCellData2(result2);
                SetCellData3(result3);
            }
        }

        private void SetChart(XRChart chart, List<XRMEA1300_LISTDATA_A> list)
        {
            Series series1 = new Series("Series1", ViewType.Pie3D);
            series1.LabelsVisibility = DefaultBoolean.True;

            chart.Series.Add(series1);
            chart.Series[0].LegendTextPattern = "{A}";

            foreach (var s in list)
                chart.Series[0].Points.Add(new SeriesPoint(s.SHOW_GRADE, new double[] { Convert.ToDouble(s.COUNT) }));

        }

        private void SetChart(XRChart chart, List<XRMEA1300_LISTDATA_B> list)
        {
            Series series1 = new Series("A", ViewType.Pie3D);
            series1.LabelsVisibility = DefaultBoolean.True;

            chart.Series.Add(series1);
            chart.Series[0].LegendTextPattern = "{A}";

            foreach (var s in list)
                chart.Series[0].Points.Add(new SeriesPoint(s.SHOW_PART, new double[] { Convert.ToDouble(s.COUNT) }));

        }

        private void SetCellData()
        {
            cell_DpmNo.Text = DbRequestHandler.GetSeqStandard("DPM01").ToString();
            cell_PrintDate.Text = DateTime.Today.ToShortDateString();
            cell_Year.Text = DateTime.Today.Year.ToString();
            cell_Year2.Text = DateTime.Today.Year.ToString();
        }

        private void SetCellData1(List<XRMEA1300_LISTDATA_A> list)
        {
            cell_GradeAQty.Text = list[0].COUNT.ToString();
            cell_GradeAQty2.Text = list[0].COUNT.ToString();

            cell_GradeBQty.Text = list[1].COUNT.ToString();
            cell_GradeBQty2.Text = list[1].COUNT.ToString();

            cell_GradeCQty.Text = list[2].COUNT.ToString();
            cell_GradeCQty2.Text = list[2].COUNT.ToString();

            cell_GradeDQty.Text = list[3].COUNT.ToString();
            cell_GradeDQty2.Text = list[3].COUNT.ToString();

            cell_GradeETCQty.Text = list[4].COUNT.ToString();
            cell_GradeETCQty2.Text = list[4].COUNT.ToString();

            cell_GradeSumQty.Text = list[4].TOTAL_CNT.ToString();

            cell_GradeAPer.Text = list[0].CNT_PERCENT.ToString() + "%";
            cell_GradeBPer.Text = list[1].CNT_PERCENT.ToString() + "%";
            cell_GradeCPer.Text = list[2].CNT_PERCENT.ToString() + "%";
            cell_GradeDPer.Text = list[3].CNT_PERCENT.ToString() + "%";
            cell_GradeETCPer.Text = list[4].CNT_PERCENT.ToString() + "%";
        }

        private void SetCellData2(List<XRMEA1300_LISTDATA_B> list)
        {
            cell_Quarter1.Text = list[0].COUNT.ToString();
            cell_Quarter2.Text = list[1].COUNT.ToString();
            cell_Quarter3.Text = list[2].COUNT.ToString();
            cell_Quarter4.Text = list[3].COUNT.ToString();

            cell_Quarter1Per.Text = list[0].CNT_PERCENT.ToString() + "%";
            cell_Quarter2Per.Text = list[1].CNT_PERCENT.ToString() + "%";
            cell_Quarter3Per.Text = list[2].CNT_PERCENT.ToString() + "%";
            cell_Quarter4Per.Text = list[3].CNT_PERCENT.ToString() + "%";
        }

        private void SetCellData3(List<XRMEA1300_LISTDATA_C> list)
        {
            cell_ScoreA.Text = list[0].SCORE.GetNullToEmpty();
            cell_ScoreB.Text = list[1].SCORE.GetNullToEmpty();
            cell_ScoreC.Text = list[2].SCORE.GetNullToEmpty();
            cell_ScoreD.Text = list[3].SCORE.GetNullToEmpty();
            
            cell_ContentA.Text = list[0].GRADE_STAND.GetNullToEmpty();
            cell_ContentB.Text = list[1].GRADE_STAND.GetNullToEmpty();
            cell_ContentC.Text = list[2].GRADE_STAND.GetNullToEmpty();
            cell_ContentD.Text = list[3].GRADE_STAND.GetNullToEmpty();
        }
        
    }

    public class XRMEA1300_LISTDATA_A
    {
        public string CLASS { get; set; }
        public string SHOW_GRADE { get; set; }
        public double CNT_PERCENT { get; set; }
        public int COUNT { get; set; }
        public int TOTAL_CNT { get; set; }
    }

    public class XRMEA1300_LISTDATA_B
    {
        public int CLASS_DATE { get; set; }
        public string SHOW_PART { get; set; }
        public int COUNT { get; set; }
        public double CNT_PERCENT { get; set; }
    }

    public class XRMEA1300_LISTDATA_C
    {
        public string GRADE { get; set; }
        public string GRADE_STAND { get; set; }
        public string SCORE { get; set; }
    }
    
}
