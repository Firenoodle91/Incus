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
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;

namespace HKInc.Service.Controls
{
    /// <summary>
    /// 20210716 오세완 차장 PLC_Interface_Modbus와 다른 점은 여기서 직접 MEA1600에 값을 Update하는 것으로
    /// 20210723 오세완 차장 사용하지 않는 것으로
    /// </summary>
    public partial class PLC_Interface_Modbus_V2 : DevExpress.XtraEditors.XtraUserControl
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
        private static readonly log4net.ILog log_Plc = log4net.LogManager.GetLogger(typeof(PLC_Interface_Modbus_V2));

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

        /// <summary>
        /// 20210716 오세완 차장 entity가 thread safe하지 않아서 이쪽에서 직접 처리 가능하지 확인차
        /// </summary>
        IService<TN_MEA1600> ModelService = (IService<TN_MEA1600>)ProductionFactory.GetDomainService("TN_MEA1600");

        #endregion
        public PLC_Interface_Modbus_V2()
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

            Read_IniFile();

            gTimer = new System.Threading.Timer(CallBack, this, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// 20210714 오세완 차장
        /// 장비 통신 시작 및 화면 출력
        /// </summary>
        public void Start()
        {
            gTimer.Change(0, gi_Timer);
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
                DisplayValue();
            }

            if (gi_InternalCnt % 10 == 0)
            {
                // 타이머가 1초로 동작한다고 가정하였을 때 10초 주기로 로그 기록
                string sMessage = "Counter : " + gi_Count.ToString();
                log_Plc.Info(sMessage);
            }

            if (gi_InternalCnt * gi_Timer > gi_Update)
            {
                gi_InternalCnt = 0;
                Update_Count_Tnmea1600();
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
                        if (ge_ConnectStatus != eConnectStatus.Connect)
                            bDoConnectionEvent = true;

                        ge_ConnectStatus = eConnectStatus.Connect;
                        gi_Count = master.ReadInputRegisters_int(startAddress);
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
                Update_Connection_Tnmea1600();
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
                        if (ge_ConnectStatus != eConnectStatus.Connect)
                            bDoConnectionEvent = true;

                        ge_ConnectStatus = eConnectStatus.Connect;
                        master.WriteSingleCoil(startAddress, true);
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
                Update_Connection_Tnmea1600();
            }
        }

        /// <summary>
        /// 20210716 오세완 차장 직접 상태 변경
        /// lock이 없어서 v1에 lock을 넣지 않은 것과 동일한 오류가 발생한다. 
        /// </summary>
        private void Update_Connection_Tnmea1600()
        {
            try
            {
                if (bDoConnectionEvent)
                {
                    List<TN_MEA1600> plc_Arr = ModelService.GetListDetached(p => p.MachineCode == gs_MachineMCode).ToList();
                    TN_MEA1600 plc_Each = null;
                    if (plc_Arr != null)
                        if (plc_Arr.Count > 0)
                        {
                            plc_Each = plc_Arr.FirstOrDefault();
                        }

                    if (plc_Each != null)
                    {
                        plc_Each.Connection = this.ConnectStatus;
                        plc_Each.UpdateId = HKInc.Utils.Common.GlobalVariable.LoginId;
                        plc_Each.UpdateTime = DateTime.Now;
                        ModelService.Update(plc_Each);
                        ModelService.Save();
                    }

                    bDoConnectionEvent = false;
                }
            }
            catch(Exception ex)
            {
                if (gu_FailCnt < 100)
                {
                    log_Plc.Error($"An exception occurred from {MethodBase.GetCurrentMethod().Name}", ex);
                    gu_FailCnt++;
                }
            }
        }

        private void Update_Count_Tnmea1600()
        {
            try
            {
                int iResult_Productcnt = gi_Count;
                if (ge_ConnectStatus == eConnectStatus.Connect)
                {
                    List<TN_MEA1600> plc_Arr = ModelService.GetListDetached(p => p.MachineCode == gs_MachineMCode).ToList();
                    TN_MEA1600 plc_Each = null;
                    if (plc_Arr != null)
                        if (plc_Arr.Count > 0)
                        {
                            plc_Each = plc_Arr.FirstOrDefault();
                        }

                    int iPLC_Count = 0;
                    if (plc_Each != null)
                    {
                        if (plc_Each.Count != null)
                            iPLC_Count = (int)plc_Each.Count;
                    }

                    DateTime dtPrevTime;
                    DateTime dtNow = DateTime.Now;

                    if (plc_Each != null)
                    {
                        if (iPLC_Count == iResult_Productcnt)
                        {
                            // 실적이 장시간 안들어 온 경우 판단
                            if (plc_Each.CountTime == null && plc_Each.PrevCountTime == null)
                                plc_Each.RunStatus = "STOP";
                            else
                            {
                                dtPrevTime = (DateTime)plc_Each.CountTime;
                                TimeSpan tsDiff1 = dtNow - dtPrevTime;
                                if (tsDiff1.TotalMilliseconds > 600000)
                                    plc_Each.RunStatus = "STOP"; // 10분 이상 실적 없으면 비가동처리 
                            }
                        }
                        else
                        {
                            plc_Each.Count = iResult_Productcnt;
                            if (iResult_Productcnt == 1)
                            {
                                plc_Each.CountTime = DateTime.Now;
                                plc_Each.RunStatus = "RUN";
                            }
                            else if (iResult_Productcnt > 1)
                            {
                                if (plc_Each.CountTime != null)
                                {
                                    dtPrevTime = (DateTime)plc_Each.CountTime;
                                    plc_Each.PrevCountTime = dtPrevTime;
                                    plc_Each.CountTime = dtNow;
                                    plc_Each.RunStatus = "RUN";
                                }
                                else
                                {
                                    // 중간서 부터 시작한 경우에 대한 초기값 설정 처리
                                    plc_Each.CountTime = DateTime.Now;
                                    plc_Each.RunStatus = "RUN";
                                }
                            }
                        }

                        ModelService.Update(plc_Each);
                        ModelService.Save();
                    }
                }
            }
            catch(Exception ex)
            {
                if (gu_FailCnt < 100)
                {
                    log_Plc.Error($"An exception occurred from {MethodBase.GetCurrentMethod().Name}", ex);
                    gu_FailCnt++;
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
        }
    }
}
