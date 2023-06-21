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
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 20210607 오세완 차장
    /// 공정순번 1번인 작업지시를 클릭했을 때 출력되는 작업시작화면 
    /// </summary>
    public partial class XPFRAW_MATERIAL_IN_NOTUSE : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        /// <summary>
        /// 20210618 오세완 차장 
        /// 금형코드가 품목기준정보에 매칭되어 있으면 false, 매칭되어 있지 않으면 true
        /// </summary>
        private bool bNeedMoldCode = false;

        /// <summary>
        /// 20210807 오세완 차장
        /// 관리자와 함께 실행했을 때 금형코드를 엔터키를 치지 않고 입력만 해서 그냥 넘겨버리는 행위를 해서 검증여부를 확실히 판단하기 위함
        /// </summary>
        private bool bMatchingMold = false;

        //bomList 저장 전역변수
        List<TEMP_XFPOP1000_WORKSTART_INFO> workInfoBomList;
        //List<TEMP_XFPOP1000_WORKSTART_INFO> srcItemList;
        List<VI_SRC_USE> srcItemList = new List<VI_SRC_USE>();
        #endregion

        public XPFRAW_MATERIAL_IN_NOTUSE()
        {
            InitializeComponent();
        }

        public XPFRAW_MATERIAL_IN_NOTUSE(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkStart");
            GridExControl = gridEx1;

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);

            tx_OutLotNo.KeyDown += Tx_OutLotNo_KeyDown;
            tx_Moldmcode.KeyDown += Tx_Moldmcode_KeyDown;
            tx_OutLotNo.Click += Tx_OutLotNo_Click;
            tx_Moldmcode.Click += Tx_Moldmcode_Click;
            btn_Start.Click += Btn_Start_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

            lup_MachineGroup.EditValueChanged += Lup_MachineGroup_EditValueChanged;
            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;
            lup_Machine.Popup += Lup_Machine_Popup;

        }

        private void Tx_Moldmcode_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad)
                return;

            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                tx_Moldmcode.EditValue = keyPad.returnval;
                Search_MoldMatching();
            }
        }

        private void Tx_OutLotNo_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad)
                return;

            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                tx_OutLotNo.EditValue = keyPad.returnval;
                Search_OutLotNo();
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
                bool bExistsMold = false;
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
                            bMatchingMold = true;
                        }

                        bExistsMold = true;
                    }

                if(!bExistsMold)
                {
                    // 20210625 오세완 차장 올바르지 않는 금형은 데이터를 날리는 것으로
                    //MessageBoxHandler.Show("존재하지 않는 금형입니다.");
                    string sMessage1 = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_162);
                    MessageBoxHandler.Show(sMessage1); // 20210829 오세완 차장 메시지 출력이 빠져서 추가
                    tx_Moldmcode.EditValue = "";
                }
            }
            catch(Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
                tx_Moldmcode.Focus();
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
            if(e.KeyCode == Keys.Enter)
            {
                Search_MoldMatching();
            }
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            lup_MachineGroup.SetDefaultPOP(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_MachineGroup.SetFontSize(new Font("맑은 고딕", 15f));

            lup_Machine.SetDefaultPOP(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Machine.SetFontSize(new Font("맑은 고딕", 15f));
            
            lup_MachineGroup.EditValue = TEMP_XFPOP1000_Obj.MachineGroupCode.GetNullToNull();
            if (!lup_MachineGroup.EditValue.IsNullOrEmpty())
            {
                lup_MachineGroup.ReadOnly = true;
                lup_MachineGroup.Properties.Buttons[1].Enabled = false;
            }

            lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode.GetNullToNull();

            btn_Start.Text = LabelConvert.GetLabelText("Start");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");

            
            if(TEMP_XFPOP1000_Obj.ProcessCode == MasterCodeSTR.Process_Press)
            {
                // 20210618 오세완 차장 품목기준정보에 금형코드를 매칭하였으면 입력 받지 않기로
                List<TN_STD1100> tempArr = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode &&
                                                                                      p.UseFlag == "Y").ToList();
                if(tempArr != null)
                    if(tempArr.Count > 0)
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
                            if(temp_moldArr != null)
                                if(temp_moldArr.Count > 0)
                                {
                                    TN_MOLD1100 temp_mold_each = temp_moldArr.FirstOrDefault();
                                    if(temp_mold_each.MoldDayInspFlag.GetNullToEmpty() == "Y")
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
                        }

                        // 20210629 오세완 차장 설정한 금형코드 pop로 전달해야 설적입력시 반영됨
                        tx_Moldmcode.EditValue = temp_each.MoldCode;
                    }
            }
            

            if (!bNeedMoldCode)
            {
                // 20210611 오세완 차장 프레스 공정 아니면 금형은 출력 안하는 걸로
                lcMoldMCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
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
            GridExControl.MainGrid.AddColumn("PROCESS_CODE", LabelConvert.GetLabelText("ProcessCode"), false);

            GridExControl.MainGrid.AddColumn("USE_QTY", LabelConvert.GetLabelText("UseQty"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("OUT_LOT_NO", LabelConvert.GetLabelText("OutLotNo"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("OUT_QTY", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Center, true);
            GridExControl.MainGrid.BestFitColumns();
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
            if (lookup == null)
                return;

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

                        // 20210629 오세완 차장 비가동이나 일시정지는 사용 못하게?
                        btn_Start.Enabled = false;
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

                // 20210701 오세완 차장 투입자재를 인식한 후 설비를 바꿀때 값이 초기화되는 현상을 막기 위해 추가
                bool bInit = false;
                List<TEMP_XFPOP1000_WORKSTART_INFO> tempArr = ModelBindingSource.List as List<TEMP_XFPOP1000_WORKSTART_INFO>;
                if(tempArr == null)
                {
                    bInit = true;
                }
                else if(tempArr.Count == 0)
                {
                    bInit = true;
                }
                else
                {
                    foreach(TEMP_XFPOP1000_WORKSTART_INFO each in tempArr)
                    {
                        if (each.OUT_LOT_NO.GetNullToEmpty() == "")
                            bInit = true;
                    }
                }

                if(bInit)
                {
                    InitGrid(); // 20210625 오세완 차장 설비가 값이 들어온 경우 여기를 먼저 타버리기 때문에 컬럼 명이 제대로 안나와서 처리
                    DataLoad();
                }

                
                // 20210629 오세완 차장 비가동이나 일시정지가 아닌 설비는 사용할 수 있게
                btn_Start.Enabled = true;

            }
        }

        private void Lup_Machine_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null)
                return;

            var machineGroupCode = lup_MachineGroup.EditValue.GetNullToNull();

            if (machineGroupCode.IsNullOrEmpty())
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            else
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[MachineGroupCode] = '" + machineGroupCode + "'";
        }

        protected override void DataLoad()
        {
            if (TEMP_XFPOP1000_Obj == null)
                return;

            string sSql = "exec USP_GET_XFPOP1000_WORKSTART_INFO '" + TEMP_XFPOP1000_Obj.WorkNo + "', '" + TEMP_XFPOP1000_Obj.ProcessCode + "'";
            DataSet ds = DbRequestHandler.GetDataQury(sSql);
            if(ds != null)
            {
                if(ds.Tables.Count > 0)
                {
                    // 20210608 오세완 차장 생산품목 상단 출력
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        string sCategory_name = ds.Tables[0].Rows[0]["CATEGORY_NAME"].GetNullToEmpty();
                        string sItemcode = ds.Tables[0].Rows[0]["ITEM_CODE"].GetNullToEmpty();
                        string sItemname = ds.Tables[0].Rows[0]["ITEM_NAME"].GetNullToEmpty();

                        tx_Top_Category.EditValue = sCategory_name;
                        tx_Top_Itemcode.EditValue = sItemcode;
                        tx_Top_Itemname.EditValue = sItemname;

                        bool bClosed = false;
                        tx_Top_Category.Enabled = bClosed;
                        tx_Top_Itemcode.Enabled = bClosed;
                        tx_Top_Itemname.Enabled = bClosed;
                    }

                    if (ds.Tables[1].Rows.Count == 0)
                    {
                        SetConrol(false, MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_163));
                    }
                    else
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
                                PROCESS_CODE = ds.Tables[1].Rows[i]["PROCESS_CODE"].GetNullToEmpty(),
                                USE_QTY = ds.Tables[1].Rows[i]["USE_QTY"].GetDecimalNullToZero()
                            };
                            //tempArr.Add(newTemp);
                            workInfoBomList.Add(newTemp);
                        }

                        

                        //ModelBindingSource.DataSource = tempArr;
                        //GridExControl.DataSource = ModelBindingSource;
                        //GridExControl.MainGrid.BestFitColumns();
                    }


                }
                else
                    SetConrol(false, MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_163));
            }
            else
            {
                // 20210615 오세완 차장 bom이 잘못 구성된 경우 FN_GET_XFPOP1000_WORKSTART_BOMINFO_BAN, FN_GET_XFPOP1000_WORKSTART_BOMINFO_wan에서 오류가 날 수 있다.
                // 그러면 아예 값이 안나오기 때문에 해당 상황을 회피

                //MessageBoxHandler.Show("BOM정보가 맞지 않습니다. 확인하시기 바랍니다.");
                string sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_163); // 20210702 오세완 차장 메시지 교체
                tx_Top_Category.Enabled = false;
                tx_Top_Itemcode.Enabled = false;
                tx_Top_Itemname.Enabled = false;
                lup_Machine.Enabled = false;
                lup_MachineGroup.Enabled = false;
                tx_Moldmcode.Enabled = false;
                tx_OutLotNo.Enabled = false;
                btn_Start.Enabled = false;

                MessageBoxHandler.Show(sMessage);
            }

            // 20210609 오세완 차장 작업지시에 설비 지정한 경우는 Lup_Machine_EditValueChanged로 처리 되는 듯?
            //if(TEMP_XFPOP1000_Obj.MachineFlag.GetNullToEmpty() == "Y")
            //    Check_MachineDailyCheck(TEMP_XFPOP1000_Obj.MachineCode);
        }

        private void SetConrol(bool useYN, string message = null)
        {
            tx_Top_Category.Enabled = useYN;
            tx_Top_Itemcode.Enabled = useYN;
            tx_Top_Itemname.Enabled = useYN;
            lup_Machine.Enabled = useYN;
            lup_MachineGroup.Enabled = useYN;
            tx_Moldmcode.Enabled = useYN;
            tx_OutLotNo.Enabled = useYN;
            btn_Start.Enabled = useYN;

            if (useYN == false)
                MessageBoxHandler.Show(message);
        }

        /// <summary>
        /// 20210609 오세완 차장 
        /// 설비일상점검이 check가 되어 있는 경우 해달라는 메시지만 출력하는 용도
        /// </summary>
        public void Check_MachineDailyCheck(string sMachinemcode)
        {
            List<TN_MEA1000> temp_meaArr = ModelService.GetChildList<TN_MEA1000>(p => p.MachineMCode == sMachinemcode &&
                                                                                      p.UseFlag == "Y").ToList();
            if(temp_meaArr != null)
            {
                if(temp_meaArr.Count > 0)
                {
                    TN_MEA1000 temp_mea = temp_meaArr.FirstOrDefault();
                    if(temp_mea.DailyCheckFlag.GetNullToEmpty() == "Y")
                    {
                        List<TN_MEA1003> temp_checkArr = ModelService.GetChildList<TN_MEA1003>(p => p.MachineCode == sMachinemcode &&
                                                                                                    p.CheckDate == DateTime.Today).ToList();

                        bool bNotCheck_Machinedailycheck = false;
                        if (temp_checkArr == null)
                            bNotCheck_Machinedailycheck = true;
                        else if (temp_checkArr.Count == 0)
                            bNotCheck_Machinedailycheck = true;

                        if(bNotCheck_Machinedailycheck)
                        {
                            MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_116));
                        }
                    }
                }
            }
        }

        protected override void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Search_OutLotNo()
        {
            try
            {
                string sOutlotno = tx_OutLotNo.EditValue.GetNullToEmpty();
                if (sOutlotno == "")
                    return;

                string sMessage = string.Empty;
                //List<TEMP_XFPOP1000_WORKSTART_INFO> bomList = ModelBindingSource.List as List<TEMP_XFPOP1000_WORKSTART_INFO>;
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
            catch(Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
                tx_OutLotNo.Focus();
            }
        }

        //원자재LotNo 스캔 시
        private void Tx_OutLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_OutLotNo();
            }
        }

        //작업시작버튼
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            var machineCode = lup_Machine.EditValue.GetNullToEmpty();
            List<TEMP_XFPOP1000_WORKSTART_INFO> tempArr = ModelBindingSource.List as List<TEMP_XFPOP1000_WORKSTART_INFO>;

            if (tx_OutLotNo.EditValue.GetNullToEmpty() == "")
            {
                MessageBoxHandler.Show("출고 LOT 입력 필요");
                return;
            }

            // 20210611 오세완 차장 bom이 출력되면 원소재를 전부 매칭할 때까지 진행을 하지 말것
            if (tempArr != null)
            {
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
            }
            else
            {
                MessageBoxHandler.Show("BOM 없음");
            }


            if (TEMP_XFPOP1000_Obj.ProcessCode == MasterCodeSTR.Process_Press)
            {
                // 20210619 오세완 차장 품목기준정보에 매칭되어 있으면 통과
                if(bNeedMoldCode)
                {
                    if (tx_Moldmcode.EditValue.GetNullToEmpty() == "")
                    {
                        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_152));
                        //MessageBoxHandler.Show("프레스 공정은 금형을 매칭해야 합니다.");
                        return;
                    }
                    else if(!bMatchingMold)
                    {
                        MessageBoxHandler.Show("올바른 금형인지 엔터키를 통해서 확인해 주세요.");
                        return;
                    }
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, machineCode);
            //if(tempArr == null)
            //    param.SetValue(PopupParameter.Value_2, "NO_ONE");
            //else
            //    param.SetValue(PopupParameter.Value_2, "HAVE");

            param.SetValue(PopupParameter.ReturnObject, tempArr);
            param.SetValue(PopupParameter.Value_3, tx_Moldmcode.EditValue.GetNullToEmpty()); // 20210611 오세완 차장 타발수 매칭때문에 값을 전달
            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        //취소버튼
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            ActClose();
        }




    }
}