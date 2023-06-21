using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using DevExpress.XtraGrid.Views.Grid;

using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain.VIEW;
using DevExpress.Utils;

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 사용자 선택 팝업창
    /// </summary>
    public partial class UserSelect : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<User> UserService;
        BindingSource bindingSource = new BindingSource();
        bool IsmultiSelect = true;
        string returnField = string.Empty;
        
        public UserSelect()
        {
            InitializeComponent();
        }

        public UserSelect(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;

            UserService = (IService<User>)ServiceFactory.GetDomainService("User");

            if (this.PopupParam.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)PopupParam.GetValue(PopupParameter.IsMultiSelect);

            if (this.PopupParam.ContainsKey(PopupParameter.Value_1))
                returnField = PopupParam.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            this.Text = LabelConvert.GetLabelText("UserList");
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
            gridEx1.MainView.RowClick += MainView_RowClick;
        }
        protected override void InitGrid()
        {
            gridEx1.Init();
            gridEx1.MultiSelect = IsmultiSelect;
            gridEx1.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            gridEx1.AddColumn("UserId");
            gridEx1.AddColumn("LoginId");
            gridEx1.AddColumn("Password", false);
            gridEx1.AddColumn("UserName");
            gridEx1.AddColumn("EmployeeNo");
            gridEx1.AddColumn("ProductTeamCode", LabelConvert.GetLabelText("ProductTeam"));
            gridEx1.AddColumn("RankCode", LabelConvert.GetLabelText("Rank"));
            gridEx1.AddColumn("Email");
            gridEx1.AddColumn("CellPhone", LabelConvert.GetLabelText("PhoneNumber"));
            gridEx1.AddColumn("HireDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            gridEx1.AddColumn("DischargeDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            gridEx1.AddColumn("Description", LabelConvert.GetLabelText("Memo"));
            gridEx1.AddColumn("Active", LabelConvert.GetLabelText("UseFlag"));

            gridEx1.BestFitColumns();
            gridEx1.GridLayoutRestore();
        }

        protected override void InitRepository()
        {
            gridEx1.SetRepositoryItemLookUpEdit("ProductTeamCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.SetRepositoryItemLookUpEdit("RankCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.RankCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.SetRepositoryItemCheckEdit("Active", "N");
            gridEx1.SetRepositoryItemDateTimeEdit("HireDate");
            gridEx1.SetRepositoryItemDateTimeEdit("DischargeDate");
            gridEx1.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            bindingSource.DataSource = UserService.GetList(p => p.Active == "Y" || string.IsNullOrEmpty(p.Active))
                                                  .OrderBy(p => p.UserName).ToList();
            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();
            if (IsmultiSelect)
            {
                if (returnField.IsNullOrEmpty())
                {
                    List<decimal> returnList = new List<decimal>();

                    foreach (var rowHandle in gridEx1.MainView.GetSelectedRows())
                        returnList.Add((decimal)gridEx1.MainView.GetRowCellValue(rowHandle, "UserId"));

                    DataParam map = new DataParam();
                    map.SetValue("returnList", returnList);

                    param.SetValue(PopupParameter.DataParam, map);
                }
                else
                {
                    List<object> returnList = new List<object>();

                    foreach (var rowHandle in gridEx1.MainView.GetSelectedRows())
                        returnList.Add(gridEx1.MainView.GetRowCellValue(rowHandle, returnField));

                    DataParam map = new DataParam();
                    map.SetValue("returnList", returnList);

                    param.SetValue(PopupParameter.DataParam, map);
                }
            }
            else
            {                
                param.SetValue(Utils.Enum.PopupParameter.ReturnObject, bindingSource.Current);                
            }
            ReturnPopupArgument = new PopupArgument(param);

            base.ActClose();
        }


        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (IsmultiSelect)
                {
                    if (returnField.IsNullOrEmpty())
                    {
                        List<decimal> returnList = new List<decimal>();
                        returnList.Add((decimal)gridEx1.MainView.GetRowCellValue(gridEx1.MainView.FocusedRowHandle, "UserId"));
                        DataParam map = new DataParam();
                        map.SetValue("returnList", returnList);

                        PopupDataParam param = new PopupDataParam();
                        param.SetValue(PopupParameter.DataParam, map);

                        ReturnPopupArgument = new PopupArgument(param);
                    }
                    else
                    {
                        List<object> returnList = new List<object>();
                        returnList.Add(gridEx1.MainView.GetRowCellValue(gridEx1.MainView.FocusedRowHandle, returnField));
                        DataParam map = new DataParam();
                        map.SetValue("returnList", returnList);

                        PopupDataParam param = new PopupDataParam();
                        param.SetValue(PopupParameter.DataParam, map);

                        ReturnPopupArgument = new PopupArgument(param);
                    }
                }
                else
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(Utils.Enum.PopupParameter.ReturnObject, bindingSource.Current);
                    ReturnPopupArgument = new PopupArgument(param);
                }
                base.ActClose();
            }            
        }
    }
}
