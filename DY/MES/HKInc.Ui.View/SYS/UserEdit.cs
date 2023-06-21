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
using HKInc.Ui.View.Handler;
using HKInc.Ui.Model.Domain.VIEW;
using System.Drawing;

namespace HKInc.Ui.View.SYS
{
    /// <summary>
    /// 사용자관리 팝업창
    /// </summary>
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

            this.Text = LabelConvert.GetLabelText(this.Name);

            lupMainYn.EditValueChanged += LupMainYn_EditValueChanged;
            chkActive1.EditValueChanged += ChkActive1_EditValueChanged;        
        }

        protected override void AddControlList() // abstract함수 구현 InitControl에서 Call 한다
        {
            ControlEnableList.Add("UserId", textUserId);
            ControlEnableList.Add("LoginId", textLoginId);
            ControlEnableList.Add("Password", textPassword);
            ControlEnableList.Add("UserName", textUserName);
            ControlEnableList.Add("EmployeeNo", textEmployeeNo);
            ControlEnableList.Add("RankCode", lupRank);
            ControlEnableList.Add("DepartmentCode", lupDepartment);
            ControlEnableList.Add("Email", textEmail);
            ControlEnableList.Add("CellPhone", textCellPhone);
            ControlEnableList.Add("HireDate", dateHireDate);
            ControlEnableList.Add("DischargeDate", dateDischargeDate);
            ControlEnableList.Add("Description", memoDescription);
            ControlEnableList.Add("Active", chkActive);
            ControlEnableList.Add("ProductTeamCode", lupProductTeam);
            ControlEnableList.Add("ScmYn", chkActive1);
            ControlEnableList.Add("MainYn", lupMainYn);
            ControlEnableList.Add("CustomerCode", lup_MainCustomerCode);



            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
                control.Value.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ModelBindingSource, control.Key, true));
            }
            #endregion

            LayoutControlHandler.SetRequiredLabelText<User>(new User(), ControlEnableList, this.Controls);
        }
        
        protected override void InitBindingSource()
        {            
            // Service설정 부모에게서 넘어온다
            UserService = (IService<User>)PopupParam.GetValue(PopupParameter.Service);                        
        }

        protected override void InitCombo()
        {
            lupProductTeam.SetDefault(true, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode));
            lupRank.SetDefault(true, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.RankCode));
            lupDepartment.SetDefault(true, "DepartmentCode", "DepartmentName", UserService.GetChildList<TN_STD1200>(p => p.UseFlag == "Y").ToList());
            lupMainYn.SetDefault(true, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MainYn));
            lup_MainCustomerCode.SetDefault(true, "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"), UserService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && p.ScmYn == "Y").ToList());

            textUserId.ReadOnly = true;

            textPassword.Validated += TextPassword_Validated;
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.AddColumn("UserUserGroupId", LabelConvert.GetLabelText("UpperAuthGroup"), false);
            gridEx1.MainGrid.AddColumn("UserId", false);
            gridEx1.MainGrid.AddColumn("UserGroupId", LabelConvert.GetLabelText("AuthGroupName"));
        }

        protected override void InitRepository()
        {            
            gridEx1.MainGrid.SetRepositoryItemLookUpEdit("UserGroupId", UserService.GetChildList<UserGroup>(p => true), "UserGroupId", Service.Helper.DataConvert.GetCultureDataFieldName("UserGroupName", "UserGroupName2", "UserGroupName3"));
            gridEx1.BestFitColumns();
        }


        protected override void DataLoad()
        {
            if(EditMode == PopupEditMode.New) // 신규 추가
            {
                userBindingSource.Add(new User()
                {
                    Active = "Y",
                    //Password = HKInc.Utils.Encrypt.AESEncrypt256.Encrypt("*11Dream"),
                    //AES 관리 X
                    Password = "1",
                    UserUserGroupList = new List<UserUserGroup>()
                });
                userBindingSource.MoveLast();                
            }
            else
            {
                // Update 
                textLoginId.ReadOnly = true;

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
            {
                if (obj.ScmYn == "Y")
                {
                    if(obj.CustomerCode == "")                  
                    {
                        MessageBox.Show("SCM사용자의 경우 거래처는 필수입니다.");
                        lup_MainCustomerCode.Focus();
                        return;

                    }
                    else
                    {

                        if(obj.ScmYn == "")
                        {
                            obj.ScmYn = "N";
                        }

                        userBindingSource.DataSource = UserService.Insert(obj);
                    }
                    
                }
                else
                {

                    if (obj.ScmYn == "")
                    {
                        obj.ScmYn = "N";
                    }

                    userBindingSource.DataSource = UserService.Insert(obj);
                }
                
            }
            else
            {
                if (obj.ScmYn == "Y")
                {
                    if (obj.CustomerCode == "")
                    {
                        MessageBox.Show("SCM사용자의 경우 거래처는 필수입니다.");
                        lup_MainCustomerCode.Focus();
                        return;

                    }
                    else
                    {
                        if (obj.ScmYn == "")
                        {
                            obj.ScmYn = "N";
                        }

                        userBindingSource.DataSource = UserService.Update(obj);
                    }

                }
                else
                {
                    if (obj.ScmYn == "")
                    {
                        obj.ScmYn = "N";
                    }

                    userBindingSource.DataSource = UserService.Update(obj);
                }
                
            }
                         
            UserService.Save();

            // 20210810 오세완 차장 기능 없어서 추가 처리
            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.UserId);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

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
            param.SetValue(PopupParameter.KeyValue, lupMainYn.EditValue.GetNullToEmpty());
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
                    var dateTime = DateTime.Now;

                    UserUserGroup obj = (UserUserGroup)UserGroupBindingSource.AddNew();
                    obj.UserGroupId = id;
                    obj.UpdateId = GlobalVariable.LoginId;
                    //obj.UpdateClass = GlobalVariable.CurrentInstance;
                    obj.UpdateTime = dateTime;
                    obj.CreateId = GlobalVariable.LoginId;
                    //obj.CreateClass = GlobalVariable.CurrentInstance;
                    obj.CreateTime = dateTime;

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
            //AES 관리 X

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

        private void LupMainYn_EditValueChanged(object sender, EventArgs e)
        {
            if(lupMainYn.EditValue.GetNullToEmpty() == "01")
            {
                chkActive1.ReadOnly = false;
            }
            else
            {
                chkActive1.ReadOnly = true;
                chkActive1.EditValue = "N";
            }
        }

        private void ChkActive1_EditValueChanged(object sender, EventArgs e)
        {
            if (chkActive1.EditValue.GetNullToEmpty() == "Y")
            {
                //lup_MainCustomerCode.ReadOnly = false;
                lcCustomerName.AppearanceItemCaption.ForeColor = Color.Red;
            }
            else
            {
                lup_MainCustomerCode.EditValue = string.Empty;
                //lup_MainCustomerCode.ReadOnly = true;
                lcCustomerName.AppearanceItemCaption.ForeColor = Color.Black;
            }
        }

        #endregion
    }
}