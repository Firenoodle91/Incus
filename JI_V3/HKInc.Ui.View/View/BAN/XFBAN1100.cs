﻿using System;
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
using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.View.View.REPORT;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.View.BAN
{
    /// <summary>
    /// 반제품출고관리
    /// </summary>
    public partial class XFBAN1100 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_BAN1100> ModelService = (IService<TN_BAN1100>)ProductionFactory.GetDomainService("TN_BAN1100");

        public XFBAN1100()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            //MasterGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailMainView_ShowingEditor;

            dt_OutDate.SetTodayIsMonth();
        }

        protected override void InitCombo()
        {
            lup_OutId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"));
            MasterGridExControl.MainGrid.AddColumn("OutDate", LabelConvert.GetLabelText("OutDate"));
            MasterGridExControl.MainGrid.AddColumn("OutId", LabelConvert.GetLabelText("OutId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutDate", "Memo");

            //IsDetailGridButtonFileChooseEnabled = true;
            //DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("ItemMovePrint") + "[Alt+R]", IconImageList.GetIconImage("print/printer"));
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "OutLotNo", true);
            DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            DetailGridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"), false);
            DetailGridExControl.MainGrid.AddColumn("OutSeq", LabelConvert.GetLabelText("OutSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"), true);
            DetailGridExControl.MainGrid.AddColumn("TN_BAN1001.BanProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            DetailGridExControl.MainGrid.AddColumn("TN_BAN1001.InWhCode", LabelConvert.GetLabelText("InWhCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_BAN1001.InWhPosition", LabelConvert.GetLabelText("InWhPosition"));
            DetailGridExControl.MainGrid.AddColumn("OutLotNo", LabelConvert.GetLabelText("OutLotNo"));
            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("CustomStockQty", "시스템사용(재고량)", HorzAlignment.Far, FormatType.Numeric, "#,#.##", false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "OutQty", "Memo");

            var barButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonPrint.Id = 4;
            barButtonPrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            barButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barButtonPrint.Name = "barButtonPrint";
            barButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonPrint.ShortcutKeyDisplayString = "Alt+P";
            barButtonPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonPrint.Caption = LabelConvert.GetLabelText("BarcodePrint") + "[Alt+P]";
            barButtonPrint.ItemClick += BarButtonPrint_ItemClick; ;
            barButtonPrint.Alignment = BarItemLinkAlignment.Right;
            DetailGridExControl.BarTools.AddItem(barButtonPrint);

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
            barTextEditBarCodeStaticItem.Edit.NullText = LabelConvert.GetLabelText("InLotNo") + ":";
            barTextEditBarCodeStaticItem.EditWidth = barTextEditBarCodeStaticItem.Edit.NullText.Length * 9;
            barTextEditBarCodeStaticItem.Enabled = false;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.ForeColor = Color.Black;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Far;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.BackColor = Color.Transparent;
            barTextEditBarCodeStaticItem.Edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Alignment = BarItemLinkAlignment.Left;
            
            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCode);
            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCodeStaticItem);
            
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_BAN1100>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_BAN1101>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OutQty", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_BAN1001.InWhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_BAN1001.InWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);

            var outQtyEdit = DetailGridExControl.MainGrid.Columns["OutQty"].ColumnEdit as RepositoryItemSpinEdit;
            outQtyEdit.EditValueChanged += OutQtyEdit_EditValueChanged;

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }
        
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("OutNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            //데이터리로드
            InitRepository();
            InitCombo();

            string outId = lup_OutId.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutDate >= dt_OutDate.DateFrEdit.DateTime
                                                                         && p.OutDate <= dt_OutDate.DateToEdit.DateTime)
                                                                         && (string.IsNullOrEmpty(outId) ? true : p.OutId == outId))
                                                                      .OrderBy(o => o.OutDate)
                                                                      .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_BAN1100;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_BAN1101List.OrderBy(p => p.OutSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            var newobj = new TN_BAN1100()
            {
                OutNo = DbRequestHandler.GetSeqMonth("BOUT"),
                OutDate = DateTime.Today,
                OutId = GlobalVariable.LoginId,
            };
            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            MasterGridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_BAN1100;
            if (masterObj == null) return;

            if (masterObj.TN_BAN1101List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("OutMasterInfo"), LabelConvert.GetLabelText("OutDetailInfo"), LabelConvert.GetLabelText("OutDetailInfo")));
                return;
            }

            ModelService.Delete(masterObj);
            MasterGridBindingSource.RemoveCurrent();
            MasterGridExControl.BestFitColumns();
        }

        private void Edit_KeyDown(object sender, KeyEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_BAN1100;
            if (masterObj == null) return;

            var detailList = DetailGridBindingSource.List as List<TN_BAN1101>;
            if (detailList == null) return;

            
            if (e.KeyCode == Keys.Enter)
            {
                var textEdit = sender as TextEdit;
                if (textEdit == null) return;

                var inLotNo = textEdit.EditValue.GetNullToEmpty().ToUpper();
                if (inLotNo.IsNullOrEmpty())
                {

                }
                else
                {
                    IService<TN_BAN1100> LocalService = (IService<TN_BAN1100>)ProductionFactory.GetDomainService("TN_BAN1100");

                    var tN_BAN1001 = LocalService.GetChildList<TN_BAN1001>(p => p.InLotNo == inLotNo).FirstOrDefault();
                    if (tN_BAN1001 == null)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InLotNo")));
                    }
                    else
                    {
                        var ItemCode = new SqlParameter("@ItemCode", tN_BAN1001.ItemCode);
                        var InLotNo = new SqlParameter("@InLotNo", inLotNo);

                        var stockObj = ModelService.GetChildList<VI_BAN_STOCK_IN_LOT_NO>(p => p.InLotNo == inLotNo).FirstOrDefault();

                        if (stockObj.StockQty <= 0)
                        {
                            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_35), LabelConvert.GetLabelText("StockQty")));
                        }
                        else
                        {
                            var sumInLotQty = detailList.Where(x => x.InLotNo == inLotNo && x.NewRowFlag == "Y").Sum(s => s.OutQty);

                            if (stockObj.StockQty <= sumInLotQty)
                            {
                                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("OutPossibleQty")));
                            }
                            else
                            {
                                var newObj = (TN_BAN1101)DetailGridBindingSource.AddNew();
                                newObj.OutNo = masterObj.OutNo;
                                newObj.OutSeq = masterObj.TN_BAN1101List.Count == 0 ? 1 : masterObj.TN_BAN1101List.Max(o => o.OutSeq) + 1;
                                newObj.InNo = tN_BAN1001.InNo;
                                newObj.InSeq = tN_BAN1001.InSeq;
                                newObj.ItemCode = tN_BAN1001.ItemCode;
                                //newObj.OutQty = stockObj.StockQty.GetDecimalNullToZero();
                                newObj.OutQty = stockObj.StockQty.GetDecimalNullToZero() - sumInLotQty;
                                newObj.OutLotNo = masterObj.OutNo.ToString().Substring(0, 10) + masterObj.OutNo.ToString().Substring(11, 4) + newObj.OutSeq.ToString().PadLeft(3, '0');
                                newObj.InLotNo = inLotNo;
                                newObj.NewRowFlag = "Y";
                                newObj.TN_BAN1100 = masterObj;
                                newObj.TN_BAN1001 = ModelService.GetChildList<TN_BAN1001>(p => p.InLotNo == inLotNo).FirstOrDefault();
                                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).FirstOrDefault();
                                masterObj.TN_BAN1101List.Add(newObj);

                                DetailGridExControl.BestFitColumns();
                            }
                        }
                    }
                }

                textEdit.EditValue = "";
                e.Handled = true;
            }
        }
        
        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_BAN1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_BAN1101;
            if (detailObj == null) return;

            if (ModelService.GetChildList<TN_LOT_DTL>(p => p.SrcInLotNo == detailObj.OutLotNo).FirstOrDefault() != null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_51), LabelConvert.GetLabelText("ProductionInputInfo")));
                return;
            }

            if (detailObj.TN_BAN1102List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_51), LabelConvert.GetLabelText("Return")));
                return;
            }

            masterObj.TN_BAN1101List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_BAN1100;
            if (masterObj == null) return;

            //if (!masterObj.TN_BAN1101List.Any(c => c.NewRowFlag == "Y"))
            //{
            //    e.Cancel = true;
            //}

            if (masterObj.TN_BAN1101List.Count > 0)
            {
                e.Cancel = true;
            }
        }

        private void DetailMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            string fieldName = view.FocusedColumn.FieldName;

            var masterObj = MasterGridBindingSource.Current as TN_BAN1100;
            if (masterObj == null) return;
            var detailObj = DetailGridBindingSource.Current as TN_BAN1101;
            if (detailObj == null) return;

            if (fieldName != "_Check" && fieldName != "Memo")
            {
                var tN_LOT_DTL = ModelService.GetChildList<TN_LOT_DTL>(x => x.SrcInLotNo == detailObj.OutLotNo).FirstOrDefault();

                if (tN_LOT_DTL != null || detailObj.TN_BAN1102List.Count > 0)
                {
                    e.Cancel = true;
                }
            }
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

                var detailList = DetailGridBindingSource.List as List<TN_BAN1101>;
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

                var mainReport = new REPORT.XRBAN1101();
                foreach (var v in printList.OrderByDescending(p => p.InSeq).ToList())
                {
                    var report = new REPORT.XRBAN1101(v);
                    report.CreateDocument();
                    mainReport.Pages.AddRange(report.Pages);
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

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = sender as GridView;

            var masterObj = MasterGridBindingSource.Current as TN_BAN1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_BAN1101;
            if (detailObj == null) return;

            if (detailObj.NewRowFlag == "Y" && e.Column.FieldName == "OutQty")
            {
                var detailList = DetailGridBindingSource.List as List<TN_BAN1101>;
                var stockObj = ModelService.GetChildList<VI_BAN_STOCK_IN_LOT_NO>(p => p.InLotNo == detailObj.InLotNo).FirstOrDefault();
                var productStock = detailList.Where(x => x.NewRowFlag == "Y" && x.InLotNo == detailObj.InLotNo).Sum(s => s.OutQty);

                //if (stockObj.StockQty < detailObj.OutQty)
                if(stockObj.StockQty - productStock < 0)
                {
                    //MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_86), stockObj.StockQty - productStock));
                    MessageBoxHandler.Show(string.Format("출고량을 확인 하세요."));
                    detailObj.OutQty = 0;
                }
                DetailGridExControl.BestFitColumns();
            }
        }

        private void OutQtyEdit_EditValueChanged(object sender, EventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_BAN1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_BAN1101;
            if (detailObj == null) return;

            var stockObj = ModelService.GetChildList<VI_BAN_STOCK_IN_LOT_NO>(p => p.InLotNo == detailObj.InLotNo).FirstOrDefault();
            if (stockObj == null) return;

            if (detailObj.NewRowFlag != "Y")
            {
                var oldQty = DetailGridExControl.MainGrid.MainView.ActiveEditor.OldEditValue.GetIntNullToZero();
                var nowQty = DetailGridExControl.MainGrid.MainView.ActiveEditor.EditValue.GetIntNullToZero();

                if (detailObj.CustomStockQty == null)
                {
                    detailObj.CustomStockQty = (stockObj.StockQty + oldQty).GetIntNullToZero();
                }

                if (detailObj.CustomStockQty < nowQty)
                {
                    try
                    {
                        var outQtyEdit = DetailGridExControl.MainGrid.Columns["OutQty"].ColumnEdit as RepositoryItemSpinEdit;
                        outQtyEdit.EditValueChanged -= OutQtyEdit_EditValueChanged;
                        //DetailGridExControl.MainGrid.MainView.ActiveEditor.EditValue = 0;
                        DetailGridExControl.MainGrid.MainView.ActiveEditor.EditValue = oldQty;
                        DetailGridExControl.MainGrid.PostEditor();
                        outQtyEdit.EditValueChanged += OutQtyEdit_EditValueChanged;

                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_86), detailObj.CustomStockQty));
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHandler.ErrorShow(ex);
                    }
                }
            }
        }

        #region 사용안함
        private void Edit_KeyDown_OLD(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var textEdit = sender as TextEdit;
                if (textEdit == null) return;

                var detailList = DetailGridBindingSource.List as List<TN_BAN1101>;
                if (detailList == null) return;

                var masterObj = MasterGridBindingSource.Current as TN_BAN1100;
                if (masterObj != null)
                {
                    var inLotNo = textEdit.EditValue.GetNullToNull().ToUpper();
                    if (inLotNo.IsNullOrEmpty()) return;

                    IService<TN_BAN1100> LocalService = (IService<TN_BAN1100>)ProductionFactory.GetDomainService("TN_BAN1100");

                    var TN_BAN1001 = LocalService.GetChildList<TN_BAN1001>(p => p.InLotNo == inLotNo).FirstOrDefault();
                    if (TN_BAN1001 == null)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InLotNo")));
                    }
                    else
                    {
                        if (detailList.Any(p => p.InLotNo == inLotNo))
                        {
                            DetailGridExControl.MainGrid.MainView.FocusedRowHandle = DetailGridExControl.MainGrid.MainView.LocateByValue("InLotNo", inLotNo);

                        }

                        else
                        {
                            var ItemCode = new SqlParameter("@ItemCode", TN_BAN1001.ItemCode);
                            var InLotNo = new SqlParameter("@InLotNo", inLotNo);
                            //var fifoDataSet = DbRequestHandler.GetDataSet("USP_GET_PUR_FIFO", ItemCode, InLotNo);

                            var stockObj = ModelService.GetChildList<VI_BAN_STOCK_IN_LOT_NO>(p => p.InLotNo == inLotNo).FirstOrDefault();

                            if (stockObj.StockQty <= 0)
                            {
                                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_35), LabelConvert.GetLabelText("StockQty")));
                            }
                            else
                            {
                                var newObj = (TN_BAN1101)DetailGridBindingSource.AddNew();
                                newObj.OutNo = masterObj.OutNo;
                                newObj.OutSeq = masterObj.TN_BAN1101List.Count == 0 ? 1 : masterObj.TN_BAN1101List.Max(o => o.OutSeq) + 1;
                                newObj.InNo = TN_BAN1001.InNo;
                                newObj.InSeq = TN_BAN1001.InSeq;
                                newObj.ItemCode = TN_BAN1001.ItemCode;
                                newObj.OutQty = stockObj.StockQty.GetDecimalNullToZero();
                                newObj.OutLotNo = inLotNo;
                                newObj.InLotNo = inLotNo;
                                newObj.NewRowFlag = "Y";
                                newObj.TN_BAN1100 = masterObj;
                                newObj.TN_BAN1001 = ModelService.GetChildList<TN_BAN1001>(p => p.InLotNo == inLotNo).FirstOrDefault();
                                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).FirstOrDefault();
                                masterObj.TN_BAN1101List.Add(newObj);

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

        protected override void DetailFileChooseClicked()
        {
            //if (DetailGridBindingSource == null || DetailGridBindingSource.DataSource == null) return;

            //var detailList = DetailGridBindingSource.List as List<TN_BAN1101>;
            //var checkList = detailList.Where(p => p._Check == "Y").ToList();
            //if (checkList.Count > 0)
            //{
            //    TEMP_ITEM_MOVE_NO_MASTER masterObj = null;
            //    List<TEMP_ITEM_MOVE_NO_DETAIL> printDetailList = null;

            //    var mainReport = new XRITEMMOVENO();

            //    foreach (var v in checkList)
            //    {
            //        using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            //        {
            //            var _workNo = new SqlParameter("@WorkNo", v.WorkNo);
            //            masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER @WorkNo", _workNo).Where(p => p.ItemMoveNo == v.TN_PUR1401.ItemMoveNo).FirstOrDefault();
            //        }

            //        if (masterObj == null) continue;

            //        using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            //        {
            //            var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
            //            var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

            //            printDetailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
            //        }
            //        var ItemMoveNoReport = new XRITEMMOVENO(masterObj, printDetailList);
            //        ItemMoveNoReport.CreateDocument();
            //        mainReport.Pages.AddRange(ItemMoveNoReport.Pages);
            //    }

            //    if (mainReport.Pages.Count > 0)
            //    {
            //        var printTool = new ReportPrintTool(mainReport);
            //        printTool.ShowPreview();
            //    }
            //}
        }
        #endregion
    }
}