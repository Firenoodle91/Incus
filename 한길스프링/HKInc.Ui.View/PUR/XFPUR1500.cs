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
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.PUR
{
    //자재재고관리화면
    public partial class XFPUR1500 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PURSTOCK> ModelService = (IService<VI_PURSTOCK>)ProductionFactory.GetDomainService("VI_PURSTOCK");

        public XFPUR1500()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.DoubleClick += MainView_DoubleClick;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
        }

        protected override void InitCombo()
        {
            lupitemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype,2));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory","소분류");
            MasterGridExControl.MainGrid.AddColumn("Unit","단위");
            MasterGridExControl.MainGrid.AddColumn("SafeQty","안전재고", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("InQty","입고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OutQty","출고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("StockQty","재고수량", HorzAlignment.Far, FormatType.Numeric, "n0");

            DetailGridExControl.SetToolbarButtonVisible(false);
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "현품표출력[Alt+E]", IconImageList.GetIconImage("print/printer"));
            DetailGridExControl.MainGrid.AddColumn("Num",false);
            DetailGridExControl.MainGrid.AddColumn("Division", "구분", HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("InOutDate", "입출고일");
            DetailGridExControl.MainGrid.AddColumn("Work", "입출고자");
            DetailGridExControl.MainGrid.AddColumn("InLotNo", "입고 LOT NO");
            //DetailGridExControl.MainGrid.AddColumn("InOutDate", "입출고일");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            DetailGridExControl.MainGrid.AddColumn("InputQty", "입고량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("WhCode", "입고창고");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "입고위치");
            DetailGridExControl.MainGrid.AddColumn("OutLotNo", "출고 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("CheckResult", "수입검사");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WhCode", "WhPosition");
        }

        protected override void InitRepository()
        {
           
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 3), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
           // MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");


            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Work", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InOutDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");

        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();            
            
            string itemtype = lupitemtype.EditValue.GetNullToEmpty();
            string item = tx_Item.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p =>  (string.IsNullOrEmpty(itemtype) ? true : p.MiddleCategory == itemtype)
                                                                         && (string.IsNullOrEmpty(item) ? true : (p.ItemNm1.Contains(item) || p.ItemNm.Contains(item)))
                                                                     )
                                                                     .OrderBy(p => p.ItemNm1)
                                                                     .ToList();                                                  
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            VI_PURSTOCK obj = MasterGridBindingSource.Current as VI_PURSTOCK;
            if (obj == null)
            {
                DetailGridExControl.DataSource = null;
                return;
            }
            DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_PURINOUT>(p => p.ItemCode == obj.ItemCode).OrderBy(o => o.InOutDate).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            SetSaveMessageCheck = false;

            IService<TN_PUR1301> WMS_Service = (IService<TN_PUR1301>)ProductionFactory.GetDomainService("TN_PUR1301");
            var DetailList = DetailGridBindingSource.List as List<VI_PURINOUT>;
            if (DetailList != null)
            {
                var InList = DetailList.Where(p => p.Division == "입고" && p.EditRowFlag == "Y").ToList();
                var reList = DetailList.Where(p => p.Division == "재입고" && p.EditRowFlag == "Y").ToList();

                foreach (var v in InList)
                {
                    var upObj = WMS_Service.GetChildList<TN_PUR1301>(p => p.InputNo == v.Num && p.InputSeq == v.InputSeq).FirstOrDefault();
                    if (upObj != null)
                    {
                        SetSaveMessageCheck = true;
                        upObj.WhCode = v.WhCode;
                        upObj.WhPosition = v.WhPosition;
                        WMS_Service.UpdateChild(upObj);
                    }
                }

                foreach (var v in reList)
                {
                    var upObj = WMS_Service.GetChildList<TN_PUR1502>(p => p.RowId == v.InputSeq).FirstOrDefault();
                    if (upObj != null)
                    {
                        SetSaveMessageCheck = true;
                        upObj.WhCode = v.WhCode;
                        upObj.WhPosition = v.WhPosition;
                        WMS_Service.UpdateChild(upObj);
                    }
                }
            }

            if (SetSaveMessageCheck)
            {
                WMS_Service.Save();
                WMS_Service.Dispose();
                ActRefresh();
            }
        }

        protected override void DetailFileChooseClicked()
        {
            VI_PURINOUT obj = DetailGridBindingSource.Current as VI_PURINOUT;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            IPopupForm form;
            param.SetValue(PopupParameter.KeyValue, obj);
            form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFPUR1503, param, PopupRefreshCallback);
            form.ShowPopup(true);

        }

        private void MainView_DoubleClick(object sender, EventArgs e)
        {
            VI_PURINOUT obj = DetailGridBindingSource.Current as VI_PURINOUT;
            if (obj == null || obj.Division != "출고") return;

            PopupDataParam param = new PopupDataParam();
            IPopupForm form;
            param.SetValue(PopupParameter.KeyValue, obj);
            form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFPUR1502, param, PopupRefreshCallback);
            form.ShowPopup(true);
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            VI_PURINOUT obj = DetailGridBindingSource.Current as VI_PURINOUT;
            if (obj == null || obj.Division == "출고") e.Cancel = true;
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            VI_PURINOUT obj = DetailGridBindingSource.Current as VI_PURINOUT;
            if (obj == null) return;
            if (e.Column.FieldName == "WhCode" || e.Column.FieldName == "WhPosition")
                obj.EditRowFlag = "Y";
        }
        
        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            VI_PURINOUT obj = DetailGridBindingSource.Current as VI_PURINOUT;
            if (obj == null) return;
            if (obj.Division == "재입고")
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, true);
            else
                DetailGridExControl.SetToolbarButtonEnable(GridToolbarButton.FileChoose, false);
        }
    }
}
