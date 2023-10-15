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
    /// 이동표교체
    /// </summary>
    public partial class XPFITEMMOVESCAN_CHANGE_RUS : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        private string useItemMoveNo = null;
        private string productLotNo = null;
        private string ItemMoveNo = null;

        public XPFITEMMOVESCAN_CHANGE_RUS()
        {
            InitializeComponent();
        }

        public XPFITEMMOVESCAN_CHANGE_RUS(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("ItemMoveChange");
            this.Size = new Size(this.Size.Width, this.Size.Height + 40);

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);
            productLotNo = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            useItemMoveNo = parameter.GetValue(PopupParameter.Value_2).GetNullToEmpty();

            tx_ItemMoveNo.KeyDown += Tx_ItemMoveNo_KeyDown;
            tx_ItemMoveNo.Click += Tx_ItemMoveNo_Click;

            btn_Change.Click += Btn_Change_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            tx_ItemMoveNo.ImeMode = ImeMode.Disable;

            lup_Machine.SetDefault(false, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            
            var processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
            lup_PriviousProcessName.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);
            lup_NowProcessName.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);
            lup_NextProcessName.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);
            lup_PriviousProcessName1.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);
            lup_NowProcessName1.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);
            lup_NextProcessName1.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);

            lup_Machine.SetFontSize(new Font("맑은 고딕", 15f));
            lup_PriviousProcessName.SetFontSize(new Font("맑은 고딕", 15f));
            lup_NowProcessName.SetFontSize(new Font("맑은 고딕", 15f));
            lup_NextProcessName.SetFontSize(new Font("맑은 고딕", 15f));
            lup_PriviousProcessName1.SetFontSize(new Font("맑은 고딕", 15f));
            lup_NowProcessName1.SetFontSize(new Font("맑은 고딕", 15f));
            lup_NextProcessName1.SetFontSize(new Font("맑은 고딕", 15f));
            tx_ItemMoveNo.Font = new Font("맑은 고딕", 15f);
            tx_WorkNo.Font = new Font("맑은 고딕", 15f);
            tx_ProductLotNo.Font = new Font("맑은 고딕", 15f);
            tx_ItemMoveNo1.Font = new Font("맑은 고딕", 15f);
            tx_WorkNo1.Font = new Font("맑은 고딕", 15f);
            tx_ProductLotNo1.Font = new Font("맑은 고딕", 15f);

            lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_PriviousProcessName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_NowProcessName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_NextProcessName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_PriviousProcessName1.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_NowProcessName1.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_NextProcessName1.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_ItemMoveNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_WorkNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_ProductLotNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_ItemMoveNo1.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_WorkNo1.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_ProductLotNo1.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            lcItemMoveNo1.Text = lcItemMoveNo.Text;
            lcProductLotNo1.Text = lcProductLotNo.Text;
            lcWorkNo1.Text = lcWorkNo.Text;
            lcPriviousProcessName1.Text = lcPriviousProcessName.Text;
            lcNowProcessName1.Text = lcNowProcessName.Text;
            lcNextProcessName1.Text = lcNextProcessName.Text;

            btn_Change.Text = LabelConvert.GetLabelText("Change");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");

            var ds = DbRequestHandler.GetDataQury("EXEC USP_GET_ITEM_MOVE_NO_SCAN_START '" + TEMP_XFPOP1000_Obj.WorkNo + "', '" + useItemMoveNo + "'," + TEMP_XFPOP1000_Obj.ProcessSeq + "");
            tx_ItemMoveNo1.EditValue = useItemMoveNo;
            tx_WorkNo1.EditValue = ds.Tables[1].Rows[0][0].GetNullToNull();
            tx_ProductLotNo1.EditValue = ds.Tables[1].Rows[0][1].GetNullToNull();
            lup_PriviousProcessName1.EditValue = ds.Tables[1].Rows[0][2].GetNullToNull();
            lup_NowProcessName1.EditValue = ds.Tables[1].Rows[0][3].GetNullToNull();
            lup_NextProcessName1.EditValue = ds.Tables[1].Rows[0][4].GetNullToNull();
            
            var TN_LOT_DTL = ModelService.GetChildList<TN_LOT_DTL>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && p.ProductLotNo == productLotNo).OrderBy(p => p.Seq).LastOrDefault();
            if (TN_LOT_DTL != null)
            {
                //lup_Machine.EditValue = TN_LOT_DTL.MachineCode;
                lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode;
            }
        }

        private void Tx_ItemMoveNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var itemMoveNo = tx_ItemMoveNo.EditValue.GetNullToEmpty().ToUpper();

                if (itemMoveNo == useItemMoveNo)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_73));
                    ItemMoveNo = null;
                    tx_ItemMoveNo.EditValue = null;
                    tx_WorkNo.EditValue = null;
                    tx_ProductLotNo.EditValue = null;
                    lup_PriviousProcessName.EditValue = null;
                    lup_NowProcessName.EditValue = null;
                    lup_NextProcessName.EditValue = null;
                    tx_ItemMoveNo.Focus();
                    return;
                }

                var ds = DbRequestHandler.GetDataQury("EXEC USP_GET_ITEM_MOVE_NO_SCAN_START '" + TEMP_XFPOP1000_Obj.WorkNo + "', '" + itemMoveNo + "'," + TEMP_XFPOP1000_Obj.ProcessSeq + "");

                if (ds == null || ds.Tables[1].Rows.Count < 1)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                    ItemMoveNo = null;
                    tx_ItemMoveNo.EditValue = null;
                    tx_WorkNo.EditValue = null;
                    tx_ProductLotNo.EditValue = null;
                    lup_PriviousProcessName.EditValue = null;
                    lup_NowProcessName.EditValue = null;
                    lup_NextProcessName.EditValue = null;
                    tx_ItemMoveNo.Focus();
                    return;
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "F")
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("OkQty")));
                    ItemMoveNo = null;
                    tx_ItemMoveNo.EditValue = null;
                    tx_WorkNo.EditValue = null;
                    tx_ProductLotNo.EditValue = null;
                    lup_PriviousProcessName.EditValue = null;
                    lup_NowProcessName.EditValue = null;
                    lup_NextProcessName.EditValue = null;
                    tx_ItemMoveNo.Focus();
                    return;
                }
                else if (ds.Tables[1].Rows[0][0].ToString() != TEMP_XFPOP1000_Obj.WorkNo)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_64));
                    ItemMoveNo = null;
                    tx_ItemMoveNo.EditValue = null;
                    tx_WorkNo.EditValue = null;
                    tx_ProductLotNo.EditValue = null;
                    lup_PriviousProcessName.EditValue = null;
                    lup_NowProcessName.EditValue = null;
                    lup_NextProcessName.EditValue = null;
                    tx_ItemMoveNo.Focus();
                    return;
                }
                
                ItemMoveNo = itemMoveNo;
                tx_WorkNo.EditValue = ds.Tables[1].Rows[0][0].GetNullToNull();
                tx_ProductLotNo.EditValue = ds.Tables[1].Rows[0][1].GetNullToNull();
                lup_PriviousProcessName.EditValue = ds.Tables[1].Rows[0][2].GetNullToNull();
                lup_NowProcessName.EditValue = ds.Tables[1].Rows[0][3].GetNullToNull();
                lup_NextProcessName.EditValue = ds.Tables[1].Rows[0][4].GetNullToNull();
            }
        }

        private void Tx_ItemMoveNo_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad) return;
            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                tx_ItemMoveNo.EditValue = keyPad.returnval;
                Tx_ItemMoveNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }
        
        private void Btn_Change_Click(object sender, EventArgs e)
        {
            var productLotNo = tx_ProductLotNo.EditValue.GetNullToNull();
            if (ItemMoveNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                return;
            }

            var machineCode = lup_Machine.EditValue.GetNullToNull();
            if (TEMP_XFPOP1000_Obj.MachineFlag == "Y" && machineCode.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("MachineInfo")));
                return;
            }

            DialogResult = DialogResult.OK;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, ItemMoveNo);
            param.SetValue(PopupParameter.Value_2, productLotNo);
            param.SetValue(PopupParameter.Value_3, machineCode);
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            ActClose();
        }
    }
}
