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
using HKInc.Utils.Common;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Controls;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 20210525 오세완 차장
    /// 반제품 작업지시관리화면
    /// </summary>
    public partial class XFMPS1210 : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1200> ModelService = (IService<TN_MPS1200>)ProductionFactory.GetDomainService("TN_MPS1200");
        List<Holiday> holidayList;

        /// <summary>
        /// 20210521 오세완 차장
        /// 작업지시현황을 삭제한 경우 공정순번을 초기화 하기 위한 프로시저 실행여부를 판단
        /// </summary>
        private bool bClick_DetailGridDelete = false;
        #endregion
        public XFMPS1210()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged; ;
            dt_WorkDate.EditValueChanged += Dt_WorkDate_EditValueChanged;
            lup_WanItem.EditValueChanged += Lup_WanItem_EditValueChanged;

            OutPutRadioGroup = radioGroup1;
            RadioGroupType = Utils.Enum.RadioGroupType.XFMPS1200;

            dt_PlanMonth.SetFormat(Utils.Enum.DateFormat.Month);
            dt_PlanMonth.DateTime = DateTime.Today;
            dt_WorkDate.DateTime = DateTime.Today;
        }

        /// <summary>
        /// 20220106 오세완 차장
        /// 완제품 선택시 반제풍을 선택할 수 있게 event 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lup_WanItem_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEditEx slue = sender as SearchLookUpEditEx;
            if (slue == null)
                return;

            bool bInit = false;
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", slue.EditValue.GetNullToEmpty());

                var vResult = context.Database.SqlQuery<TEMP_XFMPS1210_BAN_PRODUCT>("USP_GET_XFMPS1210_BAN_PRODUCT @ITEM_CODE", sp_Itemcode).ToList();
                if (vResult != null)
                    if (vResult.Count > 0)
                    {
                        lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), vResult);
                        bInit = true;
                    }
            }

            if (bInit)
                lup_Item.Enabled = true;
            else
            {
                lup_Item.EditValue = "";
                lup_Item.DataSource = null;
                lup_Item.Enabled = false;
            }
                
        }

        /// <summary>
        /// 20210607 오세완 차장 
        /// 작업지시일을 변경하면 재조회 할 수 있게 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dt_WorkDate_EditValueChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            MasterFocusedRowChanged();
        }

        protected override void InitCombo()
        { 
            // 20210526 오세완 차장 searchlookup은 useyn을 넣지 않아도 자동으로 y로 조회된다. 
            //lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p=> p.TopCategory == MasterCodeSTR.TopCategory_BAN ).ToList()); // 20220106 오세완 차장 완제품에 결정형태로 변경
            lup_WanItem.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.TopCategory_WAN).ToList()); // 20220106 오세완 차장 완제품을 먼저 선택할 수 있는 구조로 잡기 위해 설정 

            if (lup_Item.DataSource == null)
                lup_Item.Enabled = false;
            else
            {
                List<TEMP_XFMPS1210_BAN_PRODUCT> tempList = lup_Item.DataSource as List<TEMP_XFMPS1210_BAN_PRODUCT>;
                if (tempList == null)
                    lup_Item.Enabled = false;
                else if (tempList.Count == 0)
                    lup_Item.Enabled = false;
                else
                    lup_Item.Enabled = true;
            }
            
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("TOP_CATEGORY_NAME", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("ITEM_CODE", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ITEM_NAME"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("ITEM_NAME1", LabelConvert.GetLabelText("ItemName1"));
            MasterGridExControl.MainGrid.AddColumn("SAFE_QTY", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");

            MasterGridExControl.MainGrid.AddColumn("SUM_STOCK_QTY", LabelConvert.GetLabelText("SumStockQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");


            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, false);
            DetailGridExControl.MainGrid.AddColumn("JobStates", LabelConvert.GetLabelText("JobStates"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("EmergencyFlag", LabelConvert.GetLabelText("EmergencyFlag"));
            DetailGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            DetailGridExControl.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"));
            DetailGridExControl.MainGrid.AddColumn("RealWorkDate", LabelConvert.GetLabelText("RealWorkDate"));

            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            DetailGridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            DetailGridExControl.MainGrid.AddColumn("MachineGroupCode", LabelConvert.GetLabelText("MachineGroup"));

            DetailGridExControl.MainGrid.AddColumn("MachineFlag", LabelConvert.GetLabelText("MachineFlag"), false);
            DetailGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"));
            DetailGridExControl.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"));
            DetailGridExControl.MainGrid.AddColumn("OutProcFlag", LabelConvert.GetLabelText("OutProcFlag"));
            DetailGridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"), false);

            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            // 20210628 오세완 차장 재가동TO를 작업지시에 설정하지 말라는 이사님 지시로 생략
            //DetailGridExControl.MainGrid.AddColumn("RestartToFlag", LabelConvert.GetLabelText("RestartToFlag"));

            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "RealWorkDate", "EmergencyFlag", "WorkDate", "MachineCode", "WorkQty", "OutProcFlag", "WorkId", "Memo", "RestartToFlag", "MachineGroupCode");
            // 20210628 오세완 차장 재가동TO를 작업지시에 설정하지 말라는 이사님 지시로 생략
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
            // 20210628 오세완 차장 재가동TO를 작업지시에 설정하지 말라는 이사님 지시로 생략
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("RestartToFlag", "N");

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
            // 20210624 오세완 차장 김이사님 지시로 공통코드 추가시 화면 재출력 없이 하기 위해서 추가
            InitRepository();
            InitCombo();


            string itemCode = lup_Item.EditValue.GetNullToEmpty();
            if (itemCode == "")
                return; // 20220106 오세완 차장 완제품이 선택되어야 bom출력이 명확해져서 해당 로직 추가 처리

            var radioValue = OutPutRadioGroup.EditValue.GetNullToEmpty();

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Item = new SqlParameter("@ITEM_CODE", itemCode);
                var vResult = context.Database.SqlQuery<TEMP_MPS1210_BAN_PRODUCT_STOCK_PLAN_V2>("USP_GET_MPS1210_BAN_PRODUCT_STOCK_PLAN_V2 @ITEM_CODE", sp_Item).ToList();
                if (vResult != null)
                    MasterGridBindingSource.DataSource = vResult;
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SubLoad();
            // 20210521 오세완 차장 다시 조회를 하게 되면 작업지시현황을 복구했다고 가정하기에 초기화
            bClick_DetailGridDelete = false;
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TEMP_MPS1210_BAN_PRODUCT_STOCK_PLAN_V2;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            List<TN_MPS1200> totalArr = new List<TN_MPS1200>();

            List<TN_MPS1200> tempArr = ModelService.GetList(p => p.ItemCode == masterObj.ITEM_CODE &&
                                                                 p.WorkDate == dt_WorkDate.DateTime &&
                                                                 p.ProcessSeq == 1); // 20210618 오세완 차장 공정 작업일이 1일이 넘는 건들이 조회가 안되는 경우가 있어서 1공정의 작업지시 번호를 토대로 조회하기로 함
            if(tempArr != null)
                if(tempArr.Count > 0)
                {
                    //TN_MPS1200 temp_each = tempArr.FirstOrDefault();
                    //tempArr = ModelService.GetList(p => p.WorkNo == temp_each.WorkNo);

                    // 20210625 오세완 차장 동일날짜에 1개 이상 작업지시가 있는 경우가 있어서 수정
                    foreach(TN_MPS1200 each in tempArr)
                    {
                        List<TN_MPS1200> each_total = ModelService.GetList(p => p.WorkNo == each.WorkNo);
                        totalArr.AddRange(each_total);
                    }
                }

            bool bNull_Std1100 = false;
            //foreach (TN_MPS1200 each in tempArr)
            foreach (TN_MPS1200 each in totalArr)
            {
                each.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == each.ItemCode).FirstOrDefault();
                if (each.TN_STD1100 == null)
                    bNull_Std1100 = true;
            }

            if (!bNull_Std1100)
            {
                //tempArr = tempArr.Where(p => p.TN_STD1100.TopCategory == MasterCodeSTR.TopCategory_BAN &&
                //                             p.TN_STD1100.UseFlag == "Y").OrderBy(o => o.ProcessSeq).ToList();

                //DetailGridBindingSource.DataSource = tempArr;

                totalArr = totalArr.Where(p => p.TN_STD1100.TopCategory == MasterCodeSTR.TopCategory_BAN &&
                                               p.TN_STD1100.UseFlag == "Y").OrderBy(o => o.WorkNo).ThenBy(t => t.ProcessSeq).ToList();
                DetailGridBindingSource.DataSource = totalArr;
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }        

        private void SubLoad()
        {
            DataSet ds = DbRequestHandler.GetDataQury("exec USP_GET_XFMPS1200_SUB_BAN '" + dt_PlanMonth.DateTime.ToString("yyyyMM") + "'");
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
                    else if (v.FieldName == "ITEM_NAME1")
                    {
                        // 20210824 오세완 차장 품목명 컬럼 추가
                        v.Caption = LabelConvert.GetLabelText("ItemName1");
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
            var masterObj = MasterGridBindingSource.Current as TEMP_MPS1210_BAN_PRODUCT_STOCK_PLAN_V2;
            if (masterObj == null) return;

            var TN_MPS1000List = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == masterObj.ITEM_CODE && p.UseFlag == "Y").OrderBy(o => o.ProcessSeq).ToList();
            if (TN_MPS1000List.Count == 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemProcess")));
                return;
            }

            // 20220106 오세완 차장 완제품을 선택해야 정확한 bom을 출력할 수 있어서 로직 추가 
            string sWan_Itemcode = lup_WanItem.EditValue.GetNullToEmpty();
            if(sWan_Itemcode == "")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_84), LabelConvert.GetLabelText("Prod")));
                return;
            }

            AddProcess(masterObj.ITEM_CODE, TN_MPS1000List);

            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void AddProcess(string itemCode, List<TN_MPS1000> TN_MPS1000List)
        {
            string WorkNo = DbRequestHandler.GetSeqMonth("WNO");
            DateTime dt = dt_WorkDate.DateTime;

            bool FirstInsertFlag = true;
            foreach (var v in TN_MPS1000List)
            {
                if (!FirstInsertFlag)
                    dt = dt.AddDays(v.StdWorkDay.GetIntNullToZero());

                dt = CheckHolidayDate(dt.AddDays(-1), 1);
                string sWan_Itemcode = lup_WanItem.EditValue.GetNullToEmpty();

                var newObj = new TN_MPS1200()
                {
                    WorkNo = WorkNo,
                    ProcessCode = v.ProcessCode,
                    ProcessSeq = v.ProcessSeq,
                    PlanNo = "MANUAL", // 20210527 오세완 차장 entity에서 필수라 쓰레기값 넣기
                    ItemCode = itemCode,
                    MachineGroupCode = v.MachineGroupCode,
                    CustomerCode = "NOTHING", // 20210527 오세완 차장 entity에서 필수라 쓰레기값 넣기
                    EmergencyFlag = "N",
                    WorkDate = dt,
                    WorkQty = 0,
                    OutProcFlag = v.OutProcFlag,
                    MachineFlag = v.MachineFlag,
                    ToolUseFlag = v.ToolUseFlag,
                    //RestartToFlag = v.RestartToFlag, // 20210520 오세완 차장 재가동TO여부로 변경
                    RestartToFlag = "N", // 20210628 오세완 차장 재가동TO를 작업지시에 설정하지 말라는 이사님 지시로 표준공정에서도 생략처리 할거라 값을 박아주기로.
                    JobStates = MasterCodeSTR.JobStates_Wait,
                    TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itemCode).First(),
                    NewRowFlag = "Y",
                    Temp2 = "MANUAL," + sWan_Itemcode // 20210527 오세완차장 완제품 작업지시에서는 auto처리해서 이부분으로 차이를 준다. 20220106 오세완 차장 완제품 품목코드 기록
                };
                DetailGridBindingSource.Add(newObj);
                ModelService.Insert(newObj);
                FirstInsertFlag = false;
            }
        }

        protected override void DeleteDetailRow()
        {
            var detailList = DetailGridBindingSource.List as List<TN_MPS1200>;
            var detailObj = DetailGridBindingSource.Current as TN_MPS1200;
            if (detailObj == null || detailList == null) return;
           
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
            ModelService.Delete(detailObj);

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

            try
            {
                // 20210607 오세완 차장 수동으로 만든 반제품 작업지시 출력은 오류가 발생하여 수정
                var detailList = DetailGridBindingSource.List as List<TN_MPS1200>;
                TN_MPS1200 ban_mps1200 = detailList.Where(p => p.WorkNo == detailObj.WorkNo).OrderBy(p => p.ProcessSeq).First();
                if (ban_mps1200 == null)
                    return;
                else
                {
                    if(ban_mps1200.CustomerCode != null)
                    {
                        if (ban_mps1200.PlanNo.ToUpper() != "MANUAL")
                        {
                            List<TN_STD1400> tempArr = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == ban_mps1200.CustomerCode &&
                                                                                                  p.UseFlag == "Y");
                            if(tempArr != null)
                                if(tempArr.Count > 0)
                                {
                                    ban_mps1200.TN_STD1400 = tempArr.FirstOrDefault();
                                }
                        }
                    }
                }

                WaitHandler.ShowWait();
                //var report = new REPORT.XRMPS1200(detailList.Where(p => p.WorkNo == detailObj.WorkNo).OrderBy(p => p.ProcessSeq).First());
                var report = new REPORT.XRMPS1200(ban_mps1200);
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

            // 20210625 오세완 차장 수량이 0이 입력될 때가 있어서 방지처리
            bool bCheckZero = false;
            List<TN_MPS1200> detail_Arr = DetailGridBindingSource.List as List<TN_MPS1200>;
            if(detail_Arr != null)
                if(detail_Arr.Count > 0)
                {
                    foreach (TN_MPS1200 each in detail_Arr)
                    {
                        if(each.WorkQty <= 0)
                        {
                            string sMessage = "작업지시번호 {0}, 공정순서 {1}에 지시 수량이 0인 건이 존재합니다.";
                            sMessage = string.Format(sMessage, each.WorkNo, each.ProcessSeq);
                            MessageBoxHandler.Show(sMessage);
                            bCheckZero = true;
                            break;
                        }
                    }
                }

            if (bCheckZero)
                return;

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