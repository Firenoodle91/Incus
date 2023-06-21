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
using System.Data.SqlClient;
using DevExpress.XtraEditors.Mask;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 이동표 새 출력시 박스내 수량
    /// </summary>
    public partial class XPFITEMMOVEPRINT_BOX : Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_PUR_STOCK_IN_LOT_NO> ModelService = (IService<VI_PUR_STOCK_IN_LOT_NO>)ProductionFactory.GetDomainService("VI_PUR_STOCK_IN_LOT_NO");

        public XPFITEMMOVEPRINT_BOX()
        {
            InitializeComponent();
        }

        public XPFITEMMOVEPRINT_BOX(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("BoxInQty");

            //this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);
            //this.Size = this.MinimumSize;

            spin_BoxInQty.Click += Spin_Click;
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

            btn_Print.Text = LabelConvert.GetLabelText("NewPrint");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, spin_BoxInQty.EditValue.GetDecimalNullToZero());
            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            ActClose();
        }

        private void Spin_Click(object sender, EventArgs e)
        {
            var spinEdit = sender as SpinEdit;
            if (spinEdit == null) return;
            spinEdit.SelectAll();
            if (!GlobalVariable.KeyPad) return;

            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                spinEdit.EditValue = keyPad.returnval;
            }
        }
    }
}
