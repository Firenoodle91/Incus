﻿using System;
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
using HKInc.Utils.Interface.Forms;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Common;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.View.POP
{
    /// <summary>
    /// 포장POP (MES에서 사용)
    /// </summary>
    public partial class XFPOP_PACK : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");

        public XFPOP_PACK()
        {
            InitializeComponent();

            //MasterGridEx 는 미사용함. 

            DetailGridExControl = gridEx1;
            DetailGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            DetailGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;

            this.SizeChanged += XFPOP_PACK_SizeChanged;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void InitGrid()
        {
            IsDetailGridButtonExportEnabled = true;
            IsDetailGridButtonFileChooseEnabled = true;

            DetailGridExControl.SetToolbarVisible(true);

            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("WorkStart") + "[Alt+Q]", IconImageList.GetIconImage("arrows/play"));
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.DeleteRow, LabelConvert.GetLabelText("ResultAdd") + "[Alt+W]", IconImageList.GetIconImage("business%20objects/botask"));
            //DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.Export, LabelConvert.GetLabelText("ShipmentInsp") + "[Alt+E]", IconImageList.GetIconImage("maps/weightedpies"));
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("PackBarcodePrint") + "[Alt+R]", IconImageList.GetIconImage("business%20objects/bonote"));

            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            if (!UserRight.HasEdit)
            {
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.AddRow, false);
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.DeleteRow, false);
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.Export, false);
            }

            var barOutBoxBarcodePrint = new DevExpress.XtraBars.BarButtonItem();
            barOutBoxBarcodePrint.Id = 4;
            barOutBoxBarcodePrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/bosaleitem");
            barOutBoxBarcodePrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.T));
            barOutBoxBarcodePrint.Name = "barOutBoxBarcodePrint";
            barOutBoxBarcodePrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barOutBoxBarcodePrint.ShortcutKeyDisplayString = "Alt+T";
            barOutBoxBarcodePrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barOutBoxBarcodePrint.Caption = LabelConvert.GetLabelText("BoxBarcodePrint") + "[Alt+T]";
            barOutBoxBarcodePrint.ItemClick += BarOutBoxBarcodePrint_ItemClick;

            var barWorkEnd = new DevExpress.XtraBars.BarButtonItem();
            barWorkEnd.Id = 7;
            barWorkEnd.ImageOptions.Image = IconImageList.GetIconImage("actions/apply");
            barWorkEnd.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.Y));
            barWorkEnd.Name = "barWorkEnd";
            barWorkEnd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barWorkEnd.ShortcutKeyDisplayString = "Alt+Y";
            barWorkEnd.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barWorkEnd.Caption = LabelConvert.GetLabelText("WorkEnd") + "[Alt+Y]";
            barWorkEnd.ItemClick += BarWorkEnd_ItemClick;

            var barItemMoveChange = new DevExpress.XtraBars.BarButtonItem();
            barItemMoveChange.Id = 6;
            barItemMoveChange.ImageOptions.Image = IconImageList.GetIconImage("actions/convert");
            barItemMoveChange.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.U));
            barItemMoveChange.Name = "barItemMoveChange";
            barItemMoveChange.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barItemMoveChange.ShortcutKeyDisplayString = "Alt+U";
            barItemMoveChange.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barItemMoveChange.Caption = LabelConvert.GetLabelText("ItemMoveChange") + "[Alt+U]";
            barItemMoveChange.ItemClick += barItemMoveChange_ItemClick;


            // 20220207 오세완 차장 케이즈이노텍 스타일과 동일하게 처리, 박스라벨 생략하기로 함
            //DetailGridExControl.BarTools.AddItem(barOutBoxBarcodePrint);

            // 20220207 오세완 차장 케이즈이노텍 스타일과 동일하게 처리, 박스라벨 생략하기로 함
            //DetailGridExControl.BarTools.AddItem(barItemMoveChange);
            DetailGridExControl.BarTools.AddItem(barWorkEnd);

            DetailGridExControl.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            DetailGridExControl.MainGrid.AddColumn("EmergencyFlag", LabelConvert.GetLabelText("EmergencyFlag"), false);
            DetailGridExControl.MainGrid.AddColumn("JobStates", LabelConvert.GetLabelText("JobStates"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            DetailGridExControl.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("Process"));
            DetailGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("Machine"), false);
            DetailGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("ItemMoveNo", LabelConvert.GetLabelText("ItemMoveNoNow"));
            DetailGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            DetailGridExControl.MainGrid.AddColumn("ResultQty", LabelConvert.GetLabelText("ResultQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("OkQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("BadQty", LabelConvert.GetLabelText("BadQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("ResultSumQty", LabelConvert.GetLabelText("SumResultQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("PreJobStates", LabelConvert.GetLabelText("PreJobStates"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("LastItemMove", LabelConvert.GetLabelText("LastItemMove"), HorzAlignment.Center, true);

            SetButtonEnable();
        }

        protected override void InitRepository()
        {
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStates", DbRequestHandler.GetCommTopCode(MasterCodeSTR.JobStates), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PreJobStates", DbRequestHandler.GetCommTopCode(MasterCodeSTR.JobStates), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataLoad()
        {
            DetailGridRowLocator.GetCurrentRow("RowId");

            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();
            InitRepository();

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var WorkNo = new SqlParameter("@WorkNo", tx_WorkNo.EditValue.GetNullToEmpty());
                var ItemMoveNo = new SqlParameter("@ItemMoveNo", tx_ItemMoveNo.EditValue.GetNullToEmpty());
                var ItemCode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());

                var result = context.Database.SqlQuery<TEMP_XFPOP_PACK>("USP_GET_XFPOP_PACK_LIST @WorkNo, @ItemMoveNo,@ItemCode", WorkNo, ItemMoveNo, ItemCode).ToList();
                DetailGridBindingSource.DataSource = result.OrderByDescending(p => p.EmergencyFlag).ThenBy(p => p.WorkDate).ThenBy(p => p.WorkNo).ToList();
            }
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridRowLocator.SetCurrentRow();
            DetailGridExControl.BestFitColumns();
        }

        private void MainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var obj = DetailGridBindingSource.Current as TEMP_XFPOP_PACK;
            if (obj == null)
            {
                SetButtonEnable();
                return;
            }
            else
            {
                SetButtonEnable(obj.JobStates);
            }
        }

        /// <summary>작업시작</summary>
        protected override void DetailAddRowClicked()
        {
            var obj = DetailGridBindingSource.Current as TEMP_XFPOP_PACK;
            if (obj == null) return;

            //이동표 정보 조회 추가필요
            //PopupDataParam param = new PopupDataParam();
            //param.SetValue(PopupParameter.KeyValue, obj);
            //IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVESCAN_START_PACK, param, WorkStartItemMoveCallback);
            //form.ShowPopup(true);

            // 20220207 오세완 차장 이동표가 없어서 무조건 시작하는 로직으로 변경
            string sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_177);
            DialogResult dr = MessageBoxHandler.Show(sMessage, LabelConvert.GetLabelText("WorkStart"), MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                WorkStart_NoNeedItemMove(obj);
        }

        /// <summary>
        /// 20220207 오세완 차장 
        /// 케이즈 이노텍과 동일하게 부품이동표 필요 없이 시작 처리
        /// </summary>
        private void WorkStart_NoNeedItemMove(TEMP_XFPOP_PACK workObj)
        {
            TN_ITEM_MOVE pre_Itemmove_Obj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.WorkNo == workObj.WorkNo &&
                                                                                         p.ProcessSeq == workObj.ProcessSeq - 1).FirstOrDefault();

            if (pre_Itemmove_Obj == null)
                return;

            string sProductLotNo = "";
            string sItemMoveNo = pre_Itemmove_Obj.ItemMoveNo;

            if (workObj.ProcessSeq == 1)
            {
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var workingDate = DateTime.Today;
                    var WorkNo = new SqlParameter("@WorkNo", workObj.WorkNo);
                    var MachineCode = new SqlParameter("@MachineCode", "");
                    var ItemCode = new SqlParameter("@ItemCode", workObj.ItemCode);
                    var SrcItemCode = new SqlParameter("@SrcItemCode", workObj.ItemCode);
                    var SrcOutLotNo = new SqlParameter("@SrcOutLotNo", sProductLotNo);
                    var WorkingDate = new SqlParameter("@WorkingDate", workingDate);

                    //작업지시투입정보 INSERT
                    sProductLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC @WorkNo, @MachineCode ,@ItemCode ,@SrcItemCode, @SrcOutLotNo, @WorkingDate"
                                                                    , WorkNo, MachineCode, ItemCode, SrcItemCode, SrcOutLotNo, WorkingDate).SingleOrDefault();
                }
            }
            else
                sProductLotNo = pre_Itemmove_Obj.ProductLotNo;

            var ItemMoveFirstObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == sItemMoveNo && p.ProcessSeq == 1).First();

            var newItemMoveNo = new TN_ITEM_MOVE();
            newItemMoveNo.ItemMoveNo = sItemMoveNo;
            newItemMoveNo.WorkNo = workObj.WorkNo;
            newItemMoveNo.ProcessCode = workObj.ProcessCode;
            newItemMoveNo.ProcessSeq = workObj.ProcessSeq;
            newItemMoveNo.ProductLotNo = sProductLotNo;
            newItemMoveNo.BoxInQty = ItemMoveFirstObj.BoxInQty.GetDecimalNullToZero();
            newItemMoveNo.ResultSumQty = 0;
            newItemMoveNo.OkSumQty = 0;
            newItemMoveNo.BadSumQty = 0;
            newItemMoveNo.ResultQty = 0;
            newItemMoveNo.OkQty = 0;
            newItemMoveNo.BadQty = 0;
            ModelService.InsertChild(newItemMoveNo);

            #region 작업실적관리 마스터 INSERT
            var TN_MPS1201_NewObj = new TN_MPS1201();
            TN_MPS1201_NewObj.WorkNo = workObj.WorkNo;
            TN_MPS1201_NewObj.ProcessCode = workObj.ProcessCode;
            TN_MPS1201_NewObj.ProcessSeq = workObj.ProcessSeq;
            TN_MPS1201_NewObj.ItemMoveNo = sItemMoveNo;
            TN_MPS1201_NewObj.ProductLotNo = sProductLotNo;
            TN_MPS1201_NewObj.ItemCode = workObj.ItemCode;
            TN_MPS1201_NewObj.CustomerCode = workObj.CustomerCode;
            TN_MPS1201_NewObj.ResultStartDate = DateTime.Now;
            TN_MPS1201_NewObj.ResultSumQty = 0;
            TN_MPS1201_NewObj.OkSumQty = 0;
            TN_MPS1201_NewObj.BadSumQty = 0;
            ModelService.Insert(TN_MPS1201_NewObj);
            #endregion

            TN_MPS1200 current_work_Obj = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == workObj.WorkNo &&
                                                                                     p.ProcessCode == workObj.ProcessCode &&
                                                                                     p.ProcessSeq == workObj.ProcessSeq).First();

            //작업지시서 상태 변경
            current_work_Obj.JobStates = MasterCodeSTR.JobStates_Start;
            current_work_Obj.UpdateTime = DateTime.Now;
            ModelService.UpdateChild(current_work_Obj);
            ModelService.Save();

            ActRefresh();
        }

        /// <summary> 작업 시작 시 이동표 투입 CallBack </summary>
        private void WorkStartItemMoveCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            var obj = DetailGridBindingSource.Current as TEMP_XFPOP_PACK;
            if (obj == null) return;

            var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();

            
            string productLotNo = null;
            
            var itemMoveNo = e.Map.GetValue(PopupParameter.Value_1).GetNullToNull();
            productLotNo = e.Map.GetValue(PopupParameter.Value_2).GetNullToNull();

            if (obj.ProcessSeq == 1)
            {
                //var srcItemCode = e.Map.GetValue(PopupParameter.Value_3).GetNullToNull();
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var workingDate = DateTime.Today;
                    var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
                    var MachineCode = new SqlParameter("@MachineCode", "");
                    var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                    var SrcItemCode = new SqlParameter("@SrcItemCode", obj.ItemCode);
                    var SrcOutLotNo = new SqlParameter("@SrcOutLotNo", productLotNo);
                    var WorkingDate = new SqlParameter("@WorkingDate", workingDate);

                    //작업지시투입정보 INSERT
                    productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC @WorkNo, @MachineCode ,@ItemCode ,@SrcItemCode, @SrcOutLotNo, @WorkingDate"
                                                                    , WorkNo, MachineCode, ItemCode, SrcItemCode, SrcOutLotNo, WorkingDate).SingleOrDefault();
                }
            }
            
            var ItemMoveFirstObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == itemMoveNo && p.ProcessSeq == 1).First();

            var newItemMoveNo = new TN_ITEM_MOVE();
            newItemMoveNo.ItemMoveNo = itemMoveNo;
            newItemMoveNo.WorkNo = obj.WorkNo;
            newItemMoveNo.ProcessCode = obj.ProcessCode;
            newItemMoveNo.ProcessSeq = obj.ProcessSeq;
            newItemMoveNo.ProductLotNo = productLotNo;
            newItemMoveNo.BoxInQty = ItemMoveFirstObj.BoxInQty.GetDecimalNullToZero();
            newItemMoveNo.ResultSumQty = 0;
            newItemMoveNo.OkSumQty = 0;
            newItemMoveNo.BadSumQty = 0;
            newItemMoveNo.ResultQty = 0;
            newItemMoveNo.OkQty = 0;
            newItemMoveNo.BadQty = 0;
            ModelService.InsertChild(newItemMoveNo);

            #region 작업실적관리 마스터 INSERT
            var TN_MPS1201_NewObj = new TN_MPS1201();
            TN_MPS1201_NewObj.WorkNo = obj.WorkNo;
            TN_MPS1201_NewObj.ProcessCode = obj.ProcessCode;
            TN_MPS1201_NewObj.ProcessSeq = obj.ProcessSeq;
            TN_MPS1201_NewObj.ItemMoveNo = itemMoveNo;
            TN_MPS1201_NewObj.ProductLotNo = productLotNo;
            TN_MPS1201_NewObj.ItemCode = obj.ItemCode;
            TN_MPS1201_NewObj.CustomerCode = obj.CustomerCode;
            TN_MPS1201_NewObj.ResultStartDate = DateTime.Now;
            TN_MPS1201_NewObj.ResultSumQty = 0;
            TN_MPS1201_NewObj.OkSumQty = 0;
            TN_MPS1201_NewObj.BadSumQty = 0;
            ModelService.Insert(TN_MPS1201_NewObj);
            #endregion

            //작업지시서 상태 변경
            TN_MPS1200.JobStates = MasterCodeSTR.JobStates_Start;
            TN_MPS1200.UpdateTime = DateTime.Now;
            ModelService.UpdateChild(TN_MPS1200);
            ModelService.Save();

            ActRefresh();            
        }

        /// <summary>실적등록</summary>
        protected override void DeleteDetailRow()
        {
            var obj = DetailGridBindingSource.Current as TEMP_XFPOP_PACK;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.Value_1, obj.ProductLotNo);
            param.SetValue(PopupParameter.Value_2, obj.ItemMoveNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_PACK, param, ResultAddCallback);
            form.ShowPopup(true);
        }

        /// <summary>실적등록 CallBack</summary>
        private void ResultAddCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            ActRefresh();
        }

        /// <summary>포장라벨출력</summary>
        protected override void DetailFileChooseClicked()
        {
            var obj = DetailGridBindingSource.Current as TEMP_XFPOP_PACK;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.Value_1, "1");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFPACK_BARCODE_PRINT, param, BarcodePrintCallback);
            form.ShowPopup(true);
        }
        
        /// <summary>박스라벨출력</summary>
        private void BarOutBoxBarcodePrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            var obj = DetailGridBindingSource.Current as TEMP_XFPOP_PACK;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.Value_1, "2");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFPACK_BARCODE_PRINT, param, BarcodePrintCallback);
            form.ShowPopup(true);
        }

        /// <summary>라벨출력 CallBack</summary>
        private void BarcodePrintCallback(object sender, PopupArgument e){}

        /// <summary>작업종료</summary>
        private void BarWorkEnd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var obj = DetailGridBindingSource.Current as TEMP_XFPOP_PACK;
            if (obj == null) return;

            var preProcessObj = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessSeq == obj.ProcessSeq - 1).FirstOrDefault();
            if (preProcessObj != null)
            {
                if ((preProcessObj.JobStates != MasterCodeSTR.JobStates_End) && (preProcessObj.JobStates != MasterCodeSTR.JobStates_OutEnd))
                {
                    MessageBoxHandler.Show("이전 공정에 대하여 작업이 완료되어 있지 않습니다. 확인 부탁드립니다.");
                    return;
                }
            }

            try
            {
                WaitHandler.ShowWait();

                var sumObj = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).ToList();

                //작업지시수량보다 총생산수량이 적을 경우 
                if (obj.WorkQty > sumObj.Sum(p => p.ResultSumQty).GetDecimalNullToZero())
                {
                    if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_91), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.No)
                        //if (MessageBoxHandler.Show("지시수량보다 총 생산량이 부족합니다. 무시하고 종료하시겠습니까?", LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }

                if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_30), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();

                    #region 작업실적관리 마스터 UPDATE
                    var TN_MPS1201 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
                    TN_MPS1201.ResultDate = DateTime.Today;
                    TN_MPS1201.ResultEndDate = DateTime.Now;
                    ModelService.Update(TN_MPS1201);
                    #endregion

                    //작업지시서 상태 변경
                    TN_MPS1200.JobStates = MasterCodeSTR.JobStates_End;
                    //TN_MPS1200.Temp1 = customerLotNo;
                    // 20211224 오세완 차장 필요 없는 loop라 생략 처리
                    //var list = TN_MPS1200.TN_MPS1100.TN_MPS1200List.Where(p => p.WorkNo == TN_MPS1200.WorkNo).ToList();
                    //foreach (var v in list)                       
                    TN_MPS1200.UpdateTime = DateTime.Now;
                    ModelService.UpdateChild(TN_MPS1200);
                    ModelService.Save();
                    ActRefresh();
                }
            }
            finally
            {
                WaitHandler.CloseWait();
            }

        }
        /// <summary>작업완료 CallBack</summary>
        private void WorkEndCallback(object sender, PopupArgument e)
        {
            var obj = DetailGridBindingSource.Current as TEMP_XFPOP_PACK;
            if (obj == null) return;

            if (e == null) return;

            try
            {
                WaitHandler.ShowWait();
                var customerLotNo = e.Map.GetValue(PopupParameter.ReturnObject).GetNullToNull();

                var sumObj = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).ToList();

                //작업지시수량보다 총생산수량이 적을 경우 
                if (obj.WorkQty > sumObj.Sum(p => p.ResultSumQty).GetDecimalNullToZero())
                {
                    if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_91), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.No)
                        //if (MessageBoxHandler.Show("지시수량보다 총 생산량이 부족합니다. 무시하고 종료하시겠습니까?", LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }

                if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_30), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq).First();

                    #region 작업실적관리 마스터 UPDATE
                    var TN_MPS1201 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
                    TN_MPS1201.ResultDate = DateTime.Today;
                    TN_MPS1201.ResultEndDate = DateTime.Now;
                    ModelService.Update(TN_MPS1201);
                    #endregion

                    //작업지시서 상태 변경
                    TN_MPS1200.JobStates = MasterCodeSTR.JobStates_End;
                    //TN_MPS1200.Temp1 = customerLotNo;
                    var list = TN_MPS1200.TN_MPS1100.TN_MPS1200List.Where(p => p.WorkNo == TN_MPS1200.WorkNo).ToList();
                    foreach (var v in list)
                        v.Temp1 = customerLotNo;

                    TN_MPS1200.UpdateTime = DateTime.Now;
                    ModelService.UpdateChild(TN_MPS1200);
                    ModelService.Save();
                    ActRefresh();
                }
            }
            finally
            {
                WaitHandler.CloseWait();
            }
            
        }
        /// <summary>이동표교체</summary>
        private void barItemMoveChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            var obj = DetailGridBindingSource.Current as TEMP_XFPOP_PACK;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFITEMMOVESCAN_CHANGE_PACK, param, ItemMoveNoChangeCallback);
            form.ShowPopup(true);
        }

        /// <summary> 이동표 교체 CallBack </summary>
        private void ItemMoveNoChangeCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            var obj = DetailGridBindingSource.Current as TEMP_XFPOP_PACK;
            if (obj == null) return;

            var itemMoveNo = e.Map.GetValue(PopupParameter.Value_1).GetNullToNull();
            var productLotNo = e.Map.GetValue(PopupParameter.Value_2).GetNullToNull();

            if (obj.ProcessSeq == 1)
            {
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var workingDate = DateTime.Today;
                    var WorkNo = new SqlParameter("@WorkNo", obj.WorkNo);
                    var MachineCode = new SqlParameter("@MachineCode", "");
                    var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                    var SrcItemCode = new SqlParameter("@SrcItemCode", obj.ItemCode);
                    var SrcOutLotNo = new SqlParameter("@SrcOutLotNo", productLotNo);
                    var WorkingDate = new SqlParameter("@WorkingDate", workingDate);

                    //작업지시투입정보 INSERT
                    productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_SRC @WorkNo, @MachineCode ,@ItemCode ,@SrcItemCode, @SrcOutLotNo, @WorkingDate"
                                                                    , WorkNo, MachineCode, ItemCode, SrcItemCode, SrcOutLotNo, WorkingDate).SingleOrDefault();
                }
                if (obj.ProductLotNo != productLotNo)
                {
                    if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_74), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                }
            }
            else
            {
                if (obj.ProductLotNo != productLotNo)
                {
                    if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_74), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                }
            }

            #region 이전 작업실적관리 마스터 UPDATE
            var TN_MPS1201_Previous = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.ProcessCode && p.ProcessSeq == obj.ProcessSeq && p.ResultEndDate == null).LastOrDefault();
            TN_MPS1201_Previous.ResultDate = DateTime.Today;
            TN_MPS1201_Previous.ResultEndDate = DateTime.Now;
            ModelService.Update(TN_MPS1201_Previous);
            #endregion

            var checkObj = ModelService.GetChildList<TN_MPS1201>(p => p.WorkNo == obj.WorkNo
                                                                    && p.ProcessCode == obj.ProcessCode
                                                                    && p.ProcessSeq == obj.ProcessSeq
                                                                    && p.ProductLotNo == productLotNo).FirstOrDefault();
            if (checkObj != null)
            {
                checkObj.ItemMoveNo = itemMoveNo;
                checkObj.ResultDate = null;
                checkObj.ResultEndDate = null;
                ModelService.Update(checkObj);
            }
            else
            {
                #region 작업실적관리 마스터 INSERT
                var TN_MPS1201_NewObj = new TN_MPS1201();
                TN_MPS1201_NewObj.WorkNo = obj.WorkNo;
                TN_MPS1201_NewObj.ProcessCode = obj.ProcessCode;
                TN_MPS1201_NewObj.ProcessSeq = obj.ProcessSeq;
                TN_MPS1201_NewObj.ItemMoveNo = itemMoveNo;
                TN_MPS1201_NewObj.ProductLotNo = productLotNo;
                TN_MPS1201_NewObj.ItemCode = obj.ItemCode;
                TN_MPS1201_NewObj.CustomerCode = obj.CustomerCode;
                TN_MPS1201_NewObj.ResultStartDate = DateTime.Now;
                TN_MPS1201_NewObj.ResultSumQty = 0;
                TN_MPS1201_NewObj.OkSumQty = 0;
                TN_MPS1201_NewObj.BadSumQty = 0;
                ModelService.Insert(TN_MPS1201_NewObj);
                #endregion
            }

            var checkItemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.WorkNo == obj.WorkNo
                                                                            && p.ProcessCode == obj.ProcessCode
                                                                            && p.ProcessSeq == obj.ProcessSeq
                                                                            && p.ProductLotNo == productLotNo
                                                                            && p.ItemMoveNo == itemMoveNo).FirstOrDefault();
            if (checkItemMoveObj == null)
            {
                var ItemMoveFirstObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == itemMoveNo && p.ProcessSeq == 1).First();

                var newItemMoveNo = new TN_ITEM_MOVE();
                newItemMoveNo.ItemMoveNo = itemMoveNo;
                newItemMoveNo.WorkNo = obj.WorkNo;
                newItemMoveNo.ProcessCode = obj.ProcessCode;
                newItemMoveNo.ProcessSeq = obj.ProcessSeq;
                newItemMoveNo.ProductLotNo = productLotNo;
                newItemMoveNo.BoxInQty = ItemMoveFirstObj.BoxInQty.GetDecimalNullToZero();
                newItemMoveNo.ResultSumQty = 0;
                newItemMoveNo.OkSumQty = 0;
                newItemMoveNo.BadSumQty = 0;
                newItemMoveNo.ResultQty = 0;
                newItemMoveNo.OkQty = 0;
                newItemMoveNo.BadQty = 0;
                ModelService.InsertChild(newItemMoveNo);
            }

            ModelService.Save();
            ActRefresh();
        }

        private void XFPOP_PACK_SizeChanged(object sender, EventArgs e)
        {
            SetToolbarButtonVisible(false);
            if (this.MdiParent != null)
                ((IToolBar)this.MdiParent).SetToolbarButtonVisible(false);

            if (UserRight != null)
            {
                SetToolbarButtonVisible(ToolbarButton.Refresh, UserRight.HasSelect);
                if (this.MdiParent != null)
                    ((IToolBar)this.MdiParent).SetToolbarButtonVisible(ToolbarButton.Refresh, UserRight.HasSelect);
            }

            SetToolbarButtonVisible(ToolbarButton.Close, true);
            if (this.MdiParent != null)
                ((IToolBar)this.MdiParent).SetToolbarButtonVisible(ToolbarButton.Close, true);
            else
                ((IToolBar)this).SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        private void SetButtonEnable(string jobStates = null)
        {
            if (jobStates == MasterCodeSTR.JobStates_Wait) //대기
            {
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.AddRow, true);
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.DeleteRow, false);
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.Export, false);
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
                DetailGridExControl.BarTools.ItemLinks[4].Item.Enabled = false;
                // 20220207 오세완 차장 이동표교체, 박스포장 버튼 생략해서 여기도 생략처리
                //DetailGridExControl.BarTools.ItemLinks[5].Item.Enabled = false;
                //DetailGridExControl.BarTools.ItemLinks[6].Item.Enabled = false;
            }
            else if (jobStates == MasterCodeSTR.JobStates_Start) //진행
            {
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.AddRow, false);
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.DeleteRow, true);
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.Export, true);
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
                DetailGridExControl.BarTools.ItemLinks[4].Item.Enabled = true;
                // 20220207 오세완 차장 이동표교체, 박스포장 버튼 생략해서 여기도 생략처리
                //DetailGridExControl.BarTools.ItemLinks[5].Item.Enabled = false;
                //DetailGridExControl.BarTools.ItemLinks[6].Item.Enabled = false;
            }
            else
            {
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.AddRow, false);
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.DeleteRow, false);
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.Export, false);
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
                DetailGridExControl.BarTools.ItemLinks[4].Item.Enabled = false;
                // 20220207 오세완 차장 이동표교체, 박스포장 버튼 생략해서 여기도 생략처리
                //DetailGridExControl.BarTools.ItemLinks[5].Item.Enabled = false;
                //DetailGridExControl.BarTools.ItemLinks[6].Item.Enabled = false;
            }
        }

        private void MainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                var check = View.GetRowCellValue(e.RowHandle, View.Columns["EmergencyFlag"]).ToString();
                if (check == "Y")
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
                else
                {
                    var jobStates = View.GetRowCellValue(e.RowHandle, View.Columns["JobStates"]).ToString();
                    if (jobStates == MasterCodeSTR.JobStates_Start)
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                        var PrejobStates = View.GetRowCellValue(e.RowHandle, View.Columns["PreJobStates"]).ToString();
                        if (PrejobStates == MasterCodeSTR.JobStates_End)
                        {
                            e.Appearance.BackColor = Color.RosyBrown;
                        }
                    }
                   
                }
            }
        }

    }
}