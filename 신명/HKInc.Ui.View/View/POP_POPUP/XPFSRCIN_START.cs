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

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 작업 시작 (원소재투입)
    /// </summary>
    public partial class XPFSRCIN_START : Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_PUR_STOCK_IN_LOT_NO> ModelService = (IService<VI_PUR_STOCK_IN_LOT_NO>)ProductionFactory.GetDomainService("VI_PUR_STOCK_IN_LOT_NO");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        private string SrcItemCode = null;
        private string SrcOutLotNo = null;
        
        public XPFSRCIN_START()
        {
            InitializeComponent();
           
        }
        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
            gridEx1.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            gridEx1.MainGrid.AddColumn("SrcItemCode", LabelConvert.GetLabelText("SrcItemCode"), false);
            gridEx1.MainGrid.AddColumn("SrcItemName", LabelConvert.GetLabelText("ItemName"));
            gridEx1.MainGrid.AddColumn("SrcLotNo", LabelConvert.GetLabelText("SrcLotNo"));
            gridEx1.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("OutDate", LabelConvert.GetLabelText("OutDate"));
            

        }
        protected override void InitRepository()
        {
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", "MachineCode");
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode2", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"));
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStates", DbRequestHandler.GetCommTopCode(MasterCodeSTR.JobStates), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.BestFitColumns();

        }
        public XPFSRCIN_START(PopupDataParam parameter, PopupCallback callback) : this()
        {
          
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkStart");

            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)parameter.GetValue(PopupParameter.KeyValue);

       
            btn_Start.Click += Btn_Start_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

            lup_MachineGroup.EditValueChanged += Lup_MachineGroup_EditValueChanged;
            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;
            lup_Machine.Popup += Lup_Machine_Popup;

          //  this.Size = new Size(this.Size.Width, this.Size.Height - 80);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);
            //this.Size = this.MinimumSize;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            lup_MachineGroup.SetDefaultPOP(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lup_MachineGroup.SetFontSize(new Font("맑은 고딕", 12f));

            lup_Machine.SetDefaultPOP(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lup_Machine.SetFontSize(new Font("맑은 고딕", 12f));

           

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

            btn_Start.Text = LabelConvert.GetLabelText("Start");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
        }

     
        
    
        
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            SrcOutLotNo = gridEx1.MainGrid.MainView.GetFocusedRowCellValue(gridEx1.MainGrid.MainView.Columns["SrcLotNo"]).ToString();
            SrcItemCode = gridEx1.MainGrid.MainView.GetFocusedRowCellValue(gridEx1.MainGrid.MainView.Columns["SrcItemCode"]).ToString();
            if (SrcOutLotNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("SrcInfo")));
                return;
            }

            var machineCode = lup_Machine.EditValue.GetNullToNull();
            if (TEMP_XFPOP1000_Obj.MachineFlag == "Y" && machineCode.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("MachineInfo")));
                return;
            }

        
            DialogResult = System.Windows.Forms.DialogResult.OK;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, SrcItemCode);
            param.SetValue(PopupParameter.Value_2, SrcOutLotNo);
            param.SetValue(PopupParameter.Value_3, machineCode);
            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
        protected override void DataLoad()
        {
            var mc = lup_Machine.EditValue.GetNullToEmpty();

            gridEx1.DataSource = ModelService.GetChildList<VI_MCSRCLOT_STOCK>(p => p.MachineCode == mc).ToList();

            gridEx1.MainGrid.BestFitColumns();

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

    }
}
