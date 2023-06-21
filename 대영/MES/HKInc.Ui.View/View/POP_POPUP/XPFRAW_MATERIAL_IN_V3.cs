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
    /// 20210627 오세완 차장
    /// XPFRAW_MATERIAL_IN_V2와 다른점은 XPFITEMMOVESCAN_START_DAEYOUNG과 같이 상단 출력부분을 좌축으로 이동처리
    /// </summary>
    public partial class XPFRAW_MATERIAL_IN_V3 : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        /// <summary>
        /// 20210618 오세완 차장 
        /// 금형코드가 품목기준정보에 매칭되어 있으면 false, 매칭되어 있지 않으면 true
        /// </summary>
        private bool bNeedMoldCode = false;
        #endregion

        public XPFRAW_MATERIAL_IN_V3()
        {
            InitializeComponent();
        }

        public XPFRAW_MATERIAL_IN_V3(PopupDataParam parameter, PopupCallback callback) : this()
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
                        }

                        bExistsMold = true;
                    }

                if(!bExistsMold)
                {
                    // 20210625 오세완 차장 올바르지 않는 금형은 데이터를 날리는 것으로
                    MessageBoxHandler.Show("존재하지 않는 금형입니다.");
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
            GridExControl.MainGrid.AddColumn("ITEM_NAME1", LabelConvert.GetLabelText("ItemName"));
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

                InitGrid(); // 20210625 오세완 차장 설비가 값이 들어온 경우 여기를 먼저 타버리기 때문에 컬럼 명이 제대로 안나와서 처리
                DataLoad();


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
                        string sItemname1 = ds.Tables[0].Rows[0]["ITEM_NAME1"].GetNullToEmpty();

                        tx_Top_Category.EditValue = sCategory_name;
                        tx_Top_Itemcode.EditValue = sItemcode;
                        tx_Top_Itemname.EditValue = sItemname;
                        tx_Top_Itemname1.EditValue = sItemname1;

                        bool bClosed = false;
                        tx_Top_Category.Enabled = bClosed;
                        tx_Top_Itemcode.Enabled = bClosed;
                        tx_Top_Itemname.Enabled = bClosed;
                        tx_Top_Itemname1.Enabled = bClosed;
                    }

                    // 20210608 오세완 차장 하단 BOM정보 출력
                    if(ds.Tables.Count > 1)
                    {
                        if(ds.Tables[1].Rows.Count > 0)
                        {
                            // 20210611 오세완 차장 이렇게 하면 출력은 되는데 다시 재참조가 힘들다. 
                            //ModelBindingSource.DataSource = ds.Tables[1];
                            
                            List<TEMP_XFPOP1000_WORKSTART_INFO> tempArr = new List<TEMP_XFPOP1000_WORKSTART_INFO>();
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                TEMP_XFPOP1000_WORKSTART_INFO newTemp = new TEMP_XFPOP1000_WORKSTART_INFO()
                                {
                                    TYPE = ds.Tables[1].Rows[i]["TYPE"].GetNullToEmpty(),
                                    ITEM_CODE = ds.Tables[1].Rows[i]["ITEM_CODE"].GetNullToEmpty(),
                                    ITEM_NAME = ds.Tables[1].Rows[i]["ITEM_NAME"].GetNullToEmpty(),
                                    ITEM_NAME1 = ds.Tables[1].Rows[i]["ITEM_NAME1"].GetNullToEmpty(),
                                    PROCESS_CODE = ds.Tables[1].Rows[i]["PROCESS_CODE"].GetNullToEmpty(),
                                    USE_QTY = ds.Tables[1].Rows[i]["USE_QTY"].GetDecimalNullToZero(),
                                    MG_FLAG = ds.Tables[1].Rows[i]["MG_FLAG"].GetNullToEmpty()
                                };
                                tempArr.Add(newTemp);
                            }

                            ModelBindingSource.DataSource = tempArr;
                            GridExControl.DataSource = ModelBindingSource;
                            GridExControl.MainGrid.BestFitColumns();
                        }
                    }
                }
            }
            else
            {
                // 20210615 오세완 차장 bom이 잘못 구성된 경우 FN_GET_XFPOP1000_WORKSTART_BOMINFO_BAN, FN_GET_XFPOP1000_WORKSTART_BOMINFO_wan에서 오류가 날 수 있다.
                // 그러면 아예 값이 안나오기 때문에 해당 상황을 회피

                MessageBoxHandler.Show("BOM정보가 맞지 않습니다. 확인하시기 바랍니다.");
                tx_Top_Category.Enabled = false;
                tx_Top_Itemcode.Enabled = false;
                tx_Top_Itemname.Enabled = false;
                tx_Top_Itemname1.Enabled = false;
                lup_Machine.Enabled = false;
                lup_MachineGroup.Enabled = false;
                tx_Moldmcode.Enabled = false;
                tx_OutLotNo.Enabled = false;
                btn_Start.Enabled = false;
            }

            // 20210609 오세완 차장 작업지시에 설비 지정한 경우는 Lup_Machine_EditValueChanged로 처리 되는 듯?
            //if(TEMP_XFPOP1000_Obj.MachineFlag.GetNullToEmpty() == "Y")
            //    Check_MachineDailyCheck(TEMP_XFPOP1000_Obj.MachineCode);
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

        /// <summary>
        /// 20210611 오세완 차장 원자재 정보 조회
        /// </summary>
        private void Search_OutLotNo()
        {
            try
            {
                string sOutlotno = tx_OutLotNo.EditValue.GetNullToEmpty();
                if (sOutlotno == "")
                    return;

                string sMessage = "";
                List<TEMP_XFPOP1000_WORKSTART_INFO> tempArr = ModelBindingSource.List as List<TEMP_XFPOP1000_WORKSTART_INFO>;
                if (tempArr == null)
                {
                    // 20210625 오세완 차장 아예 인식할 필요가 없어도 메시지 출력하고 날려버리자. 
                    sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_150);
                    MessageBoxHandler.Show(sMessage);
                    tx_OutLotNo.EditValue = "";
                    return;
                }

                bool bSearch = false;
                int iMG_flag_cnt = tempArr.Count(p => p.MG_FLAG == "Y");
                if (iMG_flag_cnt == 0)
                {
                    // 20210625 오세완 차장 반제품은 수동여부가 체크되어 있지 않아도 검사해야 하기 때문에 처리
                    int iBanProduct = tempArr.Count(p => p.TYPE.IndexOf("반제품") >= -1);
                    if (iBanProduct == 0)
                    {
                        sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_150);
                        MessageBoxHandler.Show(sMessage);
                        //MessageBoxHandler.Show("출고 LOTNO를 인식하지 않아도 됩니다.");
                        return;
                    }
                    else
                        bSearch = true;

                }
                else
                    bSearch = true;

                if(bSearch)
                {
                    bool bNoSearch = false;
                    string sSql = "exec USP_GET_POP_SRC_IN_V2 '" + sOutlotno + "'";
                    DataTable dt = DbRequestHandler.GetDataTableSelect(sSql);
                    if (dt == null)
                    {
                        bNoSearch = true;
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string sInput_Itemcode = dt.Rows[0]["ItemCode"].ToString();
                            bool bBreak = false;
                            foreach (TEMP_XFPOP1000_WORKSTART_INFO each in tempArr)
                            {
                                // 20210608 오세완 차장 품목코드가 동일한 것
                                if (each.ITEM_CODE == sInput_Itemcode)
                                {
                                    // 20210608 오세완 차장 투입할 수량이 남은 것
                                    decimal dOutQty = dt.Rows[0]["StockQty"].GetDecimalNullToZero();
                                    if (dOutQty <= 0)
                                    {
                                        sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_151);
                                        MessageBoxHandler.Show(sMessage);
                                        //MessageBoxHandler.Show("출고량이 부적한 자재 / 반제품입니다.");
                                        bBreak = true;
                                    }
                                    else
                                    {
                                        // 20210621 오세완 차장 자동출고된 원자재는 생산 재투입을 막는 로직 추가   
                                        string sAutoOutSrc = dt.Rows[0]["AutoOutSrc"].GetNullToEmpty();
                                        if (sAutoOutSrc == "Y")
                                        {
                                            sMessage = "자동출고된 원자재는 생산투입을 할 수 없습니다.";
                                            MessageBoxHandler.Show(sMessage);
                                            bBreak = true;
                                        }
                                        else
                                        {
                                            // 20210608 오세완 차장 반제품은 전체 lot를 인식할 거라서 수량체크는 하지 않아도 되는 것으로 처리
                                            //decimal dCal_qty = TEMP_XFPOP1000_Obj.WorkQty * each.USE_QTY;
                                            each.OUT_LOT_NO = sOutlotno;
                                            each.OUT_QTY = dOutQty;
                                            bBreak = true;
                                        }
                                    }
                                }

                                if (bBreak)
                                {
                                    // 20210611 오세완 차장 올바르게 매칭이 되면 그냥 사라지게 처리하자
                                    tx_OutLotNo.EditValue = "";
                                    GridExControl.MainGrid.BestFitColumns();
                                    break;
                                }
                            }

                            if (!bBreak)
                            {
                                // 20210619 오세완 차장 김이사님 지시로 틀린 경우도 메세지를 출력할 수 있게 추가
                                sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_155);
                                MessageBoxHandler.Show(sMessage);
                                tx_OutLotNo.EditValue = "";
                            }
                        }
                        else
                            bNoSearch = true;
                    }

                    if(bNoSearch)
                    {
                        // 20210614 오세완 차장 아예 조회가 안되는 경우도 추가
                        sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_155);
                        MessageBoxHandler.Show(sMessage);
                        tx_OutLotNo.EditValue = "";
                    }
                        
                }
            }
            catch (Exception ex)
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

            // 20210611 오세완 차장 bom이 출력되면 원소재를 전부 매칭할 때까지 진행을 하지 말것
            if(tempArr != null)
                if(tempArr.Count > 0)
                {
                    int iMG_flag_cnt = 0;
                    int iMG_matching_cnt = 0;
                    foreach(TEMP_XFPOP1000_WORKSTART_INFO each in tempArr)
                    {
                        iMG_flag_cnt++;

                        if (each.OUT_LOT_NO.GetNullToEmpty() != "")
                            iMG_matching_cnt++;
                    }

                    if(iMG_flag_cnt > iMG_matching_cnt)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_126), LabelConvert.GetLabelText("OutLotNo")));
                        return;
                    }
                }

            if(TEMP_XFPOP1000_Obj.ProcessCode == MasterCodeSTR.Process_Press)
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
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, machineCode);
            if(tempArr == null)
                param.SetValue(PopupParameter.Value_2, "NO_ONE");
            else
                param.SetValue(PopupParameter.Value_2, "HAVE");

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