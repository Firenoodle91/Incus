﻿using DevExpress.XtraEditors;
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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.Data.SqlClient;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 20210612 오세완 차장
    /// 대영 스타일 실적등록 팝업 (기본)
    /// </summary>
    public partial class XPFRESULT_DEFAULT : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");
        IService<TN_MPS1204> ModelServiceDTL = (IService<TN_MPS1204>)ProductionFactory.GetDomainService("TN_MPS1204");

        IService<TN_MPS1000> PROCESSModelService = (IService<TN_MPS1000>)ProductionFactory.GetDomainService("TN_MPS1000");

        TN_MPS1201 MasterObj;
        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        /// <summary>
        /// 20210612 오세완 차장 
        /// 불량유형 임시객체
        /// </summary>
        public class fail_List
        {
            public fail_List() { }

            /// <summary>
            /// 20210612 오세완 차장
            /// 불량유형코드
            /// </summary>
            public string BadType { get; set; }

            /// <summary>
            /// 20210612 오세완 차장
            /// 불량수량
            /// </summary>
            public decimal BadQty { get; set; }
        }

        protected BindingSource BadBindingSource = new BindingSource();
        #endregion


        public XPFRESULT_DEFAULT()
        {
            InitializeComponent();
        }

        public XPFRESULT_DEFAULT(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("ResultAdd");

            //설비 변경 못하게 설정...
            lup_Machine.ReadOnly = true;

            BadBindingSource.DataSource = new List<fail_List>();
            gridEx1.DataSource = BadBindingSource;
            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitControls()
        {
            base.InitControls();

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)PopupParam.GetValue(PopupParameter.KeyValue);
                        
            var productLotNo = PopupParam.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            if (productLotNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ProductLotNo")));
                ActClose();
            }

            tx_ProductLotNo.EditValue = productLotNo;
            
            MasterObj = ModelService.GetList(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo
                                                    && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode
                                                    && p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq
                                                    && p.ProductLotNo == productLotNo
                                                )
                                                .FirstOrDefault();

            if (MasterObj == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("WorkResult")));
                ActClose();
            }

            if (TEMP_XFPOP1000_Obj.ProcessSeq > 1)
            {
                var TN_MPS1201 = ModelService.GetList(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo 
                                                        && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode 
                                                        && p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq 
                                                        && p.ResultEndDate == null).LastOrDefault();
                //현 이동표번호 가져오기
                var currentItemMoveNo = TN_MPS1201 == null ? string.Empty : TN_MPS1201.ItemMoveNo;

                var previousItemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == currentItemMoveNo
                                                                                    && p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo
                                                                                    && p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq - 1).FirstOrDefault();
                if (previousItemMoveObj != null)
                {
                    //spin_ResultQty.EditValue = previousItemMoveObj.ResultQty.GetDecimalNullToZero();
                    //spin_ResultQty.EditValue = previousItemMoveObj.OkSumQty.GetDecimalNullToZero(); // 20210614 오세완 차장 유미대리 요청으로 양품수량으로 변경
                    spin_ResultQty.EditValue = previousItemMoveObj.OkQty.GetDecimalNullToZero(); // 20210908 오세완 차장 양품 합계가 아니라 해당 이동표의 양품 수량으로 변경
                }
            }

            btn_Apply.Click += Btn_Apply_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
            btn_Add.Click += Btn_Add_Click;
            btn_Del.Click += Btn_Del_Click;
            spin_ResultQty.Click += Spin_Click;
            spin_BadQtySum.Click += Spin_Click;
            spin_BadQty.Click += Spin_Click;
            spin_WorkTime.Click += Spin_Click;
            gridEx1.MainGrid.MainView.Click += MainView_Click;
            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;
        }

        private void MainView_Click(object sender, EventArgs e)
        {
            GridView gv = gridEx1.MainGrid.MainView as GridView;
            lup_BadType.EditValue = gv.GetFocusedRowCellValue("BadType").GetNullToEmpty();
            spin_BadQty.EditValue = gv.GetFocusedRowCellValue("BadQty").GetDecimalNullToZero();
        }

        private void Btn_Del_Click(object sender, EventArgs e)
        {
            GridView gv = gridEx1.MainGrid.MainView as GridView;

            if (gv.RowCount == 0) return;

            BadBindingSource.RemoveCurrent();

            decimal qty2 = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                qty2 += gv.GetRowCellValue(i, gv.Columns[1]).GetDecimalNullToZero();

            }
            spin_BadQtySum.EditValue = qty2;
            lup_BadType.EditValue = null;
            spin_BadQty.EditValue = 0;
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            string sBadtype = lup_BadType.EditValue.GetNullToEmpty();
            if(sBadtype == "")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("BadType")));
                return;
            }

            if (spin_BadQty.EditValue.GetDecimalNullToZero() <= 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("BadQty")));
                return;
            }

            GridView gv = gridEx1.MainGrid.MainView as GridView;
            string fcode = "";
            int row = 0;

            for (int i = 0; i < gv.RowCount; i++)
            {
                fcode = gv.GetRowCellValue(i, gv.Columns[0]).GetNullToEmpty();
                if (fcode == lup_BadType.EditValue.GetNullToEmpty())
                {
                    row = i + 1;
                }

            }

            if (row == 0)
            {
                if (lup_BadType.EditValue.GetNullToEmpty() != "")
                {
                    fail_List tn = new fail_List()
                    { BadType = lup_BadType.EditValue.GetNullToEmpty(), BadQty = spin_BadQty.EditValue.GetDecimalNullToZero() };
                    BadBindingSource.Add(tn);
                    gridEx1.MainGrid.BestFitColumns();
                }
            }
            else
            {
                decimal qty = gv.GetRowCellValue(row - 1, gv.Columns[1]).GetDecimalNullToZero();
                gv.SetRowCellValue(row - 1, gv.Columns[1], qty + spin_BadQty.EditValue.GetDecimalNullToZero());

            }
            decimal qty2 = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                qty2 += gv.GetRowCellValue(i, gv.Columns[1]).GetDecimalNullToZero();

            }
            spin_BadQtySum.EditValue = qty2;
            lup_BadType.EditValue = null;
            spin_BadQty.EditValue = 0;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MainView.Columns["BadType"].Caption = LabelConvert.GetLabelText("BadType");
            gridEx1.MainGrid.MainView.Columns["BadQty"].Caption = LabelConvert.GetLabelText("BadQty");
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("BadType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
        }

        protected override void InitCombo()
        {
            btn_Apply.Text = LabelConvert.GetLabelText("Apply");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");

            var machineGroupCode = TEMP_XFPOP1000_Obj.MachineGroupCode.GetNullToNull();
            lup_Machine.SetDefault(false, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => (string.IsNullOrEmpty(machineGroupCode) ? true : p.MachineGroupCode == machineGroupCode)
                                                                                                                                                        && p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_WorkId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            
            List<TN_STD1000> badlist = DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP).ToList();
            if (badlist.Count == 0) { badlist = DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP); }

            lup_BadType.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), badlist);

            lup_Machine.SetFontSize(new Font("맑은 고딕", 14f));
            lup_WorkId.SetFontSize(new Font("맑은 고딕", 14f));
            lup_BadType.SetFontSize(new Font("맑은 고딕", 14f));

            lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_WorkId.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_BadType.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            spin_ResultQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ResultQty.Properties.Mask.EditMask = "n0";
            spin_ResultQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ResultQty.Properties.Buttons[0].Visible = false;

            spin_BadQtySum.Properties.Mask.MaskType = MaskType.Numeric;
            spin_BadQtySum.Properties.Mask.EditMask = "n0";
            spin_BadQtySum.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_BadQtySum.Properties.Buttons[0].Visible = false;


            spin_BadQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_BadQty.Properties.Mask.EditMask = "n0";
            spin_BadQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_BadQty.Properties.Buttons[0].Visible = false;

            spin_SumResultQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SumResultQty.Properties.Mask.EditMask = "n0";
            spin_SumResultQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SumResultQty.Properties.Buttons[0].Visible = false;

            spin_SumOkQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SumOkQty.Properties.Mask.EditMask = "n0";
            spin_SumOkQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SumOkQty.Properties.Buttons[0].Visible = false;


            spin_WorkTime.Properties.Mask.MaskType = MaskType.Numeric;
            spin_WorkTime.Properties.Mask.EditMask = "n2";
            spin_WorkTime.Properties.Mask.UseMaskAsDisplayFormat = true; 

            spin_SumBadQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SumBadQty.Properties.Mask.EditMask = "n0";
            spin_SumBadQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SumBadQty.Properties.Buttons[0].Visible = false;

            lup_Machine.EditValue = MasterObj.MachineCode;
            spin_SumResultQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.ResultSumQty).GetDecimalNullToZero();
            spin_SumOkQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.OkSumQty).GetDecimalNullToZero();
            spin_SumBadQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.BadSumQty).GetDecimalNullToZero();
        }

        /// <summary>
        /// 적용 버튼 클릭
        /// </summary>
        private void Btn_Apply_Click(object sender, EventArgs e)
        {
            string MSG = string.Empty;

            try
            {
                WaitHandler.ShowWait();
                var machineCode = lup_Machine.EditValue.GetNullToNull();
                var workId = lup_WorkId.EditValue.GetNullToNull();
                var resultQty = spin_ResultQty.EditValue.GetDecimalNullToZero();
                var badQty = spin_BadQty.EditValue.GetDecimalNullToZero();
                var badType = lup_BadType.EditValue.GetNullToNull();

                if (workId.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("WorkId")));
                    return;
                }


                bool bType_null = false;
                if (badQty > 0 && badType.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_68), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("BadType")));
                    return;
                }
                else
                    bType_null = true;

                bool bQty_zero = false;
                if (badQty == 0 && !badType.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_68), LabelConvert.GetLabelText("BadType"), LabelConvert.GetLabelText("BadQty")));
                    return;
                }
                else
                    bQty_zero = true;

                if (!bType_null && !bQty_zero)
                {
                    int iCount = BadBindingSource.List.Count;
                    if (iCount == 0)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_146), LabelConvert.GetLabelText("BadType")));
                        //MessageBoxHandler.Show("불량 유형을 추가해 주시기 바랍니다. ");
                        return;
                    }
                }

                if (badQty == 0)
                {
                    if (resultQty <= 0)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_122), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("ResultQty")));
                        return;
                    }
                }

                if (resultQty - badQty < 0)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_69), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("ResultQty")));
                    return;
                }

                //if (MasterObj.ProcessSeq > 1)
                //{
                //    var checkQty = spin_SumResultQty.GetDecimalNullToZero() + resultQty;
                //    var PreviousItemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.WorkNo == MasterObj.WorkNo
                //                                                                    && p.ProcessSeq == MasterObj.ProcessSeq - 1
                //                                                                    && p.ProductLotNo == MasterObj.ProductLotNo
                //                                                                    && p.ItemMoveNo == MasterObj.ItemMoveNo).FirstOrDefault();
                //    if (PreviousItemMoveObj != null && PreviousItemMoveObj.ResultSumQty < checkQty)
                //    {
                //        MessageBoxHandler.Show("직전 생산수량보다 누적생산수량이 클 수 없습니다.");
                //        return;
                //    }
                //}

                decimal dBadQtySum = spin_BadQtySum.EditValue.GetDecimalNullToZero();

                MasterObj.MachineCode = machineCode;
                MasterObj.ResultSumQty += resultQty;
                MasterObj.OkSumQty += (resultQty - dBadQtySum);
                MasterObj.BadSumQty += dBadQtySum;

                TN_MPS1202 detailNewObj = new TN_MPS1202();
                detailNewObj.ResultSeq = MasterObj.TN_MPS1202List.Count == 0 ? 1 : MasterObj.TN_MPS1202List.Max(p => p.ResultSeq) + 1;
                detailNewObj.ItemCode = MasterObj.ItemCode;
                detailNewObj.CustomerCode = MasterObj.CustomerCode;
                detailNewObj.MachineCode = machineCode;
                detailNewObj.ResultInsDate = DateTime.Today;
                detailNewObj.ResultQty = resultQty;
                detailNewObj.OkQty = (resultQty - dBadQtySum);
                detailNewObj.BadQty = dBadQtySum;
                detailNewObj.WorkId = workId;
                detailNewObj.WorkTime = spin_WorkTime.EditValue.GetDecimalNullToZero();

                if (MasterObj.ProcessSeq > 1)
                {
                    detailNewObj.ItemMoveNo = MasterObj.ItemMoveNo;

                    var checkItemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.WorkNo == MasterObj.WorkNo
                                                                                    && p.ProcessCode == MasterObj.ProcessCode
                                                                                    && p.ProcessSeq == MasterObj.ProcessSeq
                                                                                    && p.ProductLotNo == MasterObj.ProductLotNo
                                                                                    && p.ItemMoveNo == MasterObj.ItemMoveNo).FirstOrDefault();

                    if (checkItemMoveObj != null)
                    {
                        var sumResultQty = spin_SumResultQty.EditValue.GetDecimalNullToZero();
                        var sumOkQty = spin_SumOkQty.EditValue.GetDecimalNullToZero();
                        var sumBadQty = spin_SumBadQty.EditValue.GetDecimalNullToZero();
                        checkItemMoveObj.ResultSumQty = sumResultQty + resultQty;
                        checkItemMoveObj.OkSumQty = sumOkQty + (resultQty - dBadQtySum);
                        checkItemMoveObj.BadSumQty = sumBadQty + dBadQtySum;
                        checkItemMoveObj.ResultQty += resultQty;
                        checkItemMoveObj.OkQty += (resultQty - dBadQtySum);
                        checkItemMoveObj.BadQty += (sumBadQty + dBadQtySum);
                        checkItemMoveObj.UpdateTime = DateTime.Now;
                        ModelService.UpdateChild(checkItemMoveObj);
                    }
                }

                if (dBadQtySum > 0)
                {
                    GridView gv = gridEx1.MainGrid.MainView as GridView;

                    for (int i = 0; i < gv.RowCount; i++)
                    {
                        TN_MPS1204 nobj = new TN_MPS1204();

                        nobj.ResultSeq = detailNewObj.ResultSeq;
                        nobj.ItemCode = detailNewObj.ItemCode;
                        nobj.CustomerCode = detailNewObj.CustomerCode;
                        nobj.MachineCode = detailNewObj.MachineCode;
                        nobj.ResultInsDate = detailNewObj.ResultInsDate;
                        nobj.WorkId = detailNewObj.WorkId;
                        nobj.WorkNo = MasterObj.WorkNo;
                        nobj.ProcessCode = MasterObj.ProcessCode;
                        nobj.ProcessSeq = MasterObj.ProcessSeq;
                        nobj.ProductLotNo = MasterObj.ProductLotNo;
                        nobj.InSeq = DbRequestHandler.GetRowCount("exec SP_MPS1204_CNT '" + detailNewObj.ResultInsDate + "','" + MasterObj.WorkNo + "','" + MasterObj.ProcessCode + "','" + detailNewObj.ResultSeq + "'") == 0 ? 1 : DbRequestHandler.GetRowCount("exec SP_MPS1204_CNT '" + detailNewObj.ResultInsDate + "','" + MasterObj.WorkNo + "','" + MasterObj.ProcessCode + "','" + detailNewObj.ResultSeq + "'") + 1;
                        //nobj.InSeq = ModelService.GetChildList<TN_MPS1204>(x => x.WorkNo == MasterObj.WorkNo && x.ProcessCode == MasterObj.ProcessCode && x.ResultSeq == detailNewObj.ResultSeq).Select(s => s.InSeq).ToList() == null ? 1 : ModelService.GetChildList<TN_MPS1204>(x => x.WorkNo == MasterObj.WorkNo && x.ProcessCode == MasterObj.ProcessCode && x.ResultSeq == detailNewObj.ResultSeq).Select(s => s.InSeq).Max() + 1;
                        nobj.BadQty = gv.GetRowCellValue(i, gv.Columns[1]).GetIntNullToZero();
                        nobj.BadType = gv.GetRowCellValue(i, gv.Columns[0]).GetNullToEmpty();

                        ModelServiceDTL.Insert(nobj);
                        ModelServiceDTL.Save(); // 20210623 오세완 차장 LOOP 밖에 있으면 오류가 발생한다. 
                    }
                    // 20210612 오세완 차장 성능상 빼는게 나을 듯 하여 처리
                    //ModelServiceDTL.Save();
                }

                MasterObj.TN_MPS1202List.Add(detailNewObj);
                MasterObj.UpdateTime = DateTime.Now;
                MasterObj.UpdateId = workId;
                ModelService.Save();



                #region 타발수 증감 로직
                // 20210612 오세완 차장 TN_MPS1201T 테이블에 TR_MPS1201_IU 트리거 참조
                #endregion

                #region 설비별 공구 수명 차감

                MSG = string.Empty;

                string ToolUseFlag = TEMP_XFPOP1000_Obj.ToolUseFlag.GetNullToN();

                if (ToolUseFlag == "Y")
                {
                    try
                    {
                        using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                        {
                            var WorkNo = new SqlParameter("@WorkNo", TEMP_XFPOP1000_Obj.WorkNo);
                            var MachineCode = new SqlParameter("@MachineCode", TEMP_XFPOP1000_Obj.MachineCode);
                            var ItemCode = new SqlParameter("@ItemCode", TEMP_XFPOP1000_Obj.ItemCode);
                            var ProcessCode = new SqlParameter("@ProcessCode", TEMP_XFPOP1000_Obj.ProcessCode);

                            var ResultQty = new SqlParameter("@ResultQty", resultQty);
                            var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                            MSG = context.Database.SqlQuery<string>("USP_UPD_TOOL_LIFECNT @WorkNo, @MachineCode, @ItemCode, @ProcessCode, @ResultQty, @LoginId",
                                WorkNo, MachineCode, ItemCode, ProcessCode, ResultQty, LoginId).SingleOrDefault();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHandler.Show(ex.Message, "");
                    }
                }

                #endregion


                #region JI정밀 첫 공정에서 자재 자동출고, 선입선출 적용

                var ProcessCHK = PROCESSModelService.GetList(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode
                                                              && p.UseFlag == "Y"
                                                              && p.ProcessSeq == 1
                                                              && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode
                                                              ).OrderBy(p => p.ProcessSeq).ToList();
                if (ProcessCHK.Count > 0)
                {
                    MSG = string.Empty;

                    try
                    {
                        using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                        {
                            var WorkNo = new SqlParameter("@WorkNo", TEMP_XFPOP1000_Obj.WorkNo);
                            var MachineCode = new SqlParameter("@MachineCode", TEMP_XFPOP1000_Obj.MachineCode);
                            var ItemCode = new SqlParameter("@ItemCode", TEMP_XFPOP1000_Obj.ItemCode);
                            var ProcessCode = new SqlParameter("@ProcessCode", TEMP_XFPOP1000_Obj.ProcessCode);
                            var ProductLotNo = new SqlParameter("@ProductLotNo", tx_ProductLotNo.EditValue.GetNullToEmpty());

                            var ResultQty = new SqlParameter("@ResultQty", resultQty);
                            var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                            MSG = context.Database.SqlQuery<string>("USP_INS_AUTO @WorkNo, @MachineCode, @ItemCode, @ProcessCode, @ProductLotNo, @ResultQty, @LoginId",
                                WorkNo, MachineCode, ItemCode, ProcessCode, ProductLotNo, ResultQty, LoginId).SingleOrDefault();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHandler.Show(ex.Message + ' ' + ex.StackTrace, "");
                    }
                    #endregion
                }

                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Value_1, DialogResult.OK);
                ReturnPopupArgument = new PopupArgument(param);
                ActClose();

            }
            catch (Exception ex)
            {
                MessageBoxHandler.Show(ex.Message + ' ' + ex.StackTrace, "");
            }
            finally
            {
                WaitHandler.CloseWait();
            }
        }

        /// <summary>
        /// 취소 버튼 클릭
        /// </summary>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            ActClose();
        }

        private void Spin_Click(object sender, EventArgs e)
        {
            var spinEdit = sender as SpinEdit;
            if (spinEdit == null) return;
            if (!GlobalVariable.KeyPad) return;

            var keyPad = new XFCNUMPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                spinEdit.EditValue = keyPad.returnval;
            }
        }
        
        /// <summary>
        /// 자재 및 반제품 투입소요량 로직
        /// </summary>
        private void UsingQty(string itemCode, decimal useQty)
        {
            var resultQty = spin_ResultQty.EditValue.GetDecimalNullToZero();
            var productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();

            //투입정보불러오기.
            var TN_LOT_MST = ModelService.GetChildList<TN_LOT_MST>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && p.ProductLotNo == productLotNo).First();
            var TN_LOT_DTL = TN_LOT_MST.TN_LOT_DTL_List.Where(p => p.SrcCode == itemCode).OrderBy(p => p.Seq).LastOrDefault();
            if (TN_LOT_DTL == null) return;

            ModelService.InsertChild(new TN_SRC1000()
            {
                WorkNo = TN_LOT_MST.WorkNo,
                ProductLotNo = TN_LOT_MST.ProductLotNo,
                ParentSeq = TN_LOT_DTL.Seq,
                Seq = TN_LOT_DTL.TN_SRC1000List.Count == 0 ? 1 : TN_LOT_DTL.TN_SRC1000List.Max(p => p.Seq) + 1,
                SrcInLotNo = TN_LOT_DTL.SrcInLotNo,
                SpendQty = resultQty * useQty
            });

            ModelService.Save();
        }

        private void Lup_Machine_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var value = lookup.EditValue.GetNullToEmpty();
            if (!value.IsNullOrEmpty() && TEMP_XFPOP1000_Obj.MachineCode != value)
            {
                var nowMachineStateObj = ModelService.GetChildList<VI_XRREP6000_LIST>(p => p.MachineMCode == value).FirstOrDefault();
                if (nowMachineStateObj != null)
                {
                    //대영정밀 설비 중복허용 가능
                    if (nowMachineStateObj.JobStates == MasterCodeSTR.JobStates_Stop)
                    {
                        MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_173));
                        lookup.EditValue = TEMP_XFPOP1000_Obj.MachineCode;
                        return;
                    }
                }
            }
        }

        protected override void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

    }
}
