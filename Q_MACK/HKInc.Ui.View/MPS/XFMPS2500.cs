using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 생산실적변경
    /// </summary>
    public partial class XFMPS2500 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_MPS1401V> ModelService_1401 = (IService<VI_MPS1401V>)ProductionFactory.GetDomainService("VI_MPS1401V");     // 명칭만 변경   2022-07-19 김진우
        IService<VI_MPS1405V> ModelService_1405 = (IService<VI_MPS1405V>)ProductionFactory.GetDomainService("VI_MPS1405V");     // 명칭만 변경   2022-07-19 김진우

        public XFMPS2500()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.CellValueChanging += DetailGrid_CellValueChanging;        
            //DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailGrid_CellValueChanged;        // 값 변경시 바로 추가되도록 변경    2022-07-19 김진우
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today;
        }

        /// <summary>
        /// 디테일에서 양품수량과 불량수량 값 변경시 EditRowFlag Y로 변경 후 저장시 값 변경
        /// 2022-07-18 김진우
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailGrid_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            VI_MPS1405V DetailObj = DetailGridBindingSource.Current as VI_MPS1405V;
            DetailObj.EditRowFlag = "Y";

            #region 이전소스    값이 수정이 안되서 수정       2022-07-18
            //if (e.Column.Name.ToString() != "ResultQty")
            //{
            //VI_MPS1401V obj = MasterGridBindingSource.Current as VI_MPS1401V;
            //GridView gv = sender as GridView;
            //int qty = 0;
            //if (e.Column.Name.ToString() != "ResultQty")
            //{
            //    if (e.Column.Name.ToString() == "OkQty")
            //    {
            //        qty = gv.GetFocusedRowCellValue(gv.Columns["OkQty"]).GetIntNullToZero() + gv.GetFocusedRowCellValue(gv.Columns["FailQty"]).GetIntNullToZero();

            //        int rqty = 0;
            //        int okqty = 0;
            //        int fqty = 0;
            //        for (int i = 0; i < gv.RowCount; i++)
            //        {
            //            rqty += gv.GetRowCellValue(i, gv.Columns["OkQty"]).GetIntNullToZero() + gv.GetRowCellValue(i, gv.Columns["FailQty"]).GetIntNullToZero();
            //            okqty += gv.GetRowCellValue(i, gv.Columns["OkQty"]).GetIntNullToZero();
            //            fqty += gv.GetRowCellValue(i, gv.Columns["FailQty"]).GetIntNullToZero();
            //        }
            //        obj.ResultQty = rqty;
            //        obj.OkQty = okqty;
            //        obj.FailQty = fqty;
            //        gv.SetFocusedRowCellValue(gv.Columns["ResultQty"], qty.ToString());
            //    }
            //    if (e.Column.Name.ToString() == "FailQty")
            //    {
            //        qty = gv.GetFocusedRowCellValue(gv.Columns["OkQty"]).GetIntNullToZero() + gv.GetFocusedRowCellValue(gv.Columns["FailQty"]).GetIntNullToZero();

            //        int rqty = 0;
            //        int okqty = 0;
            //        int fqty = 0;
            //        for (int i = 0; i < gv.RowCount; i++)
            //        {
            //            rqty += gv.GetRowCellValue(i, gv.Columns["OkQty"]).GetIntNullToZero() + gv.GetRowCellValue(i, gv.Columns["FailQty"]).GetIntNullToZero();
            //            okqty +=  gv.GetRowCellValue(i, gv.Columns["OkQty"]).GetIntNullToZero();
            //            fqty += gv.GetRowCellValue(i, gv.Columns["FailQty"]).GetIntNullToZero();
            //        }
            //        obj.ResultQty = rqty;
            //        obj.OkQty = okqty;
            //        obj.FailQty = fqty;
            //        gv.SetFocusedRowCellValue(gv.Columns["ResultQty"], qty.ToString());
            //    }

            //    MasterGridBindingSource.EndEdit();
            //    MasterGridExControl.MainGrid.BestFitColumns();
            //}
            //}
            #endregion
        }

        protected override void InitCombo()
        {
            // 20220219 오세완 차장 품목 / 품명이 동일하게 나오는 오류가 있어서 생략             2022-02-23 김진우 ItemCodeColumnSetting 수정하여 원복            
            //lupitemcode.SetDefault(true, "ItemCode", "ItemNm", ModelService_1401.GetChildList<TN_STD1100>(p => (p.TopCategory == MasterCodeSTR.Topcategory_Material || p.TopCategory == MasterCodeSTR.Tpocategory_Outsorcing_Product)).ToList());
            lupitemcode.SetDefault(true, "ItemCode", "ItemNm", ModelService_1401.GetChildList<TN_STD1100>(p => (p.TopCategory == MasterCodeSTR.Topcategory_Final_Product || p.TopCategory == MasterCodeSTR.Tpocategory_Outsorcing_Product)).ToList());
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("WorkDate", "작업지시일");
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");                   // 2022-02-16 김진우 수정
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품목");                         // 2022-02-16 김진우 추가
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품명");                        // 2022-02-16 김진우 추가
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");                    
            MasterGridExControl.MainGrid.AddColumn("LotNo", "LotNo");
            MasterGridExControl.MainGrid.AddColumn("ResultQty", "생산수량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##}");
            MasterGridExControl.MainGrid.AddColumn("OkQty", "양품수량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##}");
            MasterGridExControl.MainGrid.AddColumn("FailQty", "불량수량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##}");

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("WorkDate", "작업지시일", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", HorzAlignment.Center, true);               // 2022-02-16 김진우 수정
            DetailGridExControl.MainGrid.AddColumn("ItemNm", "품목", HorzAlignment.Center, true);                     // 2022-02-16 김진우 추가
            DetailGridExControl.MainGrid.AddColumn("ItemNm1", "품번", HorzAlignment.Center, true);                    // 2022-02-16 김진우 추가
            DetailGridExControl.MainGrid.AddColumn("LotNo", "LotNo", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("ResultDate", "작업일", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("MachineName", "설비명", HorzAlignment.Center, true);              // 2022-04-13 김진우 추가
            DetailGridExControl.MainGrid.AddColumn("ResultQty", "생산수량", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("OkQty", "양품수량", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("FailQty", "불량수량", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("WorkId", "작업자", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OkQty", "FailQty","WorkId");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService_1401.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();     // 2022-07-18 김진우 추가

            //ModelService.ReLoad();     // 미사용 주석   2022-07-18 김진우
            

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService_1401.ReLoad();

            string itemcode = lupitemcode.EditValue.GetNullToEmpty();
            string workno = TX_WorkNo.EditValue.GetNullToEmpty();
            string lot = tx_lotno.EditValue.GetNullToEmpty();

            // 2022-06-22 김진우 추가     작업지시가 12시가 넘으면 조회가 되지 않아서 추가
            DateTime FrDateTime = datePeriodEditEx1.DateFrEdit.DateTime;
            DateTime ToDateTime = datePeriodEditEx1.DateToEdit.DateTime;
            DateTime FrTime = new DateTime(FrDateTime.Year, FrDateTime.Month, FrDateTime.Day, 00, 00, 00);
            DateTime ToTime = new DateTime(ToDateTime.Year, ToDateTime.Month, ToDateTime.Day, 23, 59, 59);

            MasterGridBindingSource.DataSource = ModelService_1401.GetList(p => (p.WorkDate >= FrTime && p.WorkDate <= ToTime)
                                                                        && (string.IsNullOrEmpty(itemcode) ? true : p.ItemCode == itemcode)
                                                                        && (string.IsNullOrEmpty(workno) ? true : p.WorkNo.Contains(workno))
                                                                        && (string.IsNullOrEmpty(lot) ? true : p.LotNo.Contains(lot))       // 2022-02-16 김진우 contains 로 수정
                                                                        ).OrderBy(o => o.WorkDate)                                          // 2022-02-16 김진우 추가
                                                                        .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();         // 2022-07-18 김진우 추가
        }

        protected override void MasterFocusedRowChanged()
        {
            VI_MPS1401V obj = MasterGridBindingSource.Current as VI_MPS1401V;
            if (obj == null) return;

            #region 미사용 주석
            //if (DetailGridBindingSource.Count != 0)
            //{
            //    MasterGridBindingSource.EndEdit();
            //    DetailGridBindingSource.EndEdit();
            //    ModelService.Save();
            //    ModelServiceDtl.Save();
            //}
            //ModelService.ReLoad();   
            #endregion

            DetailGridBindingSource.DataSource = ModelService_1405.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.LotNo == obj.LotNo).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            #region 디테일 값 수정

            VI_MPS1405V DetailObj = DetailGridBindingSource.Current as VI_MPS1405V;
            List<VI_MPS1405V> DetailList = DetailGridBindingSource.DataSource as List<VI_MPS1405V>;
            if (DetailObj != null || DetailList != null)
            {
                foreach (var v in DetailList)
                {
                    if (v.EditRowFlag == "Y")
                    {
                        TN_MPS1405 MPS1405 = ModelService_1401.GetChildList<TN_MPS1405>(p => p.WorkNo == v.WorkNo && p.Seq == v.Seq && p.ProcessCode == v.ProcessCode).ToList().FirstOrDefault();
                        //TN_MPS1405 MPS1405 = ModelService.GetList(p => p.WorkNo == DetailList[i].WorkNo && p.Seq == DetailList[i].Seq && p.ProcessCode == DetailList[i].ProcessCode).ToList().FirstOrDefault();

                        MPS1405.ResultQty = v.FailQty.GetIntNullToZero() + v.OkQty.GetIntNullToZero();
                        MPS1405.OkQty = v.OkQty.GetIntNullToZero();
                        MPS1405.FailQty = v.FailQty.GetIntNullToZero();

                        MPS1405.WorkId = DetailObj.WorkId;

                        int MPS1401_UPDATE = DbRequestHandler.SetDataQury("EXEC USP_UPD_MPS1405_QTY_CHANGE '" + v.WorkDate.ToString("yyyy-MM-dd hh:mm:ss.fff") + "', '"
                                                                                                              + v.WorkNo + "', '"
                                                                                                              + v.Seq + "', '"
                                                                                                              + v.ProcessCode + "', '"
                                                                                                              //+ v.P_Seq + "', '"
                                                                                                              + MPS1405.ResultQty + "', '"
                                                                                                              + MPS1405.OkQty + "', '"
                                                                                                              + MPS1405.FailQty + "', '"
                                                                                                              + MPS1405.WorkId + "'");

                    }
                }
            }
            #endregion

            #region 저장시 디테일 값을 기반으로 마스터값도 수정    2022-07-18 김진우 추가
            VI_MPS1401V MasterObj = MasterGridBindingSource.Current as VI_MPS1401V;

            if (MasterObj != null || DetailList != null)
            {
                Nullable<int> Tot_ResultQty = 0;
                Nullable<int> Tot_OkQty = 0;
                Nullable<int> Tot_FailQty = 0;

                for (int i = 0; i < DetailList.Count; i++)
                {
                    Tot_ResultQty += DetailList[i].OkQty.GetIntNullToZero() + DetailList[i].FailQty.GetIntNullToZero();
                    Tot_OkQty += DetailList[i].OkQty.GetIntNullToZero();
                    Tot_FailQty += DetailList[i].FailQty.GetIntNullToZero();
                }

                int MPS1401_UPDATE = DbRequestHandler.SetDataQury("EXEC USP_UPD_MPS1401_QTY_CHANGE '" + MasterObj.WorkDate.ToString("yyyy-MM-dd hh:mm:ss.fff") + "', '"
                                                                                                      + MasterObj.WorkNo + "', '"
                                                                                                      + MasterObj.Seq + "', '"
                                                                                                      + MasterObj.ProcessCode + "', '"
                                                                                                      + Tot_ResultQty + "', '"
                                                                                                      + Tot_OkQty + "', '"
                                                                                                      + Tot_FailQty + "'");
            }
            #endregion

            DataLoad();
        }
    }
}
