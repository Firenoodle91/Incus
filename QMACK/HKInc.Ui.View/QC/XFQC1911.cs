using System;
using System.Collections.Generic;
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
    /// 20220530 오세완 차장 
    /// lot역추적 고도화 후
    /// </summary>
    public partial class XFQC1911 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        /// <summary>
        /// 20220530 오세완 차장 
        /// 고도화 적용 기준일
        /// </summary>
        private DateTime gdt_Check;

        /// <summary>
        /// 20220530 오세완 차장
        /// 메시지창 중복 출력 방지
        /// 2022-07-07 김진우 미사용으로 인한 주석
        /// </summary>
        //private bool gb_ClickMessageBox = false;
        #endregion

        public XFQC1911()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            // 20220524 오세완 차장 사용자가 시스템 달력을 건드려서 22년 4월 1일 이전 검삭하는거 방지하기 위해 설정
            gdt_Check = new DateTime(2022, 04, 01);
            DateTime dt_from = DateTime.Today.AddDays(-7);
            if (gdt_Check < dt_from)
                dpe_Outdate.DateFrEdit.DateTime = dt_from;
            else
                dpe_Outdate.DateFrEdit.DateTime = gdt_Check;

            dpe_Outdate.DateToEdit.EditValue = DateTime.Now;

            dpe_Outdate.DateFrEdit.DateTimeChanged += DateFrEdit_ChangeValue;             // 2022-06-29 김진우 04-01이후 데이터 조회를 위해 추가
            dpe_Outdate.DateToEdit.DateTimeChanged += DateToEdit_ChangeValue;             // 2022-06-29 김진우 04-01이후 데이터 조회를 위해 추가

            //dpe_Outdate.OnDateValueChanged_Both += Dpe_Outdate_OnDateValueChanged_Both;       // 2022-07-01 김진우 오류로 인한 주석
        }

        protected override void InitCombo()
        {
            //List<TN_STD1100> item_Arr = ModelService.GetList(p => p.TopCategory == MasterCodeSTR.Topcategory_Final_Product && p.UseYn == "Y");        // 2022-06-30 김진우   기존방식으로 변경
            slup_Itemcode.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetList(p => p.TopCategory == MasterCodeSTR.Topcategory_Final_Product && p.UseYn == "Y"));

            //List<TN_STD1400> cust_Arr = ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y");       // 2022-06-30 김진우   기존방식으로 변경
            slup_Custcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y"));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ITEM_CODE", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ITEM_NM", "품명");
            MasterGridExControl.MainGrid.AddColumn("ITEM_NM1", "품번");
            MasterGridExControl.MainGrid.AddColumn("CUSTOMER_NAME", "거래처명");
            MasterGridExControl.MainGrid.AddColumn("OUT_NO", "출고번호");
            MasterGridExControl.MainGrid.AddColumn("OUT_DATE", "출고일", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");

            DetailGridExControl.SetToolbarVisible(false);
            //DetailGridExControl.SetToolbarButtonVisible(false);     // 2022-06-30 김진우 주석
            DetailGridExControl.MainGrid.AddColumn("WORK_NO", "작업지시번호");
            DetailGridExControl.MainGrid.AddColumn("ITEM_CODE", "품목코드");
            DetailGridExControl.MainGrid.AddColumn("ITEM_NM", "품명");
            DetailGridExControl.MainGrid.AddColumn("ITEM_NM1", "품번");
            DetailGridExControl.MainGrid.AddColumn("PROCESS_NAME", "공정");
            DetailGridExControl.MainGrid.AddColumn("PROCESS_TURN", "공정순서");
            DetailGridExControl.MainGrid.AddColumn("LOT_NO", "LOTNO");
            DetailGridExControl.MainGrid.AddColumn("START_DATE", "작업시작일시", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("END_DATE", "작업종료일시", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("RESULT_DATE", "마지막 작업일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("RESULT_QTY", "생산수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("FAIL_QTY", "불량수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OK_QTY", "양품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("WORKER_NAME", "작업자");
            DetailGridExControl.MainGrid.AddColumn("MACHINE_NAME", "설비명");
            DetailGridExControl.MainGrid.AddColumn("SRC_CODE", "원소재 품목코드");
            DetailGridExControl.MainGrid.AddColumn("SRC_NM", "원소재 품명");
            DetailGridExControl.MainGrid.AddColumn("SRC_NM1", "원소재 품번");
            DetailGridExControl.MainGrid.AddColumn("SRC_IN_LOT_NO", "원소재LOT");
        }
       
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("OUT_NO");
            #endregion

            MasterGridExControl.MainGrid.Clear();       // 2022-06-30 김진우 추가
            DetailGridExControl.MainGrid.Clear();       // 2022-06-30 김진우 추가

            ModelService.ReLoad();
            InitCombo();

            string sOutno = tx_Outno.EditValue.GetNullToEmpty();
            string sItemcode = slup_Itemcode.EditValue.GetNullToEmpty();
            string sCustcode = slup_Custcode.EditValue.GetNullToEmpty();
            string sDatefrom = dpe_Outdate.DateFrEdit.DateTime.ToShortDateString();         // 2022-06-30 김진우 추가
            string sDateto = dpe_Outdate.DateToEdit.DateTime.ToShortDateString();           // 2022-06-30 김진우 추가
            //string sDatefrom = Convert.ToDateTime(dpe_Outdate.DateFrEdit.EditValue).ToShortDateString();      // 2022-06-30 김진우       날짜 변경 후 변경된 날짜가 적용이 안됨
            //string sDateto = Convert.ToDateTime(dpe_Outdate.DateToEdit.EditValue).ToShortDateString();        // 2022-06-30 김진우       날짜 변경 후 변경된 날짜가 적용이 안됨

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Datefrom = new SqlParameter("@DATE_FROM", sDatefrom);
                SqlParameter sp_Dateto = new SqlParameter("@DATE_TO", sDateto);
                SqlParameter sp_Outno = new SqlParameter("@OUT_NO", sOutno);
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", sItemcode);
                SqlParameter sp_Custcode = new SqlParameter("@CUST_CODE", sCustcode);
                SqlParameter sp_Check = new SqlParameter("@CHECK", "B");            // 2022-07-01 김진우 추가

                // 2022-06-30 김진우 조회조건 수정
                MasterGridBindingSource.DataSource = 
                    context.Database.SqlQuery<TP_XFQCT1910_LIST>("USP_GET_XFQCT1910_LIST @DATE_FROM, @DATE_TO, @OUT_NO, @ITEM_CODE, @CUST_CODE, @CHECK",    // @CHECK 추가            2022-07-01 김진우
                    sp_Datefrom, sp_Dateto, sp_Outno, sp_Itemcode, sp_Custcode, sp_Check).OrderBy(o => o.OUT_DATE).ThenBy(t => t.ITEM_CODE).ToList();       // sp_Check 추가          2022-07-01 김진우
                #region 이전소스
                //var vResult = context.Database.SqlQuery<TP_XFQCT1910_LIST>("USP_GET_XFQCT1910_LIST @DATE_FROM, @DATE_TO, @OUT_NO, @ITEM_CODE, @CUST_CODE", 
                //    sp_Datefrom, sp_Dateto, sp_Outno, sp_Itemcode, sp_Custcode).ToList();
                //if (vResult == null)
                //{
                //    MasterGridBindingSource.Clear();
                //    DetailGridBindingSource.Clear();
                //}
                //else if (vResult.Count == 0)
                //{
                //    MasterGridBindingSource.Clear();
                //    DetailGridBindingSource.Clear();
                //}
                //else
                //{
                //    vResult = vResult.OrderBy(o => o.OUT_DATE).ThenBy(t => t.ITEM_CODE).ToList();
                //    MasterGridBindingSource.DataSource = vResult;
                //}
                #endregion
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            //SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);            // 2022-06-30 김진우   디테일과 중복으로 표시되어서 둘다 제거
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            TP_XFQCT1910_LIST masterObj = MasterGridBindingSource.Current as TP_XFQCT1910_LIST;
            if (masterObj == null) return;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Limit = new SqlParameter("@LIMIT_DATE", dpe_Outdate.DateFrEdit.DateTime.Date.ToShortDateString());
                SqlParameter sp_Lotno = new SqlParameter("@LOT_NO", masterObj.OUT_NO);

                // 2022-06-30 김진우 조회조건 수정
                DetailGridBindingSource.DataSource = 
                    context.Database.SqlQuery<TP_XFQCT1911_DETAIL_LIST>("USP_GET_XFQCT1911_DETAIL @LIMIT_DATE, @LOT_NO", 
                    sp_Limit, sp_Lotno).OrderBy(o => o.PROCESS_TURN).ThenBy(t => t.START_DATE).ToList();

                #region 이전소스
                //var vResult = context.Database.SqlQuery<TP_XFQCT1911_DETAIL_LIST>("USP_GET_XFQCT1911_DETAIL @LIMIT_DATE, @LOT_NO", sp_Limit, sp_Lotno).ToList();
                //if (vResult == null)
                //    DetailGridBindingSource.Clear();
                //else if (vResult.Count == 0)
                //    DetailGridBindingSource.Clear();
                //else
                //{
                //    vResult = vResult.OrderBy(o => o.PROCESS_TURN).ThenBy(t => t.START_DATE).ToList();
                //    DetailGridBindingSource.DataSource = vResult;
                //}
                #endregion
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();

            //SetRefreshMessage(DetailGridExControl.MainGrid.RecordCount);            // 2022-06-30 김진우   마스터와 중복으로 표시되어서 둘다 제거
        }

        /// <summary>
        /// 날짜조회 4월 1일 이후로만 조회되도록 추가
        /// 2022-07-01 김진우 추가
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
                dpe_Outdate.DateFrEdit.EditValue = gdt_Check;
            }
        }

        /// <summary>
        /// 날짜조회 4월 1일 이후로만 조회되도록 추가
        /// 2022-07-01 김진우 추가
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
                if (dpe_Outdate.DateFrEdit.DateTime < gdt_Check)
                    dpe_Outdate.DateFrEdit.EditValue = gdt_Check;
                dpe_Outdate.DateToEdit.EditValue = gdt_Check;
            }
        }

        #region 이전소스
        /// <summary>
        /// 20220530 오세완 차장 
        /// 고도화 이전 날짜 조회시 메시지 출력 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Dpe_Outdate_OnDateValueChanged_Both(object sender, EventArgs e)
        //{
        //    DatePeriodEditEx dpe = sender as DatePeriodEditEx;
        //    if (dpe == null)
        //        return;

        //    string sMessage = sMessage = "고도화 적용 이후 날짜입니다. 다른 날짜를 선택해 주세요. 기준일 :" + gdt_Check.ToShortDateString();
        //    DateTime dt_To = dpe.DateToEdit.DateTime;
        //    if (dt_To < gdt_Check)
        //    {
        //        if (!gb_ClickMessageBox)
        //        {
        //            DialogResult dr = MessageBoxHandler.Show(sMessage, "검색 종료일", MessageBoxButtons.OK);
        //            if (dr == DialogResult.OK)
        //                gb_ClickMessageBox = true;
        //        }
        //        // 20220530 오세완 차장 값을 변경하면 무한루프에 빠지는 오류가 생겨서 경고창만 출력
        //        //dpe.DateToEdit.DateTime = gdt_Check;
        //    }

        //    DateTime dt_From = dpe.DateFrEdit.DateTime;
        //    if (dt_From < gdt_Check)
        //    {
        //        if (!gb_ClickMessageBox)
        //        {
        //            DialogResult dr = MessageBoxHandler.Show(sMessage, "검색 시작일", MessageBoxButtons.OK);
        //            if (dr == DialogResult.OK)
        //                gb_ClickMessageBox = true;
        //        }

        //        // 20220530 오세완 차장 값을 변경하면 무한루프에 빠지는 오류가 생겨서 경고창만 출력
        //        //if(dt_To > gdt_Check)
        //        //    dpe.DateToEdit.DateTime = gdt_Check;

        //        //dpe.DateFrEdit.DateTime = dpe.DateToEdit.DateTime.AddDays(-1);
        //    }
        //}
        #endregion
    }
}
