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
    /// 20210613 오세완 차장 
    /// 대영 스타일 작업종료
    /// </summary>
    public partial class XPFWORKEND_V2 : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;
        private string productLotNo;

        /// <summary>
        /// 20210903 오세완 차장 실적수량
        /// </summary>
        private decimal ResultSumQty = 0; 

        /// <summary>
        /// 20210903 오세완 차장 
        /// 박스에 포장되지 못한 제품 수량
        /// </summary>
        private decimal Rest_ResultSumQty = 0;

        /// <summary>
        /// 20210903 오세완 차장 프레스 POP에서 부품이동표 출력 요청을 알기 위함
        /// </summary>
        private string gs_ItemMoveType = "";

        /// <summary>
        /// 20210903 오세완 차장 
        /// plc pop로부터 호출 여부 확인
        /// </summary>
        private bool gs_PLC_Pop = false;
        #endregion

        public XPFWORKEND_V2()
        {
            InitializeComponent();
        }

        public XPFWORKEND_V2(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkEnd");

            this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);
            productLotNo = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            // 20210903 오세완 차장 프레스 POP에서 부품이동표 출력 요청을 알기 위함
            ResultSumQty = parameter.GetValue(PopupParameter.Value_2).GetDecimalNullToZero();
            Rest_ResultSumQty = parameter.GetValue(PopupParameter.Value_4).GetDecimalNullToZero();
            
            string sParam_3 = parameter.GetValue(PopupParameter.Value_3).GetNullToEmpty();
            if (sParam_3 != "")
                gs_PLC_Pop = true;

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

        /// <summary>
        /// 20210613 오세완 차장
        /// XPFWORKEND에 있는 적용되지 않는 로직을 들어냄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ResultAdd_Click(object sender, EventArgs e)
        {
            var obj = TEMP_XFPOP1000_Obj;
            if (obj == null)
                return;
            
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.Value_1, productLotNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_DEFAULT_V2, param, ResultAddCallback);
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

        private void Btn_ItemMovePrint_Click(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, TEMP_XFPOP1000_Obj);
            param.SetValue(PopupParameter.Value_1, productLotNo);

            // 20210905 오세완 차장 프레스 공정 부품이동표 발행과 동일한 기능하게 추가
            if(gs_PLC_Pop)
            {
                param.SetValue(PopupParameter.Value_2, ResultSumQty);
                param.SetValue(PopupParameter.Value_3, "Press");
                param.SetValue(PopupParameter.Value_4, Rest_ResultSumQty);
            }
            
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

            string sKeyValue = e.Map.GetValue(PopupParameter.KeyValue).GetNullToEmpty();

            TEMP_ITEM_MOVE_NO_MASTER masterObj = null;
            List<TEMP_ITEM_MOVE_NO_DETAIL> detailList;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", obj.WorkNo);
                // 20211124 오세완 차장 2공정 이후에 이동표를 새로 발행하면 발행하기 전에 대한 실적을 출력해야 한다. 
                if (obj.ProcessSeq > 1)
                {
                    if (sKeyValue != "Re")
                        itemMoveNo = e.Map.GetValue(PopupParameter.Value_2).GetNullToEmpty();
                }

                var _ItemMoveNo = new SqlParameter("@ItemMoveNo", itemMoveNo);
                //masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER_V2 @WorkNo", _workNo).Where(p => p.ItemMoveNo == itemMoveNo).FirstOrDefault(); // 20210616 오세완 차장 수동으로 내린 작업지시의 부품이동표 출력을 위해서 수정처리
                //masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER_V2 @WorkNo, @ProcessSeq", _workNo, _ProcessSeq).Where(p => p.ItemMoveNo == itemMoveNo).FirstOrDefault(); // 20211123 오세완 차장 2공정 이상 이동표 분할 가능한 버전 교체
                var vResult = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER_V3 @WorkNo, @ItemMoveNo", _workNo, _ItemMoveNo).ToList(); // 20211124 오세완 차장 2공정 이후에도 이동표가 분할이 되는 버전 교체
                if (vResult != null)
                    masterObj = vResult.FirstOrDefault();
            }

            if (masterObj == null) return;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
                var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

                //detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
                detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL_V2 @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList(); // 20211124 오세완 차장 작업자가 제대로 표시되지 않아서 개선된 버전으로 사용
            }

            //var ItemMoveNoReport = new XRITEMMOVENO(masterObj, detailList);
            // 20210618 오세완 차장 유미대리 요청으로 A4크기 품목명도 나오는 부품이동표로 변경
            //var ItemMoveNoReport = new XRITEMMOVENO_DAEYOUNG(masterObj, detailList);
            //var printTool = new ReportPrintTool(ItemMoveNoReport);
            //printTool.ShowPreview();

            // 20210905 오세완 차장 부품이동표 양식 추가한 로직으로 변경
            string sMoveType = e.Map.GetValue(PopupParameter.Value_2).GetNullToEmpty();
            if (!gs_PLC_Pop)
                sMoveType = "A4"; // 20211125 오세완 차장 일반 공정은 A4형식으로 고정

            if(obj.ProcessSeq > 1 && sKeyValue == "")
            {
                // 20211124 오세완 차장 1공정 이후 새로 발행하기 위해 입력한 공정박스수량이 새로운 부품이동표에 기록이 되어서 로직을 추가 처리
                decimal dNew_BoxInQty = e.Map.GetValue(PopupParameter.Value_3).GetDecimalNullToZero();
                if (dNew_BoxInQty <= 1)
                {
                    if (sMoveType == "" || sMoveType == "A4")
                    {
                        var ItemMoveNoReport2 = new XRITEMMOVENO_DAEYOUNG(masterObj, detailList);
                        var printTool2 = new ReportPrintTool(ItemMoveNoReport2);
                        printTool2.ShowPreview();
                    }
                    else
                    {
                        TEMP_ITEM_MOVE_NO_DETAIL tempDetail = detailList.Where(p => p.ProcessCode == MasterCodeSTR.Process_Press).FirstOrDefault();
                        if (tempDetail != null)
                        {
                            decimal dPerBoxQty = 0;
                            string sPrintType = e.Map.GetValue(PopupParameter.KeyValue).GetNullToEmpty(); // // 20210830 오세완 차장 공정박스당 수량 재발생시 처리
                            if (sPrintType == "Re")
                            {
                                string sSql = "EXEC USP_GET_PLC_POP_BARCODE_PER_BOX_QTY '" + masterObj.ItemMoveNo + "' ";
                                string sResult = DbRequestHandler.GetCellValue(sSql, 0);
                                dPerBoxQty = sResult.GetDecimalNullToZero();
                            }
                            else
                                dPerBoxQty = e.Map.GetValue(PopupParameter.Value_3).GetDecimalNullToZero(); // 20210830 오세완 차장 공정박스당 수량 출력

                            var vBarReprot = new XRITEMMOVEDOC_PRESS(masterObj.ItemMoveNo, masterObj.ItemCode, dPerBoxQty);
                            var vPrinttool = new ReportPrintTool(vBarReprot);
                            vPrinttool.ShowPreview();
                        }
                    }
                        
                }
                else
                {
                    if (sMoveType == "" || sMoveType == "A4")
                    {
                        var printMulti2 = new REPORT.XRITEMMOVENO_DAEYOUNG();
                        for (int i = 0; i < Convert.ToInt32(dNew_BoxInQty); i++)
                        {
                            var printEach2 = new REPORT.XRITEMMOVENO_DAEYOUNG(masterObj, detailList);
                            printEach2.CreateDocument();
                            printMulti2.Pages.AddRange(printEach2.Pages);
                        }

                        printMulti2.PrintingSystem.ShowMarginsWarning = false;
                        printMulti2.ShowPrintStatusDialog = false;
                        printMulti2.ShowPreview();
                    }
                    else
                    {
                        var vPrintmulti = new XRITEMMOVEDOC_PRESS();
                        for (int j = 0; j < Convert.ToInt32(dNew_BoxInQty); j++)
                        {
                            TEMP_ITEM_MOVE_NO_DETAIL tempDetail1 = detailList.Where(p => p.ProcessCode == MasterCodeSTR.Process_Press).FirstOrDefault();
                            if (tempDetail1 != null)
                            {
                                //var vPrinteach = new XRITEMMOVEDOC_PRESS(masterObj);
                                decimal dPerBoxQty1 = 0;
                                string sPrintType = e.Map.GetValue(PopupParameter.KeyValue).GetNullToEmpty(); // // 20210830 오세완 차장 공정박스당 수량 재발생시 처리
                                if (sPrintType == "Re")
                                {
                                    string sSql = "EXEC USP_GET_PLC_POP_BARCODE_PER_BOX_QTY '" + masterObj.ItemMoveNo + "' ";
                                    string sResult = DbRequestHandler.GetCellValue(sSql, 0);
                                    dPerBoxQty1 = sResult.GetDecimalNullToZero();
                                }
                                else
                                    dPerBoxQty1 = e.Map.GetValue(PopupParameter.Value_3).GetDecimalNullToZero(); // 20210830 오세완 차장 공정박스당 수량 출력

                                var vPrinteach = new XRITEMMOVEDOC_PRESS(masterObj.ItemMoveNo, masterObj.ItemCode, dPerBoxQty1);
                                vPrinteach.CreateDocument();
                                vPrintmulti.Pages.AddRange(vPrinteach.Pages);
                            }
                        }

                        vPrintmulti.PrintingSystem.ShowMarginsWarning = false;
                        vPrintmulti.ShowPrintStatusDialog = false;
                        vPrintmulti.ShowPreview();
                    }
                        
                }
            }
            else
            {
                if (masterObj.BoxInQty <= 1)
                {
                    if (sMoveType == "" || sMoveType == "A4")
                    {
                        // 20210618 오세완 차장 유미대리 요청으로 A4크기 품목명도 나오는 부품이동표로 변경
                        var ItemMoveNoReport = new XRITEMMOVENO_DAEYOUNG(masterObj, detailList);
                        var printTool = new ReportPrintTool(ItemMoveNoReport);
                        printTool.ShowPreview();
                    }
                    else
                    {
                        // 20210820 오세완 차장 대영 요청으로 프레스 공정만 라벨형태로 공정이동표 발행 처리
                        // 20210827 오세완 차장 수량 출력까지 고려
                        //var vBarReprot = new XRITEMMOVEDOC_PRESS(masterObj);

                        TEMP_ITEM_MOVE_NO_DETAIL tempDetail = detailList.Where(p => p.ProcessCode == MasterCodeSTR.Process_Press).FirstOrDefault();
                        if (tempDetail != null)
                        {
                            decimal dPerBoxQty = 0;
                            string sPrintType = e.Map.GetValue(PopupParameter.KeyValue).GetNullToEmpty(); // // 20210830 오세완 차장 공정박스당 수량 재발생시 처리
                            if (sPrintType == "Re")
                            {
                                string sSql = "EXEC USP_GET_PLC_POP_BARCODE_PER_BOX_QTY '" + masterObj.ItemMoveNo + "' ";
                                string sResult = DbRequestHandler.GetCellValue(sSql, 0);
                                dPerBoxQty = sResult.GetDecimalNullToZero();
                            }
                            else
                                dPerBoxQty = e.Map.GetValue(PopupParameter.Value_3).GetDecimalNullToZero(); // 20210830 오세완 차장 공정박스당 수량 출력

                            var vBarReprot = new XRITEMMOVEDOC_PRESS(masterObj.ItemMoveNo, masterObj.ItemCode, dPerBoxQty);
                            var vPrinttool = new ReportPrintTool(vBarReprot);
                            vPrinttool.ShowPreview();
                        }
                    }
                }
                else
                {
                    if (sMoveType == "" || sMoveType == "A4")
                    {
                        // 20210618 오세완 차장 유미대리 요청으로 A4크기 품목명도 나오는 부품이동표로 변경
                        var ItemMoveNoReport = new XRITEMMOVENO_DAEYOUNG(masterObj, detailList);
                        var printTool = new ReportPrintTool(ItemMoveNoReport);
                        printTool.ShowPreview();
                    }
                    else
                    {
                        // 20210820 오세완 차장 대영 요청으로 프레스 공정만 라벨형태로 공정이동표 발행 처리
                        var vPrintmulti = new XRITEMMOVEDOC_PRESS();
                        for (int j = 0; j < Convert.ToInt32(masterObj.BoxInQty); j++)
                        {
                            TEMP_ITEM_MOVE_NO_DETAIL tempDetail1 = detailList.Where(p => p.ProcessCode == MasterCodeSTR.Process_Press).FirstOrDefault();
                            if (tempDetail1 != null)
                            {
                                //var vPrinteach = new XRITEMMOVEDOC_PRESS(masterObj);
                                decimal dPerBoxQty1 = 0;
                                string sPrintType = e.Map.GetValue(PopupParameter.KeyValue).GetNullToEmpty(); // // 20210830 오세완 차장 공정박스당 수량 재발생시 처리
                                if (sPrintType == "Re")
                                {
                                    string sSql = "EXEC USP_GET_PLC_POP_BARCODE_PER_BOX_QTY '" + masterObj.ItemMoveNo + "' ";
                                    string sResult = DbRequestHandler.GetCellValue(sSql, 0);
                                    dPerBoxQty1 = sResult.GetDecimalNullToZero();
                                }
                                else
                                    dPerBoxQty1 = e.Map.GetValue(PopupParameter.Value_3).GetDecimalNullToZero(); // 20210830 오세완 차장 공정박스당 수량 출력

                                var vPrinteach = new XRITEMMOVEDOC_PRESS(masterObj.ItemMoveNo, masterObj.ItemCode, dPerBoxQty1);
                                vPrinteach.CreateDocument();
                                vPrintmulti.Pages.AddRange(vPrinteach.Pages);
                            }
                        }

                        vPrintmulti.PrintingSystem.ShowMarginsWarning = false;
                        vPrintmulti.ShowPrintStatusDialog = false;
                        vPrintmulti.ShowPreview();
                    }
                }
            }
            

            if (!e.Map.ContainsKey(PopupParameter.KeyValue))
            {
                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Value_1, "SAVE");
                ReturnPopupArgument = new PopupArgument(param);
                ActClose();
            }
        }

        /// <summary>
        /// 20210613 오세완 차장
        /// XPFWORKEND과 다른 접은 TN_TOOL1003 delete 를 제거
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    //if (MessageBoxHandler.Show("지시수량보다 총 생산량이 부족합니다. 무시하고 종료하시겠습니까?", LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_30), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();
                var TN_MPS1201 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();

                if (gs_PLC_Pop)
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.Value_1, obj.ProcessPackQty);
                    //decimal dMaxQty = ResultSumQty + TN_MPS1201.WorkerInputResultQty.GetDecimalNullToZero() + Rest_ResultSumQty;
                    // 20210908 오세완 차장 수동입력 수치는 이미 반영되어 있음, 또한 나머지 수량을 더해서 발행하는 로직 제거, 대신 나머지 수량을 +1하여 남는 수량으로 발행하는 로직으로 변경(김이사님 지시)
                    decimal dMaxQty = ResultSumQty;
                    param.SetValue(PopupParameter.Value_2, dMaxQty);

                    // 20211006 오세완 차장 실수로 내린 작업지시를 시작한 경우에 대비하여 무실적으로 작업완료를 할 수 있는 조건을 검색 이동표가 없으며 실적이 0인 경으로 간주
                    if(ResultSumQty == 0)
                    {
                        TN_ITEM_MOVE itemMove_First = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == TN_MPS1201.ItemMoveNo &&
                                                                                             (p.Temp1 == "" || p.Temp1 == null)).FirstOrDefault();
                        if (itemMove_First == null)
                            param.SetValue(PopupParameter.Value_3, "NoQty");
                    }

                    IPopupForm form1 = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVEPRINT_BOX_V2, param, NewPrintCallback_Press);
                    form1.ShowPopup(true);
                }
                else
                {
                    // 20210905 오세완 차장 기존 로직은 프레스 공정이 아닌 경우는 유지
                    var ItemMoveLastObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == TN_MPS1201.ItemMoveNo &&
                                                                                       (p.Temp1 == "" || p.Temp1 == null)).FirstOrDefault(); // 20210615 오세완 차장 리워크는 부품이동표 발행하지 않기 위해 수정

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
                        ModelService.Save();

                        if(obj.ProcessSeq > 1)
                        {
                            if(ItemMoveLastObj.ResultQty == 0)
                                param.SetValue(PopupParameter.Value_1, "SAVE");
                            else
                            {
                                param.SetValue(PopupParameter.Constraint, "PRINT");
                                param.SetValue(PopupParameter.Value_1, ItemMoveLastObj.ItemMoveNo);
                            }
                        }
                        else 
                            param.SetValue(PopupParameter.Value_1, "SAVE");

                        param.SetValue(PopupParameter.Value_4, "WorkEnd"); // 20210914 오세완 차장 작업완료를 명확히 해서 수동관리 여부가 아니 건에 대해 자재 차감행위 제어
                        ReturnPopupArgument = new PopupArgument(param);
                        ActClose();
                    }
                }
            }
        }

        /// <summary>
        /// 20210914 오세완 차장 프레스 공정인 경우 작업완료 전에 이동표 발행 방법을 다르게 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPrintCallback_Press(object sender, PopupArgument e)
        {
            if (e == null)
                return;

            // 20210915 오세완 차장 신부장님 요청으로 작업종료시 이동표 발행을 취소한 경우는 재입력을 위해 아무것도 처리 하지 않는 것으로...
            string sEndType = e.Map.GetValue(PopupParameter.KeyValue).GetNullToEmpty();
            if (sEndType == "Cancel")
                return;

            decimal dBoxInQty = e.Map.GetValue(PopupParameter.Value_1).GetDecimalNullToZero();
            decimal dPerBoxQty = e.Map.GetValue(PopupParameter.Value_3).GetDecimalNullToZero();

            string sNew_Item_move_no = DbRequestHandler.GetItemMoveSeq(TEMP_XFPOP1000_Obj.WorkNo);
            int iItem_move_no_seq;
            int.TryParse(sNew_Item_move_no.Right(3), out iItem_move_no_seq);

            TN_MPS1201 temp_Result_1201 = ModelService.GetList(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo &&
                                                                    p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode &&
                                                                    p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq &&
                                                                    p.ResultEndDate == null).LastOrDefault();

            if (temp_Result_1201 != null)
            {
                temp_Result_1201.ItemMoveNo = sNew_Item_move_no;

                if (iItem_move_no_seq > 1)
                {
                    // 20210901 오세완 차장 공정이동표 발생시 계산되었던 실적으로 tn_item_move후에 mps1202에 insert
                    if (temp_Result_1201.TN_MPS1202List != null)
                    {
                        if (temp_Result_1201.TN_MPS1202List.Count > 0)
                        {
                            foreach (TN_MPS1202 each in temp_Result_1201.TN_MPS1202List)
                            {
                                if (each.ItemMoveNo.IsNullOrEmpty())
                                {
                                    each.ItemMoveNo = sNew_Item_move_no;
                                }
                            }

                            temp_Result_1201.UpdateTime = DateTime.Now;
                            temp_Result_1201.UpdateId = GlobalVariable.LoginId;
                            ModelService.Update(temp_Result_1201);

                            // 20210908 오세완 차장 수동실적이 이미 포함이 되어 있기 때문에 생락
                            //decimal dMaxQty = ResultSumQty + temp_Result_1201.WorkerInputResultQty.GetDecimalNullToZero();

                            // 부품 이동표 생성
                            TN_ITEM_MOVE new_Itemmove = new TN_ITEM_MOVE()
                            {
                                ItemMoveNo = sNew_Item_move_no,
                                WorkNo = temp_Result_1201.WorkNo,
                                ProcessCode = temp_Result_1201.ProcessCode,
                                ProcessSeq = temp_Result_1201.ProcessSeq,
                                ProductLotNo = temp_Result_1201.ProductLotNo,
                                BoxInQty = dBoxInQty,
                                ResultSumQty = temp_Result_1201.ResultSumQty,
                                OkSumQty = temp_Result_1201.OkSumQty,
                                BadSumQty = temp_Result_1201.BadSumQty,
                                ResultQty = ResultSumQty,
                                OkQty = ResultSumQty,
                                //ResultQty = dMaxQty, // 20210908 오세완 차장 수동실적이 이미 포함이 되어 있기 때문에 생락
                                //OkQty = dMaxQty,
                                Temp = dPerBoxQty.ToString()
                            };
                            ModelService.InsertChild<TN_ITEM_MOVE>(new_Itemmove);

                            ModelService.Save();
                        }
                    }
                }
                else
                {
                    // 20210901 오세완 차장 tn_item_move에 생성 후 mps1202에 insert
                    decimal dTemp_ResultQty = 0m;
                    decimal dTemp_OkQty = 0m;
                    decimal dTemp_BadQty = 0m;

                    if (temp_Result_1201.TN_MPS1202List != null)
                    {
                        if (temp_Result_1201.TN_MPS1202List.Count > 0)
                        {
                            foreach (TN_MPS1202 each in temp_Result_1201.TN_MPS1202List)
                            {
                                if (each.ItemMoveNo.IsNullOrEmpty())
                                {
                                    dTemp_ResultQty += each.ResultQty.GetDecimalNullToZero();
                                    dTemp_OkQty += each.OkQty.GetDecimalNullToZero();
                                    dTemp_BadQty += each.BadQty.GetDecimalNullToZero();
                                    each.ItemMoveNo = sNew_Item_move_no;
                                }
                            }

                            temp_Result_1201.UpdateTime = DateTime.Now;
                            temp_Result_1201.UpdateId = GlobalVariable.LoginId;
                            ModelService.UpdateChild<TN_MPS1201>(temp_Result_1201);

                            // 20211006 오세완 차장 무실적 종료는 이동표 발행도 안해버리는 걸로
                            if(sEndType != "NoQty")
                            {
                                // 부품 이동표 생성
                                TN_ITEM_MOVE new_Itemmove = new TN_ITEM_MOVE()
                                {
                                    ItemMoveNo = sNew_Item_move_no,
                                    WorkNo = temp_Result_1201.WorkNo,
                                    ProcessCode = temp_Result_1201.ProcessCode,
                                    ProcessSeq = temp_Result_1201.ProcessSeq,
                                    ProductLotNo = temp_Result_1201.ProductLotNo,
                                    BoxInQty = dBoxInQty,
                                    ResultSumQty = temp_Result_1201.ResultSumQty,
                                    OkSumQty = temp_Result_1201.OkSumQty,
                                    BadSumQty = temp_Result_1201.BadSumQty,
                                    ResultQty = dTemp_ResultQty,
                                    OkQty = dTemp_OkQty,
                                    BadQty = dTemp_BadQty,
                                    Temp = dPerBoxQty.ToString()
                                };
                                ModelService.InsertChild<TN_ITEM_MOVE>(new_Itemmove);

                                ModelService.Save();
                            }
                        }
                    }
                }

                PopupDataParam param = new PopupDataParam();
                temp_Result_1201.ResultDate = DateTime.Today;
                temp_Result_1201.ResultEndDate = DateTime.Now;
                ModelService.Update(temp_Result_1201);

                //작업지시서 상태 변경
                TN_MPS1200 masterObj = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo &&
                                                                                  p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode &&
                                                                                  p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq).FirstOrDefault();
                masterObj.JobStates = MasterCodeSTR.JobStates_End;
                masterObj.UpdateTime = DateTime.Now;
                ModelService.UpdateChild(masterObj);
                ModelService.Save();

                if(sEndType == "NoQty")
                {

                }
                else
                {
                    param.SetValue(PopupParameter.Constraint, "PRINT");
                    param.SetValue(PopupParameter.Value_1, sNew_Item_move_no);
                    // 20210905 오세완 차장 프레스 공정에서 반환되었다는 점을 알려줘야 함
                    //param.SetValue(PopupParameter.Value_2, "Barcode");

                    // 20210914 오세완 차장 부품이동표 형태를 제대로 전달 처리
                    string sDocType = e.Map.GetValue(PopupParameter.Value_2).GetNullToEmpty();
                    param.SetValue(PopupParameter.Value_2, sDocType);
                    param.SetValue(PopupParameter.Value_3, dPerBoxQty);
                }
                
                param.SetValue(PopupParameter.Value_4, "WorkEnd"); // 20210914 오세완 차장 작업완료를 명확히 해서 수동관리 여부가 아니 건에 대해 자재 차감행위 제어
                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }
        }

        /// <summary>
        /// 20210613 오세완 차장
        /// 이동표 새 출력 CallBack, wpfworkend 와 다른 점은 tool관련 테이블 삭제 로직 제거
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
            ModelService.Save();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "PRINT");
            param.SetValue(PopupParameter.Value_1, itemMoveNo);

            // 20210905 오세완 차장 프레스 공정인 경우 이동표 타입을 전달
            if (gs_PLC_Pop)
                param.SetValue(PopupParameter.Value_2, "A4");

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

            //var inspectionForm = new POP_POPUP.XPFINSPECTION(obj, productLotNo);
            var inspectionForm = new POP_POPUP.XPFINSPECTION_V2(obj, productLotNo); // 20210619 오세완 차장 초중종 로직 개선 버전
            inspectionForm.ShowDialog();
        }

        private void Btn_Pause_Click(object sender, EventArgs e)
        {
            ModelService.ReLoad();

            var obj = TEMP_XFPOP1000_Obj;
            
            var TN_MPS1201 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
            
            PopupDataParam param = new PopupDataParam();

            var TN_MPS1203_New = new TN_MPS1203()
            {
                PauseStartDate = DateTime.Now
            };
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
