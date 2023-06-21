﻿using System;
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

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 월별매출관리
    /// </summary>
    public partial class XRREP5000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XRREP5000()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            lup_Manager.EditValueChanged += Lup_Manager_EditValueChanged;
            lup_CustomerCode.Popup += Lup_CustomerCode_Popup;
        }

        protected override void InitCombo()
        {
            dt_BillDate.SetFormat(DateFormat.Month);
            dt_BillDate.DateTime = DateTime.Today;
            dt_BillDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            lup_Manager.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ManagerId", LabelConvert.GetLabelText("ManagerName")); 
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));       
            GridExControl.MainGrid.AddColumn("OrderCost", LabelConvert.GetLabelText("Cost"));
            GridExControl.MainGrid.AddColumn("Qty", LabelConvert.GetLabelText("Qty"));
            GridExControl.MainGrid.AddColumn("Sales", LabelConvert.GetLabelText("Sales"));
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ManagerId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemName", ModelService.GetChildList<TN_STD1100>(p => 1 == 1).ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderCost", DefaultBoolean.Default, "n2");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("Qty", DefaultBoolean.Default, "n0");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("Sales", DefaultBoolean.Default, "n0");

            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var billDate = new SqlParameter("@BillDate", dt_BillDate.DateTime);
                var managerId = new SqlParameter("@ManagerId", lup_Manager.EditValue.GetNullToEmpty());
                var customerCode = new SqlParameter("@CutomerCode", lup_CustomerCode.EditValue.GetNullToEmpty());

                var result = context.Database
                      .SqlQuery<TEMP_XRREP5000>("USP_GET_XRREP5000_LIST @BillDate, @ManagerId, @CutomerCode", billDate, managerId, customerCode).ToList();

                GridBindingSource.DataSource = result;
            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.MainGrid.BestFitColumns();
            SetRefreshMessage(GridExControl);
        }

        private void Lup_Manager_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            lup_CustomerCode.EditValue = null;
        }

        private void Lup_CustomerCode_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var manager = lup_Manager.EditValue.GetNullToNull();

            if (manager.IsNullOrEmpty())
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            else
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[BusinessManagementId] = '" + manager + "'";
        }
    }
}