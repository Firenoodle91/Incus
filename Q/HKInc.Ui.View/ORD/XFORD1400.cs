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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.ORD
{
    /// <summary>
    /// 재고관리(월)
    /// </summary>
    public partial class XFORD1400 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PRODQTY_MST> ModelService = (IService<VI_PRODQTY_MST>)ProductionFactory.GetDomainService("VI_PRODQTY_MST");
     
        public XFORD1400()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dp_plan.DateTime = DateTime.Today;

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
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Memo", "메모");

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("YYYYMM", "년월", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);            
            DetailGridExControl.MainGrid.AddColumn("Inqty", "생산(입고)량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Outqty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Stockqty", "재고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MainCust", ModelService.GetChildList<TN_STD1400>(p => 1 == 1).ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            //  DetailGridExControl.MainGrid.SetRepositoryItemMemoEdit("Memo");
            MasterGridExControl.MainGrid.MainView.Columns["TN_STD1100.Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void InitCombo()
        {
            // 20220219 오세완 차장 품목 / 품명이 동일하게 나오는 오류가 있어서 생략                 2022-02-23 김진우 ItemCodeColumnSetting 수정하여 원복
            lupItemcode.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && 
                                                                                                           (p.TopCategory == MasterCodeSTR.Topcategory_Final_Product || p.TopCategory == MasterCodeSTR.Topcategory_Half_Product)).OrderBy(o => o.ItemNm1).ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();

            MasterGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string item = lupItemcode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(item) ? true : p.ItemCode == item)).OrderBy(o => o.ItemCode).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
            //IsFormControlChanged = false;
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();

            VI_PRODQTY_MST obj = MasterGridBindingSource.Current as VI_PRODQTY_MST;
            if (obj == null) return;
            string yyyy = dp_plan.DateTime.Year.ToString();
            DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_PRODQTY_MON>(p => p.ItemCode == obj.ItemCode && p.YYYYMM.Substring(0, 4) == yyyy).OrderByDescending(o => o.YYYYMM).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }
  
    }
}
