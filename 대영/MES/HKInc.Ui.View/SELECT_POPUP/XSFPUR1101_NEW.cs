using System;
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
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 자재발주디테일 Select 팝업
    /// </summary>
    public partial class XSFPUR1101_NEW : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_PUR1101> ModelService = (IService<TN_PUR1101>)ProductionFactory.GetDomainService("TN_PUR1101");
        IService<SCM_VI_PUR1201_SCM> ModelScmService = (IService<SCM_VI_PUR1201_SCM>)ProductionFactory.GetDomainService("SCM_VI_PUR1201_SCM");
        private bool IsmultiSelect = true;
        private string scmyn;
        private string inno;
        

        public XSFPUR1101_NEW()
        {
            InitializeComponent();
        }

        public XSFPUR1101_NEW(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("PoDetailInfo");

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            if (parameter.ContainsKey(PopupParameter.Constraint))
                tx_PoNo.EditValue = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();
            if (parameter.ContainsKey(PopupParameter.Value_1))
                scmyn = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            if (parameter.ContainsKey(PopupParameter.Value_2))
                inno = parameter.GetValue(PopupParameter.Value_2).GetNullToEmpty();

            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitBindingSource(){}

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_MAT || p.TopCategory == MasterCodeSTR.TopCategory_BU)).ToList());
            tx_PoNo.ReadOnly = true;
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "ItemCode", IsmultiSelect);

            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            GridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"), false);
            GridExControl.MainGrid.AddColumn("PoSeq", LabelConvert.GetLabelText("PoSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("TN_STD1100.StockInspFlag", LabelConvert.GetLabelText("StockInspFlag"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));

            if (scmyn == "Y")
            {
                GridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"));
                GridExControl.MainGrid.AddColumn("PoRemainQty", LabelConvert.GetLabelText("PoRemainQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
                GridExControl.MainGrid.AddColumn("InCost", LabelConvert.GetLabelText("PoCost"));
                GridExControl.MainGrid.AddUnboundColumn("IoAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([InQty],0) * ISNULL([InCost],0)", FormatType.Numeric, "#,###,###,###.##");
                GridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
                GridExControl.MainGrid.AddColumn("InCustomerLotNo", LabelConvert.GetLabelText("InCustomerLotNo"));
            }
            else if(scmyn == "N")
            {
                GridExControl.MainGrid.AddColumn("PoQty", LabelConvert.GetLabelText("PoQty"));
                GridExControl.MainGrid.AddColumn("PoRemainQty", LabelConvert.GetLabelText("PoRemainQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
                GridExControl.MainGrid.AddColumn("PoCost", LabelConvert.GetLabelText("PoCost"));
                GridExControl.MainGrid.AddUnboundColumn("PoAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([PoQty],0) * ISNULL([PoCost],0)", FormatType.Numeric, "#,###,###,###.##");
            }
            
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check","N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));

            if(scmyn == "Y")
            {
                GridExControl.MainGrid.SetRepositoryItemSpinEdit("InQty", DefaultBoolean.Default, "n2");
                GridExControl.MainGrid.SetRepositoryItemSpinEdit("InCost", DefaultBoolean.Default, "n2");
            }
            else if(scmyn == "N")
            {
                GridExControl.MainGrid.SetRepositoryItemSpinEdit("PoQty", DefaultBoolean.Default, "n2");
                GridExControl.MainGrid.SetRepositoryItemSpinEdit("PoCost", DefaultBoolean.Default, "n2");
            }
            
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
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

            var poNo = tx_PoNo.EditValue.GetNullToEmpty();
            var itemCode = lup_Item.EditValue.GetNullToEmpty();

            if(scmyn == "Y")
            {
                ModelBindingSource.DataSource = ModelScmService.GetList(p => p.PoNo == poNo
                                                                    && p.InNo == inno
                                                                    && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                ).ToList();
                                                              
            }
            else
            {
                ModelBindingSource.DataSource = ModelService.GetList(p => p.PoNo == poNo
                                                                    && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                )
                                                                .Where(p => p.PoRemainQty > 0 && p.InConfirmState != MasterCodeSTR.MaterialInConfirmFlag_End)
                                                                .OrderBy(o => o.PoSeq)
                                                                .ToList();
            }

            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();
        }

        protected override void Confirm()
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (scmyn == "Y")
            {

                if (IsmultiSelect)
                {
                    var returnList = new List<SCM_VI_PUR1201_SCM>();
                    var dataList = ModelBindingSource.List as List<SCM_VI_PUR1201_SCM>;
                    var checkList = dataList.Where(p => p._Check == "Y").ToList();
                    foreach (var v in checkList)
                    {
                        returnList.Add(ModelScmService.Detached(v));
                    }
                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelScmService.Detached((SCM_VI_PUR1201_SCM)ModelBindingSource.Current));
                }
            }
            else
            {

                if (IsmultiSelect)
                {
                    var returnList = new List<TN_PUR1101>();
                    var dataList = ModelBindingSource.List as List<TN_PUR1101>;
                    var checkList = dataList.Where(p => p._Check == "Y").ToList();
                    foreach (var v in checkList)
                    {
                        returnList.Add(ModelService.Detached(v));
                    }
                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_PUR1101)ModelBindingSource.Current));
                }

            }

            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {

                if (scmyn == "Y")
                {
                    var itemMaster = (SCM_VI_PUR1201_SCM)ModelBindingSource.Current;
                    if (IsmultiSelect)
                    {
                        var returnList = new List<SCM_VI_PUR1201_SCM>();
                        if (itemMaster != null)
                            returnList.Add(ModelScmService.Detached(itemMaster));
                        param.SetValue(PopupParameter.ReturnObject, returnList);
                    }
                    else
                    {
                        param.SetValue(PopupParameter.ReturnObject, ModelScmService.Detached(itemMaster));
                    }
                }
                else
                {
                    var itemMaster = (TN_PUR1101)ModelBindingSource.Current;
                    if (IsmultiSelect)
                    {
                        var returnList = new List<TN_PUR1101>();
                        if (itemMaster != null)
                            returnList.Add(ModelService.Detached(itemMaster));
                        param.SetValue(PopupParameter.ReturnObject, returnList);
                    }
                    else
                    {
                        param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(itemMaster));
                    }
                    
                }

                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }

        }
    }
}