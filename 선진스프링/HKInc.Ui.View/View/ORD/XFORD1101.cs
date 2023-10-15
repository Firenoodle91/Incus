using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraReports.UI;
using HKInc.Utils.Common;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using System.Data.SqlClient;
using DevExpress.XtraBars;
using System.Windows.Forms;
using HKInc.Ui.View.View.REPORT;
using DevExpress.XtraEditors.Controls;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 출고증관리(일일출고예정관리)
    /// </summary>
    public partial class XFORD1101 : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1103> ModelService = (IService<TN_ORD1103>)ProductionFactory.GetDomainService("TN_ORD1103");
        private List<VI_PROD_STOCK_ITEM> StockList = new List<VI_PROD_STOCK_ITEM>();
        private List<TEMP_ORD1101_CUSTOMER> MasterCustomerList = new List<TEMP_ORD1101_CUSTOMER>();

        public XFORD1101()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            MasterGridExControl.MainGrid.MainView.CustomColumnDisplayText += MasterMainView_CustomColumnDisplayText; 
            DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_ORD1101;
            if (detailObj == null) return;

            if (e.Column.FieldName == "OutPlanQty")
            {
                detailObj.EditRowFlag = "Y";
            }
        }

        protected override void InitCombo()
        {            
            dt_OutDatePlan.DateTime = DateTime.Today;

            var list = ModelService.GetChildList<VI_BUSINESS_MANAGEMENT_USER>(p => true).ToList();
            lup_Manager.SetDefault(true, "LoginId", "UserName", list);
            if (list.Any(p => p.LoginId == GlobalVariable.LoginId)) 
                lup_Manager.EditValue = GlobalVariable.LoginId;
        }

        protected override void InitGrid()
        {
            //MasterGridExControl.MainGrid.CheckBoxMultiSelect(true, "OutRepNo", true);
            IsMasterGridButtonFileChooseEnabled = true;
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("ShipmentCardPrint") + "[F10]", IconImageList.GetIconImage("print/printer"));
            //MasterGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Print"));
            MasterGridExControl.MainGrid.AddColumn("OutRepNo", LabelConvert.GetLabelText("OutRepNo"));
            MasterGridExControl.MainGrid.AddColumn("OutDatePlan", LabelConvert.GetLabelText("OutDatePlan"));
            MasterGridExControl.MainGrid.AddColumn("BusinessManagementId", LabelConvert.GetLabelText("ManagerName"));
            MasterGridExControl.MainGrid.AddColumn("CustomerName", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"), false);
            //MasterGridExControl.MainGrid.SetEditable("_Check");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutDatePlan", "BusinessManagementId", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1103>(MasterGridExControl);

            DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "RowId", true);                
            DetailGridExControl.MainGrid.AddColumn("RowId", "RowId", false);
            DetailGridExControl.MainGrid.AddColumn("_Check", "명세서출력");
            DetailGridExControl.MainGrid.AddColumn("OutRepNo", LabelConvert.GetLabelText("OutRepNo"), false);
            DetailGridExControl.MainGrid.AddColumn("TN_ORD1100.TN_ORD1001.EndMonthDate", LabelConvert.GetLabelText("EndMonthDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName") , LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            DetailGridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N0);
            DetailGridExControl.MainGrid.AddColumn("TN_ORD1100.DelivQty", LabelConvert.GetLabelText("DelivQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N0);
            DetailGridExControl.MainGrid.AddColumn("OutPlanQty", LabelConvert.GetLabelText("OutPlanQty"));
            DetailGridExControl.MainGrid.AddColumn("Temp", LabelConvert.GetLabelText("ProductLotNo"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("InspAdd", "성적서추가");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutPlanQty", "Memo", "_Check", "InspAdd");

            var barButtonTradingStatePrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonTradingStatePrint.Id = 4;
            barButtonTradingStatePrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/bosale");
            //barButtonDevide.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonAdd.ImageOptions.LargeImage")));
            barButtonTradingStatePrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barButtonTradingStatePrint.Name = "barButtonTradingStatePrint";
            barButtonTradingStatePrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonTradingStatePrint.ShortcutKeyDisplayString = "Alt+P";
            barButtonTradingStatePrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonTradingStatePrint.Caption = LabelConvert.GetLabelText("TradingStatePrint") + "[Alt+P]";
            barButtonTradingStatePrint.Alignment = BarItemLinkAlignment.Right;
            barButtonTradingStatePrint.ItemClick += BarButtonTradingStatePrint_ItemClick;
            DetailGridExControl.BarTools.AddItem(barButtonTradingStatePrint);

        }

        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDatePlan");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BusinessManagementId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.BestFitColumns();

            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OutPlanQty", DefaultBoolean.Default, "n0");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");

            DetailGridExControl.MainGrid.SetRepositoryItemButtonEdit("InspAdd", InspAdd_ButtonClick, "추가");
            DetailGridExControl.BestFitColumns();
        }

        private void InspAdd_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_ORD1101;
            if (detailObj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.KeyValue, detailObj);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_QCT_SHIPMENT, param, InspAddCallBack);
            form.ShowPopup(true);
        }

        private void InspAddCallBack(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_ORD1101;
            if (detailObj == null) return;

            var returnList = (List<VI_INSP_SHIPMENT_OBJECT>)e.Map.GetValue(PopupParameter.ReturnObject);

            IService<TN_QCT1500> TEMP_ModelService = (IService<TN_QCT1500>)ProductionFactory.GetDomainService("TN_QCT1500");

            foreach (var v in returnList.ToList())
            {
                var newObj = new TN_QCT1500()
                {
                    InspNo = DbRequestHandler.GetSeqMonth(MasterCodeSTR.InspectionDivision_Shipment),
                    FinalInspNo = v.FinalInspNo,
                    ItemCode = v.ItemCode,
                    ProductLotNo = v.ProductLotNo,
                    CustomerCode = v.CustomerCode,
                    Temp1 = v.Temp1,
                    CheckDate = DateTime.Today,
                    CheckId = GlobalVariable.LoginId,
                    OutQty = detailObj.OutPlanQty.GetDecimalNullToZero(),
                    TN_STD1100 = TEMP_ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).First()
                };

                var finalDetailInspectionList = TEMP_ModelService.GetChildList<TN_QCT1101>(p => p.InspNo == v.FinalInspNo).ToList();
                foreach (var c in finalDetailInspectionList)
                {
                    var qcRev = TEMP_ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == v.ItemCode && p.UseFlag == "Y").OrderBy(p => p.RowId).LastOrDefault();
                    if (qcRev == null) break;

                    var qcList = qcRev.TN_QCT1001List.Where(p => p.UseFlag == "Y" && p.CheckDivision == MasterCodeSTR.InspectionDivision_Shipment).OrderBy(p => p.DisplayOrder).ToList();
                    foreach (var x in qcList)
                    {
                        if (x.CheckList == c.CheckList)
                        {
                            var newObj2 = new TN_QCT1501()
                            {
                                InspNo = newObj.InspNo,
                                InspSeq = newObj.TN_QCT1501List.Count == 0 ? 1 : newObj.TN_QCT1501List.Max(o => o.InspSeq) + 1,
                                RevNo = x.RevNo,
                                ItemCode = newObj.ItemCode,
                                Seq = x.Seq,
                                CheckWay = x.CheckWay,
                                CheckList = x.CheckList,
                                CheckMax = x.CheckMax,
                                CheckMin = x.CheckMin,
                                CheckSpec = x.CheckSpec,
                                CheckUpQuad = x.CheckUpQuad,
                                CheckDownQuad = x.CheckDownQuad,
                                CheckDataType = x.CheckDataType,
                                Reading1 = c.Reading1,
                                Reading2 = c.Reading2,
                                Reading3 = c.Reading3,
                                Reading4 = c.Reading4,
                                Reading5 = c.Reading5,
                                Reading6 = c.Reading6,
                                Reading7 = c.Reading7,
                                Reading8 = c.Reading8,
                                Reading9 = c.Reading9,
                                Judge = c.Judge,
                                TN_QCT1001 = x,
                            };

                            if (newObj2.CheckDataType != MasterCodeSTR.CheckDataType_C)
                            {
                                var reading1 = newObj2.Reading1.GetDecimalNullToNull();
                                var reading2 = newObj2.Reading2.GetDecimalNullToNull();
                                var reading3 = newObj2.Reading3.GetDecimalNullToNull();
                                var reading4 = newObj2.Reading4.GetDecimalNullToNull();
                                var reading5 = newObj2.Reading5.GetDecimalNullToNull();
                                var reading6 = newObj2.Reading6.GetDecimalNullToNull();
                                var reading7 = newObj2.Reading7.GetDecimalNullToNull();
                                var reading8 = newObj2.Reading8.GetDecimalNullToNull();
                                var reading9 = newObj2.Reading9.GetDecimalNullToNull();

                                var readingList = new List<decimal>();
                                if (reading1 != null) readingList.Add((decimal)reading1);
                                if (reading2 != null) readingList.Add((decimal)reading2);
                                if (reading3 != null) readingList.Add((decimal)reading3);
                                if (reading4 != null) readingList.Add((decimal)reading4);
                                if (reading5 != null) readingList.Add((decimal)reading5);
                                if (reading6 != null) readingList.Add((decimal)reading6);
                                if (reading7 != null) readingList.Add((decimal)reading7);
                                if (reading8 != null) readingList.Add((decimal)reading8);
                                if (reading9 != null) readingList.Add((decimal)reading9);

                                if (readingList.Count > 0)
                                {
                                    newObj2.Temp1 = readingList.Min().GetNullToEmpty();
                                    newObj2.Temp2 = readingList.Max().GetNullToEmpty();
                                }
                            }

                            if (newObj2.CheckDataType == MasterCodeSTR.CheckDataType_C)
                            {
                                DetailCheckEye(newObj2);
                            }
                            else if (newObj2.CheckWay == MasterCodeSTR.InspectionWay_Eye)
                            {
                                DetailCheckInput(newObj2);
                            }
                            else
                            {
                                DetailCheckInput(newObj2);
                            }

                            newObj.TN_QCT1501List.Add(newObj2);
                        }
                    }
                    DetailGridExControl.BestFitColumns();
                }
                MasterCheckResult(newObj);

                IsFormControlChanged = true;

                TEMP_ModelService.Insert(newObj);

            }

            TEMP_ModelService.Save();

            MessageBoxHandler.Show("성적서 추가가 완료되었습니다.");
        }

        private void DetailCheckInput(TN_QCT1501 detailObj)
        {
            var checkSpec = detailObj.CheckSpec.GetDecimalNullToNull();
            if (checkSpec == null) return;
            var checkUpQuad = detailObj.CheckUpQuad.GetDecimalNullToZero();
            var checkDownQuad = detailObj.CheckDownQuad.GetDecimalNullToZero();
            var checkUp = checkSpec + checkUpQuad;
            var checkDown = checkSpec - checkDownQuad;

            int NgQty = 0;
            int OkQty = 0;

            var reading1 = detailObj.Reading1.GetDecimalNullToNull();
            var reading2 = detailObj.Reading2.GetDecimalNullToNull();
            var reading3 = detailObj.Reading3.GetDecimalNullToNull();
            var reading4 = detailObj.Reading4.GetDecimalNullToNull();
            var reading5 = detailObj.Reading5.GetDecimalNullToNull();
            var reading6 = detailObj.Reading6.GetDecimalNullToNull();
            var reading7 = detailObj.Reading7.GetDecimalNullToNull();
            var reading8 = detailObj.Reading8.GetDecimalNullToNull();
            var reading9 = detailObj.Reading9.GetDecimalNullToNull();

            if (reading1 != null)
            {
                if (reading1 >= checkDown && reading1 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading2 != null)
            {
                if (reading2 >= checkDown && reading2 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading3 != null)
            {
                if (reading3 >= checkDown && reading3 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading4 != null)
            {
                if (reading4 >= checkDown && reading4 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading5 != null)
            {
                if (reading5 >= checkDown && reading5 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading6 != null)
            {
                if (reading6 >= checkDown && reading6 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading7 != null)
            {
                if (reading7 >= checkDown && reading7 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading8 != null)
            {
                if (reading8 >= checkDown && reading8 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading9 != null)
            {
                if (reading9 >= checkDown && reading9 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (NgQty == 0 && OkQty == 0)
            {
                detailObj.Judge = null;
            }
            else if (NgQty > 0)
            {
                detailObj.Judge = "NG";
            }
            else
            {
                detailObj.Judge = "OK";
            }
        }

        private void DetailCheckEye(TN_QCT1501 detailObj)
        {
            int NgQty = 0;
            int OkQty = 0;

            var reading1 = detailObj.Reading1.GetNullToNull();
            var reading2 = detailObj.Reading2.GetNullToNull();
            var reading3 = detailObj.Reading3.GetNullToNull();
            var reading4 = detailObj.Reading4.GetNullToNull();
            var reading5 = detailObj.Reading5.GetNullToNull();
            var reading6 = detailObj.Reading6.GetNullToNull();
            var reading7 = detailObj.Reading7.GetNullToNull();
            var reading8 = detailObj.Reading8.GetNullToNull();
            var reading9 = detailObj.Reading9.GetNullToNull();

            if (reading1 != null)
            {
                if (reading1 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading2 != null)
            {
                if (reading2 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading3 != null)
            {
                if (reading3 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading4 != null)
            {
                if (reading4 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading5 != null)
            {
                if (reading5 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading6 != null)
            {
                if (reading6 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading7 != null)
            {
                if (reading7 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading8 != null)
            {
                if (reading8 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading9 != null)
            {
                if (reading9 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (NgQty == 0 && OkQty == 0)
            {
                detailObj.Judge = null;
            }
            else if (NgQty > 0)
            {
                detailObj.Judge = "NG";
            }
            else
            {
                detailObj.Judge = "OK";
            }
        }

        private void MasterCheckResult(TN_QCT1500 masterObj)
        {
            if (masterObj.TN_QCT1501List.Any(p => p.Judge == "NG"))
                masterObj.CheckResult = "NG";
            else if (masterObj.TN_QCT1501List.Any(p => p.Judge == "OK"))
                masterObj.CheckResult = "OK";
            else
                masterObj.CheckResult = null;
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("OutRepNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            StockList.Clear();
            MasterCustomerList.Clear();

            ModelService.ReLoad();

            var outDatePlan = dt_OutDatePlan.DateTime;
            var manager = lup_Manager.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => p.OutDatePlan == outDatePlan
                                                                    && (string.IsNullOrEmpty(manager) ? true : p.BusinessManagementId == manager)
                                                               )
                                                               .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);

            //재고량 가져오기
            StockList.AddRange(ModelService.GetChildList<VI_PROD_STOCK_ITEM>(p => true).ToList());

            //마스터 거래처 가져오기
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var Date = new SqlParameter("@Date", outDatePlan);
                var result = context.Database.SqlQuery<TEMP_ORD1101_CUSTOMER>("USP_GET_ORD1101_CUSTOMER_LIST @Date", Date).ToList();
                MasterCustomerList.AddRange(result);
            }
            MasterGridExControl.MainGrid.Columns["CustomerName"].Width = DetailGridExControl.MainGrid.Columns["TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName")].Width;
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1103;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                MasterGridExControl.MainGrid.Columns["CustomerName"].Width = DetailGridExControl.MainGrid.Columns["TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName")].Width;
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_ORD1101List.OrderBy(p => p.OrderNo).ThenBy(p => p.OrderSeq).ToList();
            //DetailGridBindingSource.DataSource = masterObj.TN_ORD1101List.OrderBy(p => p.OrderSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();

            MasterGridExControl.MainGrid.Columns["CustomerName"].Width = DetailGridExControl.MainGrid.Columns["TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName")].Width;
        }

        protected override void AddRowClicked()
        {
            TN_ORD1103 newobj = new TN_ORD1103()
            {
                OutRepNo = DbRequestHandler.GetSeqMonth("OPR"),
                OutDatePlan = DateTime.Today,
                BusinessManagementId = GlobalVariable.LoginId,
                EditRowFlag = "Y"
            };

            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            MasterGridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var obj = MasterGridBindingSource.Current as TN_ORD1103;
            if (obj == null) return;

            if (obj.TN_ORD1101List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("ShipmentReportMasterInfo"), LabelConvert.GetLabelText("ShipmentReportDetailInfo"), LabelConvert.GetLabelText("ShipmentReportDetailInfo")));
                return;
            }

            MasterGridBindingSource.RemoveCurrent();
            ModelService.Delete(obj);
            MasterGridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }
        
        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1103;
            if (masterObj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Constraint, "XFORD1101");
            param.SetValue(PopupParameter.Value_1, masterObj.BusinessManagementId);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_ORD1100, param, AddDetailRowCallBack);
            form.ShowPopup(true);
        }
        
        private void AddDetailRowCallBack(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_ORD1103;
            if (masterObj == null) return;

            List<TN_ORD1100> returnList = (List<TN_ORD1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var v in returnList)
            {
                var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();

                var returnObj = ModelService.GetChildList<TN_ORD1100>(p => p.DelivNo == v.DelivNo).FirstOrDefault();

                //var p_ItemCode = new SqlParameter("@ItemCode", v.ItemCode);
                //var p_DelivQty = new SqlParameter("@CheckQty", returnObj.DelivQty);
                //var ds = DbRequestHandler.GetDataSet("USP_GET_ORD1101_PRODUCT_LOT_NO", p_ItemCode, p_DelivQty);

                //string lotNo = string.Empty;
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                //{
                //    lotNo = ds.Tables[0].Rows[0][0].GetNullToEmpty();
                //}

                var newObj = new TN_ORD1101();
                newObj.OrderNo = v.OrderNo;
                newObj.OrderSeq = v.OrderSeq;
                newObj.DelivNo = v.DelivNo;
                newObj.OutRepNo = masterObj.OutRepNo;
                newObj.ItemCode = v.ItemCode;
                newObj.CustomerCode = v.CustomerCode;
                newObj.OutPlanQty = returnObj.RemainOutPlanQty;
                newObj.TN_ORD1100 = returnObj;
                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).First();
                newObj.TN_STD1400 = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == newObj.CustomerCode).First();
                newObj.EditRowFlag = "Y";
                //newObj.Temp = lotNo.GetNullToNull();

                masterObj.TN_ORD1101List.Add(newObj);
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
            var masterObj = MasterGridBindingSource.Current as TN_ORD1103;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_ORD1101;
            if (detailObj == null) return;

            masterObj.TN_ORD1101List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }

        protected override void FileChooseClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1103;
            if (masterObj == null || DetailGridBindingSource == null) return;

            var list = DetailGridBindingSource.List as List<TN_ORD1101>;
            if (list == null || list.Count == 0) return;

            if (masterObj.EditRowFlag == "Y" || list.Any(p => p.EditRowFlag == "Y") || IsFormControlChanged)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_99));
                return;
            }

            var checkList = list.ToList();//list.Where(p => true).OrderBy(p => p.TN_STD1400.CustomerName).ToList();
            if (checkList.Count == 0) return;

            int rowNumber = 1;
            foreach (var v in checkList.OrderBy(p => p.OrderNo).ThenBy(p => p.OrderSeq).ToList())
            {
                v.ReportNo = rowNumber++;
            }

            foreach (var v in checkList.Where(p => !p.Temp.IsNullOrEmpty()).ToList())
            {
                var cost = (v.TN_ORD1100.TN_ORD1001.OrderCost == null ? v.TN_STD1100.Cost.GetDecimalNullToZero() : v.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero());
                v.Amt = v.OutPlanQty.GetDecimalNullToZero() * cost;
                v.Temp1 = v.OrderNo + "&" + v.OrderSeq + "&" + v.Temp;
                bool firstFlag = false;
                var split1 = v.Temp.Split(';');
                foreach (var c in split1)
                {
                    var lotNo = c.Substring(0, c.IndexOf('('));
                    var obj = ModelService.GetChildList<TN_MPS1201>(p => p.ProductLotNo == lotNo).FirstOrDefault();

                    if (!firstFlag)
                    {
                        if (obj != null)
                            v.CustomerLotNo = obj.TN_MPS1200.Temp1;
                    }
                    else
                    {
                        if (obj != null)
                            v.CustomerLotNo += Environment.NewLine + obj.TN_MPS1200.Temp1;
                    }
                    firstFlag = true;
                }
            }

            foreach (var v in checkList.Where(p => p.Temp.IsNullOrEmpty()).ToList())
            {
                var cost = (v.TN_ORD1100.TN_ORD1001.OrderCost == null ? v.TN_STD1100.Cost.GetDecimalNullToZero() : v.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero());
                v.Amt = v.OutPlanQty.GetDecimalNullToZero() * cost;
                v.Temp1 = v.OrderNo + "&" + v.OrderSeq;
            }

            var checkList2 = checkList.OrderBy(p => p.OrderNo).ThenBy(p => p.OrderSeq).ToList();

            var listCount = checkList2.Count;

            int printRowCnt = 9;

            var valueCount = listCount / printRowCnt;
            var modCount = listCount % printRowCnt;


            if (modCount == 0)
            {
                ReportCreateToPrint(checkList2);
            }
            else
            {
                //var checkCount = (valueCount == 0 ? 1 : valueCount + 1) * printRowCnt;
                //while (true)
                //{
                //    checkList2.Add(new TN_ORD1101()
                //    {
                //        Temp2 = "-1"
                //    });
                //    if (checkList2.Count == checkCount) break;
                //}
                ReportCreateToPrint(checkList2);
            }
        }
        
        private void ReportCreateToPrint(List<TN_ORD1101> checkList)
        {
            try
            {
                WaitHandler.ShowWait();
                var report = new REPORT.XRORD1101_NEW3(checkList);
                report.CreateDocument();

                report.PrintingSystem.ShowMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                report.ShowPreview();

            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally
            {
                foreach (var v in checkList.Where(p => p.Temp2 == "-2").ToList())
                    checkList.Remove(v);
                foreach (var v in checkList.Where(p => p.Temp2 == "-1").ToList())
                    checkList.Remove(v);
                foreach (var v in checkList.Where(p => !p.Temp1.IsNullOrEmpty()).ToList())
                    v.Temp1 = null;

                WaitHandler.CloseWait();

                ActSave();
            }
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            DetailGridBindingSource.EndEdit();

            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            if (MasterGridBindingSource.DataSource != null && DetailGridBindingSource.DataSource != null)
            {
                var masterList = MasterGridBindingSource.List as List<TN_ORD1103>;
                masterList = masterList.Where(p => p.TN_ORD1101List.Any(c => c.EditRowFlag == "Y")).ToList();
                foreach (var v in masterList)
                {
                    foreach (var c in v.TN_ORD1101List.Where(p => p.EditRowFlag == "Y").ToList())
                    {
                        var p_ItemCode = new SqlParameter("@ItemCode", c.ItemCode);
                        var p_DelivQty = new SqlParameter("@CheckQty", c.OutPlanQty);
                        var ds = DbRequestHandler.GetDataSet("USP_GET_ORD1101_PRODUCT_LOT_NO", p_ItemCode, p_DelivQty);

                        string lotNo = string.Empty;
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            lotNo = ds.Tables[0].Rows[0][0].GetNullToEmpty();
                        }

                        c.Temp = lotNo.GetNullToNull();
                    }
                }

                MasterGridBindingSource.EndEdit();
                DetailGridBindingSource.EndEdit();
            }

            ModelService.Save();

            DataLoad();
        }

        //거래명세서출력
        private void BarButtonTradingStatePrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DetailGridBindingSource == null || DetailGridBindingSource.DataSource == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_ORD1103;

            var detailList = DetailGridBindingSource.List as List<TN_ORD1101>;
            if (detailList == null || detailList.Count == 0) return;

            var checkList = detailList.Where(p => p._Check == "Y").ToList();
            if (checkList.Count == 0) return;

            var costDisplayFlag = true;

            if (checkList.Any(p => p.RowId == 0))
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_99));
                return;
            }

            if (checkList.GroupBy(p => p.CustomerCode).Count() != 1)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_94));
                //MessageBoxHandler.Show("거래명세서는 같은 거래처로 출력이 가능합니다. 같은 거래처를 선택해 주세요.", "경고");
                return;
            }

            var result = MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_95), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo);
            //var result = MessageBoxHandler.Show("거래명세서에 단가를 입력하시겠습니까?", "경고", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) costDisplayFlag = true;
            else costDisplayFlag = false;

            try
            {
                WaitHandler.ShowWait();
                var groupList = new List<TradingStateGroupModel>();
                var cultureIndex = DataConvert.GetCultureIndex();

                groupList.AddRange(checkList.GroupBy(p => new { p.TN_STD1100, p.TN_ORD1100 }).Select(p => new TradingStateGroupModel
                {
                    ItemCode = p.Key.TN_STD1100.CustomerItemCode,
                    ItemName = p.Key.TN_STD1100.CustomerItemName,
                    Unit = p.Key.TN_STD1100.Unit,
                    Qty = p.Sum(c => c.OutPlanQty.GetDecimalNullToZero()),
                    Cost = costDisplayFlag ? (p.Key.TN_ORD1100.TN_ORD1001.OrderCost == null ? p.Key.TN_STD1100.Cost.GetDecimalNullToZero() : p.Key.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero()) : 0,
                    Count = p.Count()
                }));

                var unitList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit);
                var printList = new List<TEMP_TRADING_DETAIL>();
                int n = 0;
                foreach (var v in groupList)
                {
                    n++;
                    var newObj = new TEMP_TRADING_DETAIL()
                    {
                        No = n,
                        ItemCode = v.ItemCode,
                        ItemName = v.ItemName,
                        Unit = unitList.Where(p=>p.CodeVal == v.Unit).First().CodeName,
                        Qty = v.Qty,
                        Cost = v.Cost,
                    };
                    newObj.Amt = newObj.Qty * newObj.Cost;
                    printList.Add(newObj);
                }

                if (printList.Count > 0)
                {
                    var ToCustomerCode = checkList.First().CustomerCode;
                    var ToCustomerObj = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == ToCustomerCode).FirstOrDefault();
                    var FrCustomerObj = ModelService.GetChildList<TN_STD1400>(p => p.MyCompanyFlag == "Y").FirstOrDefault();

                    var mainReport = new XRORD1200_TRADING_NEW();

                    var rowCount = 8;
                    var valueCount = printList.Count / rowCount;
                    var modCount = printList.Count % rowCount;

                    if (valueCount == 0)
                    {
                        var report = new XRORD1200_TRADING_NEW(DateTime.Today.ToShortDateString(), masterObj.OutDatePlan.ToShortDateString(), ToCustomerObj, FrCustomerObj, printList.OrderBy(p => p.No).ToList());
                        report.CreateDocument();
                        mainReport.Pages.AddRange(report.Pages);
                    }
                    else if (valueCount > 0 && modCount == 0)
                    {
                        for (int i = 1; i <= valueCount; i++)
                        {
                            var min = i * 8 - 7;
                            var max = i * 8;
                            var report = new XRORD1200_TRADING_NEW(DateTime.Today.ToShortDateString(), masterObj.OutDatePlan.ToShortDateString(), ToCustomerObj, FrCustomerObj, printList.Where(p => p.No >= min && p.No <= max).OrderBy(p => p.No).ToList());
                            report.CreateDocument();
                            mainReport.Pages.AddRange(report.Pages);
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= valueCount; i++)
                        {
                            var min = i * 8 - 7;
                            var max = i * 8;
                            var report = new XRORD1200_TRADING_NEW(DateTime.Today.ToShortDateString(), masterObj.OutDatePlan.ToShortDateString(), ToCustomerObj, FrCustomerObj, printList.Where(p => p.No >= min && p.No <= max).OrderBy(p => p.No).ToList());
                            report.CreateDocument();
                            mainReport.Pages.AddRange(report.Pages);
                        }

                        var report2 = new XRORD1200_TRADING_NEW(DateTime.Today.ToShortDateString(), masterObj.OutDatePlan.ToShortDateString(), ToCustomerObj, FrCustomerObj, printList.Where(p => p.No > valueCount * 8).OrderBy(p => p.No).ToList());
                        report2.CreateDocument();
                        mainReport.Pages.AddRange(report2.Pages);
                    }

                    mainReport.PrintingSystem.ShowMarginsWarning = false;
                    mainReport.ShowPrintStatusDialog = false;
                    mainReport.ShowPreview();
                    
                    foreach (var v in checkList)
                        v._Check = "N";

                    DetailGridExControl.BestFitColumns();
                }
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally
            {
                WaitHandler.CloseWait();
            }
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "StockQty")
            {
                var itemCode = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ItemCode").GetNullToEmpty();
                if (!itemCode.IsNullOrEmpty())
                {
                    var stockObj = StockList.Where(p => p.ItemCode == itemCode).FirstOrDefault();
                    e.DisplayText = stockObj == null ? "0" : stockObj.SumStockQty.ToString("#,0.##");
                }
            }
        }

        private void MasterMainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "CustomerName")
            {
                var outRepNo = MasterGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "OutRepNo").GetNullToEmpty();
                if (!outRepNo.IsNullOrEmpty())
                {
                    var obj = MasterCustomerList.Where(p => p.OutRepNo == outRepNo).FirstOrDefault();
                    e.DisplayText = obj == null ? "" : obj.CustomerName;
                }
            }
        }

        private void MainView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var view = DetailGridExControl.MainGrid.MainView;
            var obj = DetailGridBindingSource.Current as TN_ORD1101;
            
            if (view.FocusedColumn.FieldName == "_Check" && obj.RowId == 0)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_99));
                return;
            }
        }

        private class TradingStateGroupModel
        {
            public int No { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Unit { get; set; }
            public decimal Qty { get; set; }
            public decimal Cost { get; set; }
            public decimal Count { get; set; }
        }
    }
}