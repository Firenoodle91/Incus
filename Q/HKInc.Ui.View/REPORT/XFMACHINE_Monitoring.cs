using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using DevExpress.Utils;

namespace HKInc.Ui.View.REPORT
{
    public partial class XFMACHINE_Monitoring : HKInc.Service.Base.ListFormTemplate
    {
        public XFMACHINE_Monitoring()
        {
            InitializeComponent();
            GridExControl = gridEx1;
        }

        protected override void InitControls()
        {
            base.InitControls();

            string sInterval = "";
            List<TN_STD1000> common_Arr = DbRequestHandler.GetCommCode(MasterCodeSTR.MachineMonitoring);
            if(common_Arr != null)
                if(common_Arr.Count > 0)
                {
                    sInterval = common_Arr.Where(p => p.Mcode == "2").Select(s=>s.Codename).FirstOrDefault();
                    int iInterval;
                    int.TryParse(sInterval, out iInterval);
                    timer1.Interval = iInterval;
                    timer1.Tick += Timer1_Tick;
                }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (!this.IsActive)
            {
                timer1.Stop();
                SetMessage("화면이 변경되어 모니터링이 중지되었습니다. 다시 진행하시려면 조회[F1] 버튼을 이용해 주시기 바랍니다.");
            }

            sl_Time_Title.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");

            TickLoad();
            timer1.Start();
        }

        protected override void DataLoad()
        {
            TickLoad();
            InitTop();
            timer1.Enabled = true;
        }

        private void TickLoad()
        {
            GridExControl.MainGrid.Clear();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var vResult = context.Database.SqlQuery<TP_XFMACHINE_MONITORING>("USP_GET_XFMACHINE_MONITORING").OrderBy(o => o.MONITORING_LOCATION).ToList();
                GridBindingSource.DataSource = vResult;
            }

            //GridExControl.DataSource = GridBindingSource;
            GridExControl.BeginInvoke(new MethodInvoker(delegate () {
                GridExControl.DataSource = GridBindingSource;
                GridExControl.BestFitColumns();         // 2022-08-16 김진우 추가
            }));

            //GridExControl.BestFitColumns();       // 2022-08-16 김진우 주석

            //RefreshButton();
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("MACHINE_GROUP_NAME", "설비그룹명");
            GridExControl.MainGrid.AddColumn("MACHINE_NAME", "설비명");
            GridExControl.MainGrid.AddColumn("RUN_STATUS", "설비상태");
            GridExControl.MainGrid.AddColumn("JOB_STATE_NAME", "작업상태");
            GridExControl.MainGrid.AddColumn("SUM_STOP_TIME", "비가동시간(분)", HorzAlignment.Far, FormatType.Numeric, "n0");

