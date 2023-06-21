using System.Linq;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Service;

namespace HKInc.Ui.View.SYS
{
    public partial class UserList : HKInc.Service.Base.ListFormTemplate
    {
        IService<User> UserService = (IService<User>)ServiceFactory.GetDomainService("User");

        public UserList()
        {
            InitializeComponent();

            GridExControl = gridEx1;
            OutPutRadioGroup = radioGroup;
            RadioGroupType = RadioGroupType.ActiveAll;
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("UserId");
            GridExControl.MainGrid.AddColumn("LoginId");
            GridExControl.MainGrid.AddColumn("Password", false);
            GridExControl.MainGrid.AddColumn("UserName");
            GridExControl.MainGrid.AddColumn("EmployeeNo");
            GridExControl.MainGrid.AddColumn("LoginDb", false);
            GridExControl.MainGrid.AddColumn("CodeDb", false);
            GridExControl.MainGrid.AddColumn("ProductionDb", false);
            GridExControl.MainGrid.AddColumn("ADUser", false);            
            GridExControl.MainGrid.AddColumn("Rank","직급");
          //  GridExControl.MainGrid.AddColumn("GroupCode");
            GridExControl.MainGrid.AddColumn("DepartmentCode", LabelConvert.GetLabelText("DepartmentName"));
            GridExControl.MainGrid.AddColumn("Property8", "제조팀");
            GridExControl.MainGrid.AddColumn("Email");
            GridExControl.MainGrid.AddColumn("CellPhone");
            GridExControl.MainGrid.AddColumn("PurMaster",true,DevExpress.Utils.HorzAlignment.Center);
            GridExControl.MainGrid.AddColumn("HireDate");
            GridExControl.MainGrid.AddColumn("DischargeDate");
            GridExControl.MainGrid.AddColumn("Description");
            GridExControl.MainGrid.AddColumn("Property7", false);
       
            GridExControl.MainGrid.AddColumn("Property9", false);
            GridExControl.MainGrid.AddColumn("Property10", false);
            GridExControl.MainGrid.AddColumn("Active");
            GridExControl.MainGrid.AddColumn("UpdateId");
            GridExControl.MainGrid.AddColumn("UpdateTime");
            GridExControl.MainGrid.AddColumn("UpdateClass");
            GridExControl.MainGrid.AddColumn("CreateId");
            GridExControl.MainGrid.AddColumn("CreateTime");
            GridExControl.MainGrid.AddColumn("CreateClass");

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit);
            GridExControl.MainGrid.SetEditable("Description");



            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
           // GridExControl.MainGrid.SetRepositoryItemLookUpEdit("GroupCode", MasterCode.GetMasterCode((int)MasterCodeEnum.UserGroupCode).ToList());
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("Rank", MasterCode.GetMasterCode((int)MasterCodeEnum.UserRankCode).ToList());
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("DepartmentCode", UserService.GetChildList<UserDepartment>(p => p.UseFlag != "N"), "DepartmentCode", "DepartmentName");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("Rank", "N");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateTime");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateTime");
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("HireDate");
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("DischargeDate");
            GridExControl.MainGrid.MainView.Columns["Description"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Property8", DbRequestHandler.GetCommCode(MasterCodeSTR.tem), "Mcode", "Codename");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            UserService.ReLoad();

            string radioValue = radioGroup.EditValue.GetNullToEmpty();
            GridBindingSource.DataSource = UserService.GetList(p => p.UserName.Contains(textUserName.Text) &&
                                                                   (string.IsNullOrEmpty(radioValue) ? true : p.Active == radioValue))
                                                      .OrderBy(p => p.UserName).ToList();

            GridExControl.DataSource = GridBindingSource;
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(GridExControl.MainGrid.RecordCount);

            GridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            UserService.Save();            
            DataLoad();
        }        

        #region 추상함수 구현
        protected override void DeleteRow()
        {
            User obj = GridBindingSource.Current as User;

            if (obj != null)
            {
                UserService.Delete(obj);
                GridBindingSource.RemoveCurrent();
            }
        }
        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return HKInc.Ui.View.PopupFactory.ProductionPopupFactory.GetPopupForm(HKInc.Ui.View.PopupFactory.ProductionPopupView.User, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, UserService);
            return param;
        }
        #endregion
    }
}