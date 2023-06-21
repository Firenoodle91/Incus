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
using System.Data.SqlClient;
using HKInc.Service.Service;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 재공재고현황
    /// </summary>
    public partial class XFMPS1700 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFMPS1700()
        {
            InitializeComponent();
            GridExControl = gridEx1;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetList(p => p.UseFlag == "Y" && 
                (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN )).ToList()); // 20210524 오세완 차장 반제품(자사) 만 출력 
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            GridExControl.MainGrid.MainView.Columns.Clear();
            ModelService.ReLoad();
            // 20210624 오세완 차장 김이사님 지시로 공통코드 추가시 화면 재출력 없이 하기 위해서 추가
            InitCombo();

            var ItemCode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
            //var ds = DbRequestHandler.GetDataSet("USP_GET_XFMPS1700_LIST", ItemCode);
            var ds = DbRequestHandler.GetDataSet("USP_GET_XFMPS1700_LIST_V2", ItemCode); // 20210616 오세완 차장 품목명 추가로 변경처리

            SetColumn(ds);

            GridExControl.DataSource = ds.Tables[0];
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
        }

        protected override void GridRowDoubleClicked(){}

        private void SetColumn(DataSet ds)
        {
            GridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1")); // 20210616 오세완 차장 품목명 추가
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));

            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));

            var cultureIndex = DataConvert.GetCultureIndex();
            var processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                var columnName = ds.Tables[0].Columns[i].ColumnName.GetNullToEmpty();
                if (columnName.Left(3) == "PPP")
                {
                    if (columnName.Right(4) == "Wait")
                    {
                        var processCode = columnName.Substring(4, columnName.Substring(4).IndexOf('_'));
                        var processObj = processList.Where(p => p.CodeVal == processCode).FirstOrDefault();
                        if (processObj != null)
                        {
                            var processName = cultureIndex == 1 ? processObj.CodeName : (cultureIndex == 2 ? processObj.CodeNameENG : processObj.CodeNameCHN);
                            GridExControl.MainGrid.AddColumn(columnName, processName + LabelConvert.GetLabelText("Wait"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
                        }
                        else
                        {
                            GridExControl.MainGrid.AddColumn(columnName, columnName, HorzAlignment.Far, FormatType.Numeric, "#,0.##");
                        }
                    }
                    else
                    {
                        var processCode = columnName.Substring(4);
                        var processObj = processList.Where(p => p.CodeVal == processCode).FirstOrDefault();
                        if (processObj != null)
                        {
                            var processName = cultureIndex == 1 ? processObj.CodeName : (cultureIndex == 2 ? processObj.CodeNameENG : processObj.CodeNameCHN);
                            GridExControl.MainGrid.AddColumn(columnName, processName + LabelConvert.GetLabelText("Ing"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
                        }
                        else
                        {
                            GridExControl.MainGrid.AddColumn(columnName, columnName, HorzAlignment.Far, FormatType.Numeric, "#,0.##");
                        }
                    }
                }
            }

            GridExControl.MainGrid.Columns["WorkNo"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            GridExControl.MainGrid.Columns["ItemCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            GridExControl.MainGrid.Columns[DataConvert.GetCultureDataFieldName("ItemName")].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            GridExControl.MainGrid.Columns["ItemName1"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left; // 20210616 오세완 차장 중간에 추가를 해도 고정은 추가를 해야 같이 처리됨
            GridExControl.MainGrid.Columns["CustomerCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
        }

    }
}
