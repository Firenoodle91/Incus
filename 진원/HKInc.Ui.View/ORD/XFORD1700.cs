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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;

namespace HKInc.Ui.View.ORD
{
    public partial class XFORD1700 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1700> ModelService = (IService<TN_ORD1700>)ProductionFactory.GetDomainService("TN_ORD1700");
       
        public XFORD1700()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dateOrderDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dateOrderDate.DateToEdit.DateTime = DateTime.Today;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            // MasterGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
            MasterGridExControl.MainGrid.MainView.ValidatingEditor += MainView_ValidatingEdior;
        }

        private void MainView_ValidatingEdior(object sender, BaseContainerValidateEditorEventArgs e)
        {
            ColumnView columnView = sender as ColumnView;
            GridColumn column = (e as EditFormValidateEditorEventArgs)?.Column ?? columnView.FocusedColumn;

            var masterObj = MasterGridBindingSource.Current as TN_ORD1600;
            if (masterObj != null)
            {
                if (column.Name == "ItemCode")
                {
                    if (masterObj.ORD1601List.Count > 0)
                    {
                        e.Valid = false;
                    }
                }
            }
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            masterreview();
        }

        private void masterreview()
        {
            TN_ORD1700 obj = MasterGridBindingSource.Current as TN_ORD1700;
            obj.InDate = obj.ORD1701List.OrderBy(p => p.InDate).FirstOrDefault().InDate;
            obj.InQty = obj.ORD1701List.Sum(s => s.InQty).GetDecimalNullToZero();
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        private void MainView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

          
            //TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            //obj.OutDate = obj.ORD1201List.OrderBy(p => p.OutDate).FirstOrDefault().OutDate;
            //obj.OutQty = obj.ORD1201List.Sum(s => s.OutQty).GetDecimalNullToZero(); 
            
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            MasterGridExControl.MainGrid.AddColumn("InNo", "입고번호");         
            MasterGridExControl.MainGrid.AddColumn("CustCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("InQty", "입고수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("InDate", "입고일");
            MasterGridExControl.MainGrid.AddColumn("InId", "입고자");            
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CustCode", "ItemCode",  "InId", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("InNo", false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            DetailGridExControl.MainGrid.AddColumn("LotNo", "LOTNO");
            DetailGridExControl.MainGrid.AddColumn("InQty", "입고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InDate", "입고일");            
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "LotNo", "InQty","InDate", "Memo");

        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
        }
        protected override void InitCombo()
        {
            lupCustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }
        protected override void DataLoad()
        {
          
            MasterGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string customerCode = lupCustcode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.InDate >= dateOrderDate.DateFrEdit.DateTime &&
                                                                                  p.InDate <= dateOrderDate.DateToEdit.DateTime) &&
                                                                                  (string.IsNullOrEmpty(customerCode) ? true : p.CustCode == customerCode))
                                                                   .OrderBy(p => p.InDate)
                                                                   .ToList();
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

                TN_ORD1700 obj = MasterGridBindingSource.Current as TN_ORD1700;
                if (obj == null) return;

                DetailGridBindingSource.DataSource = obj.ORD1701List.OrderBy(o => o.Seq).ToList();
                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.BestFitColumns();
            }
            finally
            {
                //SetRefreshMessage("OrderList", MasterGridExControl.MainGrid.RecordCount,
                //                  "DetailList", DetailGridExControl.MainGrid.RecordCount);
            }
        }
        protected override void DataSave()
        {




            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            MasterGridBindingSource.EndEdit();

            ModelService.Save();
          //  ModelService2.Save();

            DataLoad();
        }
        protected override void AddRowClicked()
        {
            TN_ORD1700 obj = new TN_ORD1700()
            {
                InNo = DbRequesHandler.GetRequestNumber("PTIN"),
                InDate = DateTime.Today
            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }
        protected override void DeleteRow()
        {
            TN_ORD1700 tn = MasterGridBindingSource.Current as TN_ORD1700;
            if (tn.ORD1701List.Count > 0)
            {
                MessageBox.Show("출고내역이 있어서 삭제 불가합니다.");
            }
            else {
                MasterGridBindingSource.RemoveCurrent();
                ModelService.Delete(tn);
            }


        }
        protected override void DetailAddRowClicked()
        {
            TN_ORD1700 obj = MasterGridBindingSource.Current as TN_ORD1700;
            if (obj == null) return;

            if (obj.ItemCode == null) return;

            TN_ORD1701 newobj = new TN_ORD1701()
            {
                InNo = obj.InNo,
                Seq = obj.ORD1701List.Count == 0 ? 1 : obj.ORD1701List.OrderBy(o => o.Seq).LastOrDefault().Seq + 1,
                ItemCode=obj.ItemCode,
                InDate = DateTime.Today
               
            };
            DetailGridBindingSource.Add(newobj);
            obj.ORD1701List.Add(newobj);






        }
      
        protected override void DeleteDetailRow()
        {
            TN_ORD1700 obj = MasterGridBindingSource.Current as TN_ORD1700;
            TN_ORD1701 delobj = DetailGridBindingSource.Current as TN_ORD1701;
            obj.ORD1701List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        
    }
}
