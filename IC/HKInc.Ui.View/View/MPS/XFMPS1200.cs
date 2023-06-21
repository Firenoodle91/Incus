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
    /// 작업지시관리화면
    /// </summary>
    public partial class XFMPS1200 : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1100> ModelService = (IService<TN_MPS1100>)ProductionFactory.GetDomainService("TN_MPS1100");
        List<Holiday> holidayList;

        /// <summary>
        /// 20210521 오세완 차장
        /// 작업지시현황을 삭제한 경우 공정순번을 초기화 하기 위한 프로시저 실행여부를 판단
        /// </summary>
        private bool bClick_DetailGridDelete = false;
        #endregion
        public XFMPS1200()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;

            OutPutRadioGroup = radioGroup1;
            RadioGroupType = Utils.Enum.RadioGroupType.XFMPS1200;

            dt_PlanMonth.SetFormat(Utils.Enum.DateFormat.Month);
            dt_PlanMonth.DateTime = DateTime.Today;

            dt_WorkDate.DateTime = DateTime.Today;
        }

        protected override void InitCombo()
        {   
            //lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            // 20210521 오세완 차장 여기는 완제품 작업지시만 출력되야 해서 수정
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN )).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("PlanNo", LabelConvert.GetLabelText("PlanNo"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            //MasterGridExControl.MainGrid.AddColumn("PlanQty", LabelConvert.GetLabelText("PlanQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.DelivQty", LabelConvert.GetLabelText("PlanQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1000.OrderDueDate", LabelConvert.GetLabelText("DueDate"));                // 2021-11-04 김진우 주임  납기일 추가
            MasterGridExControl.MainGrid.AddColumn("PlanStartDate", LabelConvert.GetLabelText("PlanStartDate"));
            MasterGridExControl.MainGrid.AddColumn("PlanEndDate", LabelConvert.GetLabelText("PlanEndDate"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));


            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, false);
            DetailGridExControl.MainGrid.AddColumn("JobStates", LabelConvert.GetLabelText("JobStates"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("EmergencyFlag", LabelConvert.GetLabelText("EmergencyFlag"));
            DetailGridExControl.MainGrid.AddColumn("PlanNo", LabelConvert.GetLabelText("PlanNo"), false);
            DetailGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            DetailGridExControl.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"));
            DetailGridExControl.MainGrid.AddColumn("RealWorkDate", LabelConvert.GetLabelText("RealWorkDate"));            
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

            //DetailGridExControl.MainGrid.AddColumn("JobSettingFlag", LabelConvert.GetLabelText("JobSettingFlag"));
            
            //DetailGridExControl.MainGrid.AddColumn("Temp1", LabelConvert.GetLabelText("CustomerLotNo"), false);
            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "RealWorkDate", "Temp1", "EmergencyFlag", "WorkDate", "MachineCode", "WorkQty", "OutProcFlag", "WorkId", "Memo", "ToolUseFlag", "JobSettingFlag", "MachineGroupCode");
            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "RealWorkDate", "EmergencyFlag", "WorkDate", "MachineCode", "WorkQty", "OutProcFlag", "WorkId", "Memo", "RestartToFlag", "MachineGroupCode");
            
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "RealWorkDate", "EmergencyFlag", "WorkDate", "MachineCode", "WorkQty", "OutProcFlag", "WorkId", "Memo", "MachineGroupCode");


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
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("RealWorkDate");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("MachineFlag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("WorkQty", DefaultBoolean.Default, "n0");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProcFlag", "N");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);

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
            
            InitRepository();
            InitCombo();

            string itemCode = lup_Item.EditValue.GetNullToEmpty();
            string workNo = tx_WorkNo.EditValue.GetNullToEmpty();
            var radioValue = OutPutRadioGroup.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                        && (string.IsNullOrEmpty(workNo) ? true : p.TN_MPS1200List.Any(c => c.WorkNo == workNo))
                                                                        && ((p.PlanStartDate.Year == dt_PlanMonth.DateTime.Year && p.PlanStartDate.Month == dt_PlanMonth.DateTime.Month)
                                                                            || (p.PlanEndDate.Year == dt_PlanMonth.DateTime.Year && p.PlanEndDate.Month == dt_PlanMonth.DateTime.Month))
                                                                        && (p.TN_ORD1100.TN_ORD1001.TN_ORD1000.OrderType == "양산")
                                                                     )
                                                                     .Where(p => radioValue == "A" ? true : p.WorkOrderFlag == radioValue)
                                                                     .OrderBy(p => p.PlanNo)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SubLoad();
            // 20210521 오세완 차장 다시 조회를 하게 되면 작업지시현황을 복구했다고 가정하기에 초기화
            bClick_DetailGridDelete = false;
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MPS1100;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            //DetailGridBindingSource.DataSource = masterObj.TN_MPS1200List.OrderBy(p => p.WorkNo).ThenBy(p => p.ProcessSeq).ToList();
            // 20210521 오세완 차장 이 화면은 완제품 작업지시처럼 작동해야 해서 완제품 조건이 필요
            if(masterObj.TN_MPS1200List != null)
            {
                List<TN_MPS1200> tempArr = masterObj.TN_MPS1200List.ToList();
                bool bNull_Std1100 = false;
                foreach (TN_MPS1200 each in tempArr)
                {
                    each.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == each.ItemCode).FirstOrDefault();
                    if (each.TN_STD1100 == null)
                        bNull_Std1100 = true;
                }

                if (!bNull_Std1100)
                {
                    tempArr = tempArr.Where(p => p.TN_STD1100.TopCategory == MasterCodeSTR.TopCategory_WAN &&
                                                 p.TN_STD1100.UseFlag == "Y").OrderBy(o => o.WorkNo).ThenBy(t => t.ProcessSeq).ToList();

                    DetailGridBindingSource.DataSource = tempArr;
                }
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void SubLoad()
        {
            //DataSet ds = DbRequestHandler.GetDataQury("exec USP_GET_XFMPS1200_SUB '" + dt_PlanMonth.DateTime.ToString("yyyyMM") + "'");
            // 20210524 오세완 차장 완제품만 출력하게 하기 위해 프로시저 변경
            DataSet ds = DbRequestHandler.GetDataQury("exec USP_GET_XFMPS1200_SUB_WAN '" + dt_PlanMonth.DateTime.ToString("yyyyMM") + "'");
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
                    else
                    {
                        SubDetailGridExControl.MainGrid.Columns[v.FieldName].DisplayFormat.FormatType = FormatType.Numeric;
                        SubDetailGridExControl.MainGrid.Columns[v.FieldName].DisplayFormat.FormatString = "n0";
                        SubDetailGridExControl.MainGrid.Columns[v.FieldName].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
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

            #region BOM Check
            /*
            var bomObj = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == masterObj.ItemCode && p.MgFlag == "Y").FirstOrDefault();
            if (bomObj == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("BomInfo")));
                return;
            }
            */
            #endregion

            AddProcess(masterObj, masterObj.ItemCode, TN_MPS1000List);

            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void AddProcess(TN_MPS1100 masterObj, string itemCode, List<TN_MPS1000> TN_MPS1000List)
        {
            string WorkNo = DbRequestHandler.GetSeqMonth("WNO");
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
                    WorkQty = masterObj.TN_ORD1100.DelivQty,                // 2021-11-04 김진우 주임 수정             기존 masterObj.PlanQty
                    OutProcFlag = v.OutProcFlag,
                    MachineFlag = v.MachineFlag,
                    //ToolUseFlag = v.ToolUseFlag,
                    //JobSettingFlag = v.JobSettingFlag,
                    //RestartToFlag = v.RestartToFlag, // 20210520 오세완 차장 재가동TO여부로 변경
                    //RestartToFlag = "N", // 20210628 오세완 차장 재가동TO를 작업지시에 설정하지 말라는 이사님 지시로 표준공정에서도 생략처리 할거라 값을 박아주기로.
                    JobStates = MasterCodeSTR.JobStates_Wait,
                    TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itemCode).First(),
                    NewRowFlag = "Y",
                    Memo= masterObj.Memo.GetNullToEmpty()
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
           
            if (detailObj.ProcessSeq == 1 && detailObj.ProcessCode == MasterCodeSTR.Process_Packing && detailObj.JobStates != MasterCodeSTR.JobStates_Wait)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_110));
                return;
            }

            if (detailObj.ProcessCode == MasterCodeSTR.Process_Packing)
            {
                if ( DialogResult.Yes!=MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_124),"",MessageBoxButtons.YesNo))
                return;
            }
            ModelService.RemoveChild<TN_MPS1200>(detailObj);
            masterObj.TN_MPS1200List.Remove(detailObj);

            // 20210521 오세완 차장 공정순번을 화면상에서는 차감이 되나 결국 저장이 되지 않기 때문에 의미가 없어서 삭제 처리
            //foreach(TN_MPS1200 v in masterObj.TN_MPS1200List)
            //{
            //    if(v.WorkNo == detailObj.WorkNo)
            //    {
            //        if(v.ProcessSeq > detailObj.ProcessSeq)
            //        {
            //            v.ProcessSeq = v.ProcessSeq - 1;
            //        }
            //    }
            //}
            bClick_DetailGridDelete = true;

            DetailGridBindingSource.RemoveCurrent();
        }

        /// <summary>
        /// 작업지시서 출력 이벤트
        /// </summary>
        private void BarWorkOrderPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DetailGridBindingSource == null)
                return;

            if (!UserRight.HasEdit)
                return;

            var detailObj = DetailGridBindingSource.Current as TN_MPS1200;
            if (detailObj == null)
                return;

            if (detailObj.TN_MPS1100 == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_83)));
                return;
            }

            try
            {
                var detailList = DetailGridBindingSource.List as List<TN_MPS1200>;                
                WaitHandler.ShowWait();
                var report = new REPORT.XRMPS1200(detailList.Where(p => p.WorkNo == detailObj.WorkNo).OrderBy(p => p.ProcessSeq).First());
                report.CreateDocument();

                report.PrintingSystem.ShowMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                report.ShowPreview();
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex.Message);
            }

            WaitHandler.CloseWait();
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            var DetailData = DetailGridBindingSource.List as List<TN_MPS1200>;

            // 2021-11-04 김진우 주임 추가
            if (DetailData.Any(p => p.WorkQty == 0))
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_175));
                return;
            }

            //if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            //{
            //    var masterList = MasterGridBindingSource.List as List<TN_MPS1100>;
            //    if (masterList.Any(p => p.TN_MPS1200List.Any(c => c.MachineFlag == "Y" && c.MachineCode.IsNullOrEmpty())))
            //    {
            //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_54));
            //        return;
            //    }
            //}
            Check_BanProduct_WorkOrder();
            ModelService.Save();
            Check_DetailGrid_ProcessSeq();
            DataLoad();
        }

        /// <summary>
        /// 20210521 오세완 차장 
        /// 작업지시현황에서 1개 이상의 작업을 삭제하였을때 공정순번을 순차적으로 맞춰주기 위한 프로시저 실행, entity는 key를 바꿀수가 없기 때문
        /// </summary>
        private void Check_DetailGrid_ProcessSeq()
        {
            var vDetailObj = DetailGridBindingSource.Current as TN_MPS1200;
            if (vDetailObj == null)
                return;

            if(bClick_DetailGridDelete)
            {
                string sWorkno = vDetailObj.WorkNo.GetNullToEmpty();
                if (sWorkno != "")
                {
                    string sSql = "EXEC USP_UPD_MPS1200_PROCESS_SEQ '" + sWorkno + "'";
                    DbRequestHandler.GetCellValue(sSql, 0);
                }
            }
        }

        /// <summary>
        /// 20210521 오세완 차장 
        /// BOM구조상 완제품 하위에 반제품이 포함되어 있는 경우는 해당 작업지시를 생성하는 함수
        /// </summary>
        private void Check_BanProduct_WorkOrder()
        {
            var vDetailObj = DetailGridBindingSource.Current as TN_MPS1200;
            if (vDetailObj == null)
                return;

            var vMasterObj = MasterGridBindingSource.Current as TN_MPS1100;
            if (vMasterObj == null)
                return;

            string sItemcode = vDetailObj.ItemCode.GetNullToEmpty();
            if (sItemcode != "")
            {
                // 20210524 오세완 차장 기존에 작업지시를 내린 이력이 있으면 BOM에 구성된 반제품 목록이 출력되지 않는 로직으로 된 프로시저
                //string sSql = "EXEC USP_GET_MPS1200_CHECK_HALF_PRODUCT '" + sItemcode + "', '" + vMasterObj.PlanNo + "'";

                // 20210602 오세완 차장 계획번호 중복이 아니라 완제품 작업지시번호를 기준으로 하는 것으로 변경
                string sSql = "EXEC USP_GET_MPS1200_CHECK_HALF_PRODUCT '" + sItemcode + "', '" + vDetailObj.WorkNo + "'";
                DataTable dt = DbRequestHandler.GetDataTableSelect(sSql);
                if (dt != null)
                    if (dt.Rows.Count > 0)
                    {
                        DateTime dtBan = dt_WorkDate.DateTime;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string sBan_itemcode = dt.Rows[i]["ITEM_CODE"].ToString();
                            decimal dBan_useqty = dt.Rows[i]["USE_QTY"].GetDecimalNullToZero();
                            if (dBan_useqty > 0)
                            {
                                var vMps1100_List = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == sBan_itemcode &&
                                                                                            p.UseFlag == "Y").OrderBy(o => o.ProcessSeq).ToList();
                                string sBan_workno = DbRequestHandler.GetSeqMonth("WNO");
                                bool bMps1000_firstinput = true;
                                foreach (TN_MPS1000 each in vMps1100_List)
                                {
                                    if (!bMps1000_firstinput)
                                        dtBan = dtBan.AddDays(each.StdWorkDay.GetIntNullToZero());

                                    dtBan = CheckHolidayDate(dtBan.AddDays(-1), 1);
                                    TN_MPS1200 newMps = new TN_MPS1200()
                                    {
                                        WorkNo = sBan_workno,
                                        ProcessCode = each.ProcessCode,
                                        ProcessSeq = each.ProcessSeq,
                                        PlanNo = vMasterObj.PlanNo,
                                        ItemCode = sBan_itemcode,
                                        MachineGroupCode = each.MachineGroupCode,
                                        CustomerCode = vMasterObj.CustomerCode,
                                        EmergencyFlag = vDetailObj.EmergencyFlag,
                                        WorkDate = dtBan,
                                        //WorkQty = vMasterObj.PlanQty * dBan_useqty,
                                        WorkQty = vDetailObj.WorkQty * dBan_useqty, // 20210602 오세완 차장 계획수량보다는 완제품 작업지시수량을 받는 것이 더 정확할 듯
                                        OutProcFlag = each.OutProcFlag,
                                        MachineFlag = each.MachineFlag,
                                        //RestartToFlag = each.RestartToFlag,
                                        //RestartToFlag = "N", // 20210628 오세완 차장 재가동TO를 작업지시에 설정하지 말라는 이사님 지시로 생략
                                        JobStates = MasterCodeSTR.JobStates_Wait,
                                        NewRowFlag = "Y",
                                        Memo = vMasterObj.Memo.GetNullToEmpty(),
                                        //Temp2 = "AUTO",  // 20210504 오세완 차장 완제품 작업지시에서 생성한 작업지시라고 하는 구분자 추가
                                        Temp2 = vDetailObj.WorkNo // 20210602 오세완 차장 완제품의 작업지시를 저장하여 관련성을 확보
                                        //ToolUseFlag = "N"
                                    };

                                    vMasterObj.TN_MPS1200List.Add(newMps);
                                    bMps1000_firstinput = false;
                                }
                            }
                        }
                    }
            }
            
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