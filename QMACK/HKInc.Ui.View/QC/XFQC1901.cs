using System;
using System.Data;
using System.Linq;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using DevExpress.Utils;
using HKInc.Utils.Class;
using System.Data.SqlClient;
using HKInc.Service.Controls;
using HKInc.Service.Handler;
using System.Windows.Forms;

namespace HKInc.Ui.View.QC
{
    /// <summary>
    /// 20220518 오세완 차장
    /// lot추척관리 고도화 후
    /// </summary>
    public partial class XFQC1901 : HKInc.Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<VI_LOTTRACKING> ModelService = (IService<VI_LOTTRACKING>)ProductionFactory.GetDomainService("VI_LOTTRACKING");

        /// <summary>
        /// 20220523 오세완 차장 
        /// 고도화 적용 기준일
        /// </summary>
        private DateTime gdt_Check;

        /// <summary>
        /// 20220524 오세완 차장
        /// 메시지창 중복 출력 방지
        /// 미사용 주석처리   2022-06-29 김진우
        /// </summary>
        //private bool gb_ClickMessageBox = false;
        #endregion

        public XFQC1901()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            gdt_Check = new DateTime(2022, 04, 01);
            DateTime dt_from = DateTime.Today.AddDays(-7);
            if (gdt_Check < dt_from)
                dp_dt.DateFrEdit.DateTime = dt_from;
            else
                dp_dt.DateFrEdit.DateTime = gdt_Check;

            // 20220524 오세완 차장 사용자가 시스템 달력을 건드려서 22년 4월 1일 이전 검삭하는거 방지하기 위해 생략
            //dp_dt.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dp_dt.DateToEdit.DateTime = DateTime.Today;

            dp_dt.DateFrEdit.DateTimeChanged += DateFrEdit_ChangeValue;             // 2022-06-29 김진우 04-01이후 데이터 조회를 위해 추가
            dp_dt.DateToEdit.DateTimeChanged += DateToEdit_ChangeValue;             // 2022-06-29 김진우 04-01이후 데이터 조회를 위해 추가 

