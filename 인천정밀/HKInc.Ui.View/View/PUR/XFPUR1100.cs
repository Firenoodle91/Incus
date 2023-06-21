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
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.PUR
{
    /// <summary>
    /// 자재발주관리
    /// </summary>
    public partial class XFPUR1100 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR1100> ModelService = (IService<TN_PUR1100>)ProductionFactory.GetDomainService("TN_PUR1100");

        public XFPUR1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            MasterGridExControl.MainGrid.MainView.CellValueChanged += MasterMainView_CellValueChanged;
            MasterGridExControl.MainGrid.MainView.ShowingEditor += MasterView_ShowingEditor;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailView_ShowingEditor;

            btn_PurchaseStatus.Click += Btn_PurchaseStatus_Click;

            dt_PoDate.SetTodayIsMonth();
        }

        private void MasterMainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //var masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            //var scm = ModelService.GetChildList<SCM_VI_PUR1100_SCM>(p => p.PoNo == masterObj.PoNo).FirstOrDefault();
            //if (scm == null)
            //{
            //    masterObj.CustomerConfirm = "01";

            //}
            //else
            //{
            //    masterObj.Memo1 = scm.Memo.GetNullToEmpty();
            //    masterObj.CustomerConfirm = scm.CustomerConfirm.GetNullToEmpty();
            //    masterObj.CustomerConfirmDate = scm.CustomerConfirmDate;

            //}
        }

        protected override void InitCombo()
        {
            lup_PoCustomer.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Purchase || p.CustomerType == null)).ToList());
            lup_PoId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y" && p.MainYn == "02").ToList());

            btn_PurchaseStatus.Text = LabelConvert.GetLabelText("PurchaseStatus") + "(&F)";
        }

        protected override void InitGrid()
        {
            IsMasterGridButtonFileChooseEnabled = UserRight.HasEdit;
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("PoConfirm") + "[F10]", IconImageList.GetIconImage("actions/apply"));

            MasterGridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"));
            MasterGridExControl.MainGrid.AddColumn("PoDate", LabelConvert.GetLabelText("PoDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("DueDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("PoCustomerCode", LabelConvert.GetLabelText("PoCustomer"));

            MasterGridExControl.MainGrid.AddColumn("PoId", LabelConvert.GetLabelText("PoId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            MasterGridExControl.MainGrid.AddColumn("PoFlag", LabelConvert.GetLabelText("PoFlag"));
            MasterGridExControl.MainGrid.AddColumn("InConfirmState", false);                                     // 2021-11-04 김진우 주임 

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PoDate", "DueDate", "PoCustomerCode", "Memo", "PoId");

            //var btnPoCopy = new DevExpress.XtraBars.BarButtonItem();
            //btnPoCopy.Id = 4;
            //btnPoCopy.ImageOptions.Image = IconImageList.GetIconImage("miscellaneous/wizard");
            //btnPoCopy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.C));
            //btnPoCopy.Name = "btnPoCopy";
            //btnPoCopy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            //btnPoCopy.ShortcutKeyDisplayString = "Alt+C";
            //btnPoCopy.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            //btnPoCopy.Caption = LabelConvert.GetLabelText("PoCopy") + "[Alt+C]";
            //btnPoCopy.ItemClick += btnPoCopy_ItemClick;
            //btnPoCopy.Enabled = UserRight.HasEdit;
            //btnPoCopy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //MasterGridExControl.BarTools.AddItem(btnPoCopy);

            var barPoDocumentPrint = new DevExpress.XtraBars.BarButtonItem();
            barPoDocumentPrint.Id = 5;
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

            IsDetailGridButtonFileChooseEnabled = UserRight.HasEdit;
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("OrderRefer") + "[Alt+R]", IconImageList.GetIconImage("business%20objects/botask"));
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, false);

            DetailGridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"), false);
            DetailGridExControl.MainGrid.AddColumn("PoSeq", LabelConvert.GetLabelText("PoSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", LabelConvert.GetLabelText("BottomCategory"), false);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("PoQty", LabelConvert.GetLabelText("PoQty"));
            DetailGridExControl.MainGrid.AddColumn("PoRemainQty", LabelConvert.GetLabelText("PoRemainQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
            DetailGridExControl.MainGrid.AddColumn("PoCost", LabelConvert.GetLabelText("PoCost"));
            DetailGridExControl.MainGrid.AddUnboundColumn("PoAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([PoQty],0) * ISNULL([PoCost],0)", FormatType.Numeric, "#,###,###,###.##");
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("InConfirmState", false);                                     // 2021-11-04 김진우 주임
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PoQty", "PoCost", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1100>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1101>(DetailGridExControl);
        }

        protected override void InitRepository()
        {

            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoId", ModelService.GetChildList<User>(p => p.Active == "Y" && p.MainYn == "02"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoCustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Purchase || p.CustomerType == null)).ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("PoFlag", "N");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InConfirmState", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MaterialInConfirmFlag), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("PoQty", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("PoCost", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InConfirmState", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MaterialInConfirmFlag), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("PoNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            InitCombo();
            InitRepository();

            string customerCode = lup_PoCustomer.EditValue.GetNullToEmpty();
            string poId = lup_PoId.EditValue.GetNullToEmpty();

            var masterdata = ModelService.GetList(p => (p.PoDate >= dt_PoDate.DateFrEdit.DateTime
                                                                && p.PoDate <= dt_PoDate.DateToEdit.DateTime)
                                                                && (string.IsNullOrEmpty(customerCode) ? true : p.PoCustomerCode == customerCode)
                                                                && (string.IsNullOrEmpty(poId) ? true : p.PoId == poId))
                                                            //.OrderBy(o => o.PoDate)
                                                            .OrderBy(o => o.PoNo)
                                                            .ToList();

            MasterGridBindingSource.DataSource = masterdata;

            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            if (masterObj == null)
            {
                MasterGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
                MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("PoConfirm") + "[F10]", IconImageList.GetIconImage("actions/apply"));
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            MasterGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
            if (masterObj.PoFlag == "Y")
            {
                MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("PoConfirmCancel") + "[F10]", IconImageList.GetIconImage("actions/cancel"));
            }
            else
            {
                MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("PoConfirm") + "[F10]", IconImageList.GetIconImage("actions/apply"));
            }

            DetailGridBindingSource.DataSource = masterObj.TN_PUR1101List.OrderBy(p => p.PoSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            TN_PUR1100 newobj = new TN_PUR1100()
            {
                PoNo = DbRequestHandler.GetSeqMonth("PO"),
                PoDate = DateTime.Today,
                PoId = GlobalVariable.LoginId,
                DueDate = DateTime.Today.AddDays(20),
                PoFlag = "N"
            };

            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            MasterGridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            if (masterObj == null) return;

            if (masterObj.TN_PUR1101List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("PoMasterInfo"), LabelConvert.GetLabelText("PoDetailInfo"), LabelConvert.GetLabelText("PoDetailInfo")));
                return;
            }

            ModelService.Delete(masterObj);
            MasterGridBindingSource.RemoveCurrent();
            MasterGridExControl.BestFitColumns();
        }

        /// <summary> 발주확정/취소 </summary>
        protected override void FileChooseClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            if (masterObj == null) return;

            if (masterObj.TN_PUR1101List.Any(c => c.TN_PUR1201List.Count > 0))
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_52), LabelConvert.GetLabelText("InInfo")));
                return;
            }

            masterObj.PoFlag = masterObj.PoFlag == "Y" ? "N" : "Y";

            MasterFocusedRowChanged();
            MasterGridExControl.BestFitColumns();
        }

        /// <summary> 발주서 출력 </summary>
        private void BarPoDocumentPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MasterGridBindingSource == null) return;
            if (DetailGridBindingSource == null) return;
            if (!UserRight.HasEdit) return;

            DataSave();
            if (!SetSaveMessageCheck) return;

            var masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            if (masterObj == null) return;

            var listCount = masterObj.TN_PUR1101List.Count;
            if (listCount == 0) return;
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
                    masterObj.TN_PUR1101List.Add(new TN_PUR1101()
                    {
                        PoNo = masterObj.PoNo,
                        PoSeq = -1,
                        PoCost = 0
                    });
                    if (masterObj.TN_PUR1101List.Count == checkCount) break;
                }
                ReportCreateToPrint(masterObj);
            }
        }

        private void ReportCreateToPrint(TN_PUR1100 masterObj)
        {
            try
            {
                WaitHandler.ShowWait();
                var report = new REPORT.XRPUR1100(masterObj);
                report.CreateDocument();

                report.PrintingSystem.ShowMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                report.ShowPreview();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally
            {
                foreach (var v in masterObj.TN_PUR1101List.Where(p => p.PoSeq == -1).ToList())
                    masterObj.TN_PUR1101List.Remove(v);
                WaitHandler.CloseWait();
            }
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            if (masterObj == null) return;

            if (masterObj.PoFlag == "Y")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_38), LabelConvert.GetLabelText("PoConfirm")));
                return;
            }

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, MasterCodeSTR.Contraint_ItemMAT_BAN);
            param.SetValue(PopupParameter.Value_1, masterObj.PoCustomerCode);
            param.SetValue(PopupParameter.Value_2, "PoDetail");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_STD1100, param, AddPoDetailCallback);
            form.ShowPopup(true);
        }

        private void AddPoDetailCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            if (masterObj == null) return;

            //var masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            //if (masterObj == null) return;

            var returnList = (List<TN_STD1100>)e.Map.GetValue(PopupParameter.ReturnObject);
            foreach (var v in returnList)
            {

                var detailObj = DetailGridBindingSource.List as List<TN_PUR1101>;


                if (detailObj.Count > 0)
                {
                    var itemchk = detailObj.Where(p => p.ItemCode == v.ItemCode).ToList();

                    if (itemchk.Count > 0)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_157), LabelConvert.GetLabelText("PoConfirm")));
                        //return;
                    }
                    else
                    {
                        var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();
                        TN_STD1103 obj1 = ModelService.GetChildList<TN_STD1103>(p => p.CustomerCode == masterObj.PoCustomerCode && p.ItemCode == v.ItemCode).OrderByDescending(o => o.ChangeDate).FirstOrDefault();
                        var newObj = new TN_PUR1101();
                        newObj.PoNo = masterObj.PoNo;
                        newObj.PoSeq = masterObj.TN_PUR1101List.Count == 0 ? 1 : masterObj.TN_PUR1101List.Max(p => p.PoSeq) + 1;
                        newObj.ItemCode = item.ItemCode;
                        newObj.PoCost = obj1 == null ? item.Cost.GetDecimalNullToZero() : obj1.ChangeCost;
                        newObj.TN_STD1100 = item;
                        newObj.NewRowFlag = "Y";
                        masterObj.TN_PUR1101List.Add(newObj);
                        DetailGridBindingSource.Add(newObj);
                    }
                }
                else
                {
                    var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();
                    TN_STD1103 obj1 = ModelService.GetChildList<TN_STD1103>(p => p.CustomerCode == masterObj.PoCustomerCode && p.ItemCode == v.ItemCode).OrderByDescending(o => o.ChangeDate).FirstOrDefault();
                    var newObj = new TN_PUR1101();
                    newObj.PoNo = masterObj.PoNo;
                    newObj.PoSeq = masterObj.TN_PUR1101List.Count == 0 ? 1 : masterObj.TN_PUR1101List.Max(p => p.PoSeq) + 1;
                    newObj.ItemCode = item.ItemCode;
                    newObj.PoCost = obj1 == null ? item.Cost.GetDecimalNullToZero() : obj1.ChangeCost;
                    newObj.TN_STD1100 = item;
                    newObj.NewRowFlag = "Y";
                    masterObj.TN_PUR1101List.Add(newObj);
                    DetailGridBindingSource.Add(newObj);
                }






            }
            if (returnList.Count > 0)
            {
                SetIsFormControlChanged(true);
                DetailGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR1101;
            if (detailObj == null) return;

            if (detailObj.TN_PUR1201List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("PoDetailInfo"), LabelConvert.GetLabelText("InInfo"), LabelConvert.GetLabelText("InInfo")));

                return;
            }

            if (masterObj.PoFlag == "Y")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_38), LabelConvert.GetLabelText("PoConfirm")));
                IsFormControlChanged = false;
                return;
            }

            masterObj.TN_PUR1101List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }

        /// <summary> 수주참조 </summary>
        protected override void DetailFileChooseClicked()
        {
            //var masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            //if (masterObj == null) return;

            //if (masterObj.PoFlag == "Y")
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_38), LabelConvert.GetLabelText("PoConfirm")));
            //    return;
            //}

            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XSFORDER_REF, param, AddOrderRefCallback);
            form.ShowPopup(false);

        }

        private void AddOrderRefCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var returnObj_ORD = (TN_ORD1001)e.Map.GetValue(PopupParameter.Value_1);
            var returnObj_BOM = e.Map.GetValue(PopupParameter.Value_2) as List<TN_STD1300>;

            if (returnObj_ORD == null || returnObj_BOM == null) return;

            List<TN_PUR1100> master = new List<TN_PUR1100>();

            foreach (var bom in returnObj_BOM.OrderBy(p => p.TN_STD1100.MainCustomerCode))
            {
                //거래처별 발주마스터 생성. 존재하지 않을 시 새로 생성
                if (master.Count <= 0 || !master.Any(p => p.PoCustomerCode == bom.TN_STD1100.MainCustomerCode))
                {
                    TN_PUR1100 newMasterObj = new TN_PUR1100()
                    {
                        PoNo = DbRequestHandler.GetSeqMonth("PO"),
                        PoDate = DateTime.Today,
                        PoId = GlobalVariable.LoginId,
                        DueDate = DateTime.Today.AddDays(20),
                        PoFlag = "N",
                        PoCustomerCode = bom.TN_STD1100.MainCustomerCode,
                    };
                    master.Add(newMasterObj);
                    MasterGridBindingSource.Add(newMasterObj);

                    var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == bom.ItemCode).FirstOrDefault();

                    TN_PUR1101 newDetailObj = new TN_PUR1101()
                    {
                        PoNo = newMasterObj.PoNo,
                        PoSeq = newMasterObj.TN_PUR1101List.Where(p => p.PoNo == newMasterObj.PoNo).Count() == 0 ? 1 : newMasterObj.TN_PUR1101List.Where(p => p.PoNo == newMasterObj.PoNo).Max(p => p.PoSeq) + 1,
                        ItemCode = bom.ItemCode,
                        PoQty = returnObj_ORD.OrderQty * bom.UseQty,
                        NewRowFlag = "Y",
                        TN_STD1100 = item
                    };
                    DetailGridBindingSource.Add(newDetailObj);
                    newMasterObj.TN_PUR1101List.Add(newDetailObj);
                    ModelService.Insert(newMasterObj);
                }
                else
                {
                    //등록된 거래처별 발주마스터
                    var masterObj = master.OrderByDescending(p => p.PoNo).First();

                    var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == bom.ItemCode).FirstOrDefault();

                    TN_PUR1101 newDetailObj = new TN_PUR1101()
                    {
                        PoNo = masterObj.PoNo,
                        PoSeq = masterObj.TN_PUR1101List.Where(p => p.PoNo == masterObj.PoNo).Count() == 0 ? 1 : masterObj.TN_PUR1101List.Where(p => p.PoNo == masterObj.PoNo).Max(p => p.PoSeq) + 1,
                        ItemCode = item.ItemCode,
                        PoQty = returnObj_ORD.OrderQty * bom.UseQty,
                        NewRowFlag = "Y",
                        TN_STD1100 = item
                    };
                    masterObj.TN_PUR1101List.Add(newDetailObj);
                    DetailGridBindingSource.Add(newDetailObj);
                    ModelService.Insert(masterObj);
                }
            }

            if (returnObj_BOM.Count > 0)
            {
                SetIsFormControlChanged(true);
                MasterGridExControl.BestFitColumns();
                DetailGridExControl.BestFitColumns();
            }

        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            MasterGridBindingSource.EndEdit();
            DetailGridBindingSource.EndEdit();

            SetSaveMessageCheck = false;

            #region 품목단가이력 I/F 
            //if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            //{
            //    var masterList = MasterGridBindingSource.List as List<TN_PUR1100>;
            //    if (masterList.Count > 0)
            //    {
            //        if (masterList.Any(p => p.PoCustomerCode.IsNullOrEmpty()))
            //        {
            //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("PoCustomer")));
            //            return;
            //        }

            //        var editDetailList = masterList.Where(p => p.TN_PUR1101List.Any(c => c.NewRowFlag == "Y" || c.EditRowFlag == "Y")).ToList();

            //        if (masterList.Any(p => p.TN_PUR1101List.Any(c => c.PoQty == 0)))
            //        {
            //            MessageBoxHandler.Show("발주량이 없는 항목이 존재합니다. 확인해 주시기 바랍니다.");
            //            return;
            //        }

            //        foreach (var v in masterList.Where(p => p.TN_PUR1101List.Any(c => c.NewRowFlag == "Y" || c.EditRowFlag == "Y")).ToList())
            //        {
            //            foreach (var d in v.TN_PUR1101List.Where(c => c.NewRowFlag == "Y" || c.EditRowFlag == "Y").ToList())
            //            {
            //                if (d.PoCost > 0)
            //                {
            //                    TN_STD1103 old = ModelService.GetChildList<TN_STD1103>(p => p.CustomerCode == v.PoCustomerCode && p.ItemCode == d.ItemCode && p.ChangeDate == DateTime.Today).FirstOrDefault();
            //                    if (old == null)
            //                    {
            //                        var newObj = new TN_STD1103()
            //                        {
            //                            ItemCode = d.ItemCode,
            //                            Seq = d.TN_STD1100.TN_STD1103List.Count == 0 ? 1 : d.TN_STD1100.TN_STD1103List.Max(p => p.Seq) + 1,
            //                            CustomerCode = v.PoCustomerCode,
            //                            ChangeDate = DateTime.Today,
            //                            ChangeCost = d.PoCost
            //                        };
            //                        d.TN_STD1100.TN_STD1103List.Add(newObj);
            //                    }
            //                    else
            //                    {
            //                        old.ChangeCost = d.PoCost;
            //                        d.TN_STD1100.TN_STD1103List.Remove(old);
            //                        d.TN_STD1100.TN_STD1103List.Add(old);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion

            ModelService.Save();

            SetSaveMessageCheck = true;
            DataLoad();
        }

        /// <summary> 구매현황 </summary>
        private void Btn_PurchaseStatus_Click(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PUR_STATUS, param, null);
            form.ShowPopup(false);
        }

        /// <summary> 발주복사 </summary>
        private void btnPoCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PUR1100_COPY, param, PoCopyPopupCallback);

            form.ShowPopup(true);
        }

        private void PoCopyPopupCallback(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var returnList = (List<TN_PUR1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var returnObj in returnList)
            {
                var newMst = (TN_PUR1100)MasterGridBindingSource.AddNew();
                newMst.PoNo = DbRequestHandler.GetSeqMonth("PO");
                newMst.PoDate = DateTime.Today;
                newMst.DueDate = DateTime.Today.AddDays(20);
                newMst.PoId = GlobalVariable.LoginId;
                newMst.PoCustomerCode = returnObj.PoCustomerCode;
                newMst.PoFlag = "N";

                foreach (var dtlObj in ModelService.GetChildList<TN_PUR1101>(p => p.PoNo == returnObj.PoNo))
                {
                    var newDtl = (TN_PUR1101)DetailGridBindingSource.AddNew();
                    newDtl.PoNo = newMst.PoNo;
                    newDtl.PoSeq = dtlObj.PoSeq;
                    newDtl.ItemCode = dtlObj.ItemCode;
                    newDtl.PoQty = dtlObj.PoQty;
                    newDtl.PoCost = dtlObj.PoCost;
                    newDtl.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == dtlObj.ItemCode).FirstOrDefault();
                    newDtl.NewRowFlag = "Y";

                    newMst.TN_PUR1101List.Add(newDtl);
                }

                ModelService.Insert(newMst);
            }

            if (returnList.Count > 0)
            {
                SetIsFormControlChanged(true);
                MasterGridExControl.BestFitColumns();
            }
        }

        private void MasterView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            if (masterObj == null) return;

            if (masterObj.PoFlag == "Y")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_38), LabelConvert.GetLabelText("PoConfirm")));
                e.Cancel = true;
            }
            else if (masterObj.InConfirmState != MasterCodeSTR.MaterialInConfirmFlag_Wait)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_52), LabelConvert.GetLabelText("InInfo")));
                e.Cancel = true;
            }
        }

        private void DetailView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR1101;
            if (detailObj == null) return;

            if (masterObj.PoFlag == "Y")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_38), LabelConvert.GetLabelText("PoConfirm")));
                e.Cancel = true;
            }
            else if (detailObj.InConfirmState != MasterCodeSTR.MaterialInConfirmFlag_Wait)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_52), LabelConvert.GetLabelText("InInfo")));
                e.Cancel = true;
            }
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = sender as GridView;
            var detailObj = DetailGridBindingSource.Current as TN_PUR1101;
            if (e.Column.FieldName == "PoCost")
                detailObj.EditRowFlag = "Y";
        }
    }
}