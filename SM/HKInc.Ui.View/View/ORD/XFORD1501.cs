using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;
using HKInc.Ui.Model.BaseDomain;
using HKInc.Service.Helper;
using System.Collections.Generic;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 마감처리관리
    /// </summary>
    public partial class XFORD1501 : Service.Base.SixGridFormTemplate
    {
        //제품탭 모델서비스
        IService<TN_PROD_DEAD_MST> ModelService_PROD = (IService<TN_PROD_DEAD_MST>)ProductionFactory.GetDomainService("TN_PROD_DEAD_MST");

        //반제품탭 모델서비스
        IService<TN_BAN_DEAD_MST> ModelService_BAN = (IService<TN_BAN_DEAD_MST>)ProductionFactory.GetDomainService("TN_BAN_DEAD_MST");

        //자재탭 모델서비스
        IService<TN_MAT_DEAD_MST> ModelService_MAT = (IService<TN_MAT_DEAD_MST>)ProductionFactory.GetDomainService("TN_MAT_DEAD_MST");

        public XFORD1501()
        {
            InitializeComponent();

            OneGridExControl = gridEx1;
            TwoGridExControl = gridEx2;

            ThreeGridExControl = gridEx3;
            FourGridExControl = gridEx4;

            FiveGridExControl = gridEx5;
            SixGridExControl = gridEx6;

            TwoGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            TwoGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;

            FourGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            FourGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;

            SixGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            SixGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;

            xtraTabControl1.SelectedPageChanged += XtraTabControl1_SelectedPageChanged;
        }
        
        protected override void InitCombo()
        {
            lcProdDead.Text = LabelConvert.GetLabelText("Prod");
            lcBanDead.Text = LabelConvert.GetLabelText("Ban");
            lcMatDead.Text = LabelConvert.GetLabelText("Mat");
            
            lcProdDeadLine.Text = LabelConvert.GetLabelText("ProdDeadLine");
            lcProdDeadLineStockList.Text = LabelConvert.GetLabelText("ProdDeadLineStockList");

            lcBanDeadLine.Text = LabelConvert.GetLabelText("BanDeadLine");
            lcBanDeadLineStockList.Text = LabelConvert.GetLabelText("BanDeadLineStockList");

            lcMatDeadLine.Text = LabelConvert.GetLabelText("MatDeadLine");
            lcMatDeadLineStockList.Text = LabelConvert.GetLabelText("MatDeadLineStockList");
            
            lcProdDead.Appearance.HeaderActive.BackColor = System.Drawing.Color.White;
            lcBanDead.Appearance.HeaderActive.BackColor = System.Drawing.Color.White;
            lcMatDead.Appearance.HeaderActive.BackColor = System.Drawing.Color.White;

            lcProdDead.Appearance.HeaderActive.Font = new System.Drawing.Font(lcProdDead.Appearance.Header.Font, System.Drawing.FontStyle.Bold);
            lcBanDead.Appearance.HeaderActive.Font = new System.Drawing.Font(lcProdDead.Appearance.Header.Font, System.Drawing.FontStyle.Bold);
            lcMatDead.Appearance.HeaderActive.Font = new System.Drawing.Font(lcMatDead.Appearance.Header.Font, System.Drawing.FontStyle.Bold);

            dt_Year.SetFormat(DateFormat.Year, true);
            dt_Year.DateTime = DateTime.Today;
        }

        protected override void InitGrid()
        {
            //완제품 마스터
            OneGridExControl.SetToolbarVisible(false);
            OneGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"));
            OneGridExControl.MainGrid.AddColumn("DeadLineDate", LabelConvert.GetLabelText("DeadLineMonth"));
            OneGridExControl.MainGrid.AddColumn("RealDeadLineDate", LabelConvert.GetLabelText("DeadLineDate"));
            OneGridExControl.MainGrid.AddColumn("DeadLineId", LabelConvert.GetLabelText("DeadLineId"), false);

            //완제품 디테일(재고이월데이터)
            TwoGridExControl.SetToolbarVisible(false);
            TwoGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            TwoGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            TwoGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            TwoGridExControl.MainGrid.AddColumn("CarryOverQty", LabelConvert.GetLabelText("CarryOverQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            TwoGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("ResultQtyTwo"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            TwoGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            TwoGridExControl.MainGrid.AddColumn("AdjustQty", LabelConvert.GetLabelText("AdjustQty"));
            TwoGridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            TwoGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            TwoGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "AdjustQty", "Memo");

            //반제품 마스터
            ThreeGridExControl.SetToolbarVisible(false);
            ThreeGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"));
            ThreeGridExControl.MainGrid.AddColumn("DeadLineDate", LabelConvert.GetLabelText("DeadLineMonth"));
            ThreeGridExControl.MainGrid.AddColumn("RealDeadLineDate", LabelConvert.GetLabelText("DeadLineDate"));
            ThreeGridExControl.MainGrid.AddColumn("DeadLineId", LabelConvert.GetLabelText("DeadLineId"), false);

            //반제품 디테일(재고이월데이터)
            FourGridExControl.SetToolbarVisible(false);
            FourGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            FourGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            FourGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            FourGridExControl.MainGrid.AddColumn("CarryOverQty", LabelConvert.GetLabelText("CarryOverQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            FourGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("ResultQtyTwo"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            FourGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            FourGridExControl.MainGrid.AddColumn("AdjustQty", LabelConvert.GetLabelText("AdjustQty"));
            FourGridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            FourGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            FourGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "AdjustQty", "Memo");

            //자재 마스터
            FiveGridExControl.SetToolbarVisible(false);
            FiveGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"));
            FiveGridExControl.MainGrid.AddColumn("DeadLineDate", LabelConvert.GetLabelText("DeadLineMonth"));
            FiveGridExControl.MainGrid.AddColumn("RealDeadLineDate", LabelConvert.GetLabelText("DeadLineDate"));
            FiveGridExControl.MainGrid.AddColumn("DeadLineId", LabelConvert.GetLabelText("DeadLineId"), false);

            //자재 디테일(재고이월데이터)
            SixGridExControl.SetToolbarVisible(false);
            SixGridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            SixGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            SixGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            SixGridExControl.MainGrid.AddColumn("CarryOverQty", LabelConvert.GetLabelText("CarryOverQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            SixGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("ResultQtyTwo"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            SixGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            SixGridExControl.MainGrid.AddColumn("AdjustQty", LabelConvert.GetLabelText("AdjustQty"));
            SixGridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            SixGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            SixGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "AdjustQty", "Memo");
        }

        protected override void InitRepository()
        {
            var userList = ModelService_PROD.GetChildList<User>(p => p.Active == "Y").ToList();
            var deadLineDivision = DbRequestHandler.GetCommTopCode(MasterCodeSTR.DeadLineDivision);

            OneGridExControl.MainGrid.SetRepositoryItemDateEdit("DeadLineDate", DateFormat.Month);
            OneGridExControl.MainGrid.SetRepositoryItemDateEdit("RealDeadLineDate");
            OneGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", deadLineDivision, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            OneGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DeadLineId", userList, "LoginId", "UserName");

            TwoGridExControl.MainGrid.SetRepositoryItemSpinEdit("AdjustQty", DefaultBoolean.Default, "#,###,###,###,##0.##");

            ThreeGridExControl.MainGrid.SetRepositoryItemDateEdit("DeadLineDate", DateFormat.Month);
            ThreeGridExControl.MainGrid.SetRepositoryItemDateEdit("RealDeadLineDate");
            ThreeGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", deadLineDivision, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            ThreeGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DeadLineId", userList, "LoginId", "UserName");

            FourGridExControl.MainGrid.SetRepositoryItemSpinEdit("AdjustQty", DefaultBoolean.Default, "#,###,###,###,##0.##");

            FiveGridExControl.MainGrid.SetRepositoryItemDateEdit("DeadLineDate", DateFormat.Month);
            FiveGridExControl.MainGrid.SetRepositoryItemDateEdit("RealDeadLineDate");
            FiveGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", deadLineDivision, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            FiveGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DeadLineId", userList, "LoginId", "UserName");

            SixGridExControl.MainGrid.SetRepositoryItemSpinEdit("AdjustQty", DefaultBoolean.Default, "#,###,###,###,##0.##");

            OneGridExControl.BestFitColumns();
            TwoGridExControl.BestFitColumns();
            ThreeGridExControl.BestFitColumns();
            FourGridExControl.BestFitColumns();
            FiveGridExControl.BestFitColumns();
            SixGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            var selectedPageName = xtraTabControl1.SelectedTabPage.Name;
            var yearDate = dt_Year.DateTime.GetNullToDateTime();
            
            if (selectedPageName == "lcProdDead") //완제품 탭
            {
                OneGridRowLocator.GetCurrentRow();

                OneGridExControl.MainGrid.Clear();
                TwoGridExControl.MainGrid.Clear();

                ModelService_PROD.ReLoad();

                OneGridBindingSource.DataSource = ModelService_PROD.GetList(p => (yearDate == null ? true : p.DeadLineDate.Year == ((DateTime)yearDate).Year)
                                                                              //&& (p.Division != null && p.Division != MasterCodeSTR.DeadLineDivision_DeadCancel)
                                                                           )
                                                                           .OrderBy(p => p.DeadLineDate)
                                                                           .ToList();
                OneGridExControl.DataSource = OneGridBindingSource;
                OneGridExControl.BestFitColumns();
                OneGridRowLocator.SetCurrentRow();
            }
            else if (selectedPageName == "lcBanDead") //반제품 탭
            {
                ThreeGridRowLocator.GetCurrentRow();

                ThreeGridExControl.MainGrid.Clear();
                FourGridExControl.MainGrid.Clear();

                ModelService_BAN.ReLoad();

                ThreeGridBindingSource.DataSource = ModelService_BAN.GetList(p => (yearDate == null ? true : p.DeadLineDate.Year == ((DateTime)yearDate).Year)
                                                                           //&& (p.Division != null && p.Division != MasterCodeSTR.DeadLineDivision_DeadCancel)
                                                                           )
                                                                           .OrderBy(p => p.DeadLineDate)
                                                                           .ToList();

                ThreeGridExControl.DataSource = ThreeGridBindingSource;
                ThreeGridExControl.BestFitColumns();
                ThreeGridRowLocator.SetCurrentRow();
            }
            else if (selectedPageName == "lcMatDead") //자재 탭
            {
                FiveGridRowLocator.GetCurrentRow();

                FiveGridExControl.MainGrid.Clear();
                SixGridExControl.MainGrid.Clear();

                ModelService_MAT.ReLoad();

                FiveGridBindingSource.DataSource = ModelService_MAT.GetList(p => (yearDate == null ? true : p.DeadLineDate.Year == ((DateTime)yearDate).Year)
                                                                           //&& (p.Division != null && p.Division != MasterCodeSTR.DeadLineDivision_DeadCancel)
                                                                           )
                                                                           .OrderBy(p => p.DeadLineDate)
                                                                           .ToList();

                FiveGridExControl.DataSource = FiveGridBindingSource;
                FiveGridExControl.BestFitColumns();
                FiveGridRowLocator.SetCurrentRow();
            }
        }

        /// <summary> 완제품 행 변경 시 </summary>
        protected override void OneViewFocusedRowChanged()
        {
            var masterObj = OneGridBindingSource.Current as TN_PROD_DEAD_MST;
            if (masterObj == null)
            {
                TwoGridExControl.MainGrid.Clear();
                return;
            }

            TwoGridBindingSource.DataSource = masterObj.TN_PROD_DEAD_DTL_List.OrderBy(p => p.ItemCode).ThenBy(p => p.ProductLotNo).ToList();
            TwoGridExControl.DataSource = TwoGridBindingSource;     
            TwoGridExControl.BestFitColumns();
        }

        /// <summary> 반제품 행 변경 시 </summary>
        protected override void ThreeViewFocusedRowChanged()
        {
            var masterObj = ThreeGridBindingSource.Current as TN_BAN_DEAD_MST;
            if (masterObj == null)
            {
                FourGridExControl.MainGrid.Clear();
                return;
            }

            FourGridBindingSource.DataSource = masterObj.TN_BAN_DEAD_DTL_List.OrderBy(p => p.ItemCode).ThenBy(p => p.ProductLotNo).ToList();
            FourGridExControl.DataSource = FourGridBindingSource;
            FourGridExControl.BestFitColumns();
        }

        /// <summary> 자재 행 변경 시 </summary>
        protected override void FiveViewFocusedRowChanged()
        {
            var masterObj = FiveGridBindingSource.Current as TN_MAT_DEAD_MST;
            if (masterObj == null)
            {
                SixGridExControl.MainGrid.Clear();
                return;
            }

            SixGridBindingSource.DataSource = masterObj.TN_MAT_DEAD_DTL_List.OrderBy(p => p.ItemCode).ThenBy(p => p.InLotNo).ToList();
            SixGridExControl.DataSource = SixGridBindingSource;
            SixGridExControl.BestFitColumns();
        }
        
        protected override void DataSave()
        {
            var selectedPageName = xtraTabControl1.SelectedTabPage.Name;

            if (selectedPageName == "lcProdDead")
            {
                TwoGridExControl.MainGrid.PostEditor();
                
                ModelService_PROD.Save();
                DataLoad();
            }
            else if (selectedPageName == "lcBanDead")
            {
                FourGridExControl.MainGrid.PostEditor();

                ModelService_BAN.Save();
                DataLoad();
            }
            else if (selectedPageName == "lcMatDead")
            {
                SixGridExControl.MainGrid.PostEditor();

                ModelService_MAT.Save();
                DataLoad();
            }
        }

        /// <summary> 이월데이터 셀 수정 시 </summary>
        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var view = sender as GridView;
            if (e.Column.FieldName == "AdjustQty")
            {
                var carryOverQty = view.GetFocusedRowCellValue("CarryOverQty").GetDecimalNullToZero();
                var inQty = view.GetFocusedRowCellValue("InQty").GetDecimalNullToZero();
                var outQty = view.GetFocusedRowCellValue("OutQty").GetDecimalNullToZero();
                var adjustQty = e.Value.GetDecimalNullToZero();
                var stockQty = carryOverQty + inQty - outQty + adjustQty;
                view.SetFocusedRowCellValue("StockQty", stockQty);
            }
        }

        /// <summary> 셀 수정 가능여부 </summary>
        private void MainView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var view = sender as GridView;
            var selectedPageName = xtraTabControl1.SelectedTabPage.Name;
            
            if (selectedPageName == "lcProdDead")
            {
                var masterObj = OneGridBindingSource.Current as TN_PROD_DEAD_MST;
                if (masterObj == null)
                    return;
                if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirm) //마감확정일 경우 수정 불가
                    e.Cancel = true;
            }
            else if (selectedPageName == "lcBanDead")
            {
                var masterObj = ThreeGridBindingSource.Current as TN_BAN_DEAD_MST;
                if (masterObj == null)
                    return;
                if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirm) //마감확정일 경우 수정 불가
                    e.Cancel = true;
            }
            else if (selectedPageName == "lcMatDead")
            {
                var masterObj = FiveGridBindingSource.Current as TN_MAT_DEAD_MST;
                if (masterObj == null)
                    return;
                if (masterObj.Division == MasterCodeSTR.DeadLineDivision_DeadConfirm) //마감확정일 경우 수정 불가
                    e.Cancel = true;
            }
        }

        private void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            var selectedPageName = xtraTabControl1.SelectedTabPage.Name;
            if (selectedPageName == "lcProdDead")
            {

            }
            else if (selectedPageName == "lcBanDead")
            {

            }
            else if (selectedPageName == "lcMatDead")
            {

            }
        }

    }
}
