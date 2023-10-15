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
    /// 작업 시작 (원소재투입)
    /// </summary>
    public partial class XPFSRCIN_START_SJ : Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_PUR_STOCK_IN_LOT_NO> ModelService = (IService<VI_PUR_STOCK_IN_LOT_NO>)ProductionFactory.GetDomainService("VI_PUR_STOCK_IN_LOT_NO");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        private string SrcItemCode = null;
        private string SrcOutLotNo = null;
        
        public XPFSRCIN_START_SJ()
        {
            InitializeComponent();

        }

        public XPFSRCIN_START_SJ(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkStart");

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);

            tx_SrcOutLotNo.Click += Tx_SrcOutLotNo_Click;
            tx_SrcOutLotNo.KeyDown += Tx_SrcOutLotNo_KeyDown;
            btn_Start.Click += Btn_Start_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

            lup_MachineGroup.EditValueChanged += Lup_MachineGroup_EditValueChanged;
            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;
            lup_Machine.Popup += Lup_Machine_Popup;

          //  this.Size = new Size(this.Size.Width, this.Size.Height - 80);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);
            //this.Size = this.MinimumSize;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            lup_MachineGroup.SetDefaultPOP(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lup_MachineGroup.SetFontSize(new Font("맑은 고딕", 12f));

            lup_Machine.SetDefaultPOP(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lup_Machine.SetFontSize(new Font("맑은 고딕", 12f));

            tx_SrcOutLotNo.ImeMode = ImeMode.Disable;
            tx_SrcOutLotNo.Font = new Font("맑은 고딕", 15f);
            tx_SrcItemName.Font = new Font("맑은 고딕", 15f);
            tx_SrcAvailableQty.Font = new Font("맑은 고딕", 15f);

            //lup_MachineGroup.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_SrcOutLotNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_SrcItemName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_SrcAvailableQty.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;

            lup_MachineGroup.EditValue = TEMP_XFPOP1000_Obj.MachineGroupCode.GetNullToNull();
            if (!lup_MachineGroup.EditValue.IsNullOrEmpty())
            {
                lup_MachineGroup.ReadOnly = true;
                lup_MachineGroup.Properties.Buttons[1].Enabled = false;
            }

            //lup_Machine.ReadOnly = true;
            lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode.GetNullToNull();
            //var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode && p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq).FirstOrDefault();
            //if (TN_MPS1200 != null && TN_MPS1200.MachineFlag == "Y")
            //{
            //    lup_Machine.ReadOnly = false;
            //}

            btn_Start.Text = LabelConvert.GetLabelText("Start");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
        }

        private void Tx_SrcOutLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var srcOutLotNo = tx_SrcOutLotNo.EditValue.GetNullToEmpty().ToUpper();

                var TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode).First();
                var srcItemCode = TN_STD1100.SrcCode;
                if (srcItemCode.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_65));
                    tx_SrcItemName.EditValue = null;
                    tx_SrcAvailableQty.EditValue = null;
                    SrcItemCode = null;
                    SrcOutLotNo = null;
                    tx_SrcOutLotNo.EditValue = null;
                    tx_SrcOutLotNo.Focus();
                    return;
                }

                var dt = DbRequestHandler.GetDataTableSelect("EXEC USP_GET_POP_SRC_IN '" + srcOutLotNo + "'");

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("RawMaterialsAvailable")));
                    tx_SrcItemName.EditValue = null;
                    tx_SrcAvailableQty.EditValue = null;
                    SrcItemCode = null;
                    SrcOutLotNo = null;
                    tx_SrcOutLotNo.EditValue = null;
                    tx_SrcOutLotNo.Focus();
                    return;
                }

                if (dt.Rows[0]["StockQty"].GetDecimalNullToZero() == 0)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("RawMaterialsAvailable")));
                    tx_SrcItemName.EditValue = null;
                    tx_SrcAvailableQty.EditValue = null;
                    SrcItemCode = null;
                    SrcOutLotNo = null;
                    tx_SrcOutLotNo.EditValue = null;
                    tx_SrcOutLotNo.Focus();
                    return;
                }

                if (srcItemCode != dt.Rows[0]["ItemCode"].GetNullToEmpty())
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_66));
                    tx_SrcItemName.EditValue = null;
                    tx_SrcAvailableQty.EditValue = null;
                    SrcItemCode = null;
                    SrcOutLotNo = null;
                    tx_SrcOutLotNo.EditValue = null;
                    tx_SrcOutLotNo.Focus();
                    return;
                }

                var ItemCode = new System.Data.SqlClient.SqlParameter("@ItemCode", srcItemCode);
                var InLotNo = new System.Data.SqlClient.SqlParameter("@InLotNo", srcOutLotNo);
                var fifoDataSet = DbRequestHandler.GetDataSet("USP_GET_PUR_FIFO", ItemCode, InLotNo);
                if (fifoDataSet != null && fifoDataSet.Tables.Count > 0 && fifoDataSet.Tables[0].Rows.Count > 0)
                {
                    MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_117));
                    //MessageBoxHandler.Show("입력한 입고 LOT NO의 이전 LOT NO 재고가 남아 있습니다. 확인해 주시기 바랍니다.");
                    tx_SrcItemName.EditValue = null;
                    tx_SrcAvailableQty.EditValue = null;
                    SrcItemCode = null;
                    SrcOutLotNo = null;
                    tx_SrcOutLotNo.EditValue = null;
                    tx_SrcOutLotNo.Focus();
                    return;
                }
                
                tx_SrcItemName.EditValue = dt.Rows[0][DataConvert.GetCultureDataFieldName("ItemName")].GetNullToEmpty();
                tx_SrcAvailableQty.EditValue = dt.Rows[0]["StockQty"].GetDecimalNullToZero().ToString("#,#.##");
                SrcItemCode = dt.Rows[0]["ItemCode"].GetNullToEmpty();
                SrcOutLotNo = dt.Rows[0]["OutLotNo"].GetNullToEmpty();
            }
        }
        
        private void Tx_SrcOutLotNo_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad) return;
            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                tx_SrcOutLotNo.EditValue = keyPad.returnval;
                Tx_SrcOutLotNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }
        
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            if (SrcOutLotNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("SrcInfo")));
                return;
            }

            var machineCode = lup_Machine.EditValue.GetNullToNull();
            if (TEMP_XFPOP1000_Obj.MachineFlag == "Y" && machineCode.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("MachineInfo")));
                return;
            }

            #region 자재출고 INSERT

            var TN_PUR1201 = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == SrcOutLotNo).First();
            TN_PUR1300 newobj = new TN_PUR1300()
            {
                OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                OutDate = DateTime.Today,
                OutId = GlobalVariable.LoginId,
                Memo = "POP자동출고",
            };

            var newDetailObj = new TN_PUR1301();
            newDetailObj.OutNo = newobj.OutNo;
            newDetailObj.OutSeq = newobj.TN_PUR1301List.Count == 0 ? 1 : newobj.TN_PUR1301List.Max(o => o.OutSeq) + 1;
            newDetailObj.InNo = TN_PUR1201.InNo;
            newDetailObj.InSeq = TN_PUR1201.InSeq;
            newDetailObj.ItemCode = TN_PUR1201.ItemCode;
            newDetailObj.OutQty = tx_SrcAvailableQty.EditValue.GetDecimalNullToZero();
            newDetailObj.OutLotNo = SrcOutLotNo;
            newDetailObj.InLotNo = SrcOutLotNo;
            newDetailObj.InCustomerLotNo = TN_PUR1201.InCustomerLotNo;
            newDetailObj.NewRowFlag = "Y";
            newobj.TN_PUR1301List.Add(newDetailObj);

            ModelService.InsertChild(newobj);
            ModelService.Save();
            #endregion

            DialogResult = System.Windows.Forms.DialogResult.OK;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, SrcItemCode);
            param.SetValue(PopupParameter.Value_2, SrcOutLotNo);
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

        private void Lup_Machine_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var value = lookup.EditValue.GetNullToEmpty();
            if (!value.IsNullOrEmpty())
            {
                var nowMachineStateObj = ModelService.GetChildList<VI_XRREP6000_LIST>(p => p.MachineMCode == value).FirstOrDefault();
                if (nowMachineStateObj != null)
                {
                    //대영정밀 설비 중복허용 가능
                    if (nowMachineStateObj.JobStates == MasterCodeSTR.JobStates_Stop)
                    {
                        MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_173));
                        lookup.EditValue = null;
                        return;
                    }
                } 

                var data = lookup.GetSelectedDataRow() as TN_MEA1000;
                if (data != null)
                {
                    if (data.DailyCheckFlag == "Y")
                    {
                        if (ModelService.GetChildList<TN_MEA1003>(p => p.MachineCode == data.MachineMCode && p.CheckDate == DateTime.Today).FirstOrDefault() == null)
                        {
                            MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_116));
                            //MessageBoxHandler.Show("해당 설비에 일일점검이 되어있지 않습니다. 확인 부탁드립니다.");
                            lookup.EditValue = null;
                        }
                    }
                }
            }
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
