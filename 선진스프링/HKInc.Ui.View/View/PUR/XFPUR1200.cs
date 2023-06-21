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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Controls;

namespace HKInc.Ui.View.View.PUR
{
    /// <summary>
    /// 자재입고관리
    /// </summary>
    public partial class XFPUR1200 : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR1200> ModelService = (IService<TN_PUR1200>)ProductionFactory.GetDomainService("TN_PUR1200");
        bool InCustomerCodeEditCheckFlag = false;

        public XFPUR1200()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.AllowDrop = true;

            MasterGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;

            DetailGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailMainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.DragOver += new System.Windows.Forms.DragEventHandler(grid_DragOver);
            DetailGridExControl.MainGrid.DragDrop += new System.Windows.Forms.DragEventHandler(grid_DragDrop);

            btn_PurchaseStatus.Click += Btn_PurchaseStatus_Click;
        }

        private void grid_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            Point p = Control.MousePosition;
            Point mp = (this.DetailGridExControl.MainGrid.PointToClient(p));
            GridHitInfo hitInfo = this.DetailGridExControl.MainGrid.MainView.CalcHitInfo(mp);
            if (hitInfo == null || hitInfo.Column == null) return;

            if (hitInfo.Column.FieldName == "InCustomerLotNo" || hitInfo.Column.FieldName == "Memo")
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
            //if (e.Data.GetDataPresent(typeof(Drag_n_Drop_2Forms.User_DataSet.UserRow)))
            //{
            //    e.Effect = DragDropEffects.Move;

            //    // *********** Added Code ******** (start) ************************************
            //    //GridHitInfo srcHitInfo = e.Data.GetData(typeof(GridHitInfo)) as GridHitInfo;
            //    Drag_n_Drop_2Forms.User_DataSet.UserRow dr = e.Data.GetData(typeof(Drag_n_Drop_2Forms.User_DataSet.UserRow)) as Drag_n_Drop_2Forms.User_DataSet.UserRow;
            //    if (dr == null)
            //    {
            //        lblID_Value.Text = "Unable to obtain the row number.";
            //        return;
            //    }
            //    else
            //    {
            //        lblID_Value.Text = "Row Number: " + dr["id"].ToString();
            //    }

            //    //int sourceRow = srcHitInfo.RowHandle;
            //    //lblID_Value.Text = "It came from row number " + sourceRow.ToString();
            //    // *********** Added Code ******** (end) **************************************
            //}
            //else
            //{
            //    e.Effect = DragDropEffects.None;
            //}
        }

        private void grid_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            var data = e.Data.GetData(typeof(string));
            if (data.IsNullOrEmpty()) return;

            Point p = Control.MousePosition;

            Point mp = (this.DetailGridExControl.MainGrid.PointToClient(p));
            GridHitInfo hitInfo = this.DetailGridExControl.MainGrid.MainView.CalcHitInfo(mp);
            if (hitInfo == null || hitInfo.Column == null) return;
            int targetRow = hitInfo.RowHandle;
            DetailGridExControl.MainGrid.MainView.SetRowCellValue(targetRow, hitInfo.Column.FieldName, data);

            //DataRow row = e.Data.GetData(typeof(Drag_n_Drop_2Forms.User_DataSet.UserRow)) as DataRow;
            //if (row != null && table != null && row.Table != table)
            //{

            //    // *********** Added Code ******** (start) ************************************
            //    try
            //    {

            //        GridView view = grid.MainView as GridView;
            //        Point p = Control.MousePosition;

            //        Point mp = (this.gridControl2.PointToClient(p));
            //        GridHitInfo hitInfo = this.gridView2.CalcHitInfo(mp);
            //        int targetRow = hitInfo.RowHandle;
            //        object o = this.gridView2.GetRowCellValue(targetRow, "Id");
            //        string number = "";
            //        if (o != null)
            //        {
            //            number = this.gridView2.GetRowCellValue(targetRow, "Id").ToString();
            //        }

            //        //lblID_Value.Text = "It came from row number " + row["id"] + ".";
            //        lblID_Value.Text = lblID_Value.Text + " Then it went to number " + number;
            //        table.ImportRow(row);
            //        row.Delete();
            //    }
            //    catch
            //    {
            //        lblID_Value.Text = "Unable to obtain the Id or User Name.";
            //    }
            //    // *********** Added Code ******** (end) **************************************

            //}
        }

        protected override void InitCombo()
        {
            dt_InDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dt_InDate.DateToEdit.DateTime = DateTime.Today;

            lup_InCustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Purchase || p.CustomerType == null)).ToList());
            lup_InId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());

            btn_PurchaseStatus.Text = LabelConvert.GetLabelText("PurchaseStatus") + "(&F)";
        }

        protected override void InitGrid()
        {
            IsMasterGridButtonFileChooseEnabled = UserRight.HasEdit;
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, UserRight.HasEdit);
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("Return") + "[F10]", IconImageList.GetIconImage("spreadsheet/showdetail"));

            MasterGridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"));
            MasterGridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"));
            MasterGridExControl.MainGrid.AddColumn("InDate", LabelConvert.GetLabelText("InDate"));
            MasterGridExControl.MainGrid.AddColumn("InId", LabelConvert.GetLabelText("InId"));
            MasterGridExControl.MainGrid.AddColumn("InCustomerCode", LabelConvert.GetLabelText("InCustomer"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InDate", "InCustomerCode", "Memo");

            var barButtonDisposal = new DevExpress.XtraBars.BarButtonItem();
            barButtonDisposal.Id = 4;
            barButtonDisposal.ImageOptions.Image = IconImageList.GetIconImage("actions/apply");
            barButtonDisposal.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.D));
            barButtonDisposal.Name = "barButtonDisposal";
            barButtonDisposal.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonDisposal.ShortcutKeyDisplayString = "Alt+D";
            barButtonDisposal.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonDisposal.Caption = LabelConvert.GetLabelText("Disposal") + "[Alt+D]";
            barButtonDisposal.ItemClick += BarButtonDisposal_ItemClick;
            MasterGridExControl.BarTools.AddItem(barButtonDisposal);

            DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "InLotNo", true);
            DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("BarcodeSelect"));
            DetailGridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"), false);
            DetailGridExControl.MainGrid.AddColumn("InSeq", LabelConvert.GetLabelText("InSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InInspectionState", LabelConvert.GetLabelText("InInspectionState"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("InConfirmFlag", LabelConvert.GetLabelText("InConfirmFlag"));
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"));
            DetailGridExControl.MainGrid.AddColumn("InCost", LabelConvert.GetLabelText("InCost"));
            DetailGridExControl.MainGrid.AddUnboundColumn("InAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([InQty],0) * ISNULL([InCost],0)", FormatType.Numeric, "#,###,###,###.##");
            DetailGridExControl.MainGrid.AddColumn("Temp", LabelConvert.GetLabelText("InLotNoParent"), false);
            DetailGridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            DetailGridExControl.MainGrid.AddColumn("PrintQty", LabelConvert.GetLabelText("PrintQty2"));
            DetailGridExControl.MainGrid.AddColumn("InCustomerLotNo", LabelConvert.GetLabelText("InCustomerLotNo"));
            DetailGridExControl.MainGrid.AddColumn("InWhCode", LabelConvert.GetLabelText("InWhCode"));
            DetailGridExControl.MainGrid.AddColumn("InWhPosition", LabelConvert.GetLabelText("InWhPosition"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "InQty", "InCost", "InCustomerLotNo", "InWhCode", "InWhPosition", "Memo", "PrintQty");
            
            var barButtonDevide = new DevExpress.XtraBars.BarButtonItem();
            barButtonDevide.Id = 6;
            barButtonDevide.ImageOptions.Image = IconImageList.GetIconImage("spreadsheet/showdetail");
            //barButtonDevide.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonAdd.ImageOptions.LargeImage")));
            barButtonDevide.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.E));
            barButtonDevide.Name = "barButtonDevide";
            barButtonDevide.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonDevide.ShortcutKeyDisplayString = "Alt+E";
            barButtonDevide.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonDevide.Caption = LabelConvert.GetLabelText("InDevide") + "[Alt+E]";  //"분할처리[Alt+T]";
            barButtonDevide.ItemClick += BarButtonDevide_ItemClick;
            DetailGridExControl.BarTools.AddItem(barButtonDevide);

            var barButtonInConfirm = new DevExpress.XtraBars.BarButtonItem();
            barButtonInConfirm.Id = 4;
            barButtonInConfirm.ImageOptions.Image = IconImageList.GetIconImage("actions/apply");
            barButtonInConfirm.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.T));
            barButtonInConfirm.Name = "barButtonInConfirm";
            barButtonInConfirm.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonInConfirm.ShortcutKeyDisplayString = "Alt+T";
            barButtonInConfirm.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonInConfirm.Caption = LabelConvert.GetLabelText("InConfirm") + "[Alt+T]";
            barButtonInConfirm.ItemClick += BarButtonInConfirm_ItemClick;
            DetailGridExControl.BarTools.AddItem(barButtonInConfirm);

            RepositoryItemCheckEdit repositoryItemCheckEdit = new RepositoryItemCheckEdit() { ValueChecked = "Y", ValueUnchecked = "N" };
            repositoryItemCheckEdit.NullStyle = StyleIndeterminate.Unchecked;
            repositoryItemCheckEdit.NullText = "N";
            repositoryItemCheckEdit.GlyphAlignment = HorzAlignment.Far;
            repositoryItemCheckEdit.Caption = LabelConvert.GetLabelText("InCustomerCodeEditCheck");
            var barCheckBoxEdit = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, repositoryItemCheckEdit);
            barCheckBoxEdit.Id = 7;
            barCheckBoxEdit.Enabled = UserRight.HasEdit;
            barCheckBoxEdit.Name = "barCheckBoxEdit";
            barCheckBoxEdit.EditValueChanged += BarCheckBoxEdit_EditValueChanged;
            barCheckBoxEdit.Caption = LabelConvert.GetLabelText("InCustomerCodeEditCheck");
            barCheckBoxEdit.Alignment = BarItemLinkAlignment.Right;
            barCheckBoxEdit.EditWidth = 175;
            //DetailGridExControl.BarTools.AddItem(barTextEditBarCode);
            DetailGridExControl.BarTools.AddItem(barCheckBoxEdit);

            var barButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonPrint.Id = 5;
            barButtonPrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            barButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barButtonPrint.Name = "barButtonPrint";
            barButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonPrint.ShortcutKeyDisplayString = "Alt+P";
            barButtonPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonPrint.Caption = LabelConvert.GetLabelText("BarcodePrint") + "[Alt+P]";
            barButtonPrint.ItemClick += BarButtonPrint_ItemClick;
            barButtonPrint.Alignment = BarItemLinkAlignment.Right;
            DetailGridExControl.BarTools.AddItem(barButtonPrint);

            var barButtonCustomerLotNoScan = new DevExpress.XtraBars.BarButtonItem();
            barButtonCustomerLotNoScan.Id = 6;
            barButtonCustomerLotNoScan.ImageOptions.Image = IconImageList.GetIconImage("toolbox%20items/barcode2");
            barButtonCustomerLotNoScan.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.C));
            barButtonCustomerLotNoScan.Name = "barButtonCustomerLotNoScan";
            barButtonCustomerLotNoScan.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonCustomerLotNoScan.ShortcutKeyDisplayString = "Alt+C";
            barButtonCustomerLotNoScan.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonCustomerLotNoScan.Caption = LabelConvert.GetLabelText("CustomerLotNoScan") + "[Alt+C]";
            barButtonCustomerLotNoScan.ItemClick += BarButtonCustomerLotNoScan_ItemClick;
            //barButtonCustomerLotNoScan.Alignment = BarItemLinkAlignment.Right;
            DetailGridExControl.BarTools.AddItem(barButtonCustomerLotNoScan);

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1200>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1201>(DetailGridExControl);
        }

        private void BarCheckBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            var value = ((DevExpress.XtraBars.BarEditItem)sender).EditValue.GetNullToEmpty();
            this.InCustomerCodeEditCheckFlag = value == "Y" ? true : false;

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InCustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Purchase || p.CustomerType == null)).ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("PrintQty");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InQty", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InCost", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhCode", ModelService.GetChildList<TN_WMS1000>(p => (p.Temp == MasterCodeSTR.WhCodeDivision_MAT || p.Temp == MasterCodeSTR.WhCodeDivision_BU || p.Temp == null) && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("InConfirmFlag", "N");
            
            var WhPositionEdit = DetailGridExControl.MainGrid.Columns["InWhPosition"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            WhPositionEdit.Popup += WhPositionEdit_Popup;
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InputNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string inCustomerCode = lup_InCustomerCode.EditValue.GetNullToEmpty();
            string inId = lup_InId.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.InDate >= dt_InDate.DateFrEdit.DateTime
                                                                         && p.InDate <= dt_InDate.DateToEdit.DateTime)
                                                                         && (string.IsNullOrEmpty(inCustomerCode) ? true : p.InCustomerCode == inCustomerCode)
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
            var masterObj = MasterGridBindingSource.Current as TN_PUR1200;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_PUR1201List.OrderBy(p => p.InLotNo).ThenBy(p => p.InLotNo).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();

            DetailFocused();
        }

        private void MainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DetailFocused();
        }

        private void DetailFocused()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1200;
            if (masterObj == null)
            {
                DetailGridExControl.BarTools.ItemLinks[5].ImageOptions.Image = IconImageList.GetIconImage("actions/apply");
                DetailGridExControl.BarTools.ItemLinks[5].Caption = LabelConvert.GetLabelText("InConfirm") + "[Alt+T]";
                return;
            }

            var detailObj = DetailGridBindingSource.Current as TN_PUR1201;
            if (detailObj == null)
            {
                DetailGridExControl.BarTools.ItemLinks[5].ImageOptions.Image = IconImageList.GetIconImage("actions/apply");
                DetailGridExControl.BarTools.ItemLinks[5].Caption = LabelConvert.GetLabelText("InConfirm") + "[Alt+T]";
                return;
            }

            if (detailObj.InConfirmFlag == "Y")
            {
                DetailGridExControl.BarTools.ItemLinks[5].ImageOptions.Image = IconImageList.GetIconImage("actions/cancel");
                DetailGridExControl.BarTools.ItemLinks[5].Caption = LabelConvert.GetLabelText("InConfirmCancel") + "[Alt+T]";
            }
            else
            {
                DetailGridExControl.BarTools.ItemLinks[5].ImageOptions.Image = IconImageList.GetIconImage("actions/apply");
                DetailGridExControl.BarTools.ItemLinks[5].Caption = LabelConvert.GetLabelText("InConfirm") + "[Alt+T]";
            }
        }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PUR1100, param, MasterAddRowPopupCallback);

            form.ShowPopup(true);
        }

        private void MasterAddRowPopupCallback(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var returnList = (List<TN_PUR1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var returnObj in returnList)
            {
                var newObj = (TN_PUR1200)MasterGridBindingSource.AddNew();
                newObj.InNo = DbRequestHandler.GetSeqDay("IN");
                newObj.PoNo = returnObj.PoNo;
                newObj.InDate = DateTime.Today;
                newObj.InId = GlobalVariable.LoginId;
                newObj.InCustomerCode = returnObj.PoCustomerCode;
                ModelService.Insert(newObj);
            }

            if (returnList.Count > 0)
            {
                SetIsFormControlChanged(true);
                MasterGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1200;
            if (masterObj == null) return;

            if (masterObj.TN_PUR1201List.Count > 0)
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
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFPUR1200, param, PopupRefreshCallback);
            form.ShowPopup(true);
        }

        /// <summary> 반품 </summary>
        private void BarButtonDisposal_ItemClick(object sender, ItemClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFPUR1203, param, PopupRefreshCallback);
            form.ShowPopup(true);
        }

        protected override void DetailAddRowClicked()
        {
            var obj = MasterGridBindingSource.Current as TN_PUR1200;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Constraint, obj.PoNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PUR1101, param, DetailAddRowPopupCallback);
            form.ShowPopup(true);
        }

        private void DetailAddRowPopupCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_PUR1200;
            if (masterObj == null) return;

            //var itemCodeCheck = string.Empty;
            //if (masterObj.TN_PUR1201List.Count > 0)
            //    itemCodeCheck = masterObj.TN_PUR1201List.First().ItemCode;

            var returnList = (List<TN_PUR1101>)e.Map.GetValue(PopupParameter.ReturnObject);
            //foreach (var v in returnList)
            //{
            //    if (!itemCodeCheck.IsNullOrEmpty())
            //    {
            //        if (itemCodeCheck != v.ItemCode)
            //        {
            //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_112));
            //            return;
            //        }
            //    }

            //    itemCodeCheck = v.ItemCode;
            //}
            
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
                //newObj.InLotNo = masterObj.InNo.ToString().Substring(0, 10) + newObj.InSeq.ToString().PadLeft(3, '0');
                newObj.InLotNo = DbRequestHandler.USP_GET_SEQ_IN_LOT_NO("MIN", masterObj.InNo.ToString().Substring(3, 6));
                newObj.Temp = newObj.InLotNo;
                newObj.NewRowFlag = "Y";
                newObj.InConfirmFlag = "N";
                newObj.PrintQty = 1;
                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();
                var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PositionCode == newObj.TN_STD1100.StockPosition).FirstOrDefault();
                if (WMS2000 != null)
                {
                    newObj.InWhCode = WMS2000.WhCode;
                    newObj.InWhPosition = WMS2000.PositionCode;
                }
                newObj.TN_PUR1200 = masterObj;
                newObj.TN_PUR1101 = ModelService.GetChildList<TN_PUR1101>(p => p.PoNo == v.PoNo && p.PoSeq == v.PoSeq).FirstOrDefault();
                masterObj.TN_PUR1201List.Add(newObj);
                DetailGridBindingSource.Add(newObj);
            }

            if (returnList.Count > 0)
            {
                SetIsFormControlChanged(true);
                DetailGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1200;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR1201;
            if (detailObj == null) return;

            if (detailObj.InConfirmFlag == "Y")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_56), LabelConvert.GetLabelText("InConfirm")));
                return;
            }

            var InInspectionState = DetailGridExControl.MainGrid.MainView.GetFocusedRowCellDisplayText("InInspectionState").GetNullToEmpty();
            if (InInspectionState != "대기")
            {
                //MessageBoxHandler.Show("수입검사 데이터가 존재하여 삭제할 수 없습니다.");
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("InDetailInfo"), LabelConvert.GetLabelText("InInspectionInfo"), LabelConvert.GetLabelText("InInspectionInfo")));
                return;
            }

            if (detailObj.TN_PUR1301List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("InDetailInfo"), LabelConvert.GetLabelText("OutInfo"), LabelConvert.GetLabelText("OutInfo")));
                return;
            }

            DetailGridBindingSource.RemoveCurrent();
            masterObj.TN_PUR1201List.Remove(detailObj);
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            MasterGridBindingSource.EndEdit();
            DetailGridBindingSource.EndEdit();

            SetSaveMessageCheck = false;

            #region 필수 체크

            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var masterList = MasterGridBindingSource.List as List<TN_PUR1200>;
                if (masterList.Count > 0)
                {
                    //var editDetailList = masterList.Where(p => p.TN_ORD1001List.Any(c => c.NewRowFlag == "Y" || c.EditRowFlag == "Y")).ToList();

                    if (masterList.Any(p => p.TN_PUR1201List.Any(c => c.InCustomerLotNo.IsNullOrEmpty())))
                    {
                        MessageBoxHandler.Show("납품처 LOT NO가 없는 항목이 존재합니다. 확인해 주시기 바랍니다.");
                        return;
                    }
                }
            }
            #endregion

            SetSaveMessageCheck = true;
            ModelService.Save();
            DataLoad();
        }

        /// <summary> 구매현황 </summary>
        private void Btn_PurchaseStatus_Click(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PUR_STATUS, param, null);
            form.ShowPopup(false);
        }

        /// <summary> 입고확정/취소 </summary>
        private void BarButtonInConfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DetailGridBindingSource == null) return;
            if (!UserRight.HasEdit) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR1201;
            if (detailObj == null) return;

            var InInspectionState = DetailGridExControl.MainGrid.MainView.GetFocusedRowCellDisplayText("InInspectionState").GetNullToEmpty();
            if (InInspectionState != "대기" && detailObj.InConfirmFlag == "Y")
            {
                //MessageBoxHandler.Show("수입검사 데이터가 존재하여 수정할 수 없습니다.");
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_52), LabelConvert.GetLabelText("InInspectionInfo")));
            }
            else
            {
                detailObj.InConfirmFlag = detailObj.InConfirmFlag == "Y" ? "N" : "Y";
                DetailGridExControl.BestFitColumns();
                DetailFocused();
            }
        }

        /// <summary> 분할처리 </summary>
        private void BarButtonDevide_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!UserRight.HasEdit) return;

            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            var obj = MasterGridBindingSource.Current as TN_PUR1200;
            if (obj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR1201;
            if (detailObj == null) return;

            if (detailObj.InLotNo.Length != 13)
            {
                MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_118));
                //MessageBoxHandler.Show("이미 분할처리된 항목입니다. 확인 부탁드립니다.");
                return;
            }

            //if (detailObj.NewRowFlag == "Y")
            //{
            //    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_99));
            //    return;
            //}

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, detailObj);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFPUR1201, param, AddDevide1201);
            form.ShowPopup(true);
        }

        private void AddDevide1201(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;
            var masterObj = MasterGridBindingSource.Current as TN_PUR1200;
            var returnList = (List<TN_PUR1201>)e.Map.GetValue(PopupParameter.ReturnObject);
            var updateObj = e.Map.GetValue(PopupParameter.Value_1) as TN_PUR1201;
            var realUpdateObj = masterObj.TN_PUR1201List.Where(p => p.InLotNo == updateObj.InLotNo).First();
            realUpdateObj.InQty = updateObj.InQty;
            realUpdateObj.InCost = updateObj.InCost;
            realUpdateObj.PrintQty = updateObj.PrintQty;
            realUpdateObj.InWhCode = updateObj.InWhCode;
            realUpdateObj.InWhPosition = updateObj.InWhPosition;
            realUpdateObj.Memo = updateObj.Memo;
            realUpdateObj.InLotNo += "-01";

            int i = 2;
            foreach (var returnObj in returnList.Where(p => p.InLotNo.IsNullOrEmpty()).ToList())
            {
                TN_PUR1201 obj = (TN_PUR1201)DetailGridBindingSource.AddNew();
                obj.InNo = masterObj.InNo;
                obj.InSeq = masterObj.TN_PUR1201List.Count == 0 ? 1 : masterObj.TN_PUR1201List.OrderBy(o => o.InSeq).LastOrDefault().InSeq + 1;
                obj.PoNo = realUpdateObj.PoNo;
                obj.PoSeq = realUpdateObj.PoSeq;
                obj.ItemCode = realUpdateObj.ItemCode;
                obj.InQty = returnObj.InQty;
                obj.InCost = returnObj.InCost;
                obj.InLotNo = realUpdateObj.Temp + "-" + i.ToString().PadLeft(2, '0');
                obj.Temp = realUpdateObj.Temp;
                obj.NewRowFlag = "Y";
                obj.InConfirmFlag = "N";
                obj.Memo = returnObj.Memo;
                obj.PrintQty = returnObj.PrintQty;
                obj.InWhCode = returnObj.InWhCode;
                obj.InWhPosition = returnObj.InWhPosition;
                obj.InCustomerLotNo = realUpdateObj.InCustomerLotNo;
                obj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == realUpdateObj.ItemCode).First();
                masterObj.TN_PUR1201List.Add(obj);

                i++;
            }
            SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.MainView.RefreshData();
            DetailGridExControl.MainGrid.BestFitColumns();
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

                var detailList = DetailGridBindingSource.List as List<TN_PUR1201>;
                if (detailList == null) return;

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

                var mainReport = new REPORT.XRPUR1201();
                foreach (var v in printList.OrderBy(p => p.InLotNo.Length).ThenBy(p => p.InLotNo).ToList())
                {
                    var printQty = v.PrintQty.GetIntNullToZero();
                    if (printQty <= 1)
                    {
                        var report = new REPORT.XRPUR1201(v);
                        report.CreateDocument();
                        mainReport.Pages.AddRange(report.Pages);
                    }
                    else
                    {
                        for (int i = 0; i < printQty; i++)
                        {
                            var report = new REPORT.XRPUR1201(v);
                            report.CreateDocument();
                            mainReport.Pages.AddRange(report.Pages);
                        }
                    }

                    //if (v.PrintQty.GetIntNullToZero() > 0)
                    //{
                    //    #region 단위수량이 있을 경우
                    //    decimal? loopPrint = v.InQty.GetIntNullToZero() / (v.PrintQty.GetIntNullToZero() == 0 ? 1 : v.PrintQty.GetIntNullToZero());
                    //    decimal? modCheck = v.InQty.GetIntNullToZero() % (v.PrintQty.GetIntNullToZero() == 0 ? 1 : v.PrintQty.GetIntNullToZero());
                    //    #region 나머지 수량까지 있을 경우
                    //    if (modCheck > 0)
                    //    {
                    //        decimal? oldQty = v.InQty;
                    //        decimal? unitQty = v.PrintQty.GetIntNullToZero();
                    //        for (int i = 0; i < loopPrint; i++)
                    //        {
                    //            var report1 = new REPORT.XRPUR1201(v, unitQty.GetDecimalNullToZero());
                    //            report1.CreateDocument();
                    //            mainReport.Pages.AddRange(report1.Pages);
                    //        }

                    //        decimal? remainQty = oldQty - (unitQty * loopPrint);

                    //        var report2 = new REPORT.XRPUR1201(v, remainQty.GetDecimalNullToZero());
                    //        report2.CreateDocument();
                    //        mainReport.Pages.AddRange(report2.Pages);
                    //    }
                    //    #endregion
                    //    else
                    //    {
                    //        decimal? unitQty = v.PrintQty.GetIntNullToZero();
                    //        for (int i = 0; i < loopPrint; i++)
                    //        {
                    //            var report3 = new REPORT.XRPUR1201(v, unitQty.GetDecimalNullToZero());
                    //            report3.CreateDocument();
                    //            mainReport.Pages.AddRange(report3.Pages);
                    //        }
                    //    }
                    //    #endregion
                    //}
                    //else
                    //{
                    //    var report4 = new REPORT.XRPUR1201(v);
                    //    report4.CreateDocument();
                    //    mainReport.Pages.AddRange(report4.Pages);
                    //}
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

        /// <summary> 납품처LOTNO스캔버튼 </summary>
        private void BarButtonCustomerLotNoScan_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new PUR_POPUP.XPFPUR1202();
            form.Show();
        }

        private void BarCheckBoxEdit_CheckedChanged(object sender, ItemClickEventArgs e)
        {

        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1200;
            if (masterObj == null) return;

            if (masterObj.TN_PUR1201List.Any(p => p.InConfirmFlag == "Y"))
            {
                e.Cancel = true;
            }
            else
            {
                bool check = false;
                for (int i = 0; i < masterObj.TN_PUR1201List.Count; i++)
                {
                    var inInspectionState = DetailGridExControl.MainGrid.MainView.GetFocusedRowCellDisplayText("InInspectionState").GetNullToEmpty();
                    if (inInspectionState != "대기")
                    {
                        check = true;
                    }
                }

                e.Cancel = check;
            }
        }

        private void DetailMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            string fieldName = view.FocusedColumn.FieldName;

            var masterObj = MasterGridBindingSource.Current as TN_PUR1200;
            if (masterObj == null) return;
            var detailObj = DetailGridBindingSource.Current as TN_PUR1201;
            if (detailObj == null) return;

            if (fieldName != "_Check")
            {
                if (detailObj.InConfirmFlag == "Y")
                {
                    e.Cancel = true;
                }
                else
                {
                    if (view.GetFocusedRowCellDisplayText("InInspectionState").GetNullToEmpty() != "대기")
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "InInspectionState")
            {
                var InLotNo = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "Temp").GetNullToEmpty();
                var TN_QCT1100 = ModelService.GetChildList<TN_QCT1100>(p => p.CheckDivision == MasterCodeSTR.InspectionDivision_IN && p.InLotNo == InLotNo).FirstOrDefault();
                if (TN_QCT1100 != null)
                {
                    e.DisplayText = TN_QCT1100.CheckResult;
                }
                else
                {
                    e.DisplayText = "대기";
                }
            }
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_PUR1201;
            if (detailObj == null) return;

            if (e.Column.FieldName == "InWhCode")
            {
                detailObj.InWhPosition = null;
            }
            else if (e.Column.FieldName == "InCustomerLotNo")
            {
                if (!detailObj.Temp.IsNullOrEmpty())
                {
                    if (!InCustomerCodeEditCheckFlag)
                    {
                        var detailList = DetailGridBindingSource.List as List<TN_PUR1201>;
                        var temp = DetailGridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle, "Temp").GetNullToEmpty();
                        var sameWorkNoList = detailList.Where(p => p.Temp == temp).ToList();
                        if (sameWorkNoList.Count > 1)
                        {
                            foreach (var v in sameWorkNoList)
                                v.InCustomerLotNo = e.Value.GetNullToNull();
                            DetailGridExControl.BestFitColumns();
                        }
                    }
                }
            }
            else if (e.Column.FieldName == "InCost")
            {
                var detailList = DetailGridBindingSource.List as List<TN_PUR1201>;
                var sameWorkNoList = detailList.Where(p => p.Temp == detailObj.Temp).ToList();
                if (sameWorkNoList.Count > 1)
                {
                    foreach (var v in sameWorkNoList)
                        v.InCost = e.Value.GetDecimalNullToNull();
                    DetailGridExControl.BestFitColumns();
                }
            }

            detailObj.EditRowFlag = "Y";
        }

        private void WhPositionEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_PUR1201;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.InWhCode + "'";
        }
    }
}