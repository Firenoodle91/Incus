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
using System.Collections.Generic;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 20210614 오세완 차장
    /// 대영 스타일 원소재교체
    /// </summary>
    public partial class XPFSRCIN_CHANGE_DAEYOUNG : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_LOT_DTL> ModelService = (IService<TN_LOT_DTL>)ProductionFactory.GetDomainService("TN_LOT_DTL");
        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;
        string srcItemCode;
        string srcOutLotNo;
        string productLotNo;

        List<TEMP_XFPOP1000_WORKSTART_INFO> bomArr;
        #endregion

        public XPFSRCIN_CHANGE_DAEYOUNG()
        {
            InitializeComponent();
        }

        public XPFSRCIN_CHANGE_DAEYOUNG(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("SrcChange");
            this.Size = new Size(this.Size.Width, this.Size.Height - 80);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 80);

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);
            productLotNo = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            tx_OutLotNo.KeyDown += tx_OutLotNo_KeyDown;
            tx_OutLotNo.Click += Tx_OutLotNo_Click;
            btn_Change.Click += Btn_Change_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
            //Init_BomInfo();
        }

        private void Tx_OutLotNo_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad) return;
            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                tx_OutLotNo.EditValue = keyPad.returnval;
                Search_ChangeSrc();
            }
        }

        private void Init_BomInfo()
        {
            string sSql = "exec USP_GET_XFPOP1000_WORKSTART_INFO '" + TEMP_XFPOP1000_Obj.WorkNo + "', '" + TEMP_XFPOP1000_Obj.ProcessCode + "'";
            DataSet ds = DbRequestHandler.GetDataQury(sSql);
            if (ds != null)
            {
                if(ds.Tables.Count > 1)
                {
                    if(ds.Tables[1].Rows.Count > 0)
                    {
                        bomArr = new List<TEMP_XFPOP1000_WORKSTART_INFO>();
                        for(int i=0;i<ds.Tables[1].Rows.Count;i++)
                        {
                            TEMP_XFPOP1000_WORKSTART_INFO newTemp = new TEMP_XFPOP1000_WORKSTART_INFO()
                            {
                                TYPE = ds.Tables[1].Rows[i]["TYPE"].GetNullToEmpty(),
                                ITEM_CODE = ds.Tables[1].Rows[i]["ITEM_CODE"].GetNullToEmpty(),
                                ITEM_NAME = ds.Tables[1].Rows[i]["ITEM_NAME"].GetNullToEmpty(),
                                ITEM_NAME1 = ds.Tables[1].Rows[i]["ITEM_NAME1"].GetNullToEmpty(),
                                PROCESS_CODE = ds.Tables[1].Rows[i]["PROCESS_CODE"].GetNullToEmpty(),
                                USE_QTY = ds.Tables[1].Rows[i]["USE_QTY"].GetDecimalNullToZero(),
                                MG_FLAG = ds.Tables[1].Rows[i]["MG_FLAG"].GetNullToEmpty()
                            };
                            bomArr.Add(newTemp);
                        }
                    }
                }
            }
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        //원자재LotNo 스캔 시
        private void tx_OutLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_ChangeSrc();
            }
        }

        private void Search_ChangeSrc()
        {
            try
            {
                srcItemCode = null;
                srcOutLotNo = null;

                string sOutLotNo = tx_OutLotNo.EditValue.GetNullToEmpty();

                if (sOutLotNo == "")
                    return;

                bool bNotFound = false;
                // 20210614 오세완 차장 투입한 원소재의 정보
                string sSql = "exec USP_GET_POP_SRC_IN_V2 '" + sOutLotNo + "'";
                DataTable dt = DbRequestHandler.GetDataTableSelect(sSql);
                if (dt == null)
                {
                    bNotFound = true;
                }
                else if(dt.Rows.Count == 0)
                {
                    bNotFound = true;
                }
                else
                {
                    // 20210614 오세완 찾아 기투입된 원소재의 정보
                    List<TN_LOT_DTL> input_src_Arr = ModelService.GetList(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo &&
                                                                                p.ProductLotNo == productLotNo).ToList();
                    if (input_src_Arr == null)
                    {
                        bNotFound = true;
                    }
                    else if (input_src_Arr.Count == 0)
                    {
                        bNotFound = true;
                    }
                    else
                    {
                        decimal dOutQty = dt.Rows[0]["StockQty"].GetDecimalNullToZero();
                        if (dOutQty <= 0)
                        {
                            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_151));
                            tx_OutLotNo.EditValue = "";
                            btn_Change.Enabled = false;
                            return;
                        }

                        string sInput_itemcode = dt.Rows[0]["ItemCode"].GetNullToEmpty();
                        bool bBreak = false;
                        int iFoundStatus = 0;
                        foreach (TN_LOT_DTL each in input_src_Arr)
                        {
                            if (each.SrcCode == sInput_itemcode)
                            {
                                string sInput_lot = dt.Rows[0]["OutLotNo"].GetNullToEmpty();
                                if (each.SrcInLotNo == sInput_lot)
                                {
                                    iFoundStatus = 1;
                                    bBreak = true;
                                }
                                else
                                    iFoundStatus = 2;
                            }

                            if (bBreak)
                                break;
                        }

                        if (iFoundStatus == 0)
                        {
                            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_169)); // 20210702 오세완 차장 메시지 교체
                            //MessageBoxHandler.Show("기존에 투입한 원자재 / 반제품과 교체할 품목이 없습니다.");
                            tx_OutLotNo.EditValue = "";
                            btn_Change.Enabled = false;
                        }
                        else if (iFoundStatus == 1)
                        {
                            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_129), LabelConvert.GetLabelText("OutLotNo")));
                            tx_OutLotNo.EditValue = "";
                            btn_Change.Enabled = false;
                            return;
                        }
                        else
                        {
                            //투입할 ITEM CODE
                            srcItemCode = sInput_itemcode;
                            //투입할 출고LOTNO
                            srcOutLotNo = sOutLotNo;
                            btn_Change.Enabled = true;
                        }
                    }
                }

                if(bNotFound)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_161)); // 20210702 오세완 차장 메시지 교체
                    //MessageBoxHandler.Show("올바른 원자재 / 반제품이 아닙니다.");
                    tx_OutLotNo.EditValue = "";
                    btn_Change.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
                tx_OutLotNo.Focus();
            }
        }

        private void Btn_Change_Click(object sender, EventArgs e)
        {            
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, srcItemCode);
            param.SetValue(PopupParameter.Value_2, srcOutLotNo);
            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            ActClose();
        }
    }
}
