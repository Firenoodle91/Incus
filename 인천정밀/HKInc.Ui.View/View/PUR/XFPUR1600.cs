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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Service.Helper;
using DevExpress.Utils;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Enum;
using HKInc.Service.Handler;
using HKInc.Service.Service;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.View.PUR
{
    /// <summary>
    /// 턴키 발주및입고관리화면
    /// </summary>
    public partial class XFPUR1600 : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR1600> ModelService = (IService<TN_PUR1600>)ProductionFactory.GetDomainService("TN_PUR1600");

        public XFPUR1600()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dt_date.SetTodayIsMonth();
        }

        protected override void InitCombo()
        {
            cbo_Division.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbo_Division.Properties.Items.Add(LabelConvert.GetLabelText("PoDate"));
            cbo_Division.Properties.Items.Add(LabelConvert.GetLabelText("InDate"));
            cbo_Division.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            cbo_Division.Properties.AppearanceDropDown.TextOptions.HAlignment = HorzAlignment.Center;
            cbo_Division.SelectedIndex = 0;

            lup_OrderCustomer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());          
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("OrderNo");
            MasterGridExControl.MainGrid.AddColumn("OrderSeq", LabelConvert.GetLabelText("OrderSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DelivNo");
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.CustomerCode", LabelConvert.GetLabelText("OrderCustomerCode"), false);
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("OrderCustomer"));
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            MasterGridExControl.MainGrid.AddColumn("PoNo");
            MasterGridExControl.MainGrid.AddColumn("PoDate");
            MasterGridExControl.MainGrid.AddColumn("PoId");
            MasterGridExControl.MainGrid.AddColumn("PoQty");
            MasterGridExControl.MainGrid.AddColumn("PoRemainQty", LabelConvert.GetLabelText("PoRemainQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
            MasterGridExControl.MainGrid.AddColumn("PoCost");
            MasterGridExControl.MainGrid.AddUnboundColumn("PoAmt", LabelConvert.GetLabelText("PoAmt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([PoQty],0) * ISNULL([PoCost],0)", FormatType.Numeric, "n2");
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PoDate", "PoQty", "PoCost", "Memo");

            DetailGridExControl.MainGrid.AddColumn("PoNo", false);
            DetailGridExControl.MainGrid.AddColumn("InLotNo");
            DetailGridExControl.MainGrid.AddColumn("InDate");
            DetailGridExControl.MainGrid.AddColumn("InQty");
            DetailGridExControl.MainGrid.AddColumn("InCost");
            DetailGridExControl.MainGrid.AddUnboundColumn("InAmt", LabelConvert.GetLabelText("InAmt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([InQty],0) * ISNULL([InCost],0)", FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InId");
            DetailGridExControl.MainGrid.AddColumn("Memo");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InDate", "InQty", "InCost", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1600>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1700>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            var UserList = ModelService.GetChildList<User>(p => p.Active == "Y").ToList();
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoId", UserList, "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PoDate");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("PoQty", DefaultBoolean.Default, "n2");
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("PoCost", DefaultBoolean.Default, "n2");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", UserList, "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InQty", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InCost", DefaultBoolean.Default, "n2");

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        { 
            GridRowLocator.GetCurrentRow("PoNo", PopupParameter.GridRowId_1);
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var customerCode = lup_OrderCustomer.EditValue.GetNullToEmpty();

            if (cbo_Division.SelectedIndex == 0)
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.TN_ORD1100.ItemCode == itemCode)
                                                                            && (string.IsNullOrEmpty(customerCode) ? true : p.TN_ORD1100.CustomerCode == customerCode)
                                                                            && (p.PoDate >= dt_date.DateFrEdit.DateTime
                                                                             && p.PoDate <= dt_date.DateToEdit.DateTime)
                                                                          )
                                                                          .OrderBy(p => p.PoNo)
                                                                          .ToList();
            }
            else
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.TN_ORD1100.ItemCode == itemCode)
                                                                            && (string.IsNullOrEmpty(customerCode) ? true : p.TN_ORD1100.CustomerCode == customerCode)
                                                                            && (p.TN_PUR1700List.Any(c => c.InDate >= dt_date.DateFrEdit.DateTime)
                                                                             && p.TN_PUR1700List.Any(c => c.InDate <= dt_date.DateToEdit.DateTime))
                                                                          )
                                                                          .OrderBy(p => p.PoNo)
                                                                          .ToList();
            }
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1600;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_PUR1700List.OrderBy(p => p.InLotNo).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Constraint, "XFPUR1600");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_ORD1100, param, AddMaster);
            form.ShowPopup(true);
        }

        private void AddMaster(object sender, PopupArgument e)
        {
            if (e == null) return;

            var returnList = (List<TN_ORD1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var v in returnList)
            {
                var newObj = new TN_PUR1600()
                {
                    PoNo = DbRequestHandler.GetSeqMonth("TKPO"),
                    PoId = GlobalVariable.LoginId,
                    PoDate = DateTime.Today,
                    PoQty = v.TurnKeyRemainQty,
                    TN_ORD1100 = ModelService.GetChildList<TN_ORD1100>(p => p.DelivNo == v.DelivNo).First(),
                };
                MasterGridBindingSource.Add(newObj);
                MasterGridBindingSource.MoveLast();
                ModelService.Insert(newObj);
                PopupDataParam.SetValue(PopupParameter.GridRowId_1, newObj.PoNo);
            }
            MasterGridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1600;
            if (masterObj == null) return;

            if (masterObj.TN_PUR1700List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("PoInfo"), LabelConvert.GetLabelText("InInfo"), LabelConvert.GetLabelText("InInfo")));
                return;
            }

            MasterGridBindingSource.RemoveCurrent();
            ModelService.Delete(masterObj);
            MasterGridExControl.BestFitColumns();
            IsFormControlChanged = true; 
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1600;
            if (masterObj == null) return;

            if (masterObj.PoRemainQty <= 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("PoRemainQty")));
                return;
            }

            var newObj = new TN_PUR1700()
            {
                InLotNo = DbRequestHandler.GetSeqMonth("TKIN"),
                PoNo = masterObj.PoNo,
                InId = GlobalVariable.LoginId,
                InQty = masterObj.PoRemainQty,
                InDate = DateTime.Today
            };

            DetailGridBindingSource.Add(newObj);
            DetailGridBindingSource.MoveLast();
            DetailGridExControl.BestFitColumns();
            masterObj.TN_PUR1700List.Add(newObj);
            IsFormControlChanged = true;
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1600;
            var detailObj = DetailGridBindingSource.Current as TN_PUR1700;
            if (masterObj == null || detailObj == null) return;

            if (ModelService.GetChildList<TN_ORD1201>(p => p.ProductLotNo == detailObj.InLotNo).FirstOrDefault() != null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_51), LabelConvert.GetLabelText("OutInfo")));
                return;
            }

            DetailGridBindingSource.RemoveCurrent();
            masterObj.TN_PUR1700List.Remove(detailObj);
            IsFormControlChanged = true;
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
        }
    }
}