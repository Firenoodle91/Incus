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
using System.Data.SqlClient;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 작업 시작 시 부품표 스캔
    /// </summary>
    public partial class XPFITEMMOVESCAN_START : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        BindingSource bindingSource = new BindingSource();

        private string ItemMoveNo = null;
        private string ProcTeamCode = null;

        public XPFITEMMOVESCAN_START()
        {
            InitializeComponent();
        }

        public XPFITEMMOVESCAN_START(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkStart");

            gridEx_POP1.ViewType = GridViewType.POP_GridView;

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);
            ProcTeamCode = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            tx_ItemMoveNo.KeyDown += Tx_ItemMoveNo_KeyDown;
            tx_ItemMoveNo.Click += Tx_ItemMoveNo_Click;

            btn_Start.Click += Btn_Start_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

            lup_MachineGroup.EditValueChanged += Lup_MachineGroup_EditValueChanged;
            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;
            lup_Machine.Popup += Lup_Machine_Popup;

            //this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);
            this.Size = new Size(this.Size.Width, this.Size.Height-40);
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            tx_ItemMoveNo.ImeMode = ImeMode.Disable;

            lup_MachineGroup.SetDefaultPOP(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lup_MachineGroup.SetFontSize(new Font("맑은 고딕", 15f));

            var VI_MACHINE_DAILY_CHECK_LIST = ModelService.GetChildList<VI_MACHINE_DAILY_CHECK>(p => true).ToList();
            //lup_Machine.SetDefaultPOP(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Machine.SetDefaultPOP(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), VI_MACHINE_DAILY_CHECK_LIST, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            var processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
            lup_PriviousProcessName.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);
            lup_NowProcessName.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);
            lup_NextProcessName.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);

            //lup_Machine.SetFontSize(new Font("맑은 고딕", 12f));
            lup_PriviousProcessName.SetFontSize(new Font("맑은 고딕", 12f));
            lup_NowProcessName.SetFontSize(new Font("맑은 고딕", 12f));
            lup_NextProcessName.SetFontSize(new Font("맑은 고딕", 12f));
            tx_ItemMoveNo.Font = new Font("맑은 고딕", 12f);
            tx_WorkNo.Font = new Font("맑은 고딕", 12f);
            tx_ProductLotNo.Font = new Font("맑은 고딕", 12f);

            //lup_MachineGroup.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_PriviousProcessName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_NowProcessName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_NextProcessName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_ItemMoveNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_WorkNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_ProductLotNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            lup_MachineGroup.EditValue = TEMP_XFPOP1000_Obj.MachineGroupCode.GetNullToNull();
            if (!lup_MachineGroup.EditValue.IsNullOrEmpty())
                lup_MachineGroup.ReadOnly = true;

            //lup_Machine.ReadOnly = true;

            if (!TEMP_XFPOP1000_Obj.MachineCode.IsNullOrEmpty())
            {
                if (VI_MACHINE_DAILY_CHECK_LIST.Any(p => p.MachineMCode == TEMP_XFPOP1000_Obj.MachineCode))
                    lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode;
            }
            //var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode && p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq).FirstOrDefault();
            //if (TN_MPS1200 != null && TN_MPS1200.MachineFlag == "Y")
            //{
            //    lup_Machine.ReadOnly = false;
            //}

            //lup_Machine.ReadOnly = true;
            //lup_Machine.EditValue = TEMP_XFPOP1000_Obj.MachineCode.GetNullToNull();
            //var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode && p.ProcessSeq == TEMP_XFPOP1000_Obj.ProcessSeq).FirstOrDefault();
            //if (TN_MPS1200 != null && TN_MPS1200.MachineFlag == "Y")
            //{
            //    lup_Machine.ReadOnly = false;
            //}

            btn_Start.Text = LabelConvert.GetLabelText("Start");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");

            chk_machineCheck.EditValue = "N";
        }

        protected override void InitGrid()
        {
            gridEx_POP1.SetToolbarVisible(false);
            gridEx_POP1.MainGrid.AddColumn("ItemMoveNo", LabelConvert.GetLabelText("ItemMoveNo"), HorzAlignment.Center, true);

            gridEx_POP1.MainGrid.SetGridFont(gridEx_POP1.MainGrid.MainView, new Font("맑은 고딕", 15f));
            gridEx_POP1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx_POP1.MainGrid.MainView.RowHeight = 50;
            gridEx_POP1.MainGrid.MainView.OptionsView.ShowColumnHeaders = false;
            gridEx_POP1.MainGrid.MainView.OptionsView.ColumnAutoWidth = true;
            gridEx_POP1.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            gridEx_POP1.MainGrid.Clear();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var WorkNo = new SqlParameter("@WorkNo", TEMP_XFPOP1000_Obj.WorkNo);
                var ProcessSeq = new SqlParameter("@ProcessSeq", TEMP_XFPOP1000_Obj.ProcessSeq);
                var result = context.Database.SqlQuery<TEMP_NOT_USE_ITEM_MOVE_NO>("USP_GET_NOT_USE_ITEM_MOVE_NO @WorkNo,@ProcessSeq", WorkNo, ProcessSeq).ToList();
                bindingSource.DataSource = result.OrderBy(p => p.ItemMoveNo).ToList();
            }

            gridEx_POP1.DataSource = bindingSource;
            gridEx_POP1.BestFitColumns();
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
        
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            var productLotNo = tx_ProductLotNo.EditValue.GetNullToNull();
            if (ItemMoveNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                return;
            }

            var machineCode = lup_Machine.EditValue.GetNullToNull();
            if (TEMP_XFPOP1000_Obj.MachineFlag == "Y" && machineCode.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("MachineInfo")));
                return;
            }

            DialogResult = DialogResult.OK;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, ItemMoveNo);
            param.SetValue(PopupParameter.Value_2, productLotNo);
            param.SetValue(PopupParameter.Value_3, machineCode);
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            ActClose();
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
            }
        }

        private void Lup_Machine_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var machineGroupCode = lup_MachineGroup.EditValue.GetNullToNull();

            if (machineGroupCode.IsNullOrEmpty() || chk_machineCheck.EditValue.GetNullToEmpty() == "Y")
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            else
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[MachineGroupCode] = '" + machineGroupCode + "'";
        }
    }
}
