﻿using System;
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
    /// 초중종모니터링
    /// </summary>
    public partial class FME_Monitoring : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MOLD001> ModelService = (IService<TN_MOLD001>)ProductionFactory.GetDomainService("TN_MOLD001"); //무의미
        private Timer timer1 = new Timer();
        private Timer timer2 = new Timer();
        private int CheckRowPosition = 0;

        public FME_Monitoring()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitControls()
        {
            base.InitControls();
            timer1.Interval = 4000;
            timer2.Interval = 5000;

            timer1.Tick += Timer1_Tick;
            timer2.Tick += Timer2_Tick;
        }
        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            DetailGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarVisible(false);
            DetailGridExControl.SetToolbarVisible(false);

            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품목", HorzAlignment.Center, true);             // 2022-02-16 김진우 추가
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번", HorzAlignment.Center, true);            // 2022-02-16 김진우 추가
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호", HorzAlignment.Center, true);            
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비코드", HorzAlignment.Center, false);
            MasterGridExControl.MainGrid.AddColumn("MachineName", "설비", HorzAlignment.Center, true);        // 2022-02-21 김진우 추가
            MasterGridExControl.MainGrid.AddColumn("LotNo", "LOT NO", HorzAlignment.Center, true);
            
            DetailGridExControl.MainGrid.AddColumn("Q02", "초물", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("Q03", "중물", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("Q04", "종물", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.MainView.RowHeight = 150;
            DetailGridExControl.MainGrid.Columns["Q02"].MaxWidth = 190;
            DetailGridExControl.MainGrid.Columns["Q03"].MaxWidth = 190;
            DetailGridExControl.MainGrid.Columns["Q04"].MaxWidth = 190;
            DetailGridExControl.MainGrid.Columns["Q02"].MinWidth = 190;
            DetailGridExControl.MainGrid.Columns["Q03"].MinWidth = 190;
            DetailGridExControl.MainGrid.Columns["Q04"].MinWidth = 190;
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
                MasterGridBindingSource.DataSource = context.Database.SqlQuery<MasterModel>("USP_GET_FME_Monitoring_Master").ToList();
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
            var Mobj = MasterGridBindingSource.Current as MasterModel;
            if (Mobj == null) return;
            DetailGridExControl.MainGrid.Clear();

            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@WorkNo", Mobj.WorkNo),
                new SqlParameter("@LotNo", Mobj.LotNo),
                new SqlParameter("@ResultDate", Mobj.ResultDate),
                new SqlParameter("@MachineCode", Mobj.MachineCode),
            };

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var result = context.Database
                      .SqlQuery<DetailClass>("USP_GET_FME_Monitoring_Detail @WorkNo, @LotNo, @ResultDate, @MachineCode", sqlParameters).ToList();

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

            var list = MasterGridBindingSource.List as List<MasterModel>;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var result = context.Database
                      .SqlQuery<MasterModel>("USP_GET_FME_Monitoring_Master").ToList();

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

    class MasterModel : IEquatable<MasterModel>
    {
        public string WorkNo { get; set; }
        public string LotNo { get; set; }
        public string MachineCode { get; set; }
        public string MachineName { get; set; }         // 2022-02-21 김진우 추가
        public DateTime ResultDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemNm { get; set; }              // 2022-02-16 김진우 추가
        public string ItemNm1 { get; set; }             // 2022-02-16 김진우 추가

        public bool Equals(MasterModel other)
        {
            return (WorkNo == other.WorkNo && LotNo == other.LotNo && MachineCode == other.MachineCode && ResultDate == other.ResultDate);
        }

        // GetHashCode must return true whenever Equals returns true.
        public override int GetHashCode()
        {
            //Get hash code for the Name field if it is not null.
            return LotNo?.GetHashCode() ?? 0;
        }
    }

    class DetailClass
    {
        public byte[] Q02 { get; set; }
        public byte[] Q03 { get; set; }
        public byte[] Q04 { get; set; }
    }
}