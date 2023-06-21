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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;
using System.Data.SqlClient;

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 자재발주마스터 Select 팝업
    /// </summary>
    public partial class XSFPUR1100_NEW : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_PUR_SCM> ModelService = (IService<VI_PUR_SCM>)ProductionFactory.GetDomainService("VI_PUR_SCM");
        private bool IsmultiSelect = true;

        public XSFPUR1100_NEW()
        {
            InitializeComponent();
        }

        public XSFPUR1100_NEW(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("PoMasterInfo");

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitBindingSource() { }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            dt_PoDate.SetTodayIsMonth();

            lup_PoCustomer.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Purchase || p.CustomerType == null)).ToList());
            lup_PoId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.ScmYn == "N" && p.Active == "Y" && p.MainYn == "02").ToList());
        }
        
        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "PoNo", true);
            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            GridExControl.MainGrid.AddColumn("ScmYn", LabelConvert.GetLabelText("ScmYn"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"));
            GridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"));
            GridExControl.MainGrid.AddColumn("PoCustomerCode", LabelConvert.GetLabelText("PoCustomer"));
            GridExControl.MainGrid.AddColumn("PoDate", LabelConvert.GetLabelText("PoDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("PoId", LabelConvert.GetLabelText("PoId"));
            GridExControl.MainGrid.AddColumn("DueDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("InCustomerDate", LabelConvert.GetLabelText("InCustomerDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("InCustomerId", LabelConvert.GetLabelText("InCustomerId"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.AddColumn("Memo1", LabelConvert.GetLabelText("CustomerMemo"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoCustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InCustomerId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");            
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
            GridExControl.MainGrid.MainView.Columns["Memo1"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo1");
            GridExControl.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string customerCode = lup_PoCustomer.EditValue.GetNullToEmpty();
            string poId = lup_PoId.EditValue.GetNullToEmpty();

            ModelBindingSource.DataSource = ModelService.GetList(p => (p.PoDate >= dt_PoDate.DateFrEdit.DateTime && p.PoDate <= dt_PoDate.DateToEdit.DateTime)
                                                                   && (string.IsNullOrEmpty(customerCode) ? true : p.PoCustomerCode == customerCode)
                                                                   && (string.IsNullOrEmpty(poId) ? true : p.PoId == poId)                                                                 
                                                                   //&& p.InCustConfirmFlag == "Y"
                                                                )
                                                                .Where(p => p.InConfirmState != MasterCodeSTR.MaterialInConfirmFlag_End)
                                                                .OrderBy(o => o.PoDate)
                                                                .ThenBy(o => o.PoNo)
                                                                .ToList();







            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();
        }

        protected override void Confirm()
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (IsmultiSelect)
            {
                var returnList = new List<VI_PUR_SCM>();
                var dataList = ModelBindingSource.List as List<VI_PUR_SCM>;
                var checkList = dataList.Where(p => p._Check == "Y").ToList();
                foreach (var v in checkList)
                {
                    returnList.Add(ModelService.Detached(v));
                }
                param.SetValue(PopupParameter.ReturnObject, returnList);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_PUR_SCM)ModelBindingSource.Current));
            }
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var poMaster = (VI_PUR_SCM)ModelBindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<VI_PUR_SCM>();
                    if (poMaster != null)
                        returnList.Add(ModelService.Detached(poMaster));
                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(poMaster));
                }

                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }

        }
    }
}