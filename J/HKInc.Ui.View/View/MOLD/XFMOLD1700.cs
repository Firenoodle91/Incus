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
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Service.Service;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.MOLD
{
    /// <summary>
    /// 금형등급관리평가
    /// </summary>
    public partial class XFMOLD1700 : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_MOLD1700> ModelService = (IService<TN_MOLD1700>)ProductionFactory.GetDomainService("TN_MOLD1700");

        public XFMOLD1700()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            
            MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowStyle;

            DetailGridExControl.MainGrid.MainView.FocusedRowChanged += DetailMainView_FocusedRowChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellValueChanged;

            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += SubDetailMainView_CellValueChanged;
        }

        protected override void InitCombo()
        {
            lup_MoldCode.SetDefault(true, "MoldMCode", DataConvert.GetCultureDataFieldName("MoldName"), ModelService.GetChildList<TN_MOLD1100>(p => p.UseYN == "Y").ToList());
        }

        protected override void InitGrid()
        {
            SetGridBarButton();

            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("MoldMCode", LabelConvert.GetLabelText("MoldMCode"));
            MasterGridExControl.MainGrid.AddColumn("MoldCode", LabelConvert.GetLabelText("MoldCode"));
            MasterGridExControl.MainGrid.AddColumn("MoldName", LabelConvert.GetLabelText("MoldName"));
            MasterGridExControl.MainGrid.AddColumn("MoldGrade", LabelConvert.GetLabelText("MoldClass"));
            MasterGridExControl.MainGrid.AddColumn("TotalScore", LabelConvert.GetLabelText("MoldScore"));
            //MasterGridExControl.MainGrid.AddColumn("MachineGDate", LabelConvert.GetLabelText("MachineGDate"));        //등급평가일
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("SumShotcnt"), LabelConvert.GetLabelText("SumShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("TransferDate", LabelConvert.GetLabelText("TransferDate"));        //이관일
            MasterGridExControl.MainGrid.AddColumn("MoldMakerCust", LabelConvert.GetLabelText("MoldMakerCust"));      //제작처
            MasterGridExControl.MainGrid.AddColumn("MainMachineCode", LabelConvert.GetLabelText("MainMachineCode"));  //메인설비
            MasterGridExControl.MainGrid.AddColumn("CheckCycle", LabelConvert.GetLabelText("CheckCycle"));            //점검주기
            MasterGridExControl.MainGrid.AddColumn("NextCheckDate", LabelConvert.GetLabelText("NextMoldCheckDate"));  //다음점검일
            MasterGridExControl.MainGrid.AddColumn("LastQcDate", LabelConvert.GetLabelText("LastCheckDate"));      //마지막평가일자

            DetailGridExControl.MainGrid.AddColumn("MeaNo", LabelConvert.GetLabelText("MoldGradeManageNo"), true);                  
            DetailGridExControl.MainGrid.AddColumn("MoldMCode", LabelConvert.GetLabelText("MoldMCode"), false);        // 금형코드
            DetailGridExControl.MainGrid.AddColumn("GradeManageNo", LabelConvert.GetLabelText("GradeManageNo"));         
            DetailGridExControl.MainGrid.AddColumn("QcDate", LabelConvert.GetLabelText("QcDate"), HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));                          // 평가자
            DetailGridExControl.MainGrid.AddColumn("TotalScore", LabelConvert.GetLabelText("TotalScore"));                  // 평가 값
            DetailGridExControl.MainGrid.AddColumn("Grade", LabelConvert.GetLabelText("Grade"));                            // 등급
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "QcDate");

            SubDetailGridExControl.MainGrid.AddColumn("MeaNo", LabelConvert.GetLabelText("MeaNo"), false);
            SubDetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), false);
            SubDetailGridExControl.MainGrid.AddColumn("EvaluationItem", LabelConvert.GetLabelText("MoldGradeItem"));
            SubDetailGridExControl.MainGrid.AddColumn("InValue", LabelConvert.GetLabelText("JudgeValue"));
            SubDetailGridExControl.MainGrid.AddColumn("Score", LabelConvert.GetLabelText("Score"));
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InValue");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MOLD1700>(DetailGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MOLD1701>(SubDetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldGrade", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldClass), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldMakerCust", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldMakercust), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckCycle), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(x => x.Active == "Y").ToList(), "LoginId", DataConvert.GetCultureDataFieldName("UserName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Grade", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldClass), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("EvaluationItem", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldEvolItem), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MoldMCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            //using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            //{
            //    context.Database.CommandTimeout = 0;
            //    SqlParameter moldCode = new SqlParameter("@MoldCode", lup_MoldCode.EditValue.GetNullToEmpty());

            //    MasterGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_XFMOLD1700_MASTER>("USP_GET_XFMOLD1700_MASTER @MoldCode", moldCode).OrderBy(o => o.MoldMCode).ToList();

            //    MasterGridExControl.DataSource = MasterGridBindingSource;
            //    MasterGridExControl.MainGrid.BestFitColumns();
            //    MasterGridExControl.BestFitColumns();
            //}

            string moldMCode = lup_MoldCode.EditValue.GetNullToEmpty();
            
            MasterGridBindingSource.DataSource = ModelService.GetChildList<TN_MOLD1100>(x => x.UseYN == "Y" 
                                                                                        && string.IsNullOrEmpty(moldMCode) ? true : x.MoldMCode == moldMCode
                                                                                        ).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();

            try
            {
                WaitHandler.ShowWait();
                ModelService.Save();
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex.Message);
            }
            finally
            {
                WaitHandler.CloseWait();
                DataLoad();
            }
        }

        protected override void DetailAddRowClicked()
        {
            //var masterObj = MasterGridBindingSource.Current as TEMP_XFMOLD1700_MASTER;
            var masterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            if (masterObj == null) return;

            TN_MOLD1700 newObj = new TN_MOLD1700();

            newObj.MeaNo = DbRequestHandler.GetSeqMonth("MG");
            newObj.GradeManageNo = ModelService.GetChildList<TN_MOLD1600>(x => true).OrderBy(o => o.RevisionDate).LastOrDefault().GradeManageNo;
            newObj.RevisionDate = ModelService.GetChildList<TN_MOLD1600>(x => x.GradeManageNo == newObj.GradeManageNo).LastOrDefault().RevisionDate;
            newObj.MoldMCode = masterObj.MoldMCode;
            newObj.QcDate = DateTime.Now;
            newObj.WorkId = GlobalVariable.LoginId;
            newObj.NewRowFlag = "Y";

            DetailGridBindingSource.Add(newObj);
        }

        protected override void DeleteDetailRow()
        {
            //var masterObj = MasterGridBindingSource.Current as TEMP_XFMOLD1700_MASTER;
            var masterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            var detailObj = DetailGridBindingSource.Current as TN_MOLD1700;

            if (masterObj == null || detailObj == null) return;

            if (detailObj.TN_MOLD1701List.Count != 0)
            {
                MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_142));
                return;
            }

            DetailGridBindingSource.RemoveCurrent();
            if(detailObj.NewRowFlag != "Y")
                ModelService.Delete(detailObj);
        }

        protected override void SubDetailAddRowClicked()
        {
            TN_MOLD1700 detailObj = DetailGridBindingSource.Current as TN_MOLD1700;
            if (detailObj == null) return;

            if (detailObj.TN_MOLD1701List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_138), LabelConvert.GetLabelText("MoldClassAppList")));
                return;
            }

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                SqlParameter moldMCode = new SqlParameter("@MoldMCode", detailObj.MoldMCode);
                SqlParameter revisionDate = new SqlParameter("@CheckDate", detailObj.QcDate);

                var result = context.Database.SqlQuery<TEMP_XFMOLD1700_DETAIL>("USP_GET_XFMOLD1700_DETAIL @MoldMCode, @CheckDate", moldMCode, revisionDate).ToList();

                foreach (var s in result)
                {
                    TN_MOLD1701 newObj = new TN_MOLD1701();
                    newObj.MeaNo = detailObj.MeaNo;
                    newObj.Seq = detailObj.TN_MOLD1701List.Count == 0 ? 1 : detailObj.TN_MOLD1701List.Count + 1;
                    newObj.EvaluationItem = s.EvaluationItem;                    
                    //newObj.TN_MOLD1700 = detailObj;
                    //detailObj.TN_MOLD1701List.Add(newObj);

                    SubDetailGridBindingSource.Add(newObj);
                }

                SubDetailGridExControl.MainGrid.BestFitColumns();
            }
        }

        protected override void DeleteSubDetailRow()
        {
            TN_MOLD1700 detailObj = DetailGridBindingSource.Current as TN_MOLD1700;
            TN_MOLD1701 subDetailObj = SubDetailGridBindingSource.Current as TN_MOLD1701;

            if (detailObj == null || subDetailObj == null) return;

            List<TN_MOLD1701> subDetailList = SubDetailGridBindingSource.List as List<TN_MOLD1701>;

            //삭제전 금형평가 상세가 1개이하 일경우 TN_MOLD1700, TN_MOLD1100의 금형등급 NULL 처리
            if (subDetailList.Count <= 1)
            {
                TN_MOLD1100 masterObj = MasterGridBindingSource.Current as TN_MOLD1100;
                detailObj.Grade = null;
                masterObj.MoldClass = null;

                DetailGridExControl.BestFitColumns();
                MasterGridExControl.BestFitColumns();
            }

            SubDetailGridBindingSource.RemoveCurrent();
        }

        private void MainView_FocusedRowChanged(object sender, EventArgs e)
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            var masterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            if (masterObj == null) return;

            string moldMCode = masterObj.MoldMCode.GetNullToEmpty();
            DetailGridBindingSource.DataSource = masterObj.TN_MOLD1700List;

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void DetailMainView_FocusedRowChanged(object sender, EventArgs e)
        {
            SubDetailGridExControl.MainGrid.Clear();

            TN_MOLD1700 detailObj = DetailGridBindingSource.Current as TN_MOLD1700;
            if (detailObj == null) return;

            SubDetailGridBindingSource.DataSource = detailObj.TN_MOLD1701List;
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;

            SubDetailGridExControl.BestFitColumns();
        }

        private void DetailMainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            var detailObj = DetailGridBindingSource.Current as TN_MOLD1700;

            if (masterObj == null || detailObj == null) return;

            string colName = e.Column.FieldName;

            if (colName == "QcDate")
            {
                var tN_MOLD1600 = ModelService.GetChildList<TN_MOLD1600>(x => x.RevisionDate <= detailObj.QcDate).LastOrDefault();

                if (tN_MOLD1600 != null)
                {
                    detailObj.RevisionDate = tN_MOLD1600.RevisionDate;
                    detailObj.GradeManageNo = tN_MOLD1600.GradeManageNo;
                }
            }
        }

        private void SubDetailMainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_MOLD1700;
            var subObj = SubDetailGridBindingSource.Current as TN_MOLD1701;
            if (subObj == null) return;

            string colName = e.Column.FieldName;

            //if (colName == "InValue")
            //{
                // SP_GET_GRADE_SCORE_MOLD
                string sqlQuery = string.Format("{0} '{1}','{2}','{3}',{4}", "EXEC SP_GET_GRADE_SCORE_MOLD", detailObj.GradeManageNo, detailObj.RevisionDate, subObj.EvaluationItem, subObj.InValue);
                subObj.Score = DbRequestHandler.GetCellValue(sqlQuery, 0).GetIntNullToZero();

                SetGrade();

                SubDetailGridExControl.MainGrid.BestFitColumns();
            //}
        }

        private void SetGrade()
        {
            TN_MOLD1100 masterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            TN_MOLD1700 detailObj = DetailGridBindingSource.Current as TN_MOLD1700;

            int sc = 0;
            foreach (var s in detailObj.TN_MOLD1701List)
            {
                sc += s.Score;
            }

            detailObj.TotalScore = sc;
            string sqlQuery = string.Format("{0} '{1}','{2}',{3}", "EXEC SP_GET_GRADE_MOLD", detailObj.GradeManageNo, detailObj.RevisionDate, detailObj.TotalScore);
            detailObj.Grade = DbRequestHandler.GetCellValue(sqlQuery, 0).GetNullToNull();
            masterObj.MoldClass = detailObj.Grade;

            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void MainView_RowStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.RowHandle != gv.FocusedRowHandle)
            {
                if (e.Column.Name == "MoldGrade")
                {
                    switch (e.CellValue/*Grade*/)
                    {
                        case "01":
                            e.Appearance.BackColor = Color.Red;
                            break;
                        case "02":
                            e.Appearance.BackColor = Color.Orange;
                            break;
                        case "03":
                            e.Appearance.BackColor = Color.Yellow;
                            break;
                        case "04":
                            e.Appearance.BackColor = Color.Green;
                            break;
                        case "05":
                            e.Appearance.BackColor = Color.Cyan;
                            break;
                        case "06":
                            e.Appearance.BackColor = Color.Azure;
                            break;
                        case "07":
                            e.Appearance.BackColor = Color.Blue;
                            break;
                        default:
                            break;
                    }

                }
            }
        }

        private void SetGridBarButton()
        {
            var barButtonPrint1 = new DevExpress.XtraBars.BarButtonItem();
            barButtonPrint1.Id = 4;
            barButtonPrint1.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/bosale");
            //barButtonDevide.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonAdd.ImageOptions.LargeImage")));
            barButtonPrint1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barButtonPrint1.Name = "barButtonPrint1";
            barButtonPrint1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonPrint1.ShortcutKeyDisplayString = "Alt+P";
            barButtonPrint1.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonPrint1.Caption = LabelConvert.GetLabelText("GraphOutPut") + "[Alt+P]";
            barButtonPrint1.Alignment = BarItemLinkAlignment.Right;
            barButtonPrint1.ItemClick += BarButtonPrint1_ItemClick;
            MasterGridExControl.BarTools.AddItem(barButtonPrint1);

            var barButtonPrint2 = new DevExpress.XtraBars.BarButtonItem();
            barButtonPrint2.Id = 5;
            barButtonPrint2.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/bosale");
            //barButtonDevide.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonAdd.ImageOptions.LargeImage")));
            barButtonPrint2.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.Q));
            barButtonPrint2.Name = "barButtonPrint2";
            barButtonPrint2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonPrint2.ShortcutKeyDisplayString = "Alt+Q";
            barButtonPrint2.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonPrint2.Caption = LabelConvert.GetLabelText("ListOutPut") + "[Alt+Q]";
            barButtonPrint2.Alignment = BarItemLinkAlignment.Right;
            barButtonPrint2.ItemClick += BarButtonPrint2_ItemClick;
            MasterGridExControl.BarTools.AddItem(barButtonPrint2);
        }

        private void BarButtonPrint1_ItemClick(object sender, ItemClickEventArgs e)
        {
            REPORT.XRMOLD1700 report = new REPORT.XRMOLD1700();
            report.CreateDocument();

            report.PrintingSystem.ShowMarginsWarning = false;
            report.ShowPrintStatusDialog = false;
            report.ShowPreview();
        }

        private void BarButtonPrint2_ItemClick(object sender, ItemClickEventArgs e)
        {
            REPORT.XRMOLD1701 report = new REPORT.XRMOLD1701();
            report.CreateDocument();

            report.PrintingSystem.ShowMarginsWarning = false;
            report.ShowPrintStatusDialog = false;
            report.ShowPreview();
        }

    }
}