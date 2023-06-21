using System;
using System.Data;
using System.Linq;
using DevExpress.Utils;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Class;
using HKInc.Service.Helper;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using System.Windows.Forms;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.PUR
{
    /// <summary>
    /// 기간별입고현황
    /// </summary>
    public partial class XFPUR_IN_STATUS : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        private BindingSource BindingSource = new BindingSource();

        public XFPUR_IN_STATUS()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            dt_SearchDate.DateFrEdit.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dt_SearchDate.DateToEdit.DateTime = DateTime.Today;
        }

        protected override void InitCombo()
        {            
            lup_Customer.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Purchase || p.CustomerType == null)).ToList());
            lup_Item.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetList(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_MAT)).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCode"),false);
            GridExControl.MainGrid.AddColumn("CustomerName", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();
            InitRepository();

            string itemCode = lup_Item.EditValue.GetNullToEmpty();
            string customerCode = lup_Customer.EditValue.GetNullToEmpty();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var SearchDateFr = new SqlParameter("@SearchDateFr", dt_SearchDate.DateFrEdit.DateTime);
                var SearchDateTo = new SqlParameter("@SearchDateTo", dt_SearchDate.DateToEdit.DateTime);

                BindingSource.DataSource = context.Database.SqlQuery<TEMP_XFPUR_IN_STATUS>("USP_GET_PUR_IN_STATUS @SearchDateFr,@SearchDateTo", SearchDateFr, SearchDateTo)
                                          .Where(p => (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode) 
                                                    && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)).ToList();
            }

            GridExControl.DataSource = BindingSource;
            GridExControl.BestFitColumns();
        }

    }
}
