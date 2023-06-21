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

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 20210603 오세완 차장
    /// 비가동 사유가 QC확인이면 작업지시번호를 저장하여 POP에서 그걸 확인하게 하고 또한 비가동을 풀어줄 때 입력여부를 확인할 수 있게 하는 버전
    /// </summary>
    public partial class XPFMACHINESTOP_V2_RUS : Service.Base.BaseForm
    {
        #region 전역변수
        IService<TN_MEA1004> ModelService = (IService<TN_MEA1004>)ProductionFactory.GetDomainService("TN_MEA1004");
        Control[] btn_StopTypeList;

        /// <summary>
        /// 20210603 오세완 차장 적입지시정보
        /// </summary>
        private TEMP_XFPOP1000 workObj;

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

        public XPFMACHINESTOP_V2_RUS(TEMP_XFPOP1000 tempObj)
        {
            InitializeComponent();

            lup_MachineGroup.EditValueChanged += Lup_MachineGroup_EditValueChanged;
            lup_Machine.Popup += Lup_Machine_Popup;
            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;

            btn_Cancel.Click += Btn_Cancel_Click;

            this.Text = LabelConvert.GetLabelText("StopAdd");

            //this.Size = new Size(517, 551);
            this.WindowState = FormWindowState.Maximized;
            workObj = tempObj;
        }

        public XPFMACHINESTOP_V2_RUS(TEMP_XFPOP1000 tempObj, bool bPLC)
        {
            InitializeComponent();

            lup_MachineGroup.EditValueChanged += Lup_MachineGroup_EditValueChanged;
            lup_Machine.Popup += Lup_Machine_Popup;
            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;

            btn_Cancel.Click += Btn_Cancel_Click;

            this.Text = LabelConvert.GetLabelText("StopAdd");

            this.WindowState = FormWindowState.Maximized;
            workObj = tempObj;
            gb_PLC_Pop = bPLC;
        }

        protected override void InitControls()
        {
            base.InitControls(); //없애면 안됨.

            var TN_STD1000_List = DbRequestHandler.GetCommTopCode(MasterCodeSTR.StopType);

            var cultureIndex = DataConvert.GetCultureIndex();

            btn_StopTypeList = new Control[TN_STD1000_List.Count];
            int j = TN_STD1000_List.Count / 4 + 1;
            int k = 280 / j > 70 ? 70 : 280 / j;

            for (int i = 0; i < TN_STD1000_List.Count; i++)
            {
                btn_StopTypeList[i] = new SimpleButton();
                btn_StopTypeList[i].Name = TN_STD1000_List[i].CodeVal;
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
                btn_StopTypeList[i].Text = cultureIndex == 1 ? TN_STD1000_List[i].CodeName : (cultureIndex == 2 ? TN_STD1000_List[i].CodeNameENG : TN_STD1000_List[i].CodeNameCHN);
                btn_StopTypeList[i].Font = new Font("맑은 고딕", 15f, FontStyle.Bold);
                btn_StopTypeList[i].Click += new EventHandler(Btn_StopTypeClick);
            }

            SetBtnStopTypeEnable(false);
        }
        
        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            lup_MachineGroup.SetDefaultPOP(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_machinecode = new SqlParameter("@MACHINE_MCODE", "");
                SqlParameter sp_stopcode = new SqlParameter("@MACHINE_RUN_STOP_CODE", MasterCodeSTR.Machine_RunStopCode);
                var vResult = context.Database.SqlQuery<TEMP_XPFMACHINESTOP_MACHINE_STATUS>("USP_GET_XPFMACHINESTOP_MACHINE_STATE @MACHINE_MCODE, @MACHINE_RUN_STOP_CODE", 
                    sp_machinecode, sp_stopcode).ToList();

                if (vResult != null)
                {
                    lup_Machine.SetDefaultPOP(false, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), vResult, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
                    lup_Machine.Properties.View.Columns["MachineMCode"].Visible = false;
                    lup_Machine.AddColumnDisplay(DataConvert.GetCultureDataFieldName("STATUS_NAME"), LabelConvert.GetLabelText("StopInfo"), 3, true);

                    //lup_Machine.AddColumnDisplay("RESTART_TO_FLAG_NAME", LabelConvert.GetLabelText("MachineStopToRun"), 4, true);
                    // 20210609 오세완 차장 이 컬럼을 보고 작업자가 비가동을 풀수 있는지를 확인하는 곳이다. 
                    lup_Machine.AddColumnDisplay("STOP_TO_RUN_STATUS", LabelConvert.GetLabelText("MachineStopToRun"), 4, true);
                }
            }

            dt_StopStartDate.ReadOnly = true;
            dt_StopStartDate.SetFormat(Utils.Enum.DateFormat.DateAndTimeSecond);
            dt_StopStartDate.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
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
                                                         (p.StopCode == null || p.StopCode == "") );
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
                        if(gb_PLC_Pop)
                            SetBtnStopTypeEnable(true); // 20210914 오세완 차장 프레스 공정은 사용자가 비가동 유형을 설정하게 변경
                        else
                            SetBtnStopTypeEnable(false);

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
            
            var newObj = new TN_MEA1004()
            {
                MachineCode = machineCode,
                StopCode = control.Name,
                StopStartTime = DateTime.Now,
            };

            ModelService.Insert(newObj);
            sRunning = "STOP";
            sMachinecode = newObj.MachineCode;
            bExeccute = true;
            

            ModelService.Save();

            if(bExeccute)
                Update_Mps1200_Jobstate(sRunning, sMachinecode);

            DialogResult = System.Windows.Forms.DialogResult.OK;

            ActClose();
        }

        /// <summary>
        /// 20210627 오세완 차장
        /// 작업지시에 설비가 설정된 상태를 비가동 상태로 바꾸는 프로시저 실행
        /// 20210628 오세완 차장 
        /// 설비코드로 전체 작업지시의 상태를 바꾸는 형태로 변경
        /// </summary>
        private void Update_Mps1200_Jobstate(string Running, string Machinecode)
        {
            string sSql = "exec USP_UPD_XPFMACHINESTOP_V2 '" + Running + "', '" + Machinecode + "'";
            DbRequestHandler.SetDataQury(sSql);
        }

        private void SetBtnStopTypeEnable(bool enable)
        {
            //foreach (var v in btn_StopTypeList)
            //    v.Enabled = enable;
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
            if (lookup == null) return;

            lup_Machine.EditValue = null;
        }

        private void Lup_Machine_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            try
            {
                WaitHandler.ShowWait();
                var machineGroupCode = lup_MachineGroup.EditValue.GetNullToNull();

                if (machineGroupCode.IsNullOrEmpty())
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
                else
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[MachineGroupCode] = '" + machineGroupCode + "'";
            }
            finally
            {
                WaitHandler.CloseWait();
            }
        }

    }
}
