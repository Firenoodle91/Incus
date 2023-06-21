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
using HKInc.Service.Handler;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.PUR
{
    /// <summary>
    /// 자재입고관리화면
    /// </summary>
    public partial class XFPUR1300 :  HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        private event DevExpress.XtraBars.ItemClickEventHandler DetailLotNoDevide;
        IService<TN_PUR1300> ModelService = (IService<TN_PUR1300>)ProductionFactory.GetDomainService("TN_PUR1300");

        public XFPUR1300()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-10);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(+10);
            MasterGridExControl.MainGrid.MainView.ShowingEditor += MasterGridView_ShowingEditor;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            IsMasterGridButtonFileChooseEnabled = true;    
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "발주내역참조[F10]");

            MasterGridExControl.MainGrid.AddColumn("InputNo", "입고번호");
            MasterGridExControl.MainGrid.AddColumn("InputDate", "입고일");
            MasterGridExControl.MainGrid.AddColumn("InputId", "입고자");
            MasterGridExControl.MainGrid.AddColumn("ReqNo", "발주번호");
            MasterGridExControl.MainGrid.AddColumn("ReqDate", "발주일");
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.AddColumn("Temp1", "입고완료");

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InputDate", "InputId", "CustomerCode", "Memo", "Temp1");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            //IsDetailGridButtonExportEnabled = true;
            //DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.Export, "현품표출력[Alt+E]", IconImageList.GetIconImage("print/printer"));
            IsDetailGridButtonFileChooseEnabled = true;            
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "발주내역참조[Alt+R]");       
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택");
            DetailGridExControl.MainGrid.AddColumn("InputNo", false);
            DetailGridExControl.MainGrid.AddColumn("InputSeq", "입고순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품번");
            //DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("ReqNo", "발주번호");
            DetailGridExControl.MainGrid.AddColumn("ReqSeq", "발주순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ReqQty", "발주수량");
            DetailGridExControl.MainGrid.AddColumn("Cost","발주단가");
            DetailGridExControl.MainGrid.AddColumn("ReqAmt","발주금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InputQty","입고수량");
            DetailGridExControl.MainGrid.AddColumn("InCost","입고단가");
            DetailGridExControl.MainGrid.AddColumn("InputAmt","입고금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Lqty", "롤 수");
            DetailGridExControl.MainGrid.AddColumn("InYn",false);
            DetailGridExControl.MainGrid.AddColumn("Temp2", "입고 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("WhCode", "입고창고");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "입고위치");
            DetailGridExControl.MainGrid.AddColumn("Memo");
            //DetailGridExControl.MainGrid.AddColumn("inqcf");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "ItemCode", "InputQty", "InCost", "Memo", "WhCode", "Lqty", "WhPosition");
            DetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "ItemCode", "InputQty");


            var barButtonDevide = new DevExpress.XtraBars.BarButtonItem();
            barButtonDevide.Id = 4;
            barButtonDevide.ImageOptions.Image = IconImageList.GetIconImage("spreadsheet/showdetail");
            //barButtonDevide.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonAdd.ImageOptions.LargeImage")));
            barButtonDevide.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.T));
            barButtonDevide.Name = "barButtonDevide";
            barButtonDevide.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonDevide.ShortcutKeyDisplayString = "Alt+T";
            barButtonDevide.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonDevide.Caption = "분할처리[Alt+T]";
            barButtonDevide.ItemClick += BarButtonDevide_ItemClick;
            DetailGridExControl.BarTools.AddItem(barButtonDevide);

            var barButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonPrint.Id = 5;
            barButtonPrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            //barButtonDevide.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonAdd.ImageOptions.LargeImage")));
            barButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.Y));
            barButtonPrint.Name = "barButtonPrint";
            barButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonPrint.ShortcutKeyDisplayString = "Alt+Y";
            barButtonPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonPrint.Caption = "현품표출력[Alt+Y]";
            barButtonPrint.ItemClick += BarButtonPrint_ItemClick;
            DetailGridExControl.BarTools.AddItem(barButtonPrint);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InputId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InputDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("ReqDate");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp1", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("ReqQty", DefaultBoolean.Default, "n0", true, false);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("Cost", DefaultBoolean.Default, "n0", true, false);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InputQty", DefaultBoolean.Default, "n0", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InCost", DefaultBoolean.Default, "n0", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("Lqty", DefaultBoolean.Default, "n0", true);
            //DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P03"||p.TopCategory=="P02")).OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemCode", false, true, ItemCode_NotProduction_Ellipsis);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P03"||p.TopCategory=="P02")).OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1", false, true, ItemCode_NotProduction_Ellipsis);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("InYn", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
        }

        protected override void InitCombo()
        {
            lupcust.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());            
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InputNo");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string cust = lupcust.EditValue.GetNullToEmpty();
            string inputNo = tx_ReqNo.Text.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(cust) ? true : p.CustomerCode == cust)
                                                                        && (string.IsNullOrEmpty(inputNo) ? true : p.InputNo == inputNo)
                                                                        && (p.InputDate >= dp_date.DateFrEdit.DateTime.Date 
                                                                            && p.InputDate <= dp_date.DateToEdit.DateTime.Date)
                                                                      )
                                                                      .OrderBy(o => o.InputDate)
                                                                      .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.PUR1301List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            TN_PUR1300 newobj = new TN_PUR1300()
            {
                InputNo = DbRequesHandler.GetRequestNumber("IN"),
                InputDate = DateTime.Today,
                Temp1 = "N"
            };
            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);            
        }

        protected override void DeleteRow()
        {
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj == null) return;
          
            if (obj.PUR1301List.Count>= 1)
            {
                MessageBox.Show("상세내역이 있어 삭제할수 없습니다.");
            }
            else
            {
                if (obj.Temp1 == "Y")
                {
                    MessageBox.Show("입고완료건은 삭제할수 없습니다.");
                }
                else
                {
                    MasterGridBindingSource.RemoveCurrent();
                    ModelService.Delete(obj);
                }
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj == null) return;
            if (obj.Temp1 == "Y")
            {
                MessageBox.Show("입고완료건은 변경할수 없습니다.");
            }
            else
            {
                TN_PUR1301 newobj = new TN_PUR1301()
                {
                    InputNo=obj.InputNo,
                    InputSeq= obj.PUR1301List.Count == 0? 1: obj.PUR1301List.Count + 1,
                    ReqNo=obj.ReqNo,
                    Temp2= obj.InputNo.ToString()+ (obj.PUR1301List.Count == 0 ? 1 : obj.PUR1301List.Count + 1).ToString()
                   

                };
                DetailGridBindingSource.Add(newobj);
                obj.PUR1301List.Add(newobj);
            }
        }

        protected override void DeleteDetailRow()
        {
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj == null) return;
            if (obj.Temp1 == "Y")
            {
                MessageBox.Show("입고완료건은 삭제할수 없습니다.");
            }
            else
            {
                TN_PUR1301 dtlobj = DetailGridBindingSource.Current as TN_PUR1301;
                DetailGridBindingSource.RemoveCurrent();
                obj.PUR1301List.Remove(dtlobj);
            }
        }

        protected override void FileChooseClicked()
        {
            if (!UserRight.HasEdit) return;

            if (!IsFirstLoaded) ActRefresh();
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");    
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTPUR1100, param, AddPur1300);
            form.ShowPopup(true);
        }

        private void AddPur1300(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            List<TN_PUR1100> partList = (List<TN_PUR1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            if (e.Map.ContainsKey(PopupParameter.Value_1))
            {
                List<TN_PUR1200> detailList = (List<TN_PUR1200>)e.Map.GetValue(PopupParameter.Value_1);
                foreach (var returnedPart in partList)
                {
                    if (ModelService.GetList(p => p.ReqNo == returnedPart.ReqNo).Count == 0)
                    {
                        TN_PUR1300 obj = (TN_PUR1300)MasterGridBindingSource.AddNew();
                        obj.InputNo = DbRequesHandler.GetRequestNumber("IN");
                        obj.InputDate = DateTime.Today;
                        obj.CustomerCode = returnedPart.CustomerCode;
                        obj.DueDate = returnedPart.DueDate;
                        obj.ReqNo = returnedPart.ReqNo;
                        obj.ReqDate = returnedPart.ReqDate;

                        foreach (var c in detailList)
                        {
                            TN_PUR1301 obj2 = (TN_PUR1301)DetailGridBindingSource.AddNew();
                            obj2.InputNo = obj.InputNo;
                            obj2.InputSeq = obj.PUR1301List.Count == 0 ? 1 : obj.PUR1301List.OrderBy(o => o.InputSeq).LastOrDefault().InputSeq + 1;
                            obj2.ItemCode = c.ItemCode;
                            obj2.ReqNo = c.ReqNo;
                            obj2.ReqSeq = c.ReqSeq;
                            obj2.ReqQty = c.ReqQty.ToString();
                            obj2.Cost = c.Temp1;
                            obj2.Memo = c.Memo;
                            obj2.Temp2 = obj.InputNo.ToString() + obj2.InputSeq.ToString();
                            obj2.InputQty = c.RemainReqQty.GetIntNullToZero();
                            obj2.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == c.ItemCode).First();
                            obj.PUR1301List.Add(obj2);
                        }
                        ModelService.Insert(obj);
                    }
                }
                DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            else
            {
                foreach (var returnedPart in partList)
                {
                    if (ModelService.GetList(p => p.ReqNo == returnedPart.ReqNo).Count == 0)
                    {
                        TN_PUR1300 obj = (TN_PUR1300)MasterGridBindingSource.AddNew();
                        obj.InputNo = DbRequesHandler.GetRequestNumber("IN");
                        obj.InputDate = DateTime.Today;
                        obj.CustomerCode = returnedPart.CustomerCode;
                        obj.DueDate = returnedPart.DueDate;
                        obj.ReqNo = returnedPart.ReqNo;
                        obj.ReqDate = returnedPart.ReqDate;
                        ModelService.Insert(obj);
                    }
                }
            }
            if (partList.Count > 0) SetIsFormControlChanged(true);
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DetailFileChooseClicked()
        {
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj.ReqNo.GetNullToEmpty() == "") return;
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");
            param.SetValue(PopupParameter.Value_1, obj.ReqNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTPUR1200, param, AddPur1301);
            form.ShowPopup(true);
        }

        private void AddPur1301(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            List<TN_PUR1200> partList = (List<TN_PUR1200>)e.Map.GetValue(PopupParameter.ReturnObject);
            TN_PUR1300 oldobj = MasterGridBindingSource.Current as TN_PUR1300;
            foreach (var returnedPart in partList)
            {
                if (oldobj.PUR1301List.Where(p => p.ItemCode == returnedPart.ItemCode).ToList().Count == 0)
                {
                    TN_PUR1301 obj = (TN_PUR1301)DetailGridBindingSource.AddNew();
                    obj.InputNo = oldobj.InputNo;
                    obj.InputSeq = oldobj.PUR1301List.Count == 0 ? 1 : oldobj.PUR1301List.OrderBy(o => o.InputSeq).LastOrDefault().InputSeq + 1;
                    obj.ItemCode = returnedPart.ItemCode;
                    obj.ReqNo = returnedPart.ReqNo;
                    obj.ReqSeq = returnedPart.ReqSeq;
                    obj.ReqQty = returnedPart.ReqQty.ToString();
                    obj.Cost = returnedPart.Temp1;                   
                    obj.Memo = returnedPart.Memo;
                    obj.Temp2 = oldobj.InputNo.ToString() + obj.InputSeq.ToString();
                    obj.InputQty = returnedPart.RemainReqQty.GetIntNullToZero();
                    obj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == returnedPart.ItemCode).First();
                    oldobj.PUR1301List.Add(obj);
                }
            }
            if (partList.Count > 0) SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void BarButtonDevide_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!UserRight.HasEdit) return;

            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            //if (obj.ReqNo.GetNullToEmpty() == "") return;
            if (obj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR1301;
            if (detailObj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, detailObj);            
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFPUR1301, param, AddDevide1301);
            form.ShowPopup(true);
        }

        private void AddDevide1301(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;
            TN_PUR1300 masterObj = MasterGridBindingSource.Current as TN_PUR1300;
            List<TN_PUR1301> returnList = (List<TN_PUR1301>)e.Map.GetValue(PopupParameter.ReturnObject);
            var updateObj = e.Map.GetValue(PopupParameter.Value_1) as TN_PUR1301;
            var realUpdateObj = masterObj.PUR1301List.Where(p => p.Temp2 == updateObj.Temp2).First();
            realUpdateObj.InputQty = updateObj.InputQty;
            realUpdateObj.InCost = updateObj.InCost;
            realUpdateObj.Lqty = updateObj.Lqty;
            realUpdateObj.WhCode = updateObj.WhCode;
            realUpdateObj.WhPosition = updateObj.WhPosition;
            realUpdateObj.Memo = updateObj.Memo;

            foreach (var returnObj in returnList.Where(p => p.Temp2.IsNullOrEmpty()).ToList())
            {
                TN_PUR1301 obj = (TN_PUR1301)DetailGridBindingSource.AddNew();
                obj.InputNo = masterObj.InputNo;
                obj.InputSeq = masterObj.PUR1301List.Count == 0 ? 1 : masterObj.PUR1301List.OrderBy(o => o.InputSeq).LastOrDefault().InputSeq + 1;
                obj.ItemCode = realUpdateObj.ItemCode;
                obj.ReqNo = realUpdateObj.ReqNo;
                obj.ReqSeq = realUpdateObj.ReqSeq;
                obj.ReqQty = realUpdateObj.ReqQty.GetNullToNull();
                obj.Cost = realUpdateObj.Cost;
                obj.Memo = returnObj.Memo;
                obj.Temp2 = masterObj.InputNo.ToString() + obj.InputSeq.ToString();
                obj.InputQty = returnObj.InputQty;
                obj.InCost = returnObj.InCost;
                obj.Lqty = returnObj.Lqty;
                obj.WhCode = returnObj.WhCode;
                obj.WhPosition = returnObj.WhPosition;
                obj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == realUpdateObj.ItemCode).First();
                masterObj.PUR1301List.Add(obj);
            }
            SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.MainView.RefreshData();
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void BarButtonPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ModelService.Save();
                WaitHandler.ShowWait();
                if (DetailGridBindingSource == null) return;
                var PrintList = DetailGridBindingSource.List as List<TN_PUR1301>;
                if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0) return;

                var FirstReport = new REPORT.RINPUTLABLE();
                foreach (var v in PrintList.Where(p => p._Check == "Y").OrderByDescending(p => p.CreateTime).ToList())
                {
                    int Lqty = v.Lqty.GetIntNullToZero();
                    int iold = v.InputQty;
                    int i = v.InputQty / (Lqty == 0 ? 1 : Lqty);
                    v.InputQty = i;
                    for (int j = 0; j < (Lqty == 0 ? 1 : Lqty); j++)
                    {
                        var report = new REPORT.RINPUTLABLE(v);
                        report.CreateDocument();
                        FirstReport.Pages.AddRange(report.Pages);
                    }
                    v._Check = "N";
                    v.InputQty = iold;
                }

                //FirstReport.ShowPrintStatusDialog = false;
                //FirstReport.ShowPreview();
                FirstReport.PrintingSystem.ShowMarginsWarning = false;
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.Print();
                DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            ModelService.Save();
         
            DataLoad();
        }                

        private void MasterGridView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var gv = sender as GridView;
            var MasterObj = MasterGridBindingSource.Current as TN_PUR1300;
            if (gv.FocusedColumn.FieldName == "CustomerCode")
            {
                if (!MasterObj.ReqNo.IsNullOrEmpty())
                {
                    e.Cancel = true;
                }
            }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView gv = sender as GridView;
            string aa = DbRequesHandler.GetCellValue("SELECT top 1 [no] FROM [TN_QCT1200T] where  temp1='" + gv.GetFocusedRowCellValue("Temp2").GetNullToEmpty() + "'", 0);
            if (aa != null)
            {
                e.Cancel = true;
            }
        }

        private void ItemCode_NotProduction_Ellipsis(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var edit = sender as SearchLookUpEdit; //var edit = sender as GridLookUpEdit;
            if (edit == null) return;

            var DetailObj = DetailGridBindingSource.Current as TN_PUR1301;
            if (DetailObj == null) return;

            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                PopupDataParam param = new PopupDataParam();
                IPopupForm form;
                param.SetValue(PopupParameter.IsMultiSelect, false);
                param.SetValue(PopupParameter.Constraint, MasterCodeSTR.ItemCode_NotProduction);
                form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTSTD1100, param, SetDetailGridView_ItemCode);
                form.ShowPopup(true);
            }
        }

        private void SetDetailGridView_ItemCode(object sender, Utils.Common.PopupArgument e)
        {
            var DetailObj = DetailGridBindingSource.Current as TN_PUR1301;
            if (DetailObj == null) return;

            if (e != null)
            {
                PopupDataParam param = e.Map;
                if (param.Keys.Contains(PopupParameter.ReturnObject))
                {
                    var obj = param.GetValue(PopupParameter.ReturnObject) as TN_STD1100;
                    if (obj != null)
                    {
                        DetailGridExControl.MainGrid.MainView.SetFocusedRowCellValue("ItemCode", obj.ItemCode);
                    }
                }
            }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = DetailGridExControl.MainGrid.MainView as GridView;
            if (e.Column.FieldName == "WhCode")
            {
                TN_PUR1301 dtlobj = DetailGridBindingSource.Current as TN_PUR1301;
                DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == dtlobj.WhCode).ToList(), "PosionCode", "PosionName");
            }
            else if (e.Column.FieldName == "WhPosition")
            {
                DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
            }
            else if (e.Column.FieldName == "_Check")
                SetIsFormControlChanged(false);
            else if(e.Column.FieldName == "ItemCode")
            {
                string itm = gv.GetFocusedRowCellValue(e.Column.FieldName).ToString();
                int rowid = e.RowHandle;
                int cnt = 0;
                for (int i = 0; i < gv.RowCount; i++)
                {
                    if (i != rowid)
                    {
                        if (itm == gv.GetRowCellValue(i, e.Column.FieldName).GetNullToEmpty())
                        {
                            cnt++;
                        }
                    }
                }
                if (cnt >= 1)
                {
                    TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
                    MessageBox.Show("이미 등록된 품목입니다.");
                    TN_PUR1301 delobj = DetailGridBindingSource.Current as TN_PUR1301;
                    if (delobj.InputQty >= 1) return;
                    DetailGridBindingSource.RemoveCurrent();
                    obj.PUR1301List.Remove(delobj);
                    ModelService.Update(obj);
                }
                else
                {
                    var DetailObj = DetailGridBindingSource.Current as TN_PUR1301;
                    DetailObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == itm).First();
                    DetailGridExControl.MainGrid.MainView.RefreshData();
                    DetailGridExControl.BestFitColumns();
                }
            }
        }
        
    }
}

