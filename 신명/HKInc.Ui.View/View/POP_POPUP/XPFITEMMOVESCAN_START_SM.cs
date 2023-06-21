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

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 작업 시작 시 부품표 스캔
    /// </summary>
    public partial class XPFITEMMOVESCAN_START_SM : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");
        IService<TN_STD1300> ModelServiceBom = (IService<TN_STD1300>)ProductionFactory.GetDomainService("TN_STD1300");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        private string ItemMoveNo = null;

        public XPFITEMMOVESCAN_START_SM()
        {
            InitializeComponent();
        }

        public XPFITEMMOVESCAN_START_SM(PopupDataParam parameter, PopupCallback callback) : this()
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

            btn_Start.Click += Btn_Start_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
            
            this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);
            this.Size = this.MinimumSize;
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

            //lup_Machine.ReadOnly = true;
            lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode.GetNullToNull();
            //var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode && p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq).FirstOrDefault();
            //if (TN_MPS1200 != null && TN_MPS1200.MachineFlag == "Y")
            //{
            //    lup_Machine.ReadOnly = false;
            //}

            tx_ItemMoveNo.ImeMode = ImeMode.Disable;

            var processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
            lup_PriviousProcessName.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);
            lup_NowProcessName.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);
            lup_NextProcessName.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);

            lup_PriviousProcessName.SetFontSize(new Font("맑은 고딕", 12f));
            lup_NowProcessName.SetFontSize(new Font("맑은 고딕", 12f));
            lup_NextProcessName.SetFontSize(new Font("맑은 고딕", 12f));
            tx_ItemMoveNo.Font = new Font("맑은 고딕", 12f);
            tx_WorkNo.Font = new Font("맑은 고딕", 12f);
            tx_ProductLotNo.Font = new Font("맑은 고딕", 12f);

            lup_PriviousProcessName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_NowProcessName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_NextProcessName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_ItemMoveNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_WorkNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_ProductLotNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            
            btn_Start.Text = LabelConvert.GetLabelText("Start");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.SetGridFont(GridExControl.MainGrid.MainView, new Font("맑은 고딕", 12f));
            GridExControl.MainGrid.MainView.ColumnPanelRowHeight = 30;
            GridExControl.MainGrid.MainView.RowHeight = 50;
            GridExControl.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("UseQty", LabelConvert.GetLabelText("UseQty"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("OutLotNo", LabelConvert.GetLabelText("OutLotNo"), HorzAlignment.Center, true);
            GridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataLoad()
        {
            //완제품 BOM정보 LOAD - BOM정보 무조건 존재
            var bomInfo = ModelServiceBom.GetList().Where(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode).FirstOrDefault();

            ModelBindingSource.DataSource = ModelServiceBom.GetList(p => p.MgFlag.Equals("Y") && (p.ParentBomCode != null && p.ParentBomCode == bomInfo.BomCode && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode))
                                                                  .OrderBy(p => p.BomCode)
                                                                  .ToList();

            GridExControl.DataSource = ModelBindingSource;
            GridExControl.MainGrid.BestFitColumns();
        }
        private void Lup_MachineGroup_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            //var value = lookup.EditValue.GetNullToNull();
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
                    if (nowMachineStateObj.JobStates != MasterCodeSTR.JobStates_Wait && nowMachineStateObj.JobStates != MasterCodeSTR.JobStates_Pause)
                    {
                        MessageBoxHandler.Show("해당 설비는 대기 또는 일시정지 설비가 아니므로 선택할 수 없습니다. 확인 부탁드립니다.");
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

        private void Tx_ItemMoveNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var itemMoveNo = tx_ItemMoveNo.EditValue.GetNullToEmpty().ToUpper();

                if (itemMoveNo.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                    ItemMoveNo = null;
                    tx_ItemMoveNo.EditValue = null;
                    tx_WorkNo.EditValue = null;
                    tx_ProductLotNo.EditValue = null;
                    lup_PriviousProcessName.EditValue = null;
                    lup_NowProcessName.EditValue = null;
                    lup_NextProcessName.EditValue = null;
                    tx_ItemMoveNo.Focus();
                    return;
                }

                var ds = DbRequestHandler.GetDataQury("EXEC USP_GET_ITEM_MOVE_NO_SCAN_START '" + TEMP_XFPOP1000_Obj.WorkNo + "', '" + itemMoveNo + "'," + TEMP_XFPOP1000_Obj.ProcessSeq + "");

                if (ds == null || ds.Tables[1].Rows.Count < 1)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                    ItemMoveNo = null;
                    tx_ItemMoveNo.EditValue = null;
                    tx_WorkNo.EditValue = null;
                    tx_ProductLotNo.EditValue = null;
                    lup_PriviousProcessName.EditValue = null;
                    lup_NowProcessName.EditValue = null;
                    lup_NextProcessName.EditValue = null;
                    tx_ItemMoveNo.Focus();
                    return;
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "F")
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                    ItemMoveNo = null;
                    tx_ItemMoveNo.EditValue = null;
                    tx_WorkNo.EditValue = null;
                    tx_ProductLotNo.EditValue = null;
                    lup_PriviousProcessName.EditValue = null;
                    lup_NowProcessName.EditValue = null;
                    lup_NextProcessName.EditValue = null;
                    tx_ItemMoveNo.Focus();
                    return;
                }
                else if (ds.Tables[1].Rows[0][0].ToString() != TEMP_XFPOP1000_Obj.WorkNo)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_64));
                    ItemMoveNo = null;
                    tx_ItemMoveNo.EditValue = null;
                    tx_WorkNo.EditValue = null;
                    tx_ProductLotNo.EditValue = null;
                    lup_PriviousProcessName.EditValue = null;
                    lup_NowProcessName.EditValue = null;
                    lup_NextProcessName.EditValue = null;
                    tx_ItemMoveNo.Focus();
                    return;
                }

                ItemMoveNo = itemMoveNo;
                tx_WorkNo.EditValue = ds.Tables[1].Rows[0][0].GetNullToNull();
                tx_ProductLotNo.EditValue = ds.Tables[1].Rows[0][1].GetNullToNull();
                lup_PriviousProcessName.EditValue = ds.Tables[1].Rows[0][2].GetNullToNull();
                lup_NowProcessName.EditValue = ds.Tables[1].Rows[0][3].GetNullToNull();
                lup_NextProcessName.EditValue = ds.Tables[1].Rows[0][4].GetNullToNull();
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

        //원자재LotNo 스캔 시
        private void Tx_OutLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string outLotNo = tx_OutLotNo.EditValue.GetNullToEmpty();

                    if (outLotNo == "") return;

                    var outLotNoInfo = ModelService.GetChildList<VI_POP_WORK_START_BOM_CHECK>(p => p.OutLotNo == outLotNo).FirstOrDefault();
                    string lotNoItemCode = !outLotNoInfo.IsNullOrEmpty() ? outLotNoInfo.ItemCode : null;

                    if (lotNoItemCode.IsNullOrEmpty())
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_127), LabelConvert.GetLabelText("OutLotNo")));
                        tx_OutLotNo.EditValue = null;
                        return;
                    }
                    else
                    {
                        var list = ModelBindingSource.List as List<TN_STD1300>;

                        int cnt = 0;
                        foreach (var v in list)
                        {
                            if (v.ItemCode == lotNoItemCode)
                            {
                                v.OutLotNo = outLotNo;
                                tx_OutLotNo.EditValue = null;
                                cnt++;
                            }

                        }
                        GridExControl.BestFitColumns();

                        if (cnt == 0)
                        {
                            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_127), LabelConvert.GetLabelText("OutLotNo")));
                            tx_OutLotNo.EditValue = null;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBoxHandler.ErrorShow(ex);
                    tx_OutLotNo.Focus();
                }
            }

        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            var productLotNo = tx_ProductLotNo.EditValue.GetNullToNull();
            var machineCode = lup_Machine.EditValue.GetNullToEmpty();

            if (ItemMoveNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                return;
            }

            var list = ModelBindingSource.List as List<TN_STD1300>;

            //if (list.Count <= 0)
            //{
            //    MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("BomInfo"));
            //    return;
            //}

            //foreach (var v in list)
            //{
            //    if (v.OutLotNo.IsNullOrEmpty())
            //    {
            //        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_126), LabelConvert.GetLabelText("OutLotNo")));
            //        return;
            //    }
            //}

            if (list.Count >= 1)
            {
                if (list.Count <= 0)
                {
                    MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("BomInfo"));
                    return;
                }

                foreach (var v in list)
                {
                    if (v.OutLotNo.IsNullOrEmpty())
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_126), LabelConvert.GetLabelText("OutLotNo")));
                        return;
                    }
                }
            }

            DialogResult = DialogResult.OK;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, ItemMoveNo);
            param.SetValue(PopupParameter.Value_2, productLotNo);
            param.SetValue(PopupParameter.Value_3, machineCode);
            param.SetValue(PopupParameter.ReturnObject, list);
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            ActClose();
        }
    }
}
