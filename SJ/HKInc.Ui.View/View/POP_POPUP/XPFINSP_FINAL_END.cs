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
using HKInc.Ui.Model.Domain;
using DevExpress.XtraEditors.Mask;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 최종검사 작업완료
    /// </summary>
    public partial class XPFINSP_FINAL_END : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1200> ModelService = (IService<TN_MPS1200>)ProductionFactory.GetDomainService("TN_MPS1200");

        TEMP_XFPOP_INSP_FINAL TEMP_XFPOP_INSP_FINAL;

        public XPFINSP_FINAL_END()
        {
            InitializeComponent();
        }

        public XPFINSP_FINAL_END(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkEnd");

            TEMP_XFPOP_INSP_FINAL = (TEMP_XFPOP_INSP_FINAL)PopupParam.GetValue(PopupParameter.KeyValue);
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Save, true);
            SetToolbarButtonCaption(ToolbarButton.Save, LabelConvert.GetLabelText("WorkEnd") + "[F5]", IconImageList.GetIconImage("actions/apply"));
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
        }

        protected override void DataSave()
        {
            var sumObj = ModelService.GetChildList<TN_MPS1201>(p => p.WorkNo == TEMP_XFPOP_INSP_FINAL.WorkNo && p.ProcessCode == TEMP_XFPOP_INSP_FINAL.ProcessCode && p.ProcessSeq == TEMP_XFPOP_INSP_FINAL.ProcessSeq).ToList();
            if (sumObj.Sum(p => p.ResultSumQty).GetDecimalNullToZero() == 0)
            {
                MessageBoxHandler.Show("실적등록을 해주시기 바랍니다.");
                return;
            }

            //미확인 이동표 목록
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var WorkNo = new SqlParameter("@WorkNo", TEMP_XFPOP_INSP_FINAL.WorkNo);
                var ProcessSeq = new SqlParameter("@ProcessSeq", TEMP_XFPOP_INSP_FINAL.ProcessSeq);
                var result = context.Database.SqlQuery<TEMP_NOT_USE_ITEM_MOVE_NO>("USP_GET_NOT_USE_ITEM_MOVE_NO @WorkNo,@ProcessSeq", WorkNo, ProcessSeq).ToList();
                if (result.Count > 0)
                {
                    var msg = "↓미사용 이동표 번호가 존재합니다↓" + Environment.NewLine;

                    foreach (var v in result.ToList())
                    {
                        msg += "　　　" + v.ItemMoveNo + Environment.NewLine;
                    }

                    MessageBoxHandler.Show(msg);
                    return;
                }
            }

            //var customerLotNo = tx_CustomerLotNo.EditValue.GetNullToNull();

            PopupDataParam param = new PopupDataParam();
            //param.SetValue(PopupParameter.ReturnObject, customerLotNo);
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }
    }
}
