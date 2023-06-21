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
    /// <summary>
    /// 자재출고관리화면
    /// </summary>
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
            MasterGridExControl.MainGrid.MainView.CellValueChanged += MasterMainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "StockQty")
            {
                var ItemCode = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ItemCode").GetNullToEmpty();
                var Temp2 = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "Temp2").GetNullToEmpty();
                VI_PURSTOCK_LOT stock = ModelService.GetChildList<VI_PURSTOCK_LOT>(p => p.ItemCode == ItemCode && p.Temp2 == Temp2).OrderBy(o => o.ItemCode).FirstOrDefault();
                //DetailGridExControl.MainGrid.BestFitColumns();
                if(stock != null)
                {
                    e.DisplayText = stock.StockQty == null ? "0" : stock.StockQty.GetIntNullToZero().ToString("n0");
                }
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
            MasterGridExControl.MainGrid.AddColumn("ProductItemcode", "생산품번");
            //MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "생산품번");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "생산품명");
            MasterGridExControl.MainGrid.AddColumn("OutId", "출고자");
            MasterGridExControl.MainGrid.AddColumn("Memo");            
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutDate", "ProductItemcode", "OutId", "Memo");
            MasterGridExControl.MainGrid.SetHeaderColor(Color.Red, "ProductItemcode");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "현품표출력[Alt+R]", IconImageList.GetIconImage("print/printer"));
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택");
            DetailGridExControl.MainGrid.AddColumn("OutNo", false);
            DetailGridExControl.MainGrid.AddColumn("OutSeq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Temp1", "출고 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            //DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("StockQty", "재고수량");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고수량");
            DetailGridExControl.MainGrid.AddColumn("Lqty", "롤 수");
            DetailGridExControl.MainGrid.AddColumn("Temp2", "입고 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("WhCode", "입고창고");            
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "입고위치코드");
            DetailGridExControl.MainGrid.AddColumn("Memo");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "OutQty", "Memo", "Lqty"); //"WhCode", "WhPosition");
            DetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "OutQty");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<UserView>(p=>p.Active=="Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProductItemcode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1", false, true, ItemCode_Production_Ellipsis);
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y"&&(p.TopCategory=="P03"|| p.TopCategory == "P02")).OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OutQty");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("StockQty", DefaultBoolean.Default, "n0", true, false);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("Lqty", DefaultBoolean.Default, "n0", true);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("OutNo");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string luser = lupUser.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutDate >= dp_date.DateFrEdit.DateTime.Date && p.OutDate <= dp_date.DateToEdit.DateTime.Date)
                                                                       && (string.IsNullOrEmpty(luser) ? true : p.OutId == luser)).OrderBy(o => o.OutDate).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_PUR1500 obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.PUR1501List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            //DetailGridExControl.MainGrid.BestFitColumns();
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            ModelService.Save();
            DataLoad();
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
            if (obj.ProductItemcode.GetNullToEmpty().IsNullOrEmpty())
            {
                MessageBoxHandler.Show("생산품목코드를 입력해 주시기 바랍니다.", "경고");
                return;
            }
            PopupDataParam param = new PopupDataParam();
            IPopupForm form;
            param.SetValue(PopupParameter.IsMultiSelect, false);
            form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFPUR1400, param, AddDetailRowCallBack);
            form.ShowPopup(true);
        }

        private void AddDetailRowCallBack(object sender, Utils.Common.PopupArgument e)
        {
            TN_PUR1500 obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null) return;

            if (e != null)
            {
                PopupDataParam param = e.Map;
                if (param.Keys.Contains(PopupParameter.ReturnObject))
                {
                    var ProductItemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == obj.ProductItemcode).First();
                    var returnObj = param.GetValue(PopupParameter.ReturnObject) as VI_PURSTOCK_LOT;
                    if (returnObj != null)
                    {
                        var returnItemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == returnObj.ItemCode).First();
                        if (returnItemObj.Spec1.GetNullToEmpty().ToLower() != ProductItemObj.Spec1.GetNullToEmpty().ToLower())
                        {
                            MessageBoxHandler.Show("생산품목과 출고할 품목의 원재료가 다릅니다.", "경고");
                            return;
                        }
                        else
                        {
                            int seq = (obj.PUR1501List.Count == 0) ? 1 : obj.PUR1501List.OrderBy(o => o.OutSeq).LastOrDefault().OutSeq + 1;
                            TN_PUR1501 newobj = new TN_PUR1501()
                            {
                                OutNo = obj.OutNo,
                                OutSeq = seq,
                                Temp1 = obj.OutNo + seq.ToString(),
                                ItemCode = returnObj.ItemCode,
                                Temp2 = returnObj.Temp2,
                                TN_STD1100 = returnItemObj,
                                WhCode = returnObj.WhCode,
                                WhPosition = returnObj.WhPosition,
                            };

                            var ItemCode = newobj.ItemCode.GetNullToEmpty();
                            var Temp2 = newobj.Temp2.GetNullToEmpty();
                            VI_PURSTOCK_LOT stock = ModelService.GetChildList<VI_PURSTOCK_LOT>(p => p.ItemCode == ItemCode && p.Temp2 == Temp2).OrderBy(o => o.ItemCode).FirstOrDefault();
                            //DetailGridExControl.MainGrid.BestFitColumns();
                            if (stock != null)
                            {
                                newobj.OutQty = stock.StockQty == null ? 0 : stock.StockQty.GetIntNullToZero();
                            }

                            DetailGridBindingSource.Add(newobj);
                            obj.PUR1501List.Add(newobj);
                            DetailGridExControl.BestFitColumns();
                        }
                    }
                }
            }
        }

        protected override void DeleteRow()
        {
            TN_PUR1500 obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null) return;
            if (obj.PUR1501List.Count >= 1)
            {
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
            TN_PUR1501 dtlobj = DetailGridBindingSource.Current as TN_PUR1501;
            if (obj == null || dtlobj == null) return;
         
            DetailGridBindingSource.RemoveCurrent();
            obj.PUR1501List.Remove(dtlobj);
        }

        protected override void DetailFileChooseClicked()
        {
            try
            {
                MasterGridExControl.MainGrid.PostEditor();
                MasterGridBindingSource.EndEdit();
                DetailGridExControl.MainGrid.PostEditor();
                DetailGridBindingSource.EndEdit();

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

                ModelService.Save();

                WaitHandler.ShowWait();
                if (DetailGridBindingSource == null) return;
                var PrintList = DetailGridBindingSource.List as List<TN_PUR1501>;
                if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0) return;

                var FirstReport = new REPORT.RINPUTLABLE();
                foreach (var v in PrintList.Where(p => p._Check == "Y").OrderByDescending(p => p.CreateTime).ToList())
                {
                    //var report = new REPORT.RINPUTLABLE(v);
                    //report.CreateDocument();
                    //FirstReport.Pages.AddRange(report.Pages);

                    //v._Check = "N";
                    int Lqty = v.Lqty.GetIntNullToZero();
                    int iold = v.OutQty;
                    int i = v.OutQty / (Lqty == 0 ? 1 : Lqty);
                    v.OutQty = i;
                    for (int j = 0; j < (Lqty == 0 ? 1 : Lqty); j++)
                    {
                        var report = new REPORT.RINPUTLABLE(v);
                        report.CreateDocument();
                        FirstReport.Pages.AddRange(report.Pages);
                    }
                    v._Check = "N";
                    v.OutQty = iold;
                }
                FirstReport.PrintingSystem.ShowMarginsWarning = false;
                FirstReport.ShowPrintStatusDialog = false;
                //FirstReport.ShowPreview();
                FirstReport.Print();
                DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }

        private void DetailMainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            if (e.Column.FieldName == "Temp2")
            {
                //TN_PUR1501 obji = DetailGridBindingSource.Current as TN_PUR1501;
                //if (obji.Temp2.GetNullToEmpty().IsNullOrEmpty()) return;
                //string outf = DbRequesHandler.GetCellValue("exec SP_STOPINOUT '" + obji.Temp2 + "'", 0);
                //if (outf == "Y")
                //{
                //    MessageBox.Show("이전 LOT가 있습니다. 선입선출 확인하세요");
                //    obji.Temp2 = null;
                //}
                //else
                //{
                //    VI_PURSTOCK_LOT stock = ModelService.GetChildList<VI_PURSTOCK_LOT>(p => p.Temp2 == obji.Temp2).OrderBy(o => o.ItemCode).FirstOrDefault();
                //    if (stock == null)
                //    {
                //        MessageBox.Show("정보가 없습니다.");
                //        obji.ItemCode = null;
                //        obji.Temp2 = null;
                //    }
                //    else
                //    {
                //        obji.ItemCode = stock.ItemCode;
                //    }
                //}
                //DetailGridExControl.MainGrid.BestFitColumns();
            }
            else if (e.Column.FieldName == "OutQty")
            {
                TN_PUR1501 obj = DetailGridBindingSource.Current as TN_PUR1501;
                VI_PURSTOCK_LOT stock = ModelService.GetChildList<VI_PURSTOCK_LOT>(p => p.ItemCode == obj.ItemCode && p.Temp2 == obj.Temp2).OrderBy(o => o.ItemCode).FirstOrDefault();
                if (stock.StockQty < obj.OutQty)
                {
                    MessageBox.Show("출고수량이 재고수량보다 많습니다. 재고수량 : " + stock.StockQty.ToString());
                    DetailGridExControl.MainGrid.MainView.CellValueChanged -= DetailMainView_CellValueChanged;
                    obj.OutQty = stock.StockQty.GetIntNullToZero();
                    DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellValueChanged;
                }
                DetailGridExControl.MainGrid.BestFitColumns();
            }
            else if (e.Column.FieldName == "WhCode")
            {
                //TN_PUR1501 dtlobj = DetailGridBindingSource.Current as TN_PUR1501;
                //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == dtlobj.WhCode).ToList(), "PosionCode", "PosionName");
                //DetailGridExControl.MainGrid.BestFitColumns();
            }
            else if (e.Column.FieldName == "_Check")
                SetIsFormControlChanged(false);
        }

        private void MasterMainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            
            if (e.Column.FieldName == "ProductItemcode")
            {
                var MasterObj = MasterGridBindingSource.Current as TN_PUR1500;
                if (MasterObj == null) return;
                MasterObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == MasterObj.ProductItemcode).First();
                MasterGridExControl.MainGrid.MainView.RefreshData();
                MasterGridExControl.BestFitColumns();
            }
        }

        private void ItemCode_Production_Ellipsis(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var edit = sender as SearchLookUpEdit;
            if (edit == null) return;

            var MasterObj = MasterGridBindingSource.Current as TN_PUR1500;
            if (MasterObj == null) return;

            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                PopupDataParam param = new PopupDataParam();
                IPopupForm form;
                param.SetValue(PopupParameter.IsMultiSelect, false);
                param.SetValue(PopupParameter.Constraint, MasterCodeSTR.ItemCode_Production);
                form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTSTD1100, param, SetDetailGridView_ItemCode);
                form.ShowPopup(true);
            }
        }

        private void SetDetailGridView_ItemCode(object sender, Utils.Common.PopupArgument e)
        {
            var MasterObj = MasterGridBindingSource.Current as TN_PUR1500;
            if (MasterObj == null) return;

            if (e != null)
            {
                PopupDataParam param = e.Map;
                if (param.Keys.Contains(PopupParameter.ReturnObject))
                {
                    var obj = param.GetValue(PopupParameter.ReturnObject) as TN_STD1100;
                    if (obj != null)
                    {
                        MasterGridExControl.MainGrid.MainView.SetFocusedRowCellValue("ProductItemcode", obj.ItemCode);
                    }
                }
            }
        }
    }
   
}
