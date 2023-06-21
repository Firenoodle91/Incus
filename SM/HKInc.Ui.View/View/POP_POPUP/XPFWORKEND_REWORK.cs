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
    public partial class XPFWORKEND_REWORK : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1205> ModelService = (IService<TN_MPS1205>)ProductionFactory.GetDomainService("TN_MPS1205");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;
        private string productLotNo;

        public XPFWORKEND_REWORK()
        {
            InitializeComponent();
        }

        public XPFWORKEND_REWORK(PopupDataParam parameter, PopupCallback callback) : this()
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
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            btn_ResultAdd.Text = LabelConvert.GetLabelText("ResultAdd");
            btn_WorkEnd.Text = LabelConvert.GetLabelText("WorkEnd");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
        }

        private void Btn_ResultAdd_Click(object sender, EventArgs e)
        {
            var obj = TEMP_XFPOP1000_Obj;
            if (obj == null) return;
            
            if(obj.MachineCode.GetNullToEmpty()!="")
            {
                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.KeyValue, obj);
                param.SetValue(PopupParameter.Value_1, productLotNo);
                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_REWORK, param, ResultAddCallback);
                form.ShowPopup(true);
            }
        }

        private void ResultAddCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, "SAVE");
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void Btn_WorkEnd_Click(object sender, EventArgs e)
        {
            ModelService.ReLoad();

            var obj = TEMP_XFPOP1000_Obj;

            var sumObj = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).ToList();

            //작업지시수량보다 총생산수량이 적을 경우 
            if (obj.WorkQty > sumObj.Sum(p => p.ResultSumQty).GetDecimalNullToZero())
            {
                if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_91), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.No)
                    //if (MessageBoxHandler.Show("지시수량보다 총 생산량이 부족합니다. 무시하고 종료하시겠습니까?", LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_30), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();
                var TN_MPS1205 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
                var ItemMoveLastObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == TN_MPS1205.ItemMoveNo).FirstOrDefault();

                //// 이동표번호가 없는 경우
                //if (ItemMoveLastObj == null)
                //{
                //    NewItemMovePrint(TN_MPS1205.OkSumQty.GetDecimalNullToZero(), obj, TN_MPS1200, TN_MPS1205);
                //}
                //// 이동표번호가 있으나 그 이후 새로 실적을 등록했을 경우
                //else if (TN_MPS1205.TN_MPS1206List.Any(p => p.ItemMoveNo == null))
                //{
                //    NewItemMovePrint(TN_MPS1205.OkSumQty.GetDecimalNullToZero() - ItemMoveLastObj.OkSumQty.GetDecimalNullToZero(), obj, TN_MPS1200, TN_MPS1205);
                //}
                //else
                //{
                    PopupDataParam param = new PopupDataParam();
                    TN_MPS1205.ResultDate = DateTime.Today;
                    TN_MPS1205.ResultEndDate = DateTime.Now;
                    ModelService.Update(TN_MPS1205);

                    //작업지시서 상태 변경
                    TN_MPS1200.ReworkJobStates = MasterCodeSTR.JobStates_ReworkEnd;
                    TN_MPS1200.UpdateTime = DateTime.Now;
                    ModelService.UpdateChild(TN_MPS1200);

                    //TN_TOOL1003T Delete
                    var toolList = ModelService.GetChildList<TN_TOOL1003>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).ToList();
                    foreach (var v in toolList)
                        ModelService.RemoveChild(v);
                    ModelService.Save();

                    param.SetValue(PopupParameter.Value_1, "SAVE");
                    ReturnPopupArgument = new PopupArgument(param);
                    ActClose();
                //}
            }
        }

        /// <summary>
        /// 이동표 새 출력 CallBack
        /// </summary>
        private void NewItemMovePrint(decimal boxInQty, TEMP_XFPOP1000 obj, TN_MPS1200 TN_MPS1200, TN_MPS1205 TN_MPS1205)
        {
            var itemMoveNo = DbRequestHandler.GetItemMoveSeq(obj.WorkNo);

            TN_MPS1205.ItemMoveNo = itemMoveNo;

            decimal resultQty = 0;
            decimal okQty = 0;
            decimal badQty = 0;

            foreach (var v in TN_MPS1205.TN_MPS1206List)
            {
                if (v.ItemMoveNo.IsNullOrEmpty())
                {
                    resultQty += v.ResultQty.GetDecimalNullToZero();
                    okQty += v.OkQty.GetDecimalNullToZero();
                    badQty += v.BadQty.GetDecimalNullToZero();
                    v.ItemMoveNo = itemMoveNo;
                }
            }

            ModelService.Update(TN_MPS1205);

            var newItemMoveNo = new TN_ITEM_MOVE();
            newItemMoveNo.ItemMoveNo = itemMoveNo;
            newItemMoveNo.WorkNo = TN_MPS1205.WorkNo;
            newItemMoveNo.ProcessCode = TN_MPS1205.ProcessCode;
            newItemMoveNo.ProcessSeq = TN_MPS1205.ProcessSeq;
            newItemMoveNo.ProductLotNo = TN_MPS1205.ProductLotNo;
            newItemMoveNo.BoxInQty = boxInQty;
            newItemMoveNo.ResultSumQty = TN_MPS1205.ResultSumQty;
            newItemMoveNo.OkSumQty = TN_MPS1205.OkSumQty;
            newItemMoveNo.BadSumQty = TN_MPS1205.BadSumQty;
            newItemMoveNo.ResultQty = resultQty;
            newItemMoveNo.OkQty = okQty;
            newItemMoveNo.BadQty = badQty;

            ModelService.InsertChild(newItemMoveNo);

            TN_MPS1205.ResultDate = DateTime.Today;
            TN_MPS1205.ResultEndDate = DateTime.Now;
            ModelService.Update(TN_MPS1205);

            //작업지시서 상태 변경
            TN_MPS1200.ReworkJobStates = MasterCodeSTR.JobStates_ReworkEnd;
            TN_MPS1200.UpdateTime = DateTime.Now;
            ModelService.UpdateChild(TN_MPS1200);

            //TN_TOOL1003T Delete
            var toolList = ModelService.GetChildList<TN_TOOL1003>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).ToList();
            foreach (var v in toolList)
                ModelService.RemoveChild(v);
            ModelService.Save();


            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "PRINT");
            param.SetValue(PopupParameter.Value_1, itemMoveNo);
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
