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
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 설비가동정보등록
    /// </summary>
    public partial class XFMTTF1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");

        public XFMTTF1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            //MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            //DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));

        }

        protected override void InitCombo()
        {
            lup_MachineCodeName.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetList(p => true));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            //MasterGridExControl.SetToolbarButtonVisible(false);

            MasterGridExControl.MainGrid.AddColumn("MachineMCode", LabelConvert.GetLabelText("MachineMCode"));
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비코드");
            MasterGridExControl.MainGrid.AddColumn("MachineName", "설비명");
            MasterGridExControl.MainGrid.AddColumn("Model", "모델번호");
            MasterGridExControl.MainGrid.AddColumn("Maker", "제조회사");
            MasterGridExControl.MainGrid.AddColumn("InstallDate", "설치일");
            MasterGridExControl.MainGrid.AddColumn("SerialNo", "일련번호");


            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.MainGrid.AddColumn("MachineCode", false);
            DetailGridExControl.MainGrid.AddColumn("YYYY", "년도", DevExpress.Utils.HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("MM", "월");
            DetailGridExControl.MainGrid.AddColumn("RunTime", "작업시간", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("StopTime", "정지시간", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("StopCnt", "정지횟수", HorzAlignment.Far, FormatType.Numeric, "n0");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "YYYY", "MM", "RunTime", "StopTime", "StopCnt");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineMaker, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MM", DbRequestHandler.GetCommCode(MasterCodeSTR.MM, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");

            //DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, DetailGridExControl, MasterCodeSTR.FtpFolder_InstrCheckHistory, "FileName", "FileUrl");
            
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            DetailGridRowLocator.GetCurrentRow("MachineCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();
            InitCombo();

            var MCCodeName = lup_MachineCodeName.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.MachineCode.Contains(MCCodeName) || (p.MachineName == MCCodeName))
                                                                    && (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                                    && p.SerialNo != "ETC")
                                                                    .OrderBy(p => p.MachineCode)
                                                                    .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            SetRefreshMessage(MasterGridExControl);
            GridRowLocator.SetCurrentRow();
            DetailGridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_MEA1000 obj = MasterGridBindingSource.Current as TN_MEA1000;
            DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_MTTF1000>(p => p.MachineCode == obj.MachineCode).OrderBy(o => o.YYYY).ThenBy(o => o.MM).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();
        }

        protected override void DataSave()
        {
            ModelService.Save();
            DataLoad();
        }


        protected override void DetailAddRowClicked()
        {
            //    base.DetailAddRowClicked();
            if (!UserRight.HasEdit) return;
            TN_MEA1000 obj1 = MasterGridBindingSource.Current as TN_MEA1000;
            if (obj1 != null)
            {
                TN_MTTF1000 obj = new TN_MTTF1000();
                obj.MachineCode = obj1.MachineCode;
                obj.YYYY = DateTime.Today.ToString("yyyy");



                DetailGridBindingSource.Add(obj);
                ModelService.InsertChild(obj);
                DetailGridBindingSource.MoveLast();

            }

            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            TN_MEA1000 obj1 = MasterGridBindingSource.Current as TN_MEA1000;
            TN_MTTF1000 obj = DetailGridBindingSource.Current as TN_MTTF1000;

            if (obj != null)
            {
                ModelService.RemoveChild<TN_MTTF1000>(obj);
                DetailGridBindingSource.RemoveCurrent();
            }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1100;
            var detailObj = DetailGridBindingSource.Current as TN_MEA1101;
            if (masterObj == null || detailObj == null) return;

            var maxSeq = masterObj.TN_MEA1101List.Max(p => p.Seq);
            if (detailObj.Seq != maxSeq)
                e.Cancel = true;
        }

    }
}