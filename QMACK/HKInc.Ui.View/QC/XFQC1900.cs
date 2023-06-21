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
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Service.Controls;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.QC
{
    /// <summary>
    /// lot추척관리 (고도화 전)
    /// </summary>
    public partial class XFQC1900 : HKInc.Service.Base.ListFormTemplate
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
        /// 2022-06-29 김진우 제거
        /// 메시지창 중복 출력 방지
        /// </summary>
        //private bool gb_ClickMessageBox = false;
        #endregion

        public XFQC1900()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            // 20220523 오세완 차장 고도화전 일자는 조회를 못하게 기준일 로직을 수정 
            //dp_dt.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            //dp_dt.DateToEdit.DateTime = DateTime.Today;

            gdt_Check = new DateTime(2022, 04, 01);
            dp_dt.DateFrEdit.DateTime = gdt_Check.AddMonths(-1);
            dp_dt.DateToEdit.DateTime = gdt_Check.AddDays(-1);

            dp_dt.DateFrEdit.DateTimeChanged += DateFrEdit_ChangeValue;             // 2022-06-29 김진우 04-01이전 데이터 조회를 위해 추가
            dp_dt.DateToEdit.DateTimeChanged += DateToEdit_ChangeValue;             // 2022-06-29 김진우 04-01이전 데이터 조회를 위해 추가 
            //dp_dt.OnDateValueChanged_Both += Dp_dt_OnDateValueChanged_Both;       // 2022-06-29 김진우 오류로 인한 주석
            //dp_dt.OnBeforePopup += Dp_dt_OnBeforePopup;                           // 2022-06-29 김진우 미사용으로 주석
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
            GridExControl.MainGrid.AddColumn("WorkNo","작업지시번호");
            GridExControl.MainGrid.AddColumn("Seq",false);
            GridExControl.MainGrid.AddColumn("ItemCode","품목");
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            GridExControl.MainGrid.AddColumn("ProcessCode","공정");
            GridExControl.MainGrid.AddColumn("ProcessTurn","공정순서");
            GridExControl.MainGrid.AddColumn("LotNo","LOTNO");
            GridExControl.MainGrid.AddColumn("StartDate", "작업시작일시");
            GridExControl.MainGrid.AddColumn("EndDate", "작업종료일시");
            GridExControl.MainGrid.AddColumn("ResultDate","마지막 작업일");
            GridExControl.MainGrid.AddColumn("ResultQty","생산수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("FailQty","불량수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("OkQty","양품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("WorkId","작업자");            
            GridExControl.MainGrid.AddColumn("McCode","설비");
            GridExControl.MainGrid.AddColumn("SrcCode","원소재1");
            GridExControl.MainGrid.AddColumn("SrcLot","원소재LOT1");
            GridExControl.MainGrid.AddColumn("SrcCode1", "원소재2");
            GridExControl.MainGrid.AddColumn("SrcLot1", "원소재LOT2");
            GridExControl.MainGrid.AddColumn("SrcCode2", "원소재3");
            GridExControl.MainGrid.AddColumn("SrcLot2", "원소재LOT3");
            GridExControl.MainGrid.AddColumn("SrcCode3", "원소재4");
            GridExControl.MainGrid.AddColumn("SrcLot3", "원소재LOT4");
            GridExControl.MainGrid.AddColumn("SrcCode4", "원소재5");
            GridExControl.MainGrid.AddColumn("SrcLot4", "원소재LOT5");
            GridExControl.MainGrid.AddColumn("SrcCode5", "원소재6");
            GridExControl.MainGrid.AddColumn("SrcLot5", "원소재LOT6");
            GridExControl.MainGrid.AddColumn("SrcCode6", "원소재7");
            GridExControl.MainGrid.AddColumn("SrcLot6", "원소재LOT7");
            GridExControl.MainGrid.AddColumn("SrcCode7", "원소재8");
            GridExControl.MainGrid.AddColumn("SrcLot7", "원소재LOT8");
            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("StartDate");
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("EndDate");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode2", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode3", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode4", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode5", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode6", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("SrcCode7", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => true).ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("McCode", ModelService.GetChildList<TN_MEA1000>(p => true).ToList(), "MachineCode", "MachineName");
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string item = lupItem.EditValue.GetNullToEmpty();
            string lot = tx_LotNo.EditValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(item) ? true : p.ItemCode == item)
                                                                  && (string.IsNullOrEmpty(lot) ? true : p.LotNo == lot)
                                                                  && (p.ResultDate >= dp_dt.DateFrEdit.DateTime && p.ResultDate <= dp_dt.DateToEdit.DateTime)
                                                                  && (p.ResultDate < gdt_Check))       // 2022-06-30 김진우       필수적으로 4월 1일 이전만 조회되도록 추가
                                                                .OrderBy(o=>o.ItemCode).OrderBy(o => o.ProcessTurn).OrderBy(o=>o.LotNo)
                                                                .ToList();
        
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        /// <summary>
        /// 날짜조회 4월 1일 이전으로만 조회되도록 추가
        /// 2022-06-29 김진우 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateFrEdit_ChangeValue(object sender, EventArgs e)
        {
            DateEditEx Sender = sender as DateEditEx;
            if (Sender == null) return;
            DateTime FrDate = Sender.DateTime;

            if (FrDate > gdt_Check)
            {
                DialogResult dr = MessageBoxHandler.Show("고도화 적용 이후 날짜입니다. 다른 날짜를 선택해 주세요. 기준일 :" + gdt_Check.ToShortDateString(), "검색 종료일", MessageBoxButtons.OK);
                dp_dt.DateFrEdit.EditValue = gdt_Check;
            }
        }

        /// <summary>
        /// 날짜조회 4월 1일 이전으로만 조회되도록 추가
        /// 2022-06-29 김진우 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DateToEdit_ChangeValue(object sender, EventArgs e)
        {
            DateEditEx Sender = sender as DateEditEx;
            if (Sender == null) return;
            DateTime ToDate = Sender.DateTime;

            if (ToDate > gdt_Check)
            {
                DialogResult dr = MessageBoxHandler.Show("고도화 적용 이후 날짜입니다. 다른 날짜를 선택해 주세요. 기준일 :" + gdt_Check.ToShortDateString(), "검색 종료일", MessageBoxButtons.OK);
                if (dp_dt.DateFrEdit.DateTime > gdt_Check)
                    dp_dt.DateFrEdit.EditValue = gdt_Check;
                dp_dt.DateToEdit.EditValue = gdt_Check;
            }
        }

        #region 미사용
        // 미사용
        //protected override void DataSave()
        //{
        //    GridExControl.MainGrid.PostEditor();
        //    GridBindingSource.EndEdit();

        //    ModelService.Save();
        //    DataLoad();
        //}

        /// <summary>
        /// 2022-06-29 김진우 미사용으로 주석 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Dp_dt_OnBeforePopup(object sender, EventArgs e)
        //{
        //    gb_ClickMessageBox = false;
        //}

        /// <summary>
        /// 20220523 오세완 차장 
        /// 고도화 이후 날짜 조회시 메시지 출력 처리
        /// 2022-06-29 김진우 오류로 인한 주석처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Dp_dt_OnDateValueChanged_Both(object sender, EventArgs e)
        //{
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