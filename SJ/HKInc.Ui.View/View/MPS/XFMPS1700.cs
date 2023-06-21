using System;
using System.Data;
using System.Linq;
using DevExpress.Utils;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Class;
using HKInc.Service.Helper;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 재공재고현황
    /// </summary>
    public partial class XFMPS1700 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFMPS1700()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetList(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            lup_ProductTeam.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            dt_EndMonthDate.SetFormat(Utils.Enum.DateFormat.Month);
            dt_EndMonthDate.DateTime = DateTime.Today;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            DetailGridExControl.SetToolbarVisible(false);

            MasterGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("Customer"));
            MasterGridExControl.MainGrid.AddColumn("PublishDate", LabelConvert.GetLabelText("PublishDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("StartDueDate", LabelConvert.GetLabelText("StartDueDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("FinishDueDate", LabelConvert.GetLabelText("FinishDueDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("EndMonthDate", LabelConvert.GetLabelText("EndMonthDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM", false);
            MasterGridExControl.MainGrid.AddColumn("PlanWorkQty", LabelConvert.GetLabelText("PlanWorkQty"), HorzAlignment.Far, FormatType.Numeric, "N0");
            MasterGridExControl.MainGrid.AddColumn("PackQty", LabelConvert.GetLabelText("PackQty2"), HorzAlignment.Far, FormatType.Numeric, "N0");
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("비고"), HorzAlignment.Center, true);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemName", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.MainView.Columns.Clear();

            ModelService.ReLoad();


            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var EndMonthDate = new SqlParameter("@EndMonthDate", dt_EndMonthDate.DateTime);
                var ProductTeam = new SqlParameter("@ProductTeam", lup_ProductTeam.EditValue.GetNullToEmpty());
                var ItemCode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
                var result = context.Database.SqlQuery<TEMP_XFMPS1700_MASTER>("USP_GET_XFMPS1700_MASTER_LIST @EndMonthDate,@ProductTeam ,@ItemCode", EndMonthDate, ProductTeam, ItemCode).ToList();
                MasterGridBindingSource.DataSource = result.OrderBy(p => p.WorkNo).ToList();
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TEMP_XFMPS1700_MASTER;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.MainView.Columns.Clear();
                return;
            }

            var WorkNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
            var ds = DbRequestHandler.GetDataSet("USP_GET_XFMPS1700_DETAIL_LIST", WorkNo);

            SetColumn(ds);

            DetailGridExControl.DataSource = ds.Tables[0];
            DetailGridExControl.BestFitColumns();
        }

        private void SetColumn(DataSet ds)
        {
            //DetailGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            //DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            //DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            //DetailGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));

            var cultureIndex = DataConvert.GetCultureIndex();
            var processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                var columnName = ds.Tables[0].Columns[i].ColumnName.GetNullToEmpty();
                if (columnName.Left(3) == "PPP")
                {
                    if (columnName.Right(4) == "Wait")
                    {
                        var processCode = columnName.Substring(4, columnName.Substring(4).IndexOf('_'));
                        var processObj = processList.Where(p => p.CodeVal == processCode).FirstOrDefault();
                        if (processObj != null)
                        {
                            var processName = cultureIndex == 1 ? processObj.CodeName : (cultureIndex == 2 ? processObj.CodeNameENG : processObj.CodeNameCHN);
                            DetailGridExControl.MainGrid.AddColumn(columnName, processName + LabelConvert.GetLabelText("Wait"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
                        }
                        else
                        {
                            DetailGridExControl.MainGrid.AddColumn(columnName, columnName, HorzAlignment.Far, FormatType.Numeric, "#,0.##");
                        }
                    }
                    else
                    {
                        var processCode = columnName.Substring(4);
                        var processObj = processList.Where(p => p.CodeVal == processCode).FirstOrDefault();
                        if (processObj != null)
                        {
                            var processName = cultureIndex == 1 ? processObj.CodeName : (cultureIndex == 2 ? processObj.CodeNameENG : processObj.CodeNameCHN);
                            DetailGridExControl.MainGrid.AddColumn(columnName, processName + LabelConvert.GetLabelText("Ing"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
                        }
                        else
                        {
                            DetailGridExControl.MainGrid.AddColumn(columnName, columnName, HorzAlignment.Far, FormatType.Numeric, "#,0.##");
                        }
                    }
                }
            }

            //GridExControl.MainGrid.Columns["WorkNo"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //GridExControl.MainGrid.Columns["ItemCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //GridExControl.MainGrid.Columns[DataConvert.GetCultureDataFieldName("ItemName")].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //GridExControl.MainGrid.Columns["CustomerCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
        }

    }
}
