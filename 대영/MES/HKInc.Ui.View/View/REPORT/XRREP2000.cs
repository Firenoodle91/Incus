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
    /// 납기준수화면
    /// </summary>
    public partial class XRREP2000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<VI_LOT_TRACKING> ModelService = (IService<VI_LOT_TRACKING>)ProductionFactory.GetDomainService("VI_LOT_TRACKING");

        public XRREP2000()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            dp_dt.DateTime = DateTime.Today;
            Chart1.BoundDataChanged += Chart1_BoundDataChanged;

        }

        protected override void InitCombo()
        {

        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            GridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("CustNm", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"));
            GridExControl.MainGrid.AddColumn("OrderQty", LabelConvert.GetLabelText("OrderQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("DueDate", LabelConvert.GetLabelText("DueDate"));
            GridExControl.MainGrid.AddColumn("OutDate", LabelConvert.GetLabelText("OutDate"));
            GridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("DueOkQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("FQty", LabelConvert.GetLabelText("DueFQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("MQty", LabelConvert.GetLabelText("DueMQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("Rat", "납기준수율",true, HorzAlignment.Far);
            GridExControl.MainGrid.AddColumn("Rat", LabelConvert.GetLabelText("DueRate"));
            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => 1 == 1).ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            string item = textEdit1.EditValue.GetNullToEmpty();
            string dt = dp_dt.DateTime.ToShortDateString();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var fdt = new SqlParameter("@YEAR", dt);
                var result = context.Database
                      .SqlQuery<TEMP_XRREP2000>("SP_SALETODLV_RAT @YEAR", fdt).ToList();
                GridBindingSource.DataSource = result.Where(P => P.ItemName.Contains(item) || P.ItemName.Contains(item)).OrderBy(o => o.DueDate).ToList();
            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            List<TEMP_XRREP2000> ds = GridBindingSource.DataSource as List<TEMP_XRREP2000>;
            //타이틀설정
            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Visibility = DefaultBoolean.False;
            //chartTitle1.Text = "타이틀제목";
            //chartTitle1.Font = new Font("Tahoma", 14, FontStyle.Bold);
            //chartTitle1.TextColor = Color.Black;
            //ChartControls[TabPageIndex].Titles.Clear();
            //ChartControls[TabPageIndex].Titles.Add(chartTitle1);

            //X축설정 (Series)
            Chart1.DataSource = ds.GroupBy(p => new { p.CustNm, p.ItemName }).Select(p => new TEMP_XRREP2000
            {
                CustNm = p.Key.CustNm
                 ,
                ItemName = p.Key.ItemName
                 ,
                Rat1 = p.Sum(c => c.OrderQty).GetDecimalNullToZero() == 0 ? 0 : (p.Sum(c => c.OkQty) / p.Sum(c => c.OrderQty).GetDecimalNullToZero()) * 100
            }).ToList();
            Chart1.SeriesDataMember = "CustNm";
            Chart1.SeriesTemplate.ArgumentDataMember = "ItemName";
            Chart1.SeriesTemplate.ValueDataMembers.AddRange("Rat1");
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

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
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
