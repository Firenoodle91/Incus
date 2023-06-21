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
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Service.Helper;
using DevExpress.XtraBars;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using DevExpress.XtraEditors.Repository;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Handler;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.PUR_POPUP
{
    /// <summary>
    /// 재입고 팝업
    /// </summary>
    public partial class XPFPUR1200 : Service.Base.PopupCallbackFormTemplate
    {
        private IService<TN_PUR1302> ModelService = (IService<TN_PUR1302>)ProductionFactory.GetDomainService("TN_PUR1302");

        public XPFPUR1200()
        {
            InitializeComponent();
        }

        public XPFPUR1200(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("ReturnInfo");

            GridExControl = gridEx1;
            GridExControl.ActDeleteRowClicked += GridExControl_ActDeleteRowClicked;
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(UserRight.HasEdit);
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            //GridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"), false);
            //GridExControl.MainGrid.AddColumn("InSeq", LabelConvert.GetLabelText("InSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("InInspectionState", LabelConvert.GetLabelText("InInspectionState"), HorzAlignment.Center, true);
            //GridExControl.MainGrid.AddColumn("InConfirmFlag", LabelConvert.GetLabelText("InConfirmFlag"));
            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "TempCheckColumn", true);
            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Print"));
            GridExControl.MainGrid.AddColumn("TempCheckColumn", LabelConvert.GetLabelText("TempCheckColumn"), false);            
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            GridExControl.MainGrid.AddColumn("ReturnDate", LabelConvert.GetLabelText("ReturnDate2"));
            GridExControl.MainGrid.AddColumn("ReturnId", LabelConvert.GetLabelText("ReturnId"));
            GridExControl.MainGrid.AddColumn("TN_PUR1301.TN_PUR1201.InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("ReturnPossibleQty", LabelConvert.GetLabelText("ReturnPossibleQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("ReturnQty", LabelConvert.GetLabelText("ReturnQty"));
            //GridExControl.MainGrid.AddColumn("InCost", LabelConvert.GetLabelText("InCost"));
            //GridExControl.MainGrid.AddUnboundColumn("InAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([InQty],0) * ISNULL([InCost],0)", FormatType.Numeric, "#,###,###,###.##");
            GridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            GridExControl.MainGrid.AddColumn("OutLotNo", LabelConvert.GetLabelText("OutLotNo"));
            //GridExControl.MainGrid.AddColumn("PrintQty", LabelConvert.GetLabelText("PrintQty2"));
            GridExControl.MainGrid.AddColumn("TN_PUR1301.TN_PUR1201.InCustomerLotNo", LabelConvert.GetLabelText("InCustomerLotNo"));
            GridExControl.MainGrid.AddColumn("ReturnWhCode", LabelConvert.GetLabelText("ReturnWhCode"));
            GridExControl.MainGrid.AddColumn("ReturnWhPosition", LabelConvert.GetLabelText("ReturnWhPosition"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ReturnDate", "ReturnId", "ReturnQty", "Memo");

            var barTextEditBarCode = new DevExpress.XtraBars.BarEditItem(GridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCode.Id = 5;
            barTextEditBarCode.Enabled = UserRight.HasEdit;
            barTextEditBarCode.Name = "barTextEditBarCode";
            barTextEditBarCode.EditWidth = 130;
            barTextEditBarCode.Edit.KeyDown += Edit_KeyDown;
            //DetailGridExControl.BarTools.AddItem(barTextEditBarCode);

            var barTextEditBarCodeStaticItem = new DevExpress.XtraBars.BarEditItem(GridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCodeStaticItem.Id = 6;
            barTextEditBarCodeStaticItem.Name = "barTextEditBarCodeStaticItem";
            barTextEditBarCodeStaticItem.Edit.NullText = LabelConvert.GetLabelText("OutLotNo") + ":";
            barTextEditBarCodeStaticItem.EditWidth = barTextEditBarCodeStaticItem.Edit.NullText.Length * 9;
            //barTextEditBarCodeStaticItem.EditWidth = 120;
            barTextEditBarCodeStaticItem.Enabled = false;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.ForeColor = Color.Black;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Far;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.BackColor = Color.Transparent;
            barTextEditBarCodeStaticItem.Edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Alignment = BarItemLinkAlignment.Left;

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
            GridExControl.BarTools.AddItem(barButtonPrint);

            GridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCode);
            GridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCodeStaticItem);
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ReturnDate");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("ReturnQty", DefaultBoolean.Default, "n2");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnWhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);

        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            ModelBindingSource.DataSource = ModelService.GetList(p => false).ToList();

            GridExControl.DataSource = ModelBindingSource;

            GridExControl.BestFitColumns();
        }

        private void GridExControl_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_PUR1302;
            if (obj == null) return;

            ModelService.Delete(obj);
            ModelBindingSource.RemoveCurrent();

            GridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;

            if (ModelBindingSource == null) return;

            GridExControl.MainGrid.PostEditor();
            ModelBindingSource.EndEdit();

            var detailList = ModelBindingSource.List as List<TN_PUR1302>;
            if (detailList.Count == 0) return;

            foreach (var v in detailList)
            {
                v._Check = "Y";
            }
            ModelBindingSource.EndEdit();

            #region 재공재고 떨구기
            foreach (var v in detailList)
            {
                string sql = "";
                sql = "INSERT INTO TN_SRC1001T (STOCK_ADJUST_DATE, SRC_CODE, IN_QTY, USE_QTY, INS_DATE, INS_ID)" +
                      "VALUES ( '" + DateTime.Today.ToString().Substring(0, 10) + "', " +
                      "'" + v.ItemCode.GetNullToEmpty() + "', " +
                      "'" + "0" + "', " +
                      "'" + (v.ReturnPossibleQty.GetDecimalNullToZero() - v.ReturnQty.GetDecimalNullToZero()).ToString() + "', " +
                      "'" + DateTime.Now.Date.ToString().Substring(0, 10) + "', " +
                      "'" + GlobalVariable.LoginId + "')";
                int i = DbRequestHandler.SetDataQury(sql);
            }
            #endregion

            ModelService.Save();

            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_62));
            BarButtonPrint_ItemClick(null, null);

            IsFormControlChanged = false;
            SetSaveMessageCheck = true;
            ActClose();
        }

        private void Edit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var textEdit = sender as TextEdit;
                if (textEdit == null) return;

                var outLotNo = textEdit.EditValue.GetNullToNull().ToUpper();
                if (outLotNo.IsNullOrEmpty())
                {
                    textEdit.EditValue = "";
                    textEdit.Focus();
                }
                else
                {
                    IService<TN_PUR1300> LocalService = (IService<TN_PUR1300>)ProductionFactory.GetDomainService("TN_PUR1300");

                    var TN_PUR1301 = ModelService.GetChildList<TN_PUR1301>(p => p.OutLotNo == outLotNo).FirstOrDefault();
                    if (TN_PUR1301 == null)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("OutLotNo")));
                        textEdit.EditValue = "";
                        textEdit.Focus();
                    }
                    else
                    {
                        var VI_RETURN_OBJECT = LocalService.GetChildList<VI_RETURN_OBJECT>(p => p.OutLotNo == outLotNo).FirstOrDefault();
                        //if (VI_RETURN_OBJECT.ReturnPossibleQty <= 0)
                        //{
                        //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("ReturnInPossibleQty")));
                        //    textEdit.EditValue = "";
                        //    textEdit.Focus();
                        //}
                        //else
                        //{
                        var newObj = (TN_PUR1302)ModelBindingSource.AddNew();
                        newObj.OutNo = TN_PUR1301.OutNo;
                        newObj.OutSeq = TN_PUR1301.OutSeq;
                        newObj.TN_PUR1301 = TN_PUR1301;
                        newObj.ItemCode = TN_PUR1301.ItemCode;
                        newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).FirstOrDefault();
                        newObj.OutLotNo = TN_PUR1301.OutLotNo;
                        newObj.InLotNo = TN_PUR1301.InLotNo;
                        newObj.ReturnQty = 0;
                        newObj.ReturnId = GlobalVariable.LoginId;
                        newObj.ReturnDate = DateTime.Today;
                        newObj.ReturnWhCode = TN_PUR1301.TN_PUR1201.InWhCode;
                        newObj.ReturnWhPosition = TN_PUR1301.TN_PUR1201.InWhPosition;
                        newObj.ReturnPossibleQty = VI_RETURN_OBJECT.ReturnPossibleQty;
                        newObj.NewRowFlag = "Y";
                        newObj.TempCheckColumn = (ModelBindingSource.List.Count + 1).ToString();
                        ModelService.Insert(newObj);

                        textEdit.EditValue = "";
                        textEdit.Focus();
                        e.Handled = true;
                        //}
                    }
                    GridExControl.BestFitColumns();

                    LocalService.Dispose();
                }
            }
        }

        /// <summary> 현품표출력버튼 </summary>
        private void BarButtonPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (ModelBindingSource == null) return;

                GridExControl.MainGrid.PostEditor();;

                WaitHandler.ShowWait();

                var detailList = ModelBindingSource.List as List<TN_PUR1302>;
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
                foreach (var v in printList.OrderByDescending(p => p.TempCheckColumn).ToList())
                {
                    var printQty = 1;
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
                    v._Check = "N";
                }
                GridExControl.BestFitColumns();
                mainReport.PrintingSystem.ShowMarginsWarning = false;
                mainReport.ShowPrintStatusDialog = false;
                mainReport.ShowPreview();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }
    }
}