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
using DevExpress.XtraGrid.Columns;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 초중종 검사 체크시트
    /// </summary>
    public partial class XRREP5016_V2 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1101> ModelService = (IService<TN_QCT1101>)ProductionFactory.GetDomainService("TN_QCT1101");

        public XRREP5016_V2()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView = new BandedGridView();
            GridExControl.MainGrid.ViewType = GridViewType.BandedGridView;
            ((BandedGridView)GridExControl.MainGrid.MainView).InitEx();
            GridExControl.MainGrid.MainView.OptionsView.ShowIndicator = false;

            GridExControl.MainGrid.MainView.OptionsView.AllowCellMerge = true;
            GridExControl.MainGrid.MainView.CellMerge += MainView_CellMerge;
            dtEdit1.SetFormat(DateFormat.Month);
            dtEdit1.DateTime = DateTime.Today;
        }

        private void MainView_CellMerge(object sender, CellMergeEventArgs e)
        {
            #region 초중종

            if (e.Column.FieldName == "FmeDivisionDisplay")
            {
                string ivalue = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle1, "ItemCode").GetNullToEmpty();
                string ivalue2 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle2, "ItemCode").GetNullToEmpty();
                string ivalue3 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle1, "MachineCode").GetNullToEmpty();
                string ivalue4 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle2, "MachineCode").GetNullToEmpty();
                string ivalue5 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle1, "ProcessCodeDisplay").GetNullToEmpty();
                string ivalue6 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle2, "ProcessCodeDisplay").GetNullToEmpty();
                string ivalue7 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle1, "FmeDivisionDisplay").GetNullToEmpty();
                string ivalue8 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle2, "FmeDivisionDisplay").GetNullToEmpty();

                if (ivalue == ivalue2)
                {
                    if (ivalue3 == ivalue4)
                    {
                        if (ivalue5 == ivalue6)
                    {
                        e.Merge = (ivalue7 == ivalue8);
                        e.Handled = true;
                    }
                    else
                    {
                        e.Merge = false;
                        e.Handled = true;
                    }
                    }
                    else
                    {
                        e.Merge = false;
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Merge = false;
                    e.Handled = true;
                }
            }

            #endregion
            #region 공정
            if (e.Column.FieldName == "ProcessCodeDisplay")
            {
                string ivalue = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle1, "ItemCode").GetNullToEmpty();
                string ivalue2 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle2, "ItemCode").GetNullToEmpty();
                string ivalue3 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle1, "MachineCode").GetNullToEmpty();
                string ivalue4 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle2, "MachineCode").GetNullToEmpty();

                string ivalue5 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle1, "ProcessCodeDisplay").GetNullToEmpty();
                string ivalue6 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle2, "ProcessCodeDisplay").GetNullToEmpty();
              

                if (ivalue == ivalue2)
                {
                    if (ivalue3 == ivalue4)
                    {
                        if (ivalue5 == ivalue6)
                        {
                            e.Merge = true;
                            e.Handled = true;
                        }
                        else
                        {
                            e.Merge = false;
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        e.Merge = false;
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Merge = false;
                    e.Handled = true;
                }
            }

            #endregion
            #region 설비
            if (e.Column.FieldName == "MachineCode")
            {
                string ivalue = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle1, "ItemCode").GetNullToEmpty();
                string ivalue2 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle2, "ItemCode").GetNullToEmpty();
                string ivalue3 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle1, "MachineCode").GetNullToEmpty();
                string ivalue4 = GridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle2, "MachineCode").GetNullToEmpty();

              

                if (ivalue == ivalue2)
                {
                    if (ivalue3 == ivalue4)
                    {
                        
                            e.Merge = true;
                            e.Handled = true;
                       
                    }
                    else
                    {
                        e.Merge = false;
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Merge = false;
                    e.Handled = true;
                }
            }

            #endregion
        }

        protected override void InitCombo()
        {
            lup_MachineCode.SetDefault(true, "MachineCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
            lup_ItemCode.SetDefault(true, "ItemCode", "ItemName", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_process.SetDefault(true, "CodeVal",  Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process));
        }

        protected override void InitGrid()
        {
            SetColumn();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemCode", "ItemName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => 1==1).ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCodeDisplay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("FmeDivisionDisplay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.CheckPoint), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
         //   GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("SpcFlag", "N");
        }

        protected override void DataLoad()
        {
           // ((BandedGridView)GridExControl.MainGrid.MainView).BeginUpdate();
      //      ((BandedGridView)GridExControl.MainGrid.MainView).Bands.Clear();
            //((BandedGridView)GridExControl.MainGrid.MainView).Columns.Clear();
            //((BandedGridView)GridExControl.MainGrid.MainView).EndUpdate();

            ////string Machine = lup_MachineCode.EditValue.GetNullToEmpty();
            ////if (Machine == "")
            ////{
            ////    MessageBoxHandler.Show("설비를 선택하여 주십시오.");
            ////    return;
            ////}

            //InitGrid();
            //InitRepository();
            ModelService.ReLoad();

            GridExControl.DataSource = null;
            //var Date = new SqlParameter("@Date", dtEdit1.DateTime.ToString("yyyy-MM"));
            //var MachineCode = new SqlParameter("@MachineCode", lup_MachineCode.EditValue.GetNullToEmpty());
            //var ItemCode = new SqlParameter("@ItemCode", lup_ItemCode.EditValue.GetNullToEmpty());
            //var Customer = new SqlParameter("@Customer", lup_CustomerCode.EditValue.GetNullToEmpty());

            //var Data = DbRequestHandler.GetDataSet("USP_GET_XRREP5016_V2", Date, MachineCode, ItemCode, Customer);
            setgrid();
            //SetColumn();
            var Date = new SqlParameter("@date", dtEdit1.DateTime.ToString("yyyy-MM"));
            var MachineCode = new SqlParameter("@mc", lup_MachineCode.EditValue.GetNullToEmpty());
            var ItemCode = new SqlParameter("@item", lup_ItemCode.EditValue.GetNullToEmpty());
            var Customer = new SqlParameter("@cust", lup_CustomerCode.EditValue.GetNullToEmpty());
            var process = new SqlParameter("@proc", lup_process.EditValue.GetNullToEmpty());
                //lup_CustomerCode.EditValue.GetNullToEmpty());

            var Data = DbRequestHandler.GetDataSet("sp_qcreport", Date,  ItemCode, MachineCode, Customer,process);
            GridExControl.DataSource = Data.Tables[0];
            GridExControl.BestFitColumns();
            SetRefreshMessage(GridExControl);
        }
        private void setgrid()
        {
            BandedGridView gv = GridExControl.MainGrid.MainView as BandedGridView;

            int lastday = DateTime.DaysInMonth(dtEdit1.DateTime.Year, dtEdit1.DateTime.Month);
            switch (lastday)
            {
                case 28:
                    gv.Bands[69].Visible = false;                   
                    gv.Bands[71].Visible = false;                   
                    gv.Bands[73].Visible = false;
                   
                    break;
                case 29:
                    gv.Bands[69].Visible = true;
                    gv.Bands[71].Visible = false;
                    gv.Bands[73].Visible = false;
                    break;
                case 30:
                    gv.Bands[69].Visible = true;
                    gv.Bands[71].Visible = true;
                    gv.Bands[73].Visible = false;

                    break;
                case 31:
                    gv.Bands[69].Visible = true;
                    gv.Bands[71].Visible = true;
                    gv.Bands[73].Visible = true;
                    break;
                    //  if()
            }

        }
        private void SetColumn()
        {
            //GridExControl.MainGrid.AddColumn("InspNo", false);
            //GridExControl.MainGrid.AddColumn("ProductLotNo", false);

            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));                // 업체명
            GridExControl.MainGrid.AddColumn("CarType");                        // 차종
            GridExControl.MainGrid.AddColumn("ItemCode");                       // 품번
            GridExControl.MainGrid.AddColumn("ItemName");                       // 품명
                                                                                //GridExControl.MainGrid.AddColumn("ProcessCode", false);             // 공정
            GridExControl.MainGrid.AddColumn("MachineCode");             // 설비코드
            GridExControl.MainGrid.AddColumn("ProcessCodeDisplay", LabelConvert.GetLabelText("ProcessCode"));             // 공정
            //GridExControl.MainGrid.AddColumn("FmeDivision", false);              // 구분
            GridExControl.MainGrid.AddColumn("FmeDivisionDisplay", LabelConvert.GetLabelText("FME"));      // 초중종
            GridExControl.MainGrid.AddColumn("SpcFlag", LabelConvert.GetLabelText("SpecialChar"));      // 특별특성
            GridExControl.MainGrid.AddColumn("CheckList");                       
            GridExControl.MainGrid.AddColumn("CheckSpec");
          //  GridExControl.MainGrid.AddColumn("CheckWay");
            GridExControl.MainGrid.AddColumn("CheckMax");
            GridExControl.MainGrid.AddColumn("CheckMin");
            //GridExControl.MainGrid.AddColumn("Judege",false);
            //GridExControl.MainGrid.AddColumn("WorkNo",false);
            GridExControl.MainGrid.AddColumn("Info", LabelConvert.GetLabelText("Division"));
     
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CustomerCode", "CustomerCode", "CustomerCode");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CarType", "CarType", "CarType");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("ItemCode", "ItemCode", "ItemCode");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("ItemName", "ItemName", "ItemName");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("MachineCode", "MachineCode", "MachineCode");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("ProcessCodeDisplay", "ProcessCodeDisplay", "ProcessCodeDisplay");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("FmeDivisionDisplay", "FmeDivisionDisplay", "FmeDivisionDisplay");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("SpcFlag", "SpcFlag", "SpcFlag");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CheckList", "CheckList", "CheckList");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CheckSpec", "CheckSpec", "CheckSpec");
          //  GridExControl.MainGrid.MainView.SetGridBandAddedEx("CheckWay", "CheckWay", "CheckWay");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CheckMax", "CheckMax", "CheckMax");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CheckMin", "CheckMin", "CheckMin");
            //GridExControl.MainGrid.MainView.SetGridBandAddedEx("WorkNo", "WorkNo", "WorkNo");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("Info", "Info", "Info");





        GridExControl.MainGrid.MainView.Columns["MachineCode"].OptionsColumn.AllowMerge = DefaultBoolean.True;
        GridExControl.MainGrid.MainView.Columns["CustomerCode"].OptionsColumn.AllowMerge = DefaultBoolean.True;
        GridExControl.MainGrid.MainView.Columns["CarType"].OptionsColumn.AllowMerge = DefaultBoolean.True;
        GridExControl.MainGrid.MainView.Columns["ItemCode"].OptionsColumn.AllowMerge = DefaultBoolean.True;
        GridExControl.MainGrid.MainView.Columns["ItemName"].OptionsColumn.AllowMerge = DefaultBoolean.True;
        GridExControl.MainGrid.MainView.Columns["ProcessCodeDisplay"].OptionsColumn.AllowMerge = DefaultBoolean.True;
        GridExControl.MainGrid.MainView.Columns["FmeDivisionDisplay"].OptionsColumn.AllowMerge = DefaultBoolean.True;
        GridExControl.MainGrid.MainView.Columns["Info"].OptionsColumn.AllowMerge = DefaultBoolean.True;

        List<string> BandList = new List<string>();
            //dtEdit1.DateTime.Year
            //var 
            for (int i = 1; i < DateTime.DaysInMonth(dtEdit1.DateTime.Year, dtEdit1.DateTime.Month) + 1; i++)
            {
                BandList.Clear();
                BandList.Add("PROD_" + i);
                BandList.Add("QUAL_" + i);

                GridExControl.MainGrid.AddColumn("PROD_" + i, LabelConvert.GetLabelText("생산"));
                GridExControl.MainGrid.AddColumn("QUAL_" + i, LabelConvert.GetLabelText("품질"));

                GridExControl.MainGrid.MainView.SetGridBandAddedEx(i.ToString(), i.ToString(), BandList.ToArray());
            }

            BandedGridViewPainter Painter = new BandedGridViewPainter((BandedGridView)GridExControl.MainGrid.MainView, new GridBand[]
            {
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CustomerCode"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CarType"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemCode"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemName"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["MachineCode"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["ProcessCodeDisplay"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["FmeDivisionDisplay"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["SpcFlag"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckList"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckSpec"],
            //    (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckWay"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckMax"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckMin"],
                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["Info"],
            });
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CustomerCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CarType"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["MachineCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["ProcessCodeDisplay"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["FmeDivisionDisplay"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["SpcFlag"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckList"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckSpec"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckMax"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CheckMin"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["Info"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

        }

    }
}