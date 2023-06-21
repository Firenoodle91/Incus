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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.ORD
{
    public partial class XFORD1300 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PRODQTY_MST> ModelService = (IService<VI_PRODQTY_MST>)ProductionFactory.GetDomainService("VI_PRODQTY_MST");
     
        public XFORD1300()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
          
        }

        private void MainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {


                object saftqty = View.GetRowCellValue(e.RowHandle, View.Columns["TN_STD1100.SafeQty"]);
                object stockqty = View.GetRowCellValue(e.RowHandle, View.Columns["Stockqty"]);
                if (saftqty.GetDecimalNullToZero() != 0)
                {
                    if (stockqty.GetDecimalNullToZero() <= (saftqty.GetDecimalNullToZero() == 0 ? 0 : (saftqty.GetDecimalNullToZero() / 100 * 115)))


                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;

                    }
                }


            }
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.MainCust", "고객사", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드",true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("Inqty", "생산(입고)량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("Outqty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("Stockqty", "재고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.SafeQty", "안전재고", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Cost", "단가", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2");
            MasterGridExControl.MainGrid.AddColumn("TotalCost", "합계", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Memo", "메모");

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            DetailGridExControl.MainGrid.AddColumn("LotNo", "LOTNO", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("Inqty", "생산(입고)량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Outqty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");



        }
        protected override void InitRepository()
        {

            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MainCust", ModelService.GetChildList<TN_STD1400>(p => 1 == 1).ToList(), "CustomerCode", "CustomerName");
            //  DetailGridExControl.MainGrid.SetRepositoryItemMemoEdit("Memo");
            MasterGridExControl.MainGrid.MainView.Columns["TN_STD1100.Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }
        protected override void InitCombo()
        {
            //lupItemcode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P01" || p.TopCategory == "P05")).OrderBy(o=>o.ItemNm1).ToList());
            lupItemcode.SetDefault(true, "ItemCode", "ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == MasterCodeSTR.ITEM_TYPE_WAN || p.TopCategory == MasterCodeSTR.ITEM_TYPE_BAN || p.TopCategory == MasterCodeSTR.ITEM_TYPE_OUT)).OrderBy(o => o.ItemNm1).ToList());
        }
        protected override void DataLoad()
        {
            
            MasterGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string item = lupItemcode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(item) ? true : p.ItemCode == item)).OrderBy(o => o.ItemCode).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);

            LoadDetail();

            IsFormControlChanged = false;
        }
        protected override void MasterFocusedRowChanged()
        {
            LoadDetail();
        }
        private void LoadDetail()
        {
            try
            {
                DetailGridExControl.MainGrid.Clear();

                VI_PRODQTY_MST obj = MasterGridBindingSource.Current as VI_PRODQTY_MST;
                if (obj == null) return;

                DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_PRODQTYMSTLOT>(p => p.ItemCode == obj.ItemCode).OrderBy(o => o.LotNo).ToList();
                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.BestFitColumns();
            }
            finally
            {
                //SetRefreshMessage("OrderList", MasterGridExControl.MainGrid.RecordCount,
                //                  "DetailList", DetailGridExControl.MainGrid.RecordCount);
            }
        }
      
  
    }
}
