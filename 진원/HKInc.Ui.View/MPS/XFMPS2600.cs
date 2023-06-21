using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.View.POP_Popup;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.MPS
{
    public partial class XFMPS2600 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_MPS1401V> ModelService = (IService<VI_MPS1401V>)ProductionFactory.GetDomainService("VI_MPS1401V");
        
        public XFMPS2600()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today;
            DetailGridExControl.MainGrid.MainView.FocusedRowChanged += DetailView_FocusedRowChanged;
        }

     

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            
            MasterGridExControl.MainGrid.AddColumn("WORK_NO", "작업지시번호");
            MasterGridExControl.MainGrid.AddColumn("ITEM_CODE", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ITEM_NM1", "품번");
            MasterGridExControl.MainGrid.AddColumn("ITEM_NM", "품명");
            //MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");

            DetailGridExControl.SetToolbarVisible(false);
           
            DetailGridExControl.MainGrid.AddColumn("ITEMMOVENO", "이동표번호", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("LOTNO", "LOTNO", HorzAlignment.Center, true);

            gridEx3.SetToolbarVisible(false);
            gridEx3.MainGrid.AddColumn("PROCESSNM", "공정명");
            gridEx3.MainGrid.AddColumn("QTY", "실적");
            gridEx3.MainGrid.AddColumn("WORKID", "작업자");
            gridEx3.MainGrid.AddColumn("WORKDATE", "작업일" );
        }
        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ITEM_CODE", ModelService.GetChildList<TN_STD1100>(p => 1 == 1).OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
        }
        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            gridEx3.MainGrid.Clear();
            string sql = "exec SP_MPS2600 @dtf= '" + datePeriodEditEx1.DateFrEdit.DateTime.Date.ToString("yyyy-MM-dd") + "',@dtt='" + datePeriodEditEx1.DateToEdit.DateTime.Date.ToString("yyyy-MM-dd") + "'";
            sql += tx_workno.Text == "" ? "" : ",@workno='" + tx_workno.Text + "'";
            sql += tx_itemmove.Text == "" ? "" : ",@itemmove='" + tx_itemmove.Text + "' ";
            DataSet ds = DbRequesHandler.GetDataQury(sql);
            MasterGridBindingSource.DataSource = ds.Tables[0];
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
        }
        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            gridEx3.MainGrid.Clear();
            var obj = MasterGridExControl.MainGrid.MainView.GetFocusedRowCellValue("WORK_NO");
            string sql = "select ITEMMOVENO,LOTNO from TN_ITEM_MOVE where workno='" + obj + "'";
            DataSet ds = DbRequesHandler.GetDataQury(sql);
     //       if (ds == null) return;
            DetailGridBindingSource.DataSource = ds.Tables[0];
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }
        private void DetailView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridEx3.MainGrid.Clear();
            var obj = DetailGridExControl.MainGrid.MainView.GetFocusedRowCellValue("ITEMMOVENO");
            if (obj != null) { 
            DataSet ds = DbRequesHandler.GetDataQury("exec SP_ITEM_MOVE_PRT @moveno = '" + obj + "'");
            
            
                bindingSource1.DataSource = ds.Tables[1];
                gridEx3.DataSource = bindingSource1;
                gridEx3.MainGrid.BestFitColumns();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //var masterObj = MasterGridBindingSource.Current as
            var obj = DetailGridExControl.MainGrid.MainView.GetFocusedRowCellValue("ITEMMOVENO");
            var workNO = MasterGridExControl.MainGrid.MainView.GetFocusedRowCellValue("WORK_NO");
            var lotNo = DetailGridExControl.MainGrid.MainView.GetFocusedRowCellValue("LOTNO");

            //XRITEMMOVE prt=new XRITEMMOVE("", "", obj.ToString());
            //ReportPrintTool printTool = new ReportPrintTool(prt);

            //printTool.ShowPreviewDialog();

            var report = new POP_Popup.XRITEMMOVE_100X100(workNO.GetNullToEmpty(), lotNo.GetNullToEmpty(), obj.GetNullToEmpty());
            report.CreateDocument();
            report.ShowPrintStatusDialog = false;
            report.ShowPreview();

            //var report = new POP_Popup.XRITEMMOVE_100X100(v.WorkNo, v.LotNo, "");
            //report.CreateDocument();
            //FirstReport.Pages.AddRange(report.Pages);
        }
    }
}
