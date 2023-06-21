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
    /// 재고관리(일) 화면
    /// </summary>
    public partial class XFORD1500 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PRODQTY_MST> ModelService = (IService<VI_PRODQTY_MST>)ProductionFactory.GetDomainService("VI_PRODQTY_MST");
     
        public XFORD1500()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            dp_plan.DateTime = DateTime.Today;
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
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번", true);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명", true);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.MainCust", "주거래처", true);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류", true);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류", true);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "소분류", true);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위", true);
            MasterGridExControl.MainGrid.AddColumn("Inqty", "생산(입고)량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("Outqty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("Stockqty", "재고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.SafeQty", "안전재고", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Memo", "메모");

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("ResultDate", "일자", true);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);            
            DetailGridExControl.MainGrid.AddColumn("Inqty", "생산(입고)량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Outqty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Stockqty", "재고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");



        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MainCust", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 3), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            //  DetailGridExControl.MainGrid.SetRepositoryItemMemoEdit("Memo");
            MasterGridExControl.MainGrid.MainView.Columns["TN_STD1100.Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
        }
        protected override void InitCombo()
        {
          

            lupItemcode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y"&&p.TopCategory!="P03").OrderBy(o=>o.ItemNm1).ToList());
         
        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            //string item = lupItemcode.EditValue.GetNullToEmpty();
            string Item = textItemCodeName.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => p.TN_STD1100.ItemNm.Contains(Item) || p.TN_STD1100.ItemNm1.Contains(Item)).OrderBy(o => o.TN_STD1100.ItemNm1).ToList();
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
            VI_PRODQTY_MST obj = MasterGridBindingSource.Current as VI_PRODQTY_MST;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            string yyyy = dp_plan.DateTime.ToString().Substring(0,7);
            var olist = ModelService.GetChildList<VI_PRODQTY_DAY>(p => p.ItemCode == obj.ItemCode).ToList();
            DetailGridBindingSource.DataSource = olist.Where(p => p.date == yyyy).OrderBy(o => o.ResultDate).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }
    }
}
