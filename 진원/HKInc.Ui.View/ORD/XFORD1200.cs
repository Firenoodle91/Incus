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
using HKInc.Ui.View.REPORT;
using HKInc.Ui.View.View.REPORT;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.ORD
{
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
          
            // 20220111 오세완 차장 원래 생략처리 되어 있었는데 디테일 수량 대비 마스터 수량을 조절하기 위해 풀어줌
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
          
        }

       
        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string lot = gv.GetFocusedRowCellValue("LotNo").ToString();
            int outqty = gv.GetFocusedRowCellValue("OutQty").GetIntNullToZero();

            if (e.Column.Name.ToString() == "OutQty")
            {
               VI_PRODQTYMSTLOT sqty = ModelService.GetChildList<VI_PRODQTYMSTLOT>(p => p.LotNo == lot).OrderBy(o => o.LotNo).FirstOrDefault();
               if (outqty > sqty.Stockqty)
                {

                    MessageBox.Show("재고량보다 많은 양은 출고등록할수 없습니다.");
                    gv.SetFocusedRowCellValue("OutQty", sqty.Stockqty);
                   
               //     DataLoad();
                    return;

                }
            }
            masterreview();
        }

        private void masterreview()
        {
           
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj.ORD1201List.Count() >= 1)
            {
                obj.OutDate = obj.ORD1201List.OrderByDescending(p => p.OutDate).FirstOrDefault().OutDate;
                obj.OutQty = obj.ORD1201List.Sum(s => s.OutQty).GetDecimalNullToZero();
            }
            else {
                obj.OutDate = null;
                obj.OutQty = 0;
            }
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

            MasterGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "OutNo", true);
            MasterGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            MasterGridExControl.MainGrid.AddColumn("OutNo", "출고번호");
            MasterGridExControl.MainGrid.AddColumn("DelivSeq", "납품계획번호");
            MasterGridExControl.MainGrid.AddColumn("OrderNo", "수주번호");
            MasterGridExControl.MainGrid.AddColumn("OrderSeq", "순번");
            MasterGridExControl.MainGrid.AddColumn("CustCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Lctype", "기종");
          //  MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Temp5", "팀");
            MasterGridExControl.MainGrid.AddColumn("OrderQty", "출고지시수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OutDate", "출고일");
            MasterGridExControl.MainGrid.AddColumn("OutId", "출고자");
            MasterGridExControl.MainGrid.AddColumn("OutState", "상태");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "OutId", "OutState", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "Seq", true);
            DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            DetailGridExControl.MainGrid.AddColumn("OutNo", false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            DetailGridExControl.MainGrid.AddColumn("TN_ORD1200.TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_ORD1200.TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("LotNo", "LOTNO");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutDate", "출고일");            
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "LotNo", "OutQty","OutDate", "Memo");
            DetailGridExControl.MainGrid.MainView.Columns["OutQty"].RealColumnEdit.EditValueChanged += RealColumnEdit_EditValueChanged;

            var barButtonTradingStatePrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonTradingStatePrint.Id = 4;
            barButtonTradingStatePrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/bosale");
            //barButtonDevide.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonAdd.ImageOptions.LargeImage")));
            barButtonTradingStatePrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.O));
            barButtonTradingStatePrint.Name = "barButtonTradingStatePrint";
            barButtonTradingStatePrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonTradingStatePrint.ShortcutKeyDisplayString = "Alt+O";
            barButtonTradingStatePrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonTradingStatePrint.Caption = LabelConvert.GetLabelText("TradingStatePrint") + "[Alt+O]";
            barButtonTradingStatePrint.Alignment = BarItemLinkAlignment.Right;
            barButtonTradingStatePrint.ItemClick += BarButtonTradingStatePrint_ItemClick;
            MasterGridExControl.BarTools.AddItem(barButtonTradingStatePrint);

            var barButtonLabelPrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonLabelPrint.Id = 5;
            barButtonLabelPrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/bosale");
            barButtonLabelPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.O));
            barButtonLabelPrint.Name = "barButtonLabelPrint";
            barButtonLabelPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonLabelPrint.ShortcutKeyDisplayString = "Alt+O";
            barButtonLabelPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonLabelPrint.Caption = LabelConvert.GetLabelText("LabelPrint") + "[Alt+O]";
            barButtonLabelPrint.Alignment = BarItemLinkAlignment.Right;
            //barButtonLabelPrint.ItemClick += BarButtonLabelPrint_ItemClick;
            barButtonLabelPrint.ItemClick += BarButtonLabelPrint_ItemClick2;
            DetailGridExControl.BarTools.AddItem(barButtonLabelPrint);

            var barButtonLabelPrint3 = new DevExpress.XtraBars.BarButtonItem();
            barButtonLabelPrint3.Id = 6;
            barButtonLabelPrint3.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/bosale");
            barButtonLabelPrint3.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barButtonLabelPrint3.Name = "barButtonLabelPrint";
            barButtonLabelPrint3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonLabelPrint3.ShortcutKeyDisplayString = "Alt+P";
            barButtonLabelPrint3.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            //barButtonLabelPrint3.Caption = LabelConvert.GetLabelText("LabelPrint") + "[Alt+P]";
            barButtonLabelPrint3.Caption = "출하라벨출력[Alt+P]";
            barButtonLabelPrint3.Alignment = BarItemLinkAlignment.Right;
            barButtonLabelPrint3.ItemClick += BarButtonLabelPrint_ItemClick3;
            DetailGridExControl.BarTools.AddItem(barButtonLabelPrint3);
        }

        private void RealColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
        //    GridView gv = sender as GridView;
            
            string lot = DetailGridExControl.MainGrid.MainView.GetFocusedRowCellValue("LotNo").ToString();
            int outqty = DetailGridExControl.MainGrid.MainView.ActiveEditor.EditValue.GetIntNullToZero();//DetailGridExControl.MainGrid.MainView.GetFocusedRowCellValue("OutQty").GetIntNullToZero();

          
                VI_PRODQTYMSTLOT sqty = ModelService.GetChildList<VI_PRODQTYMSTLOT>(p => p.LotNo == lot).OrderBy(o => o.LotNo).FirstOrDefault();
                if (outqty > sqty.Stockqty)
                {

                    MessageBox.Show("재고량보다 많은 양은 출고등록할수 없습니다.");
                DetailGridExControl.MainGrid.MainView.SetFocusedRowCellValue("OutQty", DetailGridExControl.MainGrid.MainView.ActiveEditor.OldEditValue );

               // Console.WriteLine(gridView1.ActiveEditor.OldEditValue.ToString());
                //     DataLoad();
                return;

                }
           
            masterreview();
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Temp5", DbRequesHandler.GetCommCode(MasterCodeSTR.tem), "Mcode", "Codename");

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
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
            // 20220111 오세완 차장 그리드 포커스 기능 누락된 것 같아서 추가
            GridRowLocator.GetCurrentRow("OutNo");
            MasterGridExControl.MainGrid.Clear();

            ModelService.ReLoad();
            DataSet ds = DbRequesHandler.GetDataQury("exec SP_OUTFLAG_UPDATE");
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

                TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
                if (obj == null) return;

                DetailGridBindingSource.DataSource = obj.ORD1201List.OrderBy(o => o.Seq).ToList();
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
            TN_ORD1200 tn = MasterGridBindingSource.Current as TN_ORD1200;
            if (tn.ORD1201List.Count > 0)
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
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);
            param.SetValue(PopupParameter.Value_1,obj.ItemCode);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTORD1201, param, AddDtlList);

            form.ShowPopup(true);





        }
        private void AddDtlList(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;
            List<VI_PRODQTYMSTLOT> partList = (List<VI_PRODQTYMSTLOT>)e.Map.GetValue(PopupParameter.ReturnObject);
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;
            foreach (var returnedPart in partList)
            {
                TN_ORD1201 newobj = new TN_ORD1201()
                {
                    OutNo = obj.OutNo,
                    Seq = obj.ORD1201List.Count == 0 ? 1 : obj.ORD1201List.OrderBy(o => o.Seq).LastOrDefault().Seq + 1,
                    ItemCode = obj.ItemCode,
                    LotNo = returnedPart.LotNo,
                    OutDate = DateTime.Today,
                    OutQty= returnedPart.Stockqty
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
                newobj.OutNo = DbRequesHandler.GetRequestNumber("JW");
                newobj.DelivSeq = returnedPart.DelivSeq;
                newobj.OrderNo = returnedPart.OrderNo;
                newobj.OrderSeq = Convert.ToInt32(returnedPart.Seq);
                newobj.CustCode = returnedPart.Temp;
                newobj.ItemCode = returnedPart.ItemCode;
                newobj.OutDate = DateTime.Today;
                newobj.OrderQty = returnedPart.DelivQty;
              
                newobj.OutId = returnedPart.DelivId;
                newobj.Memo = returnedPart.Memo;
              
                MasterGridBindingSource.Add(newobj);
                ModelService.Insert(newobj);
            }

            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            TN_ORD1201 delobj = DetailGridBindingSource.Current as TN_ORD1201;
            obj.ORD1201List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            masterreview();
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
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
                        ItemNm1 = DbRequesHandler.GetCellValue("SELECT ITEM_NM1 FROM TN_STD1100T WHERE ITEM_CODE='" + v.ItemCode + "'", 0),
                        PrtDate = v.OutDate.ToString().Substring(0, 10),
                        Qty = v.OutQty,
                        LotNo = v.OutNo,
                        CustLotNo = obj.Memo
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

        /// <summary>
        /// 라벨출력
        /// </summary>
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1200, param, LabelPrintCallback1);
            form.ShowPopup(true);
        }

        private void BarButtonLabelPrint_ItemClick3(object sender, ItemClickEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1200;
            if (masterObj == null) return;
            var detailList = DetailGridBindingSource.List as List<TN_ORD1201>;
            if (detailList == null) return;

            var checkList = detailList.Where(x => x._Check == "Y").ToList();
            if (checkList.Count > 0)
            {
                PopupDataParam param = new PopupDataParam();
                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1200, param, LabelPrintCallback1);
                form.ShowPopup(true);
            }
        }


        private void LabelPrintCallback1(object sender, PopupArgument e)
        {
            if (e == null) return;

            string sKeyValue = e.Map.GetValue(PopupParameter.KeyValue).GetNullToEmpty();
            if (sKeyValue != "")
            {
                if (sKeyValue == "Cancel")
                    return;
            }

            var BoxInQty = e.Map.GetValue(PopupParameter.Value_1).GetDecimalNullToZero();

            try
            {
                WaitHandler.ShowWait();
                TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
                if (obj == null) return;

                var detailList = DetailGridBindingSource.List as List<TN_ORD1201>;
                if (detailList == null) return;

                var printList = detailList.Where(x => x._Check == "Y").ToList();
                if (printList.Count == 0) return;

                var FirstReport = new REPORT.ROUTLABLE();

                foreach (var s in printList)
                {
                    int cnt = (int)(s.OutQty / BoxInQty) + ((s.OutQty % BoxInQty) > 0 ? 1 : 0);
                    for (int i = 0; i < cnt; i++)
                    {
                        PRT_OUTLABLE prt = new PRT_OUTLABLE()
                        {
                            ItemCode = s.ItemCode,
                            ItemNm = DbRequesHandler.GetCellValue("SELECT ITEM_NM FROM TN_STD1100T WHERE ITEM_CODE='" + s.ItemCode + "'", 0),
                            ItemNm1 = DbRequesHandler.GetCellValue("SELECT ITEM_NM1 FROM TN_STD1100T WHERE ITEM_CODE='" + s.ItemCode + "'", 0),
                            PrtDate = s.OutDate.ToString().Substring(0, 10),
                            //LotNo = s.OutNo,
                            LotNo = s.LotNo,
                            CustLotNo = obj.Memo
                        };
                        if (i == cnt - 1)
                        {
                            if ((s.OutQty % BoxInQty) == 0)
                                prt.Qty = BoxInQty;
                            else
                                prt.Qty = s.OutQty % BoxInQty;
                        }
                        else
                        {
                            prt.Qty = BoxInQty;
                        }

                        var report = new REPORT.ROUTLABLE(prt);
                        report.CreateDocument();
                        FirstReport.Pages.AddRange(report.Pages);
                    }
                }

                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.ShowPreview();//.Print();
                DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }

        private void BarButtonTradingStatePrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MasterGridBindingSource == null || MasterGridBindingSource.DataSource == null) return;
            var masterList = MasterGridBindingSource.List as List<TN_ORD1200>;
            //var checkList = masterList.Where(p => p._Check == "Y" && p.SumOutQty > 0).ToList();
            var checkList = masterList.Where(x => x._Check == "Y").ToList();
            var costDisplayFlag = true;

            if (checkList.Count > 0)
            {
                if (checkList.GroupBy(p => p.CustCode).Count() != 1)
                {
                    MessageBoxHandler.Show("거래명세서는 같은 거래처로 출력이 가능합니다. 같은 거래처를 선택해 주세요.", "경고");
                    return;
                }
            }
            else
            {
                return;
            }

            var result = MessageBoxHandler.Show("거래명세서에 단가를 입력하시겠습니까?", "경고", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                costDisplayFlag = true;
            else
                costDisplayFlag = false;

            try
            {
                WaitHandler.ShowWait();
                var groupList = new List<TradingStateGroupModel>();

                //XRORD1200_TRADING
                foreach (var v in checkList)
                {
                    // 20220111 오세완 차장 없는 경우 /를 출력하지 않게 처리
                    //string combineSpec = string.Format("{0}/{1}/{2}/{3}", v.TN_STD1100.Spec1, v.TN_STD1100.Spec2, v.TN_STD1100.Spec3, v.TN_STD1100.Spec4);
                    string sSpec = "";
                    if (v.TN_STD1100 != null)
                    {
                        if (v.TN_STD1100.Spec1.GetNullToEmpty() != "")
                            sSpec += v.TN_STD1100.Spec1;

                        if (v.TN_STD1100.Spec2.GetNullToEmpty() != "")
                            sSpec += "/" + v.TN_STD1100.Spec2;

                        if (v.TN_STD1100.Spec3.GetNullToEmpty() != "")
                            sSpec += "/" + v.TN_STD1100.Spec3;

                        if (v.TN_STD1100.Spec4.GetNullToEmpty() != "")
                            sSpec += "/" + v.TN_STD1100.Spec4;
                    }

                    string combineSpec = sSpec;
                    decimal ordCost = ModelService.GetChildList<TN_ORD1002>(x => x.ItemCode == v.TN_STD1100.ItemCode && x.OrderNo == v.OrderNo && x.Seq == v.OrderSeq).Select(s => s.Cost).FirstOrDefault();
                    groupList.AddRange(v.ORD1201List.GroupBy(p => new { p.ItemCode, p.TN_ORD1200.TN_STD1100.ItemNm1, p.TN_ORD1200.TN_STD1100.ItemNm, combineSpec, p.OutQty }).Select(p => new TradingStateGroupModel
                    {
                        ItemCode = p.Key.ItemCode,
                        ItemName = p.Key.ItemNm1,
                        ItemName2 = p.Key.ItemNm,
                        Spec = p.Key.combineSpec,
                        Qty = p.Key.OutQty.GetDecimalNullToZero(),
                        //Cost = costDisplayFlag ? (v.TN_ORD1100.TN_ORD1001.OrderCost == null ? v.TN_STD1100.Cost.GetDecimalNullToZero() : v.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero()) : 0,
                        Cost = costDisplayFlag ? ordCost : 0,
                        Count = p.Count()
                    }));
                }

                var distinctList = groupList.GroupBy(p => new { p.ItemCode, p.ItemName, p.ItemName2, p.Spec, p.Qty, p.Cost }).Select(p => new TradingStateGroupModel
                {
                    ItemCode = p.Key.ItemCode,
                    ItemName = p.Key.ItemName,
                    ItemName2 = p.Key.ItemName2,
                    Spec = p.Key.Spec,
                    Qty = p.Key.Qty.GetDecimalNullToZero(),
                    Cost = p.Key.Cost,
                    Count = p.Sum(c => c.Count)
                });

                var _Date = checkList.Max(p => p.OutDate);
                var printList = new List<TEMP_TRADING_DETAIL>();

                foreach (var v in distinctList)
                {
                    var newObj = new TEMP_TRADING_DETAIL()
                    {
                        Date = ((DateTime)_Date).ToString("MM.dd"),
                        ItemCode = v.ItemCode,
                        ItemName = v.ItemName,
                        ItemName2 = v.ItemName2,
                        Spec = v.Spec,
                        Qty = v.Qty * v.Count,
                        Cost = v.Cost,
                    };
                    newObj.SupplyCost = newObj.Qty * newObj.Cost;
                    newObj.TaxCost = (newObj.Qty * newObj.Cost) / 10;

                    printList.Add(newObj);
                }

                if (printList.Count > 0)
                {
                    var inCustomerCode = checkList.First().CustCode;
                    var inCustomer = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == inCustomerCode).FirstOrDefault();
                    var ourCustomer = ModelService.GetChildList<TN_STD1400>(x => x.DefaultCompanyPlag == "Y").FirstOrDefault();
                    var report = new XRORD1200_TRADING(((DateTime)_Date).ToShortDateString(), ourCustomer, inCustomer, printList);
                    report.CreateDocument();
                    report.PrintingSystem.ShowMarginsWarning = false;
                    report.ShowPrintStatusDialog = false;
                    report.ShowPreview();
                }
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex.Message);
            }
            finally
            {
                WaitHandler.CloseWait();
            }

            //if (checkList.Count > 0)
            //{
            //    if (checkList.GroupBy(p => p.CustomerCode).Count() != 1)
            //    {
            //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_94));
            //        //MessageBoxHandler.Show("거래명세서는 같은 거래처로 출력이 가능합니다. 같은 거래처를 선택해 주세요.", "경고");
            //        return;
            //    }
            //    var result = MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_95), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo);
            //    //var result = MessageBoxHandler.Show("거래명세서에 단가를 입력하시겠습니까?", "경고", MessageBoxButtons.YesNo);
            //    if (result == DialogResult.Yes) costDisplayFlag = true;
            //    else costDisplayFlag = false;

            //    try
            //    {
            //        WaitHandler.ShowWait();
            //        var groupList = new List<TradingStateGroupModel>();
            //        var cultureIndex = DataConvert.GetCultureIndex();

            //        if (cultureIndex == 1)
            //        {
            //            foreach (var v in checkList)
            //            {
            //                groupList.AddRange(v.TN_ORD1201List.GroupBy(p => new { p.ItemCode, p.TN_ORD1200.TN_STD1100.ItemName, p.TN_STD1100.CombineSpec, p.OutQty }).Select(p => new TradingStateGroupModel
            //                {
            //                    ItemCode = p.Key.ItemCode,
            //                    ItemName = p.Key.ItemName,
            //                    Spec = p.Key.CombineSpec,
            //                    Qty = p.Key.OutQty.GetDecimalNullToZero(),
            //                    Cost = costDisplayFlag ? (v.TN_ORD1100.TN_ORD1001.OrderCost == null ? v.TN_STD1100.Cost.GetDecimalNullToZero() : v.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero()) : 0,
            //                    Count = p.Count()
            //                }));
            //            }
            //        }
            //        else if (cultureIndex == 2)
            //        {
            //            foreach (var v in checkList)
            //            {
            //                groupList.AddRange(v.TN_ORD1201List.GroupBy(p => new { p.ItemCode, p.TN_ORD1200.TN_STD1100.ItemNameENG, p.TN_STD1100.CombineSpec, p.OutQty }).Select(p => new TradingStateGroupModel
            //                {
            //                    ItemCode = p.Key.ItemCode,
            //                    ItemName = p.Key.ItemNameENG,
            //                    Spec = p.Key.CombineSpec,
            //                    Qty = p.Key.OutQty.GetDecimalNullToZero(),
            //                    Cost = costDisplayFlag ? (v.TN_ORD1100.TN_ORD1001.OrderCost == null ? v.TN_STD1100.Cost.GetDecimalNullToZero() : v.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero()) : 0,
            //                    Count = p.Count()
            //                }));
            //            }
            //        }
            //        else
            //        {
            //            foreach (var v in checkList)
            //            {
            //                groupList.AddRange(v.TN_ORD1201List.GroupBy(p => new { p.ItemCode, p.TN_ORD1200.TN_STD1100.ItemNameCHN, p.TN_STD1100.CombineSpec, p.OutQty }).Select(p => new TradingStateGroupModel
            //                {
            //                    ItemCode = p.Key.ItemCode,
            //                    ItemName = p.Key.ItemNameCHN,
            //                    Spec = p.Key.CombineSpec,
            //                    Qty = p.Key.OutQty.GetDecimalNullToZero(),
            //                    Cost = costDisplayFlag ? (v.TN_ORD1100.TN_ORD1001.OrderCost == null ? v.TN_STD1100.Cost.GetDecimalNullToZero() : v.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero()) : 0,
            //                    Count = p.Count()
            //                }));
            //            }
            //        }

            //        var distinctList = groupList.GroupBy(p => new { p.ItemCode, p.ItemName, p.Spec, p.Qty, p.Cost }).Select(p => new TradingStateGroupModel
            //        {
            //            ItemCode = p.Key.ItemCode,
            //            ItemName = p.Key.ItemName,
            //            Spec = p.Key.Spec,
            //            Qty = p.Key.Qty.GetDecimalNullToZero(),
            //            Cost = p.Key.Cost,
            //            Count = p.Sum(c => c.Count)
            //        });

            //        var _Date = checkList.Max(p => p.OutDate);
            //        var printList = new List<TEMP_TRADING_DETAIL>();
            //        foreach (var v in distinctList)
            //        {
            //            var newObj = new TEMP_TRADING_DETAIL()
            //            {
            //                Date = ((DateTime)_Date).ToString("MM.dd"),
            //                ItemCode = v.ItemCode,
            //                ItemName = v.ItemName,
            //                Spec = v.Spec,
            //                //Spec = string.Format("({0}*{1}BOX)", v.Qty, v.Count),
            //                Qty = v.Qty * v.Count,
            //                Cost = v.Cost,
            //            };
            //            //newObj.Cost = costDisplayFlag ? checkList.Where(p => p.ItemCode == v.ItemCode).First().TN_STD1100.Cost.GetDecimalNullToZero() : 0
            //            newObj.SupplyCost = newObj.Qty * newObj.Cost;
            //            newObj.TaxCost = (newObj.Qty * newObj.Cost) / 10;

            //            printList.Add(newObj);
            //        }
            //        if (printList.Count > 0)
            //        {
            //            var inCustomerCode = checkList.First().CustomerCode;
            //            var inCustomer = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == inCustomerCode).FirstOrDefault();
            //            var ourCustomer = ModelService.GetChildList<TN_STD1400>(p => p.MyCompanyFlag == "Y").FirstOrDefault();
            //            var report = new XRORD1200_TRADING(((DateTime)_Date).ToShortDateString(), ourCustomer, inCustomer, printList);
            //            report.CreateDocument();
            //            report.PrintingSystem.ShowMarginsWarning = false;
            //            report.ShowPrintStatusDialog = false;
            //            report.ShowPreview();
            //        }
            //    }
            //    catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            //    finally
            //    {
            //        WaitHandler.CloseWait();
            //    }
            //}
        }

        private void BarButtonLabelPrint_ItemClick2(object sender, ItemClickEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1200;
            if (masterObj == null) return;
            var detailList = DetailGridBindingSource.List as List<TN_ORD1201>;
            if (detailList == null) return;

            var checkList = detailList.Where(x => x._Check == "Y").ToList();
            if (checkList.Count > 0)
            {
                PopupDataParam param = new PopupDataParam();
                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1200, param, LabelPrintCallback2);
                form.ShowPopup(true);
            }
        }

        private void LabelPrintCallback2(object sender, PopupArgument e)
        {
            if (e == null) return;

            string sKeyValue = e.Map.GetValue(PopupParameter.KeyValue).GetNullToEmpty();
            if (sKeyValue != "")
            {
                if (sKeyValue == "Cancel")
                    return;
            }

            var masterObj = MasterGridBindingSource.Current as TN_ORD1200;
            var detailList = DetailGridBindingSource.List as List<TN_ORD1201>;

            var BoxInQty = e.Map.GetValue(PopupParameter.Value_1).GetIntNullToZero();
            

            var printList = detailList.Where(x => x._Check == "Y").ToList();
            if (printList.Count <= 0) return;

            try
            {
                WaitHandler.ShowWait();

                if (masterObj.CustCode == "CUST-0001")
                {
                    PrintLabel_Daeyoung(printList, BoxInQty);
                }
                else if (masterObj.CustCode == "CUST-0003")
                {
                    PrintLabel_DYS(printList, BoxInQty);
                }
                else
                {
                    PrintLabel_Nomal(printList, BoxInQty);
                }

            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex.Message);
            }
            finally
            {
                WaitHandler.CloseWait();
            }
        }

        private void PrintLabel_Daeyoung(List<TN_ORD1201> list, int boxQty)
        {
            var mainReport = new XRORD1201_DAEYOUNG();

            foreach (var s in list)
            {
                int cnt = (int)(s.OutQty / boxQty) + ((s.OutQty % boxQty) > 0 ? 1 : 0);
                for (int i = 0; i < cnt; i++)
                {
                    //TN_ORD1201 newOrd = s;
                    TN_ORD1201 newOrd = new TN_ORD1201();
                    newOrd.OutQty = s.OutQty;
                    newOrd.TN_ORD1200 = s.TN_ORD1200;
                    newOrd.LotNo = s.LotNo;

                    if (i == cnt - 1)
                    {
                        if ((s.OutQty % boxQty) == 0)
                            newOrd.OutQty = boxQty;
                        else
                            newOrd.OutQty = s.OutQty % boxQty;
                    }
                        
                    else
                        newOrd.OutQty = boxQty;

                    var report = new XRORD1201_DAEYOUNG(newOrd);
                    report.CreateDocument();
                    mainReport.Pages.AddRange(report.Pages);
                }
            }

            mainReport.PrintingSystem.ShowMarginsWarning = false;
            mainReport.ShowPrintStatusDialog = false;
            mainReport.ShowPreview();
        }

        private void PrintLabel_DYS(List<TN_ORD1201> list, int boxQty)
        {
            var mainReport = new XRORD1201_DYS();

            foreach (var s in list)
            {
                int cnt = (int)(s.OutQty / boxQty) + ((s.OutQty % boxQty) > 0 ? 1 : 0);
                for (int i = 0; i < cnt; i++)
                {
                    //TN_ORD1201 newOrd = s;
                    TN_ORD1201 newOrd = new TN_ORD1201();
                    newOrd.OutQty = s.OutQty;
                    newOrd.TN_ORD1200 = s.TN_ORD1200;
                    newOrd.LotNo = s.LotNo;
                    newOrd.Memo = s.Memo;
                    newOrd.OutDate = s.OutDate;

                    if (i == cnt - 1)
                    {
                        if ((s.OutQty % boxQty) == 0)
                            newOrd.OutQty = boxQty;
                        else
                            newOrd.OutQty = s.OutQty % boxQty;
                    }
                    else
                        newOrd.OutQty = boxQty;

                    var report = new XRORD1201_DYS(newOrd);
                    report.CreateDocument();
                    mainReport.Pages.AddRange(report.Pages);
                }
            }

            mainReport.PrintingSystem.ShowMarginsWarning = false;
            mainReport.ShowPrintStatusDialog = false;
            mainReport.ShowPreview();
        }

        private void PrintLabel_Nomal(List<TN_ORD1201> list, int boxQty)
        {
            var mainReport = new XRORD1201_NOMAL();

            foreach (var s in list)
            {
                int cnt = (int)(s.OutQty / boxQty) + ((s.OutQty % boxQty) > 0 ? 1 : 0);
                for (int i = 0; i < cnt; i++)
                {
                    //TN_ORD1201 newOrd = s;
                    TN_ORD1201 newOrd = new TN_ORD1201();
                    newOrd.OutQty = s.OutQty;
                    newOrd.TN_ORD1200 = s.TN_ORD1200;
                    newOrd.LotNo = s.LotNo;
                    newOrd.Memo = s.Memo;
                    newOrd.OutDate = s.OutDate;

                    if (i == cnt - 1)
                    {
                        if ((s.OutQty % boxQty) == 0)
                            newOrd.OutQty = boxQty;
                        else
                            newOrd.OutQty = s.OutQty % boxQty;
                    }
                    else
                        newOrd.OutQty = boxQty;

                    var report = new XRORD1201_NOMAL(newOrd);
                    report.CreateDocument();
                    mainReport.Pages.AddRange(report.Pages);
                }
            }

            mainReport.PrintingSystem.ShowMarginsWarning = false;
            mainReport.ShowPrintStatusDialog = false;
            mainReport.ShowPreview();
        }


        #region 사용안함
        private void BarButtonLabelPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MasterGridBindingSource == null || MasterGridBindingSource.DataSource == null) return;
            //var masterList = MasterGridBindingSource.List as List<TN_ORD1200>;

            var masterObj = MasterGridBindingSource.Current as TN_ORD1200;
            if (masterObj == null) return;

            var checkList = masterObj.ORD1201List;

            if (checkList.Count > 0)
            {
                try
                {
                    WaitHandler.ShowWait();

                    if (masterObj.CustCode == "CUST-0001")
                    {
                        PrintLabel_Daeyoung(checkList); //대영정밀
                    }
                    else if (masterObj.CustCode == "CUST-0003")
                    {
                        PrintLabel_DYS(checkList); //디와이솔루텍
                    }
                    else
                    {
                        PrintLabel_Nomal(checkList); //나머지 업체들
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxHandler.ErrorShow(ex.Message);
                }
                finally
                {
                    WaitHandler.CloseWait();
                }


            }
            else
            {
                return;
            }

        }
        private void PrintLabel_Daeyoung(ICollection<TN_ORD1201> printList)
        {
            var mainReport = new XRORD1201_DAEYOUNG();

            foreach (var tN_ORD1201 in printList)
            {
                var report = new XRORD1201_DAEYOUNG(tN_ORD1201);
                report.CreateDocument();
                mainReport.Pages.AddRange(report.Pages);
            }

            mainReport.PrintingSystem.ShowMarginsWarning = false;
            mainReport.ShowPrintStatusDialog = false;
            mainReport.ShowPreview();
        }

        private void PrintLabel_DYS(ICollection<TN_ORD1201> printList)
        {
            var mainReport = new XRORD1201_DYS();

            foreach (var tN_ORD1201 in printList)
            {
                var report = new XRORD1201_DYS(tN_ORD1201);
                report.CreateDocument();
                mainReport.Pages.AddRange(report.Pages);
            }

            mainReport.PrintingSystem.ShowMarginsWarning = false;
            mainReport.ShowPrintStatusDialog = false;
            mainReport.ShowPreview();
        }

        private void PrintLabel_Nomal(ICollection<TN_ORD1201> printList)
        {
            var mainReport = new XRORD1201_NOMAL();

            foreach (var tN_ORD1201 in printList)
            {
                var report = new XRORD1201_NOMAL(tN_ORD1201);
                report.CreateDocument();
                mainReport.Pages.AddRange(report.Pages);
            }

            mainReport.PrintingSystem.ShowMarginsWarning = false;
            mainReport.ShowPrintStatusDialog = false;
            mainReport.ShowPreview();
        }
        #endregion

        private class TradingStateGroupModel
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string ItemName2 { get; set; }
            public string Spec { get; set; }
            public decimal Qty { get; set; }
            public decimal Cost { get; set; }
            public decimal Count { get; set; }
        }

        
    }
}
