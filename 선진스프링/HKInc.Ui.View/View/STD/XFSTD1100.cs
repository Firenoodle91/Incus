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
using HKInc.Service.Helper;
using DevExpress.XtraEditors.Controls;

namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// 품목기준정보
    /// </summary>
    public partial class XFSTD1100 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFSTD1100()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.CustomUnboundColumnData += MainView_CustomUnboundColumnData;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitCombo()
        {
            lup_ProductTeamCode.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode));
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddUnboundColumn("ProdImage", LabelConvert.GetLabelText("ProdImage"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            GridExControl.MainGrid.AddUnboundColumn("PackPlasticImage", LabelConvert.GetLabelText("PackPlasticImage"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            GridExControl.MainGrid.AddUnboundColumn("OutBoxImage", LabelConvert.GetLabelText("OutBoxImage"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            GridExControl.MainGrid.AddColumn("ProdFileUrl", LabelConvert.GetLabelText("ProdFileUrl"), false);
            GridExControl.MainGrid.AddColumn("TN_STD1100_PACK_PLASTIC.ProdFileUrl", LabelConvert.GetLabelText("PackPlasticProdFileUrl"), false);
            GridExControl.MainGrid.AddColumn("TN_STD1100_OUT_BOX.ProdFileUrl", LabelConvert.GetLabelText("OutBoxProdFileUrl"), false);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ItemNameENG", LabelConvert.GetLabelText("ItemNameENG"));
            GridExControl.MainGrid.AddColumn("ItemNameCHN", LabelConvert.GetLabelText("ItemNameCHN"));
            GridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
            GridExControl.MainGrid.AddColumn("MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            GridExControl.MainGrid.AddColumn("BottomCategory", LabelConvert.GetLabelText("BottomCategory"));            
            GridExControl.MainGrid.AddColumn("CarType", LabelConvert.GetLabelText("CarType"));
            GridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            GridExControl.MainGrid.AddColumn("CustomerItemCode", LabelConvert.GetLabelText("CustomerItemCode"));
            GridExControl.MainGrid.AddColumn("CustomerItemName", LabelConvert.GetLabelText("CustomerItemName"));
            GridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            GridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            GridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            GridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            GridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));
            GridExControl.MainGrid.AddColumn("OutUnit", LabelConvert.GetLabelText("OutUnit"));
            GridExControl.MainGrid.AddColumn("Weight", LabelConvert.GetLabelText("UnitWeight"), HorzAlignment.Far, FormatType.Numeric, "#,0.#####");
            GridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "N0");
            GridExControl.MainGrid.AddColumn("ProdQty", LabelConvert.GetLabelText("ProperQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("Cost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("ProcTeamCode", LabelConvert.GetLabelText("ProductTeam"));
            GridExControl.MainGrid.AddColumn("StockPosition", LabelConvert.GetLabelText("DefaultStockPosition")); //어느것을 불러올지?
            GridExControl.MainGrid.AddColumn("TN_STD1100_TOOL.ItemCode", LabelConvert.GetLabelText("ToolCode2"));
            GridExControl.MainGrid.AddColumn("TN_STD1100_TOOL.ItemName", LabelConvert.GetLabelText("ToolName2"));
            GridExControl.MainGrid.AddColumn("ToolLifeQty", LabelConvert.GetLabelText("ToolLifeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0");
            GridExControl.MainGrid.AddColumn("TN_STD1100_TOOL2.ItemCode", LabelConvert.GetLabelText("ToolCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100_TOOL2.ItemName", LabelConvert.GetLabelText("ToolName"));
            GridExControl.MainGrid.AddColumn("ToolLifeQty2", LabelConvert.GetLabelText("ToolLifeQty2"), HorzAlignment.Far, FormatType.Numeric, "#,0");
            GridExControl.MainGrid.AddColumn("TN_STD1100_SRC.ItemCode", LabelConvert.GetLabelText("SrcItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100_SRC.ItemName", LabelConvert.GetLabelText("SrcItemName"));
            GridExControl.MainGrid.AddColumn("SrcWeight", LabelConvert.GetLabelText("SrcWeight"), HorzAlignment.Far, FormatType.Numeric, "#,0.#####");
            GridExControl.MainGrid.AddColumn("MainMachineCode", LabelConvert.GetLabelText("MachineGroup"));
            GridExControl.MainGrid.AddColumn("SurfaceList", LabelConvert.GetLabelText("SurfaceList"));
            GridExControl.MainGrid.AddColumn("GrindingFlag", LabelConvert.GetLabelText("GrindingFlag"));
            GridExControl.MainGrid.AddColumn("SelfInspFlag", LabelConvert.GetLabelText("SelfInspFlag"));
            GridExControl.MainGrid.AddColumn("StockInspFlag", LabelConvert.GetLabelText("StockInspFlag"));
            GridExControl.MainGrid.AddColumn("ProcInspFlag", LabelConvert.GetLabelText("ProcInspFlag"));
            GridExControl.MainGrid.AddColumn("ShipmentInspFlag", LabelConvert.GetLabelText("ShipmentInspFlag"));
            GridExControl.MainGrid.AddColumn("PackQty", LabelConvert.GetLabelText("PackQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("ProcessPackQty", LabelConvert.GetLabelText("ProcessPackQty"), HorzAlignment.Far, FormatType.Numeric, "#,0");
            GridExControl.MainGrid.AddColumn("TN_STD1100_PACK_PLASTIC.ItemCode", LabelConvert.GetLabelText("PackPlasticItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100_PACK_PLASTIC.ItemName", LabelConvert.GetLabelText("PackPlasticItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100_OUT_BOX.ItemCode", LabelConvert.GetLabelText("OutBoxItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100_OUT_BOX.ItemName", LabelConvert.GetLabelText("OutBoxItemName"));
            GridExControl.MainGrid.AddColumn("SetTime", LabelConvert.GetLabelText("SetTime"), HorzAlignment.Far, FormatType.Numeric, "#,0");
            GridExControl.MainGrid.AddColumn("ProcTime", LabelConvert.GetLabelText("ProcTime"), HorzAlignment.Far, FormatType.Numeric, "#,0");
            GridExControl.MainGrid.AddColumn("Heat", LabelConvert.GetLabelText("HeatTemperature"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Rpm", LabelConvert.GetLabelText("HeatRpm"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            GridExControl.MainGrid.Columns["ProdImage"].MinWidth = 100;
            GridExControl.MainGrid.Columns["ProdImage"].MaxWidth = 100;
            GridExControl.MainGrid.Columns["ProdImage"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            GridExControl.MainGrid.Columns["PackPlasticImage"].MinWidth = 100;
            GridExControl.MainGrid.Columns["PackPlasticImage"].MaxWidth = 100;
            GridExControl.MainGrid.Columns["PackPlasticImage"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            GridExControl.MainGrid.Columns["OutBoxImage"].MinWidth = 100;
            GridExControl.MainGrid.Columns["OutBoxImage"].MaxWidth = 100;            
            GridExControl.MainGrid.Columns["OutBoxImage"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

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
        }

        private void BarBarcodePrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var obj = GridBindingSource.Current as TN_STD1100;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.Value_1, "4");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFSTD1100_BARCODE_PRINT, param, BarcodePrintCallback);
            form.ShowPopup(true);
        }

        /// <summary>라벨출력 CallBack</summary>
        private void BarcodePrintCallback(object sender, PopupArgument e) { }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CarType", DbRequestHandler.GetCommCode(MasterCodeSTR.CarType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutUnit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcTeamCode", DbRequestHandler.GetCommCode(MasterCodeSTR.ProductTeamCode, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMachineCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SurfaceList", DbRequestHandler.GetCommCode(MasterCodeSTR.SurfaceList, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            GridExControl.MainGrid.SetRepositoryItemCheckEdit("GrindingFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("SelfInspFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("StockInspFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("ProcInspFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("ShipmentInspFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");

            GridExControl.MainGrid.SetRepositoryItemPictureEdit("ProdImage");
            GridExControl.MainGrid.SetRepositoryItemPictureEdit("PackPlasticImage");
            GridExControl.MainGrid.SetRepositoryItemPictureEdit("OutBoxImage");

            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var itemCodeName = tx_ItemCodeName.EditValue.GetNullToEmpty();
            var productTeamCode = lup_ProductTeamCode.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCodeName) ? true : (p.ItemCode.Contains(itemCodeName) || (p.ItemName.Contains(itemCodeName) || p.ItemNameENG.Contains(itemCodeName) || p.ItemNameCHN.Contains(itemCodeName))))
                                                                    && (string.IsNullOrEmpty(productTeamCode) ? true : p.ProcTeamCode == productTeamCode)
                                                                    &&  (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                                    &&  (p.TopCategory != MasterCodeSTR.TopCategory_SPARE && p.TopCategory != MasterCodeSTR.TopCategory_TOOL)
                                                               )
                                                               .OrderBy(p => p.ItemName)
                                                               .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        protected override void DeleteRow()
        {
            var obj = GridBindingSource.Current as TN_STD1100;

            if (obj != null)
            {
                var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("ItemInfo")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    obj.UseFlag = "N";
                    GridExControl.BestFitColumns();
                }
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFSTD1100, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }

        private void MainView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "ProdImage" && e.IsGetData)
            {
                var ProdFileUrl = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ProdFileUrl").GetNullToEmpty();

                if (ProdFileUrl.IsNullOrEmpty()) return;
                byte[] img = FileHandler.FtpToByte(Utils.Common.GlobalVariable.HTTP_SERVER + ProdFileUrl);
                e.Value = img;
            }
            else if (e.Column.FieldName == "PackPlasticImage" && e.IsGetData)
            {
                var ProdFileUrl = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "TN_STD1100_PACK_PLASTIC.ProdFileUrl").GetNullToEmpty();

                if (ProdFileUrl.IsNullOrEmpty()) return;
                byte[] img = FileHandler.FtpToByte(Utils.Common.GlobalVariable.HTTP_SERVER + ProdFileUrl);
                e.Value = img;
            }
            else if (e.Column.FieldName == "OutBoxImage" && e.IsGetData)
            {
                var ProdFileUrl = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "TN_STD1100_OUT_BOX.ProdFileUrl").GetNullToEmpty();

                if (ProdFileUrl.IsNullOrEmpty()) return;
                byte[] img = FileHandler.FtpToByte(Utils.Common.GlobalVariable.HTTP_SERVER + ProdFileUrl);
                e.Value = img;
            }
        }

    }
}