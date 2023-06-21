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
using System.Windows.Forms;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.QC
{
    /// <summary>
    /// 20220418 오세완 차장 
    /// lot역추적 고도화 전
    /// </summary>
    public partial class XFQC1910 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        private DateTime Date_Check;            // 2022-06-30 김진우 추가        고도화 적용 날짜
        #endregion

        public XFQC1910()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            Date_Check = new DateTime(2022, 03, 31);            // 2022-06-30 김진우       날짜기준 추가
            dpe_Outdate.DateFrEdit.EditValue = Date_Check.AddMonths(-1);
            dpe_Outdate.DateToEdit.EditValue = Date_Check;
            //dpe_Outdate.DateFrEdit.EditValue = DateTime.Now.AddMonths(-1);        //2022-06-30 김진우        현시간 기준이 아닌 4월 1일 기준으로 수정
            //dpe_Outdate.DateToEdit.EditValue = DateTime.Now;                      //2022-06-30 김진우        현시간 기준이 아닌 4월 1일 기준으로 수정

            dpe_Outdate.DateFrEdit.DateTimeChanged += DateFrEdit_ChangeValue;
            dpe_Outdate.DateToEdit.DateTimeChanged += DateToEdit_ChangeValue;
        }

        protected override void InitCombo()
        {
            //List<TN_STD1100> item_Arr = ModelService.GetList(p => p.TopCategory == MasterCodeSTR.Topcategory_Final_Product && p.UseYn == "Y");            // 2022-06-30 김진우       기존방식으로 변경
            slup_Itemcode.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetList(p => p.TopCategory == MasterCodeSTR.Topcategory_Final_Product && p.UseYn == "Y"));

            //List<TN_STD1400> cust_Arr = ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y");         // 2022-06-30 김진우       기존방식으로 변경
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
            //DetailGridExControl.SetToolbarButtonVisible(false); //2022-06-30 김진우  툴바버튼 말고 툴바안보이게 수정
            DetailGridExControl.MainGrid.AddColumn("WORK_NO", "작업지시번호");
            DetailGridExControl.MainGrid.AddColumn("ITEM_CODE", "품목코드");
            DetailGridExControl.MainGrid.AddColumn("PROCESS_NAME", "공정명");
            DetailGridExControl.MainGrid.AddColumn("PROCESS_TURN", "공정순서");
            DetailGridExControl.MainGrid.AddColumn("LOT_NO", "LOTNO");
            DetailGridExControl.MainGrid.AddColumn("START_DATE", "작업시작일시", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("END_DATE", "작업종료일시", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("RESULT_DATE", "마지막 작업일", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("RESULT_QTY", "생산수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("FAIL_QTY", "불량수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OK_QTY", "양품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("WORK_NAME", "작업자명");
            DetailGridExControl.MainGrid.AddColumn("MACHINE_NAME", "설비명");
            DetailGridExControl.MainGrid.AddColumn("SRC_CODE", "원소재 품목코드");
            DetailGridExControl.MainGrid.AddColumn("SRC_NM", "원소재 품번");
            DetailGridExControl.MainGrid.AddColumn("SRC_NM1", "원소재 품명");
            DetailGridExControl.MainGrid.AddColumn("SRC_LOT", "원소재 LOTNO");
            DetailGridExControl.MainGrid.AddColumn("SRC_CODE1", "원소재1 품목코드");
            DetailGridExControl.MainGrid.AddColumn("SRC1_NM", "원소재1 품번");
            DetailGridExControl.MainGrid.AddColumn("SRC1_NM1", "원소재1 품명");
            DetailGridExControl.MainGrid.AddColumn("SRC_LOT1", "원소재1 LOTNO");
            DetailGridExControl.MainGrid.AddColumn("SRC_CODE2", "원소재2 품목코드");
            DetailGridExControl.MainGrid.AddColumn("SRC2_NM", "원소재2 품번");
            DetailGridExControl.MainGrid.AddColumn("SRC2_NM1", "원소재2 품명");
            DetailGridExControl.MainGrid.AddColumn("SRC_LOT2", "원소재2 LOTNO");
            DetailGridExControl.MainGrid.AddColumn("SRC_CODE3", "원소재3 품목코드");
            DetailGridExControl.MainGrid.AddColumn("SRC3_NM", "원소재3 품번");
            DetailGridExControl.MainGrid.AddColumn("SRC3_NM1", "원소재3 품명");
            DetailGridExControl.MainGrid.AddColumn("SRC_LOT3", "원소재3 LOTNO");
            DetailGridExControl.MainGrid.AddColumn("SRC_CODE4", "원소재4 품목코드");
            DetailGridExControl.MainGrid.AddColumn("SRC4_NM", "원소재4 품번");
            DetailGridExControl.MainGrid.AddColumn("SRC4_NM1", "원소재4 품명");
            DetailGridExControl.MainGrid.AddColumn("SRC_LOT4", "원소재4 LOTNO");
            DetailGridExControl.MainGrid.AddColumn("SRC_CODE5", "원소재5 품목코드");
            DetailGridExControl.MainGrid.AddColumn("SRC5_NM", "원소재5 품번");
            DetailGridExControl.MainGrid.AddColumn("SRC5_NM1", "원소재5 품명");
            DetailGridExControl.MainGrid.AddColumn("SRC_LOT5", "원소재5 LOTNO");
            DetailGridExControl.MainGrid.AddColumn("SRC_CODE6", "원소재6 품목코드");
            DetailGridExControl.MainGrid.AddColumn("SRC6_NM", "원소재6 품번");
            DetailGridExControl.MainGrid.AddColumn("SRC6_NM1", "원소재6 품명");
            DetailGridExControl.MainGrid.AddColumn("SRC_LOT6", "원소재6 LOTNO");
            DetailGridExControl.MainGrid.AddColumn("SRC_CODE7", "원소재7 품목코드");
            DetailGridExControl.MainGrid.AddColumn("SRC7_NM", "원소재7 품번");
            DetailGridExControl.MainGrid.AddColumn("SRC7_NM1", "원소재7 품명");
            DetailGridExControl.MainGrid.AddColumn("SRC_LOT7", "원소재7 LOTNO");
        }
       
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("OUT_NO");
            #endregion

            MasterGridExControl.MainGrid.Clear();       // 2022-06-30 김진우 추가
            DetailGridExControl.MainGrid.Clear();       // 2022-06-30 김진우 추가

            //MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            InitCombo();

            string sOutno = tx_Outno.EditValue.GetNullToEmpty();
            string sItemcode = slup_Itemcode.EditValue.GetNullToEmpty();
            string sCustcode = slup_Custcode.EditValue.GetNullToEmpty();
            string sDatefrom = dpe_Outdate.DateFrEdit.DateTime.ToShortDateString();
            string sDateto = dpe_Outdate.DateToEdit.DateTime.ToShortDateString();
            //string sDatefrom = Convert.ToDateTime(dpe_Outdate.DateFrEdit.EditValue).ToShortDateString();      // 2022-06-30 김진우       날짜 변경 후 변경된 날짜가 적용이 안됨
            //string sDateto = Convert.ToDateTime(dpe_Outdate.DateToEdit.EditValue).ToShortDateString();        // 2022-06-30 김진우       날짜 변경 후 변경된 날짜가 적용이 안됨

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Datefrom = new SqlParameter("@DATE_FROM", sDatefrom);
                SqlParameter sp_Dateto = new SqlParameter("@DATE_TO", sDateto);
                SqlParameter sp_Outno = new SqlParameter("@OUT_NO", sOutno);
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", sItemcode);
                SqlParameter sp_Custcode = new SqlParameter("@CUST_CODE", sCustcode);
                SqlParameter sp_Check = new SqlParameter("@CHECK", "A");            // 2022-07-01 김진우 추가

                // 2022-06-30 김진우       기존방식에서 변경
                MasterGridBindingSource.DataSource = 
                    context.Database.SqlQuery<TP_XFQCT1910_LIST>("USP_GET_XFQCT1910_LIST @DATE_FROM, @DATE_TO, @OUT_NO, @ITEM_CODE, @CUST_CODE, @CHECK",    // @CHECK 추가        2022-07-01 김진우
                    sp_Datefrom, sp_Dateto, sp_Outno, sp_Itemcode, sp_Custcode, sp_Check).OrderBy(o => o.OUT_DATE).ThenBy(t => t.ITEM_CODE).ToList();       // sp_Check 추가      2022-07-01 김진우
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
            //SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);      // 2022-06-30 김진우   디테일과 중복으로 표시되어서 둘다 제거
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            TP_XFQCT1910_LIST masterObj = MasterGridBindingSource.Current as TP_XFQCT1910_LIST;
            if (masterObj == null) return;

            //DetailGridExControl.MainGrid.Clear();
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Outno = new SqlParameter("@OUT_NO", masterObj.OUT_NO);

                // 2022-06-30 김진우 조회조건 수정
                DetailGridBindingSource.DataSource = 
                    context.Database.SqlQuery<TP_XFQCT1910_DETAIL>("USP_GET_XFQCT1910_DETAIL @OUT_NO", sp_Outno)
                    .OrderBy(o => o.PROCESS_TURN).ThenBy(t => t.START_DATE).ToList();
                #region 이전소스
                //var vResult = context.Database.SqlQuery<TP_XFQCT1910_DETAIL>("USP_GET_XFQCT1910_DETAIL @OUT_NO", sp_Outno).ToList();
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

            //SetRefreshMessage(DetailGridExControl.MainGrid.RecordCount);      // 2022-06-30 김진우   마스터와 중복으로 표시되어서 둘다 제거
        }

        /// <summary>
        /// 날짜조회 4월 1일 이전으로만 조회되도록 추가
        /// 2022-06-30 김진우 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateFrEdit_ChangeValue(object sender, EventArgs e)
        {
            DateEditEx Sender = sender as DateEditEx;
            if (Sender == null) return;
            DateTime FrDate = Sender.DateTime;

            if (FrDate > Date_Check)
            {
                DialogResult dr = MessageBoxHandler.Show("고도화 적용 이후 날짜입니다. 다른 날짜를 선택해 주세요. 기준일 :" + Date_Check.ToShortDateString(), "검색 종료일", MessageBoxButtons.OK);
                dpe_Outdate.DateFrEdit.EditValue = Date_Check;
            }
        }

        /// <summary>
        /// 날짜조회 4월 1일 이전으로만 조회되도록 추가
        /// 2022-06-30 김진우 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DateToEdit_ChangeValue(object sender, EventArgs e)
        {
            DateEditEx Sender = sender as DateEditEx;
            if (Sender == null) return;
            DateTime ToDate = Sender.DateTime;

            if (ToDate > Date_Check)
            {
                DialogResult dr = MessageBoxHandler.Show("고도화 적용 이후 날짜입니다. 다른 날짜를 선택해 주세요. 기준일 :" + Date_Check.ToShortDateString(), "검색 종료일", MessageBoxButtons.OK);
                if (dpe_Outdate.DateFrEdit.DateTime > Date_Check)
                    dpe_Outdate.DateFrEdit.EditValue = Date_Check;
                dpe_Outdate.DateToEdit.EditValue = Date_Check;
            }
        }

    }
}
