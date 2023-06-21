using System;
using System.Data;
using System.Linq;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using DevExpress.Utils;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 일일생산실적
    /// </summary>
    public partial class XFMPS1301 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1202> ModelService = (IService<TN_MPS1202>)ProductionFactory.GetDomainService("TN_MPS1202");

        public XFMPS1301()
        {
            InitializeComponent();

            dt_WorkDate.DateTime = DateTime.Today;
            GridExControl = gridEx1;
            chkYn.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            chkYn.Properties.NullText = "N";
            chkYn.Properties.ValueChecked = "Y";
            chkYn.Properties.ValueUnchecked = "N";

            GridExControl.MainGrid.MainView.CustomSummaryCalculate += GridView_CustomSummaryCalculate;
        }

        protected override void InitCombo()
        {
            lupMachine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
            lupProcess.SetDefault(true, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process));
            lupItem.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.TopCategory_WAN && p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("OrderCustomer"));
            GridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));
            GridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));
            GridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("OkQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("BadQty", LabelConvert.GetLabelText("BadQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"));
            GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            GridExControl.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            GridExControl.MainGrid.AddColumn("AchieveRate", LabelConvert.GetLabelText("AchieveRate"), HorzAlignment.Far, FormatType.Numeric, "{0:#,###,###,##0}%");

            GridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;

            GridExControl.MainGrid.MainView.Columns["OkQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["OkQty"].SummaryItem.FieldName = "OkQty";
            GridExControl.MainGrid.MainView.Columns["OkQty"].SummaryItem.DisplayFormat = "{0:n0}";
            GridExControl.MainGrid.MainView.Columns["BadQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["BadQty"].SummaryItem.FieldName = "BadQty";
            GridExControl.MainGrid.MainView.Columns["BadQty"].SummaryItem.DisplayFormat = "{0:n0}";
            GridExControl.MainGrid.MainView.Columns["WorkQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["WorkQty"].SummaryItem.FieldName = "WorkQty";
            GridExControl.MainGrid.MainView.Columns["WorkQty"].SummaryItem.DisplayFormat = "{0:n0}";

            GridExControl.MainGrid.MainView.Columns["AchieveRate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            GridExControl.MainGrid.MainView.Columns["AchieveRate"].SummaryItem.DisplayFormat = "{0:n2}";

        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemName", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));            

            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                string item = lupItem.EditValue.GetNullToEmpty();
                string mc = lupMachine.EditValue.GetNullToEmpty();
                string process = lupProcess.EditValue.GetNullToEmpty();

                var date = new SqlParameter("@WorkDate", dt_WorkDate.DateTime);
                var rateYn = new SqlParameter("@RateYn", chkYn.EditValue);

                var result = context.Database
                      .SqlQuery<TEMP_XFMPS1301>("USP_GET_MPS1301_LIST @WorkDate, @RateYn", date, rateYn).ToList();

                GridBindingSource.DataSource = result.Where(p => (string.IsNullOrEmpty(item) ? true : p.ItemCode == item)
                                                            && (string.IsNullOrEmpty(mc) ? true : p.MachineCode == mc)
                                                            && (string.IsNullOrEmpty(process) ? true : p.ProcessCode == process)).ToList();
            }
            GridExControl.DataSource = GridBindingSource;            
            GridExControl.MainGrid.BestFitColumns();
            SetRefreshMessage(GridExControl);
        }

        //달성률 계산
        private void GridView_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView gv = sender as GridView;

            if(e.IsTotalSummary && (e.Item as GridSummaryItem).FieldName == "AchieveRate")
            {
                GridSummaryItem item = e.Item as GridSummaryItem;

                if (item.FieldName == "AchieveRate")
                {

                    decimal OkSumQty = 0;   //총 양품수량
                    decimal WorkSumQty = 0; //총 지시수량

                    if(e.SummaryProcess == CustomSummaryProcess.Finalize)
                    {
                        OkSumQty = gv.Columns["OkQty"].SummaryItem.SummaryValue.GetIntNullToZero();
                        WorkSumQty = gv.Columns["WorkQty"].SummaryItem.SummaryValue.GetIntNullToZero();

                        if(OkSumQty > 0 && WorkSumQty > 0)
                            e.TotalValue = OkSumQty / WorkSumQty * 100;
                    }
                }
            }

        }
    }
}