using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using System.Data.SqlClient;
using DevExpress.XtraCharts;

namespace HKInc.Ui.View.REPORT
{
    public partial class XFR1000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<VI_LOTTRACKING> ModelService = (IService<VI_LOTTRACKING>)ProductionFactory.GetDomainService("VI_LOTTRACKING");
        public XFR1000()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            dp_dt.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dp_dt.DateToEdit.DateTime = DateTime.Today;

        }
        protected override void InitCombo()
        {
            lupItem.SetDefault(true, "ItemCode", "ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P01" || p.TopCategory == "P05")).OrderBy(o=>o.ItemNm1).ToList());
        }
        protected override void InitGrid()
        {

            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("Process", "공정");
            GridExControl.MainGrid.AddColumn("ItemCode", "품목");
            GridExControl.MainGrid.AddColumn("ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            GridExControl.MainGrid.AddColumn("PlanQty", "계획수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("RunQty", "실적수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Rat", "달성률", true, HorzAlignment.Far);
            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {

           // GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("Process", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");



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
            GridBindingSource.DataSource = null;

            string item = lupItem.EditValue.GetNullToEmpty();
            string dtf = dp_dt.DateFrEdit.DateTime.ToShortDateString();
            string dtt = dp_dt.DateToEdit.DateTime.ToShortDateString();
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {


                var fdt = new SqlParameter("@datefrom", dtf);
                var tdt = new SqlParameter("@dateto", dtt);
                var itemcode = new SqlParameter("@itemcode", item);

                var result = context.Database
                      .SqlQuery<TP_WORKPLANTORUN>("WORK_PLAN_RUN @datefrom,@dateto ,@itemcode", fdt, tdt, itemcode).ToList();
                GridBindingSource.DataSource = result.OrderBy(o => o.ItemCode).ToList();

            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();


            List<TP_WORKPLANTORUN> ds = GridBindingSource.DataSource as List<TP_WORKPLANTORUN>;
            //타이틀설정
            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Visibility = DefaultBoolean.False;
            //chartTitle1.Text = "타이틀제목";
            //chartTitle1.Font = new Font("Tahoma", 14, FontStyle.Bold);
            //chartTitle1.TextColor = Color.Black;
            //ChartControls[TabPageIndex].Titles.Clear();
            //ChartControls[TabPageIndex].Titles.Add(chartTitle1);

            //X축설정 (Series)
            ////Chart1.DataSource = ds;
            ////Chart1.SeriesDataMember = "ProcessNm";
            ////Chart1.SeriesTemplate.ArgumentDataMember = "ItemNm";
            ////Chart1.SeriesTemplate.ValueDataMembers.AddRange("Rat1");
            ////Chart1.SeriesTemplate.Label.TextPattern = "{S}:{V:n0}";
            ////Chart1.CrosshairEnabled = DefaultBoolean.True;
            ////Chart1.CrosshairOptions.ShowValueLabels = true;

            ////XYDiagram diagram = (XYDiagram)Chart1.Diagram;

            ////diagram.AxisY.WholeRange.Auto = true;      // y축 범위 자동변경 설정 
            ////diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f);
            ////diagram.AxisY.Label.TextPattern = "{V:n0}";

            ////diagram.EnableAxisXScrolling = true;
            ////diagram.EnableAxisYScrolling = true;
            ////diagram.AxisX.VisualRange.Auto = true;
            ////diagram.AxisY.VisualRange.Auto = true;

            Chart1.DataSource = ds;
            //Chart1.SeriesDataMember = "ItemNm";
            //Chart1.SeriesTemplate.ArgumentDataMember = "ProcessNm";
            Chart1.Series.Clear();
            Chart1.SeriesDataMember = "ProcessNm";
            Chart1.SeriesTemplate.ArgumentDataMember = "ItemNm";
            //   Chart1.SeriesTemplate.ArgumentDataMember = "ItemNm";
            Chart1.SeriesTemplate.ValueDataMembers.AddRange("Rat1");
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