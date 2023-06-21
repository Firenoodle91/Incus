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
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");
        List<TN_STD1000> processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
        List<TN_STD1000> surfaceList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.SurfaceList);

        public XFPOP1000()
        {
            InitializeComponent();

            gridEx1.ViewType = GridViewType.POP_GridView;

            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            gridEx1.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            gridEx1.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;

            tx_WorkNo.KeyDown += Tx_WorkNo_KeyDown;

            btn_Search.Click += Btn_Search_Click;
            btn_Up.Click += Btn_Up_Click; ;
            btn_Down.Click += Btn_Down_Click;
            pic_WorkStandardDocument.DoubleClick += Pic_WorkStandardDocument_DoubleClick;
            pic_DesignFileName.DoubleClick += Pic_DesignFileName_DoubleClick;
            btn_JobSetting.Click += Btn_JobSetting_Click;
            btn_WorkStart.Click += Btn_WorkStart_Click;
            btn_ResultAdd.Click += Btn_ResultAdd_Click;
            btn_QualityAdd.Click += Btn_QualityAdd_Click;
            btn_WorkEnd.Click += Btn_WorkEnd_Click;
            btn_SrcOrItemMoveChange.Click += Btn_SrcOrItemMoveChange_Click;
            btn_MachineStop.Click += Btn_MachineStop_Click;
            btn_MachineCheck.Click += Btn_MachineCheck_Click;
            btn_ItemMovePrint.Click += Btn_ItemMovePrint_Click;
            btn_Exit.Click += Btn_Exit_Click;
            //btn_lotmake.Click += Btn_lotmake_Click;

            pdf_workView.DoubleClick += Pdf_workView_DoubleClick;
            pdf_workView.DocumentChanged += Pdf_workView_DocumentChanged;
            pdf_workView.NavigationPaneInitialVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;
            pdf_workView.CursorMode = DevExpress.XtraPdfViewer.PdfCursorMode.HandTool;

            pdf_Design.DoubleClick += Pdf_Design_DoubleClick;
            pdf_Design.DocumentChanged += Pdf_Design_DocumentChanged;
            pdf_Design.NavigationPaneInitialVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;
            pdf_Design.CursorMode = DevExpress.XtraPdfViewer.PdfCursorMode.HandTool;
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

        private void Pdf_workView_DoubleClick(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            WaitHandler.ShowWait();

            try
            {
                if (obj.WorkStandardDocumentUrl != null)
                {
                    byte[] documentContent = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.WorkStandardDocumentUrl);
                    MemoryStream ms = new MemoryStream(documentContent);
                    POP_POPUP.XPFPOPPDF fm = new POP_POPUP.XPFPOPPDF(ms);
                    fm.ShowDialog();
                }
                else
                {
                    var workStandardObj = ModelService.GetChildList<TN_STD1500>(p => p.ProcessCode == obj.ProcessCode).LastOrDefault(); //마지막 등록된 공정별 기본 작업표준서 모델
                    if (workStandardObj != null)
                    {
                        byte[] documentContent = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + workStandardObj.DesignFileUrl);
                        MemoryStream ms = new MemoryStream(documentContent);
                        POP_POPUP.XPFPOPPDF fm = new POP_POPUP.XPFPOPPDF(ms);
                        fm.ShowDialog();
                    }
                }
            }
            catch { }
            finally { WaitHandler.CloseWait(); }
        }

        private void Pdf_Design_DocumentChanged(object sender, DevExpress.XtraPdfViewer.PdfDocumentChangedEventArgs e)
        {
            pdf_Design.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.PageLevel;
        }

        private void Pdf_workView_DocumentChanged(object sender, DevExpress.XtraPdfViewer.PdfDocumentChangedEventArgs e)
        {
            pdf_workView.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.PageLevel;
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
            lup_Machine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            lup_ProcTeamCode.SetFontSize(new Font("맑은 고딕", 12f));
            lup_Process.SetFontSize(new Font("맑은 고딕", 12f));
            lup_Machine.SetFontSize(new Font("맑은 고딕", 12f));
            lup_Item.SetFontSize(new Font("맑은 고딕", 12f));

            lup_ProcTeamCode.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_Process.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_Item.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            lup_ProcTeamCode.Properties.View.OptionsView.ShowColumnHeaders = false;
            lup_Item.Properties.View.OptionsView.ShowColumnHeaders = false;

            lup_ProcTeamCode.Properties.View.Columns["CodeVal"].Visible = false;
            lup_Item.Properties.View.Columns["ItemCode"].Visible = false;
            lup_Machine.Properties.View.Columns["MachineMCode"].Visible = false;
            lup_Process.Properties.View.Columns["CodeVal"].Visible = false;

            InitButtonLabelConvert();
        }

        private void InitButtonLabelConvert()
        {
            btn_Search.Text = LabelConvert.GetLabelText("Refresh");
            btn_JobSetting.Text = LabelConvert.GetLabelText("JobSetting");
            btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
            btn_ResultAdd.Text = LabelConvert.GetLabelText("ResultAdd");
            btn_QualityAdd.Text = LabelConvert.GetLabelText("QualityAdd");
            btn_WorkEnd.Text = LabelConvert.GetLabelText("WorkEnd");
            btn_SrcOrItemMoveChange.Text = LabelConvert.GetLabelText("SrcChange");
            btn_MachineStop.Text = LabelConvert.GetLabelText("MachineStop");
            btn_MachineCheck.Text = LabelConvert.GetLabelText("MachineCheck");
            btn_ItemMovePrint.Text = LabelConvert.GetLabelText("ItemMovePrint");
            btn_Exit.Text = LabelConvert.GetLabelText("Close");
            //btn_lotmake.Text= LabelConvert.GetLabelText("LotMake");
            
            lcDesign2.Text = lcDesign.Text;
            lcDesign3.Text = lcWorkStandardDocument.Text;
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            gridEx1.MainGrid.AddColumn("EmergencyFlag", LabelConvert.GetLabelText("EmergencyFlag"), false);
            gridEx1.MainGrid.AddColumn("JobStates", LabelConvert.GetLabelText("JobStates"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));            
            gridEx1.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            gridEx1.MainGrid.AddColumn("StartDueDate", LabelConvert.GetLabelText("StartDueDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");            
            gridEx1.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("Process"));
            gridEx1.MainGrid.AddColumn("MachineGroupCode", LabelConvert.GetLabelText("MachineGroup"));
            gridEx1.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
            gridEx1.MainGrid.AddColumn("MachineCode2", LabelConvert.GetLabelText("Machine"));
            gridEx1.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            gridEx1.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            gridEx1.MainGrid.AddColumn("SurfaceList", LabelConvert.GetLabelText("SurfaceList"), false);
            gridEx1.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            gridEx1.MainGrid.AddColumn("ProcessPackQty", LabelConvert.GetLabelText("ProcessPackQty"), HorzAlignment.Far, FormatType.Numeric, "#,0");
            gridEx1.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", "MachineCode");
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
            //GridRowLocator.GetCurrentRow("RowId");

            var keyFieldName = "RowId";
            object keyValue = null;
            int currentRow = 0;
            if (gridEx1.MainGrid.MainView.RowCount > 0)
            {
                currentRow = gridEx1.MainGrid.MainView.FocusedRowHandle > 0 ? gridEx1.MainGrid.MainView.FocusedRowHandle : 0;
                keyValue = gridEx1.MainGrid.MainView.GetRowCellValue(currentRow, keyFieldName);
            }

            gridEx1.MainGrid.Clear();
            //pdf_Design.CloseDocument();
            //pdf_workView.CloseDocument();

            ModelService.ReLoad();

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var ProcTeamCode = new SqlParameter("@ProcTeamCode", lup_ProcTeamCode.EditValue.GetNullToEmpty());
                var ProcessCode = new SqlParameter("@ProcessCode", lup_Process.EditValue.GetNullToEmpty());
                var MachineCode = new SqlParameter("@MachineCode", lup_Machine.EditValue.GetNullToEmpty());
                var ItemCode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
                var WorkNo = new SqlParameter("@WorkNo", tx_WorkNo.EditValue.GetNullToEmpty().ToUpper());

                var result = context.Database.SqlQuery<TEMP_XFPOP1000>("USP_GET_XFPOP1000_LIST @ProcTeamCode, @ProcessCode,@MachineCode ,@ItemCode ,@WorkNo"
                                                                                , ProcTeamCode, ProcessCode, MachineCode, ItemCode, WorkNo)
                                                                                .ToList();
                GridBindingSource.DataSource = result.OrderByDescending(p => p.EmergencyFlag).ThenBy(p => p.WorkDate).ThenBy(p => p.WorkNo).ToList();
                if (result.Count == 0)
                {
                    SetRefreshControl();
                    //lup_Item.DataSource = ModelService.GetChildList<TN_STD1100>(p => false).ToList();
                }
                //else
                //{
                //    var itemCodeArray = result.Select(p => p.ItemCode).Distinct().ToList();
                //    lup_Item.DataSource = ModelService.GetChildList<TN_STD1100>(p => itemCodeArray.Contains(p.ItemCode)).ToList();
                //}

                //lup_Item.Properties.View.OptionsView.ShowColumnHeaders = false;
                //lup_Item.Properties.View.Columns["ItemCode"].Visible = false;
            }
            gridEx1.DataSource = GridBindingSource;
            //GridRowLocator.SetCurrentRow();
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
            var workStandardObj = ModelService.GetChildList<TN_STD1500>(p => p.ProcessCode == obj.ProcessCode).LastOrDefault(); //마지막 등록된 공정별 기본 작업표준서 모델
            if (obj == null)
            {
                SetButtonEnable(null);
                InitButtonLabelConvert();
                SetRefreshControl();
                return;
            }

            SetButtonEnable(obj.JobStates);

            if (obj.ProcessSeq > 1)
                btn_SrcOrItemMoveChange.Text = LabelConvert.GetLabelText("ItemMoveChange");
            else
                btn_SrcOrItemMoveChange.Text = LabelConvert.GetLabelText("SrcChange");

            TN_STD1300 banBomObj = null;
            var wanBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == obj.ItemCode).FirstOrDefault();
            if (wanBomObj != null)
            {
                banBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ParentBomCode == wanBomObj.BomCode).FirstOrDefault();
            }
            //if (banBomObj != null)
            //{
            //    btn_SrcOrItemMoveChange.Text = LabelConvert.GetLabelText("BanChange");
            //}

            ModelService.ReLoad();

            //마지막 진행 실적 가져오기 (실적종료시간이 없는 경우)
            var TN_MPS1201 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
            if (TN_MPS1201 != null)
            {
                tx_ProductLotNo.EditValue = TN_MPS1201.ProductLotNo;
                tx_ResultQty.EditValue = TN_MPS1201.ResultSumQty.GetDecimalNullToZero().ToString("#,0.##");
                tx_OkQty.EditValue = TN_MPS1201.OkSumQty.GetDecimalNullToZero().ToString("#,0.##");
                tx_BadQty.EditValue = TN_MPS1201.BadSumQty.GetDecimalNullToZero().ToString("#,0.##");

                //decimal BadQty = 0;
                //decimal LossQty = 0;
                //var BadTypeList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP);
                //foreach (var v in TN_MPS1201.TN_MPS1202List.Where(p => !p.BadType.IsNullOrEmpty()).ToList())
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

            //작업표준서 불러오기
            if (!obj.WorkStandardDocumentUrl.IsNullOrEmpty())
            {
                var fileName = obj.WorkStandardDocument;
                int fileExtPos = fileName.LastIndexOf(".");
                string extName = string.Empty;
                if (fileExtPos >= 0)
                    extName = fileName.Substring(fileExtPos + 1, fileName.Length - fileExtPos - 1);

                if (extName.ToLower() == "pdf")
                {
                    lcWorkStandardDocument.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcDesign3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    pic_WorkStandardDocument.EditValue = null;
                    try
                    {
                        byte[] documentContent = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.WorkStandardDocumentUrl);
                        MemoryStream ms = new MemoryStream(documentContent);
                        pdf_workView.LoadDocument(ms);
                    }
                    catch
                    {
                        lcWorkStandardDocument.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lcDesign3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        pic_WorkStandardDocument.EditValue = null;
                        pdf_workView.CloseDocument();
                    }
                }
                else
                {
                    lcWorkStandardDocument.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcDesign3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    pic_WorkStandardDocument.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.WorkStandardDocumentUrl);
                    pdf_workView.CloseDocument();
                }
            }
            else
            {
                if(workStandardObj != null)
                {
                    lcWorkStandardDocument.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcDesign3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    byte[] workStandardDocumentContent = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + workStandardObj.DesignFileUrl);
                    MemoryStream ms = new MemoryStream(workStandardDocumentContent);
                    pdf_workView.LoadDocument(ms);
                }
                else
                {
                    lcWorkStandardDocument.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcDesign3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    pic_WorkStandardDocument.EditValue = null;
                    pdf_workView.CloseDocument();
                }
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
        private void Btn_lotmake_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;
            if (obj.ProcessSeq != 1) return;
            if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_120), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            //New_LOT();
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
            TN_STD1000 jobSettingFlag = DbRequestHandler.GetCommMainCode(MasterCodeSTR.JobSettingFlag).FirstOrDefault();

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;

            if (jobStates == MasterCodeSTR.JobStates_Wait) //대기
            {
                btn_JobSetting.Enabled = true;
                //if(jobSettingFlag != null) //작업설정여부가 Y일때는 false로
                //    btn_WorkStart.Enabled = false; 
                //else
                btn_WorkStart.Enabled = true;
                btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                btn_ResultAdd.Enabled = false;
                btn_QualityAdd.Enabled = false;
                btn_WorkEnd.Enabled = false;
                btn_SrcOrItemMoveChange.Enabled = false;
                btn_MachineStop.Enabled = true;
                btn_MachineCheck.Enabled = true;
                btn_ItemMovePrint.Enabled = false;
                btn_Exit.Enabled = true;
                //btn_lotmake.Enabled = false;

                if (obj.ProcessSeq > 1)
                {
                    btn_ItemMovePrint.Enabled = true;
                    //btn_lotmake.Enabled = false;
                }
            }
            else if (jobStates == MasterCodeSTR.JobStates_Start) //진행
            {
                btn_JobSetting.Enabled = true;
                btn_WorkStart.Enabled = false;
                btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                btn_ResultAdd.Enabled = true;
                btn_QualityAdd.Enabled = true;
                btn_WorkEnd.Enabled = true;
                btn_SrcOrItemMoveChange.Enabled = true;
                btn_MachineStop.Enabled = true;
                btn_MachineCheck.Enabled = true;
                btn_ItemMovePrint.Enabled = true;
                btn_Exit.Enabled = true;
                //btn_lotmake.Enabled = true;
                if (obj.ProcessSeq > 1)
                {
                  
                    //btn_lotmake.Enabled = false;
                }
            }
            else if (jobStates == MasterCodeSTR.JobStates_Pause) //일시정지
            {
                btn_JobSetting.Enabled = true;
                //if(jobSettingFlag != null) //작업설정여부가 Y일때는 false로
                //    btn_WorkStart.Enabled = false; 
                //else
                btn_WorkStart.Enabled = true;
                btn_WorkStart.Text = LabelConvert.GetLabelText("Restart");
                btn_ResultAdd.Enabled = false;
                btn_QualityAdd.Enabled = false;
                btn_WorkEnd.Enabled = false;
                btn_SrcOrItemMoveChange.Enabled = false;
                btn_MachineStop.Enabled = true;
                btn_MachineCheck.Enabled = true;
                btn_ItemMovePrint.Enabled = false;
                btn_Exit.Enabled = true;
                //btn_lotmake.Enabled = false;

                if (obj.ProcessSeq > 1)
                    btn_ItemMovePrint.Enabled = true;
            }
            else
            {
                btn_JobSetting.Enabled = false;
                btn_WorkStart.Enabled = false;
                btn_WorkStart.Text = LabelConvert.GetLabelText("WorkStart");
                btn_ResultAdd.Enabled = false;
                btn_QualityAdd.Enabled = false;
                btn_WorkEnd.Enabled = false;
                btn_SrcOrItemMoveChange.Enabled = false;
                btn_MachineStop.Enabled = true;
                btn_MachineCheck.Enabled = true;
                btn_ItemMovePrint.Enabled = false;
                btn_Exit.Enabled = true;
                //btn_lotmake.Enabled = false;
                //if (obj.ProcessSeq > 1)
                //{                  
                //    btn_lotmake.Enabled = false;
                //}
            }
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
            }
            else
            {
                if (!obj.WorkNo.Contains("WNO_D") && !CheckJobSetting())
                {
                    //작업설정검사가 안되어 있는 경우 예외처리
                    var jobSettingForm = new POP_POPUP.XPFJOBSETTING(obj, tx_ProductLotNo.EditValue.GetNullToEmpty());
                    var jobSettingValue = jobSettingForm.ShowDialog();
                    if (jobSettingValue == DialogResult.No)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_59), LabelConvert.GetLabelText("InspJobSetting")));
                        ActRefresh();
                        return;
                    }
                    else if (jobSettingValue == DialogResult.Cancel)
                        return;
                }

                if (obj.ProcessSeq == 1) //첫번째 공정일 경우 자재투입
                {
                    //if (obj.JobStates == MasterCodeSTR.JobStates_Wait) //대기상태
                    //{
                    //    TN_STD1300 banBomObj = null;
                    //    var wanBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == obj.ItemCode).FirstOrDefault();
                    //    if (wanBomObj != null)
                    //    {
                    //        banBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ParentBomCode == wanBomObj.BomCode).FirstOrDefault();
                    //    }

                    //    if (banBomObj != null)
                    //    {
                    //        //반제품투입
                    //        PopupDataParam param = new PopupDataParam();
                    //        param.SetValue(PopupParameter.KeyValue, obj);
                    //        IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFBANIN_START, param, WorkStartBanCallback);
                    //        form.ShowPopup(true);
                    //    }
                    //    else
                    //    {
                            ////원자재투입
                            //PopupDataParam param = new PopupDataParam();
                            //param.SetValue(PopupParameter.KeyValue, obj);
                            //IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFSRCIN_START, param, WorkStartSrcCallback);
                            //form.ShowPopup(true);
                    //    }
                    //}
                    
                    //원자재투입
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.KeyValue, obj);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRAW_MATERIAL_IN, param, WorkStartSrcCallback);
                    form.ShowPopup(true);
                }
                else
                {
                    ////이동표 정보 조회 추가필요
                    //PopupDataParam param = new PopupDataParam();
                    //param.SetValue(PopupParameter.KeyValue, obj);
                    //IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVESCAN_START, param, WorkStartItemMoveCallback);
                    //form.ShowPopup(true);

                    //이동표 정보 조회 추가필요
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.KeyValue, obj);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVESCAN_START_SM, param, WorkStartItemMoveCallback);
                    form.ShowPopup(true);
                }
            }
        }

        /// <summary>
        /// 작업 시작 시 원소재 투입 CallBack
        /// </summary>
        private void WorkStartSrcCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            var machineCode = e.Map.GetValue(PopupParameter.Value_1);

            var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();

            var returnList = (List<TN_STD1300>)e.Map.GetValue(PopupParameter.ReturnObject);

            string productLotNo = null;

            var workingDate = DateTime.Today;

            if (returnList.Count > 0 )
            {
                foreach (var v in returnList)
                {
                    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                    {
                        context.Database.CommandTimeout = 0;
                        var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
                        var MachineCode = new SqlParameter("@MachineCode", machineCode);
                        var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                        var ProcessCode = new SqlParameter("@ProcessCode", obj.ProcessCode);
                        var SrcItemCode = new SqlParameter("@SrcItemCode", v.ItemCode);
                        var SrcOutLotNo = new SqlParameter("@SrcOutLotNo", v.OutLotNo);
                        var ProductLotNo = new SqlParameter("@ProductLotNo", "");
                        var WorkingDate = new SqlParameter("@WorkingDate", workingDate);
                        var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                        //작업지시투입정보 INSERT
                        productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC @WorkNo, @MachineCode, @ItemCode , @ProcessCode, @SrcItemCode, @SrcOutLotNo, @ProductLotNo, @WorkingDate, @LoginId"
                                                                        , WorkNo, MachineCode, ItemCode, ProcessCode, SrcItemCode, SrcOutLotNo, ProductLotNo, WorkingDate, LoginId).SingleOrDefault();
                    }
                }            
            }
            //bom 투입자재가 없는 경우도 작업 시작 할수 있도록 예외 변경 2022-11-18 jdw
            else
            {
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
                    var MachineCode = new SqlParameter("@MachineCode", machineCode);
                    var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                    var ProcessCode = new SqlParameter("@ProcessCode", obj.ProcessCode);
                    var SrcItemCode = new SqlParameter("@SrcItemCode", obj.ItemCode);
                    var SrcOutLotNo = new SqlParameter("@SrcOutLotNo", "");
                    var ProductLotNo = new SqlParameter("@ProductLotNo", "");
                    var WorkingDate = new SqlParameter("@WorkingDate", workingDate);
                    var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                    //작업지시투입정보 INSERT
                    productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC @WorkNo, @MachineCode, @ItemCode , @ProcessCode, @SrcItemCode, @SrcOutLotNo, @ProductLotNo, @WorkingDate, @LoginId"
                                                                    , WorkNo, MachineCode, ItemCode, ProcessCode, SrcItemCode, SrcOutLotNo, ProductLotNo, WorkingDate, LoginId).SingleOrDefault();
                }
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
            ModelService.Insert(TN_MPS1201_NewObj);
            #endregion

            //작업지시서 상태 변경
            TN_MPS1200.JobStates = MasterCodeSTR.JobStates_Start;
            TN_MPS1200.UpdateTime = DateTime.Now;
            ModelService.UpdateChild(TN_MPS1200);
            ModelService.Save();
            ActRefresh();
        }

        ///// <summary>
        ///// 작업 시작 시 반제품 투입 CallBack
        ///// </summary>
        //private void WorkStartBanCallback(object sender, PopupArgument e)
        //{
        //    if (e == null) return;

        //    var obj = GridBindingSource.Current as TEMP_XFPOP1000;
        //    if (obj == null) return;

        //    var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();

        //    var banItemCode = e.Map.GetValue(PopupParameter.Value_1).GetNullToNull();
        //    var banOutLotNo = e.Map.GetValue(PopupParameter.Value_2).GetNullToNull();
        //    var machineCode = e.Map.GetValue(PopupParameter.Value_3).GetNullToNull();

        //    string productLotNo = null;

        //    var workingDate = DateTime.Today;

        //    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
        //    {
        //        context.Database.CommandTimeout = 0;
        //        var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
        //        var MachineCode = new SqlParameter("@MachineCode", machineCode.GetNullToEmpty());
        //        var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
        //        var SrcItemCode = new SqlParameter("@SrcItemCode", banItemCode);
        //        var SrcOutLotNo = new SqlParameter("@SrcOutLotNo", banOutLotNo);
        //        var WorkingDate = new SqlParameter("@WorkingDate", workingDate);

        //        //작업지시투입정보 INSERT
        //        productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC @WorkNo, @MachineCode ,@ItemCode ,@SrcItemCode, @SrcOutLotNo, @WorkingDate"
        //                                                        , WorkNo, MachineCode, ItemCode, SrcItemCode, SrcOutLotNo, WorkingDate).SingleOrDefault();
        //    }

        //    if (productLotNo.IsNullOrEmpty())
        //        return;

        //    #region 작업실적관리 마스터 INSERT
        //    var TN_MPS1201_NewObj = new TN_MPS1201();
        //    TN_MPS1201_NewObj.WorkNo = obj.WorkNo;
        //    TN_MPS1201_NewObj.ProcessCode = obj.ProcessCode;
        //    TN_MPS1201_NewObj.ProcessSeq = obj.ProcessSeq;
        //    TN_MPS1201_NewObj.ProductLotNo = productLotNo;
        //    TN_MPS1201_NewObj.ItemCode = obj.ItemCode;
        //    TN_MPS1201_NewObj.CustomerCode = obj.CustomerCode;
        //    TN_MPS1201_NewObj.MachineCode = machineCode;
        //    TN_MPS1201_NewObj.ResultStartDate = DateTime.Now;
        //    TN_MPS1201_NewObj.ResultSumQty = 0;
        //    TN_MPS1201_NewObj.OkSumQty = 0;
        //    TN_MPS1201_NewObj.BadSumQty = 0;
        //    ModelService.Insert(TN_MPS1201_NewObj);
        //    #endregion

        //    //작업지시서 상태 변경
        //    TN_MPS1200.JobStates = MasterCodeSTR.JobStates_Start;
        //    TN_MPS1200.UpdateTime = DateTime.Now;
        //    ModelService.UpdateChild(TN_MPS1200);
        //    ModelService.Save();
        //    ActRefresh();
        //}

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
            var returnList = (List<TN_STD1300>)e.Map.GetValue(PopupParameter.ReturnObject);

            var workingDate = DateTime.Today;

            foreach (var v in returnList)
            {
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
                    var MachineCode = new SqlParameter("@MachineCode", machineCode);
                    var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                    var ProcessCode = new SqlParameter("@ProcessCode", obj.ProcessCode);
                    var SrcItemCode = new SqlParameter("@SrcItemCode", v.ItemCode);
                    var SrcOutLotNo = new SqlParameter("@SrcOutLotNo", v.OutLotNo);
                    var ProductLotNo = new SqlParameter("@ProductLotNo", productLotNo);
                    var WorkingDate = new SqlParameter("@WorkingDate", workingDate);
                    var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                    //작업지시투입정보 INSERT
                    productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC @WorkNo, @MachineCode, @ItemCode , @ProcessCode, @SrcItemCode, @SrcOutLotNo, @ProductLotNo, @WorkingDate, @LoginId"
                                                                    , WorkNo, MachineCode, ItemCode, ProcessCode, SrcItemCode, SrcOutLotNo, ProductLotNo, WorkingDate, LoginId).SingleOrDefault();
                }
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

            var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();
            if (obj.MachineCode.GetNullToEmpty() != "")
            {
                if (obj.ProcessCode == MasterCodeSTR.Process_Heat)
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.KeyValue, obj);
                    param.SetValue(PopupParameter.Value_1, productLotNo);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_HEAT_TOOL, param, ResultAddCallback);
                    form.ShowPopup(true);
                }
                else
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.KeyValue, obj);
                    param.SetValue(PopupParameter.Value_1, productLotNo);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_TOOL, param, ResultAddCallback);
                    form.ShowPopup(true);
                }
            }
            else
            {
                if (obj.ProcessCode == MasterCodeSTR.Process_Heat)
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.KeyValue, obj);
                    param.SetValue(PopupParameter.Value_1, productLotNo);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_HEAT, param, ResultAddCallback);
                    form.ShowPopup(true);
                }
                else
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.KeyValue, obj);
                    param.SetValue(PopupParameter.Value_1, productLotNo);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_DEFAULT, param, ResultAddCallback);
                    form.ShowPopup(true);
                }
            }
            //PopupDataParam param = new PopupDataParam();
            //param.SetValue(PopupParameter.KeyValue, obj);
            //param.SetValue(PopupParameter.Value_1, productLotNo);
            //param.SetValue(PopupParameter.Value_2, itemMoveNo);
            //if (obj.MachineCode.GetNullToEmpty() == "")
            //{
            //    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_DEFAULT, param, ResultAddCallback);
            //    form.ShowPopup(true);
            //}
            //else {
            //    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_TOOL, param, ResultAddCallback);
            //    form.ShowPopup(true);
            //}

            // 
            // form.ShowPopup(true);
            ////if (TN_MPS1200.ToolUseFlag == "Y")
            ////{
            ////    if (obj.ProcessCode == MasterCodeSTR.Process_Heat)
            ////    {
            ////        PopupDataParam param = new PopupDataParam();
            ////        param.SetValue(PopupParameter.KeyValue, obj);
            ////        param.SetValue(PopupParameter.Value_1, productLotNo);
            ////        param.SetValue(PopupParameter.Value_2, itemMoveNo);
            ////        IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_HEAT_TOOL, param, ResultAddCallback);
            ////        form.ShowPopup(true);
            ////    }
            ////    else
            ////    {
            ////        PopupDataParam param = new PopupDataParam();
            ////        param.SetValue(PopupParameter.KeyValue, obj);
            ////        param.SetValue(PopupParameter.Value_1, productLotNo);
            ////        param.SetValue(PopupParameter.Value_2, itemMoveNo);
            ////        IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_TOOL, param, ResultAddCallback);
            ////        form.ShowPopup(true);
            ////    }
            ////}
            ////else
            ////{
            ////    if (obj.ProcessCode == MasterCodeSTR.Process_Heat)
            ////    {
            ////        PopupDataParam param = new PopupDataParam();
            ////        param.SetValue(PopupParameter.KeyValue, obj);
            ////        param.SetValue(PopupParameter.Value_1, productLotNo);
            ////        param.SetValue(PopupParameter.Value_2, itemMoveNo);
            ////        IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_HEAT, param, ResultAddCallback);
            ////        form.ShowPopup(true);
            ////    }
            ////    else
            ////    {
            ////        PopupDataParam param = new PopupDataParam();
            ////        param.SetValue(PopupParameter.KeyValue, obj);
            ////        param.SetValue(PopupParameter.Value_1, productLotNo);
            ////        param.SetValue(PopupParameter.Value_2, itemMoveNo);
            ////        IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_DEFAULT, param, ResultAddCallback);
            ////        form.ShowPopup(true);
            ////    }
            ////}
        }

        /// <summary>
        /// 실적등록 CallBack
        /// </summary>
        private void ResultAddCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            if (!obj.WorkNo.Contains("WNO_D"))
            {
                if (e.Map.ContainsKey(PopupParameter.Value_2)) //툴교체시 작업설정검사
                {
                    var jobSettingForm = new POP_POPUP.XPFJOBSETTING(obj, tx_ProductLotNo.EditValue.GetNullToEmpty());
                    jobSettingForm.ShowDialog();
                }
            }

            ActRefresh();
        }

        /// <summary>
        /// 품질등록
        /// </summary>
        private void Btn_QualityAdd_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            var inspectionForm = new POP_POPUP.XPFINSPECTION(obj, tx_ProductLotNo.EditValue.GetNullToEmpty());
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
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFWORKEND, param, WorkEndCallback);
            form.ShowPopup(true);
        }

        /// <summary>
        /// 작업 종료 시 CallBack
        /// </summary>
        private void WorkEndCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            if(e.Map.ContainsKey(PopupParameter.Constraint)) //새출력 시 
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
                    masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER @WorkNo", _workNo).Where(p => p.ItemMoveNo == itemMoveNo).FirstOrDefault();
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
            }          

            ActRefresh();
        }

        /// <summary>
        /// 원자재/이동표 교체
        /// </summary>
        private void Btn_SrcOrItemMoveChange_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;

            if (obj.ProcessSeq == 1) //첫번째 공정일 경우 
            {
                //TN_STD1300 banBomObj = null;
                //var wanBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == obj.ItemCode).FirstOrDefault();
                //if (wanBomObj != null)
                //{
                //    banBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ParentBomCode == wanBomObj.BomCode).FirstOrDefault();
                //}
                //if (banBomObj != null)
                //{
                    ////반제품교체
                    //PopupDataParam param = new PopupDataParam();
                    //param.SetValue(PopupParameter.KeyValue, obj);
                    //param.SetValue(PopupParameter.Value_1, tx_ProductLotNo.EditValue.GetNullToEmpty());
                    //IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFBANIN_CHANGE, param, BanChangeCallback);
                    //form.ShowPopup(true);
                //}
                //else
                //{
                    //원자재교체
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.KeyValue, obj);
                    param.SetValue(PopupParameter.Value_1, tx_ProductLotNo.EditValue.GetNullToEmpty());
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFSRCIN_CHANGE_SM, param, SrcChangeCallback);
                    form.ShowPopup(true);
                //}
            }
            else
            {
                //이동표 정보 조회 추가필요
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
            var machineCode = e.Map.GetValue(PopupParameter.Value_3).GetNullToEmpty();

            string productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();

            var workingDate = DateTime.Today;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
                var MachineCode = new SqlParameter("@MachineCode", machineCode);
                var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                var ProcessCode = new SqlParameter("@ProcessCode", obj.ProcessCode);
                var SrcItemCode = new SqlParameter("@SrcItemCode", srcItemCode);
                var SrcOutLotNo = new SqlParameter("@SrcOutLotNo", srcOutLotNo);
                var ProductLotNo = new SqlParameter("@ProductLotNo", productLotNo);
                var WorkingDate = new SqlParameter("@WorkingDate", workingDate);
                var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                //작업지시투입정보 INSERT
                productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC @WorkNo, @MachineCode ,@ItemCode, @ProcessCode ,@SrcItemCode, @SrcOutLotNo, @ProductLotNo, @WorkingDate, @LoginId"
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

                ModelService.Save();

                if (!obj.WorkNo.Contains("WNO_D"))
                {
                    var jobSettingForm = new POP_POPUP.XPFJOBSETTING(obj, productLotNo);
                    jobSettingForm.ShowDialog();
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
        }

        ///// <summary>
        ///// 반제품 교체 CallBack
        ///// </summary>
        //private void BanChangeCallback(object sender, PopupArgument e)
        //{
        //    if (e == null) return;

        //    var obj = GridBindingSource.Current as TEMP_XFPOP1000;
        //    if (obj == null) return;

        //    var banItemCode = e.Map.GetValue(PopupParameter.Value_1).GetNullToNull();
        //    var banOutLotNo = e.Map.GetValue(PopupParameter.Value_2).GetNullToNull();
        //    var machineCode = e.Map.GetValue(PopupParameter.Value_3).GetNullToNull();

        //    string productLotNo = null;

        //    var workingDate = DateTime.Today;

        //    if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_80), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        //        return;
            
        //    using (var context = new HKInc.Ui.Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
        //    {
        //        context.Database.CommandTimeout = 0;
        //        var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
        //        var MachineCode = new SqlParameter("@MachineCode", machineCode.GetNullToEmpty());
        //        var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
        //        var SrcItemCode = new SqlParameter("@SrcItemCode", banItemCode);
        //        var SrcOutLotNo = new SqlParameter("@SrcOutLotNo", banOutLotNo);
        //        var WorkingDate = new SqlParameter("@WorkingDate", workingDate);

        //        //작업지시투입정보 INSERT
        //        productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC @WorkNo, @MachineCode ,@ItemCode ,@SrcItemCode, @SrcOutLotNo, @WorkingDate"
        //                                                        , WorkNo, MachineCode, ItemCode, SrcItemCode, SrcOutLotNo, WorkingDate).SingleOrDefault();
        //    }

        //    if (productLotNo.IsNullOrEmpty())
        //        return;

        //    #region 이전 작업실적관리 마스터 UPDATE
        //    var TN_MPS1201_Previous = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
        //    TN_MPS1201_Previous.ResultDate = DateTime.Today;
        //    TN_MPS1201_Previous.ResultEndDate = DateTime.Now;
        //    ModelService.Update(TN_MPS1201_Previous);
        //    #endregion

        //    var TN_MPS1201_Check = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ProductLotNo == productLotNo).LastOrDefault();
        //    if (TN_MPS1201_Check != null)
        //    {
        //        TN_MPS1201_Check.ResultDate = null;
        //        TN_MPS1201_Check.ResultEndDate = null;
        //    }
        //    else
        //    {
        //        #region 작업실적관리 마스터 INSERT
        //        var TN_MPS1201_NewObj = new TN_MPS1201();
        //        TN_MPS1201_NewObj.WorkNo = obj.WorkNo;
        //        TN_MPS1201_NewObj.ProcessCode = obj.ProcessCode;
        //        TN_MPS1201_NewObj.ProcessSeq = obj.ProcessSeq;
        //        TN_MPS1201_NewObj.ProductLotNo = productLotNo;
        //        TN_MPS1201_NewObj.ItemCode = obj.ItemCode;
        //        TN_MPS1201_NewObj.CustomerCode = obj.CustomerCode;
        //        TN_MPS1201_NewObj.MachineCode = machineCode;
        //        TN_MPS1201_NewObj.ResultStartDate = DateTime.Now;
        //        TN_MPS1201_NewObj.ResultSumQty = 0;
        //        TN_MPS1201_NewObj.OkSumQty = 0;
        //        TN_MPS1201_NewObj.BadSumQty = 0;
        //        ModelService.Insert(TN_MPS1201_NewObj);
        //        #endregion
        //    }

        //    ModelService.Save();

        //    ActRefresh();
        //}

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
            var stopForm = new POP_POPUP.XPFMACHINESTOP();
            stopForm.ShowDialog();
        }

        /// <summary>
        /// 설비점검
        /// </summary>
        private void Btn_MachineCheck_Click(object sender, EventArgs e)
        {
            var machineCheckForm = new POP_POPUP.XPFMACHINECHECK();
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
                masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER @WorkNo", _workNo).Where(p => p.ItemMoveNo == itemMoveNo).FirstOrDefault();
            }

            if (masterObj == null) return;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
                var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

                detailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
            }

            //var ItemMoveNoReport = new XRITEMMOVENO(masterObj, detailList);
            //var printTool = new ReportPrintTool(ItemMoveNoReport);
            //printTool.ShowPreview();


          //  var ItemMoveNoReport = new XRITEMMOVENO_S(masterObj, detailList);
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

        ///// <summary>
        ///// LOT 생성
        ///// </summary>
        //private void New_LOT()
        //{
          

        //    var obj = GridBindingSource.Current as TEMP_XFPOP1000;
        //    if (obj == null) return;


      
        //    string productLotNo = tx_ProductLotNo.EditValue.ToString();
        //    TN_LOT_DTL lotobj = ModelService.GetChildList<TN_LOT_DTL>(p => p.ProductLotNo == productLotNo).OrderBy(o => o.ProductLotNo).FirstOrDefault();
        //    var workingDate = DateTime.Today;

        //    using (var context = new HKInc.Ui.Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
        //    {
        //        context.Database.CommandTimeout = 0;
        //        var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
        //        var MachineCode = new SqlParameter("@MachineCode", obj.MachineCode.GetNullToEmpty());
        //        var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
        //        var SrcItemCode = new SqlParameter("@SrcItemCode", lotobj.SrcCode);//  srcItemCode);
        //        var SrcOutLotNo = new SqlParameter("@SrcOutLotNo", lotobj.SrcInLotNo); //srcOutLotNo);
        //        var WorkingDate = new SqlParameter("@WorkingDate", workingDate);

        //        //작업지시투입정보 INSERT
        //        productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC_NEW @WorkNo, @MachineCode ,@ItemCode ,@SrcItemCode, @SrcOutLotNo, @WorkingDate"
        //                                                        , WorkNo, MachineCode, ItemCode, SrcItemCode, SrcOutLotNo, WorkingDate).SingleOrDefault();
        //    }

        //    if (productLotNo.IsNullOrEmpty())
        //        return;

        //    if (productLotNo != tx_ProductLotNo.EditValue.GetNullToEmpty())
        //    {
        //        if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_72), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        //            return;

        //        #region 이전 작업실적관리 마스터 UPDATE
        //        var TN_MPS1201_Previous = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
        //        TN_MPS1201_Previous.ResultDate = DateTime.Today;
        //        TN_MPS1201_Previous.ResultEndDate = DateTime.Now;
        //        ModelService.Update(TN_MPS1201_Previous);
        //        #endregion

        //        var TN_MPS1201_Check = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ProductLotNo == productLotNo).LastOrDefault();
        //        if (TN_MPS1201_Check != null)
        //        {
        //            TN_MPS1201_Check.ResultDate = null;
        //            TN_MPS1201_Check.ResultEndDate = null;
        //        }
        //        else
        //        {
        //            #region 작업실적관리 마스터 INSERT
        //            var TN_MPS1201_NewObj = new TN_MPS1201();
        //            TN_MPS1201_NewObj.WorkNo = obj.WorkNo;
        //            TN_MPS1201_NewObj.ProcessCode = obj.ProcessCode;
        //            TN_MPS1201_NewObj.ProcessSeq = obj.ProcessSeq;
        //            TN_MPS1201_NewObj.ProductLotNo = productLotNo;
        //            TN_MPS1201_NewObj.ItemCode = obj.ItemCode;
        //            TN_MPS1201_NewObj.CustomerCode = obj.CustomerCode;
        //            TN_MPS1201_NewObj.MachineCode = obj.MachineCode;
        //            TN_MPS1201_NewObj.ResultStartDate = DateTime.Now;
        //            TN_MPS1201_NewObj.ResultSumQty = 0;
        //            TN_MPS1201_NewObj.OkSumQty = 0;
        //            TN_MPS1201_NewObj.BadSumQty = 0;
        //            ModelService.Insert(TN_MPS1201_NewObj);
        //            #endregion
        //        }

        //        var ItemMoveLastObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == TN_MPS1201_Previous.ItemMoveNo).FirstOrDefault();
        //        // 이동표번호가 없는 경우
        //        if (ItemMoveLastObj == null)
        //        {
        //            NewItemMovePrint(TN_MPS1201_Previous.OkSumQty.GetDecimalNullToZero(), obj, TN_MPS1201_Previous.TN_MPS1200, TN_MPS1201_Previous);
        //        }
        //        // 이동표번호가 있으나 그 이후 새로 실적을 등록했을 경우
        //        else if (TN_MPS1201_Previous.TN_MPS1202List.Any(p => p.ItemMoveNo == null))
        //        {
        //            NewItemMovePrint(TN_MPS1201_Previous.OkSumQty.GetDecimalNullToZero() - ItemMoveLastObj.OkSumQty.GetDecimalNullToZero(), obj, TN_MPS1201_Previous.TN_MPS1200, TN_MPS1201_Previous);
        //        }

        //        ModelService.Save();

        //        if (!obj.WorkNo.Contains("WNO_D"))
        //        {
        //            var jobSettingForm = new POP_POPUP.XPFJOBSETTING(obj, productLotNo);
        //            jobSettingForm.ShowDialog();
        //        }
        //    }

        //    ActRefresh();
        //}

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

        private void lcProductLotNo_DoubleClick(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XFPOP1000;
            if (obj == null) return;
            if (obj.ProcessSeq != 1) return;
            if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_120), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            //New_LOT();
        }
    }
}

