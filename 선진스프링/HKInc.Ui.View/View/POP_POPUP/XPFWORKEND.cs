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
        private string ProcTeamCode;
        private string itemMoveNo;

        public XPFWORKEND()
        {
            InitializeComponent();
        }

        public XPFWORKEND(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkEnd");

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);
            productLotNo = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            itemMoveNo = parameter.GetValue(PopupParameter.Value_2).GetNullToEmpty();
            ProcTeamCode = parameter.GetValue(PopupParameter.Value_3).GetNullToEmpty();

            btn_ResultAdd.Click += Btn_ResultAdd_Click;
            btn_ItemMovePrint.Click += Btn_ItemMovePrint_Click;
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
            btn_ItemMovePrint.Text = LabelConvert.GetLabelText("ItemMovePrint");
            btn_WorkEnd.Text = LabelConvert.GetLabelText("WorkEnd");
            btn_QualityAdd.Text = LabelConvert.GetLabelText("QualityAdd");
            btn_Pause.Text = LabelConvert.GetLabelText("Pause");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
        }

        private void Btn_ResultAdd_Click(object sender, EventArgs e)
        {
            var obj = TEMP_XFPOP1000_Obj;
            if (obj == null) return;
            
            var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();

            if (TN_MPS1200.ToolUseFlag == "Y")
            {
                if (obj.ProcessCode == MasterCodeSTR.Process_Heat)
                {
                    var checkObj = ModelService.GetChildList<TN_MPS1202>(p => p.WorkNo == obj.WorkNo
                                                                            && p.ProcessCode == obj.ProcessCode
                                                                            && p.ProcessSeq == obj.ProcessSeq
                                                                            && p.ProductLotNo == productLotNo
                                                                            && p.ItemMoveNo == itemMoveNo).FirstOrDefault();
                    if (checkObj == null)
                    {
                        PopupDataParam param = new PopupDataParam();
                        param.SetValue(PopupParameter.KeyValue, obj);
                        param.SetValue(PopupParameter.Value_1, productLotNo);
                        param.SetValue(PopupParameter.Value_2, itemMoveNo);
                        param.SetValue(PopupParameter.Value_3, ProcTeamCode);
                        IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_HEAT, param, ResultAddCallback);
                        form.ShowPopup(true);
                    }
                    else
                    {
                        MessageBoxHandler.Show("해당 이동표에 대해서 이미 실적을 등록하셨습니다.");
                    }
                }
                else
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.KeyValue, obj);
                    param.SetValue(PopupParameter.Value_1, productLotNo);
                    param.SetValue(PopupParameter.Value_2, itemMoveNo);
                    param.SetValue(PopupParameter.Value_3, ProcTeamCode);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_TOOL, param, ResultAddCallback);
                    form.ShowPopup(true);
                }
            }
            else
            {
                if (obj.ProcessCode == MasterCodeSTR.Process_Heat)
                {
                    var checkObj = ModelService.GetChildList<TN_MPS1202>(p => p.WorkNo == obj.WorkNo
                                                                            && p.ProcessCode == obj.ProcessCode
                                                                            && p.ProcessSeq == obj.ProcessSeq
                                                                            && p.ProductLotNo == productLotNo
                                                                            && p.ItemMoveNo == itemMoveNo).FirstOrDefault();
                    if (checkObj == null)
                    {
                        PopupDataParam param = new PopupDataParam();
                        param.SetValue(PopupParameter.KeyValue, obj);
                        param.SetValue(PopupParameter.Value_1, productLotNo);
                        param.SetValue(PopupParameter.Value_2, itemMoveNo);
                        param.SetValue(PopupParameter.Value_3, ProcTeamCode);
                        IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_HEAT, param, ResultAddCallback);
                        form.ShowPopup(true);
                    }
                    else
                    {
                        MessageBoxHandler.Show("해당 이동표에 대해서 이미 실적을 등록하셨습니다.");
                    }
                }
                else
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.KeyValue, obj);
                    param.SetValue(PopupParameter.Value_1, productLotNo);
                    param.SetValue(PopupParameter.Value_2, itemMoveNo);
                    param.SetValue(PopupParameter.Value_3, ProcTeamCode);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_DEFAULT, param, ResultAddCallback);
                    form.ShowPopup(true);
                }
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

        private void Btn_ItemMovePrint_Click(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, TEMP_XFPOP1000_Obj);
            param.SetValue(PopupParameter.Value_1, productLotNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVEPRINT, param, ItemMovePrintCallback);
            form.ShowPopup(true);
        }

        private void ItemMovePrintCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            var obj = TEMP_XFPOP1000_Obj;
            if (obj == null) return;

            var itemMoveNo = e.Map.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            if (itemMoveNo.IsNullOrEmpty()) return;

            TEMP_ITEM_MOVE_NO_MASTER masterObj;
            List<TEMP_ITEM_MOVE_NO_DETAIL> detailList;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", obj.WorkNo);
                masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER @WorkNo", _workNo).Where(p => p.ItemMoveNo == itemMoveNo).FirstOrDefault();
            }

            if (masterObj == null) return;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
                var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

                detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
            }

            var ItemMoveNoReport = new XRITEMMOVENO(masterObj, detailList);
            var printTool = new ReportPrintTool(ItemMoveNoReport);
            printTool.ShowPreview();

            if (!e.Map.ContainsKey(PopupParameter.KeyValue))
            {
                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Value_1, "SAVE");
                ReturnPopupArgument = new PopupArgument(param);
                ActClose();
            }
        }

        private void Btn_WorkEnd_Click(object sender, EventArgs e)
        {
            ModelService.ReLoad();

            var obj = TEMP_XFPOP1000_Obj;

            var preProcessObj = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessSeq == obj.ProcessSeq - 1).FirstOrDefault();
            if (preProcessObj != null)
            {
                if (preProcessObj.JobStates != MasterCodeSTR.JobStates_End && preProcessObj.JobStates != MasterCodeSTR.JobStates_OutEnd)
                {
                    MessageBoxHandler.Show("이전 공정에 대하여 작업이 완료되어 있지 않습니다. 확인 부탁드립니다.");
                    return;
                }
            }

            var sumObj = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).ToList();

            if (sumObj.Sum(p => p.ResultSumQty).GetDecimalNullToZero() <= 0)
            {
                MessageBoxHandler.Show("실적등록을 해주시기 바랍니다.");
                return;
            }

            //작업지시수량보다 총생산수량이 적을 경우 
            if (obj.WorkQty > sumObj.Sum(p => p.ResultSumQty).GetDecimalNullToZero())
            {
                if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_91), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.No)
                    //if (MessageBoxHandler.Show("지시수량보다 총 생산량이 부족합니다. 무시하고 종료하시겠습니까?", LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            //미확인 이동표 목록

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var WorkNo = new SqlParameter("@WorkNo", TEMP_XFPOP1000_Obj.WorkNo);
                var ProcessSeq = new SqlParameter("@ProcessSeq", TEMP_XFPOP1000_Obj.ProcessSeq);
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

            if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_30), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();
                var TN_MPS1201 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
                var ItemMoveLastObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == TN_MPS1201.ItemMoveNo).FirstOrDefault();

                // 이동표번호가 없는 경우
                if (ItemMoveLastObj == null)
                {
                    NewItemMovePrint(TN_MPS1201.OkSumQty.GetDecimalNullToZero(), obj, TN_MPS1200, TN_MPS1201);
                }
                // 이동표번호가 있으나 그 이후 새로 실적을 등록했을 경우
                else if (TN_MPS1201.TN_MPS1202List.Any(p => p.ItemMoveNo == null))
                {
                    NewItemMovePrint(TN_MPS1201.OkSumQty.GetDecimalNullToZero() - ItemMoveLastObj.OkSumQty.GetDecimalNullToZero(), obj, TN_MPS1200, TN_MPS1201);
                }
                else
                {
                    PopupDataParam param = new PopupDataParam();
                    TN_MPS1201.ResultDate = DateTime.Today;
                    TN_MPS1201.ResultEndDate = DateTime.Now;
                    ModelService.Update(TN_MPS1201);

                    //작업지시서 상태 변경
                    TN_MPS1200.JobStates = MasterCodeSTR.JobStates_End;
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
                }
            }
        }

        /// <summary>
        /// 이동표 새 출력 CallBack
        /// </summary>
        private void NewItemMovePrint(decimal boxInQty, TEMP_XFPOP1000 obj, TN_MPS1200 TN_MPS1200, TN_MPS1201 TN_MPS1201)
        {
            var itemMoveNo = DbRequestHandler.GetItemMoveSeq(obj.WorkNo);

            TN_MPS1201.ItemMoveNo = itemMoveNo;

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

            ModelService.Update(TN_MPS1201);

            var newItemMoveNo = new TN_ITEM_MOVE();
            newItemMoveNo.ItemMoveNo = itemMoveNo;
            newItemMoveNo.WorkNo = TN_MPS1201.WorkNo;
            newItemMoveNo.ProcessCode = TN_MPS1201.ProcessCode;
            newItemMoveNo.ProcessSeq = TN_MPS1201.ProcessSeq;
            newItemMoveNo.ProductLotNo = TN_MPS1201.ProductLotNo;
            newItemMoveNo.BoxInQty = boxInQty;
            newItemMoveNo.ResultSumQty = TN_MPS1201.ResultSumQty;
            newItemMoveNo.OkSumQty = TN_MPS1201.OkSumQty;
            newItemMoveNo.BadSumQty = TN_MPS1201.BadSumQty;
            newItemMoveNo.ResultQty = resultQty;
            newItemMoveNo.OkQty = okQty;
            newItemMoveNo.BadQty = badQty;

            ModelService.InsertChild(newItemMoveNo);

            TN_MPS1201.ResultDate = DateTime.Today;
            TN_MPS1201.ResultEndDate = DateTime.Now;
            ModelService.Update(TN_MPS1201);

            //작업지시서 상태 변경
            TN_MPS1200.JobStates = MasterCodeSTR.JobStates_End;
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

        private void Btn_QualityAdd_Click(object sender, EventArgs e)
        {
            var obj = TEMP_XFPOP1000_Obj;
            if (obj == null) return;

            var inspectionForm = new POP_POPUP.XPFINSPECTION(obj, productLotNo, ProcTeamCode);
            inspectionForm.ShowDialog();
        }

        private void Btn_Pause_Click(object sender, EventArgs e)
        {
            ModelService.ReLoad();

            var obj = TEMP_XFPOP1000_Obj;
            
            //var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();
            var TN_MPS1201 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
            //var ItemMoveLastObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == TN_MPS1201.ItemMoveNo).FirstOrDefault();
            
            PopupDataParam param = new PopupDataParam();
            //    TN_MPS1201.ResultDate = DateTime.Today;
            //    TN_MPS1201.ResultEndDate = DateTime.Now;
            //    ModelService.Update(TN_MPS1201);

            var TN_MPS1203_New = new TN_MPS1203()
            {
                //WorkNo = TN_MPS1201.WorkNo,
                //ProcessCode = TN_MPS1201.ProcessCode,
                //ProcessSeq = TN_MPS1201.ProcessSeq,
                //ProductLotNo = TN_MPS1201.ProductLotNo,
                //MachineCode = TN_MPS1201.MachineCode,
                PauseStartDate = DateTime.Now
            };
            TN_MPS1201.TN_MPS1203List.Add(TN_MPS1203_New);

            //ModelService.InsertChild(new TN_MPS1203()
            //{
            //    WorkNo = TN_MPS1201.WorkNo,
            //    ProcessCode = TN_MPS1201.ProcessCode,
            //    ProcessSeq = TN_MPS1201.ProcessSeq,
            //    ProductLotNo = TN_MPS1201.ProductLotNo,
            //    MachineCode = TN_MPS1201.MachineCode,
            //    PauseStartDate = DateTime.Now
            //});

            //작업지시서 상태 변경
            TN_MPS1201.TN_MPS1200.JobStates = MasterCodeSTR.JobStates_Pause;
            TN_MPS1201.TN_MPS1200.UpdateTime = DateTime.Now;
            //TN_MPS1200.JobStates = MasterCodeSTR.JobStates_Pause;
            //TN_MPS1200.UpdateTime = DateTime.Now;
            //ModelService.UpdateChild(TN_MPS1200);
            ModelService.Save();

            param.SetValue(PopupParameter.Value_1, "SAVE");
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }
    }
}
