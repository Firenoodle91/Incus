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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Utils.Enum;
using HKInc.Service.Helper;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 개발작업지시관리화면
    /// </summary>
    public partial class XFMPS1200_DEV : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_MPS1100> ModelService = (IService<TN_MPS1100>)ProductionFactory.GetDomainService("TN_MPS1100");
        List<Holiday> holidayList;

        public XFMPS1200_DEV()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            //DetailGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged; ;

            OutPutRadioGroup = radioGroup1;
            RadioGroupType = Utils.Enum.RadioGroupType.XFMPS1200;
        }

        protected override void InitCombo()
        {
            dt_PlanMonth.SetFormat(Utils.Enum.DateFormat.Month);
            dt_PlanMonth.DateTime = DateTime.Today;

            dt_WorkDate.DateTime = DateTime.Today;

            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("WorkOrderFlag", LabelConvert.GetLabelText("WorkOrderFlag"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("PlanNo", LabelConvert.GetLabelText("PlanNo"));
            //MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.TN_ORD1001.TN_ORD1000.OrderType", LabelConvert.GetLabelText("OrderType"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("PlanQty", LabelConvert.GetLabelText("PlanQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            MasterGridExControl.MainGrid.AddColumn("PlanStartDate", LabelConvert.GetLabelText("PlanStartDate"));
            MasterGridExControl.MainGrid.AddColumn("PlanEndDate", LabelConvert.GetLabelText("PlanEndDate"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));


            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, false);
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("CustomPackAdd") + "[Alt+R]", IconImageList.GetIconImage("business%20objects/boreport2"));
            DetailGridExControl.MainGrid.AddColumn("JobStates", LabelConvert.GetLabelText("JobStates"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("EmergencyFlag", LabelConvert.GetLabelText("EmergencyFlag"));
            DetailGridExControl.MainGrid.AddColumn("PlanNo", LabelConvert.GetLabelText("PlanNo"), false);
            DetailGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            DetailGridExControl.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"));
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            DetailGridExControl.MainGrid.AddColumn("MachineGroupCode", LabelConvert.GetLabelText("MachineGroup"));
            DetailGridExControl.MainGrid.AddColumn("MachineFlag", LabelConvert.GetLabelText("MachineFlag"), false);
            DetailGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"));
            DetailGridExControl.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"));
            DetailGridExControl.MainGrid.AddColumn("OutProcFlag", LabelConvert.GetLabelText("OutProcFlag"));
            DetailGridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"), false);
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("ToolUseFlag", LabelConvert.GetLabelText("ToolUseFlag"));
            DetailGridExControl.MainGrid.AddColumn("JobSettingFlag", LabelConvert.GetLabelText("JobSettingFlag"));
            DetailGridExControl.MainGrid.AddColumn("Temp1", LabelConvert.GetLabelText("CustomerLotNo"), false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Temp1", "EmergencyFlag", "WorkDate", "MachineCode", "WorkQty", "OutProcFlag", "WorkId", "Memo", "ToolUseFlag", "JobSettingFlag", "MachineGroupCode");

            
            var barWorkOrderPrint = new DevExpress.XtraBars.BarButtonItem();
            barWorkOrderPrint.Id = 4;
            barWorkOrderPrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/boreport2");
            barWorkOrderPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barWorkOrderPrint.Name = "barPoDocumentPrint";
            barWorkOrderPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barWorkOrderPrint.ShortcutKeyDisplayString = "Alt+P";
            barWorkOrderPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barWorkOrderPrint.Caption = LabelConvert.GetLabelText("WorkOrderPrint") + "[Alt+P]";
            barWorkOrderPrint.ItemClick += BarWorkOrderPrint_ItemClick;
            barWorkOrderPrint.Enabled = UserRight.HasEdit;
            barWorkOrderPrint.Alignment = BarItemLinkAlignment.Right;
            DetailGridExControl.BarTools.AddItem(barWorkOrderPrint);

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MPS1200>(DetailGridExControl);

            SubDetailGridExControl.SetToolbarVisible(false);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStates", DbRequestHandler.GetCommTopCode(MasterCodeSTR.JobStates), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("EmergencyFlag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("MachineFlag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("WorkQty", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProcFlag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("ToolUseFlag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("JobSettingFlag", "N");
            

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();

            var machineEdit = DetailGridExControl.MainGrid.Columns["MachineCode"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            if(machineEdit != null)
                machineEdit.Popup += MachineEdit_Popup;
        }

        protected override void DataLoad()
        {
            if (!IsFirstLoaded)
            {
                holidayList = ModelService.GetChildList<Holiday>(p => true).ToList();
            }

            GridRowLocator.GetCurrentRow("PlanNo");
            
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Columns.Clear();

            ModelService.ReLoad();

            string itemCode = lup_Item.EditValue.GetNullToEmpty();
            string workNo = tx_WorkNo.EditValue.GetNullToEmpty();
            var radioValue = OutPutRadioGroup.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                        && (string.IsNullOrEmpty(workNo) ? true : p.TN_MPS1200List.Any(c => c.WorkNo == workNo))
                                                                        && ((p.PlanStartDate.Year == dt_PlanMonth.DateTime.Year && p.PlanStartDate.Month == dt_PlanMonth.DateTime.Month)
                                                                            || (p.PlanEndDate.Year == dt_PlanMonth.DateTime.Year && p.PlanEndDate.Month == dt_PlanMonth.DateTime.Month))
                                                                        && (p.TN_ORD1100.TN_ORD1001.TN_ORD1000.OrderType == "개발")
                                                                     )
                                                                     .Where(p => radioValue == "A" ? true : p.WorkOrderFlag == radioValue)
                                                                     .OrderBy(p => p.PlanNo)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SubLoad();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1100;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_MPS1200List.OrderBy(p => p.WorkNo).ThenBy(p => p.ProcessSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }        

        private void SubLoad()
        {
            DataSet ds = DbRequestHandler.GetDataQury("exec USP_GET_XFMPS1200_DEV_SUB '" + dt_PlanMonth.DateTime.ToString("yyyyMM") + "'");
            if (ds != null)
            {
                if (SubDetailGridExControl.MainGrid.Columns.Count > 0)
                    SubDetailGridExControl.MainGrid.Columns.Clear();

                SubDetailGridBindingSource.DataSource = ds.Tables[0];
                SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
                
                foreach (var v in SubDetailGridExControl.MainGrid.Columns.ToList())
                {
                    v.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    if (v.FieldName == "ITEM_CODE")
                    {
                        v.Caption = LabelConvert.GetLabelText("ItemCode");
                    }
                    else if (v.FieldName == "ITEM_NAME" || v.FieldName == "ITEM_NAME_ENG" || v.FieldName == "ITEM_NAME_CHN")
                    {
                        v.Caption = LabelConvert.GetLabelText("ItemName");
                        if (DataConvert.GetCultureIndex() == 1)
                        {
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME"].OptionsColumn.AllowShowHide = true;
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_ENG"].OptionsColumn.AllowShowHide = false;
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_CHN"].OptionsColumn.AllowShowHide = false;

                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME"].Visible = true;
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_ENG"].Visible = false;
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_CHN"].Visible = false;
                        }
                        else if (DataConvert.GetCultureIndex() == 2)
                        {
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME"].OptionsColumn.AllowShowHide = false;
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_ENG"].OptionsColumn.AllowShowHide = true;
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_CHN"].OptionsColumn.AllowShowHide = false;

                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME"].Visible = false;
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_ENG"].Visible = true;
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_CHN"].Visible = false;
                        }
                        else
                        {
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME"].OptionsColumn.AllowShowHide = false;
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_ENG"].OptionsColumn.AllowShowHide = false;
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_CHN"].OptionsColumn.AllowShowHide = true;

                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME"].Visible = false;
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_ENG"].Visible = false;
                            SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_CHN"].Visible = true;
                        }
                    }
                    else if (v.FieldName == "MACHINE_CODE")
                    {
                        v.Caption = LabelConvert.GetLabelText("MachineCode");
                        v.Visible = false;
                    }
                    else if (v.FieldName == "MACHINE_NAME" || v.FieldName == "MACHINE_NAME_ENG" || v.FieldName == "MACHINE_NAME_CHN")
                    {
                        v.Caption = LabelConvert.GetLabelText("MachineName");
                        if (DataConvert.GetCultureIndex() == 1)
                        {
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME"].OptionsColumn.AllowShowHide = true;
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_ENG"].OptionsColumn.AllowShowHide = false;
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_CHN"].OptionsColumn.AllowShowHide = false;

                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME"].Visible = true;
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_ENG"].Visible = false;
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_CHN"].Visible = false;
                        }
                        else if (DataConvert.GetCultureIndex() == 2)
                        {
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME"].OptionsColumn.AllowShowHide = false;
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_ENG"].OptionsColumn.AllowShowHide = true;
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_CHN"].OptionsColumn.AllowShowHide = false;

                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME"].Visible = false;
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_ENG"].Visible = true;
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_CHN"].Visible = false;
                        }
                        else
                        {
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME"].OptionsColumn.AllowShowHide = false;
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_ENG"].OptionsColumn.AllowShowHide = false;
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_CHN"].OptionsColumn.AllowShowHide = true;

                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME"].Visible = false;
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_ENG"].Visible = false;
                            SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_CHN"].Visible = true;
                        }
                    }
                    else if (v.FieldName == "PROCESS_CODE")
                    {
                        v.Caption = LabelConvert.GetLabelText("ProcessCode");
                    }
                    else if (v.FieldName == "PROCESS_NAME" || v.FieldName == "PROCESS_NAME_ENG" || v.FieldName == "PROCESS_NAME_CHN")
                    {
                        v.Caption = LabelConvert.GetLabelText("ProcessName");
                        if (DataConvert.GetCultureIndex() == 1)
                        {
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME"].OptionsColumn.AllowShowHide = true;
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_ENG"].OptionsColumn.AllowShowHide = false;
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_CHN"].OptionsColumn.AllowShowHide = false;

                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME"].Visible = true;
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_ENG"].Visible = false;
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_CHN"].Visible = false;
                        }
                        else if (DataConvert.GetCultureIndex() == 2)
                        {
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME"].OptionsColumn.AllowShowHide = false;
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_ENG"].OptionsColumn.AllowShowHide = true;
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_CHN"].OptionsColumn.AllowShowHide = false;

                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME"].Visible = false;
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_ENG"].Visible = true;
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_CHN"].Visible = false;
                        }
                        else
                        {
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME"].OptionsColumn.AllowShowHide = false;
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_ENG"].OptionsColumn.AllowShowHide = false;
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_CHN"].OptionsColumn.AllowShowHide = true;

                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME"].Visible = false;
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_ENG"].Visible = false;
                            SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_CHN"].Visible = true;
                        }
                    }
                }
                SubDetailGridExControl.MainGrid.BestFitColumns();
            }
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1100;
            if (masterObj == null) return;

            var TN_MPS1000List = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == masterObj.ItemCode && p.UseFlag == "Y").OrderBy(o => o.ProcessSeq).ToList();
            if (TN_MPS1000List.Count == 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemProcess")));
                return;
            }

            #region 반제품 Process Check
            var wanBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == masterObj.ItemCode).FirstOrDefault();
            if (wanBomObj != null)
            {
                var banBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ParentBomCode == wanBomObj.BomCode).FirstOrDefault();
                if (banBomObj != null)
                {
                    var TN_MPS1000List_BAN = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == banBomObj.ItemCode && p.UseFlag == "Y").OrderBy(o => o.ProcessSeq).ToList();
                    if (TN_MPS1000List_BAN.Count == 0)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_78)));
                        return;
                    }

                    AddBanProcess(masterObj, banBomObj.ItemCode, TN_MPS1000List_BAN, TN_MPS1000List);
                    DetailGridExControl.BestFitColumns();
                    return;
                }
            }
            #endregion

            AddProcess(masterObj, masterObj.ItemCode, TN_MPS1000List);

            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void AddProcess(TN_MPS1100 masterObj, string itemCode, List<TN_MPS1000> TN_MPS1000List)
        {
            string WorkNo = DbRequestHandler.GetSeqMonth("WNO_D");
            DateTime dt = dt_WorkDate.DateTime;

            bool FirstInsertFlag = true;
            foreach (var v in TN_MPS1000List)
            {
                if (!FirstInsertFlag)
                    dt = dt.AddDays(v.StdWorkDay.GetIntNullToZero());

                dt = CheckHolidayDate(dt.AddDays(-1), 1);

                var newObj = new TN_MPS1200()
                {
                    WorkNo = WorkNo,
                    ProcessCode = v.ProcessCode,
                    ProcessSeq = v.ProcessSeq,
                    PlanNo = masterObj.PlanNo,
                    ItemCode = itemCode,
                    MachineGroupCode = v.MachineGroupCode,
                    CustomerCode = masterObj.CustomerCode,
                    EmergencyFlag = "N",
                    WorkDate = dt,
                    WorkQty = masterObj.PlanQty,
                    OutProcFlag = v.OutProcFlag,
                    MachineFlag = v.MachineFlag,
                    ToolUseFlag = v.ToolUseFlag,
                    JobSettingFlag = v.JobSettingFlag,
                    JobStates = MasterCodeSTR.JobStates_Wait,
                    TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itemCode).First(),
                    NewRowFlag = "Y"
                };
                DetailGridBindingSource.Add(newObj);
                masterObj.TN_MPS1200List.Add(newObj);
                FirstInsertFlag = false;
            }
        }

        private void AddBanProcess(TN_MPS1100 masterObj, string itemCode, List<TN_MPS1000> TN_MPS1000List_BAN, List<TN_MPS1000> TN_MPS1000List)
        {
            string WorkNo = DbRequestHandler.GetSeqMonth("WNO_D");
            DateTime dt = dt_WorkDate.DateTime;

            bool FirstInsertFlag = true;
            foreach (var v in TN_MPS1000List_BAN)
            {
                if (!FirstInsertFlag)
                    dt = dt.AddDays(v.StdWorkDay.GetIntNullToZero());

                dt = CheckHolidayDate(dt.AddDays(-1), 1);

                var newObj = new TN_MPS1200()
                {
                    WorkNo = WorkNo,
                    ProcessCode = v.ProcessCode,
                    ProcessSeq = v.ProcessSeq,
                    PlanNo = masterObj.PlanNo,
                    ItemCode = itemCode,
                    MachineGroupCode = v.MachineGroupCode,
                    CustomerCode = masterObj.CustomerCode,
                    EmergencyFlag = "N",
                    WorkDate = dt,
                    WorkQty = masterObj.PlanQty,
                    OutProcFlag = v.OutProcFlag,
                    MachineFlag = v.MachineFlag,
                    ToolUseFlag = v.ToolUseFlag,
                    JobSettingFlag = v.JobSettingFlag,
                    JobStates = MasterCodeSTR.JobStates_Wait,
                    TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itemCode).First(),
                    NewRowFlag = "Y"
                };
                DetailGridBindingSource.Add(newObj);
                masterObj.TN_MPS1200List.Add(newObj);
                FirstInsertFlag = false;
            }

            var banWorkNo = WorkNo;
            WorkNo = DbRequestHandler.GetSeqMonth("WNO_D");
            dt = dt_WorkDate.DateTime;
            FirstInsertFlag = true;

            foreach (var v in TN_MPS1000List)
            {
                if (!FirstInsertFlag)
                    dt = dt.AddDays(v.StdWorkDay.GetIntNullToZero());

                dt = CheckHolidayDate(dt.AddDays(-1), 1);

                var newObj = new TN_MPS1200()
                {
                    WorkNo = WorkNo,
                    ProcessCode = v.ProcessCode,
                    ProcessSeq = v.ProcessSeq,
                    PlanNo = masterObj.PlanNo,
                    ItemCode = masterObj.ItemCode,
                    MachineGroupCode = v.MachineGroupCode,
                    CustomerCode = masterObj.CustomerCode,
                    EmergencyFlag = "N",
                    WorkDate = dt,
                    WorkQty = masterObj.PlanQty,
                    OutProcFlag = v.OutProcFlag,
                    MachineFlag = v.MachineFlag,
                    ToolUseFlag = v.ToolUseFlag,
                    JobSettingFlag = v.JobSettingFlag,
                    JobStates = MasterCodeSTR.JobStates_Wait,
                    TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == masterObj.ItemCode).First(),
                    NewRowFlag = "Y",
                    Temp = banWorkNo,
                };
                DetailGridBindingSource.Add(newObj);
                masterObj.TN_MPS1200List.Add(newObj);
                FirstInsertFlag = false;
            }
        }
        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1100;
            if (masterObj == null) return;

            var detailList = DetailGridBindingSource.List as List<TN_MPS1200>;
            var detailObj = DetailGridBindingSource.Current as TN_MPS1200;
            if (masterObj == null || detailObj == null || detailList == null) return;

            if (detailObj.ProcessSeq != 1 || detailObj.ProcessCode != MasterCodeSTR.Process_Packing)
            {
                MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_113));
                //MessageBoxHandler.Show("임의 포장 작업지시만 삭제가 가능합니다. 확인 부탁드립니다.");
                return;
                //MessageBoxHandler.Show(string.Format("작업지시번호 : {0}는 생산이 이미 진행된 공정이 있어 삭제가 불가합니다.", currentObj.WorkNo), "경고");
                //return;
            }

            if (detailObj.ProcessSeq == 1 && detailObj.ProcessCode == MasterCodeSTR.Process_Packing && detailObj.JobStates != MasterCodeSTR.JobStates_Wait)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_110));
                return;
                //MessageBoxHandler.Show(string.Format("작업지시번호 : {0}는 생산이 이미 진행된 공정이 있어 삭제가 불가합니다.", currentObj.WorkNo), "경고");
                //return;
            }

            ModelService.RemoveChild(detailObj);
            //masterObj.TN_MPS1200List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();

            //if (masterObj.TN_MPS1200List.Any(p => p.WorkNo == detailObj.WorkNo && p.JobStates != MasterCodeSTR.JobStates_Wait))
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_53), detailObj.WorkNo));
            //    return;
            //    //MessageBoxHandler.Show(string.Format("작업지시번호 : {0}는 생산이 이미 진행된 공정이 있어 삭제가 불가합니다.", currentObj.WorkNo), "경고");
            //    //return;
            //}

            //var removeList = detailList.Where(p => p.WorkNo == detailObj.WorkNo).ToList();

            //foreach(var v in removeList)
            //{
            //    if (v.TN_MPS1100 != null)
            //        ModelService.RemoveChild(v);
            //    else
            //        masterObj.TN_MPS1200List.Remove(v);
            //    DetailGridBindingSource.Remove(v);
            //}            
        }

        /// <summary>
        /// 임의포장추가
        /// </summary>
        protected override void DetailFileChooseClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1100;
            if (masterObj == null) return;

            string WorkNo = DbRequestHandler.GetSeqMonth("WNO");

            DateTime dt = dt_WorkDate.DateTime;
            dt = CheckHolidayDate(dt.AddDays(-1), 1);

            var newObj = new TN_MPS1200()
            {
                WorkNo = WorkNo,
                ProcessCode = MasterCodeSTR.Process_Packing,
                ProcessSeq = 1,
                PlanNo = masterObj.PlanNo,
                ItemCode = masterObj.ItemCode,
                MachineGroupCode = null,
                CustomerCode = masterObj.CustomerCode,
                EmergencyFlag = "N",
                WorkDate = dt,
                WorkQty = masterObj.PlanQty,
                OutProcFlag = "N",
                MachineFlag = "N",
                ToolUseFlag = "N",
                JobSettingFlag = "N",
                JobStates = MasterCodeSTR.JobStates_Wait,
                TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == masterObj.ItemCode).First(),
                NewRowFlag = "Y"
            };

            DetailGridBindingSource.Add(newObj);
            masterObj.TN_MPS1200List.Add(newObj);

            DetailGridExControl.BestFitColumns();
        }

        /// <summary>
        /// 작업지시서 출력 이벤트
        /// </summary>
        private void BarWorkOrderPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DetailGridBindingSource == null) return;
            if (!UserRight.HasEdit) return;
            var detailObj = DetailGridBindingSource.Current as TN_MPS1200;
            if (detailObj == null) return;
            if (detailObj.TN_MPS1100 == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_83)));
                return;
            }
            try
            {
                var detailList = DetailGridBindingSource.List as List<TN_MPS1200>;                
                WaitHandler.ShowWait();
                var report = new REPORT.XRMPS1200_DEV(detailList.Where(p => p.WorkNo == detailObj.WorkNo).OrderBy(p => p.ProcessSeq).First());
                report.CreateDocument();

                report.PrintingSystem.ShowMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                report.ShowPreview();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            WaitHandler.CloseWait();
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            //if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            //{
            //    var masterList = MasterGridBindingSource.List as List<TN_MPS1100>;
            //    if (masterList.Any(p => p.TN_MPS1200List.Any(c => c.MachineFlag == "Y" && c.MachineCode.IsNullOrEmpty())))
            //    {
            //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_54));
            //        return;
            //    }
            //}

            ModelService.Save();
            DataLoad();
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Memo" && view.FocusedColumn.FieldName != "Temp1")
            {
                if (view.GetFocusedRowCellValue("JobStates").ToString() != MasterCodeSTR.JobStates_Wait)
                    e.Cancel = true;
            }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.Column.FieldName == "EmergencyFlag")
            {
                var obj = DetailGridBindingSource.Current as TN_MPS1200;
                if (obj != null)
                {
                    var checkValue = e.Value.GetNullToEmpty();
                    if (checkValue == "Y")
                    {
                        var detailList = DetailGridBindingSource.List as List<TN_MPS1200>;
                        var sameWorkNoList = detailList.Where(p => p.WorkNo == obj.WorkNo).ToList();
                        if (sameWorkNoList.Count > 1)
                        {
                            foreach (var v in sameWorkNoList)
                                v.EmergencyFlag = "Y";
                            DetailGridExControl.BestFitColumns();
                        }
                    }
                }
            }
            else if (e.Column.FieldName == "MachineGroupCode")
            {
                var detailObj = DetailGridBindingSource.Current as TN_MPS1200;
                if (detailObj == null) return;

                if (e.Value.IsNullOrEmpty())
                    detailObj.MachineFlag = "N";
                else
                    detailObj.MachineFlag = "Y";
            }
            else if (e.Column.FieldName == "Temp1")
            {
                var detailObj = DetailGridBindingSource.Current as TN_MPS1200;
                if (detailObj == null) return;

                var detailList = DetailGridBindingSource.List as List<TN_MPS1200>;
                var sameWorkNoList = detailList.Where(p => p.WorkNo == detailObj.WorkNo).ToList();
                if (sameWorkNoList.Count > 1)
                {
                    foreach (var v in sameWorkNoList)
                        v.Temp1 = e.Value.GetNullToNull();
                    DetailGridExControl.BestFitColumns();
                }
            }
        }

        //private void MainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        //{
        //    GridView View = sender as GridView;
        //    if (e.RowHandle >= 0)
        //    {
        //        if (e.Column.FieldName == "MachineCode")
        //        {
        //            object machineFlag = View.GetRowCellValue(e.RowHandle, View.Columns["MachineFlag"]);
        //            object machineValue = View.GetRowCellValue(e.RowHandle, View.Columns["MachineCode"]);
        //            if (machineFlag.GetNullToEmpty() == "Y" && machineValue.IsNullOrEmpty())
        //            {
        //                e.Appearance.BackColor = Color.Red;
        //                e.Appearance.ForeColor = Color.White;
        //            }
        //        }
        //    }
        //}

        private void MachineEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_MPS1200;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            if (detailObj.MachineGroupCode.IsNullOrEmpty())
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            else
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[MachineGroupCode] = '" + detailObj.MachineGroupCode + "'";
        }

        /// <summary>
        /// 휴일체크 재귀함수
        /// </summary>
        private DateTime CheckHolidayDate(DateTime date, int changeQty)
        {
            var addDate = date.AddDays(changeQty);

            if (changeQty > 0)
            {
                if (holidayList.Any(p => (p.HolidayFlag == "Y" && p.OvertimeFlag == "N") && p.Date == addDate))
                    return CheckHolidayDate(addDate, 1);
                else
                    return addDate;
            }
            else if (changeQty < 0)
            {
                if (holidayList.Any(p => (p.HolidayFlag == "Y" && p.OvertimeFlag == "N") && p.Date == addDate))
                    return CheckHolidayDate(addDate, -1);
                else
                    return addDate;
            }
            else
            {
                return addDate;
            }
        }
    }
}