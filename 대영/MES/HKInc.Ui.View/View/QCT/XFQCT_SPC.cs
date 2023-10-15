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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Service.Helper;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using System.Data.SqlClient;
using HKInc.Service.Handler;
using DevExpress.Utils;
using DevExpress.XtraCharts;

namespace HKInc.Ui.View.View.QCT
{
    /// <summary>
    /// SPC관리
    /// </summary>
    public partial class XFQCT_SPC : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_QCT1100> ModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");
        List<InspectionItemModel> InspectionItemModelList = new List<InspectionItemModel>();
        public XFQCT_SPC()
        {
            InitializeComponent();
            
            MasterGridExControl = gridEx1;
            MasterGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;

            lup_Item.EditValueChanged += Lup_Item_EditValueChanged;
            lup_InspectionDivision.EditValueChanged += Lup_InspectionDivision_EditValueChanged;
            lup_Process.EditValueChanged += Lup_Process_EditValueChanged;
            
            chartControl1.AnimationStartMode = ChartAnimationMode.OnDataChanged;
            chartControl1.BoundDataChanged += ChartControl1_BoundDataChanged;
            chartControl2.AnimationStartMode = ChartAnimationMode.OnDataChanged;
            chartControl2.BoundDataChanged += ChartControl1_BoundDataChanged;
            dt_CheckDate.SetTodayIsWeek();
        }

        protected override void InitCombo()
        {         
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            lup_InspectionDivision.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            lup_Process.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            lup_InspectionItem.SetDefault(true, "CodeVal", "CodeName");
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.MainView.OptionsView.ShowIndicator = false;
        }

        protected override void InitRepository()
        {

        }

