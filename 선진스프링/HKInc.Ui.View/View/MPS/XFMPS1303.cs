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
using System.Collections.Generic;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 월별생산일보
    /// </summary>
    public partial class XFMPS1303 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1302> ModelService = (IService<TN_MPS1302>)ProductionFactory.GetDomainService("TN_MPS1302");

        public XFMPS1303()
        {
            InitializeComponent();
            
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
            dt_WorkDate.EditValueChanged += Dt_WorkDate_EditValueChanged;
        }

        private void Dt_WorkDate_EditValueChanged(object sender, EventArgs e)
        {
            var lastDay = DateTime.DaysInMonth(dt_WorkDate.DateTime.Year, dt_WorkDate.DateTime.Month);
            cbo_FirstDay.Properties.Items.Clear();
            cbo_LastDay.Properties.Items.Clear();

            for (int i = 1; i <= lastDay; i++)
            {
                cbo_FirstDay.Properties.Items.Add(i);
                cbo_LastDay.Properties.Items.Add(i);
            }

            cbo_FirstDay.SelectedIndex = 0;
            cbo_LastDay.SelectedIndex = cbo_LastDay.Properties.Items.Count -1;
        }

        protected override void InitCombo()
        {
            dt_WorkDate.DateTime = DateTime.Today;
            dt_WorkDate.SetFormat(Utils.Enum.DateFormat.Month);
            dt_WorkDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            lup_ProcTeamCode.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode));
            lup_Process.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process));

            cbo_FirstDay.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbo_LastDay.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;
            GridExControl.SetToolbarVisible(false);
        }

        protected override void InitRepository()
        {
        }

        private void SetGridColumn()
        {
            GridExControl.MainGrid.AddColumn("MachineMCode", LabelConvert.GetLabelText("MachineMCode"), false);
            GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("MachineName", LabelConvert.GetLabelText("MachineName"));

            //var lastDay = cbo_LastDay.SelectedIndex - 1; //DateTime.DaysInMonth(dt_WorkDate.DateTime.Year, dt_WorkDate.DateTime.Month);
            string DayTotalQtyExpression = "";
            string DayTotalCostExpression = "";
            for (int i = cbo_FirstDay.SelectedIndex + 1; i <= cbo_LastDay.SelectedIndex + 1; i++)
            {
                //if (lastDay < i)
                //    break;

                GridExControl.MainGrid.AddColumn("Day" + i.ToString().PadLeft(2, '0'), i.ToString().PadLeft(2, '0') + "일", HorzAlignment.Far, FormatType.Numeric, "#,0.##");
                GridExControl.MainGrid.MainView.Columns["Day" + i.ToString().PadLeft(2, '0')].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                GridExControl.MainGrid.MainView.Columns["Day" + i.ToString().PadLeft(2, '0')].SummaryItem.FieldName = "Day" + i.ToString().PadLeft(2, '0');
                GridExControl.MainGrid.MainView.Columns["Day" + i.ToString().PadLeft(2, '0')].SummaryItem.DisplayFormat = "{0:#,0.##}";

                GridExControl.MainGrid.AddColumn("Day" + i.ToString().PadLeft(2, '0') + "_Price", i.ToString().PadLeft(2, '0') + "일 금액", HorzAlignment.Far, FormatType.Numeric, "#,0.##");
                GridExControl.MainGrid.MainView.Columns["Day" + i.ToString().PadLeft(2, '0') + "_Price"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                GridExControl.MainGrid.MainView.Columns["Day" + i.ToString().PadLeft(2, '0') + "_Price"].SummaryItem.FieldName = "Day" + i.ToString().PadLeft(2, '0') + "_Price";
                GridExControl.MainGrid.MainView.Columns["Day" + i.ToString().PadLeft(2, '0') + "_Price"].SummaryItem.DisplayFormat = "{0:#,0.##}";

                DayTotalQtyExpression += "Day" + i.ToString().PadLeft(2, '0') + " + ";
                DayTotalCostExpression += "Day" + i.ToString().PadLeft(2, '0') + "_Price" + " + ";
            }
            if (DayTotalQtyExpression.Trim().Right(1) == "+")
                DayTotalQtyExpression = DayTotalQtyExpression.Substring(0, DayTotalQtyExpression.Length - 2);
            if (DayTotalCostExpression.Trim().Right(1) == "+")
                DayTotalCostExpression = DayTotalCostExpression.Substring(0, DayTotalCostExpression.Length - 2);
            GridExControl.MainGrid.AddUnboundColumn("DayTotalQty", "생산수량합계", UnboundColumnType.Decimal, DayTotalQtyExpression, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddUnboundColumn("DayTotalCost", "생산금액합계", UnboundColumnType.Decimal, DayTotalCostExpression, FormatType.Numeric, "#,0.##");

            GridExControl.MainGrid.AddColumn("WorkTime", LabelConvert.GetLabelText("WorkTime"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("PlanStop", LabelConvert.GetLabelText("PlanStop"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("ItemCodeChange", LabelConvert.GetLabelText("ItemCodeChange"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("SrcSoldOut", LabelConvert.GetLabelText("SrcSoldOut"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("ReAdjust", LabelConvert.GetLabelText("ReAdjust"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("MachineBroken", LabelConvert.GetLabelText("MachineBroken"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("WorkLack", LabelConvert.GetLabelText("WorkLack"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("Etc", LabelConvert.GetLabelText("OrderLack"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("SettingQty", LabelConvert.GetLabelText("SettingQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("ProcessRate", LabelConvert.GetLabelText("ProcessRate"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            GridExControl.MainGrid.MainView.Columns["DayTotalQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["DayTotalQty"].SummaryItem.FieldName = "DayTotalQty";
            GridExControl.MainGrid.MainView.Columns["DayTotalQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["DayTotalCost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["DayTotalCost"].SummaryItem.FieldName = "DayTotalCost";
            GridExControl.MainGrid.MainView.Columns["DayTotalCost"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["WorkTime"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["WorkTime"].SummaryItem.FieldName = "WorkTime";
            GridExControl.MainGrid.MainView.Columns["WorkTime"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["PlanStop"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["PlanStop"].SummaryItem.FieldName = "PlanStop";
            GridExControl.MainGrid.MainView.Columns["PlanStop"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["ItemCodeChange"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["ItemCodeChange"].SummaryItem.FieldName = "ItemCodeChange";
            GridExControl.MainGrid.MainView.Columns["ItemCodeChange"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["SrcSoldOut"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["SrcSoldOut"].SummaryItem.FieldName = "SrcSoldOut";
            GridExControl.MainGrid.MainView.Columns["SrcSoldOut"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["ReAdjust"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["ReAdjust"].SummaryItem.FieldName = "ReAdjust";
            GridExControl.MainGrid.MainView.Columns["ReAdjust"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["MachineBroken"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["MachineBroken"].SummaryItem.FieldName = "MachineBroken";
            GridExControl.MainGrid.MainView.Columns["MachineBroken"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["WorkLack"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["WorkLack"].SummaryItem.FieldName = "WorkLack";
            GridExControl.MainGrid.MainView.Columns["WorkLack"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["Etc"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["Etc"].SummaryItem.FieldName = "Etc";
            GridExControl.MainGrid.MainView.Columns["Etc"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["SettingQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["SettingQty"].SummaryItem.FieldName = "SettingQty";
            GridExControl.MainGrid.MainView.Columns["SettingQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);

            GridExControl.MainGrid.Columns["MachineCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            GridExControl.MainGrid.Columns["MachineName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MachineMCode");

            GridExControl.MainGrid.Clear();
            GridExControl.MainGrid.Columns.Clear();
            SetGridColumn();

            ModelService.ReLoad();

            var procTeamCode = lup_ProcTeamCode.EditValue.GetNullToEmpty();
            var machineProcessCode = lup_Process.EditValue.GetNullToEmpty();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var date = new SqlParameter("@WorkDate", dt_WorkDate.DateTime);
                var firstDay = new SqlParameter("@_FirstDay", cbo_FirstDay.SelectedIndex + 1);
                var lastDay = new SqlParameter("@_LastDay", cbo_LastDay.SelectedIndex + 1);

                var result = context.Database
                      .SqlQuery<TEMP_MPS1303_LIST>("USP_GET_MPS1303_LIST_NEW @WorkDate, @_FirstDay, @_LastDay", date, firstDay, lastDay)
                      .Where(p=> string.IsNullOrEmpty(procTeamCode) ? true : p.ProcTeamCode == procTeamCode
                                && (string.IsNullOrEmpty(machineProcessCode) ? true : p.MachineProcessCode == machineProcessCode))
                      .ToList();

                GridBindingSource.DataSource = result.OrderBy(p => p.MachineCode).ToList();
            }

            GridExControl.DataSource = GridBindingSource;            
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ProcessRate")
            {
                var value = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ProcessRate").GetDecimalNullToZero().ToString("#,0.##");
                e.DisplayText = value + "%";
            }
        }

    }
}