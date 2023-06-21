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
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using System.Data.SqlClient;
using System.Collections.Generic;
using HKInc.Ui.View.View.REPORT;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 작업종료
    /// </summary>
    public partial class XPFWORKEND : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;
        private string productLotNo;

        public XPFWORKEND()
        {
            InitializeComponent();
        }

        public XPFWORKEND(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkEnd");

            this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);
            productLotNo = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            btn_ResultAdd.Click += Btn_ResultAdd_Click;
            btn_WorkEnd.Click += Btn_WorkEnd_Click; 
            btn_Cancel.Click += Btn_Cancel_Click;
            btn_QualityAdd.Click += Btn_QualityAdd_Click;
            btn_Pause.Click += Btn_Pause_Click;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            btn_ResultAdd.Text = LabelConvert.GetLabelText("ResultAdd");
            btn_WorkEnd.Text = LabelConvert.GetLabelText("WorkEnd");
            btn_QualityAdd.Text = LabelConvert.GetLabelText("QualityAdd");
            btn_Pause.Text = LabelConvert.GetLabelText("Pause");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
        }

        /// <summary>
        /// 실적 등록
        /// </summary>
        private void Btn_ResultAdd_Click(object sender, EventArgs e)
        {
            var obj = TEMP_XFPOP1000_Obj;
            if (obj == null)
                return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.Value_1, productLotNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_DEFAULT, param, ResultAddCallback);
            form.ShowPopup(true);
        }

        private void ResultAddCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, "SAVE");
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        /// <summary>
        /// 작업종료
        /// </summary>
        private void Btn_WorkEnd_Click(object sender, EventArgs e)
        {
            ModelService.ReLoad();

            var obj = TEMP_XFPOP1000_Obj;

            var preProcessObj = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessSeq == obj.ProcessSeq - 1).FirstOrDefault();
            if (preProcessObj != null)
            {
                if ((preProcessObj.JobStates != MasterCodeSTR.JobStates_End) && (preProcessObj.JobStates != MasterCodeSTR.JobStates_OutEnd))
                {
                    MessageBoxHandler.Show("이전 공정에 대하여 작업이 완료되어 있지 않습니다. 확인 부탁드립니다.");
                    return;
                }
            }

            var sumObj = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).ToList();

            //작업지시수량보다 총생산수량이 적을 경우 
            if (obj.WorkQty > sumObj.Sum(p => p.ResultSumQty).GetDecimalNullToZero())
            {
                if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_91), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_30), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();
                var TN_MPS1201 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
                var ItemMoveLastObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == TN_MPS1201.ItemMoveNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).FirstOrDefault();

                // 이동표번호가 없는 경우
                if (ItemMoveLastObj == null)
                {
                    var itemMoveNo = TN_MPS1201.ItemMoveNo == null ? DbRequestHandler.GetItemMoveSeq(obj.WorkNo) : TN_MPS1201.ItemMoveNo;

                    decimal resultQty = 0;
                    decimal okQty = 0;
                    decimal badQty = 0;

                    foreach (var v in TN_MPS1201.TN_MPS1202List)
                    {
                        if (v.ItemMoveNo.IsNullOrEmpty())
                        {
                            resultQty += v.ResultQty.GetDecimalNullToZero();
                            okQty += v.OkQty.GetDecimalNullToZero();
                            badQty += v.BadQty.GetDecimalNullToZero();
                            v.ItemMoveNo = itemMoveNo;
                        }
                    }

                    var newItemMoveNo = new TN_ITEM_MOVE();
                    newItemMoveNo.ItemMoveNo = itemMoveNo;
                    newItemMoveNo.WorkNo = TN_MPS1201.WorkNo;
                    newItemMoveNo.ProcessCode = TN_MPS1201.ProcessCode;
                    newItemMoveNo.ProcessSeq = TN_MPS1201.ProcessSeq;
                    newItemMoveNo.ProductLotNo = TN_MPS1201.ProductLotNo;
                    newItemMoveNo.BoxInQty = TN_MPS1201.OkSumQty.GetDecimalNullToZero();
                    newItemMoveNo.ResultSumQty = TN_MPS1201.ResultSumQty;
                    newItemMoveNo.OkSumQty = TN_MPS1201.OkSumQty;
                    newItemMoveNo.BadSumQty = TN_MPS1201.BadSumQty;
                    newItemMoveNo.ResultQty = resultQty;
                    newItemMoveNo.OkQty = okQty;
                    newItemMoveNo.BadQty = badQty;

                    ModelService.InsertChild(newItemMoveNo);
                }
                
                PopupDataParam param = new PopupDataParam();
                TN_MPS1201.ResultDate = DateTime.Today;
                TN_MPS1201.ResultEndDate = DateTime.Now;
                ModelService.Update(TN_MPS1201);

                //작업지시서 상태 변경
                TN_MPS1200.JobStates = MasterCodeSTR.JobStates_End;
                TN_MPS1200.UpdateTime = DateTime.Now;
                ModelService.UpdateChild(TN_MPS1200);

                ModelService.Save();

                param.SetValue(PopupParameter.Value_1, "SAVE");
                param.SetValue(PopupParameter.Value_2, TEMP_XFPOP1000_Obj);
                ReturnPopupArgument = new PopupArgument(param);
                ActClose();
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            ActClose();
        }

        private void Btn_QualityAdd_Click(object sender, EventArgs e)
        {
            var obj = TEMP_XFPOP1000_Obj;
            if (obj == null) return;

            var inspectionForm = new POP_POPUP.XPFINSPECTION(obj, productLotNo);
            inspectionForm.ShowDialog();
        }

        private void Btn_Pause_Click(object sender, EventArgs e)
        {
            ModelService.ReLoad();

            var obj = TEMP_XFPOP1000_Obj;
            
            var TN_MPS1201 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
            
            PopupDataParam param = new PopupDataParam();

            var TN_MPS1203_New = new TN_MPS1203() { PauseStartDate = DateTime.Now };
            TN_MPS1201.TN_MPS1203List.Add(TN_MPS1203_New);

            //작업지시서 상태 변경
            TN_MPS1201.TN_MPS1200.JobStates = MasterCodeSTR.JobStates_Pause;
            TN_MPS1201.TN_MPS1200.UpdateTime = DateTime.Now;
            ModelService.Save();

            param.SetValue(PopupParameter.Value_1, "SAVE");
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }
    }
}
