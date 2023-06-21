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
using DevExpress.XtraEditors.Mask;
using DevExpress.Utils;
using HKInc.Service.Helper;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using System.Data.SqlClient;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 작업지시일 일괄변경
    /// </summary>
    public partial class XFMPS1201 : Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1400> ModelService = (IService<TN_MPS1400>)ProductionFactory.GetDomainService("TN_MPS1400");
        List<Holiday> holidayList;
        #endregion

        public XFMPS1201()
        {
            InitializeComponent();

            GridExControl = gridEx1;
            //GridExControl.MainGrid.MainView.MouseUp += MainView_MouseUp;

            dt_WorkDate.SetTodayIsWeek();
        }

        protected override void InitCombo()
        {
            spin_ChangeDay.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ChangeDay.Properties.Mask.EditMask = "n0";
            spin_ChangeDay.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ChangeDay.Properties.Buttons[0].Visible = false;

            emptySpaceItem1.Text = "작업지시일 일괄변경";
            emptySpaceItem1.AppearanceItemCaption.ForeColor = Color.Blue;

        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("일괄변경") + "[F3]");
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "ROW_ID", UserRight.HasEdit);

            GridExControl.MainGrid.AddColumn("ROW_ID", false);
            GridExControl.MainGrid.AddColumn("_Check", "선택");
            GridExControl.MainGrid.AddColumn("WORK_NO", "작업지시번호");
            GridExControl.MainGrid.AddColumn("ITEM_CODE", "품목코드");
            GridExControl.MainGrid.AddColumn("ITEM_NM", "품번");

            GridExControl.MainGrid.AddColumn("ITEM_NM1", "품명");
            GridExControl.MainGrid.AddColumn("P_SEQ", "공정순번", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("PROCESS_NAME", "공정명");
            GridExControl.MainGrid.AddColumn("WORK_DATE", "작업지시일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("WORK_DATE_CHANGE", "변경할 작업지시일");

            GridExControl.MainGrid.AddColumn("MACHINE_NAME", "설비명");
            GridExControl.MainGrid.AddColumn("PLAN_QTY", "지시수량");
            GridExControl.MainGrid.AddColumn("OutProc", "외주여부");
            GridExControl.MainGrid.AddColumn("UserName", "작업자");
            GridExControl.MainGrid.AddColumn("MEMO", "메모");
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "WORK_DATE_CHANGE");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("WORK_DATE_CHANGE");
            GridExControl.MainGrid.MainView.Columns["MEMO"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "MEMO");
            //GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            if (!IsFirstLoaded)
            {
                holidayList = ModelService.GetChildList<Holiday>(p => true).ToList();
            }

            GridRowLocator.GetCurrentRow();

            GridExControl.MainGrid.Clear();

            //ModelService.ReLoad();

            InitRepository();
            InitCombo();

            string sWorkno = tx_WorkNo.EditValue.GetNullToEmpty();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var sp_Jobstate = new SqlParameter("@JOB_STATE", Convert.ToInt32(MasterCodeEnum.POP_Status_Wait));
                var sp_Datefrom = new SqlParameter("@DATE_FROM", dt_WorkDate.DateFrEdit.DateTime);
                var sp_Dateto   = new SqlParameter("@DATE_TO", dt_WorkDate.DateToEdit.DateTime);
                //var sp_Datefrom = new SqlParameter("@DATE_FROM", dt_WorkDate.DateFrEdit.EditValue);       // 날짜 변경하면서 조회 불가여서 수정      2022-07-05 김진우
                //var sp_Dateto = new SqlParameter("@DATE_TO", dt_WorkDate.DateToEdit.EditValue);           // 날짜 변경하면서 조회 불가여서 수정      2022-07-05 김진우
                var sp_Workno   = new SqlParameter("@WORK_NO", sWorkno);

                GridBindingSource.DataSource =
                    context.Database.SqlQuery<TP_XFMPS1201_LIST>("USP_GET_XFMPS1201_LIST @JOB_STATE, @DATE_FROM, @DATE_TO, @WORK_NO",
                    sp_Jobstate, sp_Datefrom, sp_Dateto, sp_Workno).OrderBy(o => o.WORK_DATE).ThenBy(t => t.WORK_NO).ThenBy(t1 => t1.P_SEQ).ToList();

                #region 이전 소스
                //var vResult = context.Database.SqlQuery<TP_XFMPS1201_LIST>("USP_GET_XFMPS1201_LIST @JOB_STATE, @DATE_FROM, @DATE_TO, @WORK_NO", 
                //    sp_Jobstate, sp_Datefrom, sp_Dateto, sp_Workno).OrderBy(o => o.WORK_DATE).ThenBy(t => t.WORK_NO).ThenBy(t1 => t1.P_SEQ).ToList();
                //GridBindingSource.DataSource = vResult;
                #endregion
            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();     // 2022-07-05 김진우 추가
        }

        /// <summary>
        /// 일괄변경이벤트
        /// </summary>
        protected override void AddRowClicked()
        {
            List<TP_XFMPS1201_LIST> work_Arr = GridBindingSource.List as List<TP_XFMPS1201_LIST>;
            List<TP_XFMPS1201_LIST> work_select_Arr = work_Arr.Where(p => p._Check == "Y").ToList();

            int iChangeDay = spin_ChangeDay.EditValue.GetIntNullToZero();

            foreach (TP_XFMPS1201_LIST each in work_select_Arr)
            {
                each.WORK_DATE_CHANGE = CheckHolidayDate(each.WORK_DATE, iChangeDay);
            }

            GridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();

            List<TP_XFMPS1201_LIST> work_Arr = GridBindingSource.List as List<TP_XFMPS1201_LIST>;
            List<TP_XFMPS1201_LIST> work_select_Arr = work_Arr.Where(p => p._Check == "Y").ToList();

            if(work_select_Arr != null)
                if(work_select_Arr.Count > 0)
                {
                    foreach (TP_XFMPS1201_LIST each in work_select_Arr)
                    {
                        string sSql = "exec USP_UPD_XFMPS1201_DATE " + each.ROW_ID.ToString() + ", '" + Convert.ToDateTime(each.WORK_DATE_CHANGE).ToShortDateString() + 
                            "','" +  Utils.Common.GlobalVariable.LoginId + "' ";
                        int iResult = DbRequestHandler.SetDataQury(sSql);
                    }
                }

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

        #region 안쓰는 기능 제거
        //private void MainView_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 1)
        //    {
        //        //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = view.CalcHitInfo(e.Location);
        //        //if (hInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.Column && hInfo.Column == view.Columns["_Check"])
        //        {

        //        }
        //    }
        //}
        #endregion
    }
}