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

            MasterGridExControl.MainGrid.MainView.ShowingEditor += MasterView_ShowingEditor;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailView_ShowingEditor;

            btn_PurchaseStatus.Click += Btn_PurchaseStatus_Click;
        }

        protected override void InitCombo()
        {
            dt_PoDate.SetTodayIsMonth();

            //lup_PoCustomer.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Purchase || p.CustomerType == null)).ToList());
            //매입/매출 거래처 가져오도록 변경
            lup_PoCustomer.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Purchase || p.CustomerType == MasterCodeSTR.CustType_Sales)).ToList());

            lup_PoId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());

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
            MasterGridExControl.MainGrid.AddColumn("ProdItem", LabelConvert.GetLabelText("ProdItem"));
            
            MasterGridExControl.MainGrid.AddColumn("PoId", LabelConvert.GetLabelText("PoId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.AddColumn("PoFlag", LabelConvert.GetLabelText("PoFlag"));
            MasterGridExControl.MainGrid.AddColumn("InConfirmState");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PoDate", "DueDate", "PoCustomerCode", "Memo", "ProdItem");

            var btnPoCopy = new DevExpress.XtraBars.BarButtonItem();
            btnPoCopy.Id = 4;
            btnPoCopy.ImageOptions.Image = IconImageList.GetIconImage("miscellaneous/wizard");
            btnPoCopy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.C));
            btnPoCopy.Name = "btnPoCopy";
            btnPoCopy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            btnPoCopy.ShortcutKeyDisplayString = "Alt+C";
            btnPoCopy.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            btnPoCopy.Caption = LabelConvert.GetLabelText("PoCopy") + "[Alt+C]";
            btnPoCopy.ItemClick += btnPoCopy_ItemClick;
            btnPoCopy.Enabled = UserRight.HasEdit;
            MasterGridExControl.BarTools.AddItem(btnPoCopy);

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

            DetailGridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"), false);
            DetailGridExControl.MainGrid.AddColumn("PoSeq", LabelConvert.GetLabelText("PoSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("PoQty", LabelConvert.GetLabelText("PoQty"));
            DetailGridExControl.MainGrid.AddColumn("PoRemainQty", LabelConvert.GetLabelText("PoRemainQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
            DetailGridExControl.MainGrid.AddColumn("PoCost", LabelConvert.GetLabelText("PoCost"));
            DetailGridExControl.MainGrid.AddUnboundColumn("PoAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([PoQty],0) * ISNULL([PoCost],0)", FormatType.Numeric, "#,###,###,###.##");
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("InConfirmState");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PoQty", "PoCost", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1100>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1101>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoCustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Purchase || p.CustomerType == MasterCodeSTR.CustType_Sales)).ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("PoFlag", "N");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InConfirmState", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MaterialInConfirmFlag), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProdItem", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory==MasterCodeSTR.TopCategory_WAN), "ItemCode", "ItemName");

            
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("PoQty", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("PoCost", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InConfirmState", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MaterialInConfirmFlag), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();

            DataLoad();
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

            string customerCode = lup_PoCustomer.EditValue.GetNullToEmpty();
            string poId = lup_PoId.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.PoDate >= dt_PoDate.DateFrEdit.DateTime
                                                                         && p.PoDate <= dt_PoDate.DateToEdit.DateTime)
                                                                         && (string.IsNullOrEmpty(customerCode) ? true : p.PoCustomerCode == customerCode)
                                                                         && (string.IsNullOrEmpty(poId) ? true : p.PoId == poId))
                                                                      .OrderBy(o => o.PoDate)
                                                                      .ToList();
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
                PoFlag = "N",
                
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

            ReportCreateToPrint(masterObj);


            //2023-01-18 김진우 주석
            //int printRowCnt = 20;

            //var valueCount = listCount / printRowCnt;
            //var modCount = listCount % printRowCnt;

            //if (modCount == 0)
            //{
            //    ReportCreateToPrint(masterObj);
            //}
            //else
            //{
            //    var checkCount = (valueCount == 0 ? 1 : valueCount + 1) * printRowCnt;
            //    while (true)
            //    {
            //        masterObj.TN_PUR1101List.Add(new TN_PUR1101()
            //        {
            //            PoNo = masterObj.PoNo,
            //            PoSeq = -1,
            //            PoCost = 0
            //        });
            //        if (masterObj.TN_PUR1101List.Count == checkCount) break;
            //    }
            //    ReportCreateToPrint(masterObj);
            //}
        }

        private void ReportCreateToPrint(TN_PUR1100 masterObj)
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
                //foreach (var v in masterObj.TN_PUR1101List.Where(p => p.PoSeq == -1).ToList())
                //    masterObj.TN_PUR1101List.Remove(v);
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
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_STD1100, param, AddPoDetailCallback);
            form.ShowPopup(true);
        }

        private void AddPoDetailCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_PUR1100;
            if (masterObj == null) return;

            var returnList = (List<TN_STD1100>)e.Map.GetValue(PopupParameter.ReturnObject);
            foreach (var v in returnList)
            {
                var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();
                TN_STD1103 obj1 = ModelService.GetChildList<TN_STD1103>(p => p.CustomerCode == masterObj.PoCustomerCode && p.ItemCode == v.ItemCode).OrderByDescending(o => o.ChangeDate).FirstOrDefault();
                var newObj = new TN_PUR1101();
                newObj.PoNo = masterObj.PoNo;
                newObj.PoSeq = masterObj.TN_PUR1101List.Count == 0 ? 1 : masterObj.TN_PUR1101List.Max(p => p.PoSeq) + 1;
                newObj.ItemCode = item.ItemCode;
                newObj.PoCost = returnList[0].Cost.GetDecimalNullToZero();
                //wObj.PoCost = obj1 == null ? item.Cost.GetDecimalNullToZero() : obj1.ChangeCost;
                newObj.TN_STD1100 = item;
                newObj.NewRowFlag = "Y";
                masterObj.TN_PUR1101List.Add(newObj);
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
            DataSet ds = new DataSet();
            //팝업에서 넘겨온 데이터 재조회 

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _itemcodeWan = new System.Data.SqlClient.SqlParameter("@ITEMCODEWAN", returnObj_ORD.ItemCode);
                ds = DbRequestHandler.GetDataSet("USP_GET_BOMLIST_MATERIAL_INFO", _itemcodeWan);
            }

            var returnObj_BOM = ds.Tables[0];            
            //var returnObj_BOM = e.Map.GetValue(PopupParameter.Value_2) as List<TN_STD1300>;

            if (returnObj_ORD == null || returnObj_BOM == null) return;

            List<TN_PUR1100> master = new List<TN_PUR1100>();

            foreach(DataRow bom in returnObj_BOM.Rows)
            {
                string ItemCode = bom["ItemCode"].ToString();
                //거래처별 발주 마스터 정보 없을 시 신규 생성
                if (master.Count <= 0 || !master.Any(p => p.PoCustomerCode == bom["MainCustomerCode"].ToString()))
                {
                    TN_PUR1100 newMasterObj = new TN_PUR1100()
                    {
                        PoNo = DbRequestHandler.GetSeqMonth("PO"),
                        PoDate = DateTime.Today,
                        PoId = GlobalVariable.LoginId,
                        DueDate = DateTime.Today.AddDays(20),
                        PoFlag = "N",
                        PoCustomerCode = bom["MainCustomerCode"].ToString(),
                        ProdItem = returnObj_ORD.ItemCode
                    };
                    master.Add(newMasterObj);
                    MasterGridBindingSource.Add(newMasterObj);

                    var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == ItemCode).FirstOrDefault();

                    TN_PUR1101 newDetailObj = new TN_PUR1101()
                    {
                        PoNo = newMasterObj.PoNo,
                        PoSeq = newMasterObj.TN_PUR1101List.Where(p => p.PoNo == newMasterObj.PoNo).Count() == 0 ? 1 : newMasterObj.TN_PUR1101List.Where(p => p.PoNo == newMasterObj.PoNo).Max(p => p.PoSeq) + 1,
                        ItemCode = bom["ItemCode"].ToString(),
                        PoQty = returnObj_ORD.OrderQty * Convert.ToDecimal(bom["UseQty"].ToString()),
                        PoCost = bom["PoCost"].ToString() == "" ? 0 : Convert.ToDecimal(bom["PoCost"]),
                        NewRowFlag = "Y",
                        TN_STD1100 = item,
                        Temp = returnObj_ORD.OrderNo,               // 수주등록시 수주번호 등록되도록 추가 2023-01-10 김진우 추가
                        Temp1 = returnObj_ORD.OrderQty.ToString()   // 수주등록시 수주량 등록되도록 추가 2023-01-10 김진우 추가
                    };
                    DetailGridBindingSource.Add(newDetailObj);
                    newMasterObj.TN_PUR1101List.Add(newDetailObj);
                    ModelService.Insert(newMasterObj);
                }

                else
                {
                    //등록된 거래처별 발주마스터
                    var masterObj = master.OrderByDescending(p => p.PoNo).First();

                    var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == ItemCode).FirstOrDefault();

                    TN_PUR1101 newDetailObj = new TN_PUR1101()
                    {
                        PoNo = masterObj.PoNo,
                        PoSeq = masterObj.TN_PUR1101List.Where(p => p.PoNo == masterObj.PoNo).Count() == 0 ? 1 : masterObj.TN_PUR1101List.Where(p => p.PoNo == masterObj.PoNo).Max(p => p.PoSeq) + 1,
                        ItemCode = item.ItemCode,
                        PoCost = bom["PoCost"].ToString() == "" ? 0 : Convert.ToDecimal(bom["PoCost"]),
                        PoQty = returnObj_ORD.OrderQty * Convert.ToDecimal(bom["UseQty"].ToString()),
                        NewRowFlag = "Y",
                        TN_STD1100 = item,
                        Temp = returnObj_ORD.OrderNo,               // 수주등록시 수주번호 등록되도록 추가 2023-01-18 김진우 추가
                        Temp1 = returnObj_ORD.OrderQty.ToString()   // 수주등록시 수주량 등록되도록 추가 2023-01-18 김진우 추가
                    };
                    masterObj.TN_PUR1101List.Add(newDetailObj);
                    DetailGridBindingSource.Add(newDetailObj);
                    ModelService.Insert(masterObj);
                }
            }

            if (returnObj_BOM.Rows.Count > 0)
            {                
                SetIsFormControlChanged(true);
                MasterGridExControl.BestFitColumns();
                DetailGridExControl.BestFitColumns();
            }


            //foreach (var bom in returnObj_BOM.OrderBy(p => p.TN_STD1100.MainCustomerCode))
            //{
            //    //거래처별 발주마스터 생성. 존재하지 않을 시 새로 생성
            //    if(master.Count <= 0 || !master.Any(p => p.PoCustomerCode == bom.TN_STD1100.MainCustomerCode))
            //    {
            //        TN_PUR1100 newMasterObj = new TN_PUR1100()
            //        {
            //            PoNo = DbRequestHandler.GetSeqMonth("PO"),
            //            PoDate = DateTime.Today,
            //            PoId = GlobalVariable.LoginId,
            //            DueDate = DateTime.Today.AddDays(20),
            //            PoFlag = "N",
            //            PoCustomerCode = bom.TN_STD1100.MainCustomerCode,
            //            ProdItem = returnObj_ORD.ItemCode
            //        };
            //        master.Add(newMasterObj);
            //        MasterGridBindingSource.Add(newMasterObj);

            //        var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == bom.ItemCode).FirstOrDefault();

            //        TN_PUR1101 newDetailObj = new TN_PUR1101()
            //        {
            //            PoNo = newMasterObj.PoNo,
            //            PoSeq = newMasterObj.TN_PUR1101List.Where(p => p.PoNo == newMasterObj.PoNo).Count() == 0 ? 1 : newMasterObj.TN_PUR1101List.Where(p => p.PoNo == newMasterObj.PoNo).Max(p => p.PoSeq) + 1,
            //            ItemCode = bom.ItemCode,
            //            PoQty = returnObj_ORD.OrderQty * bom.UseQty,
            //            NewRowFlag = "Y",
            //            TN_STD1100 = item
            //        };
            //        DetailGridBindingSource.Add(newDetailObj);
            //        newMasterObj.TN_PUR1101List.Add(newDetailObj);
            //        ModelService.Insert(newMasterObj);
            //    }
            //    else
            //    {
            //        //등록된 거래처별 발주마스터
            //        var masterObj = master.OrderByDescending(p => p.PoNo).First();

            //        var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == bom.ItemCode).FirstOrDefault();

            //        TN_PUR1101 newDetailObj = new TN_PUR1101()
            //        {
            //            PoNo = masterObj.PoNo,
            //            PoSeq = masterObj.TN_PUR1101List.Where(p => p.PoNo == masterObj.PoNo).Count() == 0 ? 1 : masterObj.TN_PUR1101List.Where(p => p.PoNo == masterObj.PoNo).Max(p => p.PoSeq) + 1,
            //            ItemCode = item.ItemCode,
            //            PoQty = returnObj_ORD.OrderQty * bom.UseQty,
            //            NewRowFlag = "Y",
            //            TN_STD1100 = item
            //        };
            //        masterObj.TN_PUR1101List.Add(newDetailObj);
            //        DetailGridBindingSource.Add(newDetailObj);
            //        ModelService.Insert(masterObj);
            //    }
            //}            
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            MasterGridBindingSource.EndEdit();
            DetailGridBindingSource.EndEdit();

            SetSaveMessageCheck = false;

            #region 품목단가이력 I/F 
            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var masterList = MasterGridBindingSource.List as List<TN_PUR1100>;
                if (masterList.Count > 0)
                {
                    if (masterList.Any(p => p.PoCustomerCode.IsNullOrEmpty()))
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("PoCustomer")));
                        return;
                    }

                    var editDetailList = masterList.Where(p => p.TN_PUR1101List.Any(c => c.NewRowFlag == "Y" || c.EditRowFlag == "Y")).ToList();

                    if (masterList.Any(p => p.TN_PUR1101List.Any(c => c.PoQty == 0)))
                    {
                        MessageBoxHandler.Show("발주량이 없는 항목이 존재합니다. 확인해 주시기 바랍니다.");
                        return;
                    }

                    foreach (var v in masterList.Where(p => p.TN_PUR1101List.Any(c => c.NewRowFlag == "Y" || c.EditRowFlag == "Y")).ToList())
                    {
                        foreach (var d in v.TN_PUR1101List.Where(c => c.NewRowFlag == "Y" || c.EditRowFlag == "Y").ToList())
                        {
                            if (d.PoCost > 0)
                            {
                                TN_STD1103 old = ModelService.GetChildList<TN_STD1103>(p => p.CustomerCode == v.PoCustomerCode && p.ItemCode == d.ItemCode && p.ChangeDate == DateTime.Today).FirstOrDefault();
                                if (old == null)
                                {
                                    var newObj = new TN_STD1103()
                                    {
                                        ItemCode = d.ItemCode,
                                        Seq = d.TN_STD1100.TN_STD1103List.Count == 0 ? 1 : d.TN_STD1100.TN_STD1103List.Max(p => p.Seq) + 1,
                                        CustomerCode = v.PoCustomerCode,
                                        ChangeDate = DateTime.Today,
                                        ChangeCost = d.PoCost
                                    };
                                    d.TN_STD1100.TN_STD1103List.Add(newObj);
                                }
                                else
                                {
                                    old.ChangeCost = d.PoCost;
                                    d.TN_STD1100.TN_STD1103List.Remove(old);
                                    d.TN_STD1100.TN_STD1103List.Add(old);
                                }
                            }
                        }
                    }
                }
            }
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
                newMst.ProdItem = returnObj.ProdItem;
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
            else if(detailObj.InConfirmState != MasterCodeSTR.MaterialInConfirmFlag_Wait)
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