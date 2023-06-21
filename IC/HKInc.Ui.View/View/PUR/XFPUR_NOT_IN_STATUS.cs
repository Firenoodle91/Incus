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
    /// 구매미입고현황
    /// </summary>
    public partial class XFPUR_NOT_IN_STATUS : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        private BindingSource BindingSource = new BindingSource();

        public XFPUR_NOT_IN_STATUS()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            dt_SearchDate.DateFrEdit.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dt_SearchDate.DateToEdit.DateTime = DateTime.Today;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetList(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_MAT)).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("PoQty", LabelConvert.GetLabelText("PoQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
            GridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
            //GridExControl.MainGrid.AddColumn("OKQty", LabelConvert.GetLabelText("PassQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
            GridExControl.MainGrid.AddColumn("NotInQty", LabelConvert.GetLabelText("NotInQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();
            InitRepository();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var SearchDateFr = new SqlParameter("@SearchDateFr", dt_SearchDate.DateFrEdit.DateTime);
                var SearchDateTo = new SqlParameter("@SearchDateTo", dt_SearchDate.DateToEdit.DateTime);
                var ItemCode = new SqlParameter("@SrcItemCode", lup_Item.EditValue.GetNullToEmpty());
                BindingSource.DataSource = context.Database.SqlQuery<TEMP_XFPUR_NOT_IN_STATUS>("USP_GET_PUR_NOT_IN_STATUS @SearchDateFr,@SearchDateTo,@SrcItemCode", SearchDateFr, SearchDateTo, ItemCode).ToList();
            }

            GridExControl.DataSource = BindingSource;
            GridExControl.BestFitColumns();
        }
    }
}
