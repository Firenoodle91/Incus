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
using DevExpress.XtraLayout.Utils;


namespace HKInc.Ui.View.View.POP
{
    /// <summary>
    /// POP
    /// </summary>
    public partial class XFPOP1000 : HKInc.Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");
        IService<TN_MPS1000> ModelService2 = (IService<TN_MPS1000>)ProductionFactory.GetDomainService("TN_MPS1000");
        IService<TN_STD1600> ModelService3 = (IService<TN_STD1600>)ProductionFactory.GetDomainService("TN_STD1600");
        IService<TN_TOOL1101> MachineToolModel = (IService<TN_TOOL1101>)ProductionFactory.GetDomainService("TN_TOOL1101");

        List<TN_STD1000> processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process).Where(p => p.UseYN =="Y").ToList();
        List<TN_STD1000> surfaceList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.SurfaceList);

        BindingSource ToolGridBindings = new BindingSource();

        #endregion

        public XFPOP1000()
        {
            LogFactory.GetLoginLogService().SetLoginLog(DateTime.Now);
            MenuOpenLogService.SetOpenMenuLog(DateTime.Now, 9999);
            InitializeComponent();
           
            gridEx1.ViewType = GridViewType.POP_GridView;

            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            gridEx1.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            //gridEx1.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;

            gridEx2.MainGrid.MainView.RowCellStyle += ToolView_RowCellStyle;

            gridEx2.MainGrid.MainView.OptionsView.EnableAppearanceEvenRow = false;
            gridEx2.MainGrid.MainView.OptionsView.EnableAppearanceOddRow = false;

            //gridControl.EmbeddedNavigator.BackColor = Color.White;
            //gridEx2.MainGrid.UseEmbeddedNavigator.BackColor = Color.White;



            tx_WorkNo.KeyDown += Tx_WorkNo_KeyDown;
            tx_WorkNo.Click += Tx_WorkNo_Click;
            btn_Search.Click += Btn_Search_Click;

            btn_Up.Click += Btn_Up_Click; ;
            btn_Down.Click += Btn_Down_Click;

            pic_WorkStandardDocument.DoubleClick += Pic_WorkStandardDocument_DoubleClick;
            pic_DesignFileName.DoubleClick += Pic_DesignFileName_DoubleClick;

            btn_WorkStart.Click += Btn_WorkStart_Click;
            btn_ResultAdd.Click += Btn_ResultAdd_Click;
            //btn_ItemMovePrint.Click += Btn_ItemMovePrint_Click;
            btn_QualityAdd.Click += Btn_QualityAdd_Click;
            //btn_AndonCall.Click += Btn_QualityAdd_Click;
            btn_WorkEnd.Click += Btn_WorkEnd_Click;

            btn_MachineStop.Click += Btn_MachineStop_Click;
            //btn_ItemMoveDoc_Change.Click += Btn_ItemMoveDoc_Change_Click;
            btn_MachineCheck.Click += Btn_MachineCheck_Click;
            //btn_ToolChange.Click += Btn_QualityAdd_Click;
            btn_ToolChange.Click += btn_ToolChange_Click;
            btn_Exit.Click += Btn_Exit_Click;

            btn_AndonCall.Click += btn_AndonCall_Click;

            if (GlobalVariable.DefaultPOPType == "POP")
            {
                lup_Machine.EditValue = HKInc.Ui.Model.BaseDomain.GsValue.MachineCode;
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

            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
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

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            SetButtonEnable(null);

            lup_Process.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Machine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            List<TN_STD1100> tempArr = ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList(); 
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), tempArr, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

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
        }

        private void InitButtonLabelConvert()
        {
            btn_Search.Text = LabelConvert.GetLabelText("Refresh");
            //btn_RestartTO.Text = LabelConvert.GetLabelText("RestartTO"); // 20210602 오세완 차장 재가동TO
            btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
            btn_ResultAdd.Text = LabelConvert.GetLabelText("ResultAdd");
            btn_QualityAdd.Text = LabelConvert.GetLabelText("QualityAdd");
            btn_WorkEnd.Text = LabelConvert.GetLabelText("WorkEnd");
            btn_MachineStop.Text = LabelConvert.GetLabelText("MachineStop");
            btn_MachineCheck.Text = LabelConvert.GetLabelText("MachineCheck");
            //btn_ItemMovePrint.Text = LabelConvert.GetLabelText("ItemMovePrint");
            btn_Exit.Text = LabelConvert.GetLabelText("Close");
            //btn_ReworkResultAdd.Text = LabelConvert.GetLabelText("ReworkResultAdd"); // 20210602 오세완 차장 리워크실적등록
            //btn_ItemMoveDoc_Change.Text = LabelConvert.GetLabelText("ItemMoveChange"); // 20210611 오세완 차장 이동표교체 전용 버튼 추가

        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            
            gridEx1.MainGrid.AddColumn("JobStates", LabelConvert.GetLabelText("JobStates"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("EmergencyFlag", LabelConvert.GetLabelText("EmergencyFlag"), false);
            gridEx1.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            gridEx1.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            gridEx1.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("Process"));
            gridEx1.MainGrid.AddColumn("MachineCode2", LabelConvert.GetLabelText("Machine"));
            gridEx1.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            gridEx1.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));

            gridEx1.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            gridEx1.MainGrid.AddColumn("StartDueDate", LabelConvert.GetLabelText("StartDueDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd", false);
            gridEx1.MainGrid.AddColumn("TopCategoryName", LabelConvert.GetLabelText("TopCategory"), false);
            gridEx1.MainGrid.AddColumn("ProcessPackQty", LabelConvert.GetLabelText("ProcessPackQty"), false);
            gridEx1.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;

            gridEx2.SetToolbarVisible(false);
            gridEx2.MainGrid.AddColumn("ToolCode", "공구명", HorzAlignment.Center, true);
            gridEx2.MainGrid.AddColumn("LifeCNT", "남은수명", HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            gridEx2.MainGrid.SetGridFont(gridEx2.MainGrid.MainView, new Font("맑은 고딕", 11f));
            gridEx2.MainGrid.MainView.ColumnPanelRowHeight = 20;
            gridEx2.MainGrid.MainView.RowHeight = 20;

        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("EmergencyFlag", "N");
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", "MachineCode");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode2", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStates", DbRequestHandler.GetCommTopCode(MasterCodeSTR.JobStates), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.BestFitColumns();

            gridEx2.MainGrid.SetRepositoryItemSearchLookUpEdit("ToolCode", ModelService.GetChildList<TN_TOOL1000>(p => true).ToList(), "ToolCode", "ToolName");
            gridEx2.MainGrid.BestFitColumns();
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
            
            InitRepository();
            InitCombo();

            #region 대영
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                var ProcessCode = new SqlParameter("@ProcessCode", lup_Process.EditValue.GetNullToEmpty());
                var MachineCode = new SqlParameter("@MachineCode", lup_Machine.EditValue.GetNullToEmpty());
                var ItemCode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
                var WorkNo = new SqlParameter("@WorkNo", tx_WorkNo.EditValue.GetNullToEmpty().ToUpper());

                var result = context.Database.SqlQuery<TEMP_XFPOP1000>("USP_GET_XFPOP1000_LIST @ProcessCode, @MachineCode, @ItemCode, @WorkNo", ProcessCode, MachineCode, ItemCode, WorkNo).ToList();
                GridBindingSource.DataSource = result.OrderByDescending(p => p.EmergencyFlag).ThenBy(p => p.WorkDate).ThenBy(p => p.WorkNo).ToList();


                if (result.Count == 0)
                {
                    SetRefreshControl();
                }
            }
            #endregion

            #region JI
            /*
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {


                var process = new SqlParameter("@process", lup_Process.EditValue.GetNullToEmpty());
                var mccode = new SqlParameter("@mccode", lup_Machine.EditValue.GetNullToEmpty());
                var itemcode = new SqlParameter("@itemcode", lup_Item.EditValue.GetNullToEmpty());
                var wkno = new SqlParameter("@wkpno", tx_WorkNo.EditValue.GetNullToEmpty());
                var mcgcode = new SqlParameter("@mcgroup", lup_Item.EditValue.GetNullToEmpty());
                var result = context.Database
                      .SqlQuery<TP_POPJOBLIST>("SP_POP_JOBLIST_NEW1 @process,@mccode ,@itemcode ,@wkpno,@mcgroup", process, mccode, itemcode, wkno, mcgcode).OrderBy(p => p.WorkNo).ToList();
                GridBindingSource.DataSource = result.OrderBy(o => o.PSeq).ToList();

                if (result.Count == 0)
                {
                    SetRefreshControl();
                }

            }
            */
            #endregion

            gridEx1.DataSource = GridBindingSource;
            
            if (string.IsNullOrEmpty(keyFieldName) || keyValue == null)
                gridEx1.MainGrid.MainView.FocusedRowHandle = currentRow;
            else
                gridEx1.MainGrid.MainView.FocusedRowHandle = gridEx1.MainGrid.MainView.LocateByValue(keyFieldName, keyValue);

            gridEx1.BestFitColumns();

            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "Refresh");
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //try
            //{
            //    WaitHandler.ShowWait();
            //    RowChange();
            //}
            //finally { WaitHandler.CloseWait(); }

            RowChange();
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
            MachineToolModel.ReLoad();

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

            #region 대영 FTP 작업표준서
            /*
            //작업표준서
            if (!obj.WorkStandardDocumentUrl.IsNullOrEmpty())
                pic_WorkStandardDocument.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.WorkStandardDocumentUrl);
            else
                pic_WorkStandardDocument.EditValue = null;
            */


            /*
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
            */
            #endregion


            //작업표준서
            var Jobstd = ModelService2.GetList(p => p.ItemCode == obj.ItemCode && p.ProcessCode == obj.ProcessCode).LastOrDefault();
            pic_WorkStandardDocument.EditValue = Jobstd == null ? null : Jobstd.FileData;

            //도면
            var DomapObj = ModelService3.GetList(p => p.ItemCode == obj.ItemCode).OrderBy(p => p.Seq).LastOrDefault();
            pic_DesignFileName.EditValue = DomapObj == null ? null : DomapObj.DesignMap;

            //string type = DomapObj.DesignFile.ToUpper();
            //if(type.Contains(".PDF"))
            //{
            //
            //}


            //공구 수명 확인
            //진행 중인 지시만 확인 가능
            if (obj.JobStates != MasterCodeSTR.JobStates_Start)
            {
                //btn_ToolChange.Enabled = false;
                return;
            }
            //btn_ToolChange.Enabled = true;

            var ToolObj = MachineToolModel.GetList(p => p.WorkNo == obj.WorkNo
                                                && p.MachineCode ==obj.MachineCode
                                                && p.ItemCode == obj.ItemCode
                                                && p.ProcessCode == obj.ProcessCode)
                                                .OrderBy(p => p.ToolCode).ToList();

            ToolGridBindings.DataSource = ToolObj;
            gridEx2.DataSource = ToolGridBindings;
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
            gridEx2.DataSource = null;
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
            btn_Exit.Enabled = true;

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;

            bool machinechk = false;

            if (obj != null)
                machinechk = obj.MachineCode.IsNullOrEmpty();

            if (obj == null)
            {
                btn_WorkStart.Enabled = false;
                btn_ResultAdd.Enabled = false;
                btn_QualityAdd.Enabled = false;
                btn_WorkEnd.Enabled = false;
                btn_MachineStop.Enabled = false;
                btn_MachineCheck.Enabled = true;
                //btn_ItemMovePrint.Enabled = false;
                //btn_ItemMoveDoc_Change.Enabled = false; 
            }
            else
            {
                string sFlag_RestartTO = "";
                TN_MPS1000 temp_mps = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == obj.ItemCode &&
                                                                                 p.UseFlag == "Y" &&
                                                                                 p.ProcessCode == obj.ProcessCode).FirstOrDefault();
                //if (temp_mps != null)
                //    sFlag_RestartTO = temp_mps.RestartToFlag.GetNullToEmpty();

                if (jobStates == MasterCodeSTR.JobStates_Wait) //대기
                {

                    btn_WorkStart.Enabled = true;
                    btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                    btn_ResultAdd.Enabled = false;
                    btn_QualityAdd.Enabled = false;
                    btn_WorkEnd.Enabled = false;
                    btn_MachineStop.Enabled = false;
                    btn_MachineCheck.Enabled = true;
                    //btn_ItemMovePrint.Enabled = false;
                    //btn_ItemMoveDoc_Change.Enabled = false;
                    btn_ToolChange.Enabled = false;
                    if (obj.ProcessSeq > 1)
                    {
                        //btn_ItemMovePrint.Enabled = true;
                    }
                }
                else if (jobStates == MasterCodeSTR.JobStates_Start) //진행
                {
                    btn_WorkStart.Enabled = false;
                    btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                    btn_ResultAdd.Enabled = true;
                    btn_QualityAdd.Enabled = true;
                    btn_WorkEnd.Enabled = true;

                    // 20210614 오세완 차장 수동관리여부가 지정되지 않은 원자재는 교체할 이유가 없음으로 상태를 확인해서 제어
                    string sSql = "exec USP_GET_XFPOP1000_CHECK_SRC_CHANGE '" + obj.WorkNo + "', '" + obj.ProcessCode + "'";
                    string sValue = DbRequestHandler.GetCellValue(sSql, 0);
                    //////////////////////
                    btn_MachineStop.Enabled = !machinechk;
                    btn_MachineCheck.Enabled = true;
                    //btn_ItemMovePrint.Enabled = true;
                    btn_ToolChange.Enabled = true;

                    /*
                    // 20210611 오세완 차장 이동표교체 전용 버튼 추가하여 첫번째 공정만 빼고 나머지는 원자재 교체 버튼과 동일한 규칙 적용
                    if (obj.ProcessSeq == 1)
                        //btn_ItemMoveDoc_Change.Enabled = false;
                    else
                        //btn_ItemMoveDoc_Change.Enabled = true;
                     */
                }
                else if (jobStates == MasterCodeSTR.JobStates_Pause) //일시정지
                {
                    btn_WorkStart.Enabled = true;
                    //btn_WorkStart.Text = LabelConvert.GetLabelText("Restart");
                    btn_ResultAdd.Enabled = false;
                    btn_QualityAdd.Enabled = false;
                    btn_WorkEnd.Enabled = false;
                    btn_MachineStop.Enabled = false;
                    btn_MachineCheck.Enabled = true;
                    //btn_ItemMovePrint.Enabled = false;
                    //btn_ItemMoveDoc_Change.Enabled = false;
                    btn_ToolChange.Enabled = true;
                    if (obj.ProcessSeq > 1)
                    {
                        //btn_ItemMovePrint.Enabled = true;
                    }
                }
                else if(jobStates == MasterCodeSTR.JobStates_Stop) 
                {

                    btn_WorkStart.Enabled = true;
                    btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                    btn_ResultAdd.Enabled = false;
                    btn_QualityAdd.Enabled = false;
                    btn_WorkEnd.Enabled = false;
                    btn_MachineStop.Enabled = false;
                    btn_MachineCheck.Enabled = true;
                    //btn_ItemMovePrint.Enabled = false;
                    //btn_ItemMoveDoc_Change.Enabled = false;
                    btn_ToolChange.Enabled = false;
                    if (obj.ProcessSeq > 1)
                    {
                        //btn_ItemMovePrint.Enabled = true;
                    }
                }
                else
                {
                    btn_WorkStart.Enabled = false;
                    btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                    btn_ResultAdd.Enabled = false;
                    btn_QualityAdd.Enabled = false;
                    btn_WorkEnd.Enabled = false;
                    btn_MachineStop.Enabled = true;
                    btn_MachineCheck.Enabled = true;
                    //btn_ItemMovePrint.Enabled = false;
                    //btn_ItemMoveDoc_Change.Enabled = false;
                    btn_ToolChange.Enabled = false;
                }
            }
            
        }


        /// <summary>
        /// 작업시작
        /// </summary>
        private void Btn_WorkStart_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            //비가동 조건 추가 - 20210713 김태영
            //
            //비가동, 일시정지 구분 삭제, 비가동으로 일괄 처리
            if (obj.JobStates == MasterCodeSTR.JobStates_Pause || obj.JobStates == MasterCodeSTR.JobStates_Stop)
            {
                var productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();

                var TN_MEA1004 = ModelService.GetChildList<TN_MEA1004>(p => p.WorkNo == obj.WorkNo
                                                                        && p.ProcessCode == obj.ProcessCode
                                                                        && p.MachineCode == obj.MachineCode
                                                                        && p.StopEndTime == null).FirstOrDefault();
                if (TN_MEA1004 != null)
                {
                    TN_MEA1004.StopEndTime = DateTime.Now;
                    TN_MEA1004.UpdateTime = DateTime.Now;
                    ModelService.UpdateChild(TN_MEA1004);
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
            }

            else
            {
                var productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();
                var itemMoveNo = tx_ItemMoveNoEnd.EditValue.GetNullToEmpty();

                var itemcode = obj.ItemCode.GetNullToEmpty();
                var processcode = obj.ProcessCode.GetNullToEmpty();
                var machinecode = obj.MachineCode.GetNullToEmpty();

                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.KeyValue, obj);
                param.SetValue(PopupParameter.Value_1, itemcode);
                param.SetValue(PopupParameter.Value_2, processcode);
                param.SetValue(PopupParameter.Value_3, machinecode);
                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFTOOL_LIFECNT, param, WorkStartCallback);
                form.ShowPopup(true);

                LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
            }
        }

        /// <summary>
        /// 작업 시작 CallBack
        /// </summary>
        private void WorkStartCallback(object sender, PopupArgument e)
        {
            if (e == null)
                return;

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null)
                return;

            var machineCode = e.Map.GetValue(PopupParameter.Value_1);

            var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo 
                                                                     && p.ProcessCode == obj.ProcessCode 
                                                                     && p.ProcessSeq == obj.ProcessSeq)
                                                                     .First();
            string productLotNo = "";

            try
            {
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
                    var MachineCode = new SqlParameter("@MachineCode", machineCode.GetNullToEmpty());
                    var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                    var ProcessCode = new SqlParameter("@ProcessCode", obj.ProcessCode);
                    var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                    //작업지시투입정보 INSERT

                    //productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO @WorkNo, @MachineCode, @ItemCode, @ProcessCode, @LoginId",
                    //    WorkNo, MachineCode, ItemCode, ProcessCode, LoginId).SingleOrDefault();

                    productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO @WorkNo, @MachineCode, @ItemCode, @LoginId",
                        WorkNo, MachineCode, ItemCode, LoginId).SingleOrDefault();
                }

                if (productLotNo.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show("productLotNo SP 확인", "제목");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHandler.Show(ex.Message, "제목");
                return;
            }

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

            ModelService.Insert(TN_MPS1201_NewObj);
            #endregion

            //작업지시서 상태 변경
            TN_MPS1200.JobStates = MasterCodeSTR.JobStates_Start;
            TN_MPS1200.UpdateTime = DateTime.Now;
            ModelService.UpdateChild(TN_MPS1200);
            ModelService.Save();
            ActRefresh();
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
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_DEFAULT, param, ResultAddCallback);
            form.ShowPopup(true);

            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
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

            //var inspectionForm = new POP_POPUP.XPFINSPECTION_V2(obj, tx_ProductLotNo.EditValue.GetNullToEmpty()); // 20210619 오세완 차장 초중종 로직 개선 버전
            var inspectionForm = new POP_POPUP.XPFQCIN(obj, tx_ProductLotNo.EditValue.GetNullToEmpty()); //JI버전

            if (inspectionForm.ShowDialog() != DialogResult.OK) { }
            else
                ActRefresh();

            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
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
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFWORKEND, param, WorkEndCallback);
            form.ShowPopup(true);
        }

        /// <summary>
        /// 작업 종료 시 CallBack
        /// </summary>
        private void WorkEndCallback(object sender, PopupArgument e)
        {
            if (e == null)
                return;

            try
            {
                //새출력 시 
                if (e.Map.ContainsKey(PopupParameter.Constraint))
                {
                    var obj = GridBindingSource.Current as TEMP_XFPOP1000;
                    if (obj == null) return;

                    var itemMoveNo = e.Map.GetValue(PopupParameter.Value_1).GetNullToEmpty();
                    if (itemMoveNo.IsNullOrEmpty()) return;

                    TEMP_ITEM_MOVE_NO_MASTER masterObj;
                    List<TEMP_ITEM_MOVE_NO_DETAIL> detailList;

                    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                    {
                        context.Database.CommandTimeout = 0;
                        var _workNo = new SqlParameter("@WorkNo", obj.WorkNo);
                        masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER @WorkNo", _workNo).Where(p => p.ItemMoveNo == itemMoveNo).FirstOrDefault(); // 20210616 오세완 차장 수동으로 지시한 반제품이 나오기 위해서 프로시저 수정본으로 교체
                    }

                    if (masterObj == null) return;

                    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                    {
                        context.Database.CommandTimeout = 0;
                        var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
                        var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

                        detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
                    }

                    ////출력
                    //var ItemMoveNoReport = new XRITEMMOVENO(masterObj, detailList);
                    //var printTool = new ReportPrintTool(ItemMoveNoReport);
                    //printTool.ShowPreview();
                }
            }
            catch
            {

            }

            finally
            {
                ActRefresh();
            }

            // 20210627 오세완 차장 첫공정이 마지막 공정일 수도 있기 때문에 생략처리
            //TEMP_XFPOP1000 tObj = GridBindingSource.Current as TEMP_XFPOP1000;
            //if (tObj == null)
            //{
            //    ActRefresh();
            //    return;
            //}

            #region 자재 또는 반재품 소요량 차감, 수동으로 관리하지 않는 자재
            //int iProcessMax = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == tObj.WorkNo).Max(m => m.ProcessSeq);
            //if (tObj.ProcessSeq == iProcessMax)
            //{
            //    // 20210622 오세완 차장 완제품은 왠만하면 포장공정을 처리해서 수동관리를 하지 않는 자재를 자동출고 처리하나 혹 그렇지 않은 제품은 여기서 처리 하도록 추가
            //    if (tObj.TopCategoryName == "완제품")
            //    {
            //        List<TN_STD1300> childBomObj_MG_NOT = null;
            //        var wanBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == tObj.ItemCode &&
            //                                                                    p.UseFlag == "Y").FirstOrDefault();
            //        if (wanBomObj != null)
            //        {
            //            // 20210622 오세완 차장 반제품 군은 무조건 종료 전에 처리가 됬다고 가정한다. 
            //            childBomObj_MG_NOT = ModelService.GetChildList<TN_STD1300>(p => p.ParentBomCode == wanBomObj.BomCode &&
            //                                                                            (p.TopCategory != MasterCodeSTR.TopCategory_BAN || p.TopCategory != MasterCodeSTR.TopCategory_BAN_Outsourcing) &&
            //                                                                            //p.MgFlag == "N" &&
            //                                                                            p.UseFlag == "Y").ToList();

            //            if (childBomObj_MG_NOT.Count > 0)
            //            {
            //                foreach (var v in childBomObj_MG_NOT)
            //                {
            //                    AutoOutSrcQty(v.ItemCode, v.UseQty, tObj);
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        using (var context = new HKInc.Ui.Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            //        {
            //            SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", tObj.ItemCode);
            //            var vResult = context.Database.SqlQuery<TEMP_XPFRESULT_BOMLIST_BANPRODUCT>("USP_GET_XPFRESULT_BOMLIST_BANPRODUCT_AUTO @ITEM_CODE", sp_Itemcode).ToList();
            //            if (vResult != null)
            //                if (vResult.Count > 0)
            //                {
            //                    foreach (TEMP_XPFRESULT_BOMLIST_BANPRODUCT each in vResult)
            //                    {
            //                        AutoOutSrcQty(each.ITEM_CODE, each.USE_QTY, tObj);
            //                    }
            //                }
            //        }
            //    }

            //    Combine_LotDtl(tObj.WorkNo, tObj.ProcessCode);

            //}
            #endregion

            //DataLoad();

            //ActRefresh(); 기능 확인 후 재조회 테스트 
            //DataLoad();
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
                                                // 20210627 오세완 차장 이방법은 다른 원자재를 투입한 경우 오류가 발생할 수 있어서 변경
                                                //List<TN_LOT_DTL> dtlArr = mstEach.TN_LOT_DTL_List.Where(p => p.SrcCode == each.ItemCode).OrderBy(p => p.Seq).ToList();
                                                decimal dSeqMax = 0;
                                                //if (dtlArr == null)
                                                //    dSeqMax = 1;
                                                //else
                                                //{
                                                //    if (dtlArr.Count > 0)
                                                //    {
                                                //        TN_LOT_DTL dtlEach = dtlArr.LastOrDefault();
                                                //        dSeqMax = dtlEach.Seq + 1;
                                                //    }
                                                //    else
                                                //        dSeqMax = 1;
                                                //}

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
                                                    var vResult = context.Database.SqlQuery<TEMP_XPFRESULT_BOMLIST_BANPRODUCT>("USP_GET_XPFRESULT_BOMLIST_BANPRODUCT_AUTO @ITEM_CODE", sp_Itemcode).ToList();
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
                                        //InCustomerLotNo = preInDetailObj.InCustomerLotNo,
                                        Memo = pObj.ItemCode + "자동차감출고",
                                        //ReOutYn = "N", //이전 LOT의 출고 막음
                                        //AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
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
                                    //ModelService.InsertChild<TN_PUR1301>(newdtlobj);

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
                                                //predtlobj.ReOutYn = "N";
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

                        //ModelService.Save();
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
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFSRCIN_CHANGE, param, SrcChangeCallback);
            form.ShowPopup(true);

            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
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

                LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
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
            var machineCode = e.Map.GetValue(PopupParameter.Value_3).GetNullToEmpty();

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
                TN_MPS1201_Previous.ResultDate = DateTime.Today;
                TN_MPS1201_Previous.ResultEndDate = DateTime.Now;
                ModelService.Update(TN_MPS1201_Previous);
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

            } 

            ActRefresh();
        }
        
        /// <summary>
        /// 원자재 교체 시 이동표 새 출력 함수
        /// </summary>
        private void NewItemMovePrint(decimal boxInQty, TEMP_XFPOP1000 obj, TN_MPS1200 TN_MPS1200, TN_MPS1201 TN_MPS1201)
        {
            var itemMoveNo = DbRequestHandler.GetItemMoveSeq(obj.WorkNo);

            TN_MPS1201.ItemMoveNo = itemMoveNo;

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
            newItemMoveNo.ResultSumQty = TN_MPS1201.ResultSumQty;
            newItemMoveNo.OkSumQty = TN_MPS1201.OkSumQty;
            newItemMoveNo.BadSumQty = TN_MPS1201.BadSumQty;
            newItemMoveNo.ResultQty = resultQty;
            newItemMoveNo.OkQty = okQty;
            newItemMoveNo.BadQty = badQty;

            ModelService.InsertChild(newItemMoveNo);

            TN_MPS1201.ResultDate = DateTime.Today;
            TN_MPS1201.ResultEndDate = DateTime.Now;

            ModelService.Update(TN_MPS1201);
            ModelService.Save();

            // 20210621 오세완 차장 원자재 교체 후 이동표 출력기능이 없어서 추가처리
            TEMP_ITEM_MOVE_NO_MASTER masterObj;
            List<TEMP_ITEM_MOVE_NO_DETAIL> detailList;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", obj.WorkNo);
                masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER @WorkNo", _workNo).Where(p => p.ItemMoveNo == itemMoveNo).FirstOrDefault(); // 20211108 오세완 차장 USP_GET_ITEM_MOVE_NO_MASTER_V2 -> USP_GET_ITEM_MOVE_NO_MASTER으로 수정
            }

            if (masterObj == null) return;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
                var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

                detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
            }

            // 20210618 오세완 차장 유미대리 요청으로 A4크기 품목명도 나오는 부품이동표로 변경
            //var ItemMoveNoReport = new XRITEMMOVENO_DAEYOUNG(masterObj, detailList);
            var ItemMoveNoReport = new XRITEMMOVENO(masterObj, detailList);
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

            var vWorkObj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (vWorkObj == null) return;

            var stopForm = new POP_POPUP.XPFMACHINESTOP(vWorkObj);
            stopForm.ShowDialog();
            ActRefresh();
            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
            
        }

        /// <summary>
        /// 설비점검
        /// </summary>
        private void Btn_MachineCheck_Click(object sender, EventArgs e)
        {
            var machineCheckForm = new POP_POPUP.XPFMACHINECHECK();
            machineCheckForm.ShowDialog();

            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
        }

        /// <summary>
        /// 이동표출력
        /// </summary>
        private void Btn_ItemMovePrint_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            ItemMovePrint(obj);

            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
        }

        /// <summary>
        /// 공구 교체
        /// </summary>
        private void btn_ToolChange_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFTOOL_CHANGE, param, null);
            form.ShowPopup(true);

            DataLoad();

            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
        }
        private void btn_AndonCall_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFANDON, param, AndonCallBack);
            form.ShowPopup(true);

            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
        }

        private void AndonCallBack(object sender, Utils.Common.PopupArgument e)
        {
            /*
             
            if (e == null) return;

            var returnList = e.Map.GetValue(PopupParameter.ReturnObject);

            MessageBoxHandler.Show(returnList.ToString(), "");

            */

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
            param.SetValue(PopupParameter.Value_2, tx_SumResultQty.EditValue.GetDecimalNullToZero());
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVEPRINT, param, ItemMovePrintCallback);
            form.ShowPopup(true);
        }

        /// <summary>
        /// 이동표 출력 CallBack
        /// </summary>
        private void ItemMovePrintCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            var itemMoveNo = e.Map.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            if (itemMoveNo.IsNullOrEmpty()) return;

            TEMP_ITEM_MOVE_NO_MASTER masterObj;
            List<TEMP_ITEM_MOVE_NO_DETAIL> detailList;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", obj.WorkNo);
                masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER @WorkNo", _workNo).Where(p => p.ItemMoveNo == itemMoveNo).FirstOrDefault(); // 20210616 오세완 차장 수동으로 내린 반제품이 출력되기 위해 프로시저 수정본으로 교체
            }

            if (masterObj == null) return;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
                var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

                detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
            }

            var ItemMoveNoReport = new XRITEMMOVENO(masterObj, detailList);
            var printTool = new ReportPrintTool(ItemMoveNoReport);
            printTool.ShowPreview();

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
        private void ToolView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle < 0) return;

            int cnt = View.GetRowCellValue(e.RowHandle, View.Columns["LifeCNT"]).GetIntNullToZero();

            /*
            if (cnt >= 30)
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.Green;

            else if (1 <= cnt && cnt <= 29)
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.Gold;

            else
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.Red;
             */

            if (11 <= cnt && cnt <= 30)
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.Green;
            else if (1 <= cnt && cnt <= 10)
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.Gold;
            else if (cnt <= 0)
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.Red;
            else
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.Black;


            //e.Appearance.BackColor = Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
        }

        private void lcProductLotNo_DoubleClick(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;
            if (obj.ProcessSeq != 1) return;
            if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_120), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
        }

        private void XFPOP1000_FormClosed(object sender, FormClosedEventArgs e)
        {
            MenuOpenLogService.SetCloseMenuLog(DateTime.Now);
            LogFactory.GetLoginLogService().SetLogoutLog(DateTime.Now);
        }
    }
}

