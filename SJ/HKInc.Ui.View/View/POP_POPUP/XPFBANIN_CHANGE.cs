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

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 반제품교체
    /// </summary>
    public partial class XPFBANIN_CHANGE : Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_BAN_STOCK_IN_LOT_NO> ModelService = (IService<VI_BAN_STOCK_IN_LOT_NO>)ProductionFactory.GetDomainService("VI_BAN_STOCK_IN_LOT_NO");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;
        private string productLotNo;
        private string itemMoveNo;
        private string BanItemCode;
        private string BanOutLotNo;
        
        public XPFBANIN_CHANGE()
        {
            InitializeComponent();

        }

        public XPFBANIN_CHANGE(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("BanChange");
            this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 80);
            this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 80);

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);
            productLotNo = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            itemMoveNo = parameter.GetValue(PopupParameter.Value_2).GetNullToEmpty();

            tx_BanOutLotNo.Click += Tx_BanOutLotNo_Click;
            tx_BanOutLotNo.KeyDown += Tx_BanOutLotNo_KeyDown;

            btn_Change.Click += Btn_Change_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            tx_BanOutLotNo.ImeMode = ImeMode.Disable;
            
            var VI_MACHINE_DAILY_CHECK_LIST = ModelService.GetChildList<VI_MACHINE_DAILY_CHECK>(p => true).ToList();
            //lup_Machine.SetDefaultPOP(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Machine.SetDefaultPOP(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), VI_MACHINE_DAILY_CHECK_LIST, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lup_Machine.SetFontSize(new Font("맑은 고딕", 12f));

            var cultureIndex = DataConvert.GetCultureIndex();

            var TN_LOT_DTL = ModelService.GetChildList<TN_LOT_DTL>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && p.ProductLotNo == productLotNo).OrderBy(p => p.Seq).LastOrDefault();
            if (TN_LOT_DTL != null)
            {
                //lup_Machine.EditValue = TN_LOT_DTL.MachineCode;
                if (!TEMP_XFPOP1000_Obj.MachineCode.IsNullOrEmpty())
                {
                    if (VI_MACHINE_DAILY_CHECK_LIST.Any(p => p.MachineMCode == TEMP_XFPOP1000_Obj.MachineCode))
                        lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode;
                }

                var dt = DbRequestHandler.GetDataTableSelect("EXEC USP_GET_POP_SRC_IN '" + TN_LOT_DTL.SrcInLotNo + "'");

                tx_UseBanOutLotNo.EditValue = TN_LOT_DTL.SrcInLotNo;
                tx_UseBanItemName.EditValue = dt.Rows[0][DataConvert.GetCultureDataFieldName("ItemName")].GetNullToEmpty();
                tx_UseBanAvailableQty.EditValue = dt.Rows[0]["StockQty"].GetDecimalNullToZero().ToString("#,#.##"); 
            }

            lcBanProductLotNo1.Text = lcBanProductLotNo.Text;
            lcBanItemName1.Text = lcBanItemName.Text;
            lcBanAvailableQty1.Text = lcBanAvailableQty.Text;

            btn_Change.Text = LabelConvert.GetLabelText("Change");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
        }

        private void Tx_BanOutLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var usebanOutLotNo = tx_UseBanOutLotNo.EditValue.GetNullToEmpty().ToUpper();
                var banOutLotNo = tx_BanOutLotNo.EditValue.GetNullToEmpty().ToUpper();

                if (usebanOutLotNo == banOutLotNo)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_41), LabelConvert.GetLabelText("BanOutLotNo")));
                    tx_BanItemName.EditValue = null;
                    tx_BanAvailableQty.EditValue = null;
                    BanItemCode = null;
                    BanOutLotNo = null;
                    tx_BanOutLotNo.EditValue = null;
                    tx_BanOutLotNo.Focus();
                    return;
                }

                TN_STD1300 banBomObj = null;
                var wanBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode).FirstOrDefault();
                if (wanBomObj != null)
                {
                    banBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ParentBomCode == wanBomObj.BomCode).FirstOrDefault();
                }

                var dt = DbRequestHandler.GetDataTableSelect("EXEC USP_GET_POP_SRC_IN '" + banOutLotNo + "'");

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("RawBanlsAvailable")));
                    tx_BanItemName.EditValue = null;
                    tx_BanAvailableQty.EditValue = null;
                    BanItemCode = null;
                    BanOutLotNo = null;
                    tx_BanOutLotNo.EditValue = null;
                    tx_BanOutLotNo.Focus();
                    return;
                }

                if (banBomObj.ItemCode != dt.Rows[0]["ItemCode"].GetNullToEmpty())
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_79));
                    tx_BanItemName.EditValue = null;
                    tx_BanAvailableQty.EditValue = null;
                    BanItemCode = null;
                    BanOutLotNo = null;
                    tx_BanOutLotNo.EditValue = null;
                    tx_BanOutLotNo.Focus();
                    return;
                }

                tx_BanItemName.EditValue = dt.Rows[0][DataConvert.GetCultureDataFieldName("ItemName")].GetNullToEmpty();
                tx_BanAvailableQty.EditValue = dt.Rows[0]["StockQty"].GetDecimalNullToZero().ToString("#,#.##");
                BanItemCode = dt.Rows[0]["ItemCode"].GetNullToEmpty();
                BanOutLotNo = dt.Rows[0]["OutLotNo"].GetNullToEmpty();
            }
        }

        private void Tx_BanOutLotNo_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad) return;
            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                tx_BanOutLotNo.EditValue = keyPad.returnval;
                Tx_BanOutLotNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }

        private void Btn_Change_Click(object sender, EventArgs e)
        {
            if (BanOutLotNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("ChangingBanInfo")));
                return;
            }

            var machineCode = lup_Machine.EditValue.GetNullToNull();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, BanItemCode);
            param.SetValue(PopupParameter.Value_2, BanOutLotNo);
            param.SetValue(PopupParameter.Value_3, machineCode);
            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            ActClose();
        }
    }
}
