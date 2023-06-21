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
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.ORD
{
    /// <summary>
    /// 제품기타출고관리
    /// </summary>
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

            MasterGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailView_CellValueChanged;
           // MasterGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //재조회 전 품명, 품번, 차종 표시

            GridView GV = sender as GridView;
            TN_ORD1600 MasterObj = MasterGridBindingSource.Current as TN_ORD1600;

            if (e.Column.Name != "ItemCode") return;

            string Item = GV.GetFocusedRowCellValue(GV.Columns["ItemCode"]).ToString();
            TN_STD1100 STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == Item).FirstOrDefault();

            MasterObj.TN_STD1100 = STD1100;
            MasterGridExControl.BestFitColumns();
        }
        private void DetailView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TN_ORD1600 obj = MasterGridBindingSource.Current as TN_ORD1600;
            obj.OutDate = obj.ORD1601List.OrderBy(p => p.OutDate).FirstOrDefault().OutDate;
            obj.OutQty = obj.ORD1601List.Sum(s => s.OutQty).GetDecimalNullToZero();
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            MasterGridExControl.MainGrid.AddColumn("OutNo", "출고번호");         
            MasterGridExControl.MainGrid.AddColumn("CustCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
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
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품목");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
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
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemCode");    // 2022-02-14 김진우 ItemNm1수정
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
        }
        protected override void InitCombo()
        {
            lupCustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }
        protected override void DataLoad()
        {
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

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

            //IsFormControlChanged = false;
        }
        protected override void MasterFocusedRowChanged()
        {
            TN_ORD1600 obj = MasterGridBindingSource.Current as TN_ORD1600;
            if (obj == null) return;

            DetailGridBindingSource.DataSource = obj.ORD1601List.OrderBy(o => o.Seq).ToList();
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
            TN_ORD1600 obj = new TN_ORD1600()
            {
                OutNo = DbRequestHandler.GetRequestNumber("PTOUT"),
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

            if (obj.ItemCode == null)       // 2022-01-25 김진우 대리        입고 마스터에 품목코드 입력하지 않고 디테일에 추가시 추가되어서 추가
            {
                MessageBoxHandler.Show("품목코드가 존재하지 않습니다.");
                return;
            }

            TN_ORD1601 newobj = new TN_ORD1601()
            {
                OutNo = obj.OutNo,
                Seq = obj.ORD1601List.Count == 0 ? 1 : obj.ORD1601List.OrderBy(o => o.Seq).LastOrDefault().Seq + 1,
                ItemCode=obj.ItemCode,
                OutDate = DateTime.Today
            };

            //재조회 전 품명, 품번, 차종 표시
            string Item = obj.ItemCode.GetNullToEmpty();
            TN_STD1100 STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == Item).FirstOrDefault();
            newobj.TN_STD1100 = STD1100;

            DetailGridBindingSource.Add(newobj);
            obj.ORD1601List.Add(newobj);
        }
     
        protected override void DeleteDetailRow()
        {
            TN_ORD1600 obj = MasterGridBindingSource.Current as TN_ORD1600;
            TN_ORD1601 delobj = DetailGridBindingSource.Current as TN_ORD1601;
            if (obj == null || delobj == null) return;

            obj.ORD1601List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        /// <summary>
        /// 포장라벨출력 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        ItemNm = DbRequestHandler.GetCellValue("SELECT ITEM_NM FROM TN_STD1100T WHERE ITEM_CODE='"+v.ItemCode+"'",0 ),
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
                DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }
    }
}
