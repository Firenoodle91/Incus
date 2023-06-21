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
using DevExpress.Utils;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using System.Data.SqlClient;
using HKInc.Utils.Class;

namespace HKInc.Ui.View.View.REPORT
{
    public partial class FME_MonitoringProcess : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100"); 

        private Timer timer1 = new Timer();
        private Timer timer2 = new Timer();
        private int CheckRowPosition = 0;

        public FME_MonitoringProcess()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitControls()
        {
            base.InitControls();
            timer1.Interval = 20000;
            timer2.Interval = 30000;

            timer1.Tick += Timer1_Tick;
            timer2.Tick += Timer2_Tick;
        }
        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            DetailGridExControl.MainGrid.Init();

            MasterGridExControl.SetToolbarVisible(false);
            DetailGridExControl.SetToolbarVisible(false);

            //MasterGridExControl.MainGrid.AddColumn("WorkDate", "작업일자", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호", HorzAlignment.Center, true);   
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", HorzAlignment.Center, false);
            MasterGridExControl.MainGrid.AddColumn("ItemName", "품명", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정코드", HorzAlignment.Center, false);
            MasterGridExControl.MainGrid.AddColumn("ProcessName", "공정", HorzAlignment.Center, true);
            //MasterGridExControl.MainGrid.AddColumn("LotNo", "LOT NO", HorzAlignment.Center, true);

            DetailGridExControl.MainGrid.AddColumn("Q02", "주간초물", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("Q03", "주간중물", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("Q04", "주간종물", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("Q05", "야간초물", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("Q06", "야간중물", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("Q07", "야간종물", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.MainView.RowHeight = 150;            
            DetailGridExControl.MainGrid.Columns["Q02"].MaxWidth = 150;
            DetailGridExControl.MainGrid.Columns["Q03"].MaxWidth = 150;
            DetailGridExControl.MainGrid.Columns["Q04"].MaxWidth = 150;
            DetailGridExControl.MainGrid.Columns["Q02"].MinWidth = 150;
            DetailGridExControl.MainGrid.Columns["Q03"].MinWidth = 150;
            DetailGridExControl.MainGrid.Columns["Q04"].MinWidth = 150;
            DetailGridExControl.MainGrid.Columns["Q05"].MaxWidth = 150;
            DetailGridExControl.MainGrid.Columns["Q06"].MaxWidth = 150;
            DetailGridExControl.MainGrid.Columns["Q07"].MaxWidth = 150;
            DetailGridExControl.MainGrid.Columns["Q05"].MinWidth = 150;
            DetailGridExControl.MainGrid.Columns["Q06"].MinWidth = 150;
            DetailGridExControl.MainGrid.Columns["Q07"].MinWidth = 150;
            //DetailGridExControl.MainGrid.SetGridFont(DetailGridExControl.MainGrid.MainView, new Font(Font.FontFamily, 15f, FontStyle.Bold));
            DetailGridExControl.MainGrid.MainView.Appearance.Row.Font = new Font(Font.FontFamily, 15f, FontStyle.Bold);
        }

        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckName", DbRequesHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            CheckRowPosition = 0;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var result = context.Database
                      .SqlQuery<MasterModel2>("USP_GET_FME_Monitoring_Master_Process_V2").ToList();

                if (result == null) return;

                MasterGridBindingSource.DataSource = result;

            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            if (timer1.Enabled) timer1.Stop();
            if (timer2.Enabled) timer2.Stop();
            timer1.Start();
            timer2.Start();
            SetMessage("모니터링 진행중...");
        }

        protected override void MasterFocusedRowChanged()
        {
            var Mobj = MasterGridBindingSource.Current as MasterModel2;
            if (Mobj == null) return;
            DetailGridExControl.MainGrid.Clear();

            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@WorkNo", Mobj.WorkNo),
                new SqlParameter("@ProcessCode", Mobj.ProcessCode)
                //new SqlParameter("@WorkDate", Mobj.WorkDate)
            };

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var result = context.Database
                      .SqlQuery<DetailClass2>("USP_GET_FME_Monitoring_Detail_Process_V2 @WorkNo, @ProcessCode", sqlParameters).ToList();

                if (result == null) return;
                DetailGridBindingSource.DataSource = result;
            }
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        //마스터를 다음 ROW로 움직이기 위한 타이머
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (!this.IsActive)
            {
                timer1.Stop();
                timer2.Stop();
                SetMessage("화면이 변경되어 모니터링이 중지되었습니다. 다시 진행하시려면 조회[F1] 버튼을 이용해 주시기 바랍니다.");
            }

            if (MasterGridExControl.MainGrid.MainView.DataRowCount > 0)
            {
                MasterGridExControl.MainGrid.MainView.FocusedRowHandle = CheckRowPosition;
                CheckRowPosition++;
                if (CheckRowPosition == MasterGridExControl.MainGrid.MainView.DataRowCount) CheckRowPosition = 0;
            }
        }

        //데이터를 가져와 마스터 맨밑으로 삽입하는 타이머
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (!this.IsActive)
            {
                timer1.Stop();
                timer2.Stop();
                SetMessage("화면이 변경되어 모니터링이 중지되었습니다. 다시 진행하시려면 조회[F1] 버튼을 이용해 주시기 바랍니다.");
            }

            var list = MasterGridBindingSource.List as List<MasterModel2>;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var result = context.Database
                      .SqlQuery<MasterModel2>("USP_GET_FME_Monitoring_Master_Process_V2").ToList();

                if (result == null) return;
                var checkResult = result.Except(list).ToList();
                if(checkResult.Count > 0)
                {
                    list.AddRange(checkResult);
                    MasterGridExControl.MainGrid.MainView.RefreshData();
                }
            }
        }
    }

    class MasterModel2 : IEquatable<MasterModel2>
    {
        public DateTime WorkDate { get; set; }
        public string WorkNo { get; set; }
        public string LotNo { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public bool Equals(MasterModel2 other)
        {
            return (WorkNo == other.WorkNo && LotNo == other.LotNo && ItemCode == other.ItemCode && WorkDate == other.WorkDate && ProcessCode == other.ProcessCode);
        }

        // GetHashCode must return true whenever Equals returns true.
        public override int GetHashCode()
        {
            //Get hash code for the Name field if it is not null.
            return LotNo?.GetHashCode() ?? 0;
        }
    }

    class DetailClass2
    {
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public byte[] Q02 { get; set; }
        public byte[] Q03 { get; set; }
        public byte[] Q04 { get; set; }
        public byte[] Q05 { get; set; }
        public byte[] Q06 { get; set; }
        public byte[] Q07 { get; set; }
    }
}