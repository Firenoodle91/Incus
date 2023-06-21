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
using HKInc.Ui.Model.Domain;
using HKInc.Service.Handler.EventHandler;

namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// 품목기준정보
    /// </summary>
    public partial class XFSTD1100 : HKInc.Service.Base.ListFormTemplate
    {
        IService<VI_STD1100> ModelService = (IService<VI_STD1100>)ProductionFactory.GetDomainService("VI_STD1100");
        IService<TN_STD1100> ModelMainService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFSTD1100()
        {
            InitializeComponent();
            GridExControl = gridEx1;
           // GridExControl.MainGrid.MainView.CustomUnboundColumnData += MainView_CustomUnboundColumnData;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
            GridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
        }

        private void MainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 1)
            {
                VI_STD1100 obj = GridBindingSource.Current as VI_STD1100;
                if (e.Column.Name.ToString() == "ProdFileName")
                {
                    string fileName = obj.ProdFileName.GetNullToEmpty();
                    FileHandler.SaveFile(new FileHolder
                    {
                        FileName = fileName,
                        FileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + MasterCodeSTR.FtpFolder_ProdImage + "/" + fileName)
                    });
                }
                return;
            }
        }

        protected override void InitCombo()
        {
            lup_ProductTeamCode.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode));
        }

        /// <summary>
        /// 20220302 오세완 차장 기존 메서드가 정리가 안되서 사용 처리 
        /// 기존에 사용하던 차종하고 단위중량을 생략처리
        /// </summary>
        private void InitGrid_V2()
        {
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ItemNameENG", LabelConvert.GetLabelText("ItemNameENG"));
            GridExControl.MainGrid.AddColumn("ItemNameCHN", LabelConvert.GetLabelText("ItemNameCHN"));
            GridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));

            GridExControl.MainGrid.AddColumn("MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            GridExControl.MainGrid.AddColumn("BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            // 20220311 오세완 차장 권박사님 요청으로 추가
            GridExControl.MainGrid.AddColumn("Temp1", LabelConvert.GetLabelText("ColorCode"));
            // 20220314 오세완 차장 컬러명 추가 
            GridExControl.MainGrid.AddColumn("ColorName", LabelConvert.GetLabelText("ColorName"));
            GridExControl.MainGrid.AddColumn("Temp2", LabelConvert.GetLabelText("ColorNickname"));

            GridExControl.MainGrid.AddColumn("ProdFileName", LabelConvert.GetLabelText("ProdFileName"));
            GridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            GridExControl.MainGrid.AddColumn("CustomerItemCode", LabelConvert.GetLabelText("CustomerItemCode"));
            GridExControl.MainGrid.AddColumn("CustomerItemName", LabelConvert.GetLabelText("CustomerItemName"));
            GridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));

            GridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            GridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            GridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            GridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));
            GridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            GridExControl.MainGrid.AddColumn("ProdQty", LabelConvert.GetLabelText("ProperQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("Cost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("MoldCode", LabelConvert.GetLabelText("MoldName"));
            GridExControl.MainGrid.AddColumn("StockPosition", LabelConvert.GetLabelText("DefaultStockPosition")); 
            GridExControl.MainGrid.AddColumn("SrcCode", LabelConvert.GetLabelText("SrcItemCode"));

            GridExControl.MainGrid.AddColumn("SrcName", LabelConvert.GetLabelText("SrcItemName"));
            GridExControl.MainGrid.AddColumn("SrcWeight", LabelConvert.GetLabelText("SrcWeight"), HorzAlignment.Far, FormatType.Numeric, "#,0.#####");
            GridExControl.MainGrid.AddColumn("MainMachineCode", LabelConvert.GetLabelText("MainMachineCode"));
            GridExControl.MainGrid.AddColumn("SelfInspFlag", LabelConvert.GetLabelText("SelfInspFlag"));
            GridExControl.MainGrid.AddColumn("StockInspFlag", LabelConvert.GetLabelText("StockInspFlag"));

            GridExControl.MainGrid.AddColumn("ProcInspFlag", LabelConvert.GetLabelText("ProcInspFlag"));
            GridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            GridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);

            GridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            GridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);
        }

        protected override void InitGrid()
        {
            /*
            //GridExControl.MainGrid.AddUnboundColumn("ProdImage", LabelConvert.GetLabelText("ProdImage"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            //GridExControl.MainGrid.AddUnboundColumn("PackPlasticImage", LabelConvert.GetLabelText("PackPlasticImage"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            //GridExControl.MainGrid.AddUnboundColumn("OutBoxImage", LabelConvert.GetLabelText("OutBoxImage"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);

            //GridExControl.MainGrid.AddColumn("TN_STD1100_PACK_PLASTIC.ProdFileUrl", LabelConvert.GetLabelText("PackPlasticProdFileUrl"), false);
            //GridExControl.MainGrid.AddColumn("TN_STD1100_OUT_BOX.ProdFileUrl", LabelConvert.GetLabelText("OutBoxProdFileUrl"), false);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ItemNameENG", LabelConvert.GetLabelText("ItemNameENG"));
            GridExControl.MainGrid.AddColumn("ItemNameCHN", LabelConvert.GetLabelText("ItemNameCHN"));
            GridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
            GridExControl.MainGrid.AddColumn("MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            GridExControl.MainGrid.AddColumn("BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            //GridExControl.MainGrid.AddColumn("ProdFileUrl", LabelConvert.GetLabelText("ProdFileUrl"), false);
            GridExControl.MainGrid.AddColumn("ProdFileName", LabelConvert.GetLabelText("ProdFileName"));
            GridExControl.MainGrid.AddColumn("CarType", LabelConvert.GetLabelText("CarType"));
            GridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            GridExControl.MainGrid.AddColumn("CustomerItemCode", LabelConvert.GetLabelText("CustomerItemCode"));
            GridExControl.MainGrid.AddColumn("CustomerItemName", LabelConvert.GetLabelText("CustomerItemName"));
            GridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            GridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            GridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            GridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            GridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));
            GridExControl.MainGrid.AddColumn("Weight", LabelConvert.GetLabelText("UnitWeight"), HorzAlignment.Far, FormatType.Numeric, "#,0.#####");
            GridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("ProdQty", LabelConvert.GetLabelText("ProperQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("Cost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("MoldCode", LabelConvert.GetLabelText("MoldName"));
            //GridExControl.MainGrid.AddColumn("ProcTeamCode", LabelConvert.GetLabelText("ProductTeam"),false);
            GridExControl.MainGrid.AddColumn("StockPosition", LabelConvert.GetLabelText("DefaultStockPosition")); //어느것을 불러올지?
            //GridExControl.MainGrid.AddColumn("TN_STD1100_TOOL.ItemCode", LabelConvert.GetLabelText("ToolCode2"));
            //GridExControl.MainGrid.AddColumn("TN_STD1100_TOOL.ItemName", LabelConvert.GetLabelText("ToolName2"));
            //GridExControl.MainGrid.AddColumn("ToolLifeQty", LabelConvert.GetLabelText("ToolLifeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0");
            //GridExControl.MainGrid.AddColumn("TN_STD1100_TOOL2.ItemCode", LabelConvert.GetLabelText("ToolCode"));
            //GridExControl.MainGrid.AddColumn("TN_STD1100_TOOL2.ItemName", LabelConvert.GetLabelText("ToolName"));
            //GridExControl.MainGrid.AddColumn("ToolLifeQty2", LabelConvert.GetLabelText("ToolLifeQty2"), HorzAlignment.Far, FormatType.Numeric, "#,0");
            GridExControl.MainGrid.AddColumn("SrcCode", LabelConvert.GetLabelText("SrcItemCode"));
            GridExControl.MainGrid.AddColumn("SrcName", LabelConvert.GetLabelText("SrcItemName"));
            GridExControl.MainGrid.AddColumn("SrcWeight", LabelConvert.GetLabelText("SrcWeight"), HorzAlignment.Far, FormatType.Numeric, "#,0.#####");
            GridExControl.MainGrid.AddColumn("MainMachineCode", LabelConvert.GetLabelText("MainMachineCode"));
            //GridExControl.MainGrid.AddColumn("SurfaceList", LabelConvert.GetLabelText("SurfaceList"), false);
            //GridExControl.MainGrid.AddColumn("GrindingFlag", LabelConvert.GetLabelText("GrindingFlag"), false);
            GridExControl.MainGrid.AddColumn("SelfInspFlag", LabelConvert.GetLabelText("SelfInspFlag"));
            GridExControl.MainGrid.AddColumn("StockInspFlag", LabelConvert.GetLabelText("StockInspFlag"));
            GridExControl.MainGrid.AddColumn("ProcInspFlag", LabelConvert.GetLabelText("ProcInspFlag"));
            //GridExControl.MainGrid.AddColumn("ShipmentInspFlag", LabelConvert.GetLabelText("ShipmentInspFlag"), false);
            //GridExControl.MainGrid.AddColumn("PackQty", LabelConvert.GetLabelText("PackQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            //GridExControl.MainGrid.AddColumn("ProcessPackQty", LabelConvert.GetLabelText("ProcessPackQty"), HorzAlignment.Far, FormatType.Numeric, "#,0");
            //GridExControl.MainGrid.AddColumn("TN_STD1100_PACK_PLASTIC.ItemCode", LabelConvert.GetLabelText("PackPlasticItemCode"), false);
            //GridExControl.MainGrid.AddColumn("TN_STD1100_PACK_PLASTIC.ItemName", LabelConvert.GetLabelText("PackPlasticItemName"), false);
            //GridExControl.MainGrid.AddColumn("TN_STD1100_OUT_BOX.ItemCode", LabelConvert.GetLabelText("OutBoxItemCode"), false);
            //GridExControl.MainGrid.AddColumn("TN_STD1100_OUT_BOX.ItemName", LabelConvert.GetLabelText("OutBoxItemName"), false);
            //GridExControl.MainGrid.AddColumn("SetTime", LabelConvert.GetLabelText("SetTime"), HorzAlignment.Far, FormatType.Numeric, "#,0");
            //GridExControl.MainGrid.AddColumn("ProcTime", LabelConvert.GetLabelText("ProcTime"), HorzAlignment.Far, FormatType.Numeric, "#,0");
            //GridExControl.MainGrid.AddColumn("Heat", LabelConvert.GetLabelText("HeatTemperature"), HorzAlignment.Center, true);
            //GridExControl.MainGrid.AddColumn("Rpm", LabelConvert.GetLabelText("HeatRpm"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            GridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            GridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            GridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);
            //GridExControl.MainGrid.Columns["ProdImage"].MinWidth = 100;
            //GridExControl.MainGrid.Columns["ProdImage"].MaxWidth = 100;
            //GridExControl.MainGrid.Columns["ProdImage"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //GridExControl.MainGrid.Columns["PackPlasticImage"].MinWidth = 100;
            //GridExControl.MainGrid.Columns["PackPlasticImage"].MaxWidth = 100;
            //GridExControl.MainGrid.Columns["PackPlasticImage"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //GridExControl.MainGrid.Columns["OutBoxImage"].MinWidth = 100;
            //GridExControl.MainGrid.Columns["OutBoxImage"].MaxWidth = 100;            
            //GridExControl.MainGrid.Columns["OutBoxImage"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center; 
            */

            InitGrid_V2();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CarType", DbRequestHandler.GetCommCode(MasterCodeSTR.CarType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcTeamCode", DbRequestHandler.GetCommCode(MasterCodeSTR.ProductTeamCode, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMachineCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SurfaceList", DbRequestHandler.GetCommCode(MasterCodeSTR.SurfaceList, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, GridExControl, MasterCodeSTR.FtpFolder_ProdImage, "ProdFileName", "ProdFileUrl");
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("GrindingFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("SelfInspFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("StockInspFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("ProcInspFlag", "N");
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("ShipmentInspFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldCode", ModelService.GetChildList<TN_MOLD1100>(p => p.UseYN == "Y").ToList(), "MoldMCode", DataConvert.GetCultureDataFieldName("MoldName"), true);

            //GridExControl.MainGrid.SetRepositoryItemPictureEdit("ProdImage");
            //GridExControl.MainGrid.SetRepositoryItemPictureEdit("PackPlasticImage");
            //GridExControl.MainGrid.SetRepositoryItemPictureEdit("OutBoxImage");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");

            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            GridRowLocator.GetCurrentRow("ItemCode", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var itemCodeName = tx_ItemCodeName.EditValue.GetNullToEmpty();
            var productTeamCode = lup_ProductTeamCode.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCodeName) ? true : (p.ItemCode.Contains(itemCodeName) || (p.ItemName.Contains(itemCodeName) || p.ItemName.Contains(itemCodeName) || p.ItemNameENG.Contains(itemCodeName) || p.ItemNameCHN.Contains(itemCodeName))))
                                                                    && (string.IsNullOrEmpty(productTeamCode) ? true : p.ProcTeamCode == productTeamCode)
                                                                    &&  (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                                    &&  (p.TopCategory != MasterCodeSTR.TopCategory_SPARE && p.TopCategory != MasterCodeSTR.TopCategory_TOOL)
                                                               )
                                                               .OrderBy(p => p.ItemCode)
                                                               .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
        }

        protected override void DataSave()
        {
          
            ModelMainService.Save();
            DataLoad();
        }

        protected override void DeleteRow()
        {
            var obj = GridBindingSource.Current as VI_STD1100;

            if (obj != null)
            {
                var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("ItemInfo")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    obj.UseFlag = "N";

                    var delobj = ModelMainService.GetList(p => p.ItemCode == obj.ItemCode).FirstOrDefault();
                    delobj.UseFlag = "N";

                    ModelMainService.Update(delobj);
                    GridExControl.BestFitColumns();
                }
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            //return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFSTD1100, param, PopupRefreshCallback);
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFSTD1100_V2, param, PopupRefreshCallback); // 20211210 오세완 차장 케이즈 스타일 품목기준 팝업 추가 
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }

        private void MainView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            //if (e.Column.FieldName == "ProdImage" && e.IsGetData)
            //{
            //    var ProdFileUrl = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ProdFileUrl").GetNullToEmpty();

            //    if (ProdFileUrl.IsNullOrEmpty()) return;
            //    byte[] img = FileHandler.FtpToByte(Utils.Common.GlobalVariable.HTTP_SERVER + ProdFileUrl);
            //    e.Value = img;
            //}
            //else if (e.Column.FieldName == "PackPlasticImage" && e.IsGetData)
            //{
            //    var ProdFileUrl = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "TN_STD1100_PACK_PLASTIC.ProdFileUrl").GetNullToEmpty();

            //    if (ProdFileUrl.IsNullOrEmpty()) return;
            //    byte[] img = FileHandler.FtpToByte(Utils.Common.GlobalVariable.HTTP_SERVER + ProdFileUrl);
            //    e.Value = img;
            //}
            //else if (e.Column.FieldName == "OutBoxImage" && e.IsGetData)
            //{
            //    var ProdFileUrl = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "TN_STD1100_OUT_BOX.ProdFileUrl").GetNullToEmpty();

            //    if (ProdFileUrl.IsNullOrEmpty()) return;
            //    byte[] img = FileHandler.FtpToByte(Utils.Common.GlobalVariable.HTTP_SERVER + ProdFileUrl);
            //    e.Value = img;
            //}
        }

    }
}