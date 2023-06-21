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
using HKInc.Service.Helper;
using DevExpress.Utils;
using HKInc.Utils.Class;

namespace HKInc.Ui.View.View.WMS
{
    /// <summary>
    /// 위치별 재고관리
    /// </summary>
    public partial class XFWMS2000 : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_WMS2000> ModelService = (IService<TN_WMS2000>)ProductionFactory.GetDomainService("TN_WMS2000");

        public XFWMS2000()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
        }

        protected override void InitCombo()
        {
            lup_WhCode.SetDefault(true, "WhCode", DataConvert.GetCultureDataFieldName("WhName"), ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList());
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("WhCode", LabelConvert.GetLabelText("WhName"));
            MasterGridExControl.MainGrid.AddColumn("PositionCode", LabelConvert.GetLabelText("WhPosition"));

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("WhCode", LabelConvert.GetLabelText("WhName"));
            DetailGridExControl.MainGrid.AddColumn("PositionCode", LabelConvert.GetLabelText("WhPosition"));
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("LotNo", LabelConvert.GetLabelText("LotNo"));
            DetailGridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");

            SubDetailGridExControl.SetToolbarVisible(false);
            SubDetailGridExControl.MainGrid.AddColumn("WhCode", LabelConvert.GetLabelText("WhName"));
            SubDetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            SubDetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            SubDetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName") , LabelConvert.GetLabelText("ItemName"));
            SubDetailGridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
        }

        protected override void InitRepository()
        {
            var TN_WMS1000List = ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList();
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", TN_WMS1000List, "WhCode", DataConvert.GetCultureDataFieldName("WhName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", TN_WMS1000List, "WhCode", DataConvert.GetCultureDataFieldName("WhName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", TN_WMS1000List, "WhCode", DataConvert.GetCultureDataFieldName("WhName"));
        }

        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var whCode = lup_WhCode.EditValue.GetNullToEmpty();
            var itemCode = lup_Item.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => string.IsNullOrEmpty(whCode) ? true : p.WhCode == whCode)
                                                             .OrderBy(p => p.PositionCode)
                                                             .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            SubDetailGridBindingSource.DataSource = ModelService.GetChildList<VI_WH_STOCK_QTY>(p => (string.IsNullOrEmpty(whCode) ? true : p.WhCode == whCode)
                                                                                                     && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                                                   )
                                                                                                   .OrderBy(o => o.WhCode)
                                                                                                   .ThenBy(o => o.ItemCode)
                                                                                                   .ToList();
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_WMS2000;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_WH_POSITON_QTY>(p => p.WhCode == masterObj.WhCode && p.PositionCode == masterObj.PositionCode).OrderBy(o => o.ItemCode).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }
    }
}