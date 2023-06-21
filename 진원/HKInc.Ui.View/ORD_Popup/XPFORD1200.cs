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
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Mask;

namespace HKInc.Ui.View.ORD_Popup
{
    public partial class XPFORD1200 : Service.Base.PopupCallbackFormTemplate
    {
        public XPFORD1200()
        {
            InitializeComponent();
        }

        public XPFORD1200(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            //this.Text = LabelConvert.GetLabelText("BoxInQty");

            this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);
            this.Size = this.MinimumSize;

            //spin_BoxInQty.Click += Spin_Click;
            btn_Print.Click += Btn_Print_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
        }


        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            spin_BoxInQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_BoxInQty.Properties.Mask.EditMask = "n0";
            spin_BoxInQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_BoxInQty.Properties.Buttons[0].Visible = false;

            spin_BoxInQty.EditValue = this.PopupParam.GetValue(PopupParameter.Value_1).GetDecimalNullToZero();

            //btn_Print.Text = LabelConvert.GetLabelText("NewPrint");
            //btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {
            if (spin_BoxInQty.EditValue.GetNullToZero() == 0)
            {
                return;
            }
                

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, spin_BoxInQty.EditValue.GetDecimalNullToZero());
            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            // 20211123 오세완 차장 여기서 취소한 경우 작업을 종료시키지 않게 하기 위해 구분
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, "Cancel");
            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        //private void Spin_Click(object sender, EventArgs e)
        //{
        //    var spinEdit = sender as SpinEdit;
        //    if (spinEdit == null) return;
        //    if (!GlobalVariable.KeyPad) return;

        //    ////var keyPad = new XFCKEYPAD();
        //    //var keyPad = new XFCNUMPAD(); // 20211123 오세완 차장 keypad에서 numpad로 변경처리
        //    //if (keyPad.ShowDialog() != DialogResult.Cancel)
        //    //{
        //    //    spinEdit.EditValue = keyPad.returnval;
        //    //}
        //}
    }
}