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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using DevExpress.Utils;
using HKInc.Utils.Enum;
using DevExpress.XtraEditors.Repository;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using System.Data.SqlClient;
using HKInc.Service.Handler;
using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.PUR
{
    /// <summary>
    /// 외주발주관리화면
    /// </summary>
    public partial class XFPUR1400 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR1400> ModelService = (IService<TN_PUR1400>)ProductionFactory.GetDomainService("TN_PUR1400");
        private List<TN_LOT_MST> TN_LOT_MST_ADD_LIST = new List<TN_LOT_MST>();
        List<TN_STD1000> processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
        List<TN_STD1000> surfaceList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.SurfaceList);

        public XFPUR1400()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ProcessCode")
            {
                var processCode = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ProcessCode").GetNullToEmpty();
                var surface = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "TN_STD1100.SurfaceList").GetNullToEmpty();
                if (!processCode.IsNullOrEmpty() && !surface.IsNullOrEmpty())
                {
                    var processObj = processList.Where(p => p.CodeVal == processCode).FirstOrDefault();
                    if (processObj != null && processObj.CodeName.Contains("표면처리"))
                    {
                        var surfaceObj = surfaceList.Where(p => p.CodeVal == surface).FirstOrDefault();
                        if (surfaceObj != null)
                        {
                            e.DisplayText = processObj.CodeName + "_" + surfaceObj.CodeName;
                        }
                    }
                }
            }
        }

        protected override void InitCombo()
        {
            datePeriodEditEx1.SetTodayIsMonth();
            lupcustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p =>  p.UseFlag == "Y").OrderBy(p =>p.CustomerName).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"));
            MasterGridExControl.MainGrid.AddColumn("PoDate", LabelConvert.GetLabelText("PoDate"));
            MasterGridExControl.MainGrid.AddColumn("PoId", LabelConvert.GetLabelText("PoId"));
            MasterGridExControl.MainGrid.AddColumn("PoCustomerCode", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("DueDate", LabelConvert.GetLabelText("DueDate"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PoDate", "PoCustomerCode", "DueDate", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1400>(MasterGridExControl);

            var barPoDocumentPrint = new DevExpress.XtraBars.BarButtonItem();
            barPoDocumentPrint.Id = 4;
            barPoDocumentPrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/boreport2");
            barPoDocumentPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barPoDocumentPrint.Name = "barPoDocumentPrint";
            barPoDocumentPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barPoDocumentPrint.ShortcutKeyDisplayString = "Alt+P";
            barPoDocumentPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barPoDocumentPrint.Caption = LabelConvert.GetLabelText("PoDocumentPrint") + "[Alt+P]";
            barPoDocumentPrint.ItemClick += BarPoDocumentPrint_ItemClick; ;
            barPoDocumentPrint.Enabled = UserRight.HasEdit;
            barPoDocumentPrint.Alignment = BarItemLinkAlignment.Right;
            MasterGridExControl.BarTools.AddItem(barPoDocumentPrint);
            
            DetailGridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, false);
            DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "PoSeq", true);
            DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            DetailGridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"));            
            DetailGridExControl.MainGrid.AddColumn("PoSeq", LabelConvert.GetLabelText("PoSeq"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"), false);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            DetailGridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0", false);
            DetailGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            DetailGridExControl.MainGrid.AddColumn("ItemMoveNo", LabelConvert.GetLabelText("ItemMoveNo"));
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemName", LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.SurfaceList", LabelConvert.GetLabelText("SurfaceList"), false);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("PoQty", LabelConvert.GetLabelText("PoQty"));
            DetailGridExControl.MainGrid.AddColumn("PoCost", LabelConvert.GetLabelText("PoCost"));
            DetailGridExControl.MainGrid.AddUnboundColumn("PoAmt", LabelConvert.GetLabelText("PoAmt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([PoQty],0) * ISNULL([PoCost],0)", FormatType.Numeric, "n0");            
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "PoQty", "PoCost", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1401>(DetailGridExControl);

            DetailGridExControl.MainGrid.MainView.Columns["PoQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            DetailGridExControl.MainGrid.MainView.Columns["PoQty"].SummaryItem.FieldName = "PoQty";
            DetailGridExControl.MainGrid.MainView.Columns["PoQty"].SummaryItem.DisplayFormat = "{0:#,#.##}";

            var barButtonItemMovePrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonItemMovePrint.Id = 5;
            barButtonItemMovePrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            barButtonItemMovePrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.I));
            barButtonItemMovePrint.Name = "barButtonItemMovePrint";
            barButtonItemMovePrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonItemMovePrint.ShortcutKeyDisplayString = "Alt+I";
            barButtonItemMovePrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonItemMovePrint.Caption = LabelConvert.GetLabelText("ItemMovePrint") + "[Alt+I]";
            barButtonItemMovePrint.ItemClick += BarButtonItemMovePrint_ItemClick;
            barButtonItemMovePrint.Alignment = BarItemLinkAlignment.Right;
            DetailGridExControl.BarTools.AddItem(barButtonItemMovePrint);

            var barTextEditBarCode = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCode.Id = 6;
            barTextEditBarCode.Enabled = UserRight.HasEdit;
            barTextEditBarCode.Name = "barTextEditBarCode";
            barTextEditBarCode.EditWidth = 150;
            barTextEditBarCode.Edit.KeyDown += Edit_KeyDown;
            //DetailGridExControl.BarTools.AddItem(barTextEditBarCode);

            var barTextEditBarCodeStaticItem = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCodeStaticItem.Id = 7;
            barTextEditBarCodeStaticItem.Name = "barTextEditBarCodeStaticItem";
            barTextEditBarCodeStaticItem.Edit.NullText = LabelConvert.GetLabelText("ItemMoveNo") + ":";
            barTextEditBarCodeStaticItem.EditWidth = barTextEditBarCodeStaticItem.Edit.NullText.Length * 15;
            //barTextEditBarCodeStaticItem.EditWidth = 120;
            barTextEditBarCodeStaticItem.Enabled = false;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.ForeColor = Color.Black;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Far;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.BackColor = Color.Transparent;
            barTextEditBarCodeStaticItem.Edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Alignment = BarItemLinkAlignment.Left;

            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCode);
            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCodeStaticItem);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoCustomerCode",  ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(),"CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PoDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("DueDate");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("PoQty");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("PoCost");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.SurfaceList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.SurfaceList), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            
            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("PoNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            TN_LOT_MST_ADD_LIST.Clear();

            ModelService.ReLoad();
            
            string cust = lupcustcode.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.PoDate >= datePeriodEditEx1.DateFrEdit.DateTime 
                                                                        &&  p.PoDate <= datePeriodEditEx1.DateToEdit.DateTime)
                                                                        && (string.IsNullOrEmpty(cust) ? true : p.PoCustomerCode == cust)
                                                                     )
                                                                     .OrderBy(o => o.PoNo)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_PUR1400 obj = MasterGridBindingSource.Current as TN_PUR1400;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.TN_PUR1401List.OrderBy(p => p.PoSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var masterList = MasterGridBindingSource.List as List<TN_PUR1400>;
                if (masterList != null && masterList.Count > 0)
                {
                    if (masterList.Any(p => p.DueDate.ToString("yyyy-MM-dd") == "0001-01-01"))
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("DueDate")));
                        SetSaveMessageCheck = false;
                        //MessageBoxHandler.Show("입고예정일이 제대로 등록되지 않은 발주가 있습니다.", "경고");
                        return;
                    }

                    if (masterList.Any(p => p.PoCustomerCode.IsNullOrEmpty()))
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("CustomerName")));
                        SetSaveMessageCheck = false;
                        //MessageBoxHandler.Show("거래처는 필수입니다.", "경고");
                        return;
                    }

                    var editList = masterList.Where(p => p.EditRowFlag == "Y").ToList();

                    foreach (var v in editList.Where(p => p.TN_PUR1401List.Any(c => c.NewRowFlag == "Y")).ToList())
                    {
                        foreach (var d in v.TN_PUR1401List.Where(p => p.NewRowFlag == "Y").ToList())
                        {
                            var ItemMoveFirstObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == d.ItemMoveNo && p.ProcessSeq == 1).FirstOrDefault();
                            var newItemMove = new TN_ITEM_MOVE();
                            newItemMove.ItemMoveNo = d.ItemMoveNo;
                            newItemMove.WorkNo = d.WorkNo;
                            newItemMove.ProcessCode = d.ProcessCode;
                            newItemMove.ProcessSeq = d.ProcessSeq;
                            newItemMove.ProductLotNo = d.ProductLotNo;
                            newItemMove.BoxInQty = ItemMoveFirstObj == null ? 0 : ItemMoveFirstObj.BoxInQty.GetDecimalNullToZero();
                            newItemMove.ResultSumQty = d.PoQty;
                            newItemMove.OkSumQty = d.PoQty;
                            newItemMove.BadSumQty = 0;
                            newItemMove.ResultQty = d.PoQty;
                            newItemMove.OkQty = d.PoQty;
                            newItemMove.BadQty = 0;
                            ModelService.InsertChild(newItemMove);
                        }
                    }

                    foreach (var v in editList.Where(p => p.TN_PUR1401List.Any(c => c.NewRowFlag != "Y" && c.EditRowFlag == "Y")).ToList())
                    {
                        foreach (var d in v.TN_PUR1401List.Where(p => p.NewRowFlag != "Y" && p.EditRowFlag == "Y").ToList())
                        {
                            var updateItemMove = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == d.ItemMoveNo && p.ProcessSeq == d.ProcessSeq).FirstOrDefault();
                            if (updateItemMove != null)
                            {
                                updateItemMove.ResultSumQty = d.PoQty;
                                updateItemMove.OkSumQty = d.PoQty;
                                updateItemMove.ResultQty = d.PoQty;
                                updateItemMove.OkQty = d.PoQty;
                                updateItemMove.UpdateTime = DateTime.Now;
                                ModelService.UpdateChild(updateItemMove);
                            }
                        }
                    }
                }

                foreach (var v in TN_LOT_MST_ADD_LIST)
                {
                    ModelService.InsertChild(v);
                }
            }         

            ModelService.Save();

            //작업지시상태 갱신
            DbRequestHandler.USP_UPD_PUR1400_JOBSTATES();

            DataLoad();
        }

        protected override void AddRowClicked()
        {
            TN_PUR1400 obj = new TN_PUR1400()
            {
                PoNo = DbRequestHandler.GetSeqMonth("OPO"),
                PoDate = DateTime.Today,
                DueDate = DateTime.Today.AddMonths(1),
                PoId = GlobalVariable.LoginId
            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
            MasterGridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }

        protected override void DeleteRow()
        {
            TN_PUR1400 obj = MasterGridBindingSource.Current as TN_PUR1400;
            if (obj == null) return;
            if (obj.TN_PUR1401List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("OutPoMasterInfo"), LabelConvert.GetLabelText("OutPoDetailInfo"), LabelConvert.GetLabelText("OutPoDetailInfo")));
                return;
            }
            if (obj.TN_PUR1500List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("OutPoMasterInfo"), LabelConvert.GetLabelText("OutInMasterInfo"), LabelConvert.GetLabelText("OutInMasterInfo")));
                return;
            }
            MasterGridBindingSource.Remove(obj);
            ModelService.Delete(obj);
            MasterGridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }

        private void Edit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var textEdit = sender as TextEdit;
                if (textEdit == null) return;

                IService<TN_PUR1400> LocalService = (IService<TN_PUR1400>)ProductionFactory.GetDomainService("TN_PUR1400");

                try
                {
                    var itemMoveNo = textEdit.EditValue.GetNullToEmpty().ToUpper();
                    if (itemMoveNo.IsNullOrEmpty()) return;
                    
                    var detailList = DetailGridBindingSource.List as List<TN_PUR1401>;
                    if (detailList == null) return;

                    if (detailList != null)
                    {
                        var masterObj = MasterGridBindingSource.Current as TN_PUR1400;
                        if (masterObj != null)
                        {
                            var itemMoveList = LocalService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == itemMoveNo).ToList();
                            if (itemMoveList.Count == 0)
                            {
                                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                            }
                            else
                            {
                                var preItemMoveObj = itemMoveList.OrderBy(p => p.ProcessSeq).LastOrDefault();
                                if (preItemMoveObj == null)
                                    return;

                                var preProcessCheckObj = LocalService.GetChildList<TN_MPS1200>(p => p.WorkNo == preItemMoveObj.WorkNo
                                                                                                && p.ProcessSeq == preItemMoveObj.ProcessSeq
                                                                                                ).FirstOrDefault();
                                if (preProcessCheckObj == null)
                                    return;

                                var outProcessCheckObj = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == preItemMoveObj.WorkNo
                                                                                                && p.ProcessSeq == preItemMoveObj.ProcessSeq + 1
                                                                                                && p.OutProcFlag == "Y").FirstOrDefault();

                                if (outProcessCheckObj == null)
                                {
                                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("OutProcess")));
                                    return;
                                }

                                var checkObj = LocalService.GetChildList<TN_PUR1401>(p => p.ItemMoveNo == itemMoveNo && p.ProcessSeq == outProcessCheckObj.ProcessSeq).FirstOrDefault();
                                if (checkObj != null)
                                {
                                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_41), LabelConvert.GetLabelText("ItemMoveNo")));
                                    return;
                                }

                                //외주이전공정 작업이 완료되지 않아도 출고가능 2021.01.13 김정진대리 요청
                                //if (preProcessCheckObj.JobStates != MasterCodeSTR.JobStates_End && preProcessCheckObj.JobStates != MasterCodeSTR.JobStates_OutEnd)
                                //{
                                //    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_92));
                                //    //MessageBoxHandler.Show("이전 공정이 끝나지 않았습니다.");
                                //    return;
                                //}

                                var detailCheckObj = detailList.Where(p => p.ItemMoveNo == itemMoveNo && p.ProcessSeq == outProcessCheckObj.ProcessSeq).FirstOrDefault();
                                if (detailCheckObj != null)
                                {
                                    DetailGridExControl.MainGrid.MainView.FocusedRowHandle = DetailGridExControl.MainGrid.MainView.LocateByValue("PoSeq", detailCheckObj.PoSeq);
                                }
                                else
                                {
                                    TN_PUR1401 newobj = new TN_PUR1401();
                                    newobj.PoNo = masterObj.PoNo;
                                    newobj.PoSeq = masterObj.TN_PUR1401List.Count == 0 ? 1 : masterObj.TN_PUR1401List.Max(o => o.PoSeq) + 1;
                                    newobj.WorkNo = outProcessCheckObj.WorkNo;
                                    newobj.ProcessCode = outProcessCheckObj.ProcessCode;
                                    newobj.ProcessSeq = outProcessCheckObj.ProcessSeq;
                                    newobj.ProductLotNo = preItemMoveObj.ProductLotNo;
                                    newobj.ItemMoveNo = itemMoveNo;
                                    newobj.ItemCode = outProcessCheckObj.ItemCode;
                                    newobj.PoQty = preItemMoveObj.OkQty.GetDecimalNullToZero();
                                    newobj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newobj.ItemCode).First();
                                    newobj.TN_MPS1200 = outProcessCheckObj;
                                    //newobj.TN_MPS1200.JobStates = MasterCodeSTR.JobStates_OutStart; //상태 - 외주진행
                                    newobj.NewRowFlag = "Y";
                                    masterObj.TN_PUR1401List.Add(newobj);
                                    masterObj.EditRowFlag = "Y";
                                    DetailGridBindingSource.Add(newobj);
                                }
                            }
                        }
                    }
                }
                finally
                {
                    DetailGridExControl.BestFitColumns();
                    LocalService.Dispose();
                    textEdit.EditValue = "";
                    e.Handled = true;
                }
            }
        }

        //protected override void DetailAddRowClicked()
        //{
        //    TN_PUR1400 obj = MasterGridBindingSource.Current as TN_PUR1400;        
        //    if (obj == null) return;

        //    var masterList = MasterGridBindingSource.List as List<TN_PUR1400>;
        //    if (masterList == null) return;

        //    var newMasterList = masterList.Where(p => p.TN_PUR1401List.Any(c => c.NewRowFlag == "Y")).ToList();
            
        //    PopupDataParam param = new PopupDataParam();
        //    param.SetValue(PopupParameter.Constraint, "TN_PUR1400");
        //    param.SetValue(PopupParameter.Value_1, newMasterList);

        //    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_MPS1200, param, AddOrderList);
        //    form.ShowPopup(true);
        //}

        //private void AddOrderList(object sender, HKInc.Utils.Common.PopupArgument e)
        //{
        //    if (e == null) return;

        //    TN_PUR1400 obj = MasterGridBindingSource.Current as TN_PUR1400;
        //    List<TN_MPS1200> returnValueList = (List<TN_MPS1200>)e.Map.GetValue(PopupParameter.ReturnObject);
        //    foreach (var v in returnValueList)
        //    {
        //        var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == v.WorkNo && p.ProcessCode == v.ProcessCode && p.ProcessSeq == v.ProcessSeq).First();

        //        var productLotNo = string.Empty;
        //        var itemMoveNo = string.Empty;

        //        if (v.ProcessSeq == 1)
        //        {
        //            var workingDate = DateTime.Today;
        //            var newLotMst = new TN_LOT_MST();
        //            newLotMst.WorkNo = TN_MPS1200.WorkNo;
        //            //newLotMst.ItemMoveNo = DbRequestHandler.GetSeqMonth("IMV");
        //            newLotMst.ProductLotNo = workingDate.ToString("yyMMdd") + TN_MPS1200.WorkNo.Right(5) + "001";
        //            newLotMst.ItemCode = TN_MPS1200.ItemCode;
        //            //newLotMst.MachineCode = TN_MPS1200.MachineCode;
        //            //newLotMst.WorkingDate = workingDate;

        //            var newLotDtl = new TN_LOT_DTL();
        //            newLotDtl.WorkNo = TN_MPS1200.WorkNo;
        //            newLotDtl.ProductLotNo = newLotMst.ProductLotNo;
        //            newLotDtl.Seq = 1;
        //            newLotDtl.ItemCode = TN_MPS1200.ItemCode;
        //            newLotDtl.MachineCode = TN_MPS1200.MachineCode;
        //            newLotDtl.WorkingDate = workingDate;

        //            newLotMst.TN_LOT_DTL_List.Add(newLotDtl);
        //            TN_LOT_MST_ADD_LIST.Add(newLotMst);
        //            //ModelService.InsertChild(newLotMst);

        //            productLotNo = newLotMst.ProductLotNo;
        //            itemMoveNo = null;
        //        }
        //        else
        //        {
        //            var Previous_TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == v.WorkNo && p.ProcessSeq == v.ProcessSeq - 1).First();
        //            productLotNo = Previous_TN_MPS1200.TN_MPS1201List.OrderBy(p => p.UpdateTime).Last().ProductLotNo;
        //            itemMoveNo = Previous_TN_MPS1200.TN_MPS1201List.OrderBy(p => p.UpdateTime).Last().ItemMoveNo;
        //        }

        //        TN_PUR1401 newobj = new TN_PUR1401();
        //        newobj.PoNo = obj.PoNo;
        //        newobj.PoSeq = obj.TN_PUR1401List.Count == 0 ? 1 : obj.TN_PUR1401List.Max(o => o.PoSeq) + 1;
        //        newobj.WorkNo = v.WorkNo;
        //        newobj.ProcessCode = v.ProcessCode;
        //        newobj.ProcessSeq = v.ProcessSeq;
        //        newobj.ProductLotNo = productLotNo;
        //        newobj.ItemMoveNo = itemMoveNo;
        //        newobj.ItemCode = v.ItemCode;
        //        newobj.PoQty = v.WorkQty;
        //        newobj.Memo = v.Memo;
        //        newobj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).First();
        //        newobj.TN_MPS1200 = TN_MPS1200;
        //        newobj.TN_MPS1200.JobStates = MasterCodeSTR.JobStates_OutStart; //상태 - 외주진행
        //        newobj.NewRowFlag = "Y";

        //        obj.TN_PUR1401List.Add(newobj);
        //        DetailGridBindingSource.Add(newobj);
        //        IsFormControlChanged = true;
        //    }
        //    DetailGridExControl.BestFitColumns();
        //}

        protected override void DeleteDetailRow()
        {
            TN_PUR1400 obj = MasterGridBindingSource.Current as TN_PUR1400;
            if (obj == null) return;
            TN_PUR1401 delobj = DetailGridBindingSource.Current as TN_PUR1401;
            if (delobj == null) return;

            if (delobj.TN_PUR1501List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("OutPoDetailInfo"), LabelConvert.GetLabelText("OutInDetailInfo"), LabelConvert.GetLabelText("OutInDetailInfo")));
                
                return;
            }

            if (delobj.ProcessSeq == 1)
            {
                var lotObj = ModelService.GetChildList<TN_LOT_MST>(p => p.WorkNo == delobj.WorkNo && p.ProductLotNo == delobj.ProductLotNo).FirstOrDefault();
                if (lotObj != null)
                {
                    foreach (var v in lotObj.TN_LOT_DTL_List)
                        ModelService.RemoveChild(v);
                    ModelService.RemoveChild(lotObj);
                }
                var addObj = TN_LOT_MST_ADD_LIST.Where(p => p.WorkNo == delobj.WorkNo && p.ProductLotNo == delobj.ProductLotNo).FirstOrDefault();
                if (addObj != null)
                {
                    foreach (var v in addObj.TN_LOT_DTL_List)
                        ModelService.RemoveChild(v);
                    TN_LOT_MST_ADD_LIST.Remove(addObj);
                }
            }

            //var nextProcessSeq = delobj.TN_MPS1200.ProcessSeq.GetNullToZero() + 1;
            //var nextProcess = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == delobj.WorkNo && p.ProcessSeq == nextProcessSeq);
            //if (nextProcess.Count > 0)
            //{
            //    if(nextProcess.First().JobStates != MasterCodeSTR.JobStates_Wait) // 해당공정의 다음 공정순번의 작업상태가 대기상태가 아닐 경우 삭제 불가
            //    {
            //        MessageBoxHandler.Show("다음 공정의 데이터가 존재합니다.", "경고");
            //        return;
            //    }
            //}

            DetailGridBindingSource.Remove(delobj);
            obj.TN_PUR1401List.Remove(delobj);
            //delobj.TN_MPS1200.JobStates = MasterCodeSTR.JobStates_Wait; //상태 - 작업대기
            obj.EditRowFlag = "Y";
            IsFormControlChanged = true;

            var updateItemMove = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == delobj.ItemMoveNo && p.ProcessSeq == delobj.ProcessSeq).FirstOrDefault();
            if (updateItemMove != null)
            {
                ModelService.RemoveChild(updateItemMove);
            }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1400;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR1401;
            if (detailObj == null) return;

            masterObj.EditRowFlag = "Y";
            if(detailObj.NewRowFlag != "Y")
                detailObj.EditRowFlag = "Y";

            if (e.Column.FieldName == "PoCost")
            {
                foreach (var v in masterObj.TN_PUR1401List)
                {
                    if (v.ItemCode == detailObj.ItemCode)
                        v.PoCost = e.Value.GetDecimalNullToNull();
                }
                DetailGridExControl.BestFitColumns();
            }
            else if (e.Column.FieldName == "Memo")
            {
                foreach (var v in masterObj.TN_PUR1401List)
                {
                    if (v.ItemCode == detailObj.ItemCode)
                        v.Memo = e.Value.GetNullToNull();
                }
                DetailGridExControl.BestFitColumns();
            }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var obj = DetailGridBindingSource.Current as TN_PUR1401;

            if (obj.TN_PUR1501List.Count > 0)
            {
                //MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_52), LabelConvert.GetLabelText("OutInDetailInfo")));
                e.Cancel = true;
            }
        }

        /// <summary> 이동표 출력 </summary>
        private void BarButtonItemMovePrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            var detailList = DetailGridBindingSource.List as List<TN_PUR1401>;
            if (detailList == null) return;

            var checkList = detailList.Where(p => p._Check == "Y").ToList();
            if (checkList.Count == 0) return;

            var mainReport = new REPORT.XRITEMMOVENO();

            foreach (var v in checkList)
            {
                TEMP_ITEM_MOVE_NO_MASTER masterObj = null;
                List<TEMP_ITEM_MOVE_NO_DETAIL> detailList2 = null;

                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var _workNo = new SqlParameter("@WorkNo", v.WorkNo);
                    var result = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER @WorkNo", _workNo).ToList();
                    if (result != null && result.Count > 0)
                    {
                        masterObj = result.Where(p => p.ItemMoveNo == v.ItemMoveNo).FirstOrDefault();
                    }
                }

                if (masterObj == null) continue;

                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
                    var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

                    detailList2 = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
                }

                if (detailList2 == null) continue;

                var report = new REPORT.XRITEMMOVENO(masterObj, detailList2);
                report.CreateDocument();
                mainReport.Pages.AddRange(report.Pages);
                v._Check = "N";
            }

            var printTool = new ReportPrintTool(mainReport);
            printTool.ShowPreview();
        }

        /// <summary> 발주서 출력 </summary>
        private void BarPoDocumentPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MasterGridBindingSource == null) return;
            if (DetailGridBindingSource == null) return;
            if (!UserRight.HasEdit) return;

            var masterObj = MasterGridBindingSource.Current as TN_PUR1400;
            if (masterObj == null) return;

            var listCount = masterObj.TN_PUR1401List.Count;
            if (listCount == 0) return;

            ActSave();
            if (!SetSaveMessageCheck) return;

            int printRowCnt = 20;

            var valueCount = listCount / printRowCnt;
            var modCount = listCount % printRowCnt;

            if (modCount == 0)
            {
                ReportCreateToPrint(masterObj);
            }
            else
            {
                var checkCount = (valueCount == 0 ? 1 : valueCount + 1) * printRowCnt;
                while (true)
                {
                    masterObj.TN_PUR1401List.Add(new TN_PUR1401()
                    {
                        PoNo = masterObj.PoNo,
                        PoSeq = -1,
                        PoCost = 0
                    });
                    if (masterObj.TN_PUR1401List.Count == checkCount) break;
                }
                ReportCreateToPrint(masterObj);
            }

            //try
            //{
            //    WaitHandler.ShowWait();

            //    //List<TEMP_PUR1400_REPORT> detailReportList;
            //    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            //    {
            //        context.Database.CommandTimeout = 0;
            //        var PoNo = new SqlParameter("@PoNo", masterObj.PoNo);
            //        var detailReportList = context.Database.SqlQuery<TEMP_PUR1400_REPORT>("USP_GET_PUR1400_REPORT @PoNo", PoNo).ToList();

            //        var report = new REPORT.XRPUR1400(masterObj, detailReportList);
            //        report.CreateDocument();

            //        report.PrintingSystem.ShowMarginsWarning = false;
            //        report.ShowPrintStatusDialog = false;
            //        report.ShowPreview();
            //    }
            //}
            //catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            //finally
            //{
            //    WaitHandler.CloseWait();
            //}
        }

        private void ReportCreateToPrint(TN_PUR1400 masterObj)
        {
            try
            {
                WaitHandler.ShowWait();
                var report = new REPORT.XRPUR1100_V2(masterObj);
                //var report = new REPORT.XRPUR1100(masterObj);     // 2023-01-09 김진우 주석
                report.CreateDocument();

                report.PrintingSystem.ShowMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                report.ShowPreview();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally
            {
                foreach (var v in masterObj.TN_PUR1401List.Where(p => p.PoSeq == -1).ToList())
                    masterObj.TN_PUR1401List.Remove(v);
                WaitHandler.CloseWait();
            }

            //try
            //{
            //    WaitHandler.ShowWait();
            //    var report = new REPORT.XRPUR1400(masterObj);
            //    report.CreateDocument();

            //    report.PrintingSystem.ShowMarginsWarning = false;
            //    report.ShowPrintStatusDialog = false;
            //    report.ShowPreview();
            //}
            //catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            //finally
            //{
            //    foreach (var v in masterObj.TN_PUR1401List.Where(p => p.PoSeq == -1).ToList())
            //        masterObj.TN_PUR1401List.Remove(v);
            //    WaitHandler.CloseWait();
            //}
        }
    }    
}
