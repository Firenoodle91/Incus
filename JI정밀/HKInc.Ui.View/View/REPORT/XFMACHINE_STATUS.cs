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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using DevExpress.XtraGrid.Views.Base;
using System.IO;
using System.Drawing.Imaging;
using System.Data.SqlClient;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 설비별 생산현황
    /// </summary>
    public partial class XFMACHINE_STATUS : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        List<TEMP_XFMACHINE_STATUS> MachineStatusList = new List<TEMP_XFMACHINE_STATUS>();

        private Timer timer1 = new Timer();

        //dynamic result = null;
        //dynamic rownum = null;

        decimal MinRange = 0;
        decimal MaxRange = 15;

        public XFMACHINE_STATUS()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;

            this.FormClosed += XFMACHINE_STATUS_FormClosed;

            gridEx1.MainGrid.MainView.OptionsView.EnableAppearanceEvenRow = false;
            gridEx1.MainGrid.MainView.OptionsView.EnableAppearanceOddRow = false;

            MasterGridExControl.MainGrid.MainView.RowStyle += MainView_RowStyle;

            Btn_Next.Tag = "next";
            Btn_Prev.Tag = "prev";

            Btn_Next.Click += Btn_Click;
            Btn_Prev.Click += Btn_Click;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            int multipliers = 0;

            SimpleButton btn = sender as SimpleButton;

            switch(btn.Tag)
            {
                case "next":
                    multipliers = 1;
                    MinRange += 15;
                    MaxRange += 15;
                    break;

                case "prev":
                    multipliers = -1;
                    MinRange -= 15;
                    MaxRange -= 15;
                    break;
            }

            try
            {
                if (MinRange < 0) return;
                
                MasterGridBindingSource.DataSource = MachineStatusList.Where(p => p.RowId > MinRange && p.RowId <= MaxRange).ToList();
                MasterGridExControl.DataSource = MasterGridBindingSource;
                MasterGridExControl.BestFitColumns();
   
            }
            catch { }
        }

        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (!UserRight.HasSelect)
                    ActRefresh();
            }
        }

        protected override void InitCombo()
        {
            timer1.Interval = 30 * 1000; //30초
            timer1.Tick += Timer1_Tick;

        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();

            MasterGridExControl.SetToolbarVisible(false);

            MasterGridExControl.MainGrid.AddColumn("RowId", "RowId", HorzAlignment.Center, false);


            MasterGridExControl.MainGrid.AddColumn("MachineGroupName", "설비그룹", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("MachineName", "설비명", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("MachineState", "설비상태", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("MachineStateColor", "색상", HorzAlignment.Center, false);
            MasterGridExControl.MainGrid.AddColumn("StopTime", "비가동(분)", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OperationTime", "가동(분)", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", HorzAlignment.Center, false);
            MasterGridExControl.MainGrid.AddColumn("ItemName", "품명", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("CarType", "차종", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("CustomerName", "거래처", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("ProcessName", "공정", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("ProductLotNo", "생산LOTNO", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("ResultStartDate", "작업시작일", HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OkQty", "양품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("BadQty", "불량수량", HorzAlignment.Far, FormatType.Numeric, "n0");



            MasterGridExControl.MainGrid.MainView.RowHeight = 50;
            MasterGridExControl.MainGrid.SetGridFont(this.MasterGridExControl.MainGrid.MainView, new Font(DefaultFont.FontFamily, 12f, FontStyle.Bold));
         
        }

        protected override void InitRepository()
        {

        }

        protected override void InitDataLoad()
        {
            DataLoad();
            timer1.Start();
        }

        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();
            MachineStatusList.Clear();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var result = context.Database.SqlQuery<TEMP_XFMACHINE_STATUS>("USP_GET_XFMACHINE_STATUS").ToList();

                if (result == null) return;

                MachineStatusList.AddRange(result);

                MasterGridBindingSource.DataSource = MachineStatusList.Where(p => p.RowId <= 15).ToList();
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            if (timer1.Enabled) timer1.Stop();
            timer1.Start();
            SetMessage("모니터링 진행중...");
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            if (!this.IsActive)
            {
                timer1.Stop();
                SetMessage("화면이 변경되어 모니터링이 중지되었습니다. 다시 진행하시려면 조회[F1] 버튼을 이용해 주시기 바랍니다.");
                return;
            }

            if (MasterGridBindingSource == null || MasterGridBindingSource.DataSource == null)
            {
                timer1.Stop();
            }
            else
            {
                List<TEMP_XFMACHINE_STATUS> DateList = MasterGridBindingSource.List as List<TEMP_XFMACHINE_STATUS>;

                if(DateList != null)
                {
                    decimal MaxRowNum = DateList.Count == 0 ? 0 : DateList.Max(p => p.RowId).GetDecimalNullToZero();

                    if(MaxRowNum == MachineStatusList.Count)
                    {
                        MinRange = 0;
                        MaxRange = 15;

                        timer1.Stop();
                        ActRefresh();
                    }
                    else
                    {
                        MasterGridBindingSource.DataSource = MachineStatusList.Where(p => p.RowId > MaxRowNum && p.RowId <= MaxRowNum + 15).ToList();

                        MasterGridExControl.DataSource = MasterGridBindingSource;
                        MasterGridExControl.BestFitColumns();

                        MinRange += 15;
                        MaxRange += 15;
                    }                    
                }
            }
        }

        private void XFMACHINE_STATUS_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();
        }

        private void MainView_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;

                //if (e.RowHandle <= 0) return;


                string color = View.GetRowCellDisplayText(e.RowHandle, View.Columns["MachineStateColor"]);

                Color backcolor = ColorTranslator.FromHtml(color);

                View.Columns["MachineState"].AppearanceCell.BackColor = backcolor;
                View.Columns["MachineState"].AppearanceCell.ForeColor = Color.White;


                /*
                //or
                string State = View.GetRowCellDisplayText(e.RowHandle, View.Columns["MachineState"]);

                switch (State)
                {
                    case "가동":
                        View.Columns["MachineState"].AppearanceCell.BackColor = Color.Green;
                        break;
                }
                */
            }
            catch { }
                

        }

    }
}
