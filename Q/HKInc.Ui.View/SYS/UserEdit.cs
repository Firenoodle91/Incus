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
using HKInc.Service.Service;

namespace HKInc.Ui.View.SYS
{
    public partial class UserEdit : HKInc.Service.Base.ListEditFormTemplate
    {                
        private IService<User> UserService;        
        private BindingSource UserGroupBindingSource = new BindingSource();

        public UserEdit(PopupDataParam param, PopupCallback callback)
        { 
            InitializeComponent();

            PopupParam = param;
            Callback = callback;

            GridExControl = gridEx1;  //  Grid설정
            ModelBindingSource = userBindingSource; // BindingSource설정                      
        }
        
        protected override void AddControlList() // abstract함수 구현 InitControl에서 Call 한다
        {            
            ControlEnableList.Add("LoginId", textLoginId);
            ControlEnableList.Add("Password", textPassword);
            ControlEnableList.Add("UserName", textUserName);
            ControlEnableList.Add("EmployeeNo", textEmployeeNo);            
            ControlEnableList.Add("ADUser", textAdUser);
            ControlEnableList.Add("Description", memoDescription);
            ControlEnableList.Add("Rank", lupRank);
            ControlEnableList.Add("GroupCode", lupGroupCode);
            ControlEnableList.Add("DepartmentCode", lupDepartment);
            ControlEnableList.Add("Email", textEmail);
            ControlEnableList.Add("HireDate", dateHireDate);
            ControlEnableList.Add("DischargeDate", dateDischargeDate);
            ControlEnableList.Add("CellPhone", textCellPhone);
            ControlEnableList.Add("Active", chkActive);

            LayoutControlHandler.SetRequiredLabelText<HKInc.Ui.Model.Domain.User>(new HKInc.Ui.Model.Domain.User(), ControlEnableList, this.Controls);
        }
        
        protected override void InitBindingSource()
        {            
            // Service설정 부모에게서 넘어온다
            UserService = (IService<User>)PopupParam.GetValue(PopupParameter.Service);                        
        }

