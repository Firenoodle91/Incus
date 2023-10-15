using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Forms;
using System.Reflection;

namespace HKInc.Service.Controls
{
    public partial class PLC_Interface_Modbus : DevExpress.XtraEditors.XtraUserControl
    {
        #region 전역변수
        /// <summary>
        /// 20210712 오세완 차장 ini파일 중 TIMER_PERIOD 숫자
        /// </summary>
        private int gi_Timer = 1000;

        /// <summary>
        /// 20210712 오세완 차장 실적수량
        /// </summary>
        private int gi_Count = 0;

        /// <summary>
        /// 20210712 오세완 차장 ini파일 중 PLCINFO 숫자
        /// </summary>
        private int gi_Update = 30000;

        /// <summary>
        /// 20210712 오세완 차장 ini파일 중 IP 문자
        /// </summary>
        private string gs_IpAddress = "10.10.0.11";

        /// <summary>
        /// 20210712 오세완 차장 ini파일 중 PORT숫자
        /// </summary>
        private int gi_PortNumber = 502;

        /// <summary>
        /// 20210712 오세완 차장 ini파일 중 MACHINE 문자
        /// </summary>
        private string gs_MachineMCode = "MC-00001";

        public int TimerPeroid
        {
            get
            {
                return gi_Timer;
            }

            set
            {
                gi_Timer = value;
            }
        }

        /// <summary>
        /// 20210712 오세완 차장
        /// 생산 실적은 값을 설정할 수가 없다. 
        /// </summary>
        public int PlcCount
        {
            get
            {
                return gi_Count;
            }
        }

        public int UpdatePeriod
        {
            get
            {
                return gi_Update;
            }

            set
            {
                gi_Update = value;
            }
        }

        /// <summary>
        /// 20210712 오세완 차장 
        /// PLC로부터 실적수량을 읽어올 count
        /// 20210714 오세완 차장 System.Timers.Timer -> Threading.Timer로 교체, ui freezing현상 때문, static도 소용없음 한번만 호출됨
        /// </summary>
        private System.Threading.Timer gTimer;

        /// <summary>
        /// 20210712 오세완 차장 log파일 기록
        /// </summary>
        private static readonly log4net.ILog log_Plc = log4net.LogManager.GetLogger(typeof(PLC_Interface_Modbus));

        /// <summary>
        /// 20210713 오세완 차장 통신이 종료된 경우 log가 계속 쌓여서 어느정도만 하게 하기 위해 설정
        /// </summary>
        private ushort gu_FailCnt = 0;

        private enum eConnectStatus { Init = 0, Connect = 1, Disconncet =2};

        /// <summary>
        /// 20210712 오세완 차장 
        /// 연결상태 확인
        /// </summary>
        private eConnectStatus ge_ConnectStatus = eConnectStatus.Init;

        /// <summary>
        /// 20210712 오세완 차장 연결상태 문자 반환
        /// </summary>
        public string ConnectStatus
        {
            get
            {
                if (ge_ConnectStatus == eConnectStatus.Init)
                    return "Init";
                else if (ge_ConnectStatus == eConnectStatus.Connect)
                    return "Connect";
                else
                    return "Disconnect";
            }
        }

        private int gi_InternalCnt = 0;

        /// <summary>
        /// 20210715 오세완 차장
        /// update주기를 맞춰놓은 시간때에 호출되게 함
        /// </summary>
        public event EventHandler OnUpdate;

        /// <summary>
        /// 20210715 오세완 차장 
        /// 연결 정보가 변경이 될 때 POP에 전달하기 위함
        /// </summary>
        public event EventHandler OnChangeConnection;

        /// <summary>
        /// 20210715 오세완 차장
        /// 연결정보 한번만 일으키키 위한 수단
        /// </summary>
        private bool bDoConnectionEvent = false;

        /// <summary>
        /// 20210715 오세완 차장
        /// 설정한 설비관리코드
        /// </summary>
        public string MachineMCode
        {
            get
            {
                return this.gs_MachineMCode;
            }
        }

        public event EventHandler OnDoubleClick;

