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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Class;
using HKInc.Service.Helper;
using DevExpress.Utils;
using HKInc.Utils.Enum;
using DevExpress.XtraBars;
using HKInc.Service.Handler;
using DevExpress.XtraReports.UI;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using HKInc.Service.Service;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Common;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 20211214 오세완 차장
    /// 작업지시 bom 정보를 기반으로 완/반제품 따로 생성
    /// 작업지시 생성 후 포장라벨 출력하는 기능이 있는 작업지시화면 
    /// </summary>
    public partial class XFMPS1200_V2 : Service.Base.ListMasterDetailSubDetailFormTemplate
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

        public XFMPS1200_V2()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            SubDetailGridExControl.MainGrid.MainView.ShowingEditor += SubDetailView_ShowingEditor;
            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += SubDetailView_CellValueChanged;

            OutPutRadioGroup = radioGroup1;
            RadioGroupType = Utils.Enum.RadioGroupType.XFMPS1200;

            dt_PlanMonth.SetFormat(Utils.Enum.DateFormat.Month);
            dt_PlanMonth.DateTime = DateTime.Today;

            dt_WorkDate.DateTime = DateTime.Today;
        }

        protected override void InitCombo()
        {
            // 20210521 오세완 차장 여기는 완제품 작업지시만 출력되야 해서 수정
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.TopCategory_WAN).ToList());

            SetLabelText();
        }

        /// <summary>
        /// 20211220 오세완 차장
        /// design을 새로 해서 basdform에서 label이 자동으로 변환이 안되서 수동처리
        /// </summary>
        private void SetLabelText()
        {
            #region 상단
            string sContolname = lcCondition.Text.Replace("lc", "");
            lcCondition.Text = LabelConvert.GetLabelText(sContolname);
            sContolname = lcPlanMonth.Text.Replace("lc", "");
            lcPlanMonth.Text = LabelConvert.GetLabelText(sContolname);
            sContolname = lcItem.Text.Replace("lc", "");
            lcItem.Text = LabelConvert.GetLabelText(sContolname);
            sContolname = lcDelivNo.Text.Replace("lc", "");
            lcDelivNo.Text = LabelConvert.GetLabelText(sContolname);
            sContolname = lcCustomerWorkNo.Text.Replace("lc", ""); // 20220210 오세완 차장 거래처 작업지시번호 조회 조건 추가
            lcCustomerWorkNo.Text = LabelConvert.GetLabelText(sContolname);
            #endregion

            #region 중단
            sContolname = lcPlanList.Text.Replace("lc", "");
            lcPlanList.Text = LabelConvert.GetLabelText(sContolname);
            #endregion

            #region 좌측하단
            sContolname = lcBomList.Text.Replace("lc", "");
            lcBomList.Text = LabelConvert.GetLabelText(sContolname);
            #endregion

            #region 우측하단
            sContolname = lcWorkList.Text.Replace("lc", "");
            lcWorkList.Text = LabelConvert.GetLabelText(sContolname);
            sContolname = lcWorkDate.Text.Replace("lc", "");
            lcWorkDate.Text = LabelConvert.GetLabelText(sContolname);
            #endregion
        }

        protected override void InitGrid()
        {
            #region 생산계획
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("DelivNo", LabelConvert.GetLabelText("DelivNo")); // 20220103 오세완 차장 이태식 차장 요청으로 추가 
            MasterGridExControl.MainGrid.AddColumn("PlanNo", LabelConvert.GetLabelText("PlanNo"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            // 20220311 오세완 차장 권박사님 요청으로 추가
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Temp1", LabelConvert.GetLabelText("ColorCode"));

            // 20220314 오세완 차장 컬러명 추가 
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ColorName", LabelConvert.GetLabelText("ColorName"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Temp2", LabelConvert.GetLabelText("ColorNickname"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.DelivQty", LabelConvert.GetLabelText("PlanQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1000.OrderDueDate", LabelConvert.GetLabelText("DueDate"));                // 2021-11-04 김진우 주임  납기일 추가

            MasterGridExControl.MainGrid.AddColumn("PlanStartDate", LabelConvert.GetLabelText("PlanStartDate"));
            MasterGridExControl.MainGrid.AddColumn("PlanEndDate", LabelConvert.GetLabelText("PlanEndDate"));
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.TN_ORD1001.CustomerWorkNo", LabelConvert.GetLabelText("CustomerWorkNo")); // 20220210 오세완 차장 이태식 차장 요청으로 수주 엑셀업로드시 참조한 데이터 출력 추가
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.TN_ORD1001.CustomerOrderPartName", LabelConvert.GetLabelText("CustomerOrderPartName"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            #endregion

            #region 품목정보
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.MainGrid.AddColumn("TYPE", LabelConvert.GetLabelText("TopCategory"));
            DetailGridExControl.MainGrid.AddColumn("ITEM_CODE", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("ITEM_NAME", LabelConvert.GetLabelText("ItemName"));
            // 20220313 오세완 차장 권박사님 요청으로 컬러 정보 출력 추가 
            DetailGridExControl.MainGrid.AddColumn("TEMP1", LabelConvert.GetLabelText("ColorCode"));
            // 20220314 오세완 차장 컬러명 추가
            DetailGridExControl.MainGrid.AddColumn("COLOR_NAME", LabelConvert.GetLabelText("ColorName"));

            DetailGridExControl.MainGrid.AddColumn("TEMP2", LabelConvert.GetLabelText("ColorNickname"));
            DetailGridExControl.MainGrid.AddColumn("UNIT_NAME", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("USE_QTY", LabelConvert.GetLabelText("UseQty"));
            DetailGridExControl.MainGrid.AddColumn("WORK_ORDER_JUDGE", "작업지시 생성여부");
            #endregion

            #region 작업지시목록
            SubDetailGridExControl.MainGrid.AddColumn("JobStates", LabelConvert.GetLabelText("JobStates"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("EmergencyFlag", LabelConvert.GetLabelText("EmergencyFlag"));
            SubDetailGridExControl.MainGrid.AddColumn("PlanNo", LabelConvert.GetLabelText("PlanNo"), false);
            SubDetailGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            SubDetailGridExControl.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"));

            SubDetailGridExControl.MainGrid.AddColumn("RealWorkDate", LabelConvert.GetLabelText("RealWorkDate"));
            SubDetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            SubDetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            // 20220311 오세완 차장 권박사님 요청으로 추가
            SubDetailGridExControl.MainGrid.AddColumn("TN_STD1100.Temp1", LabelConvert.GetLabelText("ColorCode"));
            // 20220314 오세완 차장 컬러명 추가 
            SubDetailGridExControl.MainGrid.AddColumn("TN_STD1100.ColorName", LabelConvert.GetLabelText("ColorName"));

            SubDetailGridExControl.MainGrid.AddColumn("TN_STD1100.Temp2", LabelConvert.GetLabelText("ColorNickname"));
            SubDetailGridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            SubDetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            SubDetailGridExControl.MainGrid.AddColumn("MachineGroupCode", LabelConvert.GetLabelText("MachineGroup"));
            SubDetailGridExControl.MainGrid.AddColumn("MachineFlag", LabelConvert.GetLabelText("MachineFlag"), false);

            SubDetailGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"));
            SubDetailGridExControl.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"));
            SubDetailGridExControl.MainGrid.AddColumn("OutProcFlag", LabelConvert.GetLabelText("OutProcFlag"));
            SubDetailGridExControl.MainGrid.AddColumn("Temp", LabelConvert.GetLabelText("SortPackingJudge"));
            SubDetailGridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"), false);

            SubDetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "RealWorkDate", "EmergencyFlag", "WorkDate", "MachineCode", "WorkQty", "OutProcFlag", "WorkId", "Memo", "MachineGroupCode", "Temp");

            // 포장라벨 선출력
            var barBoxLabel_Print = new DevExpress.XtraBars.BarButtonItem();
            barBoxLabel_Print.Id = 5;
            barBoxLabel_Print.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/boreport");
            barBoxLabel_Print.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.B));
            barBoxLabel_Print.Name = "barBoxLabelPrint";
            barBoxLabel_Print.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barBoxLabel_Print.ShortcutKeyDisplayString = "Alt+B";
            barBoxLabel_Print.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barBoxLabel_Print.Caption = LabelConvert.GetLabelText("PackBarcodePrint") + "[Alt+B]";
            barBoxLabel_Print.ItemClick += BarPackBarcodePrint_ItemClick;
            barBoxLabel_Print.Enabled = UserRight.HasEdit;
            barBoxLabel_Print.Alignment = BarItemLinkAlignment.Right;
            SubDetailGridExControl.BarTools.AddItem(barBoxLabel_Print);

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MPS1200>(SubDetailGridExControl);

            #endregion
        }

        /// <summary>
        /// 20211214 오세완 차장 
        /// 작업지시 생성 후 포장라벨 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarPackBarcodePrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (SubDetailGridBindingSource == null)
                return;

            if (!UserRight.HasEdit)
                return;

            var detailObj = SubDetailGridBindingSource.Current as TN_MPS1200;
            if (detailObj == null)
                return;

            if (detailObj.TN_MPS1100 == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_83)));
                return;
            }

            try
            {
                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.KeyValue, detailObj);
                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFMPS1200_BAR, param, null);
                form.ShowPopup(true);
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex.Message);
            }
        }

        private void SubDetailView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.Column.FieldName == "EmergencyFlag")
            {
                var obj = SubDetailGridBindingSource.Current as TN_MPS1200;
                if (obj != null)
                {
                    var checkValue = e.Value.GetNullToEmpty();
                    if (checkValue == "Y")
                    {
                        var detailList = SubDetailGridBindingSource.List as List<TN_MPS1200>;
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
                var detailObj = SubDetailGridBindingSource.Current as TN_MPS1200;
                if (detailObj == null) return;

                if (e.Value.IsNullOrEmpty())
                    detailObj.MachineFlag = "N";
                else
                    detailObj.MachineFlag = "Y";
            }
            else if(e.Column.FieldName == "Temp")
            {
                // 20211220 오세완 차장 정렬포장여부 전체 선택 로직
                TN_MPS1200 subDetail_Obj = SubDetailGridBindingSource.Current as TN_MPS1200;
                if(subDetail_Obj != null)
                {
                    string sCheckValue = e.Value.GetNullToEmpty();
                    List<TN_MPS1200> tempArr = SubDetailGridBindingSource.List as List<TN_MPS1200>;
                    tempArr = tempArr.Where(p => p.ProcessCode != subDetail_Obj.ProcessCode).ToList();

                    if(tempArr != null)
                        if(tempArr.Count > 0)
                        {
                            foreach (TN_MPS1200 each in tempArr)
                                each.Temp = sCheckValue;

                            SubDetailGridExControl.BestFitColumns();
                        }
                            
                }
            }
        }

        private void SubDetailView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Memo" && view.FocusedColumn.FieldName != "Temp1")
            {
                if (view.GetFocusedRowCellValue("JobStates").ToString() != MasterCodeSTR.JobStates_Wait)
                    e.Cancel = true;
            }
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");

            MasterGridExControl.BestFitColumns();

            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStates", DbRequestHandler.GetCommTopCode(MasterCodeSTR.JobStates), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("EmergencyFlag", "N");
            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("RealWorkDate");

            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("MachineFlag", "N");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("WorkQty", DefaultBoolean.Default, "n0");
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProcFlag", "N");

            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(SubDetailGridExControl, "Memo", UserRight.HasEdit);
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp", "N");

            SubDetailGridExControl.BestFitColumns();

            var machineEdit = SubDetailGridExControl.MainGrid.Columns["MachineCode"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            if (machineEdit != null)
                machineEdit.Popup += MachineEdit_Popup;
        }

        private void MachineEdit_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null)
                return;

            var vSubdetailObj = SubDetailGridBindingSource.Current as TN_MPS1200;
            if (vSubdetailObj == null) return;

            if (vSubdetailObj.MachineGroupCode.IsNullOrEmpty())
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            else
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[MachineGroupCode] = '" + vSubdetailObj.MachineGroupCode + "'";
        }

        protected override void DataLoad()
        {
            if (!IsFirstLoaded)
            {
                holidayList = ModelService.GetChildList<Holiday>(p => true).ToList();
            }

            GridRowLocator.GetCurrentRow("PlanNo");
            DetailGridRowLocator.GetCurrentRow("ITEM_CODE");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();
            InitCombo();

            string itemCode = lup_Item.EditValue.GetNullToEmpty();
            string sDelivNo = tx_DelivNo.EditValue.GetNullToEmpty(); // 20220103 오세완 차장 이태식 차장 납품계획번호 조회 기능 요청하여 추가 
            var radioValue = OutPutRadioGroup.EditValue.GetNullToEmpty();
            string sCustomerworkno = tx_Customerworkno.EditValue.GetNullToEmpty(); // 20220210 오세완 차장 이태식차장 요청으로 거래처 작업지시번호 조회 조건 추가 

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                        && (string.IsNullOrEmpty(sDelivNo) ? true : p.DelivNo.Contains(sDelivNo))
                                                                        && ((p.PlanStartDate.Year == dt_PlanMonth.DateTime.Year && p.PlanStartDate.Month == dt_PlanMonth.DateTime.Month)
                                                                            || (p.PlanEndDate.Year == dt_PlanMonth.DateTime.Year && p.PlanEndDate.Month == dt_PlanMonth.DateTime.Month))
                                                                        && (p.TN_ORD1100.TN_ORD1001.TN_ORD1000.OrderType == "양산")
                                                                        && (string.IsNullOrEmpty(sCustomerworkno) ? true : p.TN_ORD1100.TN_ORD1001.CustomerWorkNo.Contains(sCustomerworkno))
                                                                     )
                                                                     .Where(p => radioValue == "A" ? true : p.WorkOrderFlag == radioValue)
                                                                     .OrderBy(p => p.PlanNo)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            TN_MPS1100 masterObj = MasterGridBindingSource.Current as TN_MPS1100;
            if (masterObj == null)
                return;

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", masterObj.ItemCode);
                SqlParameter sp_Planno = new SqlParameter("@PLAN_NO", masterObj.PlanNo);
                SqlParameter sp_Planqty = new SqlParameter("@PLAN_QTY", masterObj.PlanQty);
                
                var vResult = context.Database.SqlQuery<TEMP_XFMPS1200_BOM_INFO>("USP_GET_XFMPS1200_BOM_INFO @ITEM_CODE, @PLAN_NO, @PLAN_QTY", sp_Itemcode, sp_Planno, sp_Planqty).ToList();
                
                // 20220207 오세완 차장 인천정밀과 동일하게 bom이 구성되어 있지 않은 경우는 작업지시를 생성하지 못하게 처리
                bool bPrintMessage = false;
                if (vResult == null)
                    bPrintMessage = true;
                else if (vResult.Count == 0)
                    bPrintMessage = true;

                if (bPrintMessage)
                {
                    string sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_179);
                    MessageBoxHandler.Show(sMessage);
                    DetailGridBindingSource.Clear();
                    SubDetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.AddRow, false);
                }
                else
                {
                    DetailGridBindingSource.DataSource = vResult;
                    SubDetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.AddRow, true);
                }
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();
        }

        protected override void DetailFocusedRowChanged()
        {
            SubDetailGridExControl.MainGrid.Clear();

            TN_MPS1100 masterObj = MasterGridBindingSource.Current as TN_MPS1100;
            if (masterObj == null)
                return;

            TEMP_XFMPS1200_BOM_INFO detailObj = DetailGridBindingSource.Current as TEMP_XFMPS1200_BOM_INFO;
            if (detailObj == null)
                return;

            if (masterObj.TN_MPS1200List != null)
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
                    tempArr = tempArr.Where(p => p.TN_STD1100.ItemCode == detailObj.ITEM_CODE &&
                                                 p.TN_STD1100.UseFlag == "Y").OrderBy(o => o.WorkNo).ThenBy(t => t.ProcessSeq).ToList();

                    SubDetailGridBindingSource.DataSource = tempArr;
                }
            }

            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void SubDetailAddRowClicked()
        {
            TN_MPS1100 masterObj = MasterGridBindingSource.Current as TN_MPS1100;
            if (masterObj == null)
                return;

            TEMP_XFMPS1200_BOM_INFO detailObj = DetailGridBindingSource.Current as TEMP_XFMPS1200_BOM_INFO;
            if (detailObj == null)
                return;

            List<TN_MPS1000> mps1000_Arr = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == detailObj.ITEM_CODE &&
                                                                                      p.UseFlag == "Y").OrderBy(o => o.ProcessSeq).ToList();
            bool bNull_Mps1000 = false;
            if (mps1000_Arr == null)
                bNull_Mps1000 = true;
            else if (mps1000_Arr.Count == 0)
                bNull_Mps1000 = true;

            if(bNull_Mps1000)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemProcess")));
                return;
            }

            AddProcess(masterObj, detailObj, mps1000_Arr);

            SubDetailGridExControl.MainGrid.BestFitColumns();
        }

        private void AddProcess(TN_MPS1100 masterObj, TEMP_XFMPS1200_BOM_INFO detailObj, List<TN_MPS1000> TN_MPS1000List)
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
                    ItemCode = detailObj.ITEM_CODE,
                    MachineGroupCode = v.MachineGroupCode,
                    CustomerCode = masterObj.CustomerCode,
                    EmergencyFlag = "N",
                    WorkDate = dt,
                    WorkQty = masterObj.TN_ORD1100.DelivQty,                // 2021-11-04 김진우 주임 수정             기존 masterObj.PlanQty
                    OutProcFlag = v.OutProcFlag,
                    MachineFlag = v.MachineFlag,
                    JobStates = MasterCodeSTR.JobStates_Wait,
                    TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == detailObj.ITEM_CODE).First(),
                    NewRowFlag = "Y",
                    Memo = masterObj.Memo.GetNullToEmpty()
                };

                // 20211221 오세완 차장 반제품인 경우 해당 완제품의 품목코드를 넣어주기
                if(detailObj.TYPE.IndexOf("반제품") > -1)
                {
                    List<TEMP_XFMPS1200_BOM_INFO> bomArr = DetailGridBindingSource.List as List<TEMP_XFMPS1200_BOM_INFO>;
                    TEMP_XFMPS1200_BOM_INFO bom_Wan = bomArr.Where(p => p.TYPE == "완제품").FirstOrDefault();
                    newObj.Temp2 = bom_Wan.ITEM_CODE;
                }
                    

                SubDetailGridBindingSource.Add(newObj);
                masterObj.TN_MPS1200List.Add(newObj);
                FirstInsertFlag = false;
            }
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

        protected override void DeleteSubDetailRow()
        {
            TN_MPS1100 masterObj = MasterGridBindingSource.Current as TN_MPS1100;
            if (masterObj == null)
                return;

            List<TN_MPS1200> mpsArr = SubDetailGridBindingSource.List as List<TN_MPS1200>;
            TN_MPS1200 current_mps = SubDetailGridBindingSource.Current as TN_MPS1200;
            if (masterObj == null || mpsArr == null || current_mps == null)
                return;

            if (current_mps.JobStates != MasterCodeSTR.JobStates_Wait)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_110));
                return;
            }

            if (current_mps.ProcessSeq == 1 && current_mps.ProcessCode == MasterCodeSTR.Process_Packing && current_mps.JobStates != MasterCodeSTR.JobStates_Wait)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_110));
                return;
            }

            if (current_mps.ProcessCode == MasterCodeSTR.Process_Packing)
            {
                if (DialogResult.Yes != MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_124), "", MessageBoxButtons.YesNo))
                    return;
            }

            ModelService.RemoveChild<TN_MPS1200>(current_mps);
            masterObj.TN_MPS1200List.Remove(current_mps);
            SubDetailGridBindingSource.RemoveCurrent();
            bClick_DetailGridDelete = true;
        }

        protected override void DataSave()
        {
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();

            List<TN_MPS1200> mpsArr = SubDetailGridBindingSource.List as List<TN_MPS1200>;

            // 2021-11-04 김진우 주임 추가
            if (mpsArr.Any(p => p.WorkQty == 0))
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_175));
                return;
            }
            
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
            TN_MPS1200 current_mps = SubDetailGridBindingSource.Current as TN_MPS1200;
            if (current_mps == null)
                return;

            if (bClick_DetailGridDelete)
            {
                string sWorkno = current_mps.WorkNo.GetNullToEmpty();
                if (sWorkno != "")
                {
                    string sSql = "EXEC USP_UPD_MPS1200_PROCESS_SEQ '" + sWorkno + "'";
                    DbRequestHandler.GetCellValue(sSql, 0);
                }
            }
        }
    }
}