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
using HKInc.Service.Service;
using HKInc.Utils.Common;
using DevExpress.Utils;
using HKInc.Utils.Enum;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;

namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 2021-06-03 김진우 주임 생성
    /// 설비등급관리평가
    /// </summary>
    public partial class XFMEA1301 : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");

        public XFMEA1301()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += SubView_CellValueChanged;
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowStyle;
        }

        protected override void InitCombo()
        {
            lup_MachineName.SetDefault(false, true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetList(p => true).ToList());
        }

        protected override void InitGrid()
        {
            // MEA1000
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("MachineMCode", LabelConvert.GetLabelText("MachineCode"));
            MasterGridExControl.MainGrid.AddColumn("MachineName", LabelConvert.GetLabelText("MachineName"));
            MasterGridExControl.MainGrid.AddColumn("Class", LabelConvert.GetLabelText("MachineGrade"));
            MasterGridExControl.MainGrid.AddColumn("Score", LabelConvert.GetLabelText("MachineScroe"));
            MasterGridExControl.MainGrid.AddColumn("ClassDate", LabelConvert.GetLabelText("MachineGDate"));
            MasterGridExControl.MainGrid.AddColumn("Model", LabelConvert.GetLabelText("Model"));
            MasterGridExControl.MainGrid.AddColumn("Maker", LabelConvert.GetLabelText("Maker"));
            MasterGridExControl.MainGrid.AddColumn("InstallDate", LabelConvert.GetLabelText("InstallDate"));
            MasterGridExControl.MainGrid.AddColumn("ManufactureDate", LabelConvert.GetLabelText("ProductionDate"));
            MasterGridExControl.MainGrid.AddColumn("SerialNo", LabelConvert.GetLabelText("SerialNo"));

            // MEQ1300
            DetailGridExControl.MainGrid.AddColumn("MeaNo", LabelConvert.GetLabelText("MeaNo"), false);                     // 설비등급평가번호
            DetailGridExControl.MainGrid.AddColumn("MachineMCode", LabelConvert.GetLabelText("MachineCode"), false);        // 설비코드
            DetailGridExControl.MainGrid.AddColumn("GradeManageNo", LabelConvert.GetLabelText("GradeManageNo"));            // 등급관리번호
            DetailGridExControl.MainGrid.AddColumn("QcDate", LabelConvert.GetLabelText("QcDate"), HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));                          // 평가자
            DetailGridExControl.MainGrid.AddColumn("TotalScore", LabelConvert.GetLabelText("TotalScore"));                  // 평가 값
            DetailGridExControl.MainGrid.AddColumn("Grade", LabelConvert.GetLabelText("Grade"));                            // 등급
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "QcDate", "Memo");

            // MEQ1301
            SubDetailGridExControl.MainGrid.AddColumn("MeaNo", LabelConvert.GetLabelText("MeaNo"), false);
            SubDetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), false);
            SubDetailGridExControl.MainGrid.AddColumn("EvaluationItem", LabelConvert.GetLabelText("MachineJudgeItem"));            
            SubDetailGridExControl.MainGrid.AddColumn("InValue", LabelConvert.GetLabelText("JudgeValue"));
            SubDetailGridExControl.MainGrid.AddColumn("Score", LabelConvert.GetLabelText("Score"));
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InValue");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEQ1300>(DetailGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEQ1301>(SubDetailGridExControl);

            SetGridBarButton();
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineMaker), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineMCode", ModelService.GetList(p => p.UseFlag == "Y"), "MachineMCode", Service.Helper.DataConvert.GetCultureDataFieldName("MachineName"));
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("EvaluationItem", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGradeEvaluationList), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();
            InitCombo();

            string MachineCode = lup_MachineName.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(MachineCode) ? true : p.MachineMCode == MachineCode)
                                                                        && p.UseFlag == "Y"
                                                                        ).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            var masterObj = MasterGridBindingSource.Current as TN_MEA1000;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
        
            DetailGridBindingSource.DataSource = masterObj.TN_MEQ1300List;
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            TN_MEA1000 MasterObj = MasterGridBindingSource.Current as TN_MEA1000;
            TN_MEQ1300 NewObj = new TN_MEQ1300();

            NewObj.MeaNo = DbRequestHandler.GetSeqMonth("MGRD");
            NewObj.MachineMCode = MasterObj.MachineMCode;
            NewObj.GradeManageNo = ModelService.GetChildList<TN_MEA1300>(p => true).OrderBy(p => p.RevDate).LastOrDefault().GradeManageNo; 
            NewObj.QcDate = DateTime.Now;
            NewObj.WorkId = GlobalVariable.LoginId;

            DetailGridBindingSource.Add(NewObj);
        }

        protected override void DeleteDetailRow()
        {
            TN_MEA1000 MasterObj = MasterGridBindingSource.Current as TN_MEA1000;
            TN_MEQ1300 DetailObj = DetailGridBindingSource.Current as TN_MEQ1300;
            if (DetailObj == null) return;

            if (DetailObj.TN_MEQ1301List.Count != 0)
            {
                MessageBoxHandler.Show(string.Format(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_137), HelperFactory.GetLabelConvert().GetLabelText("MachineGradeJudgeDetailList"), HelperFactory.GetLabelConvert().GetLabelText("MachineGradeJudgeList")));
                return;
            }

            DetailGridBindingSource.RemoveCurrent();
  
            if (DetailObj.NewRowFlag == "N")
                ModelService.RemoveChild(DetailObj);
           
        }

        protected override void DetailFocusedRowChanged()
        {
            TN_MEQ1300 DetailObj = DetailGridBindingSource.Current as TN_MEQ1300;
            if (DetailObj == null) return;
            SubDetailGridExControl.MainGrid.Clear();
            SubDetailGridBindingSource.DataSource = DetailObj.TN_MEQ1301List;
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;

            SubDetailGridExControl.BestFitColumns();
        }

      
        // SUB 추가
        protected override void SubDetailAddRowClicked()
        {
            TN_MEQ1300 DetailObj = DetailGridBindingSource.Current as TN_MEQ1300;
            if (DetailObj == null) return;

            if (DetailObj.TN_MEQ1301List.Count >= 1)
            {
                MessageBoxHandler.Show(string.Format(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_138), HelperFactory.GetLabelConvert().GetLabelText("MachineGradeJudgeList")));
                return;
            }

            DataSet qclist = DbRequestHandler.GetDataQury("SELECT [EVALUATION_ITEM]  as EvaluationItem FROM TN_MEA1301T where GRADE_MANAGE_NO='"+ DetailObj.GradeManageNo.ToString() + "' group by [EVALUATION_ITEM]");
            if (qclist == null) { return; }

            for(int i=0;i< qclist.Tables[0].Rows.Count;i++)
            {
                TN_MEQ1301 NewObj = new TN_MEQ1301();

                NewObj.MeaNo = DetailObj.MeaNo;
                NewObj.Seq = i+1;
                NewObj.EvaluationItem = qclist.Tables[0].Rows[i][0].GetNullToEmpty();
                    
                SubDetailGridBindingSource.Add(NewObj);
            }
        }

        // SUB 삭제
        protected override void DeleteSubDetailRow()
        {
            TN_MEQ1300 DetailObj = DetailGridBindingSource.Current as TN_MEQ1300;
            TN_MEQ1301 SubdetailObj = SubDetailGridBindingSource.Current as TN_MEQ1301;
            if (SubdetailObj == null) return;
          
            SubDetailGridBindingSource.RemoveCurrent();
        }

        private void SubView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TN_MEQ1300 DetailObj = DetailGridBindingSource.Current as TN_MEQ1300;
            TN_MEQ1301 SubdetailObj = SubDetailGridBindingSource.Current as TN_MEQ1301;
            SubdetailObj.Score = DbRequestHandler.GetCellValue("exec SP_GET_GRADE_SCORE '" + DetailObj.GradeManageNo + "','" + SubdetailObj.EvaluationItem + "','" + SubdetailObj.InValue.ToString() + "'", 0).GetIntNullToZero();
            Grade();
            SubDetailGridExControl.MainGrid.BestFitColumns();
        }

        private void Grade()
        {
            TN_MEQ1300 DetailObj = DetailGridBindingSource.Current as TN_MEQ1300;
            TN_MEA1000 MasterObj = MasterGridBindingSource.Current as TN_MEA1000;

            int sc = 0;

            foreach (var v in DetailObj.TN_MEQ1301List)
                sc += v.Score;

            DetailObj.TotalScore = sc;
            DetailObj.Grade = DbRequestHandler.GetCellValue("exec SP_GET_GRADE '" + DetailObj.TotalScore.ToString() + "'", 0).ToString();

            MasterObj.Class = DetailObj.Grade;
            MasterObj.ClassDate = DetailObj.QcDate;
            MasterObj.Score = DetailObj.TotalScore;

            MasterGridExControl.MainGrid.BestFitColumns();
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        private void MainView_RowStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.RowHandle != gv.FocusedRowHandle)
            {
                switch (e.CellValue)
                {
                    case "A":
                        e.Appearance.BackColor = Color.Red;
                        break;
                    case "B":
                        e.Appearance.BackColor = Color.Orange;
                        break;
                    case "C":
                        e.Appearance.BackColor = Color.Yellow;
                        break;
                    case "D":
                        e.Appearance.BackColor = Color.Green;
                        break;
                    case "E":
                        e.Appearance.BackColor = Color.Cyan;
                        break;
                    case "F":
                        e.Appearance.BackColor = Color.Azure;
                        break;
                    case "G":
                        e.Appearance.BackColor = Color.Blue;
                        break;
                    default:
                        break;
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
            REPORT.XRMEA1300 report = new REPORT.XRMEA1300();
            report.CreateDocument();

            report.PrintingSystem.ShowMarginsWarning = false;
            report.ShowPrintStatusDialog = false;
            report.ShowPreview();
        }

        private void BarButtonPrint2_ItemClick(object sender, ItemClickEventArgs e)
        {
            REPORT.XRMEA1301 report = new REPORT.XRMEA1301();
            report.CreateDocument();

            report.PrintingSystem.ShowMarginsWarning = false;
            report.ShowPrintStatusDialog = false;
            report.ShowPreview();
        }


    }
}