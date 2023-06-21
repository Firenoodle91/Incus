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
using HKInc.Service.Base;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Common;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Handler;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.View.PUR_POPUP
{
    public partial class XPFPUR1201 : PopupCallbackFormTemplate
    {
        IService<TN_PUR1201> ModelService = (IService<TN_PUR1201>)ProductionFactory.GetDomainService("TN_PUR1201");
        TN_PUR1201 detailObj;

        public XPFPUR1201()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            GridExControl.ActAddRowClicked += GridExControl_ActAddRowClicked;
            GridExControl.ActDeleteRowClicked += GridExControl_ActDeleteRowClicked;

            ModelBindingSource.CurrentItemChanged -= ModelBindingSource_CurrentItemChanged;
        }

        public XPFPUR1201(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            detailObj = (TN_PUR1201)PopupParam.GetValue(PopupParameter.KeyValue);
        }
        
        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitControls()
        {
            base.InitControls();

            textItemCode.ReadOnly = true;
            textItemName.ReadOnly = true;
            tx_PoNo.ReadOnly = true;
            tx_PoQty.ReadOnly = true;
            tx_PoCost.ReadOnly = true;
            tx_Amt.ReadOnly = true;
            tx_InQty.ReadOnly = true;

            var cultureIndex = DataConvert.GetCultureIndex();

            textItemCode.EditValue = detailObj.ItemCode;
            textItemName.EditValue = cultureIndex == 1 ? detailObj.TN_STD1100.ItemName : (cultureIndex == 2 ? detailObj.TN_STD1100.ItemNameENG : detailObj.TN_STD1100.ItemNameCHN);
            tx_PoNo.EditValue = detailObj.PoNo;
            tx_PoQty.EditValue = detailObj.TN_PUR1101.PoQty.GetDecimalNullToZero().ToString("#,0.##");
            tx_PoCost.EditValue = detailObj.TN_PUR1101.PoCost.GetDecimalNullToZero().ToString("#,0.##");
            tx_Amt.EditValue = (detailObj.TN_PUR1101.PoQty.GetDecimalNullToZero() * detailObj.TN_PUR1101.PoCost.GetDecimalNullToZero()).ToString("#,0.##");
            tx_InQty.EditValue = detailObj.InQty;
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            GridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"), false);
            GridExControl.MainGrid.AddColumn("InSeq", LabelConvert.GetLabelText("InSeq"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            GridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("InCost", LabelConvert.GetLabelText("InCost"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddUnboundColumn("InAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([InQty],0) * ISNULL([InCost],0)", FormatType.Numeric, "#,###,###,###.##");
            GridExControl.MainGrid.AddColumn("PrintQty", LabelConvert.GetLabelText("Qty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##", false);
            //GridExControl.MainGrid.AddColumn("Lqty", LabelConvert.GetLabelText("Qty"));
            GridExControl.MainGrid.AddColumn("InWhCode", LabelConvert.GetLabelText("InWhCode"));
            GridExControl.MainGrid.AddColumn("InWhPosition", LabelConvert.GetLabelText("InWhPosition"));
            GridExControl.MainGrid.AddColumn("Memo");
            GridExControl.MainGrid.SetEditable("InQty", "InCost");

            GridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;

            GridExControl.MainGrid.MainView.Columns["InQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["InQty"].SummaryItem.FieldName = "InQty";
            GridExControl.MainGrid.MainView.Columns["InQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["InAmt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["InAmt"].SummaryItem.FieldName = "InAmt";
            GridExControl.MainGrid.MainView.Columns["InAmt"].SummaryItem.DisplayFormat = "{0:#,0.##}";
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("InQty", DefaultBoolean.Default, "n2", true);
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("InCost", DefaultBoolean.Default, "n2", true);
            //GridExControl.MainGrid.SetRepositoryItemSpinEdit("Lqty", DefaultBoolean.Default, "n0", true);         
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhCode", ModelService.GetChildList<TN_WMS1000>(p => (p.Temp == MasterCodeSTR.WhCodeDivision_MAT || p.Temp == MasterCodeSTR.WhCodeDivision_BU || p.Temp == null) && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);
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

            ModelBindingSource.DataSource = ModelService.GetList(p => p.InLotNo == detailObj.InLotNo).ToList();

            GridExControl.DataSource = ModelBindingSource;

            var list = ModelBindingSource.List as List<TN_PUR1201>;
            if (list.Count == 0)
            {
                //저장을 안하고 분할처리 버튼 클릭 시
                ModelBindingSource.Add(detailObj);
            }

            GridExControl.BestFitColumns();

            SetIsFormControlChanged(false);
        }

        private void GridExControl_ActAddRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var newObj = new TN_PUR1201();
            newObj.ItemCode = detailObj.ItemCode;
            newObj.PrintQty = 1;
            newObj.InCost = detailObj.InCost;
            newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == detailObj.ItemCode).FirstOrDefault();
            newObj.Temp = newObj.Temp;
            var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PositionCode == newObj.TN_STD1100.StockPosition).FirstOrDefault();
            if (WMS2000 != null)
            {
                newObj.InWhCode = WMS2000.WhCode;
                newObj.InWhPosition = WMS2000.PositionCode;
            }

            ModelBindingSource.Add(newObj);
            ChangePrintQty();
        }

        private void GridExControl_ActDeleteRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TN_PUR1201 dtlobj = ModelBindingSource.Current as TN_PUR1201;
            if (!dtlobj.InLotNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_111));
                return;
            }
            ModelBindingSource.RemoveCurrent();
            ChangePrintQty();
        }

        protected override void ActConfirm()
        {
            var list = ModelBindingSource.List as List<TN_PUR1201>;
            if (list == null) return;

            var updateObj = list.Where(p => !p.InLotNo.IsNullOrEmpty()).First();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, updateObj);
            param.SetValue(PopupParameter.ReturnObject, list);
            ReturnPopupArgument = new PopupArgument(param);

            ModelService.Dispose();

            SetIsFormControlChanged(false);
            ActClose();
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            var dtlobj = ModelBindingSource.Current as TN_PUR1201;
            if (e.Column.FieldName == "WhCode")
            {
                //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == dtlobj.WhCode).ToList(), "PosionCode", "PosionName");
            }
            else if (e.Column.FieldName == "WhPosition")
            {
                //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
            }
            else if (e.Column.FieldName == "PrintQty")
            {
                var dtlList = ModelBindingSource.List as List<TN_PUR1201>;
                var inQty = tx_InQty.EditValue.GetDecimalNullToZero();
                var sumLqty = dtlList.Sum(p => p.PrintQty).GetDecimalNullToZero();
                if (sumLqty != 0)
                {
                    var oneQty = inQty / sumLqty;
                    foreach (var v in dtlList)
                    {
                        v.InQty = oneQty * v.PrintQty.GetDecimalNullToZero();
                    }
                }
                else
                {
                    dtlobj.InQty = inQty;
                }
                GridExControl.MainGrid.MainView.RefreshData();
            }
            else if (e.Column.FieldName == "InCost")
            {
                var dtlList = ModelBindingSource.List as List<TN_PUR1201>;
                foreach (var v in dtlList)
                {
                    v.InCost = dtlobj.InCost.GetDecimalNullToZero();
                }
                GridExControl.MainGrid.MainView.RefreshData();
            }
        }

        private void ChangePrintQty()
        {
            var dtlobj = ModelBindingSource.Current as TN_PUR1201;
            var dtlList = ModelBindingSource.List as List<TN_PUR1201>;
            var inQty = tx_InQty.EditValue.GetDecimalNullToZero();
            var sumLqty = dtlList.Sum(p => p.PrintQty).GetDecimalNullToZero();
            if (sumLqty != 0)
            {
                var oneQty = inQty / sumLqty;
                foreach (var v in dtlList)
                {
                    v.InQty = oneQty * v.PrintQty.GetDecimalNullToZero();
                }
            }
            else
            {
                dtlobj.InQty = inQty;
            }
            GridExControl.MainGrid.MainView.RefreshData();
        }
    }
}