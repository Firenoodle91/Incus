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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.XtraReports.UI;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using DevExpress.XtraLayout;
using DevExpress.Utils;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Common;
using System.Data.SqlClient;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 설비모니터링
    /// POP 비가동 팝업 참조
    /// </summary>
    public partial class XFMACHINE_STATUS_RECTANGLE : Service.Base.BaseForm
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
        List<TEMP_XFMACHINE_STATUS> MachineStatusList = new List<TEMP_XFMACHINE_STATUS>();

        //Control[] MachineList;
        Label [] MachineList;

        private Timer timer1 = new Timer();

        

        public XFMACHINE_STATUS_RECTANGLE()
        {
            InitializeComponent();
            this.Text = "설비모니터링";

            this.FormClosed += _FormClosed;

            int second = 30;
            timer1.Interval = second * 1000;
            timer1.Tick += Timer1_Tick;

        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (!this.IsActive)
            {
                timer1.Stop();
                //SetMessage("화면이 변경되어 모니터링이 중지되었습니다. 다시 진행하시려면 조회[F1] 버튼을 이용해 주시기 바랍니다.");
                return;
            }

            InitControls();
        }
        /*
        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (!UserRight.HasSelect)
                    ActRefresh();
            }
        }
        */

        protected override void InitDataLoad()
        {
            //InitControls();
            //timer1.Start();
        }

        protected override void DataLoad()
        {
            InitControls();
            timer1.Start();
        }

        protected override void InitControls()
        {
            base.InitControls(); //없애면 안됨.

            this.panel_Button.Controls.Clear();

            //var TN_MEA1000_List = ModelService.GetList(p => p.UseFlag == "Y").OrderBy(o => o.MachineMCode).ToList();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                MachineStatusList.Clear();
                context.Database.CommandTimeout = 0;
                var result = context.Database.SqlQuery<TEMP_XFMACHINE_STATUS>("USP_GET_XFMACHINE_STATUS").ToList();
                MachineStatusList.AddRange(result);
            }

            //var cultureIndex = DataConvert.GetCultureIndex();

            MachineList = new Label[MachineStatusList.Count];

            //첫 사각형 시작 좌표
            int X = 30;
            int Y = 0;

            //사각형 사이즈
            int width = 190;
            int height = 100;

            //사각형 사이 공백
            int xvoid = 10;
            int yvoid = 10;

            //다음 사각형 좌표
            int Xpoint = 0;
            int Ypoint = 0;

            //열 수량
            int Xcnt = 8;

            int ListCNT = MachineStatusList.Count;

            int sss = panel_Button.Size.Width / Xcnt;

            for (int i = 0; i < ListCNT; i++)
            {
                MachineList[i] = new Label();
                //MachineList[i].Name = MachineStatusList[i].MachineMCode;
                MachineList[i].Parent = this.panel_Button;
                MachineList[i].Size = new Size(width, height);
                MachineList[i].Font = new Font("맑은 고딕", 14f, FontStyle.Bold);
                MachineList[i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                Color backcolor = ColorTranslator.FromHtml(MachineStatusList[i].MachineStateColor.ToString());
                MachineList[i].BackColor = backcolor;
                MachineList[i].ForeColor = Color.White;

                string LabelText = string.Empty;

                if (MachineList[i].BackColor == Color.Black)
                {
                    LabelText = MachineStatusList[i].MachineName + "\n" + "N / A";
                }
                else
                {
                    LabelText = MachineStatusList[i].MachineName + "\n" 
                              + MachineStatusList[i].ItemName + "\n" 
                              + "생산량 : " + string.Format("{0:#,##0}", MachineStatusList[i].OkQty);
                }
                
                MachineList[i].Text = LabelText;
                MachineList[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                //MachineList[i].AutoEllipsis = true;

                //var value = i % 8;
                //var RowCNT = (ListCNT / Xcnt) + 1;
                var ColCalculation = (i / Xcnt) * Xcnt;
                var RowCalculation = i / Xcnt;

                //아래 i 범위 주석 참고...
                Xpoint = X + (width + xvoid) * (i - ColCalculation);
                Ypoint = Y + (height + yvoid) * RowCalculation;
                MachineList[i].Location = new Point(Xpoint, Ypoint);

                #region 라벨 위치 지정 노가다, 사용 안함
                /*
                if (0 <= i && i <= 7)
                {
                    Xpoint = X + (width + xvoid) * (i-0);
                    Ypoint = Y + (height + yvoid) * 0;
                    MachineList[i].Location = new Point(Xpoint, Ypoint);
                }
                else if (8 <= i && i <= 15)
                {
                    Xpoint = X + (width + xvoid) * (i-8);
                    Ypoint = Y + (height + yvoid) * 1;
                    MachineList[i].Location = new Point(Xpoint, Ypoint);
                }
                else if (16 <= i && i <= 23)
                {
                    Xpoint = X + (width + xvoid) * (i - 16);
                    Ypoint = Y + (height + yvoid) * 2;
                    MachineList[i].Location = new Point(Xpoint, Ypoint);
                }
                else if (24 <= i && i <= 31)
                {
                    Xpoint = X + (width + xvoid) * (i-24);
                    Ypoint = Y + (height + yvoid) * 3;
                    MachineList[i].Location = new Point(Xpoint, Ypoint);
                }
                else if (32 <= i && i <= 39)
                {
                    Xpoint = X + (width + xvoid) * (i-32);
                    Ypoint = Y + (height + yvoid) * 4;
                    MachineList[i].Location = new Point(Xpoint, Ypoint);
                }
                else if (40 <= i && i <= 47)
                {
                    var test = (i / 8) * 8;
                    var test2 = i / 8;

                    Xpoint = X + (width + xvoid) * (i-40);
                    Ypoint = Y + (height + yvoid) * 5;
                    MachineList[i].Location = new Point(Xpoint, Ypoint);
                }
                else if (48 <= i && i <= 55)
                {
                    var test = (i / 8) * 8;

                    Xpoint = X + (width + xvoid) * (i-48);
                    Ypoint = Y + (height + yvoid) * 6;
                    MachineList[i].Location = new Point(Xpoint, Ypoint);
                }*/
                #endregion


            }

        }
        
        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        private void _FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();
        }


    }
}
