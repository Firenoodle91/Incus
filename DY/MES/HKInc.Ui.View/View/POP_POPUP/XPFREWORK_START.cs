using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraEditors.Mask;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 리워크 공정 작업 시작 팝업
    /// </summary>
    public partial class XPFREWORK_START : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1200> ModelService = (IService<TN_MPS1200>)ProductionFactory.GetDomainService("TN_MPS1200");

        TEMP_XFPOP_PACK TEMP_XFPOP_PACK;

        public XPFREWORK_START()
        {
            InitializeComponent();
        }

        public XPFREWORK_START(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            lblMessageText.Text = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_131);

            btn_WorkStart.Click += Btn_WorkStart_Click;
            btn_WorkCancel.Click += Btn_WorkCancel_Click;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        private void Btn_WorkStart_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;

            PopupDataParam param = new PopupDataParam();
            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        private void Btn_WorkCancel_Click(object sender, EventArgs e)
        {
            ActClose();
        }
    }
}

