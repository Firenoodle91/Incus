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
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.POP_Popup
{
    public partial class XPFWORKSTART_RUS : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1000> ModelService = (IService<TN_MPS1000>)ProductionFactory.GetDomainService("TN_MPS1000");
        TP_XFPOP1000_V2_LIST TEMP_XFPOP1000_Obj;
        #endregion

        public XPFWORKSTART_RUS()
        {
            InitializeComponent();
        }

        public XPFWORKSTART_RUS(PopupDataParam param, PopupCallback callback) : this()
        {
            this.PopupParam = param;
            this.Callback = callback;
            this.Text = "Начните работу";

            TEMP_XFPOP1000_Obj = (TP_XFPOP1000_V2_LIST)PopupParam.GetValue(PopupParameter.Value_1);

            btn_Apply.Click += Btn_Apply_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
        }

        protected override void InitControls()
        {
            var tN_STD1100 = ModelService.GetChildList<TN_STD1100>(x => x.ItemCode == TEMP_XFPOP1000_Obj.ItemCode && x.UseYn == "Y").FirstOrDefault();

            if (TEMP_XFPOP1000_Obj.MachineCode != null)
            {
                lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode;
            }

            // 20220103 오세완 차장 이태식 차장 요청으로 버튼 숨김 처리
            this.SetToolbarButtonVisible(ToolbarButton.Export, false); // 내보내기
            this.SetToolbarButtonVisible(ToolbarButton.Save, false); // 저장
            this.SetToolbarButtonVisible(ToolbarButton.Refresh, false); // 조회
            this.SetToolbarButtonVisible(ToolbarButton.Confirm, false); // 확인
            this.SetToolbarButtonVisible(ToolbarButton.Print, false); // 인쇄
            this.SetToolbarButtonVisible(ToolbarButton.Close, false); // 종료
        }

        protected override void InitCombo()
        {
            string sMachine_group_code = "";
            if (TEMP_XFPOP1000_Obj != null)
            {
                TN_MPS1000 mps1000 = ModelService.GetList(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode &&
                                                               p.ProcessCode == TEMP_XFPOP1000_Obj.Process && 
                                                               p.UseYn == "Y").FirstOrDefault();
                if(mps1000 != null)
                    sMachine_group_code = mps1000.MachineGroupCode.GetNullToEmpty();
                
            }

            List<TN_MEA1000> mea_Arr = ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y" ).ToList();
            if(mea_Arr != null)
                if(mea_Arr.Count > 0)
                {
                    // 20220427 오세완 차장 그룹코드를 입력안한 경우가 있을지도 몰라서 추가 처리 
                    if (sMachine_group_code != "")
                        mea_Arr = mea_Arr.Where(p => p.MachineGroupCode == sMachine_group_code).ToList();
                }

            lup_Machine.SetDefault(false, "MachineCode", "MachineName", mea_Arr, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Machine.SetFontSize(new Font("맑은 고딕", 14f));
            lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
        }

        private void Btn_Apply_Click(object sender, EventArgs e)
        {
            TEMP_XFPOP1000_Obj.MachineCode = lup_Machine.EditValue.GetNullToNull();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, DialogResult.OK);
            param.SetValue(PopupParameter.Value_2, TEMP_XFPOP1000_Obj);
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            ActClose();
        }

        protected override void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

    }
}