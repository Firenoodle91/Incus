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

namespace HKInc.Ui.View.SELECT_Popup
{
    /// <summary>
    /// 다른설비의 점검항목 Select 팝업
    /// </summary>
    public partial class XSFMACHINE_CHECK_COPY : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");

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
            lup_Machine.SetDefault(true, "MachineCode", "MachineName", ModelService.GetList(p => true));
        }
        
        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "RowId", IsmultiSelect);
            GridExControl.MainGrid.AddColumn("_Check", "선택");

            GridExControl.MainGrid.AddColumn("MachineCode", false);
            GridExControl.MainGrid.AddColumn("CheckSeq", false);
            GridExControl.MainGrid.AddColumn("Division", "구분");
            GridExControl.MainGrid.AddColumn("CheckPosition", "점검위치");
            GridExControl.MainGrid.AddColumn("CheckList", "점검항목");

            GridExControl.MainGrid.AddColumn("CheckWay", "점검방법");
            GridExControl.MainGrid.AddColumn("Temp", "육안검사여부");
            GridExControl.MainGrid.AddColumn("CheckCycle", "점검주기");
            GridExControl.MainGrid.AddColumn("CheckStandardDate", "점검기준일");
            GridExControl.MainGrid.AddColumn("ManagementStandard", "관리기준");

            GridExControl.MainGrid.AddColumn("DisplayOrder", "출력순서");
            GridExControl.MainGrid.AddColumn("RowId", false);

            GridExControl.MainGrid.AddColumn("Memo", "메모");
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineStandardCheckDivision), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPosition", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckPosition), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckList), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckWay), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckCycle), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckStandardDate", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckStandardDate), "Mcode", "Codename");
            GridExControl.MainGrid.MainView.Columns["ManagementStandard"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("DisplayOrder");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

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
                string sMessage = "설비명이 없습니다. ";
                MessageBoxHandler.Show(sMessage);   
                GridExControl.MainGrid.Clear();
            }
            else
            {
                // 2021-05-21 김진우 주임 변경     MEA1000형식으로 조회되어서 변경
                ModelBindingSource.DataSource = ModelService.GetChildList<TN_MEA1002>(p => p.MachineCode == value).ToList();

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
                // 2021-05-21 김진우 주임 변경     MEA1000형식으로 조회되어서 변경
                ModelBindingSource.DataSource = ModelService.GetChildList<TN_MEA1002>(p => p.MachineCode == value)
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
                var returnList = new List<TN_MEA1002>();                                            // 2021-05-21 김진우 주임 변경     MEA1000형식으로 조회되어서 변경
                var dataList = ModelBindingSource.List as List<TN_MEA1002>;                         // 2021-05-21 김진우 주임 변경     MEA1000형식으로 조회되어서 변경
                var checkList = dataList.Where(p => p._Check == "Y").ToList();
                foreach (var v in checkList)
                {
                    returnList.Add(ModelService.DetachChild<TN_MEA1002>(v));                        // 2021-05-21 김진우 주임 변경     MEA1000형식으로 조회되어서 변경
                }
                param.SetValue(PopupParameter.ReturnObject, returnList);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.DetachChild<TN_MEA1002>((TN_MEA1002)ModelBindingSource.Current));  // 2021-05-21 김진우 주임 변경     MEA1000형식으로 조회되어서 변경
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
                var obj = (TN_MEA1002)ModelBindingSource.Current;                                   // 2021-05-21 김진우 주임 변경     MEA1000형식으로 조회되어서 변경
                if (IsmultiSelect)
                {
                    var returnList = new List<TN_MEA1002>();                                        // 2021-05-21 김진우 주임 변경     MEA1000형식으로 조회되어서 변경
                    if (obj != null)
                        returnList.Add(ModelService.DetachChild<TN_MEA1002>(obj));                  // 2021-05-21 김진우 주임 변경     MEA1000형식으로 조회되어서 변경
                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelService.DetachChild<TN_MEA1002>(obj));         // 2021-05-21 김진우 주임 변경     MEA1000형식으로 조회되어서 변경
                }

                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }

        }
    }
}