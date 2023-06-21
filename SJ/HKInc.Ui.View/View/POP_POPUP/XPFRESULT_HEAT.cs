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

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 실적등록 팝업 (열처리)
    /// </summary>
    public partial class XPFRESULT_HEAT : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");

        TN_MPS1201 MasterObj;
        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;
        string ProcTeamCode;

        public XPFRESULT_HEAT()
        {
            InitializeComponent();
        }

        public XPFRESULT_HEAT(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("ResultAdd");

            //this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);

            layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 0, 2, 2);
            layoutControlItem1.MaxSize = new Size(layoutControlItem1.MaxSize.Width, layoutControlItem1.MaxSize.Height + 13);
            layoutControlItem1.MinSize = layoutControlItem1.MaxSize;

            layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 1, 2, 2);
            layoutControlItem2.MaxSize = layoutControlItem1.MaxSize;
            layoutControlItem2.MinSize = layoutControlItem1.MaxSize;

            layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 0, 2, 2);
            layoutControlItem3.MaxSize = layoutControlItem1.MaxSize;
            layoutControlItem3.MinSize = layoutControlItem1.MaxSize;

            layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 1, 2, 2);
            layoutControlItem4.MaxSize = layoutControlItem1.MaxSize;
            layoutControlItem4.MinSize = layoutControlItem1.MaxSize;
        }

        protected override void InitControls()
        {
            base.InitControls();

            ProcTeamCode = PopupParam.GetValue(PopupParameter.Value_3).GetNullToEmpty();

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
                    spin_ResultQty.EditValue = previousItemMoveObj.OkQty.GetDecimalNullToZero();
                }
            }

            btn_Apply.Click += Btn_Apply_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

            spin_ResultQty.Click += Spin_Click;
            spin_BadQty.Click += Spin_Click;
            spin_HeatTemperature.Click += Spin_Click;
            spin_HeatRpm.Click += Spin_Click;
            btn_TemperatureMinus.Click += Btn_Minus_Click;
            btn_TemperaturePlus.Click += Btn_Plus_Click;
            btn_RpmMinus.MouseDown += Btn_Minus_Click;
            btn_RpmPlus.Click += Btn_Plus_Click;

            //같은 작업지시에 열처리 온도, RPM은 마지막 버전으로 디폴트
            //var lastObj = ModelService.GetChildList<TN_MPS1202>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo
            //                                                    && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode
            //                                                    && p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq
            //                                                    )
            //                                                    .OrderBy(p => p.RowId).LastOrDefault();
            //if (lastObj != null)
            //{
            //    spin_HeatTemperature.EditValue = lastObj.Heat;
            //    spin_HeatRpm.EditValue = lastObj.Rpm;
            //}

            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;
        }

        private void Btn_Minus_Click(object sender, EventArgs e)
        {
            var button = sender as SimpleButton;
            if (button.Name.Contains("Rpm"))
            {
                spin_HeatRpm.Value -= 1;
            }
            else
            {
                spin_HeatTemperature.Value -= 1;
            }
        }

        private void Btn_Plus_Click(object sender, EventArgs e)
        {
            var button = sender as SimpleButton;
            if (button.Name.Contains("Rpm"))
            {
                spin_HeatRpm.Value += 1;
            }
            else
            {
                spin_HeatTemperature.Value += 1;
            }
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            btn_Apply.Text = LabelConvert.GetLabelText("Apply");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");

            var VI_MACHINE_DAILY_CHECK_LIST = ModelService.GetChildList<VI_MACHINE_DAILY_CHECK>(p => true).ToList();
            //var machineGroupCode = TEMP_XFPOP1000_Obj.MachineGroupCode.GetNullToNull();
            //lup_Machine.SetDefaultPOP(false, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), VI_MACHINE_DAILY_CHECK_LIST.Where(p => (string.IsNullOrEmpty(machineGroupCode) ? true : p.MachineGroupCode == machineGroupCode)).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Machine.SetDefaultPOP(false, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), VI_MACHINE_DAILY_CHECK_LIST.ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Machine.ReadOnly = true;

            var procTeamCode = ProcTeamCode;
            var userList = ModelService.GetChildList<User>(p => p.Active == "Y" && p.UserUserGroupList.Any(c => c.UserGroupId == MasterCodeSTR.UserGroup_Technician)).ToList();
            var loginId = GlobalVariable.LoginId;
            if (TEMP_XFPOP1000_Obj.ProcessSeq == 1)
            {
                lup_WorkId.SetDefaultPOP(false, "LoginId", "UserName", userList.Where(p => (string.IsNullOrEmpty(procTeamCode) ? true : p.ProductTeamCode == procTeamCode)).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
                if (userList.Where(p => (string.IsNullOrEmpty(procTeamCode) ? true : p.ProductTeamCode == procTeamCode)).ToList().Any(p => p.LoginId == loginId))
                    lup_WorkId.EditValue = loginId;
            }
            else
            {
                lup_WorkId.SetDefaultPOP(false, "LoginId", "UserName", userList, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
                if (userList.Any(p => p.LoginId == loginId))
                    lup_WorkId.EditValue = loginId;
            }

            lup_BadType.SetDefaultPOP(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP));

            //lup_Machine.SetFontSize(new Font("맑은 고딕", 12f));
            //lup_WorkId.SetFontSize(new Font("맑은 고딕", 12f));
            //lup_BadType.SetFontSize(new Font("맑은 고딕", 12f));

            //lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //lup_WorkId.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //lup_BadType.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            spin_ResultQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ResultQty.Properties.Mask.EditMask = "n0";
            spin_ResultQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ResultQty.Properties.Buttons[0].Visible = false;
            spin_ResultQty.ReadOnly = true;

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

            spin_SumBadQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SumBadQty.Properties.Mask.EditMask = "n0";
            spin_SumBadQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SumBadQty.Properties.Buttons[0].Visible = false;

            spin_HeatTemperature.Properties.Mask.MaskType = MaskType.Numeric;
            spin_HeatTemperature.Properties.Mask.EditMask = "n0";
            spin_HeatTemperature.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_HeatTemperature.Properties.Buttons[0].Visible = false;

            spin_HeatRpm.Properties.Mask.MaskType = MaskType.Numeric;
            spin_HeatRpm.Properties.Mask.EditMask = "n0";
            spin_HeatRpm.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_HeatRpm.Properties.Buttons[0].Visible = false;

            if (!MasterObj.MachineCode.IsNullOrEmpty())
            {
                if (VI_MACHINE_DAILY_CHECK_LIST.Any(p => p.MachineMCode == MasterObj.MachineCode))
                    lup_Machine.EditValue = MasterObj.MachineCode;
            }
            //lup_Machine.EditValue = MasterObj.MachineCode;
            spin_SumResultQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.ResultSumQty).GetDecimalNullToZero();
            spin_SumOkQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.OkSumQty).GetDecimalNullToZero();
            spin_SumBadQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.BadSumQty).GetDecimalNullToZero();

            tx_HeatTemperatureStd.EditValue = MasterObj.TN_STD1100.Heat;
            tx_HeatRpmStd.EditValue = MasterObj.TN_STD1100.Rpm;
            spin_HeatTemperature.EditValue = MasterObj.TN_STD1100.Heat.GetDecimalNullToZero();
            spin_HeatRpm.EditValue = MasterObj.TN_STD1100.Rpm.GetDecimalNullToZero();

            tx_ProductLotNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
        }

        /// <summary>
        /// 적용 버튼 클릭
        /// </summary>
        private void Btn_Apply_Click(object sender, EventArgs e)
        {
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

                if (badQty > 0 && badType.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_68), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("BadType")));
                    return;
                }

                if (badQty == 0 && !badType.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_68), LabelConvert.GetLabelText("BadType"), LabelConvert.GetLabelText("BadQty")));
                    return;
                }

                if (resultQty - badQty < 0)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_69), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("ResultQty")));
                    return;
                }

                if (resultQty == 0)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("ResultQty")));
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

                if (MasterObj.ProcessSeq > 1)
                {
                    var previousItemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.WorkNo == MasterObj.WorkNo
                                                                                    && p.ProcessSeq == MasterObj.ProcessSeq - 1
                                                                                    && p.ProductLotNo == MasterObj.ProductLotNo
                                                                                    && p.ItemMoveNo == MasterObj.ItemMoveNo).FirstOrDefault();
                    if (previousItemMoveObj != null)
                    {
                        var checkItemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.WorkNo == MasterObj.WorkNo
                                                                                        && p.ProcessCode == MasterObj.ProcessCode
                                                                                        && p.ProcessSeq == MasterObj.ProcessSeq
                                                                                        && p.ProductLotNo == MasterObj.ProductLotNo
                                                                                        && p.ItemMoveNo == MasterObj.ItemMoveNo).FirstOrDefault();
                        var checkQty = checkItemMoveObj.ResultQty + resultQty;
                        if (checkQty > previousItemMoveObj.ResultQty)
                        {
                            var possibleQty = previousItemMoveObj.ResultQty - checkItemMoveObj.ResultQty;
                            MessageBoxHandler.Show(string.Format("이전 공정의 생산 수량을 벗어났습니다. 가능한 생산 수량 : {0}", possibleQty.GetDecimalNullToZero().ToString("N0")));
                            return;
                        }
                    }
                }

                MasterObj.MachineCode = machineCode;
                MasterObj.ResultSumQty += resultQty;
                MasterObj.OkSumQty += (resultQty - badQty);
                MasterObj.BadSumQty += badQty;

                var detailNewObj = new TN_MPS1202();
                detailNewObj.ResultSeq = MasterObj.TN_MPS1202List.Count == 0 ? 1 : MasterObj.TN_MPS1202List.Max(p => p.ResultSeq) + 1;
                detailNewObj.ItemCode = MasterObj.ItemCode;
                detailNewObj.CustomerCode = MasterObj.CustomerCode;
                detailNewObj.MachineCode = machineCode;
                detailNewObj.ResultInsDate = DateTime.Today;
                detailNewObj.ResultQty = resultQty;
                detailNewObj.OkQty = (resultQty - badQty);
                detailNewObj.BadQty = badQty;
                detailNewObj.BadType = badType;
                detailNewObj.WorkId = workId;
                detailNewObj.Heat = spin_HeatTemperature.EditValue.GetDecimalNullToZero();
                detailNewObj.Rpm = spin_HeatRpm.EditValue.GetDecimalNullToZero();

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
                        checkItemMoveObj.OkSumQty = sumOkQty + (resultQty - badQty);
                        checkItemMoveObj.BadSumQty = sumBadQty + badQty;
                        checkItemMoveObj.ResultQty += resultQty;
                        checkItemMoveObj.OkQty += (resultQty - badQty);
                        checkItemMoveObj.BadQty += (sumBadQty + badQty);
                        checkItemMoveObj.UpdateTime = DateTime.Now;
                        ModelService.UpdateChild(checkItemMoveObj);
                    }
                }

                //같은 LOT에 열처리 온도, RPM은 마지막 버전 값과 입력 값이 다를 경우 이전 실적 모두 업데이트
                var lastObj = MasterObj.TN_MPS1202List.OrderBy(p => p.RowId).LastOrDefault();
                if (lastObj != null)
                {
                    bool heatCheck = false;
                    bool rpmCheck = false;

                    if (detailNewObj.Heat.GetDecimalNullToZero() != lastObj.Heat.GetDecimalNullToZero())
                        heatCheck = true;
                    if (detailNewObj.Rpm.GetDecimalNullToZero() != lastObj.Rpm.GetDecimalNullToZero())
                        rpmCheck = true;

                    foreach (var v in MasterObj.TN_MPS1202List)
                    {
                        if (heatCheck)
                            v.Heat = detailNewObj.Heat;
                        if (rpmCheck)
                            v.Rpm = detailNewObj.Rpm;
                    }
                }

                MasterObj.TN_MPS1202List.Add(detailNewObj);
                MasterObj.UpdateTime = DateTime.Now;
                MasterObj.UpdateId = workId;
                ModelService.Save();

                #region 자재 또는 반제품 소요량 로직
                if (TEMP_XFPOP1000_Obj.ProcessSeq == 1)
                {
                    TN_STD1300 banBomObj = null;
                    var wanBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode).FirstOrDefault();
                    if (wanBomObj != null)
                    {
                        banBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ParentBomCode == wanBomObj.BomCode).FirstOrDefault();
                    }
                    if (banBomObj != null)
                        BanUsingQty(banBomObj.UseQty);
                    else
                        MaterialUsingQty();
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
            spinEdit.SelectAll();
            if (!GlobalVariable.KeyPad) return;

            //var keyPad = new XFCKEYPAD();
            //if (keyPad.ShowDialog() != DialogResult.Cancel)
            //{
            //    spinEdit.EditValue = keyPad.returnval;
            //}

            var keyPad = new XFCNUMPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                spinEdit.EditValue = keyPad.returnval;
            }
        }

        /// <summary>
        /// 자재 투입소요량 로직
        /// </summary>
        private void MaterialUsingQty()
        {
            var resultQty = spin_ResultQty.EditValue.GetDecimalNullToZero();
            var productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();

            //투입정보불러오기.
            var TN_LOT_MST = ModelService.GetChildList<TN_LOT_MST>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && p.ProductLotNo == productLotNo).First();
            var TN_LOT_DTL = TN_LOT_MST.TN_LOT_DTL_List.OrderBy(p => p.Seq).LastOrDefault();
            if (TN_LOT_DTL == null) return;

            //제품에 등록된 자재소요량 가져오기
            var srcWeight = TN_LOT_DTL.TN_LOT_MST.TN_STD1100.SrcWeight.GetDecimalNullToZero();
            if (srcWeight <= 0) return;

            ModelService.InsertChild(new TN_SRC1000()
            {
                WorkNo = TN_LOT_MST.WorkNo,
                ProductLotNo = TN_LOT_MST.ProductLotNo,
                ParentSeq = TN_LOT_DTL.Seq,
                Seq = TN_LOT_DTL.TN_SRC1000List.Count == 0 ? 1 : TN_LOT_DTL.TN_SRC1000List.Max(p => p.Seq) + 1,
                SrcInLotNo = TN_LOT_DTL.SrcInLotNo,
                SpendQty = (resultQty * srcWeight) / 1000
            });

            ModelService.Save();
        }

        /// <summary>
        /// 반제품 투입소요량 로직
        /// </summary>
        private void BanUsingQty(decimal useQty)
        {
            var resultQty = spin_ResultQty.EditValue.GetDecimalNullToZero();
            var productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();

            //투입정보불러오기.
            var TN_LOT_MST = ModelService.GetChildList<TN_LOT_MST>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && p.ProductLotNo == productLotNo).First();
            var TN_LOT_DTL = TN_LOT_MST.TN_LOT_DTL_List.OrderBy(p => p.Seq).LastOrDefault();
            if (TN_LOT_DTL == null) return;

            ////제품에 등록된 자재소요량 가져오기
            //var srcWeight = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TN_LOT_MST.ItemCode).First().SrcWeight.GetDecimalNullToZero();
            //if (srcWeight <= 0) return;

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
                    if (nowMachineStateObj.JobStates != MasterCodeSTR.JobStates_Wait && nowMachineStateObj.JobStates != MasterCodeSTR.JobStates_Pause)
                    {
                        MessageBoxHandler.Show("해당 설비는 대기 또는 일시정지 설비가 아니므로 선택할 수 없습니다. 확인 부탁드립니다.");
                        lookup.EditValue = TEMP_XFPOP1000_Obj.MachineCode;
                        return;
                    }
                }
            }
        }
    }
}
