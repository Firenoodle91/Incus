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
using HKInc.Ui.Model.Domain.TEMP;
using System.Data.SqlClient;
using HKInc.Service.Helper;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 영업판매계획관리
    /// </summary>
    public partial class XFORD1600 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_ORD1500> ModelService = (IService<TN_ORD1500>)ProductionFactory.GetDomainService("TN_ORD1500");
        DateTime? searchDate;

        public XFORD1600()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            GridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            GridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;

            lup_Manager.EditValueChanged += Lup_Manager_EditValueChanged;
            lup_Customer.Popup += Lup_CustomerCode_Popup;
            dt_date.SetFormat(DateFormat.Month);
            dt_date.DateTime = DateTime.Today.AddDays(1 - DateTime.Today.Day);
            dt_date.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

        }

        protected override void InitCombo()
        {
            lup_Manager.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<VI_BUSINESS_MANAGEMENT_USER>(p => 1 == 1).ToList());
            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("QtyMon3", LabelConvert.GetLabelText("SalesQtyMon3"));
            GridExControl.MainGrid.AddColumn("QtyMon2", LabelConvert.GetLabelText("SalesQtyMon2"));
            GridExControl.MainGrid.AddColumn("QtyMon1", LabelConvert.GetLabelText("SalesQtyMon1"));
            GridExControl.MainGrid.AddColumn("QtyMonAvg", LabelConvert.GetLabelText("SalesQtyAvg"));
            GridExControl.MainGrid.AddColumn("PlanQty", LabelConvert.GetLabelText("SalesPlanQty"));
            GridExControl.MainGrid.AddColumn("Cost", LabelConvert.GetLabelText("Cost"));
            GridExControl.MainGrid.AddUnboundColumn("Amt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([PlanQty],0) * ISNULL([Cost],0)", FormatType.Numeric, "#,###,###,###.##");
            GridExControl.MainGrid.AddColumn("ResultQty", LabelConvert.GetLabelText("Result"));
            GridExControl.MainGrid.AddColumn("Decision", LabelConvert.GetLabelText("Decision"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1500>(GridExControl);

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PlanQty", "Cost", "Decision", "Memo");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("QtyMon3", DefaultBoolean.Default, "n0");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("QtyMon2", DefaultBoolean.Default, "n0");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("QtyMon1", DefaultBoolean.Default, "n0");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("QtyMonAvg", DefaultBoolean.Default, "n2");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("PlanQty", DefaultBoolean.Default, "n0");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("ResultQty", DefaultBoolean.Default, "n0");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("Cost", DefaultBoolean.Default, "n2");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Decision", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            var custCode = lup_Customer.EditValue.GetNullToEmpty();

            if (custCode.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("Customer")));
                return;
            }

            GridRowLocator.GetCurrentRow("ItemCode");

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            searchDate = dt_date.DateTime;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var date = new SqlParameter("@VarDt", searchDate);
                var customerCode = new SqlParameter("@CustomerCode", lup_Customer.EditValue);

                var result = context.Database
                      .SqlQuery<TEMP_XFORD1600>("USP_GET_XFORD1600_LIST @VarDt, @CustomerCode", date, customerCode).ToList();

                GridBindingSource.DataSource = result;
            }
            GridExControl.DataSource = GridBindingSource;
            GridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(GridExControl);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            if (GridBindingSource != null && GridBindingSource.DataSource != null)
            {
                var masterList = GridBindingSource.List as List<TEMP_XFORD1600>;
                if (masterList.Count > 0)
                {
                    foreach(var list in masterList.Where(p => p.Decision == "Y"))
                    {
                        TN_ORD1500 old = ModelService.GetList(p => p.SalePlanDate == dt_date.DateTime && p.CustomerCode == list.MainCustomerCode && p.ItemCode == list.ItemCode).FirstOrDefault();
                                                
                        if (old == null) //신규등록
                        {
                            var newObj = new TN_ORD1500()
                            {
                                SalePlanDate = dt_date.DateTime,
                                CustomerCode = list.MainCustomerCode,
                                ItemCode = list.ItemCode,
                                PlanQty = list.PlanQty.GetDecimalNullToZero(),
                                Cost = list.Cost,
                                Memo = list.Memo
                            };
                            ModelService.Insert(newObj);
                        }
                        else //수정
                        {
                            old.PlanQty = list.PlanQty.GetDecimalNullToZero();
                            old.Cost = list.Cost;
                            old.Memo = list.Memo;
                            old.UpdateId = GsValue.UserId;
                            old.UpdateTime = DateTime.Now;
                        }
                    }

                    ModelService.Save();
                }
            }
            DataLoad();
        }

        private void Lup_Manager_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            lup_Customer.EditValue = null;
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

        private void MainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0 && (e.Column.FieldName == "PlanQty" || e.Column.FieldName == "Cost" || e.Column.FieldName == "Decision" || e.Column.FieldName == "Memo"))
            {
                if (searchDate == null)
                {
                    e.Appearance.BackColor = Color.Empty;
                }
                else
                {
                    var searchDateToString = ((DateTime)searchDate).ToString("yyyyMM");
                    var TodayDateToString = DateTime.Today.ToString("yyyyMM");
                    if (searchDateToString.GetDecimalNullToZero() < TodayDateToString.GetDecimalNullToZero())
                    {
                        e.Appearance.BackColor = Color.Empty;
                    }
                    else
                    {
                        e.Appearance.BackColor = GlobalVariable.GridEditableColumnColor;
                    }
                }
            }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (searchDate == null)
            {
                e.Cancel = true;
            }
            else
            {
                var searchDateToString = ((DateTime)searchDate).ToString("yyyyMM");
                var TodayDateToString = DateTime.Today.ToString("yyyyMM");
                if (searchDateToString.GetDecimalNullToZero() < TodayDateToString.GetDecimalNullToZero())
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }
    }
}