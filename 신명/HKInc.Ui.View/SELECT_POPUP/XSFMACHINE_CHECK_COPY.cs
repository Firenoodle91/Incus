using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 다른설비의 점검항목 Select 팝업
    /// </summary>
    public partial class XSFMACHINE_CHECK_COPY : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MEA1002> ModelService = (IService<TN_MEA1002>)ProductionFactory.GetDomainService("TN_MEA1002");
        private bool IsmultiSelect = true;

        public XSFMACHINE_CHECK_COPY()
        {
            InitializeComponent();
        }

        public XSFMACHINE_CHECK_COPY(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("OtherMachineCheckCopy");

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;
        }

        protected override void InitBindingSource() { }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            lup_Machine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y" && p.DailyCheckFlag == "Y").ToList());
        }
        
        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "RowId", IsmultiSelect);
            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            GridExControl.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"), false);
            GridExControl.MainGrid.AddColumn("CheckSeq", LabelConvert.GetLabelText("CheckSeq"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            GridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"));
            GridExControl.MainGrid.AddColumn("CheckPosition", LabelConvert.GetLabelText("CheckPosition"));
            GridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("CheckList"));
            GridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("CheckWay"));
            GridExControl.MainGrid.AddColumn("Temp", LabelConvert.GetLabelText("EyeCheckFlag"));
            GridExControl.MainGrid.AddColumn("CheckCycle", LabelConvert.GetLabelText("CheckCycle"));
            GridExControl.MainGrid.AddColumn("CheckStandardDate", LabelConvert.GetLabelText("CheckStandardDate"));
            GridExControl.MainGrid.AddColumn("ManagementStandard", LabelConvert.GetLabelText("ManagementStandard"));
            GridExControl.MainGrid.AddColumn("DisplayOrder", LabelConvert.GetLabelText("DisplayOrder"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineStandardCheckDivision), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPosition", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckPosition), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckList), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckWay), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckCycle), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckStandardDate", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckStandardDate), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.MainView.Columns["ManagementStandard"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "ManagementStandard");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("DisplayOrder");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");

            GridExControl.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            //DataLoad();
        }

        protected override void DataLoad()
        {
            //GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var value = lup_Machine.EditValue.GetNullToEmpty();
            if (value.IsNullOrEmpty())
            {
                GridExControl.MainGrid.Clear();
            }
            else
            {
                ModelBindingSource.DataSource = ModelService.GetList(p => p.MachineCode == value)
                                                                   .OrderBy(p => p.DisplayOrder)
                                                                   .ToList();
                GridExControl.DataSource = ModelBindingSource;
                GridExControl.BestFitColumns();
            }
        }

        private void Lup_Machine_EditValueChanged(object sender, EventArgs e)
        {
            var value = lup_Machine.EditValue.GetNullToEmpty();
            if (value.IsNullOrEmpty())
            {
                GridExControl.MainGrid.Clear();
            }
            else
            {
                ModelBindingSource.DataSource = ModelService.GetList(p => p.MachineCode == value)
                                                                   .OrderBy(p => p.DisplayOrder)
                                                                   .ToList();
                GridExControl.DataSource = ModelBindingSource;
                GridExControl.BestFitColumns();
            }
        }

        protected override void Confirm()
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (IsmultiSelect)
            {
                var returnList = new List<TN_MEA1002>();
                var dataList = ModelBindingSource.List as List<TN_MEA1002>;
                var checkList = dataList.Where(p => p._Check == "Y").ToList();
                foreach (var v in checkList)
                {
                    returnList.Add(ModelService.Detached(v));
                }
                param.SetValue(PopupParameter.ReturnObject, returnList);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_MEA1002)ModelBindingSource.Current));
            }
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var obj = (TN_MEA1002)ModelBindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<TN_MEA1002>();
                    if (obj != null)
                        returnList.Add(ModelService.Detached(obj));
                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(obj));
                }

                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }

        }
    }
}