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
using System.Data.SqlClient;

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 자재반품상세팝업
    /// </summary>
    public partial class XSFPUR1201_RETURN : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_PUR1305> ModelService = (IService<VI_PUR1305>)ProductionFactory.GetDomainService("VI_PUR1305");
        private bool IsmultiSelect = true;
        private string pono;
        private string inno;



        public XSFPUR1201_RETURN()
        {
            InitializeComponent();
        }

        public XSFPUR1201_RETURN(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("입고상세목록");

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);
            if (parameter.ContainsKey(PopupParameter.Constraint))
                pono = (string)parameter.GetValue(PopupParameter.Constraint);
            if (parameter.ContainsKey(PopupParameter.Value_1))
                inno = (string)parameter.GetValue(PopupParameter.Value_1);

            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitBindingSource() { }

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
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "InLotNo", IsmultiSelect);
            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            GridExControl.MainGrid.AddColumn("StockInspFlag", LabelConvert.GetLabelText("StockInspFlag"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("InConfirmFlag", LabelConvert.GetLabelText("InConfirmFlag"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"));
            GridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("BottomCategory", LabelConvert.GetLabelText("BottomCategory"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("CombineSpec", LabelConvert.GetLabelText("Spec"));
            GridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
            GridExControl.MainGrid.AddColumn("InCost", LabelConvert.GetLabelText("InCost"), HorzAlignment.Far, FormatType.Numeric, "n2");
            GridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            GridExControl.MainGrid.AddColumn("InspNo", LabelConvert.GetLabelText("InspNo"));
            GridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("OkQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
            GridExControl.MainGrid.AddColumn("NgQty", LabelConvert.GetLabelText("InspectNgQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
            GridExControl.MainGrid.AddColumn("InInspectionState", LabelConvert.GetLabelText("InInspectionState"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("InWhCode", LabelConvert.GetLabelText("InWhCode"));
            GridExControl.MainGrid.AddColumn("InWhPosition", LabelConvert.GetLabelText("InWhPosition"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.AddColumn("Memo1", LabelConvert.GetLabelText("CustomerMemo"));
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InCustomerId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InCustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhCode", ModelService.GetChildList<TN_WMS1000>(p => (p.Temp == MasterCodeSTR.WhCodeDivision_MAT || p.Temp == MasterCodeSTR.WhCodeDivision_BU || p.Temp == null) && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
            GridExControl.MainGrid.MainView.Columns["Memo1"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo1");
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

            string itemCode = lup_Item.EditValue.GetNullToEmpty();
            string inlotno = tx_InLotNo.Text.GetNullToEmpty();
            //string poId = lup_PoId.EditValue.GetNullToEmpty();

            ModelBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                   && (string.IsNullOrEmpty(inlotno) ? true : (p.InLotNo.Contains(inlotno)))
                                                                   && p.PoNo == pono
                                                                   && p.InNo == inno).ToList();

            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();
        }

        protected override void Confirm()
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (IsmultiSelect)
            {
                var returnList = new List<VI_PUR1305>();
                var dataList = ModelBindingSource.List as List<VI_PUR1305>;
                var checkList = dataList.Where(p => p._Check == "Y").ToList();
                foreach (var v in checkList)
                {
                    returnList.Add(ModelService.Detached(v));
                }
                param.SetValue(PopupParameter.ReturnObject, returnList);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_PUR1305)ModelBindingSource.Current));
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
                var InMaster = (VI_PUR1305)ModelBindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<VI_PUR1305>();
                    if (InMaster != null)
                        returnList.Add(ModelService.Detached(InMaster));
                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(InMaster));
                }

                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }

        }
    }
}