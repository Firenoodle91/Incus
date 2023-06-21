using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Handler;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using DevExpress.Utils;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 20210613 오세완 차장
    /// 대영 스타일 이동표 인식 및 작업시작 팝업
    /// </summary>
    public partial class XPFITEMMOVESCAN_START_V2 : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");
        IService<TN_STD1300> ModelServiceBom = (IService<TN_STD1300>)ProductionFactory.GetDomainService("TN_STD1300");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        private string ItemMoveNo = null;

        /// <summary>
        /// 20210613 오세완 차장
        /// 이동표 영역이 인식이 완료되면 우측 영역을 건들 수 있게 하기 위함
        /// </summary>
        private bool bInit_Left = false;

        /// <summary>
        /// 20210618 오세완 차장 
        /// 금형코드가 품목기준정보에 매칭되어 있으면 false, 매칭되어 있지 않으면 true
        /// </summary>
        private bool bNeedMoldCode = false;

        //bomList 저장 전역변수
        List<TEMP_XFPOP1000_WORKSTART_INFO> workInfoBomList;
        List<VI_SRC_USE> srcItemList = new List<VI_SRC_USE>();
        #endregion

        public XPFITEMMOVESCAN_START_V2()
        {
            InitializeComponent();
        }

        public XPFITEMMOVESCAN_START_V2(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkStart");
            GridExControl = gridEx1;

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);

            lup_MachineGroup.EditValueChanged += Lup_MachineGroup_EditValueChanged;
            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;
            lup_Machine.Popup += Lup_Machine_Popup;

            tx_ItemMoveNo.KeyDown += Tx_ItemMoveNo_KeyDown;
            tx_ItemMoveNo.Click += Tx_ItemMoveNo_Click;
            tx_OutLotNo.KeyDown += Tx_OutLotNo_KeyDown;
            tx_OutLotNo.Click += Tx_OutLotNo_Click;
            tx_Moldmcode.KeyDown += Tx_Moldmcode_KeyDown;
            tx_Moldmcode.Click += Tx_Moldmcode_Click;

            btn_Start.Click += Btn_Start_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

            //this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            //this.Size = this.MinimumSize;
        }

        private void Tx_Moldmcode_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad) return;
            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                tx_Moldmcode.EditValue = keyPad.returnval;
                Search_MoldMatching();
            }
        }

        /// <summary>
        /// 20210609 오세완 차장 
        /// 금형이 일상점검이 포함된 경우 메시지 창을 뜨게 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tx_Moldmcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_MoldMatching();
            }
        }

        /// <summary>
        /// 20210611 오세완 차장
        /// 프레스 공정인 경우 금형을 매칭할 수 있게 처리
        /// </summary>
        private void Search_MoldMatching()
        {
            try
            {
                string sMoldmcode = tx_Moldmcode.EditValue.GetNullToEmpty();
                List<TN_MOLD1100> tempArr = ModelService.GetChildList<TN_MOLD1100>(p => p.MoldMCode == sMoldmcode &&
                                                                                        p.UseYN == "Y").ToList();
                if (tempArr != null)
                    if (tempArr.Count > 0)
                    {
                        TN_MOLD1100 temp_mold = tempArr.FirstOrDefault();

                        // 20210611 오세완 차장 올바른 금형을 매칭하면 일상점검 내용을 출력할 수 있게 함
                        bool bCorrectMatching = false;
                        string sMessage = "";

                        List<TN_STD1100> std_Arr = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode &&
                                                                                              p.UseFlag == "Y").ToList();
                        if (std_Arr != null)
                            if (std_Arr.Count > 0)
                            {
                                TN_STD1100 std = std_Arr.FirstOrDefault();
                                if (std.MoldCode.GetNullToEmpty() != "")
                                {
                                    if (std.MoldCode != sMoldmcode)
                                    {
                                        sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_147);
                                        MessageBoxHandler.Show(sMessage);
                                        //MessageBoxHandler.Show("올바른 금형이 아닙니다.");
                                        tx_Moldmcode.EditValue = "";
                                    }
                                    else
                                        bCorrectMatching = true;
                                }
                                else
                                    bCorrectMatching = true;
                            }

                        if (bCorrectMatching)
                        {
                            if (temp_mold.MoldDayInspFlag.GetNullToEmpty() == "Y")
                            {
                                List<TN_MOLD1500> temp_moldcheck = ModelService.GetChildList<TN_MOLD1500>(p => p.MoldMCode == sMoldmcode &&
                                                                                                               p.CheckDate == DateTime.Today).ToList();
                                int iCount = temp_moldcheck.Count;
                                if (iCount <= 0)
                                {
                                    sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_148);
                                    MessageBoxHandler.Show(sMessage);
                                    //MessageBoxHandler.Show("금형일상점검 대상인 금형입니다.");
                                }
                            }

                            // 20210625 오세완 차장 저장전에 code값을 변경할 수 있어서 추가
                            tx_Moldmcode.Enabled = false;
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
                tx_Moldmcode.Focus();
            }

        }

        private void Tx_OutLotNo_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad) return;
            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                tx_OutLotNo.EditValue = keyPad.returnval;
                Search_OutLotNo();
            }
        }

        private void Lup_MachineGroup_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null)
                return;

            lup_Machine.EditValue = null;
        }

        private void Lup_Machine_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var value = lookup.EditValue.GetNullToEmpty();
            if (!value.IsNullOrEmpty())
            {
                var nowMachineStateObj = ModelService.GetChildList<VI_XRREP6000_LIST>(p => p.MachineMCode == value).FirstOrDefault();
                if (nowMachineStateObj != null)
                {
                    //대영정밀 설비 중복허용 가능
                    if (nowMachineStateObj.JobStates == MasterCodeSTR.JobStates_Stop)
                    {
                        MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_173));
                        lookup.EditValue = null;
                        return;
                    }
                }

                var data = lookup.GetSelectedDataRow() as TN_MEA1000;
                if (data != null)
                {
                    if (data.DailyCheckFlag == "Y")
                    {
                        if (ModelService.GetChildList<TN_MEA1003>(p => p.MachineCode == data.MachineMCode && p.CheckDate == DateTime.Today).FirstOrDefault() == null)
                        {
                            MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_116));
                            //MessageBoxHandler.Show("해당 설비에 일일점검이 되어있지 않습니다. 확인 부탁드립니다.");
                            lookup.EditValue = null;
                        }
                    }
                }

                DataLoad();
            }
        }

        protected override void DataLoad()
        {
            if (!bInit_Left)
                return;

            if (TEMP_XFPOP1000_Obj == null)
                return;

            string sSql = "exec USP_GET_XFPOP1000_WORKSTART_INFO '" + TEMP_XFPOP1000_Obj.WorkNo + "', '" + TEMP_XFPOP1000_Obj.ProcessCode + "'";
            DataSet ds = DbRequestHandler.GetDataQury(sSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    // 20210608 오세완 차장 생산품목 상단 출력
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string sCategory_name = ds.Tables[0].Rows[0]["CATEGORY_NAME"].GetNullToEmpty();
                        string sItemcode = ds.Tables[0].Rows[0]["ITEM_CODE"].GetNullToEmpty();
                        string sItemname = ds.Tables[0].Rows[0]["ITEM_NAME"].GetNullToEmpty();
                        //string sItemname1 = ds.Tables[0].Rows[0]["ITEM_NAME1"].GetNullToEmpty();

                        tx_Top_Category.EditValue = sCategory_name;
                        tx_Top_Itemcode.EditValue = sItemcode;
                        tx_Top_Itemname.EditValue = sItemname;
                        //tx_Top_Itemname1.EditValue = sItemname1;
                    }

                    tx_OutLotNo.Enabled = true;
                    lup_Machine.Enabled = true;
                    lup_MachineGroup.Enabled = true;

                    if (TEMP_XFPOP1000_Obj.ProcessCode == MasterCodeSTR.Process_Press)
                    {
                        // 20210618 오세완 차장 품목기준정보에 금형코드를 매칭하였으면 입력 받지 않기로
                        List<TN_STD1100> tempArr = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode &&
                                                                                              p.UseFlag == "Y").ToList();
                        if (tempArr != null)
                            if (tempArr.Count > 0)
                            {
                                TN_STD1100 temp_each = tempArr.FirstOrDefault();
                                if (temp_each.MoldCode.GetNullToEmpty() == "")
                                    bNeedMoldCode = true;
                                else
                                {
                                    // 20210619 오세완 차장 품목기존정보에 매칭된 금형코드가 금형일상점검을 시행했는지 확인
                                    string sMoldcode = temp_each.MoldCode;
                                    List<TN_MOLD1100> temp_moldArr = ModelService.GetChildList<TN_MOLD1100>(p => p.MoldMCode == sMoldcode &&
                                                                                                                 p.UseYN == "Y").ToList();
                                    if (temp_moldArr != null)
                                        if (temp_moldArr.Count > 0)
                                        {
                                            TN_MOLD1100 temp_mold_each = temp_moldArr.FirstOrDefault();
                                            if (temp_mold_each.MoldDayInspFlag.GetNullToEmpty() == "Y")
                                            {
                                                List<TN_MOLD1500> temp_moldcheck = ModelService.GetChildList<TN_MOLD1500>(p => p.MoldMCode == sMoldcode &&
                                                                                                                               p.CheckDate == DateTime.Today).ToList();
                                                int iCount = temp_moldcheck.Count;
                                                if (iCount <= 0)
                                                {
                                                    string sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_148);
                                                    MessageBoxHandler.Show(sMessage);
                                                    //MessageBoxHandler.Show("금형일상점검 대상인 금형입니다.");
                                                }
                                            }
                                        }

                                    // 20210629 오세완 차장 기인식한 코드도 값을 넣어줘야 전달을 시킨다. 
                                    tx_Moldmcode.EditValue = temp_each.MoldCode;
                                }
                            }
                    }


                    if (bNeedMoldCode)
                    {
                        // 20210627 오세완 차장 금형을 인식해야 되면 
                        tx_Moldmcode.Enabled = true;
                    }
                    else
                    {
                        // 20210611 오세완 차장 프레스 공정 아니면 금형은 출력 안하는 걸로
                        lcMoldMCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    }

                    //BOM정보 전역변수로 저장
                    if (ds.Tables.Count > 1)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            workInfoBomList = new List<TEMP_XFPOP1000_WORKSTART_INFO>();
                            //List<TEMP_XFPOP1000_WORKSTART_INFO> tempArr = new List<TEMP_XFPOP1000_WORKSTART_INFO>();
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                TEMP_XFPOP1000_WORKSTART_INFO newTemp = new TEMP_XFPOP1000_WORKSTART_INFO()
                                {
                                    TYPE = ds.Tables[1].Rows[i]["TYPE"].GetNullToEmpty(),
                                    ITEM_CODE = ds.Tables[1].Rows[i]["ITEM_CODE"].GetNullToEmpty(),
                                    ITEM_NAME = ds.Tables[1].Rows[i]["ITEM_NAME"].GetNullToEmpty(),
                                    ITEM_NAME1 = ds.Tables[1].Rows[i]["ITEM_NAME1"].GetNullToEmpty(),
                                    PROCESS_CODE = ds.Tables[1].Rows[i]["PROCESS_CODE"].GetNullToEmpty(),
                                    USE_QTY = ds.Tables[1].Rows[i]["USE_QTY"].GetDecimalNullToZero()
                                };
                                //tempArr.Add(newTemp);
                                workInfoBomList.Add(newTemp);
                            }

                            //ModelBindingSource.DataSource = tempArr;
                            //GridExControl.DataSource = ModelBindingSource;
                            //GridExControl.MainGrid.BestFitColumns();

                            //gridEx1.Enabled = true;
                        }
                    }
                }
            }

        }

        private void Lup_Machine_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var machineGroupCode = lup_MachineGroup.EditValue.GetNullToNull();

            if (machineGroupCode.IsNullOrEmpty())
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            else
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[MachineGroupCode] = '" + machineGroupCode + "'";
        }

        /// <summary>
        /// 20210613 오세완 차장
        /// 부품 이동표 번호 인식에 따라서 메시지 다르게 출력하고 이동표 번호 초기화처리
        /// </summary>
        /// <param name="bDiffrent_Workno">true - 작업지시번호가 다릅니다 메시지 출력, false - 이동표번호가 맞지 않습니다 메시지 출력</param>
        private void Init_ItemMoveNo(bool bDiffrent_Workno)
        {
            string sMessage = "";
            if (bDiffrent_Workno)
            {
                sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_64);
            }
            else
            {
                sMessage = string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo"));
            }

            MessageBoxHandler.Show(sMessage);
            ItemMoveNo = null;
            tx_ItemMoveNo.EditValue = null;
            tx_Workno.EditValue = null;
            tx_ProductLotNo.EditValue = null;
            tx_Previousprocessname.EditValue = null;
            tx_Nowprocessname.EditValue = null;
            tx_Nextprocessname.EditValue = null;
            tx_ItemMoveNo.Focus();
            bInit_Left = false;
        }

        private void Tx_ItemMoveNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var itemMoveNo = tx_ItemMoveNo.EditValue.GetNullToEmpty().ToUpper();

                string sMessage = string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo"));
                if (itemMoveNo.IsNullOrEmpty())
                {
                    Init_ItemMoveNo(false);
                    return;
                }

                DataSet ds = DbRequestHandler.GetDataQury("EXEC USP_GET_ITEM_MOVE_NO_SCAN_START '" + TEMP_XFPOP1000_Obj.WorkNo + "', '" + itemMoveNo + "'," + TEMP_XFPOP1000_Obj.ProcessSeq + "");

                if (ds == null || ds.Tables[1].Rows.Count < 1)
                {
                    Init_ItemMoveNo(false);
                    return;
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "F")
                {
                    Init_ItemMoveNo(false);
                    return;
                }
                else if (ds.Tables[1].Rows[0][0].ToString() != TEMP_XFPOP1000_Obj.WorkNo)
                {
                    Init_ItemMoveNo(true);
                    return;
                }

                ItemMoveNo = itemMoveNo;
                tx_Workno.EditValue = ds.Tables[1].Rows[0]["WorkNo"].GetNullToEmpty();
                tx_ProductLotNo.EditValue = ds.Tables[1].Rows[0]["ProductLotNo"].GetNullToEmpty();
                tx_Previousprocessname.EditValue = ds.Tables[1].Rows[0]["PreviousProcessName"].GetNullToEmpty();
                tx_Nowprocessname.EditValue = ds.Tables[1].Rows[0]["NowProcessName"].GetNullToEmpty();
                tx_Nextprocessname.EditValue = ds.Tables[1].Rows[0]["NextProcessName"].GetNullToEmpty();
                bInit_Left = true;
                DataLoad();
            }
        }

        private void Tx_ItemMoveNo_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad) return;
            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                tx_ItemMoveNo.EditValue = keyPad.returnval;
                Tx_ItemMoveNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            if (ItemMoveNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                return;
            }

            List<TEMP_XFPOP1000_WORKSTART_INFO> tempArr = ModelBindingSource.List as List<TEMP_XFPOP1000_WORKSTART_INFO>;

            // 20210611 오세완 차장 bom이 출력되면 원소재를 전부 매칭할 때까지 진행을 하지 말것
            if (tempArr != null)
                if (tempArr.Count > 0)
                {
                    int iMG_flag_cnt = 0;
                    int iMG_matching_cnt = 0;
                    foreach (TEMP_XFPOP1000_WORKSTART_INFO each in tempArr)
                    {
                        iMG_flag_cnt++;

                        if (each.OUT_LOT_NO.GetNullToEmpty() != "")
                            iMG_matching_cnt++;
                    }

                    if (iMG_flag_cnt > iMG_matching_cnt)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_126), LabelConvert.GetLabelText("OutLotNo")));
                        return;
                    }
                }

            if (TEMP_XFPOP1000_Obj.ProcessCode == MasterCodeSTR.Process_Press)
            {
                // 20210619 오세완 차장 품목기준정보에 매칭되어 있으면 통과
                if (bNeedMoldCode)
                {
                    if (tx_Moldmcode.EditValue.GetNullToEmpty() == "")
                    {
                        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_152));
                        return;
                    }
                }
            }

            DialogResult = DialogResult.OK;
            string sProductlotno = tx_ProductLotNo.EditValue.GetNullToEmpty();
            string sMachine = lup_Machine.EditValue.GetNullToEmpty();
            string sMoldmcode = tx_Moldmcode.EditValue.GetNullToEmpty();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, ItemMoveNo);
            param.SetValue(PopupParameter.Value_2, sProductlotno);
            param.SetValue(PopupParameter.Value_3, sMachine);
            param.SetValue(PopupParameter.Value_4, sMoldmcode); // 20210613 오세완 차장 금형공정인 경우만 전달
            param.SetValue(PopupParameter.ReturnObject, tempArr); // 20210613 오세완 차장 bom이 존재하면서 수동관리여부가 체크된 항목이 있는 경우에 값이 있음
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            ActClose();
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            lup_MachineGroup.SetDefaultPOP(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_MachineGroup.SetFontSize(new Font("맑은 고딕", 12f));

            lup_Machine.SetDefaultPOP(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Machine.SetFontSize(new Font("맑은 고딕", 12f));

            lup_MachineGroup.EditValue = TEMP_XFPOP1000_Obj.MachineGroupCode.GetNullToNull();
            if (!lup_MachineGroup.EditValue.IsNullOrEmpty())
            {
                lup_MachineGroup.ReadOnly = true;
                lup_MachineGroup.Properties.Buttons[1].Enabled = false;
            }

            lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode.GetNullToNull();
            tx_ItemMoveNo.ImeMode = ImeMode.Disable;
            btn_Start.Text = LabelConvert.GetLabelText("Start");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");

            lup_Machine.Enabled = false;
            lup_MachineGroup.Enabled = false;
            tx_Moldmcode.Enabled = false;
            tx_OutLotNo.Enabled = false;
            gridEx1.Enabled = false;
        }

        private void Tx_OutLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_OutLotNo();
            }
        }

        //XPFRAW_MATERIAL_IN 과 동일하게 처리
        private void Search_OutLotNo()
        {
            try
            {
                string sOutlotno = tx_OutLotNo.EditValue.GetNullToEmpty();
                if (sOutlotno == "")
                    return;

                string sMessage = string.Empty;
                if (workInfoBomList == null)
                {
                    sMessage = string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("BomList"));
                    MessageBoxHandler.Show(sMessage);
                    tx_OutLotNo.EditValue = "";
                    return;
                }

                var srcItem = ModelService.GetChildList<VI_SRC_USE>(x => x.SrcLotNo == sOutlotno).FirstOrDefault();

                if (srcItem != null)
                {
                    if (srcItemList.Where(x => x.SrcItemCode == srcItem.SrcItemCode).ToList().Count > 0)
                    {
                        sMessage = "이미 추가된 재료품번";
                        MessageBoxHandler.Show(sMessage);
                        tx_OutLotNo.EditValue = "";
                        return;
                    }

                    if (srcItem.OutQty.GetNullToZero() - srcItem.UseQty.GetNullToZero() > 0)
                    {
                        //사용가능
                        srcItemList.Add(srcItem);

                        List<TEMP_XFPOP1000_WORKSTART_INFO> list = new List<TEMP_XFPOP1000_WORKSTART_INFO>();
                        //workInfoBomList

                        foreach (var s in srcItemList)
                        {
                            var aa = workInfoBomList.Where(x => x.ITEM_CODE == s.SrcItemCode).FirstOrDefault();
                            aa.OUT_LOT_NO = s.SrcLotNo;
                            aa.OUT_QTY = s.OutQty.GetNullToZero() - s.UseQty.GetNullToZero();
                            list.Add(aa);
                        }

                        //ModelBindingSource.DataSource = srcItemList;
                        ModelBindingSource.DataSource = list;
                        GridExControl.MainGrid.DataSource = ModelBindingSource;
                        GridExControl.MainGrid.BestFitColumns();
                    }
                    else
                    {
                        //사용불가능
                        sMessage = "해당 lot 재고수량 없음.";
                        MessageBoxHandler.Show(sMessage);
                        tx_OutLotNo.EditValue = "";
                        return;
                    }


                }
                else
                {
                    sMessage = string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("SrcOutLotNo"));
                    MessageBoxHandler.Show(sMessage);
                    tx_OutLotNo.EditValue = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
                tx_OutLotNo.Focus();
            }
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.SetGridFont(GridExControl.MainGrid.MainView, new Font("맑은 고딕", 12f));
            GridExControl.MainGrid.MainView.ColumnPanelRowHeight = 30;
            GridExControl.MainGrid.MainView.RowHeight = 50;
            GridExControl.MainGrid.AddColumn("TYPE", LabelConvert.GetLabelText("TopCategory"));
            GridExControl.MainGrid.AddColumn("ITEM_CODE", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ITEM_NAME", LabelConvert.GetLabelText("ItemName"));
            //GridExControl.MainGrid.AddColumn("ITEM_NAME1", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("PROCESS_CODE", LabelConvert.GetLabelText("ProcessCode"), false);

            GridExControl.MainGrid.AddColumn("USE_QTY", LabelConvert.GetLabelText("UseQty"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("OUT_LOT_NO", LabelConvert.GetLabelText("OutLotNo"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("OUT_QTY", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Center, true);
            GridExControl.MainGrid.BestFitColumns();
        }

        protected override void BaseForm_FormClosing(object sender, FormClosingEventArgs e) { }
    }
}