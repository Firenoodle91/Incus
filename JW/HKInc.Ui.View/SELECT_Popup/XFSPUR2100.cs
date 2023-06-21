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
    public partial class XFSPUR2100 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_PUR2100> ModelService = (IService<TN_PUR2100>)ProductionFactory.GetDomainService("TN_PUR2100");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        public XFSPUR2100()
        {
            InitializeComponent();
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-20);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(20);
        }
        public XFSPUR2100(PopupDataParam parameter, PopupCallback callback) :this()
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
            gridEx1.MainGrid.AddColumn("PoNo", "발주번호");
            gridEx1.MainGrid.AddColumn("PoDate", "발주일");
            gridEx1.MainGrid.AddColumn("DueDate", "납기예정일");
            gridEx1.MainGrid.AddColumn("PoId", "발주자");
            gridEx1.MainGrid.AddColumn("CustomerCode", "거래처");
            gridEx1.MainGrid.AddColumn("Memo");
            gridEx1.MainGrid.AddColumn("PoFlag", "발주확정");
           

            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("PoId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            //gridEx1.MainGrid.SetRepositoryItemDateEdit("ReqDate");
            //gridEx1.MainGrid.SetRepositoryItemDateEdit("DueDate");
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("PoFlag", "N");
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


            bindingSource.DataSource = ModelService.GetList(p => (p.PoDate >= dp_date.DateFrEdit.DateTime.Date && p.PoDate <= dp_date.DateToEdit.DateTime.Date) && (string.IsNullOrEmpty(cust) ? true : p.CustomerCode == cust) &&p.PoFlag=="Y").OrderBy(o => o.CustomerCode).OrderBy(o => o.PoDate).ToList();

            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                TN_PUR2100 obj = bindingSource.Current as TN_PUR2100;

                var rtnList = new List<TN_PUR2100>();

                rtnList.Add(obj);

                param.SetValue(PopupParameter.ReturnObject, rtnList);
                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            List<TN_PUR2100> rtnList = new List<TN_PUR2100>();

            var list = bindingSource.List as List<TN_PUR2100>;
            var checkList = list.Where(x => x._Check == "Y").ToList();

            rtnList = checkList;

            param.SetValue(PopupParameter.ReturnObject, rtnList);
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        //protected override void Confirm()
        //{
        //    PopupDataParam param = new PopupDataParam();

        //    string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();

        //    if (IsmultiSelect)
        //    {
        //        List<TN_PUR2100> Pur1100List = new List<TN_PUR2100>();

        //        foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
        //        {
        //            string Reqno = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ReqNo").GetNullToEmpty();

        //            TN_PUR2100 pur1100 = ModelService.GetList(p => p.ReqNo == Reqno).FirstOrDefault();
        //            Pur1100List.Add(ModelService.Detached(pur1100));
        //        }
        //        param.SetValue(PopupParameter.ReturnObject, Pur1100List);
        //    }
        //    else
        //    {
        //        param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_PUR2100)bindingSource.Current));
        //    }

        //    ReturnPopupArgument = new PopupArgument(param);
        //    base.Close();
        //}

        //private void MainView_RowClick(object sender, RowClickEventArgs e)
        //{
        //    PopupDataParam param = new PopupDataParam();

        //    if (e.Clicks == 2)
        //    {

        //        TN_PUR2100 Pur1100 = (TN_PUR2100)bindingSource.Current;

        //        if (IsmultiSelect)
        //        {
        //            List<TN_PUR2100> Pur1100List = new List<TN_PUR2100>();
        //            if (Pur1100 != null)
        //                Pur1100List.Add(ModelService.Detached(Pur1100));

        //            param.SetValue(PopupParameter.ReturnObject, Pur1100List);
        //        }
        //        else
        //        {
        //            param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(Pur1100));
        //        }

        //        ReturnPopupArgument = new PopupArgument(param);

        //        base.ActClose();
        //    }

        //}
    }
}

