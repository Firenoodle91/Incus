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
    /// 일일생산일보
    /// </summary>
    public partial class XFMPS1302 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1302> ModelService = (IService<TN_MPS1302>)ProductionFactory.GetDomainService("TN_MPS1302");

        public XFMPS1302()
        {
            InitializeComponent();

            dt_WorkDate.DateTime = DateTime.Today;
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            GridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
        }

        protected override void InitCombo()
        {
            lup_ProcTeamCode.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode));
            lup_Process.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process));
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            GridExControl.MainGrid.AddColumn("Date", LabelConvert.GetLabelText("Date"), false);
            GridExControl.MainGrid.AddColumn("MachineMCode", LabelConvert.GetLabelText("MachineMCode"), false);
            GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("MachineName", LabelConvert.GetLabelText("MachineName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("Customer"));
            GridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("Process"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("ResultQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Price", LabelConvert.GetLabelText("Price"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("PlanStop", LabelConvert.GetLabelText("PlanStop"));
            GridExControl.MainGrid.AddColumn("ItemCodeChange", LabelConvert.GetLabelText("ItemCodeChange"));
            GridExControl.MainGrid.AddColumn("SrcSoldOut", LabelConvert.GetLabelText("SrcSoldOut"));
            GridExControl.MainGrid.AddColumn("ReAdjust", LabelConvert.GetLabelText("ReAdjust"));
            GridExControl.MainGrid.AddColumn("MachineBroken", LabelConvert.GetLabelText("MachineBroken"));
            GridExControl.MainGrid.AddColumn("WorkLack", LabelConvert.GetLabelText("WorkLack"));
            GridExControl.MainGrid.AddColumn("Etc", LabelConvert.GetLabelText("OrderLack"));
            GridExControl.MainGrid.AddColumn("SettingQty", LabelConvert.GetLabelText("SettingQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("ProcessRate", LabelConvert.GetLabelText("ProcessRate"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PlanStop", "ItemCodeChange", "SrcSoldOut", "ReAdjust", "MachineBroken", "WorkLack", "Etc");

            GridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;
            GridExControl.MainGrid.MainView.Columns["OkQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["OkQty"].SummaryItem.FieldName = "OkQty";
            GridExControl.MainGrid.MainView.Columns["OkQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["Price"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["Price"].SummaryItem.FieldName = "Price";
            GridExControl.MainGrid.MainView.Columns["Price"].SummaryItem.DisplayFormat = "{0:#,0.##}";

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

            GridExControl.MainGrid.MainView.Columns["ProcessRate"].SummaryItem.SummaryType = SummaryItemType.Custom;
            GridExControl.MainGrid.MainView.CustomSummaryCalculate += MainView_CustomSummaryCalculate;

            GridExControl.MainGrid.Columns["MachineCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            GridExControl.MainGrid.Columns["MachineName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
        }

        decimal MachineCount = 0;
        decimal DayWorkTimeHour = 0;
        decimal PlanStop = 0;
        decimal ItemCodeChange = 0;
        decimal SrcSoldOut = 0;
        decimal ReAdjust = 0;
        decimal MachineBroken = 0;
        decimal WorkLack = 0;
        decimal Etc = 0;


        private void MainView_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.IsTotalSummary && (e.Item as GridSummaryItem).FieldName == "ProcessRate")
            {
                GridSummaryItem item = e.Item as GridSummaryItem;
                if (item.FieldName == "ProcessRate")
                {
                    switch (e.SummaryProcess)
                    {
                        case CustomSummaryProcess.Start:
                            //MachineCount = 0;
                            DayWorkTimeHour = 0;
                            PlanStop = 0;
                            ItemCodeChange = 0;
                            SrcSoldOut = 0;
                            ReAdjust = 0;
                            MachineBroken = 0;
                            WorkLack = 0;
                            Etc = 0;
                            break;
                        case CustomSummaryProcess.Calculate:
                            DayWorkTimeHour = view.GetListSourceRowCellValue(e.RowHandle, "DayWorkTimeHour").GetDecimalNullToZero();

                            PlanStop += view.GetListSourceRowCellValue(e.RowHandle, "PlanStop").GetDecimalNullToZero();
                            ItemCodeChange += view.GetListSourceRowCellValue(e.RowHandle, "ItemCodeChange").GetDecimalNullToZero();
                            SrcSoldOut += view.GetListSourceRowCellValue(e.RowHandle, "SrcSoldOut").GetDecimalNullToZero();
                            ReAdjust += view.GetListSourceRowCellValue(e.RowHandle, "ReAdjust").GetDecimalNullToZero();
                            MachineBroken += view.GetListSourceRowCellValue(e.RowHandle, "MachineBroken").GetDecimalNullToZero();
                            WorkLack += view.GetListSourceRowCellValue(e.RowHandle, "WorkLack").GetDecimalNullToZero();
                            Etc += view.GetListSourceRowCellValue(e.RowHandle, "Etc").GetDecimalNullToZero();
                            //MachineCount++;
                            break;
                        case CustomSummaryProcess.Finalize:
                            if (MachineCount == 0 || DayWorkTimeHour == 0)
                                e.TotalValue = "0%";
                            else
                                e.TotalValue = ((((MachineCount * DayWorkTimeHour) - (PlanStop + ItemCodeChange + SrcSoldOut + ReAdjust + MachineBroken + WorkLack + Etc)) / (MachineCount * DayWorkTimeHour)) * 100).ToString("#,#.##") + "%";
                            break;
                    }
                }
            }
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("PlanStop", DefaultBoolean.Default, "N1");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("ItemCodeChange", DefaultBoolean.Default, "N1");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("SrcSoldOut", DefaultBoolean.Default, "N1");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("ReAdjust", DefaultBoolean.Default, "N1");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("MachineBroken", DefaultBoolean.Default, "N1");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("WorkLack", DefaultBoolean.Default, "N1");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("Etc", DefaultBoolean.Default, "N1");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemName", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName");
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var procTeamCode = lup_ProcTeamCode.EditValue.GetNullToEmpty();
            var machineProcessCode = lup_Process.EditValue.GetNullToEmpty();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var date = new SqlParameter("@WorkDate", dt_WorkDate.DateTime);

                var result = context.Database
                      .SqlQuery<TEMP_MPS1302_LIST>("USP_GET_MPS1302_LIST @WorkDate", date)
                      .Where(p=> (string.IsNullOrEmpty(procTeamCode) ? true : p.ProcTeamCode == procTeamCode)
                                && (string.IsNullOrEmpty(machineProcessCode) ? true : p.MachineProcessCode == machineProcessCode))
                      .ToList();

                MachineCount = result.Select(p => p.MachineMCode).Distinct().Count();
                GridBindingSource.DataSource = result.OrderBy(p => p.MachineCode).ToList();
            }

            GridExControl.DataSource = GridBindingSource;            
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            if (GridBindingSource != null && GridBindingSource.DataSource != null)
            {
                var list = GridBindingSource.List as List<TEMP_MPS1302_LIST>;
                var editList = list.Where(p => p.EditRowFlag == "Y").ToList();
                if (editList.Count == 0) return;

                //var machineCodeList = editList.Select(p => p.MachineMCode).Distinct().ToList();
                //var date = editList.First().Date;

                foreach (var v in editList)
                {
                    var obj = ModelService.GetList(p => p.Date == v.Date 
                                                    && p.MachineMCode == v.MachineMCode
                                                    && p.WorkNo == v.WorkNo
                                                    && p.ProcessCode == v.ProcessCode
                                                    && p.ProcessSeq == v.ProcessSeq).FirstOrDefault();
                    if (obj == null)
                    {
                        ModelService.Insert(new TN_MPS1302()
                        {
                            Date = v.Date,
                            MachineMCode = v.MachineMCode,
                            WorkNo = v.WorkNo,
                            ProcessCode = v.ProcessCode,
                            ProcessSeq = v.ProcessSeq,
                            PlanStop = v.PlanStop,
                            ItemCodeChange = v.ItemCodeChange,
                            SrcSoldOut = v.SrcSoldOut,
                            ReAdjust = v.ReAdjust,
                            MachineBroken = v.MachineBroken,
                            WorkLack = v.WorkLack,
                            Etc = v.Etc
                        });
                    }
                    else
                    {
                        obj.PlanStop = v.PlanStop;
                        obj.ItemCodeChange = v.ItemCodeChange;
                        obj.SrcSoldOut = v.SrcSoldOut;
                        obj.ReAdjust = v.ReAdjust;
                        obj.MachineBroken = v.MachineBroken;
                        obj.WorkLack = v.WorkLack;
                        obj.Etc = v.Etc;
                        ModelService.Update(obj);
                    }
                }
            }

            ModelService.Save();
            DataLoad();
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_MPS1302_LIST;
            if (obj != null)
                obj.EditRowFlag = "Y";
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