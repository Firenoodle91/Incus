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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using DevExpress.Utils;
using HKInc.Utils.Enum;
using DevExpress.XtraEditors.Repository;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Handler;
using HKInc.Ui.View.View.REPORT;
using DevExpress.XtraReports.UI;
using DevExpress.XtraBars;

namespace HKInc.Ui.View.View.PUR
{
    /// <summary>
    /// 외주입고관리화면
    /// </summary>
    public partial class XFPUR1500 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        IService<TN_PUR1500> ModelService = (IService<TN_PUR1500>)ProductionFactory.GetDomainService("TN_PUR1500");
        List<TN_STD1000> processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
        List<TN_STD1000> surfaceList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.SurfaceList);

        /// <summary>
        /// 20210625 오세완 차장
        /// 외주에서 마지막 공정인 경우 자동출고한 로트를 투입 실행여부 결정
        /// </summary>
        private bool bInsertLot = false;
        #endregion
        public XFPUR1500()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            //DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;

            datePeriodEditEx1.SetTodayIsMonth();
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "TN_PUR1401.ProcessCode")
            {
                var processCode = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "TN_PUR1401.ProcessCode").GetNullToEmpty();
                var surface = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "TN_STD1100.SurfaceList").GetNullToEmpty();
                if (!processCode.IsNullOrEmpty() && !surface.IsNullOrEmpty())
                {
                    var processObj = processList.Where(p => p.CodeVal == processCode).FirstOrDefault();
                    if (processObj != null && processObj.CodeName.Contains("표면처리"))
                    {
                        var surfaceObj = surfaceList.Where(p => p.CodeVal == surface).FirstOrDefault();
                        if (surfaceObj != null)
                        {
                            e.DisplayText = processObj.CodeName + "_" + surfaceObj.CodeName;
                        }
                    }
                }
            }
        }

        protected override void InitCombo()
        {            
            lupcustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(p => p.CustomerName).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"));
            MasterGridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"));
            MasterGridExControl.MainGrid.AddColumn("InDate", LabelConvert.GetLabelText("InDate"));
            MasterGridExControl.MainGrid.AddColumn("InId", LabelConvert.GetLabelText("InId"));
            MasterGridExControl.MainGrid.AddColumn("InCustomerCode", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InDate", "InCustomerCode", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1500>(MasterGridExControl);

            DetailGridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;
            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, false);

            // 20211105 오세완 차장 이과장님 요청으로 추가 팝업 추가
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"));            
            DetailGridExControl.MainGrid.AddColumn("InSeq", LabelConvert.GetLabelText("InSeq"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"), false);
            DetailGridExControl.MainGrid.AddColumn("PoSeq", LabelConvert.GetLabelText("PoSeq"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0", false);
            DetailGridExControl.MainGrid.AddColumn("TN_PUR1401.ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            DetailGridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            DetailGridExControl.MainGrid.AddColumn("TN_PUR1401.ItemMoveNo", LabelConvert.GetLabelText("ItemMoveNo"));
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("TN_PUR1401.PoQty", LabelConvert.GetLabelText("PoQty"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,#.##");
            DetailGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"));
            DetailGridExControl.MainGrid.AddColumn("InCost", LabelConvert.GetLabelText("InCost"));
            DetailGridExControl.MainGrid.AddUnboundColumn("InAmt", LabelConvert.GetLabelText("InAmt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([InQty],0) * ISNULL([InCost],0)", FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("BadQty", LabelConvert.GetLabelText("BadQty"));
            DetailGridExControl.MainGrid.AddColumn("BadType", LabelConvert.GetLabelText("BadType"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InQty", "InCost", "Memo", "BadQty", "BadType");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1501>(DetailGridExControl);

            DetailGridExControl.MainGrid.MainView.Columns["InQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            DetailGridExControl.MainGrid.MainView.Columns["InQty"].SummaryItem.FieldName = "InQty";
            DetailGridExControl.MainGrid.MainView.Columns["InQty"].SummaryItem.DisplayFormat = "{0:#,#.##}";

            DetailGridExControl.MainGrid.MainView.Columns["BadQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            DetailGridExControl.MainGrid.MainView.Columns["BadQty"].SummaryItem.FieldName = "BadQty";
            DetailGridExControl.MainGrid.MainView.Columns["BadQty"].SummaryItem.DisplayFormat = "{0:#,#.##}";

            var barTextEditBarCode = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCode.Id = 6;
            barTextEditBarCode.Enabled = UserRight.HasEdit;
            barTextEditBarCode.Name = "barTextEditBarCode";
            barTextEditBarCode.EditWidth = 150;
            barTextEditBarCode.Edit.KeyDown += Edit_KeyDown;
            //DetailGridExControl.BarTools.AddItem(barTextEditBarCode);

            var barTextEditBarCodeStaticItem = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCodeStaticItem.Id = 7;
            barTextEditBarCodeStaticItem.Name = "barTextEditBarCodeStaticItem";
            barTextEditBarCodeStaticItem.Edit.NullText = LabelConvert.GetLabelText("ItemMoveNo") + ":";
            barTextEditBarCodeStaticItem.EditWidth = barTextEditBarCodeStaticItem.Edit.NullText.Length * 15;
            //barTextEditBarCodeStaticItem.EditWidth = 120;
            barTextEditBarCodeStaticItem.Enabled = false;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.ForeColor = Color.Black;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Far;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.BackColor = Color.Transparent;
            barTextEditBarCodeStaticItem.Edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Alignment = BarItemLinkAlignment.Left;

            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCode);
            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCodeStaticItem);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InCustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);

            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InQty", DefaultBoolean.Default, MasterCodeSTR.Numeric_N2);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InCost");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_PUR1401.ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("BadQty", DefaultBoolean.Default, MasterCodeSTR.Numeric_N2);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BadType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string cust = lupcustcode.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.InDate >= datePeriodEditEx1.DateFrEdit.DateTime 
                                                                        &&  p.InDate <= datePeriodEditEx1.DateToEdit.DateTime)
                                                                        && (string.IsNullOrEmpty(cust) ? true : p.InCustomerCode == cust)
                                                                     )
                                                                     .OrderBy(o => o.InNo)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_PUR1500 obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = obj.TN_PUR1501List.OrderBy(p => p.InSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var masterList = MasterGridBindingSource.List as List<TN_PUR1500>;
                if (masterList != null && masterList.Count > 0)
                {
                    var editList = masterList.Where(p => p.EditRowFlag == "Y").ToList();

                    foreach (var v in editList.Where(p => p.TN_PUR1501List.Any(c => c.NewRowFlag == "Y")).ToList())
                    {
                        foreach (var d in v.TN_PUR1501List.Where(p => p.NewRowFlag == "Y").ToList())
                        {
                            var updateItemMove = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == d.TN_PUR1401.ItemMoveNo && p.ProcessSeq == d.TN_PUR1401.ProcessSeq).FirstOrDefault();
                            if (updateItemMove != null)
                            {
                                updateItemMove.OkSumQty = d.InQty - d.BadQty.GetDecimalNullToZero();
                                updateItemMove.BadSumQty = d.BadQty.GetDecimalNullToZero();
                                updateItemMove.UpdateTime = DateTime.Now;
                                ModelService.UpdateChild(updateItemMove);
                            }
                        }
                    }

                    foreach (var v in editList.Where(p => p.TN_PUR1501List.Any(c => c.NewRowFlag != "Y" && c.EditRowFlag == "Y")).ToList())
                    {
                        foreach (var d in v.TN_PUR1501List.Where(p => p.NewRowFlag != "Y" && p.EditRowFlag == "Y").ToList())
                        {
                            var updateItemMove = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == d.TN_PUR1401.ItemMoveNo && p.ProcessSeq == d.TN_PUR1401.ProcessSeq).FirstOrDefault();
                            if (updateItemMove != null)
                            {
                                updateItemMove.OkSumQty = d.InQty - d.BadQty.GetDecimalNullToZero();
                                updateItemMove.BadSumQty = d.BadQty.GetDecimalNullToZero();
                                updateItemMove.UpdateTime = DateTime.Now;
                                ModelService.UpdateChild(updateItemMove);
                            }
                        }
                    }
                }
            }

            ModelService.Save();

            //작업지시상태 갱신
            DbRequestHandler.USP_UPD_PUR1500_JOBSTATES();

            Check_AutoSrcOut();

            // 20210625 오세완 차장 자동출고가 실행되면 로트 투입을 실행
            if (bInsertLot)
            {
                Combine_LotDtl();
                bInsertLot = false;
            }

            DataLoad();
        }

        /// <summary>
        /// 20210624 오세완 차장 자동 출고한 원자재 혹은 반제품 lot를 추적할 수 있게 tn_lot_dtl에 insert
        /// </summary>
        /// <param name="sWorkno">작업지시번호</param>
        /// <param name="sProcesscode">공정코드</param>
        private void Combine_LotDtl()
        {
            TN_PUR1500 mObj = MasterGridBindingSource.Current as TN_PUR1500;
            if (mObj != null)
            {
                if (mObj.TN_PUR1501List != null)
                    if (mObj.TN_PUR1501List.Count > 0)
                    {
                        foreach (TN_PUR1501 each in mObj.TN_PUR1501List)
                        {
                            if (each.TN_PUR1401 != null)
                            {
                                string sProductLotNo = each.TN_PUR1401.ProductLotNo;
                                string sWorkno = each.TN_PUR1401.WorkNo;

                                //투입정보불러오기.
                                List<TN_LOT_MST> mstArr = ModelService.GetChildList<TN_LOT_MST>(p => p.WorkNo == sWorkno &&
                                                                                                     p.ProductLotNo == sProductLotNo).ToList();

                                if (mstArr != null)
                                    if (mstArr.Count > 0)
                                    {
                                        TN_LOT_MST mstEach = mstArr.FirstOrDefault();
                                        if (mstEach.TN_LOT_DTL_List != null)
                                            if (mstEach.TN_LOT_DTL_List.Count > 0)
                                            {
                                                List<TN_PUR1300> purInlot_Arr = ModelService.GetChildList<TN_PUR1300>(p => p.Temp == each.InNo).ToList();
                                                if (purInlot_Arr != null)
                                                    if (purInlot_Arr.Count > 0)
                                                    {
                                                        foreach (TN_PUR1300 each_Inlot in purInlot_Arr)
                                                        {
                                                            if (each_Inlot.TN_PUR1301List != null)
                                                                if (each_Inlot.TN_PUR1301List.Count > 0)
                                                                {
                                                                    foreach (TN_PUR1301 each_Inlot_dtl in each_Inlot.TN_PUR1301List)
                                                                    {
                                                                        decimal dSeqMax = 0;
                                                                        if (mstEach.TN_LOT_DTL_List != null)
                                                                            dSeqMax = mstEach.TN_LOT_DTL_List.Count + 1;
                                                                        else
                                                                            dSeqMax = 1;

                                                                        TN_LOT_DTL newDtl = new TN_LOT_DTL()
                                                                        {
                                                                            WorkNo = sWorkno,
                                                                            ProductLotNo = sProductLotNo,
                                                                            ItemCode = mstEach.ItemCode,
                                                                            Seq = dSeqMax,
                                                                            SrcCode = each_Inlot_dtl.ItemCode,
                                                                            SrcInLotNo = each_Inlot_dtl.OutLotNo,
                                                                            WorkingDate = each.CreateTime
                                                                        };

                                                                        // 20210625 오세완 차장 공정코드를 bom이 있으면 bom으로, 아니면 마지막 공정으로 설정
                                                                        using (var context = new HKInc.Ui.Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
                                                                        {
                                                                            SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", mstEach.ItemCode);
                                                                            var vResult = context.Database.SqlQuery<TEMP_XPFRESULT_BOMLIST_BANPRODUCT>("USP_GET_XPFRESULT_BOMLIST_BANPRODUCT_AUTO @ITEM_CODE", sp_Itemcode).ToList();
                                                                            if (vResult != null)
                                                                                if (vResult.Count > 0)
                                                                                {
                                                                                    foreach (TEMP_XPFRESULT_BOMLIST_BANPRODUCT each1 in vResult)
                                                                                    {
                                                                                        if (each1.ITEM_CODE == each_Inlot_dtl.ItemCode)
                                                                                        {
                                                                                            if (each1.PROCESS_CODE.GetNullToEmpty() != "")
                                                                                                newDtl.ProcessCode = each1.PROCESS_CODE.GetNullToEmpty();
                                                                                            else
                                                                                                newDtl.ProcessCode = each.TN_PUR1401.ProcessCode;

                                                                                            break;
                                                                                        }
                                                                                    }
                                                                                }
                                                                        }

                                                                        mstEach.TN_LOT_DTL_List.Add(newDtl);
                                                                        ModelService.UpdateChild<TN_LOT_MST>(mstEach);
                                                                        ModelService.Save();
                                                                    }
                                                                }
                                                        }
                                                    }
                                            }
                                    }
                            }
                        }
                    }
            }
        }

        /// <summary>
        /// 20210624 오세완 차장
        /// 외주공정에서 종료되는 품목은 여기서 수동관리되지 않는 자재를 자동출고처리
        /// </summary>
        private void Check_AutoSrcOut()
        {
            TN_PUR1500 mObj = MasterGridBindingSource.Current as TN_PUR1500;
            if (mObj != null)
            {
                if (mObj.TN_PUR1501List != null)
                    if (mObj.TN_PUR1501List.Count > 0)
                    {
                        foreach (TN_PUR1501 each in mObj.TN_PUR1501List)
                        {
                            if (each.TN_PUR1401 != null)
                            {
                                string sWorkno = each.TN_PUR1401.WorkNo;
                                string sProcesscode = each.TN_PUR1401.ProcessCode;
                                List<TN_MPS1200> workObj_Arr = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == sWorkno &&
                                                                                                          p.ProcessCode == sProcesscode).ToList();
                                if (workObj_Arr != null)
                                    if (workObj_Arr.Count > 0)
                                    {
                                        TN_MPS1200 workObj = workObj_Arr.FirstOrDefault();
                                        int iProcessMax = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == sWorkno).Max(m => m.ProcessSeq);
                                        if (workObj.ProcessSeq == iProcessMax)
                                        {
                                            // 20210624 오세완 차장 완제품은 왠만하면 포장공정을 처리해서 수동관리를 하지 않는 자재를 자동출고 처리하나 혹 그렇지 않은 제품은 여기서 처리 하도록 추가
                                            if (workObj.TN_STD1100 != null)
                                                if (workObj.TN_STD1100.TopCategory == MasterCodeSTR.TopCategory_WAN)
                                                {
                                                    List<TN_STD1300> childBomObj_MG_NOT = null;
                                                    var wanBomObj = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == workObj.ItemCode &&
                                                                                                               p.UseFlag == "Y").FirstOrDefault();
                                                    if (wanBomObj != null)
                                                    {
                                                        // 20210622 오세완 차장 반제품 군은 무조건 종료 전에 처리가 됬다고 가정한다. 
                                                        childBomObj_MG_NOT = ModelService.GetChildList<TN_STD1300>(p => p.ParentBomCode == wanBomObj.BomCode &&
                                                                                                                        (p.TopCategory != MasterCodeSTR.TopCategory_BAN || p.TopCategory != MasterCodeSTR.TopCategory_BAN_Outsourcing) &&
                                                                                                                        p.MgFlag == "N" &&
                                                                                                                        p.UseFlag == "Y").ToList();

                                                        if (childBomObj_MG_NOT.Count > 0)
                                                        {
                                                            foreach (var v in childBomObj_MG_NOT)
                                                            {
                                                                AutoOutSrcQty(v.ItemCode, v.UseQty, workObj, each.InQty, each.InNo);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    using (var context = new HKInc.Ui.Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
                                                    {
                                                        SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", workObj.ItemCode);
                                                        var vResult = context.Database.SqlQuery<TEMP_XPFRESULT_BOMLIST_BANPRODUCT>("USP_GET_XPFRESULT_BOMLIST_BANPRODUCT_AUTO @ITEM_CODE", sp_Itemcode).ToList();
                                                        if (vResult != null)
                                                            if (vResult.Count > 0)
                                                            {
                                                                foreach (TEMP_XPFRESULT_BOMLIST_BANPRODUCT each1 in vResult)
                                                                {
                                                                    AutoOutSrcQty(each1.ITEM_CODE, each1.USE_QTY, workObj, each.InQty, each.InNo);
                                                                }
                                                            }
                                                    }
                                                }

                                            bInsertLot = true;
                                        }
                                    }
                            }
                        }
                    }
            }
        }

        /// <summary>
        /// 20210624 오세완 차장 수동관리여부 N인 건에 대한 원자재를 작업종료시 자동출고처리
        /// </summary>
        /// <param name="sItemcode">차감할 원자재 품목코드</param>
        /// <param name="dUseqty">소요량</param>
        /// <param name="pObj">작업지시객체</param>
        private void AutoOutSrcQty(string sItemcode, decimal dUseqty, TN_MPS1200 mpsParam, decimal dResult, string sInNo)
        {
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", sItemcode);
                var vResult = context.Database.SqlQuery<TEMP_XPFRESULT_AUTO_SRCOUT_LIST>("USP_GET_XPFRESULT_AUTO_SRCOUT_LIST @ITEM_CODE", sp_Itemcode).ToList();
                if (vResult != null)
                    if (vResult.Count > 0)
                    {
                        bool bSet_Reout = false;
                        decimal dCalQty = dResult * dUseqty;
                        bool bBreak = false;
                        foreach (TEMP_XPFRESULT_AUTO_SRCOUT_LIST each in vResult)
                        {
                            TN_PUR1300 prenewobj = new TN_PUR1300()
                            {
                                OutNo = DbRequestHandler.GetSeqMonth("OUT"),
                                OutDate = DateTime.Today,
                                OutId = GlobalVariable.LoginId,
                                Memo = mpsParam.ItemCode + " 자동차감출고",
                                Temp = sInNo // 20210624 오세완 차장 자동출고한 외주출고번호 저장
                            };

                            List<TN_PUR1201> tempArr = ModelService.GetChildList<TN_PUR1201>(p => p.InLotNo == each.IN_LOT_NO).ToList();
                            if (tempArr != null)
                                if (tempArr.Count > 0)
                                {
                                    TN_PUR1201 preInDetailObj = tempArr.FirstOrDefault();
                                    TN_PUR1301 newdtlobj = new TN_PUR1301()
                                    {
                                        OutNo = prenewobj.OutNo,
                                        OutSeq = prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1,
                                        InNo = preInDetailObj.InNo,
                                        InSeq = preInDetailObj.InSeq,
                                        ItemCode = sItemcode,
                                        OutLotNo = prenewobj.OutNo + (prenewobj.TN_PUR1301List.Count == 0 ? 1 : prenewobj.TN_PUR1301List.Max(o => o.OutSeq) + 1).ToString().PadLeft(3, '0'),
                                        InLotNo = each.IN_LOT_NO,
                                        //InCustomerLotNo = preInDetailObj.InCustomerLotNo,
                                        Memo = mpsParam.ItemCode + "자동차감출고",
                                        ReOutYn = "N", //이전 LOT의 출고 막음
                                        AutoFlag = "Y", //자동출고LOTNO 수동출가 불가
                                    };

                                    if (dCalQty <= each.STOCK_QTY)
                                    {
                                        newdtlobj.OutQty = dCalQty;
                                    }
                                    else
                                    {
                                        newdtlobj.OutQty = (decimal)each.STOCK_QTY;
                                        bSet_Reout = true;
                                    }

                                    prenewobj.TN_PUR1301List.Add(newdtlobj);
                                    ModelService.InsertChild<TN_PUR1300>(prenewobj);
                                    //ModelService.InsertChild<TN_PUR1301>(newdtlobj);

                                    if (bSet_Reout)
                                    {
                                        List<TN_PUR1201> tempArr1 = ModelService.GetChildList<TN_PUR1201>(p => p.InNo == preInDetailObj.InNo &&
                                                                                                               p.InSeq == preInDetailObj.InSeq &&
                                                                                                               p.ItemCode == sItemcode &&
                                                                                                               p.InLotNo == each.IN_LOT_NO).ToList();

                                        if (tempArr1 != null)
                                            if (tempArr1.Count > 0)
                                            {
                                                // 20210622 오세완 차장 박차장님이 이 컬럼으로 포장에서 자동출고 조회하기 때문에 다사용하면 처리
                                                TN_PUR1201 predtlobj = tempArr1.FirstOrDefault();
                                                predtlobj.ReOutYn = "N";
                                                ModelService.UpdateChild<TN_PUR1201>(predtlobj);
                                            }
                                    }

                                    dCalQty -= newdtlobj.OutQty;
                                    if (dCalQty <= 0)
                                        bBreak = true;

                                    ModelService.Save();
                                }

                            if (bBreak)
                                break;
                        }
                    }
            }
        }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            var masterList = MasterGridBindingSource.List as List<TN_PUR1500>;
            var newList = masterList.Where(p => p.NewRowFlag == "Y").ToList();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Value_1, newList);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PUR1400, param, MasterAddRowPopupCallback);

            form.ShowPopup(true);
        }

        private void MasterAddRowPopupCallback(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var returnList = (List<TN_PUR1400>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var returnObj in returnList)
            {
                var newobj = new TN_PUR1500();
                newobj.InNo = DbRequestHandler.GetSeqMonth("OIN");
                newobj.PoNo = returnObj.PoNo;
                newobj.InCustomerCode = returnObj.PoCustomerCode;
                newobj.InDate = DateTime.Today;
                newobj.InId = GlobalVariable.LoginId;
                newobj.NewRowFlag = "Y";
                MasterGridBindingSource.Add(newobj);
                ModelService.Insert(newobj);
            }

            if (returnList.Count > 0)
            {
                SetIsFormControlChanged(true);
                MasterGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteRow()
        {
            TN_PUR1500 obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null) return;
            if (obj.TN_PUR1501List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("OutInMasterInfo"), LabelConvert.GetLabelText("OutInDetailInfo"), LabelConvert.GetLabelText("OutInDetailInfo")));
                return;
            }

            MasterGridBindingSource.Remove(obj);
            ModelService.Delete(obj);
        }

        private void Edit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var textEdit = sender as TextEdit;
                if (textEdit == null) return;

                try
                {
                    var itemMoveNo = textEdit.EditValue.GetNullToEmpty().ToUpper();
                    if (itemMoveNo.IsNullOrEmpty()) return;

                    var masterObj = MasterGridBindingSource.Current as TN_PUR1500;
                    if (masterObj == null) return;

                    var itemMoveList = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == itemMoveNo).ToList();
                    if (itemMoveList.Count == 0)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                        return;
                    }

                    var preItemMoveObj = itemMoveList.OrderBy(p => p.ProcessSeq).LastOrDefault();
                    if (preItemMoveObj == null)
                        return;

                    var outProcessCheckObj = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == preItemMoveObj.WorkNo
                                                                                    && p.ProcessSeq == preItemMoveObj.ProcessSeq
                                                                                    && p.OutProcFlag == "Y").FirstOrDefault();

                    if (outProcessCheckObj == null)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("OutProcess")));
                        return;
                    }

                    var checkObj2 = ModelService.GetChildList<TN_PUR1501>(p => p.TN_PUR1401.ItemMoveNo == itemMoveNo && p.TN_PUR1401.ProcessSeq == outProcessCheckObj.ProcessSeq).FirstOrDefault();
                    if (checkObj2 != null)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_41), LabelConvert.GetLabelText("ItemMoveNo")));
                        return;
                    }

                    var checkObj = ModelService.GetChildList<TN_PUR1401>(p => p.PoNo == masterObj.PoNo && p.ItemMoveNo == itemMoveNo && p.ProcessSeq == outProcessCheckObj.ProcessSeq).FirstOrDefault();
                    if (checkObj == null)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                        return;
                    }

                    var detailList = DetailGridBindingSource.List as List<TN_PUR1501>;
                    if (detailList == null) return;

                    if (detailList != null)
                    {
                        var detailCheckObj = detailList.Where(p => p.TN_PUR1401.ItemMoveNo == itemMoveNo && p.TN_PUR1401.ProcessSeq == outProcessCheckObj.ProcessSeq).FirstOrDefault();
                        if (detailCheckObj != null)
                        {
                            DetailGridExControl.MainGrid.MainView.FocusedRowHandle = DetailGridExControl.MainGrid.MainView.LocateByValue("InSeq", detailCheckObj.InSeq);
                        }
                        else
                        {
                            var pobj = new TN_PUR1501();
                            pobj.InNo = masterObj.InNo;
                            pobj.InSeq = masterObj.TN_PUR1501List.Count == 0 ? 1 : masterObj.TN_PUR1501List.Max(p => p.InSeq) + 1;
                            //pobj.InLotNo = pobj.InNo + pobj.InSeq.ToString().PadLeft(2, '0');
                            pobj.InLotNo = checkObj.ProductLotNo;
                            pobj.ItemCode = checkObj.ItemCode;
                            pobj.InQty = checkObj.NotInQty;
                            pobj.InCost = checkObj.PoCost;
                            pobj.PoNo = checkObj.PoNo;
                            pobj.PoSeq = checkObj.PoSeq;
                            pobj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == checkObj.ItemCode).First();
                            pobj.TN_PUR1401 = checkObj;
                            //pobj.TN_PUR1401.TN_MPS1200.JobStates = MasterCodeSTR.JobStates_OutEnd; // 상태 - 외주완료
                            pobj.NewRowFlag = "Y";

                            masterObj.TN_PUR1501List.Add(pobj);
                            masterObj.EditRowFlag = "Y";
                            DetailGridBindingSource.Add(pobj);
                        }
                    }

                }
                finally
                {
                    DetailGridExControl.BestFitColumns();
                    textEdit.EditValue = "";
                    e.Handled = true;
                }
            }
        }

        // 20211105 오세완 차장 이과장님 요청으로 추가 팝업 추가
        protected override void DetailAddRowClicked()
        {
            var obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null) return;

            var masterList = MasterGridBindingSource.List as List<TN_PUR1500>;
            if (masterList == null) return;

            var newMasterList = masterList.Where(p => p.TN_PUR1501List.Any(c => c.NewRowFlag == "Y")).ToList();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Constraint, obj.PoNo);
            param.SetValue(PopupParameter.Value_1, newMasterList);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PUR1401, param, DetailAddRowPopupCallback);
            form.ShowPopup(true);
        }

        private void DetailAddRowPopupCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_PUR1500;
            if (masterObj == null) return;

            var returnList = (List<TN_PUR1401>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var v in returnList)
            {
                var pobj = new TN_PUR1501();
                pobj.InNo = masterObj.InNo;
                pobj.InSeq = masterObj.TN_PUR1501List.Count == 0 ? 1 : masterObj.TN_PUR1501List.Max(p => p.InSeq) + 1;
                //pobj.InLotNo = pobj.InNo + pobj.InSeq.ToString().PadLeft(2, '0');
                pobj.InLotNo = v.ProductLotNo;
                pobj.ItemCode = v.ItemCode;
                pobj.InQty = v.PoQty;
                pobj.InCost = v.PoCost;
                pobj.PoNo = v.PoNo;
                pobj.PoSeq = v.PoSeq;
                pobj.TN_PUR1401 = ModelService.GetChildList<TN_PUR1401>(p => p.PoNo == v.PoNo && p.PoSeq == v.PoSeq).First();
                pobj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).First();
                pobj.TN_PUR1401.TN_MPS1200.JobStates = MasterCodeSTR.JobStates_OutEnd; // 상태 - 외주완료
                masterObj.TN_PUR1501List.Add(pobj);
                DetailGridBindingSource.Add(pobj);
            }

            if (returnList.Count > 0)
            {
                SetIsFormControlChanged(true);
                DetailGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteDetailRow()
        {
            var obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null) return;

            var delobj = DetailGridBindingSource.Current as TN_PUR1501;
            if (delobj == null) return;

            var detailList = DetailGridBindingSource.List as List<TN_PUR1501>;

            var nextObj = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == delobj.TN_PUR1401.WorkNo && p.ProcessSeq == delobj.TN_PUR1401.ProcessSeq + 1).FirstOrDefault();

            if (nextObj != null)
            {
                if (nextObj.JobStates != MasterCodeSTR.JobStates_Wait)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_53), nextObj.WorkNo));
                    return;
                }
            }

            if (detailList.Where(p => p.InLotNo == delobj.InLotNo).Count() == 1)
            {
                var JobStatesObj = ModelService.GetChildList<TN_PUR1401>(p => p.PoNo == delobj.PoNo && p.PoSeq == delobj.PoSeq).First();

                //JobStatesObj.TN_MPS1200.JobStates = MasterCodeSTR.JobStates_OutStart; // 상태 - 외주진행
                JobStatesObj.UpdateTime = DateTime.Now;
                ModelService.UpdateChild(JobStatesObj);
            }

            if (delobj.TN_PUR1401.ProcessSeq == 1)
            {
                var TN_LOT_MST = ModelService.GetChildList<TN_LOT_MST>(p => p.WorkNo == delobj.TN_PUR1401.WorkNo && p.ProductLotNo == delobj.TN_PUR1401.ProductLotNo).FirstOrDefault();
                if (TN_LOT_MST != null)
                {
                    foreach (var v in TN_LOT_MST.TN_LOT_DTL_List) 
                        ModelService.RemoveChild(v);
                    ModelService.RemoveChild(TN_LOT_MST);
                }
            }

            DetailGridBindingSource.Remove(delobj);
            obj.TN_PUR1501List.Remove(delobj);
            obj.EditRowFlag = "Y";
        }

        //protected override void DetailFileChooseClicked()
        //{
        //    if (DetailGridBindingSource == null || DetailGridBindingSource.DataSource == null) return;

        //    var detailList = DetailGridBindingSource.List as List<TN_PUR1501>;
        //    var checkList = detailList.Where(p => p._Check == "Y").ToList();
        //    if (checkList.Count > 0)
        //    {
        //        TEMP_ITEM_MOVE_NO_MASTER masterObj = null;
        //        List<TEMP_ITEM_MOVE_NO_DETAIL> printDetailList = null;

        //        var mainReport = new XRITEMMOVENO();

        //        foreach (var v in checkList)
        //        {
        //            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
        //            {
        //                var _workNo = new SqlParameter("@WorkNo", v.TN_PUR1401.WorkNo);
        //                masterObj = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER @WorkNo", _workNo).Where(p => p.ItemMoveNo == v.TN_PUR1401.ItemMoveNo).FirstOrDefault();
        //            }

        //            if (masterObj == null) continue;

        //            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
        //            {
        //                var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
        //                var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

        //                printDetailList = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();
        //            }
        //            var ItemMoveNoReport = new XRITEMMOVENO(masterObj, printDetailList);
        //            ItemMoveNoReport.CreateDocument();
        //            mainReport.Pages.AddRange(ItemMoveNoReport.Pages);
        //        }

        //        if (mainReport.Pages.Count > 0)
        //        {
        //            var printTool = new ReportPrintTool(mainReport);
        //            printTool.ShowPreview();
        //        }
        //    }
        //}

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1500;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR1501;
            if (detailObj == null) return;

            masterObj.EditRowFlag = "Y";
            if (detailObj.NewRowFlag != "Y")
                detailObj.EditRowFlag = "Y";
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var obj = DetailGridBindingSource.Current as TN_PUR1501;

            var nextObj = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == obj.TN_PUR1401.WorkNo && p.ProcessSeq == obj.TN_PUR1401.ProcessSeq + 1).FirstOrDefault();

            if (nextObj != null)
            {
                if (nextObj.JobStates != MasterCodeSTR.JobStates_Wait)
                {
                    e.Cancel = true;
                }
            }
        }
    }

}
