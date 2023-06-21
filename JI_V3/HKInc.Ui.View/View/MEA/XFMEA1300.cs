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
    /// 2021-05-24 김진우 주임 생성
    /// 설비등급관리기준
    /// </summary>
    public partial class XFMEA1300 : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_MEA1300> ModelService = (IService<TN_MEA1300>)ProductionFactory.GetDomainService("TN_MEA1300");
        IService<TN_MEA1302> SubModelService = (IService<TN_MEA1302>)ProductionFactory.GetDomainService("TN_MEA1302");
        /// <summary>
        /// 2021-06-30 김진우 주임 추가    개정일자에 오류가 있을시 저장되지 않도록 하는 플래그
        /// </summary>
        bool CheckSave;

        public XFMEA1300()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            dt_CheckDate.DateFrEdit.DateTime = DateTime.Today.AddMonths(-1);
            dt_CheckDate.DateToEdit.DateTime = DateTime.Today.AddMonths(1);

            MasterGridExControl.MainGrid.MainView.CellValueChanged+= DetailView_CellValueChanged;
            MasterGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;

            CheckSave = true;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("GradeManageNo", LabelConvert.GetLabelText("GradeManageNo"), false);
            MasterGridExControl.MainGrid.AddColumn("RevDate", LabelConvert.GetLabelText("RevisionDate"));               // 개정일자
            MasterGridExControl.MainGrid.AddColumn("RegDate", LabelConvert.GetLabelText("RegistDate"), HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");     // 등록일자
            MasterGridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("CreateId"));                    // 등록자
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));                          // 메모
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "RevDate", "RegDate", "Memo");

            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Num"),false);                      // NO.
            DetailGridExControl.MainGrid.AddColumn("EvaluationItem", LabelConvert.GetLabelText("JudgeItem"));           // 평가항목
            DetailGridExControl.MainGrid.AddColumn("EvaluationStand", LabelConvert.GetLabelText("JudgeStd"));           // 평가기준
            DetailGridExControl.MainGrid.AddColumn("EvaluationValueMin", LabelConvert.GetLabelText("MinValue"));        // 최소값
            DetailGridExControl.MainGrid.AddColumn("EvaluationValueMax", LabelConvert.GetLabelText("MaxValue"));        // 최대값
            DetailGridExControl.MainGrid.AddColumn("Score", LabelConvert.GetLabelText("Distribution"));                 // 배점
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));                          // 메모
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "EvaluationItem", "EvaluationStand", "EvaluationValueMax", "EvaluationValueMin", "Score", "Memo");

            SubDetailGridExControl.MainGrid.AddColumn("DisplayOrder", LabelConvert.GetLabelText("Num"),false);                        // NO.
            SubDetailGridExControl.MainGrid.AddColumn("GradeStandCode", LabelConvert.GetLabelText("JudgeGrade"));               // 평가등급
            SubDetailGridExControl.MainGrid.AddColumn("GradeValueMin", LabelConvert.GetLabelText("MinValue"));                  // 최소값
            SubDetailGridExControl.MainGrid.AddColumn("GradeValueMax", LabelConvert.GetLabelText("MaxValue"));                  // 최대값
            SubDetailGridExControl.MainGrid.AddColumn("GradeStand", LabelConvert.GetLabelText("GradeStdContent"));              // 등급기준 내용
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "GradeStandCode", "GradeValueMin", "GradeValueMax", "GradeStand");


            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1300>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1301>(DetailGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1302>(SubDetailGridExControl);
        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("RevDate");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");

            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("EvaluationItem", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGradeEvaluationList), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("GradeStandCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineClass), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();
            SubModelService.ReLoad();

            InitRepository();
            InitCombo();

            DateTime FromDate = dt_CheckDate.DateFrEdit.DateTime;
            DateTime ToDate = dt_CheckDate.DateToEdit.DateTime;

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.RevDate >= FromDate) && (p.RevDate <= ToDate)).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();

            SubLoad();

            GridRowLocator.SetCurrentRow();
            CheckSave = true;
        }

        protected override void AddRowClicked()
        {
            TN_MEA1300 NewObj = new TN_MEA1300();

            NewObj.GradeManageNo = DbRequestHandler.GetSeqStandard("GRADE");
            NewObj.RevDate = DateTime.Today;
            NewObj.RegDate = DateTime.Today;
            NewObj.WorkId = GlobalVariable.LoginId;
            NewObj.NewRowFlag = "Y";
            NewObj.UseFlag = "Y";

            ModelService.Insert(NewObj);
            MasterGridBindingSource.Add(NewObj);
        }

        protected override void DeleteRow()
        {
            TN_MEA1300 Obj = MasterGridBindingSource.Current as TN_MEA1300;
            if (Obj != null)
            {
                if (Obj.TN_MEA1301List.Any(p => p.GradeManageNo == Obj.GradeManageNo))
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_134));
                else
                {
                    MasterGridBindingSource.Remove(Obj);
                    ModelService.Delete(Obj);
                }
            }
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1300;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_MEA1301List.OrderBy(p => p.EvaluationItem).ThenBy(p => p.Score).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void SubLoad()
        {
            SubDetailGridBindingSource.DataSource = SubModelService.GetList(p => true).OrderBy(p => p.GradeStandCode).ToList();
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;

            SubDetailGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            TN_MEA1300 MasterObj = MasterGridBindingSource.Current as TN_MEA1300;
            if (MasterObj == null) return;

            List<TN_MEA1301> DetailList = DetailGridBindingSource.DataSource as List<TN_MEA1301>;
            if (DetailList == null) return;

            TN_MEA1301 NewObj = new TN_MEA1301();

            NewObj.GradeManageNo = MasterObj.GradeManageNo;
            NewObj.Seq = MasterObj.TN_MEA1301List.Count == 0 ? 1 : MasterObj.TN_MEA1301List.Max(p => p.Seq) + 1;

            ModelService.InsertChild(NewObj);
            MasterObj.TN_MEA1301List.Add(NewObj);
            DetailGridBindingSource.Add(NewObj);
        }

        protected override void DeleteDetailRow()
        {
            TN_MEA1300 MasterObj = MasterGridBindingSource.Current as TN_MEA1300;
            if (MasterObj == null) return;

            TN_MEA1301 DetailObj = DetailGridBindingSource.Current as TN_MEA1301;
            if (DetailObj == null) return;

            ModelService.RemoveChild(DetailObj);
            MasterObj.TN_MEA1301List.Remove(DetailObj);
            DetailGridBindingSource.RemoveCurrent();
        }

        // SUB 추가
        protected override void SubDetailAddRowClicked()
        {
            TN_MEA1300 MasterObj = MasterGridBindingSource.Current as TN_MEA1300;
            if (MasterObj == null) return;

            TN_MEA1302 NewObj = new TN_MEA1302();

            NewObj.GradeStandCode = MasterObj.GradeManageNo;

            SubModelService.InsertChild(NewObj);
            SubDetailGridBindingSource.Add(NewObj);
        }

        // SUB 삭제
        protected override void DeleteSubDetailRow()
        {
            TN_MEA1302 SubDetailObj = SubDetailGridBindingSource.Current as TN_MEA1302;
            if (SubDetailObj == null) return;

            SubModelService.RemoveChild(SubDetailObj);
            SubDetailGridBindingSource.RemoveCurrent();
        }

        protected override void DataSave()
        {
            GridRowLocator.GetCurrentRow();

            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();

            List<TN_MEA1300> MasterList = MasterGridBindingSource.DataSource as List<TN_MEA1300>;

            DateTime DBLastDate = ModelService.GetList(p => true).OrderBy(p => p.RevDate).ToList().Count == 0 ? Convert.ToDateTime("2000-01-01 12:00:00") :
                                  ModelService.GetList(p => true).OrderBy(p => p.RevDate).LastOrDefault().RevDate;

            DateTime GridLastDate = MasterList.Where(p => p.NewRowFlag == "N").OrderBy(p => p.RevDate).ToList().Count() == 0 ? Convert.ToDateTime("2000-01-02 12:00:00") :
                                    MasterList.Where(p => p.NewRowFlag == "N").OrderBy(p => p.RevDate).LastOrDefault().RevDate;

            DateTime LastDate = DBLastDate > GridLastDate ? DBLastDate : GridLastDate;

            foreach (var v in MasterList)
            {
                if (v.NewRowFlag == "Y")
                {
                    if (v.RevDate <= LastDate)
                    {
                        MessageBoxHandler.Show("이전 설비등급관리목록의 개정일자보다 이후의 날짜로 수정하여 주십시오.");
                        CheckSave = false;
                        return;
                    }
                    LastDate = v.RevDate;
                    CheckSave = true;
                }

                List<TN_MEA1300> CheckData = MasterList.Where(p => p.GradeManageNo != v.GradeManageNo).ToList() as List<TN_MEA1300>;

                if (CheckData.Any(p => p.RevDate == v.RevDate))
                {
                    MessageBoxHandler.Show("개정일자가 중복됩니다 확인하십시오.");
                    CheckSave = false;
                    return;
                }
                else
                    CheckSave = true;
            }

            if (!CheckSave)
            {
                MessageBoxHandler.Show("설비등급관리목록의 개정일자를 수정하십시오.");
                return;
            }

            SubModelService.Save();
            ModelService.Save();
            DataLoad();
            GridRowLocator.SetCurrentRow();

        }

        // 개정일자 저장시 수정 불가
        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView gv = sender as GridView;
            var MasterObj = MasterGridBindingSource.Current as TN_MEA1300;
            if (MasterObj == null) return;

            if (gv.FocusedColumn.FieldName == "RevDate")
            {
                if (MasterObj.NewRowFlag == "N")
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }

        // 개정일자 수정시 날짜 
        private void DetailView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "RevDate")
                {
                    List<TN_MEA1300> MasterList = MasterGridBindingSource.DataSource as List<TN_MEA1300>;

                    DateTime DBLastDate = ModelService.GetList(p => true).OrderBy(p => p.RevDate).ToList().Count == 0 ? Convert.ToDateTime("2000-01-01 12:00:00") :
                                          ModelService.GetList(p => true).OrderBy(p => p.RevDate).LastOrDefault().RevDate;

                    DateTime GridLastDate = MasterList.Where(p => p.NewRowFlag == "N").OrderBy(p => p.RevDate).ToList().Count() == 0 ? Convert.ToDateTime("2000-01-02 12:00:00") :
                                            MasterList.Where(p => p.NewRowFlag == "N").OrderBy(p => p.RevDate).LastOrDefault().RevDate;

                    DateTime LastDate = DBLastDate > GridLastDate ? DBLastDate : GridLastDate;

                    foreach (var v in MasterList)
                    {
                        if (v.NewRowFlag == "Y")
                        {
                            if (v.RevDate <= LastDate)
                            {
                                MessageBoxHandler.Show("이전 설비등급관리목록의 개정일자보다 이후의 날짜로 수정하여 주십시오.");
                                CheckSave = false;
                                return;
                            }
                            LastDate = v.RevDate;
                            CheckSave = true;
                        }

                        List<TN_MEA1300> CheckData = MasterList.Where(p => p.GradeManageNo != v.GradeManageNo).ToList() as List<TN_MEA1300>;

                        if (CheckData.Any(p => p.RevDate == v.RevDate))
                        {
                            MessageBoxHandler.Show("개정일자가 중복됩니다 확인하십시오.");
                            CheckSave = false;
                            return;
                        }
                        else
                            CheckSave = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHandler.Show(ex.ToString());
            }
        }
    
    }
}