using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

using DevExpress.XtraBars;

using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Interface.Handler;
using HKInc.Utils.Interface.Forms;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using HKInc.Service.Forms;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Service;
using DevExpress.Utils;

namespace HKInc.Ui.View.POP
{
    public partial class FPOP010 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MPS1401> ModelService = (IService<TN_MPS1401>)ProductionFactory.GetDomainService("TN_MPS1401");
        public FPOP010()
        {
            InitializeComponent();
            SetToolbarVisible(false);
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            InitGrid();
            InitRepository();
            DataLoad();
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            //    MasterGridExControl.MainGrid.ShowFooter = true;
            MasterGridExControl.MainGrid.MainView.RowHeight = 50;
            MasterGridExControl.MainGrid.SetGridFont(this.MasterGridExControl.MainGrid.MainView, new Font(DefaultFont.FontFamily, 11, FontStyle.Bold));
            //      MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
        //    MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");          
            MasterGridExControl.MainGrid.AddColumn("WorkId", "작업자");
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비");
            MasterGridExControl.MainGrid.AddColumn("OrderQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("Resultqty", "생산량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("Okqty", "양품수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("Failqty", "불량수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");

            //MasterGridExControl.MainGrid.SummaryItemAddNew(6);
            //MasterGridExControl.MainGrid.SummaryItemAddNew(7);
            //MasterGridExControl.MainGrid.SummaryItemAddNew(8);


            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.MainView.RowHeight = 50;
            DetailGridExControl.MainGrid.SetGridFont(this.DetailGridExControl.MainGrid.MainView, new Font(DefaultFont.FontFamily, 11, FontStyle.Bold));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MainCust", "고객사", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("Inqty", "생산(입고)량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Outqty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Stockqty", "재고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");



            gridEx3.SetToolbarVisible(false);
            //    MasterGridExControl.MainGrid.ShowFooter = true;
            gridEx3.MainGrid.MainView.RowHeight = 50;
            gridEx3.MainGrid.SetGridFont(this.gridEx3.MainGrid.MainView, new Font(DefaultFont.FontFamily, 11, FontStyle.Bold));
            // gridEx3.MainGrid.AddColumn("ItemCode", "품목코드");
            gridEx3.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            gridEx3.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            gridEx3.MainGrid.AddColumn("WorkId", "작업자");
            gridEx3.MainGrid.AddColumn("MachineCode", "설비");
            gridEx3.MainGrid.AddColumn("OrderQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            gridEx3.MainGrid.AddColumn("Resultqty", "생산량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            gridEx3.MainGrid.AddColumn("Okqty", "양품수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            gridEx3.MainGrid.AddColumn("Failqty", "불량수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");

            gridEx4.SetToolbarVisible(false);
            //    MasterGridExControl.MainGrid.ShowFooter = true;
            gridEx4.MainGrid.MainView.RowHeight = 50;
            gridEx4.MainGrid.SetGridFont(this.gridEx4.MainGrid.MainView, new Font(DefaultFont.FontFamily, 11, FontStyle.Bold));
            //gridEx4.MainGrid.AddColumn("ItemCode", "품목코드");
            gridEx4.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            gridEx4.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            gridEx4.MainGrid.AddColumn("WorkId", "작업자");
            gridEx4.MainGrid.AddColumn("MachineCode", "설비");
            gridEx4.MainGrid.AddColumn("OrderQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            gridEx4.MainGrid.AddColumn("Resultqty", "생산량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            gridEx4.MainGrid.AddColumn("Okqty", "양품수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            gridEx4.MainGrid.AddColumn("Failqty", "불량수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");

            gridEx5.SetToolbarVisible(false);
            //    MasterGridExControl.MainGrid.ShowFooter = true;
            gridEx5.MainGrid.MainView.RowHeight = 50;
            gridEx5.MainGrid.SetGridFont(this.gridEx5.MainGrid.MainView, new Font(DefaultFont.FontFamily, 11, FontStyle.Bold));
            // gridEx5.MainGrid.AddColumn("ItemCode", "품목코드");
            gridEx5.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            gridEx5.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            gridEx5.MainGrid.AddColumn("WorkId", "작업자");
            gridEx5.MainGrid.AddColumn("MachineCode", "설비");
            gridEx5.MainGrid.AddColumn("OrderQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            gridEx5.MainGrid.AddColumn("Resultqty", "생산량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            gridEx5.MainGrid.AddColumn("Okqty", "양품수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            gridEx5.MainGrid.AddColumn("Failqty", "불량수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
           // MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process, "", "", ""), "Mcode", "Codename");
            // GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");

            gridEx3.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            gridEx3.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            gridEx4.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            gridEx4.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            gridEx5.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            gridEx5.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MainCust", ModelService.GetChildList<TN_STD1400>(p => 1 == 1).ToList(), "CustomerCode", "CustomerName");
        }

        protected override void DataLoad()
        {
            timer1.Stop();
            MasterGridExControl.MainGrid.Clear();
            gridEx3.MainGrid.Clear();
            gridEx4.MainGrid.Clear();
            gridEx5.MainGrid.Clear();
            ModelService.ReLoad();
            MasterGridBindingSource.DataSource = ModelService.GetChildList<VI_RESULT_QTY>(p => p.ResultDate == DateTime.Today&&p.ProcessCode=="P01").OrderBy(o => o.WorkNo).OrderBy(s => s.Pseq).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;

            gx3bindingSource.DataSource = ModelService.GetChildList<VI_RESULT_QTY>(p => p.ResultDate == DateTime.Today && p.ProcessCode == "P03").OrderBy(o => o.WorkNo).OrderBy(s => s.Pseq).ToList();
            gridEx3.DataSource = gx3bindingSource;

            gx4bindingSource.DataSource = ModelService.GetChildList<VI_RESULT_QTY>(p => p.ResultDate == DateTime.Today && p.ProcessCode == "P04").OrderBy(o => o.WorkNo).OrderBy(s => s.Pseq).ToList();
            gridEx4.DataSource = gx4bindingSource;

            gx5bindingSource.DataSource = ModelService.GetChildList<VI_RESULT_QTY>(p => p.ResultDate == DateTime.Today && p.ProcessCode == "P06").OrderBy(o => o.WorkNo).OrderBy(s => s.Pseq).ToList();
            gridEx5.DataSource = gx5bindingSource;


            //DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_PRODQTY_MST>(p =>1==1).ToList();
            //DetailGridExControl.DataSource = DetailGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            gridEx3.MainGrid.BestFitColumns();
            gridEx4.MainGrid.BestFitColumns();
            gridEx5.MainGrid.BestFitColumns();
            //DetailGridExControl.MainGrid.BestFitColumns();
            string memo = DbRequesHandler.GetCellValue("select  memo from Tn_memo", 0).GetNullToEmpty();
            memoEdit1.EditValue = memo;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DataLoad();
        }
    }
}
