﻿using System;
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
    public partial class XFPUR_STOCK : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PUR_STOCK_ITEM> ModelService = (IService<VI_PUR_STOCK_ITEM>)ProductionFactory.GetDomainService("VI_PUR_STOCK_ITEM");
        private string searchDivision;
        private DateTime? searchYearMonthDate;
        private string searchItemCode;
        public XFPUR_STOCK()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dateEditEx1.SetFormat(Utils.Enum.DateFormat.Month);
            dateEditEx1.DateTime = DateTime.Today;
            btn_PurchaseStatus.Click += Btn_PurchaseStatus_Click;
          //  cbo_YearMonth.Visible = false;
            DetailGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_MAT || p.TopCategory == MasterCodeSTR.TopCategory_BU || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());

            btn_PurchaseStatus.Text = LabelConvert.GetLabelText("PurchaseStatus") + "(&F)";

            //cbo_YearMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            //cbo_YearMonth.Properties.Items.Add("당월");

            //var deadLineDateList = ModelService.GetChildList<TN_MAT_DEAD_MST>(p => p.Division == MasterCodeSTR.DeadLineDivision_DeadConfirm).Select(p => p.DeadLineDate).ToList();
            //foreach (var v in deadLineDateList)
            //{
            //    cbo_YearMonth.Properties.Items.Add(v.ToString("yyyy-MM"));
            //}

            //cbo_YearMonth.EditValue = "당월";
            //searchDivision = cbo_YearMonth.EditValue.GetNullToEmpty();
            
            MasterGridExControl.SetToolbarVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(false);
            IsDetailGridButtonFileChooseEnabled = UserRight.HasEdit;
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, UserRight.HasEdit);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("ReturnAdjust") + "[F10]", IconImageList.GetIconImage("spreadsheet/showdetail"));
            DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));

            //if (searchDivision == "당월")
            //{
            //    MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            //    MasterGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            //    MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            //    MasterGridExControl.MainGrid.AddColumn("TN_STD1100.SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            //}
            //else
            //{
                MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
                MasterGridExControl.MainGrid.AddColumn("Spec", LabelConvert.GetLabelText("Spec"));
                MasterGridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));
                MasterGridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            //}
                
            MasterGridExControl.MainGrid.AddColumn("SumInQty", LabelConvert.GetLabelText("SumInQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumOutQty", LabelConvert.GetLabelText("SumOutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
          //  MasterGridExControl.MainGrid.AddColumn("SumCarryOverQty", LabelConvert.GetLabelText("SumCarryOverQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            //if (searchDivision != "당월")
            //    MasterGridExControl.MainGrid.AddColumn("SumAdjustQty", LabelConvert.GetLabelText("SumAdjustQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumStockQty", LabelConvert.GetLabelText("SumStockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            DetailGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("InOutDate", LabelConvert.GetLabelText("InOutDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("InOutLotNo", LabelConvert.GetLabelText("InOutLotNo"));
            DetailGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("InOutId", LabelConvert.GetLabelText("InOutId"));
            DetailGridExControl.MainGrid.AddColumn("InWhCode", LabelConvert.GetLabelText("InWhCode"));
            DetailGridExControl.MainGrid.AddColumn("InWhPosition", LabelConvert.GetLabelText("InWhPosition"));
            DetailGridExControl.MainGrid.AddColumn("InInspectionState", "수입검사", HorzAlignment.Center, true);
        }

        protected override void InitRepository()
        {
            //if (searchDivision == "당월")
            //{
            //    MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //}
            //else
            //{
                MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //}

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InOutId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            searchItemCode = lup_Item.EditValue.GetNullToEmpty();
            searchDivision = cbo_YearMonth.EditValue.GetNullToEmpty();

            MasterGridExControl.MainGrid.Columns.Clear();
            DetailGridExControl.MainGrid.Columns.Clear();
            InitGrid();
            InitRepository();

            //if (searchDivision == "당월")
            //{
            //    MasterGridBindingSource.DataSource = ModelService.GetList(p => string.IsNullOrEmpty(searchItemCode) ? true : p.ItemCode == searchItemCode
            //                                                             )
            //                                                             .OrderBy(p => p.ItemCode)
            //                                                             .ToList();
            //}
            //else
            //{
         //   searchYearMonthDate = dt_month.EditValue. DateTime.Today;// new DateTime(searchDivision.Left(4).GetIntNullToZero(), searchDivision.Substring(5).GetIntNullToZero(), 1);
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                // var Date = new SqlParameter("@Date", searchYearMonthDate);
                var ItemCode = new SqlParameter("@ItemCode", searchItemCode);
                //    MasterGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PUR_STOCK_ITEM>("USP_GET_PUR_STOCK_MASTER @Date,@ItemCode", Date, ItemCode).OrderBy(p => p.ItemCode).ToList();
                MasterGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PUR_STOCK_ITEM>("USP_GET_PUR_STOCK_MASTER @ItemCode",  ItemCode).OrderBy(p => p.ItemCode).ToList();
            }
            //}

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            //if (searchDivision == "당월")
            //{
                var masterObj = MasterGridBindingSource.Current as TEMP_PUR_STOCK_ITEM;
                if (masterObj == null)
                {
                    DetailGridExControl.MainGrid.Clear();
                    return;
                }
                DateTime ndate = dateEditEx1.DateTime.Date;
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var Date = new SqlParameter("@Date", ndate);
                    var ItemCode = new SqlParameter("@ItemCode", masterObj.ItemCode);
                    DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PUR_STOCK_DETAIL>("USP_GET_PUR_STOCK_DETAIL @Date,@ItemCode", Date, ItemCode)
                        .OrderBy(p => p.InOutLotNo)
                        .ThenBy(p=>p.UpdateTime)
                        .ToList();
                }

                DetailRowChange();
            //}
            //else
            //{
            //    var masterObj = MasterGridBindingSource.Current as TEMP_PUR_STOCK_ITEM;
            //    if (masterObj == null)
            //    {
            //        DetailGridExControl.MainGrid.Clear();
            //        return;
            //    }

            //    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            //    {
            //        var Date = new SqlParameter("@Date", searchYearMonthDate);
            //        var ItemCode = new SqlParameter("@ItemCode", masterObj.ItemCode);
            //        DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PUR_STOCK_DETAIL>("USP_GET_PUR_STOCK_DETAIL @Date,@ItemCode", Date, ItemCode)
            //            .OrderBy(p => p.InOutLotNo)
            //            .ThenBy(p => p.UpdateTime)
            //            .ToList();
            //    }
            //}
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        private void MainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DetailRowChange();
        }

        private void DetailRowChange()
        {
            var detailObj = DetailGridBindingSource.Current as TEMP_PUR_STOCK_DETAIL;
            if (detailObj == null)
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
            else
            {
                //if (searchDivision == "당월")
                //{
                    if(detailObj.Division == "재입고")
                        DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                    else
                        DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
                //}
                //else
                //{
                //    DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
                //}
            }
        }

        /// <summary> 구매현황 </summary>
        private void Btn_PurchaseStatus_Click(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PUR_STATUS, param, null);
            form.ShowPopup(false);
        }

        /// <summary> 재입고 수정 </summary>
        protected override void DetailFileChooseClicked()
        {
            var detailObj = DetailGridBindingSource.Current as TEMP_PUR_STOCK_DETAIL;
            if (detailObj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.KeyValue, detailObj.InOutLotNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFPUR1302, param, PopupRefreshCallback);
            form.ShowPopup(true);
        }
    }
}