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

            MasterGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged1;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;

            dt_InDate.SetTodayIsWeek();
        }

        
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));            
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
            MasterGridExControl.MainGrid.SetRepositoryItemCodeLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.TopCategory_WAN && p.UseFlag == "Y").ToList());  // 2021-07-15 김진우 주임 SetRepositoryItemSearchLookUpEdit 에서 SetRepositoryItemCodeLookUpEdit 로 변경
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
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("OutNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

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
                CustomerCode = "CUST-00003",
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

        private void MainView_CellValueChanged1(object sender, CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            var masterObj = MasterGridBindingSource.Current as TN_ORD1400;
            if (masterObj == null) return;

            if (e.Column.FieldName == "ItemCode")
            {
                TN_STD1100 iteminfo = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == masterObj.ItemCode).FirstOrDefault();

                masterObj.TN_STD1100 = iteminfo;

                MasterGridExControl.MainGrid.BestFitColumns();
            }
        }


        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1400;
            var detailObj = DetailGridBindingSource.Current as TN_ORD1401;
            if (detailObj == null) return;
            if (e.Column.FieldName == "OutLotNo")
            {
                int cnt = ModelService.GetChildList<VI_PROD_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == detailObj.OutLotNo&&p.ItemCode==masterObj.ItemCode).Count();
                if (cnt == 0)
                {
                    var result = MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_139), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo);
                    //var result = MessageBoxHandler.Show("거래명세서에 단가를 입력하시겠습니까?", "경고", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes) { detailObj.OutLotNo = ""; }
                    
                }
            }
            if (e.Column.FieldName == "OutQty")
            {
                int cnt = ModelService.GetChildList<VI_PROD_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == detailObj.OutLotNo && p.ItemCode == masterObj.ItemCode).Count();
                if (cnt >= 1)
                {
                    decimal qty = ModelService.GetChildList<VI_PROD_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == detailObj.OutLotNo && p.ItemCode == masterObj.ItemCode).FirstOrDefault().StockQty.GetDecimalNullToZero();
                    if (qty < detailObj.OutQty)
                    {
                        var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_86), qty)+"계속하시겠습니까?" ,LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo);
                        //MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_86), stockObj.StockQty));
                        //var result = MessageBoxHandler.Show("거래명세서에 단가를 입력하시겠습니까?", "경고", MessageBoxButtons.YesNo);
                        if (result != DialogResult.Yes) { detailObj.OutQty = 0; }

                    }
                }
              
            }
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
