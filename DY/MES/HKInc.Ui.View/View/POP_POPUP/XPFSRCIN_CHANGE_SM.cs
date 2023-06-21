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
    public partial class XPFSRCIN_CHANGE_SM : Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_POP_WORK_START_BOM_CHECK> ModelService = (IService<VI_POP_WORK_START_BOM_CHECK>)ProductionFactory.GetDomainService("VI_POP_WORK_START_BOM_CHECK");
        IService<TN_STD1300> ModelServiceBom = (IService<TN_STD1300>)ProductionFactory.GetDomainService("TN_STD1300");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;
        string srcItemCode;
        string srcOutLotNo;
        string productLotNo;

        public XPFSRCIN_CHANGE_SM()
        {
            InitializeComponent();
        }

        public XPFSRCIN_CHANGE_SM(PopupDataParam parameter, PopupCallback callback) : this()
        {
            InitGrid();
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("SrcChange");
            this.Size = new Size(this.Size.Width, this.Size.Height - 80);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 80);

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);
            productLotNo = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            tx_OutLotNo.KeyDown += tx_OutLotNo_KeyDown;
            btn_Change.Click += Btn_Change_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

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
                try
                {
                    srcItemCode = null;
                    srcOutLotNo = null;

                    string outLotNo = tx_OutLotNo.EditValue.GetNullToEmpty();

                    if (outLotNo == "") return;

                    //부모BOM코드
                    var parentBomCode = ModelServiceBom.GetList(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode).FirstOrDefault().BomCode;

                    //자식BOM 리스트
                    var bomList = ModelServiceBom.GetList(p => p.ParentBomCode == parentBomCode && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode).ToList();

                    //출고LOTNO 확인
                    var outLotNoInfo = ModelService.GetList(p => p.OutLotNo == outLotNo).FirstOrDefault();
                    string lotNoItemCode = !outLotNoInfo.IsNullOrEmpty() ? outLotNoInfo.ItemCode : null;
                    
                    //잘못된 출고LOTNO
                    if (lotNoItemCode.IsNullOrEmpty())
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_127), LabelConvert.GetLabelText("OutLotNo")));
                        tx_OutLotNo.EditValue = null;
                        return;
                    }
                    else
                    {
                        int bomItemCnt = 0;
                        foreach (var v in bomList)
                        {
                            if (v.ItemCode == lotNoItemCode)
                                bomItemCnt++;
                        }

                        //BOM정보 없음
                        if(bomItemCnt == 0)
                        {
                            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("BomInfo")));
                            tx_OutLotNo.EditValue = null;
                            return;
                        }

                        //현재 투입LOTNO 확인
                        var nowSrcInLotNo = ModelService.GetChildList<TN_LOT_DTL>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && p.ProductLotNo == productLotNo && p.SrcCode == lotNoItemCode).OrderBy(p => p.Seq).LastOrDefault().SrcInLotNo;

                        if (nowSrcInLotNo == outLotNo)
                        {
                            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_129), LabelConvert.GetLabelText("OutLotNo")));
                            tx_OutLotNo.EditValue = null;
                            return;
                        }

                        //투입할 ITEM CODE
                        srcItemCode = lotNoItemCode;
                        //투입할 출고LOTNO
                        srcOutLotNo = outLotNo;
                    }

                }
                catch (Exception ex)
                {
                    MessageBoxHandler.ErrorShow(ex);
                    tx_OutLotNo.Focus();
                }
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
