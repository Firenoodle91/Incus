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
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 납품계획관리화면
    /// </summary>
    public partial class XFORD1100 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_ORD1000> ModelService = (IService<TN_ORD1000>)ProductionFactory.GetDomainService("TN_ORD1000");

        public XFORD1100()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            SubDetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;

            btn_OrderStatus.Click += Btn_OrdStatus_Click;

            dt_OrderDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            dt_OrderDate.DateToEdit.DateTime = DateTime.Today.AddDays(+30);
        }

        protected override void InitCombo()
        {
            lup_OrderCustomerCode.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());

            btn_OrderStatus.Text = LabelConvert.GetLabelText("OrderStatus") + "(&O)";
        }

        protected override void InitGrid()
        {
            //MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);

            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            MasterGridExControl.MainGrid.AddColumn("OrderDate", LabelConvert.GetLabelText("OrderDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderCustomerCode", LabelConvert.GetLabelText("OrderCustomerCode"), false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("OrderCustomer"));
            MasterGridExControl.MainGrid.AddColumn("OrderManagerName", LabelConvert.GetLabelText("OrderManagerName"));
            MasterGridExControl.MainGrid.AddColumn("OrderDueDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderId", LabelConvert.GetLabelText("OrderId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            //DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            IsDetailGridButtonFileChooseEnabled = UserRight.HasEdit; // 20220209 오세완 차장 이게 있어야 파일추가 버튼이 동작한다. 
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, UserRight.HasEdit); // 20220209 오세완 차장 수주정보 엑셀파일 일괄 업로드 기능 추가
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "불러오기");

            DetailGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"), false);
            DetailGridExControl.MainGrid.AddColumn("DelivFlag", LabelConvert.GetLabelText("DelivFlag"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("OrderSeq", LabelConvert.GetLabelText("OrderSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));

            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CarType", LabelConvert.GetLabelText("CarType"));
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            //DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));     // 2021-11-03 김진우 주임 제거
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));

            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("OrderQty", LabelConvert.GetLabelText("OrderQty"));
            DetailGridExControl.MainGrid.AddColumn("OrderCost", LabelConvert.GetLabelText("Cost"));
            DetailGridExControl.MainGrid.AddUnboundColumn("OrderAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([OrderQty],0) * ISNULL([OrderCost],0)", FormatType.Numeric, "#,###,###,###.##");
            DetailGridExControl.MainGrid.AddColumn("DlvDate", LabelConvert.GetLabelText("ObeyDate")); // 20220209 오세완 차장 납기준수일 추가 

            DetailGridExControl.MainGrid.AddColumn("CustomerWorkNo", LabelConvert.GetLabelText("CustomerWorkNo")); // 20220209 오세완 차장 엑셀 업로드 때문에 컬럼 추가 
            DetailGridExControl.MainGrid.AddColumn("CustomerOrderPartName", LabelConvert.GetLabelText("CustomerOrderPartName")); // 20220209 오세완 차장 엑셀 업로드 때문에 컬럼 추가 
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OrderQty", "OrderCost", "Memo");

            //SubDetailGridExControl.MainGrid.AddColumn("DelivNo", LabelConvert.GetLabelText("DelivNo"), false);
            SubDetailGridExControl.MainGrid.AddColumn("DelivNo", LabelConvert.GetLabelText("DelivNo")); // 20220119 오세완 차장 케이즈이노텍 스타일과 동일하게 설정
            SubDetailGridExControl.MainGrid.AddColumn("DelivDate", LabelConvert.GetLabelText("DelivDate"));
            SubDetailGridExControl.MainGrid.AddColumn("DelivQty", LabelConvert.GetLabelText("DelivQty"));
            SubDetailGridExControl.MainGrid.AddColumn("DelivId", LabelConvert.GetLabelText("DelivId"));
            SubDetailGridExControl.MainGrid.AddColumn("ProductionFlag", LabelConvert.GetLabelText("ProductionFlag"));
            SubDetailGridExControl.MainGrid.AddColumn("TurnKeyFlag", LabelConvert.GetLabelText("TurnKeyFlag"),false);
            SubDetailGridExControl.MainGrid.AddColumn("OutConfirmFlag", LabelConvert.GetLabelText("OutConfirmFlag"));
            SubDetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DelivDate", "DelivQty", "ProductionFlag", "TurnKeyFlag", "OutConfirmFlag", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1100>(SubDetailGridExControl);

            //2021-11-04 특정 컬럼 헤더만 글자 색상 변경
            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1100>(DetailGridExControl);
            DetailGridExControl.MainGrid.MainView.Columns["OrderQty"].AppearanceHeader.ForeColor = Color.Red;
        }

        protected override void InitRepository()
        {
            var UserList = ModelService.GetChildList<User>(p => p.Active != "N").ToList();
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OrderId", UserList, "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.CarType", DbRequestHandler.GetCommCode(MasterCodeSTR.CarType, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderQty", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderCost", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);

            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("DelivDate");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("DelivQty", DefaultBoolean.Default, "n2");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivId", UserList, "LoginId", "UserName");
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("ProductionFlag", "N");
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutConfirmFlag", "N");
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("TurnKeyFlag", "N");
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(SubDetailGridExControl, "Memo", UserRight.HasEdit);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("OrderNo");
            DetailGridRowLocator.GetCurrentRow("OrderSeq");
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            string orderCustomerCode = lup_OrderCustomerCode.EditValue.GetNullToEmpty();
            string itemCode = lup_Item.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(orderCustomerCode) ? true : p.OrderCustomerCode == orderCustomerCode)
                                                                        && (string.IsNullOrEmpty(itemCode) ? true : (p.TN_ORD1001List.Any(c => c.TN_STD1100.ItemCode == itemCode)))
                                                                        && (p.OrderDate >= dt_OrderDate.DateFrEdit.DateTime
                                                                         && p.OrderDate <= dt_OrderDate.DateToEdit.DateTime)
                                                                        && (p.OrderType == "양산")
                                                                     )
                                                                     .OrderBy(p => p.OrderNo)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            var MasterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (MasterObj == null)
            {
                return;
            }
            DetailGridBindingSource.DataSource = MasterObj.TN_ORD1001List.OrderBy(o => o.OrderSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

            if (MasterObj.TN_ORD1001List.Count > 0) DetailFocusedRowChanged();
        }

        protected override void DetailFocusedRowChanged()
        {
            var DetailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (DetailObj == null)
            {
                SubDetailGridExControl.MainGrid.Clear();
                return;
            }

            SubDetailGridBindingSource.DataSource = DetailObj.TN_ORD1100List.OrderBy(o => o.DelivNo).ToList();
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);
            param.SetValue(PopupParameter.Value_1, "양산");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000, param, AddRowPopupCallback);

            form.ShowPopup(true);
        }

        private void AddRowPopupCallback(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null || e.Map == null)
            {
                ActRefresh();
            }
            else
            {
                PopupDataParam.SetValue(PopupParameter.GridRowId_1, e.Map.GetValue(PopupParameter.GridRowId_1));
                ActRefresh();
            }
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Constraint, MasterCodeSTR.Contraint_ItemWAN);
            param.SetValue(PopupParameter.Value_1, masterObj.OrderCustomerCode);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_STD1100, param, AddOrderDetailCallback);
            form.ShowPopup(true);
        }

        private void AddOrderDetailCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            var returnList = (List<TN_STD1100>)e.Map.GetValue(PopupParameter.ReturnObject);
            foreach (var v in returnList)
            {
                var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();
                TN_STD1103 obj1 = ModelService.GetChildList<TN_STD1103>(p => p.CustomerCode == masterObj.OrderCustomerCode && p.ItemCode == v.ItemCode).OrderByDescending(o => o.ChangeDate).FirstOrDefault();
                var newObj = new TN_ORD1001();
                newObj.OrderNo = masterObj.OrderNo;
                newObj.OrderSeq = masterObj.TN_ORD1001List.Count == 0 ? 1 : masterObj.TN_ORD1001List.Max(p => p.OrderSeq) + 1;
                newObj.ItemCode = item.ItemCode;
                newObj.TN_STD1100 = item;
                newObj.OrderCost = obj1 == null ? item.Cost.GetDecimalNullToZero() : obj1.ChangeCost;
                newObj.ProductionFlag = "N";
                newObj.OutConfirmFlag = "Y";
                newObj.TurnKeyFlag = "N";
                newObj.NewRowFlag = "Y";
                newObj.DlvDate = masterObj.OrderDueDate;
                masterObj.TN_ORD1001List.Add(newObj);
                DetailGridBindingSource.Add(newObj);
            }
            if (returnList.Count > 0) SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void SubDetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            var DetailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (DetailObj == null) return;

            var delivQty = DetailObj.TN_ORD1100List.Count == 0 ? DetailObj.OrderQty : DetailObj.OrderQty - DetailObj.TN_ORD1100List.Sum(p => p.DelivQty);
            var obj = new TN_ORD1100()
            {
                OrderNo = DetailObj.OrderNo,
                OrderSeq = DetailObj.OrderSeq,
                DelivNo = DbRequestHandler.GetSeqMonth("DLV"),
                ItemCode = DetailObj.ItemCode,
                //CustomerCode = DetailObj.TN_ORD1000.OrderCustomerCode,
                CustomerCode = masterObj.OrderCustomerCode,
                DelivDate = DateTime.Today,
                DelivQty = 0,
                DelivId = GlobalVariable.LoginId,
                ProductionFlag = "Y",
                OutConfirmFlag = "Y",
                TurnKeyFlag = "N",
                Memo= DetailObj.Memo.GetNullToEmpty()
            };

            if (DetailObj.TN_ORD1100List.Count == 0)
                obj.DelivQty = DetailObj.OrderQty;
            else
            {
                var checkQty = DetailObj.OrderQty - DetailObj.TN_ORD1100List.Sum(p => p.DelivQty);
                if (checkQty > 0)
                    obj.DelivQty = checkQty;
            }

            var tN_MPS1100 = new TN_MPS1100();
            tN_MPS1100.PlanNo = DbRequestHandler.GetSeqMonth("WP");
            tN_MPS1100.OrderNo = obj.OrderNo;
            tN_MPS1100.OrderSeq = obj.OrderSeq;
            tN_MPS1100.DelivNo = obj.DelivNo;
            tN_MPS1100.ItemCode = obj.ItemCode;
            tN_MPS1100.CustomerCode = obj.CustomerCode;
            tN_MPS1100.PlanStartDate = DateTime.Today;
            tN_MPS1100.PlanEndDate = DateTime.Today;
            tN_MPS1100.Memo = obj.Memo;

            obj.TN_MPS1100List.Add(tN_MPS1100);

            SubDetailGridBindingSource.Add(obj);
            DetailObj.TN_ORD1100List.Add(obj);
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            if (masterObj.TN_ORD1001List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("OrderMasterInfo"), LabelConvert.GetLabelText("OrderDetailInfo"), LabelConvert.GetLabelText("OrderDetailInfo")));
                return;
            }
            ModelService.Delete(masterObj);
            MasterGridBindingSource.RemoveCurrent();
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (detailObj == null) return;

            if (detailObj.TN_ORD1100List.Count > 0)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_102));
                return;
            }

            masterObj.TN_ORD1001List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DeleteSubDetailRow()
        {
            var DetailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (DetailObj == null) return;

            var SubObj = SubDetailGridBindingSource.Current as TN_ORD1100;
            if (SubObj == null) return;

            if (SubObj.TN_MPS1100List.Any(a => a.TN_MPS1200List.Count > 0))
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("DelivInfo"), LabelConvert.GetLabelText("WorkInfo"), LabelConvert.GetLabelText("WorkInfo")));
                return;
            }

            if (SubObj.TN_ORD1200List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("DelivInfo"), LabelConvert.GetLabelText("OutInfo"), LabelConvert.GetLabelText("OutInfo")));
                return;
            }
            //else if (SubObj.TN_MPS1100List.Count > 0)
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("DelivInfo"), LabelConvert.GetLabelText("PlanInfo"), LabelConvert.GetLabelText("PlanInfo")));
            //    return;
            //}

            foreach (TN_MPS1100 s in SubObj.TN_MPS1100List)
            {
                //SubObj.TN_MPS1100List.Remove(s);
                SubObj.TN_MPS1100List.ToList().Remove(s);
            }

            DetailObj.TN_ORD1100List.Remove(SubObj);
            SubDetailGridBindingSource.RemoveCurrent();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            var FieldName = view.FocusedColumn.FieldName;
            var SubObj = SubDetailGridBindingSource.Current as TN_ORD1100;
            if (FieldName == "ProductionFlag")
            {
                if (SubObj.TurnKeyFlag == "Y")
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_55), LabelConvert.GetLabelText("TurnKeyFlag")));
                    e.Cancel = true;
                }
            }
        }
        
        /// <summary> 수주현황 </summary>
        private void Btn_OrdStatus_Click(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_ORD_STATUS, param, null);
            form.ShowPopup(false);
        }

        /// <summary>
        /// 20220209 오세완 차장 
        /// 수주정보 엑셀 불러오기 
        /// </summary>
        protected override void DetailFileChooseClicked()
        {
            TN_ORD1000 masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null)
                return;

            List<TN_ORD1001> detailObj = DetailGridBindingSource.List as List<TN_ORD1001>;
            if (detailObj != null)
                if (detailObj.Count > 0)
                {
                    MessageBoxHandler.Show("기존에 추가된 자료가 있으면 해당 기능을 사용할 수 없습니다.");
                    return;
                }

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "xlsx파일(*.xlsx)|*.xlsx|xls파일(*.xls)|*.xls";
            openFileDialog1.Title = "엑셀 저장";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    bool bSucessRead = false;
                    string sErrorMessage = "";
                    DataTable dt_From_excel = ExcelImport.GetTableFromExcel(openFileDialog1.FileName.ToString(), 0);

                    #region 엑셀파일 형식 검증
                    if (dt_From_excel.Columns.Count < 9)
                    {
                        sErrorMessage = "지정된 열의 갯수가 적습니다. 9가지입니다. ";
                    }
                    else if (dt_From_excel.Rows.Count < 0)
                    {
                        sErrorMessage = "불러올 데이터가 없습니다.";
                    }
                    else
                    {
                        int iCnt_Obeydate = 0;
                        int iCnt_Orderqty = 0;
                        decimal dOrderqty;
                        int iRow_Cnt = 0;
                        DateTime dt_Obeydate;
                        StringBuilder sb = new StringBuilder();
                        foreach (DataRow dr in dt_From_excel.Rows)
                        {
                            // (구매팀)납기준수일
                            if (DateTime.TryParse(dr[1].GetNullToEmpty(), out dt_Obeydate))
                                iCnt_Obeydate++;

                            // 발주수량
                            if (decimal.TryParse(dr[7].GetNullToEmpty(), out dOrderqty))
                                iCnt_Orderqty++;

                            if (dt_From_excel.Rows.Count == 1)
                                sb.Append(dr[3].GetNullToEmpty());
                            else
                            {
                                if (iRow_Cnt == dt_From_excel.Rows.Count - 1)
                                    sb.Append(dr[3].GetNullToEmpty());
                                else
                                {
                                    sb.Append(dr[3].GetNullToEmpty() + ",");
                                    iRow_Cnt++;
                                }
                            }
                        }

                        if (iCnt_Obeydate < dt_From_excel.Rows.Count)
                            sErrorMessage = dt_From_excel.Columns[1].ColumnName + "에 날짜 데이터가 잘못된 건이 있습니다.";
                        else if (iCnt_Orderqty < dt_From_excel.Rows.Count)
                            sErrorMessage = dt_From_excel.Columns[7].ColumnName + "에 숫자 데이터가 잘못된 건이 있습니다.";
                        else
                        {
                            // 20220210 오세완 차장 이 프로시저는 품번이 중복되어도 실제로 tn_std1100t에 등록된 숫자만 반환한다. 
                            string sSql = "exec USP_GET_COUNT_ITEMCODE '" + sb.ToString() + "' ";
                            DataTable dt = DbRequestHandler.GetDataTableSelect(sSql);

                            string sItemCode_Columnname = dt_From_excel.Columns[3].ColumnName;
                            if (dt == null)
                                sErrorMessage = sItemCode_Columnname + "이 존재하지 않는 건이 있습니다.";
                            else if (dt.Rows.Count == 0)
                                sErrorMessage = sItemCode_Columnname + "이 존재하지 않는 건이 있습니다.";
                            else
                            {
                                int iCount_Itemcode;
                                int.TryParse(dt.Rows[0][0].ToString(), out iCount_Itemcode);

                                // 20220210 오세완 차장 품번이 중복될 수 있어서 그걸 걸러내는 로직
                                // 20220214 밑에 로직이 어느순간부터 전체 건에서 중복을 제외한 리스트를 가져오는게 아니라 중복된 건을 가져오는 로직이 되어 버림
                                //var vItem_distinct = dt_From_excel.AsEnumerable().GroupBy(g => g.Field<string>(sItemCode_Columnname)).Where(p => p.Count() > 1).Select(s => s.Key);
                                var vItem_distinct = dt_From_excel.AsEnumerable().GroupBy(g => g.Field<string>(sItemCode_Columnname));
                                int iVar_Count = vItem_distinct.Count();
                                if (iCount_Itemcode < iVar_Count)
                                    sErrorMessage = sItemCode_Columnname + "이 존재하지 않는 건이 있습니다.";
                                else
                                    bSucessRead = true;
                            }
                        }
                    }
                    #endregion

                    if (bSucessRead)
                    {
                        //MessageBoxHandler.Show("읽기 성공");
                        foreach (DataRow dr in dt_From_excel.Rows)
                        {
                            string sDr_itemcode = dr[3].GetNullToEmpty();
                            TN_STD1100 search_std1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == sDr_itemcode &&
                                                                                                   p.UseFlag == "Y").FirstOrDefault();

                            TN_ORD1001 each = new TN_ORD1001()
                            {
                                OrderNo = masterObj.OrderNo,
                                OrderSeq = masterObj.TN_ORD1001List.Count == 0 ? 1 : masterObj.TN_ORD1001List.Max(p => p.OrderSeq) + 1,
                                ItemCode = dr[3].GetNullToEmpty(),
                                DlvDate = Convert.ToDateTime(dr[1].GetNullToEmpty()),
                                CustomerWorkNo = dr[2].GetNullToEmpty(),
                                CustomerOrderPartName = dr[6].GetNullToEmpty(),
                                OrderQty = dr[7].GetDecimalNullToZero(),
                                ProductionFlag = "N",
                                OutConfirmFlag = "Y",
                                TurnKeyFlag = "N",
                                NewRowFlag = "Y"
                            };

                            if (search_std1100 != null)
                                each.TN_STD1100 = search_std1100;

                            masterObj.TN_ORD1001List.Add(each);
                            DetailGridBindingSource.Add(each);
                        }

                        SetIsFormControlChanged(true);
                        DetailGridExControl.MainGrid.BestFitColumns();

                    }
                    else
                    {
                        MessageBoxHandler.Show(sErrorMessage);
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxHandler.Show(ex.ToString());
                }
            }
        }
    }
}