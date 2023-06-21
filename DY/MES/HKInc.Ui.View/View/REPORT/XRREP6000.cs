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
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 설비현황
    /// </summary>
    public partial class XRREP6000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<VI_XRREP6000_LIST> ModelService = (IService<VI_XRREP6000_LIST>)ProductionFactory.GetDomainService("VI_XRREP6000_LIST");

        public XRREP6000()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            lup_MachineGroup.EditValueChanged += Lup_MachineGroup_EditValueChanged;
            lup_MachineCode.Popup += Lup_Machine_Popup;
        }

        protected override void InitCombo()
        {
            lup_MachineGroup.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_MachineCode.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("MachineGroupCode", LabelConvert.GetLabelText("MachineGroup"));
            GridExControl.MainGrid.AddColumn("MachineMCode", LabelConvert.GetLabelText("MachineMCode"), false);
            GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
            GridExControl.MainGrid.AddColumn("MachineName", LabelConvert.GetLabelText("MachineName"));
            GridExControl.MainGrid.AddColumn("WorkTime", LabelConvert.GetLabelText("WorkTime"), HorzAlignment.Far, FormatType.Numeric, "N0");
            GridExControl.MainGrid.AddColumn("StopM", LabelConvert.GetLabelText("StopM"), HorzAlignment.Far, FormatType.Numeric, "N0");
            GridExControl.MainGrid.AddColumn("UpTime", LabelConvert.GetLabelText("UpTime"), HorzAlignment.Far, FormatType.Numeric, "N0");
            GridExControl.MainGrid.AddColumn("JobStates", LabelConvert.GetLabelText("JobStates"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"));
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("Customer"));
            GridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("Process"));
            GridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            GridExControl.MainGrid.AddColumn("ResultStartDate", LabelConvert.GetLabelText("ResultStartDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm:ss");
            GridExControl.MainGrid.AddColumn("OkSumQty", LabelConvert.GetLabelText("SumOkQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("BadSumQty", LabelConvert.GetLabelText("SumBadQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStates", DbRequestHandler.GetCommTopCode(MasterCodeSTR.JobStates), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemName", ModelService.GetChildList<TN_STD1100>(p => 1 == 1).ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
            
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MachineCode");

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            var machineGroupCode = lup_MachineGroup.EditValue.GetNullToEmpty();
            var machineCode = lup_MachineCode.EditValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(machineGroupCode) ? true : p.MachineGroupCode == machineGroupCode)     
                                                                  && (string.IsNullOrEmpty(machineCode) ? true : p.MachineCode == machineCode)
                                                                )
                                                               .OrderBy(p => p.MachineName)
                                                               .ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        private void Lup_MachineGroup_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            lup_MachineCode.EditValue = null;
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