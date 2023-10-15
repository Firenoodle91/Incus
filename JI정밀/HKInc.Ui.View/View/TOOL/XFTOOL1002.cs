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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using DevExpress.Utils;

namespace HKInc.Ui.View.View.TOOL
{
    /// <summary>
    /// 툴 교체이력
    /// </summary>
    public partial class XFTOOL1002 : Service.Base.ListFormTemplate
    {
        IService<VI_TOOL1002_LIST> ModelService = (IService<VI_TOOL1002_LIST>)ProductionFactory.GetDomainService("VI_TOOL1002_LIST");

        public XFTOOL1002()
        {
            InitializeComponent();
            GridExControl = gridEx1;
        }

        protected override void InitCombo()
        {
            lup_Tool.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_TOOL)).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            GridExControl.MainGrid.AddColumn("TN_MPS1201.TN_MPS1200.WorkDate", LabelConvert.GetLabelText("WorkDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("TN_STD1100_TOOL.ItemCode", LabelConvert.GetLabelText("ToolCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100_TOOL." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ToolName"));
            GridExControl.MainGrid.AddColumn("ChangeLifeQty", LabelConvert.GetLabelText("ToolLifeQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("TN_MPS1201.TN_STD1100.ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_MPS1201.TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("Process"));
            GridExControl.MainGrid.AddColumn("TN_MPS1201.MachineCode", LabelConvert.GetLabelText("Machine"));
            GridExControl.MainGrid.AddColumn("ChangeQty", LabelConvert.GetLabelText("ToolChangeQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("ToolChangeDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm:ss");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_MPS1201.MachineCode", ModelService.GetChildList<TN_MEA1000>(p => true).ToList(), "MachineMCode", Service.Helper.DataConvert.GetCultureDataFieldName("MachineName"));

            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            var toolCode = lup_Tool.EditValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(toolCode) ? true : p.TN_MPS1201.TN_STD1100.TN_STD1100_TOOL.ItemCode == toolCode)
                                                               )
                                                               .OrderBy(p => p.CreateTime)
                                                               .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void GridRowDoubleClicked() { }
    }
}