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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Service;
using DevExpress.Utils;

namespace HKInc.Ui.View.SYS
{
    /// <summary>
    /// 권합그룹관리 팝업창
    /// </summary>
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

            this.Text = LabelConvert.GetLabelText("AuthGroupEdit");
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
            lupUpperUserGroupId.ParentFieldName = "UpperUserGroupId";
            lupUpperUserGroupId.KeyFieldName = "UserGroupId";
            lupUpperUserGroupId.DisplayMember = Service.Helper.DataConvert.GetCultureDataFieldName("UserGroupName", "UserGroupName2", "UserGroupName3");
            lupUpperUserGroupId.ValueMember = "UserGroupId";
            lupUpperUserGroupId.ShowColumns = false;
            lupUpperUserGroupId.AddColumn(lupUpperUserGroupId.DisplayMember);
            lupUpperUserGroupId.DataSource = upperUserGroupList;
            lupUpperUserGroupId.ExpandAll();

            var userList = UserGroupService.GetChildList<User>(p => true).ToList();
            lupCreateId.SetDefault(false, "LoginId", "UserName", userList);
            lupUpdateId.SetDefault(false, "LoginId", "UserName", userList);
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("UserId", false);
            GridExControl.MainGrid.AddColumn("LoginId");
            GridExControl.MainGrid.AddColumn("Password", false);
            GridExControl.MainGrid.AddColumn("UserName");
            GridExControl.MainGrid.AddColumn("EmployeeNo");
            GridExControl.MainGrid.AddColumn("Description", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.AddColumn("RankCode", LabelConvert.GetLabelText("Rank"));
            //GridExControl.MainGrid.AddColumn("DepartmentCode", LabelConvert.GetLabelText("Department"));
            GridExControl.MainGrid.AddColumn("HireDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("DischargeDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("Active", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("RankCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.RankCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemLookUpEdit("DepartmentCode", UserGroupService.GetChildList<TN_STD1200>(p => true), "DepartmentCode", "DepartmentName");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
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

            List<decimal> userIdList = ((DataParam)e.Map.GetValue(PopupParameter.DataParam)).GetValue("returnList") as List<decimal>;
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
                    obj.CreateId = user.CreateId;
                    obj.CreateTime = user.CreateTime;
                    obj.Description = user.Description;
                    obj.EmployeeNo = user.EmployeeNo;
                    obj.RankCode = user.RankCode;
                    //obj.UpdateClass = user.UpdateClass;
                    obj.UpdateId = user.UpdateId;
                    obj.UpdateTime = user.UpdateTime;
                                                               
                    // Main의 datasource Binding에 Add해준다 이것이 DB로 업데이트된다.
                    UserUserGroup userUserGroup = new UserUserGroup { UserId = user.UserId };
                    ((UserGroup)userGroupBindingSource.Current).UserUserGroupList.Add(userUserGroup);

                    IsFormControlChanged = true;
                }
                GridExControl.BestFitColumns();
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