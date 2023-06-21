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
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 실적등록 팝업 (포장)
    /// </summary>
    public partial class XPFRESULT_PACK_BACKUP : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");
        IService<TN_MPS1204> ModelServiceDTL = (IService<TN_MPS1204>)ProductionFactory.GetDomainService("TN_MPS1204");
        IService<TN_PUR1300> ModelpurmService = (IService<TN_PUR1300>)ProductionFactory.GetDomainService("TN_PUR1300");
        IService<TN_PUR1301> ModelpurdService = (IService<TN_PUR1301>)ProductionFactory.GetDomainService("TN_PUR1301");
        IService<TN_PUR1201> ModelinService = (IService<TN_PUR1201>)ProductionFactory.GetDomainService("TN_PUR1201");


        TN_MPS1201 MasterObj;
        TEMP_XFPOP_PACK TEMP_XFPOP_PACK;

        protected BindingSource BadBindingSource = new BindingSource();
        public XPFRESULT_PACK_BACKUP()
        {
            InitializeComponent();
        }

        public XPFRESULT_PACK_BACKUP(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("ResultAdd");

            pic_ProdImage.DoubleClick += Pic_DoubleClick;
            //    pic_PackPlasticImage.DoubleClick += Pic_DoubleClick;
            pic_OutBoxImage.DoubleClick += Pic_DoubleClick;
            BadBindingSource.DataSource = new List<fail_List>();// ModelService.GetChildList<temp_qty>(p => 1 == 1).ToList();
            gridEx1.DataSource = BadBindingSource;
            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitControls()
        {
            base.InitControls();
            gridEx1.MainGrid.MainView.Click += MainView_Click;
            btn_Add.Click += Btn_Add_Click;
            btn_Del.Click += Btn_Del_Click;
            TEMP_XFPOP_PACK = (TEMP_XFPOP_PACK)PopupParam.GetValue(PopupParameter.KeyValue);

            var productLotNo = PopupParam.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            if (productLotNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ProductLotNo")));
                ActClose();
            }
            //tx_ProductLotNo.EditValue = productLotNo;

            MasterObj = ModelService.GetList(p => p.WorkNo == TEMP_XFPOP_PACK.WorkNo
                                                    && p.ProcessCode == TEMP_XFPOP_PACK.ProcessCode
                                                    && p.ProcessSeq == TEMP_XFPOP_PACK.ProcessSeq
                                                    && p.ProductLotNo == productLotNo
                                                )
                                                .FirstOrDefault();

            if (MasterObj == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("WorkResult")));
                ActClose();
            }

            var TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP_PACK.ItemCode).FirstOrDefault();
            if (!TN_STD1100.ProdFileUrl.IsNullOrEmpty())
            {
                pic_ProdImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1100.ProdFileUrl);
            }
            if (TN_STD1100.TN_STD1100_PACK_PLASTIC != null)
            {
                if (!TN_STD1100.TN_STD1100_PACK_PLASTIC.ProdFileUrl.IsNullOrEmpty())
                {
                    //    pic_PackPlasticImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1100.TN_STD1100_PACK_PLASTIC.ProdFileUrl);
                }
            }
            if (TN_STD1100.TN_STD1100_OUT_BOX != null)
            {
                if (!TN_STD1100.TN_STD1100_OUT_BOX.ProdFileUrl.IsNullOrEmpty())
                {
                    pic_OutBoxImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1100.TN_STD1100_OUT_BOX.ProdFileUrl);
                }
            }
        }
        private void Btn_Del_Click(object sender, EventArgs e)
        {
            BadBindingSource.RemoveCurrent();
            GridView gv = gridEx1.MainGrid.MainView as GridView;
            decimal qty2 = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                qty2 += gv.GetRowCellValue(i, gv.Columns[1]).GetDecimalNullToZero();

            }
            spin_BadQty.EditValue = qty2;
            lup_BadType.EditValue = null;
            spin_BadQty.EditValue = 0;
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
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
                    { BadType = lup_BadType.EditValue.GetNullToEmpty(), BadQty = spin_badQty1.EditValue.GetDecimalNullToZero() };
                    BadBindingSource.Add(tn);
                    gridEx1.MainGrid.BestFitColumns();
                }
            }
            else
            {
                decimal qty = gv.GetRowCellValue(row - 1, gv.Columns[1]).GetDecimalNullToZero();
                gv.SetRowCellValue(row - 1, gv.Columns[1], qty + spin_badQty1.EditValue.GetDecimalNullToZero());

            }
            decimal qty2 = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                qty2 += gv.GetRowCellValue(i, gv.Columns[1]).GetDecimalNullToZero();

            }
            spin_BadQty.EditValue = qty2;
            lup_BadType.EditValue = null;
            spin_badQty1.EditValue = 0;


        }
        public class fail_List
        {
            public fail_List() { }
            public string BadType { get; set; }
            public decimal BadQty { get; set; }
        }
        private void MainView_Click(object sender, EventArgs e)
        {
            // DevExpress.XtraGrid.Views.Grid.GridView gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            GridView gv = gridEx1.MainGrid.MainView as GridView;
            lup_BadType.EditValue = gv.GetFocusedRowCellValue("BadType").GetNullToEmpty();
            spin_BadQty.EditValue = gv.GetFocusedRowCellValue("BadQty").GetDecimalNullToZero();
        }
        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Save, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            var procTeamCode = TEMP_XFPOP_PACK.ProcTeamCode.GetNullToNull();
            lup_WorkId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => (string.IsNullOrEmpty(procTeamCode) ? true : p.ProductTeamCode == procTeamCode)
                                                                                                                                                        && p.Active == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);


            lup_BadType.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP));

            lup_WorkId.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_BadType.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            spin_ResultQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ResultQty.Properties.Mask.EditMask = "n0";
            spin_ResultQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ResultQty.Properties.Buttons[0].Visible = false;

            spin_BadQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_BadQty.Properties.Mask.EditMask = "n0";
            spin_BadQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_BadQty.Properties.Buttons[0].Visible = false;

            spin_badQty1.Properties.Mask.MaskType = MaskType.Numeric;
            spin_badQty1.Properties.Mask.EditMask = "n0";
            spin_badQty1.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_badQty1.Properties.Buttons[0].Visible = false;

            spin_SumResultQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SumResultQty.Properties.Mask.EditMask = "n0";
            spin_SumResultQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SumResultQty.Properties.Buttons[0].Visible = false;

            spin_SumOkQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SumOkQty.Properties.Mask.EditMask = "n0";
            spin_SumOkQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SumOkQty.Properties.Buttons[0].Visible = false;

            spin_SumBadQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SumBadQty.Properties.Mask.EditMask = "n0";
            spin_SumBadQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SumBadQty.Properties.Buttons[0].Visible = false;

            spin_SumResultQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.ResultSumQty).GetDecimalNullToZero();
            spin_SumOkQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.OkSumQty).GetDecimalNullToZero();
            spin_SumBadQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.BadSumQty).GetDecimalNullToZero();

            lup_WorkId.EditValue = GlobalVariable.LoginId;
            lup_WorkId.ReadOnly = true;
        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;
            var workId = lup_WorkId.EditValue.GetNullToNull();
            var resultQty = spin_ResultQty.EditValue.GetDecimalNullToZero();
            var badQty = spin_BadQty.EditValue.GetDecimalNullToZero();
            var badType = lup_BadType.EditValue.GetNullToNull();

            if (workId.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("WorkId")));
                return;
            }

            //if (badQty > 0 && badType.IsNullOrEmpty())
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_68), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("BadType")));
            //    return;
            //}

            //if (badQty == 0 && !badType.IsNullOrEmpty())
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_68), LabelConvert.GetLabelText("BadType"), LabelConvert.GetLabelText("BadQty")));
            //    return;
            //}

            if (resultQty - badQty < 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_69), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("ResultQty")));
                return;
            }

            //if(MasterObj.ProcessSeq > 1)
            //{
            //    var checkQty = spin_SumResultQty.GetDecimalNullToZero() + resultQty;
            //    var PreviousItemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.WorkNo == MasterObj.WorkNo
            //                                                                    && p.ProcessSeq == MasterObj.ProcessSeq -1
            //                                                                    && p.ProductLotNo == MasterObj.ProductLotNo
            //                                                                    && p.ItemMoveNo == MasterObj.ItemMoveNo).FirstOrDefault();
            //    if (PreviousItemMoveObj != null && PreviousItemMoveObj.ResultSumQty < checkQty)
            //    {
            //        MessageBoxHandler.Show("직전 생산수량보다 누적생산수량이 클 수 없습니다.");
            //        return;
            //    }
            //}

            try
            {

                MasterObj.ResultSumQty += resultQty;
                MasterObj.OkSumQty += (resultQty - badQty);
                MasterObj.BadSumQty += badQty;

                var detailNewObj = new TN_MPS1202();
                detailNewObj.ResultSeq = MasterObj.TN_MPS1202List.Count == 0 ? 1 : MasterObj.TN_MPS1202List.Max(p => p.ResultSeq) + 1;
                detailNewObj.ItemCode = MasterObj.ItemCode;
                detailNewObj.CustomerCode = MasterObj.CustomerCode;
                detailNewObj.ResultInsDate = DateTime.Today;
                detailNewObj.ResultQty = resultQty;
                detailNewObj.OkQty = (resultQty - badQty);
                detailNewObj.BadQty = badQty;
                detailNewObj.BadType = badType;
                detailNewObj.WorkId = workId;

                var stockPosition = MasterObj.TN_STD1100.StockPosition.GetNullToEmpty();

                if (stockPosition.IsNullOrEmpty())
                {
                    var TN_MPS1300_UPD = MasterObj.TN_MPS1300List.Where(p => p.WhCode == MasterCodeSTR.WAN_WhCode_DefaultCode && p.PositionCode == MasterCodeSTR.WAN_PositionCode_DefaultCode).FirstOrDefault();
                    if (TN_MPS1300_UPD != null)
                    {
                        //완제품 입고관리에 데이터가 있는 경우
                        TN_MPS1300_UPD.InQty += (resultQty - badQty);
                    }
                    else
                    {
                        //완제품 입고관리에 데이터가 없는 경우
                        var TN_MPS1300_NEW = new TN_MPS1300();
                        TN_MPS1300_NEW.WhCode = MasterCodeSTR.WAN_WhCode_DefaultCode;
                        TN_MPS1300_NEW.PositionCode = MasterCodeSTR.WAN_PositionCode_DefaultCode;
                        TN_MPS1300_NEW.ItemCode = MasterObj.ItemCode;
                        TN_MPS1300_NEW.CustomerCode = MasterObj.CustomerCode;
                        TN_MPS1300_NEW.InQty = (resultQty - badQty);
                        MasterObj.TN_MPS1300List.Add(TN_MPS1300_NEW);
                    }
                }
                else
                {
                    var TN_MPS1300_UPD = MasterObj.TN_MPS1300List.Where(p => p.WhCode == MasterCodeSTR.WAN_WhCode_DefaultCode && p.PositionCode == stockPosition).FirstOrDefault();
                    if (TN_MPS1300_UPD != null)
                    {
                        //완제품 입고관리에 데이터가 있는 경우
                        TN_MPS1300_UPD.InQty += (resultQty - badQty);
                    }
                    else
                    {
                        //완제품 입고관리에 데이터가 없는 경우
                        var TN_MPS1300_NEW = new TN_MPS1300();
                        TN_MPS1300_NEW.WhCode = MasterCodeSTR.WAN_WhCode_DefaultCode;
                        TN_MPS1300_NEW.PositionCode = stockPosition;
                        TN_MPS1300_NEW.ItemCode = MasterObj.ItemCode;
                        TN_MPS1300_NEW.CustomerCode = MasterObj.CustomerCode;
                        TN_MPS1300_NEW.InQty = (resultQty - badQty);
                        MasterObj.TN_MPS1300List.Add(TN_MPS1300_NEW);
                    }
                }

                //if (MasterObj.ProcessSeq > 1)
                //{
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
                    checkItemMoveObj.OkSumQty = sumOkQty + (resultQty - badQty);
                    checkItemMoveObj.BadSumQty = sumBadQty + badQty;
                    checkItemMoveObj.ResultQty += resultQty;
                    checkItemMoveObj.OkQty += (resultQty - badQty);
                    checkItemMoveObj.BadQty += (sumBadQty + badQty);
                    checkItemMoveObj.UpdateTime = DateTime.Now;
                    ModelService.UpdateChild(checkItemMoveObj);
                }
                //}
                if (badQty > 0)
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
                        // nobj.Temp = WorkGroup;
                        nobj.InSeq = DbRequestHandler.GetRowCount("exec SP_MPS1204_CNT '" + detailNewObj.ResultInsDate + "','" + MasterObj.WorkNo + "','" + MasterObj.ProcessCode + "','" + detailNewObj.ResultSeq + "'") == 0 ? 1 : DbRequestHandler.GetRowCount("exec SP_MPS1204_CNT '" + detailNewObj.ResultInsDate + "','" + MasterObj.WorkNo + "','" + MasterObj.ProcessCode + "','" + detailNewObj.ResultSeq + "'") + 1;
                        nobj.BadQty = gv.GetRowCellValue(i, gv.Columns[1]).GetIntNullToZero();
                        nobj.BadType = gv.GetRowCellValue(i, gv.Columns[0]).GetNullToEmpty();


                        ModelServiceDTL.Insert(nobj);
                        ModelServiceDTL.Save();

                    }
                }
                MasterObj.TN_MPS1202List.Add(detailNewObj);
                MasterObj.UpdateTime = DateTime.Now;
                MasterObj.UpdateId = workId;


                string InCustomerLotNo = null;
                #region 자동출고
                //=============================================자동출고시작 phs =================================================//

                List<TEMP_BOMLIST_STOCK> result;
                List<TEMP_BOMLIST_STOCK> nextinlot;


                using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
                {
                    var ItemCode = new SqlParameter("@ITEM_CODE", MasterObj.ItemCode);

                    result = context.Database.SqlQuery<TEMP_BOMLIST_STOCK>("USP_GET_BOMLIST_LOT @ITEM_CODE", ItemCode).ToList();

                    //result.Where(w => w.ReOutYn == "Y").OrderByDescending(p => p.ItemCode).ThenBy(p => p.InLotNo).ToList();

                }



                if (result != null && result.Count > 0)
                {
                    string inlotno = null;

                    int cntchk = 0;

                    foreach (var v in result)
                    {

                        if (v.CheckResult == "NG")
                        {
                            var inspchk = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();

                            if (inspchk.StockInspFlag == "Y")
                            {

                                MessageBoxHandler.Show("자재입고LOTNO중 " + v.ItemCode + "/" + v.InLotNo + "의 수입검사가 없습니다. 확인하여 주십시오");
                                return;
                            }
                        }

                        cntchk++;

                        decimal StockQty = v.StockQty;

                        decimal OutQty = (v.UseQty.GetDecimalNullToZero() * resultQty);

                        if (StockQty < Convert.ToDecimal(0.000)) //실적등록시점에 재고수량이 없을시
                        {
                            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
                            {
                                var ItemCode = new SqlParameter("@ITEM_CODE", MasterObj.ItemCode);
                                var InLotNo = new SqlParameter("@INLOTNO", v.InLotNo);
                                var NextItemCode = new SqlParameter("@NEXT_ITEM_CODE", v.ItemCode);

                                nextinlot = context.Database.SqlQuery<TEMP_BOMLIST_STOCK>("USP_GET_BOMLIST_LOT_NEXT @ITEM_CODE, @INLOTNO, @NEXT_ITEM_CODE", ItemCode, InLotNo, NextItemCode).ToList();
                                nextinlot.OrderByDescending(p => p.ItemCode).ThenBy(p => p.InLotNo).ToList();

                            }

                            if (nextinlot.Count > 0)//이후 LOTNO가 있을시에 
                            {
                                int cntchk_next = 0;

                                #region 기존LOT막는 부분 시작
                                TN_PUR1300 prenewobj = new TN_PUR1300()
                                {
                                    OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                                    OutDate = DateTime.Today,
                                    OutId = GlobalVariable.LoginId,
                                    Memo = MasterObj.ItemCode + "자동차감출고",
                                };

                                ModelpurmService.Insert(prenewobj);

                                var preInDetailObj = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == v.InLotNo).FirstOrDefault();

                                TN_PUR1301 newdtlobj = new TN_PUR1301()
                                {
                                    OutNo = prenewobj.OutNo,
                                    OutSeq = prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                                    InNo = preInDetailObj.InNo,
                                    InSeq = preInDetailObj.InSeq,
                                    ItemCode = v.ItemCode,
                                    OutQty = v.StockQty,
                                    //OutLotNo = prenewobj.OutNo.ToString().Substring(0, 11) + prenewobj.OutNo.ToString().Substring(13) + (prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                    OutLotNo = prenewobj.OutNo + (prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                    InLotNo = v.InLotNo,
                                    InCustomerLotNo = preInDetailObj.InCustomerLotNo,
                                    Memo = MasterObj.ItemCode + "자동차감출고",
                                    ReOutYn = "N", //이전 LOT의 출고 막음
                                    AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
                                };

                                ModelpurdService.Insert(newdtlobj);


                                var predtlobj = ModelinService.GetList(p => p.InNo == preInDetailObj.InNo
                                                                                       && p.InSeq == preInDetailObj.InSeq
                                                                                       && p.ItemCode == v.ItemCode
                                                                                      && p.InLotNo == v.InLotNo).First();

                                predtlobj.ReOutYn = "N";

                                //TN_PUR1201 newdtlobjin = new TN_PUR1201()
                                //{
                                //    InNo = InDetailObj.InNo,
                                //    InSeq = InDetailObj.InSeq,
                                //    ItemCode = v.ItemCode,
                                //    InLotNo = v.InLotNo,
                                //    ReOutYn = "N", //이전 LOT의 출고 막음
                                //};

                                ModelinService.Update(predtlobj);

                                string WorkNo = TEMP_XFPOP_PACK.WorkNo;
                                string MachineCode = "";
                                string ItemCode = TEMP_XFPOP_PACK.ItemCode;
                                string ProcessCode = v.ProcessCode.GetNullToEmpty() == "" ? TEMP_XFPOP_PACK.ProcessCode : v.ProcessCode.GetNullToEmpty();
                                string SrcItemCode = v.ItemCode;
                                string SrcOutLotNo = prenewobj.OutNo + (prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0');
                                string ProductLotNo = TEMP_XFPOP_PACK.ProductLotNo;
                                string WorkingDate = DateTime.Today.ToString("yyyy-MM-dd");
                                string LoginId = GlobalVariable.LoginId;

                                //투입정보 INSERT

                                string sSql = "EXEC USP_INS_AUTO '" + WorkNo + "','" + MachineCode + "','" + ItemCode + "','" + ProcessCode + "','" + SrcItemCode + "','" + SrcOutLotNo + "','" + ProductLotNo + "','" + WorkingDate + "','" + LoginId + "'";
                                DbRequestHandler.GetCellValue(sSql, 0);

                                #endregion 기존LOT막는 부분 끝


                                decimal g_outqty = OutQty + v.StockQty;

                                #region  다음입고LOT출고부분
                                foreach (var t in nextinlot)
                                {

                                    if (t.CheckResult == "NG")
                                    {
                                        var inspchk = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();

                                        if (inspchk.StockInspFlag == "Y")
                                        {
                                            MessageBoxHandler.Show("자재입고LOTNO중 " + t.ItemCode + "/" + t.InLotNo + "의 수입검사가 없습니다. 확인하여 주십시오");
                                            return;
                                        }
                                    }



                                    cntchk_next++;


                                    if (t.StockQty >= g_outqty) //재고수량이 (출고수량*소요량)보다 많은 경우
                                    {

                                        //if (cntchk == result.Count)
                                        //{

                                        //    var InDetailObjlast = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == t.InLotNo).FirstOrDefault();

                                        //    InCustomerLotNo = InDetailObjlast.InCustomerLotNo;

                                        //}

                                        TN_PUR1300 newobjnext = new TN_PUR1300()
                                        {
                                            OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                                            OutDate = DateTime.Today,
                                            OutId = GlobalVariable.LoginId,
                                            Memo = MasterObj.ItemCode + "자동차감출고",
                                        };

                                        ModelpurmService.Insert(newobjnext);

                                        var InDetailObjnext = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == t.InLotNo).FirstOrDefault();

                                        TN_PUR1301 newdtlobjnext = new TN_PUR1301()
                                        {
                                            OutNo = newobjnext.OutNo,
                                            OutSeq = newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                                            InNo = InDetailObjnext.InNo,
                                            InSeq = InDetailObjnext.InSeq,
                                            ItemCode = t.ItemCode,
                                            OutQty = OutQty,
                                            //OutLotNo = newobjnext.OutNo.ToString().Substring(0, 11) + newobjnext.OutNo.ToString().Substring(13) + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                            OutLotNo = newobjnext.OutNo + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                            InLotNo = t.InLotNo,
                                            InCustomerLotNo = InDetailObjnext.InCustomerLotNo,
                                            Memo = MasterObj.ItemCode + "자동차감출고",
                                            ReOutYn = "Y",
                                            AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
                                        };

                                        ModelpurdService.Insert(newdtlobjnext);

                                        var inobjnext = ModelinService.GetList(p => p.InNo == InDetailObjnext.InNo
                                                                                       && p.InSeq == InDetailObjnext.InSeq
                                                                                       && p.ItemCode == t.ItemCode
                                                                                      && p.InLotNo == t.InLotNo).First();

                                        inobjnext.ReOutYn = "Y";


                                        //TN_PUR1201 newdtlobjnextin = new TN_PUR1201()
                                        //{
                                        //    InNo = InDetailObjnext.InNo,
                                        //    InSeq = InDetailObjnext.InSeq,
                                        //    ItemCode = t.ItemCode,
                                        //    InLotNo = t.InLotNo,
                                        //    ReOutYn = "Y",
                                        //};

                                        ModelinService.Update(inobjnext);

                                        string nWorkNo = TEMP_XFPOP_PACK.WorkNo;
                                        string nMachineCode = "";
                                        string nItemCode = TEMP_XFPOP_PACK.ItemCode;
                                        string nProcessCode = v.ProcessCode.GetNullToEmpty() == "" ? TEMP_XFPOP_PACK.ProcessCode : v.ProcessCode.GetNullToEmpty();
                                        string nSrcItemCode = v.ItemCode;
                                        string nSrcOutLotNo = newobjnext.OutNo + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0');
                                        string nProductLotNo = TEMP_XFPOP_PACK.ProductLotNo;
                                        string nWorkingDate = DateTime.Today.ToString("yyyy-MM-dd");
                                        string nLoginId = GlobalVariable.LoginId;

                                        //투입정보 INSERT

                                        string nsSql = "EXEC USP_INS_AUTO '" + nWorkNo + "','" + nMachineCode + "','" + nItemCode + "','" + nProcessCode + "','" + nSrcItemCode + "','" + nSrcOutLotNo + "','" + ProductLotNo + "','" + WorkingDate + "','" + LoginId + "'";
                                        DbRequestHandler.GetCellValue(nsSql, 0);

                                        break;


                                    }
                                    else //재고수량이 (출고수량*소요량)보다 적은 경우
                                    {

                                        if (cntchk_next < nextinlot.Count())//다음 입고lot가 존재할 경우를 체크 
                                        {
                                            TN_PUR1300 newobjnext = new TN_PUR1300()
                                            {
                                                OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                                                OutDate = DateTime.Today,
                                                OutId = GlobalVariable.LoginId,
                                                Memo = MasterObj.ItemCode + "자동차감출고",
                                            };

                                            ModelpurmService.Insert(newobjnext);

                                            var InDetailObjnext = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == t.InLotNo).FirstOrDefault();

                                            TN_PUR1301 newdtlobjnext = new TN_PUR1301()
                                            {
                                                OutNo = newobjnext.OutNo,
                                                OutSeq = newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                                                InNo = InDetailObjnext.InNo,
                                                InSeq = InDetailObjnext.InSeq,
                                                ItemCode = t.ItemCode,
                                                OutQty = t.StockQty,
                                                //OutLotNo = newobjnext.OutNo.ToString().Substring(0, 11) + newobjnext.OutNo.ToString().Substring(13) + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                                OutLotNo = newobjnext.OutNo + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                                InLotNo = t.InLotNo,
                                                InCustomerLotNo = InDetailObjnext.InCustomerLotNo,
                                                Memo = MasterObj.ItemCode + "자동차감출고",
                                                ReOutYn = "N",
                                                AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
                                            };

                                            ModelpurdService.Insert(newdtlobjnext);


                                            var dtlobjnext = ModelinService.GetList(p => p.InNo == InDetailObjnext.InNo
                                                                                       && p.InSeq == InDetailObjnext.InSeq
                                                                                       && p.ItemCode == t.ItemCode
                                                                                      && p.InLotNo == t.InLotNo).First();

                                            dtlobjnext.ReOutYn = "N";


                                            //TN_PUR1201 newdtlobjnextin = new TN_PUR1201()
                                            //{
                                            //    InNo = InDetailObjnext.InNo,
                                            //    InSeq = InDetailObjnext.InSeq,
                                            //    ItemCode = t.ItemCode,
                                            //    InLotNo = t.InLotNo,
                                            //    ReOutYn = "N",
                                            //};

                                            ModelinService.Update(dtlobjnext);

                                            string nWorkNo = TEMP_XFPOP_PACK.WorkNo;
                                            string nMachineCode = "";
                                            string nItemCode = TEMP_XFPOP_PACK.ItemCode;
                                            string nProcessCode = v.ProcessCode.GetNullToEmpty() == "" ? TEMP_XFPOP_PACK.ProcessCode : v.ProcessCode.GetNullToEmpty();
                                            string nSrcItemCode = v.ItemCode;
                                            string nSrcOutLotNo = newobjnext.OutNo + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0');
                                            string nProductLotNo = TEMP_XFPOP_PACK.ProductLotNo;
                                            string nWorkingDate = DateTime.Today.ToString("yyyy-MM-dd");
                                            string nLoginId = GlobalVariable.LoginId;

                                            //투입정보 INSERT

                                            string nSql = "EXEC USP_INS_AUTO '" + nWorkNo + "','" + nMachineCode + "','" + nItemCode + "','" + nProcessCode + "','" + nSrcItemCode + "','" + nSrcOutLotNo + "','" + nProductLotNo + "','" + nWorkingDate + "','" + nLoginId + "'";
                                            DbRequestHandler.GetCellValue(nSql, 0);

                                            g_outqty = t.StockQty * t.UseQty;

                                        }
                                        else //다음 입고lot가 없을 경우 
                                        {
                                            DialogResult okyn = MessageBoxHandler.Show("" + v.ItemCode + "/" + v.InLotNo + " 해당입고LOTNO 이후의 재고수량이 없습니다. 해당LOTNO의 재고가 수량없이 진행됩니다. \r\n 계속 하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                            if (okyn == DialogResult.Yes)
                                            {
                                                if (StockQty != 0)
                                                {
                                                    TN_PUR1300 newobjnext = new TN_PUR1300()
                                                    {
                                                        OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                                                        OutDate = DateTime.Today,
                                                        OutId = GlobalVariable.LoginId,
                                                        Memo = MasterObj.ItemCode + "볼자동차감출고",
                                                    };

                                                    ModelpurmService.Insert(newobjnext);

                                                    var InDetailObjnext = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == t.InLotNo).FirstOrDefault();

                                                    TN_PUR1301 newdtlobjnext = new TN_PUR1301()
                                                    {
                                                        OutNo = newobjnext.OutNo,
                                                        OutSeq = newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                                                        InNo = InDetailObjnext.InNo,
                                                        InSeq = InDetailObjnext.InSeq,
                                                        ItemCode = t.ItemCode,
                                                        OutQty = StockQty,
                                                        //OutLotNo = newobjnext.OutNo.ToString().Substring(0, 11) + newobjnext.OutNo.ToString().Substring(13) + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                                        OutLotNo = newobjnext.OutNo + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                                        InLotNo = t.InLotNo,
                                                        InCustomerLotNo = InDetailObjnext.InCustomerLotNo,
                                                        Memo = MasterObj.ItemCode + "자동차감출고",
                                                        ReOutYn = "N",
                                                        AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
                                                    };

                                                    ModelpurdService.Insert(newdtlobjnext);

                                                    var dtlobjnext = ModelinService.GetList(p => p.InNo == InDetailObjnext.InNo
                                                                                         && p.InSeq == InDetailObjnext.InSeq
                                                                                         && p.ItemCode == t.ItemCode
                                                                                        && p.InLotNo == t.InLotNo).FirstOrDefault();

                                                    dtlobjnext.ReOutYn = "N";

                                                    //TN_PUR1201 newdtlobjnextin = new TN_PUR1201()
                                                    //{
                                                    //    InNo = InDetailObjnext.InNo,
                                                    //    InSeq = InDetailObjnext.InSeq,
                                                    //    ItemCode = t.ItemCode,
                                                    //    InLotNo = t.InLotNo,
                                                    //    ReOutYn = "Y",
                                                    //};

                                                    ModelinService.Update(dtlobjnext);

                                                    string nWorkNo = TEMP_XFPOP_PACK.WorkNo;
                                                    string nMachineCode = "";
                                                    string nItemCode = TEMP_XFPOP_PACK.ItemCode;
                                                    string nProcessCode = v.ProcessCode.GetNullToEmpty() == "" ? TEMP_XFPOP_PACK.ProcessCode : v.ProcessCode.GetNullToEmpty();
                                                    string nSrcItemCode = v.ItemCode;
                                                    string nSrcOutLotNo = newobjnext.OutNo + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0');
                                                    string nProductLotNo = TEMP_XFPOP_PACK.ProductLotNo;
                                                    string nWorkingDate = DateTime.Today.ToString("yyyy-MM-dd");
                                                    string nLoginId = GlobalVariable.LoginId;

                                                    //투입정보 INSERT

                                                    string nSql = "EXEC USP_INS_AUTO '" + nWorkNo + "','" + nMachineCode + "','" + nItemCode + "','" + nProcessCode + "','" + nSrcItemCode + "','" + nSrcOutLotNo + "','" + nProductLotNo + "','" + nWorkingDate + "','" + nLoginId + "'";
                                                    DbRequestHandler.GetCellValue(nSql, 0);
                                                }

                                            }
                                            else
                                            {
                                                return;
                                            }

                                        }


                                    }

                                }//반복문 끝
                                #endregion  다음입고LOT출고부분


                            }
                            else//이후 lot가 없을시 
                            {

                                DialogResult okyn = MessageBoxHandler.Show("" + v.ItemCode + "/" + v.InLotNo + " 해당입고LOTNO 이후의 재고수량이 없습니다.  해당LOTNO의 재고가 수량없이 진행됩니다. \r\n 계속 하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                if (okyn == DialogResult.Yes)
                                {

                                    if (StockQty != 0)
                                    {
                                        TN_PUR1300 newobj = new TN_PUR1300()
                                        {
                                            OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                                            OutDate = DateTime.Today,
                                            OutId = GlobalVariable.LoginId,
                                            Memo = MasterObj.ItemCode + "자동차감출고",
                                        };

                                        ModelpurmService.Insert(newobj);

                                        var InDetailObj = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == v.InLotNo).FirstOrDefault();

                                        TN_PUR1301 newdtlobj = new TN_PUR1301()
                                        {
                                            OutNo = newobj.OutNo,
                                            OutSeq = newobj.TN_PUR1301List.Count == 0 ? 1 : newobj.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                                            InNo = InDetailObj.InNo,
                                            InSeq = InDetailObj.InSeq,
                                            ItemCode = v.ItemCode,
                                            OutQty = StockQty,
                                            //OutLotNo = newobj.OutNo.ToString().Substring(0, 11) + newobj.OutNo.ToString().Substring(13) + (newobj.TN_PUR1301List.Count == 0 ? 1 : newobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                            OutLotNo = newobj.OutNo + (newobj.TN_PUR1301List.Count == 0 ? 1 : newobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                            InLotNo = v.InLotNo,
                                            InCustomerLotNo = InDetailObj.InCustomerLotNo,
                                            Memo = MasterObj.ItemCode + "자동차감출고",
                                            ReOutYn = "N",
                                            AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
                                        };

                                        ModelpurdService.Insert(newdtlobj);

                                        var newdtlobjin = ModelinService.GetList(p => p.InNo == InDetailObj.InNo
                                                                                          && p.InSeq == InDetailObj.InSeq
                                                                                          && p.ItemCode == v.ItemCode
                                                                                         && p.InLotNo == v.InLotNo).First();

                                        newdtlobjin.ReOutYn = "N";

                                        //TN_PUR1201 newdtlobjin = new TN_PUR1201()
                                        //{
                                        //    InNo = InDetailObj.InNo,
                                        //    InSeq = InDetailObj.InSeq,
                                        //    ItemCode = v.ItemCode,
                                        //    InLotNo = v.InLotNo,
                                        //    ReOutYn = "Y",
                                        //};

                                        ModelinService.Update(newdtlobjin);

                                        string WorkNo = TEMP_XFPOP_PACK.WorkNo;
                                        string MachineCode = "";
                                        string ItemCode = TEMP_XFPOP_PACK.ItemCode;
                                        string ProcessCode = v.ProcessCode.GetNullToEmpty() == "" ? TEMP_XFPOP_PACK.ProcessCode : v.ProcessCode.GetNullToEmpty();
                                        string SrcItemCode = v.ItemCode;
                                        string SrcOutLotNo = newobj.OutNo + (newobj.TN_PUR1301List.Count == 0 ? 1 : newobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0');
                                        string ProductLotNo = TEMP_XFPOP_PACK.ProductLotNo;
                                        string WorkingDate = DateTime.Today.ToString("yyyy-MM-dd");
                                        string LoginId = GlobalVariable.LoginId;

                                        //투입정보 INSERT

                                        string sSql = "EXEC USP_INS_AUTO '" + WorkNo + "','" + MachineCode + "','" + ItemCode + "','" + ProcessCode + "','" + SrcItemCode + "','" + SrcOutLotNo + "','" + ProductLotNo + "','" + WorkingDate + "','" + LoginId + "'";
                                        DbRequestHandler.GetCellValue(sSql, 0);

                                    }

                                }
                                else
                                {
                                    return;
                                }


                            }


                        }
                        else //실적등록시점에 재고수량이 존재할 경우
                        {
                            if (StockQty >= OutQty) //재고수량이 (출고수량*소요량)보다 많은 경우
                            {

                                //if (cntchk == result.Count)
                                //{

                                //    var InDetailObjlast = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == v.InLotNo).FirstOrDefault();

                                //    InCustomerLotNo = InDetailObjlast.InCustomerLotNo;

                                //}



                                TN_PUR1300 newobj = new TN_PUR1300()
                                {
                                    OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                                    OutDate = DateTime.Today,
                                    OutId = GlobalVariable.LoginId,
                                    Memo = MasterObj.ItemCode + "자동차감출고",
                                };

                                ModelpurmService.Insert(newobj);

                                var InDetailObj = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == v.InLotNo).FirstOrDefault();

                                TN_PUR1301 newdtlobj = new TN_PUR1301()
                                {
                                    OutNo = newobj.OutNo,
                                    OutSeq = newobj.TN_PUR1301List.Count == 0 ? 1 : newobj.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                                    InNo = InDetailObj.InNo,
                                    InSeq = InDetailObj.InSeq,
                                    ItemCode = v.ItemCode,
                                    OutQty = OutQty,
                                    //OutLotNo = newobj.OutNo.ToString().Substring(0, 11) + newobj.OutNo.ToString().Substring(13) + (newobj.TN_PUR1301List.Count == 0 ? 1 : newobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                    OutLotNo = newobj.OutNo + (newobj.TN_PUR1301List.Count == 0 ? 1 : newobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                    InLotNo = v.InLotNo,
                                    InCustomerLotNo = InDetailObj.InCustomerLotNo,
                                    Memo = MasterObj.ItemCode + "자동차감출고",
                                    ReOutYn = "Y",
                                    AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
                                };

                                ModelpurdService.Insert(newdtlobj);

                                var inobjnext = ModelinService.GetList(p => p.InNo == InDetailObj.InNo
                                                                               && p.InSeq == InDetailObj.InSeq
                                                                               && p.ItemCode == v.ItemCode
                                                                              && p.InLotNo == v.InLotNo).First();

                                inobjnext.ReOutYn = "Y";


                                //TN_PUR1201 newdtlobjnextin = new TN_PUR1201()
                                //{
                                //    InNo = InDetailObjnext.InNo,
                                //    InSeq = InDetailObjnext.InSeq,
                                //    ItemCode = t.ItemCode,
                                //    InLotNo = t.InLotNo,
                                //    ReOutYn = "Y",
                                //};

                                ModelinService.Update(inobjnext);



                                string WorkNo = TEMP_XFPOP_PACK.WorkNo;
                                string MachineCode = "";
                                string ItemCode = TEMP_XFPOP_PACK.ItemCode;
                                string ProcessCode = v.ProcessCode.GetNullToEmpty() == "" ? TEMP_XFPOP_PACK.ProcessCode : v.ProcessCode.GetNullToEmpty();
                                string SrcItemCode = v.ItemCode;
                                string SrcOutLotNo = newobj.OutNo + (newobj.TN_PUR1301List.Count == 0 ? 1 : newobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0');
                                string ProductLotNo = TEMP_XFPOP_PACK.ProductLotNo;
                                string WorkingDate = DateTime.Today.ToString("yyyy-MM-dd");
                                string LoginId = GlobalVariable.LoginId;

                                //투입정보 INSERT

                                string sSql = "EXEC USP_INS_AUTO '" + WorkNo + "','" + MachineCode + "','" + ItemCode + "','" + ProcessCode + "','" + SrcItemCode + "','" + SrcOutLotNo + "','" + ProductLotNo + "','" + WorkingDate + "','" + LoginId + "'";
                                DbRequestHandler.GetCellValue(sSql, 0);




                                //break;


                            }
                            else //재고수량이 (출고수량*소요량)보다 적은경우
                            {

                                using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
                                {
                                    var ItemCode = new SqlParameter("@ITEM_CODE", MasterObj.ItemCode);
                                    var InLotNo = new SqlParameter("@INLOTNO", v.InLotNo);
                                    var NextItemCode = new SqlParameter("@NEXT_ITEM_CODE", v.ItemCode);

                                    nextinlot = context.Database.SqlQuery<TEMP_BOMLIST_STOCK>("USP_GET_BOMLIST_LOT_NEXT @ITEM_CODE, @INLOTNO, @NEXT_ITEM_CODE", ItemCode, InLotNo, NextItemCode).ToList();
                                    nextinlot.OrderByDescending(p => p.ItemCode).ThenBy(p => p.InLotNo).ToList();

                                }

                                if (nextinlot.Count > 0)//이후 LOTNO가 있을시에 
                                {
                                    int cntchk_next = 0;

                                    #region 기존LOT막는 부분 시작
                                    TN_PUR1300 prenewobj = new TN_PUR1300()
                                    {
                                        OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                                        OutDate = DateTime.Today,
                                        OutId = GlobalVariable.LoginId,
                                        Memo = MasterObj.ItemCode + "자동차감출고",
                                    };

                                    ModelpurmService.Insert(prenewobj);

                                    var preInDetailObj = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == v.InLotNo).FirstOrDefault();

                                    TN_PUR1301 newdtlobj = new TN_PUR1301()
                                    {
                                        OutNo = prenewobj.OutNo,
                                        OutSeq = prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                                        InNo = preInDetailObj.InNo,
                                        InSeq = preInDetailObj.InSeq,
                                        ItemCode = v.ItemCode,
                                        OutQty = StockQty,
                                        //OutLotNo = prenewobj.OutNo.ToString().Substring(0, 11) + prenewobj.OutNo.ToString().Substring(13) + (prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                        OutLotNo = prenewobj.OutNo + (prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                        InLotNo = v.InLotNo,
                                        InCustomerLotNo = preInDetailObj.InCustomerLotNo,
                                        Memo = MasterObj.ItemCode + "자동차감출고",
                                        ReOutYn = "N", //이전 LOT의 출고 막음
                                        AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
                                    };

                                    ModelpurdService.Insert(newdtlobj);

                                    string WorkNo = TEMP_XFPOP_PACK.WorkNo;
                                    string MachineCode = "";
                                    string ItemCode = TEMP_XFPOP_PACK.ItemCode;
                                    string ProcessCode = v.ProcessCode.GetNullToEmpty() == "" ? TEMP_XFPOP_PACK.ProcessCode : v.ProcessCode.GetNullToEmpty();
                                    string SrcItemCode = v.ItemCode;
                                    string SrcOutLotNo = prenewobj.OutNo + (prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0');
                                    string ProductLotNo = TEMP_XFPOP_PACK.ProductLotNo;
                                    string WorkingDate = DateTime.Today.ToString("yyyy-MM-dd");
                                    string LoginId = GlobalVariable.LoginId;

                                    //투입정보 INSERT

                                    string sSql = "EXEC USP_INS_AUTO '" + WorkNo + "','" + MachineCode + "','" + ItemCode + "','" + ProcessCode + "','" + SrcItemCode + "','" + SrcOutLotNo + "','" + ProductLotNo + "','" + WorkingDate + "','" + LoginId + "'";
                                    DbRequestHandler.GetCellValue(sSql, 0);


                                    var predtlobj = ModelinService.GetList(p => p.InNo == preInDetailObj.InNo
                                                                                           && p.InSeq == preInDetailObj.InSeq
                                                                                           && p.ItemCode == v.ItemCode
                                                                                          && p.InLotNo == v.InLotNo).First();

                                    predtlobj.ReOutYn = "N";

                                    //TN_PUR1201 newdtlobjin = new TN_PUR1201()
                                    //{
                                    //    InNo = InDetailObj.InNo,
                                    //    InSeq = InDetailObj.InSeq,
                                    //    ItemCode = v.ItemCode,
                                    //    InLotNo = v.InLotNo,
                                    //    ReOutYn = "N", //이전 LOT의 출고 막음
                                    //};

                                    ModelinService.Update(predtlobj);

                                    #endregion 기존LOT막는 부분 끝


                                    decimal g_outqty = OutQty - v.StockQty;

                                    #region  다음입고LOT출고부분
                                    foreach (var t in nextinlot)
                                    {

                                        if (t.CheckResult == "NG")
                                        {
                                            var inspchk = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();

                                            if (inspchk.StockInspFlag == "Y")
                                            {
                                                MessageBoxHandler.Show("자재입고LOTNO중 " + t.ItemCode + "/" + t.InLotNo + "의 수입검사가 없습니다. 확인하여 주십시오");
                                                return;
                                            }
                                        }

                                        cntchk_next++;

                                        //if (g_outqty > 0)
                                        //{
                                        //    OutQty = OutQty - g_outqty;
                                        //}


                                        if (t.StockQty >= g_outqty) //재고수량이 (출고수량*소요량)보다 많은 경우
                                        {

                                            //if (cntchk == result.Count)
                                            //{

                                            //    var InDetailObjlast = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == t.InLotNo).FirstOrDefault();

                                            //    InCustomerLotNo = InDetailObjlast.InCustomerLotNo;

                                            //}

                                            TN_PUR1300 newobjnext = new TN_PUR1300()
                                            {
                                                OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                                                OutDate = DateTime.Today,
                                                OutId = GlobalVariable.LoginId,
                                                Memo = MasterObj.ItemCode + "자동차감출고",
                                            };

                                            ModelpurmService.Insert(newobjnext);

                                            var InDetailObjnext = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == t.InLotNo).FirstOrDefault();

                                            TN_PUR1301 newdtlobjnext = new TN_PUR1301()
                                            {
                                                OutNo = newobjnext.OutNo,
                                                OutSeq = newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                                                InNo = InDetailObjnext.InNo,
                                                InSeq = InDetailObjnext.InSeq,
                                                ItemCode = t.ItemCode,
                                                OutQty = g_outqty,
                                                //OutLotNo = newobjnext.OutNo.ToString().Substring(0, 11) + newobjnext.OutNo.ToString().Substring(13) + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                                OutLotNo = newobjnext.OutNo + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                                InLotNo = t.InLotNo,
                                                InCustomerLotNo = InDetailObjnext.InCustomerLotNo,
                                                Memo = MasterObj.ItemCode + "자동차감출고",
                                                ReOutYn = "Y",
                                                AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
                                            };

                                            ModelpurdService.Insert(newdtlobjnext);

                                            var inobjnext = ModelinService.GetList(p => p.InNo == InDetailObjnext.InNo
                                                                                           && p.InSeq == InDetailObjnext.InSeq
                                                                                           && p.ItemCode == t.ItemCode
                                                                                          && p.InLotNo == t.InLotNo).First();

                                            inobjnext.ReOutYn = "Y";


                                            //TN_PUR1201 newdtlobjnextin = new TN_PUR1201()
                                            //{
                                            //    InNo = InDetailObjnext.InNo,
                                            //    InSeq = InDetailObjnext.InSeq,
                                            //    ItemCode = t.ItemCode,
                                            //    InLotNo = t.InLotNo,
                                            //    ReOutYn = "Y",
                                            //};

                                            ModelinService.Update(inobjnext);

                                            string nWorkNo = TEMP_XFPOP_PACK.WorkNo;
                                            string nMachineCode = "";
                                            string nItemCode = TEMP_XFPOP_PACK.ItemCode;
                                            string nProcessCode = v.ProcessCode.GetNullToEmpty() == "" ? TEMP_XFPOP_PACK.ProcessCode : v.ProcessCode.GetNullToEmpty();
                                            string nSrcItemCode = v.ItemCode;
                                            string nSrcOutLotNo = newobjnext.OutNo + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0');
                                            string nProductLotNo = TEMP_XFPOP_PACK.ProductLotNo;
                                            string nWorkingDate = DateTime.Today.ToString("yyyy-MM-dd");
                                            string nLoginId = GlobalVariable.LoginId;

                                            //투입정보 INSERT

                                            string nsSql = "EXEC USP_INS_AUTO '" + nWorkNo + "','" + nMachineCode + "','" + nItemCode + "','" + nProcessCode + "','" + nSrcItemCode + "','" + nSrcOutLotNo + "','" + ProductLotNo + "','" + WorkingDate + "','" + LoginId + "'";
                                            DbRequestHandler.GetCellValue(nsSql, 0);

                                            break;


                                        }
                                        else //재고수량이 (출고수량*소요량)보다 적은 경우
                                        {

                                            if (cntchk_next < nextinlot.Count())//다음 입고lot가 존재할 경우를 체크 
                                            {
                                                TN_PUR1300 newobjnext = new TN_PUR1300()
                                                {
                                                    OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                                                    OutDate = DateTime.Today,
                                                    OutId = GlobalVariable.LoginId,
                                                    Memo = MasterObj.ItemCode + "자동차감출고",
                                                };

                                                ModelpurmService.Insert(newobjnext);

                                                var InDetailObjnext = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == t.InLotNo).FirstOrDefault();

                                                TN_PUR1301 newdtlobjnext = new TN_PUR1301()
                                                {
                                                    OutNo = newobjnext.OutNo,
                                                    OutSeq = newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                                                    InNo = InDetailObjnext.InNo,
                                                    InSeq = InDetailObjnext.InSeq,
                                                    ItemCode = t.ItemCode,
                                                    OutQty = t.StockQty,
                                                    OutLotNo = newobjnext.OutNo.ToString().Substring(0, 11) + newobjnext.OutNo.ToString().Substring(13) + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                                    InLotNo = t.InLotNo,
                                                    InCustomerLotNo = InDetailObjnext.InCustomerLotNo,
                                                    Memo = MasterObj.ItemCode + "자동차감출고",
                                                    ReOutYn = "N",
                                                    AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
                                                };

                                                ModelpurdService.Insert(newdtlobjnext);


                                                var dtlobjnext = ModelinService.GetList(p => p.InNo == InDetailObjnext.InNo
                                                                                           && p.InSeq == InDetailObjnext.InSeq
                                                                                           && p.ItemCode == t.ItemCode
                                                                                          && p.InLotNo == t.InLotNo).First();

                                                dtlobjnext.ReOutYn = "N";


                                                //TN_PUR1201 newdtlobjnextin = new TN_PUR1201()
                                                //{
                                                //    InNo = InDetailObjnext.InNo,
                                                //    InSeq = InDetailObjnext.InSeq,
                                                //    ItemCode = t.ItemCode,
                                                //    InLotNo = t.InLotNo,
                                                //    ReOutYn = "N",
                                                //};

                                                ModelinService.Update(dtlobjnext);

                                                string nWorkNo = TEMP_XFPOP_PACK.WorkNo;
                                                string nMachineCode = "";
                                                string nItemCode = TEMP_XFPOP_PACK.ItemCode;
                                                string nProcessCode = v.ProcessCode.GetNullToEmpty() == "" ? TEMP_XFPOP_PACK.ProcessCode : v.ProcessCode.GetNullToEmpty();
                                                string nSrcItemCode = v.ItemCode;
                                                string nSrcOutLotNo = newobjnext.OutNo.ToString().Substring(0, 11) + newobjnext.OutNo.ToString().Substring(13) + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0');
                                                string nProductLotNo = TEMP_XFPOP_PACK.ProductLotNo;
                                                string nWorkingDate = DateTime.Today.ToString("yyyy-MM-dd");
                                                string nLoginId = GlobalVariable.LoginId;

                                                //투입정보 INSERT

                                                string nsSql = "EXEC USP_INS_AUTO '" + nWorkNo + "','" + nMachineCode + "','" + nItemCode + "','" + nProcessCode + "','" + nSrcItemCode + "','" + nSrcOutLotNo + "','" + ProductLotNo + "','" + WorkingDate + "','" + LoginId + "'";
                                                DbRequestHandler.GetCellValue(nsSql, 0);

                                                g_outqty = t.StockQty * t.UseQty;

                                            }
                                            else //다음 입고lot가 없을 경우 
                                            {
                                                DialogResult okyn = MessageBoxHandler.Show("" + v.ItemCode + "/" + v.InLotNo + " 해당입고LOTNO 이후의 재고수량이 없습니다. 해당LOTNO의 재고가 수량없이 진행됩니다. \r\n 계속 하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                                if (okyn == DialogResult.Yes)
                                                {

                                                    if (StockQty != 0)
                                                    {
                                                        //if (cntchk == result.Count)
                                                        //{

                                                        //    var InDetailObjlast = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == t.InLotNo).FirstOrDefault();

                                                        //    InCustomerLotNo = InDetailObjlast.InCustomerLotNo;

                                                        //}


                                                        TN_PUR1300 newobjnext = new TN_PUR1300()
                                                        {
                                                            OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                                                            OutDate = DateTime.Today,
                                                            OutId = GlobalVariable.LoginId,
                                                            Memo = MasterObj.ItemCode + "자동차감출고",
                                                        };

                                                        ModelpurmService.Insert(newobjnext);

                                                        var InDetailObjnext = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == t.InLotNo).FirstOrDefault();

                                                        TN_PUR1301 newdtlobjnext = new TN_PUR1301()
                                                        {
                                                            OutNo = newobjnext.OutNo,
                                                            OutSeq = newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                                                            InNo = InDetailObjnext.InNo,
                                                            InSeq = InDetailObjnext.InSeq,
                                                            ItemCode = t.ItemCode,
                                                            OutQty = StockQty,
                                                            OutLotNo = newobjnext.OutNo.ToString().Substring(0, 11) + newobjnext.OutNo.ToString().Substring(13) + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                                            InLotNo = t.InLotNo,
                                                            InCustomerLotNo = InDetailObjnext.InCustomerLotNo,
                                                            Memo = MasterObj.ItemCode + "자동차감출고",
                                                            ReOutYn = "N",
                                                            AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
                                                        };

                                                        ModelpurdService.Insert(newdtlobjnext);

                                                        var dtlobjnext = ModelinService.GetList(p => p.InNo == InDetailObjnext.InNo
                                                                                             && p.InSeq == InDetailObjnext.InSeq
                                                                                             && p.ItemCode == t.ItemCode
                                                                                            && p.InLotNo == t.InLotNo).First();

                                                        dtlobjnext.ReOutYn = "N";

                                                        //TN_PUR1201 newdtlobjnextin = new TN_PUR1201()
                                                        //{
                                                        //    InNo = InDetailObjnext.InNo,
                                                        //    InSeq = InDetailObjnext.InSeq,
                                                        //    ItemCode = t.ItemCode,
                                                        //    InLotNo = t.InLotNo,
                                                        //    ReOutYn = "Y",
                                                        //};

                                                        ModelinService.Update(dtlobjnext);

                                                        string nWorkNo = TEMP_XFPOP_PACK.WorkNo;
                                                        string nMachineCode = "";
                                                        string nItemCode = TEMP_XFPOP_PACK.ItemCode;
                                                        string nProcessCode = v.ProcessCode.GetNullToEmpty() == "" ? TEMP_XFPOP_PACK.ProcessCode : v.ProcessCode.GetNullToEmpty();
                                                        string nSrcItemCode = v.ItemCode;
                                                        string nSrcOutLotNo = newobjnext.OutNo.ToString().Substring(0, 11) + newobjnext.OutNo.ToString().Substring(13) + (newobjnext.TN_PUR1301List.Count == 0 ? 1 : newobjnext.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0');
                                                        string nProductLotNo = TEMP_XFPOP_PACK.ProductLotNo;
                                                        string nWorkingDate = DateTime.Today.ToString("yyyy-MM-dd");
                                                        string nLoginId = GlobalVariable.LoginId;

                                                        //투입정보 INSERT

                                                        string nsSql = "EXEC USP_INS_AUTO '" + nWorkNo + "','" + nMachineCode + "','" + nItemCode + "','" + nProcessCode + "','" + nSrcItemCode + "','" + nSrcOutLotNo + "','" + ProductLotNo + "','" + WorkingDate + "','" + LoginId + "'";
                                                        DbRequestHandler.GetCellValue(nsSql, 0);
                                                    }


                                                }
                                                else
                                                {
                                                    return;
                                                }

                                            }


                                        }

                                    }//반복문 끝
                                    #endregion  다음입고LOT출고부분


                                }
                                else//이후 lot가 없을시 
                                {

                                    DialogResult okyn = MessageBoxHandler.Show("" + v.ItemCode + "/" + v.InLotNo + " 해당입고LOTNO 이후의 재고수량이 없습니다. 해당LOTNO의 재고가 수량없이 진행됩니다. \r\n 계속 하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                    if (okyn == DialogResult.Yes)
                                    {

                                        if (StockQty != 0)
                                        {
                                            //if (cntchk == result.Count)
                                            //{

                                            //    var InDetailObjlast = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == v.InLotNo).FirstOrDefault();

                                            //    InCustomerLotNo = InDetailObjlast.InCustomerLotNo;

                                            //}

                                            TN_PUR1300 newobj = new TN_PUR1300()
                                            {
                                                OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                                                OutDate = DateTime.Today,
                                                OutId = GlobalVariable.LoginId,
                                                Memo = MasterObj.ItemCode + "자동차감출고",

                                            };

                                            ModelpurmService.Insert(newobj);

                                            var InDetailObj = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == v.InLotNo).FirstOrDefault();

                                            TN_PUR1301 newdtlobj = new TN_PUR1301()
                                            {
                                                OutNo = newobj.OutNo,
                                                OutSeq = newobj.TN_PUR1301List.Count == 0 ? 1 : newobj.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                                                InNo = InDetailObj.InNo,
                                                InSeq = InDetailObj.InSeq,
                                                ItemCode = v.ItemCode,
                                                OutQty = StockQty,
                                                OutLotNo = newobj.OutNo.ToString().Substring(0, 11) + newobj.OutNo.ToString().Substring(13) + (newobj.TN_PUR1301List.Count == 0 ? 1 : newobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                                InLotNo = v.InLotNo,
                                                InCustomerLotNo = InDetailObj.InCustomerLotNo,
                                                Memo = MasterObj.ItemCode + "자동차감출고",
                                                ReOutYn = "N",
                                                AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
                                            };

                                            ModelpurdService.Insert(newdtlobj);

                                            var newdtlobjin = ModelinService.GetList(p => p.InNo == InDetailObj.InNo
                                                                                              && p.InSeq == InDetailObj.InSeq
                                                                                              && p.ItemCode == v.ItemCode
                                                                                             && p.InLotNo == v.InLotNo).First();

                                            newdtlobjin.ReOutYn = "N";

                                            //TN_PUR1201 newdtlobjin = new TN_PUR1201()
                                            //{
                                            //    InNo = InDetailObj.InNo,
                                            //    InSeq = InDetailObj.InSeq,
                                            //    ItemCode = v.ItemCode,
                                            //    InLotNo = v.InLotNo,
                                            //    ReOutYn = "Y",
                                            //};

                                            ModelinService.Update(newdtlobjin);

                                            string WorkNo = TEMP_XFPOP_PACK.WorkNo;
                                            string MachineCode = "";
                                            string ItemCode = TEMP_XFPOP_PACK.ItemCode;
                                            string ProcessCode = v.ProcessCode.GetNullToEmpty() == "" ? TEMP_XFPOP_PACK.ProcessCode : v.ProcessCode.GetNullToEmpty();
                                            string SrcItemCode = v.ItemCode;
                                            string SrcOutLotNo = newobj.OutNo.ToString().Substring(0, 11) + newobj.OutNo.ToString().Substring(13) + (newobj.TN_PUR1301List.Count == 0 ? 1 : newobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0');
                                            string ProductLotNo = TEMP_XFPOP_PACK.ProductLotNo;
                                            string WorkingDate = DateTime.Today.ToString("yyyy-MM-dd");
                                            string LoginId = GlobalVariable.LoginId;

                                            //투입정보 INSERT

                                            string sSql = "EXEC USP_INS_AUTO '" + WorkNo + "','" + MachineCode + "','" + ItemCode + "','" + ProcessCode + "','" + SrcItemCode + "','" + SrcOutLotNo + "','" + ProductLotNo + "','" + WorkingDate + "','" + LoginId + "'";
                                            DbRequestHandler.GetCellValue(sSql, 0);
                                        }

                                    }
                                    else
                                    {
                                        return;
                                    }

                                }


                            }

                        }


                    }

                }
                else
                {

                    //DialogResult okyn = MessageBoxHandler.Show("입고된 자재가 없거나  해당 완제품의 BOM 또는 원자재가 존재하지 않습니다. \r\n 계속 하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    //if (okyn == DialogResult.Yes)
                    //{

                    //}
                    //else
                    //{
                    //    MessageBoxHandler.Show("해당 완제품의 원자재나 BOM을 확인하여 주십시오.");

                    //    return;
                    //}

                }

                ModelpurmService.Save();
                ModelpurdService.Save();
                ModelinService.Save();
                ModelService.Save();

                //=============================================자동출고종료=================================================//

                #endregion 자동출고

            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex.Message);
            }
            finally
            {
                WaitHandler.CloseWait();
                //GridExControl.BestFitColumns();
            }

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, DialogResult.OK);
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void Pic_DoubleClick(object sender, EventArgs e)
        {
            var pictureEidt = sender as PictureEdit;
            if (pictureEidt.EditValue == null) return;
            var imgForm = new POP_POPUP.XPFPOPIMG(LabelConvert.GetLabelText(pictureEidt.Name.Replace("pic_", "")), pictureEidt.EditValue);
            imgForm.ShowDialog();
        }
        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            //gridEx1.MainGrid.AddColumn("BadType", LabelConvert.GetLabelText("BadType"));
            //gridEx1.MainGrid.AddColumn("BadQty", LabelConvert.GetLabelText("BadQty"));
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
    }
}
