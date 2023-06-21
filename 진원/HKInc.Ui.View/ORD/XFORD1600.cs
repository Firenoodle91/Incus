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
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;

namespace HKInc.Ui.View.ORD
{
    public partial class XFORD1600 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1600> ModelService = (IService<TN_ORD1600>)ProductionFactory.GetDomainService("TN_ORD1600");
       
        public XFORD1600()
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

            if (column.Name == "ItemCode")
            {
                if (masterObj.ORD1601List.Count > 0)
                {
                    e.Valid = false;
                }
            }
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            masterreview();
        }

        private void masterreview()
        {
            TN_ORD1600 obj = MasterGridBindingSource.Current as TN_ORD1600;
            obj.OutDate = obj.ORD1601List.OrderBy(p => p.OutDate).FirstOrDefault().OutDate;
            obj.OutQty = obj.ORD1601List.Sum(s => s.OutQty).GetDecimalNullToZero();
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

            MasterGridExControl.MainGrid.AddColumn("OutNo", "출고번호");         
            MasterGridExControl.MainGrid.AddColumn("CustCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종", true, HorzAlignment.Center);
            MasterGridExControl.MainGrid.AddColumn("OrderQty", "요청수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OutDate", "출고일");
            MasterGridExControl.MainGrid.AddColumn("OutId", "출고자");
            MasterGridExControl.MainGrid.AddColumn("OutState", "상태");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CustCode", "ItemCode", "OrderQty", "OutId", "OutState", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("OutNo", false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            DetailGridExControl.MainGrid.AddColumn("LotNo", "LOTNO");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutDate", "출고일");            
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "LotNo", "OutQty","OutDate", "Memo");

        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
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
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutDate >= dateOrderDate.DateFrEdit.DateTime &&
                                                                                  p.OutDate <= dateOrderDate.DateToEdit.DateTime) &&
                                                                                  (string.IsNullOrEmpty(customerCode) ? true : p.CustCode == customerCode))
                                                                   .OrderBy(p => p.OutDate)
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

                TN_ORD1600 obj = MasterGridBindingSource.Current as TN_ORD1600;
                if (obj == null) return;

                DetailGridBindingSource.DataSource = obj.ORD1601List.OrderBy(o => o.Seq).ToList();
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
            TN_ORD1600 obj = new TN_ORD1600()
            {
                OutNo = DbRequesHandler.GetRequestNumber("PTOUT"),
                OutDate = DateTime.Today
            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }
        protected override void DeleteRow()
        {
            TN_ORD1600 tn = MasterGridBindingSource.Current as TN_ORD1600;
            if (tn.ORD1601List.Count > 0)
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
            TN_ORD1600 obj = MasterGridBindingSource.Current as TN_ORD1600;
            if (obj == null) return;

            if (obj.ItemCode == null) return;

            

            TN_ORD1601 newobj = new TN_ORD1601()
            {
                OutNo = obj.OutNo,
                Seq = obj.ORD1601List.Count == 0 ? 1 : obj.ORD1601List.OrderBy(o => o.Seq).LastOrDefault().Seq + 1,
                ItemCode=obj.ItemCode,
                OutDate = DateTime.Today
               
            };
            DetailGridBindingSource.Add(newobj);
            obj.ORD1601List.Add(newobj);

            if (obj.ORD1601List.Count > 1)
            {
                if (obj.ORD1601List.GroupBy(g => g.ItemCode).Count() > 1)
                {
                    DetailGridBindingSource.Remove(newobj);
                    obj.ORD1601List.Remove(newobj);
                }
            }
        }
     
     
        protected override void DeleteDetailRow()
        {
            TN_ORD1600 obj = MasterGridBindingSource.Current as TN_ORD1600;
            TN_ORD1601 delobj = DetailGridBindingSource.Current as TN_ORD1601;
            obj.ORD1601List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                TN_ORD1600 obj = MasterGridBindingSource.Current as TN_ORD1600;
                if (DetailGridBindingSource == null) return;
                var PrintList = DetailGridBindingSource.List as List<TN_ORD1601>;
                if (PrintList.Count == 0) return;

                var FirstReport = new REPORT.ROUTLABLE();
                foreach (var v in PrintList.OrderByDescending(p => p.CreateTime).ToList())
                {
                    PRT_OUTLABLE prt = new PRT_OUTLABLE()
                    {
                        ItemCode = v.ItemCode,
                        ItemNm = DbRequesHandler.GetCellValue("SELECT ITEM_NM FROM TN_STD1100T WHERE ITEM_CODE='"+v.ItemCode+"'",0 ),
                        ItemNm1 = DbRequesHandler.GetCellValue("SELECT ITEM_NM1 FROM TN_STD1100T WHERE ITEM_CODE='" + v.ItemCode + "'", 0),
                        PrtDate = v.OutDate.ToString().Substring(0, 10),
                        Qty = v.OutQty,
                        LotNo=v.OutNo,
                        CustLotNo= obj.Memo
                    };
                    var report = new REPORT.ROUTLABLE(prt);
                    report.CreateDocument();
                    FirstReport.Pages.AddRange(report.Pages);

                    
                }
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.ShowPreview();//.Print();
                //DataLoad();
                DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }
    }
}
