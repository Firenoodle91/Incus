using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Entity;

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using DevExpress.XtraBars;

using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Enum;


namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 원본 : XFORD1300 제품기타입고관리화면
    /// 매출현황 등록관리
    /// </summary>
    public partial class XRREP5004 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        //IService<TN_ORD1600> MasterModel = (IService<TN_ORD1600>)ProductionFactory.GetDomainService("TN_ORD1600");
        IService<TN_ORD1601> DetailModel = (IService<TN_ORD1601>)ProductionFactory.GetDomainService("TN_ORD1601");

        IService<TN_STD1100> ItemModel = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        IService<TN_STD1400> CustModel = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");

        public XRREP5004()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailView_CellValueChanged;

            //DetailGridExControl.MainGrid.MainView.RowStyle += DetailMainView_RowStyle;

            dt_OrderDate.SetTodayIsMonth();
        }        

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            MasterGridExControl.MainGrid.AddColumn("DelivNo", "납품계획번호");
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCode"));
            MasterGridExControl.MainGrid.AddColumn("CustomerName", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("CarType", LabelConvert.GetLabelText("CarType"));
            
            MasterGridExControl.MainGrid.AddColumn("OrderItemCost", "수주단가", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DelivDate", "납품예정일");
            MasterGridExControl.MainGrid.AddColumn("DelivQty", "납품예정계획수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DueQty", "실 납품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DueMQty", "미납수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DuePlanCost", "납품예정계획금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DueCost", "실 납품금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DueMCost", "미 달성금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DueRate", "달성율", HorzAlignment.Far, FormatType.Numeric, "n2");

            //MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OrderItemCost");
            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1300>(MasterGridExControl);

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.MainGrid.AddColumn("RowId", "RowId", false);
            DetailGridExControl.MainGrid.AddColumn("OrderNo", "OrderNo", false);
            DetailGridExControl.MainGrid.AddColumn("DelivNo", "DelivNo", false);
            DetailGridExControl.MainGrid.AddColumn("OutSeq", "출고순번", HorzAlignment.Center, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutDate", "출고일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "납품수량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0}");
            DetailGridExControl.MainGrid.AddColumn("ItemCost", "제품단가", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("DueCost", "납품금액", HorzAlignment.Far, FormatType.Numeric, "n0");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutDate", "OutQty");
            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1301>(DetailGridExControl);

        }

        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("ItemCost");
            MasterGridExControl.BestFitColumns();

            //DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            //DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("ItemCost");


            DetailGridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;
            DetailGridExControl.MainGrid.MainView.Columns["OutQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            DetailGridExControl.MainGrid.MainView.Columns["OutQty"].SummaryItem.DisplayFormat = "{0:#,#}";
            DetailGridExControl.MainGrid.MainView.Columns["DueCost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            DetailGridExControl.MainGrid.MainView.Columns["DueCost"].SummaryItem.DisplayFormat = "{0:#,#}";

            //var WhPositionEdit = DetailGridExControl.MainGrid.Columns["InWhPosition"].ColumnEdit as RepositoryItemSearchLookUpEdit;
        }

        protected override void InitCombo()
        {
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", CustModel.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_ItemCode.SetDefault(true, "ItemCode", "ItemName", ItemModel.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("DelivNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            InitCombo();
            InitRepository();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                //var MonthDate = new SqlParameter("@MonthDate", dt_YearMonth.DateTime);
                //var MachineCode = new SqlParameter("@MachineCode", masterObj.MachineCode);

                var startdate = new SqlParameter("@StartDate", dt_OrderDate.DateFrEdit.DateTime);
                var enddate = new SqlParameter("@EndDate", dt_OrderDate.DateToEdit.DateTime);

                var customercode = new SqlParameter("@CustomerCode", lup_CustomerCode.EditValue.GetNullToEmpty());
                var itemcode = new SqlParameter("@ItemCode", lup_ItemCode.EditValue.GetNullToEmpty());

                var result = context.Database.SqlQuery<TEMP_XRREP5004_MASTER>
                    ("USP_GET_XRREP5004_MASTER @StartDate, @EndDate, @CustomerCode, @ItemCode", startdate, enddate, customercode, itemcode).ToList();

                MasterGridBindingSource.DataSource = result.ToList();
                MasterGridExControl.DataSource = MasterGridBindingSource;
                MasterGridExControl.BestFitColumns();

            }

            GridRowLocator.SetCurrentRow();
            
            //SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {

            try
            {
                DetailGridExControl.MainGrid.Clear();

                List<TEMP_XRREP5004_MASTER> List = MasterGridBindingSource.DataSource as List<TEMP_XRREP5004_MASTER>;

                ////이거 왜 안되는지 모르겠음
                //var obj = MasterGridBindingSource.Current as TEMP_XRREP5004_MASTER;
                //if (obj == null) return;

                int currentRow = 0;
                currentRow = gridEx1.MainGrid.MainView.FocusedRowHandle;
                var obj2 = List[currentRow];


                string orderno = obj2.OrderNo;
                string delivno = obj2.DelivNo;

                DetailModel.ReLoad();

                //(p.InDate >= dt_InDate.DateFrEdit.DateTime && p.InDate <= dt_InDate.DateToEdit.DateTime)
                DetailGridBindingSource.DataSource = DetailModel.GetList(p => p.OrderNo == orderno 
                                                                           && p.DelivNo == delivno)
                                                                           .OrderBy(o => o.OutSeq)
                                                                           .ToList();
                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.BestFitColumns();
            }
            catch { }
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();

            List<TN_ORD1601> detailList = DetailGridBindingSource.DataSource as List<TN_ORD1601>;

            foreach(var v in detailList)
            {
                //추가
                if (v.NewRowFlag == "Y")
                {
                    DetailModel.Insert(v);
                }
                //수정
                else if (v.NewRowFlag == "N" && v.EditRowFlag == "Y")
                {
                    DetailModel.Update(v);
                }
            }
            DetailModel.Save();

            DataLoad();

            GridRowLocator.SetCurrentRow();
        }

        protected override void DetailAddRowClicked()
        {
            List<TEMP_XRREP5004_MASTER> List = MasterGridBindingSource.DataSource as List<TEMP_XRREP5004_MASTER>;

            int currentRow = 0;
            currentRow = gridEx1.MainGrid.MainView.FocusedRowHandle;
            var obj2 = List[currentRow];
            if (obj2 == null) return;

            List<TN_ORD1601> detailList = DetailGridBindingSource.DataSource as List<TN_ORD1601>;

            TN_ORD1601 obj = new TN_ORD1601
            {
                OrderNo = obj2.OrderNo,
                DelivNo = obj2.DelivNo,
                OutSeq = detailList.Count == 0 ? 1 : detailList.Max(p => p.OutSeq) + 1,
                OutDate = DateTime.Today,
                ItemCost = DbRequestHandler.GetCustItemCost(obj2.CustomerCode, obj2.ItemCode, DateTime.Now.ToString("yyyy-MM-dd"), "")
            };
            DetailGridBindingSource.Add(obj);
        }
        private void  DetailMainView_RowStyle(object sender, RowStyleEventArgs e)
        {/*
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                var ttt = View.GetRowCellValue(e.RowHandle, View.Columns["ItemCost"]);

                int ItemCost = View.GetRowCellValue(e.RowHandle, View.Columns["ItemCost"]).GetIntNullToZero();
                int OutQty = View.GetRowCellValue(e.RowHandle, View.Columns["OutQty"]).GetIntNullToZero();

                int DueCost = ItemCost * OutQty;

                View.SetRowCellValue(e.RowHandle, View.Columns["DueCost"], "111");
            }
            */
        }
        protected override void DeleteDetailRow()
        {
            TEMP_XRREP5004_MASTER obj = MasterGridBindingSource.Current as TEMP_XRREP5004_MASTER;
            if (obj == null) return;

            TN_ORD1601 delobj = DetailGridBindingSource.Current as TN_ORD1601;
            if (delobj == null) return;


            DetailGridBindingSource.Remove(delobj);
            //DetailGridBindingSource.RemoveCurrent();
            DetailModel.Delete(delobj);
            DetailGridExControl.MainGrid.BestFitColumns();

        }

        private void DetailView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            List<TEMP_XRREP5004_MASTER> List = MasterGridBindingSource.DataSource as List<TEMP_XRREP5004_MASTER>;
            int currentRow = 0;
            currentRow = gridEx1.MainGrid.MainView.FocusedRowHandle;
            var obj2 = List[currentRow];
            if (obj2 == null) return;

            List<TN_ORD1601> DetailList = DetailGridBindingSource.DataSource as List<TN_ORD1601>;
            string fieldName = e.Column.FieldName;

            if (fieldName != "OutDate") return;

            dynamic checkDate = null;
            dynamic rowid = null;

            
            checkDate = gv.GetRowCellValue(e.RowHandle, gv.Columns[fieldName]);
            rowid = gv.GetRowCellValue(e.RowHandle, gv.Columns["RowId"]); //수정 대상은 비교 제외

            //출고일자 중복등록 확인
            //최초 출고 등록은 제외
            if (DetailList.Count > 1)
            {
                foreach (var v in DetailList)
                {
                    if (v.OutDate == checkDate && v.NewRowFlag != "Y" && rowid != v.RowId)
                    {
                        HKInc.Service.Handler.MessageBoxHandler.Show("중복되는 기간 등록 불가");

                        DetailGridExControl.MainGrid.MainView.CellValueChanged -= DetailView_CellValueChanged;
                        gv.SetFocusedRowCellValue(e.Column.FieldName, v.OutDate.AddDays(1));
                        DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailView_CellValueChanged;
                        gv.SelectRow(e.RowHandle);
                        return;
                    }
                }
            }

            //업체별 제품 단가 
            var date = gv.GetRowCellValue(e.RowHandle, gv.Columns["OutDate"]).GetNullToEmpty();
            var cost = DbRequestHandler.GetCustItemCost(obj2.CustomerCode, obj2.ItemCode, date, "");
                
            gv.SetRowCellValue(e.RowHandle, gv.Columns["ItemCost"], cost);

            DetailGridExControl.MainGrid.BestFitColumns();

        }
    }
}
