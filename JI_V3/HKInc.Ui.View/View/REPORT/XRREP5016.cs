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
using HKInc.Ui.Model.Domain;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Helper;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;
using HKInc.Service.Controls;
using DevExpress.XtraEditors.Controls;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 초중종 검사 체크시트
    /// </summary>
    public partial class XRREP5016 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1101> ModelService = (IService<TN_QCT1101>)ProductionFactory.GetDomainService("TN_QCT1101");

        public XRREP5016()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView = new BandedGridView();
            GridExControl.MainGrid.ViewType = GridViewType.BandedGridView;
            ((BandedGridView)GridExControl.MainGrid.MainView).InitEx();
            GridExControl.MainGrid.MainView.OptionsView.ShowIndicator = false;
            GridExControl.MainGrid.MainView.OptionsView.AllowCellMerge = true;
            GridExControl.MainGrid.MainView.CellMerge += GridView_CellMerge;    // 2022-10-18 김진우 추가

            dtEdit1.DateTime = DateTime.Today;
        }

        protected override void InitCombo()
        {
            lup_MachineCode.SetDefault(true, "MachineCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
            lup_ItemCode.SetDefault(true, "ItemCode", "ItemName", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("MachineCode");             // 설비코드
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCode"));                // 업체명
            GridExControl.MainGrid.AddColumn("CarType");                        // 차종
            GridExControl.MainGrid.AddColumn("ItemCode");                       // 품번
            GridExControl.MainGrid.AddColumn("ItemName");                       // 품명
            GridExControl.MainGrid.AddColumn("ProcessCode");             // 공정
            GridExControl.MainGrid.AddColumn("FmeDivision", LabelConvert.GetLabelText("FME"));              // 초중종
            GridExControl.MainGrid.AddColumn("SpcFlag", LabelConvert.GetLabelText("SpecialChar"));      // 특별특성
            GridExControl.MainGrid.AddColumn("CheckList");
            GridExControl.MainGrid.AddColumn("CheckSpec");
            GridExControl.MainGrid.AddColumn("CheckWay");
            GridExControl.MainGrid.AddColumn("CheckMax");
            GridExControl.MainGrid.AddColumn("CheckMin");
            GridExControl.MainGrid.AddColumn("Info", LabelConvert.GetLabelText("Division"));

            GridExControl.MainGrid.MainView.SetGridBandAddedEx("MachineCode", "MachineCode", "MachineCode");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CustomerCode", "CustomerCode", "CustomerCode");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CarType", "CarType", "CarType");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("ItemCode", "ItemCode", "ItemCode");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("ItemName", "ItemName", "ItemName");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("ProcessCode", "ProcessCode", "ProcessCode");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("FmeDivision", "FmeDivision", "FmeDivision");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("SpcFlag", "SpcFlag", "SpcFlag");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CheckList", "CheckList", "CheckList");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CheckSpec", "CheckSpec", "CheckSpec");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CheckWay", "CheckWay", "CheckWay");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CheckMax", "CheckMax", "CheckMax");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CheckMin", "CheckMin", "CheckMin");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("Info", "Info", "Info");

            GridExControl.MainGrid.MainView.Columns["MachineCode"].OptionsColumn.AllowMerge = DefaultBoolean.True;
            GridExControl.MainGrid.MainView.Columns["CustomerCode"].OptionsColumn.AllowMerge = DefaultBoolean.True;
            GridExControl.MainGrid.MainView.Columns["CarType"].OptionsColumn.AllowMerge = DefaultBoolean.True;
            GridExControl.MainGrid.MainView.Columns["ItemCode"].OptionsColumn.AllowMerge = DefaultBoolean.True;
            GridExControl.MainGrid.MainView.Columns["ItemName"].OptionsColumn.AllowMerge = DefaultBoolean.True;
            GridExControl.MainGrid.MainView.Columns["ProcessCode"].OptionsColumn.AllowMerge = DefaultBoolean.True;
            GridExControl.MainGrid.MainView.Columns["FmeDivision"].OptionsColumn.AllowMerge = DefaultBoolean.True;
            GridExControl.MainGrid.MainView.Columns["Info"].OptionsColumn.AllowMerge = DefaultBoolean.True;

            BandedGridViewPainter Painter = new BandedGridViewPainter((BandedGridView)GridExControl.MainGrid.MainView, new GridBand[]
            {
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["MachineCode"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CustomerCode"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CarType"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemCode"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemName"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["ProcessCode"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["FmeDivision"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["SpcFlag"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckList"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckSpec"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckWay"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckMax"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckMin"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["Info"],
            });

            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["MachineCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CustomerCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CarType"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["ProcessCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["FmeDivision"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["SpcFlag"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckList"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckSpec"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckWay"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckMax"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckMin"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["Info"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            List<string> BandList = new List<string>();

            for (int i = 1; i < DateTime.DaysInMonth(dtEdit1.DateTime.Year, dtEdit1.DateTime.Month) + 1; i++)
            {
                BandList.Clear();
                BandList.Add("PROD" + i.ToString("D2"));
                BandList.Add("QUAL" + i.ToString("D2"));

                GridExControl.MainGrid.AddColumn("PROD" + i.ToString("D2"), LabelConvert.GetLabelText("생산"));
                GridExControl.MainGrid.AddColumn("QUAL" + i.ToString("D2"), LabelConvert.GetLabelText("품질"));

                GridExControl.MainGrid.MainView.SetGridBandAddedEx(i.ToString(), i.ToString(), BandList.ToArray());
            }
        }

        // 2022-10-18 김진우 추가
        private void GridView_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView gridview = sender as GridView;

            if (e.Column.FieldName == "ItemCode" || e.Column.FieldName == "MachineCode" || e.Column.FieldName == "CustomerCode" || e.Column.FieldName == "CarType"
                || e.Column.FieldName == "ItemName" || e.Column.FieldName == "ProcessCode")
            {
                var item1 = gridview.GetRowCellValue(e.RowHandle1, gridview.Columns["FmeDivision"]);
                var item2 = gridview.GetRowCellValue(e.RowHandle2, gridview.Columns["FmeDivision"]);

                e.Merge = item1.Equals(item2);
                e.Handled = true;
            }
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemCode", "ItemName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("FmeDivision", DbRequestHandler.GetCommTopCode(MasterCodeSTR.CheckPoint), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("SpcFlag", "N");
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();

            var Date = new SqlParameter("@Date", dtEdit1.DateTime);
            var MachineCode = new SqlParameter("@MachineCode", lup_MachineCode.EditValue.GetNullToEmpty());
            var ItemCode = new SqlParameter("@ItemCode", lup_ItemCode.EditValue.GetNullToEmpty());
            var Customer = new SqlParameter("@Customer", lup_CustomerCode.EditValue.GetNullToEmpty());

            var Data = DbRequestHandler.GetDataSet("USP_GET_XRREP5016", Date, MachineCode, ItemCode, Customer);

            int LastDay = DateTime.DaysInMonth(dtEdit1.DateTime.Year, dtEdit1.DateTime.Month);
            SetColumn(LastDay);

            GridExControl.DataSource = Data.Tables[0];
            GridExControl.BestFitColumns();
            SetRefreshMessage(GridExControl);
        }

        // 2022-10-18 김진우 추가
        private void SetColumn(int LastDay)
        {
            BandedGridView view = GridExControl.MainGrid.MainView as BandedGridView;

            for (int i = 28; i < LastDay + 1; i++)
            {
                var ColumnName_Prod = "PROD" + i.ToString("D2");
                var ColumnName_Qual = "QUAL" + i.ToString("D2");

                view.Columns[ColumnName_Prod].OwnerBand.Visible = true;
                view.Columns[ColumnName_Qual].OwnerBand.Visible = true;
                GridExControl.MainGrid.MainView.Columns[ColumnName_Prod].Visible = true;
                GridExControl.MainGrid.MainView.Columns[ColumnName_Qual].Visible = true;
            }

            for (int i = LastDay + 1; i < 32; i++)
            {
                var ColumnName_Prod = "PROD" + i.ToString("D2");
                var ColumnName_Qual = "QUAL" + i.ToString("D2");

                view.Columns[ColumnName_Prod].OwnerBand.Visible = false;
                view.Columns[ColumnName_Qual].OwnerBand.Visible = false;
                GridExControl.MainGrid.MainView.Columns[ColumnName_Prod].Visible = false;
                GridExControl.MainGrid.MainView.Columns[ColumnName_Qual].Visible = false;
            }
        }

    }
}