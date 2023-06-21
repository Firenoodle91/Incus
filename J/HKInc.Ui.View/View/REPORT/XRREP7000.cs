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
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 리워크 불량률화면
    /// </summary>
    public partial class XRREP7000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<VI_LOT_TRACKING> ModelService = (IService<VI_LOT_TRACKING>)ProductionFactory.GetDomainService("VI_LOT_TRACKING");

        public XRREP7000()
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
            lup_Process.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process));
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("ProcessName", LabelConvert.GetLabelText("ProcessName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"), false);
            GridExControl.MainGrid.AddColumn("BadQty", LabelConvert.GetLabelText("BadQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("ReWorkOkQty", LabelConvert.GetLabelText("ReWorkOkQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("ReWorkBadQty", LabelConvert.GetLabelText("ReWorkBadQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("BadRate", LabelConvert.GetLabelText("BadRate"), HorzAlignment.Far, FormatType.Numeric, "n2");
            GridExControl.BestFitColumns();
        }

        protected override void GridRowDoubleClicked() { }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            //string item = lupItem.EditValue.GetNullToEmpty();
            string Item = lup_Item.EditValue.GetNullToEmpty();
            string dtf = dp_dt.DateFrEdit.DateTime.ToShortDateString();
            string dtt = dp_dt.DateToEdit.DateTime.ToShortDateString();
            string processcode = lup_Process.EditValue.GetNullToEmpty();
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var lType = new SqlParameter("@type", "1");
                var fdt = new SqlParameter("@fromdt", dtf);
                var tdt = new SqlParameter("@todt", dtt);
                //var itemcode = new SqlParameter("@process_code", Item);
                var Processcode = new SqlParameter("@process_code", processcode);
                var result = context.Database
                      .SqlQuery<TEMP_XRREP7000>("SP_FAIL_RAT_REWORK @type, @fromdt,@todt ,@process_code", lType, fdt, tdt, Processcode).ToList();
                GridBindingSource.DataSource = result.OrderBy(o => o.ProcessCode).ToList();

            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();


            List<TEMP_XRREP7000> ds = GridBindingSource.DataSource as List<TEMP_XRREP7000>;
            //타이틀설정
            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Visibility = DefaultBoolean.False;
            //chartTitle1.Text = "타이틀제목";
            //chartTitle1.Font = new Font("Tahoma", 14, FontStyle.Bold);
            //chartTitle1.TextColor = Color.Black;
            //ChartControls[TabPageIndex].Titles.Clear();
            //ChartControls[TabPageIndex].Titles.Add(chartTitle1);

            //X축설정 (Series)
            Chart1.DataSource = ds.GroupBy(p => new { p.ProcessName, p.ProcessCode }).Select(p => new TEMP_XRREP7000
            {
                ProcessName = p.Key.ProcessName
                 ,
                ProcessCode = p.Key.ProcessCode
                 ,
                BadRate = p.Sum(c => c.ReWorkBadQty) == 0 ? 0 : (p.Sum(c => c.ReWorkBadQty) / p.Sum(c => c.ReWorkOkQty).GetDecimalNullToZero()) * 100
            }).ToList();
            Chart1.SeriesDataMember = "ProcessName";
            Chart1.SeriesTemplate.ArgumentDataMember = "ProcessName";
            //Chart1.SeriesTemplate.ValueDataMembers.AddRange("ResultQty");
            //Chart1.SeriesTemplate.ValueDataMembers.AddRange("FQty");
            Chart1.SeriesTemplate.ValueDataMembers.AddRange("BadRate");
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

        private void Chart1_BoundDataChanged(object sender, EventArgs e)
        {
            foreach (Series series in this.Chart1.Series)
            {
                series.CrosshairLabelPattern = "{S}:{V:n2}%";
            }
        }
    }
}
