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
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.PUR
{
    /// <summary>
    /// 자재재고관리
    /// </summary>
    public partial class XFPUR1500_JI : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PUR_INOUT_LIST> ModelService = (IService<VI_PUR_INOUT_LIST>)ProductionFactory.GetDomainService("VI_PUR_INOUT_LIST");


        //private string searchDivision;
        private string searchItemCode;

        public XFPUR1500_JI()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

        }

        

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_MAT || p.TopCategory == MasterCodeSTR.TopCategory_BU)).ToList());            

        }

        protected override void InitGrid()
        {
            
            MasterGridExControl.SetToolbarVisible(false);

            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            MasterGridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            MasterGridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            MasterGridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            MasterGridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));
            MasterGridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "#,##0.##");
            MasterGridExControl.MainGrid.AddColumn("SumInQty", LabelConvert.GetLabelText("SumInQty"), HorzAlignment.Far, FormatType.Numeric, "#,##0.##");
            MasterGridExControl.MainGrid.AddColumn("SumOutQty", LabelConvert.GetLabelText("SumOutQty"), HorzAlignment.Far, FormatType.Numeric, "#,##0.##");
            MasterGridExControl.MainGrid.AddColumn("SumStockQty", LabelConvert.GetLabelText("SumStockQty"), HorzAlignment.Far, FormatType.Numeric, "#,##0.##");


            DetailGridExControl.SetToolbarVisible(false);
            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, false);
            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, false);
            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, false);
            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            DetailGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("InOutDate", LabelConvert.GetLabelText("InOutDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");

            DetailGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,##0.##");
            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,##0.##");
            DetailGridExControl.MainGrid.AddColumn("InOutId", LabelConvert.GetLabelText("InOutId"));


        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InOutId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();
            InitRepository();

            searchItemCode = lup_Item.EditValue.GetNullToEmpty();
            //searchDivision = cbo_YearMonth.EditValue.GetNullToEmpty();
                     
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;

                var ItemCode = new SqlParameter("@ItemCode", searchItemCode);

                MasterGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_XFPUR1500_JI_MASTER>("USP_GET_XFPUR1500_JI_MASTER @ItemCode",  ItemCode).ToList();
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();

            var masterObj = MasterGridBindingSource.Current as TEMP_XFPUR1500_JI_MASTER;

            if (masterObj == null) return;

            var ItemCode = masterObj.ItemCode;

            DetailGridBindingSource.DataSource = ModelService.GetList(p => p.ItemCode == ItemCode).ToList();

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

    }
}