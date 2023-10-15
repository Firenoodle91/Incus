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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using HKInc.Utils.Class;
using HKInc.Service.Helper;
using HKInc.Service.Service;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 20210722 오세완 차장 
    /// plc 정보 ini파일 입력 팝업
    /// </summary>
    public partial class XPFPLCINFO_V2 : DevExpress.XtraEditors.XtraForm
    {
        #region 전역변수 
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
        #endregion
        public XPFPLCINFO_V2()
        {
            InitializeComponent();
            this.Text = "PLC연결설정";
            Init_Event();
            InitValue();
        }

        /// <summary>
        /// 20210722 오세완 차장 IP정보 클릭이벤트 공통 정의
        /// </summary>
        private void Init_Event()
        {
            tx_Ipa.KeyDown += Tx_IpValue_KeyDown;
            tx_Ipb.KeyDown += Tx_IpValue_KeyDown;
            tx_Ipc.KeyDown += Tx_IpValue_KeyDown;
            tx_Ipd.KeyDown += Tx_IpValue_KeyDown;
            tx_Ipa.Click += Tx_IpValue_Click;
            tx_Ipb.Click += Tx_IpValue_Click;
            tx_Ipc.Click += Tx_IpValue_Click;
            tx_Ipd.Click += Tx_IpValue_Click;

            tx_Updateperiod.KeyDown += Tx_period_KeyDown;
            tx_Countperiod.KeyDown += Tx_period_KeyDown;
            tx_port.KeyDown += Tx_period_KeyDown;
            tx_Updateperiod.Click += Tx_period_Click;
            tx_Countperiod.Click += Tx_period_Click;
            tx_port.Click += Tx_period_Click;

            sb_Save.Click += Sb_Save_Click;
            sb_Cancel.Click += Sb_Cancel_Click;

            slup_Machine.Popup += Slup_Machine_Popup;
            slup_Machinegroup.EditValueChanged += Slup_Machinegroup_EditValueChanged;
        }

        private void Slup_Machinegroup_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null)
                return;

            slup_Machine.EditValue = null;
        }

        private void Slup_Machine_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null)
                return;

            string  sMachineGroupCode = slup_Machinegroup.EditValue == null ? "" : slup_Machinegroup.EditValue.ToString();

            if (sMachineGroupCode == "")
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            else
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[MachineGroupCode] = '" + sMachineGroupCode + "'";
        }

        /// <summary>
        /// 20210722 오세완 차장 취소 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sb_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// 20210722 오세완 차장 저장 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sb_Save_Click(object sender, EventArgs e)
        {
            string sMessage = "";
            bool bReturn = false;

            if (tx_port.Text == "")
            {
                //sMessage = "포트번호를 입력해주세요.";
                sMessage = string.Format(HelperFactory.GetStandardMessage().GetStandardMessage((int)(StandardMessageEnum.M_143)), layoutControlItem2.Text);
                bReturn = true;
            }
            else if (tx_Ipa.Text == "" || tx_Ipb.Text == "" || tx_Ipc.Text == "" || tx_Ipd.Text == "")
            {
                //sMessage = "IP를 입력해주세요.";
                sMessage = string.Format(HelperFactory.GetStandardMessage().GetStandardMessage((int)(StandardMessageEnum.M_143)), layoutControlItem1.Text);
                bReturn = true;
            }
            else if (slup_Machine.EditValue.GetNullToEmpty() == "")
            {
                sMessage = "설비를 선택해주세요.";
                bReturn = true;
            }
            else if (tx_Countperiod.EditValue.GetNullToEmpty() == "")
            {
                sMessage = "주기를 입력해주세요.";
                bReturn = true;
            }
            else if(tx_Updateperiod.EditValue.GetNullToEmpty() == "")
            {
                sMessage = "주기를 입력해 주세요.";
                bReturn = true;
            }

            if(bReturn)
            {
                MessageBoxHandler.Show(sMessage);
                return;
            }

            string sIpValue = tx_Ipa.Text + "." + tx_Ipb.Text + "." + tx_Ipc.Text + "." + tx_Ipd.Text;
            HKInc.Utils.Interface.Forms.IIniFileControl.WriteIniFile("PLCINFO", "IP", sIpValue);

            HKInc.Utils.Interface.Forms.IIniFileControl.WriteIniFile("PLCINFO", "PORT", tx_port.Text);

            string sMachineCode = slup_Machine.EditValue.ToString();
            if (sMachineCode == null)
                sMachineCode = "";

            HKInc.Utils.Interface.Forms.IIniFileControl.WriteIniFile("PLCINFO", "MACHINE", sMachineCode);

            string sCounterperiod = tx_Countperiod.EditValue.ToString();
            HKInc.Utils.Interface.Forms.IIniFileControl.WriteIniFile("PLCINFO", "TIMER_PERIOD", sCounterperiod);

            string sUpdateperiod = tx_Updateperiod.EditValue.ToString();
            HKInc.Utils.Interface.Forms.IIniFileControl.WriteIniFile("PLCINFO", "UPDATE_PERIOD", sUpdateperiod);

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 20210722 오세완 차장
        /// 실적 주기 정보 키패드 입력 팝업 이벤트 공통
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tx_period_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCNUMPAD keypad = new XFCNUMPAD();
            keypad.ShowDialog();

            TextEdit te_Obj = sender as TextEdit;
            int iValue;
            bool bCheck = int.TryParse(keypad.returnval, out iValue);
            if (bCheck)
            {
                te_Obj.Text = keypad.returnval;
            }
            else
                te_Obj.Text = "";
        }

        /// <summary>
        /// 20210722 오세완 차장 
        /// 실적 주기 정보 키보드 입력 이벤트 공통 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tx_period_KeyDown(object sender, KeyEventArgs e)
        {
            TextEdit te_Obj = sender as TextEdit;
            if (e.KeyCode != Keys.Enter)
                return;

            int iValue;
            bool bCheck = int.TryParse(te_Obj.Text, out iValue);
            if(!bCheck)
            {
                te_Obj.Text = "";
            }
        }

        /// <summary>
        /// 20210722 오세완 차장 IP정보 키패드 입력 이젠트 공통 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tx_IpValue_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCNUMPAD keypad = new XFCNUMPAD();
            keypad.ShowDialog();

            TextEdit te_Obj = sender as TextEdit;
            int iValue;
            bool bCheck = int.TryParse(keypad.returnval, out iValue);

            if(bCheck)
            {
                if (iValue > 255)
                {
                    te_Obj.Text = "";
                }
                else
                    te_Obj.Text = keypad.returnval;
            }
            else
                te_Obj.Text = "";

        }

        /// <summary>
        /// 20210722 오세완 차장 IP정보 키입력 이벤트 공통 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tx_IpValue_KeyDown(object sender, KeyEventArgs e)
        {
            TextEdit te_Obj = sender as TextEdit;
            if (e.KeyCode != Keys.Enter)
                return;

            int iValue;
            bool bCheck = int.TryParse(te_Obj.Text, out iValue);

            if (bCheck)
            {
                if (iValue > 255)
                {
                    te_Obj.Text = "";
                }
            }
            else
                te_Obj.Text = "";
        }

        /// <summary>
        /// 20210722 오세완 차장 PLC_INFO.ini 내용 읽기 및 콤보박스 값 초기화
        /// </summary>
        private void InitValue()
        {
            string sIPAddress = HKInc.Utils.Interface.Forms.IIniFileControl.ReadIniFile("PLCINFO", "IP");
            string[] sArr_IP = sIPAddress.Split('.');
            if (sArr_IP != null)
            {
                if (sArr_IP.Length > 1)
                    tx_Ipa.Text = sArr_IP[0];

                if (sArr_IP.Length > 2)
                    tx_Ipb.Text = sArr_IP[1];

                if (sArr_IP.Length > 3)
                {
                    tx_Ipc.Text = sArr_IP[2];
                    tx_Ipd.Text = sArr_IP[3];
                }
            }

            string sPort = HKInc.Utils.Interface.Forms.IIniFileControl.ReadIniFile("PLCINFO", "PORT");
            if (sPort != null)
                tx_port.Text = sPort;

            slup_Machine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            slup_Machine.SetFontSize(new Font("맑은 고딕", 15f));
            string sMachine = HKInc.Utils.Interface.Forms.IIniFileControl.ReadIniFile("PLCINFO", "MACHINE");
            if (sMachine != null)
            {
                if (sMachine != "")
                    slup_Machine.EditValue = sMachine;
            }

            string sRefreshTime = HKInc.Utils.Interface.Forms.IIniFileControl.ReadIniFile("PLCINFO", "TIMER_PERIOD");
            if (sRefreshTime != null)
            {
                if (sRefreshTime != "")
                    tx_Countperiod.EditValue = sRefreshTime;
            }

            string sUpdatePeriod = HKInc.Utils.Interface.Forms.IIniFileControl.ReadIniFile("PLCINFO", "UPDATE_PERIOD");
            if(sUpdatePeriod != null)
            {
                if (sUpdatePeriod != "")
                    tx_Updateperiod.EditValue = sUpdatePeriod;
            }

            slup_Machinegroup.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            slup_Machinegroup.SetFontSize(new Font("맑은 고딕", 15f));

            
        }
    }
}