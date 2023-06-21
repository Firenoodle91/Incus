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
    public partial class XSFQCT : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_MPS1401LIST> ModelService = (IService<VI_MPS1401LIST>)ProductionFactory.GetDomainService("VI_MPS1401LIST");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        private string qctype = string.Empty;

        public XSFQCT()
        {
            InitializeComponent();
            dp_workdt.DateFrEdit.DateTime = DateTime.Today.AddDays(-20);
            dp_workdt.DateToEdit.DateTime = DateTime.Today.AddDays(20);
        }

        public XSFQCT(PopupDataParam parameter, PopupCallback callback) :this()
        {
            dp_workdt.DateFrEdit.DateTime = DateTime.Today.AddDays(-20);
            dp_workdt.DateToEdit.DateTime = DateTime.Today.AddDays(20);
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Text);

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);
            if (parameter.ContainsKey(PopupParameter.Value_1))
                qctype= parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();

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
            this.Text = "품목정보";
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.Init();
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("WorkDate");
            gridEx1.MainGrid.AddColumn("WorkNo");
            gridEx1.MainGrid.AddColumn("Seq");
            gridEx1.MainGrid.AddColumn("ItemCode");
            gridEx1.MainGrid.AddColumn("ItemNm","품목명");
            gridEx1.MainGrid.AddColumn("ItemNm1","품번");
            gridEx1.MainGrid.AddColumn("ProcessCode");
            gridEx1.MainGrid.AddColumn("ProcessTurn", LabelConvert.GetLabelText("ProcessOrder"));
            gridEx1.MainGrid.AddColumn("LotNo");
            gridEx1.MainGrid.AddColumn("ResultDate");
            gridEx1.MainGrid.AddColumn("ResultQty");
            gridEx1.MainGrid.AddColumn("OkQty");
            gridEx1.MainGrid.AddColumn("WorkId");
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            gridEx1.MainGrid.SetRepositoryItemDateEdit("ResultDate");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => true), "LoginId", "UserName");
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            string ItemCode = tx_itemcode.EditValue.GetNullToEmpty();

            if (qctype == "Q05") {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(ItemCode) ? true : p.ItemCode.Contains(ItemCode) || (string.IsNullOrEmpty(ItemCode) ? true : p.ItemNm.Contains(ItemCode)))
                                                                  //(string.IsNullOrEmpty(ItemCode) ? true : p.ItemCode == ItemCode)
                                                                  && (p.ResultDate >= dp_workdt.DateFrEdit.DateTime && p.ResultDate <= dp_workdt.DateToEdit.DateTime)
                                                                  && p.ProcessCode == "P09"
                                                                ).OrderBy(p => p.ResultDate).ToList();
            }
            if (qctype == MasterCodeSTR.Process_Inspection)             // 2022-01-14 김진우 대리 공정검사관리 추가
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(ItemCode) ? true : p.ItemCode.Contains(ItemCode) || (string.IsNullOrEmpty(ItemCode) ? true : p.ItemNm.Contains(ItemCode)))
                                                  //(string.IsNullOrEmpty(ItemCode) ? true : p.ItemCode == ItemCode) 
                                                                    && (p.ResultDate >= dp_workdt.DateFrEdit.DateTime && p.ResultDate <= dp_workdt.DateToEdit.DateTime)
                                                                    ).OrderBy(p => p.ResultDate).ToList();
            }
            else
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(ItemCode) ? true : p.ItemCode.Contains(ItemCode) || (string.IsNullOrEmpty(ItemCode) ? true : p.ItemNm.Contains(ItemCode)))
                                                                     //(string.IsNullOrEmpty(ItemCode) ? true : p.ItemCode == ItemCode) 
                                                                  && (p.ResultDate >= dp_workdt.DateFrEdit.DateTime && p.ResultDate <= dp_workdt.DateToEdit.DateTime)
                                                                  && (string.IsNullOrEmpty(qctype) ? true : p.ProcessCode == "P06")
                                                               ).OrderBy(p => p.ResultDate).ToList();
            }
            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();
      
                    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_MPS1401LIST)bindingSource.Current));
                
           
            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_MPS1401LIST)bindingSource.Current));
                ReturnPopupArgument = new PopupArgument(param);

                base.ActClose();
            }

        }
    }
}

