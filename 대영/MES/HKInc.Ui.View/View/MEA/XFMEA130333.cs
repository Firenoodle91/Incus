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

namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 2021-05-24 김진우 주임 생성
    /// 설비등급관리이력
    /// </summary>
    public partial class XFMEA130333 : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");

        public XFMEA130333()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
        }

        protected override void InitCombo()
        {
            dt_YearMonth.SetFormat(DateFormat.Year);
            dt_YearMonth.DateTime = DateTime.Today;
            lup_MachineCodeName.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetList(p => true) );
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));            // 설비코드
            MasterGridExControl.MainGrid.AddColumn("MachineName", LabelConvert.GetLabelText("MachineName"));            // 설비명
            MasterGridExControl.MainGrid.AddColumn("Model", LabelConvert.GetLabelText("Model"));                        // 모델
            MasterGridExControl.MainGrid.AddColumn("Maker", LabelConvert.GetLabelText("Maker"));                        // 제작사
            MasterGridExControl.MainGrid.AddColumn("InstallDate", LabelConvert.GetLabelText("InstallDate"));            // 설치일자
            MasterGridExControl.MainGrid.AddColumn("ProductionDate", LabelConvert.GetLabelText("ProductionDate"));      // 제작일자
            MasterGridExControl.MainGrid.AddColumn("SerialNo", LabelConvert.GetLabelText("SerialNo"));                  // S / N
            MasterGridExControl.MainGrid.AddColumn("FinalJudgeDate", LabelConvert.GetLabelText("FinalJudgeDate"));      // 최종평가일자
            MasterGridExControl.MainGrid.AddColumn("TotalScore", LabelConvert.GetLabelText("TotalScore"));              // 종합점수
            MasterGridExControl.MainGrid.AddColumn("JudgeGrade", LabelConvert.GetLabelText("JudgeGrade"));              // 평가등급
            //MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "RevDate", "UpdateTime", "Memo");

            DetailGridExControl.MainGrid.AddColumn("JudgeDate", LabelConvert.GetLabelText("JudgeDate"));                // 평가일
            DetailGridExControl.MainGrid.AddColumn("JudgeRevisionDate", LabelConvert.GetLabelText("JudgeRevisionDate"));// 평가개정일자
            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "EvaluationItem", "EvaluationStand", "EvaluationValueMax", "EvaluationValueMin", "Score", "Memo");

            SubDetailGridExControl.MainGrid.AddColumn("JudgeItem", LabelConvert.GetLabelText("JudgeItem"));             // 평가항목
            SubDetailGridExControl.MainGrid.AddColumn("JudgeValue", LabelConvert.GetLabelText("JudgeValue"));           // 평가값
            SubDetailGridExControl.MainGrid.AddColumn("Score", LabelConvert.GetLabelText("Score"));                     // 점수


            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1300>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1301>(DetailGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1302>(SubDetailGridExControl);
        }
        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("RevDate");
            //MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);

            //DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("EvaluationItem", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGradeEvaluationList), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

        }

        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            DateTime DateMoth = dt_YearMonth.DateTime;

            MasterGridBindingSource.DataSource = ModelService.GetList(p => p.UseFlag == "Y").ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            //MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();

            //SubLoad();
        }

        protected override void AddRowClicked()
        {
            TN_MEA1300 NewObj = new TN_MEA1300();
            NewObj.GradeManageNo = DbRequestHandler.GetSeqStandard("GRADE");
            NewObj.UpdateTime = DateTime.Now;
            NewObj.RevDate = DateTime.Now;
            NewObj.WorkId = GlobalVariable.LoginId;
            NewObj.NewRowFlag = "Y";
            NewObj.UseFlag = "Y";

            //ModelService.Insert(NewObj);
            MasterGridBindingSource.Add(NewObj);
        }

        protected override void DeleteRow()
        {
            TN_MEA1300 Obj = MasterGridBindingSource.Current as TN_MEA1300;
            if (Obj != null)
            {
                if (Obj.TN_MEA1301List.Any(p => p.GradeManageNo == Obj.GradeManageNo))
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_1));
                }
                else
                {
                    MasterGridBindingSource.Remove(Obj);
                    //ModelService.Delete(Obj);
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

            DetailGridBindingSource.DataSource = masterObj.TN_MEA1301List.OrderBy(p => p.EvaluationItem).ThenBy(x => x.DisplayOrder).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        #region 삭제
        //private void SubLoad()
        //{
        //    DataSet ds = DbRequestHandler.GetDataQury("exec USP_GET_XFMPS1200_SUB '" + dt_PlanMonth.DateTime.ToString("yyyyMM") + "'");
        //    if (ds != null)
        //    {
        //        if (SubDetailGridExControl.MainGrid.Columns.Count > 0)
        //            SubDetailGridExControl.MainGrid.Columns.Clear();

        //        SubDetailGridBindingSource.DataSource = ds.Tables[0];
        //        SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
                
        //        foreach (var v in SubDetailGridExControl.MainGrid.Columns.ToList())
        //        {
        //            v.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
        //            if (v.FieldName == "ITEM_CODE")
        //            {
        //                v.Caption = LabelConvert.GetLabelText("ItemCode");
        //            }
        //            else if (v.FieldName == "ITEM_NAME" || v.FieldName == "ITEM_NAME_ENG" || v.FieldName == "ITEM_NAME_CHN")
        //            {
        //                v.Caption = LabelConvert.GetLabelText("ItemName");
        //                if (DataConvert.GetCultureIndex() == 1)
        //                {
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME"].OptionsColumn.AllowShowHide = true;
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_ENG"].OptionsColumn.AllowShowHide = false;
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_CHN"].OptionsColumn.AllowShowHide = false;

        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME"].Visible = true;
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_ENG"].Visible = false;
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_CHN"].Visible = false;
        //                }
        //                else if (DataConvert.GetCultureIndex() == 2)
        //                {
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME"].OptionsColumn.AllowShowHide = false;
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_ENG"].OptionsColumn.AllowShowHide = true;
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_CHN"].OptionsColumn.AllowShowHide = false;

        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME"].Visible = false;
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_ENG"].Visible = true;
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_CHN"].Visible = false;
        //                }
        //                else
        //                {
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME"].OptionsColumn.AllowShowHide = false;
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_ENG"].OptionsColumn.AllowShowHide = false;
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_CHN"].OptionsColumn.AllowShowHide = true;

        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME"].Visible = false;
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_ENG"].Visible = false;
        //                    SubDetailGridExControl.MainGrid.Columns["ITEM_NAME_CHN"].Visible = true;
        //                }
        //            }
        //            else if (v.FieldName == "MACHINE_CODE")
        //            {
        //                v.Caption = LabelConvert.GetLabelText("MachineCode");
        //                v.Visible = false;
        //            }
        //            else if (v.FieldName == "MACHINE_NAME" || v.FieldName == "MACHINE_NAME_ENG" || v.FieldName == "MACHINE_NAME_CHN")
        //            {
        //                v.Caption = LabelConvert.GetLabelText("MachineName");
        //                if (DataConvert.GetCultureIndex() == 1)
        //                {
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME"].OptionsColumn.AllowShowHide = true;
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_ENG"].OptionsColumn.AllowShowHide = false;
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_CHN"].OptionsColumn.AllowShowHide = false;

        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME"].Visible = true;
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_ENG"].Visible = false;
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_CHN"].Visible = false;
        //                }
        //                else if (DataConvert.GetCultureIndex() == 2)
        //                {
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME"].OptionsColumn.AllowShowHide = false;
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_ENG"].OptionsColumn.AllowShowHide = true;
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_CHN"].OptionsColumn.AllowShowHide = false;

        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME"].Visible = false;
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_ENG"].Visible = true;
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_CHN"].Visible = false;
        //                }
        //                else
        //                {
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME"].OptionsColumn.AllowShowHide = false;
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_ENG"].OptionsColumn.AllowShowHide = false;
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_CHN"].OptionsColumn.AllowShowHide = true;

        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME"].Visible = false;
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_ENG"].Visible = false;
        //                    SubDetailGridExControl.MainGrid.Columns["MACHINE_NAME_CHN"].Visible = true;
        //                }
        //            }
        //            else if (v.FieldName == "PROCESS_CODE")
        //            {
        //                v.Caption = LabelConvert.GetLabelText("ProcessCode");
        //            }
        //            else if (v.FieldName == "PROCESS_NAME" || v.FieldName == "PROCESS_NAME_ENG" || v.FieldName == "PROCESS_NAME_CHN")
        //            {
        //                v.Caption = LabelConvert.GetLabelText("ProcessName");
        //                if (DataConvert.GetCultureIndex() == 1)
        //                {
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME"].OptionsColumn.AllowShowHide = true;
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_ENG"].OptionsColumn.AllowShowHide = false;
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_CHN"].OptionsColumn.AllowShowHide = false;

        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME"].Visible = true;
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_ENG"].Visible = false;
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_CHN"].Visible = false;
        //                }
        //                else if (DataConvert.GetCultureIndex() == 2)
        //                {
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME"].OptionsColumn.AllowShowHide = false;
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_ENG"].OptionsColumn.AllowShowHide = true;
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_CHN"].OptionsColumn.AllowShowHide = false;

        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME"].Visible = false;
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_ENG"].Visible = true;
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_CHN"].Visible = false;
        //                }
        //                else
        //                {
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME"].OptionsColumn.AllowShowHide = false;
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_ENG"].OptionsColumn.AllowShowHide = false;
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_CHN"].OptionsColumn.AllowShowHide = true;

        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME"].Visible = false;
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_ENG"].Visible = false;
        //                    SubDetailGridExControl.MainGrid.Columns["PROCESS_NAME_CHN"].Visible = true;
        //                }
        //            }
        //            else
        //            {
        //                SubDetailGridExControl.MainGrid.Columns[v.FieldName].DisplayFormat.FormatType = FormatType.Numeric;
        //                SubDetailGridExControl.MainGrid.Columns[v.FieldName].DisplayFormat.FormatString = "n0";
        //                SubDetailGridExControl.MainGrid.Columns[v.FieldName].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
        //            }
        //        }
        //        SubDetailGridExControl.MainGrid.BestFitColumns();
        //    }
        //}
        #endregion

        protected override void DetailAddRowClicked()
        {

            TN_MEA1300 MasterObj = MasterGridBindingSource.Current as TN_MEA1300;
            if (MasterObj == null) return;

            List<TN_MEA1301> DetailList = DetailGridBindingSource.DataSource as List<TN_MEA1301>;
            if (DetailList == null) return;

            TN_MEA1301 NewObj = new TN_MEA1301();

            NewObj.GradeManageNo = MasterObj.GradeManageNo;
            NewObj.Seq = MasterObj.TN_MEA1301List.Count == 0 ? 1 : MasterObj.TN_MEA1301List.Max(x => x.Seq) + 1;
            NewObj.NewRowFlag = "Y";

            ModelService.InsertChild(NewObj);
            DetailGridBindingSource.Add(NewObj);
        }


        protected override void DeleteDetailRow()
        {
            TN_MEA1300 MasterObj = MasterGridBindingSource.Current as TN_MEA1300;
            if (MasterObj == null) return;

            TN_MEA1301 DetailObj = DetailGridBindingSource.Current as TN_MEA1301;
            if (DetailObj == null) return;

            ModelService.RemoveChild<TN_MEA1301>(DetailObj);
            MasterObj.TN_MEA1301List.Remove(DetailObj);
            DetailGridBindingSource.RemoveCurrent();
        }

        // SUB 추가
        protected override void SubDetailAddRowClicked()
        {

        }

        // SUB 삭제
        protected override void DeleteSubDetailRow()
        {

        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();



            ModelService.Save();
            DataLoad();
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Memo" && view.FocusedColumn.FieldName != "Temp1")
            {
                if (view.GetFocusedRowCellValue("JobStates").ToString() != MasterCodeSTR.JobStates_Wait)
                    e.Cancel = true;
            }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //GridView View = sender as GridView;

            //if (e.Column.FieldName == "EmergencyFlag")
            //{
            //    var obj = DetailGridBindingSource.Current as TN_MPS1200;
            //    if (obj != null)
            //    {
            //        var checkValue = e.Value.GetNullToEmpty();
            //        if (checkValue == "Y")
            //        {
            //            var detailList = DetailGridBindingSource.List as List<TN_MPS1200>;
            //            var sameWorkNoList = detailList.Where(p => p.WorkNo == obj.WorkNo).ToList();
            //            if (sameWorkNoList.Count > 1)
            //            {
            //                foreach (var v in sameWorkNoList)
            //                    v.EmergencyFlag = "Y";
            //                DetailGridExControl.BestFitColumns();
            //            }
            //        }
            //    }
            //}
            //else if (e.Column.FieldName == "MachineGroupCode")
            //{
            //    var detailObj = DetailGridBindingSource.Current as TN_MPS1200;
            //    if (detailObj == null) return;

            //    if (e.Value.IsNullOrEmpty())
            //        detailObj.MachineFlag = "N";
            //    else
            //        detailObj.MachineFlag = "Y";
            //}
            //else if (e.Column.FieldName == "Temp1")
            //{
            //    var detailObj = DetailGridBindingSource.Current as TN_MPS1200;
            //    if (detailObj == null) return;

            //    var detailList = DetailGridBindingSource.List as List<TN_MPS1200>;
            //    var sameWorkNoList = detailList.Where(p => p.WorkNo == detailObj.WorkNo).ToList();
            //    if (sameWorkNoList.Count > 1)
            //    {
            //        foreach (var v in sameWorkNoList)
            //            v.Temp1 = e.Value.GetNullToNull();
            //        DetailGridExControl.BestFitColumns();
            //    }
            //}
        }

        //private void MainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        //{
        //    GridView View = sender as GridView;
        //    if (e.RowHandle >= 0)
        //    {
        //        if (e.Column.FieldName == "MachineCode")
        //        {
        //            object machineFlag = View.GetRowCellValue(e.RowHandle, View.Columns["MachineFlag"]);
        //            object machineValue = View.GetRowCellValue(e.RowHandle, View.Columns["MachineCode"]);
        //            if (machineFlag.GetNullToEmpty() == "Y" && machineValue.IsNullOrEmpty())
        //            {
        //                e.Appearance.BackColor = Color.Red;
        //                e.Appearance.ForeColor = Color.White;
        //            }
        //        }
        //    }
        //}

        //private void MachineEdit_Popup(object sender, EventArgs e)
        //{
        //    var detailObj = DetailGridBindingSource.Current as TN_MPS1200;
        //    var lookup = sender as SearchLookUpEdit;
        //    if (lookup == null) return;
        //    if (detailObj == null) return;

        //    if (detailObj.MachineGroupCode.IsNullOrEmpty())
        //        lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
        //    else
        //        lookup.Properties.View.ActiveFilter.NonColumnFilter = "[MachineGroupCode] = '" + detailObj.MachineGroupCode + "'";
        //}

        /// <summary>
        /// 휴일체크 재귀함수
        /// </summary>
        //private DateTime CheckHolidayDate(DateTime date, int changeQty)
        //{
        //    var addDate = date.AddDays(changeQty);

        //    if (changeQty > 0)
        //    {
        //        if (holidayList.Any(p => (p.HolidayFlag == "Y" && p.OvertimeFlag == "N") && p.Date == addDate))
        //            return CheckHolidayDate(addDate, 1);
        //        else
        //            return addDate;
        //    }
        //    else if (changeQty < 0)
        //    {
        //        if (holidayList.Any(p => (p.HolidayFlag == "Y" && p.OvertimeFlag == "N") && p.Date == addDate))
        //            return CheckHolidayDate(addDate, -1);
        //        else
        //            return addDate;
        //    }
        //    else
        //    {
        //        return addDate;
        //    }
        //}
    }
}