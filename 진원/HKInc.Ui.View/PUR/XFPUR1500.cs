﻿using System;
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
            lupitemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype,2));
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            //MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");            
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory","차종");
            MasterGridExControl.MainGrid.AddColumn("Unit","단위");
            MasterGridExControl.MainGrid.AddColumn("SafeQty","안전재고", HorzAlignment.Far, FormatType.Numeric, "N2");
            MasterGridExControl.MainGrid.AddColumn("InQty","입고수량", HorzAlignment.Far, FormatType.Numeric, "N2");
            MasterGridExControl.MainGrid.AddColumn("OutQty","출고수량", HorzAlignment.Far, FormatType.Numeric, "N2");
            MasterGridExControl.MainGrid.AddColumn("StockQty","재고수량", HorzAlignment.Far, FormatType.Numeric, "N2");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Cost", "단가", HorzAlignment.Far, FormatType.Numeric, "N2");
            MasterGridExControl.MainGrid.AddColumn("TotalCost", "합계", HorzAlignment.Far, FormatType.Numeric, "N2");

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("Num",false);
            DetailGridExControl.MainGrid.AddColumn("InOutDate", "입출고일");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            DetailGridExControl.MainGrid.AddColumn("InputQty", "입고량", HorzAlignment.Far, FormatType.Numeric, "N2");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고량", HorzAlignment.Far, FormatType.Numeric, "N2");
            DetailGridExControl.MainGrid.AddColumn("Work","입출고자");       
            DetailGridExControl.MainGrid.AddColumn("CheckResult", "수입검사");
        }
        protected override void InitRepository()
        {
           
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            // MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => 1==1 ).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Work", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InOutDate");
         
        }
        protected override void DataLoad()
        {
            ModelService.ReLoad();
          //  DetailGridExControl.DataSource = null;
            string itemtype = lupitemtype.EditValue.GetNullToEmpty();
            string item = tx_Item.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemtype) ? true : p.MiddleCategory == itemtype)
                                                                         &&(string.IsNullOrEmpty(item) ?true: (p.ItemCode.Contains(item)||p.ItemNm.Contains(item)))).ToList();                                                  
            //MasterGridBindingSource.DataSource = ModelService.GetList();
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
        protected override void DataExport()
        {
            DialogResult dlg = MessageBox.Show("선택하신 정보만 엑셀변환 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
            if (dlg == DialogResult.Yes)
            {
                HKInc.Service.Controls.GridEx gv = new HKInc.Service.Controls.GridEx();
                BindingSource gv1 = new BindingSource();
                gv.MainGrid.AddColumn("ItemCode", "품목코드");
                gv.MainGrid.AddColumn("ItemNm", "품번");
                gv.MainGrid.AddColumn("TopCategory", "대분류");
                gv.MainGrid.AddColumn("MiddleCategory", "중분류");
                gv.MainGrid.AddColumn("BottomCategory", "소분류");
                gv.MainGrid.AddColumn("Unit", "단위");
                gv.MainGrid.AddColumn("SafeQty", "안전재고", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
                gv.MainGrid.AddColumn("InQty", "입고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
                gv.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
                gv.MainGrid.AddColumn("StockQty", "재고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
                gv.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
                gv.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
                gv.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 3), "Mcode", "Codename");
                gv.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
                gv1.DataSource = MasterGridBindingSource.Current as VI_PURSTOCK;
                gv.DataSource = gv1;
                //HKInc.Service.Helper.ExcelExport.ExportToExcel(MasterGridExControl.MainGrid.MainView,gv.MainGrid.MainView,DetailGridExControl.MainGrid.MainView);
                HKInc.Service.Helper.ExcelExport.ExportToExcel(gv.MainGrid.MainView, DetailGridExControl.MainGrid.MainView);
            }
            else { base.DataExport(); }
        }


    }
}
