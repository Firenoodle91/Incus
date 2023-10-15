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

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 작업지시대비실적화면
    /// </summary>
    public partial class XRREP1000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<VI_LOT_TRACKING> ModelService = (IService<VI_LOT_TRACKING>)ProductionFactory.GetDomainService("VI_LOT_TRACKING");

        public XRREP1000()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            dp_dt.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dp_dt.DateToEdit.DateTime = DateTime.Today;
            Chart1.BoundDataChanged += Chart1_BoundDataChanged;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void InitGrid()
        {

            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("ProcessName", LabelConvert.GetLabelText("ProcessName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"));
            GridExControl.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("ResultQty", LabelConvert.GetLabelText("ResultQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("AchieveRate", LabelConvert.GetLabelText("AchieveRate"), HorzAlignment.Far, FormatType.Numeric,"{0:#,###,###,##0.##}%");
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            GridBindingSource.DataSource = null;

            string item = lup_Item.EditValue.GetNullToEmpty();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var fdt = new SqlParameter("@datefrom", dp_dt.DateFrEdit.DateTime);
                var tdt = new SqlParameter("@dateto", dp_dt.DateToEdit.DateTime);
                var itemcode = new SqlParameter("@itemcode", item);

                var result = context.Database
                      .SqlQuery<TEMP_XRREP1000>("SP_GET_XRREP1000 @datefrom,@dateto ,@itemcode", fdt, tdt, itemcode).ToList();
                GridBindingSource.DataSource = result.OrderBy(o => o.ItemCode).ToList();

            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();


            List<TEMP_XRREP1000> ds = GridBindingSource.DataSource as List<TEMP_XRREP1000>;
            //타이틀설정
            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Visibility = DefaultBoolean.False;
            //chartTitle1.Text = "타이틀제목";
            //chartTitle1.Font = new Font("Tahoma", 14, FontStyle.Bold);
            //chartTitle1.TextColor = Color.Black;
            //ChartControls[TabPageIndex].Titles.Clear();
            //ChartControls[TabPageIndex].Titles.Add(chartTitle1);

            //X축설정 (Series)
            Chart1.DataSource = ds.GroupBy(p => new { p.ProcessName, p.ItemName }).Select(p => new TEMP_XRREP1000
            {
                ProcessName = p.Key.ProcessName
                 ,
                ItemName = p.Key.ItemName
                 ,
                AchieveRate = p.Sum(c => c.WorkQty).GetDecimalNullToZero() == 0 ? 0 : (p.Sum(c => c.ResultQty) / p.Sum(c => c.WorkQty).GetDecimalNullToZero()) * 100
            }).ToList();
            Chart1.SeriesDataMember = "ProcessName";
            Chart1.SeriesTemplate.ArgumentDataMember = "ItemName";
            Chart1.SeriesTemplate.ValueDataMembers.AddRange("AchieveRate");
            Chart1.SeriesTemplate.Label.TextPattern = "{S}:{V:n2}%";
            Chart1.CrosshairEnabled = DefaultBoolean.True;
            Chart1.CrosshairOptions.ShowValueLabels = true;

            XYDiagram diagram = (XYDiagram)Chart1.Diagram;

            diagram.AxisY.WholeRange.Auto = true;      // y축 범위 자동변경 설정 
            diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f);
            diagram.AxisY.Label.TextPattern = "{V:n2}%";

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
            //    series[i].ValueDataMembers.AddRange(new string[] { "AchieveRate" });
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

        private void Chart1_BoundDataChanged(object sender, EventArgs e)
        {
            foreach (Series series in this.Chart1.Series)
            {
                series.CrosshairLabelPattern = "{S}:{V:n2}%";
            }
        }
    }
}
