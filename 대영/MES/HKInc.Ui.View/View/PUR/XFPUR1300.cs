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
using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.View.PUR
{
    /// <summary>
    /// 자재출고관리
    /// </summary>
    public partial class XFPUR1300 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR1300> ModelService = (IService<TN_PUR1300>)ProductionFactory.GetDomainService("TN_PUR1300");

        public XFPUR1300()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            btn_PurchaseStatus.Click += Btn_PurchaseStatus_Click;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;

            dt_OutDate.SetTodayIsMonth();
        }

        protected override void InitCombo()
        {          

            lup_OutId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.ScmYn == "N" && p.Active == "Y" && p.MainYn == "02").ToList());

            btn_PurchaseStatus.Text = LabelConvert.GetLabelText("PurchaseStatus") + "(&F)";
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"));
            MasterGridExControl.MainGrid.AddColumn("OutDate", LabelConvert.GetLabelText("OutProcDate"));
            MasterGridExControl.MainGrid.AddColumn("OutId", LabelConvert.GetLabelText("OutId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutDate", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, false);
            DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "OutLotNo", true);
            DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            DetailGridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"), false);
            DetailGridExControl.MainGrid.AddColumn("OutSeq", LabelConvert.GetLabelText("OutSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", LabelConvert.GetLabelText("BottomCategory"), false);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
            DetailGridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            DetailGridExControl.MainGrid.AddColumn("InCustomerLotNo", LabelConvert.GetLabelText("InCustomerLotNo"));
            DetailGridExControl.MainGrid.AddColumn("TN_PUR1201.InWhCode", LabelConvert.GetLabelText("InWhCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_PUR1201.InWhPosition", LabelConvert.GetLabelText("InWhPosition"));
            DetailGridExControl.MainGrid.AddColumn("OutLotNo", LabelConvert.GetLabelText("OutLotNo"));
            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("CustomStockQty", "시스템사용(재고량)", HorzAlignment.Far, FormatType.Numeric, "#,#.##", false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "OutQty", "Memo", "MachineCode");

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

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1300>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1301>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OutQty", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", "MachineName", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_PUR1201.InWhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_PUR1201.InWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);
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

            InitCombo(); //phs20210624
            InitRepository();//phs20210624


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
            var masterObj = MasterGridBindingSource.Current as TN_PUR1300;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_PUR1301List.OrderBy(p => p.OutSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            TN_PUR1300 newobj = new TN_PUR1300()
            {
                OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                OutDate = DateTime.Today,
                OutId = GlobalVariable.LoginId,
            };
            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            MasterGridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1300;
            if (masterObj == null) return;

            if (masterObj.TN_PUR1301List.Count > 0)
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
            if (e.KeyCode == Keys.Enter)
            {
                var textEdit = sender as TextEdit;
                if (textEdit == null) return;

                var detailList = DetailGridBindingSource.List as List<TN_PUR1301>;
                if (detailList != null)
                {

                    var masterObj = MasterGridBindingSource.Current as TN_PUR1300;
                    if (masterObj != null)
                    {
                        var inLotNo = textEdit.EditValue.GetNullToEmpty().ToUpper();
                        if (inLotNo.IsNullOrEmpty()) return;

                        IService<TN_PUR1300> LocalService = (IService<TN_PUR1300>)ProductionFactory.GetDomainService("TN_PUR1300");

                        var TN_PUR1201 = LocalService.GetChildList<TN_PUR1201>(p => p.InLotNo == inLotNo).FirstOrDefault();
                        if (TN_PUR1201 == null)
                        {
                            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InLotNo")));
                        }
                        else
                        {
                            //----------------------수입검사 필수 아님-------------------------
                            //var TN_QCT1101 = LocalService.GetChildList<TN_QCT1100>(p => p.InLotNo == inLotNo).FirstOrDefault();

                            //if (TN_QCT1101 == null)
                            //{
                            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InInspectionInfo")));
                            //}
                            //else if (TN_QCT1101.CheckResult == "NG")
                            //{
                            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_59), LabelConvert.GetLabelText("InInspectionInfo")));
                            //}
                            //else
                            //{
                                if (detailList.Any(p => p.InLotNo == inLotNo))
                                {
                                    DetailGridExControl.MainGrid.MainView.FocusedRowHandle = DetailGridExControl.MainGrid.MainView.LocateByValue("InLotNo", inLotNo);
                                }
                                else
                                {
                                    var ItemCode = new SqlParameter("@ItemCode", TN_PUR1201.ItemCode);
                                    var InLotNo = new SqlParameter("@InLotNo", inLotNo);
                                    var fifoDataSet = DbRequestHandler.GetDataSet("USP_GET_PUR_FIFO_NEW", ItemCode, InLotNo);
                                    if (fifoDataSet != null && fifoDataSet.Tables.Count > 0 && fifoDataSet.Tables[0].Rows.Count > 0)
                                    {
                                        string message = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_60) + Environment.NewLine;
                                        foreach (DataRow dr in fifoDataSet.Tables[0].Rows)
                                        {
                                            message += string.Format("{0}:{1} {2}:{3}", LabelConvert.GetLabelText("InLotNo"), dr["Key"], LabelConvert.GetLabelText("StockQty"), dr["Value"].GetDecimalNullToZero().ToString("#,#.##")) + Environment.NewLine;
                                        }
                                        if (MessageBoxHandler.Show(message, LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            var stockObj = ModelService.GetChildList<VI_PUR_STOCK_IN_LOT_NO>(p => p.InLotNo == inLotNo).FirstOrDefault();
                                            
                                            if(stockObj == null)
                                            {
                                                // 20210805 오세완 차장 시연시 수입검사를 진행하지 않은 경우 문제가 생겨서 추가 처리
                                                string sTempMessage = "수입검사가 진행되지 않았습니다.";
                                                MessageBoxHandler.Show(sTempMessage);
                                            }
                                            else if (stockObj.StockQty <= 0)
                                            {
                                                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_35), LabelConvert.GetLabelText("StockQty")));
                                            }
                                            else
                                            {
                                                var newObj = (TN_PUR1301)DetailGridBindingSource.AddNew();
                                                newObj.OutNo = masterObj.OutNo;
                                                newObj.OutSeq = masterObj.TN_PUR1301List.Count == 0 ? 1 : masterObj.TN_PUR1301List.Max(o => o.OutSeq) + 1;
                                                newObj.InNo = TN_PUR1201.InNo;
                                                newObj.InSeq = TN_PUR1201.InSeq;
                                                newObj.ItemCode = TN_PUR1201.ItemCode;
                                                newObj.OutQty = stockObj.StockQty.GetDecimalNullToZero();
                                                newObj.OutLotNo = newObj.OutNo + newObj.OutSeq.ToString().PadLeft(3, '0'); 
                                                newObj.InLotNo = inLotNo;
                                                newObj.InCustomerLotNo = TN_PUR1201.InCustomerLotNo;
                                                newObj.NewRowFlag = "Y";
                                                newObj.TN_PUR1300 = masterObj;
                                                newObj.TN_PUR1201 = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == inLotNo).FirstOrDefault();
                                                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).FirstOrDefault();
                                                newObj.AutoFlag = "N";
                                                masterObj.TN_PUR1301List.Add(newObj);

                                                DetailGridExControl.BestFitColumns();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var stockObj = ModelService.GetChildList<VI_PUR_STOCK_IN_LOT_NO>(p => p.InLotNo == inLotNo).FirstOrDefault();
                                    if (stockObj == null) { MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_35), LabelConvert.GetLabelText("StockQty"))); }
                                    else
                                    {
                                        if (stockObj.StockQty.GetNullToZero() <= 0)
                                        {
                                            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_35), LabelConvert.GetLabelText("StockQty")));
                                        }
                                        else
                                        {
                                            var newObj = (TN_PUR1301)DetailGridBindingSource.AddNew();
                                            newObj.OutNo = masterObj.OutNo;
                                            newObj.OutSeq = masterObj.TN_PUR1301List.Count == 0 ? 1 : masterObj.TN_PUR1301List.Max(o => o.OutSeq) + 1;
                                            newObj.InNo = TN_PUR1201.InNo;
                                            newObj.InSeq = TN_PUR1201.InSeq;
                                            newObj.ItemCode = TN_PUR1201.ItemCode;
                                            newObj.OutQty = stockObj.StockQty.GetDecimalNullToZero();
                                            newObj.OutLotNo = newObj.OutNo + newObj.OutSeq.ToString().PadLeft(3, '0'); 
                                            newObj.InLotNo = inLotNo;
                                            newObj.InCustomerLotNo = TN_PUR1201.InCustomerLotNo;
                                            newObj.NewRowFlag = "Y";
                                            newObj.TN_PUR1300 = masterObj;
                                            newObj.TN_PUR1201 = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == inLotNo).FirstOrDefault();
                                            newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).FirstOrDefault();
                                            newObj.AutoFlag = "N";
                                            masterObj.TN_PUR1301List.Add(newObj);

                                            DetailGridExControl.BestFitColumns();
                                        }
                                    }
                                    }
                                }
                            //}
                        }
                        LocalService.Dispose();
                    }
                }
                textEdit.EditValue = "";
                e.Handled = true;
            }
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1300;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR1301;
            if (detailObj == null) return;

            if (ModelService.GetChildList<TN_LOT_DTL>(p => p.SrcInLotNo == detailObj.OutLotNo).FirstOrDefault() != null)
            {

                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_51), LabelConvert.GetLabelText("ProductionInputInfo")));
                return;
            } 

            masterObj.TN_PUR1301List.Remove(detailObj);
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

        /// <summary> 현품표출력버튼 </summary>
        private void BarButtonPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (DetailGridBindingSource == null) return;

                MasterGridExControl.MainGrid.PostEditor();
                DetailGridExControl.MainGrid.PostEditor();

                WaitHandler.ShowWait();

                var detailList = DetailGridBindingSource.List as List<TN_PUR1301>;
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

                //var mainReport = new REPORT.XRPUR1301();
                // 20210819 오세완 차장 대영요청으로 10*6크기로 변경
                var mainReport = new REPORT.XRPUR1301_V2();
                foreach (var v in printList.OrderByDescending(p => p.InSeq).ToList())
                {
                    //var report = new REPORT.XRPUR1301(v);
                    var report = new REPORT.XRPUR1301_V2(v);
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

        /// <summary> 구매현황 </summary>
        private void Btn_PurchaseStatus_Click(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PUR_STATUS, param, null);
            form.ShowPopup(false);
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = sender as GridView;

            var masterObj = MasterGridBindingSource.Current as TN_PUR1300;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR1301;
            if (detailObj == null) return;

            if (detailObj.NewRowFlag == "Y" && e.Column.FieldName == "OutQty")
            {
                var stockObj = ModelService.GetChildList<VI_PUR_STOCK_IN_LOT_NO>(p => p.InLotNo == detailObj.InLotNo).FirstOrDefault();
                if (stockObj.StockQty < detailObj.OutQty)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_86), stockObj.StockQty));
                    //MessageBoxHandler.Show(string.Format("현 재고량은 {0}이므로 그 이상 출고할 수 없습니다.", stockObj.StockQty));
                    detailObj.OutQty = 0;
                }
                DetailGridExControl.BestFitColumns();
            }
        }

        private void OutQtyEdit_EditValueChanged(object sender, EventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1300;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR1301;
            if (detailObj == null) return;

            var stockObj = ModelService.GetChildList<VI_PUR_STOCK_IN_LOT_NO>(p => p.InLotNo == detailObj.InLotNo).FirstOrDefault();
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
                        DetailGridExControl.MainGrid.MainView.ActiveEditor.EditValue = 0;
                        DetailGridExControl.MainGrid.PostEditor();
                        outQtyEdit.EditValueChanged += OutQtyEdit_EditValueChanged;

                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_86), detailObj.CustomStockQty));
                        //MessageBoxHandler.Show(string.Format("현 재고량은 {0}이므로 그 이상 출고할 수 없습니다.", detailObj.CustomStockQty));
                    }
                    catch(Exception ex)
                    {
                        MessageBoxHandler.ErrorShow(ex);
                    }
                }
            }
        }
    }
}