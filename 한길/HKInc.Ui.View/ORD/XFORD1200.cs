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
using HKInc.Ui.View.REPORT;

namespace HKInc.Ui.View.ORD
{
    /// <summary>
    /// 제품출고관리
    /// </summary>
    public partial class XFORD1200 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1200> ModelService = (IService<TN_ORD1200>)ProductionFactory.GetDomainService("TN_ORD1200");
       
        public XFORD1200()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dateOrderDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dateOrderDate.DateToEdit.DateTime = DateTime.Today;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        protected override void InitGrid()
        {
            IsMasterGridButtonFileChooseEnabled = true;
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "라벨출력[F10]", IconImageList.GetIconImage("print/printer"));

            MasterGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "OutNo", true);
            MasterGridExControl.MainGrid.AddColumn("_Check", "선택");
            MasterGridExControl.MainGrid.AddColumn("OutState", "상태");
            MasterGridExControl.MainGrid.AddColumn("OutNo", "출고번호");
            MasterGridExControl.MainGrid.AddColumn("DelivSeq", "납품계획번호");
            MasterGridExControl.MainGrid.AddColumn("OrderNo", "수주번호");
            MasterGridExControl.MainGrid.AddColumn("OrderSeq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("CustCode", "수주처");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명"); 
            MasterGridExControl.MainGrid.AddColumn("MasterOrderQty", "출고지시수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OrderQty", "출고잔량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn("ShipmentRemainQty", "출고잔량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DisplayOutQty", "출고수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OutDate", "출고일");
            MasterGridExControl.MainGrid.AddColumn("OutId", "출고자");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "OutId", "Memo");

            var barButtonTradingStatePrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonTradingStatePrint.Id = 4;
            barButtonTradingStatePrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/bosale");
            //barButtonDevide.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonAdd.ImageOptions.LargeImage")));
            barButtonTradingStatePrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.O));
            barButtonTradingStatePrint.Name = "barButtonTradingStatePrint";
            barButtonTradingStatePrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonTradingStatePrint.ShortcutKeyDisplayString = "Alt+O";
            barButtonTradingStatePrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonTradingStatePrint.Caption = "거래명세서 출력[Alt+O]";
            barButtonTradingStatePrint.ItemClick += BarButtonTradingStatePrint_ItemClick;
            MasterGridExControl.BarTools.AddItem(barButtonTradingStatePrint);

            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "포장 바코드 스캔[Alt+R]", IconImageList.GetIconImage("toolbox%20items/barcode2"));

            DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "Seq", true);
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택");
            DetailGridExControl.MainGrid.AddColumn("OutNo", false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "픔목코드", false);
            DetailGridExControl.MainGrid.AddColumn("LotNo", "생산 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("PackLotNo", "포장 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고량");
            DetailGridExControl.MainGrid.AddColumn("OutDate", "출고일");            
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutQty","OutDate", "Memo", "_Check");

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OutQty", DefaultBoolean.Default, "n0", true);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
        }

        protected override void InitCombo()
        {
            lupCustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("OutNo");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            DataSet ds = DbRequesHandler.GetDataQury("exec SP_OUTFLAG_UPDATE");
            string customerCode = lupCustcode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutDate >= dateOrderDate.DateFrEdit.DateTime &&
                                                                            p.OutDate <= dateOrderDate.DateToEdit.DateTime) &&
                                                                            (string.IsNullOrEmpty(customerCode) ? true : p.CustCode == customerCode))
                                                            .OrderBy(p => p.OutDate)
                                                            .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.ORD1201List.OrderBy(o => o.Seq)
                                                            .ThenBy(p => p.LotNo)
                                                            .ThenBy(p => p.PackLotNo.IsNullOrEmpty() ? 0 : p.PackLotNo.Length)
                                                            .ThenBy(p => p.PackLotNo)
                                                            .ToList();
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
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTORD1200, param, AddOrderList);
            form.ShowPopup(true);
        }

        protected override void DeleteRow()
        {
            if (!UserRight.HasEdit) return;

            TN_ORD1200 tn = MasterGridBindingSource.Current as TN_ORD1200;
            if (tn == null) return;

            if (tn.ORD1201List.Count > 0)
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
            if (!UserRight.HasEdit) return;

            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);
            param.SetValue(PopupParameter.Value_1,obj.ItemCode);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTORD1201, param, AddDtlList);
            form.ShowPopup(true);
        }

        protected override void DetailFileChooseClicked()
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            IPopupForm form;
            param.SetValue(PopupParameter.IsMultiSelect, false);
            form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFORD1200, param, AddDtlList);
            form.ShowPopup(true);
        }

        protected override void DeleteDetailRow()
        {
            if (!UserRight.HasEdit) return;

            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;

            var detailList = DetailGridBindingSource.List as List<TN_ORD1201>;
            if (detailList == null) return;
            var delList = detailList.Where(p => p._Check == "Y").ToList();
            foreach (var v in delList)
            {
                obj.ORD1201List.Remove(v);
                DetailGridBindingSource.Remove(v);
            }
            //TN_ORD1201 delobj = DetailGridBindingSource.Current as TN_ORD1201;
            //if (delobj == null) return;

            //obj.ORD1201List.Remove(delobj);
            //DetailGridBindingSource.Remove(delobj);
            DetailGridExControl.BestFitColumns();
            masterreview();
        }

        protected override void FileChooseClicked()
        {
            try
            {
                WaitHandler.ShowWait();
                TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
                if (obj == null) return;
                if (DetailGridBindingSource == null) return;

                var PrintList = DetailGridBindingSource.List as List<TN_ORD1201>;
                if (PrintList.Count == 0) return;

                var FirstReport = new REPORT.ROUTLABLE();
                foreach (var v in PrintList.OrderByDescending(p => p.CreateTime).ToList())
                {
                    PRT_OUTLABLE prt = new PRT_OUTLABLE()
                    {
                        ItemCode = v.ItemCode,
                        ItemNm = DbRequesHandler.GetCellValue("SELECT ITEM_NM FROM TN_STD1100T WHERE ITEM_CODE='" + v.ItemCode + "'", 0),
                        PrtDate = v.OutDate.ToString().Substring(0, 10),
                        Qty = v.OutQty,
                        LotNo = v.LotNo,
                        CustLotNo = obj.Memo
                    };
                    var report = new REPORT.ROUTLABLE(prt);
                    report.CreateDocument();
                    FirstReport.Pages.AddRange(report.Pages);
                }
                //FirstReport.ShowPrintStatusDialog = false;
                //FirstReport.ShowPreview();
                FirstReport.PrintingSystem.ShowMarginsWarning = false;
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.Print();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }

        //거래명세서출력
        private void BarButtonTradingStatePrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MasterGridBindingSource == null || MasterGridBindingSource.DataSource == null) return;
            var masterList = MasterGridBindingSource.List as List<TN_ORD1200>;
            var checkList = masterList.Where(p => p._Check == "Y" && p.DisplayOutQty > 0).ToList();
            var costDisplayFlag = true;

            if (checkList.Count > 0)
            {
                if (checkList.GroupBy(p => p.CustCode).Count() != 1)
                {
                    MessageBoxHandler.Show("거래명세서는 같은 거래처로 출력이 가능합니다. 같은 거래처를 선택해 주세요.", "경고");
                    return;
                }
                var result = MessageBoxHandler.Show("거래명세서에 단가를 입력하시겠습니까?", "경고", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) costDisplayFlag = true;
                else costDisplayFlag = false;

                try
                {
                    WaitHandler.ShowWait();
                    var groupList = new List<TradingStateGroupModel>();
                    foreach (var v in checkList)
                    {
                        groupList.AddRange(v.ORD1201List.GroupBy(p => new { p.ItemCode, p.TN_ORD1200.TN_STD1100.ItemNm, p.OutQty }).Select(p => new TradingStateGroupModel
                        {
                            ItemCode = p.Key.ItemCode,
                            ItemName = p.Key.ItemNm,
                            Qty = p.Key.OutQty.GetDecimalNullToZero(),
                            Count = p.Count()
                        }));
                    }

                    var distinctList = groupList.GroupBy(p => new { p.ItemCode, p.ItemName, p.Qty }).Select(p => new TradingStateGroupModel
                    {
                        ItemCode = p.Key.ItemCode,
                        ItemName = p.Key.ItemName,
                        Qty = p.Key.Qty.GetDecimalNullToZero(),
                        Count = p.Sum(c => c.Count)
                    });

                    var _Date = checkList.Max(p => p.OutDate);
                    var printList = new List<RORD1200_DETAIL>();
                    foreach (var v in distinctList)
                    {
                        var newObj = new RORD1200_DETAIL()
                        {
                            Date = ((DateTime)_Date).ToString("MM.dd"),
                            ItemCode = v.ItemCode,
                            ItemName = v.ItemName,
                            Spec = string.Format("({0}*{1})", v.Qty, v.Count),      // 2022-04-14 김진우       기존 ({0}*{1}BOX) 에서 요청사항으로 인한 수정
                            Qty = v.Qty * v.Count,
                            Cost = costDisplayFlag ? checkList.Where(p => p.ItemCode == v.ItemCode).First().TN_STD1100.ProcCnt.GetDecimalNullToZero() : 0
                        };
                        newObj.SupplyCost = newObj.Qty * newObj.Cost;
                        newObj.TaxCost = (newObj.Qty * newObj.Cost) / 10;

                        printList.Add(newObj);
                    }
                    if (printList.Count > 0)
                    {
                        var inCustomerCode = checkList.First().CustCode;
                        var inCustomer = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == inCustomerCode).FirstOrDefault();
                        var ourCustomer = ModelService.GetChildList<TN_STD1400>(p => p.DefaultCompanyPlag == "Y").FirstOrDefault();
                        var report = new RORD1200(((DateTime)_Date).ToShortDateString(), ourCustomer, inCustomer, printList);
                        report.CreateDocument();
                        report.PrintingSystem.ShowMarginsWarning = false;
                        report.ShowPrintStatusDialog = false;
                        report.ShowPreview();
                    }
                }
                catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
                finally
                {
                    WaitHandler.CloseWait();
                }
            }
        }

        private void AddDtlList(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;
            if (e == null) return;

            List<VI_ORD1200_DETAIL_ADD> partList = (List<VI_ORD1200_DETAIL_ADD>)e.Map.GetValue(PopupParameter.ReturnObject);
            foreach (var returnedPart in partList)
            {
                TN_ORD1201 newobj = new TN_ORD1201()
                {
                    OutNo = obj.OutNo,
                    Seq = obj.ORD1201List.Count == 0 ? 1 : obj.ORD1201List.OrderBy(o => o.Seq).LastOrDefault().Seq + 1,
                    ItemCode = obj.ItemCode,
                    LotNo = returnedPart.LotNo,
                    OutDate = DateTime.Today,
                    OutQty = returnedPart.Inqty,
                    PackLotNo = returnedPart.PackLotNo
                };
                DetailGridBindingSource.Add(newobj);
                obj.ORD1201List.Add(newobj);
            }
            masterreview();
        }

        private void AddOrderList(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            List<TN_ORD1100> partList = (List<TN_ORD1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var returnedPart in partList)
            {
                TN_ORD1200 newobj = new TN_ORD1200();
                newobj.OutNo = DbRequesHandler.GetRequestNumber("POUT");
                newobj.DelivSeq = returnedPart.DelivSeq;
                newobj.OrderNo = returnedPart.OrderNo;
                newobj.OrderSeq = Convert.ToInt32(returnedPart.Seq);
                newobj.CustCode = returnedPart.Temp;
                newobj.ItemCode = returnedPart.ItemCode;
                newobj.OutDate = DateTime.Today;
                newobj.OrderQty = ModelService.GetChildList<TN_ORD1100>(p=>p.DelivSeq == returnedPart.DelivSeq).First().ShipmentRemainQty;
                newobj.OutId = returnedPart.DelivId;
                newobj.Memo = returnedPart.Memo;
                newobj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == returnedPart.ItemCode).First();
                newobj.TN_ORD1100 = ModelService.GetChildList<TN_ORD1100>(p => p.OrderNo == returnedPart.OrderNo && p.Seq == returnedPart.Seq && p.DelivSeq == returnedPart.DelivSeq).First();
                MasterGridBindingSource.Add(newobj);
                ModelService.Insert(newobj);
            }
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            masterreview();
        }

        private void masterreview()
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj.ORD1201List.Count > 0)
            {
                obj.OutDate = obj.ORD1201List.OrderBy(p => p.OutDate).FirstOrDefault().OutDate;
                obj.OutQty = obj.ORD1201List.Sum(s => s.OutQty).GetDecimalNullToZero();
            }
            MasterGridExControl.MainGrid.BestFitColumns();
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
                if (obj == null) return;
                if (DetailGridBindingSource == null) return;

                var PrintList = DetailGridBindingSource.List as List<TN_ORD1201>;
                if (PrintList.Count == 0) return;

                var FirstReport = new REPORT.ROUTLABLE();
                foreach (var v in PrintList.OrderByDescending(p => p.CreateTime).ToList())
                {
                    PRT_OUTLABLE prt = new PRT_OUTLABLE()
                    {
                        ItemCode = v.ItemCode,
                        ItemNm = DbRequesHandler.GetCellValue("SELECT ITEM_NM FROM TN_STD1100T WHERE ITEM_CODE='" + v.ItemCode + "'", 0),
                        PrtDate = v.OutDate.ToString().Substring(0, 10),
                        Qty = v.OutQty,
                        LotNo = v.LotNo,
                        CustLotNo = obj.Memo
                    };
                    var report = new REPORT.ROUTLABLE(prt);
                    report.CreateDocument();
                    FirstReport.Pages.AddRange(report.Pages);
                }
                //FirstReport.ShowPrintStatusDialog = false;
                //FirstReport.ShowPreview();                
                FirstReport.PrintingSystem.ShowMarginsWarning = false;
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.Print();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }

        private class TradingStateGroupModel
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public decimal Qty { get; set; }
            public decimal Count { get; set; }
        }
    }
}
