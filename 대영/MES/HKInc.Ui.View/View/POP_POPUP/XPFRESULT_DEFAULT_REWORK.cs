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
    /// 20210614 오세완 차장
    /// 대영 스타일 리워크 실적등록 팝업
    /// </summary>
    public partial class XPFRESULT_DEFAULT_REWORK : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");
        IService<TN_MPS1204> ModelServiceDTL = (IService<TN_MPS1204>)ProductionFactory.GetDomainService("TN_MPS1204");

        TN_MPS1201 MasterObj;

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


        public XPFRESULT_DEFAULT_REWORK()
        {
            InitializeComponent();
        }

        public XPFRESULT_DEFAULT_REWORK(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("ReworkResultAdd");

            BadBindingSource.DataSource = new List<fail_List>();
            gridEx1.DataSource = BadBindingSource;
            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitControls()
        {
            base.InitControls();

            btn_Apply.Click += Btn_Apply_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
            btn_Add.Click += Btn_Add_Click;
            btn_Del.Click += Btn_Del_Click;
            spin_OkQty.Click += Spin_Click;
            spin_BadQtySum.Click += Spin_Click;
            spin_BadQty.Click += Spin_Click;
            spin_WorkTime.Click += Spin_Click;
            tx_ItemMoveNo.Click += Tx_ItemMoveNo_Click;
            tx_ItemMoveNo.KeyDown += Tx_ItemMoveNo_KeyDown;
            lup_Processcode.EditValueChanged += Lup_Processcode_EditValueChanged;

            gridEx1.MainGrid.MainView.Click += MainView_Click;
            Set_Input(0);

        }

        private void Lup_Processcode_EditValueChanged(object sender, EventArgs e)
        {
            string sItemmoveno = tx_ItemMoveNo.EditValue.GetNullToEmpty();
            string sProcesscode = lup_Processcode.EditValue.GetNullToEmpty();
            if(sProcesscode != null)
            {
                List<TN_MPS1201> tempArr = ModelService.GetList(p => p.ItemMoveNo == sItemmoveno && 
                                                                     p.ProcessCode == sProcesscode).ToList();
                if(tempArr != null)
                    if(tempArr.Count > 0)
                    {
                        MasterObj = tempArr.FirstOrDefault();
                        Set_Input(2);
                    }
            }
        }

        private void Tx_ProductLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_WorkNo();
            }
        }

        private void Tx_ItemMoveNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Search_WorkNo();
            }
        }

        private void Tx_ItemMoveNo_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad) return;
            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                tx_ItemMoveNo.EditValue = keyPad.returnval;
                Search_WorkNo();
            }
        }

        /// <summary>
        /// 20210614 오세완 차장
        /// 사용자가 이동표번호하고 생산 LOTNO를 입력하였을 때 해당 작업지시를 찾기
        /// </summary>
        private void Search_WorkNo()
        {
            string sItemmoveno = tx_ItemMoveNo.EditValue.GetNullToEmpty();

            if (sItemmoveno != "")
            {
                // 20210627 오세완 차장 이동표번호가 2개 이상일 수 있기 때문에 TN_ITEM_MOVE에서 조회로 변경처리
                //List<TN_MPS1201> tempArr = ModelService.GetList(p => p.ItemMoveNo == sItemmoveno ).ToList();
                List<TN_ITEM_MOVE> tempArr = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == sItemmoveno).ToList();
                if (tempArr != null)
                    if (tempArr.Count > 0)
                    {
                        List<TN_STD1000> processcode_Arr = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
                        if(processcode_Arr != null)
                            if(processcode_Arr.Count > 0)
                            {
                                List<TN_STD1000> rework_process_Arr = new List<TN_STD1000>();
                                foreach(TN_STD1000 each in processcode_Arr)
                                {
                                    TN_ITEM_MOVE itemMoveEach = tempArr.FirstOrDefault();
                                    List<TN_MPS1201> mpsArr = ModelService.GetList(p => p.WorkNo == itemMoveEach.WorkNo);

                                    if(mpsArr != null)
                                        if(mpsArr.Count > 0)
                                        {
                                            foreach (TN_MPS1201 each2 in mpsArr)
                                            {
                                                if (each.CodeVal == each2.ProcessCode)
                                                {
                                                    rework_process_Arr.Add(each);
                                                }
                                            }
                                        }
                                    
                                }

                                lup_Processcode.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), rework_process_Arr, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
                                tx_Read_ItemMoveNo.EditValue = sItemmoveno;
                                Set_Input(1);
                            }
                    }
                    else
                        Set_Input(0);
            }
            else
                Set_Input(0);
        }

        /// <summary>
        /// 20210614 오세완 차장 
        /// 사용자가 이동표번호하고 생산 lotno를 인식시키기 전에 다른 control을 못누르게 처리
        /// </summary>
        /// <param name="sStatus"> 0 : 이동표 번호가 인식이 안된 경우, 1 : 이동표 번호는 인식이 되어서 공정만 입력할 수 있는 경우 , 2 : 공정을 입력하여 전체를 입력할 수 있는 경우</param>
        private void Set_Input(short sStatus)
        {
            bool bEnable = false;
            if (sStatus < 2)
            {
                bEnable = false;
            }
            else
                bEnable = true;

            if(sStatus == 0)
            {
                tx_Read_ItemMoveNo.EditValue = "";
            }

            if(sStatus == 1)
                lup_Processcode.Enabled = true;
            else
                lup_Processcode.Enabled = bEnable;

            lup_WorkId.Enabled = bEnable;
            spin_WorkTime.Enabled = bEnable;
            spin_OkQty.Enabled = bEnable;
            lup_BadType.Enabled = bEnable;
            spin_BadQty.Enabled = bEnable;
            btn_Add.Enabled = bEnable;
            btn_Del.Enabled = bEnable;
            btn_Apply.Enabled = bEnable;
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
            lc_Read_ItemMoveNo.Text = LabelConvert.GetLabelText("ItemMoveNo");
            lc_ProcessCode.Text = LabelConvert.GetLabelText("Process");

            lup_WorkId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            
            List<TN_STD1000> badlist = DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP).ToList();
            if (badlist.Count == 0) { badlist = DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP); }

            lup_BadType.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), badlist);

            lup_WorkId.SetFontSize(new Font("맑은 고딕", 14f));
            lup_BadType.SetFontSize(new Font("맑은 고딕", 14f));

            lup_WorkId.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_BadType.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            spin_OkQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_OkQty.Properties.Mask.EditMask = "n0";
            spin_OkQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_OkQty.Properties.Buttons[0].Visible = false;

            spin_BadQtySum.Properties.Mask.MaskType = MaskType.Numeric;
            spin_BadQtySum.Properties.Mask.EditMask = "n0";
            spin_BadQtySum.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_BadQtySum.Properties.Buttons[0].Visible = false;


            spin_BadQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_BadQty.Properties.Mask.EditMask = "n0";
            spin_BadQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_BadQty.Properties.Buttons[0].Visible = false;

            spin_WorkTime.Properties.Mask.MaskType = MaskType.Numeric;
            spin_WorkTime.Properties.Mask.EditMask = "n2";
            spin_WorkTime.Properties.Mask.UseMaskAsDisplayFormat = true; 
        }

        /// <summary>
        /// 적용 버튼 클릭
        /// </summary>
        private void Btn_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                var workId = lup_WorkId.EditValue.GetNullToNull();
                var okQty = spin_OkQty.EditValue.GetDecimalNullToZero();
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

                if(!bType_null && !bQty_zero)
                {
                    int iCount = BadBindingSource.List.Count;
                    if(iCount == 0)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_146), LabelConvert.GetLabelText("BadType")));
                        //MessageBoxHandler.Show("불량 유형을 추가해 주시기 바랍니다. ");
                        return;
                    }
                }

                if (badQty == 0)
                {
                    if (okQty <= 0)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_122), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("ResultQty")));
                        return;
                    }
                }
                
                decimal dBadQtySum = spin_BadQtySum.EditValue.GetDecimalNullToZero();

                if (MasterObj.ReworkOkSumQty == null)
                {
                    MasterObj.ReworkOkSumQty = 0;
                    MasterObj.ReworkOkSumQty = okQty;
                }
                else
                    MasterObj.ReworkOkSumQty += okQty;

                if(MasterObj.ReworkBadSumQty == null)
                {
                    MasterObj.ReworkBadSumQty = 0;
                    MasterObj.ReworkBadSumQty = dBadQtySum;
                }
                else
                    MasterObj.ReworkBadSumQty += dBadQtySum;

                TN_MPS1202 detailNewObj = new TN_MPS1202();
                detailNewObj.ResultSeq = MasterObj.TN_MPS1202List.Count == 0 ? 1 : MasterObj.TN_MPS1202List.Max(p => p.ResultSeq) + 1;
                detailNewObj.ItemCode = MasterObj.ItemCode;
                detailNewObj.CustomerCode = MasterObj.CustomerCode;
                detailNewObj.ResultInsDate = DateTime.Today;
                detailNewObj.ResultQty = okQty + dBadQtySum;
                detailNewObj.OkQty = okQty;
                detailNewObj.BadQty = dBadQtySum;
                detailNewObj.WorkId = workId;
                detailNewObj.WorkTime = spin_WorkTime.EditValue.GetDecimalNullToZero();
                detailNewObj.Temp1 = "REWORK";
                detailNewObj.ItemMoveNo = tx_Read_ItemMoveNo.EditValue.GetNullToEmpty();

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
                        nobj.BadQty = gv.GetRowCellValue(i, gv.Columns[1]).GetIntNullToZero();
                        nobj.BadType = gv.GetRowCellValue(i, gv.Columns[0]).GetNullToEmpty();
                        nobj.Temp1 = "REWORK";
                          
                        ModelServiceDTL.Insert(nobj);
                    }
                    // 20210612 오세완 차장 성능상 빼는게 나을 듯 하여 처리
                    ModelServiceDTL.Save();
                }

                MasterObj.TN_MPS1202List.Add(detailNewObj);
                MasterObj.UpdateTime = DateTime.Now;
                MasterObj.UpdateId = workId;
                ModelService.Save();

                #region 타발수 증감 로직
                // 20210612 오세완 차장 TN_MPS1201T 테이블에 TR_MPS1201_IU 트리거 참조, TEMP1에 REWORK로 지정했기 때문에 타발수는 증가하지 않음
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
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                spinEdit.EditValue = keyPad.returnval;
            }
        }
        
    }
}
