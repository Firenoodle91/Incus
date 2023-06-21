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

namespace HKInc.Ui.View.View.BAN
{
    /// <summary>
    /// 20211222 오세완 차장
    /// 케이즈이노텍 스타일 반제품재고관리, 이월없이 간단하게
    /// </summary>
    public partial class XFBAN_STOCK_V2 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        #endregion

        public XFBAN_STOCK_V2()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && 
                (p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).ToList()); // 20210524 오세완 차장 반제품(타사) 조회 조건 추가, 자재입고로 받은 반제품(타사)를 입고 받을 수 있게 안배

        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ITEM_CODE", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("ITEM_NAME", LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("TOP_CATEGORY_NAME", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("MIDDLE_CATEGORY_NAME", LabelConvert.GetLabelText("MiddleCategory"));
            MasterGridExControl.MainGrid.AddColumn("BOTTOM_CATEGORY_NAME", LabelConvert.GetLabelText("BottomCategory"));

            MasterGridExControl.MainGrid.AddColumn("UNIT_NAME", LabelConvert.GetLabelText("Unit"));
            MasterGridExControl.MainGrid.AddColumn("WEIGHT", LabelConvert.GetLabelText("Weight"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SPEC_1", LabelConvert.GetLabelText("Spec1"));
            MasterGridExControl.MainGrid.AddColumn("SPEC_2", LabelConvert.GetLabelText("Spec2"));
            MasterGridExControl.MainGrid.AddColumn("SPEC_3", LabelConvert.GetLabelText("Spec3"));
            MasterGridExControl.MainGrid.AddColumn("SPEC_4", LabelConvert.GetLabelText("Spec4"));

            MasterGridExControl.MainGrid.AddColumn("SAFE_QTY", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("PROD_QTY", LabelConvert.GetLabelText("ProperQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SUM_INQTY", LabelConvert.GetLabelText("SumInQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SUM_OUTQTY", LabelConvert.GetLabelText("SumOutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("STOCK_QTY", LabelConvert.GetLabelText("SumStockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
         
            DetailGridExControl.SetToolbarButtonVisible(false);
            #region 기존 양식
            //DetailGridExControl.MainGrid.AddColumn("TYPE", LabelConvert.GetLabelText("Division"), HorzAlignment.Center, true);
            //DetailGridExControl.MainGrid.AddColumn("INOUT_DATE", LabelConvert.GetLabelText("InOutDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            //DetailGridExControl.MainGrid.AddColumn("LOTNO", LabelConvert.GetLabelText("InOutLotNo"));
            //DetailGridExControl.MainGrid.AddColumn("QTY", LabelConvert.GetLabelText("InOutQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            //DetailGridExControl.MainGrid.AddColumn("WORKER", LabelConvert.GetLabelText("InOutId"));
            #endregion

            #region 인천정밀 대표가 원한 양식
            // 20220117 오세완 차장 수정
            DetailGridExControl.MainGrid.AddColumn("TYPE", LabelConvert.GetLabelText("Division"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("BAN_INDATE", LabelConvert.GetLabelText("InDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("BAN_OUT_DATE", LabelConvert.GetLabelText("OutDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("LOTNO", LabelConvert.GetLabelText("InOutLotNo"));
            DetailGridExControl.MainGrid.AddColumn("BAN_IN_QTY", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            DetailGridExControl.MainGrid.AddColumn("BAN_OUT_QTY", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("STOCK_QTY", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("WORKER", LabelConvert.GetLabelText("InOutId"));
            #endregion

            // 20211222 오세완 차장 반제픔재고조정
            var barButtonDevide = new DevExpress.XtraBars.BarButtonItem();
            barButtonDevide.Id = 5;
            barButtonDevide.ImageOptions.Image = IconImageList.GetIconImage("spreadsheet/showdetail");
            barButtonDevide.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.E));
            barButtonDevide.Name = "barReturn";
            barButtonDevide.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonDevide.ShortcutKeyDisplayString = "Alt+E";
            barButtonDevide.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonDevide.Caption = LabelConvert.GetLabelText("Adjust") + "[Alt+E]";  //"분할처리[Alt+T]";
            barButtonDevide.ItemClick += BarButtonDevide_ItemClick;
            DetailGridExControl.BarTools.AddItem(barButtonDevide);

        }

        protected override void InitRepository()
        {
            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ITEM_CODE");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", lup_Item.EditValue.GetNullToEmpty());

                var vResult = context.Database.SqlQuery<TEMP_BAN_STOCK_MASTER>("USP_GET_BAN_STOCK_MASTER @ITEM_CODE", sp_Itemcode).ToList();
                MasterGridBindingSource.DataSource = vResult;
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TEMP_BAN_STOCK_MASTER;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var ItemCode = new SqlParameter("@ITEM_CODE", masterObj.ITEM_CODE);
                //var vResult = context.Database.SqlQuery<TEMP_BAN_STOCK_DETAIL>("USP_GET_BAN_STOCK_DETAIL @ITEM_CODE", ItemCode).ToList();
                //var vResult = context.Database.SqlQuery<TEMP_BAN_STOCK_DETAIL_V2>("USP_GET_BAN_STOCK_DETAIL_V2 @ITEM_CODE", ItemCode).ToList(); // 20220117 오세완 차장 양식 변경 요청으로 수정
                var vResult = context.Database.SqlQuery<TEMP_BAN_STOCK_DETAIL_V2>("USP_GET_BAN_STOCK_DETAIL_V3 @ITEM_CODE", ItemCode).ToList(); // 20220115 오세완 차장 특정 품목에서 오류가 생겨서 개선 버번으로 교체
                DetailGridBindingSource.DataSource = vResult;
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        private void BarButtonDevide_ItemClick(object sender, ItemClickEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TEMP_BAN_STOCK_MASTER;
            if (masterObj == null)
                return;

            //var obj = DetailGridBindingSource.Current as TEMP_BAN_STOCK_DETAIL;
            var obj = DetailGridBindingSource.Current as TEMP_BAN_STOCK_DETAIL_V2; // 20220117 오세완 차장 양식 변경 요청으로 수정
            if (obj == null)
                return;

            string sTypeCode = "";
            decimal dTemp_Qty = 0;
            DateTime dtTemp = DateTime.Now;
            if (obj.TYPE == "입고")
            {
                //sTypeCode = "IN";
                dTemp_Qty = obj.BAN_IN_QTY.GetDecimalNullToZero();
                dtTemp = (DateTime)obj.BAN_INDATE;
            }
            else if(obj.TYPE == "출고")
            {
                //sTypeCode = "OUT";
                dTemp_Qty = obj.BAN_OUT_QTY.GetDecimalNullToZero();
                dtTemp = (DateTime)obj.BAN_OUT_DATE;
            }

            //BAN_POPUP.XPFBAN_STOCK fm = new BAN_POPUP.XPFBAN_STOCK(masterObj.ITEM_CODE, obj.LOTNO, sTypeCode, obj.QTY, obj.INOUT_DATE);
            BAN_POPUP.XPFBAN_STOCK fm = new BAN_POPUP.XPFBAN_STOCK(masterObj.ITEM_CODE, obj.LOTNO, sTypeCode, dTemp_Qty, dtTemp); // 20220117 오세완 차장 양식 변경으로 호출 타입 변경
            fm.ShowDialog();
            DataLoad();
        }
    }
}