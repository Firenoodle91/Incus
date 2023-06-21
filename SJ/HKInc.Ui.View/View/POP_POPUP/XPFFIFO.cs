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
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using System.Data.SqlClient;
using System.Collections.Generic;
using HKInc.Ui.View.View.REPORT;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 선입선출메시지
    /// </summary>
    public partial class XPFFIFO : Service.Base.PopupCallbackFormTemplate
    {
        public XPFFIFO()
        {
            InitializeComponent();
        }

        public XPFFIFO(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;

            btn_WorkEnd.Click += Btn_WorkEnd_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        private void Btn_WorkEnd_Click(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, this.PopupParam.GetValue(PopupParameter.KeyValue));
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            ActClose();
        }
    }
}
