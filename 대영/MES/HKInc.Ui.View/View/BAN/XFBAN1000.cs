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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.BAN
{
    /// <summary>
    /// 반제품입고관리
    /// </summary>
    public partial class XFBAN1000 : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_BAN1000> ModelService = (IService<TN_BAN1000>)ProductionFactory.GetDomainService("TN_BAN1000");

        public XFBAN1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            //MasterGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailMainView_ShowingEditor;

            dt_InDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dt_InDate.DateToEdit.DateTime = DateTime.Today;
        }

        protected override void InitCombo()
        {            
            lup_InId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y" && p.ScmYn == "N").ToList());
        }

        protected override void InitGrid()
        {
            IsMasterGridButtonFileChooseEnabled = UserRight.HasEdit;
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, UserRight.HasEdit);
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("Return") + "[F10]", IconImageList.GetIconImage("spreadsheet/showdetail"));

            MasterGridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"));
            MasterGridExControl.MainGrid.AddColumn("InDate", LabelConvert.GetLabelText("InDate"));
            MasterGridExControl.MainGrid.AddColumn("InId", LabelConvert.GetLabelText("InId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InDate", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "InLotNo", true);
            DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"), true);
            DetailGridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"), false);
            DetailGridExControl.MainGrid.AddColumn("InSeq", LabelConvert.GetLabelText("InSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"));
            DetailGridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            DetailGridExControl.MainGrid.AddColumn("BanProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            DetailGridExControl.MainGrid.AddColumn("PrintQty", LabelConvert.GetLabelText("PrintQty2"), false);
            DetailGridExControl.MainGrid.AddColumn("InWhCode", LabelConvert.GetLabelText("InWhCode"));
            DetailGridExControl.MainGrid.AddColumn("InWhPosition", LabelConvert.GetLabelText("InWhPosition"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "InQty", "InWhCode", "InWhPosition", "Memo", "PrintQty");

            var barTextEditBarCode = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCode.Id = 5;
            barTextEditBarCode.Enabled = UserRight.HasEdit;
            barTextEditBarCode.Name = "barTextEditBarCode";
            barTextEditBarCode.EditWidth = 130;
            barTextEditBarCode.Edit.KeyDown += Edit_KeyDown;
            //DetailGridExControl.BarTools.AddItem(barTextEditBarCode);

            var barTextEditBarCodeStaticItem = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCodeStaticItem.Id = 6;
            barTextEditBarCodeStaticItem.Name = "barTextEditBarCodeStaticItem";
            //barTextEditBarCodeStaticItem.Edit.NullText = LabelConvert.GetLabelText("ProductLotNo") + ":";
            // 20210627 오세완 차장 이동포번호로 인식하는 기능도 추가하였기에 text 추가처리
            barTextEditBarCodeStaticItem.Edit.NullText = LabelConvert.GetLabelText("ItemMoveNo") + " / " + LabelConvert.GetLabelText("ProductLotNo") + " :";
            barTextEditBarCodeStaticItem.EditWidth = barTextEditBarCodeStaticItem.Edit.NullText.Length * 9;
            barTextEditBarCodeStaticItem.EditWidth = 180;           // 2021-07-13 김진우 주임        뒤쪽 짤려서 추가
            barTextEditBarCodeStaticItem.Enabled = false;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.ForeColor = Color.Black;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Far;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.BackColor = Color.Transparent;
            barTextEditBarCodeStaticItem.Edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Alignment = BarItemLinkAlignment.Left;

            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCode);
            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCodeStaticItem);

            var barButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonPrint.Id = 7;
            barButtonPrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            barButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barButtonPrint.Name = "barButtonPrint";
            barButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonPrint.ShortcutKeyDisplayString = "Alt+P";
            barButtonPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonPrint.Caption = LabelConvert.GetLabelText("BarcodePrint") + "[Alt+P]";
            barButtonPrint.ItemClick += BarButtonPrint_ItemClick;
            barButtonPrint.Alignment = BarItemLinkAlignment.Right;
            barButtonPrint.Visibility = BarItemVisibility.Always;
            DetailGridExControl.BarTools.AddItem(barButtonPrint);

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1200>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1201>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("PrintQty");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InQty", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhCode", ModelService.GetChildList<TN_WMS1000>(p => (p.WhDivision == MasterCodeSTR.WhCodeDivision_BAN) && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
           
            var WhPositionEdit = DetailGridExControl.MainGrid.Columns["InWhPosition"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            WhPositionEdit.Popup += WhPositionEdit_Popup;

            var inQtyEdit = DetailGridExControl.MainGrid.Columns["InQty"].ColumnEdit as RepositoryItemSpinEdit;
            inQtyEdit.EditValueChanged += InQtyEdit_EditValueChanged;
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InputNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            //데이터리로드
            InitRepository();
            InitCombo();

            string inId = lup_InId.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.InDate >= dt_InDate.DateFrEdit.DateTime
                                                                         && p.InDate <= dt_InDate.DateToEdit.DateTime)
                                                                         && (string.IsNullOrEmpty(inId) ? true : p.InId == inId)
                                                                      )
                                                                      .OrderBy(o => o.InDate)
                                                                      .ThenBy(o => o.InNo)
                                                                      .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_BAN1000;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_BAN1001List.OrderBy(p => p.InSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            var newObj = (TN_BAN1000)MasterGridBindingSource.AddNew();
            newObj.InNo = DbRequestHandler.GetSeqMonth("BIN");
            newObj.InDate = DateTime.Today;
            newObj.InId = GlobalVariable.LoginId;
            ModelService.Insert(newObj);
            SetIsFormControlChanged(true);
            MasterGridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_BAN1000;
            if (masterObj == null) return;

            if (masterObj.TN_BAN1001List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("InMasterInfo"), LabelConvert.GetLabelText("InDetailInfo"), LabelConvert.GetLabelText("InDetailInfo")));
                return;
            }
            ModelService.Delete(masterObj);
            MasterGridBindingSource.RemoveCurrent();
        }

        /// <summary> 재입고 </summary>
        protected override void FileChooseClicked()
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFBAN1000, param, PopupRefreshCallback);
            form.ShowPopup(true);
        }

        private void Edit_KeyDown(object sender, KeyEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_BAN1000;
            if (masterObj == null) return;

            var detailList = DetailGridBindingSource.List as List<TN_BAN1001>;
            if (detailList == null) return;

            if (e.KeyCode == Keys.Enter)
            {
                var textEdit = sender as TextEdit;
                if (textEdit == null) return;

                string txt = textEdit.EditValue.GetNullToEmpty().ToUpper();
                if (txt.IsNullOrEmpty())
                {

                }
                else
                {
                    IService<TN_BAN1100> LocalService = (IService<TN_BAN1100>)ProductionFactory.GetDomainService("TN_BAN1100");

                    //이동표번호 스캔시
                    if (txt.Contains("IMV"))
                    {
                        GetItemMoveNo(txt, masterObj, detailList);
                    }
                    //LotNo 스캔시
                    else
                    {
                        GetProductionLotNo(txt, masterObj, detailList);
                    }
                }

                textEdit.EditValue = "";
                e.Handled = true;
            }
        }

        private void GetProductionLotNo(string productLotNo, TN_BAN1000 masterObj, List<TN_BAN1001> detailList)
        {
            IService<TN_BAN1100> LocalService = (IService<TN_BAN1100>)ProductionFactory.GetDomainService("TN_BAN1100");
            var VI_BAN_STOCK_PRODUCT_LOT_NO = LocalService.GetChildList<VI_BAN_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == productLotNo).FirstOrDefault();

            if (VI_BAN_STOCK_PRODUCT_LOT_NO == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("BanProductLotNo")));
            }
            else
            {
                if (VI_BAN_STOCK_PRODUCT_LOT_NO.StockQty <= 0)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("InPossibleQty")));

                }
                else
                {
                    //새로 추가한 것중 같은 LOT 인 리스트(저장안한것)
                    List<TN_BAN1001> productLotList = detailList.Where(x => x.BanProductLotNo == productLotNo && x.NewRowFlag == "Y").ToList();
                    decimal sumProductQty = productLotList.Sum(s => s.InQty);

                    if (VI_BAN_STOCK_PRODUCT_LOT_NO.StockQty <= sumProductQty)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("InPossibleQty")));
                        //return;
                    }
                    else
                    {
                        var newObj = new TN_BAN1001();
                        newObj.InNo = masterObj.InNo;
                        newObj.InSeq = masterObj.TN_BAN1001List.Count == 0 ? 1 : masterObj.TN_BAN1001List.Max(p => p.InSeq) + 1;
                        newObj.ItemCode = VI_BAN_STOCK_PRODUCT_LOT_NO.ItemCode;
                        //newObj.InQty = VI_BAN_STOCK_PRODUCT_LOT_NO.StockQty;
                        newObj.InQty = VI_BAN_STOCK_PRODUCT_LOT_NO.StockQty - sumProductQty;
                        newObj.BanProductLotNo = VI_BAN_STOCK_PRODUCT_LOT_NO.ProductLotNo;
                        newObj.InLotNo = masterObj.InNo.ToString().Substring(0, 9) + masterObj.InNo.ToString().Substring(10, 4) + newObj.InSeq.ToString().PadLeft(3, '0');
                        newObj.NewRowFlag = "Y";
                        newObj.PrintQty = 1;
                        newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == VI_BAN_STOCK_PRODUCT_LOT_NO.ItemCode).FirstOrDefault();
                        newObj.TN_BAN1000 = masterObj;
                        masterObj.TN_BAN1001List.Add(newObj);
                        DetailGridBindingSource.Add(newObj);
                        DetailGridExControl.BestFitColumns();
                    }
                }
            }

            LocalService.Dispose();
        }

        private void GetItemMoveNo(string itemMoveNo, TN_BAN1000 masterObj, List<TN_BAN1001> detailList)
        {
            IService<TN_BAN1100> LocalService = (IService<TN_BAN1100>)ProductionFactory.GetDomainService("TN_BAN1100");

            var VI_BAN_STOCK_ITEM_MOVE_NO = LocalService.GetChildList<VI_BAN_STOCK_ITEM_MOVE_NO>(x => x.ItemMoveNo == itemMoveNo).FirstOrDefault();
            if (VI_BAN_STOCK_ITEM_MOVE_NO == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("BanProductLotNo")));
            }
            else
            {
                var newObj = new TN_BAN1001();
                newObj.InNo = masterObj.InNo;
                newObj.InSeq = masterObj.TN_BAN1001List.Count == 0 ? 1 : masterObj.TN_BAN1001List.Max(p => p.InSeq) + 1;
                newObj.ItemCode = VI_BAN_STOCK_ITEM_MOVE_NO.ItemCode;
                newObj.InQty = VI_BAN_STOCK_ITEM_MOVE_NO.InQty;
                newObj.BanProductLotNo = VI_BAN_STOCK_ITEM_MOVE_NO.ProductLotNo;
                newObj.InLotNo = masterObj.InNo.ToString().Substring(0, 9) + masterObj.InNo.ToString().Substring(10, 4) + newObj.InSeq.ToString().PadLeft(3, '0');
                newObj.NewRowFlag = "Y";
                newObj.PrintQty = 1;
                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == VI_BAN_STOCK_ITEM_MOVE_NO.ItemCode).FirstOrDefault();
                newObj.TN_BAN1000 = masterObj;
                masterObj.TN_BAN1001List.Add(newObj);
                DetailGridBindingSource.Add(newObj);
                DetailGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_BAN1000;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_BAN1001;
            if (detailObj == null) return;

            if (detailObj.TN_BAN1101List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("InDetailInfo"), LabelConvert.GetLabelText("OutInfo"), LabelConvert.GetLabelText("OutInfo")));
                return;
            }

            DetailGridBindingSource.RemoveCurrent();
            masterObj.TN_BAN1001List.Remove(detailObj);
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
        }

        /// <summary> 현품표출력버튼 </summary>
        private void BarButtonPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (DetailGridBindingSource == null) return;

                MasterGridExControl.MainGrid.PostEditor();
                DetailGridExControl.MainGrid.PostEditor();

                WaitHandler.ShowWait();

                var detailList = DetailGridBindingSource.List as List<TN_BAN1001>;
                var printList = detailList.Where(p => p._Check == "Y").ToList();
                if (printList.Count == 0) return;

                if (printList.Any(p => p.TN_STD1100 == null))
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)(StandardMessageEnum.M_58)), "ItemInfo"));
                    return;
                }
                if (printList.Any(p => p.InLotNo.IsNullOrEmpty()))
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)(StandardMessageEnum.M_58)), "InLotNo"));
                    return;
                }

                if (printList.Any(x => x.NewRowFlag == "Y"))
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)(StandardMessageEnum.M_99))));
                    return;
                }

                var mainReport = new REPORT.XRBAN1001();
                foreach (var v in printList.OrderByDescending(p => p.InSeq).ToList())
                {
                    var printQty = v.PrintQty.GetIntNullToZero();
                    if (printQty <= 1)
                    {
                        var report = new REPORT.XRBAN1001(v);
                        report.CreateDocument();
                        mainReport.Pages.AddRange(report.Pages);
                    }
                    else
                    {
                        for (int i = 0; i < printQty; i++)
                        {
                            var report = new REPORT.XRBAN1001(v);
                            report.CreateDocument();
                            mainReport.Pages.AddRange(report.Pages);
                        }
                    }
                    v._Check = "N";
                }
                DetailGridExControl.BestFitColumns();
                mainReport.PrintingSystem.ShowMarginsWarning = false;
                mainReport.ShowPrintStatusDialog = false;
                mainReport.ShowPreview();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_BAN1000;
            if (masterObj == null) return;

            if (masterObj.TN_BAN1001List.Any(c => c.TN_BAN1101List.Count > 0))
            {
                e.Cancel = true;
            }
        }

        private void DetailMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            string fieldName = view.FocusedColumn.FieldName;

            var masterObj = MasterGridBindingSource.Current as TN_BAN1000;
            if (masterObj == null) return;
            var detailObj = DetailGridBindingSource.Current as TN_BAN1001;
            if (detailObj == null) return;

            if (fieldName != "_Check" && fieldName != "Memo")
            {
                if (detailObj.TN_BAN1101List.Count > 0)
                {
                    e.Cancel = true;
                }
            }
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_BAN1001;
            if (detailObj == null) return;

            if (e.Column.FieldName == "InWhCode")
            {
                detailObj.InWhPosition = null;
            }

            if (detailObj.NewRowFlag == "Y" && e.Column.FieldName == "InQty")
            {
                //재고수량, 새로추가한 입고수량 합
                var detailList = DetailGridBindingSource.List as List<TN_BAN1001>;
                var stockObj = ModelService.GetChildList<VI_BAN_STOCK_PRODUCT_LOT_NO>(x => x.ProductLotNo == detailObj.BanProductLotNo).FirstOrDefault();
                var productStock = detailList.Where(x => x.NewRowFlag == "Y" && x.BanProductLotNo == detailObj.BanProductLotNo).Sum(s => s.InQty);


                //if (stockObj.StockQty < detailObj.InQty)
                if (stockObj.StockQty - productStock < 0)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_153)));
                    detailObj.InQty = 0;
                }
                DetailGridExControl.BestFitColumns();
            }
        }

        private void InQtyEdit_EditValueChanged(object sender, EventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_BAN1000;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_BAN1001;
            if (detailObj == null) return;

            //var stockObj = ModelService.GetChildList<VI_BAN_STOCK_IN_LOT_NO>(p => p.InLotNo == detailObj.InLotNo).FirstOrDefault();
            var stockObj = ModelService.GetChildList<VI_BAN_STOCK_PRODUCT_LOT_NO>(x => x.ProductLotNo == detailObj.BanProductLotNo).FirstOrDefault();
            if (stockObj == null) return;

            if (detailObj.NewRowFlag != "Y")
            {
                var oldQty = DetailGridExControl.MainGrid.MainView.ActiveEditor.OldEditValue.GetIntNullToZero();
                var nowQty = DetailGridExControl.MainGrid.MainView.ActiveEditor.EditValue.GetIntNullToZero();

                decimal allowQty = (stockObj.StockQty + oldQty).GetIntNullToZero();

                if (allowQty < nowQty)
                {
                    try
                    {
                        var inQtyEdit = DetailGridExControl.MainGrid.Columns["InQty"].ColumnEdit as RepositoryItemSpinEdit;
                        inQtyEdit.EditValueChanged -= InQtyEdit_EditValueChanged;
                        DetailGridExControl.MainGrid.MainView.ActiveEditor.EditValue = oldQty;
                        DetailGridExControl.MainGrid.PostEditor();
                        inQtyEdit.EditValueChanged += InQtyEdit_EditValueChanged;

                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_153)));
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHandler.ErrorShow(ex);
                    }
                }
            }
        }

        private void WhPositionEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_BAN1001;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.InWhCode + "'";
        }

        #region 사용안함
        private void DetailAddRowPopupCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_PUR1200;
            if (masterObj == null) return;

            var returnList = (List<TN_PUR1101>)e.Map.GetValue(PopupParameter.ReturnObject);
            foreach (var v in returnList)
            {
                var poDetailObj = ModelService.GetChildList<TN_PUR1101>(p => p.PoNo == v.PoNo && p.PoSeq == v.PoSeq).FirstOrDefault();
                var newObj = new TN_PUR1201();
                newObj.InNo = masterObj.InNo;
                newObj.InSeq = masterObj.TN_PUR1201List.Count == 0 ? 1 : masterObj.TN_PUR1201List.Max(p => p.InSeq) + 1;
                newObj.PoNo = poDetailObj.PoNo;
                newObj.PoSeq = poDetailObj.PoSeq;
                newObj.ItemCode = poDetailObj.TN_STD1100.ItemCode;
                newObj.InQty = poDetailObj.PoRemainQty;
                newObj.InCost = v.PoCost;
                newObj.InLotNo = masterObj.InNo.ToString().Substring(0, 10) + newObj.InSeq.ToString().PadLeft(3, '0');
                newObj.NewRowFlag = "Y";
                newObj.InConfirmFlag = "N";
                newObj.PrintQty = 1;
                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();
                newObj.TN_PUR1200 = masterObj;
                masterObj.TN_PUR1201List.Add(newObj);
                DetailGridBindingSource.Add(newObj);
            }
            if (returnList.Count > 0)
            {
                SetIsFormControlChanged(true);
                DetailGridExControl.BestFitColumns();
            }
        }

        private void Edit_KeyDown_OLD(object sender, KeyEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_BAN1000;
            if (masterObj == null) return;

            var detailList = DetailGridBindingSource.List as List<TN_BAN1001>;
            if (detailList == null) return;

            if (e.KeyCode == Keys.Enter)
            {
                var textEdit = sender as TextEdit;
                if (textEdit == null) return;

                var productLotNo = textEdit.EditValue.GetNullToEmpty().ToUpper();
                if (productLotNo.IsNullOrEmpty())
                {
                }
                else
                {
                    IService<TN_BAN1100> LocalService = (IService<TN_BAN1100>)ProductionFactory.GetDomainService("TN_BAN1100");

                    var VI_BAN_STOCK_PRODUCT_LOT_NO = LocalService.GetChildList<VI_BAN_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == productLotNo).FirstOrDefault();
                    if (VI_BAN_STOCK_PRODUCT_LOT_NO == null)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("BanProductLotNo")));

                    }
                    else
                    {
                        if (VI_BAN_STOCK_PRODUCT_LOT_NO.StockQty <= 0)
                        {
                            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("InPossibleQty")));

                        }
                        else
                        {
                            //새로 추가한 것중 같은 LOT 인 리스트(저장안한것)
                            List<TN_BAN1001> productLotList = detailList.Where(x => x.BanProductLotNo == productLotNo && x.NewRowFlag == "Y").ToList();
                            decimal sumProductQty = productLotList.Sum(s => s.InQty);

                            if (VI_BAN_STOCK_PRODUCT_LOT_NO.StockQty <= sumProductQty)
                            {
                                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("InPossibleQty")));
                                //return;
                            }
                            else
                            {
                                var newObj = new TN_BAN1001();
                                newObj.InNo = masterObj.InNo;
                                newObj.InSeq = masterObj.TN_BAN1001List.Count == 0 ? 1 : masterObj.TN_BAN1001List.Max(p => p.InSeq) + 1;
                                newObj.ItemCode = VI_BAN_STOCK_PRODUCT_LOT_NO.ItemCode;
                                //newObj.InQty = VI_BAN_STOCK_PRODUCT_LOT_NO.StockQty;
                                newObj.InQty = VI_BAN_STOCK_PRODUCT_LOT_NO.StockQty - sumProductQty;
                                newObj.BanProductLotNo = VI_BAN_STOCK_PRODUCT_LOT_NO.ProductLotNo;
                                newObj.InLotNo = masterObj.InNo.ToString().Substring(0, 9) + masterObj.InNo.ToString().Substring(10, 4) + newObj.InSeq.ToString().PadLeft(3, '0');
                                newObj.NewRowFlag = "Y";
                                newObj.PrintQty = 1;
                                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == VI_BAN_STOCK_PRODUCT_LOT_NO.ItemCode).FirstOrDefault();
                                newObj.TN_BAN1000 = masterObj;
                                masterObj.TN_BAN1001List.Add(newObj);
                                DetailGridBindingSource.Add(newObj);
                                DetailGridExControl.BestFitColumns();
                            }
                        }
                    }
                    LocalService.Dispose();
                }

                textEdit.EditValue = "";
                e.Handled = true;
            }
        }
        #endregion
    }
}