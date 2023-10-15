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

namespace HKInc.Ui.View.View.POP
{
    /// <summary>
    /// 리워크POP
    /// </summary>
    public partial class XFPOP_REWORK : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1205> ModelService = (IService<TN_MPS1205>)ProductionFactory.GetDomainService("TN_MPS1205");
        List<TN_STD1000> processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);

        public XFPOP_REWORK()
        {
            InitializeComponent();

            gridEx1.ViewType = GridViewType.POP_GridView;

            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            gridEx1.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            gridEx1.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;

            tx_WorkNo.Click += Tx_WorkNo_Click;
            tx_WorkNo.KeyDown += Tx_WorkNo_KeyDown;

            btn_Search.Click += Btn_Search_Click;
            btn_Up.Click += Btn_Up_Click;
            btn_Down.Click += Btn_Down_Click;
            pic_WorkStandardDocument.DoubleClick += Pic_WorkStandardDocument_DoubleClick;
            pic_DesignFileName.DoubleClick += Pic_DesignFileName_DoubleClick;
            btn_WorkStart.Click += Btn_WorkStart_Click;
            btn_ResultAdd.Click += Btn_ResultAdd_Click;
            btn_WorkEnd.Click += Btn_WorkEnd_Click;
            btn_ItemMovePrint.Click += Btn_ItemMovePrint_Click;
            btn_Exit.Click += Btn_Exit_Click;
            pdf_Design.DocumentChanged += Pdf_Design_DocumentChanged;
            pdf_Design.DoubleClick += Pdf_Design_DoubleClick;
            pdf_Design.NavigationPaneInitialVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;
            pdf_Design.CursorMode = DevExpress.XtraPdfViewer.PdfCursorMode.HandTool;
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ProcessCode")
            {
                var processCode = gridEx1.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ProcessCode").GetNullToEmpty();
            }
        }


        private void Tx_WorkNo_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad) return;
            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                tx_WorkNo.EditValue = keyPad.returnval;
                Tx_WorkNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
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
        }

        protected override void InitCombo()
        {
            SetButtonEnable(null);

            lup_ProcTeamCode.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Process.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            lup_ProcTeamCode.SetFontSize(new Font("맑은 고딕", 12f));
            lup_Process.SetFontSize(new Font("맑은 고딕", 12f));
            lup_Item.SetFontSize(new Font("맑은 고딕", 12f));

            lup_ProcTeamCode.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_Process.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_Item.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            lup_ProcTeamCode.Properties.View.OptionsView.ShowColumnHeaders = false;
            lup_Item.Properties.View.OptionsView.ShowColumnHeaders = false;

            lup_ProcTeamCode.Properties.View.Columns["CodeVal"].Visible = false;
            lup_Item.Properties.View.Columns["ItemCode"].Visible = false;
            lup_Process.Properties.View.Columns["CodeVal"].Visible = false;
            InitButtonLabelConvert();

        }

        private void InitButtonLabelConvert()
        {
            btn_Search.Text = LabelConvert.GetLabelText("Refresh");
            btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
            btn_ResultAdd.Text = LabelConvert.GetLabelText("ResultAdd");
            btn_WorkEnd.Text = LabelConvert.GetLabelText("WorkEnd");
            btn_ItemMovePrint.Text = LabelConvert.GetLabelText("ItemMovePrint");
            btn_Exit.Text = LabelConvert.GetLabelText("Close");
            
            lcDesign2.Text = lcDesign.Text;
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            gridEx1.MainGrid.AddColumn("EmergencyFlag", LabelConvert.GetLabelText("EmergencyFlag"), false);
            gridEx1.MainGrid.AddColumn("JobStates", LabelConvert.GetLabelText("JobStates"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));            
            gridEx1.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd"); 
            gridEx1.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("Process"));
            gridEx1.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            gridEx1.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            gridEx1.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            gridEx1.MainGrid.AddColumn("ProcessPackQty", LabelConvert.GetLabelText("ProcessPackQty"), HorzAlignment.Far, FormatType.Numeric, "#,0");
            gridEx1.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStates", DbRequestHandler.GetCommTopCode(MasterCodeSTR.JobStates), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
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

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var ProcTeamCode = new SqlParameter("@ProcTeamCode", lup_ProcTeamCode.EditValue.GetNullToEmpty());
                var ProcessCode = new SqlParameter("@ProcessCode", lup_Process.EditValue.GetNullToEmpty());
                var ItemCode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
                var WorkNo = new SqlParameter("@WorkNo", tx_WorkNo.EditValue.GetNullToEmpty().ToUpper());

                var result = context.Database.SqlQuery<TEMP_XFPOP1000>("USP_GET_XFPOP_REWORK_LIST @ProcTeamCode, @ProcessCode, @ItemCode ,@WorkNo"
                                                                                , ProcTeamCode, ProcessCode, ItemCode, WorkNo)
                                                                                .ToList();
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

            TN_STD1300 banBomObj = null;
            var wanBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == obj.ItemCode).FirstOrDefault();
            if (wanBomObj != null)
            {
                banBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ParentBomCode == wanBomObj.BomCode).FirstOrDefault();
            }
            
            ModelService.ReLoad();

            //마지막 진행 실적 가져오기 (실적종료시간이 없는 경우)
            var TN_MPS1205 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
            if (TN_MPS1205 != null)
            {
                tx_ProductLotNo.EditValue = TN_MPS1205.ProductLotNo;
                tx_ResultQty.EditValue = TN_MPS1205.ResultSumQty.GetDecimalNullToZero().ToString("#,0.##");
                tx_OkQty.EditValue = TN_MPS1205.OkSumQty.GetDecimalNullToZero().ToString("#,0.##");
                tx_BadQty.EditValue = TN_MPS1205.BadSumQty.GetDecimalNullToZero().ToString("#,0.##");

                //decimal BadQty = 0;
                //decimal LossQty = 0;
                                
                //var BadTypeList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP);
                //foreach (var v in TN_MPS1205.TN_MPS1202List.Where(p => !p.BadType.IsNullOrEmpty()).ToList())
                //{
                //    var checkObj = BadTypeList.Where(p => p.CodeVal == v.BadType).FirstOrDefault();
                //    if (checkObj != null)
                //    {
                //        if (checkObj.Memo == "N")
                //        {
                //            LossQty += v.BadQty.GetDecimalNullToZero();
                //        }
                //        else
                //        {
                //            BadQty += v.BadQty.GetDecimalNullToZero();
                //        }
                //    }
                //}

                //tx_BadQty.EditValue = BadQty.ToString("#,0.##");
                //tx_LossQty.EditValue = LossQty.ToString("#,0.##");

                var sumObj = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).ToList();
                tx_SumResultQty.EditValue = sumObj.Sum(p => p.ResultSumQty).GetDecimalNullToZero().ToString("#,0.##");
            }
            else
            {
                SetRefreshControl();
            }

            lcItemMoveNoEnd.Text = LabelConvert.GetLabelText("ItemMoveNoNow");
            //현 이동표번호 가져오기
            tx_ItemMoveNoEnd.EditValue = TN_MPS1205 == null ? string.Empty : TN_MPS1205.ItemMoveNo;

            //작업표준서
            if (!obj.WorkStandardDocumentUrl.IsNullOrEmpty())
            {
                pic_WorkStandardDocument.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.WorkStandardDocumentUrl);
            }
            else
            {
                pic_WorkStandardDocument.EditValue = null;
            }

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
        }

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
            TN_STD1000 jobSettingFlag = DbRequestHandler.GetCommMainCode(MasterCodeSTR.JobSettingFlag).FirstOrDefault();

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;

            if (jobStates == MasterCodeSTR.JobStates_Wait) //대기
            {
                btn_WorkStart.Enabled = true;
                btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                btn_ResultAdd.Enabled = false;
                btn_WorkEnd.Enabled = false;
                btn_ItemMovePrint.Enabled = false;
                btn_Exit.Enabled = true;

                if (obj.ProcessSeq > 1)
                {
                    btn_ItemMovePrint.Enabled = true;
                }
            }
            else if (jobStates == MasterCodeSTR.JobStates_Start || jobStates == MasterCodeSTR.JobStates_ReworkStart) //진행
            {
                btn_WorkStart.Enabled = false;
                btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                btn_ResultAdd.Enabled = true;
                btn_WorkEnd.Enabled = true;
                btn_ItemMovePrint.Enabled = true;
                btn_Exit.Enabled = true;
            }
            else if (jobStates == MasterCodeSTR.JobStates_Pause) //일시정지
            {
                btn_WorkStart.Enabled = true;
                btn_WorkStart.Text = LabelConvert.GetLabelText("Restart");
                btn_ResultAdd.Enabled = false;
                btn_WorkEnd.Enabled = false;
                btn_ItemMovePrint.Enabled = false;
                btn_Exit.Enabled = true;

                if (obj.ProcessSeq > 1)
                    btn_ItemMovePrint.Enabled = true;
            }
            else
            {
                btn_WorkStart.Enabled = false;
                btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                btn_ResultAdd.Enabled = false;
                btn_WorkEnd.Enabled = false;
                btn_ItemMovePrint.Enabled = false;
                btn_Exit.Enabled = true;
            }
        }

        /// <summary>
        /// 작업시작
        /// </summary>
        private void Btn_WorkStart_Click(object sender, EventArgs e)
        {
            //리워크 작업시작

            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFREWORK_START, param, WorkStartReworkCallback);
            form.ShowPopup(true);

        }

        /// <summary>
        /// 작업 시작 CallBack
        /// </summary>
        private void WorkStartReworkCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            var productLotNo = ModelService.GetChildList<TN_LOT_MST>(p => p.WorkNo == obj.WorkNo).First().ProductLotNo;
            var workingDate = DateTime.Today;

            if (productLotNo.IsNullOrEmpty())
                return;

            #region 작업실적관리 마스터 INSERT
            var TN_MPS1205_NewObj = new TN_MPS1205();
            TN_MPS1205_NewObj.WorkNo = obj.WorkNo;
            TN_MPS1205_NewObj.ProcessCode = obj.ProcessCode;
            TN_MPS1205_NewObj.ProcessSeq = obj.ProcessSeq;
            TN_MPS1205_NewObj.ProductLotNo = productLotNo;
            TN_MPS1205_NewObj.ItemCode = obj.ItemCode;
            TN_MPS1205_NewObj.CustomerCode = obj.CustomerCode;
            TN_MPS1205_NewObj.ResultStartDate = DateTime.Now;
            TN_MPS1205_NewObj.ResultSumQty = 0;
            TN_MPS1205_NewObj.OkSumQty = 0;
            TN_MPS1205_NewObj.BadSumQty = 0;
            ModelService.Insert(TN_MPS1205_NewObj);
            #endregion

            //작업지시서 상태 변경
            var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();

            TN_MPS1200.ReworkJobStates = MasterCodeSTR.JobStates_ReworkStart;
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
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_REWORK, param, ResultAddCallback);
            form.ShowPopup(true);
        }

        /// <summary>
        /// 실적등록 CallBack
        /// </summary>
        private void ResultAddCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

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
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFWORKEND_REWORK, param, WorkEndCallback);
            form.ShowPopup(true);
        }

        /// <summary>
        /// 작업 종료 시 CallBack
        /// </summary>
        private void WorkEndCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            //if(e.Map.ContainsKey(PopupParameter.Constraint)) //새출력 시 
            //{
            //    var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            //    if (obj == null) return;

            //    var itemMoveNo = e.Map.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            //    if (itemMoveNo.IsNullOrEmpty()) return;

            //    TEMP_ITEM_MOVE_NO_MASTER masterObj;
            //    List<TEMP_ITEM_MOVE_NO_DETAIL> detailList;

            //    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            //    {
            //        context.Database.CommandTimeout = 0;
            //        var _workNo = new SqlParameter("@WorkNo", obj.WorkNo);
            //        var _ProcessCode = new SqlParameter("@ProcessCode", obj.ProcessCode);
            //        masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER_REWORK @WorkNo, @ProcessCode", _workNo, _ProcessCode).Where(p => p.ItemMoveNo == itemMoveNo).FirstOrDefault();
            //    }

            //    if (masterObj == null) return;

            //    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            //    {
            //        context.Database.CommandTimeout = 0;
            //        var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
            //        var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

            //        detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
            //    }

            //    var ItemMoveNoReport = new XRITEMMOVENO(masterObj, detailList);
            //    var printTool = new ReportPrintTool(ItemMoveNoReport);
            //    printTool.ShowPreview();
            //}          

            ActRefresh();
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
                var _ProcessCode = new SqlParameter("ProcessCode", obj.ProcessCode);
                masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER_REWORK @WorkNo, @ProcessCode", _workNo, _ProcessCode).Where(p => p.ItemMoveNo == itemMoveNo).FirstOrDefault();
            }

            if (masterObj == null) return;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
                var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

                detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
            }

            var ItemMoveNoReport = new XRITEMMOVENO_S(masterObj, detailList);
            var printTool = new ReportPrintTool(ItemMoveNoReport);
            printTool.ShowPreview();

            ActRefresh();
        }

        private void MainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                //var check = View.GetRowCellValue(e.RowHandle, View.Columns["EmergencyFlag"]).ToString();
                //if (check == "Y")
                //{
                //    e.Appearance.BackColor = Color.Red;
                //    e.Appearance.ForeColor = Color.White;
                //}
                //else
                //{
                    var jobStates = View.GetRowCellValue(e.RowHandle, View.Columns["JobStates"]).ToString();
                    if (jobStates == MasterCodeSTR.JobStates_Start || jobStates == MasterCodeSTR.JobStates_ReworkStart)
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                    }
                //}
            }
        }

        private bool CheckJobSetting()
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null)
            {
                return false;
            }

            TN_STD1000 jobSettingFlag = DbRequestHandler.GetCommMainCode(MasterCodeSTR.JobSettingFlag).FirstOrDefault();
            if (jobSettingFlag != null)
            {
                var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).FirstOrDefault();
                if (TN_MPS1200.JobSettingFlag == "Y") //작업설정검사여부가 체크되어 있을 경우
                {
                    var TN_QCT1100 = ModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == obj.WorkNo && p.WorkSeq == obj.ProcessSeq && p.CheckDivision == MasterCodeSTR.InspectionDivision_Setting).OrderBy(p => p.RowId).LastOrDefault();
                    if (TN_QCT1100 == null) //작업설정검사가 없을 경우
                        return false;
                    else if (TN_QCT1100.CheckResult == "OK") //작업설정검사가 OK일 경우
                        return true;
                    else
                        return false;
                }
                else
                    return true;
            }
            else
                return true;
        }
    }
}

