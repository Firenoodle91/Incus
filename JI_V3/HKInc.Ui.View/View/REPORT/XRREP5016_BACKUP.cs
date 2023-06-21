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

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 초중종 검사 체크시트
    /// </summary>
    public partial class XRREP5016_BACK : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1101> ModelService = (IService<TN_QCT1101>)ProductionFactory.GetDomainService("TN_QCT1101");

        public XRREP5016_BACK()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            dtEdit1.DateTime = DateTime.Today;

            GridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;

            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("CHECK_WAY"))
                {
                    //var ChckWay = view.GetRowCellValue(e.RowHandle, view.Columns[""])
                    //if (e.CellValue.ToString() == "OK")
                    //{

                    //}
                }
            }

        }

        protected override void InitCombo()
        {
            lup_MachineCode.SetDefault(true, "MachineCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
            lup_ItemCode.SetDefault(true, "ItemCode", "ItemName", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("INSP_NO", LabelConvert.GetLabelText("INSP_NO"), false);
            GridExControl.MainGrid.AddColumn("ID_TIME", LabelConvert.GetLabelText("ID_TIME"), false);
            GridExControl.MainGrid.AddColumn("MACHINE_CODE", LabelConvert.GetLabelText("MACHINE_CODE"));
            GridExControl.MainGrid.AddColumn("CUSTOMER", LabelConvert.GetLabelText("CUSTOMER"));
            GridExControl.MainGrid.AddColumn("CAR", LabelConvert.GetLabelText("CAR"));
            GridExControl.MainGrid.AddColumn("ITEM_CODE", LabelConvert.GetLabelText("ITEM_CODE"));
            GridExControl.MainGrid.AddColumn("PROCESS", LabelConvert.GetLabelText("PROCESS"));
            GridExControl.MainGrid.AddColumn("SPC_FLAG", LabelConvert.GetLabelText("SPC_FLAG"));
            GridExControl.MainGrid.AddColumn("CHECK_LIST", LabelConvert.GetLabelText("CHECK_LIST"));
            GridExControl.MainGrid.AddColumn("CHECK_SPEC", LabelConvert.GetLabelText("CHECK_SPEC"));
            GridExControl.MainGrid.AddColumn("CHECK_WAY", LabelConvert.GetLabelText("CHECK_WAY"));
            GridExControl.MainGrid.AddColumn("CHECK_MAX", LabelConvert.GetLabelText("CHECK_MAX"));
            GridExControl.MainGrid.AddColumn("CHECK_MIN", LabelConvert.GetLabelText("CHECK_MIN"));
            GridExControl.MainGrid.AddColumn("CHECK_POINT", LabelConvert.GetLabelText("CHECK_POINT"));
            GridExControl.MainGrid.AddColumn("INFO", LabelConvert.GetLabelText("INFO"));
            GridExControl.MainGrid.AddColumn("a01", LabelConvert.GetLabelText("01"));
            GridExControl.MainGrid.AddColumn("a02", LabelConvert.GetLabelText("02"));
            GridExControl.MainGrid.AddColumn("a03", LabelConvert.GetLabelText("03"));
            GridExControl.MainGrid.AddColumn("a04", LabelConvert.GetLabelText("04"));
            GridExControl.MainGrid.AddColumn("a05", LabelConvert.GetLabelText("05"));
            GridExControl.MainGrid.AddColumn("a06", LabelConvert.GetLabelText("06"));
            GridExControl.MainGrid.AddColumn("a07", LabelConvert.GetLabelText("07"));
            GridExControl.MainGrid.AddColumn("a08", LabelConvert.GetLabelText("08"));
            GridExControl.MainGrid.AddColumn("a09", LabelConvert.GetLabelText("09"));
            GridExControl.MainGrid.AddColumn("a10", LabelConvert.GetLabelText("10"));
            GridExControl.MainGrid.AddColumn("a11", LabelConvert.GetLabelText("11"));
            GridExControl.MainGrid.AddColumn("a12", LabelConvert.GetLabelText("12"));
            GridExControl.MainGrid.AddColumn("a13", LabelConvert.GetLabelText("13"));
            GridExControl.MainGrid.AddColumn("a14", LabelConvert.GetLabelText("14"));
            GridExControl.MainGrid.AddColumn("a15", LabelConvert.GetLabelText("15"));
            GridExControl.MainGrid.AddColumn("a16", LabelConvert.GetLabelText("16"));
            GridExControl.MainGrid.AddColumn("a17", LabelConvert.GetLabelText("17"));
            GridExControl.MainGrid.AddColumn("a18", LabelConvert.GetLabelText("18"));
            GridExControl.MainGrid.AddColumn("a19", LabelConvert.GetLabelText("19"));
            GridExControl.MainGrid.AddColumn("a20", LabelConvert.GetLabelText("20"));
            GridExControl.MainGrid.AddColumn("a21", LabelConvert.GetLabelText("21"));
            GridExControl.MainGrid.AddColumn("a22", LabelConvert.GetLabelText("22"));
            GridExControl.MainGrid.AddColumn("a23", LabelConvert.GetLabelText("23"));
            GridExControl.MainGrid.AddColumn("a24", LabelConvert.GetLabelText("24"));
            GridExControl.MainGrid.AddColumn("a25", LabelConvert.GetLabelText("25"));
            GridExControl.MainGrid.AddColumn("a26", LabelConvert.GetLabelText("26"));
            GridExControl.MainGrid.AddColumn("a27", LabelConvert.GetLabelText("27"));
            GridExControl.MainGrid.AddColumn("a28", LabelConvert.GetLabelText("28"));
            GridExControl.MainGrid.AddColumn("a29", LabelConvert.GetLabelText("29"));
            GridExControl.MainGrid.AddColumn("a30", LabelConvert.GetLabelText("30"));
            GridExControl.MainGrid.AddColumn("a31", LabelConvert.GetLabelText("31"));

            GridExControl.MainGrid.MainView.OptionsView.AllowCellMerge = true;
            GridExControl.MainGrid.MainView.Columns["MACHINE_CODE"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            GridExControl.MainGrid.MainView.Columns["CUSTOMER"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            GridExControl.MainGrid.MainView.Columns["CAR"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            GridExControl.MainGrid.MainView.Columns["ITEM_CODE"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            GridExControl.MainGrid.MainView.Columns["PROCESS"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            GridExControl.MainGrid.MainView.Columns["CHECK_POINT"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            GridExControl.MainGrid.MainView.Columns["INFO"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PROCESS", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CHECK_POINT", DbRequestHandler.GetCommTopCode(MasterCodeSTR.CheckPoint), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CHECK_WAY", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("SPC_FLAG", "N");
        }

        protected override void DataLoad()
        {
            //ModelService.ReLoad();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var Date = new SqlParameter("@Date", dtEdit1.DateTime.ToString("yyyy-MM"));
                var MachineCode = new SqlParameter("@MachineCode", lup_MachineCode.EditValue.GetNullToEmpty());
                var ItemCode = new SqlParameter("@ItemCode", lup_ItemCode.EditValue.GetNullToEmpty());
                var Customer = new SqlParameter("@Customer", lup_CustomerCode.EditValue.GetNullToEmpty());

                //GridBindingSource.DataSource = context.Database.SqlQuery<TEMP_XRREP5016>("USP_GET_XRREP5016 @Date", Date).ToList();
                GridBindingSource.DataSource = context.Database.SqlQuery<TEMP_XRREP5016>("USP_GET_XRREP5016 @Date, @MachineCode, @ItemCode, @Customer", Date, MachineCode, ItemCode, Customer).ToList();
            }
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            SetRefreshMessage(GridExControl);
        }

    }
}