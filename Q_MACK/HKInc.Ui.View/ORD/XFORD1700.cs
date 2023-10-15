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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.ORD
{
    /// <summary>
    /// 제품기타입고등록
    /// </summary>
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

            MasterGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailView_CellValueChanged;
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //재조회 전 품명, 품번, 차종 표시

            GridView GV = sender as GridView;
            TN_ORD1700 MasterObj = MasterGridBindingSource.Current as TN_ORD1700;

            if (e.Column.Name != "ItemCode") return;

            string Item = GV.GetFocusedRowCellValue(GV.Columns["ItemCode"]).ToString();
            TN_STD1100 STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == Item).FirstOrDefault();

            MasterObj.TN_STD1100 = STD1100;
            MasterGridExControl.BestFitColumns();

        }
        private void DetailView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TN_ORD1700 obj = MasterGridBindingSource.Current as TN_ORD1700;
            obj.InDate = obj.ORD1701List.OrderBy(p => p.InDate).FirstOrDefault().InDate;
            obj.InQty = obj.ORD1701List.Sum(s => s.InQty).GetDecimalNullToZero();
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            MasterGridExControl.MainGrid.AddColumn("InNo", "입고번호");         
            MasterGridExControl.MainGrid.AddColumn("CustCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품목");
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
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품목");                   // 2022-04-07 김진우 추가
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");                  // 2022-04-07 김진우 추가
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
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
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemCode");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
        }

        protected override void InitCombo()
        {
            lupCustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
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
            IsFormControlChanged = false;
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();

            TN_ORD1700 obj = MasterGridBindingSource.Current as TN_ORD1700;
            if (obj == null) return;

            DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_ORD1701>(p => p.InNo == obj.InNo).ToList();
            //DetailGridBindingSource.DataSource = obj.ORD1701List.OrderBy(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            MasterGridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        protected override void AddRowClicked()
        {
            TN_ORD1700 obj = new TN_ORD1700()
            {
                InNo = DbRequestHandler.GetRequestNumber("PTIN"),
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
            else
            {
                MasterGridBindingSource.RemoveCurrent();
                ModelService.Delete(tn);
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_ORD1700 obj = MasterGridBindingSource.Current as TN_ORD1700;
            if (obj == null) return;

            if (obj.ItemCode == null)       // 2022-01-25 김진우 대리        입고 마스터에 품목코드 입력하지 않고 디테일에 추가시 추가되어서 추가
            {
                MessageBoxHandler.Show("품목코드가 존재하지 않습니다.");
                return;
            }

            TN_ORD1701 newobj = new TN_ORD1701()
            {
                InNo = obj.InNo,
                Seq = obj.ORD1701List.Count == 0 ? 1 : obj.ORD1701List.OrderBy(o => o.Seq).LastOrDefault().Seq + 1,
                ItemCode = obj.ItemCode,
                InDate = DateTime.Today,
            };

            //재조회 전 품명, 품번, 차종 표시
            string Item = obj.ItemCode.GetNullToEmpty();
            TN_STD1100 STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == Item).FirstOrDefault();
            newobj.TN_STD1100 = STD1100;

            DetailGridBindingSource.Add(newobj);
            obj.ORD1701List.Add(newobj);
        }
      
        protected override void DeleteDetailRow()
        {
            TN_ORD1700 obj = MasterGridBindingSource.Current as TN_ORD1700;
            TN_ORD1701 delobj = DetailGridBindingSource.Current as TN_ORD1701;
            if (obj == null || delobj == null) return;

            obj.ORD1701List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }
    }
}
