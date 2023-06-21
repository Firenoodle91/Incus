using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;
using HKInc.Ui.Model.BaseDomain;
using HKInc.Service.Helper;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 제품기타출고관리화면
    /// </summary>
    public partial class XFORD1400 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1400> ModelService = (IService<TN_ORD1400>)ProductionFactory.GetDomainService("TN_ORD1400");

        public XFORD1400()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("ReqQty", LabelConvert.GetLabelText("ReqQty"));
            MasterGridExControl.MainGrid.AddColumn("OutDate", LabelConvert.GetLabelText("OutDate"));
            MasterGridExControl.MainGrid.AddColumn("OutId", LabelConvert.GetLabelText("OutId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CustomerCode", "ItemCode", "ReqQty", "OutDate", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1400>(MasterGridExControl);

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"), false);
            DetailGridExControl.MainGrid.AddColumn("OutSeq", LabelConvert.GetLabelText("OutSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutLotNo", LabelConvert.GetLabelText("OutLotNo"));
            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"));
            DetailGridExControl.MainGrid.AddColumn("WhCode", LabelConvert.GetLabelText("WhCode"));
            DetailGridExControl.MainGrid.AddColumn("PositionCode", LabelConvert.GetLabelText("PositionName"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutLotNo", "OutQty", "Memo", "PositionCode", "WhCode");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1401>(DetailGridExControl);

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.TopCategory_WAN && p.UseFlag == "Y").ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("ReqQty");

            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OutQty");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => (p.Temp == MasterCodeSTR.WhCodeDivision_WAN || p.Temp == null) && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PositionCode", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName");

            var PositionCodeEdit = DetailGridExControl.MainGrid.Columns["PositionCode"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            PositionCodeEdit.Popup += PositionCodeEdit_Popup;
        }

        protected override void InitCombo()
        {
            dt_InDate.SetTodayIsWeek();
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("OutNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string customerCode = lup_CustomerCode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutDate >= dt_InDate.DateFrEdit.DateTime
                                                                            && p.OutDate <= dt_InDate.DateToEdit.DateTime)
                                                                        && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                     )
                                                                     .OrderBy(p => p.OutDate)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            var obj = MasterGridBindingSource.Current as TN_ORD1400;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.TN_ORD1401List.OrderBy(o => o.OutSeq).ToList();
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
            TN_ORD1400 obj = new TN_ORD1400()
            {
                OutNo = DbRequestHandler.GetSeqMonth("EOUT"),
                OutDate = DateTime.Today,
                OutId = Utils.Common.GlobalVariable.LoginId,
            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }

        protected override void DeleteRow()
        {
            TN_ORD1400 tn = MasterGridBindingSource.Current as TN_ORD1400;
            if (tn == null) return;

            if (tn.TN_ORD1401List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("OutMasterInfo"), LabelConvert.GetLabelText("OutDetailInfo"), LabelConvert.GetLabelText("OutDetailInfo")));
            }
            else
            {
                MasterGridBindingSource.RemoveCurrent();
                ModelService.Delete(tn);
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_ORD1400 obj = MasterGridBindingSource.Current as TN_ORD1400;
            if (obj == null) return;
            TN_ORD1401 newobj = new TN_ORD1401()
            {
                OutNo = obj.OutNo,
                OutSeq = obj.TN_ORD1401List.Count == 0 ? 1 : obj.TN_ORD1401List.Max(o => o.OutSeq) + 1,
                OutQty = 0
            };
            DetailGridBindingSource.Add(newobj);
            obj.TN_ORD1401List.Add(newobj);
        }

        protected override void DeleteDetailRow()
        {
            TN_ORD1400 obj = MasterGridBindingSource.Current as TN_ORD1400;
            if (obj == null) return;
            TN_ORD1401 delobj = DetailGridBindingSource.Current as TN_ORD1401;
            if (delobj == null) return;

            obj.TN_ORD1401List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_ORD1401;
            if (detailObj == null) return;

            if (e.Column.FieldName == "WhCode")
            {
                detailObj.PositionCode = null;
            }
        }

        private void PositionCodeEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_ORD1401;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.WhCode + "'";
        }
    }
}
