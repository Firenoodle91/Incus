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
using HKInc.Service.Helper;

namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 설비관리
    /// </summary>
    public partial class XFMEA1000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");

        public XFMEA1000()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        /// <summary>
        /// 2021-05-21 김진우 주임 추가
        /// </summary>
        protected override void InitCombo()
        {
            lup_MachineCodeName.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetList(p => true));
        }


        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("MachineMCode", LabelConvert.GetLabelText("MachineMCode"));
            GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
            GridExControl.MainGrid.AddColumn("MachineGroupCode", LabelConvert.GetLabelText("MachineGroup"));
            GridExControl.MainGrid.AddColumn("MachineName", LabelConvert.GetLabelText("MachineName"));
            GridExControl.MainGrid.AddColumn("MachineNameENG", LabelConvert.GetLabelText("MachineNameENG"));
            GridExControl.MainGrid.AddColumn("MachineNameCHN", LabelConvert.GetLabelText("MachineNameCHN"));
            GridExControl.MainGrid.AddColumn("ProcTeamCode", LabelConvert.GetLabelText("ProductTeam"), false);      // 2021-05-21 김진우 주임 주석처리
            GridExControl.MainGrid.AddColumn("Model", LabelConvert.GetLabelText("Model"));
            GridExControl.MainGrid.AddColumn("Maker", LabelConvert.GetLabelText("Maker"));
            GridExControl.MainGrid.AddColumn("InstallDate", LabelConvert.GetLabelText("InstallDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("SerialNo", LabelConvert.GetLabelText("SerialNo"));
            GridExControl.MainGrid.AddColumn("CheckTurn", LabelConvert.GetLabelText("CheckTurn"));
            GridExControl.MainGrid.AddColumn("NextCheckDate", LabelConvert.GetLabelText("NextCheckDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("ManufactureDate", LabelConvert.GetLabelText("ProductionDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");     // 2021-06-02 김진우 주임 추가
            GridExControl.MainGrid.AddColumn("Class", LabelConvert.GetLabelText("Class"));
            GridExControl.MainGrid.AddColumn("ClassDate", LabelConvert.GetLabelText("ClassDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("Score", LabelConvert.GetLabelText("Score"), HorzAlignment.Center, FormatType.Numeric, "#,#");
            GridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("FileName"));
            GridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            GridExControl.MainGrid.AddColumn("FileName2", LabelConvert.GetLabelText("CheckPointFileName"));
            GridExControl.MainGrid.AddColumn("FileUrl2", LabelConvert.GetLabelText("CheckPointFileUrl"), false);
            GridExControl.MainGrid.AddColumn("FileName3", LabelConvert.GetLabelText("CheckMaintenanceFileName"));
            GridExControl.MainGrid.AddColumn("FileUrl3", LabelConvert.GetLabelText("CheckMaintenanceFileUrl"), false);
            GridExControl.MainGrid.AddColumn("DailyCheckFlag", LabelConvert.GetLabelText("DailyCheckFlag"));
            GridExControl.MainGrid.AddColumn("MachineStopCheckManageFlag", LabelConvert.GetLabelText("MachineStopCheckManageFlag"), false);      // 2021-06-07 김진우 주임 추가     비가동품질관리여부 추가
            GridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcTeamCode", DbRequestHandler.GetCommCode(MasterCodeSTR.ProductTeamCode, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Model", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineModel), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineMaker, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckTurn", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckCycle, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("DailyCheckFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("MachineStopCheckManageFlag", "N");            // 2021-06-07 김진우 주임 추가     비가동품질관리여부 추가
            GridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, MasterCodeSTR.FtpFolder_MachineImage, "FileName", "FileUrl", true);
            GridExControl.MainGrid.MainView.Columns["FileName2"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, MasterCodeSTR.FtpFolder_MachineCheckPoint, "FileName2", "FileUrl2");
            GridExControl.MainGrid.MainView.Columns["FileName3"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, MasterCodeSTR.FtpFolder_MachineMaintenance, "FileName3", "FileUrl3");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MachineMCode", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();
            InitCombo();

            var machineCodeName = lup_MachineCodeName.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            // 2021-05-21 김진우 주임 조회조건 p.MachineCode => p.MachineMCode로 변경
            GridBindingSource.DataSource = ModelService.GetList(p =>    (p.MachineMCode.Contains(machineCodeName) || (p.MachineName == machineCodeName) || p.MachineNameENG.Contains(machineCodeName) || p.MachineNameCHN.Contains(machineCodeName))
                                                                    &&  (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                               )
                                                               .OrderBy(p => p.MachineName)
                                                               .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(GridExControl);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        protected override void DeleteRow()
        {
            var obj = GridBindingSource.Current as TN_MEA1000;

            if (obj != null)
            {
                var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("MachineInfo")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    obj.UseFlag = "N";
                    GridExControl.BestFitColumns();
                }
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFMEA1000, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }

    }
}