            //dp_dt.OnDateValueChanged_Both += Dp_dt_OnDateValueChanged_Both;       // 2022-06-29 김진우 오류로 인한 주석
        }

        protected override void InitCombo()
        {
            // 20220219 오세완 차장 품목 / 품명이 동일하게 나오는 오류가 있어서 생략                 2022-02-23 김진우 ItemCodeColumnSetting 수정하여 원복            
            lupItem.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && 
                                                                                                       (p.TopCategory == MasterCodeSTR.Topcategory_Final_Product || p.TopCategory == MasterCodeSTR.Topcategory_Half_Product)).OrderBy(o => o.ItemNm1).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("WORK_NO","작업지시번호");
            GridExControl.MainGrid.AddColumn("ITEM_CODE","품목코드");
            GridExControl.MainGrid.AddColumn("ITEM_NM", "품명");
            GridExControl.MainGrid.AddColumn("ITEM_NM1", "품번");
            GridExControl.MainGrid.AddColumn("PROCESS_NAME","공정");
            GridExControl.MainGrid.AddColumn("PROCESS_TURN","공정순서");
            GridExControl.MainGrid.AddColumn("LOT_NO","LOTNO");
            GridExControl.MainGrid.AddColumn("START_DATE", "작업시작일시", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("END_DATE", "작업종료일시", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("RESULT_DATE","마지막 작업일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("RESULT_QTY","생산수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("FAIL_QTY","불량수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("OK_QTY","양품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("WORKER_NAME","작업자");            
            GridExControl.MainGrid.AddColumn("MACHINE_NAME","설비명");
            GridExControl.MainGrid.AddColumn("SRC_CODE","원소재 품목코드");
            GridExControl.MainGrid.AddColumn("SRC_NM","원소재 품명");
            GridExControl.MainGrid.AddColumn("SRC_NM1", "원소재 품번");
            GridExControl.MainGrid.AddColumn("SRC_IN_LOT_NO", "원소재LOT");
            GridExControl.BestFitColumns();
        }
        
        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            //ModelService.ReLoad();
            InitCombo();

            string item = lupItem.EditValue.GetNullToEmpty();
            string lot = tx_LotNo.EditValue.GetNullToEmpty();
            string sDateFrom = dp_dt.DateFrEdit.DateTime.ToShortDateString();
            string sDateTo = dp_dt.DateToEdit.DateTime.ToShortDateString();
            string sWorkno = tx_Workno.EditValue.GetNullToEmpty();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", item);
                SqlParameter sp_Lotno = new SqlParameter("@LOT_NO", lot);
                SqlParameter sp_Workno = new SqlParameter("@WORK_NO", sWorkno);
                SqlParameter sp_Result_Datefrom = new SqlParameter("@RESULT_DATE_FROM", sDateFrom);
                SqlParameter sp_Result_Dateto = new SqlParameter("@RESULT_DATE_TO", sDateTo);

                // 2022-06-30 김진우   조회조건 수정
                GridBindingSource.DataSource = 
                    context.Database.SqlQuery<TP_XFQCT1900_LIST>("USP_GET_XFQCT1900_LIST @ITEM_CODE, @LOT_NO, @WORK_NO, @RESULT_DATE_FROM, @RESULT_DATE_TO",
                    sp_Itemcode, sp_Lotno, sp_Workno, sp_Result_Datefrom, sp_Result_Dateto).OrderBy(o => o.WORK_NO).ThenBy(t => t.PROCESS_TURN).ThenBy(t1 => t1.LOT_NO).ToList();
                #region 이전소스
                //var vResult = context.Database.SqlQuery<TP_XFQCT1900_LIST>("USP_GET_XFQCT1900_LIST @ITEM_CODE, @LOT_NO, @WORK_NO, @RESULT_DATE_FROM, @RESULT_DATE_TO", 
                //    sp_Itemcode, sp_Lotno, sp_Workno, sp_Result_Datefrom, sp_Result_Dateto).ToList();

                //if (vResult == null)
                //    GridBindingSource.Clear();
                //else if (vResult.Count == 0)
                //    GridBindingSource.Clear();
                //else
                //{
                //    vResult = vResult.OrderBy(o => o.WORK_NO).ThenBy(t => t.PROCESS_TURN).ThenBy(t1 => t1.LOT_NO).ToList();
                //    GridBindingSource.DataSource = vResult;
                //}
                #endregion
            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        /// <summary>
        /// 날짜조회 4월 1일 이후로만 조회되도록 추가
        /// 2022-06-29 김진우 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateFrEdit_ChangeValue(object sender, EventArgs e)
        {
            DateEditEx Sender = sender as DateEditEx;
            if (sender == null) return;
            DateTime FrDate = Sender.DateTime;

            if (FrDate < gdt_Check)
            {
                DialogResult dr = MessageBoxHandler.Show("고도화 적용 이전 날짜입니다. 다른 날짜를 선택해 주세요. 기준일 :" + gdt_Check.ToShortDateString(), "검색 종료일", MessageBoxButtons.OK);
                dp_dt.DateFrEdit.EditValue = gdt_Check;
            }
        }

        /// <summary>
        /// 날짜조회 4월 1일 이후로만 조회되도록 추가
        /// 2022-06-29 김진우 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DateToEdit_ChangeValue(object sender, EventArgs e)
        {
            DateEditEx Sender = sender as DateEditEx;
            if (sender == null) return;
            DateTime ToDate = Sender.DateTime;

            if (ToDate < gdt_Check)
            {
                DialogResult dr = MessageBoxHandler.Show("고도화 적용 이전 날짜입니다. 다른 날짜를 선택해 주세요. 기준일 :" + gdt_Check.ToShortDateString(), "검색 종료일", MessageBoxButtons.OK);
                if (dp_dt.DateFrEdit.DateTime < gdt_Check)
                    dp_dt.DateFrEdit.EditValue = gdt_Check;
                dp_dt.DateToEdit.EditValue = gdt_Check;
            }
        }

        #region 이전소스
        /// <summary>
        /// 20220523 오세완 차장 
        /// 고도화 이전 날짜 조회시 메시지 출력 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Dp_dt_OnDateValueChanged_Both(object sender, EventArgs e)
        //{
        //    DatePeriodEditEx dpe = sender as DatePeriodEditEx;
        //    if (dpe == null)
        //        return;

        //    string sMessage = sMessage = "고도화 적용 이후 날짜입니다. 다른 날짜를 선택해 주세요. 기준일 :" + gdt_Check.ToShortDateString();
        //    DateTime dt_To = dpe.DateToEdit.DateTime;
        //    if (dt_To > gdt_Check)
        //    {
        //        if (!gb_ClickMessageBox)
        //        {
        //            DialogResult dr = MessageBoxHandler.Show(sMessage, "검색 종료일", MessageBoxButtons.OK);
        //            if (dr == DialogResult.OK)
        //                gb_ClickMessageBox = true;
        //        }
        //        // 20220524 오세완 차장 값을 변경하면 무한루프에 빠지는 오류가 생겨서 경고창만 출력
        //        //dpe.DateToEdit.DateTime = gdt_Check;
        //    }

        //    DateTime dt_From = dpe.DateFrEdit.DateTime;
        //    if (dt_From > gdt_Check)
        //    {
        //        if (!gb_ClickMessageBox)
        //        {
        //            DialogResult dr = MessageBoxHandler.Show(sMessage, "검색 시작일", MessageBoxButtons.OK);
        //            if (dr == DialogResult.OK)
        //                gb_ClickMessageBox = true;
        //        }

        //        // 20220524 오세완 차장 값을 변경하면 무한루프에 빠지는 오류가 생겨서 경고창만 출력
        //        //if(dt_To > gdt_Check)
        //        //    dpe.DateToEdit.DateTime = gdt_Check;

        //        //dpe.DateFrEdit.DateTime = dpe.DateToEdit.DateTime.AddDays(-1);
        //    }
        //}
        #endregion
    }
}