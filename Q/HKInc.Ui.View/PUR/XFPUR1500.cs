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

namespace HKInc.Ui.View.PUR
{
    public partial class XFPUR1500 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PURSTOCK> ModelService = (IService<VI_PURSTOCK>)ProductionFactory.GetDomainService("VI_PURSTOCK");

        public XFPUR1500()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitCombo()
        {
            lupitemtype.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype,2));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품목");                  // 2022-04-01 김진우 수정
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");                 // 2022-04-01 김진우 수정
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류");            // 2022-04-01 김진우 수정
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류");         // 2022-04-01 김진우 수정
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");           // 2022-04-01 김진우 수정
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");                     // 2022-04-01 김진우 수정
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.SafeQty", "안전재고", HorzAlignment.Far, FormatType.Numeric, "N2"); // 주의    // 2022-03-07 김진우 소수점 0. 표시 안되는것 수정
            MasterGridExControl.MainGrid.AddColumn("InQty", "입고수량", HorzAlignment.Far, FormatType.Numeric, "N2");           // 2022-03-07 김진우 소수점 0. 표시 안되는것 수정
            MasterGridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, FormatType.Numeric, "N2");          // 2022-03-07 김진우 소수점 0. 표시 안되는것 수정
            MasterGridExControl.MainGrid.AddColumn("StockQty", "재고수량", HorzAlignment.Far, FormatType.Numeric, "N2");        // 2022-03-07 김진우 소수점 0. 표시 안되는것 수정

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("NO", false);             // 입고 NO, 출고 NO
            DetailGridExControl.MainGrid.AddColumn("SEQ", false);             // 입고 SEQ, 출고 SEQ
            DetailGridExControl.MainGrid.AddColumn("InOutDate", "입출고일");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            DetailGridExControl.MainGrid.AddColumn("ItemNm", "품목");
            DetailGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("InputQty", "입고량", HorzAlignment.Far, FormatType.Numeric, "N2");         // 2022-03-07 김진우 소수점 0. 표시 안되는것 수정
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고량", HorzAlignment.Far, FormatType.Numeric, "N2");           // 2022-03-07 김진우 소수점 0. 표시 안되는것 수정
            DetailGridExControl.MainGrid.AddColumn("Work","입출고자");
            DetailGridExControl.MainGrid.AddColumn("WhCode", "입고창고");                               // 2022-04-01 김진우 추가
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "위치");                               // 2022-04-01 김진우 추가
            //DetailGridExControl.MainGrid.AddColumn("CheckResult", "수입검사");                        // 2022-04-01 김진우 주석
        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");             // 2022-04-01 김진우 TN_STD1100추가
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");          // 2022-04-01 김진우 TN_STD1100추가
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");              // 2022-04-01 김진우 TN_STD1100추가
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");                           // 2022-04-01 김진우 TN_STD1100추가

            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => 1==1 ).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");          // 2022-04-01 김진우 주석
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Work", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");                              // 2022-04-01 김진우 추가
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "WhPositionCode", "WhPositionName");          // 2022-04-01 김진우 추가
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InOutDate");
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();

            string itemtype = lupitemtype.EditValue.GetNullToEmpty();
            string item = tx_Item.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemtype) ? true : p.TN_STD1100.MiddleCategory == itemtype)
                                                                         &&(string.IsNullOrEmpty(item) ?true: (p.ItemCode.Contains(item)||p.TN_STD1100.ItemNm.Contains(item)))).ToList();                                                  
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void MasterFocusedRowChanged()
        {
            VI_PURSTOCK obj = MasterGridBindingSource.Current as VI_PURSTOCK;
            if (obj == null)
            {
                DetailGridExControl.DataSource = null;
                return;
            }
            DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_PURINOUT>(p => p.ItemCode == obj.ItemCode).OrderBy(o => o.InOutDate).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        /// <summary>
        /// EXCEL 출력
        /// 2022-04-01 김진우  TN_STD1100. 추가
        /// </summary>
        protected override void DataExport()
        {
            DialogResult dlg = MessageBox.Show("선택하신 정보만 엑셀변환 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
            if (dlg == DialogResult.Yes)
            {
                HKInc.Service.Controls.GridEx gv = new HKInc.Service.Controls.GridEx();
                BindingSource gv1 = new BindingSource();
                gv.MainGrid.AddColumn("ItemCode", "품목코드");
                gv.MainGrid.AddColumn("TN_STD1100.ItemNm", "품목");
                gv.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
                gv.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류");
                gv.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류");
                gv.MainGrid.AddColumn("TN_STD1100.BottomCategory", "소분류");
                gv.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
                gv.MainGrid.AddColumn("TN_STD1100.SafeQty", "안전재고", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
                gv.MainGrid.AddColumn("InQty", "입고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
                gv.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
                gv.MainGrid.AddColumn("StockQty", "재고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
                gv.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
                gv.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
                gv.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 3), "Mcode", "Codename");
                gv.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
                gv1.DataSource = MasterGridBindingSource.Current as VI_PURSTOCK;
                gv.DataSource = gv1;
                HKInc.Service.Helper.ExcelExport.ExportToExcel(gv.MainGrid.MainView, DetailGridExControl.MainGrid.MainView);
            }
            else { base.DataExport(); }
        }
    }
}