        protected override void InitCombo()
        {
            lupRank.SetDefault(true, "CodeId", "CodeName", MasterCode.GetMasterCode((int)MasterCodeEnum.UserRankCode).ToList());
            luptem.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.tem));
            luppurmaster.SetDefault(false, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.YN));
            //gridLookUpEditEx1.SetDefault(true, "CodeId", "CodeName", MasterCode.GetMasterCode(30478).ToList());

            //List<HKInc.Ui.Model.Domain.UserDepartment> departmentList = UserService.GetChildList<UserDepartment>(p => p.UseFlag == "Y" || string.IsNullOrEmpty(p.UseFlag));
            //lupDepartment.ParentFieldName = "ParentDepartmentCode";
            //lupDepartment.KeyFieldName = "DepartmentCode";
            //lupDepartment.DisplayMember = "DepartmentName";
            //lupDepartment.ValueMember = "DepartmentCode";
            //lupDepartment.ShowColumns = false;
            //lupDepartment.AddColumn("DepartmentName");
            //lupDepartment.AddColumn("DepartmentCode", false);
            //lupDepartment.DataSource = departmentList;
            //lupDepartment.ExpandAll();
            lupGroupCode.SetDefault(true, "CodeId", "CodeName", MasterCode.GetMasterCode((int)MasterCodeEnum.UserGroupCode).OrderBy(p=>p.DisplayOrder).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            lupDepartment.SetDefault(true, "DepartmentCode", "DepartmentName", UserService.GetChildList<UserDepartment>(p => p.UseFlag == "Y"));
            lupDepartment.AddButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis);
            lupDepartment.ButtonClick += new HKInc.Ui.View.PopupFactory.SystemLookUpButtonHandler<UserDepartment>(lupDepartment, PopupFactory.ProductionPopupView.UserDepartmentSelect).ButtonClick;
            
            textPassword.Validated += TextPassword_Validated;
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.AddColumn("UserUserGroupId", false);
            gridEx1.MainGrid.AddColumn("UserId", false);
            gridEx1.MainGrid.AddColumn("UserGroupId");
            gridEx1.MainGrid.AddColumn("UpdateId");
            gridEx1.MainGrid.AddColumn("UpdateTime");
            gridEx1.MainGrid.AddColumn("UpdateClass");
            gridEx1.MainGrid.AddColumn("CreateId");
            gridEx1.MainGrid.AddColumn("CreateTime");
            gridEx1.MainGrid.AddColumn("CreateClass");

            gridEx1.BestFitColumns();
        }

        protected override void InitRepository()
        {
            List<UserGroup> userGroupList = UserService.GetChildList<UserGroup>(p => p.Active == "Y" || string.IsNullOrEmpty(p.Active));

            gridEx1.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateTime");
            gridEx1.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateTime");
            gridEx1.MainGrid.SetRepositoryItemLookUpEdit("UserGroupId", userGroupList, "UserGroupId", HKInc.Service.Helper.LookUpFieldHelper.GetCultureFieldName("UserGroup"));
        }


        protected override void DataLoad()
        {
            if(EditMode == PopupEditMode.New) // 신규 추가
            {
                userBindingSource.Add(new User()
                { Active = "Y",
                  //Password = HKInc.Utils.Encrypt.AESEncrypt256.Encrypt("*11Dream"),
                  //TCK AES 관리 X
                    Password = "1",
                    UserUserGroupList = new List<UserUserGroup>() });

                userBindingSource.MoveLast();                
            }
            else
            {  // Update          
                User obj = (User)PopupParam.GetValue(PopupParameter.KeyValue);                
                userBindingSource.DataSource = obj;
            }

            // Grid Binding 설정     

            UserGroupBindingSource.AllowNew = true;            
            UserGroupBindingSource.DataSource = UserService.GetChildList<UserUserGroup>(p => p.UserId == ((HKInc.Ui.Model.Domain.User)userBindingSource.Current).UserId);

            gridEx1.DataSource = UserGroupBindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void DataSave()
        {
            userBindingSource.EndEdit(); //저장전 수정사항 Posting

            User obj = (User)userBindingSource.Current;            

            if (EditMode == PopupEditMode.New)             
                userBindingSource.DataSource = UserService.Insert(obj);                             
            else            
                userBindingSource.DataSource = UserService.Update(obj);
                         
            UserService.Save();
            
            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }

        #region Button click Function
        protected override void DeleteGridRow()
        {
            UserUserGroup obj = (UserUserGroup)UserGroupBindingSource.Current;

            if (obj != null)
            {
                UserGroupBindingSource.RemoveCurrent();
                ((User)userBindingSource.Current).UserUserGroupList.Remove(obj);

                if(obj.UserUserGroupId > 0)                
                    UserService.RemoveChild<UserUserGroup>(obj);

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
            // obj에 return된 UserGroup으로 값설정
            // 기존값과비교 중복이면 추가하지않는다.
            if (e == null) return;
            List<decimal> userGroupIdList = (List<decimal>)((DataParam)e.Map.GetValue(PopupParameter.DataParam)).GetValue("UserGroupIdList");
            foreach(var id in userGroupIdList)
            {                
                if (!((User)userBindingSource.Current).UserUserGroupList.Any(p=>p.UserGroupId == id))
                {
                    UserUserGroup obj = (UserUserGroup)UserGroupBindingSource.AddNew();
                    obj.UserGroupId = id;
                    obj.UpdateId = GlobalVariable.LoginId;
                    obj.UpdateClass = GlobalVariable.CurrentInstance;
                    obj.UpdateTime = DateTime.Now;
                    obj.CreateId = GlobalVariable.LoginId;
                    obj.CreateClass = GlobalVariable.CurrentInstance;
                    obj.CreateTime = DateTime.Now;

                    ((User)userBindingSource.Current).UserUserGroupList.Add(obj);
                    GridExControl.MainGrid.MainView.RefreshData();

                    IsFormControlChanged = true;
                }
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            
            return HKInc.Ui.View.PopupFactory.ProductionPopupFactory.GetPopupForm(HKInc.Ui.View.PopupFactory.ProductionPopupView.UserGroupSelectList, param, AddNewUserGroup);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, UserService);
            return param;
        }
        #endregion

        #region Event Handler

        private void TextPassword_Validated(object sender, EventArgs e)
        {
            User obj = (User)userBindingSource.Current;
            //obj.Password = HKInc.Utils.Encrypt.AESEncrypt256.Encrypt(textPassword.EditValue.GetNullToEmpty());
            //TCK AES 관리 X
            string pwd = textPassword.EditValue.GetNullToEmpty();
            if(!string.IsNullOrEmpty(pwd))
                obj.Password = textPassword.EditValue.GetNullToEmpty();

            //if (ValidatePasswordFormat(textPassword.EditValue.GetNullToEmpty()))
            //{
            //    User obj = (User)userBindingSource.Current;
            //    obj.Password = HKInc.Utils.Encrypt.AESEncrypt256.Encrypt(textPassword.EditValue.GetNullToEmpty());
            //}
            //else
            //{
            //    textPassword.EditValue = null;
            //    textPassword.Focus();
            //}
        }


        private bool ValidatePasswordFormat(string password)
        {
            HKInc.Utils.Interface.Handler.IPasswordHandler passwordHandler = Service.Factory.LoginFactory.GetPasswordHandler();
            string errMessage = string.Empty;
            if (passwordHandler.IsValidFormat(password, out errMessage))
                return true;
            else
                HKInc.Service.Handler.MessageBoxHandler.Show(errMessage);

            return false;
        }
        #endregion
    }
}