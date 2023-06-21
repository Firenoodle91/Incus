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
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 개발생산계획관리화면
    /// </summary>
    public partial class XFMPS1100_DEV : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1100> ModelService = (IService<TN_ORD1100>)ProductionFactory.GetDomainService("TN_ORD1100");

        public XFMPS1100_DEV()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;

            dt_deliv.SetTodayIsMonth();

            OutPutRadioGroup = radioGroup1;
            RadioGroupType = RadioGroupType.XFMPS1100;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("PlanFlag", LabelConvert.GetLabelText("PlanFlag"), HorzAlignment.Center, true);
            //MasterGridExControl.MainGrid.AddColumn("TN_ORD1001.TN_ORD1000.OrderType", LabelConvert.GetLabelText("OrderType"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("DelivNo", LabelConvert.GetLabelText("DelivNo"));
            MasterGridExControl.MainGrid.AddColumn("DelivDate", LabelConvert.GetLabelText("DelivDate"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("DelivQty", LabelConvert.GetLabelText("DelivQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
            MasterGridExControl.MainGrid.AddColumn("DelivId", LabelConvert.GetLabelText("DelivId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.MainGrid.AddColumn("WorkOrderFlag", LabelConvert.GetLabelText("WorkOrderFlag"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("PlanNo", LabelConvert.GetLabelText("PlanNo"));
            DetailGridExControl.MainGrid.AddColumn("PlanQty", LabelConvert.GetLabelText("PlanQty"));
            DetailGridExControl.MainGrid.AddColumn("PlanStartDate", LabelConvert.GetLabelText("PlanStartDate"));
            DetailGridExControl.MainGrid.AddColumn("PlanEndDate", LabelConvert.GetLabelText("PlanEndDate"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PlanQty", "PlanStartDate", "PlanEndDate", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MPS1100>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");

            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanStartDate");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanEndDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("PlanQty", DefaultBoolean.Default, "n2");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("DelivNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string itemCode = lup_Item.EditValue.GetNullToEmpty();
            var radioValue = OutPutRadioGroup.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.DelivDate >= dt_deliv.DateFrEdit.DateTime
                                                                        && p.DelivDate <= dt_deliv.DateToEdit.DateTime)
                                                                        && (string.IsNullOrEmpty(itemCode) ? true : (p.ItemCode == itemCode))
                                                                        && (p.ProductionFlag == "Y")
                                                                        && (p.TN_ORD1001.TN_ORD1000.OrderType == "개발")
                                                                     )
                                                                     .Where(p => radioValue == "A" ? true : p.PlanFlag == radioValue)
                                                                     .OrderBy(p => p.DelivDate)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            var obj = MasterGridBindingSource.Current as TN_ORD1100;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.TN_MPS1100List.OrderBy(o => o.PlanNo).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
        }

        protected override void DetailAddRowClicked()
        {
            var obj = MasterGridBindingSource.Current as TN_ORD1100;
            if (obj == null) return;

            var newObj = new TN_MPS1100()
            {
                PlanNo = DbRequestHandler.GetSeqMonth("WP_D"),
                OrderNo = obj.OrderNo,
                OrderSeq = obj.OrderSeq,
                DelivNo = obj.DelivNo,
                ItemCode = obj.ItemCode,
                CustomerCode = obj.CustomerCode,
                PlanQty = obj.DelivQty,
                PlanStartDate = DateTime.Today,
                PlanEndDate = DateTime.Today,
                Memo = obj.Memo.GetNullToEmpty()
            };
            DetailGridBindingSource.Add(newObj);
            obj.TN_MPS1100List.Add(newObj);
        }

        protected override void DeleteDetailRow()
        {
            var MasterObj = MasterGridBindingSource.Current as TN_ORD1100;
            if (MasterObj == null) return;

            var obj = DetailGridBindingSource.Current as TN_MPS1100;
            if (obj == null) return;

            if (obj.TN_MPS1200List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("PlanInfo"), LabelConvert.GetLabelText("WorkInfo"), LabelConvert.GetLabelText("WorkInfo")));
                return;
            }

            DetailGridBindingSource.RemoveCurrent();
            if (obj.RowId > 0)
                ModelService.RemoveChild(obj);
            else
                MasterObj.TN_MPS1100List.Remove(obj);
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var obj = DetailGridBindingSource.Current as TN_MPS1100;

            if (obj.WorkOrderFlag == "Y")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_52), LabelConvert.GetLabelText("WorkInfo")));
                e.Cancel = true;
            }
        }

    }
}