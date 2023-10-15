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
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;
using HKInc.Utils.Common;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.QCT
{
    /// <summary>
    /// 20210917 오세완 차장
    /// 출하검사
    /// </summary>
    public partial class XFQCT1900 : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        #region 전역변수
        IService<TN_QCT1900> ModelService = (IService<TN_QCT1900>)ProductionFactory.GetDomainService("TN_QCT1900");

        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemSpinEdit repositoryItemSpinEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;

        /// <summary>
        /// 20211006 오세완 차장 
        /// 검사상세목록 삭제시 임시 저장하여 entity로 삭제처리
        /// </summary>
        private List<TEMP_XFQCT1901_SUBDETAIL> subDetail_Delete = new List<TEMP_XFQCT1901_SUBDETAIL>();

        /// <summary>
        /// 20211006 오세완 차장 
        /// 출하검사 상세목록 삭제시 임시 저장하여 entity로 삭제처리
        /// </summary>
        private List<TEMP_XFQCT1900_DETAIL> detail_Delete = new List<TEMP_XFQCT1900_DETAIL>();

        /// <summary>
        /// 20211006 오세완 차장 
        /// 출하검사목록 삭제시 임시 저장하여 entity로 삭제처리
        /// </summary>
        private List<TEMP_XFQCT1900_MASTER> master_Delete = new List<TEMP_XFQCT1900_MASTER>();
        #endregion
        public XFQCT1900()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += SubDetailGridView_CellValueChanged;
            SubDetailGridExControl.MainGrid.MainView.RowCellStyle += SubDetailGRidView_RowCellStyle;
            SubDetailGridExControl.MainGrid.MainView.CustomRowCellEdit += SubDetailGRidView_CustomRowCellEditForEditing;
            SubDetailGridExControl.MainGrid.MainView.ShowingEditor += SubDetailGRidView_ShowingEditor;
            SubDetailGridExControl.MainGrid.MainView.KeyDown += SubDetailGRidView_KeyDown;

            DetailGridExControl.MainGrid.MainView.RowCellStyle += DetailGridView_RowCellStyle;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailGridView_CellValueChanged;

            MasterGridExControl.MainGrid.MainView.CellValueChanged += MasterGridView_CellValueChanged;

            repositoryItemGridLookUpEdit = new RepositoryItemGridLookUpEdit()
            {
                ValueMember = "CodeVal"
                ,
                DisplayMember = DataConvert.GetCultureDataFieldName("CodeName")
            };
            repositoryItemGridLookUpEdit.View.OptionsView.ShowColumnHeaders = false;
            repositoryItemGridLookUpEdit.View.OptionsBehavior.AutoPopulateColumns = false;
            repositoryItemGridLookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            repositoryItemGridLookUpEdit.View.OptionsBehavior.AllowIncrementalSearch = true;
            repositoryItemGridLookUpEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            repositoryItemGridLookUpEdit.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            repositoryItemGridLookUpEdit.NullText = "";
            repositoryItemGridLookUpEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
            repositoryItemGridLookUpEdit.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            repositoryItemGridLookUpEdit.Appearance.BackColor = Color.White;
            repositoryItemGridLookUpEdit.DataSource = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG).ToList();
            GridColumn col1 = repositoryItemGridLookUpEdit.View.Columns.AddField(repositoryItemGridLookUpEdit.DisplayMember);
            col1.VisibleIndex = 0;

            repositoryItemSpinEdit = new RepositoryItemSpinEdit();
            repositoryItemSpinEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            repositoryItemSpinEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repositoryItemSpinEdit.Mask.UseMaskAsDisplayFormat = true;
            repositoryItemSpinEdit.AllowMouseWheel = true;
            repositoryItemSpinEdit.Appearance.BackColor = Color.White;
            repositoryItemSpinEdit.Buttons[0].Visible = false;

            repositoryItemTextEdit = new RepositoryItemTextEdit();

            dpee_Shipment.DateFrEdit.DateTime = DateTime.Now;
            dpee_Shipment.DateToEdit.DateTime = DateTime.Now.AddDays(3);

            // 20211006 오세완 차장 출하검사 성적서 출력 버튼 추가
            var barButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonPrint.Id = 6;
            barButtonPrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            barButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barButtonPrint.Name = "barButtonPrint";
            barButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonPrint.ShortcutKeyDisplayString = "Alt+P";
            barButtonPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonPrint.Caption = LabelConvert.GetLabelText("InspectionReportPrint") + "[Alt+P]";
            barButtonPrint.ItemClick += BarButtonPrint_ItemClick;
            barButtonPrint.Alignment = BarItemLinkAlignment.Right;
            DetailGridExControl.BarTools.AddItem(barButtonPrint);
        }

        /// <summary>
        /// 20211013 오세완 차장 출하검사 산출물 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (DetailGridBindingSource == null)
                    return;

                MasterGridExControl.MainGrid.PostEditor();
                DetailGridExControl.MainGrid.PostEditor();
                SubDetailGridExControl.MainGrid.PostEditor();

                WaitHandler.ShowWait();

                List<TEMP_XFQCT1900_DETAIL> detail_Arr = DetailGridBindingSource.List as List<TEMP_XFQCT1900_DETAIL>;
                if(detail_Arr != null)
                    if(detail_Arr.Count > 0)
                    {
                        var vMainReport = new REPORT.XRQCT1900();
                        foreach(TEMP_XFQCT1900_DETAIL each in detail_Arr)
                        {
                            if(each._Check.GetNullToEmpty() == "Y")
                            {
                                TN_QCT1900 qct1900 = ModelService.GetList(p => p.ShipmentInspectionNo == each.ShipmentInspectionNo).FirstOrDefault();
                                if(qct1900 != null)
                                    if(qct1900.TN_QCT1901List.Count > 0)
                                    {
                                        TN_QCT1901 qct1901 = qct1900.TN_QCT1901List.Where(p => p.ShipmentInspectionSeq == each.Seq).FirstOrDefault();
                                        if(qct1901 != null)
                                        {
                                            if(qct1901.TN_QCT1100 != null)
                                            {
                                                var vSubReport = new REPORT.XRQCT1900(qct1901.TN_QCT1100);
                                                vSubReport.CreateDocument();
                                                vMainReport.Pages.AddRange(vSubReport.Pages);
                                                each._Check = "N";
                                            }
                                        }
                                    }
                            }
                        }

                        if(vMainReport.Pages.Count > 0)
                        {
                            vMainReport.PrintingSystem.ShowMarginsWarning = false;
                            vMainReport.ShowPrintStatusDialog = false;
                            vMainReport.ShowPreview();
                        }

                        DetailGridExControl.BestFitColumns();
                    }
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex.Message);
            }
            finally
            {
                WaitHandler.CloseWait();
            }
        }

        protected override void InitCombo()
        {
            slup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && 
                                                                                                                                               (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList());

            InitLabelConvert();
        }

        /// <summary>
        /// 20210924 오세완 차장
        /// 기존 화면 스타일을 따르지 않아서 직접 라벨을 변환해야 한다. 
        /// </summary>
        private void InitLabelConvert()
        {
            gcCondition.Text = LabelConvert.GetLabelText("Condition");
            gcShipMaster.Text = LabelConvert.GetLabelText("ShipMaster");
            gcShipDetail.Text = LabelConvert.GetLabelText("ShipDetail");
            gcInspectionDetail.Text = LabelConvert.GetLabelText("InspectionDetailList");
            lcShipmentPeriod.Text = LabelConvert.GetLabelText("CheckDate");
            lcItem.Text = LabelConvert.GetLabelText("Item");
        }

        protected override void InitGrid()
        {
            #region 출하검사 품목
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            MasterGridExControl.MainGrid.AddColumn("ShipmentInspectionNo", false);
            MasterGridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"));
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            MasterGridExControl.MainGrid.AddColumn("DelivNo", LabelConvert.GetLabelText("DelivNo"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));

            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"));
            MasterGridExControl.MainGrid.AddColumn("DelivQty", LabelConvert.GetLabelText("OrderQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            MasterGridExControl.MainGrid.AddColumn("SumOutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");

            MasterGridExControl.MainGrid.AddColumn("OutDate", LabelConvert.GetLabelText("OutDate"));
            MasterGridExControl.MainGrid.AddColumn("UserName", LabelConvert.GetLabelText("OutId"));
            MasterGridExControl.MainGrid.AddColumn("CheckDate", LabelConvert.GetLabelText("CheckDate"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckDate", "Memo");
            #endregion

            #region 출하검사 상세목록
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.MainGrid.AddColumn("ShipmentInspectionNo", false);
            DetailGridExControl.MainGrid.AddColumn("InspNo", false);
            DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            //DetailGridExControl.MainGrid.AddColumn("ShipmentInspectionSeq", LabelConvert.GetLabelText("Seq"));
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"));
            DetailGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));

            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            //DetailGridExControl.MainGrid.AddColumn("TN_QCT1100.CheckResult", LabelConvert.GetLabelText("CheckResult"));
            //DetailGridExControl.MainGrid.AddColumn("TN_QCT1100.CheckId", LabelConvert.GetLabelText("CheckId"));
            DetailGridExControl.MainGrid.AddColumn("CheckResult", LabelConvert.GetLabelText("CheckResult"));
            DetailGridExControl.MainGrid.AddColumn("CheckId", LabelConvert.GetLabelText("CheckId"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "TN_QCT1100.CheckResult", "TN_QCT1100.CheckId", "Memo");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckResult", "CheckId", "Memo", "_Check");
            #endregion

            #region 검사규격
            // 20210927 오세완 차장 3단 그리드 구조는 아래처럼 모든걸 false하면 동작하지 않는다. 
            //SubDetailGridExControl.SetToolbarVisible(false);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            SubDetailGridExControl.MainGrid.AddColumn("InspNo", LabelConvert.GetLabelText("InspNo"), false);
            SubDetailGridExControl.MainGrid.AddColumn("InspSeq", LabelConvert.GetLabelText("InspSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            //SubDetailGridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));
            SubDetailGridExControl.MainGrid.AddColumn("CheckWay_Value", LabelConvert.GetLabelText("InspectionWay")); // 20211013 오세완 차장 프로시저로 변경하면서 코드명을 반환하는 것으로 변경
            SubDetailGridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"));
            SubDetailGridExControl.MainGrid.AddColumn("CheckDataType", LabelConvert.GetLabelText("CheckDataType"), false);

            SubDetailGridExControl.MainGrid.AddColumn("InspectionReportMemo", LabelConvert.GetLabelText("InspectionReportMemo"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("CheckMin", LabelConvert.GetLabelText("CheckMin"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("CheckMax", LabelConvert.GetLabelText("CheckMax"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("CheckSpec", LabelConvert.GetLabelText("CheckSpec"), false);
            SubDetailGridExControl.MainGrid.AddColumn("CheckUpQuad", LabelConvert.GetLabelText("CheckUpQuad"), false);

            SubDetailGridExControl.MainGrid.AddColumn("CheckDownQuad", LabelConvert.GetLabelText("CheckDownQuad"), false);
            //SubDetailGridExControl.MainGrid.AddColumn("MaxReading", LabelConvert.GetLabelText("MaxReading"));
            SubDetailGridExControl.MainGrid.AddColumn("MaxReading_Value", LabelConvert.GetLabelText("MaxReading")); // 20211013 오세완 차장 프로시저로 변경하면서 코드명을 반환하는 것으로 변경
            SubDetailGridExControl.MainGrid.AddColumn("Reading1", LabelConvert.GetLabelText("Reading1"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading2", LabelConvert.GetLabelText("Reading2"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading3", LabelConvert.GetLabelText("Reading3"));

            SubDetailGridExControl.MainGrid.AddColumn("Reading4", LabelConvert.GetLabelText("Reading4"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading5", LabelConvert.GetLabelText("Reading5"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading6", LabelConvert.GetLabelText("Reading6"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading7", LabelConvert.GetLabelText("Reading7"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading8", LabelConvert.GetLabelText("Reading8"));

            SubDetailGridExControl.MainGrid.AddColumn("Reading9", LabelConvert.GetLabelText("Reading9"));
            SubDetailGridExControl.MainGrid.AddColumn("Judge", LabelConvert.GetLabelText("Judge"));
            SubDetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Reading1", "Reading2", "Reading3", "Reading4", "Reading5", "Reading6", "Reading7", "Reading8", "Reading9", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_QCT1101>(SubDetailGridExControl);
            #endregion

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", true);
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("CheckDate");

            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", true);
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_QCT1100.CheckId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            //DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("TN_QCT1100.CheckResult", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", 
            //    Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("CheckResult", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", 
                Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");

            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(SubDetailGridExControl, "Memo", true);
            //SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", 
            //    DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Judge", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", 
                DataConvert.GetCultureDataFieldName("CodeName"));
            //SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_QCT1001.MaxReading", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), "CodeVal", 
            //    DataConvert.GetCultureDataFieldName("CodeName"));

        }

        protected override void DataLoad()
        {
            #region grid focus
            GridRowLocator.GetCurrentRow("ShipmentInspectionNo");
            DetailGridRowLocator.GetCurrentRow("Seq");
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            Delete_Temp_Arr();

            ModelService.ReLoad();
            InitRepository();
            InitCombo();

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Datafrom = new SqlParameter("@DATE_FROM", dpee_Shipment.DateFrEdit.DateTime.ToShortDateString());
                SqlParameter sp_Datato = new SqlParameter("@DATE_TO", dpee_Shipment.DateToEdit.DateTime.ToShortDateString());
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", slup_Item.EditValue.GetNullToEmpty());

                var vResult = context.Database.SqlQuery<TEMP_XFQCT1900_MASTER>("USP_GET_XFQCT1900_MASTER @DATE_FROM, @DATE_TO, @ITEM_CODE", sp_Datafrom, sp_Datato, sp_Itemcode).ToList();
                if (vResult != null)
                {
                    //if (vResult.Count > 0) // 20211006 오세완 차장 0이 아니었다가 0이 된경우 초기화가 안되서 이 로직은 쓸수가 없다. 
                    MasterGridBindingSource.DataSource = vResult.OrderBy(p => p.OutDate).ToList();
                }
            }

            MasterGridExControl.DataSource = MasterGridBindingSource; // 20210924 오세완 차장 여기에 위치해야 팝업에서 추가한 내용이 출력이 된다. 
            MasterGridExControl.BestFitColumns();

            #region grid focus
            GridRowLocator.SetCurrentRow();
            DetailGridRowLocator.SetCurrentRow();
            #endregion
        }

        /// <summary>
        /// 20211006 오세완 차장 
        /// 임시 삭제 저장 공간 초기화
        /// </summary>
        private void Delete_Temp_Arr()
        {
            if (master_Delete != null)
                master_Delete.Clear();

            if (detail_Delete != null)
                detail_Delete.Clear();

            if (subDetail_Delete != null)
                subDetail_Delete.Clear();
        }

        protected override void AddRowClicked()
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.IsMultiSelect, true);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFQCT1900_MASTER, param, MasterAddCallBack);
            form.ShowPopup(true);
        }

        private void MasterAddCallBack(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null)
                return;

            var vReturnList = (List<TEMP_XPFQCT1900_LIST>)e.Map.GetValue(PopupParameter.ReturnObject);
            if(vReturnList != null)
                if(vReturnList.Count > 0)
                {
                    foreach(TEMP_XPFQCT1900_LIST each in vReturnList.ToList())
                    {
                        TEMP_XFQCT1900_MASTER tempMaster = new TEMP_XFQCT1900_MASTER()
                        {
                            OutNo = each.OutNo,
                            OrderNo = each.OrderNo,
                            DelivNo = each.DelivNo,
                            CustomerCode = each.CustomerCode,
                            CustomerName = each.CustomerName,
                            CustomerNameCHN = each.CustomerNameCHN,
                            CustomerNameENG = each.CustomerNameENG,
                            ItemCode = each.ItemCode,
                            ItemName1 = each.ItemName1,
                            ItemName = each.ItemName,
                            ItemNameCHN = each.ItemNameCHN,
                            ItemNameENG = each.ItemNameENG,
                            SumOutQty = each.OutQty,
                            DelivQty = each.OrderQty,
                            OutDate = each.OutDate,
                            OutId = each.OutId,
                            UserName = each.UserName,
                            CheckDate = DateTime.Now,
                            ShipmentInspectionNo = DbRequestHandler.GetSeqMonth("SI"),
                            Type = "Insert"
                        };

                        #region entity style
                        //TN_QCT1900 tempObj = new TN_QCT1900()
                        //{
                        //    ShipmentInspectionNo = tempMaster.ShipmentInspectionNo,
                        //    OutNo = tempMaster.OutNo,
                        //    CheckDate = tempMaster.CheckDate
                        //};
                        #endregion

                        if(MasterGridBindingSource.DataSource == null)
                        {
                            MasterGridBindingSource.DataSource = new List<TEMP_XFQCT1900_MASTER>();
                        }

                        MasterGridBindingSource.Add(tempMaster);
                        //ModelService.Insert(tempObj);
                        IsFormControlChanged = true;
                    }

                    MasterGridExControl.BestFitColumns();
                }
        }

        protected override void DeleteRow()
        {
            TEMP_XFQCT1900_MASTER tObj = MasterGridBindingSource.Current as TEMP_XFQCT1900_MASTER;
            if (tObj == null)
                return;

            #region entity style
            //List<TN_QCT1900> tempArr = ModelService.GetList(p => p.ShipmentInspectionNo == tObj.ShipmentInspectionNo);
            //if(tempArr != null)
            //    if(tempArr.Count > 0)
            //    {
            //        TN_QCT1900 tempObj = tempArr.FirstOrDefault();
            //        if(tempObj.TN_QCT1901List != null)
            //            if(tempObj.TN_QCT1901List.Count > 0)
            //            {
            //                MessageBoxHandler.Show("출하검사 상세 목록이 존재하여 삭제가 불가합니다.");
            //                return;
            //            }
            //            else
            //            {
            //                MasterGridBindingSource.Remove(tObj);
            //                ModelService.Delete(tempObj);
            //            }
            //    }
            //    else
            //    {
            //        // 20210924 오세완 차장 아직 저장이 되지 않고 그리드 안에서만 있는 경우에는 바인딩 객체만 삭제
            //        MasterGridBindingSource.Remove(tObj);
            //    }
            #endregion

            bool bDelete = false;
            List<TEMP_XFQCT1900_DETAIL> detail_Arr = DetailGridBindingSource.List as List<TEMP_XFQCT1900_DETAIL>;
            if (detail_Arr != null)
            {
                if (detail_Arr.Count > 0)
                {
                    MessageBoxHandler.Show("출하검사 상세 목록이 존재하여 삭제가 불가합니다.");
                    return;
                }
                else
                    bDelete = true;
            }
            else
                bDelete = true;

            if(bDelete)
            {
                master_Delete.Add(tObj);
                MasterGridBindingSource.Remove(tObj);
                MasterGridExControl.BestFitColumns();
            }
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();
            SubDetailGridExControl.MainGrid.PostEditor();

            MasterGridBindingSource.EndEdit();
            DetailGridBindingSource.EndEdit();
            SubDetailGridBindingSource.EndEdit();

            if(CheckSave())
            {
                SaveProcess();
                ModelService.Save();
            }
                
            //CheckUpdate();
            DataLoad();
        }

        /// <summary>
        /// 20211008 오세완 차장 저장 전 필수값을 확인
        /// </summary>
        /// <returns></returns>
        private bool CheckSave()
        {
            bool bResult = false;
            bool bFind = false;
            string sMessage = "";

            #region 출학검사 품목 
            List<TEMP_XFQCT1900_MASTER> tempList = MasterGridBindingSource.List as List<TEMP_XFQCT1900_MASTER>;
            if (tempList != null)
                if (tempList.Count > 0)
                    foreach (TEMP_XFQCT1900_MASTER each in tempList)
                    {
                        if (each.Type.GetNullToEmpty() != "")
                        {
                            if (each.CheckDate == null)
                            {
                                sMessage = "날짜가 없습니다. 입력해 주세요.";
                                bFind = true;
                            }
                        }

                        if (bFind)
                            break;
                    }

            if (bFind)
            {
                bResult = false;
                MessageBoxHandler.Show(sMessage);
                return bResult;
            }
            else
                bResult = true;

            #endregion

            #region 출하검사 상세목록
            List<TEMP_XFQCT1900_DETAIL> tempList_Detail = DetailGridBindingSource.List as List<TEMP_XFQCT1900_DETAIL>;
            if (tempList_Detail != null)
                if (tempList_Detail.Count > 0)
                {
                    foreach (TEMP_XFQCT1900_DETAIL each in tempList_Detail)
                    {
                        if (each.Type.GetNullToEmpty() != "")
                        {
                            if (each.CheckResult.GetNullToEmpty() == "")
                            {
                                sMessage = "검사 결과가 없습니다. 입력해 주세요.";
                                bFind = true;
                            }
                            else if(each.CheckId.GetNullToEmpty() == "")
                            {
                                sMessage = "검사자가 없습니다. 입력해 주세요.";
                                bFind = true;
                            }
                        }

                        if (bFind)
                            break;
                    }
                }

            if (bFind)
            {
                bResult = false;
                MessageBoxHandler.Show(sMessage);
                return bResult;
            }
            else
                bResult = true;

            #endregion

            #region 검사상세목록
            List<TEMP_XFQCT1901_SUBDETAIL> tempList_Sub = SubDetailGridBindingSource.List as List<TEMP_XFQCT1901_SUBDETAIL>;
            if (tempList_Sub != null)
                if (tempList_Sub.Count > 0)
                {
                    foreach (TEMP_XFQCT1901_SUBDETAIL each in tempList_Sub)
                    {
                        if (each.Type.GetNullToEmpty() != "")
                        {
                            int iCnt_Null = 0;

                            if (each.Reading1.GetNullToEmpty() == "")
                                iCnt_Null++;
                            else if (each.Reading2.GetNullToEmpty() == "")
                                iCnt_Null++;
                            else if (each.Reading3.GetNullToEmpty() == "")
                                iCnt_Null++;
                            else if (each.Reading4.GetNullToEmpty() == "")
                                iCnt_Null++;
                            else if (each.Reading5.GetNullToEmpty() == "")
                                iCnt_Null++;
                            else if (each.Reading6.GetNullToEmpty() == "")
                                iCnt_Null++;
                            else if (each.Reading7.GetNullToEmpty() == "")
                                iCnt_Null++;
                            else if (each.Reading8.GetNullToEmpty() == "")
                                iCnt_Null++;
                            else if (each.Reading9.GetNullToEmpty() == "")
                                iCnt_Null++;
                            
                            if(iCnt_Null > 8)
                            {
                                sMessage = "측정값을 1개 이상 입력해 주세요.";
                                bFind = true;
                            }
                        }

                        if (bFind)
                            break;
                    }
                }

            if (bFind)
            {
                bResult = false;
                MessageBoxHandler.Show(sMessage);
                return bResult;
            }
            else
                bResult = true;

            #endregion

            return bResult;
        }

        /// <summary>
        /// 20211008 오세완 차장 실제 저장을 담당
        /// </summary>
        private void SaveProcess()
        {
            #region 출하검사 품목 
            //TEMP_XFQCT1900_MASTER tObj = new TEMP_XFQCT1900_MASTER();
            TEMP_XFQCT1900_MASTER tObj = null; // 20211013 오세완 차장 마스터를 기존에 저장한 걸 불러오는 시나리오 때문에 null처리
            List<TEMP_XFQCT1900_MASTER> tempList = MasterGridBindingSource.List as List<TEMP_XFQCT1900_MASTER>;
            if(tempList != null)
                if(tempList.Count > 0)
                {
                    foreach (TEMP_XFQCT1900_MASTER each in tempList)
                    {
                        if (each.Type.GetNullToEmpty() != "")
                        {
                            tObj = each;

                            if (each.Type == "Insert")
                            {
                                TN_QCT1900 insertObj = new TN_QCT1900()
                                {
                                    ShipmentInspectionNo = each.ShipmentInspectionNo,
                                    OutNo = each.OutNo,
                                    CheckDate = each.CheckDate,
                                    Memo = each.Memo
                                };

                                ModelService.Insert(insertObj);
                            }
                            else if (each.Type == "Update")
                            {
                                TN_QCT1900 updateObj = ModelService.GetList(p => p.ShipmentInspectionNo == each.ShipmentInspectionNo).FirstOrDefault();
                                if (updateObj != null)
                                {
                                    updateObj.CheckDate = each.CheckDate;
                                    updateObj.Memo = each.Memo;

                                    ModelService.Update(updateObj);
                                }
                            }

                            each.Type = "";
                        }
                    }

                    if (tObj == null)
                        tObj = MasterGridBindingSource.Current as TEMP_XFQCT1900_MASTER;
                }
                    
            #endregion

            #region 출하검사 상세목록 & 검사상세목록
            List<TEMP_XFQCT1900_DETAIL> tempList_Detail = DetailGridBindingSource.List as List<TEMP_XFQCT1900_DETAIL>;
            if(tempList_Detail != null)
                if(tempList_Detail.Count > 0)
                {
                    foreach (TEMP_XFQCT1900_DETAIL each in tempList_Detail)
                    {
                        if (each.Type.GetNullToEmpty() != "")
                        {
                            if (each.Type == "Insert")
                            {
                                TN_QCT1901 insertObj_Detail = new TN_QCT1901()
                                {
                                    ShipmentInspectionNo = each.ShipmentInspectionNo,
                                    ShipmentInspectionSeq = each.Seq,
                                    ProductLotNo = each.ProductLotNo,
                                    OutQty = each.OutQty,
                                    InspNo = each.InspNo,
                                    Memo = each.Memo
                                };

                                TN_QCT1100 insertObj_qct1100 = new TN_QCT1100()
                                {
                                    InspNo = insertObj_Detail.InspNo,
                                    CheckDivision = MasterCodeSTR.InspectionDivision_ShipmentV2,
                                    CheckPoint = MasterCodeSTR.CheckPoint_General,
                                    WorkNo = tObj.OutNo,
                                    WorkSeq = insertObj_Detail.ShipmentInspectionSeq,
                                    CustomerCode = tObj.CustomerCode,
                                    ItemCode = tObj.ItemCode,
                                    InLotNo = insertObj_Detail.ShipmentInspectionNo,
                                    ProductLotNo = insertObj_Detail.ProductLotNo,
                                    NewRowFlag = "Y",
                                    EditRowFlag = "Y",
                                    ScmYn = "N",
                                    CheckDate = tObj.CheckDate,
                                    CheckResult = each.CheckResult,
                                    CheckId = each.CheckId
                                };

                                insertObj_Detail.TN_QCT1100 = insertObj_qct1100;
                                ModelService.InsertChild<TN_QCT1901>(insertObj_Detail);
                            }
                            else if (each.Type == "Update")
                            {
                                TN_QCT1901 updateObj_Detail = ModelService.GetChildList<TN_QCT1901>(p => p.ShipmentInspectionNo == each.ShipmentInspectionNo &&
                                                                                                         p.ShipmentInspectionSeq == each.Seq).FirstOrDefault();
                                if (updateObj_Detail != null)
                                {
                                    updateObj_Detail.ProductLotNo = each.ProductLotNo;
                                    updateObj_Detail.OutQty = each.OutQty;
                                    updateObj_Detail.Memo = each.Memo;

                                    if(updateObj_Detail.TN_QCT1100 != null)
                                    {
                                        updateObj_Detail.TN_QCT1100.CheckResult = each.CheckResult;
                                        updateObj_Detail.TN_QCT1100.CheckId = each.CheckId;
                                    }

                                    ModelService.UpdateChild<TN_QCT1901>(updateObj_Detail);
                                }
                            }

                            each.Type = "";
                        }
                    }
                }
            #endregion

            #region 검사상세목록
            List<TEMP_XFQCT1901_SUBDETAIL> tempList_Sub = SubDetailGridBindingSource.List as List<TEMP_XFQCT1901_SUBDETAIL>;
            if (tempList_Sub != null)
                if(tempList_Sub.Count > 0)
                {
                    foreach(TEMP_XFQCT1901_SUBDETAIL each in tempList_Sub)
                    {
                        if(each.Type.GetNullToEmpty() != "")
                        {
                            if(each.Type == "Insert")
                            {
                                TN_QCT1101 insertObj_Sub = new TN_QCT1101()
                                {
                                    InspNo = each.InspNo,
                                    InspSeq = each.InspSeq,
                                    RevNo = each.REV_NO,
                                    ItemCode = each.ITEM_CODE,
                                    Seq = each.InspSeq,
                                    CheckWay = each.CheckWay,
                                    CheckList = each.CheckList,
                                    CheckMax = each.CheckMax,
                                    CheckMin = each.CheckMin,
                                    CheckSpec = each.CheckSpec,
                                    CheckUpQuad = each.CheckUpQuad,
                                    CheckDownQuad = each.CheckDownQuad,
                                    CheckDataType = each.CheckDataType,
                                    Reading1 = each.Reading1,
                                    Reading2 = each.Reading2,
                                    Reading3 = each.Reading3,
                                    Reading4 = each.Reading4,
                                    Reading5 = each.Reading5,
                                    Reading6 = each.Reading6,
                                    Reading7 = each.Reading7,
                                    Reading8 = each.Reading8,
                                    Reading9 = each.Reading9,
                                    Judge = each.Judge,
                                    Memo = each.Memo,
                                    ScmYn = "N"
                                };

                                ModelService.InsertChild<TN_QCT1101>(insertObj_Sub);
                            }
                            else if(each.Type == "Update")
                            {
                                TN_QCT1101 updateObj_Sub = ModelService.GetChildList<TN_QCT1101>(p => p.InspNo == each.InspNo &&
                                                                                                      p.InspSeq == each.InspSeq).FirstOrDefault();
                                if(updateObj_Sub != null)
                                {
                                    updateObj_Sub.Reading1 = each.Reading1;
                                    updateObj_Sub.Reading2 = each.Reading2;
                                    updateObj_Sub.Reading3 = each.Reading3;
                                    updateObj_Sub.Reading4 = each.Reading4;
                                    updateObj_Sub.Reading5 = each.Reading5;
                                    updateObj_Sub.Reading6 = each.Reading6;
                                    updateObj_Sub.Reading7 = each.Reading7;
                                    updateObj_Sub.Reading8 = each.Reading8;
                                    updateObj_Sub.Reading9 = each.Reading9;
                                    updateObj_Sub.Memo = each.Memo;
                                    updateObj_Sub.Judge = each.Judge;

                                    ModelService.UpdateChild<TN_QCT1101>(updateObj_Sub);
                                }
                            }

                            each.Type = "";
                        }
                    }
                }
            #endregion

            #region 삭제목록 진행
            if(subDetail_Delete != null)
                if(subDetail_Delete.Count > 0)
                {
                    foreach(TEMP_XFQCT1901_SUBDETAIL each in subDetail_Delete)
                    {
                        TN_QCT1101 qct1101Obj = ModelService.GetChildList<TN_QCT1101>(p => p.InspNo == each.InspNo &&
                                                                                       p.InspSeq == each.InspSeq).FirstOrDefault();
                        if (qct1101Obj != null)
                            ModelService.RemoveChild<TN_QCT1101>(qct1101Obj);
                    }
                }

            if(detail_Delete != null)
                if(detail_Delete.Count > 0)
                {
                    foreach(TEMP_XFQCT1900_DETAIL each in detail_Delete)
                    {
                        TN_QCT1901 qct1901Obj = ModelService.GetChildList<TN_QCT1901>(p => p.ShipmentInspectionNo == each.ShipmentInspectionNo &&
                                                                                           p.ShipmentInspectionSeq == each.Seq).FirstOrDefault();
                        if (qct1901Obj != null)
                        {
                            TN_QCT1100 qct1100Obj = ModelService.GetChildList<TN_QCT1100>(p => p.InspNo == each.InspNo).FirstOrDefault();
                            if(qct1100Obj != null)
                                ModelService.RemoveChild<TN_QCT1100>(qct1100Obj);

                            ModelService.RemoveChild<TN_QCT1901>(qct1901Obj);
                        }
                    }
                }

            if(master_Delete != null)
                if(master_Delete.Count > 0)
                {
                    foreach(TEMP_XFQCT1900_MASTER each in master_Delete)
                    {
                        TN_QCT1900 qct1900Obj = ModelService.GetList(p => p.ShipmentInspectionNo == each.ShipmentInspectionNo).FirstOrDefault();
                        if (qct1900Obj != null)
                            ModelService.Delete(qct1900Obj);
                    }
                }
            #endregion
        }

        protected override void MasterFocusedRowChanged()
        {
            //DetailGridRowLocator.GetCurrentRow("Seq");
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            TEMP_XFQCT1900_MASTER tObj = MasterGridBindingSource.Current as TEMP_XFQCT1900_MASTER;
            if (tObj == null)
                return;

            #region entity style
            //List<TN_QCT1900> tempArr = ModelService.GetList(p => p.ShipmentInspectionNo == tObj.ShipmentInspectionNo).ToList();
            //if (tempArr != null)
            //    if(tempArr.Count > 0)
            //    {
            //        TN_QCT1900 masterObj = tempArr.FirstOrDefault();
            //        if (masterObj != null)
            //            if (masterObj.TN_QCT1901List.Count > 0)
            //            {
            //                // 출하검사 상세 목록 출력
            //                DetailGridBindingSource.DataSource = masterObj.TN_QCT1901List.OrderBy(p => p.ShipmentInspectionSeq).ToList();
            //            }
            //    }
            #endregion 

            bool bNotExist = false;
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Sino = new SqlParameter("@SI_NO", tObj.ShipmentInspectionNo);
                var vResult = context.Database.SqlQuery<TEMP_XFQCT1900_DETAIL>("USP_GET_XFQCT1900_DETAIL @SI_NO", sp_Sino).ToList();
                if (vResult == null)
                    bNotExist = true;
                else if (vResult.Count == 0)
                    bNotExist = true;
                else
                    DetailGridBindingSource.DataSource = vResult.OrderBy(o=>o.Seq).ToList();
            }

            if (bNotExist)
                DetailGridBindingSource.DataSource = new List<TEMP_XFQCT1900_DETAIL>();

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();
        }

        protected override void DetailAddRowClicked()
        {
            TEMP_XFQCT1900_MASTER tObj = MasterGridBindingSource.Current as TEMP_XFQCT1900_MASTER;
            if (tObj == null)
                return;

            // 20211013 오세완 차장 다중선책을 못하게 하여 저장을 유도했기 때문에 여기서도 기존에 상세목록이 저장이 안되어 있는 상황을 확인
            List<TEMP_XFQCT1900_DETAIL> detailArr = DetailGridBindingSource.List as List<TEMP_XFQCT1900_DETAIL>;
            if(detailArr != null)
                if(detailArr.Count >= 1)
                {
                    bool bNullValue = false;
                    string sMessage = "";
                    int iCnt_NullType = 0;
                    foreach(TEMP_XFQCT1900_DETAIL each in detailArr)
                    {
                        string sType = each.Type.GetNullToEmpty();
                        if (sType == "Insert" || sType == "Update")
                        {
                            if (each.CheckResult.GetNullToEmpty() == "")
                            {
                                sMessage = "검사 결과가 없습니다. 입력해 주세요.";
                                bNullValue = true;
                            }
                            else if (each.CheckId.GetNullToEmpty() == "")
                            {
                                sMessage = "검사자가 없습니다. 입력해 주세요.";
                                bNullValue = true;
                            }
                        }
                        else
                            iCnt_NullType++;
                        
                    }

                    if (bNullValue)
                    {
                        MessageBoxHandler.Show(sMessage);
                        return;
                    }
                    else
                    {
                        if(iCnt_NullType > 0 && iCnt_NullType != detailArr.Count)
                        {
                            DialogResult result = MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_1), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (result == DialogResult.Yes)
                            {
                                DataSave();
                            }
                            else
                                return;
                        }
                    }
                }

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            //param.SetValue(PopupParameter.IsMultiSelect, true);
            param.SetValue(PopupParameter.IsMultiSelect, false); // 20211008 오세완 차장 subdetail까지 있는 구조라 여러개를 추가하면 subdetail을 제어할 수가 없어서 부득이 하게 삭제처리
            param.SetValue(PopupParameter.Value_1, tObj.OutNo);

            // 20210927 오세완 차장 추가한 생산 lotno 중복추가 방지하려고 했으나 최초 추가인 경우 save가 되지 않아서 값을 읽지 못한다. 
            //List<TN_QCT1901> tempList = DetailGridBindingSource.List as List<TN_QCT1901>;
            //param.SetValue(PopupParameter.Value_2, tempList);

            //var vTemplist = DetailGridBindingSource.List as List<TN_QCT1901>;
            //param.SetValue(PopupParameter.Value_2, vTemplist);

            // 20210927 오세완 차장 간단하게 bindingsource로 처리되지 않아서 생산 lotno만 처리
            List<string> sArr_Productlotno = new List<string>();
            GridView gv = DetailGridExControl.MainGrid.MainView;
            for(int i=0;i<DetailGridExControl.MainGrid.MainView.RowCount;i++)
            {
                string sProductLotNo = gv.GetRowCellValue(i, gv.Columns["ProductLotNo"]).GetNullToEmpty();
                sArr_Productlotno.Add(sProductLotNo);
            }
            param.SetValue(PopupParameter.Value_2, sArr_Productlotno);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFQCT1900_DETAIL, param, DetailAddCallback_V2);
            form.ShowPopup(true);
        }

        private void DetailAddCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null)
                return;

            var vReturnList = (List<TN_ORD1201>)e.Map.GetValue(PopupParameter.ReturnObject);
            if (vReturnList != null)
                if (vReturnList.Count > 0)
                {
                    TEMP_XFQCT1900_MASTER tObj = MasterGridBindingSource.Current as TEMP_XFQCT1900_MASTER;
                    if (tObj == null)
                        return;

                    List<TN_QCT1900> tempArr = ModelService.GetList(p => p.ShipmentInspectionNo == tObj.ShipmentInspectionNo).ToList();
                    bool bInserted = false;

                    if (tempArr != null)
                        if(tempArr.Count > 0)
                            bInserted = true;

                    if(bInserted)
                    {
                        TN_QCT1900 masterObj = tempArr.FirstOrDefault();

                        foreach (TN_ORD1201 each in vReturnList.ToList())
                        {
                            TN_QCT1901 detailObj = new TN_QCT1901()
                            {
                                ShipmentInspectionNo = masterObj.ShipmentInspectionNo,
                                ShipmentInspectionSeq = masterObj.TN_QCT1901List.Count == 0 ? 1 : masterObj.TN_QCT1901List.Max(o=>o.ShipmentInspectionSeq) + 1,
                                ProductLotNo = each.ProductLotNo,
                                OutQty = each.OutQty,
                                NewRowFlag = "Y"
                            };

                            TN_QCT1100 siMaster = new TN_QCT1100()
                            {
                                InspNo = DbRequestHandler.GetSeqMonth(MasterCodeSTR.InspectionDivision_ShipmentV2),
                                CheckDivision = MasterCodeSTR.InspectionDivision_ShipmentV2,
                                CheckPoint = MasterCodeSTR.CheckPoint_General,
                                WorkNo = tObj.OutNo,
                                WorkSeq = detailObj.ShipmentInspectionSeq,
                                CustomerCode = tObj.CustomerCode,
                                ItemCode = tObj.ItemCode,
                                InLotNo = detailObj.ShipmentInspectionNo,
                                ProductLotNo = detailObj.ProductLotNo,
                                TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == tObj.ItemCode).FirstOrDefault(),
                                NewRowFlag = "Y",
                                EditRowFlag = "Y",
                                ScmYn = "N",
                                CheckDate = masterObj.CheckDate
                            };

                            detailObj.InspNo = siMaster.InspNo;
                            detailObj.TN_QCT1100 = siMaster;

                            DetailGridBindingSource.Add(detailObj);
                            masterObj.TN_QCT1901List.Add(detailObj);
                            ModelService.InsertChild(detailObj);
                        }
                    }
                    else
                    {
                        foreach (TN_ORD1201 each in vReturnList.ToList())
                        {
                            TN_QCT1901 detailObj1 = new TN_QCT1901()
                            {
                                ShipmentInspectionNo = tObj.ShipmentInspectionNo,
                                ShipmentInspectionSeq = DetailGridBindingSource.Count == 0 ? 1 : DetailGridBindingSource.Count + 1,
                                ProductLotNo = each.ProductLotNo,
                                OutQty = each.OutQty
                            };

                            TN_QCT1100 siMaster1 = new TN_QCT1100()
                            {
                                InspNo = DbRequestHandler.GetSeqMonth(MasterCodeSTR.InspectionDivision_ShipmentV2),
                                CheckDivision = MasterCodeSTR.InspectionDivision_ShipmentV2,
                                CheckPoint = MasterCodeSTR.CheckPoint_General,
                                WorkNo = tObj.OutNo,
                                WorkSeq = detailObj1.ShipmentInspectionSeq,
                                CustomerCode = tObj.CustomerCode,
                                ItemCode = tObj.ItemCode,
                                InLotNo = detailObj1.ShipmentInspectionNo,
                                ProductLotNo = detailObj1.ProductLotNo,
                                TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == tObj.ItemCode).FirstOrDefault(),
                                NewRowFlag = "Y",
                                EditRowFlag = "Y",
                                ScmYn = "N",
                                CheckDate = tObj.CheckDate
                            };

                            detailObj1.InspNo = siMaster1.InspNo;
                            detailObj1.TN_QCT1100 = siMaster1;

                            DetailGridBindingSource.Add(detailObj1);
                            ModelService.InsertChild(detailObj1);
                        }
                    }

                    DetailGridExControl.DataSource = DetailGridBindingSource;
                    DetailGridExControl.BestFitColumns();
                }
        }

        /// <summary>
        /// 20210930 오세완 차장 entity아닌 버전
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailAddCallback_V2(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null)
                return;

            #region 다수 건을 선택하는 로직, 사용안함
            //var vReturnList = (List<TN_ORD1201>)e.Map.GetValue(PopupParameter.ReturnObject);
            //if (vReturnList != null)
            //    if (vReturnList.Count > 0)
            //    {
            //        TEMP_XFQCT1900_MASTER tObj = MasterGridBindingSource.Current as TEMP_XFQCT1900_MASTER;
            //        if (tObj == null)
            //            return;

            //        foreach (TN_ORD1201 each in vReturnList.ToList())
            //        {
            //            TEMP_XFQCT1900_DETAIL tempObj = new TEMP_XFQCT1900_DETAIL()
            //            {
            //                ShipmentInspectionNo = tObj.ShipmentInspectionNo,
            //                Seq = DetailGridBindingSource.Count == 0 ? 1 : DetailGridBindingSource.Count + 1,
            //                ProductLotNo = each.ProductLotNo,
            //                OutQty = each.OutQty,
            //                InspNo = DbRequestHandler.GetSeqMonth(MasterCodeSTR.InspectionDivision_ShipmentV2),
            //                Type = "Insert"
            //            };

            //            DetailGridBindingSource.Add(tempObj);
            //        }

            //        DetailGridExControl.DataSource = DetailGridBindingSource;
            //        DetailGridExControl.BestFitColumns();
            //    }
            #endregion

            var vReturn = (TN_ORD1201)e.Map.GetValue(PopupParameter.ReturnObject);
            if (vReturn != null)
                {
                    TEMP_XFQCT1900_MASTER tObj = MasterGridBindingSource.Current as TEMP_XFQCT1900_MASTER;
                    if (tObj == null)
                        return;

                    TEMP_XFQCT1900_DETAIL tempObj = new TEMP_XFQCT1900_DETAIL()
                    {
                        ShipmentInspectionNo = tObj.ShipmentInspectionNo,
                        Seq = DetailGridBindingSource.Count == 0 ? 1 : DetailGridBindingSource.Count + 1,
                        ProductLotNo = vReturn.ProductLotNo,
                        OutQty = vReturn.OutQty,
                        InspNo = DbRequestHandler.GetSeqMonth(MasterCodeSTR.InspectionDivision_ShipmentV2),
                        Type = "Insert"
                    };

                    DetailGridBindingSource.Add(tempObj);

                    DetailGridExControl.DataSource = DetailGridBindingSource;
                    DetailGridExControl.BestFitColumns();
                }


        }

        protected override void DetailFocusedRowChanged()
        {
            #region entity style
            //TN_QCT1901 tObj = DetailGridBindingSource.Current as TN_QCT1901;
            //if (tObj == null)
            //    return;

            //if(tObj.TN_QCT1100 != null)
            //{
            //    if (tObj.TN_QCT1100.TN_QCT1101List != null)
            //        if (tObj.TN_QCT1100.TN_QCT1101List.Count > 0)
            //            SubDetailGridBindingSource.DataSource = tObj.TN_QCT1100.TN_QCT1101List.OrderBy(p => p.InspSeq).ToList();
            //}
            #endregion

            TEMP_XFQCT1900_DETAIL detailObj = DetailGridBindingSource.Current as TEMP_XFQCT1900_DETAIL;
            if (detailObj == null)
                return;

            // 20211007 오세완 차장 초기에 생산 lot번호가 2개 이상인 경우 1개를 검사상세목록까지 저장한 후 다른 생산 lot번호를 입력하려 할때 기존 데이터 저장처리
            List<TEMP_XFQCT1901_SUBDETAIL> subDetail_Arr = SubDetailGridBindingSource.List as List<TEMP_XFQCT1901_SUBDETAIL>;
            if(subDetail_Arr != null)
                if(subDetail_Arr.Count > 0)
                {
                    int iCnt = subDetail_Arr.Where(p => p.Type == "Insert").ToList().Count;
                    if(iCnt > 0)
                    {
                        DialogResult result = MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_1), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if(result == DialogResult.Yes)
                        {
                            DataSave();
                            return;
                        }
                    }
                }

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Inspno = new SqlParameter("@INSP_NO", detailObj.InspNo.GetNullToEmpty());
                var vResult = context.Database.SqlQuery<TEMP_XFQCT1901_SUBDETAIL>("USP_GET_XFQCT1901_SUBDETAIL @INSP_NO", sp_Inspno).ToList();
                if (vResult != null)
                {
                    //if (vResult.Count > 0) // 20211006 오세완 차장 0이 아니었다가 0이 되는 경우 기존 항목이 초기화되지 않아서 생략해야 함
                    SubDetailGridBindingSource.DataSource = vResult.OrderBy(o => o.InspSeq).ToList();
                }
                    
            }

            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            #region entity style
            //TN_QCT1901 tObj = DetailGridBindingSource.Current as TN_QCT1901;
            //if (tObj == null)
            //    return;

            //if (tObj.TN_QCT1100 != null)
            //{
            //    if (tObj.TN_QCT1100.TN_QCT1101List != null)
            //    {
            //        if (tObj.TN_QCT1100.TN_QCT1101List.Count > 0)
            //        {
            //            MessageBoxHandler.Show("출하검사 상세 목록이 있어서 삭제가 불가합니다.");
            //            return;
            //        }
            //        else
            //        {
            //            DetailGridBindingSource.Remove(tObj);
            //            ModelService.RemoveChild(tObj);
            //        }
            //    }
            //    else
            //    {
            //        DetailGridBindingSource.Remove(tObj);
            //    }
            //}
            //else
            //{
            //    DetailGridBindingSource.Remove(tObj);
            //}
            #endregion

            TEMP_XFQCT1900_DETAIL detailObj = DetailGridBindingSource.Current as TEMP_XFQCT1900_DETAIL;
            if (detailObj == null)
                return;

            bool bDelete = false;
            List<TEMP_XFQCT1901_SUBDETAIL> subDetail_Arr = SubDetailGridBindingSource.List as List<TEMP_XFQCT1901_SUBDETAIL>;
            if(subDetail_Arr != null)
            {
                if (subDetail_Arr.Count > 0)
                {
                    MessageBoxHandler.Show("검사상세목록이 있어서 삭제가 불가합니다.");
                    return;
                }
                else
                    bDelete = true;
            }
                
            if(bDelete)
            {
                detail_Delete.Add(detailObj);
                DetailGridBindingSource.Remove(detailObj);
                DetailGridExControl.BestFitColumns();
            }
        }

        protected override void SubDetailAddRowClicked()
        {
            TEMP_XFQCT1900_MASTER masterObj = MasterGridBindingSource.Current as TEMP_XFQCT1900_MASTER;
            if (masterObj == null)
                return;

            //TN_QCT1901 detailObj = DetailGridBindingSource.Current as TN_QCT1901;
            TEMP_XFQCT1900_DETAIL detailObj = DetailGridBindingSource.Current as TEMP_XFQCT1900_DETAIL;
            if (detailObj == null)
                return;

            bool bNotExists_Rev = false;
            TN_QCT1000 tempObj = null;

            // 20210928 오세완 차장 검사규격을 저장후 바로 조회가 되게 해보려고 추가
            ModelService.ReLoad();
            List <TN_QCT1000> qcRev_Arr = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == masterObj.ItemCode &&
                                                                                     p.UseFlag == "Y");
            if (qcRev_Arr == null)
                bNotExists_Rev = true;
            else if (qcRev_Arr.Count == 0)
                bNotExists_Rev = true;
            else
            {
                tempObj = qcRev_Arr.FirstOrDefault();
                if (tempObj == null)
                    bNotExists_Rev = true;
            }

            List<TN_QCT1001> qcList_Arr = null;
            if (bNotExists_Rev)
            {
                MessageBoxHandler.Show("해당품번의 리비전이 존재하지 않습니다.");
                return;
            }
            else
            {
                bool bNotExits_Content = false;
                List<TN_QCT1001> qcList_Content = tempObj.TN_QCT1001List.Where(p => p.UseFlag == "Y" &&
                                                                                    p.CheckDivision == MasterCodeSTR.InspectionDivision_ShipmentV2).ToList();
                if (qcList_Content == null)
                    bNotExits_Content = true;
                else if (qcList_Content.Count == 0)
                    bNotExits_Content = true;
                else
                {
                    qcList_Arr = qcList_Content.OrderBy(o => o.DisplayOrder).ToList();
                }

                if(bNotExits_Content)
                {
                    MessageBoxHandler.Show("해당품번의 출하검사 규격이 존재하지 않습니다.");
                    return;
                }
            }

            if(qcList_Arr != null)
                if(qcList_Arr.Count > 0)
                {
                    int iCnt = 0;
                    List<TEMP_XFQCT1901_SUBDETAIL> subDetail_Arr = SubDetailGridBindingSource.List as List<TEMP_XFQCT1901_SUBDETAIL>;
                    if (subDetail_Arr != null)
                    {
                        if(subDetail_Arr.Count > 0)
                            iCnt = subDetail_Arr.Max(o => o.InspSeq);
                    }

                    if (iCnt > 0)
                    {
                        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_174));
                    }
                    else
                    {
                        List<TN_STD1000> Arr_CheckList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay);
                        // 20211013 오세완 차장 최대시료수 코드 / 값 출력 추가
                        List<TN_STD1000> Arr_MaxReading = DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber);
                        foreach (TN_QCT1001 each in qcList_Arr)
                        {
                            // 20211005 오세완 차장 해당 값에 맞지 않으면 insert하지 않는다. 
                            if (tempObj.TN_QCT1001List.All(p => p.RevNo == each.RevNo &&
                                                                p.ItemCode == each.ItemCode &&
                                                                p.Seq == each.Seq))
                                continue;

                            #region entity style
                            //TN_QCT1101 subDetail_Obj = new TN_QCT1101()
                            //{
                            //    InspNo = detailObj.InspNo,
                            //    InspSeq = detailObj.TN_QCT1100.TN_QCT1101List.Count == 0 ? 1 : detailObj.TN_QCT1100.TN_QCT1101List.Max(o => o.InspSeq) + 1,
                            //    RevNo = each.RevNo,
                            //    ItemCode = each.ItemCode,
                            //    Seq = each.Seq,
                            //    CheckWay = each.CheckWay,
                            //    CheckList = each.CheckList,
                            //    CheckMax = each.CheckMax,
                            //    CheckMin = each.CheckMin,
                            //    CheckSpec = each.CheckSpec,
                            //    CheckUpQuad = each.CheckUpQuad,
                            //    CheckDownQuad = each.CheckDownQuad,
                            //    CheckDataType = each.CheckDataType,
                            //    TN_QCT1001 = each,
                            //    EditRowFlag = "Y"
                            //};

                            //SubDetailGridBindingSource.Add(subDetail_Obj);
                            //detailObj.TN_QCT1100.TN_QCT1101List.Add(subDetail_Obj);
                            //ModelService.InsertChild<TN_QCT1101>(subDetail_Obj);
                            #endregion

                            TEMP_XFQCT1901_SUBDETAIL subDetail_Obj = new TEMP_XFQCT1901_SUBDETAIL()
                            {
                                InspNo = detailObj.InspNo,
                                CheckWay = each.CheckWay,
                                CheckWay_Value = Arr_CheckList.Where(p=>p.CodeVal == each.CheckWay).Select(s=>s.CodeName).FirstOrDefault(),
                                CheckList = each.CheckList,
                                CheckMax = each.CheckMax,
                                CheckMin = each.CheckMin,
                                CheckSpec = each.CheckSpec,
                                InspectionReportMemo = each.InspectionReportMemo,
                                CheckUpQuad = each.CheckUpQuad,
                                CheckDownQuad = each.CheckDownQuad,
                                CheckDataType = each.CheckDataType,
                                REV_NO = each.RevNo,
                                ITEM_CODE = each.ItemCode,
                                MaxReading_Value = Arr_MaxReading.Where(p=>p.CodeVal == each.MaxReading).Select(s=>s.CodeName).FirstOrDefault(),
                                Type = "Insert"
                            };

                            iCnt += 1;
                            subDetail_Obj.InspSeq = iCnt;

                            if (iCnt == 1)
                                SubDetailGridBindingSource.DataSource = new List<TEMP_XFQCT1901_SUBDETAIL>();

                            SubDetailGridBindingSource.Add(subDetail_Obj);
                        }
                    }

                    SubDetailGridExControl.BestFitColumns();
                }
        }

        protected override void DeleteSubDetailRow()
        {
            #region entity style
            //TN_QCT1901 detailObj = DetailGridBindingSource.Current as TN_QCT1901;
            //if (detailObj == null)
            //    return;

            //TN_QCT1101 subdetailObj = SubDetailGridBindingSource.Current as TN_QCT1101;
            //if (subdetailObj == null)
            //    return;

            //if(detailObj.TN_QCT1100 != null)
            //{
            //    detailObj.TN_QCT1100.TN_QCT1101List.Remove(subdetailObj);
            //    SubDetailGridBindingSource.Remove(subdetailObj);
            //    SubDetailGridExControl.BestFitColumns();
            //}

            //// 20210928 오세완 차장 검사상세목록이 존재하는 경우에만 전체 판정에 영향을 주는 걸로.
            //if(detailObj.TN_QCT1100.TN_QCT1101List.Count > 0)
            //    MasterCheckResult(detailObj);
            #endregion

            TEMP_XFQCT1900_DETAIL detailObj = DetailGridBindingSource.Current as TEMP_XFQCT1900_DETAIL;
            if (detailObj == null)
                return;

            TEMP_XFQCT1901_SUBDETAIL subDetailObj = SubDetailGridBindingSource.Current as TEMP_XFQCT1901_SUBDETAIL;
            if (subDetailObj == null)
                return;

            subDetail_Delete.Add(subDetailObj);
            SubDetailGridBindingSource.Remove(subDetailObj);
            SubDetailGridExControl.BestFitColumns();

            MasterCheckResult_V2(detailObj);
        }

        private void SubDetailGridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            #region entity style
            //TN_QCT1901 detailObj = DetailGridBindingSource.Current as TN_QCT1901;
            //if (detailObj == null)
            //    return;

            //TN_QCT1101 subdetailObj = SubDetailGridBindingSource.Current as TN_QCT1101;
            //if (subdetailObj == null)
            //    return;

            //subdetailObj.EditRowFlag = "Y";
            //if (e.Column.FieldName.Contains("Reading"))
            //{
            //    string sCheckdatatype = subdetailObj.CheckDataType.GetNullToEmpty();
            //    if(sCheckdatatype == MasterCodeSTR.CheckDataType_C)
            //    {
            //        DetailCheckEye(subdetailObj);
            //    }
            //    else if (sCheckdatatype.IsNullOrEmpty())
            //    {
            //        DetailCheckInput(subdetailObj);
            //    }
            //    else
            //    {
            //        DetailCheckInput(subdetailObj);
            //    }
            //}

            //MasterCheckResult(detailObj);
            #endregion

            TEMP_XFQCT1900_DETAIL detailObj = DetailGridBindingSource.Current as TEMP_XFQCT1900_DETAIL;
            if (detailObj == null)
                return;

            TEMP_XFQCT1901_SUBDETAIL subDetailObj = SubDetailGridBindingSource.Current as TEMP_XFQCT1901_SUBDETAIL;
            if (subDetailObj == null)
                return;


            if (e.Column.FieldName.Contains("Reading"))
            {
                string sCheckdatatype = subDetailObj.CheckDataType.GetNullToEmpty();
                if (sCheckdatatype == MasterCodeSTR.CheckDataType_C)
                    DetailCheckEye_V2(subDetailObj);
                else if (sCheckdatatype.IsNullOrEmpty())
                    DetailCheckInput_V2(subDetailObj);
                else
                    DetailCheckInput_V2(subDetailObj);
            }

            if (subDetailObj.Type != "Insert")
            {
                // 20211006 오세완 차장 어떤 항목이 변했는지 체크르 하기 위해서는 고려해야할 사항이 너무 많아서 insert가 아니면 전부 값이 변한 경우는 update로 간주한다. 
                subDetailObj.Type = "Update";
            }

            MasterCheckResult_V2(detailObj);
        }

        /// <summary>
        /// 20210928 오세완 차장 육안검사 판정
        /// </summary>
        /// <param name="detailObj"></param>
        private void DetailCheckEye(TN_QCT1101 detailObj)
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

            DetailGridExControl.BestFitColumns();
        }

        private void DetailCheckEye_V2(TEMP_XFQCT1901_SUBDETAIL detailObj)
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

            DetailGridExControl.BestFitColumns();
        }

        /// <summary>
        /// 20210928 오세완 차장 검사규격에 따른 공차계산 판정
        /// </summary>
        /// <param name="detailObj"></param>
        private void DetailCheckInput(TN_QCT1101 detailObj)
        {
            var checkSpec = detailObj.CheckSpec.GetDecimalNullToNull();
            if (checkSpec == null)
                return;

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

            DetailGridExControl.BestFitColumns();
        }

        /// <summary>
        /// 20211013 오세완 차장 프로시저를 사용하는 버전
        /// </summary>
        /// <param name="detailObj"></param>
        private void DetailCheckInput_V2(TEMP_XFQCT1901_SUBDETAIL detailObj)
        {
            var checkSpec = detailObj.CheckSpec.GetDecimalNullToNull();
            if (checkSpec == null)
                return;

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

            DetailGridExControl.BestFitColumns();
        }

        /// <summary>
        /// 20210928 오세완 차장 검사상세목록의 최종 판정을 출하검사 상세목록에 반영
        /// </summary>
        /// <param name="masterObj"></param>
        private void MasterCheckResult(TN_QCT1901 detailObj)
        {
            if(detailObj.TN_QCT1100 != null)
            {
                if (detailObj.TN_QCT1100.TN_QCT1101List.Any(p => p.Judge == "NG"))
                    detailObj.TN_QCT1100.CheckResult = "NG";
                else if (detailObj.TN_QCT1100.TN_QCT1101List.Any(p => p.Judge == "OK"))
                    detailObj.TN_QCT1100.CheckResult = "OK";
                else
                    detailObj.TN_QCT1100.CheckResult = null;

                // 20210928 오세완 차장 검사상세목록에서 전체 판정을 변경후 저장하였을 때 반영이 안되서 추가
                if(detailObj.NewRowFlag.GetNullToEmpty() == "")
                {
                    //    ModelService.UpdateChild<TN_QCT1901>(detailObj);
                }

            }
                

            DetailGridExControl.BestFitColumns();
        }

        /// <summary>
        /// 20211013 오세완 자장 프로시저 형태를 사용하는 버전
        /// </summary>
        /// <param name="detailObj"></param>
        private void MasterCheckResult_V2(TEMP_XFQCT1900_DETAIL detailObj)
        {
            bool bUpdate = false;
            List<TEMP_XFQCT1901_SUBDETAIL> subDetail_Arr = SubDetailGridBindingSource.List as List<TEMP_XFQCT1901_SUBDETAIL>;
            if(subDetail_Arr != null)
                if(subDetail_Arr.Count > 0)
                {
                    if (subDetail_Arr.Any(p => p.Judge == "NG"))
                    {
                        if (detailObj.CheckResult == "OK")
                            bUpdate = true;

                        detailObj.CheckResult = "NG";
                        
                    }
                    else if (subDetail_Arr.Any(p => p.Judge == "OK"))
                    {
                        if (detailObj.CheckResult == "NG")
                            bUpdate = true;

                        detailObj.CheckResult = "OK";
                    }
                    else
                        detailObj.CheckResult = "";

                    if(bUpdate)
                    {
                        if (detailObj.Type != "Insert")
                            detailObj.Type = "Update";
                    }
                }

            DetailGridExControl.BestFitColumns();
        }

        private void SubDetailGRidView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("Reading") && !e.Column.FieldName.Contains("MaxReading"))
                {
                    string sCheckDataType = gv.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    if (sCheckDataType == MasterCodeSTR.CheckDataType_C)
                    {
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                        string sReadingValue = e.CellValue.GetNullToEmpty();
                        e.Appearance.ForeColor = DetailCheckInputColor(sReadingValue);
                    }
                    else
                    {
                        //검사방법이 치수검사일 때 검사데이터타입이 없을 경우 
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                        decimal? dCheckSpec = gv.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                        decimal dCheckUpQuad = gv.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                        decimal dCheckDownQuad = gv.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                        decimal? dReadingValue = e.CellValue.GetDecimalNullToNull();
                        e.Appearance.ForeColor = DetailCheckInputColor(dCheckSpec, dCheckUpQuad, dCheckDownQuad, dReadingValue);
                    }
                }
                else if (e.Column.FieldName == "Judge")
                {
                    string sJudgeValue = gv.GetRowCellValue(e.RowHandle, "Judge").GetNullToEmpty();
                    if (sJudgeValue == "NG")
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
        }

        /// <summary>
        /// 20210926 오세완 차장 문자로 검사값을 입력하는 경우 column의 색상으로 판정 결과에 따라서 변경
        /// </summary>
        /// <param name="readingValue"></param>
        /// <returns></returns>
        private Color DetailCheckInputColor(string readingValue)
        {
            if (!readingValue.IsNullOrEmpty())
            {
                if (readingValue == "OK")
                    return Color.Black;
                else
                    return Color.Red;
            }
            else
                return Color.Black;
        }

        /// <summary>
        /// 20210928 오세완 차장 기준이 있는 경우 
        /// </summary>
        /// <param name="checkSpec"></param>
        /// <param name="checkUpQuad"></param>
        /// <param name="checkDownQuad"></param>
        /// <param name="readingValue"></param>
        /// <returns></returns>
        private Color DetailCheckInputColor(decimal? checkSpec, decimal checkUpQuad, decimal checkDownQuad, decimal? readingValue)
        {
            if (checkSpec == null)
                return Color.Black;
            else
            {
                var checkUp = checkSpec + checkUpQuad;
                var checkDown = checkSpec - checkDownQuad;

                if (readingValue != null)
                {
                    if (readingValue >= checkDown && readingValue <= checkUp)
                        return Color.Black;
                    else
                        return Color.Red;
                }
                else
                    return Color.Black;
            }
        }

        private void SubDetailGRidView_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("Reading") && !e.Column.FieldName.Contains("MaxReading"))
                {
                    string sCheckDataType = gv.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    if (sCheckDataType == MasterCodeSTR.CheckDataType_C)
                    {
                        //육안검사 
                        e.RepositoryItem = repositoryItemGridLookUpEdit;
                    }
                    else if (sCheckDataType.IsNullOrEmpty())
                    {
                        e.RepositoryItem = repositoryItemTextEdit;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sCheckDataType)) repositoryItemSpinEdit.Mask.EditMask = string.Format("N{0}", sCheckDataType);
                        e.RepositoryItem = repositoryItemSpinEdit;
                    }
                }
            }
        }

        private void SubDetailGRidView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView gv = sender as GridView;

            #region entity style
            //TN_QCT1101 subdetailObj = SubDetailGridBindingSource.Current as TN_QCT1101;
            //if (subdetailObj == null) return;

            //string sMaxReading = subdetailObj.TN_QCT1001.MaxReading.GetNullToEmpty();
            //if (gv.FocusedColumn.FieldName.Contains("Reading") && !gv.FocusedColumn.FieldName.Contains("MaxReading"))
            //{
            //    if (gv.FocusedColumn.FieldName.Right(1).GetDecimalNullToNull() > sMaxReading.GetDecimalNullToNull())
            //        e.Cancel = true;
            //}
            #endregion

            TEMP_XFQCT1901_SUBDETAIL subDetailObj = SubDetailGridBindingSource.Current as TEMP_XFQCT1901_SUBDETAIL;
            if (subDetailObj == null)
                return;

            string sMaxReading = subDetailObj.MaxReading.GetNullToEmpty();
            decimal dMaxReading;
            if(decimal.TryParse(sMaxReading, out dMaxReading))
            {
                if (gv.FocusedColumn.FieldName.Contains("Reading") && !gv.FocusedColumn.FieldName.Contains("MaxReading"))
                {
                    if (gv.FocusedColumn.FieldName.Right(1).GetDecimalNullToNull() > dMaxReading)
                        e.Cancel = true;
                }
            }
        }

        private void SubDetailGRidView_KeyDown(object sender, KeyEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.KeyCode == Keys.Enter)
            {
                #region entity style
                //if (gv.FocusedColumn.FieldName.Contains("Reading") && !gv.FocusedColumn.FieldName.Contains("MaxReading"))
                //{
                //    string sMaxReading = gv.GetFocusedRowCellValue("TN_QCT1001.MaxReading").GetNullToEmpty();
                //    if (gv.FocusedColumn.FieldName.Right(1).GetDecimalNullToNull() == sMaxReading.GetDecimalNullToNull())
                //    {
                //        if (gv.RowCount == gv.FocusedRowHandle + 1)
                //        {
                //            gv.FocusedRowHandle = 0;
                //        }
                //        else
                //        {
                //            gv.FocusedRowHandle = gv.FocusedRowHandle + 1;
                //        }

                //        gv.FocusedColumn = gv.Columns["Reading1"];
                //    }
                //    else
                //    {
                //        if (gv.FocusedColumn.VisibleIndex + 1 == gv.VisibleColumns.Count)
                //        {
                //            gv.FocusedColumn = gv.VisibleColumns[0];
                //            if (gv.RowCount == gv.FocusedRowHandle + 1)
                //                gv.FocusedRowHandle = 0;
                //            else
                //                gv.FocusedRowHandle = gv.FocusedRowHandle + 1;
                //        }
                //        else
                //        {
                //            gv.FocusedColumn = gv.VisibleColumns[gv.FocusedColumn.VisibleIndex + 1];
                //        }
                //    }
                //}
                //else
                //{
                //    if (gv.FocusedColumn.VisibleIndex + 1 == gv.VisibleColumns.Count)
                //    {
                //        gv.FocusedColumn = gv.VisibleColumns[0];
                //        if (gv.RowCount == gv.FocusedRowHandle + 1)
                //            gv.FocusedRowHandle = 0;
                //        else
                //            gv.FocusedRowHandle = gv.FocusedRowHandle + 1;
                //    }
                //    else
                //    {
                //        gv.FocusedColumn = gv.VisibleColumns[gv.FocusedColumn.VisibleIndex + 1];
                //    }
                //}
                #endregion

                bool bPassReading = false;
                if (gv.FocusedColumn.FieldName.Contains("Reading") && !gv.FocusedColumn.FieldName.Contains("MaxReading"))
                {
                    string sMaxReading = gv.GetFocusedRowCellValue("MaxReading").GetNullToEmpty();
                    decimal dMaxReading;
                    if (decimal.TryParse(sMaxReading, out dMaxReading))
                    {
                        if (gv.FocusedColumn.FieldName.Right(1).GetDecimalNullToNull() == dMaxReading)
                        {
                            if (gv.RowCount == gv.FocusedRowHandle + 1)
                                gv.FocusedRowHandle = 0;
                            else
                                gv.FocusedRowHandle = gv.FocusedRowHandle + 1;

                            gv.FocusedColumn = gv.Columns["Reading1"];
                        }
                        else
                            bPassReading = true;
                    }
                }
                else
                    bPassReading = true;
                
                if (bPassReading)
                {
                    if (gv.FocusedColumn.VisibleIndex + 1 == gv.VisibleColumns.Count)
                    {
                        gv.FocusedColumn = gv.VisibleColumns[0];

                        if (gv.RowCount == gv.FocusedRowHandle + 1)
                            gv.FocusedRowHandle = 0;
                        else
                            gv.FocusedRowHandle = gv.FocusedRowHandle + 1;
                    }
                    else
                    {
                        gv.FocusedColumn = gv.VisibleColumns[gv.FocusedColumn.VisibleIndex + 1];
                    }
                }
            }
        }

        private void DetailGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.RowHandle >= 0)
            {
                #region entity style
                //if (e.Column.FieldName == "TN_QCT1100.CheckResult")
                //{
                //    string sJudgeValue = gv.GetRowCellValue(e.RowHandle, "TN_QCT1100.CheckResult").GetNullToEmpty();
                //    if (sJudgeValue == "NG")
                //    {
                //        e.Appearance.ForeColor = Color.Red;
                //    }
                //    else
                //    {
                //        e.Appearance.ForeColor = Color.Black;
                //    }
                //}
                #endregion

                if (e.Column.FieldName == "CheckResult")
                {
                    string sJudgeValue = gv.GetRowCellValue(e.RowHandle, "CheckResult").GetNullToEmpty();
                    if (sJudgeValue == "NG")
                        e.Appearance.ForeColor = Color.Red;
                    else
                        e.Appearance.ForeColor = Color.Black;
                }
            }
        }

        private void DetailGridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TEMP_XFQCT1900_DETAIL detailObj = DetailGridBindingSource.Current as TEMP_XFQCT1900_DETAIL;
            if (detailObj == null)
                return;

            if (detailObj.Type.GetNullToEmpty() == "")
                detailObj.Type = "Update";
        }

        /// <summary>
        /// 20210928 오세완 차장 
        /// 출하검사 품목의 검사일을 상세목록에서 전부 적용을 시켜야 하기 때문에 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterGridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TEMP_XFQCT1900_MASTER masterObj = MasterGridBindingSource.Current as TEMP_XFQCT1900_MASTER;
            if (masterObj == null)
                return;

            if (masterObj.Type.GetNullToEmpty() == "")
                masterObj.Type = "Update";

            #region entity style
            //TN_QCT1901 detailObj = DetailGridBindingSource.Current as TN_QCT1901;
            //if (detailObj == null)
            //    return;

            //if (e.Column.FieldName.Contains("CheckDate"))
            //{
            //    if (detailObj.TN_QCT1100 != null)
            //        detailObj.TN_QCT1100.CheckDate = masterObj.CheckDate;
            //}
            #endregion
        }
    }
}