        protected override void DataLoad()
        {
            InspectionItemModelList.Clear();
            MasterGridExControl.MainGrid.Columns.Clear();
            MasterGridExControl.MainGrid.Clear();
            chartControl1.Series.Clear();
            chartControl2.Series.Clear();

            ModelService.ReLoad();

            if (dt_CheckDate.DateFrEdit.DateTime.GetNullToDateTime() == null
                || dt_CheckDate.DateToEdit.DateTime.GetNullToDateTime() == null
                || lup_Item.EditValue.IsNullOrEmpty()
                || lup_InspectionDivision.EditValue.IsNullOrEmpty()
                || lup_Process.EditValue.IsNullOrEmpty()
                || lup_InspectionItem.EditValue.IsNullOrEmpty())
            {

                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("Condition")));
                return;
            }

            var frDate = new SqlParameter("@FrDate", dt_CheckDate.DateFrEdit.DateTime);
            var toDate = new SqlParameter("@ToDate", dt_CheckDate.DateToEdit.DateTime);
            var itemCode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
            var checkDivision = new SqlParameter("@CheckDivision", lup_InspectionDivision.EditValue.GetNullToEmpty());
            var checkList = new SqlParameter("@CheckList", lup_InspectionItem.EditValue.GetNullToEmpty());
            var processCode = new SqlParameter("@ProcessCode", lup_Process.EditValue.GetNullToEmpty());

            var ds = DbRequestHandler.GetDataSet("USP_GET_SPC_GRID", frDate, toDate, itemCode, checkDivision, checkList, processCode);

            if (ds.Tables[0].Columns.Count > 0)
            {
                tx_CheckSpec.EditValue = ds.Tables[0].Rows[0][0].GetDecimalNullToNull();
                tx_CheckUpQuad.EditValue = ds.Tables[0].Rows[0][1].GetDecimalNullToNull();
                tx_CheckDownQuad.EditValue = ds.Tables[0].Rows[0][2].GetDecimalNullToNull();
                tx_USL.EditValue = ds.Tables[0].Rows[0][3].GetDecimalNullToNull();
                tx_LSL.EditValue = ds.Tables[0].Rows[0][4].GetDecimalNullToNull();
            }
            else
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionInfo")));
                //MessageBoxHandler.Show("검사목록(데이터)가 없습니다.");
                return;
            }

            if (ds.Tables[1].Columns.Count > 2)
            {
                SetMasterGridColumn(ds);
                MasterGridExControl.DataSource = ds.Tables[1];
                MasterGridExControl.BestFitColumns();                
            }
            else
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionInfo")));
                //MessageBoxHandler.Show("검사목록(데이터)가 없습니다.");
                return;
            }

            if (ds.Tables[2].Columns.Count > 1)
            {
                tx_XBar.EditValue = ds.Tables[2].Rows[0][1].GetDecimalNullToNull();
                tx_XBarUCL.EditValue = ds.Tables[2].Rows[1][1].GetDecimalNullToNull();
                tx_XBarLCL.EditValue = ds.Tables[2].Rows[2][1].GetDecimalNullToNull();

                tx_RBar.EditValue = ds.Tables[2].Rows[3][1].GetDecimalNullToNull();
                tx_RBarUCL.EditValue = ds.Tables[2].Rows[4][1].GetDecimalNullToNull();
                tx_RBarLCL.EditValue = ds.Tables[2].Rows[5][1].GetDecimalNullToNull();
            }
            else
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionInfo")));
                //MessageBoxHandler.Show("검사목록(데이터)가 없습니다.");
                return;
            }
            
            if(!tx_XBar.EditValue.IsNullOrEmpty() && !tx_XBarUCL.EditValue.IsNullOrEmpty() && !tx_XBarLCL.EditValue.IsNullOrEmpty())
                XBarSetChart(ds);

            if (!tx_RBar.EditValue.IsNullOrEmpty() && !tx_RBarUCL.EditValue.IsNullOrEmpty() && !tx_RBarLCL.EditValue.IsNullOrEmpty())
                RBarSetChart(ds);
        }

        private void SetMasterGridColumn(DataSet ds)
        {
            MasterGridExControl.MainGrid.AddColumn("DisplayOrder", LabelConvert.GetLabelText("DisplayOrder"), HorzAlignment.Far, false);
            MasterGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"), HorzAlignment.Far, true);

            foreach (DataColumn v in ds.Tables[1].Columns)
            {
                if (v.ColumnName != "Division" && v.ColumnName != "DisplayOrder")
                {
                    MasterGridExControl.MainGrid.AddColumn(v.ColumnName, v.ColumnName.Substring(3), HorzAlignment.Far, FormatType.Numeric, "#,0.#####");
                }
            }
        }

        private void XBarSetChart(DataSet ds)
        {
            try
            {
                WaitHandler.CloseWait();
                WaitHandler.ShowWait();

                chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                chartControl1.Legend.Visibility = DefaultBoolean.True;

                chartControl1.DataSource = ds.Tables[3];

                var dataMax = ds.Tables[3].Compute("Max(Value)", "").GetDecimalNullToZero();
                var dataMin = ds.Tables[3].Compute("Min(Value)", "").GetDecimalNullToZero();
                var dataXBar = tx_XBar.EditValue.GetDecimalNullToZero();
                var dataXBarUCL = tx_XBarUCL.EditValue.GetDecimalNullToZero();
                var dataXBarLCL = tx_XBarLCL.EditValue.GetDecimalNullToZero();

                var dataMinDate = ds.Tables[3].Compute("Min(CheckDate)", "");

                // Create a line series. 
                Series series1 = new Series("Series 1", ViewType.Line);
                series1.ArgumentScaleType = ScaleType.DateTime;
                series1.ArgumentDataMember = "CheckDate";
                series1.ValueDataMembers.AddRange("Value");
                series1.LegendText = "MEAN";
                chartControl1.Series.Add(series1);

                // Access the view-type-specific options of the series. 
                ((LineSeriesView)series1.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                ((LineSeriesView)series1.View).LineMarkerOptions.Color = Color.Black;
                ((LineSeriesView)series1.View).LineMarkerOptions.Kind = MarkerKind.Circle;
                ((LineSeriesView)series1.View).LineStyle.DashStyle = DashStyle.Solid;
                ((LineSeriesView)series1.View).LineStyle.Thickness = 2;
                ((LineSeriesView)series1.View).Color = Color.Black;

                //Show crosshair axis lines and crosshair axis labels to see format values of crosshair labels  
                chartControl1.CrosshairEnabled = DefaultBoolean.True;
                chartControl1.CrosshairOptions.ShowArgumentLabels = false;
                chartControl1.CrosshairOptions.ShowValueLabels = true;
                chartControl1.CrosshairOptions.ShowValueLine = true;
                chartControl1.CrosshairOptions.ShowArgumentLine = true;
                chartControl1.CrosshairOptions.ShowGroupHeaders = false;

                XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                diagram.AxisY.ConstantLines.Clear();

                // Create a constant line. 
                ConstantLine constantLineUCL = new ConstantLine("UCL");
                ConstantLine constantLineXBar = new ConstantLine("XBar");
                ConstantLine constantLineLCL = new ConstantLine("LCL");
                diagram.AxisY.ConstantLines.Add(constantLineUCL);
                diagram.AxisY.ConstantLines.Add(constantLineXBar);
                diagram.AxisY.ConstantLines.Add(constantLineLCL);
                constantLineUCL.AxisValue = tx_XBarUCL.EditValue.GetDecimalNullToNull();
                constantLineXBar.AxisValue = tx_XBar.EditValue.GetDecimalNullToNull();
                constantLineLCL.AxisValue = tx_XBarLCL.EditValue.GetDecimalNullToNull();

                // Customize the behavior of the constant line. 
                constantLineUCL.Visible = true;
                constantLineUCL.ShowInLegend = true;
                constantLineUCL.LegendText = "UCL";
                constantLineUCL.ShowBehind = true;

                constantLineXBar.Visible = true;
                constantLineXBar.ShowInLegend = true;
                constantLineXBar.LegendText = "XBar";
                constantLineXBar.ShowBehind = true;

                constantLineLCL.Visible = true;
                constantLineLCL.ShowInLegend = true;
                constantLineLCL.LegendText = "LCL";
                constantLineLCL.ShowBehind = true;

                // Customize the constant line's title. 
                constantLineUCL.Title.Visible = true;
                constantLineUCL.Title.Text = "UCL:" + tx_XBarUCL.EditValue.GetDecimalNullToZero().ToString("#,0.#####");
                constantLineUCL.Title.ShowBelowLine = true;
                constantLineUCL.Title.Alignment = ConstantLineTitleAlignment.Near;
                constantLineUCL.Title.TextColor = Color.Red;

                constantLineXBar.Title.Visible = true;
                constantLineXBar.Title.Text = "XBar:" + tx_XBar.EditValue.GetDecimalNullToZero().ToString("#,0.#####");
                constantLineXBar.Title.Alignment = ConstantLineTitleAlignment.Far;
                constantLineXBar.Title.TextColor = Color.Blue;

                constantLineLCL.Title.Visible = true;
                constantLineLCL.Title.Text = "LCL:" + tx_XBarLCL.EditValue.GetDecimalNullToZero().ToString("#,0.#####");
                constantLineLCL.Title.ShowBelowLine = false;
                constantLineLCL.Title.Alignment = ConstantLineTitleAlignment.Near;
                constantLineLCL.Title.TextColor = Color.Red;

                // Customize the appearance of the constant line. 
                constantLineUCL.Color = Color.Red;
                constantLineUCL.LineStyle.DashStyle = DashStyle.Solid;
                constantLineUCL.LineStyle.Thickness = 2;

                constantLineXBar.Color = Color.Blue;
                constantLineXBar.LineStyle.DashStyle = DashStyle.Solid;
                constantLineXBar.LineStyle.Thickness = 2;

                constantLineLCL.Color = Color.Red;
                constantLineLCL.LineStyle.DashStyle = DashStyle.Solid;
                constantLineLCL.LineStyle.Thickness = 2;

                diagram.AxisY.WholeRange.Auto = false;      // y축 범위 자동변경 설정 (값)
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false; //최소 0부터 나오게 제거

                dataMax = dataMax <= dataXBarUCL ? dataXBarUCL : dataMax;
                dataMin = dataMin <= dataXBarLCL ? dataMin : dataXBarLCL;
                diagram.AxisY.WholeRange.SetMinMaxValues(dataMin, dataMax); //범위지정

                diagram.AxisX.WholeRange.Auto = true;// x축 범위 자동변경 설정 (Arg)
                if (dataMinDate != null)
                {
                    var date = (DateTime)dataMinDate;
                    diagram.AxisX.VisualRange.Auto = false;
                    diagram.AxisX.VisualRange.SetMinMaxValues(date, date.AddDays(5));
                }

                diagram.EnableAxisXScrolling = true;
                diagram.EnableAxisYScrolling = true;

                diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f); // Argument 쪽 font 변경
                diagram.AxisY.GridLines.Visible = false; // 값 축 그리드 라인 삭제

                //diagram.AxisX.WholeRange.Auto = true;      // x축 범위 자동변경 설정 
                //diagram.AxisX.WholeRange.AlwaysShowZeroLevel = false;

                //diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f);
                //diagram.AxisY.Label.TextPattern = "{A}";

                //diagram.EnableAxisXScrolling = true;
                //diagram.EnableAxisYScrolling = true;
                //diagram.AxisX.VisualRange.Auto = false;
                //diagram.AxisY.VisualRange.Auto = false;

                //diagram.AxisY.GridLines.Visible = false;

                //diagram.AxisY.VisualRange.SetMinMaxValues(new DateTime(2018, 01, 01), new DateTime(2019, 01, 01));

                // or use the SetMinMaxValues method to specify range limits. 
                //diagram.AxisY.WholeRange.SetMinMaxValues(4, 8);

                //// Set limits for an x-axis's visual range 
                //diagram.AxisY.VisualRange.MinValue = 1000;
                //diagram.AxisY.VisualRange.MaxValue = 1500;
                //// or use the SetMinMaxValues method to specify range limits. 
                //diagram.AxisY.VisualRange.SetMinMaxValues(1000, 1500);
            }
            finally { WaitHandler.CloseWait(); }
        }

        private void RBarSetChart(DataSet ds)
        {
            try
            {
                WaitHandler.CloseWait();
                WaitHandler.ShowWait();

                chartControl2.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                chartControl2.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                chartControl2.Legend.Visibility = DefaultBoolean.True;

                chartControl2.DataSource = ds.Tables[4];

                var dataMax = ds.Tables[4].Compute("Max(Value)", "").GetDecimalNullToZero();
                var dataMin = ds.Tables[4].Compute("Min(Value)", "").GetDecimalNullToZero();
                var dataRBar = tx_RBar.EditValue.GetDecimalNullToZero();
                var dataRBarUCL = tx_RBarUCL.EditValue.GetDecimalNullToZero();
                var dataRBarLCL = tx_RBarLCL.EditValue.GetDecimalNullToZero();

                var dataMinDate = ds.Tables[4].Compute("Min(CheckDate)", "");

                // Create a line series. 
                Series series1 = new Series("Series 1", ViewType.Line);
                series1.ArgumentScaleType = ScaleType.Auto;
                series1.ArgumentDataMember = "CheckDate";
                series1.ValueDataMembers.AddRange("Value");
                series1.LegendText = "Range";
                chartControl2.Series.Add(series1);

                // Access the view-type-specific options of the series. 
                ((LineSeriesView)series1.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                ((LineSeriesView)series1.View).LineMarkerOptions.Color = Color.Black;
                ((LineSeriesView)series1.View).LineMarkerOptions.Kind = MarkerKind.Circle;
                ((LineSeriesView)series1.View).LineStyle.DashStyle = DashStyle.Solid;
                ((LineSeriesView)series1.View).LineStyle.Thickness = 2;
                ((LineSeriesView)series1.View).Color = Color.Black;

                //Show crosshair axis lines and crosshair axis labels to see format values of crosshair labels  
                chartControl2.CrosshairEnabled = DefaultBoolean.True;
                chartControl2.CrosshairOptions.ShowArgumentLabels = false;
                chartControl2.CrosshairOptions.ShowValueLabels = true;
                chartControl2.CrosshairOptions.ShowValueLine = true;
                chartControl2.CrosshairOptions.ShowArgumentLine = true;
                chartControl2.CrosshairOptions.ShowGroupHeaders = false;

                XYDiagram diagram = (XYDiagram)chartControl2.Diagram;
                diagram.AxisY.ConstantLines.Clear();

                // Create a constant line. 
                ConstantLine constantLineUCL = new ConstantLine("UCL");
                ConstantLine constantLineXBar = new ConstantLine("XBar");
                ConstantLine constantLineLCL = new ConstantLine("LCL");
                diagram.AxisY.ConstantLines.Add(constantLineUCL);
                diagram.AxisY.ConstantLines.Add(constantLineXBar);
                diagram.AxisY.ConstantLines.Add(constantLineLCL);
                constantLineUCL.AxisValue = tx_RBarUCL.EditValue.GetDecimalNullToNull();
                constantLineXBar.AxisValue = tx_RBar.EditValue.GetDecimalNullToNull();
                constantLineLCL.AxisValue = tx_RBarLCL.EditValue.GetDecimalNullToNull();

                // Customize the behavior of the constant line. 
                constantLineUCL.Visible = true;
                constantLineUCL.ShowInLegend = true;
                constantLineUCL.LegendText = "UCL";
                constantLineUCL.ShowBehind = true;

                constantLineXBar.Visible = true;
                constantLineXBar.ShowInLegend = true;
                constantLineXBar.LegendText = "RBar";
                constantLineXBar.ShowBehind = true;

                constantLineLCL.Visible = true;
                constantLineLCL.ShowInLegend = true;
                constantLineLCL.LegendText = "LCL";
                constantLineLCL.ShowBehind = true;

                // Customize the constant line's title. 
                constantLineUCL.Title.Visible = true;
                constantLineUCL.Title.Text = "UCL:" + tx_RBarUCL.EditValue.GetDecimalNullToZero().ToString("#,0.#####");
                constantLineUCL.Title.ShowBelowLine = true;
                constantLineUCL.Title.Alignment = ConstantLineTitleAlignment.Near;
                constantLineUCL.Title.TextColor = Color.Red;

                constantLineXBar.Title.Visible = true;
                constantLineXBar.Title.Text = "RBar:" + tx_RBar.EditValue.GetDecimalNullToZero().ToString("#,0.#####");
                constantLineXBar.Title.Alignment = ConstantLineTitleAlignment.Far;
                constantLineXBar.Title.TextColor = Color.Blue;

                constantLineLCL.Title.Visible = true;
                constantLineLCL.Title.Text = "LCL:" + tx_RBarLCL.EditValue.GetDecimalNullToZero().ToString("#,0.#####");
                constantLineLCL.Title.ShowBelowLine = false;
                constantLineLCL.Title.Alignment = ConstantLineTitleAlignment.Near;
                constantLineLCL.Title.TextColor = Color.Red;

                // Customize the appearance of the constant line. 
                constantLineUCL.Color = Color.Red;
                constantLineUCL.LineStyle.DashStyle = DashStyle.Solid;
                constantLineUCL.LineStyle.Thickness = 2;

                constantLineXBar.Color = Color.Blue;
                constantLineXBar.LineStyle.DashStyle = DashStyle.Solid;
                constantLineXBar.LineStyle.Thickness = 2;

                constantLineLCL.Color = Color.Red;
                constantLineLCL.LineStyle.DashStyle = DashStyle.Solid;
                constantLineLCL.LineStyle.Thickness = 2;

                diagram.AxisY.WholeRange.Auto = false;      // y축 범위 자동변경 설정 (값)
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false; //최소 0부터 나오게 제거

                dataMax = dataMax <= dataRBarUCL ? dataRBarUCL : dataMax;
                dataMin = dataMin <= dataRBarLCL ? dataMin : dataRBarLCL;
                diagram.AxisY.WholeRange.SetMinMaxValues(dataMin, dataMax); //범위지정

                diagram.AxisX.WholeRange.Auto = true;// x축 범위 자동변경 설정 (Arg)
                if (dataMinDate != null)
                {
                    var date = (DateTime)dataMinDate;
                    diagram.AxisX.VisualRange.Auto = false;
                    diagram.AxisX.VisualRange.SetMinMaxValues(date, date.AddDays(5));
                }

                diagram.EnableAxisXScrolling = true;
                diagram.EnableAxisYScrolling = true;

                diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f);
                diagram.AxisY.GridLines.Visible = false;

            }
            finally { WaitHandler.CloseWait(); }
        }

        private void Lup_Item_EditValueChanged(object sender, EventArgs e)
        {
            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            if (itemCode.IsNullOrEmpty())
            {
                lup_InspectionDivision.EditValue = null;
                lup_Process.EditValue = null;
                lup_InspectionItem.EditValue = null;

                lup_InspectionDivision.DataSource = null;
                lup_Process.DataSource = null;
                lup_InspectionItem.DataSource = null;
                InspectionItemModelList.Clear();
            }
            else
            {
                lup_InspectionDivision.EditValue = null;
                var checkLastList = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == itemCode && p.UseFlag == "Y").OrderBy(p => p.RevNo).LastOrDefault();
                if (checkLastList == null)
                    lup_InspectionDivision.DataSource = null;
                else
                {
                    var checkDivisionList = checkLastList.TN_QCT1001List.Where(p => p.UseFlag == "Y" && p.SpcFlag == "Y").GroupBy(p => p.CheckDivision).Select(p => p.Key).ToList();
                    var inspectionDivisionList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDivision);
                    lup_InspectionDivision.DataSource = inspectionDivisionList.Where(p => checkDivisionList.Contains(p.CodeVal)).OrderBy(p => p.DisplayOrder).ToList();
                    lup_InspectionDivision.Columns[0].Visible = false;
                }
            }
        }

        private void Lup_InspectionDivision_EditValueChanged(object sender, EventArgs e)
        {
            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var inspectionDivision = lup_InspectionDivision.EditValue.GetNullToEmpty();
            if (inspectionDivision.IsNullOrEmpty())
            {
                lup_Process.EditValue = null;
                lup_InspectionItem.EditValue = null;
                lup_Process.DataSource = null;
                lup_InspectionItem.DataSource = null;
                InspectionItemModelList.Clear();
            }
            else
            {
                lup_Process.EditValue = null;
                var checkLastList = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == itemCode && p.UseFlag == "Y").OrderBy(p => p.RevNo).LastOrDefault();
                if (checkLastList == null)
                    lup_Process.DataSource = null;
                else
                {
                    var checkProcessList = checkLastList.TN_QCT1001List.Where(p => p.CheckDivision == inspectionDivision && p.CheckDataType != MasterCodeSTR.CheckDataType_C && p.UseFlag == "Y" && p.SpcFlag == "Y").GroupBy(p => p.ProcessCode).Select(p => p.Key).ToList();
                    var processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
                    lup_Process.DataSource = processList.Where(p => checkProcessList.Contains(p.CodeVal)).OrderBy(p => p.DisplayOrder).ToList();
                    lup_Process.Columns[0].Visible = false;
                }
            }
        }

        private void Lup_Process_EditValueChanged(object sender, EventArgs e)
        {
            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var inspectionDivision = lup_InspectionDivision.EditValue.GetNullToEmpty();
            var processCode = lup_Process.EditValue.GetNullToEmpty();

            if (processCode.IsNullOrEmpty())
            {
                lup_InspectionItem.EditValue = null;
                lup_InspectionItem.DataSource = null;
                InspectionItemModelList.Clear();
            }
            else
            {
                lup_InspectionItem.EditValue = null;
                var checkLastList = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == itemCode && p.UseFlag == "Y").OrderBy(p => p.RevNo).LastOrDefault();
                if (checkLastList == null)
                {
                    lup_InspectionItem.DataSource = null;
                    InspectionItemModelList.Clear();
                }
                else
                {
                    var checkInspectionItemList = checkLastList.TN_QCT1001List.Where(p => p.CheckDivision == inspectionDivision && p.ProcessCode == processCode && p.CheckDataType != MasterCodeSTR.CheckDataType_C && p.UseFlag == "Y" && p.SpcFlag == "Y").GroupBy(p => p.CheckList).Select(p => p.Key).ToList();
                    foreach (var v in checkInspectionItemList)
                        InspectionItemModelList.Add(new InspectionItemModel() { CodeVal = v, CodeName = v });

                    //var inspectionItemList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionItem);
                    lup_InspectionItem.DataSource = InspectionItemModelList;//inspectionItemList.Where(p => checkInspectionItemList.Contains(p.CodeVal)).OrderBy(p => p.DisplayOrder).ToList();
                    lup_InspectionItem.Columns[0].Visible = false;
                }
            }
        }
        
        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName != "Division" && e.Column.FieldName != "DisplayOrder")
            {
                var displayOrder = MasterGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "DisplayOrder").GetIntNullToZero();
                if (displayOrder == 17)
                {
                    var XBarLCL = tx_XBarLCL.EditValue.GetDecimalNullToNull();
                    var XBarUCL = tx_XBarUCL.EditValue.GetDecimalNullToNull();

                    var Mean = MasterGridExControl.MainGrid.MainView.GetListSourceRowCellValue(10, e.Column.FieldName).GetDecimalNullToNull(); //MEAN
                    if (XBarLCL == null || XBarUCL == null || Mean == null)
                    {
                        e.DisplayText = "";
                    }
                    else if (Mean < XBarLCL)
                    {
                        e.DisplayText = "NG";
                    }
                    else if (Mean > XBarUCL)
                    {
                        e.DisplayText = "NG";
                    }
                    else
                    {
                        e.DisplayText = "OK";
                    }
                }
            }
        }

        private void ChartControl1_BoundDataChanged(object sender, EventArgs e)
        {
            foreach (Series series in this.chartControl1.Series)
            {
                series.CrosshairLabelPattern = "{A:yyyy-MM-dd} : {V:#,0.#####}";
                XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            }

        }

        private class InspectionItemModel
        {
            public string CodeVal { get; set; }
            public string CodeName { get; set; }
        }
    }
}