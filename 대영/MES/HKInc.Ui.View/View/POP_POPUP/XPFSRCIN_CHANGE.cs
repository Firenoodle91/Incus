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
            InitGrid();
        }
        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
            gridEx1.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            gridEx1.MainGrid.AddColumn("SrcItemCode", LabelConvert.GetLabelText("SrcItemCode"), false);
            gridEx1.MainGrid.AddColumn("SrcItemName", LabelConvert.GetLabelText("ItemName"));
            gridEx1.MainGrid.AddColumn("SrcLotNo", LabelConvert.GetLabelText("SrcLotNo"));
            gridEx1.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("OutDate", LabelConvert.GetLabelText("OutDate"));
            


        }
        public XPFSRCIN_CHANGE(PopupDataParam parameter, PopupCallback callback) : this()
        {
            InitGrid();
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("SrcChange");
            this.Size = new Size(this.Size.Width, this.Size.Height - 80);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 80);

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);
            productLotNo = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            itemMoveNo = parameter.GetValue(PopupParameter.Value_2).GetNullToEmpty();

            //tx_SrcOutLotNo.Click += Tx_SrcOutLotNo_Click;
            //tx_SrcOutLotNo.KeyDown += Tx_SrcOutLotNo_KeyDown;

            btn_Change.Click += Btn_Change_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
           // tx_SrcOutLotNo.ImeMode = ImeMode.Disable;

            lup_Machine.SetDefault(false, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Machine.SetFontSize(new Font("맑은 고딕", 15f));

            var cultureIndex = DataConvert.GetCultureIndex();

            var TN_LOT_DTL = ModelService.GetChildList<TN_LOT_DTL>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && p.ProductLotNo == productLotNo).OrderBy(p => p.Seq).LastOrDefault();
            if (TN_LOT_DTL != null)
            {
                //lup_Machine.EditValue = TN_LOT_DTL.MachineCode;
                lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode;

                var dt = DbRequestHandler.GetDataTableSelect("EXEC USP_GET_POP_SRC_IN '" + TN_LOT_DTL.SrcInLotNo + "'");

                tx_UseSrcOutLotNo.EditValue = TN_LOT_DTL.SrcInLotNo;
                tx_UseSrcItemName.EditValue = dt.Rows[0][DataConvert.GetCultureDataFieldName("ItemName")].GetNullToEmpty();
                tx_UseSrcAvailableQty.EditValue = dt.Rows[0]["StockQty"].GetDecimalNullToZero().ToString("#,#.##"); 
            }

            //lcSrcOutLotNo1.Text = lcSrcOutLotNo.Text;
            //lcSrcItemName1.Text = lcSrcItemName.Text;
            //lcSrcAvailableQty1.Text = lcSrcAvailableQty.Text;
            McSrcLot();
            btn_Change.Text = LabelConvert.GetLabelText("Change");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
        }
        private void McSrcLot()
        {
            var mc = lup_Machine.EditValue.GetNullToEmpty();
            var UseSrcOutLotNo = tx_UseSrcOutLotNo.EditValue.GetNullToEmpty();
            gridEx1.DataSource = ModelService.GetChildList<VI_MCSRCLOT_STOCK>(p => p.MachineCode == mc&&p.OutQty>0&&p.SrcLotNo!= UseSrcOutLotNo).ToList();

            gridEx1.MainGrid.BestFitColumns();
        }
        //private void Tx_SrcOutLotNo_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        var useSrcOutLotNo = tx_UseSrcOutLotNo.EditValue.GetNullToEmpty().ToUpper();
        //     //   var srcOutLotNo = tx_SrcOutLotNo.EditValue.GetNullToEmpty().ToUpper();

        //        //if (useSrcOutLotNo == srcOutLotNo)
        //        //{
        //        //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_41), LabelConvert.GetLabelText("SrcOutLotNo")));
        //        //    tx_SrcItemName.EditValue = null;
        //        //    tx_SrcAvailableQty.EditValue = null;
        //        //    SrcItemCode = null;
        //        //    SrcOutLotNo = null;
        //        //    tx_SrcOutLotNo.EditValue = null;
        //        //    tx_SrcOutLotNo.Focus();
        //        //    return;
        //        //}

        //        var TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode).First();
        //        var srcItemCode = TN_STD1100.SrcCode;
        //        if (srcItemCode.IsNullOrEmpty())
        //        {
        //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_65));
        //            //tx_SrcItemName.EditValue = null;
        //            //tx_SrcAvailableQty.EditValue = null;
        //            SrcItemCode = null;
        //            SrcOutLotNo = null;
        //            //tx_SrcOutLotNo.EditValue = null;
        //            //tx_SrcOutLotNo.Focus();
        //            return;
        //        }

        //        var dt = DbRequestHandler.GetDataTableSelect("EXEC USP_GET_POP_SRC_IN '" + srcOutLotNo + "'");

        //        if (dt == null || dt.Rows.Count == 0)
        //        {
        //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("RawMaterialsAvailable")));

        //            SrcItemCode = null;
        //            SrcOutLotNo = null;

        //            return;
        //        }

        //        if (dt.Rows[0]["StockQty"].GetDecimalNullToZero() == 0)
        //        {
        //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("RawMaterialsAvailable")));

        //            SrcItemCode = null;
        //            SrcOutLotNo = null;

        //            return;
        //        }

        //        if (srcItemCode != dt.Rows[0]["ItemCode"].GetNullToEmpty())
        //        {
        //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_66));

        //            SrcItemCode = null;
        //            SrcOutLotNo = null;

        //            return;
        //        }

        //        var ItemCode = new System.Data.SqlClient.SqlParameter("@ItemCode", srcItemCode);
        //        var InLotNo = new System.Data.SqlClient.SqlParameter("@InLotNo", srcOutLotNo);
        //        var fifoDataSet = DbRequestHandler.GetDataSet("USP_GET_PUR_FIFO", ItemCode, InLotNo);
        //        if (fifoDataSet != null && fifoDataSet.Tables.Count > 0 && fifoDataSet.Tables[0].Rows.Count > 0)
        //        {
        //            MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_117));
        //            //MessageBoxHandler.Show("입력한 입고 LOT NO의 이전 LOT NO 재고가 남아 있습니다. 확인해 주시기 바랍니다.");

        //            SrcItemCode = null;
        //            SrcOutLotNo = null;

        //            return;
        //        }


        //        SrcItemCode = dt.Rows[0]["ItemCode"].GetNullToEmpty();
        //        SrcOutLotNo = dt.Rows[0]["OutLotNo"].GetNullToEmpty();
        //    }
        //}

        //private void Tx_SrcOutLotNo_Click(object sender, EventArgs e)
        //{
        //    if (!GlobalVariable.KeyPad) return;
        //    var keyPad = new XFCKEYPAD();
        //    if (keyPad.ShowDialog() != DialogResult.Cancel)
        //    {

        //        Tx_SrcOutLotNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        //    }
        //}

        private void Btn_Change_Click(object sender, EventArgs e)
        {
            if(gridEx1.MainGrid.MainView.RowCount <= 0)
            { 
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("ChangingSrcInfo")));
                return;
            }

            SrcOutLotNo = gridEx1.MainGrid.MainView.GetFocusedRowCellValue(gridEx1.MainGrid.MainView.Columns["SrcLotNo"]).ToString();
            SrcItemCode = gridEx1.MainGrid.MainView.GetFocusedRowCellValue(gridEx1.MainGrid.MainView.Columns["SrcItemCode"]).ToString();

            //if (SrcOutLotNo.IsNullOrEmpty())
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("ChangingSrcInfo")));
            //    return;
            //}

            var machineCode = lup_Machine.EditValue.GetNullToNull();

            //#region 자재출고 INSERT
            //var TN_PUR1201 = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == SrcOutLotNo).First();
            //TN_PUR1300 newobj = new TN_PUR1300()
            //{
            //    OutNo = DbRequestHandler.GetSeqMonth("OUT"),
            //    OutDate = DateTime.Today,
            //    OutId = GlobalVariable.LoginId,
            //    Memo = "POP자동출고",
            //};

            //var newDetailObj = new TN_PUR1301();
            //newDetailObj.OutNo = newobj.OutNo;
            //newDetailObj.OutSeq = newobj.TN_PUR1301List.Count == 0 ? 1 : newobj.TN_PUR1301List.Max(o => o.OutSeq) + 1;
            //newDetailObj.InNo = TN_PUR1201.InNo;
            //newDetailObj.InSeq = TN_PUR1201.InSeq;
            //newDetailObj.ItemCode = TN_PUR1201.ItemCode;
            //newDetailObj.OutQty = tx_SrcAvailableQty.EditValue.GetDecimalNullToZero();
            //newDetailObj.OutLotNo = SrcOutLotNo;
            //newDetailObj.InLotNo = SrcOutLotNo;
            //newDetailObj.InCustomerLotNo = TN_PUR1201.InCustomerLotNo;
            //newDetailObj.NewRowFlag = "Y";
            //newobj.TN_PUR1301List.Add(newDetailObj);

            //ModelService.InsertChild(newobj);
            //ModelService.Save();
            //#endregion
            
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
