using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;
using DevExpress.XtraEditors.Repository;

namespace HKInc.Ui.View.PUR
{
    public partial class XFPUR2200 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR2200> ModelService = (IService<TN_PUR2200>)ProductionFactory.GetDomainService("TN_PUR2200");

        public XFPUR2200()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;


            MasterGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            MasterGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailView_ShowingEditor;

            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-10);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(+10);
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_PUR2200;
            if (masterObj == null) return;

            if (view.FocusedColumn.FieldName == "InCustomerCode")
            {
                if (masterObj.PoNo != null)
                    e.Cancel = true;
            }

            if (view.FocusedColumn.FieldName == "InConfirmFlag")
            {
                if (masterObj.InConfirmFlag == "Y")
                {
                    //출고된 InLot가 있을경우 입고확정을 풀 수 없게 처리
                    int cntCheck = DbRequesHandler.GetRowCount("SELECT IIF(COUNT(*) = 0, NULL, 1) FROM TN_PUR2201T A JOIN TN_ORD1201T B ON A.IN_LOT_NO = B.LOT_NO WHERE A.IN_NO = '" + masterObj.InNo + "'");
                    if (cntCheck > 0)
                    {
                        MessageBox.Show("출고 된 LOT가 존재 합니다.");
                        e.Cancel = true;
                    }
                        
                }
            }

        }

        private void DetailView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_PUR2200;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR2201;
            if (detailObj == null) return;

            if (masterObj.InConfirmFlag == "Y")
                e.Cancel = true;

            //출고된 LOT 수정 막기
            if (ModelService.GetChildList<TN_ORD1201>(x => x.LotNo == detailObj.InLotNo).ToList().Count > 0)
                e.Cancel = true;
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_PUR2200;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR2201;
            if (detailObj == null) return;


        }

        protected override void InitCombo()
        {
            lupcust.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "발주내역참조");
            IsMasterGridButtonFileChooseEnabled = true;

            MasterGridExControl.MainGrid.AddColumn("InNo", "입고번호");
            MasterGridExControl.MainGrid.AddColumn("InDate", "입고일");
            MasterGridExControl.MainGrid.AddColumn("InId", "입고자");
            MasterGridExControl.MainGrid.AddColumn("PoNo", "발주번호");
            MasterGridExControl.MainGrid.AddColumn("TN_PUR2100.PoDate", "발주일");
            MasterGridExControl.MainGrid.AddColumn("InCustomerCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.AddColumn("InConfirmFlag", "입고완료");

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InDate", "InId", "InCustomerCode", "Memo", "InConfirmFlag");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "발주내역참조");
            IsDetailGridButtonFileChooseEnabled = true;

            DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "InSeq", true);
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택");
            DetailGridExControl.MainGrid.AddColumn("InNo", false);
            DetailGridExControl.MainGrid.AddColumn("InSeq", "입고순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            DetailGridExControl.MainGrid.AddColumn("PoNo", "발주번호");
            DetailGridExControl.MainGrid.AddColumn("PoSeq", "발주순번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
            DetailGridExControl.MainGrid.AddColumn("ReqQty", "발주수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("Cost", "발주단가", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("ReqAmt", "발주금액", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("InQty", "입고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("InCost", "입고단가", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("InputAmt", "입고금액", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("PrintQty", "라벨수", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InYn", false);
            DetailGridExControl.MainGrid.AddColumn("InLotNo", "InLotNo");
            DetailGridExControl.MainGrid.AddColumn("InWhCode", "창고");
            DetailGridExControl.MainGrid.AddColumn("InWhPosition", "위치코드");
            DetailGridExControl.MainGrid.AddColumn("Memo");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "ItemCode", "InQty", "InCost", "PrintQty", "InWhCode", "InWhPosition", "Memo");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InCustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            //MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("ReqDate");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("InConfirmFlag", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(x => x.UseYn == "Y" && x.TopCategory == MasterCodeSTR.ITEM_TYPE_OUT).ToList(), "ItemCode", "ItemCode");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("InYn", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");

            var WhPositionEdit = DetailGridExControl.MainGrid.Columns["InWhPosition"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            WhPositionEdit.Popup += WhPositionEdit_Popup;
        }

        private void WhPositionEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_PUR2201;
            if (detailObj == null) return;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.InWhCode + "'";
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InputNo");
            string cust = lupcust.EditValue.GetNullToEmpty();
            string inputNo = tx_InNO.Text.GetNullToEmpty();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();

            MasterGridBindingSource.DataSource = ModelService.GetList(x => (dp_date.DateFrEdit.DateTime <= x.InDate && x.InDate <= dp_date.DateToEdit.DateTime)
                                                                        && (string.IsNullOrEmpty(cust) ? true : x.InCustomerCode == cust)
                                                                        && (string.IsNullOrEmpty(inputNo) ? true : x.InNo == inputNo))
                                                                        .OrderBy(o => o.InNo).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void DataSave()
        {
            //for (int rowHandle = 0; rowHandle < gridEx2.MainGrid.MainView.RowCount; rowHandle++)
            //{

            //    string inqty = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "InputQty").GetNullToEmpty());
            //    if (inqty == "0") { HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage(40)); return; }
            //    if (inqty == "") { HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage(40)); return; }

            //}

            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            ModelService.Save();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();

            var masterObj = MasterGridBindingSource.Current as TN_PUR2200;
            if (masterObj == null) return;

            DetailGridBindingSource.DataSource = masterObj.TN_PUR2201List.ToList();

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void FileChooseClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            //param.SetValue(PopupParameter.Constraint, "Final");

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTPUR2100, param, AddRowCallback);
            form.ShowPopup(true);
        }

        protected override void DetailFileChooseClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR2200;
            if (masterObj == null) return;

            if (masterObj.PoNo == null) return;

            if (masterObj.InConfirmFlag == "Y") return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, masterObj.PoNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTPUR2101, param, DetailAddRowCallback);
            form.ShowPopup(true);
        }

        protected override void AddRowClicked()
        {
            TN_PUR2200 newObj = new TN_PUR2200();
            newObj.InNo = DbRequesHandler.GetRequestNumber("OIN");
            newObj.InDate = DateTime.Today;
            newObj.InConfirmFlag = "N";

            MasterGridBindingSource.Add(newObj);
            ModelService.Insert(newObj);
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        private void AddRowCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            List<TN_PUR2100> rtnList = (List<TN_PUR2100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var s in rtnList)
            {
                TN_PUR2200 newObj = new TN_PUR2200();
                newObj.InNo = DbRequesHandler.GetRequestNumber("OIN");
                newObj.PoNo = s.PoNo;
                newObj.InDate = DateTime.Today;
                newObj.InCustomerCode = s.CustomerCode;
                newObj.InConfirmFlag = "N";

                MasterGridBindingSource.Add(newObj);
                ModelService.Insert(newObj);
            }

            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR2200;
            if (masterObj == null) return;

            if (masterObj.InConfirmFlag == "Y")
            {
                MessageBox.Show("입고완료건은 변경할수 없습니다.");
                return;
            }

            List<TN_STD1100> itemlist = ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == MasterCodeSTR.ITEM_TYPE_OUT) && p.MainCust == masterObj.InCustomerCode).OrderBy(o => o.ItemNm).ToList();
            if (itemlist.Count() == 0)
            {
                MessageBox.Show("해당 거래처에 등록된 품목이 없습니다.");
                return;
            }

            TN_PUR2201 newObj = new TN_PUR2201();
            newObj.InNo = masterObj.InNo;
            newObj.InSeq = masterObj.TN_PUR2201List.Count == 0 ? 1 : masterObj.TN_PUR2201List.Max(m => m.InSeq) + 1;
            newObj.PrintQty = 1;
            newObj.InConfirmFlag = "N";
            newObj.NotInconfirmFlag = "N";
            newObj.InLotNo = masterObj.InNo.ToString() + (newObj.InSeq).ToString();

            DetailGridBindingSource.Add(newObj);
            masterObj.TN_PUR2201List.Add(newObj);

            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void DetailAddRowCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            List<TN_PUR2101> rtnList = (List<TN_PUR2101>)e.Map.GetValue(PopupParameter.ReturnObject);

            var masterObj = MasterGridBindingSource.Current as TN_PUR2200;
            if (masterObj == null) return;

            foreach (var s in rtnList)
            {
                TN_PUR2201 newObj = new TN_PUR2201();
                newObj.InNo = masterObj.InNo;
                newObj.InSeq = masterObj.TN_PUR2201List.Count == 0 ? 1 : masterObj.TN_PUR2201List.Max(m => m.InSeq) + 1;
                newObj.PoNo = s.PoNo;
                newObj.PoSeq = s.PoSeq;
                newObj.ItemCode = s.ItemCode;
                newObj.PrintQty = 1;
                newObj.InConfirmFlag = "N";
                newObj.NotInconfirmFlag = "N";
                newObj.InLotNo = masterObj.InNo.ToString() + (newObj.InSeq).ToString();

                DetailGridBindingSource.Add(newObj);
                masterObj.TN_PUR2201List.Add(newObj);
            }

            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR2200;
            if (masterObj == null) return;

            if (masterObj.TN_PUR2201List.Count > 0)
            {
                MessageBox.Show("상세내역이 있어 삭제할수 없습니다.");
                return;
            }

            if (masterObj.InConfirmFlag == "Y")
            {
                MessageBox.Show("입고완료 삭제할수 없습니다.");
                return;
            }

            MasterGridBindingSource.Remove(masterObj);
            ModelService.Delete(masterObj);
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR2200;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR2201;
            if (detailObj == null) return;

            if (masterObj.InConfirmFlag == "Y")
            {
                MessageBox.Show("입고 확정건은 삭제할수 없습니다.");
                return;
            }

            if (ModelService.GetChildList<TN_ORD1201>(x => x.LotNo == detailObj.InLotNo).ToList().Count > 0)
            {
                MessageBox.Show("출고된 LOT는 삭제할수 없습니다.");
                return;
            }

            DetailGridBindingSource.Remove(detailObj);
            masterObj.TN_PUR2201List.Remove(detailObj);

            DetailGridExControl.MainGrid.BestFitColumns();
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var detailList = DetailGridBindingSource.List as List<TN_PUR2201>;
            if (detailList == null) return;

            try
            {
                WaitHandler.ShowWait();

                var printList = detailList.Where(x => x._Check == "Y").ToList();

                if (printList.Count == 0) return;

                var FirstReport = new REPORT.RINPUTLABLE();
                foreach (var s in printList)
                {
                    decimal iold = Convert.ToDecimal(s.InQty);
                    int i = Convert.ToInt32(s.InQty / (s.PrintQty == 0 ? 1 : s.PrintQty));

                    decimal ii = Convert.ToDecimal(s.InQty / (s.PrintQty == 0 ? 1 : s.PrintQty));
                    s.InQty = ii;
                    for (int j = 0; j < (s.PrintQty == 0 ? 1 : s.PrintQty); j++)
                    {
                        var report = new REPORT.RINPUTLABLE(s);
                        report.CreateDocument();
                        FirstReport.Pages.AddRange(report.Pages);
                    }
                    s._Check = "N";
                    s.InQty = iold;
                }
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.ShowPreview();//.Print();
                                          //DataLoad();
                                          //     DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }

        private void simpleButton1_Click_COPY(object sender, EventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                if (DetailGridBindingSource == null) return;
                var PrintList = DetailGridBindingSource.List as List<TN_PUR1301>;
                if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0) return;

                var FirstReport = new REPORT.RINPUTLABLE();
                foreach (var v in PrintList.Where(p => p._Check == "Y").OrderByDescending(p => p.CreateTime).ToList())
                {

                    decimal iold = Convert.ToDecimal(v.InputQty);
                    int i = Convert.ToInt32(v.InputQty / (v.Lqty == 0 ? 1 : v.Lqty));
                    decimal ii = Convert.ToDecimal(v.InputQty / (v.Lqty == 0 ? 1 : v.Lqty));
                    v.InputQty = ii;
                    for (int j = 0; j < (v.Lqty == 0 ? 1 : v.Lqty); j++)
                    {
                        var report = new REPORT.RINPUTLABLE(v);
                        report.CreateDocument();
                        FirstReport.Pages.AddRange(report.Pages);
                    }
                    v._Check = "N";
                    v.InputQty = iold;

                }
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.ShowPreview();//.Print();
                                          //DataLoad();
                                          //     DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }
    }
}