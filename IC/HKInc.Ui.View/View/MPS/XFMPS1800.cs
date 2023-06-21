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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using HKInc.Service.Handler;
using System.Collections.Generic;
using HKInc.Utils.Enum;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 완제품입고관리
    /// </summary>
    public partial class XFMPS1800 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1300> ModelService = (IService<TN_MPS1300>)ProductionFactory.GetDomainService("TN_MPS1300");

        public XFMPS1800()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            dt_WorkDate.SetTodayIsWeek();
        }

        protected override void InitCombo()
        {
            lup_ItemCode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            GridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("WhPositionChange") + "[F3]", IconImageList.GetIconImage("business%20objects/bosaleitem"));
            GridExControl.SetToolbarButtonCaption(GridToolbarButton.DeleteRow, LabelConvert.GetLabelText("WhPositionStockChange") + "[F7]", IconImageList.GetIconImage("business%20objects/bosaleitem"));
            GridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            GridExControl.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            GridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            //GridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            GridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            GridExControl.MainGrid.AddColumn("WhCode", LabelConvert.GetLabelText("WhName"));
            GridExControl.MainGrid.AddColumn("PositionCode", LabelConvert.GetLabelText("PositionName"));
            GridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");

            var barBarcodePrint = new DevExpress.XtraBars.BarButtonItem();
            barBarcodePrint.Id = 4;
            barBarcodePrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/bosaleitem");
            barBarcodePrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T));
            barBarcodePrint.Name = "barBarcodePrint";
            barBarcodePrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barBarcodePrint.ShortcutKeyDisplayString = "Alt+P";
            barBarcodePrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barBarcodePrint.Caption = LabelConvert.GetLabelText("BarcodePrint") + "[Alt+P]";
            barBarcodePrint.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            barBarcodePrint.ItemClick += BarBarcodePrint_ItemClick;
            GridExControl.BarTools.AddItem(barBarcodePrint);
            //GridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"));
            //GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WhCode", "PositionCode", "BoxInQty", "BoxQty");
        }

        private void BarBarcodePrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var obj = GridBindingSource.Current as VI_MPS1800_LIST;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.Value_1, "3");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFPACK_BARCODE_PRINT, param, BarcodePrintCallback);
            form.ShowPopup(true);
        }

        /// <summary>라벨출력 CallBack</summary>
        private void BarcodePrintCallback(object sender, PopupArgument e) { }

        protected override void InitRepository()
        {
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), false);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PositionCode", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", false);
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();
            // 20210624 오세완 차장 김이사님 지시로 공통코드 추가시 화면 재출력 없이 하기 위해서 추가
            InitRepository();
            InitCombo();

            var productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();
            var itemCode = lup_ItemCode.EditValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetChildList<VI_MPS1800_LIST>(p => (p.WorkDate >= dt_WorkDate.DateFrEdit.DateTime && p.WorkDate <= dt_WorkDate.DateToEdit.DateTime)
                                                                                         && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                                         && (p.ProductLotNo.Contains(productLotNo))
                                                                                      ).ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
        }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            var obj = GridBindingSource.Current as VI_MPS1800_LIST;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.KeyValue, obj);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFMPS1800, param, PopupRefreshCallback);

            form.ShowPopup(true);
        }

        protected override void DeleteRow()
        {
            if (!UserRight.HasEdit) return;

            var obj = GridBindingSource.Current as VI_MPS1800_LIST;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.KeyValue, obj);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFMPS1801, param, PopupRefreshCallback);

            form.ShowPopup(true);
        }

        protected override void GridRowDoubleClicked(){}
    }
}
