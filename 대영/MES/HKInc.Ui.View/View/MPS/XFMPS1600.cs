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
using HKInc.Utils.Enum;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 자재재공재고
    /// </summary>
    public partial class XFMPS1600 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_SRC_STOCK_SUM> ModelService = (IService<VI_SRC_STOCK_SUM>)ProductionFactory.GetDomainService("VI_SRC_STOCK_SUM");

        public XFMPS1600()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
        }

        private void MainView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                VI_SRC_STOCK obj = DetailGridBindingSource.Current as VI_SRC_STOCK;
                if (obj == null) return;
                if (!obj.Memo.Contains("재고조정")) return;
                
                    MPS_POPUP.XPFMPS1600 fm = new MPS_POPUP.XPFMPS1600(obj);
                    fm.ShowDialog();
                    DataLoad();
                
            }
        }

        protected override void InitCombo()
        {
            //lupitemcode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());
            lupitemcode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(x => x.UseFlag == "Y" && (x.TopCategory == MasterCodeSTR.TopCategory_MAT || x.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("SrcItemCode", LabelConvert.GetLabelText("ItemCode"));
            //MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemName", LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName")); // 20210824 오세완 차장 다국어 변환으로 변경
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemName1", LabelConvert.GetLabelText("ItemName1")); // 20210824 오세완 차장 품목명 추가
            MasterGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("UseQty", LabelConvert.GetLabelText("UseQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.BestFitColumns();

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("재공재고조정") + "[F3]", IconImageList.GetIconImage("edit/customization"));
            DetailGridExControl.MainGrid.AddColumn("ResultDate", LabelConvert.GetLabelText("Date"));
            DetailGridExControl.MainGrid.AddColumn("SrcLotNo", LabelConvert.GetLabelText("LotNo"));
            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("UseQty", LabelConvert.GetLabelText("UseQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            //DetailGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
        }

        protected override void InitRepository()
        {
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", "MachineName", true);
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();
            // 20210624 오세완 차장 김이사님 지시로 공통코드 추가시 화면 재출력 없이 하기 위해서 추가
            InitCombo();

            string itemcode = lupitemcode.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => string.IsNullOrEmpty(itemcode) ? true : p.SrcItemCode == itemcode)
                                                                     .OrderBy(p => p.TN_STD1100.ItemName)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            var obj = MasterGridBindingSource.Current as VI_SRC_STOCK_SUM;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_SRC_STOCK>(p => p.SrcItemCode == obj.SrcItemCode
                                                                                      )
                                                                                      .OrderBy(o => o.ResultDate)
                                                                                      .ThenBy(o => o.SrcLotNo)
                                                                                      .ThenBy(o => o.RowIndex)
                                                                                      .ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        /// <summary>
        /// 재공재고조정
        /// </summary>
        protected override void DetailAddRowClicked()
        {
            VI_SRC_STOCK_SUM obj = MasterGridBindingSource.Current as VI_SRC_STOCK_SUM;
            if (obj == null) return;

            MPS_POPUP.XPFMPS1600 fm = new MPS_POPUP.XPFMPS1600(obj);
            fm.ShowDialog();
            DataLoad();
        }
    }
}
