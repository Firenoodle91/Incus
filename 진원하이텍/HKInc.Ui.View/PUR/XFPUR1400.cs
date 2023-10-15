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
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.PUR
{
    public partial class XFPUR1400 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {

        IService<TN_PUR1500> ModelService = (IService<TN_PUR1500>)ProductionFactory.GetDomainService("TN_PUR1500");
        public XFPUR1400()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
         
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(+30);
            //MasterGridExControl.MainGrid.MainView.CellValueChanged += MaserMainView_CellValueChanged;
            MasterGridExControl.MainGrid.MainView.CustomColumnDisplayText += MasterView_DisplayText;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            TN_PUR1501 obj = DetailGridBindingSource.Current as TN_PUR1501;
            //GridView gv = sender as GridView;
            //string aa = DbRequesHandler.GetCellValue("SELECT top 1 [no] FROM [TN_QCT1200T] where  temp1='" + gv.GetFocusedRowCellValue("Temp2").GetNullToEmpty() + "'", 0);
            if (obj.Temp1 == null)
            {
                MessageBox.Show("자동출고건은 변경이 불가능합니다.");
                e.Cancel = true;
            }
        }

        private void MasterView_DisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            GridView gv = (GridView)sender;

            if (e.Column.Name == "ProductItemcodeItemNm1" || e.Column.Name == "ProductItemcodeItemNm")
            {
                string itemCode = gv.GetListSourceRowCellValue(e.ListSourceRowIndex, "ProductItemcode").GetNullToEmpty();
                if (itemCode == null) return;

                var tN_STD1100 = ModelService.GetChildList<TN_STD1100>(x => x.ItemCode == itemCode).FirstOrDefault();

                if (tN_STD1100 != null)
                {
                    if (e.Column.Name == "ProductItemcodeItemNm1")
                        e.DisplayText = tN_STD1100.ItemNm1;
                    else
                        e.DisplayText = tN_STD1100.ItemNm;
                }
            }
        }

        private void DetailMainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            if (e.Column.Name == "Temp2")
            {
                TN_PUR1501 obji = DetailGridBindingSource.Current as TN_PUR1501;
                string outf = DbRequesHandler.GetCellValue("exec SP_STOPINOUT '"+ obji.Temp2 + "'", 0);
                if (outf == "Y")
                {
                    MessageBox.Show("이전 LOT가 있습니다. 선입선출 확인하세요");
                  
                }
              
                    VI_PURSTOCK_LOT stock = ModelService.GetChildList<VI_PURSTOCK_LOT>(p => p.Temp2 == obji.Temp2).OrderBy(o => o.ItemCode).FirstOrDefault();
                    if (stock == null) { MessageBox.Show("정보가 없습니다.");
                        obji.ItemCode = null;
                        obji.Temp2 = null;
                    }
                    else
                    {
                        obji.ItemCode = stock.ItemCode;
                    }
               

            }

                if (e.Column.Name == "OutQty")
            {
                TN_PUR1501 obj = DetailGridBindingSource.Current as TN_PUR1501;
                if (obj.Temp2 == null)
                {
                    obj.OutQty = 0;
                    MessageBox.Show("입고 LOT는 필수입력값입니다.");
                }
                else
                {
                    VI_PURSTOCK_LOT stock = ModelService.GetChildList<VI_PURSTOCK_LOT>(p => p.ItemCode == obj.ItemCode && p.Temp2 == obj.Temp2).OrderBy(o => o.ItemCode).FirstOrDefault();
                    if (stock == null)
                    {
                        MessageBox.Show("해당 품고에 대한 입고LOT " + obj.Temp2 + "정보가 없습니다.");
                        obj.Temp2 = null;
                        obj.OutQty = null;
                    }
                    else
                    {
                        if (stock.StockQty < obj.OutQty)
                        {
                            MessageBox.Show("재고량이 부족하여 출고할수 없습니다.");

                            obj.OutQty = stock.StockQty <= 0 ? 0 : stock.StockQty;

                        }
                        else
                        {
                            obj.WhCode = stock.WhCode;
                            obj.WhPosition = stock.WhPosition;
                        }
                    }

                }
            }

            if (e.Column.Name == "WhCode")
            {
                TN_PUR1501 dtlobj = DetailGridBindingSource.Current as TN_PUR1501;
                DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == dtlobj.WhCode).ToList(), "PosionCode", "PosionName");
                DetailGridExControl.MainGrid.BestFitColumns();
            }
        }

        protected override void InitCombo()
        {
            lupUser.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("OutNo", "출고번호");
            MasterGridExControl.MainGrid.AddColumn("OutDate", "출고일");            
            MasterGridExControl.MainGrid.AddColumn("ProductItemcode", "생산제품");
            MasterGridExControl.MainGrid.AddColumn("ProductItemcodeItemNm1", "생산제품번");
            MasterGridExControl.MainGrid.AddColumn("ProductItemcodeItemNm", "생산제품명");
            MasterGridExControl.MainGrid.AddColumn("OutId", "출고자");
            MasterGridExControl.MainGrid.AddColumn("Memo");
            

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutDate", "ProductItemcode", "OutId", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택");
            DetailGridExControl.MainGrid.AddColumn("OutNo", false);
            DetailGridExControl.MainGrid.AddColumn("OutSeq", "순번");
            DetailGridExControl.MainGrid.AddColumn("Temp1", "출고Lot");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            DetailGridExControl.MainGrid.AddColumn("Temp2", "입고Lot");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
            //DetailGridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, FormatType.Numeric, "n2");
            DetailGridExControl.MainGrid.AddColumn("WhCode", "창고");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "위치코드");
            DetailGridExControl.MainGrid.AddColumn("Memo");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "ItemCode","Temp2", "OutQty", "Memo","WhPosition");

        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<UserView>(p=>p.Active=="Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProductItemcode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemCode");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProductItemcodeItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProductItemcodeItemNm", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");

            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
           // DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y"&&(p.TopCategory=="P03"|| p.TopCategory == "P02" || p.TopCategory == "P07" || p.TopCategory == "P06")).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
        }
        protected override void DataLoad()
        {
            ModelService.ReLoad();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            string luser = lupUser.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutDate >= dp_date.DateFrEdit.DateTime.Date && p.OutDate <= dp_date.DateToEdit.DateTime.Date)
                                                                       && (string.IsNullOrEmpty(luser) ? true : p.OutId == luser)).OrderBy(o => o.OutDate).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
        }
        protected override void MasterFocusedRowChanged()
        {
            TN_PUR1500 obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null) return;
            DetailGridBindingSource.DataSource = obj.PUR1501List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }
        protected override void AddRowClicked()
        {
            TN_PUR1500 newobj = new TN_PUR1500()
            {
                OutNo = DbRequesHandler.GetRequestNumber("OUT"),
                OutDate = DateTime.Today
            };
            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
        }
        protected override void DetailAddRowClicked()
        {
            TN_PUR1500 obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null) return;
            int seq = (obj.PUR1501List.Count == 0) ? 1 : obj.PUR1501List.OrderBy(o => o.OutSeq).LastOrDefault().OutSeq + 1;
            TN_PUR1501 newobj = new TN_PUR1501()
            {
                OutNo = obj.OutNo,
                OutSeq = seq,
                Temp1=obj.OutNo+ seq.ToString()
                //obj.PUR1200List.Count + 1
            };
            DetailGridBindingSource.Add(newobj);
            obj.PUR1501List.Add(newobj);
        }
        protected override void DeleteRow()
        {
            TN_PUR1500 obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null) return;
            if (obj.PUR1501List.Count >= 1) {
                MessageBox.Show("상세내역이 있어 삭제할수 없습니다");

            }
            else
            {
             
                    MasterGridBindingSource.RemoveCurrent();
                    ModelService.Delete(obj);
              
            }
        }
        protected override void DeleteDetailRow()
        {
            TN_PUR1500 obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null) return;
         
                TN_PUR1501 dtlobj = DetailGridBindingSource.Current as TN_PUR1501;
                DetailGridBindingSource.RemoveCurrent();
                obj.PUR1501List.Remove(dtlobj);
              
           
            
        }
        protected override void DataSave()
        {

            foreach (var rowHandle in gridEx2.MainGrid.MainView.GetSelectedRows())
            {
                string _inlot = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp2").GetNullToEmpty());
                string _outqty = gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "OutQty").GetNullToEmpty();
                

                if (_inlot == null || _inlot == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("출고등록" + Convert.ToInt32(rowHandle + 1) + "행의 입고LOt는 필수입력 사항입니다.");
                    return;
                }

                if (_outqty == null || _outqty == ""||_outqty=="0")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("출고등록 " + Convert.ToInt32(rowHandle + 1) + "행의 출고수량은 필수입력 사항입니다.");
                    return;
                }

             
            }
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            ModelService.Save();
            DataLoad();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var rowHandle in gridEx2.MainGrid.MainView.GetSelectedRows())
                {
                   
                    string _inlot = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp2").GetNullToEmpty());
                    string _outqty = gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "OutQty").GetNullToEmpty();


                    if (_inlot == null || _inlot == "")
                    {
                        HKInc.Service.Handler.MessageBoxHandler.Show("출고등록" + Convert.ToInt32(rowHandle + 1) + "행의 입고LOt는 필수입력 사항입니다.");
                        return;
                    }

                    if (_outqty == null || _outqty == "" || _outqty == "0")
                    {
                        HKInc.Service.Handler.MessageBoxHandler.Show("출고등록 " + Convert.ToInt32(rowHandle + 1) + "행의 출고수량은 필수입력 사항입니다.");
                        return;
                    }


                }
                MasterGridExControl.MainGrid.PostEditor();
                MasterGridBindingSource.EndEdit();
                DetailGridExControl.MainGrid.PostEditor();
                DetailGridBindingSource.EndEdit();
                ModelService.Save();
                WaitHandler.ShowWait();
                if (DetailGridBindingSource == null) return;
                var PrintList = DetailGridBindingSource.List as List<TN_PUR1501>;
                if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0) return;
                ModelService.Save();
                var FirstReport = new REPORT.RINPUTLABLE();
                foreach (var v in PrintList.Where(p => p._Check == "Y").OrderByDescending(p => p.CreateTime).ToList())
                {

                    var report = new REPORT.RINPUTLABLE(v);
                    report.CreateDocument();
                    FirstReport.Pages.AddRange(report.Pages);

                    v._Check = "N";
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
