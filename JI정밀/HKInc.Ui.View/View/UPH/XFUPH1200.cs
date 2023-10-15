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
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.UPH
{
    /// <summary>
    /// 시간당생산량현황
    /// </summary>
    public partial class XFUPH1200 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_UPH_DATA> ModelService = (IService<TN_UPH_DATA>)ProductionFactory.GetDomainService("TN_UPH_DATA");
        public XFUPH1200()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
        }

        protected override void InitControls()
        {
            base.InitControls();

            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today;
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today;
            dtbase.DateTime = DateTime.Today.Date.AddDays(-1);
        }

        protected override void InitCombo()
        {
            lupItemCode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            lupMachineCode.SetDefault(true, "MachineMCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList());
            lupWorkId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y" && p.MainYn == "02").ToList());
            lup_mc_group.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarVisible(true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            MasterGridExControl.MainGrid.AddColumn("Seq", false);
            MasterGridExControl.MainGrid.AddColumn("WorkDt", LabelConvert.GetLabelText("WorkDt"));
            MasterGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("Process"));
            MasterGridExControl.MainGrid.AddColumn("Worker", LabelConvert.GetLabelText("WorkId"));
            MasterGridExControl.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("ResultQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("OkQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("FailQty", LabelConvert.GetLabelText("BadQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("WorkTime", LabelConvert.GetLabelText("POPWorkTime"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WorkDt", "MachineCode", "ItemCode", "ProcessCode", "Worker", "WorkQty", "OkQty", "FailQty", "WorkTime");

            MasterGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode",    ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.TopCategory_WAN && p.UseFlag == "Y").ToList(), "ItemCode", "ItemName");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetList(p => true).ToList(), "MachineMCode", "MachineName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => true).ToList(), "MachineMCode", "MachineName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Worker",      ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");

        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            MasterGridExControl.MainGrid.Clear();

            ModelService.ReLoad();
            string item = lupItemCode.EditValue.GetNullToEmpty();
            string machine = lupMachineCode.EditValue.GetNullToEmpty();
            string mcg = lup_mc_group.EditValue.GetNullToEmpty();
            string worker = lupWorkId.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = 
                ModelService.GetList(p => (p.WorkDt >= datePeriodEditEx1.DateFrEdit.DateTime && p.WorkDt <= datePeriodEditEx1.DateToEdit.DateTime)
                && (string.IsNullOrEmpty(mcg) ? true : p.TN_MEA1000.MachineGroupCode == mcg) 
                && (string.IsNullOrEmpty(item) ? true : p.ItemCode == item) 
                && (string.IsNullOrEmpty(machine) ? true : p.MachineCode == machine) 
                && (string.IsNullOrEmpty(worker) ? true : p.Worker == worker)
                ).ToList();


            InitCombo();
            InitRepository();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void AddRowClicked()
        {
            TN_UPH_DATA obj = new TN_UPH_DATA() { WorkDt = DateTime.Today, Seq = DbRequestHandler.GetSeqYear("WORKQRT") };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }
        protected override void DeleteRow()
        {
            TN_UPH_DATA obj = MasterGridBindingSource.Current as TN_UPH_DATA;
            if (obj == null) return;
            MasterGridBindingSource.RemoveCurrent();
            ModelService.Delete(obj);
        }
        protected override void DataSave()
        {
            ModelService.Save();
        }



        private void dtto_EditValueChanged(object sender, EventArgs e)
        {
            if (dtbase.DateTime.Date == dtto.DateTime.Date)
            {
                MessageBox.Show("기준일과 복사일은 같을수 없습니다.");
                Btn_COPY.Enabled = false;
                return;
            }
            string worker = lupWorkId.EditValue.GetNullToEmpty();

            var olist = ModelService.GetList(p => (p.WorkDt == dtto.DateTime.Date & p.Worker == worker)).ToList();
            if (olist.Count >= 1)
            {

                MessageBox.Show(dtto.DateTime.Date.ToString() + "일에 입력된 값이 있어서 복사할수 없습니다.");
                Btn_COPY.Enabled = false;
                return;

            }

            datePeriodEditEx1.DateFrEdit.EditValue = dtto.DateTime.Date;
            datePeriodEditEx1.DateToEdit.EditValue = dtto.DateTime.Date;
            Btn_COPY.Enabled = true;
        }

        private void Btn_COPY_Click(object sender, EventArgs e)
        {
            if (dtbase.DateTime.Date == dtto.DateTime.Date)
            {
                MessageBox.Show("기준일과 복사일은 같을수 없습니다.");
                Btn_COPY.Enabled = false;
                return;
            }
            MasterGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            //string worker = lupWorkId.EditValue.GetNullToEmpty();
            //if (worker == "")
            //{
            //    MessageBox.Show("복사시 작업자는  필수 값입니다..");
            //    return;
            //}

            string mcg = lup_mc_group.EditValue.GetNullToEmpty();
            if (mcg == "")
            {
                MessageBox.Show("설비그룹은 필수입니다.");
                //    MasterGridBindingSource.DataSource= ModelService.GetList(p => (p.WorkDt == dtto.DateTime.Date)).ToList();
            }
            else
            {
                List<TN_UPH_DATA> lowdata = ModelService.GetList(p => (p.WorkDt == dtbase.DateTime.Date && p.TN_MEA1000.Temp1 == mcg)).OrderBy(o => o.MachineCode).OrderBy(o => o.ItemCode).ToList();
                if (lowdata.Count <= 0) return;
                for (int i = 0; i < lowdata.Count; i++)
                {
                    TN_UPH_DATA nobj = new TN_UPH_DATA();
                    nobj.WorkDt = dtto.DateTime.Date;
                    nobj.Seq = lowdata[i].Seq;
                    nobj.MachineCode = lowdata[i].MachineCode;
                    nobj.ItemCode = lowdata[i].ItemCode;
                    nobj.ProcessCode = lowdata[i].ProcessCode;
                    nobj.WorkQty = lowdata[i].WorkQty;
                    nobj.OkQty = lowdata[i].OkQty;
                    nobj.FailQty = lowdata[i].FailQty;
                    nobj.WorkTime = lowdata[i].WorkTime;
                    nobj.Worker = lowdata[i].Worker;
                    ModelService.Insert(nobj);
                    MasterGridBindingSource.Add(nobj);
                }
            }
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
        }


    }
}
