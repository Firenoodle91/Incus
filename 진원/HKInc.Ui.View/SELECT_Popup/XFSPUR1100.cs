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

namespace HKInc.Ui.View.SELECT_Popup
{
    public partial class XFSPUR1100 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_PUR1100> ModelService = (IService<TN_PUR1100>)ProductionFactory.GetDomainService("TN_PUR1100");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        public XFSPUR1100()
        {
            InitializeComponent();
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-20);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(20);
        }
        public XFSPUR1100(PopupDataParam parameter, PopupCallback callback) :this()
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
            this.Text = "발주정보";
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {
            
            gridEx1.MainGrid.Init();
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridEx1.MainGrid.AddColumn("ReqNo", "발주번호");
            gridEx1.MainGrid.AddColumn("ReqDate", "발주일");
            gridEx1.MainGrid.AddColumn("DueDate", "납기예정일");
            gridEx1.MainGrid.AddColumn("ReqId", "발주자");
            gridEx1.MainGrid.AddColumn("CustomerCode", "거래처");
            gridEx1.MainGrid.AddColumn("Memo");
            gridEx1.MainGrid.AddColumn("Temp2", "발주확정");
           

            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ReqId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            gridEx1.MainGrid.SetRepositoryItemDateEdit("ReqDate");
            gridEx1.MainGrid.SetRepositoryItemDateEdit("DueDate");
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("Temp2", "N");
           




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


            bindingSource.DataSource = ModelService.GetList(p => (p.ReqDate >= dp_date.DateFrEdit.DateTime.Date && p.ReqDate <= dp_date.DateToEdit.DateTime.Date) && (string.IsNullOrEmpty(cust) ? true : p.CustomerCode == cust)&&p.Temp2=="Y").OrderBy(o => o.CustomerCode).OrderBy(o => o.ReqDate).ToList();

            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            if (IsmultiSelect)
            {
                List<TN_PUR1100> Pur1100List = new List<TN_PUR1100>();

                foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
                {
                    string Reqno = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ReqNo").GetNullToEmpty();

                    TN_PUR1100 pur1100 = ModelService.GetList(p => p.ReqNo == Reqno).FirstOrDefault();
                    Pur1100List.Add(ModelService.Detached(pur1100));
                }
                param.SetValue(PopupParameter.ReturnObject, Pur1100List);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_PUR1100)bindingSource.Current));
            }

            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {

                TN_PUR1100 Pur1100 = (TN_PUR1100)bindingSource.Current;

                if (IsmultiSelect)
                {
                    List<TN_PUR1100> Pur1100List = new List<TN_PUR1100>();
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

