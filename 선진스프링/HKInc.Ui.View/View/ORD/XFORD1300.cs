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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 제품기타입고관리화면
    /// </summary>
    public partial class XFORD1300 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1300> ModelService = (IService<TN_ORD1300>)ProductionFactory.GetDomainService("TN_ORD1300");

        public XFORD1300()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            MasterGridExControl.MainGrid.MainView.CellValueChanged += MasterMainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
        }
        
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("InDate", LabelConvert.GetLabelText("InDate"));
            MasterGridExControl.MainGrid.AddColumn("InId", LabelConvert.GetLabelText("InId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CustomerCode", "ItemCode", "InDate", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1300>(MasterGridExControl);

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"), false);
            DetailGridExControl.MainGrid.AddColumn("InSeq", LabelConvert.GetLabelText("InSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            DetailGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"));
            DetailGridExControl.MainGrid.AddColumn("InWhCode", LabelConvert.GetLabelText("InWhCode"));
            DetailGridExControl.MainGrid.AddColumn("InWhPosition", LabelConvert.GetLabelText("InWhPosition"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InLotNo", "InQty", "Memo", "InWhCode", "InWhPosition");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1301>(DetailGridExControl);

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.TopCategory_WAN && p.UseFlag == "Y").ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));

            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("InQty");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhCode", ModelService.GetChildList<TN_WMS1000>(p => (p.Temp == MasterCodeSTR.WhCodeDivision_WAN || p.Temp == null) && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);

            var WhPositionEdit = DetailGridExControl.MainGrid.Columns["InWhPosition"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            WhPositionEdit.Popup += WhPositionEdit_Popup;
        }

        protected override void InitCombo()
        {
            dt_InDate.SetTodayIsWeek();
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string customerCode = lup_CustomerCode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.InDate >= dt_InDate.DateFrEdit.DateTime
                                                                            && p.InDate <= dt_InDate.DateToEdit.DateTime)
                                                                        && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                     )
                                                                     .OrderBy(p => p.InDate)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            var obj = MasterGridBindingSource.Current as TN_ORD1300;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.TN_ORD1301List.OrderBy(o => o.InSeq).ToList();
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
            TN_ORD1300 obj = new TN_ORD1300()
            {
                InNo = DbRequestHandler.GetSeqMonth("EIN"),
                InDate = DateTime.Today,
                InId = Utils.Common.GlobalVariable.LoginId,
            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }

        protected override void DeleteRow()
        {
            TN_ORD1300 tn = MasterGridBindingSource.Current as TN_ORD1300;
            if (tn == null) return;

            if (tn.TN_ORD1301List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("InMasterInfo"), LabelConvert.GetLabelText("InDetailInfo"), LabelConvert.GetLabelText("InDetailInfo")));
            }
            else
            {
                MasterGridBindingSource.RemoveCurrent();
                ModelService.Delete(tn);
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_ORD1300 obj = MasterGridBindingSource.Current as TN_ORD1300;
            if (obj == null) return;
            TN_ORD1301 newobj = new TN_ORD1301()
            {
                InNo = obj.InNo,
                InSeq = obj.TN_ORD1301List.Count == 0 ? 1 : obj.TN_ORD1301List.Max(o => o.InSeq) + 1,
                InQty = 0
            };
            DetailGridBindingSource.Add(newobj);
            obj.TN_ORD1301List.Add(newobj);
        }

        protected override void DeleteDetailRow()
        {
            TN_ORD1300 obj = MasterGridBindingSource.Current as TN_ORD1300;
            if (obj == null) return;
            TN_ORD1301 delobj = DetailGridBindingSource.Current as TN_ORD1301;
            if (delobj == null) return;

            obj.TN_ORD1301List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void MasterMainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TN_ORD1300 obj = MasterGridBindingSource.Current as TN_ORD1300;
            if (obj == null) return;

            //if (e.Column.FieldName == "ItemCode")
            //{
            //    foreach (var v in obj.TN_ORD1301List)
            //    {
            //        v.ItemCode = obj.ItemCode;
            //    }
            //}
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TN_ORD1300 obj = MasterGridBindingSource.Current as TN_ORD1300;
            if (obj == null) return;
            var detailObj = DetailGridBindingSource.Current as TN_ORD1301;
            if (detailObj == null) return;

            if (e.Column.FieldName == "InWhCode")
            {
                detailObj.InWhPosition = null;
            }
            else if (e.Column.FieldName == "InLotNo")
            {
                if (!detailObj.InLotNo.IsNullOrEmpty())
                {
                    if (ModelService.GetChildList<TN_ORD1301>(p => p.InLotNo == detailObj.InLotNo && p.TN_ORD1300.ItemCode != obj.ItemCode).Count > 0)
                    {
                        MessageBoxHandler.Show("해당 LOT NO는 다른 품목으로 이미 등록되어 있기 때문에 사용하실 수 없습니다.");
                        detailObj.InLotNo = null;
                    }
                }
            }
        }

        private void MainView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedColumn.FieldName == "InLotNo")
            {
                TN_ORD1300 obj = MasterGridBindingSource.Current as TN_ORD1300;
                if (obj == null) e.Cancel = true;
                else
                {
                    if (obj.ItemCode.IsNullOrEmpty())
                    {
                        MessageBoxHandler.Show("마스터 그리드의 품목을 먼저 선택해 주시기 바랍니다.");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void WhPositionEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_ORD1301;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.InWhCode + "'";
        }
    }
}
