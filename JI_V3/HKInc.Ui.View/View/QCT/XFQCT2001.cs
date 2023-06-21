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
using HKInc.Service.Helper;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using System.IO;
using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.View.QCT
{
    public partial class XFQCT2001 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFQCT2001()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dp_dt.DateFrEdit.DateTime = DateTime.Today.AddMonths(-6);
            dp_dt.DateToEdit.DateTime = DateTime.Today;

            //MasterGridExControl.MainGrid.MainView.RowStyle += MainView_RowStyle;

            lup_Item.EditValueChanged += lup_Item_EditValueChanged;

            timer1.Stop();
        }

        private void MainView_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                //string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["ChaeckFlag"]);
                //if (category == "NG")
                //{
                //    e.Appearance.BackColor = Color.OrangeRed;
                //    e.Appearance.ForeColor = Color.White;
                //}
            }
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", "ItemName", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());

        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품명", HorzAlignment.Near, true);
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정", HorzAlignment.Near, true);
            MasterGridExControl.MainGrid.AddColumn("CheckList", "검사항목", HorzAlignment.Near, true);
            MasterGridExControl.MainGrid.AddColumn("STDEV", "σ", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##0}", false);
            MasterGridExControl.MainGrid.AddColumn("CpK", "CpK", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##0}", true);

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("CheckDate", "측정일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목", false);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정", false);
            DetailGridExControl.MainGrid.AddColumn("CheckList", "검사명", false);

            DetailGridExControl.MainGrid.AddColumn("CheckMin", "하한", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#0}", true);
            DetailGridExControl.MainGrid.AddColumn("CheckResult", "측정치", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#0}", true);
            DetailGridExControl.MainGrid.AddColumn("CheckMax", "상한", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#0}", true);

        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemCode", "ItemName");
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
        }
        protected override void DataLoad()
        {
            MasterGridBindingSource.DataSource = null;

            string dtf = dp_dt.DateFrEdit.DateTime.ToShortDateString();
            string dtt = dp_dt.DateToEdit.DateTime.ToShortDateString();
            string item = lup_Item.EditValue.GetNullToEmpty();

            try
            {
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    var fdt = new SqlParameter("@fdate", dtf);
                    var tdt = new SqlParameter("@tdate", dtt);
                    var itemcode = new SqlParameter("@itemcode", item);

                    var result = context.Database.SqlQuery<TEMP_XFQCT2001_MASTER>("USP_GET_QCT2001_MASTER @fdate, @tdate, @itemcode", fdt, tdt, itemcode).ToList();
                    MasterGridBindingSource.DataSource = result.ToList();

                }
            }
            catch(Exception ex)
            {
                MessageBoxHandler.Show(ex.InnerException.Message + ex.StackTrace, "");
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
        }

        private void lup_Item_EditValueChanged(object sender, EventArgs e)
        {
            DataLoad();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();

            TEMP_XFQCT2001_MASTER obj = MasterGridBindingSource.Current as TEMP_XFQCT2001_MASTER;
            if (obj == null) return;

            //디테일 시작------------------------------------------------------------------------------------------------------------------------------
            string _st = dp_dt.DateFrEdit.DateTime.ToShortDateString();
            string _et = dp_dt.DateToEdit.DateTime.ToShortDateString();
            string _itemcode = obj.ItemCode;
            string _processcode = obj.ProcessCode;
            string _checklist = obj.CheckList;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var st = new SqlParameter("@fdate", _st);
                var et = new SqlParameter("@tdate", _et);
                var itemcode = new SqlParameter("@itemcode", _itemcode);
                var processcode = new SqlParameter("@processcode", _processcode);
                var checklist = new SqlParameter("@checklist", _checklist);

                var result = context.Database
                      .SqlQuery<TEMP_XFQCT2001_DETAIL>("USP_GET_QCT2001_DETAIL @fdate, @tdate, @itemcode, @processcode, @checklist", st, et, itemcode, processcode, checklist).ToList();
                DetailGridBindingSource.DataSource = result.ToList();
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();

            //디테일 끝--------------------------------------------------------------------------------------------------------------------------------



            //차트-------------------------------------------------------------------------------------------------------------------------------------
            var frDate = new SqlParameter("@FrDate", _st);
            var toDate = new SqlParameter("@ToDate", _et);
            var itemCode = new SqlParameter("@ItemCode", _itemcode);
            var processCode = new SqlParameter("@ProcessCode", _processcode);
            var checkList = new SqlParameter("@CheckList", _checklist);

            var ds = DbRequestHandler.GetDataSet("USP_GET_QCT2001_CHART", frDate, toDate, itemCode, processCode, checkList); //Xbar-R
            var ds2 = DbRequestHandler.GetDataSet("USP_GET_QCT2001_CHART2", frDate, toDate, itemCode, processCode, checkList); //Gaussian

            XBarSetChart(ds); //XBar
            RSetChart(ds); //R
            GaussianDistribution(ds2); //정규분포
        }

        private void XBarSetChart(DataSet ds)
        {

            chartControl1.Series.Clear();

            Series Xbar = new Series("Xbar", ViewType.ScatterLine);
            Xbar.DataSource = ds.Tables[0];
            Xbar.ArgumentDataMember = "CHECK_DATE"; //X축
            Xbar.ValueDataMembers.AddRange("Xbar"); //Y축
            Xbar.View.Color = Color.Black;

            Xbar.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            chartControl1.Series.Add(Xbar);

            chartControl1.CrosshairEnabled = DefaultBoolean.True;
            chartControl1.CrosshairOptions.ShowValueLabels = true;

            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

            //기준선 시작-------------------------------------------------------------------
            diagram.AxisY.ConstantLines.Clear();

            decimal xbarucl = ds.Tables[1].Rows[0]["Xbar_UCL"].GetDecimalNullToZero();
            decimal xbarcl = ds.Tables[1].Rows[0]["Xbar_CL"].GetDecimalNullToZero();
            decimal xbarlcl = ds.Tables[1].Rows[0]["Xbar_LCL"].GetDecimalNullToZero();

            ConstantLine Xbar_UCL = new ConstantLine("UCL", xbarucl);
            ConstantLine Xbar_CL = new ConstantLine("CL", xbarcl);
            ConstantLine Xbar_LCL = new ConstantLine("LCL", xbarlcl);

            Xbar_UCL.Color = Color.Red;
            Xbar_CL.Color = Color.Black;
            Xbar_LCL.Color = Color.Blue;

            Xbar_UCL.LineStyle.Thickness = 2; //선 두께, 굵기
            Xbar_CL.LineStyle.Thickness = 2;
            Xbar_LCL.LineStyle.Thickness = 2;

            Xbar_CL.LineStyle.DashStyle = DashStyle.DashDot; //점선

            diagram.AxisY.ConstantLines.Add(Xbar_UCL);
            diagram.AxisY.ConstantLines.Add(Xbar_CL);
            diagram.AxisY.ConstantLines.Add(Xbar_LCL);

            //기준선 끝---------------------------------------------------------------------

            chartControl1.Legend.Visible = true; //범례 표시
            diagram.AxisY.WholeRange.Auto = false;      // Y축 범위 수동으로 변경
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false; //Y축 최소 0부터 나오게 제거

            decimal maxxbar = ds.Tables[1].Rows[0]["maxXbar"].GetDecimalNullToZero();
            decimal minxbar = ds.Tables[1].Rows[0]["minXbar"].GetDecimalNullToZero();

            //var minMinus = Convert.ToDouble(minlcl) - 0.01;
            //var maxPlus = Convert.ToDouble(maxucl) + 0.01;

            //diagram.AxisY.WholeRange.SetMinMaxValues(minMinus, maxPlus); //Y축 범위지정 (최소, 최대)
            //diagram.AxisY.WholeRange.SetMinMaxValues(minlcl, maxucl); //Y축 범위지정 (최소, 최대)

            decimal Ymax = 0;
            decimal Ymin = 0;

            Ymax = maxxbar <= xbarucl ? xbarucl : maxxbar;
            Ymin = minxbar <= xbarlcl ? xbarlcl : minxbar;

            diagram.AxisY.WholeRange.SetMinMaxValues(Ymin, Ymax); //Y축 범위지정 (최소, 최대)

            //diagram.EnableAxisXScrolling = true;
            //diagram.EnableAxisYScrolling = true;
        }

        private void RSetChart(DataSet ds)
        {

            chartControl2.Series.Clear();

            Series Xbar = new Series("R", ViewType.ScatterLine);
            Xbar.DataSource = ds.Tables[0];
            Xbar.ArgumentDataMember = "CHECK_DATE"; //X축
            Xbar.ValueDataMembers.AddRange("R");    //Y축
            Xbar.View.Color = Color.Black;

            Xbar.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            chartControl2.Series.Add(Xbar);

            chartControl2.CrosshairEnabled = DefaultBoolean.True;
            chartControl2.CrosshairOptions.ShowValueLabels = true;

            XYDiagram diagram = (XYDiagram)chartControl2.Diagram;

            //기준선 시작-------------------------------------------------------------------
            diagram.AxisY.ConstantLines.Clear();

            decimal rucl = ds.Tables[1].Rows[0]["R_UCL"].GetDecimalNullToZero();
            decimal rcl = ds.Tables[1].Rows[0]["R_CL"].GetDecimalNullToZero();
            decimal rlcl = ds.Tables[1].Rows[0]["R_LCL"].GetDecimalNullToZero();

            ConstantLine R_UCL = new ConstantLine("UCL", rucl);
            ConstantLine R_CL = new ConstantLine("CL", rcl);
            ConstantLine R_LCL = new ConstantLine("LCL", rlcl);

            R_UCL.Color = Color.Red;
            R_CL.Color = Color.Black;
            R_LCL.Color = Color.Blue;

            R_UCL.LineStyle.Thickness = 2; //선 두께, 굵기
            R_CL.LineStyle.Thickness = 2;
            R_LCL.LineStyle.Thickness = 2;

            R_CL.LineStyle.DashStyle = DashStyle.DashDot; //점선

            diagram.AxisY.ConstantLines.Add(R_UCL);
            diagram.AxisY.ConstantLines.Add(R_CL);
            diagram.AxisY.ConstantLines.Add(R_LCL);

            //기준선 끝---------------------------------------------------------------------

            chartControl2.Legend.Visible = true; //범례 표시
            diagram.AxisY.WholeRange.Auto = false;      // Y축 범위 수동으로 변경
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false; //Y축 최소 0부터 나오게 제거

            decimal maxr = ds.Tables[1].Rows[0]["maxR"].GetDecimalNullToZero();
            decimal minr = ds.Tables[1].Rows[0]["minR"].GetDecimalNullToZero();

            //var minMinus = Convert.ToDouble(minlcl) - 0.01;
            //var maxPlus = Convert.ToDouble(maxucl) + 0.01;

            //diagram.AxisY.WholeRange.SetMinMaxValues(minMinus, maxPlus); //Y축 범위지정 (최소, 최대)
            //diagram.AxisY.WholeRange.SetMinMaxValues(minlcl, maxucl); //Y축 범위지정 (최소, 최대)

            decimal Ymax = 0;
            decimal Ymin = 0;

            Ymax = maxr <= rucl ? rucl : maxr;
            Ymin = minr <= rlcl ? rlcl : minr;

            diagram.AxisY.WholeRange.SetMinMaxValues(Ymin, Ymax); //Y축 범위지정 (최소, 최대)

            //diagram.EnableAxisXScrolling = true;
            //diagram.EnableAxisYScrolling = true;
        }
        private void GaussianDistribution(DataSet ds)
        {
            /*
            TEMP_XFQCT2001_MASTER obj = MasterGridBindingSource.Current as TEMP_XFQCT2001_MASTER;

            var avg = obj.AVG; //평균
            var stdev = obj.STDEV; //표준편차
            */


            chartControl3.Series.Clear();

            Series Gaussian = new Series("Gaussian", ViewType.Spline);

            Gaussian.DataSource = ds.Tables[0];
            Gaussian.ArgumentDataMember = "xPOINT";      //X축
            Gaussian.ValueDataMembers.AddRange("yPOINT");//Y축
            Gaussian.View.Color = Color.Black;

            chartControl3.Series.Add(Gaussian);
            XYDiagram diagram = (XYDiagram)chartControl3.Diagram;

            //기준선 시작-------------------------------------------------------------------
            diagram.AxisX.ConstantLines.Clear();

            decimal ucl = ds.Tables[1].Rows[0]["UCL"].GetDecimalNullToZero();
            decimal cl = ds.Tables[1].Rows[0]["CL"].GetDecimalNullToZero();
            decimal lcl = ds.Tables[1].Rows[0]["LCL"].GetDecimalNullToZero();

            ConstantLine UCL = new ConstantLine("UCL", ucl);
            ConstantLine CL = new ConstantLine("CL", cl);
            ConstantLine LCL = new ConstantLine("LCL", lcl);

            UCL.Color = Color.Red;
            CL.Color = Color.Black;
            LCL.Color = Color.Blue;

            UCL.LineStyle.Thickness = 1; //선 두께, 굵기
            CL.LineStyle.Thickness = 1;
            LCL.LineStyle.Thickness = 1;

            UCL.LineStyle.DashStyle = DashStyle.DashDot; //점선
            CL.LineStyle.DashStyle = DashStyle.Dot; //점선
            LCL.LineStyle.DashStyle = DashStyle.DashDot; //점선

            diagram.AxisX.ConstantLines.Add(UCL);
            diagram.AxisX.ConstantLines.Add(CL);
            diagram.AxisX.ConstantLines.Add(LCL);

            //기준선 끝---------------------------------------------------------------------

            //X,Y 축 범위 자동 조절
            diagram.AxisX.WholeRange.Auto = true;
            diagram.AxisY.WholeRange.Auto = true;

        }

        private double Gaussian(double x, double avg, double stdev)
        {
            //정규분포 계산식
            //사용x, DB에서 계산
            var v1 = x - avg;
            var v2 = Math.Pow(v1, 2) / 2 * Math.Pow(stdev, 2);  
            var v3 = 1.0 / (Math.Sqrt(2 * Math.PI) * stdev) * Math.Exp(-v2);

            return v3;
        }

        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            GridView gv = gridEx1.MainGrid.MainView as GridView;
            gv.FocusedRowHandle = i;
            i++;
            if (i == gv.RowCount)
            {
                i = 0;
            }
            timer1.Start();


        }
        string timers="";
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (timers == "")
            {
                timers = "S";
                timer1.Start();
            }
            else
            {
                timers = "";
                timer1.Stop();
            }

            
        }
    
    }
}
