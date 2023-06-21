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
using HKInc.Ui.Model.Domain.VIEW;

using HKInc.Service.Handler;
using HKInc.Service.Helper;

using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.UPH
{
    public partial class XFUPH1300 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_UPH_REPORT> ModelService = (IService<VI_UPH_REPORT>)ProductionFactory.GetDomainService("VI_UPH_REPORT");
        public XFUPH1300()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
        }

        protected override void InitControls()
        {
            base.InitControls();
            datePeriodEditEx1.SetTodayIsMonth();
        }

        protected override void InitCombo()
        {
            lupItemCode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            lupMachineCode.SetDefault(true, "MachineMCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList());
            lupWorkId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y" && p.MainYn == "02").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("WorkDt", "생산일");
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비코드");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            MasterGridExControl.MainGrid.AddColumn("WorkQty", "생산수량");
            MasterGridExControl.MainGrid.AddColumn("OkQty", "양품수량");
            MasterGridExControl.MainGrid.AddColumn("FailQty", "불량수량");
            MasterGridExControl.MainGrid.AddColumn("Worker", "작업자");
            MasterGridExControl.MainGrid.AddColumn("StdUPH", "표준 UPH", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("RealUPH", "실 UPH", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("Rat", "달성률", HorzAlignment.Far, FormatType.Numeric, "{0:#,###,###,##0.##}%");            
            MasterGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.TopCategory_WAN && p.UseFlag == "Y").ToList(), "ItemCode", "ItemName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => true).ToList(), "MachineMCode", "MachineName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Worker", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            MasterGridExControl.MainGrid.Clear();
            
            ModelService.ReLoad();
            string item = lupItemCode.EditValue.GetNullToEmpty();
            string machine = lupMachineCode.EditValue.GetNullToEmpty();
            string worker = lupWorkId.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.WorkDt >= datePeriodEditEx1.DateFrEdit.DateTime && p.WorkDt <= datePeriodEditEx1.DateToEdit.DateTime)
            && (string.IsNullOrEmpty(item) ? true : p.ItemCode == item) && (string.IsNullOrEmpty(machine) ? true : p.MachineCode == machine) && (string.IsNullOrEmpty(worker) ? true : p.Worker == worker)).ToList();

            InitCombo();
            InitRepository();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }
    }
}