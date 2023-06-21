using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 작업자별실적현황
    /// </summary>
    public partial class XRREP3000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<VI_LOT_TRACKING> ModelService = (IService<VI_LOT_TRACKING>)ProductionFactory.GetDomainService("VI_LOT_TRACKING");

        public XRREP3000()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            dp_dt.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dp_dt.DateToEdit.DateTime = DateTime.Today;

        }

        protected override void InitCombo()
        {
            lup_WorkId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("ProcessName", LabelConvert.GetLabelText("ProcessName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"));
            GridExControl.MainGrid.AddColumn("UserName", LabelConvert.GetLabelText("WorkId"));
            GridExControl.MainGrid.AddColumn("ResultQty", LabelConvert.GetLabelText("ResultQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("OKQty", LabelConvert.GetLabelText("OkQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("FQty", LabelConvert.GetLabelText("BadQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.BestFitColumns();
        }

        protected override void GridRowDoubleClicked() { }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            //InitCombo(); //phs20210624
            InitRepository();//phs20210624

            string workid = lup_WorkId.EditValue.GetNullToEmpty();
            string dtf = dp_dt.DateFrEdit.DateTime.ToShortDateString();
            string dtt = dp_dt.DateToEdit.DateTime.ToShortDateString();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var fdt = new SqlParameter("@dtfrom", dtf);
                var tdt = new SqlParameter("@dtto", dtt);
                var Workid = new SqlParameter("@worker", workid);
                var result = context.Database
                      .SqlQuery<TEMP_XRREP3000>("SP_WORKER_QTY @dtfrom,@dtto ,@worker", fdt, tdt, Workid).ToList();
                GridBindingSource.DataSource = result.OrderBy(o => o.ItemCode).ToList();
            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();


            List<TEMP_XRREP3000> ds = GridBindingSource.DataSource as List<TEMP_XRREP3000>;
            //타이틀설정
            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Visibility = DefaultBoolean.False;
            //chartTitle1.Text = "타이틀제목";
            //chartTitle1.Font = new Font("Tahoma", 14, FontStyle.Bold);
            //chartTitle1.TextColor = Color.Black;
            //ChartControls[TabPageIndex].Titles.Clear();
            //ChartControls[TabPageIndex].Titles.Add(chartTitle1);

            //X축설정 (Series)
            Chart1.DataSource = ds.GroupBy(p => new { p.ProcessName, p.UserName }).Select(p => new TEMP_XRREP3000
            {
                ProcessName = p.Key.ProcessName
                 ,
                UserName = p.Key.UserName
                 ,
                OKQty = p.Sum(c => c.OKQty)
            }).ToList();
            Chart1.SeriesDataMember = "ProcessName";
            Chart1.SeriesTemplate.ArgumentDataMember = "UserName";
            //Chart1.SeriesTemplate.ValueDataMembers.AddRange("ResultQty");
            //Chart1.SeriesTemplate.ValueDataMembers.AddRange("FQty");
            Chart1.SeriesTemplate.ValueDataMembers.AddRange("OKQty");
            Chart1.SeriesTemplate.Label.TextPattern = "{S}:{V:n0}";
            Chart1.CrosshairEnabled = DefaultBoolean.True;
            Chart1.CrosshairOptions.ShowValueLabels = true;

            XYDiagram diagram = (XYDiagram)Chart1.Diagram;

            diagram.AxisY.WholeRange.Auto = true;      // y축 범위 자동변경 설정 
            diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f);
            diagram.AxisY.Label.TextPattern = "{V:n0}";

            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisYScrolling = true;
            diagram.AxisX.VisualRange.Auto = true;
            diagram.AxisY.VisualRange.Auto = true;
            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Visibility = DefaultBoolean.True;

            //chartTitle1.Font = new Font("Tahoma", 14, FontStyle.Bold);
            //chartTitle1.TextColor = Color.Black;
            //Chart1.Titles.Clear();
            //Chart1.Titles.Add(chartTitle1);
            //List<TP_WORKPLANTORUN> ds = GridBindingSource.DataSource as List<TP_WORKPLANTORUN>;
            ////X축설정 (Series)
            //Chart1.Series.Clear();
            //Series[] series = new Series[ds.Count];
            //for (int i = 0; i < ds.Count; i++)
            //{


            //    series[i] = new Series(ds[i].Rat, ViewType.Bar);
            //    //series[i] = "BadName";
            //    series[i].SetDataMembers()
            //    series[i].ArgumentDataMember = "Process";
            //    series[i].ValueDataMembers.AddRange(new string[] { "Rat1" });
            //    series[i].LabelsVisibility = DefaultBoolean.True;

            //    //    series[i].CrosshairLabelPattern = "{S} : {V:0.#####}";
            //}

            //Chart1.Series.AddRange(series);
            //Chart1.DataSource = ds;

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

    }
}
