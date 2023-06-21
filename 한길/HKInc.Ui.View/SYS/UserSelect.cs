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

namespace HKInc.Ui.View.SYS
{
    public partial class UserSelect : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<User> UserService;
        BindingSource bindingSource = new BindingSource();
        bool IsmultiSelect = true;

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

            this.Text = "사용자정보";
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

            gridEx1.AddColumn("UserId", false);
            gridEx1.AddColumn("LoginId");
            gridEx1.AddColumn("EmployeeNo");
            gridEx1.AddColumn("UserName");
            gridEx1.AddColumn("Rank");
            //gridEx1.AddColumn("ADUser", false);
            gridEx1.AddColumn("DepartmentCode", "부서");
            gridEx1.AddColumn("HireDate");
            gridEx1.AddColumn("DischargeDate");
            //gridEx1.AddColumn("GroupCode");
            gridEx1.AddColumn("Email");
            gridEx1.AddColumn("CellPhone");
            //gridEx1.AddColumn("SequenceNumber");
            gridEx1.AddColumn("Description");
            gridEx1.AddColumn("Active");
            //gridEx1.AddColumn("UpdateId", false);
            //gridEx1.AddColumn("UpdateTime", false);
            //gridEx1.AddColumn("UpdateClass", false);
            //gridEx1.AddColumn("CreateId", false);
            //gridEx1.AddColumn("CreateTime", false);
            //gridEx1.AddColumn("CreateClass", false);

            gridEx1.BestFitColumns();
            gridEx1.GridLayoutRestore();
        }

        protected override void InitRepository()
        {
         //   gridEx1.SetRepositoryItemSearchLookUpEdit("GroupCode", MasterCode.GetMasterCode((int)MasterCodeEnum.UserGroupCode).ToList());
            gridEx1.SetRepositoryItemSearchLookUpEdit("Rank", MasterCode.GetMasterCode((int)MasterCodeEnum.UserRankCode).ToList());
            gridEx1.SetRepositoryItemSearchLookUpEdit("DepartmentCode", UserService.GetChildList<UserDepartment>(p => p.UseFlag != "N"), "DepartmentCode", "DepartmentName");
            gridEx1.SetRepositoryItemDateEdit("HireDate");
            gridEx1.SetRepositoryItemDateEdit("DischargeDate");
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
                List<decimal> userIdList = new List<decimal>();

                foreach (var rowHandle in gridEx1.MainView.GetSelectedRows())
                    userIdList.Add((decimal)gridEx1.MainView.GetRowCellValue(rowHandle, "UserId"));

                DataParam map = new DataParam();
                map.SetValue("UserIdList", userIdList);
                
                param.SetValue(Utils.Enum.PopupParameter.DataParam, map);                
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
                    List<decimal> userIdList = new List<decimal>();
                    userIdList.Add((decimal)gridEx1.MainView.GetRowCellValue(gridEx1.MainView.FocusedRowHandle, "UserId"));
                    DataParam map = new DataParam();
                    map.SetValue("UserIdList", userIdList);

                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(Utils.Enum.PopupParameter.DataParam, map);

                    ReturnPopupArgument = new PopupArgument(param);
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
