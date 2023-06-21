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
    /// 작업 시작 (반제품투입)
    /// </summary>
    public partial class XPFBANIN_START : Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_BAN_STOCK_IN_LOT_NO> ModelService = (IService<VI_BAN_STOCK_IN_LOT_NO>)ProductionFactory.GetDomainService("VI_BAN_STOCK_IN_LOT_NO");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        private string BanItemCode = null;
        private string BanOutLotNo = null;
        
        public XPFBANIN_START()
        {
            InitializeComponent();

        }

        public XPFBANIN_START(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkStart");

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);

            tx_BanOutLotNo.Click += Tx_BanOutLotNo_Click;
            tx_BanOutLotNo.KeyDown += Tx_BanOutLotNo_KeyDown;
            btn_Start.Click += Btn_Start_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

            lup_MachineGroup.EditValueChanged += Lup_MachineGroup_EditValueChanged;
            lup_Machine.Popup += Lup_Machine_Popup;

            this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 80);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);
            this.Size = this.MinimumSize;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            lup_MachineGroup.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_MachineGroup.SetFontSize(new Font("맑은 고딕", 12f));

            lup_Machine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Machine.SetFontSize(new Font("맑은 고딕", 12f));

            tx_BanOutLotNo.ImeMode = ImeMode.Disable;
            tx_BanOutLotNo.Font = new Font("맑은 고딕", 12f);
            tx_BanItemName.Font = new Font("맑은 고딕", 12f);
            tx_BanAvailableQty.Font = new Font("맑은 고딕", 12f);

            lup_MachineGroup.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_BanOutLotNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_BanItemName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_BanAvailableQty.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;

            lup_MachineGroup.EditValue = TEMP_XFPOP1000_Obj.MachineGroupCode.GetNullToNull();
            if (!lup_MachineGroup.EditValue.IsNullOrEmpty())
                lup_MachineGroup.ReadOnly = true;
            //lup_Machine.ReadOnly = true;
            lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode.GetNullToNull();
            //var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode && p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq).FirstOrDefault();
            //if (TN_MPS1200 != null && TN_MPS1200.MachineFlag == "Y")
            //{
            //    lup_Machine.ReadOnly = false;
            //}

            //lup_Machine.ReadOnly = true;
            //lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode.GetNullToNull();
            //var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode && p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq).FirstOrDefault();
            //if (TN_MPS1200 != null && TN_MPS1200.MachineFlag == "Y")
            //{
            //    lup_Machine.ReadOnly = false;
            //}

            btn_Start.Text = LabelConvert.GetLabelText("Start");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
        }

        private void Tx_BanOutLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var banOutLotNo = tx_BanOutLotNo.EditValue.GetNullToEmpty().ToUpper();

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
        
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            if (BanOutLotNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("BanInfo")));
                return;
            }

            var machineCode = lup_Machine.EditValue.GetNullToNull();
            if (TEMP_XFPOP1000_Obj.MachineFlag == "Y" && machineCode.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("MachineInfo")));
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, BanItemCode);
            param.SetValue(PopupParameter.Value_2, BanOutLotNo);
            param.SetValue(PopupParameter.Value_3, machineCode);
            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            ActClose();
        }

        private void Lup_MachineGroup_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            //var value = lookup.EditValue.GetNullToNull();
            lup_Machine.EditValue = null;
        }

        private void Lup_Machine_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var machineGroupCode = lup_MachineGroup.EditValue.GetNullToNull();

            if (machineGroupCode.IsNullOrEmpty())
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            else
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[MachineGroupCode] = '" + machineGroupCode + "'";
        }
    }
}
