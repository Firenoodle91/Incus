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
using HKInc.Service.Helper;
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

namespace HKInc.Ui.View.POP_Popup
{
    /// <summary>
    /// 20210612 오세완 차장
    /// 대영 스타일 실적등록 팝업 (기본)
    /// </summary>
    public partial class XPFRESULT_DEFAULT : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1401> ModelService = (IService<TN_MPS1401>)ProductionFactory.GetDomainService("TN_MPS1401");

        private TN_MPS1401 MasterObj;
        TP_XFPOP1000_V2_LIST TEMP_XFPOP1000_Obj;

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
            //this.Text = LabelConvert.GetLabelText("ResultAdd");
            this.Text = "실적등록";

            BadBindingSource.DataSource = new List<fail_List>();
            gridEx1.DataSource = BadBindingSource;
            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitControls()
        {
            base.InitControls();

            string sMessage = "";
            TEMP_XFPOP1000_Obj = (TP_XFPOP1000_V2_LIST)PopupParam.GetValue(PopupParameter.KeyValue);
                        
            string sProductLotNo = PopupParam.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            if (sProductLotNo.IsNullOrEmpty())
            {
                sMessage = "생산LOTNO가 존재하지 않습니다.";
                MessageBoxHandler.Show(sMessage);
                ActClose();
            }
            else
            {
                tx_ProductLotNo.EditValue = sProductLotNo;
                //ModelService.ReLoad(); // System.Data.Entity.Infrastructure.DbUpdateConcurrencyException 때문에 추가해봄

                // 20220502 오세완 차장 동일 작업지시 2회차 실적 입력시 오류를 일으킴
                //MasterObj = ModelService.GetList(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo &&
                //                                      p.ProcessCode == TEMP_XFPOP1000_Obj.Process &&
                //                                      p.ProcessTurn == TEMP_XFPOP1000_Obj.PSeq).OrderByDescending(o => o.Seq).SingleOrDefault();

                List<TN_MPS1401> temp_Arr = ModelService.GetList(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo &&
                                                                      p.ProcessCode == TEMP_XFPOP1000_Obj.Process &&
                                                                      p.ProcessTurn == TEMP_XFPOP1000_Obj.PSeq);
                if (temp_Arr != null)
                    if (temp_Arr.Count > 0)
                    {
                        MasterObj = temp_Arr.OrderByDescending(o => o.Seq).FirstOrDefault();
                    }

                if (MasterObj == null)
                {
                    sMessage = "작업실적이 존재하지 않습니다.";
                    MessageBoxHandler.Show(sMessage);
                    ActClose();
                }
                else
                {
                    bool bNeedSum = false;
                    if (TEMP_XFPOP1000_Obj.PSeq > 1)
                        bNeedSum = true;
                    //else if (MasterObj.Seq > 1)
                    //    bNeedSum = true;

                    if(bNeedSum)
                    {
                        // 20220502 오세완 차장 동일한 작업지시대비 생산 LOTNO인지는 아직 정해지지 않았으나 기존 로직을 내버려둠
                        List<TN_MPS1401> mps1401_Arr = ModelService.GetList(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo &&
                                                                                 p.ProcessCode == TEMP_XFPOP1000_Obj.Process &&
                                                                                 p.LotNo == sProductLotNo);
                        bool bCheck = false;
                        if (mps1401_Arr != null)
                            if (mps1401_Arr.Count > 0)
                            {
                                if (mps1401_Arr.Count > 1)
                                    bCheck = true;
                            }

                        int iPrev_Result_Sum = 0;
                        int iPrev_Ok_Sum = 0;
                        int iPrev_Fail_Sum = 0;
                        if (bCheck)
                        {
                            iPrev_Result_Sum = mps1401_Arr.Where(p => p.Seq < MasterObj.Seq - 1).Sum(s => s.ResultQty).GetIntNullToZero(); ;
                            iPrev_Ok_Sum = mps1401_Arr.Where(p => p.Seq < MasterObj.Seq - 1).Sum(s => s.OkQty).GetIntNullToZero();
                            iPrev_Fail_Sum = mps1401_Arr.Where(p => p.Seq < MasterObj.Seq - 1).Sum(s => s.FailQty).GetIntNullToZero();
                        }
                        else
                        {
                            iPrev_Result_Sum = MasterObj.ResultQty.GetIntNullToZero();
                            iPrev_Ok_Sum = MasterObj.OkQty.GetIntNullToZero();
                            iPrev_Fail_Sum = MasterObj.FailQty.GetIntNullToZero();
                        }

                        spin_SumResultQty.EditValue = iPrev_Result_Sum;
                        spin_SumOkQty.EditValue = iPrev_Ok_Sum;
                        spin_SumBadQty.EditValue = iPrev_Fail_Sum;
                        spin_ResultQty.EditValue = iPrev_Ok_Sum;
                    }
                    else
                    {
                        spin_SumResultQty.EditValue = 0;
                        spin_SumOkQty.EditValue = 0;
                        spin_SumBadQty.EditValue = 0;
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
                }
            }
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
            string sMessage = "";
            if(sBadtype == "")
            {
                sMessage = "불량 유형이 없습니다.";
                MessageBoxHandler.Show(sMessage);
                return;
            }

            if (spin_BadQty.EditValue.GetDecimalNullToZero() <= 0)
            {
                sMessage = "불량 수량이 없습니다. ";
                MessageBoxHandler.Show(sMessage);
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
            //gridEx1.MainGrid.MainView.Columns["BadType"].Caption = LabelConvert.GetLabelText("BadType");
            //gridEx1.MainGrid.MainView.Columns["BadQty"].Caption = LabelConvert.GetLabelText("BadQty");
            gridEx1.MainGrid.MainView.Columns["BadType"].Caption = "불량유형";
            gridEx1.MainGrid.MainView.Columns["BadQty"].Caption = "불량수량";
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("BadType", DbRequestHandler.GetCommCode(MasterCodeSTR.QCFAIL), "Mcode", "Codename");
        }

        protected override void InitCombo()
        {
            //btn_Apply.Text = LabelConvert.GetLabelText("Apply");
            //btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");

            lup_Machine.SetDefault(false, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), 
                DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_WorkId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), 
                DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            
            List<TN_STD1000> badlist = DbRequestHandler.GetCommCode(MasterCodeSTR.QCFAIL);
            if (badlist.Count > 0)
            {
                lup_BadType.SetDefault(true, "Mcode", "Codename", badlist);
            }

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

            lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode.GetNullToEmpty();
            lup_WorkId.EditValue = TEMP_XFPOP1000_Obj.WorkId.GetNullToEmpty();          // 작업자 자동추가     2022-07-14 김진우
        }

        /// <summary>
        /// 적용 버튼 클릭
        /// </summary>
        private void Btn_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                string sMessage = "";
                var machineCode = lup_Machine.EditValue.GetNullToNull();
                var workId = lup_WorkId.EditValue.GetNullToNull();
                var resultQty = spin_ResultQty.EditValue.GetIntNullToZero();
                var badQty = spin_BadQty.EditValue.GetIntNullToZero();
                var badType = lup_BadType.EditValue.GetNullToNull();

                #region 입력 데이터 검증 
                if (workId.IsNullOrEmpty())
                {
                    sMessage = "작업자는 필수입니다. ";
                    MessageBoxHandler.Show(sMessage);
                    return;
                }

                bool bType_null = false;
                //if (badQty > 0 && badType.IsNullOrEmpty())
                if (badQty > 0 && badType == null)
                {
                    sMessage = "불량 수량이 존재합니다. 불량 유형을 추가해 주시기 바랍니다. ";
                    MessageBoxHandler.Show(sMessage);
                    return;
                }
                else
                    bType_null = true;

                bool bQty_zero = false;
                //if (badQty == 0 && !badType.IsNullOrEmpty())
                if (badQty == 0 && badType != null)
                {
                    sMessage = "불량 유형이 존재합니다. 불량 수량을 추가해 주시기 바랍니다. ";
                    MessageBoxHandler.Show(sMessage);
                    return;
                }
                else
                    bQty_zero = true;

                if (bType_null && bQty_zero)
                {
                    // 20220504 오세완 차장 이경우를 진짜 입력을 안한 것으로 간주해야 한다. 
                }
                else
                {
                    int iCount = BadBindingSource.List.Count;
                    if (iCount == 0)
                    {
                        sMessage = "불량 유형을 추가해 주시기 바랍니다. ";
                        MessageBoxHandler.Show(sMessage);
                        return;
                    }
                }

                if (badQty == 0)
                {
                    if (resultQty <= 0)
                    {
                        sMessage = "생산 실적이 입력되지 않았습니다. ";
                        MessageBoxHandler.Show(sMessage);
                        return;
                    }
                }

                if (resultQty - badQty < 0)
                {
                    sMessage = "불량 실적이 생산 실적보다 클 수 없습니다.";
                    MessageBoxHandler.Show(sMessage);
                    return;
                }
                #endregion

                #region 생산 실적 저장 
                int iBadQtySum = spin_BadQtySum.EditValue.GetIntNullToZero();

                // 20220427 오세완 차장 기존에도 tn_mps1401을 insert할때 수량을 넣지 않고 진행을 했는데 이처리를 하지 않으면 초기값이 들어가지 않는다. 
                // USP_INS_MPS1401를 수정하긴 했으나 보험격으로 추가 
                // 20220429 오세완 차장 DbUpdateConcurrencyException이 발생되어 오류는 회피하였으나 결과적으로 값이 변경이 안되서 사용불가 
                // 20220504 오세완 차장 DbUpdateConcurrencyException을 STATE로 구분해도 위와 같다. 사용 불가하다. 
                //if (MasterObj != null)
                //{
                //    if (MasterObj.ResultQty == null)
                //        MasterObj.ResultQty = 0;

                //    if (MasterObj.OkQty == null)
                //        MasterObj.OkQty = 0;

                //    if (MasterObj.FailQty == null)
                //        MasterObj.FailQty = 0;
                //}

                //MasterObj.ResultQty += Convert.ToInt32(resultQty);
                //MasterObj.OkQty += Convert.ToInt32(resultQty - iBadQtySum);
                //MasterObj.FailQty += Convert.ToInt32(iBadQtySum);

                //ModelService.Update(MasterObj);
                //ModelService.Save();

                // 20220429 오세완 차장 DbUpdateConcurrencyException 회피용으로 사용 전 바로 UPDATE방법을 써봤으나 실패
                //TN_MPS1401 upd_Obj = ModelService.GetList(p => p.WorkNo == MasterObj.WorkNo &&
                //                                               p.Seq == MasterObj.Seq &&
                //                                               p.ProcessCode == MasterObj.ProcessCode).FirstOrDefault();
                //if (upd_Obj != null)
                //{
                //    if (upd_Obj.ResultQty == null)
                //        upd_Obj.ResultQty = 0;

                //    if (upd_Obj.OkQty == null)
                //        upd_Obj.OkQty = 0;

                //    if (upd_Obj.FailQty == null)
                //        upd_Obj.FailQty = 0;

                //    upd_Obj.ResultQty += Convert.ToInt32(resultQty);
                //    upd_Obj.OkQty += Convert.ToInt32(resultQty - dBadQtySum);
                //    upd_Obj.FailQty += Convert.ToInt32(dBadQtySum);

                //    ModelService.Update(upd_Obj);
                //    ModelService.Save();
                //}

                string sSql_Upd_Result = "exec USP_UPD_MPS1401_QTY '" + MasterObj.WorkDate.ToShortDateString() + "', '" + MasterObj.WorkNo + "', " + MasterObj.Seq.ToString() + ", '" +
                    MasterObj.ProcessCode + "', " + resultQty.ToString() + ", " + iBadQtySum.ToString() + ", '" + GlobalVariable.LoginId + "' ";
                int iUpd_Result = DbRequestHandler.SetDataQury(sSql_Upd_Result);

                TN_MPS1405 detailNewObj = new TN_MPS1405()
                {
                    WorkDate = (DateTime)MasterObj.WorkDate,
                    WorkNo = MasterObj.WorkNo,
                    ProcessCode = MasterObj.ProcessCode,
                    Pseq = MasterObj.ProcessTurn,
                    LotNo = MasterObj.LotNo,
                    ResultDate = DateTime.Now,
                    ResultQty = resultQty,                                                          // 2022-07-04 김진우       들어가는 값이 없어서 추가
                    OkQty = resultQty - iBadQtySum,
                    FailQty = iBadQtySum,
                    WorkId = workId,
                    MachineCode = machineCode,
                    WorkingTime = spin_WorkTime.EditValue.GetNullToEmpty(),
                };

                string sSql = "exec SP_MPS1405_CNT '" + MasterObj.WorkDate.ToString() + "', '" + MasterObj.WorkNo + "', '" + MasterObj.ProcessCode + "' ";
                int iMaxSeq = DbRequestHandler.GetRowCount(sSql);
                if (iMaxSeq == 0)
                    detailNewObj.Seq = 1;
                else
                    detailNewObj.Seq = iMaxSeq + 1;

                ModelService.InsertChild<TN_MPS1405>(detailNewObj);
                ModelService.Save();
                #endregion

                #region 불량 실적 저장 
                if (iBadQtySum > 0)
                {
                    GridView gv = gridEx1.MainGrid.MainView as GridView;
                  
                    for (int i = 0; i < gv.RowCount; i++)
                    {
                        // 20220504 오세완 차장 ENTITY 오류가 발생해서 사용 불가
                        //TN_MPS1404 nobj = new TN_MPS1404()
                        //{
                        //    WorkDate = (DateTime)MasterObj.WorkDate,
                        //    ResultDate = DateTime.Now,
                        //    WorkNo = MasterObj.WorkNo,
                        //    ProcessCode = MasterObj.ProcessCode,
                        //    LotNo = MasterObj.LotNo,
                        //    FailQty = gv.GetRowCellValue(i, gv.Columns[1]).GetIntNullToZero(),
                        //    FaleType = gv.GetRowCellValue(i, gv.Columns[0]).GetNullToEmpty(),
                        //    ItemCode = TEMP_XFPOP1000_Obj.ItemCode
                        //};

                        //string sFail_sql = "exec SP_MPS1404_CNT '" + MasterObj.WorkDate.ToString() + "', '" + MasterObj.WorkNo + "', '" + MasterObj.ProcessCode + "' ";
                        //int iMps1404_Maxseq = DbRequestHandler.GetRowCount(sFail_sql);
                        //if (iMps1404_Maxseq == 0)
                        //    nobj.Seq = 1;
                        //else
                        //    nobj.Seq = iMps1404_Maxseq + 1;

                        //ModelService.InsertChild<TN_MPS1404>(nobj);
                        //ModelService.Save(); // LOOP 밖에 있으면 오류가 발생한다. 

                        string sSql_mps1404 = "exec USP_INS_MPS1404 '" + MasterObj.WorkNo + "', '" + MasterObj.ProcessCode + "', '" + MasterObj.LotNo + "', '" + gv.GetRowCellValue(i, gv.Columns[0]).GetNullToEmpty() +
                            "', " + gv.GetRowCellValue(i, gv.Columns[1]).ToString() + ", '" + TEMP_XFPOP1000_Obj.ItemCode + "', '" + GlobalVariable.LoginId + "' ";
                        int iResult_mps1404 = DbRequestHandler.SetDataQury(sSql_mps1404);
                    }
                }
                #endregion

                #region 자동 출고, 자재 소요량 차감, lot 추적 연계
                // 20220422 오세완 차장 대영은 수동관리 자재와 자동으로 관리하는 자재가 분리가 되어 있어서 작업완료 시점에서 
                // 처리하였으나 큐맥은 전체가 자동관리하기 때문에 일단 실적을 입력할 때마다 차감으로 구현하게 됨
                TN_STD1100 prod_std = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode &&
                                                                                 p.UseYn == "Y").FirstOrDefault();
                if(prod_std != null)
                //if(prod_std.SrcCode.GetNullToEmpty() != "")
                //첫공정인 경우에만 자동 출고 처리
                if(prod_std.SrcCode.GetNullToEmpty() != "" && TEMP_XFPOP1000_Obj.PSeq == 1)
                {
                    using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
                        {
                            SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", prod_std.SrcCode.GetNullToEmpty());
                            List<TN_PUR1500> auto_src_Arr = new List<TN_PUR1500>();
                            #region 입고 자재 자동출고
                            var vResult = context.Database.SqlQuery<TP_XPFRESULT_AUTO_SRCOUT_LIST>("USP_GET_XPFRESULT_AUTO_SRCOUT_LIST @ITEM_CODE", sp_Itemcode).ToList();
                            if(vResult != null)
                                if(vResult.Count >0)
                                {
                                    decimal dCalQty = resultQty * prod_std.SrcQty;
                                    bool bBreak = false;
                                    foreach (TP_XPFRESULT_AUTO_SRCOUT_LIST each in vResult)
                                    {
                                        TN_PUR1500 auto_out_Mobj = new TN_PUR1500()
                                        {
                                            OutNo = DbRequestHandler.GetRequestNumber("OUT"),
                                            OutDate = DateTime.Now,
                                            OutId = GlobalVariable.LoginId,
                                            ItemCode = prod_std.ItemCode,
                                            Memo = prod_std.ItemCode + " 자동차감출고",
                                            Temp = TEMP_XFPOP1000_Obj.WorkNo,
                                            TN_STD1100 = prod_std
                                        };

                                        List<TN_PUR1301> in_lot_Arr = ModelService.GetChildList<TN_PUR1301>(p => p.ItemCode == prod_std.SrcCode &&
                                                                                                                 p.Temp2 == each.LOTNO).ToList();
                                        if(in_lot_Arr != null)
                                            if(in_lot_Arr.Count > 0)
                                            {
                                                TN_PUR1301 prev_in_detail_obj = in_lot_Arr.FirstOrDefault();
                                                if(prev_in_detail_obj != null)
                                                {
                                                    TN_PUR1501 auto_out_Dobj = new TN_PUR1501()
                                                    {
                                                        OutNo = auto_out_Mobj.OutNo,
                                                        OutSeq = auto_out_Mobj.PUR1501List.Count == 0 ? 1 : auto_out_Mobj.PUR1501List.Max(m => m.OutSeq) + 1,
                                                        ItemCode = prod_std.SrcCode,
                                                        Temp = TEMP_XFPOP1000_Obj.Process, // 20220422 오세완 차장 자동차감 공정 저장
                                                        Temp2 = prev_in_detail_obj.Temp2 // 20220422 오세완 차장 입고lotno 매칭
                                                    };

                                                    auto_out_Dobj.Temp1 = auto_out_Dobj.OutNo + auto_out_Dobj.OutSeq.ToString(); // 20220422 오세완 차장 출고lotno 채번

                                                    if (dCalQty <= each.STOCK_QTY)
                                                        auto_out_Dobj.OutQty = dCalQty;
                                                    else
                                                        auto_out_Dobj.OutQty = each.STOCK_QTY;

                                                    auto_out_Dobj.TN_PUR1500 = auto_out_Mobj;
                                                    auto_out_Mobj.PUR1501List.Add(auto_out_Dobj);
                                                    ModelService.InsertChild<TN_PUR1500>(auto_out_Mobj);
                                                    auto_src_Arr.Add(auto_out_Mobj);

                                                    dCalQty -= (decimal)auto_out_Dobj.OutQty;
                                                    if (dCalQty <= 0)
                                                        bBreak = true;

                                                    ModelService.Save();
                                                }
                                            }

                                        if (bBreak)
                                            break;
                                        //else
                                        //    auto_src_Arr.Add(auto_out_Mobj);
                                    }
                                }

                            #endregion

                            #region 로트 추적 연결
                            List<TN_LOT_MST_V2> lot_mst_Arr = new List<TN_LOT_MST_V2>();
                            if(auto_src_Arr != null)
                                if(auto_src_Arr.Count > 0)
                                {
                                    TN_LOT_MST_V2 lot_mst_Obj = ModelService.GetChildList<TN_LOT_MST_V2>(p => p.WorkingDate >= DateTime.Today &&
                                                                                                              p.ProductLotNo == MasterObj.LotNo &&
                                                                                                              p.WorkNo == MasterObj.WorkNo).FirstOrDefault();
                                    List<TN_LOT_DTL> lot_Dtl_List = ModelService.GetChildList<TN_LOT_DTL>(x => x.ProductLotNo == MasterObj.LotNo
                                                                                                && x.WorkNo == MasterObj.WorkNo
                                                                                                && x.ProcessCode == MasterObj.ProcessCode).ToList();

                                    if(lot_mst_Obj != null)
                                    {
                                        foreach (TN_PUR1500 each in auto_src_Arr)
                                            foreach (TN_PUR1501 each_detail in each.PUR1501List)
                                            {
                                                if (lot_Dtl_List.Where(x => x.SrcInLotNo == each_detail.Temp1).Count() > 0)
                                                    return;

                                                TN_LOT_DTL lot_dtl_Obj = new TN_LOT_DTL()
                                                {
                                                    WorkingDate = lot_mst_Obj.WorkingDate,
                                                    ProductLotNo = lot_mst_Obj.ProductLotNo,
                                                    WorkNo = lot_mst_Obj.WorkNo,
                                                    SrcCode = each_detail.ItemCode,
                                                    MachineCode = MasterObj.MachineCode,
                                                    ProcessCode = MasterObj.ProcessCode,
                                                    SrcInLotNo = each_detail.Temp1
                                                };

                                                if (lot_mst_Obj.LOT_DTL_List == null)
                                                    lot_dtl_Obj.Seq = 1;
                                                else if (lot_mst_Obj.LOT_DTL_List.Count == 0)
                                                    lot_dtl_Obj.Seq = 1;
                                                else
                                                    lot_dtl_Obj.Seq = lot_mst_Obj.LOT_DTL_List.Max(m => m.Seq) + 1;

                                                lot_dtl_Obj.TN_LOT_MST = lot_mst_Obj;
                                                lot_mst_Obj.LOT_DTL_List.Add(lot_dtl_Obj);
                                                ModelService.UpdateChild<TN_LOT_MST_V2>(lot_mst_Obj);
                                                lot_mst_Arr.Add(lot_mst_Obj);
                                                ModelService.Save();
                                            }
                                    }
                                }
                            #endregion

                            #region 자재 소요량 
                            // 20220425 오세완 차장 일단 자재를 사용한 만큼 자동출고하기 때문에 소요량 저장은 일단 제외
                            #endregion
                        }
                    }

                #endregion

                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Value_1, DialogResult.OK);
                ReturnPopupArgument = new PopupArgument(param);
                ActClose();
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
            keyPad.ShowDialog();
            spinEdit.EditValue = keyPad.returnval;
        }
        
        /// <summary>
        /// 팝업 종료시 저장하시겠습니까? 메시지 방지 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

    }
}
