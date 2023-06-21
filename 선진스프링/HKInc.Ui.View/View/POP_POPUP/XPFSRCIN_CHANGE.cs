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
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 원소재교체
    /// </summary>
    public partial class XPFSRCIN_CHANGE : Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_PUR_STOCK_IN_LOT_NO> ModelService = (IService<VI_PUR_STOCK_IN_LOT_NO>)ProductionFactory.GetDomainService("VI_PUR_STOCK_IN_LOT_NO");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;
        private string productLotNo;
        private string itemMoveNo;
        private string SrcItemCode;
        private string SrcOutLotNo;
        public XPFSRCIN_CHANGE()
        {
            InitializeComponent();

        }

        public XPFSRCIN_CHANGE(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("SrcChange");
            this.Size = new Size(this.Size.Width, this.Size.Height - 80);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 80);

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);
            productLotNo = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            itemMoveNo = parameter.GetValue(PopupParameter.Value_2).GetNullToEmpty();

            tx_SrcOutLotNo.Click += Tx_SrcOutLotNo_Click;
            tx_SrcOutLotNo.KeyDown += Tx_SrcOutLotNo_KeyDown;

            btn_Change.Click += Btn_Change_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            tx_SrcOutLotNo.ImeMode = ImeMode.Disable;

            var VI_MACHINE_DAILY_CHECK_LIST = ModelService.GetChildList<VI_MACHINE_DAILY_CHECK>(p => true).ToList();
            //lup_Machine.SetDefaultPOP(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Machine.SetDefaultPOP(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), VI_MACHINE_DAILY_CHECK_LIST, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lup_Machine.SetFontSize(new Font("맑은 고딕", 15f));

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

                tx_UseSrcOutLotNo.EditValue = TN_LOT_DTL.SrcInLotNo;
                tx_UseSrcItemName.EditValue = dt.Rows[0][DataConvert.GetCultureDataFieldName("ItemName")].GetNullToEmpty();
                tx_UseSrcAvailableQty.EditValue = dt.Rows[0]["StockQty"].GetDecimalNullToZero().ToString("#,#.##"); 
            }

            lcSrcOutLotNo1.Text = lcSrcOutLotNo.Text;
            lcSrcItemName1.Text = lcSrcItemName.Text;
            lcSrcAvailableQty1.Text = lcSrcAvailableQty.Text;

            btn_Change.Text = LabelConvert.GetLabelText("Change");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
        }

        private void Tx_SrcOutLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var useSrcOutLotNo = tx_UseSrcOutLotNo.EditValue.GetNullToEmpty().ToUpper();
                var srcOutLotNo = tx_SrcOutLotNo.EditValue.GetNullToEmpty().ToUpper();

                //if (useSrcOutLotNo == srcOutLotNo)
                //{
                //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_41), LabelConvert.GetLabelText("SrcOutLotNo")));
                //    tx_SrcItemName.EditValue = null;
                //    tx_SrcAvailableQty.EditValue = null;
                //    SrcItemCode = null;
                //    SrcOutLotNo = null;
                //    tx_SrcOutLotNo.EditValue = null;
                //    tx_SrcOutLotNo.Focus();
                //    return;
                //}

                var TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode).First();
                if (TN_STD1100.TN_STD1100_SRC == null)
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

                if (dt.Rows[0]["StockQty"].GetDecimalNullToZero() <= 0)
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
                    var spec1 = TN_STD1100.Spec1.GetNullToEmpty();
                    if (spec1.Trim().IsNullOrEmpty())
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
                    else
                    {
                        var checkItemCode = dt.Rows[0]["ItemCode"].GetNullToEmpty();
                        var itemList = spec1.Split(',').ToList();
                        //var checkSpec = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == checkItemCode).First().Spec1;
                        if (!itemList.Contains(checkItemCode))
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
                        else
                        {
                            srcItemCode = checkItemCode;
                        }
                    }
                }

                var ItemCode = new System.Data.SqlClient.SqlParameter("@ItemCode", srcItemCode);
                var InLotNo = new System.Data.SqlClient.SqlParameter("@InLotNo", srcOutLotNo);
                var fifoDataSet = DbRequestHandler.GetDataSet("USP_GET_PUR_FIFO", ItemCode, InLotNo);
                if (fifoDataSet != null && fifoDataSet.Tables.Count > 0 && fifoDataSet.Tables[0].Rows.Count > 0)
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.KeyValue, dt);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFFIFO, param, FifoCallBack);
                    form.ShowPopup(true);
                    return;
                    //MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_117));
                    ////MessageBoxHandler.Show("입력한 입고 LOT NO의 이전 LOT NO 재고가 남아 있습니다. 확인해 주시기 바랍니다.");
                    //tx_SrcItemName.EditValue = null;
                    //tx_SrcAvailableQty.EditValue = null;
                    //SrcItemCode = null;
                    //SrcOutLotNo = null;
                    //tx_SrcOutLotNo.EditValue = null;
                    //tx_SrcOutLotNo.Focus();
                    //return;
                }

                tx_SrcItemName.EditValue = dt.Rows[0][DataConvert.GetCultureDataFieldName("ItemName")].GetNullToEmpty();
                tx_SrcAvailableQty.EditValue = dt.Rows[0]["StockQty"].GetDecimalNullToZero().ToString("#,#.##");
                SrcItemCode = dt.Rows[0]["ItemCode"].GetNullToEmpty();
                SrcOutLotNo = dt.Rows[0]["OutLotNo"].GetNullToEmpty();
            }
        }

        private void FifoCallBack(object sender, PopupArgument e)
        {
            if (e == null)
            {
                tx_SrcItemName.EditValue = null;
                tx_SrcAvailableQty.EditValue = null;
                SrcItemCode = null;
                SrcOutLotNo = null;
                tx_SrcOutLotNo.EditValue = null;
                tx_SrcOutLotNo.Focus();
                return;
            }

            var dt = e.Map.GetValue(PopupParameter.KeyValue) as DataTable;

            tx_SrcItemName.EditValue = dt.Rows[0][DataConvert.GetCultureDataFieldName("ItemName")].GetNullToEmpty();
            tx_SrcAvailableQty.EditValue = dt.Rows[0]["StockQty"].GetDecimalNullToZero().ToString("#,#.##");
            SrcItemCode = dt.Rows[0]["ItemCode"].GetNullToEmpty();
            SrcOutLotNo = dt.Rows[0]["OutLotNo"].GetNullToEmpty();
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
        
        private void Btn_Change_Click(object sender, EventArgs e)
        {
            if (SrcOutLotNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("ChangingSrcInfo")));
                return;
            }

            var machineCode = lup_Machine.EditValue.GetNullToNull();

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
            newDetailObj.Temp = TEMP_XFPOP1000_Obj.WorkNo;
            newobj.TN_PUR1301List.Add(newDetailObj);

            ModelService.InsertChild(newobj);
            ModelService.Save();
            #endregion

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, SrcItemCode);
            param.SetValue(PopupParameter.Value_2, SrcOutLotNo);
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
