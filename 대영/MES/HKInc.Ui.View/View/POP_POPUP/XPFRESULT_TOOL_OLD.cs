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
    /// 실적등록 팝업 (툴교체가능)
    /// </summary>
    public partial class XPFRESULT_TOOL_OLD : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");

        TN_MPS1201 MasterObj;
        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        public XPFRESULT_TOOL_OLD()
        {
            InitializeComponent();
        }

        public XPFRESULT_TOOL_OLD(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("ResultAdd");

            this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 240);
            this.Size = new Size(this.Size.Width, this.Size.Height - 240);
         
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
                    spin_ResultQty.EditValue = previousItemMoveObj.ResultQty.GetDecimalNullToZero();
                }
            }

            btn_Apply.Click += Btn_Apply_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
            btn_ToolChange.Click += Btn_ToolChange_Click;
            btn_ToolChange2.Click += Btn_ToolChange2_Click;

            spin_ResultQty.Click += Spin_Click;
            spin_BadQty.Click += Spin_Click;
            spin_WorkTime.Click += Spin_Click;
            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;
        }

     

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            btn_Apply.Text = LabelConvert.GetLabelText("Apply");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
            btn_ToolChange.Text = LabelConvert.GetLabelText("ToolChange");

            lcToolLifeQty2.Text = lcToolLifeQty.Text;
            lcToolUseQty2.Text = lcToolUseQty.Text;
            lcToolChangeDate2.Text = lcToolChangeDate.Text;
            btn_ToolChange2.Text = LabelConvert.GetLabelText("ToolChange");

            var machineGroupCode = TEMP_XFPOP1000_Obj.MachineGroupCode.GetNullToNull();
            lup_Machine.SetDefault(false, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => (string.IsNullOrEmpty(machineGroupCode) ? true : p.MachineGroupCode == machineGroupCode)
                                                                                                                                                        && p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            var procTeamCode = TEMP_XFPOP1000_Obj.ProcTeamCode.GetNullToNull();
            lup_WorkId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => (string.IsNullOrEmpty(procTeamCode) ? true : p.ProductTeamCode == procTeamCode)
                                                                                                    && p.Active == "Y"
                                                                                                  //  && p.UserUserGroupList.Any(c => c.UserGroupId == MasterCodeSTR.UserGroup_Technician)
                                                                                               )
                                                                                               .ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);


            lup_BadType.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP));

            lup_Machine.SetFontSize(new Font("맑은 고딕", 12f));
            lup_WorkId.SetFontSize(new Font("맑은 고딕", 12f));
            lup_BadType.SetFontSize(new Font("맑은 고딕", 12f));

            lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_WorkId.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_BadType.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_ToolLifeQty.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            tx_ToolUseQty.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            tx_ToolChangeDate.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            spin_ResultQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ResultQty.Properties.Mask.EditMask = "n0";
            spin_ResultQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ResultQty.Properties.Buttons[0].Visible = false;

            spin_BadQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_BadQty.Properties.Mask.EditMask = "n0";
            spin_BadQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_BadQty.Properties.Buttons[0].Visible = false;

            spin_SumResultQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SumResultQty.Properties.Mask.EditMask = "n0";
            spin_SumResultQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SumResultQty.Properties.Buttons[0].Visible = false;

            spin_WorkTime.Properties.Mask.MaskType = MaskType.Numeric;
            spin_WorkTime.Properties.Mask.EditMask = "n2";
            spin_WorkTime.Properties.Mask.UseMaskAsDisplayFormat = true; 

            spin_SumOkQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SumOkQty.Properties.Mask.EditMask = "n0";
            spin_SumOkQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SumOkQty.Properties.Buttons[0].Visible = false;

            spin_SumBadQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SumBadQty.Properties.Mask.EditMask = "n0";
            spin_SumBadQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SumBadQty.Properties.Buttons[0].Visible = false;

            lup_Machine.EditValue = MasterObj.MachineCode;
            spin_SumResultQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.ResultSumQty).GetDecimalNullToZero(); 
            spin_SumOkQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.OkSumQty).GetDecimalNullToZero();
            spin_SumBadQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.BadSumQty).GetDecimalNullToZero();

            var Tool1_Last_TN_TOOL1002 = ModelService.GetChildList<TN_TOOL1002>(p => p.WorkNo == MasterObj.WorkNo
                                                                                && p.ProcessSeq == MasterObj.ProcessSeq
                                                                                && p.ProcessCode == MasterObj.ProcessCode
                                                                                && p.ToolSeq == 1)
                                                                                .OrderBy(p => p.RowId)
                                                                                .LastOrDefault();
            var Tool1_TN_TOOL1003List = ModelService.GetChildList<TN_TOOL1003>(p => p.WorkNo == MasterObj.WorkNo
                                                                                && p.ProcessSeq == MasterObj.ProcessSeq
                                                                                && p.ProcessCode == MasterObj.ProcessCode
                                                                                && p.ToolSeq == 1).ToList();

            var toolLifeQty = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode).First().ToolLifeQty.GetDecimalNullToNull();
            tx_ToolLifeQty.EditValue = toolLifeQty == null ? 50000.ToString("N0") : ((decimal)toolLifeQty).ToString("N0");
            tx_ToolUseQty.EditValue = Tool1_TN_TOOL1003List.Count == 0 ? "0" : ((decimal)Tool1_TN_TOOL1003List.Sum(p => p.ToolUseQty)).ToString("N0");
            //tx_ToolUseQty.EditValue = Tool1_TN_TOOL1003 == null ? "0" : ((decimal)Tool1_TN_TOOL1003.ToolUseQty).ToString("N0");
            if (Tool1_Last_TN_TOOL1002 != null)
                tx_ToolChangeDate.EditValue = Tool1_Last_TN_TOOL1002.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

            if (toolLifeQty <= 0)
                btn_ToolChange.Enabled = false;

            //var Tool2_Last_TN_TOOL1002 = MasterObj.TN_TOOL1002List.Where(p => p.ToolSeq == 2).OrderBy(p => p.ChangeSeq).LastOrDefault();
            //var Tool2_TN_TOOL1003 = MasterObj.TN_TOOL1003List.Where(p => p.ToolSeq == 2).FirstOrDefault();
            var Tool2_Last_TN_TOOL1002 = ModelService.GetChildList<TN_TOOL1002>(p => p.WorkNo == MasterObj.WorkNo
                                                                            && p.ProcessSeq == MasterObj.ProcessSeq
                                                                            && p.ProcessCode == MasterObj.ProcessCode
                                                                            && p.ToolSeq == 2)
                                                                            .OrderBy(p => p.RowId)
                                                                            .LastOrDefault();
            var Tool2_TN_TOOL1003List = ModelService.GetChildList<TN_TOOL1003>(p => p.WorkNo == MasterObj.WorkNo
                                                                                && p.ProcessSeq == MasterObj.ProcessSeq
                                                                                && p.ProcessCode == MasterObj.ProcessCode
                                                                                && p.ToolSeq == 2).ToList();

            var toolLifeQty2 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode).First().ToolLifeQty2.GetDecimalNullToNull();
            tx_ToolLifeQty2.EditValue = toolLifeQty2 == null ? 50000.ToString("N0") : ((decimal)toolLifeQty2).ToString("N0");
            //tx_ToolUseQty2.EditValue = Tool2_TN_TOOL1003 == null ? "0" : ((decimal)Tool2_TN_TOOL1003.ToolUseQty).ToString("N0");
            tx_ToolUseQty2.EditValue = Tool2_TN_TOOL1003List.Count == 0 ? "0" : ((decimal)Tool2_TN_TOOL1003List.Sum(p => p.ToolUseQty)).ToString("N0");
            if (Tool2_Last_TN_TOOL1002 != null)
                tx_ToolChangeDate2.EditValue = Tool2_Last_TN_TOOL1002.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

            if (toolLifeQty2 <= 0)
                btn_ToolChange2.Enabled = false;

            tx_ToolLifeQty.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            tx_ToolUseQty.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            tx_ToolLifeQty2.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            tx_ToolUseQty2.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            tx_ToolChangeDate.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_ToolChangeDate2.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

        }

        /// <summary>
        /// 툴 연마 버튼 클릭
        /// </summary>
        private void Btn_ToolChange_Click(object sender, EventArgs e)
        {
            var resultQty = 0;//spin_ResultQty.EditValue.GetDecimalNullToZero();
            var toolUseQty = tx_ToolUseQty.EditValue.GetDecimalNullToZero();

            var sumCheckQty = resultQty + toolUseQty;

            var TN_TOOL1002_NewObj = new TN_TOOL1002();
            TN_TOOL1002_NewObj.WorkNo = MasterObj.WorkNo;
            TN_TOOL1002_NewObj.ProcessCode = MasterObj.WorkNo;
            TN_TOOL1002_NewObj.ProcessSeq = MasterObj.ProcessSeq;
            TN_TOOL1002_NewObj.ProductLotNo = MasterObj.ProductLotNo;
            TN_TOOL1002_NewObj.ChangeSeq = MasterObj.TN_TOOL1002List.Where(p => p.ToolSeq == 1).Count() == 0 ? 1 : MasterObj.TN_TOOL1002List.Where(p => p.ToolSeq == 1).Max(p => p.ChangeSeq) + 1;
            TN_TOOL1002_NewObj.ToolSeq = 1;
            TN_TOOL1002_NewObj.ChangeLifeQty = tx_ToolLifeQty.EditValue.GetDecimalNullToZero();
            TN_TOOL1002_NewObj.ChangeQty = sumCheckQty;
            MasterObj.TN_TOOL1002List.Add(TN_TOOL1002_NewObj);
            var Tool1_TN_TOOL1003List = ModelService.GetChildList<TN_TOOL1003>(p => p.WorkNo == MasterObj.WorkNo
                                                                                && p.ProcessSeq == MasterObj.ProcessSeq
                                                                                && p.ProcessCode == MasterObj.ProcessCode
                                                                                && p.ToolSeq == 1).ToList();
            foreach (var v in Tool1_TN_TOOL1003List) 
                ModelService.RemoveChild(v);
            //var TN_TOOL1003 = MasterObj.TN_TOOL1003List.Where(p => p.ToolSeq == 1).FirstOrDefault();
            //MasterObj.TN_TOOL1003List.Remove(TN_TOOL1003);
            //TN_TOOL1003 = null;
            //MasterObj.TN_TOOL1003 = null;
            tx_ToolUseQty.EditValue = 0;
            tx_ToolChangeDate.EditValue = DateTime.Now;

            ModelService.Save();

            var toolLifeQty2 = tx_ToolLifeQty2.EditValue.GetDecimalNullToZero();
            var toolUseQty2 = tx_ToolUseQty2.EditValue.GetDecimalNullToZero();
            if (toolLifeQty2 > 0 && btn_ToolChange2.Enabled)
            {
                if (toolLifeQty2 <= resultQty + toolUseQty2)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_71));
                    btn_Apply.Enabled = false;
                    btn_Cancel.Enabled = false;
                    lup_WorkId.ReadOnly = true;
                    spin_ResultQty.ReadOnly = true;
                    spin_BadQty.ReadOnly = true;
                    lup_BadType.ReadOnly = true;
                    btn_ToolChange.Enabled = false;
                    lup_Machine.ReadOnly = true;
                    return;
                }
            }

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, DialogResult.OK);
            param.SetValue(PopupParameter.Value_2, "툴교체");
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();

            //if (resultQty > 0)
            //{
            //    lup_WorkId.ReadOnly = true;
            //    spin_ResultQty.ReadOnly = true;
            //    spin_BadQty.ReadOnly = true;
            //    lup_BadType.ReadOnly = true;
            //    btn_Cancel.Enabled = false;
            //}

            //btn_ToolChange.Enabled = false;
        }

        /// <summary>
        /// 툴 연마 버튼2 클릭
        /// </summary>
        private void Btn_ToolChange2_Click(object sender, EventArgs e)
        {
            var resultQty = 0;
            var toolUseQty2 = tx_ToolUseQty2.EditValue.GetDecimalNullToZero();

            var sumCheckQty = resultQty + toolUseQty2;

            var TN_TOOL1002_NewObj = new TN_TOOL1002();
            TN_TOOL1002_NewObj.WorkNo = MasterObj.WorkNo;
            TN_TOOL1002_NewObj.ProcessCode = MasterObj.WorkNo;
            TN_TOOL1002_NewObj.ProcessSeq = MasterObj.ProcessSeq;
            TN_TOOL1002_NewObj.ProductLotNo = MasterObj.ProductLotNo;
            TN_TOOL1002_NewObj.ChangeSeq = MasterObj.TN_TOOL1002List.Where(p => p.ToolSeq == 2).Count() == 0 ? 1 : MasterObj.TN_TOOL1002List.Where(p => p.ToolSeq == 2).Max(p => p.ChangeSeq) + 1;
            TN_TOOL1002_NewObj.ToolSeq = 2;
            TN_TOOL1002_NewObj.ChangeLifeQty = tx_ToolLifeQty2.EditValue.GetDecimalNullToZero();
            TN_TOOL1002_NewObj.ChangeQty = sumCheckQty;
            MasterObj.TN_TOOL1002List.Add(TN_TOOL1002_NewObj);
            var Tool2_TN_TOOL1003List = ModelService.GetChildList<TN_TOOL1003>(p => p.WorkNo == MasterObj.WorkNo
                                                                                && p.ProcessSeq == MasterObj.ProcessSeq
                                                                                && p.ProcessCode == MasterObj.ProcessCode
                                                                                && p.ToolSeq == 2).ToList();
            foreach (var v in Tool2_TN_TOOL1003List)
                ModelService.RemoveChild(v);
            //var TN_TOOL1003 = MasterObj.TN_TOOL1003List.Where(p => p.ToolSeq == 2).FirstOrDefault();
            //MasterObj.TN_TOOL1003List.Remove(TN_TOOL1003);
            //MasterObj.TN_TOOL1003 = null;
            tx_ToolUseQty2.EditValue = 0;
            tx_ToolChangeDate2.EditValue = DateTime.Now;

            ModelService.Save();

            var toolLifeQty = tx_ToolLifeQty.EditValue.GetDecimalNullToZero();
            var toolUseQty = tx_ToolUseQty.EditValue.GetDecimalNullToZero();
            if (toolLifeQty > 0 && btn_ToolChange.Enabled)
            {
                if (toolLifeQty <= resultQty + toolUseQty)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_71));
                    btn_Apply.Enabled = false;
                    btn_Cancel.Enabled = false;
                    lup_WorkId.ReadOnly = true;
                    spin_ResultQty.ReadOnly = true;
                    spin_BadQty.ReadOnly = true;
                    lup_BadType.ReadOnly = true;
                    btn_ToolChange2.Enabled = false;
                    lup_Machine.ReadOnly = true;
                    return;
                }
            }

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, DialogResult.OK);
            param.SetValue(PopupParameter.Value_2, "툴교체");
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
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

                var toolLifeQty = tx_ToolLifeQty.EditValue.GetDecimalNullToZero();
                var toolUseQty = tx_ToolUseQty.EditValue.GetDecimalNullToZero();

                var toolLifeQty2 = tx_ToolLifeQty2.EditValue.GetDecimalNullToZero();
                var toolUseQty2 = tx_ToolUseQty2.EditValue.GetDecimalNullToZero();
                
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
                if (badQty == 0)
                {
                    if (resultQty <= 0)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_122), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("ResultQty")));
                            return;
                    }
                }
                if (spin_WorkTime.EditValue.GetDecimalNullToZero() == 0)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_121), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("ResultQty")));
                    return;
                }
                //////if (resultQty - badQty < 0)
                //////{
                //////    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_69), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("ResultQty")));
                //////    return;
                //////}

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
                        checkItemMoveObj.OkSumQty = sumOkQty + (resultQty - badQty);
                        checkItemMoveObj.BadSumQty = sumBadQty + badQty;
                        checkItemMoveObj.ResultQty += resultQty;
                        checkItemMoveObj.OkQty += (resultQty - badQty);
                        checkItemMoveObj.BadQty += (sumBadQty + badQty);
                        checkItemMoveObj.UpdateTime = DateTime.Now;
                        ModelService.UpdateChild(checkItemMoveObj);
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

                #region 툴 사용량 로직
                if (btn_ToolChange.Enabled)
                    ToolUsingQty();
                if (btn_ToolChange2.Enabled)
                    ToolUsingQty2();
                #endregion

                if (toolLifeQty > 0 && btn_ToolChange.Enabled)
                {
                    if (toolLifeQty <= resultQty + toolUseQty)
                    {
                        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_71));
                        btn_Apply.Enabled = false;
                        btn_Cancel.Enabled = false;
                        lup_WorkId.ReadOnly = true;
                        spin_ResultQty.ReadOnly = true;
                        spin_BadQty.ReadOnly = true;
                        lup_BadType.ReadOnly = true;
                        lup_Machine.ReadOnly = true;
                    }
                    else
                    {
                        PopupDataParam param = new PopupDataParam();
                        param.SetValue(PopupParameter.Value_1, DialogResult.OK);
                        ReturnPopupArgument = new PopupArgument(param);
                        ActClose();
                    }
                }
                else if (toolLifeQty2 > 0 && btn_ToolChange2.Enabled)
                {
                    if (toolLifeQty2 <= resultQty + toolUseQty2)
                    {
                        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_71));
                        btn_Apply.Enabled = false;
                        btn_Cancel.Enabled = false;
                        lup_WorkId.ReadOnly = true;
                        spin_ResultQty.ReadOnly = true;
                        spin_BadQty.ReadOnly = true;
                        lup_BadType.ReadOnly = true;
                        lup_Machine.ReadOnly = true;
                    }
                    else
                    {
                        PopupDataParam param = new PopupDataParam();
                        param.SetValue(PopupParameter.Value_1, DialogResult.OK);
                        ReturnPopupArgument = new PopupArgument(param);
                        ActClose();
                    }
                }
                else
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.Value_1, DialogResult.OK);
                    ReturnPopupArgument = new PopupArgument(param);
                    ActClose();
                }
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
        
        /// <summary>
        /// 툴 사용량 로직
        /// </summary>
        private void ToolUsingQty()
        {
            var resultQty = spin_ResultQty.EditValue.GetDecimalNullToZero();

            var checkObj = ModelService.GetChildList<TN_TOOL1003>(p => p.WorkNo == MasterObj.WorkNo
                                                                    && p.ProcessCode == MasterObj.ProcessCode
                                                                    && p.ProcessSeq == MasterObj.ProcessSeq
                                                                    //&& p.ProductLotNo == MasterObj.ProductLotNo
                                                                    && p.ToolSeq == 1
                                                                 ).FirstOrDefault();
            if (checkObj != null)
            {
                checkObj.ToolUseQty += resultQty;
                checkObj.UpdateTime = DateTime.Now;
                ModelService.UpdateChild(checkObj);
                tx_ToolUseQty.EditValue = checkObj.ToolUseQty.GetDecimalNullToZero().ToString("N0");
            }
            else
            {
                ModelService.InsertChild(new TN_TOOL1003()
                {
                    WorkNo = MasterObj.WorkNo,
                    ProcessCode = MasterObj.ProcessCode,
                    ProcessSeq = MasterObj.ProcessSeq,
                    ProductLotNo = MasterObj.ProductLotNo,
                    ToolUseQty = resultQty,
                    ToolSeq = 1,
                });
                tx_ToolUseQty.EditValue = resultQty.GetDecimalNullToZero().ToString("N0");
            }

            ModelService.Save();
        }

        /// <summary>
        /// 툴2 사용량 로직
        /// </summary>
        private void ToolUsingQty2()
        {
            var resultQty = spin_ResultQty.EditValue.GetDecimalNullToZero();

            var checkObj = ModelService.GetChildList<TN_TOOL1003>(p => p.WorkNo == MasterObj.WorkNo
                                                                    && p.ProcessCode == MasterObj.ProcessCode
                                                                    && p.ProcessSeq == MasterObj.ProcessSeq
                                                                    //&& p.ProductLotNo == MasterObj.ProductLotNo
                                                                    && p.ToolSeq == 2
                                                                 ).FirstOrDefault();
            if (checkObj != null)
            {
                checkObj.ToolUseQty += resultQty;
                checkObj.UpdateTime = DateTime.Now;
                ModelService.UpdateChild(checkObj);
                tx_ToolUseQty2.EditValue = checkObj.ToolUseQty.GetDecimalNullToZero().ToString("N0");
            }
            else
            {
                ModelService.InsertChild(new TN_TOOL1003()
                {
                    WorkNo = MasterObj.WorkNo,
                    ProcessCode = MasterObj.ProcessCode,
                    ProcessSeq = MasterObj.ProcessSeq,
                    ProductLotNo = MasterObj.ProductLotNo,
                    ToolUseQty = resultQty,
                    ToolSeq = 2,
                });
                tx_ToolUseQty2.EditValue = resultQty.GetDecimalNullToZero().ToString("N0");
            }

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
    }
}