        #endregion
        public PLC_Interface_Modbus()
        {
            InitializeComponent();
            Init_Value();
        }

        /// <summary>
        /// 20210714 오세완 차장 control 초기값 설정
        /// </summary>
        public void Init_Value()
        {
            tx_DB_Period.EditValue = "0";
            tx_PLC_Count.EditValue = "0";
            tx_TimerPeriod.EditValue = "0";

            // 통신상태 색상 표현 
            // 20210714 오세완 차장 thread때문에 Layoutcontrolitem에는 invoke가 없어서 설정 불가
            //layoutControlItem2.AppearanceItemCaption.ForeColor = Color.Gray;

            Read_IniFile();

            gTimer = new System.Threading.Timer(CallBack, this, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            //timer1.Interval = gi_Count;
        }

        /// <summary>
        /// 20210714 오세완 차장
        /// 장비 통신 시작 및 화면 출력
        /// </summary>
        public void Start()
        {
            gTimer.Change(0, gi_Timer);
            //timer1.Start();
        }

        public void Reset()
        {
            Reset_Count();
        }

        /// <summary>
        /// 20210712 오세완 차장 ini파일에서 설정값 읽기
        /// </summary>
        private void Read_IniFile()
        {
            string sIPAddress = IIniFileControl.ReadIniFile("PLCINFO", "IP");
            System.Net.IPAddress tempIp;
            bool bCheckIP = System.Net.IPAddress.TryParse(sIPAddress, out tempIp);
            if (bCheckIP)
                gs_IpAddress = tempIp.ToString();

            string sPort = IIniFileControl.ReadIniFile("PLCINFO", "PORT");
            int iTemp_port;
            bool bCheckPort = int.TryParse(sPort, out iTemp_port);
            if (bCheckPort)
                gi_PortNumber = iTemp_port;

            gs_MachineMCode = IIniFileControl.ReadIniFile("PLCINFO", "MACHINE");

            string sTimerPeroid = IIniFileControl.ReadIniFile("PLCINFO", "TIMER_PERIOD");
            int iTemp_timer;
            bool bCheckTimer = int.TryParse(sTimerPeroid, out iTemp_timer);
            if (bCheckTimer)
                gi_Timer = iTemp_timer;

            string sUpdatePeriod = IIniFileControl.ReadIniFile("PLCINFO", "UPDATE_PERIOD");
            int iTemp_update;
            bool bCheckUpdate = int.TryParse(sUpdatePeriod, out iTemp_update);
            if (bCheckUpdate)
                gi_Update = iTemp_update;

            string sMessage = "IP : " + gs_IpAddress + ", Port : " + gi_PortNumber.ToString() + ", Machine : " + gs_MachineMCode + ", Timer : " + 
                gi_Timer.ToString() + ", Update : " + gi_Update.ToString();
            log_Plc.Info(sMessage);
        }

        /// <summary>
        /// 20210714 오세완 차장 timer tick과 비슷한 동작 실행
        /// </summary>
        /// <param name="oState"></param>
        private void CallBack(object oState)
        {
            Recive_Count();

            gi_InternalCnt++;
            if (gi_InternalCnt % 5 == 0)
            {
                // 타이머가 1초로 동작한다고 가정하였을 때 5초 주기로 화면 출력
                //DisplayValue();
                //DisplayValue_V2();
                DisplayValue_V3();
                DisplayValue_Update_V3(""); // 20210909 오세완 차장 연결상태 출력을 위해 공란으로 출력
            }

            if (gi_InternalCnt % 10 == 0)
            {
                // 타이머가 1초로 동작한다고 가정하였을 때 10초 주기로 로그 기록
                string sMessage = "Counter : " + gi_Count.ToString();
                log_Plc.Info(sMessage);
            }

            if(gi_InternalCnt % 2 == 0)
            {
                // 20210726 오세완 차장 update나 reset을 수신하였을 때 상태창을 지우기 위해서 사용
                //DisplayValue_Update("");
                //DisplayValue_Update_V2("");
                DisplayValue_Update_V3("");
            }

            if (gi_InternalCnt * gi_Timer > gi_Update)
            {
                gi_InternalCnt = 0;
                if(OnUpdate != null)
                {
                    // 20210721 오세완 차장 실적정보가 갱신된다는 것을 시각화 표시하기 위해 추가처리
                    //DisplayValue_Update("실적 정보 갱신");
                    //DisplayValue_Update_V2("실적 정보 갱신");
                    DisplayValue_Update_V3("실적 정보 갱신");

                    // 20210715 오세완 차장 db에 update할 수 있게 pop에 전달
                    this.OnUpdate(this, new EventArgs());

                    // 20210726 오세완 차장 여기 있으면 너무 빨라서 상태 변화가 보이지 않음
                    //DisplayValue_Update("");
                }
            }
        }

        /// <summary>
        /// 20210714 오세완 차장 실제로 PLC와 연결하여 MODBUS통신으로 실적 count를 가져오는 부분
        /// </summary>
        private void Recive_Count()
        {
            try
            {
                using (System.Net.Sockets.TcpClient tc1 = new System.Net.Sockets.TcpClient(gs_IpAddress, gi_PortNumber))
                {
                    ushort startAddress = 0;
                    Modbus.Device.ModbusIpMaster master = Modbus.Device.ModbusIpMaster.CreateIp(tc1);
                    if (master != null)
                    {
                        gi_Count = master.ReadInputRegisters_int(startAddress);

                        // 20210719 오세완 차장 값을 정상적으로 받아온 경우에만 연결상태로 변경해주기
                        if (ge_ConnectStatus != eConnectStatus.Connect)
                            bDoConnectionEvent = true;

                        ge_ConnectStatus = eConnectStatus.Connect;
                    }
                    else
                    {
                        log_Plc.Warn("실적 카운트 못받아옴");
                    }
                }
            }
            catch (System.ComponentModel.Win32Exception ex_win32)
            {
                if (ge_ConnectStatus != eConnectStatus.Disconncet)
                    bDoConnectionEvent = true;

                ge_ConnectStatus = eConnectStatus.Disconncet;
                if(gu_FailCnt < 100)
                {
                    log_Plc.Error($"An exception occurred from {MethodBase.GetCurrentMethod().Name}", ex_win32);
                    gu_FailCnt++;
                }
                
            }
            catch (Exception ex)
            {
                if (ge_ConnectStatus != eConnectStatus.Disconncet)
                    bDoConnectionEvent = true;

                ge_ConnectStatus = eConnectStatus.Disconncet;
                if(gu_FailCnt < 100)
                {
                    log_Plc.Error($"An exception occurred from {MethodBase.GetCurrentMethod().Name}", ex);
                    gu_FailCnt++;
                }
            }
            finally
            {
                CheckConnectEvent();
            }
        }

        /// <summary>
        /// 20210715 오세완 차장 카운트 리셋
        /// </summary>
        public void Reset_Count()
        {
            try
            {
                using (System.Net.Sockets.TcpClient tc1 = new System.Net.Sockets.TcpClient(gs_IpAddress, gi_PortNumber))
                {
                    ushort startAddress = 0;
                    Modbus.Device.ModbusIpMaster master = Modbus.Device.ModbusIpMaster.CreateIp(tc1);
                    if (master != null)
                    {
                        master.WriteSingleCoil(startAddress, true);

                        // 20210719 오세완 차장 receive_count와 동일하게 처리
                        if (ge_ConnectStatus != eConnectStatus.Connect)
                            bDoConnectionEvent = true;

                        ge_ConnectStatus = eConnectStatus.Connect;

                        // 20210726 오세완 차장 plc에 reset 신호를 보냈다는 표시 내기
                        //DisplayValue_Update("초기화 신호 송신");
                        //DisplayValue_Update_V2("초기화 신호 송신");
                        DisplayValue_Update_V3("초기화 신호 송신");
                    }
                    else
                    {
                        log_Plc.Warn("리셋하기 전에 연결 실패");
                    }
                }
            }
            catch (System.ComponentModel.Win32Exception ex_win32)
            {
                if (ge_ConnectStatus != eConnectStatus.Disconncet)
                    bDoConnectionEvent = true;

                ge_ConnectStatus = eConnectStatus.Disconncet;
                if (gu_FailCnt < 100)
                {
                    log_Plc.Error($"An exception occurred from {MethodBase.GetCurrentMethod().Name}", ex_win32);
                    gu_FailCnt++;
                }

            }
            catch (Exception ex)
            {
                if (ge_ConnectStatus != eConnectStatus.Disconncet)
                    bDoConnectionEvent = true;

                ge_ConnectStatus = eConnectStatus.Disconncet;
                if (gu_FailCnt < 100)
                {
                    log_Plc.Error($"An exception occurred from {MethodBase.GetCurrentMethod().Name}", ex);
                    gu_FailCnt++;
                }
            }
            finally
            {
                CheckConnectEvent();
            }
        }

        /// <summary>
        /// 20210715 오세완 차장 연결상태 변경을 pop에 전달
        /// </summary>
        private void CheckConnectEvent()
        {
            if(bDoConnectionEvent)
            {
                if (this.OnChangeConnection != null)
                {
                    this.OnChangeConnection(this, new EventArgs());
                    bDoConnectionEvent = false;
                }
            }
        }

        /// <summary>
        /// 20210712 오세완 차장 화면 출력
        /// </summary>
        private void DisplayValue()
        {
            tx_DB_Period.BeginInvoke(new MethodInvoker(delegate () {
                tx_DB_Period.EditValue = gi_Update.ToString();
            }));

            tx_PLC_Count.BeginInvoke(new MethodInvoker(delegate () {
                tx_PLC_Count.EditValue = gi_Count.ToString();
            }));

            tx_TimerPeriod.BeginInvoke(new MethodInvoker(delegate () {
                tx_TimerPeriod.EditValue = gi_Timer.ToString();
            }));

            Color cTemp = new Color();
            switch(ge_ConnectStatus)
            {
                case eConnectStatus.Connect:
                    cTemp = Color.LightGreen;
                    break;

                case eConnectStatus.Disconncet:
                    cTemp = Color.PaleVioletRed;
                    break;

                default:
                    cTemp = Color.LightGray;
                    break;
            }

            tx_Status.BeginInvoke(new MethodInvoker(delegate () {
                tx_Status.BackColor = cTemp;
            }));

            //layoutControlItem2.AppearanceItemCaption.ForeColor = cTemp;
        }

        /// <summary>
        /// 20210829 오세완 차장 
        /// ver1이 화면이 닫을 때 디버깅에서 오류가 발생해서 이런 형식으로 변경처리
        /// </summary>
        private void DisplayValue_V2()
        {
            if(!tx_DB_Period.Disposing)
            {
                if(tx_DB_Period.InvokeRequired)
                {
                    tx_DB_Period.BeginInvoke(new MethodInvoker(delegate () {
                        tx_DB_Period.EditValue = gi_Update.ToString();
                    }));
                }
                else
                    tx_DB_Period.EditValue = gi_Update.ToString();

            }

            if(!tx_PLC_Count.Disposing)
            {
                if(tx_PLC_Count.InvokeRequired)
                {
                    tx_PLC_Count.BeginInvoke(new MethodInvoker(delegate () {
                        tx_PLC_Count.EditValue = gi_Count.ToString();
                    }));
                }
                else
                    tx_PLC_Count.EditValue = gi_Count.ToString();

            }

            if(!tx_TimerPeriod.Disposing)
            {
                if(tx_TimerPeriod.InvokeRequired)
                {
                    tx_TimerPeriod.BeginInvoke(new MethodInvoker(delegate () {
                        tx_TimerPeriod.EditValue = gi_Timer.ToString();
                    }));
                }
                else
                    tx_TimerPeriod.EditValue = gi_Timer.ToString();
            }

            Color cTemp = new Color();
            switch (ge_ConnectStatus)
            {
                case eConnectStatus.Connect:
                    cTemp = Color.LightGreen;
                    break;

                case eConnectStatus.Disconncet:
                    cTemp = Color.PaleVioletRed;
                    break;

                default:
                    cTemp = Color.LightGray;
                    break;
            }

            if(!tx_Status.Disposing)
            {
                if(tx_Status.InvokeRequired)
                {
                    tx_Status.BeginInvoke(new MethodInvoker(delegate () {
                        tx_Status.BackColor = cTemp;
                    }));
                }
                else
                    tx_Status.BackColor = cTemp;

            }
        }

        /// <summary>
        /// 20210909 오세완 차장 상태값 출력을 다른 곳으로 분리한 버전
        /// </summary>
        private void DisplayValue_V3()
        {
            if (!tx_DB_Period.Disposing)
            {
                if (tx_DB_Period.InvokeRequired)
                {
                    tx_DB_Period.BeginInvoke(new MethodInvoker(delegate () {
                        tx_DB_Period.EditValue = gi_Update.ToString();
                    }));
                }
                else
                    tx_DB_Period.EditValue = gi_Update.ToString();

            }

            if (!tx_PLC_Count.Disposing)
            {
                if (tx_PLC_Count.InvokeRequired)
                {
                    tx_PLC_Count.BeginInvoke(new MethodInvoker(delegate () {
                        tx_PLC_Count.EditValue = gi_Count.ToString();
                    }));
                }
                else
                    tx_PLC_Count.EditValue = gi_Count.ToString();

            }

            if (!tx_TimerPeriod.Disposing)
            {
                if (tx_TimerPeriod.InvokeRequired)
                {
                    tx_TimerPeriod.BeginInvoke(new MethodInvoker(delegate () {
                        tx_TimerPeriod.EditValue = gi_Timer.ToString();
                    }));
                }
                else
                    tx_TimerPeriod.EditValue = gi_Timer.ToString();
            }
        }

        private void DisplayValue_Update(string sMessage)
        {
            tx_Status.BeginInvoke(new MethodInvoker(delegate () {
                tx_Status.Text = sMessage;
            }));
        }

        /// <summary>
        /// 20210829 오세완 차장 
        /// ver1이 화면이 닫을 때 디버깅에서 오류가 발생해서 이런 형식으로 변경처리
        /// </summary>
        private void DisplayValue_Update_V2(string sMessage)
        {
            if(!tx_Status.Disposing)
            {
                if(tx_Status.InvokeRequired)
                {
                    tx_Status.BeginInvoke(new MethodInvoker(delegate () {
                        tx_Status.Text = sMessage;
                    }));
                }
                else
                    tx_Status.Text = sMessage;
            }
        }

        /// <summary>
        /// 20210909 오세완 차장 
        /// 동일한 control을 색상이나 값을 따로 설정하니 오류가 발생한 적이 1번 있어서 통합하여 처리
        /// </summary>
        /// <param name="sMessage"></param>
        private void DisplayValue_Update_V3(string sMessage)
        {
            Color cTemp = new Color();
            switch (ge_ConnectStatus)
            {
                case eConnectStatus.Connect:
                    cTemp = Color.LightGreen;
                    break;

                case eConnectStatus.Disconncet:
                    cTemp = Color.PaleVioletRed;
                    break;

                default:
                    cTemp = Color.LightGray;
                    break;
            }

            if (!tx_Status.Disposing)
            {
                if (tx_Status.InvokeRequired)
                {
                    tx_Status.BeginInvoke(new MethodInvoker(delegate () {
                        tx_Status.Text = sMessage;
                        tx_Status.BackColor = cTemp;
                    }));
                }
                else
                {
                    tx_Status.Text = sMessage;
                    tx_Status.BackColor = cTemp;
                }
            }
        }

        /// <summary>
        /// 20210722 오세완 차장 
        /// 통신 설정 파일 수정을 위해 더블클릭 이벤트 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupControl1_DoubleClick(object sender, EventArgs e)
        {
            if (OnDoubleClick != null)
                this.OnDoubleClick(sender, new EventArgs());
        }
    }
}
