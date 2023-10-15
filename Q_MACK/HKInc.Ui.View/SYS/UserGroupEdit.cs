using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;

using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Interface.Popup;


namespace HKInc.Ui.View.SYS
{
    public partial class UserGroupEdit : HKInc.Service.Base.ListEditFormTemplate
    {                
        private IService<HKInc.Ui.Model.Domain.UserGroup> UserGroupService;
        private BindingSource UserUserGroupBindingSource = new BindingSource();

        public UserGroupEdit(PopupDataParam param, PopupCallback callback)
        { 
            InitializeComponent();

            PopupParam = param;
            Callback = callback;

            GridExControl = gridEx1;  //  Grid설정
            ModelBindingSource = userGroupBindingSource; // BindingSource설정                      
        }
        
        protected override void AddControlList() // abstract함수 구현 InitControl에서 Call 한다
        {            
            ControlEnableList.Add("UpperUserGroupId", lupUpperUserGroupId);
            ControlEnableList.Add("UserGroupName", textUserGroupName);
            ControlEnableList.Add("UserGroupName2", textUserGroupName2);
            ControlEnableList.Add("UserGroupName3", textUserGroupName3);
            ControlEnableList.Add("Description", memoDescription);
            ControlEnableList.Add("Active", chkActive);

            LayoutControlHandler.SetRequiredLabelText<HKInc.Ui.Model.Domain.UserGroup>(new HKInc.Ui.Model.Domain.UserGroup(), ControlEnableList, this.Controls);
        }
        
        protected override void InitBindingSource()
        {            
            // Service설정 부모에게서 넘어온다
            UserGroupService = (IService<HKInc.Ui.Model.Domain.UserGroup>)PopupParam.GetValue(PopupParameter.Service);                 
        }

        protected override void InitCombo()
        {
            List<HKInc.Ui.Model.Domain.UserGroup> upperUserGroupList = UserGroupService.GetList(p => p.Active == "Y" || string.IsNullOrEmpty(p.Active));
            //lupUpperUserGroupId.SetDefault(true, "UserGroupId", HKInc.Service.Helper.LookUpFieldHelper.GetCultureFieldName("UserGroup"), upperUserGroupList);
            lupUpperUserGroupId.ParentFieldName = "UpperUserGroupId";
            lupUpperUserGroupId.KeyFieldName = "UserGroupId";
            lupUpperUserGroupId.DisplayMember = HKInc.Service.Helper.LookUpFieldHelper.GetCultureFieldName("UserGroup");
            lupUpperUserGroupId.ValueMember = "UserGroupId";
            lupUpperUserGroupId.ShowColumns = false;
            lupUpperUserGroupId.AddColumn(HKInc.Service.Helper.LookUpFieldHelper.GetCultureFieldName("UserGroup"));
            lupUpperUserGroupId.DataSource = upperUserGroupList;
            lupUpperUserGroupId.ExpandAll();
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
            GridExControl.MainGrid.AddColumn("Description");
            GridExControl.MainGrid.AddColumn("Rank");
            GridExControl.MainGrid.AddColumn("DepartmentCode");
            GridExControl.MainGrid.AddColumn("HireDate");
            GridExControl.MainGrid.AddColumn("DischargeDate");
            GridExControl.MainGrid.AddColumn("Active");
            GridExControl.MainGrid.AddColumn("UpdateId");
            GridExControl.MainGrid.AddColumn("UpdateTime");
            GridExControl.MainGrid.AddColumn("UpdateClass");
            GridExControl.MainGrid.AddColumn("CreateId");
            GridExControl.MainGrid.AddColumn("CreateTime");
            GridExControl.MainGrid.AddColumn("CreateClass");

            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("Rank", MasterCode.GetMasterCode(27).ToList());
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("DepartmentCode", UserGroupService.GetChildList<UserDepartment>(p => 1 == 1), "DepartmentCode", "DepartmentName");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateTime");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateTime");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("HireDate");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("DischargeDate");
        }

