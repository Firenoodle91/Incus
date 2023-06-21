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
    /// POP
    /// </summary>
    public partial class XFPOP1000 : HKInc.Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");
        List<TN_STD1000> processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
        List<TN_STD1000> surfaceList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.SurfaceList);
        #endregion

        public XFPOP1000()
        {
            LogFactory.GetLoginLogService().SetLoginLog(DateTime.Now);
            MenuOpenLogService.SetOpenMenuLog(DateTime.Now, 9999);
            InitializeComponent();
           
            gridEx1.ViewType = GridViewType.POP_GridView;

            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            gridEx1.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            gridEx1.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;

            tx_WorkNo.KeyDown += Tx_WorkNo_KeyDown;
            tx_WorkNo.Click += Tx_WorkNo_Click;

            btn_Search.Click += Btn_Search_Click;
            btn_Up.Click += Btn_Up_Click; ;
            btn_Down.Click += Btn_Down_Click;
            pic_WorkStandardDocument.DoubleClick += Pic_WorkStandardDocument_DoubleClick;
            pic_DesignFileName.DoubleClick += Pic_DesignFileName_DoubleClick;
            btn_WorkStart.Click += Btn_WorkStart_Click;
            btn_ResultAdd.Click += Btn_ResultAdd_Click;
            btn_QualityAdd.Click += Btn_QualityAdd_Click;
            btn_WorkEnd.Click += Btn_WorkEnd_Click;
            btn_MachineStop.Click += Btn_MachineStop_Click;
            btn_MachineCheck.Click += Btn_MachineCheck_Click;
            btn_Exit.Click += Btn_Exit_Click;

            pdf_Design.DocumentChanged += Pdf_Design_DocumentChanged;
            pdf_Design.DoubleClick += Pdf_Design_DoubleClick;
            pdf_Design.NavigationPaneInitialVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;
            pdf_Design.CursorMode = DevExpress.XtraPdfViewer.PdfCursorMode.HandTool;
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
        }

        protected override void InitCombo()
        {
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

            lup_Machine.Properties.View.Columns["MachineMCode"].Visible = false;
            lup_Process.Properties.View.Columns["CodeVal"].Visible = false;

            InitButtonLabelConvert();
        }

        private void InitButtonLabelConvert()
        {
            btn_Search.Text = LabelConvert.GetLabelText("Refresh");
            btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
            btn_ResultAdd.Text = LabelConvert.GetLabelText("ResultAdd");
            btn_QualityAdd.Text = LabelConvert.GetLabelText("QualityAdd");
            btn_WorkEnd.Text = LabelConvert.GetLabelText("WorkEnd");
            btn_MachineStop.Text = LabelConvert.GetLabelText("MachineStop");
            btn_MachineCheck.Text = LabelConvert.GetLabelText("MachineCheck");
            btn_Exit.Text = LabelConvert.GetLabelText("Close");

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

            gridEx1.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            gridEx1.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));            
            gridEx1.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            gridEx1.MainGrid.AddColumn("StartDueDate", LabelConvert.GetLabelText("StartDueDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");

            gridEx1.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("Process"));
            gridEx1.MainGrid.AddColumn("MachineCode2", LabelConvert.GetLabelText("Machine"));
            gridEx1.MainGrid.AddColumn("TopCategoryCode", LabelConvert.GetLabelText("TopCategory"), false);
            gridEx1.MainGrid.AddColumn("TopCategoryName", LabelConvert.GetLabelText("TopCategory"), false);
            gridEx1.MainGrid.AddColumn("ProcessPackQty", LabelConvert.GetLabelText("ProcessPackQty"), false); // 20210603 오세완 차장 생산하는데 필요하다 생각되지 않아서 생략처리

            gridEx1.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
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
                btn_WorkStart.Enabled = false;
                btn_ResultAdd.Enabled = false;
                btn_QualityAdd.Enabled = false;
                btn_WorkEnd.Enabled = false;
                btn_MachineStop.Enabled = true;
                btn_MachineCheck.Enabled = true;
                btn_Exit.Enabled = true;
            }
            else
            {
                TN_MPS1000 temp_mps = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == obj.ItemCode &&
                                                                                 p.UseFlag == "Y" &&
                                                                                 p.ProcessCode == obj.ProcessCode).FirstOrDefault();
                
                if (jobStates == MasterCodeSTR.JobStates_Wait) //대기
                {
                    btn_WorkStart.Enabled = true;
                    btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                    btn_ResultAdd.Enabled = false;
                    btn_QualityAdd.Enabled = false;
                    btn_WorkEnd.Enabled = false;
                    btn_MachineStop.Enabled = true;
                    btn_MachineCheck.Enabled = true;
                    btn_Exit.Enabled = true;
                }
                else if (jobStates == MasterCodeSTR.JobStates_Start) //진행
                {
                    btn_WorkStart.Enabled = false;
                    btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                    btn_ResultAdd.Enabled = true;
                    btn_QualityAdd.Enabled = true;
                    btn_WorkEnd.Enabled = true;
                    btn_MachineStop.Enabled = true;
                    btn_MachineCheck.Enabled = true;
                    btn_Exit.Enabled = true;
                }
                else if (jobStates == MasterCodeSTR.JobStates_Pause) //일시정지
                {
                    btn_WorkStart.Enabled = true;
                    btn_WorkStart.Text = LabelConvert.GetLabelText("Restart");
                    btn_ResultAdd.Enabled = false;
                    btn_QualityAdd.Enabled = false;
                    btn_WorkEnd.Enabled = false;
                    btn_MachineStop.Enabled = true;
                    btn_MachineCheck.Enabled = true;
                    btn_Exit.Enabled = true;
                }
                else if(jobStates == MasterCodeSTR.JobStates_Stop) 
                {
                    btn_WorkStart.Enabled = true;
                    btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                    btn_ResultAdd.Enabled = false;
                    btn_QualityAdd.Enabled = false;
                    btn_WorkEnd.Enabled = false;                    
                    btn_MachineStop.Enabled = true;
                    btn_MachineCheck.Enabled = true;
                    btn_Exit.Enabled = true;
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
                    btn_Exit.Enabled = true;
                }
            }
            
        }

        private void Btn_WorkStart_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            // 20220204 오세완 차장 bom을 구성해야 작업지시를 생성하게 변경해서 이 로직을 제외처리
            //using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            //{
            //    string query = string.Format("USP_GET_BOMLIST_MATERIAL_INFO '{0}','{1}'", obj.ItemCode, obj.WorkNo);
            //    DataSet ds = DbRequestHandler.GetDataQury(query);

            //    if (ds.Tables[0].Rows.Count == 0)
            //    {
            //        //bom 없음
            //        MessageBox.Show("BOM 없음 등록 필요");
            //        return;
            //    }
            //}

            //비가동 조건 추가 - 20210713 김태영
            if (obj.JobStates == MasterCodeSTR.JobStates_Pause || obj.JobStates == MasterCodeSTR.JobStates_Stop)
            {
                if (!obj.MachineCode.IsNullOrEmpty())
                {
                    var nowMachineStateObj = ModelService.GetChildList<VI_XRREP6000_LIST>(p => p.MachineMCode == obj.MachineCode).FirstOrDefault();
                    if (nowMachineStateObj != null)
                    {
                        //대영정밀 설비 중복허용 가능
                        if (nowMachineStateObj.JobStates == MasterCodeSTR.JobStates_Stop)
                        {
                            MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_173));
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
            }
            else
            {
                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Value_1, obj);
                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFWORK_START, param, WorkStartCallback);
                form.ShowPopup(true);
            }

        }

        private void WorkStartCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            if (!e.Map.ContainsKey(PopupParameter.Value_1))
                return;

            DialogResult dialogResult = (DialogResult)e.Map.GetValue(PopupParameter.Value_1);
            var obj = (TEMP_XFPOP1000)e.Map.GetValue(PopupParameter.Value_2);

            if (dialogResult == DialogResult.OK)
            {
                var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();
                string productLotNo = "";
                var workingDate = DateTime.Today;

                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
                    var MachineCode = new SqlParameter("@MachineCode", string.Empty);
                    var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                    var WorkingDate = new SqlParameter("@WorkingDate", workingDate);
                    var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                    //작업지시투입정보 INSERT
                    productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC_V3 @WorkNo, @MachineCode, @ItemCode, @WorkingDate, @LoginId"
                                                                    , WorkNo, MachineCode, ItemCode, WorkingDate, LoginId).SingleOrDefault();
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
                TN_MPS1201_NewObj.MachineCode = obj.MachineCode2;
                TN_MPS1201_NewObj.Temp = obj.Temp;

                if (obj.ProcessSeq == 1)
                {
                    TN_MPS1201_NewObj.ItemMoveNo = DbRequestHandler.GetItemMoveSeq(obj.WorkNo);
                }
                else
                {
                    var prvObj = ModelService.GetChildList<TN_MPS1201>(x => x.WorkNo == obj.WorkNo && x.ProcessSeq == obj.ProcessSeq - 1).FirstOrDefault();
                    TN_MPS1201_NewObj.ItemMoveNo = prvObj.ItemMoveNo.GetNullToNull();
                }

                //// 20210701 오세완 차장 작업지작시 선택된 설비코드를 반영못하는 오류 수정
                //if (machineCode != null)
                //    if (machineCode.GetNullToEmpty() != "")
                //        TN_MPS1201_NewObj.MachineCode = machineCode.GetNullToEmpty();
                //    else
                //        TN_MPS1201_NewObj.MachineCode = obj.MachineCode; // 20210625 오세완 차장 시작전에 설비를 입력하니까 실적도 입력하는 것으로 변경처리

                //// 20210612 오세완 차장 주성 스타일처럼 트리거를 쓰지않고 해보기 위해서 금형코드를 매칭해 봄
                //if (obj.ProcessCode == MasterCodeSTR.Process_Press)
                //    TN_MPS1201_NewObj.Temp = e.Map.GetValue(PopupParameter.Value_3).GetNullToEmpty();

                ModelService.Insert(TN_MPS1201_NewObj);
                #endregion

                //작업지시서 상태 변경
                TN_MPS1200.JobStates = MasterCodeSTR.JobStates_Start;
                TN_MPS1200.UpdateTime = DateTime.Now;
                if (obj.Temp != null)
                    TN_MPS1200.Temp = obj.Temp;

                ModelService.UpdateChild(TN_MPS1200);
                ModelService.Save();
                ActRefresh();
            }


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
            param.SetValue(PopupParameter.Value_3, obj.ItemCode);
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

            var inspectionForm = new POP_POPUP.XPFINSPECTION_V2(obj, tx_ProductLotNo.EditValue.GetNullToEmpty()); // 20210619 오세완 차장 초중종 로직 개선 버전
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

        private void WorkEndCallback(object sender, PopupArgument e)
        {
            if (e == null)
                return;

            //종료하는 작업지시 정보
            if (!e.Map.ContainsKey(PopupParameter.Value_2))
                return;

            TEMP_XFPOP1000 endWorkInfo = (TEMP_XFPOP1000)e.Map.GetValue(PopupParameter.Value_2);
            string productionLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                string query = string.Format("USP_GET_BOMLIST_MATERIAL_INFO '{0}', '{1}'", endWorkInfo.ItemCode, endWorkInfo.WorkNo);
                DataSet ds = DbRequestHandler.GetDataQury(query);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    //bom 없음
                    //자동출고 및 lot_dtl 데이터가 없어 추적이 불가능함.
                    //MessageBox.Show("등록된 BOM이 없음 확인 필요");
                    //return;
                }
                
                //생산수량은 굳이 DB안들려도 가져올수 있음
                decimal resultQty = tx_ResultQty.EditValue.GetDecimalNullToZero();
                //ModelService.GetChildList<TN_MPS1201>(x => x.WorkNo == endWorkInfo.WorkNo && x.ProcessCode == endWorkInfo.ProcessCode && x.ProcessSeq == endWorkInfo.ProcessSeq).FirstOrDefault();

                //BOM 등록한 UseQty(생산품)
                decimal bomMakeQty = 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    TEMP_XFPOP_BOMLIST bomInfo = new TEMP_XFPOP_BOMLIST();
                    bomInfo.BomCode = row.ItemArray[0].GetNullToEmpty();
                    bomInfo.ItemCode = row.ItemArray[1].GetNullToEmpty();
                    bomInfo.TopCategory = row.ItemArray[2].GetNullToEmpty();
                    bomInfo.UseQty = row.ItemArray[3].GetNullToZero();
                    bomInfo.ProcessCode = row.ItemArray[4].GetNullToEmpty();
                    bomInfo.MgFlag = row.ItemArray[5].GetNullToEmpty();
                    bomInfo.BomTyp = row.ItemArray[6].GetNullToEmpty();

                    if (bomInfo.BomTyp == "MAKE")
                        bomMakeQty = bomInfo.UseQty;


                    //재료bomList중 해당 공정이고 수동관리여부N인것(자동출고)
                    if (endWorkInfo.ProcessCode == bomInfo.ProcessCode && bomInfo.MgFlag == "N")
                    {
                        // 안료? g계산으로 인해 비율계산(?) 처리 ((생산실적 / 생산bom수량) * 자재소요량)
                        decimal totalUseQty = ((resultQty / bomMakeQty) * bomInfo.UseQty).GetNullToZero();

                        List<string> outLotNoList = new List<string>();
                        //자동출고

                        //재료품이 반제품일경우
                        if (bomInfo.TopCategory == MasterCodeSTR.TopCategory_BAN)
                        {
                            outLotNoList = AutoOutSrc_Ban(bomInfo.ItemCode, totalUseQty, endWorkInfo.WorkNo);
                        }
                        else
                        {
                            outLotNoList = AutoOutSrc(bomInfo.ItemCode, totalUseQty, endWorkInfo.WorkNo);
                        }

                        //LOT_DTL 추가
                        //소요량 추가
                        var lotMst = ModelService.GetChildList<TN_LOT_MST>(x => x.WorkNo == endWorkInfo.WorkNo && x.ProductLotNo == productionLotNo).FirstOrDefault();

                        foreach (string srcLotNo in outLotNoList.Distinct())
                        {
                            TN_LOT_DTL newLotDtl = new TN_LOT_DTL();
                            newLotDtl.WorkNo = endWorkInfo.WorkNo;
                            newLotDtl.ProductLotNo = productionLotNo;
                            newLotDtl.Seq = lotMst.TN_LOT_DTL_List.Count == 0 ? 1 : lotMst.TN_LOT_DTL_List.Max(m => m.Seq) + 1;
                            newLotDtl.ItemCode = endWorkInfo.ItemCode;
                            newLotDtl.MachineCode = endWorkInfo.MachineCode2;
                            newLotDtl.ProcessCode = endWorkInfo.ProcessCode;
                            newLotDtl.SrcCode = bomInfo.ItemCode;
                            newLotDtl.SrcInLotNo = srcLotNo;
                            newLotDtl.WorkingDate = DateTime.Today;

                            ModelService.InsertChild<TN_LOT_DTL>(newLotDtl);

                            TN_SRC1000 newSrc = new TN_SRC1000();
                            newSrc.WorkNo = endWorkInfo.WorkNo;
                            newSrc.ProductLotNo = productionLotNo;
                            newSrc.ParentSeq = newLotDtl.Seq;
                            newSrc.Seq = newLotDtl.TN_SRC1000List.Count == 0 ? 1 : newLotDtl.TN_SRC1000List.Max(m => m.Seq) + 1;
                            newSrc.SrcInLotNo = srcLotNo;
                            newSrc.SpendQty = totalUseQty;

                            ModelService.InsertChild<TN_SRC1000>(newSrc);

                            ModelService.Save();
                        }
                    }
                }
            }

            // 20220204 오세완 차장 반제품인 경우 마지막에 입고처리 하기 위해 로직을 변경
            if(endWorkInfo != null)
            {
                int iMax_ProcessSeq = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == endWorkInfo.WorkNo).Max(m => m.ProcessSeq);
                if(iMax_ProcessSeq == endWorkInfo.ProcessSeq)
                {
                    //반제품 생산 종료시 투입
                    if (endWorkInfo.TopCategoryCode == MasterCodeSTR.TopCategory_BAN)
                    {
                        TN_BAN1000 tN_BAN1000 = new TN_BAN1000();
                        tN_BAN1000.InNo = DbRequestHandler.GetSeqMonth("BIN");
                        tN_BAN1000.InDate = DateTime.Today;
                        tN_BAN1000.InId = GlobalVariable.LoginId;

                        TN_BAN1001 tN_BAN1001 = new TN_BAN1001();
                        tN_BAN1001.InNo = tN_BAN1000.InNo;
                        tN_BAN1001.InSeq = tN_BAN1000.TN_BAN1001List.Count == 0 ? 1 : tN_BAN1000.TN_BAN1001List.Max(m => m.InSeq) + 1;
                        tN_BAN1001.ItemCode = endWorkInfo.ItemCode;
                        tN_BAN1001.InQty = tx_OkQty.EditValue.GetDecimalNullToZero(); // 20220104 오세완 차장 양품 수량만 입고하는 것으로 변경
                        tN_BAN1001.BanProductLotNo = productionLotNo;
                        // 20220206 오세완 차장  in_lot_no가 필요가 없어서 table 에 null조건 해제시킴

                        tN_BAN1000.TN_BAN1001List.Add(tN_BAN1001);
                        ModelService.InsertChild<TN_BAN1000>(tN_BAN1000); // 20220204 오세완 차장 저장 스타일을 조금 변경
                        ModelService.Save();
                    }
                }
            }

            ActRefresh();
        }

        /// <summary>
        /// 투입자재 자동출고
        /// </summary>
        /// <returns>자동출고Lot List</returns>
        private List<string> AutoOutSrc(string srcItemCode, decimal srcUseQty, string workNo)
        {
            List<string> rtnOutLotNo = new List<string>();
            //자동출고
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", srcItemCode);
                var vResult = context.Database.SqlQuery<TEMP_XPFRESULT_AUTO_SRCOUT_LIST>("USP_GET_XPFRESULT_AUTO_SRCOUT_LIST @ITEM_CODE", sp_Itemcode).ToList();

                //해당 자재 재고가 있을경우 (입고) 자동출고를 한다.
                //해당 자재 재고가 없을경우 자동출고는 하지않고 자재에대한 마지막 출고 lot를 가져와서 차감을 한다
                decimal calQty = 0;
                string lastOutLotNo = null;
                bool check = false;

                TN_PUR1300 prenewobj = new TN_PUR1300()
                {
                    OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                    OutDate = DateTime.Today,
                    OutId = GlobalVariable.LoginId,
                    Memo = srcItemCode + " 자동차감출고",
                    Temp = workNo // 20210622 오세완 차장 자동출고한 지시번호 연결
                };

                ModelService.InsertChild<TN_PUR1300>(prenewobj);

                if (vResult != null && vResult.Count > 0)
                {
                    foreach (var s in vResult)
                    {
                        TN_PUR1301 newdtlobj = new TN_PUR1301()
                        {
                            OutNo = prenewobj.OutNo,
                            OutSeq = prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                            InNo = s.IN_NO,
                            InSeq = s.IN_SEQ,
                            ItemCode = srcItemCode,
                            OutLotNo = prenewobj.OutNo + (prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                            InLotNo = s.IN_LOT_NO,
                            Memo = srcItemCode + "자동차감출고",
                        };

                        lastOutLotNo = newdtlobj.OutLotNo;

                        if (srcUseQty - calQty > s.STOCK_QTY.GetDecimalNullToZero())
                        {
                            calQty += s.STOCK_QTY.GetDecimalNullToZero();
                            newdtlobj.OutQty = s.STOCK_QTY.GetDecimalNullToZero();
                            rtnOutLotNo.Add(newdtlobj.OutLotNo);
                        }
                        else
                        {
                            newdtlobj.OutQty = srcUseQty - calQty;
                            lastOutLotNo = null; //재고 차감이 전부 되었을 경우 lastOutLotNo Null 처리
                            rtnOutLotNo.Add(newdtlobj.OutLotNo);
                            check = true;
                        }

                        rtnOutLotNo.Add(newdtlobj.OutLotNo);

                        ModelService.InsertChild<TN_PUR1301>(newdtlobj);
                        ModelService.Save();

                        if (check)
                            break;
                    }
                }

                //재고가 남아있지 않는경우 마지막 출고 LOT를 찾는다.
                //마지막 출고LotNo도 없을 경우 투입처리를 하지않는다 (자재출고 처리, LotDtl 추가, 소요량 처리)
                if (lastOutLotNo != null)
                {
                    //재고를 다 차감하지 못한 케이스
                    var updateObj = ModelService.GetChildList<TN_PUR1301>(x => x.OutLotNo == lastOutLotNo).FirstOrDefault();
                    if (updateObj != null)
                    {
                        updateObj.OutQty = updateObj.OutQty + (srcUseQty - calQty);
                        ModelService.UpdateChild(updateObj);
                    }
                }
                else if (vResult.Count == 0)
                {
                    ////남은 재고가 아예없어 단순히 마지막 입고LOT에서 출고를 진행한다.
                    var lastPur1201 = ModelService.GetChildList<TN_PUR1201>(x => x.ItemCode == srcItemCode).LastOrDefault();

                    if (lastPur1201 != null)
                    {
                        TN_PUR1301 newdtlobj = new TN_PUR1301()
                        {
                            OutNo = prenewobj.OutNo,
                            OutSeq = prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                            InNo = lastPur1201.InNo,
                            InSeq = lastPur1201.InSeq,
                            ItemCode = srcItemCode,
                            OutLotNo = prenewobj.OutNo + (prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                            InLotNo = lastPur1201.InLotNo,
                            OutQty = (srcUseQty - calQty),
                            Memo = srcItemCode + "자동차감출고",
                        };

                        rtnOutLotNo.Add(newdtlobj.OutLotNo);
                        ModelService.InsertChild<TN_PUR1301>(newdtlobj);
                    }
                    else
                    {
                        ModelService.RemoveChild<TN_PUR1300>(prenewobj);
                    }
                }

                ModelService.Save();

                return rtnOutLotNo;
            }
        }

        /// <summary>
        /// 반제품 투입 자재 자동출고
        /// </summary>
        /// <returns></returns>
        private List<string> AutoOutSrc_Ban(string srcItemCode, decimal srcUseQty, string workNo)
        {
            List<string> rtnOutLotNo = new List<string>();
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", srcItemCode);
                var result = context.Database.SqlQuery<TEMP_BAN_STOCK_LOTNO>("USP_GET_BAN_STOCK_LOTNO @ITEM_CODE", sp_Itemcode).ToList();

                decimal calQty = 0;
                string lastOutLotNo = null;
                bool check = false;

                TN_BAN1100 tN_BAN1100 = new TN_BAN1100();
                tN_BAN1100.OutNo = DbRequestHandler.GetSeqMonth("BOUT");
                tN_BAN1100.OutDate = DateTime.Today;
                tN_BAN1100.OutId = GlobalVariable.LoginId;
                tN_BAN1100.Memo = string.Format("{0} 자동차감출고", srcItemCode);
                tN_BAN1100.Temp = workNo;

                ModelService.InsertChild(tN_BAN1100);

                if (result.Count > 0)
                {
                    foreach (var s in result)
                    {
                        TN_BAN1101 newTN_BAN1101 = new TN_BAN1101();
                        newTN_BAN1101.OutNo = tN_BAN1100.OutNo;
                        newTN_BAN1101.OutSeq = tN_BAN1100.TN_BAN1101List.Count == 0 ? 1 : tN_BAN1100.TN_BAN1101List.Max(m => m.OutSeq) + 1;
                        newTN_BAN1101.InNo = s.InNo; //수정
                        newTN_BAN1101.InSeq = s.InSeq; //수정
                        newTN_BAN1101.ItemCode = s.ItemCode;
                        newTN_BAN1101.OutLotNo = tN_BAN1100.OutNo + (tN_BAN1100.TN_BAN1101List.Count == 0 ? 1 : tN_BAN1100.TN_BAN1101List.Max(m => m.OutSeq) + 1).ToString().PadLeft(3, '0');
                        newTN_BAN1101.Memo = string.Format("{0} 자동차감출고", s.ItemCode);

                        lastOutLotNo = newTN_BAN1101.OutLotNo;

                        if (srcUseQty - calQty > s.StockQty)
                        {
                            calQty += s.StockQty;
                            newTN_BAN1101.OutQty = s.StockQty;
                            rtnOutLotNo.Add(newTN_BAN1101.OutLotNo);
                        }
                        else
                        {
                            newTN_BAN1101.OutQty = srcUseQty - calQty;
                            lastOutLotNo = null; //재고 차감이 전부 되었을 경우 lastOutLotNo Null 처리
                            rtnOutLotNo.Add(newTN_BAN1101.OutLotNo);
                            check = true;
                        }

                        rtnOutLotNo.Add(newTN_BAN1101.OutLotNo);

                        ModelService.InsertChild<TN_BAN1101>(newTN_BAN1101);
                        ModelService.Save();

                        if (check)
                            break;
                    }
                }

                //재고가 남아있지 않는경우 마지막 출고 LOT를 찾는다.
                //마지막 출고LotNo도 없을 경우 투입처리를 하지않는다 (자재출고 처리, LotDtl 추가, 소요량 처리)
                if (lastOutLotNo != null)
                {
                    //재고를 다 차감하지 못한 케이스
                    var updateObj = ModelService.GetChildList<TN_BAN1101>(x => x.OutLotNo == lastOutLotNo).FirstOrDefault();
                    if (updateObj != null)
                    {
                        updateObj.OutQty = updateObj.OutQty + (srcUseQty - calQty);
                        ModelService.UpdateChild(updateObj);
                    }
                }
                else if (result.Count == 0)
                {
                    ////남은 재고가 아예없어 단순히 마지막 입고LOT에서 출고를 진행한다.
                    var lastBan1001 = ModelService.GetChildList<TN_BAN1001>(x => x.ItemCode == srcItemCode).LastOrDefault();

                    if (lastBan1001 != null)
                    {
                        TN_BAN1101 newTN_BAN1101 = new TN_BAN1101();
                        newTN_BAN1101.OutNo = tN_BAN1100.OutNo;
                        newTN_BAN1101.OutSeq = tN_BAN1100.TN_BAN1101List.Count == 0 ? 1 : tN_BAN1100.TN_BAN1101List.Max(m => m.OutSeq) + 1;
                        newTN_BAN1101.InNo = lastBan1001.InNo;
                        newTN_BAN1101.InSeq = lastBan1001.InSeq;
                        newTN_BAN1101.ItemCode = srcItemCode;
                        newTN_BAN1101.OutLotNo = tN_BAN1100.OutNo + (tN_BAN1100.TN_BAN1101List.Count == 0 ? 1 : tN_BAN1100.TN_BAN1101List.Max(m => m.OutSeq) + 1).ToString().PadLeft(3, '0');
                        newTN_BAN1101.OutQty = (srcUseQty - calQty);
                        newTN_BAN1101.Memo = string.Format("{0} 자동차감출고", srcItemCode);

                        rtnOutLotNo.Add(newTN_BAN1101.OutLotNo);
                        ModelService.InsertChild(newTN_BAN1101);
                    }
                    else
                    {
                        ModelService.RemoveChild(tN_BAN1100);
                    }
                }

                ModelService.Save();
            }

            return rtnOutLotNo;
        }

        /// <summary>
        /// 비가동
        /// </summary>
        private void Btn_MachineStop_Click(object sender, EventArgs e)
        {
            //var stopForm = new POP_POPUP.XPFMACHINESTOP();

            // 20210603 오세완 차장 비가동TO때문에 작업지시번호가 필요해서 변경
            var vWorkObj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (vWorkObj != null)
            {
                var stopForm = new POP_POPUP.XPFMACHINESTOP_V2(vWorkObj);
                stopForm.ShowDialog();

                ActRefresh();

                LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
            }
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
        /// POP 종료
        /// </summary>
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_30), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
                Close();
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
        
        private void XFPOP1000_FormClosed(object sender, FormClosedEventArgs e)
        {
            MenuOpenLogService.SetCloseMenuLog(DateTime.Now);
            LogFactory.GetLoginLogService().SetLogoutLog(DateTime.Now);
        }
    }
}

