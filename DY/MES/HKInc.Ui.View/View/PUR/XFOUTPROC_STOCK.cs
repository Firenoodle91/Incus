using System.Data;
using System.Linq;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Context;
using HKInc.Utils.Common;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Service;

namespace HKInc.Ui.View.View.PUR
{
    /// <summary>
    /// 외주재고관리화면
    /// </summary>
    public partial class XFOUTPROC_STOCK : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_OUT_PROC_STOCK_MASTER> ModelService = (IService<VI_OUT_PROC_STOCK_MASTER>)ProductionFactory.GetDomainService("VI_OUT_PROC_STOCK_MASTER");
        public XFOUTPROC_STOCK()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitCombo()
        {
            datePeriodEditEx1.SetTodayIsMonth();

            lupcustcode.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"));
            MasterGridExControl.MainGrid.AddColumn("PoDate", LabelConvert.GetLabelText("PoDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("PoId", LabelConvert.GetLabelText("PoId"));
            MasterGridExControl.MainGrid.AddColumn("PoCustCode", LabelConvert.GetLabelText("PoCustomer"));
            MasterGridExControl.MainGrid.AddColumn("DueDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"));
            MasterGridExControl.MainGrid.AddColumn("InDate", LabelConvert.GetLabelText("InDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("InId", LabelConvert.GetLabelText("InId"));
            MasterGridExControl.MainGrid.AddColumn("InCustCode", LabelConvert.GetLabelText("InCustomer"));

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"), false);            
            DetailGridExControl.MainGrid.AddColumn("PoSeq", LabelConvert.GetLabelText("PoSeq"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0", false);
            DetailGridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"), false);
            DetailGridExControl.MainGrid.AddColumn("InSeq", LabelConvert.GetLabelText("InSeq"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0", false);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1")); // 20210824 오세완 차장 품목명 누락 추가
            DetailGridExControl.MainGrid.AddColumn("CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            DetailGridExControl.MainGrid.AddColumn("ItemMoveNo", LabelConvert.GetLabelText("ItemMoveNo"));
            //DetailGridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            DetailGridExControl.MainGrid.AddColumn("PoQty", LabelConvert.GetLabelText("PoQty"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("RemainQty", LabelConvert.GetLabelText("PoRemainQty"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("BadQty", LabelConvert.GetLabelText("BadQty"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("BadType", LabelConvert.GetLabelText("BadType"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
        }
        protected override void InitRepository()
        {
            var UserList = ModelService.GetChildList<User>(p => p.Active != "N").ToList();
            var CustList = ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList();
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoId", UserList, "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", UserList, "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoCustCode", CustList, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InCustCode", CustList, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BadType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }
        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string cust = lupcustcode.EditValue.GetNullToEmpty();
            string poNo = tx_PoNo.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.PoDate >= datePeriodEditEx1.DateFrEdit.DateTime &&
                                                                            p.PoDate <= datePeriodEditEx1.DateToEdit.DateTime)
                                                                            && (string.IsNullOrEmpty(cust) ? true:p.InCustCode == cust)
                                                                            && (string.IsNullOrEmpty(poNo) ? true : (p.PoNo.Contains(poNo)))
                                                                     )
                                                                     .OrderBy(o => o.InNo)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl);

        }
        protected override void MasterFocusedRowChanged()
        {
            var MasterObj = MasterGridBindingSource.Current as VI_OUT_PROC_STOCK_MASTER;
            if (MasterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            using (var context = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var PoNoParam = new SqlParameter("@PoNo", MasterObj.PoNo);
                var result = context.Database
                      .SqlQuery<TEMP_XFOUTPROC_STOCK_DETAIL>("USP_GET_OUT_PROC_STOCK_DETAIL @PoNo", PoNoParam).ToList();
                DetailGridBindingSource.DataSource = result.ToList();
            }
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }
    }
}
