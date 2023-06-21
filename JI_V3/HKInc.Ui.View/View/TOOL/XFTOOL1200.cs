using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Common;
using HKInc.Service.Handler;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.View.TOOL
{
    /// <summary>
    /// 공구관리 → 공구 수명이력관리(작업지시 기준)
    /// </summary>
    public partial class XFTOOL1200 : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_TOOL1100> ModelService = (IService<TN_TOOL1100>)ProductionFactory.GetDomainService("TN_TOOL1100");

        public XFTOOL1200()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            //dt_OrderDate.SetTodayIsMonth(2);
            dt_OrderDate.SetTolerance(1);

        }

        protected override void InitCombo()
        {
            lup_Machine.SetDefault(false, true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").OrderBy(o => o.MachineCode).ToList());
            lup_Item.SetDefault(false, true, "ItemCode", "ItemName", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).OrderBy(o => o.ItemCode).ToList());
            lup_Process.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process));
            lup_Tool.SetDefault(false, true, "ToolCode", "ToolName", ModelService.GetChildList<TN_TOOL1000>(p => p.UseFlag == "Y").OrderBy(o => o.ToolCode).ToList());
            lup_User.SetDefault(false, true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            MasterGridExControl.MainGrid.AddColumn("WorkDate", "작업지시일");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목", false);
            MasterGridExControl.MainGrid.AddColumn("ItemName", "품명");

            DetailGridExControl.MainGrid.AddColumn("MachineCode", "설비명");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품명");
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            DetailGridExControl.MainGrid.AddColumn("ToolCode", "공구명");
            DetailGridExControl.MainGrid.AddColumn("UserId", "작업자");

            DetailGridExControl.MainGrid.AddColumn("BaseCNT", "공구기초수명", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("UseCNT", "공구사용수명", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("LifeCNT", "공구잔여수명", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ChangeDate", "공구교체일시", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm:ss");
            DetailGridExControl.MainGrid.AddColumn("Seq", "교체횟수", HorzAlignment.Center, FormatType.Numeric, "n0");

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => true).ToList(), "MachineMCode", "MachineName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemCode", "ItemName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ToolCode", ModelService.GetChildList<TN_TOOL1000>(p => p.UseFlag == "Y" ).ToList(), "ToolCode", "ToolName");


            //DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("BaseCNT", DefaultBoolean.Default, "n2");

        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();  
            InitRepository();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;

                var startdate = new SqlParameter("@StartDate", dt_OrderDate.DateFrEdit.DateTime.ToString("yyyy-MM-dd"));
                var enddate = new SqlParameter("@EndDate", dt_OrderDate.DateToEdit.DateTime.ToString("yyyy-MM-dd"));
                var itemcode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());

                var result = context.Database.SqlQuery<TEMP_XFTOOL1200_MASTER>
                    ("USP_GET_XFTOOL1200_MASTER @StartDate, @EndDate, @ItemCode", startdate, enddate, itemcode).ToList();

                MasterGridBindingSource.DataSource = result.ToList();
                MasterGridExControl.DataSource = MasterGridBindingSource;
                MasterGridExControl.BestFitColumns();

            }
            

            GridRowLocator.SetCurrentRow();
            
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();

            var masterObj = MasterGridBindingSource.Current as TEMP_XFTOOL1200_MASTER;

            if (masterObj == null) return;

            
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;

                var workno = new SqlParameter("@WorkNo", masterObj.WorkNo);
                var machincode = new SqlParameter("@MachineCode", lup_Machine.EditValue.GetNullToEmpty());
                var itemcode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
                var processcode = new SqlParameter("@ProcessCode", lup_Process.EditValue.GetNullToEmpty());
                var toolcode = new SqlParameter("@ToolCode", lup_Tool.EditValue.GetNullToEmpty());
                var userid = new SqlParameter("@UserId", lup_User.EditValue.GetNullToEmpty());

                var result = context.Database.SqlQuery<TEMP_XFTOOL1200_DETAIL>
                    ("USP_GET_XFTOOL1200_DETAIL @WorkNo, @MachineCode, @ItemCode, @ProcessCode, @ToolCode, @UserId", workno, machincode, itemcode, processcode, toolcode, userid).ToList();

                DetailGridBindingSource.DataSource = result.ToList();
                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.BestFitColumns();

            }
            
        }
        


    }
}