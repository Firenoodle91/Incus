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

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 이동표관리(이동표 사이즈 10x10 출력)
    /// </summary>
    public partial class XFMPS1501 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");

        public XFMPS1501()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
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
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            MasterGridExControl.MainGrid.AddColumn("BoxInQty", LabelConvert.GetLabelText("BoxInQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            DetailGridExControl.MainGrid.AddColumn("MachineName", LabelConvert.GetLabelText("MachineName"));
            DetailGridExControl.MainGrid.AddColumn("OutProcFlag", LabelConvert.GetLabelText("OutProcFlag"), false);            
            DetailGridExControl.MainGrid.AddColumn("OkSumQty", LabelConvert.GetLabelText("SumOkQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            DetailGridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("OkQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            DetailGridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));
            DetailGridExControl.MainGrid.AddColumn("ResultStartDate", LabelConvert.GetLabelText("ResultStartDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm:ss");
            DetailGridExControl.MainGrid.AddColumn("ResultEndDate", LabelConvert.GetLabelText("ResultEndDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm:ss");
            DetailGridExControl.MainGrid.AddColumn("ResultDate", LabelConvert.GetLabelText("ResultDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProcFlag", "N");

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemMoveNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            var workNo = tx_WorkNo.EditValue.GetNullToEmpty();

            if (workNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("WorkNo")));
                return;
            }

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _workNo = new SqlParameter("@WorkNo", workNo);
                var result = context.Database.SqlQuery<TEMP_ITEM_MOVE_NO_MASTER>("USP_GET_ITEM_MOVE_NO_MASTER @WorkNo", _workNo).ToList();
                if(result != null && result.Count > 0)
                    MasterGridBindingSource.DataSource = result.ToList();
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