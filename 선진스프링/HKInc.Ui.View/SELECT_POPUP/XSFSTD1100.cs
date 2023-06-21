﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 품목 Select 팝업
    /// </summary>
    public partial class XSFSTD1100 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string Constraint = string.Empty;

        public XSFSTD1100()
        {
            InitializeComponent();
        }

        public XSFSTD1100(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Name);

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);
            if (parameter.ContainsKey(PopupParameter.Constraint))
                Constraint = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();
            if (parameter.ContainsKey(PopupParameter.Value_1))
                lup_Customer.EditValue = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            if (parameter.ContainsKey(PopupParameter.Value_2))
            {
                if (parameter.GetValue(PopupParameter.Value_2).GetNullToEmpty() == "XFORD1000")
                {
                    lup_Item.EditValueChanged += Lup_Item_EditValueChanged;
                }
            }

            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        private void Lup_Item_EditValueChanged(object sender, EventArgs e)
        {
            lup_Customer.EditValue = null;
            ActRefresh();
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => (p.UseFlag == "Y") && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList());

            if (Constraint.IsNullOrEmpty()) //전품목조회
            {
                lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => (p.UseFlag == "Y")).ToList());
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemWAN)  //완제품만조회
            {
                lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => (p.UseFlag == "Y") && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList());
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemBAN)  //반제품만조회
            {
                lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => (p.UseFlag == "Y") && (p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemWAN_BAN)  //완제품&반제품만조회
            {
                lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => (p.UseFlag == "Y") && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemMAT)  //자재만조회
            {
                lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => (p.UseFlag == "Y") && (p.TopCategory == MasterCodeSTR.TopCategory_MAT)).ToList());
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemBU)  //소모품만조회
            {
                lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => (p.UseFlag == "Y") && (p.TopCategory == MasterCodeSTR.TopCategory_BU)).ToList());
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemMAT_BU)  //자재&소모품만조회
            {
                lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => (p.UseFlag == "Y") && (p.TopCategory == MasterCodeSTR.TopCategory_MAT || p.TopCategory == MasterCodeSTR.TopCategory_BU)).ToList());
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemTOOL)  //툴만조회
            {
                lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => (p.UseFlag == "Y") && (p.TopCategory == MasterCodeSTR.TopCategory_TOOL)).ToList());
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemSPARE)  //스페어파트만조회
            {
                lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => (p.UseFlag == "Y") && (p.TopCategory == MasterCodeSTR.TopCategory_SPARE)).ToList());
            }
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.CheckBoxMultiSelect(true, "ItemCode", IsmultiSelect);

            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
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
            GridExControl.MainGrid.AddColumn("Weight", LabelConvert.GetLabelText("UnitWeight"), HorzAlignment.Far, FormatType.Numeric, "n3");
            GridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "N0");
            GridExControl.MainGrid.AddColumn("ProdQty", LabelConvert.GetLabelText("ProperQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
            GridExControl.MainGrid.AddColumn("Cost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "n2");
            GridExControl.MainGrid.AddColumn("ProcTeamCode", LabelConvert.GetLabelText("ProductTeam"));
            GridExControl.MainGrid.AddColumn("StockPosition", LabelConvert.GetLabelText("DefaultStockPosition")); //어느것을 불러올지?
            GridExControl.MainGrid.AddColumn("TN_STD1100_SRC.ItemCode", LabelConvert.GetLabelText("SrcItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100_SRC.ItemName", LabelConvert.GetLabelText("SrcItemName"));
            GridExControl.MainGrid.AddColumn("SrcWeight", LabelConvert.GetLabelText("SrcWeight"), HorzAlignment.Far, FormatType.Numeric, "n3");
            GridExControl.MainGrid.AddColumn("MainMachineCode", LabelConvert.GetLabelText("MachineGroup"));
            GridExControl.MainGrid.AddColumn("SurfaceList", LabelConvert.GetLabelText("SurfaceList"));
            GridExControl.MainGrid.AddColumn("GrindingFlag", LabelConvert.GetLabelText("GrindingFlag"));
            GridExControl.MainGrid.AddColumn("SelfInspFlag", LabelConvert.GetLabelText("SelfInspFlag"));
            GridExControl.MainGrid.AddColumn("StockInspFlag", LabelConvert.GetLabelText("StockInspFlag"));
            GridExControl.MainGrid.AddColumn("ProcInspFlag", LabelConvert.GetLabelText("ProcInspFlag"));
            GridExControl.MainGrid.AddColumn("ShipmentInspFlag", LabelConvert.GetLabelText("ShipmentInspFlag"));
            GridExControl.MainGrid.AddColumn("ProdFileName", LabelConvert.GetLabelText("ProdFileName"));
            GridExControl.MainGrid.AddColumn("ProdFileUrl", LabelConvert.GetLabelText("ProdFileUrl"), false);
            GridExControl.MainGrid.AddColumn("PackQty", LabelConvert.GetLabelText("PackQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
            GridExControl.MainGrid.AddColumn("TN_STD1100_PACK_PLASTIC.ItemCode", LabelConvert.GetLabelText("PackPlasticItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100_PACK_PLASTIC.ItemName", LabelConvert.GetLabelText("PackPlasticItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100_OUT_BOX.ItemCode", LabelConvert.GetLabelText("OutBoxItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100_OUT_BOX.ItemName", LabelConvert.GetLabelText("OutBoxItemName"));
            GridExControl.MainGrid.AddColumn("SetTime", LabelConvert.GetLabelText("SetTime"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("ProcTime", LabelConvert.GetLabelText("ProcTime"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable("_Check");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CarType", DbRequestHandler.GetCommCode(MasterCodeSTR.CarType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcTeamCode", DbRequestHandler.GetCommCode(MasterCodeSTR.ProductTeamCode, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMachineCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SurfaceList", DbRequestHandler.GetCommCode(MasterCodeSTR.SurfaceList, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("GrindingFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("SelfInspFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("StockInspFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("ProcInspFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("ShipmentInspFlag", "N");

            GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, MasterCodeSTR.FtpFolder_ProdImage, "ProdFileName", "ProdFileUrl", true);
            GridExControl.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var itemCodeName = lup_Item.EditValue.GetNullToEmpty(); //tx_ItemCodeName.EditValue.GetNullToEmpty();
            var CustomerCode = lup_Customer.EditValue.GetNullToEmpty();

            if (Constraint.IsNullOrEmpty()) //전품목조회
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCodeName) ? true : p.ItemCode == itemCodeName)//(p.ItemCode.Contains(itemCodeName) || (p.ItemName.Contains(itemCodeName) || p.ItemNameENG.Contains(itemCodeName) || p.ItemNameCHN.Contains(itemCodeName))))
                                                                        && (p.UseFlag == "Y")
                                                                        && (string.IsNullOrEmpty(CustomerCode) ? true : (p.MainCustomerCode == CustomerCode))
                                                               )
                                                               .OrderBy(p => p.ItemName)
                                                               .ToList();
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemWAN)  //완제품만조회
            {
                if (PopupParam.GetValue(PopupParameter.Value_2).GetNullToEmpty() == "XFORD1000")
                {
                    if (CustomerCode.IsNullOrEmpty())
                    {
                        bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCodeName) ? true : p.ItemCode == itemCodeName)//(p.ItemCode.Contains(itemCodeName) || (p.ItemName.Contains(itemCodeName) || p.ItemNameENG.Contains(itemCodeName) || p.ItemNameCHN.Contains(itemCodeName))))
                                                                            && (p.UseFlag == "Y")
                                                                            && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)
                                                                   )
                                                                   .OrderBy(p => p.ItemName)
                                                                   .ToList();
                    }
                    else
                    {
                        var itemList = ModelService.GetChildList<TN_ORD1001>(p => p.TN_ORD1000.OrderCustomerCode == CustomerCode && (string.IsNullOrEmpty(itemCodeName) ? true : p.ItemCode == itemCodeName)).Select(p => p.ItemCode).Distinct().ToArray();
                        bindingSource.DataSource = ModelService.GetList(p => itemList.Contains(p.ItemCode)//(p.ItemCode.Contains(itemCodeName) || (p.ItemName.Contains(itemCodeName) || p.ItemNameENG.Contains(itemCodeName) || p.ItemNameCHN.Contains(itemCodeName))))
                                                                            && (p.UseFlag == "Y")
                                                                   )
                                                                   .OrderBy(p => p.ItemName)
                                                                   .ToList();
                    }
                }
                else
                {
                    bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCodeName) ? true : p.ItemCode == itemCodeName)//(p.ItemCode.Contains(itemCodeName) || (p.ItemName.Contains(itemCodeName) || p.ItemNameENG.Contains(itemCodeName) || p.ItemNameCHN.Contains(itemCodeName))))
                                                                        && (p.UseFlag == "Y")
                                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)
                                                                        && (string.IsNullOrEmpty(CustomerCode) ? true : (p.MainCustomerCode == CustomerCode))
                                                               )
                                                               .OrderBy(p => p.ItemName)
                                                               .ToList();
                }
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemBAN)  //반제품만조회
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCodeName) ? true : p.ItemCode == itemCodeName)//(p.ItemCode.Contains(itemCodeName) || (p.ItemName.Contains(itemCodeName) || p.ItemNameENG.Contains(itemCodeName) || p.ItemNameCHN.Contains(itemCodeName))))
                                                                        && (p.UseFlag == "Y")
                                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_BAN)
                                                                        && (string.IsNullOrEmpty(CustomerCode) ? true : (p.MainCustomerCode == CustomerCode))
                                                               )
                                                               .OrderBy(p => p.ItemName)
                                                               .ToList();
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemWAN_BAN)  //완제품&반제품만조회
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCodeName) ? true : p.ItemCode == itemCodeName)//(p.ItemCode.Contains(itemCodeName) || (p.ItemName.Contains(itemCodeName) || p.ItemNameENG.Contains(itemCodeName) || p.ItemNameCHN.Contains(itemCodeName))))
                                                                        && (p.UseFlag == "Y")
                                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)
                                                                        && (string.IsNullOrEmpty(CustomerCode) ? true : (p.MainCustomerCode == CustomerCode))
                                                               )
                                                               .OrderBy(p => p.ItemName)
                                                               .ToList();
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemMAT)  //자재만조회
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCodeName) ? true : p.ItemCode == itemCodeName)//(p.ItemCode.Contains(itemCodeName) || (p.ItemName.Contains(itemCodeName) || p.ItemNameENG.Contains(itemCodeName) || p.ItemNameCHN.Contains(itemCodeName))))
                                                                        && (p.UseFlag == "Y")
                                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_MAT)
                                                                        && (string.IsNullOrEmpty(CustomerCode) ? true : (p.MainCustomerCode == CustomerCode))
                                                               )
                                                               .OrderBy(p => p.ItemName)
                                                               .ToList();
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemBU)  //소모품만조회
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCodeName) ? true : p.ItemCode == itemCodeName)//(p.ItemCode.Contains(itemCodeName) || (p.ItemName.Contains(itemCodeName) || p.ItemNameENG.Contains(itemCodeName) || p.ItemNameCHN.Contains(itemCodeName))))
                                                                        && (p.UseFlag == "Y")
                                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_BU)
                                                                        && (string.IsNullOrEmpty(CustomerCode) ? true : (p.MainCustomerCode == CustomerCode))
                                                               )
                                                               .OrderBy(p => p.ItemName)
                                                               .ToList();
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemMAT_BU)  //자재&소모품만조회
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCodeName) ? true : p.ItemCode == itemCodeName)//(p.ItemCode.Contains(itemCodeName) || (p.ItemName.Contains(itemCodeName) || p.ItemNameENG.Contains(itemCodeName) || p.ItemNameCHN.Contains(itemCodeName))))
                                                                        && (p.UseFlag == "Y")
                                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_MAT || p.TopCategory == MasterCodeSTR.TopCategory_BU)
                                                                        && (string.IsNullOrEmpty(CustomerCode) ? true : (p.MainCustomerCode == CustomerCode))
                                                               )
                                                               .OrderBy(p => p.ItemName)
                                                               .ToList();
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemTOOL)  //툴만조회
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCodeName) ? true : p.ItemCode == itemCodeName)//(p.ItemCode.Contains(itemCodeName) || (p.ItemName.Contains(itemCodeName) || p.ItemNameENG.Contains(itemCodeName) || p.ItemNameCHN.Contains(itemCodeName))))
                                                                        && (p.UseFlag == "Y")
                                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_TOOL)
                                                                        && (string.IsNullOrEmpty(CustomerCode) ? true : (p.MainCustomerCode == CustomerCode))
                                                               )
                                                               .OrderBy(p => p.ItemName)
                                                               .ToList();
            }
            else if (Constraint == MasterCodeSTR.Contraint_ItemSPARE)  //스페어파트만조회
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCodeName) ? true : p.ItemCode == itemCodeName)//(p.ItemCode.Contains(itemCodeName) || (p.ItemName.Contains(itemCodeName) || p.ItemNameENG.Contains(itemCodeName) || p.ItemNameCHN.Contains(itemCodeName))))
                                                                        && (p.UseFlag == "Y")
                                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_SPARE)
                                                                        && (string.IsNullOrEmpty(CustomerCode) ? true : (p.MainCustomerCode == CustomerCode))
                                                               )
                                                               .OrderBy(p => p.ItemName)
                                                               .ToList();
            }
            GridExControl.DataSource = bindingSource;
            GridExControl.BestFitColumns();
            SetRefreshMessage(GridExControl);


            if (PopupParam.ContainsKey(PopupParameter.Value_2) && !itemCodeName.IsNullOrEmpty())
            {
                if (PopupParam.GetValue(PopupParameter.Value_2).GetNullToEmpty() == "XFORD1000")
                {
                    var List = bindingSource.List as List<TN_STD1100>;
                    if (List.Count == 1)
                    {
                        List.First()._Check = "Y";
                        Confirm();
                    }
                }
            }
        }

        protected override void Confirm()
        {
            if (bindingSource == null || bindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (IsmultiSelect)
            {
                var returnList = new List<TN_STD1100>();
                var dataList = bindingSource.List as List<TN_STD1100>;
                var checkList = dataList.Where(p => p._Check == "Y").ToList();
                foreach (var v in dataList.Where(p => p._Check == "Y").ToList())
                {
                    var returnObj = ModelService.GetList(p => p.ItemCode == v.ItemCode).FirstOrDefault();
                    if (returnObj != null)
                        returnList.Add(ModelService.Detached(returnObj));
                }
                param.SetValue(PopupParameter.ReturnObject, returnList);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_STD1100)bindingSource.Current));
            }
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var itemMaster = (TN_STD1100)bindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<TN_STD1100>();
                    if (itemMaster != null)
                        returnList.Add(ModelService.Detached(itemMaster));
                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(itemMaster));
                }

                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }

        }
    }
}