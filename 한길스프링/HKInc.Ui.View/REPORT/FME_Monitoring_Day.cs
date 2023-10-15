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
using HKInc.Ui.Model.Domain;
using System.Data.SqlClient;
using HKInc.Utils.Class;

namespace HKInc.Ui.View.REPORT
{
    public partial class FME_Monitoring_Day : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MOLD001> ModelService = (IService<TN_MOLD001>)ProductionFactory.GetDomainService("TN_MOLD001"); //무의미

        private Timer timer1 = new Timer();

        private int CheckRowPosition = 0;

        public FME_Monitoring_Day()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;

        }

        protected override void InitControls()
        {
            base.InitControls();
            timer1.Interval = 4000;

            timer1.Tick += Timer1_Tick;

        }
        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();


            MasterGridExControl.SetToolbarVisible(false);

            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("LotNo", "LOT NO", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("WorkId", "작업자", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("Qty", "생산수량", HorzAlignment.Center, true);

            //DetailGridExControl.MainGrid.AddColumn("CheckName", "검사항목", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("Q02", "초물", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("Q03", "중물", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("Q04", "종물", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.MainView.RowHeight = 150;
            MasterGridExControl.MainGrid.Columns["Q02"].MaxWidth = 190;
            MasterGridExControl.MainGrid.Columns["Q03"].MaxWidth = 190;
            MasterGridExControl.MainGrid.Columns["Q04"].MaxWidth = 190;
            MasterGridExControl.MainGrid.Columns["Q02"].MinWidth = 190;
            MasterGridExControl.MainGrid.Columns["Q03"].MinWidth = 190;
            MasterGridExControl.MainGrid.Columns["Q04"].MinWidth = 190;
            MasterGridExControl.MainGrid.SetGridFont(this.MasterGridExControl.MainGrid.MainView, new Font(DefaultFont.FontFamily, 14, FontStyle.Bold));
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckName", DbRequesHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");

            MasterGridExControl.BestFitColumns();

        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();

            CheckRowPosition = 0;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var result = context.Database
                      .SqlQuery<MasterModel3>("SP_FME_MONITERING_DAY").ToList();

                if (result == null) return;

                MasterGridBindingSource.DataSource = result;

            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            if (timer1.Enabled) timer1.Stop();

            timer1.Start();

            SetMessage("모니터링 진행중...");
        }


        //마스터를 다음 ROW로 움직이기 위한 타이머
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (!this.IsActive)
            {
                timer1.Stop();

                SetMessage("화면이 변경되어 모니터링이 중지되었습니다. 다시 진행하시려면 조회[F1] 버튼을 이용해 주시기 바랍니다.");
            }

            DataLoad();
            //if (MasterGridExControl.MainGrid.MainView.DataRowCount > 0)
            //{
            //    MasterGridExControl.MainGrid.MainView.FocusedRowHandle = CheckRowPosition;
            //    CheckRowPosition++;
            //    if (CheckRowPosition == MasterGridExControl.MainGrid.MainView.DataRowCount) CheckRowPosition = 0;
            //}
        }

    }

}
class MasterModel3 //: IEquatable<MasterModel>
        {
            public string WorkNo { get; set; }
            public string LotNo { get; set; }
            public string MachineCode { get; set; }
            public DateTime ResultDate { get; set; }
            public string ItemCode { get; set; }
    public   string WorkId { get; set; }
    public string Qty { get; set; }

            //public bool Equals(MasterModel other)
            //{
            //    return (WorkNo == other.WorkNo && LotNo == other.LotNo && MachineCode == other.MachineCode && ResultDate == other.ResultDate);
            //}

            //// GetHashCode must return true whenever Equals returns true.
            //public override int GetHashCode()
            //{
            //    //Get hash code for the Name field if it is not null.
            //    return LotNo?.GetHashCode() ?? 0;
            //}

            public byte[] Q02 { get; set; }
            public byte[] Q03 { get; set; }
            public byte[] Q04 { get; set; }
        }
