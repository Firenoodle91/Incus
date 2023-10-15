using System.Linq;
using System.Windows.Forms;

using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;

using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using System.Collections.Generic;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.SYS
{
    public partial class FUserGList : HKInc.Service.Base.ListFormTemplate
    {
        IService<UserUserGroup> LabelTextService = (IService<UserUserGroup>)ServiceFactory.GetDomainService("UserUserGroup");
      
        public FUserGList()
        {
            InitializeComponent();
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Save, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
            GridExControl = gridEx1;
            
        }

        public FUserGList(UserGroup obj)
        {
            InitializeComponent();
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Save, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
            GridExControl = gridEx1;
            lup_group.EditValue = obj.UserGroupId;
            lup_group.EditValueChanged += Lup_group_EditValueChanged;
      //      DataLoad()
        }

        private void Lup_group_EditValueChanged(object sender, System.EventArgs e)
        {
            GridRowLocator.GetCurrentRow("UserUserGroupId");

            GridExControl.MainGrid.Clear();
            decimal? usrgroup = lup_group.EditValue.GetDecimalNullToNull();
            if (usrgroup == null) return;

            GridBindingSource.DataSource = LabelTextService.GetList(p => p.UserGroupId == usrgroup).ToList();

            GridExControl.DataSource = GridBindingSource;
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);

            GridExControl.BestFitColumns();
        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();          
        }
       
        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            GridExControl.MainGrid.AddColumn("UserUserGroupId",false);
            GridExControl.MainGrid.AddColumn("UserGroupId",false);
            GridExControl.MainGrid.AddColumn("UserId", false);
            GridExControl.MainGrid.AddColumn("User.LoginId","로그인ID");
            GridExControl.MainGrid.AddColumn("User.UserName","사용자명");
                    
            GridExControl.MainGrid.AddColumn("UpdateId");
            GridExControl.MainGrid.AddColumn("UpdateTime");
            GridExControl.MainGrid.AddColumn("UpdateClass", false);
            GridExControl.MainGrid.AddColumn("CreateId");
            GridExControl.MainGrid.AddColumn("CreateTime");
            GridExControl.MainGrid.AddColumn("CreateClass", false);

          
            

            GridExControl.BestFitColumns();
        }
        protected override void InitCombo()
        {
            lup_group.SetDefault(false, "UserGroupId", "UserGroupName", LabelTextService.GetChildList<UserGroup>(p => p.Active == "Y").ToList());
        }
        protected override void InitRepository()
        {
         
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateTime");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateTime");
        }

        protected override void DataLoad()
        {

            InitDataLoad2();
        }

        protected override void InitDataLoad2()
        {
       
            GridRowLocator.GetCurrentRow("UserUserGroupId");
            LabelTextService.ReLoad();

            decimal? usrgroup = lup_group.EditValue.GetDecimalNullToNull();
            if (usrgroup == null) return;

            GridBindingSource.DataSource = LabelTextService.GetList(p => p.UserGroupId == usrgroup).ToList();

            GridExControl.DataSource = GridBindingSource;
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);

            GridExControl.BestFitColumns();
        }
        protected override void DataSave()
        {
            gridEx1.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            LabelTextService.Save(true);            
            DataLoad();
        }

        //   #region 추상함수 구현
        protected override void DeleteRow()
        {
            UserUserGroup obj = GridBindingSource.Current as UserUserGroup;

            if (obj != null)
            {
                LabelTextService.Delete(obj);
                GridBindingSource.RemoveCurrent();
            }
        }

        protected override void AddRowClicked()
        {
            PopupDataParam param = new PopupDataParam();
            OpenPopup(param);
        }
        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {

            return HKInc.Ui.View.PopupFactory.ProductionPopupFactory.GetPopupForm(HKInc.Ui.View.PopupFactory.ProductionPopupView.UserSelectList, param, AddNewUserGroup);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, LabelTextService);
            param.SetValue(PopupParameter.EditMode, true);
            param.SetValue(PopupParameter.IsMultiSelect, true);

            return param;
        }

    



        private void AddNewUserGroup(object sender, PopupArgument e)
        {
            if (e == null) return;
            decimal? groupid = lup_group.EditValue.GetDecimalNullToNull();
            if (groupid == null) return;
            List<decimal> userIdList = ((DataParam)e.Map.GetValue(PopupParameter.DataParam)).GetValue("returnList") as List<decimal>;
            if (userIdList == null) return;

            foreach (var userId in userIdList)
            { int i = 0;
                foreach (UserUserGroup olduser in GridBindingSource)
                {
                    if (userId == olduser.UserId)
                    {
                        i++;
                     }
                }
                if (i==0)
                {
                    User user = LabelTextService.GetChildList<User>(p => p.UserId == userId).FirstOrDefault();

                    // Grid의 BindingSource에 추가해서 Grid에 추가표시
                    UserUserGroup userUserGroup = new UserUserGroup();

                    userUserGroup.UserId = user.UserId;
                    userUserGroup.UserGroupId =  groupid.GetDecimalNullToZero();
                    GridBindingSource.Add(userUserGroup);
                    LabelTextService.Insert(userUserGroup);

                    //       ((UserGroup)userGroupBindingSource.Current).UserUserGroupList.Add(userUserGroup);



                    IsFormControlChanged = true;
                }
                i = 0;
            }
        }

    }
}