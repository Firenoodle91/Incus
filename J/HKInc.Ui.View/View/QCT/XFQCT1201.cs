using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Service.Helper;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Domain.TEMP;
using System.Data.SqlClient;





namespace HKInc.Ui.View.View.QCT
{
    /// <summary>
    /// LOT역 추적관리
    /// </summary>
    public partial class XFQCT1201 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<VI_LOT_TRACKING_V2> ModelService = (IService<VI_LOT_TRACKING_V2>)ProductionFactory.GetDomainService("VI_LOT_TRACKING_V2");

        public XFQCT1201()
        {
            InitializeComponent();

            MasterGridExControl = gridEx2;
            DetailGridExControl = gridEx1;

        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"), false);
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));            
            MasterGridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"));
            MasterGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"), false);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            DetailGridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Far, true);
            DetailGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            DetailGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"));
            DetailGridExControl.MainGrid.AddColumn("ResultDate", LabelConvert.GetLabelText("ResultDate"));
            DetailGridExControl.MainGrid.AddColumn("CustomerLotNo", LabelConvert.GetLabelText("CustomerLotNo"));
            DetailGridExControl.MainGrid.AddColumn("ResultQty", LabelConvert.GetLabelText("ResultQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("OkQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("BadQty", LabelConvert.GetLabelText("BadQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##"); 
            //DetailGridExControl.MainGrid.AddColumn("Heat", LabelConvert.GetLabelText("HeatTemperature"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            //DetailGridExControl.MainGrid.AddColumn("Rpm", LabelConvert.GetLabelText("HeatRpm"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            DetailGridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));
            DetailGridExControl.MainGrid.AddColumn("SrcItemCode", LabelConvert.GetLabelText("SrcItemCode"));
            DetailGridExControl.MainGrid.AddColumn("SrcItemName", LabelConvert.GetLabelText("SrcItemName"));
            DetailGridExControl.MainGrid.AddColumn("SrcItemName1", LabelConvert.GetLabelText("SrcItemName1"));
            DetailGridExControl.MainGrid.AddColumn("SrcInLotNo", LabelConvert.GetLabelText("SrcInLotNo"));

            var countSummary = new DevExpress.XtraGrid.GridGroupSummaryItem();
            countSummary.FieldName = "countQty";
            countSummary.SummaryType = DevExpress.Data.SummaryItemType.Count;
            countSummary.DisplayFormat = "(" + LabelConvert.GetLabelText("Count") + ":{0:n0})";

            var resultQtySummary = new DevExpress.XtraGrid.GridGroupSummaryItem();
            resultQtySummary.FieldName = "ResultQty";
            resultQtySummary.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            resultQtySummary.DisplayFormat = "(" + LabelConvert.GetLabelText("SumResultQty2") + ":{0:#,0.##})";

            var okQtySummary = new DevExpress.XtraGrid.GridGroupSummaryItem();
            okQtySummary.FieldName = "OkQty";
            okQtySummary.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            okQtySummary.DisplayFormat = "(" + LabelConvert.GetLabelText("SumOkQty2") + ":{0:#,0.##})";

            var badQtySummary = new DevExpress.XtraGrid.GridGroupSummaryItem();
            badQtySummary.FieldName = "BadQty";
            badQtySummary.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            badQtySummary.DisplayFormat = "(" + LabelConvert.GetLabelText("SumBadQty2") + ":{0:#,0.##})";

            DetailGridExControl.MainGrid.MainView.GroupSummary.Add(countSummary);
            DetailGridExControl.MainGrid.MainView.GroupSummary.Add(resultQtySummary);
            DetailGridExControl.MainGrid.MainView.GroupSummary.Add(okQtySummary);
            DetailGridExControl.MainGrid.MainView.GroupSummary.Add(badQtySummary);
            dt_OutDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dt_OutDate.DateToEdit.DateTime = DateTime.Today;

        }

        protected override void InitCombo()
        {      
            lup_ItemCode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && 
                (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).ToList()); // 20210524 오세완 차장 반제품(타사)가 출고될 수도 있기 때문에 조회조건에 추가
            Iup_CustomerCode.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            
            MasterGridExControl.BestFitColumns();

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => true).ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName");
           
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            var releaseNumer = tx_ReleaseNumber.EditValue.GetNullToEmpty();
            var itemCode = lup_ItemCode.EditValue.GetNullToEmpty();
            var Customer = Iup_CustomerCode.EditValue.GetNullToEmpty();

            ModelService.ReLoad();

            //데이터리로드
            InitRepository();
            InitCombo();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var OutDateFr = new SqlParameter("@OutDateFr", dt_OutDate.DateFrEdit.DateTime);
                var OutDateTo = new SqlParameter("@OutDateTo", dt_OutDate.DateToEdit.DateTime);
                var OutNo = new SqlParameter("@OutNo", releaseNumer);
                var ItemCode = new SqlParameter("@ItemCode", itemCode);
                var CustomerCode = new SqlParameter("@CustomerCode", Customer);
     
                MasterGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_XFQCT1201_MASTER>("USP_GET_XFQCT1201_MASTER @OutDateFr,@OutDateTo,@OutNo,@ItemCode,@CustomerCode", OutDateFr, OutDateTo, OutNo, ItemCode, CustomerCode).OrderBy(p => p.ItemCode).ToList();

            }
            
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            var masterObj = MasterGridBindingSource.Current as TEMP_XFQCT1201_MASTER;
            if (masterObj == null) return;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                SqlParameter param1 = new SqlParameter("@LotNo", masterObj.ProductLotNo);

                
                DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_XFQCT1201_DETAIL>("EXEC USP_GET_XFQCT1201_DETAIL @LotNo", param1).ToList();
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();     
        }
    }
}