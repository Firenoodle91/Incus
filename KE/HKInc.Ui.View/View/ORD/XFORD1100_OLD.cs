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
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 납품계획관리화면
    /// </summary>
    public partial class XFORD1100_OLD : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_ORD1000> ModelService = (IService<TN_ORD1000>)ProductionFactory.GetDomainService("TN_ORD1000");

        public XFORD1100_OLD()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            SubDetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;

            btn_OrderStatus.Click += Btn_OrdStatus_Click;

            dt_OrderDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            dt_OrderDate.DateToEdit.DateTime = DateTime.Today.AddDays(+30);
        }

        protected override void InitCombo()
        {
            lup_OrderCustomerCode.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());

            btn_OrderStatus.Text = LabelConvert.GetLabelText("OrderStatus") + "(&O)";
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            MasterGridExControl.MainGrid.AddColumn("OrderDate", LabelConvert.GetLabelText("OrderDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderCustomerCode", LabelConvert.GetLabelText("OrderCustomerCode"), false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("OrderCustomer"));
            MasterGridExControl.MainGrid.AddColumn("OrderManagerName", LabelConvert.GetLabelText("OrderManagerName"));
            MasterGridExControl.MainGrid.AddColumn("OrderDueDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderId", LabelConvert.GetLabelText("OrderId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"), false);
            DetailGridExControl.MainGrid.AddColumn("DelivFlag", LabelConvert.GetLabelText("DelivFlag"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("OrderSeq", LabelConvert.GetLabelText("OrderSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CarType", LabelConvert.GetLabelText("CarType"));
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("OrderQty", LabelConvert.GetLabelText("OrderQty"));
            DetailGridExControl.MainGrid.AddColumn("OrderCost", LabelConvert.GetLabelText("Cost"));
            DetailGridExControl.MainGrid.AddUnboundColumn("OrderAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([OrderQty],0) * ISNULL([OrderCost],0)", FormatType.Numeric, "#,###,###,###.##");
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            
            SubDetailGridExControl.MainGrid.AddColumn("DelivNo", LabelConvert.GetLabelText("DelivNo"), false);
            SubDetailGridExControl.MainGrid.AddColumn("DelivDate", LabelConvert.GetLabelText("DelivDate"));
            SubDetailGridExControl.MainGrid.AddColumn("DelivQty", LabelConvert.GetLabelText("DelivQty"));
            SubDetailGridExControl.MainGrid.AddColumn("DelivId", LabelConvert.GetLabelText("DelivId"));
            SubDetailGridExControl.MainGrid.AddColumn("ProductionFlag", LabelConvert.GetLabelText("ProductionFlag"));
            SubDetailGridExControl.MainGrid.AddColumn("TurnKeyFlag", LabelConvert.GetLabelText("TurnKeyFlag"),false);
            SubDetailGridExControl.MainGrid.AddColumn("OutConfirmFlag", LabelConvert.GetLabelText("OutConfirmFlag"));
            SubDetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DelivDate", "DelivQty", "ProductionFlag", "TurnKeyFlag", "OutConfirmFlag", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1100>(SubDetailGridExControl);
        }

        protected override void InitRepository()
        {
            var UserList = ModelService.GetChildList<User>(p => p.Active != "N").ToList();
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OrderId", UserList, "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.CarType", DbRequestHandler.GetCommCode(MasterCodeSTR.CarType, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderQty", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderCost", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo");

            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("DelivDate");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("DelivQty", DefaultBoolean.Default, "n2");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivId", UserList, "LoginId", "UserName");
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("ProductionFlag", "N");
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutConfirmFlag", "N");
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("TurnKeyFlag", "N");
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(SubDetailGridExControl, "Memo", UserRight.HasEdit);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("OrderNo");
            DetailGridRowLocator.GetCurrentRow("OrderSeq");
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            string orderCustomerCode = lup_OrderCustomerCode.EditValue.GetNullToEmpty();
            string itemCode = lup_Item.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(orderCustomerCode) ? true : p.OrderCustomerCode == orderCustomerCode)
                                                                        && (string.IsNullOrEmpty(itemCode) ? true : (p.TN_ORD1001List.Any(c => c.TN_STD1100.ItemCode == itemCode)))
                                                                        && (p.OrderDate >= dt_OrderDate.DateFrEdit.DateTime
                                                                         && p.OrderDate <= dt_OrderDate.DateToEdit.DateTime)
                                                                        && (p.OrderType == "양산")
                                                                     )
                                                                     .OrderBy(p => p.OrderNo)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            var MasterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (MasterObj == null)
            {
                return;
            }
            DetailGridBindingSource.DataSource = MasterObj.TN_ORD1001List.OrderBy(o => o.OrderSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

            if (MasterObj.TN_ORD1001List.Count > 0) DetailFocusedRowChanged();
        }

        protected override void DetailFocusedRowChanged()
        {
            var DetailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (DetailObj == null)
            {
                SubDetailGridExControl.MainGrid.Clear();
                return;
            }

            SubDetailGridBindingSource.DataSource = DetailObj.TN_ORD1100List.OrderBy(o => o.DelivNo).ToList();
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void SubDetailAddRowClicked()
        {
            var DetailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (DetailObj == null) return;

            var delivQty = DetailObj.TN_ORD1100List.Count == 0 ? DetailObj.OrderQty : DetailObj.OrderQty - DetailObj.TN_ORD1100List.Sum(p => p.DelivQty);
            var obj = new TN_ORD1100()
            {
                OrderNo = DetailObj.OrderNo,
                OrderSeq = DetailObj.OrderSeq,
                DelivNo = DbRequestHandler.GetSeqMonth("DLV"),
                ItemCode = DetailObj.ItemCode,
                CustomerCode = DetailObj.TN_ORD1000.OrderCustomerCode,
                DelivDate = DateTime.Today,
                DelivQty = 0,
                DelivId = GlobalVariable.LoginId,
                ProductionFlag = "Y",
                OutConfirmFlag = "Y",
                TurnKeyFlag = "N",
                Memo= DetailObj.Memo.GetNullToEmpty()
            };

            if (DetailObj.TN_ORD1100List.Count == 0)
                obj.DelivQty = DetailObj.OrderQty;
            else
            {
                var checkQty = DetailObj.OrderQty - DetailObj.TN_ORD1100List.Sum(p => p.DelivQty);
                if (checkQty > 0)
                    obj.DelivQty = checkQty;
            }

            SubDetailGridBindingSource.Add(obj);
            DetailObj.TN_ORD1100List.Add(obj);
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void DeleteSubDetailRow()
        {
            var DetailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (DetailObj == null) return;

            var SubObj = SubDetailGridBindingSource.Current as TN_ORD1100;
            if (SubObj == null) return;

            if (SubObj.TN_ORD1200List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("DelivInfo"), LabelConvert.GetLabelText("OutInfo"), LabelConvert.GetLabelText("OutInfo")));
                return;
            }
            else if (SubObj.TN_MPS1100List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("DelivInfo"), LabelConvert.GetLabelText("PlanInfo"), LabelConvert.GetLabelText("PlanInfo")));
                return;
            }
            //else if (SubObj.TN_PUR1600List.Count > 0)
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("DelivInfo"), LabelConvert.GetLabelText("TurnKeyInfo"), LabelConvert.GetLabelText("TurnKeyInfo")));
            //    return;
            //}


            DetailObj.TN_ORD1100List.Remove(SubObj);
            SubDetailGridBindingSource.RemoveCurrent();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            var FieldName = view.FocusedColumn.FieldName;
            var SubObj = SubDetailGridBindingSource.Current as TN_ORD1100;
            if (FieldName == "ProductionFlag")
            {
                if (SubObj.TurnKeyFlag == "Y")
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_55), LabelConvert.GetLabelText("TurnKeyFlag")));
                    e.Cancel = true;
                }
            }
            else if (FieldName == "TurnKeyFlag")
            {
                if (SubObj.ProductionFlag == "Y")
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_55), LabelConvert.GetLabelText("ProductionFlag")));
                    e.Cancel = true;
                }
                //else if (SubObj.TN_PUR1600List.Count > 0)
                //{
                //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_52), LabelConvert.GetLabelText("TurnKeyInfo")));
                //    e.Cancel = true;
                //}
            }
        }
        
        /// <summary> 수주현황 </summary>
        private void Btn_OrdStatus_Click(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_ORD_STATUS, param, null);
            form.ShowPopup(false);
        }
    }
}