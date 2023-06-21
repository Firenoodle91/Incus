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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Helper;
using DevExpress.XtraGrid.Views.BandedGrid;
using HKInc.Service.Handler;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Controls;
using HKInc.Service.Service;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 작업지시참조
    /// </summary>
    public partial class XSFWORK_REF : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        private BindingSource DetailBindingSource = new BindingSource();
        private TN_PUR1100 TN_PUR1100_Obj;

        public XSFWORK_REF()
        {
            InitializeComponent();
        }
        
        public XSFWORK_REF(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkRef");
            this.Icon = Icon.FromHandle(((Bitmap)IconImageList.GetIconImage("business%20objects/botask")).GetHicon());

            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;

            TN_PUR1100_Obj = this.PopupParam.GetValue(PopupParameter.KeyValue) as TN_PUR1100;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitBindingSource(){}

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_MAT)).ToList());

            dt_YearMonth.SetFormat(DateFormat.Month);
            dt_YearMonth.DateTime = TN_PUR1100_Obj.PoMonth; //DateTime.Today;

            dt_YearMonth.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            dt_YearMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("SrcItemCode"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("SrcItemName"));
            GridExControl.MainGrid.AddColumn("Spec", LabelConvert.GetLabelText("Spec"), false);
            GridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));
            GridExControl.MainGrid.AddColumn("WorkRequireQty", LabelConvert.GetLabelText("WorkRequireQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("RequireQty", LabelConvert.GetLabelText("RequireQty2"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("PoQty", LabelConvert.GetLabelText("PoQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("NotInQty", LabelConvert.GetLabelText("NotInQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("PredictionQty", LabelConvert.GetLabelText("PredictionQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##" , false);
            GridExControl.MainGrid.AddColumn("SrcStockQty", LabelConvert.GetLabelText("SrcStockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("ThirdSpendQty", LabelConvert.GetLabelText("ThirdSpendQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            gridEx2.SetToolbarVisible(false);
            gridEx2.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            gridEx2.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            gridEx2.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            gridEx2.MainGrid.AddColumn("ProcTeamCode", LabelConvert.GetLabelText("ProductTeam"));
            gridEx2.MainGrid.AddColumn("PlanStartDate", LabelConvert.GetLabelText("StartDueDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            gridEx2.MainGrid.AddColumn("PlanEndDate", LabelConvert.GetLabelText("FinishDueDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            gridEx2.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            gridEx2.MainGrid.AddColumn("ResultQty", LabelConvert.GetLabelText("ResultQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            gridEx2.MainGrid.AddColumn("WorkRequireQty", LabelConvert.GetLabelText("WorkRequireQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            gridEx2.MainGrid.AddColumn("RequireQty", LabelConvert.GetLabelText("RequireQty2"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            gridEx2.MainGrid.AddColumn("InputWeight", LabelConvert.GetLabelText("InputWeight"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit));
            gridEx2.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcTeamCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode));
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            gridEx2.MainGrid.Clear();
            
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var SearchDate = new SqlParameter("@SearchDate", dt_YearMonth.DateTime);
                var ItemCode = new SqlParameter("@SrcItemCode", lup_Item.EditValue.GetNullToEmpty());
                ModelBindingSource.DataSource = context.Database.SqlQuery<TEMP_WORK_REF_MASTER>("USP_GET_WORK_REF_MASTER @SearchDate,@SrcItemCode", SearchDate, ItemCode).ToList();
            }
            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var masterObj = ModelBindingSource.Current as TEMP_WORK_REF_MASTER;
            if (masterObj == null)
            {
                gridEx2.MainGrid.Clear();
                return;
            }

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var SearchDate = new SqlParameter("@SearchDate", dt_YearMonth.DateTime);
                var ItemCode = new SqlParameter("@SrcItemCode", masterObj.ItemCode.GetNullToEmpty());
                DetailBindingSource.DataSource = context.Database.SqlQuery<TEMP_WORK_REF_DETAIL>("USP_GET_WORK_REF_DETAIL @SearchDate,@SrcItemCode", SearchDate, ItemCode).ToList();
            }

            gridEx2.DataSource = DetailBindingSource;
            gridEx2.BestFitColumns();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var obj = (TEMP_WORK_REF_MASTER)ModelBindingSource.Current;
                param.SetValue(PopupParameter.ReturnObject, obj);
                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }
        }
    }
}