        protected override void DataLoad()
        {
            if(EditMode == PopupEditMode.New) // 신규 추가
            {
                userGroupBindingSource.Add(new HKInc.Ui.Model.Domain.UserGroup() { Active = "Y", UserUserGroupList = new List<UserUserGroup>() });
                userGroupBindingSource.MoveLast();                
            }
            else
            {  // Update                                     
               userGroupBindingSource.DataSource = (HKInc.Ui.Model.Domain.UserGroup)PopupParam.GetValue(PopupParameter.KeyValue);                
            }

            // User정보표시위해서 UserUserGroup 의 GroupId를 이용해서 UserGroup을 찾고 여기에 있는 User를 Grid에 Binding한다
            List<UserUserGroup> userUserGroupList = UserGroupService.GetChildList<UserUserGroup>(p => p.UserGroupId == ((UserGroup)userGroupBindingSource.Current).UserGroupId);            

            UserUserGroupBindingSource.AllowNew = true;
            UserUserGroupBindingSource.DataSource = userUserGroupList.Select(p => p.User).ToList();
            gridEx1.DataSource = UserUserGroupBindingSource;

            GridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            userGroupBindingSource.EndEdit(); //저장전 수정사항 Posting

            HKInc.Ui.Model.Domain.UserGroup obj = (HKInc.Ui.Model.Domain.UserGroup)userGroupBindingSource.Current;
           
            if (EditMode == PopupEditMode.New)             
                userGroupBindingSource.DataSource = UserGroupService.Insert(obj);                             
            else
                userGroupBindingSource.DataSource = UserGroupService.Update(obj);

            UserGroupService.Save();
            
            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }

        #region Button click Function
        protected override void DeleteGridRow()
        {
           User obj = UserUserGroupBindingSource.Current as User; // User를 UserUserGroup으로 풀어내야 한다.

            if (obj != null)
            {
                UserUserGroupBindingSource.RemoveCurrent(); // Grid에서 삭제
                // Main에서 삭제
                UserUserGroup userUserGroupToRemove = ((HKInc.Ui.Model.Domain.UserGroup)userGroupBindingSource.Current).UserUserGroupList.Where(p => p.UserId == obj.UserId).First();
                ((HKInc.Ui.Model.Domain.UserGroup)userGroupBindingSource.Current).UserUserGroupList.Remove(userUserGroupToRemove);

                if (userUserGroupToRemove.UserUserGroupId > 0) // DB에 저장된 객체이면 DB에 삭제 적용
                    UserGroupService.RemoveChild<UserUserGroup>(userUserGroupToRemove);

                IsFormControlChanged = true;
            }
        }
        protected override void AddGridRow()
        {
            PopupDataParam param = new PopupDataParam();
            OpenPopup(param);
        }

        // Select Usergroup 에서 Callback함수
        private void AddNewUserGroup(object sender, PopupArgument e)
        {
            if (e == null) return;

            List<decimal> userIdList = ((DataParam)e.Map.GetValue(PopupParameter.DataParam)).GetValue("UserIdList") as List<decimal>;
            if (userIdList == null) return;

            foreach (var userId in userIdList)
            {
                // 기존값과비교 중복이면 추가하지않는다.
                if (!((HKInc.Ui.Model.Domain.UserGroup)userGroupBindingSource.Current).UserUserGroupList.Any(p => p.UserId == userId))
                {                    
                    User user = UserGroupService.GetChildList<User>(p => p.UserId == userId).FirstOrDefault();

                    // Grid의 BindingSource에 추가해서 Grid에 추가표시
                    User obj = (User)UserUserGroupBindingSource.AddNew();
                    obj.UserId = user.UserId;
                    obj.UserName = user.UserName;
                    obj.LoginId = user.LoginId;
                    obj.Active = user.Active;
                    obj.ADUser = user.ADUser;
                    obj.CreateId = user.CreateId;
                    obj.CreateTime = user.CreateTime;
                    obj.Description = user.Description;
                    obj.EmployeeNo = user.EmployeeNo;
                    obj.UpdateClass = user.UpdateClass;
                    obj.UpdateId = user.UpdateId;
                    obj.UpdateTime = user.UpdateTime;
                                                               
                    // Main의 datasource Binding에 Add해준다 이것이 DB로 업데이트된다.
                    UserUserGroup userUserGroup = new UserUserGroup { UserId = user.UserId };
                    ((UserGroup)userGroupBindingSource.Current).UserUserGroupList.Add(userUserGroup);

                    IsFormControlChanged = true;
                }
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            
            return HKInc.Ui.View.PopupFactory.ProductionPopupFactory.GetPopupForm(HKInc.Ui.View.PopupFactory.ProductionPopupView.UserSelectList, param, AddNewUserGroup);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, UserGroupService);
            return param;
        }
        #endregion

    }
}