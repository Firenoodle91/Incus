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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using DevExpress.XtraEditors.Mask;
using DevExpress.Utils;
using HKInc.Service.Helper;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 작업지시일 일괄변경
    /// </summary>
    public partial class XFMPS1201 : Service.Base.ListFormTemplate
    {
        IService<TN_MPS1200> ModelService = (IService<TN_MPS1200>)ProductionFactory.GetDomainService("TN_MPS1200");
        List<Holiday> holidayList;

        public XFMPS1201()
        {
            InitializeComponent();

            GridExControl = gridEx1;

            dt_WorkDate.SetTodayIsWeek();
        }

        protected override void InitCombo()
        {
            spin_ChangeDay.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ChangeDay.Properties.Mask.EditMask = "n0";
            spin_ChangeDay.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ChangeDay.Properties.Buttons[0].Visible = false;

            emptySpaceItem1.Text = LabelConvert.GetLabelText("CheckItemBatchChange");
            emptySpaceItem1.AppearanceItemCaption.ForeColor = Color.Blue;

        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("BatchChange") + "[F3]", IconImageList.GetIconImage("navigation/next"));
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, false);
            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "RowId", UserRight.HasEdit);
            GridExControl.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            GridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemName1", LabelConvert.GetLabelText("ItemName1"), false); // 20210824 오세완 차장 품목명 누락 추가
            GridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            GridExControl.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("ChangeWorkDate", LabelConvert.GetLabelText("ChangeWorkDate"));
            GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"));
            GridExControl.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"));
            GridExControl.MainGrid.AddColumn("OutProcFlag", LabelConvert.GetLabelText("OutProcFlag"));
            GridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "ChangeWorkDate");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("WorkQty", DefaultBoolean.Default, "n2");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProcFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ChangeWorkDate", true);
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");

            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            if (!IsFirstLoaded)
            {
                holidayList = ModelService.GetChildList<Holiday>(p => true).ToList();
            }

            GridRowLocator.GetCurrentRow();

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();
            
            InitRepository();
            InitCombo();

            string workNo = tx_WorkNo.EditValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => p.JobStates == MasterCodeSTR.JobStates_Wait
                                                                  && (p.WorkDate >= dt_WorkDate.DateFrEdit.DateTime
                                                                      && p.WorkDate <= dt_WorkDate.DateToEdit.DateTime)
                                                                  && (string.IsNullOrEmpty(workNo) ? true : p.WorkNo == workNo)
                                                                )
                                                                .OrderBy(p => p.WorkNo)
                                                                .ThenBy(p => p.ProcessSeq)
                                                                .ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
        }

        /// <summary>
        /// 일괄변경이벤트
        /// </summary>
        protected override void AddRowClicked()
        {
            var list = GridBindingSource.List as List<TN_MPS1200>;
            var checkList = list.Where(p => p._Check == "Y").ToList();

            var changeDay = spin_ChangeDay.EditValue.GetIntNullToZero();
            
            foreach (var v in checkList)
            {
                v.ChangeWorkDate = CheckHolidayDate(v.WorkDate, changeDay);
            }

            GridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();

            var list = GridBindingSource.List as List<TN_MPS1200>;
            var saveList = list.Where(p => p.ChangeWorkDate != null).ToList();

            var changeDay = spin_ChangeDay.EditValue.GetIntNullToZero();

            foreach (var v in saveList)
            {
                v.WorkDate = (DateTime)v.ChangeWorkDate;
            }

            ModelService.Save();
            DataLoad();
        }

        /// <summary>
        /// 휴일체크 재귀함수
        /// </summary>
        private DateTime CheckHolidayDate(DateTime date, int changeQty)
        {
            var addDate = date.AddDays(changeQty);

            if (changeQty > 0)
            {
                if (holidayList.Any(p => (p.HolidayFlag == "Y" && p.OvertimeFlag == "N") && p.Date == addDate))
                    return CheckHolidayDate(addDate, 1);
                else
                    return addDate;
            }
            else if (changeQty < 0)
            {
                if (holidayList.Any(p => (p.HolidayFlag == "Y" && p.OvertimeFlag == "N") && p.Date == addDate))
                    return CheckHolidayDate(addDate, -1);
                else
                    return addDate;
            }
            else
            {
                return addDate;
            }
        }
    }
}