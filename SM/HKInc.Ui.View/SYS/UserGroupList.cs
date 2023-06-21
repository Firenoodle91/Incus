using System;
using System.Linq;

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;

using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;

namespace HKInc.Ui.View.SYS
{
    /// <summary>
    /// 권한그룹관리
    /// </summary>
    public partial class UserGroupList : HKInc.Service.Base.TreeListFormTemplate
    {
        IService<UserGroup> UserGroupService = (IService<UserGroup>)ServiceFactory.GetDomainService("UserGroup");

        public UserGroupList()
        {
            InitializeComponent();

            TreeListExControl = treeList;
            //OutPutRadioGroup = radioGroup;
            //RadioGroupType = RadioGroupType.ActiveAll;
            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitGrid()
        {
            TreeListExControl.AddColumn("UserGroupId", LabelConvert.GetLabelText("AuthGroupId"), false);
            TreeListExControl.AddColumn("UpperUserGroupId", LabelConvert.GetLabelText("UpperAuthGroup"), false);
            TreeListExControl.AddColumn("UserGroupName", LabelConvert.GetLabelText("AuthGroupName"));            
            TreeListExControl.AddColumn("UserGroupName2", LabelConvert.GetLabelText("AuthGroupNameENG"));
            TreeListExControl.AddColumn("UserGroupName3", LabelConvert.GetLabelText("AuthGroupNameCHN"));
            TreeListExControl.AddColumn("Description", LabelConvert.GetLabelText("Memo"));
            TreeListExControl.AddColumn("Active", LabelConvert.GetLabelText("UseFlag"));
            TreeListExControl.AddColumn("CreateId");
            TreeListExControl.AddColumn("CreateTime");
            TreeListExControl.AddColumn("UpdateId");
            TreeListExControl.AddColumn("UpdateTime");

            TreeListExControl.ParentFieldName = "UpperUserGroupId";
            TreeListExControl.KeyFieldName = "UserGroupId";

            TreeListExControl.SetTreeListOption(false);
        }

        protected override void InitRepository()
        {
            RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit()
            {
                ValueChecked = "Y",
                ValueUnchecked = "N"
            };

            TreeListExControl.TreeList.Columns["Active"].ColumnEdit = checkEdit;
            var userList = UserGroupService.GetChildList<User>(p => true).ToList();
            TreeListExControl.TreeList.Columns["CreateId"].ColumnEdit = HKInc.Service.Factory.RepositoryFactory.GetRepositoryItemLookUpEdit(userList, "LoginId", "UserName");
            TreeListExControl.TreeList.Columns["UpdateId"].ColumnEdit = HKInc.Service.Factory.RepositoryFactory.GetRepositoryItemLookUpEdit(userList, "LoginId", "UserName");
            TreeListExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            UserGroupService.ReLoad();

            //string radioValue = radioGroup1.EditValue.GetNullToEmpty();
            string radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();
            TreeListBindingSource.DataSource = UserGroupService.GetList(p => (p.UserGroupName.Contains(textUserGroupName.Text) || p.UserGroupName2.Contains(textUserGroupName.Text) || p.UserGroupName3.Contains(textUserGroupName.Text)) 
                                                                           && (radioValue == "A" ? true : p.Active == radioValue)
                                                                       )
                                                                       .OrderBy(p=>p.UpperUserGroupId)
                                                                       .ThenBy(p=>p.UserGroupId)
                                                                       .ToList();
            TreeListExControl.DataSource = TreeListBindingSource;

            SetRefreshMessage(TreeListBindingSource.Count);

            TreeListExControl.BestFitColumns();
            TreeListExControl.ExpandAll();
        }

        protected override void DataSave()
        {
            UserGroupService.Save();            
            DataLoad();
        }        

        #region 추상함수 구현
        protected override void DeleteRow()
        {
            UserGroup obj = TreeListBindingSource.Current as UserGroup;

            if (obj != null)
            {
                UserGroupService.Delete(obj);
                TreeListBindingSource.RemoveCurrent();
            }
        }
        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            
            return HKInc.Ui.View.PopupFactory.ProductionPopupFactory.GetPopupForm(HKInc.Ui.View.PopupFactory.ProductionPopupView.UserGroup, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, UserGroupService);
            return param;
        }
        #endregion
    }
}