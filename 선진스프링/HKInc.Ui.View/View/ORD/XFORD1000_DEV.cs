using System;
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

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 개발의뢰관리
    /// </summary>
    public partial class XFORD1000_DEV : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1000> ModelService = (IService<TN_ORD1000>)ProductionFactory.GetDomainService("TN_ORD1000");

        public XFORD1000_DEV()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            dt_OrderDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dt_OrderDate.DateToEdit.DateTime = DateTime.Today;

            MasterGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("ReqDevelopmentNo"));
            MasterGridExControl.MainGrid.AddColumn("OrderDate", LabelConvert.GetLabelText("ReqDevelopmentDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderCustomerCode", LabelConvert.GetLabelText("CustomerCode"), false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("OrderManagerName", LabelConvert.GetLabelText("OrderManagerName"));
            MasterGridExControl.MainGrid.AddColumn("OrderDueDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderId", LabelConvert.GetLabelText("OrderId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"), false);
            DetailGridExControl.MainGrid.AddColumn("OrderSeq", LabelConvert.GetLabelText("Seq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CarType", LabelConvert.GetLabelText("CarType"));
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("OrderQty", LabelConvert.GetLabelText("ReqDevelopmentQty"));
            DetailGridExControl.MainGrid.AddColumn("OrderCost", LabelConvert.GetLabelText("Cost"));
            DetailGridExControl.MainGrid.AddUnboundColumn("OrderAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([OrderQty],0) * ISNULL([OrderCost],0)", FormatType.Numeric, "#,###,###,###.##");
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OrderQty", "OrderCost", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1001>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OrderId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.CarType", DbRequestHandler.GetCommCode(MasterCodeSTR.CarType, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderQty", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderCost", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
        }

        protected override void InitCombo()
        {
            lup_OrderCustomerCode.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("OrderNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string customerCode = lup_OrderCustomerCode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OrderDate >= dt_OrderDate.DateFrEdit.DateTime
                                                                         && p.OrderDate <= dt_OrderDate.DateToEdit.DateTime)
                                                                         && (string.IsNullOrEmpty(customerCode) ? true : p.OrderCustomerCode == customerCode)
                                                                         && (p.OrderType == "개발")
                                                                      )
                                                                      .OrderBy(p => p.OrderNo)
                                                                      .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = masterObj.TN_ORD1001List.OrderBy(p => p.OrderSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            MasterGridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);
            param.SetValue(PopupParameter.Value_1, "개발");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000_DEV, param, AddRowPopupCallback);

            form.ShowPopup(true);
        }

        private void AddRowPopupCallback(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null || e.Map == null)
            {
                ActRefresh();
            }
            else
            {
                PopupDataParam.SetValue(PopupParameter.GridRowId_1, e.Map.GetValue(PopupParameter.GridRowId_1));
                ActRefresh();
            }
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            if (masterObj.TN_ORD1001List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("ReqDevelopmentMasterInfo"), LabelConvert.GetLabelText("ReqDevelopmentDetailInfo"), LabelConvert.GetLabelText("ReqDevelopmentDetailInfo")));
                return;
            }
            ModelService.Delete(masterObj);
            MasterGridBindingSource.RemoveCurrent();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Constraint, MasterCodeSTR.Contraint_ItemWAN);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_STD1100, param, AddOrderDetailCallback);
            form.ShowPopup(true);
        }

        private void AddOrderDetailCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            var returnList = (List<TN_STD1100>)e.Map.GetValue(PopupParameter.ReturnObject);
            foreach (var v in returnList)
            {
                var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();
                var newObj = new TN_ORD1001();
                newObj.OrderNo = masterObj.OrderNo;
                newObj.OrderSeq = masterObj.TN_ORD1001List.Count == 0 ? 1 : masterObj.TN_ORD1001List.Max(p => p.OrderSeq) + 1;
                newObj.ItemCode = item.ItemCode;
                newObj.ProductionFlag = "N";
                newObj.OutConfirmFlag = "Y";
                newObj.TurnKeyFlag = "N";
                newObj.NewRowFlag = "Y";
                newObj.TN_STD1100 = item;
                masterObj.TN_ORD1001List.Add(newObj);
                DetailGridBindingSource.Add(newObj);
            }
            if (returnList.Count > 0) SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (detailObj == null) return;

            if (detailObj.TN_ORD1100List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("ReqDevelopmentDetailInfo"), LabelConvert.GetLabelText("DevelopmentDelivInfo"), LabelConvert.GetLabelText("DevelopmentDelivInfo")));
                return;
            }

            masterObj.TN_ORD1001List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.BestFitColumns();
        }

        private void MainView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
                if (masterObj == null) return;

                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Service, ModelService);
                param.SetValue(PopupParameter.KeyValue, masterObj);
                param.SetValue(PopupParameter.UserRight, UserRight);
                param.SetValue(PopupParameter.EditMode, PopupEditMode.Update);

                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000_DEV, param, PopupRefreshCallback);

                form.ShowPopup(true);
            }
        }
    }
}