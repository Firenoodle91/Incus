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
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 월별매출관리
    /// </summary>
    public partial class XRREP5001 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XRREP5001()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            //lup_Manager.EditValueChanged += Lup_Manager_EditValueChanged;
            //lup_CustomerCode.Popup += Lup_CustomerCode_Popup;

            OutDate.DateFrEdit.DateTime = DateTime.Today.AddMonths(-1);
            OutDate.DateToEdit.DateTime = DateTime.Today;

        }

        protected override void InitCombo()
        {
            lup_ItemCode.SetDefault(true, "ItemCode", "ItemName", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("OutDate", LabelConvert.GetLabelText("OutDate"));
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCode"));
            GridExControl.MainGrid.AddColumn("CustomerName", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));       
            GridExControl.MainGrid.AddColumn("Cost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");
            GridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "{0:#,##0}");
            GridExControl.MainGrid.AddColumn("Sales", LabelConvert.GetLabelText("Sales"), HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.#####}");

            GridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;

            GridExControl.MainGrid.MainView.Columns["OutQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["OutQty"].SummaryItem.DisplayFormat = "{0:#,##0}";

            GridExControl.MainGrid.MainView.Columns["Sales"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["Sales"].SummaryItem.DisplayFormat = "{0:#,##0.#####}";
        }

        protected override void InitRepository()
        {


        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var StartDate = new SqlParameter("@StartDate", OutDate.DateFrEdit.DateTime.ToString("yyyy-MM-dd"));
                var EndDate = new SqlParameter("@EndDate", OutDate.DateToEdit.DateTime.ToString("yyyy-MM-dd"));
                var ItemCode = new SqlParameter("@ItemCode", lup_ItemCode.EditValue.GetNullToEmpty());
                var CustomerCode = new SqlParameter("@CustomerCode", lup_CustomerCode.EditValue.GetNullToEmpty());

                var result = context.Database
                      .SqlQuery<TEMP_XRREP5001>("USP_GET_XRREP5001 @StartDate, @EndDate, @ItemCode, @CustomerCode", StartDate, EndDate, ItemCode, CustomerCode).ToList();

                GridBindingSource.DataSource = result;
            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            SetRefreshMessage(GridExControl);
        }

        private void Lup_Manager_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            lup_CustomerCode.EditValue = null;
        }

        private void Lup_CustomerCode_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var manager = lup_ItemCode.EditValue.GetNullToNull();

            if (manager.IsNullOrEmpty())
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            else
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[BusinessManagementId] = '" + manager + "'";
        }
    }
}