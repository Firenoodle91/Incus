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
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using System.Data.SqlClient;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.ORD
{
    /// <summary>
    /// 판매계획관리
    /// </summary>
    public partial class XFORD1100 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        #region 전역변수
        IService<TN_ORD1100> ModelService = (IService<TN_ORD1100>)ProductionFactory.GetDomainService("TN_ORD1100");
        IService<TN_ORD1000> ModelService1 = (IService<TN_ORD1000>)ProductionFactory.GetDomainService("TN_ORD1000");
        IService<TN_ORD1002> ModelService2 = (IService<TN_ORD1002>)ProductionFactory.GetDomainService("TN_ORD1002");
        #endregion

        public XFORD1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today.AddDays(+30);

            // 20220426 오세완 차장 판매계획 중 수주를 등록하고 수정하는 기능이 없어서 추가 
            // 20220426 오세완 차장 이차장과 협의 하여 수정 기능을 빼기로 함 
            //MasterGridExControl.MainGrid.MainView.RowClick += MasterGridView_RowClick;
            SubDetailGridExControl.MainGrid.MainView.RowCellStyle += SubDetail_RowCellStyle;
            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += SubDetail_CellValueChanged;
        }

        protected override void InitCombo()
        {
            lupCustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("OrderDate", LabelConvert.GetLabelText("OrderDate"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("OrderCust"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("CustOrderNo", LabelConvert.GetLabelText("CustOrderNo"), HorzAlignment.Center, false);       // 2022-03-14 김진우 고객사발주번호 제거
            MasterGridExControl.MainGrid.AddColumn("CustOrderid", LabelConvert.GetLabelText("VanEmp"), HorzAlignment.Center, true);             // 2022-03-14 김진우 Id를 id로 변경
            MasterGridExControl.MainGrid.AddColumn("PeriodDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("OrderId", LabelConvert.GetLabelText("EmpCode"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo1"));
            MasterGridExControl.BestFitColumns();

            DetailGridExControl.MainGrid.Init();
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"), HorzAlignment.Center, false);
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("No"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", LabelConvert.GetLabelText("차종"), HorzAlignment.Center, true);
            //DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Lctype", LabelConvert.GetLabelText("기종"), HorzAlignment.Center, true);         // 2022-03-14 김진우 기종 삭제
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", LabelConvert.GetLabelText("품번"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("Cost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "n0");         // 2022-04-15 김진우 추가
            DetailGridExControl.MainGrid.AddColumn("OrderQty", LabelConvert.GetLabelText("Qty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("VI_PRODQTY_MST.Stockqty", "재고량", HorzAlignment.Far, FormatType.Numeric, "N0");
            DetailGridExControl.MainGrid.AddColumn("PeriodDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Center, true);
            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OrderQty", "PeriodDate");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OrderQty", "PeriodDate", "Cost"); // 20220426 오세완 차장 단가이력관리에서 따라서 단가는 출력되나 사용자가 수정은 할수 있게 해야 해서 추가 
            DetailGridExControl.BestFitColumns();

            SubDetailGridExControl.MainGrid.Init();
            SubDetailGridExControl.SetToolbarButtonVisible(false);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            SubDetailGridExControl.MainGrid.AddColumn("DelivSeq", LabelConvert.GetLabelText("납품계획번호"));
            SubDetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("품목코드"));
            SubDetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", LabelConvert.GetLabelText("품목"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", LabelConvert.GetLabelText("품번"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("DelivDate", LabelConvert.GetLabelText("DelivPlanDate"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("DelivQty", LabelConvert.GetLabelText("DelivPlanQty"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            SubDetailGridExControl.MainGrid.AddColumn("DelivId", LabelConvert.GetLabelText("DelivId"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("ProdYn", "생산의뢰", HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("OutConfirmflag", "출고의뢰", HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DelivDate", "DelivQty", "ProdYn", "OutConfirmflag", "Memo");
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OrderDate");       
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag != "N"), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OrderId", ModelService.GetChildList<UserView>(p => p.Active != "N"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("PeriodDate");
            
            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("DelivDate");
            SubDetailGridExControl.MainGrid.SetRepositoryItemTextEdit("DelivQty", 0, DefaultBoolean.Default, MaskType.Numeric, "n0", true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivId", ModelService.GetChildList<UserView>(p => p.Active != "N"), "LoginId", "UserName");
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(SubDetailGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProdYn", DbRequestHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutConfirmflag", DbRequestHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            DetailGridRowLocator.GetCurrentRow();
            SubDetailGridRowLocator.GetCurrentRow();

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();
            ModelService1.ReLoad();
            ModelService2.ReLoad();

            string CustCode = lupCustcode.EditValue.GetNullToEmpty();
            string ItemCode = tx_item.EditValue.GetNullToEmpty();
            string FrDate = datePeriodEditEx1.DateFrEdit.DateTime.ToShortDateString();
            string ToDate = datePeriodEditEx1.DateToEdit.DateTime.ToShortDateString();

            if (ItemCode == null || ItemCode == "")
                MasterGridBindingSource.DataSource = ModelService1.GetList(p => (p.OrderType == "양산") && (string.IsNullOrEmpty(CustCode) ? true : p.CustomerCode == CustCode)
                                                                             && (p.OrderDate >= datePeriodEditEx1.DateFrEdit.DateTime && p.OrderDate <= datePeriodEditEx1.DateToEdit.DateTime)
                                                                                ).GroupBy(g => g.OrderNo).Select(s => s.First()).ToList();
            else                 // 2022-03-30 김진우 추가       조회조건에 품목코드가 있는데 마스터까지 조회해줘야 해서 추가
                using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
                {
                    SqlParameter SqlItemCode = new SqlParameter("@ItemCode", ItemCode);
                    SqlParameter SqlCustomer = new SqlParameter("@Customer", CustCode);
                    SqlParameter SqlFrDate = new SqlParameter("@FrDate", FrDate);
                    SqlParameter SqlToDate = new SqlParameter("@ToDate", ToDate);

                    var SqlData = context.Database.SqlQuery<TN_ORD1000>("SP_ORD1100_MASTER @ItemCode, @Customer, @FrDate, @ToDate", SqlItemCode, SqlCustomer, SqlFrDate, SqlToDate).ToList();

                    MasterGridBindingSource.DataSource = SqlData;
                }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_ORD1000 MasterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (MasterObj == null) return;

            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            string ItemCode = tx_item.EditValue.GetNullToEmpty();
            DetailGridBindingSource.DataSource = ModelService2.GetList(p => p.OrderNo == MasterObj.OrderNo
                                                                       && (ItemCode == null ? true : (p.ItemCode.Contains(ItemCode) || p.TN_STD1100.ItemNm.Contains(ItemCode) || p.TN_STD1100.ItemNm1.Contains(ItemCode)))
                                                                       ).OrderBy(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

            #region 원본
            //VI_ORD1100 obj = MasterGridBindingSource.Current as VI_ORD1100;

            //DetailGridExControl.MainGrid.Clear();
            //SubDetailGridExControl.MainGrid.Clear();
            //if (obj == null) return;
            //DetailGridBindingSource.DataSource = ModelService2.GetList(p => p.OrderNo == obj.OrderNo).OrderBy(o => o.Seq).ToList();
            //DetailGridExControl.DataSource = DetailGridBindingSource;
            //DetailGridExControl.MainGrid.BestFitColumns();

            //SetRefreshMessage(DetailGridExControl.MainGrid.RecordCount);
            #endregion
        }

        protected override void DetailFocusedRowChanged()
        {
            TN_ORD1002 DetailObj = DetailGridBindingSource.Current as TN_ORD1002;
            if (DetailObj == null) return;

            SubDetailGridExControl.MainGrid.Clear();
            SubDetailGridBindingSource.DataSource = ModelService.GetList(p => p.OrderNo == DetailObj.OrderNo 
                                                                           && p.Seq == DetailObj.Seq)
                                                                           .OrderBy(o => o.DelivSeq).ToList();
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();
            SubDetailGridRowLocator.SetCurrentRow();
        }

        protected override void SubDetailAddRowClicked()
        {
            if (!UserRight.HasEdit) return;
            var Dobj = DetailGridBindingSource.Current as TN_ORD1002;
            if (Dobj == null) return;

            TN_ORD1100 obj = new TN_ORD1100()
            {
                DelivSeq = DbRequestHandler.GetRequestNumber("DLV"),
                OrderNo = Dobj.OrderNo,
                Seq = Dobj.Seq,
                ItemCode = Dobj.ItemCode,
                DelivDate = DateTime.Today,
                DelivQty = Dobj.OrderQty.GetIntNullToZero(),    // 2022-01-20 김진우 대리    0 에서 변경
                DelivId = Utils.Common.GlobalVariable.LoginId,
                ProdYn = "Y",                                   // 2022-01-28 김진우 대리 Y로 변경
                OutConfirmflag = "Y",                           // 2022-01-28 김진우 대리 Y로 변경
                Temp = Dobj.Temp
            };

            // 20220426 오세완 차장 추가시 품목코드만 보이고 나머지가 안보여서 추가 
            TN_STD1100 std1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == Dobj.ItemCode &&
                                                                            p.UseYn == "Y").FirstOrDefault();
            if (std1100 != null)
                obj.TN_STD1100 = std1100;
          
            SubDetailGridBindingSource.Add(obj);
            ModelService.Insert(obj);
            SubDetailGridExControl.MainGrid.BestFitColumns();

            #region Grid Focus를 위한 코드
            PopupDataParam.SetValue(PopupParameter.GridRowId_3, obj);
            #endregion
        }

        protected override void DeleteSubDetailRow()
        {
            if (!UserRight.HasEdit) return;

            var Dobj = DetailGridBindingSource.Current as TN_ORD1002;
            if (Dobj == null) return;

            var SubObj = SubDetailGridBindingSource.Current as TN_ORD1100;
            if (SubObj != null)
            {
                //int cnt = DbRequestHandler.GetCellValue("SELECT COUNT(*)  FROM[TN_MPS1300T] where DELIV_SEQ = '"+ SubObj.DelivSeq+"' and WORKORDER_YN = 'Y'", 0).GetIntNullToZero();
                //if (cnt >= 1) { MessageBox.Show("작업지시가 내려간 판매계획은 삭제할수 없습니다."); return; }

                //ModelService.Delete(SubObj);
                //SubDetailGridBindingSource.RemoveCurrent();           // 2022-03-30 김진우 주석
                //SubDetailGridBindingSource.Remove(SubObj);              // 2022-03-30 김진우 추가

                // 20220426 오세완 차장 날쿼리 방지 
                TN_MPS1300 mps_temp = ModelService.GetChildList<TN_MPS1300>(p => p.DelivSeq == SubObj.DelivSeq &&
                                                                                 p.WorkorderYn == "Y").FirstOrDefault();
                if(mps_temp == null)
                {
                    SubDetailGridBindingSource.Remove(SubObj);
                    ModelService.Delete(SubObj);
                }
                else
                {
                    MessageBoxHandler.Show("작업지시가 내려간 판매계획은 삭제할수 없습니다.");
                }
            }
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();
            ModelService1.Save();
            ModelService2.Save();
            ModelService.Save();
            DataLoad();
        }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService1);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);
            param.SetValue(PopupParameter.Value_1, "양산");

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000, param, PopupRefreshCallback);

            form.ShowPopup(true);
        }

        protected override void DeleteRow()
        {
            TN_ORD1000 obj = MasterGridBindingSource.Current as TN_ORD1000;
            if (obj != null)
            {
                if (ModelService2.GetList(p => p.OrderNo == obj.OrderNo).ToList().Count > 0)
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show(MessageHelper.GetStandardMessage(10), HKInc.Service.Factory.HelperFactory.GetLabelConvert().GetLabelText("Confirm"));
                    return;
                }
                //TN_ORD1000 nobj = ModelService1.GetList(p => p.OrderNo == obj.OrderNo).OrderBy(o => o.OrderNo).FirstOrDefault();
                ModelService1.Delete(obj);
                //MasterGridBindingSource.RemoveCurrent();                      // 2022-03-30 김진우 주석
                MasterGridBindingSource.Remove(obj);                            // 2022-03-30 김진우 추가
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_ORD1000 obj = MasterGridBindingSource.Current as TN_ORD1000;
            if (obj == null) return;

            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");
            param.SetValue(PopupParameter.KeyValue, obj.CustomerCode);
            param.SetValue(PopupParameter.Value_1, "");

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTSTD1100, param, AddOrderList);
            form.ShowPopup(true);
        }

        private void AddOrderList(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            TN_ORD1000 OrderMaster = (TN_ORD1000)MasterGridBindingSource.Current;
            List<TN_STD1100> partList = (List<TN_STD1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var returnedPart in partList)
            {
                if (ModelService2.GetList(p => p.OrderNo == OrderMaster.OrderNo && p.ItemCode == returnedPart.ItemCode).Count == 0)
                {
                    TN_ORD1002 obj = (TN_ORD1002)DetailGridBindingSource.AddNew();
                    TN_STD1100 item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == returnedPart.ItemCode).FirstOrDefault();
                    List<TN_ORD1002> oldobj = DetailGridBindingSource.DataSource as List<TN_ORD1002>;
                    int iseq = oldobj.OrderByDescending(o => o.Seq).FirstOrDefault().Seq + 1;
                    obj.OrderNo = OrderMaster.OrderNo;
                    obj.Seq = iseq;
                    obj.ItemCode = item.ItemCode;
                    obj.PeriodDate = OrderMaster.PeriodDate;
                    obj.Temp = OrderMaster.CustomerCode;

                    // 20220426 오세완 차장 단가이력에 입력된 단가 가져오기 
                    string sSql = "exec USP_GET_XFSTD1120_COST '" + obj.ItemCode + "', '" + OrderMaster.CustomerCode + "', '" + Convert.ToDateTime(OrderMaster.PeriodDate).ToString("yyyy-MM-dd") + "' ";
                    string sCost = DbRequestHandler.GetCellValue(sSql, 0);
                    if (!string.IsNullOrEmpty(sCost))
                    {
                        decimal dCost;
                        decimal.TryParse(sCost, out dCost);
                        obj.Cost = dCost;
                    }

                    ModelService2.Insert(obj);
                }
            }
            if (partList.Count > 0) SetIsFormControlChanged(true);
            DataSave();
        }

        protected override void DeleteDetailRow()
        {
            TN_ORD1002 obj = DetailGridBindingSource.Current as TN_ORD1002;

            //if (SubDetailGridExControl.MainGrid.MainView.RowCount <= 1)         // 2022-03-30 김진우 추가   subdetail에 데이터 존재하는데 삭제되어서 추가
            if (SubDetailGridExControl.MainGrid.MainView.RowCount >= 1)         // 2022-04-26 오세완 차장 조건이 반대라서 수정 
            {
                MessageBoxHandler.Show("판매계획이 작성된 수주상세내역은 제거할수 없습니다.");
                return;
            }
            if (obj != null)
            {
                //DetailGridBindingSource.RemoveCurrent();      // 2022-03-30 김진우 주석
                DetailGridBindingSource.Remove(obj);            // 2022-03-30 김진우 추가
                ModelService2.Delete(obj);
            }
        }

        private void SubDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TN_ORD1100 obj = SubDetailGridBindingSource.Current as TN_ORD1100;
            if (obj == null) return;

            if (e.Column.Name.ToString() == "ProdYn")
            {
                //int cnt = DbRequestHandler.GetCellValue("SELECT COUNT(*)  FROM[TN_MPS1300T] where DELIV_SEQ = '" + obj.DelivSeq + "' and WORKORDER_YN = 'Y'", 0).GetIntNullToZero();
                //if (cnt >= 1)
                //{
                //    MessageBox.Show("작업지시가 내려간 판매계획은 생산의뢰를 수정 할 수 없습니다.");
                //    obj.ProdYn = "Y";
                //    return;
                //}

                // 20220427 오세완 차장 날쿼리 방지 
                TN_MPS1300 mps_temp = ModelService.GetChildList<TN_MPS1300>(p => p.DelivSeq == obj.DelivSeq &&
                                                                                 p.WorkorderYn == "Y").FirstOrDefault();
                if(mps_temp != null)
                {
                    MessageBoxHandler.Show("작업지시가 내려간 판매계획은 생산의뢰를 수정 할 수 없습니다.");
                    obj.ProdYn = "Y";
                }
            }
        }

        private void SubDetail_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                object ProdYn = View.GetRowCellValue(e.RowHandle, View.Columns["ProdYn"]);
                object OutConfirmflag = View.GetRowCellValue(e.RowHandle, View.Columns["OutConfirmflag"]).GetNullToEmpty();

                if (e.Column.Name.ToString() == "ProdYn")
                {
                    if (ProdYn.ToString() == "Y")
                    {
                        e.Appearance.BackColor = Color.Blue;
                        e.Appearance.ForeColor = Color.White;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
                if (e.Column.Name.ToString() == "OutConfirmflag")
                {
                    if (OutConfirmflag.ToString() == "Y")
                    {
                        e.Appearance.BackColor = Color.Blue;
                        e.Appearance.ForeColor = Color.White;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }

        /// <summary>
        /// 20220426 오세완 차장
        /// 수주등록 수정기능 추가 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterGridView_RowClick(object sender, RowClickEventArgs e)
        {
            if(e.Clicks == 2)
            {
                TN_ORD1000 mst_Obj = MasterGridBindingSource.Current as TN_ORD1000;
                if (mst_Obj == null)
                    return;

                if (!UserRight.HasEdit) return;

                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Service, ModelService1);
                param.SetValue(PopupParameter.UserRight, UserRight);
                param.SetValue(PopupParameter.EditMode, PopupEditMode.Update);
                param.SetValue(PopupParameter.KeyValue, mst_Obj);

                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000, param, PopupRefreshCallback);

                form.ShowPopup(true);
            }
        }

    }
}
