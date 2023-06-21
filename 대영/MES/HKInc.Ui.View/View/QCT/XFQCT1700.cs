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
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Handler.EventHandler;
using DevExpress.XtraGrid.Views.BandedGrid;
using HKInc.Service.Controls;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.QCT
{
    /// <summary>
    /// 교육계획 관리
    /// </summary>
    public partial class XFQCT1700 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1700> ModelService = (IService<TN_QCT1700>)ProductionFactory.GetDomainService("TN_QCT1700");

        public XFQCT1700()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            GridExControl.MainGrid.MainView = new BandedGridView();
            GridExControl.MainGrid.ViewType = GridViewType.BandedGridView;
            
            dt_PlanDate.SetTodayIsMonth();

            GridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            GridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;

        }

       
        protected override void InitCombo()
        {   
            lup_EduFlag.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.EduFlag));
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "Seq", true);
            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            GridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), false);
            GridExControl.MainGrid.AddColumn("EduFlag", LabelConvert.GetLabelText("EduFlag"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("EduContent", LabelConvert.GetLabelText("EduContent"));
            GridExControl.MainGrid.AddColumn("EduOrgan", LabelConvert.GetLabelText("EduOrgan"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("EduObj", LabelConvert.GetLabelText("EduObj"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("EduTime", LabelConvert.GetLabelText("EduTime"));
            GridExControl.MainGrid.AddColumn("EduPlanStart", LabelConvert.GetLabelText("EduPlanStart"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("EduPlanEnd", LabelConvert.GetLabelText("EduPlanEnd"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("EduBudget", LabelConvert.GetLabelText("EduBudget"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("EduStart", LabelConvert.GetLabelText("EduStart"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("EduEnd", LabelConvert.GetLabelText("EduEnd"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("EduId", LabelConvert.GetLabelText("EduId"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));


            BandedGridView view = GridExControl.MainGrid.MainView as BandedGridView;
            view.OptionsView.ShowIndicator = false;

            //BandedGridEx.SetGridBandAddedEx(view, LabelConvert.GetLabelText("Seq"), "Seq", "Seq");
            BandedGridEx.SetGridBandAddedEx(view, LabelConvert.GetLabelText("Select"), "_Check", "_Check");
            BandedGridEx.SetGridBandAddedEx(view, LabelConvert.GetLabelText("EduFlag"), "EduFlag", "EduFlag");
            BandedGridEx.SetGridBandAddedEx(view, LabelConvert.GetLabelText("EduContent"), "EduContent", "EduContent");
            BandedGridEx.SetGridBandAddedEx(view, LabelConvert.GetLabelText("EduOrgan"), "EduOrgan", "EduOrgan");
            BandedGridEx.SetGridBandAddedEx(view, LabelConvert.GetLabelText("EduObj"), "EduObj", "EduObj");
            BandedGridEx.SetGridBandAddedEx(view, LabelConvert.GetLabelText("EduTime"), "EduTime", "EduTime");

            BandedGridEx.SetGridBandAddedEx(view, LabelConvert.GetLabelText("Plan"), "Plan", new string[] { "EduPlanStart", "EduPlanEnd"});

            BandedGridEx.SetGridBandAddedEx(view, LabelConvert.GetLabelText("EduBudget"), "EduBudget", "EduBudget");

            BandedGridEx.SetGridBandAddedEx(view, LabelConvert.GetLabelText("Result"), "Result", new string[] { "EduStart", "EduEnd" });

            BandedGridEx.SetGridBandAddedEx(view, LabelConvert.GetLabelText("EduId"), "EduId", "EduId");
            BandedGridEx.SetGridBandAddedEx(view, LabelConvert.GetLabelText("Memo"), "Memo", "Memo");

            BandedGridViewPainter painter = new BandedGridViewPainter(view, new GridBand[] { (GridBand)view.Bands["_Check"], (GridBand)view.Bands["EduFlag"], (GridBand)view.Bands["EduContent"], (GridBand)view.Bands["EduOrgan"]
                              , (GridBand)view.Bands["EduObj"], (GridBand)view.Bands["EduTime"], (GridBand)view.Bands["EduBudget"], (GridBand)view.Bands["EduId"], (GridBand)view.Bands["Memo"] });

            

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "EduFlag", "EduContent", "EduOrgan", "EduObj", "EduTime", "EduPlanStart", "EduPlanEnd", "EduBudget", "EduStart", "EduEnd", "EduId", "Memo");


            var barDocumentPrint = new DevExpress.XtraBars.BarButtonItem();
            barDocumentPrint.Id = 5;
            barDocumentPrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/boreport2");
            barDocumentPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barDocumentPrint.Name = "Print";
            barDocumentPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barDocumentPrint.ShortcutKeyDisplayString = "Alt+P";
            barDocumentPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barDocumentPrint.Caption = LabelConvert.GetLabelText("Print") + "[Alt+P]";
            barDocumentPrint.ItemClick += Print_Click; ;
            barDocumentPrint.Enabled = UserRight.HasEdit;
            barDocumentPrint.Alignment = BarItemLinkAlignment.Right;
            GridExControl.BarTools.AddItem(barDocumentPrint);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1700>(GridExControl);

            GridExControl.MainGrid.Columns["EduFlag"].AppearanceHeader.ForeColor = Color.Red;
            GridExControl.MainGrid.Columns["EduContent"].AppearanceHeader.ForeColor = Color.Red;
            GridExControl.MainGrid.Columns["EduPlanStart"].AppearanceHeader.ForeColor = Color.Red;
            GridExControl.MainGrid.Columns["EduPlanEnd"].AppearanceHeader.ForeColor = Color.Red;

        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("EduFlag", DbRequestHandler.GetCommTopCode(MasterCodeSTR.EduFlag), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("EduObj", ModelService.GetChildList<User>(p => p.ScmYn == "N" && p.Active == "Y" && p.MainYn == "02"), "LoginId", "UserName", true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("EduId", ModelService.GetChildList<User>(p => p.ScmYn == "N" && p.Active == "Y" && p.MainYn == "02"), "LoginId", "UserName", true);
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("Seq", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            //데이터리로드
            InitRepository();
            InitCombo();

            var EduContent = tx_EduContent.EditValue.GetNullToEmpty();
            var EduFlag = lup_EduFlag.EditValue.GetNullToEmpty();
            //int year = dp_dt.DateTime.Year;

            GridBindingSource.DataSource = ModelService.GetList(p => (p.EduPlanStart >= dt_PlanDate.DateFrEdit.DateTime && p.EduPlanEnd <= dt_PlanDate.DateToEdit.DateTime)
                                                                  //(p.EduPlanStart.Year >= year && p.EduPlanEnd.Year <= year)
                                                                  && (string.IsNullOrEmpty(EduFlag) ? true : (p.EduFlag.Contains(EduFlag)))
                                                                  && (string.IsNullOrEmpty(EduContent) ? true : (p.EduContent.Contains(EduContent)))
                                                               )
                                                               .OrderBy(p => p.EduPlanStart)
                                                               .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
        }

        protected override void AddRowClicked()
        {
            TN_QCT1700 newobj = new TN_QCT1700();
            {
                newobj.EduPlanStart = DateTime.Today;
                newobj.EduPlanEnd = DateTime.Today;
                newobj.EduStart = DateTime.Today;
                newobj.EduEnd = DateTime.Today;
                newobj.EditRowFlag = "Y";
                newobj.EduBudget = 0;
            };

            GridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            GridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var delobj = GridBindingSource.Current as TN_QCT1700;

            if (delobj != null)
            {
                GridBindingSource.Remove(delobj);
                ModelService.Delete(delobj);
                GridExControl.BestFitColumns();
            }
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //var obj = GridBindingSource.Current as TN_QCT1800;
            //obj.EditRowFlag = "Y";
            GridExControl.BestFitColumns();
        }


        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

          

            ModelService.Save();
            DataLoad();
        }

        /// <summary>출력 </summary>
        private void Print_Click(object sender, ItemClickEventArgs e)
        {
            var masterList = GridBindingSource.List as List<TN_QCT1700>;
            if (masterList == null || masterList.Count == 0) return;

            GridExControl.MainGrid.PostEditor();

            try
            {
                WaitHandler.ShowWait();

                var printList = masterList.Where(x => x._Check == "Y").ToList();
                if (printList == null || printList.Count == 0) return;

                var mainReport = new REPORT.XRFQCT1700(printList);
                mainReport.CreateDocument();

                GridExControl.BestFitColumns();
                mainReport.PrintingSystem.ShowMarginsWarning = false;
                mainReport.ShowPrintStatusDialog = false;
                mainReport.ShowPreview();

                foreach (var s in printList)
                    s._Check = "N";
            }
            catch(Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex.Message);
            }
            finally
            {
                WaitHandler.CloseWait();
                GridExControl.BestFitColumns();
            }
        }

        private void MainView_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "EduObj")
            {
                var eduflag = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "EduObj").GetNullToEmpty();
                var objchk = ModelService.GetChildList<User>(p => p.LoginId == eduflag).FirstOrDefault();
                if (objchk == null)
                {
                    e.DisplayText = "전사원";
                }                
            }

            if (e.Column.FieldName == "EduBudget")
            {
                var edubudget = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "EduBudget").GetNullToEmpty();                
                if (edubudget == null || edubudget == "0" || edubudget == "" || edubudget == "0.00")
                {
                    e.DisplayText = "없음";
                }
                
            }
        }


    }
}