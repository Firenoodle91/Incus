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
using DevExpress.Utils;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using System.Data.SqlClient;
using HKInc.Utils.Enum;
using DevExpress.XtraReports.UI;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.View.View.REPORT;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 이동표관리(이동표 사이즈 10x10 출력)
    /// </summary>
    public partial class XFMPS1501 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");

        /// <summary>
        /// 20210826 오세완 차장
        /// 빈 버튼 양식
        /// </summary>
        RepositoryItemButtonEdit tempRi_Empty = new RepositoryItemButtonEdit();

        #endregion

        public XFMPS1501()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
            DetailGridExControl.MainGrid.MainView.CustomRowCellEdit += MainView_CustomRowCellEdit;
            DetailGridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;

            tempRi_Empty.ReadOnly = true;
            tempRi_Empty.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            tempRi_Empty.Buttons.Clear();
        }

        /// <summary>
        /// 20210826 오세완 차장
        /// 프레스 공정인 경우 이동표 출력 버튼 클릭하면 프레스용 이동표 출력할 수 있게 구현
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.Clicks == 1)
            {
                if(e.Column.FieldName.Contains("PrintPress"))
                {
                    if(e.RowHandle >= 0)
                    {
                        string sProcess = View.GetRowCellValue(e.RowHandle, View.Columns["ProcessCode"]).GetNullToEmpty();
                        if (sProcess == "P04")
                        {
                            TEMP_ITEM_MOVE_NO_MASTER masterObj = MasterGridBindingSource.Current as TEMP_ITEM_MOVE_NO_MASTER;
                            if (masterObj == null || masterObj.ItemMoveNo.IsNullOrEmpty())
                                return;

                            TEMP_ITEM_MOVE_NO_DETAIL detailObj = DetailGridBindingSource.Current as TEMP_ITEM_MOVE_NO_DETAIL;
                            if (detailObj == null)
                                return;

                            //var ItemMoveNoReport = new XRITEMMOVEDOC_PRESS(masterObj.ItemMoveNo, masterObj.ItemCode, (decimal)detailObj.OkQty);
                            //var ItemMoveNoReport = new XRITEMMOVEDOC_PRESS(masterObj.ItemMoveNo, masterObj.ItemCode, masterObj.PerBoxQty.GetDecimalNullToZero()); // 20210905 오세완 차장 프레스 공정에서 입력한 수량 그대로 출력 기능으로 수정
                            //var printTool = new ReportPrintTool(ItemMoveNoReport);
                            //printTool.ShowPreview();

                            // 20210908 오세완 차장 자투리 수량 + 입력한 수량 2개만 출력하는 걸로 수정
                            var vPrintmulti = new XRITEMMOVEDOC_PRESS();
                            var vBox_Itemmove = new XRITEMMOVEDOC_PRESS(masterObj.ItemMoveNo, masterObj.ItemCode, masterObj.PerBoxQty.GetDecimalNullToZero());
                            vBox_Itemmove.CreateDocument();
                            vPrintmulti.Pages.AddRange(vBox_Itemmove.Pages);

                            decimal dOkQty = (decimal)detailObj.OkQty;
                            decimal dDiffQty = dOkQty - (masterObj.PerBoxQty.GetDecimalNullToZero() * (decimal)masterObj.BoxInQty);
                            if (dDiffQty > 0)
                            {
                                var vPrintAdd = new XRITEMMOVEDOC_PRESS(masterObj.ItemMoveNo, masterObj.ItemCode, dDiffQty);
                                vPrintAdd.CreateDocument();
                                vPrintmulti.Pages.AddRange(vPrintAdd.Pages);
                            }

                            vPrintmulti.PrintingSystem.ShowMarginsWarning = false;
                            vPrintmulti.ShowPrintStatusDialog = false;
                            vPrintmulti.ShowPreview();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 20210826 오세완 차장 
        /// 프레스 공정 아니면 출력 버튼 안보이게 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("PrintPress"))
                {
                    string sProcess = View.GetRowCellValue(e.RowHandle, View.Columns["ProcessCode"]).GetNullToEmpty();
                    if (sProcess != "P04")
                    {
                        e.RepositoryItem = tempRi_Empty;
                    }
                }
            }
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            IsMasterGridButtonFileChooseEnabled = true;
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("ItemMovePrint") + "[F10]", IconImageList.GetIconImage("print/printer"));

            MasterGridExControl.MainGrid.AddColumn("ItemMoveNo", LabelConvert.GetLabelText("ItemMoveNo"));
            MasterGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"), false); // 20210615 오세완 차장 화면설계서 대로 추가 

            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            MasterGridExControl.MainGrid.AddColumn("BoxInQty", LabelConvert.GetLabelText("BoxInQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.AddColumn("PerBoxQty", false); // 20210905 오세완 차장 부품이동표 내 저장된 박스당 수량 출력 추가

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            //DetailGridExControl.MainGrid.AddColumn("MachineName", LabelConvert.GetLabelText("MachineName")); // 20210615 오세완 차장 화면 설계서 대로 생략
            DetailGridExControl.MainGrid.AddColumn("OutProcFlag", LabelConvert.GetLabelText("OutProcFlag"), false);            
            DetailGridExControl.MainGrid.AddColumn("OkSumQty", LabelConvert.GetLabelText("SumOkQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            DetailGridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("OkQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            DetailGridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));
            DetailGridExControl.MainGrid.AddColumn("ResultStartDate", LabelConvert.GetLabelText("ResultStartDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm:ss");
            DetailGridExControl.MainGrid.AddColumn("ResultEndDate", LabelConvert.GetLabelText("ResultEndDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm:ss");
            DetailGridExControl.MainGrid.AddColumn("ResultDate", LabelConvert.GetLabelText("ResultDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");

            // 20210826 오세완 차장 프레스 공정이동펴 출력 버튼 추가
            DetailGridExControl.MainGrid.AddColumn("PrintPress", LabelConvert.GetLabelText("ItemMovePrint"));
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProcFlag", "N");

            // 20210826 오세완 차장 프레스 공정이동펴 출력 버튼 추가, null부분에 event전달이 잘 되지 않는다. 
            DetailGridExControl.MainGrid.SetRepositoryItemButtonEdit("PrintPress", null, LabelConvert.GetLabelText("ItemMovePrint"));

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemMoveNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();
            // 20210624 오세완 차장 김이사님 지시로 공통코드 추가시 화면 재출력 없이 하기 위해서 추가
            InitRepository();

            var workNo = tx_WorkNo.EditValue.GetNullToEmpty();

            if (workNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("WorkNo")));
                return;
            }

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var _workNo = new SqlParameter("@WorkNo", workNo);
                //var result = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER @WorkNo", _workNo).ToList();
                var result = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER_V2 @WorkNo", _workNo).ToList(); // 20210615 오세완 차장 품목명 추가 때문에 변경
                if (result != null && result.Count > 0)
                    MasterGridBindingSource.DataSource = result.ToList().OrderBy(p=>p.ItemMoveNo); // 20210905 오세완 차장 이동표 정렬 출력 추가
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TEMP_ITEM_MOVE_NO_MASTER;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", masterObj.WorkNo);
                var _itemMoveNo = new SqlParameter("@ItemMoveNo", masterObj.ItemMoveNo);

                var result = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_DETAIL>("USP_GET_ITEM_MOVE_NO_DETAIL @WorkNo, @ItemMoveNo", _workNo, _itemMoveNo).ToList();

                DetailGridBindingSource.DataSource = result.ToList();
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void FileChooseClicked()
        {
            var obj = MasterGridBindingSource.Current as TEMP_ITEM_MOVE_NO_MASTER;
            if (obj == null || obj.ItemMoveNo.IsNullOrEmpty()) return;

            var detailList = DetailGridBindingSource.List as List<TEMP_ITEM_MOVE_NO_DETAIL>;
            if (detailList == null) return;

            var ItemMoveNoReport = new XRITEMMOVENO_S(obj, detailList);
            //var ItemMoveNoReport = new XRITEMMOVENO(obj, detailList); // 20210615 오세완 차장 생산현장에서 a4를 사용해서 변경 처리
            //var ItemMoveNoReport = new XRITEMMOVENO_DAEYOUNG(obj, detailList); // 20210618 오세완 차장 품목명 출력되는 버전 교체
            var printTool = new ReportPrintTool(ItemMoveNoReport);
            printTool.ShowPreview();
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ProcessCode")
            {
                var outProcFlag = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "OutProcFlag").GetNullToEmpty();
                if (outProcFlag == "Y")
                {
                    e.DisplayText += "(" + LabelConvert.GetLabelText("Outsourcing") + ")";
                }
            }
        }
    }
}