using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;

using HKInc.Ui.Model.Domain;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Base;

namespace HKInc.Ui.View.SELECT_Popup
{
    public partial class XFSORD1100 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_ORD1100> ModelService = (IService<TN_ORD1100>)ProductionFactory.GetDomainService("TN_ORD1100");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        private bool ItemOutCheck = false; //외주가공품 팝업 확인 

        public XFSORD1100()
        {
            InitializeComponent();

            gridEx1.MainGrid.MainView.CustomColumnDisplayText += MainViewCustomDisplayText;

            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-20);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(20);
        }

        private void MainViewCustomDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            GridView gv = (GridView)sender;

            if (e.Column.Name == "OutRemainQty")
            {
                string orderNo = gv.GetListSourceRowCellValue(e.ListSourceRowIndex, "OrderNo").GetNullToEmpty();
                int orderSeq = gv.GetListSourceRowCellValue(e.ListSourceRowIndex, "Seq").GetIntNullToZero();
                string delivSeq = gv.GetListSourceRowCellValue(e.ListSourceRowIndex, "DelivSeq").GetNullToEmpty();

                decimal? sumOutQty = ModelService.GetChildList<TN_ORD1200>(x => x.OrderNo == orderNo && x.OrderSeq == orderSeq && x.DelivSeq == delivSeq).ToList().Sum(s => s.OutQty);
                decimal delivQty = gv.GetListSourceRowCellValue(e.ListSourceRowIndex, "DelivQty").GetIntNullToZero();

                e.DisplayText = (delivQty - sumOutQty).ToString();
            }
        }

        public XFSORD1100(PopupDataParam parameter, PopupCallback callback) :this()
        {
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-20);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(20);
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Text);
            
            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            if (parameter.ContainsKey(PopupParameter.Constraint))
                DepartmentCode = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            if (parameter.ContainsKey(PopupParameter.Value_1))
                ItemOutCheck = true;

        }

        


        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();

            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitControls()
        {
            base.InitControls();
            this.Text = "납품정보";
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.Init();
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridEx1.MainGrid.AddColumn("OrderNo", "주문번호");
            gridEx1.MainGrid.AddColumn("Seq", "주문순번");
            gridEx1.MainGrid.AddColumn("DelivSeq", "납품계획번호");
            gridEx1.MainGrid.AddColumn("DelivDate", "납기예정일");
            gridEx1.MainGrid.AddColumn("ItemCode", "품목");
            gridEx1.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            gridEx1.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            gridEx1.MainGrid.AddColumn("TN_STD1100.Lctype", "기종");
            gridEx1.MainGrid.AddColumn("TN_STD1100.Temp5", "팀");
            gridEx1.MainGrid.AddColumn("DelivQty", "납품수량");
            gridEx1.MainGrid.AddColumn("Temp", "거래처");
            gridEx1.MainGrid.AddColumn("DelivId", "수주담당자");
            gridEx1.MainGrid.AddColumn("OutRemainQty", "미납품량");

            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Temp5", DbRequesHandler.GetCommCode(MasterCodeSTR.tem), "Mcode", "Codename");

        }
        protected override void InitCombo()
        {
            lupcust.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());

        }
        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            string cust = lupcust.EditValue.GetNullToEmpty();

            if (!ItemOutCheck)
                bindingSource.DataSource = ModelService.GetList(p => (p.DelivDate >= dp_date.DateFrEdit.DateTime.Date && p.DelivDate <= dp_date.DateToEdit.DateTime.Date)
                && (string.IsNullOrEmpty(cust) ? true : p.Temp == cust) && p.OutDate == null && p.OutConfirmflag == "Y")
                //&& p.TN_STD1100.TopCategory != MasterCodeSTR.ITEM_TYPE_OUT)
                    .OrderBy(o => o.DelivDate).ToList();
            else
            {
                //외주가공품
                bindingSource.DataSource = ModelService.GetList(p => (p.DelivDate >= dp_date.DateFrEdit.DateTime.Date && p.DelivDate <= dp_date.DateToEdit.DateTime.Date) 
                                && (string.IsNullOrEmpty(cust) ? true : p.Temp == cust) 
                                && p.OutDate == null && p.OutConfirmflag == "Y"
                                && p.TN_STD1100.TopCategory == MasterCodeSTR.ITEM_TYPE_OUT)
                                .OrderBy(o => o.DelivDate).ToList();
            }

            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            if (IsmultiSelect)
            {
                List<TN_ORD1100> Pur1100List = new List<TN_ORD1100>();

                foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
                {
                    string DelivSeq = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "DelivSeq").GetNullToEmpty();

                    TN_ORD1100 pur1100 = ModelService.GetList(p => p.DelivSeq == DelivSeq).FirstOrDefault();
                    Pur1100List.Add(ModelService.Detached(pur1100));
                }
                param.SetValue(PopupParameter.ReturnObject, Pur1100List);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_ORD1100)bindingSource.Current));
            }

            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {

                TN_ORD1100 Pur1100 = (TN_ORD1100)bindingSource.Current;

                if (IsmultiSelect)
                {
                    List<TN_ORD1100> Pur1100List = new List<TN_ORD1100>();
                    if (Pur1100 != null)
                        Pur1100List.Add(ModelService.Detached(Pur1100));

                    param.SetValue(PopupParameter.ReturnObject, Pur1100List);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(Pur1100));
                }

                ReturnPopupArgument = new PopupArgument(param);

                base.ActClose();
            }

        }
    }
}

