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
    /// 재고관리(LOT) 화면
    /// </summary>
    public partial class XFORD1300 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PRODQTY_MST> ModelService = (IService<VI_PRODQTY_MST>)ProductionFactory.GetDomainService("VI_PRODQTY_MST");
     
        public XFORD1300()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
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
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드",false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번", true);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명", true);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.MainCust", "주거래처", true);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류", true);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류", true);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "소분류", true);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
            MasterGridExControl.MainGrid.AddColumn("Inqty", "입고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("Outqty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("Stockqty", "재고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.SafeQty", "안전재고", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Memo", "메모");

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            DetailGridExControl.MainGrid.AddColumn("LotNo", "생산 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("PackLotNo", "포장 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("Inqty", "입고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("WhCode", "입고창고");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "입고위치");
            DetailGridExControl.MainGrid.AddColumn("Outqty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WhCode", "WhPosition");
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

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");

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
            MasterGridBindingSource.DataSource = ModelService.GetList(p => p.TN_STD1100.ItemNm.Contains(Item) || p.TN_STD1100.ItemNm1.Contains(Item)
                //(string.IsNullOrEmpty(item) ? true : p.ItemCode == item)
                                                                    )
                                                                    .OrderBy(o => o.TN_STD1100.ItemNm1)
                                                                    .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;            
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);

            IsFormControlChanged = false;
        }
        protected override void MasterFocusedRowChanged()
        {
            VI_PRODQTY_MST obj = MasterGridBindingSource.Current as VI_PRODQTY_MST;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_PRODQTYMSTLOT>(p => p.ItemCode == obj.ItemCode)
                                                            .OrderBy(o => o.LotNo)
                                                            .ThenBy(p => p.PackLotNo.IsNullOrEmpty() ? 0 : p.PackLotNo.Length)
                                                            .ThenBy(p => p.PackLotNo)
                                                            .ThenByDescending(p => p.Inqty)
                                                            .ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            SetSaveMessageCheck = false;

            IService<TN_MPS1700> WMS_Service = (IService<TN_MPS1700>)ProductionFactory.GetDomainService("TN_MPS1700");
            var DetailList = DetailGridBindingSource.List as List<VI_PRODQTYMSTLOT>;
            if (DetailList != null)
            {
                var InList = DetailList.Where(p => p.EditRowFlag == "Y").ToList();

                foreach (var v in InList)
                {
                    var upObj = WMS_Service.GetChildList<TN_MPS1700>(p => p.PackLotNo == v.PackLotNo).FirstOrDefault();
                    if (upObj != null)
                    {
                        SetSaveMessageCheck = true;
                        upObj.WhCode = v.WhCode;
                        upObj.WhPosition = v.WhPosition;
                        WMS_Service.UpdateChild(upObj);
                    }
                }
            }

            if (SetSaveMessageCheck)
            {
                WMS_Service.Save();
                WMS_Service.Dispose();
                ActRefresh();
            }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            VI_PRODQTYMSTLOT obj = DetailGridBindingSource.Current as VI_PRODQTYMSTLOT;
            if (obj == null || obj.Inqty == 0) e.Cancel = true;
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            VI_PRODQTYMSTLOT obj = DetailGridBindingSource.Current as VI_PRODQTYMSTLOT;
            if (obj == null) return;
            if (e.Column.FieldName == "WhCode" || e.Column.FieldName == "WhPosition")
                obj.EditRowFlag = "Y";
        }

    }
}
