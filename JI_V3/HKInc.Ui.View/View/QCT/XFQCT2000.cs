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
    public partial class XFQCT2000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_SPC_VALUE> ModelService = (IService<VI_SPC_VALUE>)ProductionFactory.GetDomainService("VI_SPC_VALUE");
        public XFQCT2000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dp_dt.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dp_dt.DateToEdit.DateTime = DateTime.Today;

            MasterGridExControl.MainGrid.MainView.RowStyle += MainView_RowStyle;
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
            lupitem.SetDefault(true, "ItemCode", "fullName", ModelService.GetChildList<TN_STD1100>(p=>p.UseFlag=="Y"));

        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            DetailGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목", HorzAlignment.Near, true);
            MasterGridExControl.MainGrid.AddColumn("Process", "공정", HorzAlignment.Near, true);
            MasterGridExControl.MainGrid.AddColumn("FME_NO", "검사구분", HorzAlignment.Near, true);
            MasterGridExControl.MainGrid.AddColumn("Seq", false);

            DetailGridExControl.MainGrid.AddColumn("Checkdate", "측정일");
            DetailGridExControl.MainGrid.AddColumn("Itemcode", "품목");
            DetailGridExControl.MainGrid.AddColumn("Itemnm", "품목명", HorzAlignment.Near, true);
            DetailGridExControl.MainGrid.AddColumn("Process", "공정");
            DetailGridExControl.MainGrid.AddColumn("processnm", "공정명", HorzAlignment.Near, true);
            DetailGridExControl.MainGrid.AddColumn("FmeNo", "검사명", HorzAlignment.Near, true);
            DetailGridExControl.MainGrid.AddColumn("seq", "규격", HorzAlignment.Far, true);
            DetailGridExControl.MainGrid.AddColumn("Checknm", "검사명", HorzAlignment.Near, true);

            DetailGridExControl.MainGrid.AddColumn("Stddown", "LCL", HorzAlignment.Far, true);
            DetailGridExControl.MainGrid.AddColumn("Std", "CL", HorzAlignment.Far, true);
            DetailGridExControl.MainGrid.AddColumn("Stdup", "UCL", HorzAlignment.Far, true);

            DetailGridExControl.MainGrid.AddColumn("Testval", "측정값", HorzAlignment.Far, true);
          
        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("FME_NO", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDivision), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("Process", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.TopCategory_WAN ).ToList(), "ItemCode", "ItemName");
        }
        protected override void DataLoad()
        {
            MasterGridBindingSource.DataSource = null;

            string item = lupitem.EditValue.GetNullToEmpty();
            string dtf = dp_dt.DateFrEdit.DateTime.ToShortDateString();
            string dtt = dp_dt.DateToEdit.DateTime.ToShortDateString();
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {


                var fdt = new SqlParameter("@fdate", dtf);
                var tdt = new SqlParameter("@tdate", dtt);
                var itemcode = new SqlParameter("@itemcode", item);

                var result = context.Database
                      .SqlQuery<TP_SPC_LIST>("SP_SPC_LIST @fdate,@tdate ,@itemcode", fdt, tdt, itemcode).ToList();

                MasterGridBindingSource.DataSource = result.OrderBy(o => o.ItemCode).ToList();

            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
        }
        protected override void MasterFocusedRowChanged()
        {
            TP_SPC_LIST obj = MasterGridBindingSource.Current as TP_SPC_LIST;
            if (obj == null) return;

            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();
            DetailGridBindingSource.DataSource= ModelService.GetList(p => p.Itemcode == obj.ItemCode 
                                                                    && p.Process == obj.Process 
                                                                    && p.FmeNo == obj.FME_NO 
                                                                    && p.seq == obj.Seq 
                                                                    && p.Check_date >= dp_dt.DateFrEdit.DateTime 
                                                                    && p.Check_date <= dp_dt.DateToEdit.DateTime)
                                                                    .OrderBy(o => o.Checkdate).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();

            List<VI_SPC_VALUE> ds = DetailGridBindingSource.DataSource as List<VI_SPC_VALUE>;
            // //X축설정 (Series)
            Chart1.Series.Clear();
            Series LCL = new Series("LCL", ViewType.ScatterLine);
            LCL.DataSource = ds;
            LCL.ArgumentDataMember = "Checkdate";
            LCL.ValueDataMembers.AddRange("Stddown");
            LCL.View.Color = Color.Red;

            Series UCL = new Series("UCL", ViewType.ScatterLine);
            UCL.DataSource = ds;
            UCL.ArgumentDataMember = "Checkdate";
            UCL.ValueDataMembers.AddRange("Stdup");
            UCL.View.Color = Color.Red;

            Series CL = new Series("CL", ViewType.ScatterLine);
            CL.DataSource = ds;
            CL.ArgumentDataMember = "Checkdate";
            CL.ValueDataMembers.AddRange("Std");
            CL.View.Color = Color.Blue;

            Series TestVal = new Series("측정치", ViewType.ScatterLine);
            TestVal.DataSource = ds;
            TestVal.ArgumentDataMember = "Checkdate";
            TestVal.ValueDataMembers.AddRange("Testval");
            TestVal.View.Color = Color.Black;
            LCL.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            UCL.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            CL.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            TestVal.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            
            Chart1.Series.Add(LCL);
            Chart1.Series.Add(UCL);
            Chart1.Series.Add(CL);
            Chart1.Series.Add(TestVal);

            Chart1.CrosshairEnabled = DefaultBoolean.True;
            Chart1.CrosshairOptions.ShowValueLabels = true;
            XYDiagram diagram = (XYDiagram)Chart1.Diagram;
            
            diagram.AxisY.WholeRange.Auto = true;      // y축 범위 자동변경 설정 
            diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f);
            diagram.AxisY.Label.TextPattern = "{V:n0}";

            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisYScrolling = true;
            diagram.AxisX.VisualRange.Auto = true;
            //  diagram.AxisY.VisualRange.Auto = true;
            decimal lcl = ds[0].Stddown.GetDecimalNullToZero();
            decimal ucl = ds[0].Stdup.GetDecimalNullToZero();
            for(int i=0;i<ds.Count;i++)
            {
                if (lcl >= ds[i].Stddown.GetDecimalNullToZero())
                {
                    lcl = ds[i].Stddown.GetDecimalNullToZero();
                }
                if (ucl <= ds[i].Stdup.GetDecimalNullToZero())
                {
                    ucl = ds[i].Stdup.GetDecimalNullToZero();
                }

            }
            
            diagram.AxisY.VisualRange.MinValue = lcl;//s[0].Stddown;// - (ds[0].Stddown/100*10);// LCL ;
            diagram.AxisY.VisualRange.MaxValue =ucl ;//ds[0].Stdup;// + (ds[0].Stdup / 100 * 10);// UCL;
            
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
