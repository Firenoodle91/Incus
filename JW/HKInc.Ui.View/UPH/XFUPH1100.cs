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

namespace HKInc.Ui.View.UPH
{
    public partial class XFUPH1100 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
        public XFUPH1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
        }

        protected override void InitControls()
        {
            base.InitControls();
            datePeriodEditEx1.SetTodayIsMonth();
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ResultDate","생산일");
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비코드");
            MasterGridExControl.MainGrid.AddColumn("MachineName", "설비명");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            MasterGridExControl.MainGrid.AddColumn("WorkId", "작업자");
            MasterGridExControl.MainGrid.AddColumn("UPH", "표준 UPH", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("SpendUPH", "실 UPH", HorzAlignment.Far, FormatType.Numeric, "{0:#,###,###,##0.##}");
            MasterGridExControl.MainGrid.AddColumn("SpendUPH_Rate", "달성률", HorzAlignment.Far, FormatType.Numeric, "{0:#,###,###,##0.##}%");
            MasterGridExControl.MainGrid.AddColumn("S01", "계획정지", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("S02", "교육", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("S03", "설비고장", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("S04", "자재부족", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            var ItemList = ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").ToList();
            var MachineList = ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList();
            var UserList = ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList();
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemNm", ItemList, "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineName", MachineList, "MachineCode", "MachineName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", UserList, "LoginId", "UserName");

            lupItemCode.SetDefault(true, "ItemCode", "ItemCode", ItemList);
            lupMachineCode.SetDefault(true, "MachineCode", "MachineName", MachineList);
            lupWorkId.SetDefault(true, "LoginId", "UserName", UserList);

        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            MasterGridExControl.MainGrid.Clear();
            
            ModelService.ReLoad();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var FromDate = new SqlParameter("@FromDate", datePeriodEditEx1.DateFrEdit.DateTime);
                var ToDate = new SqlParameter("@ToDate", datePeriodEditEx1.DateToEdit.DateTime);
                var MachineCode = new SqlParameter("@MachineCode", lupMachineCode.EditValue.GetNullToEmpty());
                var ItemCode = new SqlParameter("@ItemCode", lupItemCode.EditValue.GetNullToEmpty());
                var WorkId = new SqlParameter("@WorkId", lupWorkId.EditValue.GetNullToEmpty());
                var result = context.Database
                      .SqlQuery<DataModel>("SP_GET_UPH1100 @FromDate, @ToDate, @MachineCode, @ItemCode, @WorkId", FromDate, ToDate, MachineCode, ItemCode, WorkId)
                      .ToList();
                MasterGridBindingSource.DataSource = result;

            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        private class DataModel
        {
            public DateTime ResultDate { get; set; }
            public string MachineCode { get; set; }
            public string MachineName { get; set; }
            public string WorkId { get; set; }
            public string ItemCode { get; set; }
            public string ItemNm { get; set; }
            public string ProcessCode { get; set; }
            public int? UPH { get; set; }
            public decimal? SpendUPH { get; set; }
            public decimal? SpendUPH_Rate { get; set; }
            public int? S01 { get; set; }
            public int? S02 { get; set; }
            public int? S03 { get; set; }
            public int? S04 { get; set; }
        }
    }
}