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
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.XtraReports.UI;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using DevExpress.XtraLayout;
using DevExpress.Utils;
using HKInc.Utils.Common;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.POP_POPUP
{
    /// <summary>
    /// 20220310 오세완 차장
    /// 고도화로 대영 스타일 화면 사용
    /// </summary>
    public partial class XPFMACHINESTOP_V2_RUS : Service.Base.BaseForm
    {
        #region 전역변수
        // 20220404 오세완 차장 고도화로 인하여 다른 테이블 사용
        //IService<TN_MPS1600> ModelService = (IService<TN_MPS1600>)ProductionFactory.GetDomainService("TN_MPS1600");
        IService<TN_MEA1004> ModelService = (IService<TN_MEA1004>)ProductionFactory.GetDomainService("TN_MEA1004");
        Control[] btn_StopTypeList;

        /// <summary>
        /// 20220310 오세완 차장 적입지시정보
        /// </summary>
        private TP_XFPOP1000_V2_LIST workObj;

        /// <summary>
        /// 20210627 오세완 차장 작업지시상태 변경 프로시저 실행 여부 판단
        /// </summary>
        private bool bExeccute = false;

        /// <summary>
        /// 20210914 오세완 차장 PLC와 인터페이스 하는 pop는 비가동을 update하는 방식을 취하기 위함
        /// </summary>
        private bool gb_PLC_Pop = false;

        /// <summary>
        /// 20210915 오세완 차장 비가동을 한건지 풀어준건지 확인
        /// </summary>
        public string gs_Status = string.Empty;

        /// <summary>
        /// 20210915 오세완 차장 비가동여부 확인
        /// Run - 비가동을 출어줌, STOP - 비가동
        /// </summary>
        public string STATUS
        {
            get
            {
                return gs_Status;
            }
        }
        #endregion

        public XPFMACHINESTOP_V2_RUS(TP_XFPOP1000_V2_LIST tempObj)
        {
            InitializeComponent();

            lup_Machine.EditValue = tempObj.MachineCode;                // 비가동팝업 실행시 자동으로 작업지시의 설비명 불러옴     2022-07-15 김진우   
            

            lup_MachineGroup.EditValueChanged += Lup_MachineGroup_EditValueChanged;
            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;

            btn_Cancel.Click += Btn_Cancel_Click;

            //this.Text = LabelConvert.GetLabelText("StopAdd");
            this.Text = "нерабочий регистрация";      // 비가동등록

            //this.Size = new Size(517, 551);
            this.WindowState = FormWindowState.Maximized;
            workObj = tempObj;

            
        }

        // 미사용 주석
        public XPFMACHINESTOP_V2_RUS(TP_XFPOP1000_V2_LIST tempObj, bool bPLC)
        {
            //InitializeComponent();

            //lup_MachineGroup.EditValueChanged += Lup_MachineGroup_EditValueChanged;
            //lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;

            //btn_Cancel.Click += Btn_Cancel_Click;

            //this.Text = LabelConvert.GetLabelText("StopAdd");

            //this.WindowState = FormWindowState.Maximized;
            //workObj = tempObj;
            //gb_PLC_Pop = bPLC;
        }

        protected override void InitControls()
        {
            base.InitControls(); //없애면 안됨.

            List<TN_STD1000> stop_Arr = DbRequestHandler.GetCommCode(MasterCodeSTR.STOPTYPE_RUS);

            if(stop_Arr != null)
                if(stop_Arr.Count > 0)
                {
                    btn_StopTypeList = new Control[stop_Arr.Count];

                    int j = stop_Arr.Count / 4 + 1;
                    int k = 280 / j > 70 ? 70 : 280 / j;

                    for (int i = 0; i < stop_Arr.Count; i++)
                    {
                        btn_StopTypeList[i] = new SimpleButton();
                        btn_StopTypeList[i].Name = stop_Arr[i].Mcode;
                        btn_StopTypeList[i].Parent = this.panel_Button;

                        var value = i / 4;
                        var height1 = ((i / 4 * k) + (value * 10) + 30);

                        switch (i % 4)
                        {
                            case 0:
                                btn_StopTypeList[i].Location = new Point(30, height1);
                                break;
                            case 1:
                                btn_StopTypeList[i].Location = new Point(200, height1);
                                break;
                            case 2:
                                btn_StopTypeList[i].Location = new Point(370, height1);
                                break;
                            case 3:
                                btn_StopTypeList[i].Location = new Point(540, height1);
                                break;
                        }

                        btn_StopTypeList[i].Size = new Size(150, k);
                        btn_StopTypeList[i].Text = stop_Arr[i].Codename;
                        btn_StopTypeList[i].Font = new Font("맑은 고딕", 15f, FontStyle.Bold);
                        btn_StopTypeList[i].Click += new EventHandler(Btn_StopTypeClick);
                    }

                    SetBtnStopTypeEnable(false);
                }
        }
        
        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            List<TN_MEA1000> tempList = ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList();
            lup_Machine.SetDefaultPOP(false, "MachineCode", "MachineName", tempList, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            dt_StopStartDate.ReadOnly = true;
            dt_StopStartDate.SetFormat(Utils.Enum.DateFormat.DateAndTimeSecond);
            dt_StopStartDate.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            SetValue();
            Load_Process();     // 화면 로드시 설비가 선택되어있는경우 기능활성화        2022-07-21 김진우 추가
        }

        /// <summary>
        /// 20220509 오세완 차장
        /// 비가동으로 선택한 경우 풀어줄 수 있게 설정이 필요
        /// </summary>
        private void SetValue()
        {
            if(workObj != null)
            {
                if(workObj.JobStatus == "35")
                {
                    if(workObj.MachineCode.GetNullToEmpty() != "")
                    {
                        TN_MEA1004 stop_obj = ModelService.GetList(p => p.MachineCode == workObj.MachineCode &&
                                                                        p.StopCode != "S08" &&
                                                                        p.StopEndTime == null).FirstOrDefault();

                        if(stop_obj != null)
                        {
                            lup_Machine.EditValue = workObj.MachineCode;
                            dt_StopStartDate.EditValue = (DateTime)stop_obj.StopStartTime;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 설비 변경 이벤트
        /// </summary>
        private void Lup_Machine_EditValueChanged(object sender, EventArgs e)
        {
            var edit = sender as SearchLookUpEdit;

            var value = edit.EditValue.GetNullToEmpty();
            if (value.IsNullOrEmpty())
            {
                SetBtnStopTypeEnable(false);
                dt_StopStartDate.EditValue = null;
            }
            else
            {
                bool bStart = false;
                List<TN_MEA1004> tempList = null;
                if(gb_PLC_Pop)
                {
                    tempList = ModelService.GetList(p => p.MachineCode == value && 
                                                         p.StopStartTime != null && 
                                                         p.StopEndTime == null &&
                                                         (p.StopCode == null || p.StopCode != "")); // 20211006 오세완 차장 비가동을 사유를 선택하기 때문에 해당 조건이 필요
                }
                else
                {
                    tempList = ModelService.GetList(p => p.MachineCode == value && 
                                                         p.StopStartTime != null && 
                                                         p.StopEndTime == null);
                }

                if (tempList == null)
                    bStart = true;
                else if (tempList.Count == 0)
                    bStart = true;

                if(bStart)
                {
                    SetBtnStopTypeEnable(true);
                    dt_StopStartDate.EditValue = null;
                }
                else
                {
                    TN_MEA1004 stopObj = tempList.OrderByDescending(p => p.StopStartTime).FirstOrDefault();
                    if(stopObj != null)
                    {
                        SetBtnStopTypeEnable(true, stopObj.StopCode);
                        dt_StopStartDate.EditValue = stopObj.StopStartTime;
                    }
                }

            }
        }

        /// <summary>
        /// 화면 로드시 설비가 선택되어있는 경우 버튼 활성화 및 작업이 가능하도록 기능 추가
        /// 2022-07-21 김진우
        /// </summary>
        private void Load_Process()
        {
            var value = lup_Machine.EditValue;
            if (value.IsNullOrEmpty())
            {
                SetBtnStopTypeEnable(false);
                dt_StopStartDate.EditValue = null;
            }
            else
            {
                bool bStart = false;
                List<TN_MEA1004> tempList = null;
                if (gb_PLC_Pop)
                {
                    tempList = ModelService.GetList(p => p.MachineCode == value &&
                                                         p.StopStartTime != null &&
                                                         p.StopEndTime == null &&
                                                         (p.StopCode == null || p.StopCode != "")); // 20211006 오세완 차장 비가동을 사유를 선택하기 때문에 해당 조건이 필요
                }
                else
                {
                    tempList = ModelService.GetList(p => p.MachineCode == value &&
                                                         p.StopStartTime != null &&
                                                         p.StopEndTime == null);
                }

                if (tempList == null)
                    bStart = true;
                else if (tempList.Count == 0)
                    bStart = true;

                if (bStart)
                {
                    SetBtnStopTypeEnable(true);
                    dt_StopStartDate.EditValue = null;
                }
                else
                {
                    TN_MEA1004 stopObj = tempList.OrderByDescending(p => p.StopStartTime).FirstOrDefault();
                    if (stopObj != null)
                    {
                        SetBtnStopTypeEnable(true, stopObj.StopCode);
                        dt_StopStartDate.EditValue = stopObj.StopStartTime;
                    }
                }
            }
        }

        /// <summary>
        /// 취소 클릭 이벤트
        /// </summary>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 비가동유형 선택
        /// </summary>
        private void Btn_StopTypeClick(object sender, EventArgs e)
        {
            var control = sender as Control;

            var machineCode = lup_Machine.EditValue.GetNullToEmpty();

            var stopList = ModelService.GetList(p => p.MachineCode == machineCode).ToList();

            string sRunning = "";
            string sMachinecode = "";
            if (dt_StopStartDate.EditValue != null)
            {
                string sMessage = "";
                var updateObj = stopList.Where(p => p.StopStartTime != null && 
                                                    p.StopEndTime == null).FirstOrDefault();
                bool bUpdate = false;
                if(updateObj != null)
                {
                    if (gb_PLC_Pop)
                        updateObj.StopCode = control.Name;

                    bUpdate = true;
                }
                
                if(bUpdate)
                {
                    updateObj.StopEndTime = DateTime.Now;
                    ModelService.Update(updateObj);
                    sRunning = "Run";
                    sMachinecode = updateObj.MachineCode;
                }
                
            }
            else
            {
                TN_MEA1004 newObj = new TN_MEA1004()
                {
                    MachineCode = machineCode,
                    StopCode = control.Name,
                    StopStartTime = DateTime.Now,
                    WorkNo = workObj.WorkNo,
                    Process = workObj.Process
                };

                ModelService.Insert(newObj);
                sRunning = "STOP";
                sMachinecode = newObj.MachineCode;
            }

            ModelService.Save();

            Update_Mps1200_Jobstate(sRunning, sMachinecode);

            DialogResult = System.Windows.Forms.DialogResult.OK;

            // 20210915 오세완 차장 프레스 공정인 경우 비가동해제, 비가동처리에 따라서 작업선택을 정지해야 해서 값을 반환처리
            if(gb_PLC_Pop)
            {
                gs_Status = sRunning;
            }

            ActClose();
        }

        /// <summary>
        /// 20220310 오세완 차장
        /// 비가동시킨 경우 작업까지 영향을 주게 할건지 결정이 안되서 잠시 보류
        /// 20220509 오세완 차장
        /// 수동으로 하는 경우는 작업지시 변환할 수 있게 기능 사용처리
        /// </summary>
        private void Update_Mps1200_Jobstate(string Running, string Machinecode)
        {
            string sSql = "exec USP_UPD_XPFMACHINESTOP_V2 '" + Running + "', '" + Machinecode + "'";
            DbRequestHandler.SetDataQury(sSql);
        }

        private void SetBtnStopTypeEnable(bool enable)
        {
            foreach (var v in btn_StopTypeList)
                v.Enabled = enable;
        }

        private void SetBtnStopTypeEnable(bool enable, string stopType)
        {
            var obj = btn_StopTypeList.Where(p => p.Name == stopType).FirstOrDefault();
            if (obj != null)
                obj.Enabled = enable;
        }

        private void Lup_MachineGroup_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null)
                return;

            lup_Machine.EditValue = null;
        }
    }
}
