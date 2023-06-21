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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using System.Data.SqlClient;
using HKInc.Ui.View.View.REPORT;
using DevExpress.XtraReports.UI;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;

namespace HKInc.Ui.View.View.POP_POPUP
{
    public partial class XPFWORKSTART : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");
        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        public XPFWORKSTART()
        {
            InitializeComponent();
        }


        public XPFWORKSTART(PopupDataParam param, PopupCallback callback) : this()
        {
            this.PopupParam = param;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkStart");

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)PopupParam.GetValue(PopupParameter.Value_1);

            btn_Apply.Click += Btn_Apply_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
        }

        protected override void InitControls()
        {
            var tN_STD1100 = ModelService.GetChildList<TN_STD1100>(x => x.ItemCode == TEMP_XFPOP1000_Obj.ItemCode && x.UseFlag == "Y").FirstOrDefault();

            if (TEMP_XFPOP1000_Obj.MachineCode2 != null)
            {
                lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode2;
            }

            if (tN_STD1100 != null && tN_STD1100.MoldCode != null)
            {
                lup_Mold.EditValue = tN_STD1100.MoldCode;
            }

            // 20220103 오세완 차장 생성자로 위치 이동 처리
            //btn_Apply.Click += Btn_Apply_Click;
            //btn_Cancel.Click += Btn_Cancel_Click;

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
            btn_Apply.Text = LabelConvert.GetLabelText("Apply");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");

            lup_Machine.SetDefault(false, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Mold.SetDefault(false, "MoldMCode", DataConvert.GetCultureDataFieldName("MoldName"), ModelService.GetChildList<TN_MOLD1100>(x => x.UseYN == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            lup_Machine.SetFontSize(new Font("맑은 고딕", 14f));
            lup_Mold.SetFontSize(new Font("맑은 고딕", 14f));

            lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_Mold.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
        }

        private void Btn_Apply_Click(object sender, EventArgs e)
        {
            TEMP_XFPOP1000_Obj.Temp = lup_Mold.EditValue.GetNullToNull();
            TEMP_XFPOP1000_Obj.MachineCode2 = lup_Machine.EditValue.GetNullToNull();

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