            GridExControl.MainGrid.AddColumn("MACHINE_RUN_TIME", "가동시간(분)", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("ITEM_CODE", "품목코드");
            GridExControl.MainGrid.AddColumn("ITEM_NM", "품명");
            GridExControl.MainGrid.AddColumn("ITEM_NM1", "품번");
            GridExControl.MainGrid.AddColumn("CAR_TYPE", "차종");

            GridExControl.MainGrid.AddColumn("CUSTOMER_NAME", "거래처명");
            GridExControl.MainGrid.AddColumn("WORK_NO", "작업지시번호");
            GridExControl.MainGrid.AddColumn("PROCESS_NAME", "공정명");
            GridExControl.MainGrid.AddColumn("LOT_NO", "생산LOTNO");
            GridExControl.MainGrid.AddColumn("START_DATE", "작업시작일", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");

            GridExControl.MainGrid.AddColumn("OK_QTY", "누적양품수량", HorzAlignment.Far, FormatType.Numeric, "#,###");
            GridExControl.MainGrid.AddColumn("FAIL_QTY", "누적불량수량", HorzAlignment.Far, FormatType.Numeric, "#,###");
        }

        /// <summary>
        /// 20220420 오세완 차장
        /// 상단 버튼 초기화
        /// </summary>
        private void InitTop()
        {
            this.panel_MachineButton.Controls.Clear();

            List<TP_XFMACHINE_MONITORING> machine_Arr = GridBindingSource.List as List<TP_XFMACHINE_MONITORING>;
            if (machine_Arr == null) return;

            int iMachine_Count = machine_Arr.Count;
            Label[] lbMachine = new Label[iMachine_Count];

            //첫 사각형 시작 좌표
            int X = 30;
            int Y = 0;

            //사각형 사이즈
            int width = 250;
            int height = 120;

            //사각형 사이 공백
            int xvoid = 10;
            int yvoid = 10;

            //다음 사각형 좌표
            int Xpoint = 0;
            int Ypoint = 0;

            //열 수량
            int Xcnt = 4;

            for (int i = 0; i < iMachine_Count; i++)
            {
                lbMachine[i] = new Label();
                lbMachine[i].Parent = this.panel_MachineButton;
                lbMachine[i].Size = new Size(width, height);
                lbMachine[i].Font = new Font("맑은 고딕", 12f, FontStyle.Bold);
                lbMachine[i].BorderStyle = BorderStyle.FixedSingle;

                if (!string.IsNullOrEmpty(machine_Arr[i].MACHINE_CODE))
                    lbMachine[i].Name = "lbl_" + machine_Arr[i].MACHINE_CODE;

                string sLabeltext = "";
                if (!string.IsNullOrEmpty(machine_Arr[i].RUN_STATUS))
                    if (machine_Arr[i].RUN_STATUS == "STOP")
                    {
                        sLabeltext = machine_Arr[i].MACHINE_NAME + "\n N / A";
                        lbMachine[i].BackColor = Color.Black;
                        lbMachine[i].ForeColor = Color.White;
                    }
                    else if (machine_Arr[i].RUN_STATUS == "RUN")
                    {
                        int iTotal = machine_Arr[i].OK_QTY - machine_Arr[i].FAIL_QTY;
                        sLabeltext = machine_Arr[i].MACHINE_NAME + "\n" + machine_Arr[i].ITEM_NM + " / " + machine_Arr[i].ITEM_NM1 + "\n 생산량 : " + iTotal.ToString("#,###");
                        lbMachine[i].BackColor = Color.Green;
                    }

                if (!string.IsNullOrEmpty(machine_Arr[i].JOB_STATE))
                    if (machine_Arr[i].JOB_STATE == ((int)MasterCodeEnum.POP_Status_Stop).ToString())
                    {
                        lbMachine[i].BackColor = Color.Red;
                    }


                lbMachine[i].Text = sLabeltext;
                lbMachine[i].TextAlign = ContentAlignment.MiddleCenter;

                var ColCalculation = (i / Xcnt) * Xcnt;
                var RowCalculation = i / Xcnt;

                //아래 i 범위 주석 참고...
                Xpoint = X + (width + xvoid) * (i - ColCalculation);
                Ypoint = Y + (height + yvoid) * RowCalculation;
                lbMachine[i].Location = new Point(Xpoint, Ypoint);
            }

            if (lbMachine.Length < 8)
            {
                Label[] newArr = new Label[8];
                for (int i = 0; i < newArr.Length; i++)
                {
                    if (i < lbMachine.Length)
                    {
                        newArr[i] = lbMachine[i];
                    }
                    else
                    {
                        newArr[i] = new Label();
                        newArr[i].Parent = this.panel_MachineButton;
                        newArr[i].Size = new Size(width, height);
                        newArr[i].Font = new Font("맑은 고딕", 12f, FontStyle.Bold);
                        newArr[i].BorderStyle = BorderStyle.FixedSingle;
                        newArr[i].BackColor = Color.Black;
                        newArr[i].ForeColor = Color.White;
                        newArr[i].Text = string.Empty;
                        newArr[i].TextAlign = ContentAlignment.MiddleCenter;

                        var ColCalculation = (i / Xcnt) * Xcnt;
                        var RowCalculation = i / Xcnt;

                        //아래 i 범위 주석 참고...
                        Xpoint = X + (width + xvoid) * (i - ColCalculation);
                        Ypoint = Y + (height + yvoid) * RowCalculation;
                        newArr[i].Location = new Point(Xpoint, Ypoint);
                    }
                }

            }
        }

        /// <summary>
        /// 20220421 오세완 차장
        /// 초기화 이후 안에 내용을 실시간 변경하기 위해 다르게 설정 
        /// </summary>
        private void RefreshButton()
        {
            /*
            List<TP_XFMACHINE_MONITORING> machine_Arr = GridBindingSource.List as List<TP_XFMACHINE_MONITORING>;
            if (machine_Arr != null)
                if (machine_Arr.Count > 0)
                {
                    foreach(TP_XFMACHINE_MONITORING each in machine_Arr)
                    {
                        string sMachinecode = each.MACHINE_CODE == null ? "" : "lbl_" + each.MACHINE_CODE;
                        Label temp_Label = panel_MachineButton.Controls.Find(sMachinecode, false).FirstOrDefault() as Label;
                        if(temp_Label != null)
                        {
                            string sLabeltext = "";
                            Color cBack = new Color();
                            Color cFore = Color.Black;
                            if(each.RUN_STATUS == "STOP")
                            {
                                cBack = Color.Black;
                                cFore = Color.White;
                                sLabeltext = each.MACHINE_NAME + "\n N / A";
                            }
                            else if(each.RUN_STATUS == "RUN")
                            {
                                int iTotal = each.OK_QTY - each.FAIL_QTY;
                                sLabeltext = each.MACHINE_NAME + "\n" + each.ITEM_NM + " / " + each.ITEM_NM1 + "\n 생산량 : " + iTotal.ToString("#,###");
                                cBack = Color.Green;
                            }

                            temp_Label.BeginInvoke(new MethodInvoker(delegate () {
                                temp_Label.Text = sLabeltext;
                                temp_Label.BackColor = cBack;
                                temp_Label.ForeColor = cFore;
                            }));
                        }
                    }
                }
                */
        }

        protected override void BaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();
        }
    }
}