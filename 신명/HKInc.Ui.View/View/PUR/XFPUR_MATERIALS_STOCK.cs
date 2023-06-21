using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.PUR
{
    /// <summary>
    /// 자재재고관리
    /// </summary>
    public partial class XFPUR_MATERIALS_STOCK : HKInc.Service.Base.ListMasterDetailFormTemplate
    {        
        IService<VI_PUR_STOCK_ITEM> ModelService = (IService<VI_PUR_STOCK_ITEM>)ProductionFactory.GetDomainService("VI_PUR_STOCK_ITEM");
        private string searchDivision;
        private DateTime? searchYearMonthDate;
        private string searchItemCode;
        public XFPUR_MATERIALS_STOCK()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dateEditEx1.SetFormat(Utils.Enum.DateFormat.Month);
            dateEditEx1.DateTime = DateTime.Today;

            DetailGridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;

            DetailGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
        }



        protected override void InitCombo()
        {
            lup_Item.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_MAT || p.TopCategory == MasterCodeSTR.TopCategory_BU || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());            
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("LotNo", LabelConvert.GetLabelText("LotNo"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("Spec", LabelConvert.GetLabelText("Spec"));
            MasterGridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));
                            
            MasterGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            DetailGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("InOutDate", LabelConvert.GetLabelText("InOutDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("InOutLotNo", LabelConvert.GetLabelText("InOutLotNo"));
            DetailGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("InOutId", LabelConvert.GetLabelText("InOutId"));
            DetailGridExControl.MainGrid.AddColumn("InWhCode", LabelConvert.GetLabelText("InWhCode"));
            DetailGridExControl.MainGrid.AddColumn("InWhPosition", LabelConvert.GetLabelText("InWhPosition"));

            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);

            DetailGridExControl.BarTools.ClearLinks();
            /*  자재재고조정 */
            var barButtonDevide = new DevExpress.XtraBars.BarButtonItem();
            barButtonDevide.Id = 5;
            barButtonDevide.ImageOptions.Image = IconImageList.GetIconImage("spreadsheet/showdetail");
            barButtonDevide.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.E));
            barButtonDevide.Name = "barReturn";
            barButtonDevide.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonDevide.ShortcutKeyDisplayString = "Alt+E";
            barButtonDevide.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonDevide.Caption = LabelConvert.GetLabelText("Adjust") + "[Alt+E]";  //재고조정
            barButtonDevide.ItemClick += BarButtonDevide_ItemClick;
            DetailGridExControl.BarTools.AddItem(barButtonDevide);
        }

        protected override void InitRepository()
        {
             MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InOutId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            gridEx1.MainGrid.Clear();
            gridEx2.MainGrid.Clear();

            //MasterGridExControl.MainGrid.Clear();
            //DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            searchItemCode = lup_Item.EditValue.GetNullToEmpty();
            searchDivision = cbo_YearMonth.EditValue.GetNullToEmpty();

            MasterGridExControl.MainGrid.Columns.Clear();
            DetailGridExControl.MainGrid.Columns.Clear();
            InitGrid();
            InitRepository();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var ItemCode = new SqlParameter("@ItemCode", searchItemCode);
                var lotno = new SqlParameter("@Lotno", tx_Lotno.Text);
                DataSet ds = DbRequestHandler.GetDataSet("USP_GET_PUR_STOCK_MASTER_V2", ItemCode, lotno);
                if (ds != null)
                    MasterGridBindingSource.DataSource = ds.Tables[0];
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            int iRowHandle = MasterGridExControl.MainGrid.FocusedRowHandle;
            string pItemCode = string.Empty;
            string pLotno = string.Empty;

            if (iRowHandle >= 0)
            {
                pItemCode = gridEx1.MainGrid.MainView.GetRowCellValue(iRowHandle, "ItemCode").ToString();
                pLotno = gridEx1.MainGrid.MainView.GetRowCellValue(iRowHandle, "LotNo").ToString();
            }
           
                if (pItemCode == null && pLotno == null)
                {
                    DetailGridExControl.MainGrid.Clear();
                    return;
                }
                DateTime ndate = dateEditEx1.DateTime.Date;
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var Date = new SqlParameter("@Date", ndate);
                    var ItemCode = new SqlParameter("@ItemCode", pItemCode);
                    var Lotno = new SqlParameter("@LotNo", pLotno);
                    DataSet ds = DbRequestHandler.GetDataSet("USP_GET_PUR_STOCK_DETAIL_V2", Date, ItemCode, Lotno);
                //DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PUR_STOCK_DETAIL>("USP_GET_PUR_STOCK_DETAIL @Date,@ItemCode,@LotNo", Date, ItemCode,)
                //    .OrderBy(p => p.InOutDate)                        
                //    .OrderBy(p => p.InOutLotNo)
                //    .ThenBy(p=>p.UpdateTime)
                //    .ToList();
                DetailRowChange();
                if (ds != null)
                    DetailGridBindingSource.DataSource = ds.Tables[0];
                    DetailGridExControl.DataSource = ds.Tables[0];
                //DetailGridExControl.BestFitColumns();
            }

            //    DetailRowChange();
     
            //DetailGridExControl.DataSource = DetailGridBindingSource;
            //DetailGridExControl.BestFitColumns();
        }

        private void MainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DetailRowChange();
        }

        private void DetailRowChange()
        {            
            int detailCnt = gridEx2.MainGrid.RecordCount;

            //재고수정 버튼 활성화 비활성화 처리
            if (detailCnt == 0)
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);

            else if (detailCnt > 0 )
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                
            //상세항목 선택된 행 컬럼 확인 -- 확인 필요
            //string sDivision = gridEx2.MainGrid.MainView.GetFocusedRowCellValue("Division").ToString();
            //if (sDivision == "재고수정")
            //    DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);            
        }

        /// <summary>
        /// 재고수정 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonDevide_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //var masterObj = MasterGridBindingSource.Current as DataTable;
                //var detailObj = DetailGridBindingSource.Current as DataTable;

                //DataRow masterObj = gridEx1.MainGrid.MainView.GetFocusedDataRow();
                //DataRow detailObj = gridEx2.MainGrid.MainView.GetFocusedDataRow();

                //if (detailObj == null) return;

                ////Popup 호출
                //PUR_POPUP.XPFPUR_STOCK fm = new PUR_POPUP.XPFPUR_STOCK(masterObj, detailObj: detailObj);
                //fm.ShowDialog();
                //DataLoad();

                var objItemcode = gridEx1.MainGrid.MainView.GetFocusedRowCellValue("ItemCode");
                var objInOutLotno = gridEx1.MainGrid.MainView.GetFocusedRowCellValue("LotNo");
                var objDivision = gridEx2.MainGrid.MainView.GetFocusedRowCellValue("Division");
                var objInoutDate = gridEx2.MainGrid.MainView.GetFocusedRowCellValue("InOutDate");
                var objInqty = gridEx2.MainGrid.MainView.GetFocusedRowCellValue("InQty");
                

                //var masterObj = MasterGridBindingSource.Current as DataTable;
                //var obj = DetailGridBindingSource.Current as DataTable;

                if (objInoutDate == null)
                    return;

                var innochk = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == objInoutDate.ToString()).FirstOrDefault();

                //PUR_POPUP.XPFPUR_STOCK fm = new PUR_POPUP.XPFPUR_STOCK(Itemcode.ToString(), InOutLotno.ToString(), objDivision.ToString(), objInqty.GetDecimalNullToZero(), objInoutDate.ToString());
                PUR_POPUP.XPFPUR_STOCK fm = new PUR_POPUP.XPFPUR_STOCK(objItemcode.ToString(), objInOutLotno.ToString(), objDivision.ToString(), objInqty.GetDecimalNullToZero(), objInoutDate.ToString() );

                fm.ShowDialog();

                DataLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
                return;
            }
        }

        private void MainView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //if (e.Clicks == 2)
            //{
            //    var masterObj = MasterGridBindingSource.Current as TEMP_PUR_STOCK_ITEM;

            //    var obj = DetailGridBindingSource.Current as TEMP_PUR_STOCK_DETAIL;
            //    if (obj == null) return;

            //    if (obj.Division.Contains("재고조정"))
            //    {
            //        var innochk = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == obj.InOutLotNo).FirstOrDefault();

            //        var scmchk = ModelService.GetChildList<TN_PUR1200>(p => p.InNo == innochk.InNo).FirstOrDefault();

            //        PUR_POPUP.XPFPUR_STOCK fm = new PUR_POPUP.XPFPUR_STOCK(masterObj.ItemCode, obj.InOutLotNo, obj.Division, obj.InQty, obj.InOutDate ?? DateTime.Now);
                   
            //        fm.ShowDialog();

            //        DataLoad();

            //    }
            //}
        }
    }
}