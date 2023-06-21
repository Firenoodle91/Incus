using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using DevExpress.Utils;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using System.Data.SqlClient;
using HKInc.Utils.Enum;

namespace HKInc.Ui.View.PUR
{
    /// <summary>
    /// 20220414 오세완 차장
    /// 자재재고관리 (LOT NO)
    /// </summary>
    public partial class XFPUR1510 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        IService<VI_PURINOUT_LOT> ModelService = (IService<VI_PURINOUT_LOT>)ProductionFactory.GetDomainService("VI_PURINOUT_LOT");
        #endregion

        public XFPUR1510()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitCombo()
        {
            //List<TN_STD1100> item_Arr = ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" &&
            //                                                                       p.TopCategory == MasterCodeSTR.Topcategory_Material).ToList();     // 2022-07-05 김진우 주석
            lup_Itemcode.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" &&
                                                                                   p.TopCategory == MasterCodeSTR.Topcategory_Material).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ITEM_CODE", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ITEM_NM", "품목");                  
            MasterGridExControl.MainGrid.AddColumn("ITEM_NM1", "품번");                 
            MasterGridExControl.MainGrid.AddColumn("TOP_CATEGORY_NAME", "대분류");            
            MasterGridExControl.MainGrid.AddColumn("MIDDLE_CATEGORY_NAME", "중분류");         
            MasterGridExControl.MainGrid.AddColumn("BOTTOM_CATEGORY_NAME", "차종");           
            MasterGridExControl.MainGrid.AddColumn("UNIT_NAME", "단위");                     
            MasterGridExControl.MainGrid.AddColumn("SAFE_QTY", "안전재고", HorzAlignment.Far, FormatType.Numeric, "N2"); 
            MasterGridExControl.MainGrid.AddColumn("SUM_INPUT", "입고수량", HorzAlignment.Far, FormatType.Numeric, "N2");
            MasterGridExControl.MainGrid.AddColumn("SUM_OUTPUT", "출고수량", HorzAlignment.Far, FormatType.Numeric, "N2");
            MasterGridExControl.MainGrid.AddColumn("TOTAL_QTY", "재고수량", HorzAlignment.Far, FormatType.Numeric, "N2");

            DetailGridExControl.SetToolbarButtonVisible(false);     // 2022-07-05 김진우 추가
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, "재고조정");
            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, false);
            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            DetailGridExControl.MainGrid.AddColumn("Type", "구분");
            DetailGridExControl.MainGrid.AddColumn("InoutDate", "입출고일", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("LotNo", "입/출고 LOTNO");
            DetailGridExControl.MainGrid.AddColumn("Qty", "입/출고량", HorzAlignment.Far, FormatType.Numeric, "N2"); 
            DetailGridExControl.MainGrid.AddColumn("UserName", "입/출고자");
            DetailGridExControl.MainGrid.AddColumn("WhCodeName", "입고창고");
            DetailGridExControl.MainGrid.AddColumn("WhPositionName", "입고위치");
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            MasterGridExControl.MainGrid.Clear();

            string sItemcode = lup_Itemcode.EditValue.GetNullToEmpty();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Middlecategory = new SqlParameter("@MIDDLE_CATEGORY", "");
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", sItemcode);

                MasterGridBindingSource.DataSource = 
                    context.Database.SqlQuery<TP_XFPUR1500_LIST>("USP_GET_XFPUR1500_LIST @MIDDLE_CATEGORY, @ITEM_CODE", sp_Middlecategory, sp_Itemcode).OrderBy(o => o.ITEM_CODE).ToList();

                #region 이전소스
                //var vResult = context.Database.SqlQuery<TP_XFPUR1500_LIST>("USP_GET_XFPUR1500_LIST @MIDDLE_CATEGORY, @ITEM_CODE", sp_Middlecategory, sp_Itemcode).ToList();
                //if (vResult == null)
                //    MasterGridBindingSource.Clear();
                //else if(vResult.Count == 0)
                //    MasterGridBindingSource.Clear();
                //else
                //{
                //    vResult = vResult.OrderBy(o => o.ITEM_CODE).ToList();
                //    MasterGridBindingSource.DataSource = vResult;
                //}
                #endregion
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            //SetRefreshMessage(MasterGridBindingSource.Count);             // 2022-07-05 김진우  디테일도 사용하여 둘다 주석처리
        }

        protected override void MasterFocusedRowChanged()
        {
            TP_XFPUR1500_LIST obj = MasterGridBindingSource.Current as TP_XFPUR1500_LIST;
            if (obj == null) return;

            DetailGridBindingSource.DataSource = ModelService.GetList(p => p.ItemCode == obj.ITEM_CODE).OrderBy(p => p.InoutDate).ToList();
            
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
            //SetRefreshMessage(DetailGridBindingSource.Count);             // 2022-07-05 김진우  마스터도 사용하여 둘다 주석처리

            #region 이전소스 오류 수정
            //List<VI_PURINOUT_LOT> detail_Arr = ModelService.GetList(p => p.ItemCode == obj.ITEM_CODE).OrderBy(p => p.InoutDate).ToList();

            ///*
            //if (detail_Arr != null)
            //{

            //    if (detail_Arr.Count > 0)
            //    {
            //        detail_Arr = detail_Arr.OrderBy(p => p.InoutDate).ToList();
            //        DetailGridBindingSource.DataSource = detail_Arr;
            //    }
            //}


            //DetailGridExControl.DataSource = DetailGridBindingSource;
            //*/

            //DetailGridExControl.DataSource = detail_Arr;
            #endregion
        }

        /// <summary>
        /// 20220415 오세완 차장
        /// 재고조정 팝업 출력 
        /// </summary>
        protected override void DetailAddRowClicked()
        {
            VI_PURINOUT_LOT detailObj = DetailGridBindingSource.Current as VI_PURINOUT_LOT;
            if (detailObj == null)
                return;

            PUR_Popup.XPFPUR_STOCK form = new PUR_Popup.XPFPUR_STOCK(detailObj.ItemCode, detailObj.LotNo, detailObj.Type, detailObj.Qty, detailObj.InoutDate);
            form.ShowDialog();
            // 20220415 오세완 차장 refresh때문에 추가 
            DataLoad();
            MasterFocusedRowChanged();
        }

        #region 안쓰는 기능
        /// <summary>
        /// EXCEL 출력
        /// 2022-04-01 김진우  TN_STD1100. 추가
        /// </summary>
        //protected override void DataExport()
        //{
        //    DialogResult dlg = MessageBox.Show("선택하신 정보만 엑셀변환 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
        //    if (dlg == DialogResult.Yes)
        //    {
        //        HKInc.Service.Controls.GridEx gv = new HKInc.Service.Controls.GridEx();
        //        BindingSource gv1 = new BindingSource();
        //        gv.MainGrid.AddColumn("ITEM_CODE", "품목코드");
        //        gv.MainGrid.AddColumn("ITEM_NM", "품목");
        //        gv.MainGrid.AddColumn("ITEM_NM1", "품번");
        //        gv.MainGrid.AddColumn("TOP_CATEGORY_NAME", "대분류");
        //        gv.MainGrid.AddColumn("MIDDLE_CATEGORY_NAME", "중분류");
        //        gv.MainGrid.AddColumn("BOTTOM_CATEGORY_NAME", "차종");
        //        gv.MainGrid.AddColumn("UNIT_NAME", "단위");
        //        gv.MainGrid.AddColumn("SAFE_QTY", "안전재고", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
        //        gv.MainGrid.AddColumn("SUM_INPUT", "입고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
        //        gv.MainGrid.AddColumn("SUM_OUTPUT", "출고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
        //        gv.MainGrid.AddColumn("TOTAL_QTY", "재고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
        //        gv1.DataSource = MasterGridBindingSource.Current as TP_XFPUR1500_LIST;
        //        gv.DataSource = gv1;
        //        HKInc.Service.Helper.ExcelExport.ExportToExcel(gv.MainGrid.MainView, DetailGridExControl.MainGrid.MainView);
        //    }
        //    else { base.DataExport(); }
        //}
        #endregion
    }
}
