using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraReports.UI;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.View.View.REPORT;
using System.IO;
using HKInc.Utils.Interface.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace HKInc.Ui.View.View.POP
{
    /// <summary>
    /// 20210712 오세완 차장
    /// 프레스 POP, plc user control 사용 버전
    /// 20210721 오세완 차장
    /// PLC 인터페이스 최종 버전으로 사용하기로 함
    /// </summary>
    public partial class XFPOP_PLC_V2 : HKInc.Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");
        List<TN_STD1000> processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
        List<TN_STD1000> surfaceList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.SurfaceList);
        //IService<TN_MEA1600> ModelService_Plc = (IService<TN_MEA1600>)ProductionFactory.GetDomainService("TN_MEA1600");

        private static object oLock = new object();
        private bool bLockCount = false;

        /// <summary>
        /// 20210820 오세완 차장 
        /// 공정코드 초기화 
        /// </summary>
        private bool gb_Click_Process_Delete = false;

        /// <summary>
        /// 20210820 오세완 차장 
        /// 설비코드 초기화
        /// </summary>
        private bool gb_Click_Machine_Delete = false;

        /// <summary>
        /// 20210906 오세완 차장 
        /// 팝업이 활성화 되어 있을 때 실적이 update되는 것을 방지?
        /// </summary>
        private bool gb_ShowPopup = false;

        /// <summary>
        /// 20210915 오세완 차장
        /// 작업이 시작된 경우는 조회를 막아서 시작한 작업지시가 사라지거나 변경되는 것을 방지
        /// </summary>
        private bool gb_WorkStart = false;

        #endregion

        public XFPOP_PLC_V2()
        {
            LogFactory.GetLoginLogService().SetLoginLog(DateTime.Now);
            MenuOpenLogService.SetOpenMenuLog(DateTime.Now, 9999);
            InitializeComponent();
           
            gridEx1.ViewType = GridViewType.POP_GridView;

            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            gridEx1.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            gridEx1.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
            gridEx1.MainGrid.MainView.BeforeLeaveRow += MainView_BeforeLeaveRow;

            tx_WorkNo.KeyDown += Tx_WorkNo_KeyDown;
            tx_WorkNo.Click += Tx_WorkNo_Click;

            btn_Search.Click += Btn_Search_Click;
            btn_Up.Click += Btn_Up_Click; ;
            btn_Down.Click += Btn_Down_Click;
            pic_WorkStandardDocument.DoubleClick += Pic_WorkStandardDocument_DoubleClick;
            pic_DesignFileName.DoubleClick += Pic_DesignFileName_DoubleClick;
            btn_RestartTO.Click += Btn_RestartTO_Click;
            btn_WorkStart.Click += Btn_WorkStart_Click;
            btn_ResultAdd.Click += Btn_ResultAdd_Click;
            btn_QualityAdd.Click += Btn_QualityAdd_Click;
            btn_WorkEnd.Click += Btn_WorkEnd_Click;
            btn_SrcChange.Click += Btn_SrcChange_Click; // 20210611 오세완 차장 원자재 전용 교체로 변경
            btn_ItemMoveDoc_Change.Click += Btn_ItemMoveDoc_Change_Click; // 20210611 오세완 차장 이동표교체 전용 버튼 추가
            btn_MachineStop.Click += Btn_MachineStop_Click;
            btn_MachineCheck.Click += Btn_MachineCheck_Click;
            btn_ItemMovePrint.Click += Btn_ItemMovePrint_Click;
            btn_Exit.Click += Btn_Exit_Click;
            btn_MoldCheck.Click += Btn_MoldCheck_Click;
            btn_ReworkResultAdd.Click += Btn_ReworkResultAdd_Click;
            btn_Reset_count_before_workstart.Click += Btn_Reset_count_before_workstart_Click;

            pdf_Design.DocumentChanged += Pdf_Design_DocumentChanged;
            pdf_Design.DoubleClick += Pdf_Design_DoubleClick;
            pdf_Design.NavigationPaneInitialVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;
            pdf_Design.CursorMode = DevExpress.XtraPdfViewer.PdfCursorMode.HandTool;

            // 20210715 오세완 차장 PLC 통신 모듈 event확인
            plc_Modbus.OnUpdate += Plc_Modbus_OnUpdate_UseProcedure;
            plc_Modbus.OnChangeConnection += Plc_Modbus_OnChangeConnection_UseProcedure;
            plc_Modbus.OnDoubleClick += Plc_Modbus_DoubleClick;

            // 20210820 오세완 차장 프레스 pop도 다른 작업지시를 조회 할 수 있게 설정이 필요하여 추가 처리
            lup_Process.ButtonClick += Lup_Process_ButtonClick;
            lup_Machine.ButtonClick += Lup_Machine_ButtonClick;
        }

        /// <summary>
        /// 20210913 오세완 차장 작업중인 경우 작업지시 이동 막기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            TEMP_XFPOP1000 obj = GridBindingSource.Current as TEMP_XFPOP1000;
            bool bDonotChange = false;
            if (obj == null)
                bDonotChange = true;
            else if (obj.JobStates == MasterCodeSTR.JobStates_Start)
                bDonotChange = true;

            if (bDonotChange)
                e.Allow = false;
        }

        /// <summary>
        /// 20210905 오세완 차장 
        /// 대영 신부장 요청으로 작업 시작전 시험 타발을 리셋하는 용도로 사용한다고 추가요청한 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Reset_count_before_workstart_Click(object sender, EventArgs e)
        {
            TEMP_XFPOP1000 obj = GridBindingSource.Current as TEMP_XFPOP1000;
            bool bDo_Reset = false;
            if(obj == null)
                bDo_Reset = true;
            else if(obj.JobStates == MasterCodeSTR.JobStates_Wait)
                bDo_Reset = true;

            if (bDo_Reset)
                plc_Modbus.Reset_Count();
        }

        private void Lup_Machine_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                gb_Click_Machine_Delete = true;
            }
        }

        private void Lup_Process_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                gb_Click_Process_Delete = true;
            }
        }

        private void Btn_ReworkResultAdd_Click(object sender, EventArgs e)
        {
            TEMP_XFPOP1000 mObj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (mObj == null)
                return;

            //PopupDataParam param = new PopupDataParam();
            //param.SetValue(PopupParameter.KeyValue, mObj);
            //IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_REWORK_V2, param, ResultAddCallback);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_REWORK_V2, null, null); // 20210618 오세완 차장 리워크실적은 현재 작업에 영향을 주지 않게 하기 위해서 callback을 끊어버림
            form.ShowPopup(true);
        }



        /// <summary>
        /// 20210604 오세완 차장
        /// 금형일상점검 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MoldCheck_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;

            //Form modalF = new POP_POPUP.XPFMOLDCHECK();
            Form modalF = new POP_POPUP.XPFMOLDCHECK(obj); // 20220110 오세완 차장 신부장 요청으로 작업지시에 지정된 설비의 금형 있는 경우 해당 금형으로 일상점검 진행하게 수정
            modalF.ShowDialog();
        }

        /// <summary>
        /// 20210603 오세완 차장 
        /// 재가동TO 입력 팝업
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_RestartTO_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            var qcRev = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == obj.ItemCode && p.UseFlag == "Y").OrderBy(p => p.RowId).LastOrDefault();
            if (qcRev == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionItem")));
                return;
            }

            if (!qcRev.TN_QCT1001List.Any(p => p.CheckDivision == MasterCodeSTR.InspectionDivision_Setting && p.ProcessCode == obj.ProcessCode && p.UseFlag == "Y"))
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionItem")));
                return;

            }

            var restartForm = new POP_POPUP.XPFRESTART_TO(obj, tx_ProductLotNo.EditValue.GetNullToEmpty());
            if (restartForm.ShowDialog() == DialogResult.Cancel) { }
            else
                ActRefresh();
        }

        private void Tx_WorkNo_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad)
                return;

            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                tx_WorkNo.EditValue = keyPad.returnval;
                DataLoad();
            }
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ProcessCode")
            {
                var processCode = gridEx1.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ProcessCode").GetNullToEmpty();
                var surface = gridEx1.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "SurfaceList").GetNullToEmpty();
                if (!processCode.IsNullOrEmpty() && !surfaceList.IsNullOrEmpty())
                {
                    var processObj = processList.Where(p => p.CodeVal == processCode).FirstOrDefault();
                    if (processObj != null && processObj.CodeName.Contains("표면처리"))
                    {
                        var surfaceObj = surfaceList.Where(p => p.CodeVal == surface).FirstOrDefault();
                        if (surfaceObj != null)
                        {
                            e.DisplayText = processObj.CodeName + "_" + surfaceObj.CodeName;
                        }
                    }
                }
            }
        }

        private void Tx_WorkNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DataLoad();
        }

        private void Pdf_Design_DoubleClick(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null)
            {
                return;
            }
            try
            {
                WaitHandler.ShowWait();
                try
                {
                    byte[] documentContent = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.DesignFileUrl);
                    MemoryStream ms = new MemoryStream(documentContent);
                    POP_POPUP.XPFPOPPDF fm = new POP_POPUP.XPFPOPPDF(ms);
                    fm.ShowDialog();
                }
                catch { }
            }
            finally { WaitHandler.CloseWait(); }
        }

        private void Pdf_Design_DocumentChanged(object sender, DevExpress.XtraPdfViewer.PdfDocumentChangedEventArgs e)
        {
            pdf_Design.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.PageLevel;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
            //plc_Modbus.Init_Value();
            //plc_Modbus.SyncObj = this;
            plc_Modbus.Start();
        }

        protected override void InitCombo()
        {
            // 20211001 오세완 차장 긴급지시때 이것때문에 버튼을 못누르게 된다. 
            if(!gb_WorkStart)
                SetButtonEnable(null);

            lup_Process.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            // 20210602 오세완 차장 SerachLookup은 textbox에 입력때문에 무조건 useflag가 Y를 조회한다. 
            lup_Machine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => true).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            List<TN_STD1100> tempArr = ModelService.GetChildList<TN_STD1100>(p => (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList();
            // 20210602 오세완 차장 SerachLookup은 textbox에 입력때문에 무조건 useflag가 Y를 조회한다. 
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), tempArr, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            lup_FactoryCode.SetFontSize(new Font("맑은 고딕", 12f));
            lup_Process.SetFontSize(new Font("맑은 고딕", 12f));
            lup_Machine.SetFontSize(new Font("맑은 고딕", 12f));
            lup_Item.SetFontSize(new Font("맑은 고딕", 12f));

            lup_Process.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_Item.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            lup_Item.Properties.View.OptionsView.ShowColumnHeaders = false;

            // 20210807 오세완 차장 이걸 빼야 대영의 3가지 출력 조건이 될 듯
            //lup_Item.Properties.View.Columns["ItemCode"].Visible = false;
            lup_Machine.Properties.View.Columns["MachineMCode"].Visible = false;
            lup_Process.Properties.View.Columns["CodeVal"].Visible = false;

            InitButtonLabelConvert();

            // 20210809 오세완 차장 프레스 공정이니 상단의 콤보박스를 고정처리
            TN_STD1000 temp_Process = processList.Find(p => p.CodeName == "프레스");
            if (temp_Process != null)
            {
                // 20210820 오세완 차장 검색조건을 사용자가 삭제처리하면 날려버리기
                if(!gb_Click_Process_Delete)
                    lup_Process.EditValue = temp_Process.CodeVal;
            }
                

            if(plc_Modbus != null)
            {
                string sMachineMCode = plc_Modbus.MachineMCode;
                if(sMachineMCode.GetNullToEmpty() != "")
                {
                    // 20210820 오세완 차장 검색조건을 사용자가 삭제처리하면 날려버리기
                    if (!gb_Click_Machine_Delete)
                        lup_Machine.EditValue = sMachineMCode;
                }
            }
        }

        private void InitButtonLabelConvert()
        {
            btn_Search.Text = LabelConvert.GetLabelText("Refresh");
            btn_RestartTO.Text = LabelConvert.GetLabelText("RestartTO"); // 20210602 오세완 차장 재가동TO
            btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
            btn_ResultAdd.Text = LabelConvert.GetLabelText("ResultAdd");
            btn_QualityAdd.Text = LabelConvert.GetLabelText("QualityAdd");
            btn_WorkEnd.Text = LabelConvert.GetLabelText("WorkEnd");
            btn_SrcChange.Text = LabelConvert.GetLabelText("SrcChange");
            btn_MachineStop.Text = LabelConvert.GetLabelText("MachineStop");
            btn_MachineCheck.Text = LabelConvert.GetLabelText("MachineCheck");
            btn_ItemMovePrint.Text = LabelConvert.GetLabelText("ItemMovePrint");
            btn_Exit.Text = LabelConvert.GetLabelText("Close");
            btn_ReworkResultAdd.Text = LabelConvert.GetLabelText("ReworkResultAdd"); // 20210602 오세완 차장 리워크실적등록
            btn_ItemMoveDoc_Change.Text = LabelConvert.GetLabelText("ItemMoveChange"); // 20210611 오세완 차장 이동표교체 전용 버튼 추가
            btn_Reset_count_before_workstart.Text = LabelConvert.GetLabelText("ResetCountBeforeStart"); // 20210905 오세완 차장 시타발초기화 버튼 추가

            lcDesign2.Text = lcDesign.Text;
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            gridEx1.MainGrid.AddColumn("EmergencyFlag", LabelConvert.GetLabelText("EmergencyFlag"), false);
            gridEx1.MainGrid.AddColumn("JobStates", LabelConvert.GetLabelText("JobStates"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            gridEx1.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));

            gridEx1.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"));
            gridEx1.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            gridEx1.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));            
            gridEx1.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            gridEx1.MainGrid.AddColumn("StartDueDate", LabelConvert.GetLabelText("StartDueDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");

            gridEx1.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("Process"));
            // 20210625 오세완 차장 이사님 지시로 생략 처리
            //gridEx1.MainGrid.AddColumn("MachineGroupCode", LabelConvert.GetLabelText("MachineGroup"));
            //gridEx1.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
            gridEx1.MainGrid.AddColumn("MachineCode2", LabelConvert.GetLabelText("Machine"));
            gridEx1.MainGrid.AddColumn("TopCategoryName", LabelConvert.GetLabelText("TopCategory"), false);
            gridEx1.MainGrid.AddColumn("ProcessPackQty", LabelConvert.GetLabelText("ProcessPackQty"), false); // 20210603 오세완 차장 생산하는데 필요하다 생각되지 않아서 생략처리
            gridEx1.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
        }

        protected override void InitRepository()
        {
            // 20210625 오세완 차장 이사님 지시로 생략 처리
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", "MachineCode");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode2", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStates", DbRequestHandler.GetCommTopCode(MasterCodeSTR.JobStates), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            SetMessage("★긴급 작업은 빨간색으로 표시됩니다.★");
            DataLoad();
        }

        /// <summary>
        /// 조회
        /// </summary>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            // 20210915 오세완 차장 작업시작한 경우는 지시 조회를 안시킨다. 
            if(!gb_WorkStart)
                ActRefresh();
        }

        protected override void DataLoad()
        {
            var keyFieldName = "RowId";
            object keyValue = null;
            int currentRow = 0;
            if (gridEx1.MainGrid.MainView.RowCount > 0)
            {
                currentRow = gridEx1.MainGrid.MainView.FocusedRowHandle > 0 ? gridEx1.MainGrid.MainView.FocusedRowHandle : 0;
                keyValue = gridEx1.MainGrid.MainView.GetRowCellValue(currentRow, keyFieldName);
            }

            gridEx1.MainGrid.Clear();

            ModelService.ReLoad();
            // 20210624 오세완 차장 김이사님 지시로 공통코드 추가시 화면 재출력 없이 하기 위해서 추가
            InitRepository();
            InitCombo();

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                var ProcessCode = new SqlParameter("@ProcessCode", lup_Process.EditValue.GetNullToEmpty());
                var MachineCode = new SqlParameter("@MachineCode", lup_Machine.EditValue.GetNullToEmpty());
                var ItemCode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
                var WorkNo = new SqlParameter("@WorkNo", tx_WorkNo.EditValue.GetNullToEmpty().ToUpper());

                var result = context.Database.SqlQuery<TEMP_XFPOP1000>("USP_GET_XFPOP1000_LIST_V2 @ProcessCode, @MachineCode, @ItemCode, @WorkNo",
                                                                                ProcessCode, MachineCode, ItemCode, WorkNo).ToList();
                GridBindingSource.DataSource = result.OrderByDescending(p => p.EmergencyFlag).ThenBy(p => p.WorkDate).ThenBy(p => p.WorkNo).ToList();
                if (result.Count == 0)
                {
                    SetRefreshControl();
                }
            }

            gridEx1.DataSource = GridBindingSource;
            
            if (string.IsNullOrEmpty(keyFieldName) || keyValue == null)
                gridEx1.MainGrid.MainView.FocusedRowHandle = currentRow;
            else
                gridEx1.MainGrid.MainView.FocusedRowHandle = gridEx1.MainGrid.MainView.LocateByValue(keyFieldName, keyValue);

            gridEx1.BestFitColumns();
        }

        /// <summary>
        /// 20210914 오세완 차장 설비통신 모듈에서 비가동 처리를 해서 cross thread 방지하면서 grid내용 update처리
        /// </summary>
        private void DataLoad_Grid()
        {
            var keyFieldName = "RowId";
            object keyValue = null;
            int currentRow = 0;
            if (gridEx1.MainGrid.MainView.RowCount > 0)
            {
                currentRow = gridEx1.MainGrid.MainView.FocusedRowHandle > 0 ? gridEx1.MainGrid.MainView.FocusedRowHandle : 0;
                keyValue = gridEx1.MainGrid.MainView.GetRowCellValue(currentRow, keyFieldName);
            }

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                var ProcessCode = new SqlParameter("@ProcessCode", lup_Process.EditValue.GetNullToEmpty());
                var MachineCode = new SqlParameter("@MachineCode", lup_Machine.EditValue.GetNullToEmpty());
                var ItemCode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
                var WorkNo = new SqlParameter("@WorkNo", tx_WorkNo.EditValue.GetNullToEmpty().ToUpper());

                var result = context.Database.SqlQuery<TEMP_XFPOP1000>("USP_GET_XFPOP1000_LIST_V2 @ProcessCode, @MachineCode, @ItemCode, @WorkNo",
                                                                                ProcessCode, MachineCode, ItemCode, WorkNo).ToList();
                if(!this.Disposing)
                {
                    if(this.InvokeRequired)
                    {
                        this.BeginInvoke(new MethodInvoker(delegate () {
                            GridBindingSource.DataSource = result.OrderByDescending(p => p.EmergencyFlag).ThenBy(p => p.WorkDate).ThenBy(p => p.WorkNo).ToList();
                        }));
                    }
                    else
                        GridBindingSource.DataSource = result.OrderByDescending(p => p.EmergencyFlag).ThenBy(p => p.WorkDate).ThenBy(p => p.WorkNo).ToList();
                }
                
                if (result.Count == 0)
                {
                    SetRefreshControl();
                }
                else
                {
                    if(!gridEx1.Disposing)
                    {
                        int iLocateValue = gridEx1.MainGrid.MainView.LocateByValue(keyFieldName, keyValue);

                        if (string.IsNullOrEmpty(keyFieldName) || keyValue == null)
                        {
                            if (gridEx1.InvokeRequired)
                            {
                                gridEx1.BeginInvoke(new MethodInvoker(delegate () {
                                    gridEx1.MainGrid.Clear();
                                    gridEx1.DataSource = GridBindingSource;
                                    gridEx1.MainGrid.MainView.FocusedRowHandle = currentRow;
                                    gridEx1.BestFitColumns();
                                }));
                            }
                            else
                            {
                                gridEx1.MainGrid.Clear();
                                gridEx1.DataSource = GridBindingSource;
                                gridEx1.MainGrid.MainView.FocusedRowHandle = currentRow;
                                gridEx1.BestFitColumns();
                            }
                        }
                        else
                        {
                            if (gridEx1.InvokeRequired)
                            {
                                gridEx1.BeginInvoke(new MethodInvoker(delegate () {
                                    gridEx1.MainGrid.Clear();
                                    gridEx1.DataSource = GridBindingSource;
                                    gridEx1.MainGrid.MainView.FocusedRowHandle = iLocateValue;
                                    gridEx1.BestFitColumns();
                                }));
                            }
                            else
                            {
                                gridEx1.MainGrid.Clear();
                                gridEx1.DataSource = GridBindingSource;
                                gridEx1.MainGrid.MainView.FocusedRowHandle = iLocateValue;
                                gridEx1.BestFitColumns();
                            }
                        }
                    }
                }
            }
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                RowChange();
            }
            finally { WaitHandler.CloseWait(); }
        }

        private void RowChange()
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null)
            {
                SetButtonEnable(null);
                InitButtonLabelConvert();
                SetRefreshControl();
                return;
            }

            SetButtonEnable(obj.JobStates);
            ModelService.ReLoad();

            //마지막 진행 실적 가져오기 (실적종료시간이 없는 경우)
            var TN_MPS1201 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
            if (TN_MPS1201 != null)
            {
                tx_ProductLotNo.EditValue = TN_MPS1201.ProductLotNo;
                tx_ResultQty.EditValue = TN_MPS1201.ResultSumQty.GetDecimalNullToZero().ToString("#,0.##");
                tx_OkQty.EditValue = TN_MPS1201.OkSumQty.GetDecimalNullToZero().ToString("#,0.##");
                tx_BadQty.EditValue = TN_MPS1201.BadSumQty.GetDecimalNullToZero().ToString("#,0.##");

                var sumObj = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).ToList();
                tx_SumResultQty.EditValue = sumObj.Sum(p => p.ResultSumQty).GetDecimalNullToZero().ToString("#,0.##");

            }
            else
            {
                SetRefreshControl();
            }

            if (obj.ProcessSeq == 1)
            {
                lcItemMoveNoEnd.Text = LabelConvert.GetLabelText("ItemMoveNoEnd");
                var productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();
                var LastItemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ProductLotNo == productLotNo).OrderBy(p => p.RowId).LastOrDefault();
                if (LastItemMoveObj == null)
                    //마지막 이동표번호 가져오기
                    tx_ItemMoveNoEnd.EditValue = string.Empty;
                else
                    tx_ItemMoveNoEnd.EditValue = LastItemMoveObj.ItemMoveNo;
            }
            else
            {
                lcItemMoveNoEnd.Text = LabelConvert.GetLabelText("ItemMoveNoNow");
                //현 이동표번호 가져오기
                tx_ItemMoveNoEnd.EditValue = TN_MPS1201 == null ? string.Empty : TN_MPS1201.ItemMoveNo;
            }

            //작업표준서
            if (!obj.WorkStandardDocumentUrl.IsNullOrEmpty())
                pic_WorkStandardDocument.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.WorkStandardDocumentUrl);
            else
                pic_WorkStandardDocument.EditValue = null;

            //품목 도면
            if (!obj.DesignFileUrl.IsNullOrEmpty())
            {
                var fileName = obj.DesignFileName;
                int fileExtPos = fileName.LastIndexOf(".");
                string extName = string.Empty;
                if (fileExtPos >= 0)
                    extName = fileName.Substring(fileExtPos + 1, fileName.Length - fileExtPos - 1);

                if (extName.ToLower() == "pdf")
                {
                    lcDesign.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcDesign2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    pic_DesignFileName.EditValue = null;
                    try
                    {
                        byte[] documentContent = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.DesignFileUrl);
                        MemoryStream ms = new MemoryStream(documentContent);
                        pdf_Design.LoadDocument(ms);
                    }
                    catch
                    {
                        lcDesign.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lcDesign2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    }
                }
                else
                {
                    lcDesign.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcDesign2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    pic_DesignFileName.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.DesignFileUrl);
                    pdf_Design.CloseDocument();
                }
            }
            else
            {
                lcDesign.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcDesign2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                pic_DesignFileName.EditValue = null;
                pdf_Design.CloseDocument();
            }

            // 20210915 오세완 차장 실행될때 작업지시 있는 경우 판단 필요
            if (obj.JobStates == MasterCodeSTR.JobStates_Start)
            {
                gb_WorkStart = true;
                btn_Search.Enabled = false;
            }
            else
            {
                gb_WorkStart = false;
                btn_Search.Enabled = true;
            }
                
        }

        /// <summary>
        /// 20210903 오세완 차장
        /// plc로 부터 수신받은 수량을 실적으로 처리한 내용을 화면으로 출력
        /// </summary>
        /// <param name="tUpdateObj"></param>
        private void QtyChange(TEMP_XFPOP1000 tUpdateObj)
        {
            string sSql = "EXEC USP_GET_POP_PLC_RESULT_QTY '" + tUpdateObj.WorkNo + "', '" + tUpdateObj.ProcessCode + "', " + tUpdateObj.ProcessSeq.ToString();
            DataTable dt = DbRequestHandler.GetDataTableSelect(sSql);
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    if (tx_ProductLotNo.InvokeRequired)
                    {
                        tx_ProductLotNo.BeginInvoke(new MethodInvoker(delegate () {
                            tx_ProductLotNo.EditValue = dt.Rows[0]["PRODUCT_LOT_NO"].GetNullToEmpty();
                        }));
                    }
                    else
                        tx_ProductLotNo.EditValue = dt.Rows[0]["PRODUCT_LOT_NO"].GetNullToEmpty();

                    if(tx_ResultQty.InvokeRequired)
                    {
                        tx_ResultQty.BeginInvoke(new MethodInvoker(delegate () {
                            tx_ResultQty.EditValue = dt.Rows[0]["RESULT_QTY"].GetDecimalNullToZero().ToString("#,0.##");
                        }));
                    }
                    else 
                        tx_ResultQty.EditValue = dt.Rows[0]["RESULT_QTY"].GetDecimalNullToZero().ToString("#,0.##");

                    if(tx_OkQty.InvokeRequired)
                    {
                        tx_OkQty.BeginInvoke(new MethodInvoker(delegate () {
                            tx_OkQty.EditValue = dt.Rows[0]["OK_QTY"].GetDecimalNullToZero().ToString("#,0.##");
                        }));
                    }
                    else
                        tx_OkQty.EditValue = dt.Rows[0]["OK_QTY"].GetDecimalNullToZero().ToString("#,0.##");

                    if(tx_BadQty.InvokeRequired)
                    {
                        tx_BadQty.BeginInvoke(new MethodInvoker(delegate () {
                            tx_BadQty.EditValue = dt.Rows[0]["BAD_QTY"].GetDecimalNullToZero().ToString("#,0.##");
                        }));
                    }
                    else
                        tx_BadQty.EditValue = dt.Rows[0]["BAD_QTY"].GetDecimalNullToZero().ToString("#,0.##");

                    if(tx_SumResultQty.InvokeRequired)
                    {
                        tx_SumResultQty.BeginInvoke(new MethodInvoker(delegate () {
                            tx_SumResultQty.EditValue = dt.Rows[0]["SUM_QTY"].GetDecimalNullToZero().ToString("#,0.##");
                        }));
                    }
                    else
                        tx_SumResultQty.EditValue = dt.Rows[0]["SUM_QTY"].GetDecimalNullToZero().ToString("#,0.##");
                }
            }
        }
        
        protected override void GridRowDoubleClicked() { }

        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        private void SetRefreshControl()
        {
            tx_ItemMoveNoEnd.EditValue = ""; //이동표번호
            tx_ProductLotNo.EditValue = ""; //LOT_NO
            tx_ResultQty.EditValue = "0"; //생산수량
            tx_OkQty.EditValue = "0"; //양품수량
            tx_BadQty.EditValue = "0"; //불량수량
            tx_SumResultQty.EditValue = "0"; //누적 생산수량
            pic_WorkStandardDocument.EditValue = null; //작업표준서
            pic_DesignFileName.EditValue = null; //도면
        }
        
        /// <summary>
        /// ▲ 클릭
        /// </summary>
        private void Btn_Up_Click(object sender, EventArgs e)
        {
            var view = gridEx1.MainGrid.MainView as GridView;
            view.FocusedRowHandle -= 1;
        }

        /// <summary>
        /// ▼ 클릭
        /// </summary>
        private void Btn_Down_Click(object sender, EventArgs e)
        {
            var view = gridEx1.MainGrid.MainView as GridView;
            view.FocusedRowHandle += 1;
        }

        /// <summary>
        /// 작업표준서 더블클릭
        /// </summary>
        private void Pic_WorkStandardDocument_DoubleClick(object sender, EventArgs e)
        {
            if (pic_WorkStandardDocument.EditValue == null) return;
            var imgForm = new POP_POPUP.XPFPOPIMG(LabelConvert.GetLabelText("WorkStandardDocument"), pic_WorkStandardDocument.EditValue);
            imgForm.ShowDialog();
        }

        /// <summary>
        /// 품목도면 더블클릭
        /// </summary>
        private void Pic_DesignFileName_DoubleClick(object sender, EventArgs e)
        {
            if (pic_DesignFileName.EditValue == null) return;
            var imgForm = new POP_POPUP.XPFPOPIMG(LabelConvert.GetLabelText("DesignFileName"), pic_DesignFileName.EditValue);
            imgForm.ShowDialog();
        }

        /// <summary>
        /// 버튼 상태값 CHECK
        /// </summary>
        private void SetButtonEnable(string jobStates)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if(obj == null)
            {
                btn_RestartTO.Enabled = false;
                btn_WorkStart.Enabled = false;
                btn_ResultAdd.Enabled = false;
                btn_QualityAdd.Enabled = false;
                btn_WorkEnd.Enabled = false;
                btn_SrcChange.Enabled = false;
                btn_MachineStop.Enabled = true;
                btn_MachineCheck.Enabled = true;
                btn_MoldCheck.Enabled = true;
                btn_ItemMovePrint.Enabled = false;
                btn_Exit.Enabled = true;
                btn_ItemMoveDoc_Change.Enabled = false; // 20210611 오세완 차장 이동표교체 전용 버튼 추가하여 원자재 교체랑 동일하게 처리
                btn_ReworkResultAdd.Enabled = true; // 20210618 오세완 차장 리워크실적 등록은 언제라도 할 수 있게 처리
                btn_Reset_count_before_workstart.Enabled = true; // 20210905 오세완 차장 시타발 초기화 버튼 기능 추가
            }
            else
            {
                string sFlag_RestartTO = "";
                TN_MPS1000 temp_mps = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == obj.ItemCode &&
                                                                                 p.UseFlag == "Y" &&
                                                                                 p.ProcessCode == obj.ProcessCode).FirstOrDefault();
                if (temp_mps != null)
                    sFlag_RestartTO = temp_mps.RestartToFlag.GetNullToEmpty();

                if (jobStates == MasterCodeSTR.JobStates_Wait) //대기
                {
                    btn_RestartTO.Enabled = true;
                    // 20210603 오세완 차장 재가동TO가 사용함으로 되어 있으면 작업시작 전 꼭 진행할 수 있게 처리
                    // 20210609 오세완 차장            필수 입력값이 아니라서 그냥 작업시작 버튼을 풀어줌
                    //if (sFlag_RestartTO == "Y")
                    //{
                    //    bool bCheck = Check_RestartToProcess(obj);
                    //    btn_WorkStart.Enabled = bCheck;
                    //}
                    //else
                    //    btn_WorkStart.Enabled = true;

                    btn_WorkStart.Enabled = true;
                    btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                    btn_ResultAdd.Enabled = false;
                    btn_QualityAdd.Enabled = false;
                    btn_WorkEnd.Enabled = false;
                    btn_SrcChange.Enabled = false;
                    btn_MachineStop.Enabled = true;
                    btn_MachineCheck.Enabled = true;
                    btn_ItemMovePrint.Enabled = false;
                    btn_Exit.Enabled = true;
                    btn_ItemMoveDoc_Change.Enabled = false; // 20210611 오세완 차장 이동표교체 전용 버튼 추가하여 원자재 교체랑 동일하게 처리
                    btn_ReworkResultAdd.Enabled = true; // 20210618 오세완 차장 리워크실적 등록은 언제라도 할 수 있게 처리
                    btn_Reset_count_before_workstart.Enabled = true; // 20210905 오세완 차장 시타발 초기화 버튼 기능 추가

                    if (obj.ProcessSeq > 1)
                    {
                        btn_ItemMovePrint.Enabled = true;
                    }
                }
                else if (jobStates == MasterCodeSTR.JobStates_Start) //진행
                {
                    btn_RestartTO.Enabled = true;
                    btn_WorkStart.Enabled = false;
                    btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                    btn_ResultAdd.Enabled = true;
                    btn_QualityAdd.Enabled = true;
                    btn_WorkEnd.Enabled = true;

                    // 20210614 오세완 차장 수동관리여부가 지정되지 않은 원자재는 교체할 이유가 없음으로 상태를 확인해서 제어
                    string sSql = "exec USP_GET_XFPOP1000_CHECK_SRC_CHANGE '" + obj.WorkNo + "', '" + obj.ProcessCode + "'";
                    string sValue = DbRequestHandler.GetCellValue(sSql, 0);
                    if (sValue != null)
                        if (sValue == "OK")
                            btn_SrcChange.Enabled = true;
                        else
                            btn_SrcChange.Enabled = false;

                    btn_MachineStop.Enabled = true;
                    btn_MachineCheck.Enabled = true;
                    btn_ItemMovePrint.Enabled = true;
                    btn_Exit.Enabled = true;
                    btn_ReworkResultAdd.Enabled = true; // 20210618 오세완 차장 리워크실적 등록은 언제라도 할 수 있게 처리
                    btn_Reset_count_before_workstart.Enabled = false; // 20210905 오세완 차장 시타발 초기화는 대기 아니면 처리 못하게 추가

                    // 20210611 오세완 차장 이동표교체 전용 버튼 추가하여 첫번째 공정만 빼고 나머지는 원자재 교체 버튼과 동일한 규칙 적용
                    if (obj.ProcessSeq == 1)
                        btn_ItemMoveDoc_Change.Enabled = false;
                    else
                        btn_ItemMoveDoc_Change.Enabled = true;
                }
                else if (jobStates == MasterCodeSTR.JobStates_Pause) //일시정지
                {
                    btn_RestartTO.Enabled = true;
                    btn_WorkStart.Enabled = true;
                    btn_WorkStart.Text = LabelConvert.GetLabelText("Restart");
                    btn_ResultAdd.Enabled = false;
                    btn_QualityAdd.Enabled = false;
                    btn_WorkEnd.Enabled = false;
                    btn_SrcChange.Enabled = false;
                    btn_MachineStop.Enabled = true;
                    btn_MachineCheck.Enabled = true;
                    btn_ItemMovePrint.Enabled = false;
                    btn_Exit.Enabled = true;
                    btn_ItemMoveDoc_Change.Enabled = false; // 20210611 오세완 차장 이동표교체 전용 버튼 추가하여 원자재 교체랑 동일하게 처리
                    btn_ReworkResultAdd.Enabled = true; // 20210618 오세완 차장 리워크실적 등록은 언제라도 할 수 있게 처리
                    btn_Reset_count_before_workstart.Enabled = false; // 20210905 오세완 차장 시타발 초기화는 대기 아니면 처리 못하게 추가

                    if (obj.ProcessSeq > 1)
                    {
                        btn_ItemMovePrint.Enabled = true;
                    }
                }
                else if(jobStates == MasterCodeSTR.JobStates_Stop) 
                {
                    // 20210063 오세완 차장 비가동TO때문에 비가동이 추가가 됨
                    if (sFlag_RestartTO == "Y")
                    {
                        // 20210603 오세완 차장 재가동TO를 입력해 달라고 풀어준다. 대신 비가동을 풀때 입력여부를 확인한다. 
                        btn_RestartTO.Enabled = true;
                    }
                    else
                        btn_RestartTO.Enabled = false;

                    //btn_WorkStart.Enabled = true;
                    btn_WorkStart.Enabled = false; // 20210915 오세완 차장 비가동이면 풀때까지 작업시작 못하게 설정
                    btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                    btn_ResultAdd.Enabled = false;
                    btn_QualityAdd.Enabled = false;
                    btn_WorkEnd.Enabled = false;
                    btn_SrcChange.Enabled = false;
                    btn_MachineStop.Enabled = true;
                    btn_MachineCheck.Enabled = true;
                    btn_ItemMovePrint.Enabled = false;
                    btn_Exit.Enabled = true;
                    btn_ItemMoveDoc_Change.Enabled = false; // 20210611 오세완 차장 이동표교체 전용 버튼 추가하여 원자재 교체랑 동일하게 처리
                    btn_ReworkResultAdd.Enabled = true; // 20210618 오세완 차장 리워크실적 등록은 언제라도 할 수 있게 처리
                    btn_Reset_count_before_workstart.Enabled = false; // 20210905 오세완 차장 시타발 초기화는 대기 아니면 처리 못하게 추가

                    if (obj.ProcessSeq > 1)
                    {
                        btn_ItemMovePrint.Enabled = true;
                    }
                }
                else
                {
                    btn_RestartTO.Enabled = false;
                    btn_WorkStart.Enabled = false;
                    btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                    btn_ResultAdd.Enabled = false;
                    btn_QualityAdd.Enabled = false;
                    btn_WorkEnd.Enabled = false;
                    btn_SrcChange.Enabled = false;
                    btn_MachineStop.Enabled = true;
                    btn_MachineCheck.Enabled = true;
                    btn_ItemMovePrint.Enabled = false;
                    btn_Exit.Enabled = true;
                    btn_ItemMoveDoc_Change.Enabled = false; // 20210611 오세완 차장 이동표교체 전용 버튼 추가하여 원자재 교체랑 동일하게 처리
                    btn_ReworkResultAdd.Enabled = true; // 20210618 오세완 차장 리워크실적 등록은 언제라도 할 수 있게 처리
                    btn_Reset_count_before_workstart.Enabled = false; // 20210905 오세완 차장 시타발 초기화는 대기 아니면 처리 못하게 추가
                }
            }
            
        }

        /// <summary>
        /// 20210609 오세완 차장
        /// 특정 작업시지가 재가동TO와 연관으로 비가동이 걸렸는지 여부를 판단
        /// </summary>
        /// <param name="tObj">작업지시 객체</param>
        /// <returns>false - 작업시작 못함, true - 작업시작 가능</returns>
        private bool Check_RestartToProcess(TEMP_XFPOP1000 tObj)
        {
            bool bResult = false;

            if (tObj == null)
                return bResult;

            // 20210609 오세완 차장
            // 특정 작업지시가 재가동TO와 연관있는 작업지시인데 비가동을 풀지 않은 경우
            List<TN_MEA1004> tempArr = ModelService.GetChildList<TN_MEA1004>(p => p.Temp == tObj.WorkNo &&
                                                                                  p.StopEndTime == null &&
                                                                                  p.StopCode == "07").ToList();

            if (tempArr == null)
                bResult = true;
            else if (tempArr.Count == 0)
                bResult = true;
            
            return bResult;
        }

        /// <summary>
        /// 작업설정
        /// </summary>
        private void Btn_JobSetting_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            var qcRev = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == obj.ItemCode && p.UseFlag == "Y").OrderBy(p => p.RowId).LastOrDefault();
            if (qcRev == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionItem")));
                return;
            }

            if(!qcRev.TN_QCT1001List.Any(p => p.CheckDivision == MasterCodeSTR.InspectionDivision_Setting && p.ProcessCode == obj.ProcessCode && p.UseFlag == "Y"))
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionItem")));
                return;

            }

            var jobSettingForm = new POP_POPUP.XPFJOBSETTING(obj, tx_ProductLotNo.EditValue.GetNullToEmpty());
            if (jobSettingForm.ShowDialog() == DialogResult.Cancel){ }
            else
                ActRefresh();
        }

        /// <summary>
        /// 작업시작
        /// </summary>
        private void Btn_WorkStart_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            if (obj.JobStates == MasterCodeSTR.JobStates_Pause)
            {
                if (!obj.MachineCode.IsNullOrEmpty())
                {
                    var nowMachineStateObj = ModelService.GetChildList<VI_XRREP6000_LIST>(p => p.MachineMCode == obj.MachineCode).FirstOrDefault();
                    if (nowMachineStateObj != null)
                    {
                        if (nowMachineStateObj.JobStates != MasterCodeSTR.JobStates_Wait && nowMachineStateObj.JobStates != MasterCodeSTR.JobStates_Pause)
                        {
                            MessageBoxHandler.Show("해당 설비는 대기 또는 일시정지 설비가 아니므로 선택할 수 없습니다. 확인 부탁드립니다.");
                            return;
                        }
                    }
                }

                var productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();
                var TN_MPS1203 = ModelService.GetChildList<TN_MPS1203>(p => p.WorkNo == obj.WorkNo
                                                                        && p.ProcessCode == obj.ProcessCode
                                                                        && p.ProcessSeq == obj.ProcessSeq
                                                                        && p.ProductLotNo == productLotNo
                                                                        && p.PauseEndDate == null).FirstOrDefault();
                if (TN_MPS1203 != null)
                {
                    TN_MPS1203.PauseEndDate = DateTime.Now;
                    TN_MPS1203.UpdateTime = TN_MPS1203.PauseEndDate;
                    ModelService.UpdateChild(TN_MPS1203);
                }

                var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo
                                                                        && p.ProcessCode == obj.ProcessCode
                                                                        && p.ProcessSeq == obj.ProcessSeq).FirstOrDefault();
                if (TN_MPS1200 != null)
                {
                    //작업지시서 상태 변경
                    TN_MPS1200.JobStates = MasterCodeSTR.JobStates_Start;
                    TN_MPS1200.UpdateTime = DateTime.Now;
                    ModelService.UpdateChild(TN_MPS1200);
                }
                ModelService.Save();
                ActRefresh();

                if(obj.ProcessCode == MasterCodeSTR.Process_Press)
                {
                    gb_WorkStart = true;
                    btn_Search.Enabled = false;
                }
                    
            }
            else
            {
                if (obj.ProcessSeq == 1) //첫번째 공정일 경우 자재투입
                {
                    //원자재투입 / 반제품 투입
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.KeyValue, obj);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRAW_MATERIAL_IN_V2, param, WorkStartSrcCallback); // 20210629 오세완 차장 품목정보 위로 올린 버전
                    //IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRAW_MATERIAL_IN_V3, param, WorkStartSrcCallback); // 20210627 오세완 차장 이사님이 품목명이 출력이 많은 경우 깨질 수 있다하여 변경
                    form.ShowPopup(true);
                }
                else
                {
                    //이동표 정보 조회 추가필요
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.KeyValue, obj);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVESCAN_START_DAEYOUNG, param, WorkStartItemMoveCallback);
                    form.ShowPopup(true);
                }
            }
        }

        /// <summary>
        /// 작업 시작 시 원소재 투입 CallBack
        /// </summary>
        private void WorkStartSrcCallback(object sender, PopupArgument e)
        {
            if (e == null)
                return;

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null)
                return;

            var machineCode = e.Map.GetValue(PopupParameter.Value_1);

            var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();
            string productLotNo = "";
            var workingDate = DateTime.Today;

            string sFlag = e.Map.GetValue(PopupParameter.Value_2).GetNullToEmpty();
            if(sFlag == "NO_ONE")
            {
                // 20210608 오세완 차장 첫번째 공정에 수동관리품목이 없으면 원자재 투입 없이 생산LOTNO를 생성해야 한다. 
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
                    var MachineCode = new SqlParameter("@MachineCode", machineCode);
                    var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                    var WorkingDate = new SqlParameter("@WorkingDate", workingDate);
                    var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                    //작업지시투입정보 INSERT
                    productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC_V3 @WorkNo, @MachineCode, @ItemCode, @WorkingDate, @LoginId"
                                                                    , WorkNo, MachineCode, ItemCode, WorkingDate, LoginId).SingleOrDefault();
                }
            }
            else if(sFlag == "HAVE")
            {
                var returnList = (List<TEMP_XFPOP1000_WORKSTART_INFO>)e.Map.GetValue(PopupParameter.ReturnObject);
                bool bBreak = false;

                foreach (var v in returnList)
                {
                    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                    {
                        var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
                        var MachineCode = new SqlParameter("@MachineCode", machineCode);
                        var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                        var ProcessCode = new SqlParameter("@ProcessCode", obj.ProcessCode);
                        var SrcItemCode = new SqlParameter("@SrcItemCode", v.ITEM_CODE);
                        var SrcOutLotNo = new SqlParameter("@SrcOutLotNo", v.OUT_LOT_NO);
                        var ProductLotNo = new SqlParameter("@ProductLotNo", productLotNo);
                        var WorkingDate = new SqlParameter("@WorkingDate", workingDate);
                        var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                        //작업지시투입정보 INSERT
                        productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC_V2 @WorkNo, @MachineCode, @ItemCode , @ProcessCode, @SrcItemCode, @SrcOutLotNo, @ProductLotNo, @WorkingDate, @LoginId"
                                                                        , WorkNo, MachineCode, ItemCode, ProcessCode, SrcItemCode, SrcOutLotNo, ProductLotNo, WorkingDate, LoginId).SingleOrDefault();
                    }

                    if (productLotNo.IsNullOrEmpty())
                        bBreak = true;

                    if (bBreak)
                        break;

                }

                if (bBreak)
                    return;
            }

            if (productLotNo.IsNullOrEmpty())
                return;

            #region 작업실적관리 마스터 INSERT
            var TN_MPS1201_NewObj = new TN_MPS1201();
            TN_MPS1201_NewObj.WorkNo = obj.WorkNo;
            TN_MPS1201_NewObj.ProcessCode = obj.ProcessCode;
            TN_MPS1201_NewObj.ProcessSeq = obj.ProcessSeq;
            TN_MPS1201_NewObj.ProductLotNo = productLotNo;
            TN_MPS1201_NewObj.ItemCode = obj.ItemCode;
            TN_MPS1201_NewObj.CustomerCode = obj.CustomerCode;
            TN_MPS1201_NewObj.ResultStartDate = DateTime.Now;
            TN_MPS1201_NewObj.ResultSumQty = 0;
            TN_MPS1201_NewObj.OkSumQty = 0;
            TN_MPS1201_NewObj.BadSumQty = 0;
            
            // 20210701 오세완 차장 작업지작시 선택된 설비코드를 반영못하는 오류 수정
            if (machineCode != null)
                if (machineCode.GetNullToEmpty() != "")
                    TN_MPS1201_NewObj.MachineCode = machineCode.GetNullToEmpty();
                else 
                    TN_MPS1201_NewObj.MachineCode = obj.MachineCode; // 20210625 오세완 차장 시작전에 설비를 입력하니까 실적도 입력하는 것으로 변경처리

            // 20210612 오세완 차장 주성 스타일처럼 트리거를 쓰지않고 해보기 위해서 금형코드를 매칭해 봄
            if (obj.ProcessCode == MasterCodeSTR.Process_Press)
                TN_MPS1201_NewObj.Temp = e.Map.GetValue(PopupParameter.Value_3).GetNullToEmpty();

            ModelService.Insert(TN_MPS1201_NewObj);
            #endregion

            //작업지시서 상태 변경
            TN_MPS1200.JobStates = MasterCodeSTR.JobStates_Start;
            TN_MPS1200.UpdateTime = DateTime.Now;
            ModelService.UpdateChild(TN_MPS1200);
            ModelService.Save();
            ActRefresh();

            if (obj.ProcessCode == MasterCodeSTR.Process_Press)
            {
                gb_WorkStart = true;
                btn_Search.Enabled = false;
            }
                
        }

        /// <summary>
        /// 작업 시작 시 이동표 투입 CallBack
        /// </summary>
        private void WorkStartItemMoveCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();

            var itemMoveNo = e.Map.GetValue(PopupParameter.Value_1).GetNullToNull();
            var productLotNo = e.Map.GetValue(PopupParameter.Value_2).GetNullToEmpty();
            var machineCode = e.Map.GetValue(PopupParameter.Value_3).GetNullToEmpty();

            #region BOM 원자재 투입
            var returnList = (List<TEMP_XFPOP1000_WORKSTART_INFO>)e.Map.GetValue(PopupParameter.ReturnObject);
            var workingDate = DateTime.Today;
            bool bBreak = false;

            if(returnList != null)
            {
                foreach (var v in returnList)
                {
                    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                    {
                        var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
                        var MachineCode = new SqlParameter("@MachineCode", machineCode);
                        var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                        var ProcessCode = new SqlParameter("@ProcessCode", obj.ProcessCode);
                        var SrcItemCode = new SqlParameter("@SrcItemCode", v.ITEM_CODE);
                        var SrcOutLotNo = new SqlParameter("@SrcOutLotNo", v.OUT_LOT_NO);
                        var ProductLotNo = new SqlParameter("@ProductLotNo", productLotNo);
                        var WorkingDate = new SqlParameter("@WorkingDate", workingDate);
                        var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                        //작업지시투입정보 INSERT
                        productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC_V2 @WorkNo, @MachineCode, @ItemCode , @ProcessCode, @SrcItemCode, @SrcOutLotNo, @ProductLotNo, @WorkingDate, @LoginId"
                                                                        , WorkNo, MachineCode, ItemCode, ProcessCode, SrcItemCode, SrcOutLotNo, ProductLotNo, WorkingDate, LoginId).SingleOrDefault();
                    }

                    if (productLotNo.IsNullOrEmpty())
                        bBreak = true;

                    if (bBreak)
                        break;

                }

                if (bBreak)
                    return;
            }

            if (productLotNo.IsNullOrEmpty())
                return;
            #endregion

            

            var ItemMoveFirstObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == itemMoveNo && p.ProcessSeq == 1).First();

            var newItemMoveNo = new TN_ITEM_MOVE();
            newItemMoveNo.ItemMoveNo = itemMoveNo;
            newItemMoveNo.WorkNo = obj.WorkNo;
            newItemMoveNo.ProcessCode = obj.ProcessCode;
            newItemMoveNo.ProcessSeq = obj.ProcessSeq;
            newItemMoveNo.ProductLotNo = productLotNo;
            newItemMoveNo.BoxInQty = ItemMoveFirstObj.BoxInQty.GetDecimalNullToZero();
            newItemMoveNo.ResultSumQty = 0;
            newItemMoveNo.OkSumQty = 0;
            newItemMoveNo.BadSumQty = 0;
            newItemMoveNo.ResultQty = 0;
            newItemMoveNo.OkQty = 0;
            newItemMoveNo.BadQty = 0;
            ModelService.InsertChild(newItemMoveNo);

            #region 작업실적관리 마스터 INSERT
            var TN_MPS1201_NewObj = new TN_MPS1201();
            TN_MPS1201_NewObj.WorkNo = obj.WorkNo;
            TN_MPS1201_NewObj.ProcessCode = obj.ProcessCode;
            TN_MPS1201_NewObj.ProcessSeq = obj.ProcessSeq;
            TN_MPS1201_NewObj.ItemMoveNo = itemMoveNo;
            TN_MPS1201_NewObj.ProductLotNo = productLotNo;
            TN_MPS1201_NewObj.ItemCode = obj.ItemCode;
            TN_MPS1201_NewObj.CustomerCode = obj.CustomerCode;
            TN_MPS1201_NewObj.MachineCode = machineCode;
            TN_MPS1201_NewObj.ResultStartDate = DateTime.Now;
            TN_MPS1201_NewObj.ResultSumQty = 0;
            TN_MPS1201_NewObj.OkSumQty = 0;
            TN_MPS1201_NewObj.BadSumQty = 0;
            ModelService.Insert(TN_MPS1201_NewObj);
            #endregion

            #region 타발수 증감 
            string sMoldmcode = e.Map.GetValue(PopupParameter.Value_4).GetNullToEmpty();
            if (obj.ProcessCode == MasterCodeSTR.Process_Press)
            {
                //TN_MPS1200.Temp = sMoldmcode;
                TN_MPS1201_NewObj.Temp = sMoldmcode; // 20210629 오세완 차장 이렇게 해야 증감이 제대로 됨
            }
            #endregion

            //작업지시서 상태 변경
            TN_MPS1200.JobStates = MasterCodeSTR.JobStates_Start;
            TN_MPS1200.UpdateTime = DateTime.Now;
            ModelService.UpdateChild(TN_MPS1200);
            ModelService.Save();
            ActRefresh();

            if (obj.ProcessCode == MasterCodeSTR.Process_Press)
            {
                gb_WorkStart = true;
                btn_Search.Enabled = false;
            }
                
        }

        /// <summary>
        /// 실적등록
        /// </summary>
        private void Btn_ResultAdd_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            var productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();
            var itemMoveNo = tx_ItemMoveNoEnd.EditValue.GetNullToEmpty();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.Value_1, productLotNo);
            param.SetValue(PopupParameter.Value_2, itemMoveNo);
            param.SetValue(PopupParameter.Value_3, "PLC"); // 20210709 오세완 차장 PLC용 POP 실적 처리 구분
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_DEFAULT_V2, param, ResultAddCallback);
            form.ShowPopup(true);
        }

        /// <summary>
        /// 실적등록 CallBack
        /// </summary>
        private void ResultAddCallback(object sender, PopupArgument e)
        {
            if (e == null) return;
            
            ActRefresh();
        }

        /// <summary>
        /// 품질등록
        /// </summary>
        private void Btn_QualityAdd_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            //var inspectionForm = new POP_POPUP.XPFINSPECTION(obj, tx_ProductLotNo.EditValue.GetNullToEmpty());
            var inspectionForm = new POP_POPUP.XPFINSPECTION_V2(obj, tx_ProductLotNo.EditValue.GetNullToEmpty()); // 20210619 오세완 차장 초중종 로직 개선 버전
            if (inspectionForm.ShowDialog() != DialogResult.OK) { }
            else
                ActRefresh();
        }

        /// <summary>
        /// 작업종료
        /// </summary>
        private void Btn_WorkEnd_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            var productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.Value_1, productLotNo);

            // 20210903 오세완 차장 프레스 pop에서 부품이동표 출력 양식 그대로 구현
            string sSql = "EXEC USP_GET_PLC_POP_CURRENT_QTY '" + obj.WorkNo + "', '" + obj.ProcessCode + "', " + obj.ProcessSeq.ToString() + ", '" + tx_ProductLotNo.EditValue.GetNullToEmpty() + "' ";
            string sResult = DbRequestHandler.GetCellValue(sSql, 0);
            param.SetValue(PopupParameter.Value_2, sResult.GetDecimalNullToZero());
            param.SetValue(PopupParameter.Value_3, "Press");

            sSql = "EXEC USP_GET_POP_PRESS_REST_RESULTQTY '" + obj.WorkNo + "' ";
            string sResult1 = DbRequestHandler.GetCellValue(sSql, 0);
            param.SetValue(PopupParameter.Value_4, sResult1.GetDecimalNullToZero());
            gb_ShowPopup = true;
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFWORKEND_V2, param, WorkEndCallback);
            form.ShowPopup(true);
        }

        /// <summary>
        /// 작업 종료 시 CallBack
        /// </summary>
        private void WorkEndCallback(object sender, PopupArgument e)
        {
            gb_ShowPopup = false;
            if (e == null)
                return;

            if (e.Map.ContainsKey(PopupParameter.Constraint)) //새출력 시 
            {
                var obj = GridBindingSource.Current as TEMP_XFPOP1000;
                if (obj == null) return;

                var itemMoveNo = e.Map.GetValue(PopupParameter.Value_1).GetNullToEmpty();
                if (itemMoveNo.IsNullOrEmpty()) return;

                TEMP_ITEM_MOVE_NO_MASTER masterObj = null;
                List<TEMP_ITEM_MOVE_NO_DETAIL> detailList;

                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var _workNo = new SqlParameter("@WorkNo", obj.WorkNo);
                    var _ItemMoveNo = new SqlParameter("@ItemMoveNo", itemMoveNo);

                    //masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER_V2 @WorkNo", _workNo).Where(p => p.ItemMoveNo == itemMoveNo).FirstOrDefault(); // 20210616 오세완 차장 수동으로 지시한 반제품이 나오기 위해서 프로시저 수정본으로 교체
                    var vResult = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER_V3 @WorkNo, @ItemMoveNo", _workNo, _ItemMoveNo).ToList(); // 20211126 오세완 차장 일반 pop에서 방식을 바꿔서 일치처리
                    if (vResult != null)
                        masterObj = vResult.FirstOrDefault();

                }

                if (masterObj == null) return;

                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
                    var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

                    //detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
                    detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL_V2 @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList(); // 20211126 오세완 차장 일반 pop에서 작업자가 잘 나오지 않는 현상이 있어서 수정한 버전 적용
                }

                string sPressType = e.Map.GetValue(PopupParameter.Value_2).GetNullToEmpty();
                if(sPressType == "A4")
                {
                    // 20210905 오세완 차장 a4형태 출력
                    var ItemMoveNoReport = new XRITEMMOVENO(masterObj, detailList);
                    var printTool = new ReportPrintTool(ItemMoveNoReport);
                    printTool.ShowPreview();
                }
                else
                {
                    // 20210905 오세완 프레스 공정 바코드 형태 출력
                    var vPrintmulti = new XRITEMMOVEDOC_PRESS();
                    for (int j = 0; j < Convert.ToInt32(masterObj.BoxInQty); j++)
                    {
                        TEMP_ITEM_MOVE_NO_DETAIL tempDetail1 = detailList.Where(p => p.ProcessCode == MasterCodeSTR.Process_Press).FirstOrDefault();
                        if (tempDetail1 != null)
                        {
                            decimal dPerBoxQty1 = e.Map.GetValue(PopupParameter.Value_3).GetDecimalNullToZero(); // 20210830 오세완 차장 공정박스당 수량 출력
                            var vPrinteach = new XRITEMMOVEDOC_PRESS(masterObj.ItemMoveNo, masterObj.ItemCode, dPerBoxQty1);
                            vPrinteach.CreateDocument();
                            vPrintmulti.Pages.AddRange(vPrinteach.Pages);
                        }
                    }

                    // 20210907 오세완 차장 박스에 정량화되지 못한 수량은 +1처리하여 발행하는 로직으로 처리하기로 이사님이 지시함
                    decimal dOkQty = (decimal)detailList.Where(p => p.ProcessCode == obj.ProcessCode).FirstOrDefault().OkQty;
                    decimal dDiffQty = dOkQty - (masterObj.PerBoxQty.GetDecimalNullToZero() * (decimal)masterObj.BoxInQty);
                    if (dDiffQty > 0)
                    {
                        var vPrintAdd = new XRITEMMOVEDOC_PRESS(masterObj.ItemMoveNo, masterObj.ItemCode, dDiffQty);
                        vPrintAdd.CreateDocument();
                        vPrintmulti.Pages.AddRange(vPrintAdd.Pages);
                    }

                    vPrintmulti.PrintingSystem.ShowMarginsWarning = false;
                    vPrintmulti.ShowPrintStatusDialog = false;
                    vPrintmulti.ShowPreview();
                }
            }
            //else
            //{
            // 20210627 오세완 차장 첫공정이 마지막 공정일 수도 있기 때문에 생략처리

            TEMP_XFPOP1000 tObj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (tObj == null)
            {
                ActRefresh();
                return;
            }

            // 20210914 오세완 차장 작업종료 상태를 명확하게 하여 자재소진 및 실적 초기화 실행
            string sWorkEnd = e.Map.GetValue(PopupParameter.Value_4).GetNullToEmpty();
            if(sWorkEnd == "WorkEnd")
            {
                #region 자재 또는 반재품 소요량 차감, 수동으로 관리하지 않는 자재
                int iProcessMax = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == tObj.WorkNo).Max(m => m.ProcessSeq);
                if (tObj.ProcessSeq == iProcessMax)
                {
                    // 20210622 오세완 차장 완제품은 왠만하면 포장공정을 처리해서 수동관리를 하지 않는 자재를 자동출고 처리하나 혹 그렇지 않은 제품은 여기서 처리 하도록 추가
                    if (tObj.TopCategoryName == "완제품")
                    {
                        List<TN_STD1300> childBomObj_MG_NOT = null;
                        var wanBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == tObj.ItemCode &&
                                                                                    p.UseFlag == "Y").FirstOrDefault();
                        if (wanBomObj != null)
                        {
                            // 20210622 오세완 차장 반제품 군은 무조건 종료 전에 처리가 됬다고 가정한다. 
                            childBomObj_MG_NOT = ModelService.GetChildList<TN_STD1300>(p => p.ParentBomCode == wanBomObj.BomCode &&
                                                                                            (p.TopCategory != MasterCodeSTR.TopCategory_BAN || p.TopCategory != MasterCodeSTR.TopCategory_BAN_Outsourcing) &&
                                                                                            p.MgFlag == "N" &&
                                                                                            p.UseFlag == "Y").ToList();

                            if (childBomObj_MG_NOT.Count > 0)
                            {
                                foreach (var v in childBomObj_MG_NOT)
                                {
                                    AutoOutSrcQty(v.ItemCode, v.UseQty, tObj);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (var context = new HKInc.Ui.Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
                        {
                            SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", tObj.ItemCode);
                            SqlParameter sp_Workno = new SqlParameter("@WORK_NO", tObj.WorkNo); // 20220106 오세완 차장 프로시저 수정으로 추가 
                            //var vResult = context.Database.SqlQuery<TEMP_XPFRESULT_BOMLIST_BANPRODUCT>("USP_GET_XPFRESULT_BOMLIST_BANPRODUCT_AUTO @ITEM_CODE", sp_Itemcode).ToList();
                            var vResult = context.Database.SqlQuery<TEMP_XPFRESULT_BOMLIST_BANPRODUCT>("USP_GET_XPFRESULT_BOMLIST_BANPRODUCT_AUTO @ITEM_CODE, @WORK_NO", sp_Itemcode, sp_Workno).ToList(); // 20220106 오세완 차장 프로시저 수정으로 추가 
                            if (vResult != null)
                                if (vResult.Count > 0)
                                {
                                    foreach (TEMP_XPFRESULT_BOMLIST_BANPRODUCT each in vResult)
                                    {
                                        AutoOutSrcQty(each.ITEM_CODE, each.USE_QTY, tObj);
                                    }
                                }
                        }
                    }

                    Combine_LotDtl(tObj.WorkNo, tObj.ProcessCode);

                }
                #endregion

                // 20210721 오세완 차장 초기화 방법 변경
                // 20210914 오세완 차장 작업종료일때만 실적 카운트 초기화 처리
                Plc_Modbus_ResetCount();
            }
            //}

            // 20210714 오세완 차장 작업종료시에서 실적 초기화
            //plc_Modbus.Reset_Count();

            ActRefresh();
            RowChange();

            if (tObj.ProcessCode == MasterCodeSTR.Process_Press)
            {
                // 20210916 오세완 차장 작업지시가 1개만 있는 경우에도 작업완료를 하면 이전 상태가 반영이 되지 않은 상태로 여기까지 오기 때문에 작업상태를 가지고 판단을 못해서 그냥 풀어주는 것으로...
                gb_WorkStart = false;
                btn_Search.Enabled = true;
            }
                
        }

        /// <summary>
        /// 20210624 오세완 차장 자동 출고한 원자재 혹은 반제품 lot를 추적할 수 있게 tn_lot_dtl에 insert
        /// </summary>
        /// <param name="sWorkno">작업지시번호</param>
        /// <param name="sProcesscode">공정코드</param>
        private void Combine_LotDtl(string sWorkno, string sProcesscode)
        {
            string sProductLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();
            //투입정보불러오기.
            List<TN_LOT_MST> mstArr = ModelService.GetChildList<TN_LOT_MST>(p => p.WorkNo == sWorkno && 
                                                                                 p.ProductLotNo == sProductLotNo).ToList();
            if(mstArr != null)
                if(mstArr.Count > 0)
                {
                    TN_LOT_MST mstEach = mstArr.FirstOrDefault();
                    if(mstEach.TN_LOT_DTL_List != null)
                        if(mstEach.TN_LOT_DTL_List.Count > 0)
                        {
                            List<TN_PUR1300> tempArr = ModelService.GetChildList<TN_PUR1300>(p => p.Temp == sWorkno).ToList();
                            if (tempArr != null)
                                if (tempArr.Count > 0)
                                {
                                    TN_PUR1300 purEach = tempArr.FirstOrDefault();
                                    if (purEach.TN_PUR1301List != null)
                                        if (purEach.TN_PUR1301List.Count > 0)
                                        {
                                            foreach (TN_PUR1301 each in purEach.TN_PUR1301List)
                                            {
                                                decimal dSeqMax = 0;
                                                if (mstEach.TN_LOT_DTL_List != null)
                                                    dSeqMax = mstEach.TN_LOT_DTL_List.Count + 1;
                                                else
                                                    dSeqMax = 1;

                                                TN_LOT_DTL newDtl = new TN_LOT_DTL()
                                                {
                                                    WorkNo = sWorkno,
                                                    ProductLotNo = sProductLotNo,
                                                    ItemCode = mstEach.ItemCode,
                                                    Seq = dSeqMax,
                                                    SrcCode = each.ItemCode,
                                                    SrcInLotNo = each.OutLotNo,
                                                    WorkingDate = each.CreateTime
                                                };

                                                using (var context = new HKInc.Ui.Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
                                                {
                                                    SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", mstEach.ItemCode);
                                                    SqlParameter sp_Workno = new SqlParameter("@WORK_NO", mstEach.WorkNo); // 20220106 오세완 차장 프로시저 변경으로 수정 
                                                    //var vResult = context.Database.SqlQuery<TEMP_XPFRESULT_BOMLIST_BANPRODUCT>("USP_GET_XPFRESULT_BOMLIST_BANPRODUCT_AUTO @ITEM_CODE", sp_Itemcode).ToList();
                                                    var vResult = context.Database.SqlQuery<TEMP_XPFRESULT_BOMLIST_BANPRODUCT>("USP_GET_XPFRESULT_BOMLIST_BANPRODUCT_AUTO @ITEM_CODE, @WORK_NO", sp_Itemcode, sp_Workno).ToList(); // 20220106 오세완 차장 프로시저 변경으로 수정 
                                                    if (vResult != null)
                                                        if (vResult.Count > 0)
                                                        {
                                                            foreach (TEMP_XPFRESULT_BOMLIST_BANPRODUCT each1 in vResult)
                                                            {
                                                                if(each1.ITEM_CODE == each.ItemCode)
                                                                {
                                                                    if (each1.PROCESS_CODE.GetNullToEmpty() != "")
                                                                        newDtl.ProcessCode = each1.PROCESS_CODE.GetNullToEmpty();
                                                                    else
                                                                        newDtl.ProcessCode = sProcesscode;

                                                                    break;
                                                                }
                                                            }
                                                        }
                                                }

                                                mstEach.TN_LOT_DTL_List.Add(newDtl);
                                                ModelService.UpdateChild<TN_LOT_MST>(mstEach);
                                                ModelService.Save();
                                            }
                                        }
                                }
                        }
                }
        }

        /// <summary>
        /// 20210622 오세완 차장 수동관리여부 N인 건에 대한 원자재를 작업종료시 자동출고처리
        /// </summary>
        /// <param name="sItemcode">차감할 원자재 품목코드</param>
        /// <param name="dUseqty">소요량</param>
        /// <param name="pObj">작업지시객체</param>
        private void AutoOutSrcQty(string sItemcode, decimal dUseqty, TEMP_XFPOP1000 pObj)
        {
            decimal dResultQty = tx_ResultQty.EditValue.GetDecimalNullToZero();
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", sItemcode);
                var vResult = context.Database.SqlQuery<TEMP_XPFRESULT_AUTO_SRCOUT_LIST>("USP_GET_XPFRESULT_AUTO_SRCOUT_LIST @ITEM_CODE", sp_Itemcode).ToList();
                if (vResult != null)
                    if (vResult.Count > 0)
                    {
                        bool bSet_Reout = false;
                        decimal dCalQty = dResultQty * dUseqty;
                        bool bBreak = false;
                        foreach (TEMP_XPFRESULT_AUTO_SRCOUT_LIST each in vResult)
                        {
                            TN_PUR1300 prenewobj = new TN_PUR1300()
                            {
                                OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                                OutDate = DateTime.Today,
                                OutId = GlobalVariable.LoginId,
                                Memo = pObj.ItemCode + " 자동차감출고",
                                Temp = pObj.WorkNo // 20210622 오세완 차장 자동출고한 지시번호 연결
                            };

                            List<TN_PUR1201> tempArr = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == each.IN_LOT_NO).ToList();
                            if(tempArr != null)
                                if(tempArr.Count > 0)
                                {
                                    TN_PUR1201 preInDetailObj = tempArr.FirstOrDefault();
                                    TN_PUR1301 newdtlobj = new TN_PUR1301()
                                    {
                                        OutNo = prenewobj.OutNo,
                                        OutSeq = prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                                        InNo = preInDetailObj.InNo,
                                        InSeq = preInDetailObj.InSeq,
                                        ItemCode = sItemcode,
                                        OutLotNo = prenewobj.OutNo + (prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                        InLotNo = each.IN_LOT_NO,
                                        InCustomerLotNo = preInDetailObj.InCustomerLotNo,
                                        Memo = pObj.ItemCode + "자동차감출고",
                                        ReOutYn = "N", //이전 LOT의 출고 막음
                                        AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
                                    };

                                    if (dCalQty <= each.STOCK_QTY)
                                    {
                                        newdtlobj.OutQty = dCalQty;
                                    }
                                    else
                                    {
                                        newdtlobj.OutQty = (decimal)each.STOCK_QTY;
                                        bSet_Reout = true;
                                    }

                                    // 20210624 오세완 차장 아래보다 이게 효율적으로 보임
                                    preInDetailObj.TN_PUR1301List.Add(newdtlobj);
                                    ModelService.InsertChild<TN_PUR1300>(prenewobj);

                                    if (bSet_Reout)
                                    {
                                        List<TN_PUR1201> tempArr1 = ModelService.GetChildList<TN_PUR1201>(p => p.InNo == preInDetailObj.InNo &&
                                                                                                               p.InSeq == preInDetailObj.InSeq &&
                                                                                                               p.ItemCode == sItemcode &&
                                                                                                               p.InLotNo == each.IN_LOT_NO).ToList();

                                        if(tempArr1 != null)
                                            if(tempArr1.Count > 0)
                                            {
                                                // 20210622 오세완 차장 박차장님이 이 컬럼으로 포장에서 자동출고 조회하기 때문에 다사용하면 처리
                                                TN_PUR1201 predtlobj = tempArr1.FirstOrDefault();
                                                predtlobj.ReOutYn = "N";
                                                ModelService.UpdateChild<TN_PUR1201>(predtlobj);
                                            }
                                    }

                                    dCalQty -= newdtlobj.OutQty;
                                    if (dCalQty <= 0)
                                        bBreak = true;

                                    // 20210624 오세완 차장 왠지 여기서 처리해야 1이상일때 오류가 안생길 듯
                                    ModelService.Save();
                                }

                            if (bBreak)
                                break;
                        }
                    }
            }
        }

        /// <summary>
        /// 20210611 오세완 차장
        /// 원자재 전용 교체로 변경
        /// </summary>
        private void Btn_SrcChange_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;
            
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.Value_1, tx_ProductLotNo.EditValue.GetNullToEmpty());
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFSRCIN_CHANGE_DAEYOUNG, param, SrcChangeCallback);
            form.ShowPopup(true);
            
        }

        /// <summary>
        /// 20210611 오세완 차장 
        /// 이동표 전용 교체로 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ItemMoveDoc_Change_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null)
                return;
            
            if(obj.ProcessSeq > 1)
            {
                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.KeyValue, obj);
                param.SetValue(PopupParameter.Value_1, tx_ProductLotNo.EditValue.GetNullToEmpty());
                param.SetValue(PopupParameter.Value_2, tx_ItemMoveNoEnd.EditValue.GetNullToEmpty());
                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVESCAN_CHANGE, param, ItemMoveNoChangeCallback);
                form.ShowPopup(true);
            }
        }

        /// <summary>
        /// 원소재 교체 CallBack
        /// </summary>
        private void SrcChangeCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            var srcItemCode = e.Map.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            var srcOutLotNo = e.Map.GetValue(PopupParameter.Value_2).GetNullToEmpty();
            //var machineCode = e.Map.GetValue(PopupParameter.Value_3).GetNullToEmpty();
            var machineCode = obj.MachineCode; // 20220107 오세완 차장 팝업에서 설비코드를 보내지 않는데 받아서 수정 처리

            string productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();

            var workingDate = DateTime.Today;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
                var MachineCode = new SqlParameter("@MachineCode", machineCode);
                var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                var ProcessCode = new SqlParameter("@ProcessCode", obj.ProcessCode);
                var SrcItemCode = new SqlParameter("@SrcItemCode", srcItemCode);
                var SrcOutLotNo = new SqlParameter("@SrcOutLotNo", srcOutLotNo);
                var ProductLotNo = new SqlParameter("@ProductLotNo", productLotNo);
                var WorkingDate = new SqlParameter("@WorkingDate", workingDate);
                var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC_V2 @WorkNo, @MachineCode, @ItemCode , @ProcessCode, @SrcItemCode, @SrcOutLotNo, @ProductLotNo, @WorkingDate, @LoginId"
                                                                        , WorkNo, MachineCode, ItemCode, ProcessCode, SrcItemCode, SrcOutLotNo, ProductLotNo, WorkingDate, LoginId).SingleOrDefault();
                
            }

            if (productLotNo.IsNullOrEmpty())
                return;

            if (productLotNo != tx_ProductLotNo.EditValue.GetNullToEmpty())
            {
                if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_72), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

                #region 이전 작업실적관리 마스터 UPDATE
                var TN_MPS1201_Previous = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();

                // 20220113 오세완 차장 실적값을 제대로 조회 못한 채로 와서 날짜만 변경했는데 실적까지 변경이 되서 프로시저로 대처
                //TN_MPS1201_Previous.ResultDate = DateTime.Today;
                //TN_MPS1201_Previous.ResultEndDate = DateTime.Now;
                //ModelService.Update(TN_MPS1201_Previous);

                string sSql_Upd = "exec USP_UPD_MPS1201T_PLC_POP '" + obj.WorkNo + "', '" + obj.ProcessCode + "', " + obj.ProcessSeq.ToString() + ", '" + GlobalVariable.LoginId + "' ";
                int iUpd = DbRequestHandler.SetDataQury(sSql_Upd);

                #endregion

                var TN_MPS1201_Check = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ProductLotNo == productLotNo).LastOrDefault();
                if (TN_MPS1201_Check != null)
                {
                    TN_MPS1201_Check.ResultDate = null;
                    TN_MPS1201_Check.ResultEndDate = null;
                }
                else
                {
                    #region 작업실적관리 마스터 INSERT
                    var TN_MPS1201_NewObj = new TN_MPS1201();
                    TN_MPS1201_NewObj.WorkNo = obj.WorkNo;
                    TN_MPS1201_NewObj.ProcessCode = obj.ProcessCode;
                    TN_MPS1201_NewObj.ProcessSeq = obj.ProcessSeq;
                    TN_MPS1201_NewObj.ProductLotNo = productLotNo;
                    TN_MPS1201_NewObj.ItemCode = obj.ItemCode;
                    TN_MPS1201_NewObj.CustomerCode = obj.CustomerCode;
                    TN_MPS1201_NewObj.MachineCode = machineCode;
                    TN_MPS1201_NewObj.ResultStartDate = DateTime.Now;
                    TN_MPS1201_NewObj.ResultSumQty = 0;
                    TN_MPS1201_NewObj.OkSumQty = 0;
                    TN_MPS1201_NewObj.BadSumQty = 0;
                    TN_MPS1201_NewObj.Temp = TN_MPS1201_Previous.Temp; // 20220110 오세완 차장 타발수 때문에 인식한 금형 정보를 넘겨줘야 함
                    ModelService.Insert(TN_MPS1201_NewObj);
                    #endregion
                }

                var ItemMoveLastObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == TN_MPS1201_Previous.ItemMoveNo).FirstOrDefault();
                // 이동표번호가 없는 경우
                if (ItemMoveLastObj == null)
                {
                    NewItemMovePrint(TN_MPS1201_Previous.OkSumQty.GetDecimalNullToZero(), obj, TN_MPS1201_Previous.TN_MPS1200, TN_MPS1201_Previous);
                }
                // 이동표번호가 있으나 그 이후 새로 실적을 등록했을 경우
                else if (TN_MPS1201_Previous.TN_MPS1202List.Any(p => p.ItemMoveNo == null))
                {
                    NewItemMovePrint(TN_MPS1201_Previous.OkSumQty.GetDecimalNullToZero() - ItemMoveLastObj.OkSumQty.GetDecimalNullToZero(), obj, TN_MPS1201_Previous.TN_MPS1200, TN_MPS1201_Previous);
                }

                // 20210714 오세완 차장 이동표가 바뀌면 plc실적 리셋한다. 
                //plc_Modbus.Reset_Count();
                // 20210721 오세완 차장 초기화 방법 변경
                Plc_Modbus_ResetCount();
            } 

            ActRefresh();
        }

        /// <summary>
        /// 원자재 교체 시 이동표 새 출력 함수
        /// </summary>
        private void NewItemMovePrint(decimal boxInQty, TEMP_XFPOP1000 obj, TN_MPS1200 TN_MPS1200, TN_MPS1201 TN_MPS1201)
        {
            var itemMoveNo = DbRequestHandler.GetItemMoveSeq(obj.WorkNo);

            // 20220113 오세완 차장 이동표 번호 갱신 하려다 실적이 날아가 버려서 프로시저로 교체
            //TN_MPS1201.ItemMoveNo = itemMoveNo;
            string sSql_Upd = "exec USP_UPD_MPS1201T_PLC_POP_ITEMMOVENO '" + obj.WorkNo + "', '" + obj.ProcessCode + "', " + obj.ProcessSeq.ToString() + ", '" + TN_MPS1201.ProductLotNo + "', '" 
                + GlobalVariable.LoginId + "', '" + itemMoveNo + "' ";
            int iUpd = DbRequestHandler.SetDataQury(sSql_Upd);


            decimal resultQty = 0;
            decimal okQty = 0;
            decimal badQty = 0;

            foreach (var v in TN_MPS1201.TN_MPS1202List)
            {
                if (v.ItemMoveNo.IsNullOrEmpty())
                {
                    resultQty += v.ResultQty.GetDecimalNullToZero();
                    okQty += v.OkQty.GetDecimalNullToZero();
                    badQty += v.BadQty.GetDecimalNullToZero();
                    v.ItemMoveNo = itemMoveNo;
                }
            }

            var newItemMoveNo = new TN_ITEM_MOVE();
            newItemMoveNo.ItemMoveNo = itemMoveNo;
            newItemMoveNo.WorkNo = TN_MPS1201.WorkNo;
            newItemMoveNo.ProcessCode = TN_MPS1201.ProcessCode;
            newItemMoveNo.ProcessSeq = TN_MPS1201.ProcessSeq;
            newItemMoveNo.ProductLotNo = TN_MPS1201.ProductLotNo;
            newItemMoveNo.BoxInQty = boxInQty;

            // 20220113 오세완 차장 실적 수량이 조회가 잘 안되서 프로시저로 대처
            //newItemMoveNo.ResultSumQty = TN_MPS1201.ResultSumQty;
            //newItemMoveNo.OkSumQty = TN_MPS1201.OkSumQty;
            //newItemMoveNo.BadSumQty = TN_MPS1201.BadSumQty;
            newItemMoveNo.ResultQty = resultQty;
            newItemMoveNo.OkQty = okQty;
            newItemMoveNo.BadQty = badQty;

            decimal dTemp_ResultSum = 0;
            decimal dTemp_OkSum = 0;
            decimal dTemp_BadSum = 0;

            string sSql_Get = "exec USP_GET_PLC_POP_BEFORE_MPS1201T '" + obj.WorkNo + "', '" + obj.ProcessCode + "', " + obj.ProcessSeq.ToString() + ", '" + TN_MPS1201.ProductLotNo + "' ";
            DataTable dt_Result = DbRequestHandler.GetDataTableSelect(sSql_Get);
            if(dt_Result != null)
                if(dt_Result.Rows.Count > 0)
                {
                    dTemp_ResultSum = dt_Result.Rows[0]["RESULT_SUM_QTY"].GetDecimalNullToZero();
                    dTemp_OkSum = dt_Result.Rows[0]["OK_SUM_QTY"].GetDecimalNullToZero();
                    dTemp_BadSum = dt_Result.Rows[0]["BAD_SUM_QTY"].GetDecimalNullToZero();
                }

            newItemMoveNo.ResultSumQty = dTemp_ResultSum;
            newItemMoveNo.OkSumQty = dTemp_OkSum;
            newItemMoveNo.BadSumQty = dTemp_BadSum;

            ModelService.InsertChild(newItemMoveNo);

            // 20220113 오세완 차장 이동표 번호 갱신 하려다 실적이 날아가 버려서 프로시저로 교체하기 위해 생략
            //TN_MPS1201.ResultDate = DateTime.Today;
            //TN_MPS1201.ResultEndDate = DateTime.Now;

            //ModelService.Update(TN_MPS1201);
            ModelService.Save();

            // 20210621 오세완 차장 원자재 교체 후 이동표 출력기능이 없어서 추가처리
            TEMP_ITEM_MOVE_NO_MASTER masterObj = null;
            List<TEMP_ITEM_MOVE_NO_DETAIL> detailList;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", obj.WorkNo);
                var _ItemMoveNo = new SqlParameter("@ItemMoveNo", itemMoveNo);

                //masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER_V2 @WorkNo", _workNo).Where(p => p.ItemMoveNo == itemMoveNo).FirstOrDefault(); // 20210616 오세완 차장 수동으로 내린 반제품이 출력되기 위해 프로시저 수정본으로 교체
                var vResult = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER_V3 @WorkNo, @ItemMoveNo", _workNo, _ItemMoveNo).ToList(); // 20211126 오세완 차장 일반 pop에서 기능을 개선한 버전으로 통일 처리
                if (vResult != null)
                    masterObj = vResult.FirstOrDefault();
            }

            if (masterObj == null) return;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
                var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

                //detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
                detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL_V2 @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList(); // 20211126 오세완 차장 작업자가 나오지 않는 오류를 수정한 버전
            }

            // 20210618 오세완 차장 유미대리 요청으로 A4크기 품목명도 나오는 부품이동표로 변경
            var ItemMoveNoReport = new XRITEMMOVENO_DAEYOUNG(masterObj, detailList);
            var printTool = new ReportPrintTool(ItemMoveNoReport);
            printTool.ShowPreview();
        }

        /// <summary>
        /// 이동표 교체 CallBack
        /// </summary>
        private void ItemMoveNoChangeCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            var itemMoveNo = e.Map.GetValue(PopupParameter.Value_1).GetNullToNull();
            var productLotNo = e.Map.GetValue(PopupParameter.Value_2).GetNullToEmpty();
            var machineCode = e.Map.GetValue(PopupParameter.Value_3).GetNullToEmpty();

            if (tx_ProductLotNo.EditValue.GetNullToEmpty() != productLotNo)
            {
                if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_74), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }

            #region 이전 작업실적관리 마스터 UPDATE
            var TN_MPS1201_Previous = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
            TN_MPS1201_Previous.ResultDate = DateTime.Today;
            TN_MPS1201_Previous.ResultEndDate = DateTime.Now;
            ModelService.Update(TN_MPS1201_Previous);
            #endregion

            var checkObj = ModelService.GetChildList<TN_MPS1201>(p => p.WorkNo == obj.WorkNo 
                                                                    && p.ProcessCode == obj.ProcessCode 
                                                                    && p.ProcessSeq == obj.ProcessSeq 
                                                                    && p.ProductLotNo == productLotNo).FirstOrDefault();
            if (checkObj != null)
            {
                checkObj.ItemMoveNo = itemMoveNo;
                checkObj.ResultDate = null;
                checkObj.ResultEndDate = null;
                ModelService.Update(checkObj);
            }
            else
            {
                #region 작업실적관리 마스터 INSERT
                var TN_MPS1201_NewObj = new TN_MPS1201();
                TN_MPS1201_NewObj.WorkNo = obj.WorkNo;
                TN_MPS1201_NewObj.ProcessCode = obj.ProcessCode;
                TN_MPS1201_NewObj.ProcessSeq = obj.ProcessSeq;
                TN_MPS1201_NewObj.ItemMoveNo = itemMoveNo;
                TN_MPS1201_NewObj.ProductLotNo = productLotNo;
                TN_MPS1201_NewObj.ItemCode = obj.ItemCode;
                TN_MPS1201_NewObj.CustomerCode = obj.CustomerCode;
                TN_MPS1201_NewObj.MachineCode = machineCode;
                TN_MPS1201_NewObj.ResultStartDate = DateTime.Now;
                TN_MPS1201_NewObj.ResultSumQty = 0;
                TN_MPS1201_NewObj.OkSumQty = 0;
                TN_MPS1201_NewObj.BadSumQty = 0;
                ModelService.Insert(TN_MPS1201_NewObj);
                #endregion
            }

            var checkItemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.WorkNo == obj.WorkNo 
                                                                            && p.ProcessCode == obj.ProcessCode 
                                                                            && p.ProcessSeq == obj.ProcessSeq
                                                                            && p.ProductLotNo == productLotNo
                                                                            && p.ItemMoveNo == itemMoveNo).FirstOrDefault();
            if (checkItemMoveObj == null)
            {
                var ItemMoveFirstObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == itemMoveNo && p.ProcessSeq == 1).First();

                var newItemMoveNo = new TN_ITEM_MOVE();
                newItemMoveNo.ItemMoveNo = itemMoveNo;
                newItemMoveNo.WorkNo = obj.WorkNo;
                newItemMoveNo.ProcessCode = obj.ProcessCode;
                newItemMoveNo.ProcessSeq = obj.ProcessSeq;
                newItemMoveNo.ProductLotNo = productLotNo;
                newItemMoveNo.BoxInQty = ItemMoveFirstObj.BoxInQty.GetDecimalNullToZero();
                newItemMoveNo.ResultSumQty = 0;
                newItemMoveNo.OkSumQty = 0;
                newItemMoveNo.BadSumQty = 0;
                newItemMoveNo.ResultQty = 0;
                newItemMoveNo.OkQty = 0;
                newItemMoveNo.BadQty = 0;
                ModelService.InsertChild(newItemMoveNo);
            }

            ModelService.Save();
            ActRefresh();
        }

        /// <summary>
        /// 비가동
        /// </summary>
        private void Btn_MachineStop_Click(object sender, EventArgs e)
        {
            // 20210603 오세완 차장 비가동TO때문에 작업지시번호가 필요해서 변경
            var vWorkObj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (vWorkObj != null)
            {
                //var stopForm = new POP_POPUP.XPFMACHINESTOP_V2(vWorkObj);
                var stopForm = new POP_POPUP.XPFMACHINESTOP_V2(vWorkObj, true);
                stopForm.ShowDialog();

                ActRefresh();

                if (vWorkObj.ProcessCode == MasterCodeSTR.Process_Press)
                {
                    if(stopForm.STATUS == "Run")
                    {
                        gb_WorkStart = true;
                        btn_Search.Enabled = false;
                    }
                    else if(stopForm.STATUS == "STOP")
                    {
                        gb_WorkStart = false;
                        btn_Search.Enabled = true;
                    }
                }
                    
            }
        }

        /// <summary>
        /// 설비점검
        /// </summary>
        private void Btn_MachineCheck_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            //var machineCheckForm = new POP_POPUP.XPFMACHINECHECK();
            var machineCheckForm = new POP_POPUP.XPFMACHINECHECK(obj); // 20220110 오세완 차장 작업지시 혹은 실적에 맞물려 있는 설비를 먼저 출력해 달라는 신부장님 요청으로 수정
            machineCheckForm.ShowDialog();
        }

        /// <summary>
        /// 이동표출력
        /// </summary>
        private void Btn_ItemMovePrint_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            ItemMovePrint(obj);
        }

        /// <summary>
        /// POP 종료
        /// </summary>
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_30), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
                Close();
        }

        /// <summary>
        /// 이동표 출력 함수
        /// </summary>
        private void ItemMovePrint(TEMP_XFPOP1000 obj)
        {
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.Value_1, tx_ProductLotNo.EditValue.GetNullToEmpty());

            string sSql = "EXEC USP_GET_PLC_POP_CURRENT_QTY '" + obj.WorkNo + "', '" + obj.ProcessCode + "', " + obj.ProcessSeq.ToString() + ", '" + tx_ProductLotNo.EditValue.GetNullToEmpty() + "' ";
            string sResult = DbRequestHandler.GetCellValue(sSql, 0);
            param.SetValue(PopupParameter.Value_2, tx_SumResultQty.EditValue.GetDecimalNullToZero());
            param.SetValue(PopupParameter.Value_2, sResult.GetDecimalNullToZero());
            param.SetValue(PopupParameter.Value_3, "Press"); // 20210819 오세완 차장 프레스 pop에서 부품이동표를 다른 형식으로 출력해야 해서 추가 처리

            sSql = "EXEC USP_GET_POP_PRESS_REST_RESULTQTY '" + obj.WorkNo + "' ";
            string sResult1 = DbRequestHandler.GetCellValue(sSql, 0);
            param.SetValue(PopupParameter.Value_4, sResult1.GetDecimalNullToZero()); // 20210902 오세완 차장 박스당 포장수량 입력후 남은 수량을 합산한 값을 전달
            gb_ShowPopup = true;
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVEPRINT, param, ItemMovePrintCallback);
            form.ShowPopup(true);
        }

        /// <summary>
        /// 이동표 출력 CallBack
        /// </summary>
        private void ItemMovePrintCallback(object sender, PopupArgument e)
        {
            gb_ShowPopup = false;
            if (e == null) return;

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            var itemMoveNo = e.Map.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            if (itemMoveNo.IsNullOrEmpty()) return;

            TEMP_ITEM_MOVE_NO_MASTER masterObj = null;
            List<TEMP_ITEM_MOVE_NO_DETAIL> detailList;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", obj.WorkNo);
                var _ItemMoveNo = new SqlParameter("@ItemMoveNo", itemMoveNo);

                //masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER_V2 @WorkNo", _workNo).Where(p => p.ItemMoveNo == itemMoveNo).FirstOrDefault(); // 20210616 오세완 차장 수동으로 내린 반제품이 출력되기 위해 프로시저 수정본으로 교체
                var vResult = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER_V3 @WorkNo, @ItemMoveNo", _workNo, _ItemMoveNo).ToList(); // 20211126 오세완 차장 일반 POP에서 사용하는 버전으로 교체
                if (vResult != null)
                    masterObj = vResult.FirstOrDefault();
            }

            if (masterObj == null) return;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
                var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

                //detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
                detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL_V2 @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList(); // 20211126 오세완 차장 일반 POP에서 사용하는 버전으로 교체
            }

            string sMoveType = e.Map.GetValue(PopupParameter.Value_2).GetNullToEmpty();

            if(masterObj.BoxInQty <= 1)
            {
                if(sMoveType == "" || sMoveType == "A4")
                {
                    // 20210618 오세완 차장 유미대리 요청으로 A4크기 품목명도 나오는 부품이동표로 변경
                    var ItemMoveNoReport = new XRITEMMOVENO_DAEYOUNG(masterObj, detailList);
                    var printTool = new ReportPrintTool(ItemMoveNoReport);
                    printTool.ShowPreview();
                }
                else
                {
                    // 20210820 오세완 차장 대영 요청으로 프레스 공정만 라벨형태로 공정이동표 발행 처리
                    // 20210827 오세완 차장 수량 출력까지 고려
                    //var vBarReprot = new XRITEMMOVEDOC_PRESS(masterObj);

                    TEMP_ITEM_MOVE_NO_DETAIL tempDetail = detailList.Where(p => p.ProcessCode == MasterCodeSTR.Process_Press).FirstOrDefault();
                    if(tempDetail != null)
                    {
                        decimal dPerBoxQty = 0;
                        string sPrintType = e.Map.GetValue(PopupParameter.KeyValue).GetNullToEmpty(); // // 20210830 오세완 차장 공정박스당 수량 재발생시 처리
                        if (sPrintType == "Re")
                        {
                            string sSql = "EXEC USP_GET_PLC_POP_BARCODE_PER_BOX_QTY '" + masterObj.ItemMoveNo + "' ";
                            string sResult = DbRequestHandler.GetCellValue(sSql, 0);
                            dPerBoxQty = sResult.GetDecimalNullToZero();
                        }
                        else
                            dPerBoxQty = e.Map.GetValue(PopupParameter.Value_3).GetDecimalNullToZero(); // 20210830 오세완 차장 공정박스당 수량 출력

                        var vBarReprot = new XRITEMMOVEDOC_PRESS(masterObj.ItemMoveNo, masterObj.ItemCode, dPerBoxQty);
                        var vPrinttool = new ReportPrintTool(vBarReprot);
                        vPrinttool.ShowPreview();
                    }
                }
            }
            else
            {
                if (sMoveType == "" || sMoveType == "A4")
                {
                    // 20210627 오세완 차장 이사님 요청대로 박스수량만큼 이동표 출력으로 변경
                    // 20210905 오세완 차장 신부장님이 a4는 한장만 출력해달라고 했던 것이 기억이 나서 한장만 출력하는 것으로 수정
                    //var printMulti = new REPORT.XRITEMMOVENO_DAEYOUNG();
                    //for (int i = 0; i < Convert.ToInt32(masterObj.BoxInQty); i++)
                    //{
                    //    var printEach = new REPORT.XRITEMMOVENO_DAEYOUNG(masterObj, detailList);
                    //    printEach.CreateDocument();
                    //    printMulti.Pages.AddRange(printEach.Pages);
                    //}

                    //printMulti.PrintingSystem.ShowMarginsWarning = false;
                    //printMulti.ShowPrintStatusDialog = false;
                    //printMulti.ShowPreview();

                    var ItemMoveNoReport = new XRITEMMOVENO_DAEYOUNG(masterObj, detailList);
                    var printTool = new ReportPrintTool(ItemMoveNoReport);
                    printTool.ShowPreview();
                }
                else
                {
                    // 20210820 오세완 차장 대영 요청으로 프레스 공정만 라벨형태로 공정이동표 발행 처리
                    var vPrintmulti = new XRITEMMOVEDOC_PRESS();
                    for (int j = 0; j < Convert.ToInt32(masterObj.BoxInQty); j++)
                    {
                        TEMP_ITEM_MOVE_NO_DETAIL tempDetail1 = detailList.Where(p => p.ProcessCode == MasterCodeSTR.Process_Press).FirstOrDefault();
                        if (tempDetail1 != null)
                        {
                            //var vPrinteach = new XRITEMMOVEDOC_PRESS(masterObj);
                            decimal dPerBoxQty1 = 0;
                            string sPrintType = e.Map.GetValue(PopupParameter.KeyValue).GetNullToEmpty(); // // 20210830 오세완 차장 공정박스당 수량 재발생시 처리
                            if (sPrintType == "Re")
                            {
                                string sSql = "EXEC USP_GET_PLC_POP_BARCODE_PER_BOX_QTY '" + masterObj.ItemMoveNo + "' ";
                                string sResult = DbRequestHandler.GetCellValue(sSql, 0);
                                dPerBoxQty1 = sResult.GetDecimalNullToZero();
                            }
                            else
                                dPerBoxQty1 = e.Map.GetValue(PopupParameter.Value_3).GetDecimalNullToZero(); // 20210830 오세완 차장 공정박스당 수량 출력

                            var vPrinteach = new XRITEMMOVEDOC_PRESS(masterObj.ItemMoveNo, masterObj.ItemCode, dPerBoxQty1);
                            vPrinteach.CreateDocument();
                            vPrintmulti.Pages.AddRange(vPrinteach.Pages);
                        }
                    }

                    // 20210907 오세완 차장 박스에 정량화되지 못한 수량은 +1처리하여 발행하는 로직으로 처리하기로 이사님이 지시함
                    decimal dOkQty = (decimal)detailList.Where(p => p.ProcessCode == obj.ProcessCode).FirstOrDefault().OkQty;
                    decimal dDiffQty = dOkQty - (masterObj.PerBoxQty.GetDecimalNullToZero() * (decimal)masterObj.BoxInQty);
                    if(dDiffQty > 0)
                    {
                        var vPrintAdd = new XRITEMMOVEDOC_PRESS(masterObj.ItemMoveNo, masterObj.ItemCode, dDiffQty);
                        vPrintAdd.CreateDocument();
                        vPrintmulti.Pages.AddRange(vPrintAdd.Pages);
                    }

                    vPrintmulti.PrintingSystem.ShowMarginsWarning = false;
                    vPrintmulti.ShowPrintStatusDialog = false;
                    vPrintmulti.ShowPreview();
                }
                
            }
            
            ActRefresh();
        }

        private void MainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                var check = View.GetRowCellValue(e.RowHandle, View.Columns["EmergencyFlag"]).ToString();
                if (check == "Y")
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
                else
                {
                    var jobStates = View.GetRowCellValue(e.RowHandle, View.Columns["JobStates"]).ToString();
                    if (jobStates == MasterCodeSTR.JobStates_Start)
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                    }
                }
            }
        }

        // 20210520 오세완 차장 대영에는 작업설정검사가 없어서 임시 생략 처리
        //private bool CheckJobSetting()
        //{
        //    var obj = GridBindingSource.Current as TEMP_XFPOP1000;
        //    if (obj == null)
        //    {
        //        return false;
        //    }

        //    TN_STD1000 jobSettingFlag = DbRequestHandler.GetCommMainCode(MasterCodeSTR.JobSettingFlag).FirstOrDefault();
        //    if (jobSettingFlag != null)
        //    {
        //        var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).FirstOrDefault();
        //        if (TN_MPS1200.JobSettingFlag == "Y") //작업설정검사여부가 체크되어 있을 경우
        //        {
        //            var TN_QCT1100 = ModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == obj.WorkNo && p.WorkSeq == obj.ProcessSeq && p.CheckDivision == MasterCodeSTR.InspectionDivision_Setting).OrderBy(p => p.RowId).LastOrDefault();
        //            if (TN_QCT1100 == null) //작업설정검사가 없을 경우
        //                return false;
        //            else if (TN_QCT1100.CheckResult == "OK") //작업설정검사가 OK일 경우
        //                return true;
        //            else
        //                return false;
        //        }
        //        else
        //            return true;
        //    }
        //    else
        //        return true;
        //}

        private void lcProductLotNo_DoubleClick(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;
            if (obj.ProcessSeq != 1) return;
            if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_120), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
        }

        private void XFPOP1000_V2_FormClosed(object sender, FormClosedEventArgs e)
        {
            MenuOpenLogService.SetCloseMenuLog(DateTime.Now);
            LogFactory.GetLoginLogService().SetLogoutLog(DateTime.Now);
        }

        #region PLC통신 관련
        #region 사용안함
        /// <summary>
        /// 20210715 오세완 차장 연결상태 변경될 때 호출 처리
        /// 20210719 오세완 차장 datareader가 열렸다는 오류가 발생 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Plc_Modbus_OnChangeConnection(object sender, EventArgs e)
        {
            #region PLC 연결 상태 정보 Update
            string sValue = plc_Modbus.ConnectStatus;
            string sMachine = plc_Modbus.MachineMCode;
            List<TN_MEA1600> plc_Arr = ModelService.GetChildList<TN_MEA1600>(p => p.MachineCode == sMachine);
            TN_MEA1600 plc_Each = null;
            if (plc_Arr != null)
                if (plc_Arr.Count > 0)
                {
                    plc_Each = plc_Arr.FirstOrDefault();
                }

            if (plc_Each != null)
            {
                plc_Each.Connection = sValue;
                plc_Each.UpdateId = HKInc.Utils.Common.GlobalVariable.LoginId;
                plc_Each.UpdateTime = DateTime.Now;
                ModelService.UpdateChild<TN_MEA1600>(plc_Each);
                ModelService.Save();
            }
            #endregion
        }

        /// <summary>
        /// 20210719 오세완 차장
        /// ModelService를 다른 table로 해도, 동일한 것으로 사용해도 오류가 발생
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Plc_Modbus_OnChangeConnection_UseTack(object sender, EventArgs e)
        {
            #region PLC 연결 상태 정보 Update
            string sValue = plc_Modbus.ConnectStatus;
            string sMachine = plc_Modbus.MachineMCode;

            //await Task.Factory.StartNew(new Action(() => {
            //    List<TN_MEA1600> plc_Arr = ModelService_Plc.GetList(p => p.MachineCode == sMachine);
            //    TN_MEA1600 plc_Each = null;
            //    if (plc_Arr != null)
            //        if (plc_Arr.Count > 0)
            //        {
            //            plc_Each = plc_Arr.FirstOrDefault();
            //        }

            //    if (plc_Each != null)
            //    {
            //        plc_Each.Connection = sValue;
            //        plc_Each.UpdateId = HKInc.Utils.Common.GlobalVariable.LoginId;
            //        plc_Each.UpdateTime = DateTime.Now;
            //        ModelService_Plc.Update(plc_Each);
            //        ModelService_Plc.Save();
            //    }
            //}));
            
            #endregion
        }

        /// <summary>
        /// 20210716 오세완 차장 이번전으로 명일 테스트해볼 것
        /// 20210719 오세완 차장 제대로 lock이 되지 않는다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Plc_Modbus_OnChangeConnection_UseLock(object sender, EventArgs e)
        {
            #region PLC 연결 상태 정보 Update
            string sValue = plc_Modbus.ConnectStatus;
            string sMachine = plc_Modbus.MachineMCode;
            lock(oLock)
            {
                while (bLockCount == true)
                    Monitor.Wait(oLock);

                bLockCount = true;
                List<TN_MEA1600> plc_Arr = ModelService.GetChildList<TN_MEA1600>(p => p.MachineCode == sMachine).ToList();
                TN_MEA1600 plc_Each = null;
                if (plc_Arr != null)
                    if (plc_Arr.Count > 0)
                    {
                        plc_Each = plc_Arr.FirstOrDefault();
                    }

                if (plc_Each != null)
                {
                    plc_Each.Connection = sValue;
                    plc_Each.UpdateId = HKInc.Utils.Common.GlobalVariable.LoginId;
                    plc_Each.UpdateTime = DateTime.Now;
                    ModelService.UpdateChild<TN_MEA1600>(plc_Each);
                    ModelService.Save();
                    // 20210721 오세완 차장 trandaction을 해결하기 위해 core단을 건드렸으나 실패하여 사용안함
                    //ModelService.Save_V2();
                }
                bLockCount = false;
                Monitor.Pulse(oLock);
            }
            
            #endregion
        }

        /// <summary>
        /// 20210715 오세완 차장 Update주기에 맞는 시간일 때 호출됨
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Plc_Modbus_OnUpdate(object sender, EventArgs e)
        {
            int iResult_Productcnt = plc_Modbus.PlcCount;
            string sMachine = plc_Modbus.MachineMCode;
            if (plc_Modbus.ConnectStatus == "Connect")
            {
                // 20210715 오세완 차장 연결된 상태로 간주한다. 

                #region PLC 현황 테이블에 실적 수량 및 가동상태 update
                List<TN_MEA1600> plc_Arr = ModelService.GetChildList<TN_MEA1600>(p => p.MachineCode == sMachine);
                TN_MEA1600 plc_Each = null;
                if (plc_Arr != null)
                    if (plc_Arr.Count > 0)
                    {
                        plc_Each = plc_Arr.FirstOrDefault();
                    }

                int iPLC_Count = 0;
                if (plc_Each != null)
                {
                    if (plc_Each.Count != null)
                        iPLC_Count = (int)plc_Each.Count;
                }

                DateTime dtPrevTime;
                DateTime dtNow = DateTime.Now;

                if (plc_Each != null)
                {
                    if (iPLC_Count == iResult_Productcnt)
                    {
                        // 실적이 장시간 안들어 온 경우 판단
                        if (plc_Each.CountTime == null && plc_Each.PrevCountTime == null)
                            plc_Each.RunStatus = "STOP";
                        else
                        {
                            dtPrevTime = (DateTime)plc_Each.CountTime;
                            TimeSpan tsDiff1 = dtNow - dtPrevTime;
                            if (tsDiff1.TotalMilliseconds > 600000)
                                plc_Each.RunStatus = "STOP"; // 10분 이상 실적 없으면 비가동처리 
                        }
                    }
                    else
                    {
                        plc_Each.Count = iResult_Productcnt;
                        if (iResult_Productcnt == 1)
                        {
                            plc_Each.CountTime = DateTime.Now;
                            plc_Each.RunStatus = "RUN";
                        }
                        else if (iResult_Productcnt > 1)
                        {
                            if (plc_Each.CountTime != null)
                            {
                                dtPrevTime = (DateTime)plc_Each.CountTime;
                                plc_Each.PrevCountTime = dtPrevTime;
                                plc_Each.CountTime = dtNow;
                                plc_Each.RunStatus = "RUN";
                            }
                            else
                            {
                                // 중간서 부터 시작한 경우에 대한 초기값 설정 처리
                                plc_Each.CountTime = DateTime.Now;
                                plc_Each.RunStatus = "RUN";
                            }
                        }
                    }

                    ModelService.UpdateChild<TN_MEA1600>(plc_Each);
                    ModelService.Save();
                }
                #endregion

                #region 실적처리
                if (plc_Each != null)
                {
                    if (plc_Each.RunStatus == "RUN")
                    {
                        TEMP_XFPOP1000 tObj = GridBindingSource.Current as TEMP_XFPOP1000;
                        if (tObj != null)
                        {
                            if (tObj.JobStates == MasterCodeSTR.JobStates_Start)
                            {
                                // 작업지시 디테일 갱신
                                TN_MPS1201 mps1201_ord = ModelService.GetList(p => p.WorkNo == tObj.WorkNo &&
                                                                              p.ProcessCode == tObj.ProcessCode &&
                                                                              p.ProductLotNo == tx_ProductLotNo.EditValue.ToString()).OrderBy(o => o.ProcessSeq).LastOrDefault();
                                if (mps1201_ord != null)
                                {
                                    // 중간에 사용자가 임의로 입력한 실적을 반영하기 위함
                                    mps1201_ord.PlcCount = plc_Each.Count;
                                    int iWorkerInputResult = mps1201_ord.WorkerInputResultQty == null ? 0 : (int)mps1201_ord.WorkerInputResultQty;
                                    mps1201_ord.ResultSumQty = iWorkerInputResult + plc_Each.Count;
                                    int iWorkerInputOk = mps1201_ord.WorkerInputOkQty == null ? 0 : (int)mps1201_ord.WorkerInputOkQty;
                                    mps1201_ord.OkSumQty = iWorkerInputOk + plc_Each.Count;
                                    mps1201_ord.MachineCode = sMachine;
                                    mps1201_ord.ResultEndDate = DateTime.Now;
                                    mps1201_ord.ResultDate = DateTime.Now;
                                    ModelService.Update(mps1201_ord);
                                    ModelService.Save();

                                    // mps1405가 entity로 제어가 안되어서 변경처리
                                    string sResult_Sql = "EXEC USP_PLC_POP_RESULT_CU '" + tObj.WorkNo + "', '" + tObj.ProcessCode + "', '" + tx_ProductLotNo.EditValue.GetNullToEmpty()
                                                        + "', " + tObj.ProcessSeq + ", '" + sMachine + "', '" + HKInc.Ui.Model.BaseDomain.GsValue.UserId + "' ";
                                    int k = DbRequestHandler.SetDataQury(sResult_Sql);
                                }

                                // 비가동인 경우 설비를 가동상태로 변경처리
                                List<TN_MEA1004> mea1004_stop1 = ModelService.GetChildList<TN_MEA1004>(p => p.MachineCode == sMachine &&
                                                                                                       p.StopStartTime.HasValue &&
                                                                                                       !p.StopEndTime.HasValue);
                                bool bInsert1 = false;
                                if (mea1004_stop1 == null)
                                    bInsert1 = true;
                                else if (mea1004_stop1.Count == 0)
                                    bInsert1 = true;

                                if (!bInsert1)
                                    DbRequestHandler.GetCellValue("exec USP_SET_MACHINE_STOP_TO_RUN '" + sMachine + "'", 0);
                            }
                        }
                    }
                }
                #endregion

                #region 비가동인 경우 비가동 내역 추가 
                if (plc_Each != null)
                {
                    if (plc_Each.RunStatus == "STOP")
                    {
                        List<TN_MEA1004> mea1004_stop = ModelService.GetChildList<TN_MEA1004>(p => p.MachineCode == sMachine &&
                                                                                              p.StopStartTime.HasValue &&
                                                                                              !p.StopEndTime.HasValue);
                        bool bInsert = false;
                        if (mea1004_stop == null)
                            bInsert = true;
                        else if (mea1004_stop.Count == 0)
                            bInsert = true;

                        if (bInsert)
                        {
                            TN_MEA1004 mea1004_obj = new TN_MEA1004()
                            {
                                StopStartTime = DateTime.Now,
                                MachineCode = sMachine,
                                StopCode = ""
                            };

                            ModelService.InsertChild<TN_MEA1004>(mea1004_obj);
                            ModelService.Save();
                        }
                    }
                }
                #endregion
            }
        }

        #endregion

        /// <summary>
        /// 20210719 오세완 차장 
        /// threading timer내에서 datareader 오류나 transaction 오류를 일으키지 않는 형태
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Plc_Modbus_OnChangeConnection_UseProcedure(object sender, EventArgs e)
        {
            string sValue = plc_Modbus.ConnectStatus;
            string sMachine = plc_Modbus.MachineMCode;
            //string sSql = "EXEC USP_SET_POP_RESULT_IU '" + sMachine + "', '" + sValue + "', '0', '" + GlobalVariable.LoginId + "' ";

            // 20210915 오세완 차장 비가동처리때문에 호출방식 변경
            // 20210916 오세완 차장 배포 때문에 V2를 생성
            string sSql = "EXEC USP_SET_POP_RESULT_IU_V2 '" + sMachine + "', '" + sValue + "', '0', '" + GlobalVariable.LoginId + "', '', '', 0 ";

            try
            {
                int iResult = DbRequestHandler.SetDataQury(sSql);
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// 20210721 오세완 차장
        /// threading timer내에서 datareader 오류나 transaction 오류를 일으키지 않는 형태
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Plc_Modbus_OnUpdate_UseProcedure(object sender, EventArgs e)
        {
            int iResult_Productcnt = plc_Modbus.PlcCount;
            string sMachine = plc_Modbus.MachineMCode;
            // 20210719 오세완 차장 tn_mea1600에 실적 카운트 정보 udpate, 비가동인 경우는 tn_mea1004에 insert
            //string sSql = "EXEC USP_SET_POP_RESULT_IU '" + sMachine + "', '', '" + iResult_Productcnt.ToString() + "', '" + GlobalVariable.LoginId + "' ";
            // 20210915 오세완 차장 작업지시와 같이 동작하는 것으로 변경
            // 20210916 오세완 차장 배포 때문에 V2를 생성
            string sSql = "";
            TEMP_XFPOP1000 tObj = GridBindingSource.Current as TEMP_XFPOP1000;
            if( tObj != null)
                sSql = "EXEC USP_SET_POP_RESULT_IU_V2 '" + sMachine + "', '', '" + iResult_Productcnt.ToString() + "', '" + GlobalVariable.LoginId + "', '" + tObj.WorkNo + "', '" + tObj.ProcessCode + "', " + tObj.ProcessSeq.ToString();

            try
            {
                //int iResult = DbRequestHandler.SetDataQury(sSql);
                string sResult = DbRequestHandler.GetCellValue(sSql, 0); // 20210914 오세완 차장 특정 작업지시를 비가동 처리하기 위해 실행 상태 확인
                if(sResult == "STOP")
                {
                    // 20220126 오세완 차장 자동으로 비가동 처리를 해재해야 하는데 USP_SET_POP_RESULT_IU_V2에 아예 처리를 넣어놔서 여기도 생략을 해야 함
                    //sSql = "EXEC USP_UPD_XPFMACHINESTOP_V2 '" + sResult + "','" + sMachine + "' ";
                    //int iResult2 = DbRequestHandler.SetDataQury(sSql);

                    //DataLoad_Grid();
                }
                else
                {
                    //TEMP_XFPOP1000 tObj = GridBindingSource.Current as TEMP_XFPOP1000;
                    if (tObj != null)
                    {
                        if (tObj.JobStates == MasterCodeSTR.JobStates_Start)
                        {
                            // 20210906 오세완 차장 실적하고 부품이동표 하고 차이가 나는 상황이 발생해서 부품이동표 발행과 관련된 팝업 화면인 경우에는 실적을 변경 못하게 수정
                            if (!gb_ShowPopup)
                            {
                                // 20210719 오세완 차장 mps1201 update, USP_PLC_POP_RESULT_CU 실행하여 mps1202 insert
                                sSql = "EXEC USP_SET_PLC_RESULT_MPS1201_1202 '" + tObj.WorkNo + "', '" + tObj.ProcessCode + "', '" + tx_ProductLotNo.EditValue.GetNullToEmpty() + "', " +
                                    tObj.ProcessSeq.ToString() + ", '" + sMachine + "', '" + GlobalVariable.LoginId + "' "; // 20210811 오세완 차장 USERID -> LOGINID 변경

                                //iResult = DbRequestHandler.SetDataQury(sSql);
                                int iResult = DbRequestHandler.SetDataQury(sSql);

                                //RowChange();
                                QtyChange(tObj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 20210721 오세완 차장
        /// 설비 count 및 상태 table 초기화
        /// </summary>
        private void Plc_Modbus_ResetCount()
        {
            plc_Modbus.Reset_Count();
            string sMachine = plc_Modbus.MachineMCode;
            string sSql = "exec USP_UPD_POP_PLC_RESET_MEA1600T '" + sMachine + "', '" + GlobalVariable.LoginId + "' "; // 20210811 오세완 차장 USERID -> LOGINID 변경
            try
            {
                // 20210721 오세완 차장 PLC 상태 table 초기화 추가
                int iResult = DbRequestHandler.SetDataQury(sSql);
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// 20210722 오세완 차장 
        /// 통신 설정 팝업 출력 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Plc_Modbus_DoubleClick(object sender, EventArgs e)
        {
            Form fPopup = new POP_POPUP.XPFPLCINFO_V2();
            DialogResult dr = fPopup.ShowDialog();
            if(dr == DialogResult.OK)
            {
                string sMessage = "설정을 변경하시면 프로그램을 재시작 하셔야 합니다.";
                MessageBoxHandler.Show(sMessage);
            }
        }

        /// <summary>
        /// 20210726 오세완 차장 
        /// 테스트용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lcItemMoveNoEnd_DoubleClick(object sender, EventArgs e)
        {
            plc_Modbus.Reset_Count();
        }

        #endregion
    }
}

