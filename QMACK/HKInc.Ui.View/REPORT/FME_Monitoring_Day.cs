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
    /// <summary>
    /// 현장 현황판
    /// </summary>
    public partial class FME_Monitoring_Day : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MOLD001> ModelService = (IService<TN_MOLD001>)ProductionFactory.GetDomainService("TN_MOLD001"); //무의미
        private Timer timer1 = new Timer();

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
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").ToList(), "ItemCode", "ItemNm");
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                MasterGridBindingSource.DataSource = context.Database.SqlQuery<MasterModel3>("SP_FME_MONITERING_DAY").ToList();
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
    public string WorkId { get; set; }
    public string Qty { get; set; }
    public byte[] Q02 { get; set; }
    public byte[] Q03 { get; set; }
    public byte[] Q04 { get; set; }
}
