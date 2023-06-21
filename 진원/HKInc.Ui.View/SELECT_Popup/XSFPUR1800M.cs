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
    public partial class XSFPUR1800M : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_PUR1600_LIST> ModelService = (IService<VI_PUR1600_LIST>)ProductionFactory.GetDomainService("VI_PUR1600_LIST");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        public XSFPUR1800M()
        {
            InitializeComponent();
       

        }
        public XSFPUR1800M(PopupDataParam parameter, PopupCallback callback) :this()
        {
        
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
            this.Text = "발주목록";
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
            gridEx1.MainGrid.AddColumn("PoId", "발주자");
            gridEx1.MainGrid.AddColumn("CustCode", "거래처");
            gridEx1.MainGrid.AddColumn("InDuedate", "입고예정일");           
            gridEx1.MainGrid.AddColumn("Memo", "비고");
       
            
            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {            
            
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("PoId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            gridEx1.MainGrid.SetRepositoryItemDateEdit("PoDate");
            gridEx1.MainGrid.SetRepositoryItemDateEdit("InDuedate");

        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();

            bindingSource.DataSource = ModelService.GetList();

            
            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();
      
                if (IsmultiSelect)
                {
                    List<VI_PUR1600_LIST> itemMasterList = new List<VI_PUR1600_LIST>();

                    foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
                    {
                    VI_PUR1600_LIST tn = new VI_PUR1600_LIST();
                    tn.PoDate = Convert.ToDateTime(gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "PoDate").GetNullToEmpty()); 
                    tn.PoNo = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "PoNo").GetNullToEmpty(); 
                    tn.PoId = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "PoId").GetNullToEmpty();
                    tn.CustCode = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CustCode").GetNullToEmpty();
                    tn.InDuedate = Convert.ToDateTime(gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "InDuedate").GetNullToEmpty());
                    tn.Memo = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Memo").GetNullToEmpty();
                    tn.Temp = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp").GetNullToEmpty();
                    tn.Temp1 = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp1").GetNullToEmpty();
                    tn.Temp2 = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp2").GetNullToEmpty();
                    itemMasterList.Add(tn);
                    }
                    param.SetValue(PopupParameter.ReturnObject, itemMasterList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, (VI_PUR1600_LIST)bindingSource.Current);
                }
           
            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {

                VI_PUR1600_LIST itemMaster = (VI_PUR1600_LIST)bindingSource.Current;

                    if (IsmultiSelect)
                    {
                        List<VI_PUR1600_LIST> itemMasterList = new List<VI_PUR1600_LIST>();
                        if (itemMaster != null)
                            itemMasterList.Add(itemMaster);

                        param.SetValue(PopupParameter.ReturnObject, itemMasterList);
                    }
                    else
                    {
                        param.SetValue(PopupParameter.ReturnObject, itemMaster);
                    }
                
                ReturnPopupArgument = new PopupArgument(param);

                base.ActClose();
            }

        }
    }
}

