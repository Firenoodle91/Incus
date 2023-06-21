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
            //datePeriodEditEx1.SetTodayIsMonth();

            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today;
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarVisible(false);

            #region 기존방식
            //MasterGridExControl.MainGrid.AddColumn("ResultDate", "생산일");
            //MasterGridExControl.MainGrid.AddColumn("MachineGroupCode", "설비그룹");
            //MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비");
            //MasterGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            //MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            //MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            //MasterGridExControl.MainGrid.AddColumn("WorkId", "작업자");
            //MasterGridExControl.MainGrid.AddColumn("QTY", "생산량", HorzAlignment.Far, FormatType.Numeric, "{ 0:#,###,###,##0.##}");
            //MasterGridExControl.MainGrid.AddColumn("WorkTime", "작업시간(분)", HorzAlignment.Far, FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn("UPH", "표준 UPH", HorzAlignment.Far, FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn("SpendUPH", "실 UPH", HorzAlignment.Far, FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn("SpendUPH_Rate", "달성률", HorzAlignment.Far, FormatType.Numeric, "{0:#,###,###,##0.##}%");
            #endregion

            MasterGridExControl.MainGrid.AddColumn("ResultDate", LabelConvert.GetLabelText("ResultDate"));
            MasterGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));

            MasterGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("Process"));
            MasterGridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));
            MasterGridExControl.MainGrid.AddColumn("QTY", LabelConvert.GetLabelText("ResultQtyTwo"), HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##}");
            MasterGridExControl.MainGrid.AddColumn("WorkTime", LabelConvert.GetLabelText("POPWorkTime"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("UPH", LabelConvert.GetLabelText("StandardUPH"), HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##}");

            MasterGridExControl.MainGrid.AddColumn("SpendUPH", LabelConvert.GetLabelText("SpendUPH"), HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##}");
            MasterGridExControl.MainGrid.AddColumn("SpendUPH_Rate", LabelConvert.GetLabelText("SpendUPH_Rate"), HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##}%");

            MasterGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetList(p => true).ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));


            lupItemCode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());            
            lupMachineCode.SetDefault(true, "MachineMCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList());            
            lupWorkId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y" && p.MainYn == "02").ToList());

        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            MasterGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();
            InitRepository();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                //var FromDate = new SqlParameter("@FromDate", datePeriodEditEx1.DateFrEdit.DateTime);
                var FromDate = new SqlParameter("@FromDate", datePeriodEditEx1.DateFrEdit.DateTime.ToShortDateString());
                //var ToDate = new SqlParameter("@ToDate", datePeriodEditEx1.DateToEdit.DateTime);
                var ToDate = new SqlParameter("@ToDate", datePeriodEditEx1.DateToEdit.DateTime.ToShortDateString());
                var MachineCode = new SqlParameter("@MachineCode", lupMachineCode.EditValue.GetNullToEmpty()); 
                var ItemCode = new SqlParameter("@ItemCode", lupItemCode.EditValue.GetNullToEmpty());
                var WorkId = new SqlParameter("@WorkId", lupWorkId.EditValue.GetNullToEmpty());
                var result = context.Database.SqlQuery<DataModel>("SP_GET_UPH1100_V3 @FromDate, @ToDate, @MachineCode, @ItemCode, @WorkId", FromDate, ToDate, MachineCode, ItemCode, WorkId).ToList();
                //var result = context.Database.SqlQuery<DataModel>("SP_GET_UPH1100_V2 @FromDate, @ToDate, @MachineCode, @ItemCode, @WorkId", FromDate, ToDate, MachineCode, ItemCode, WorkId).ToList(); // 20210616 오세완 차장 제품명 출력 때문에 변경 처리

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
            public string WorkId { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string ProcessCode { get; set; }
            public int? UPH { get; set; }
            public decimal? SpendUPH { get; set; }
            public decimal? SpendUPH_Rate { get; set; }
            public decimal QTY { get; set; }
            public decimal WorkTime { get; set; }
        }
    }
}