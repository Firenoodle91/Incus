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
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using DevExpress.Utils;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using HKInc.Service.Handler;
using HKInc.Utils.Common;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;



namespace HKInc.Ui.View.View.ORD_POPUP
{
    /// <summary>
    /// 납기회의목록 상세보기
    /// </summary>
    public partial class XPFORD1102 : Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_XFORD1102_MASTER_VIEW> ModelService = (IService<VI_XFORD1102_MASTER_VIEW>)ProductionFactory.GetDomainService("VI_XFORD1102_MASTER_VIEW");

        private string DelivNo;

        public XPFORD1102()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            GridExControl.ActAddRowClicked += GridExControl_ActAddRowClicked;
        }

        public XPFORD1102(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("LotTrackingInfo");

            DelivNo = parameter.GetValue(PopupParameter.KeyValue).GetNullToEmpty();
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitBindingSource(){}

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            GridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("Grouping") + "[F3]");

            GridExControl.MainGrid.AddColumn("PlanNo", LabelConvert.GetLabelText("PlanNo"));
            GridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            GridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Far, true);
            GridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            GridExControl.MainGrid.AddColumn("ItemMoveNo", LabelConvert.GetLabelText("ItemMoveNo"));
            GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"));
            GridExControl.MainGrid.AddColumn("ResultDate", LabelConvert.GetLabelText("ResultDate"));
            GridExControl.MainGrid.AddColumn("CustomerLotNo", LabelConvert.GetLabelText("CustomerLotNo"));
            GridExControl.MainGrid.AddColumn("ResultQty", LabelConvert.GetLabelText("ResultQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("OkQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("BadQty", LabelConvert.GetLabelText("BadQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            //GridExControl.MainGrid.AddColumn("BadType", LabelConvert.GetLabelText("BadType"));
            GridExControl.MainGrid.AddColumn("Heat", LabelConvert.GetLabelText("HeatTemperature"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("Rpm", LabelConvert.GetLabelText("HeatRpm"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));
            GridExControl.MainGrid.AddColumn("SrcItemCode", LabelConvert.GetLabelText("SrcItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100_TOOL." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("SrcItemName"));
            
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

            GridExControl.MainGrid.MainView.GroupSummary.Add(countSummary);
            GridExControl.MainGrid.MainView.GroupSummary.Add(resultQtySummary);
            GridExControl.MainGrid.MainView.GroupSummary.Add(okQtySummary);
            GridExControl.MainGrid.MainView.GroupSummary.Add(badQtySummary);

            GridExControl.MainGrid.Columns["WorkNo"].GroupIndex = 0;
            GridExControl.MainGrid.Columns["ProductLotNo"].GroupIndex = 1;
            //GridExControl.MainGrid.Columns["ProcessCode"].GroupIndex = 2;
            GridExControl.MainGrid.Columns["ProcessSeq"].GroupIndex = 2;
            //GridExControl.MainGrid.MainView.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Office2003;
            GridExControl.MainGrid.MainView.OptionsBehavior.AllowFixedGroups = DefaultBoolean.True;
            GridExControl.MainGrid.MainView.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            GridExControl.MainGrid.MainView.Appearance.GroupRow.Font = new Font("맑은 고딕", 9f, FontStyle.Bold);
            // Expand group rows. 
            GridExControl.MainGrid.MainView.OptionsBehavior.AutoExpandAllGroups = true;

            SetGridGrouping(false);
        }

        protected override void InitRepository()
        {           
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => true).ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BadType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));

            GridExControl.BestFitColumns();
        }


        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var itemCode = lup_Item.EditValue.GetNullToEmpty();

            ModelBindingSource.DataSource = ModelService.GetList(p => (p.DelivNo == DelivNo)
                                                                 && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode))
                                                                .OrderBy(p => p.ProductLotNo)
                                                                .ThenBy(p => p.ProcessSeq)
                                                                .ThenBy(p => p.ResultDate)
                                                                .ToList();
            
            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();

            ModelService.ReLoad();          

        }
        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (GridExControl.MainGrid.MainView.OptionsBehavior.AutoExpandAllGroups)
            {
                GridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("NotGrouping") + "[F3]");
            }
            else
            {
                GridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("Grouping") + "[F3]");
            }
        }

        private void GridExControl_ActAddRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetGridGrouping(!GridExControl.MainGrid.MainView.OptionsBehavior.AutoExpandAllGroups);
            IsFormControlChanged = false;
        }

        private void SetGridGrouping(bool enable)
        {
            if (enable)
            {
                GridExControl.MainGrid.Columns["WorkNo"].GroupIndex = 0;
                GridExControl.MainGrid.Columns["ProductLotNo"].GroupIndex = 1;
                GridExControl.MainGrid.Columns["ProcessSeq"].GroupIndex = 2;
                GridExControl.MainGrid.MainView.OptionsBehavior.AutoExpandAllGroups = enable;
            }
            else
            {
                GridExControl.MainGrid.MainView.ClearGrouping();
                GridExControl.MainGrid.MainView.OptionsBehavior.AutoExpandAllGroups = enable;
            }
            GridExControl.BestFitColumns();
            MainView_FocusedRowChanged(null, null);
        }
    }
}