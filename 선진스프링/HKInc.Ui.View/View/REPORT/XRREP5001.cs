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

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 일별매출관리
    /// </summary>
    public partial class XRREP5001 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XRREP5001()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            lup_Manager.EditValueChanged += Lup_Manager_EditValueChanged;
            lup_CustomerCode.Popup += Lup_CustomerCode_Popup;

            GridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        protected override void InitCombo()
        {
            dt_BillDate.SetFormat(DateFormat.Month);
            dt_BillDate.DateTime = DateTime.Today;
            dt_BillDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            lup_Manager.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());

            //dt_SalesDate.DateFrEdit.SetDeleteButton();
            //dt_SalesDate.DateToEdit.SetDeleteButton();

            dt_SalesDate.EditFrValue = null;
            dt_SalesDate.EditToValue = null;
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("SalesConfirmFlag", "매출 확정");
            GridExControl.MainGrid.AddColumn("BillDate", LabelConvert.GetLabelText("BillDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM");
            GridExControl.MainGrid.AddColumn("SalesDate", LabelConvert.GetLabelText("SalesDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ManagerId", LabelConvert.GetLabelText("ManagerName")); 
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));       
            GridExControl.MainGrid.AddColumn("OrderCost", LabelConvert.GetLabelText("Cost"));
            GridExControl.MainGrid.AddColumn("Qty", LabelConvert.GetLabelText("Qty"));
            GridExControl.MainGrid.AddColumn("Sales", LabelConvert.GetLabelText("Sales"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "SalesConfirmFlag");

            GridExControl.MainGrid.ShowFooter = true;
            GridExControl.MainGrid.MainView.Columns["Sales"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["Sales"].SummaryItem.FieldName = "Sales";
            GridExControl.MainGrid.MainView.Columns["Sales"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["Qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["Qty"].SummaryItem.FieldName = "Qty";
            GridExControl.MainGrid.MainView.Columns["Qty"].SummaryItem.DisplayFormat = "{0:#,0.##}";
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("SalesConfirmFlag", "N");
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

                bool salesDateFlag = false;
                if (dt_SalesDate.DateFrEdit.DateTime.GetNullToDateTime() != null && dt_SalesDate.DateToEdit.DateTime.GetNullToDateTime() != null)
                    salesDateFlag = true;

                var result = context.Database
                      .SqlQuery<TEMP_XRREP5000>("USP_GET_XRREP5001_LIST @BillDate, @ManagerId, @CutomerCode", billDate, managerId, customerCode)             
                      .Where(p=> salesDateFlag ? (dt_SalesDate.DateFrEdit.DateTime <= p.SalesDate && dt_SalesDate.DateToEdit.DateTime >= p.SalesDate) : true)
                      .OrderBy(p => p.BillDate)
                      .ThenBy(p => p.SalesDate)
                      .ToList();

                GridBindingSource.DataSource = result;
            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.MainGrid.BestFitColumns();
            SetRefreshMessage(GridExControl);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            if (GridBindingSource.DataSource != null)
            {
                var list = GridBindingSource.List as List<TEMP_XRREP5000>;
                var editList = list.Where(p => p.EditRowFlag == "Y").ToList();
                if (editList.Count > 0)
                {
                    foreach (var v in editList)
                    {
                        var checkObj = ModelService.GetChildList<TN_ORD1600>(p => p.SalesNo == v.SalesNo).FirstOrDefault();
                        if (checkObj == null)
                        {
                            ModelService.InsertChild(new TN_ORD1600()
                            {
                                SalesNo = v.SalesNo,
                                SalesConfirmFlag = v.SalesConfirmFlag
                            });
                        }
                        else
                        {
                            checkObj.SalesConfirmFlag = v.SalesConfirmFlag;
                            checkObj.UpdateId = GlobalVariable.LoginId;
                            checkObj.UpdateTime = DateTime.Now;
                            ModelService.UpdateChild(checkObj);
                        }
                    }
                }
            }

            ModelService.Save();
            DataLoad();
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

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var obj = GridBindingSource.Current as TEMP_XRREP5000;
            if (obj != null)
            {
                obj.EditRowFlag = "Y";
            }
        }
    }
}