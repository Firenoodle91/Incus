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
using DevExpress.XtraGrid;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 실적등록 팝업 (리워크)
    /// </summary>
    public partial class XPFRESULT_REWORK : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1205> ModelService = (IService<TN_MPS1205>)ProductionFactory.GetDomainService("TN_MPS1205");
        IService<TN_MPS1207> ModelServiceDTL = (IService<TN_MPS1207>)ProductionFactory.GetDomainService("TN_MPS1207");

        TN_MPS1205 MasterObj;
        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;
        protected BindingSource BadBindingSource = new BindingSource();
        public XPFRESULT_REWORK()
        {
            InitializeComponent();
        }

        public XPFRESULT_REWORK(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("ResultAdd");

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

            //if (TEMP_XFPOP1000_Obj.ProcessSeq > 1)
            //{
            //    var TN_MPS1205 = ModelService.GetList(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo 
            //                                            && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode 
            //                                            && p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq 
            //                                            && p.ResultEndDate == null).LastOrDefault();
            //    //현 이동표번호 가져오기
            //    var currentItemMoveNo = TN_MPS1205 == null ? string.Empty : TN_MPS1205.ItemMoveNo;

            //    var previousItemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == currentItemMoveNo
            //                                                                        && p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo
            //                                                                        && p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq - 1).FirstOrDefault();
            //    if (previousItemMoveObj != null)
            //    {
            //        spin_ResultQty.EditValue = previousItemMoveObj.ResultQty.GetDecimalNullToZero();
            //    }
            //}

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
            lup_WorkId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.DepartmentCode == "DEPT-00003"
                                                                       && p.Active == "Y"

                                                                   )
                                                                   .ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //var procTeamCode = TEMP_XFPOP1000_Obj.ProcTeamCode.GetNullToNull();
            ////lup_WorkId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => (string.IsNullOrEmpty(procTeamCode) ? true : p.ProductTeamCode == procTeamCode)
            ////                                                                                        && p.Active == "Y"
            ////                                                                                        && p.UserUserGroupList.Any(c => c.UserGroupId == MasterCodeSTR.UserGroup_Technician)
            ////                                                                                   )
            ////                                                                                   .ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lup_WorkId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p =>p.Active == "Y"
            //                                                                                     && p.UserUserGroupList.Any(c => c.UserGroupId == MasterCodeSTR.UserGroup_Product1 || c.UserGroupId == MasterCodeSTR.UserGroup_Product2)

            //                                                                                )
            //                                                                                .ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            // lupWorkGroup.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.WorkGroupCode));
            List<TN_STD1000> badlist = DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP).ToList();
            if (badlist.Count == 0) { badlist = DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP); }

            lup_BadType.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), badlist);

            lup_Machine.SetFontSize(new Font("맑은 고딕", 14f));
            lup_WorkId.SetFontSize(new Font("맑은 고딕", 14f));
            lup_BadType.SetFontSize(new Font("맑은 고딕", 14f));
            lupWorkGroup.SetFontSize(new Font("맑은 고딕", 14f));

            lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_WorkId.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_BadType.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lupWorkGroup.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

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
            spin_SumResultQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1205List.Sum(p => p.ResultSumQty).GetDecimalNullToZero();
            spin_SumOkQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1205List.Sum(p => p.OkSumQty).GetDecimalNullToZero();
            spin_SumBadQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1205List.Sum(p => p.BadSumQty).GetDecimalNullToZero();
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
                var badQty = spin_BadQtySum.EditValue.GetDecimalNullToZero();
                var WorkGroup= lupWorkGroup.EditValue.GetNullToNull();
                //     var badType = lup_BadType.EditValue.GetNullToNull();

                if (workId.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("WorkId")));
                    return;
                }
                //if (WorkGroup.IsNullOrEmpty())
                //{
                //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("WorkGroup")));
                //    return;
                //}

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
                if (badQty == 0)
                {
                    if (resultQty <= 0)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_122), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("ResultQty")));
                        return;
                    }
                }

                //if (resultQty - badQty < 0)
                //{
                //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_69), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("ResultQty")));
                //    return;
                //}

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

                TN_MPS1206 detailNewObj = new TN_MPS1206();
                detailNewObj.ResultSeq = MasterObj.TN_MPS1206List.Count == 0 ? 1 : MasterObj.TN_MPS1206List.Max(p => p.ResultSeq) + 1;
                detailNewObj.ItemCode = MasterObj.ItemCode;
                detailNewObj.CustomerCode = MasterObj.CustomerCode;
                detailNewObj.MachineCode = machineCode;
                detailNewObj.ResultInsDate = DateTime.Today;
                detailNewObj.ResultQty = resultQty;
                detailNewObj.OkQty = (resultQty - badQty);
                detailNewObj.BadQty = badQty;
              //  detailNewObj.BadType = badType;
                detailNewObj.WorkId = workId;
                detailNewObj.WorkTime = spin_WorkTime.EditValue.GetDecimalNullToZero();
                detailNewObj.Temp = WorkGroup;

                //if (MasterObj.ProcessSeq > 1)
                //{
                //    detailNewObj.ItemMoveNo = MasterObj.ItemMoveNo;

                //    var checkItemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.WorkNo == MasterObj.WorkNo
                //                                                                    && p.ProcessCode == MasterObj.ProcessCode
                //                                                                    && p.ProcessSeq == MasterObj.ProcessSeq
                //                                                                    && p.ProductLotNo == MasterObj.ProductLotNo
                //                                                                    && p.ItemMoveNo == MasterObj.ItemMoveNo).FirstOrDefault();
                //    if (checkItemMoveObj != null)
                //    {
                //        var sumResultQty = spin_SumResultQty.EditValue.GetDecimalNullToZero();
                //        var sumOkQty = spin_SumOkQty.EditValue.GetDecimalNullToZero();
                //        var sumBadQty = spin_SumBadQty.EditValue.GetDecimalNullToZero();
                //        checkItemMoveObj.ResultSumQty = sumResultQty + resultQty;
                //        checkItemMoveObj.OkSumQty = sumOkQty + (resultQty - badQty);
                //        checkItemMoveObj.BadSumQty = sumBadQty + badQty;
                //        checkItemMoveObj.ResultQty += resultQty;
                //        checkItemMoveObj.OkQty += (resultQty - badQty);
                //        checkItemMoveObj.BadQty += (sumBadQty + badQty);
                //        checkItemMoveObj.UpdateTime = DateTime.Now;
                //        ModelService.UpdateChild(checkItemMoveObj);
                //    }
                //}

                if (badQty > 0)
                {
                    GridView gv = gridEx1.MainGrid.MainView as GridView;
                  
                    for (int i = 0; i < gv.RowCount; i++)
                    {
                        TN_MPS1207 nobj = new TN_MPS1207();
                        
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
                        nobj.Temp = WorkGroup;
                        nobj.InSeq = DbRequestHandler.GetRowCount("EXEC SP_MPS1207_CNT '" + detailNewObj.ResultInsDate + "','" + MasterObj.WorkNo + "','" + MasterObj.ProcessCode + "','" + detailNewObj.ResultSeq + "'") == 0 ? 1 : DbRequestHandler.GetRowCount("EXEC SP_MPS1207_CNT '" + detailNewObj.ResultInsDate + "','" + MasterObj.WorkNo + "','" + MasterObj.ProcessCode + "','" + detailNewObj.ResultSeq + "'") + 1;
                        nobj.BadQty = gv.GetRowCellValue(i, gv.Columns[1]).GetIntNullToZero();
                        nobj.BadType = gv.GetRowCellValue(i, gv.Columns[0]).GetNullToEmpty();

                        ModelServiceDTL.Insert(nobj);
                        ModelServiceDTL.Save();

                    }
                }

                MasterObj.TN_MPS1206List.Add(detailNewObj);
                MasterObj.UpdateTime = DateTime.Now;
                MasterObj.UpdateId = workId;

                ModelService.Save();

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
        public class fail_List {
            public fail_List() { }
            public string BadType{ get; set; }
            public decimal BadQty { get; set; }
        }
    }
}
