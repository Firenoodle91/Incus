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
    public partial class UserGroupList : HKInc.Service.Base.TreeListFormTemplate
    {
        IService<UserGroup> UserGroupService = (IService<UserGroup>)ServiceFactory.GetDomainService("UserGroup");

        public UserGroupList()
        {
            InitializeComponent();

            TreeListExControl = treeList;
            OutPutRadioGroup = radioGroup;
            RadioGroupType = RadioGroupType.ActiveAll;
        }

        protected override void InitGrid()
        {
            TreeListExControl.AddColumn("UserGroupId", false);
            TreeListExControl.AddColumn("UpperUserGroupId", false);
            TreeListExControl.AddColumn("UserGroupName");            
            TreeListExControl.AddColumn("UserGroupName2");
            TreeListExControl.AddColumn("UserGroupName3");
            TreeListExControl.AddColumn("Description");
            TreeListExControl.AddColumn("Active");
            TreeListExControl.AddColumn("UpdateId");
            TreeListExControl.AddColumn("UpdateTime");
            TreeListExControl.AddColumn("UpdateClass");
            TreeListExControl.AddColumn("CreateId");
            TreeListExControl.AddColumn("CreateTime");
            TreeListExControl.AddColumn("CreateClass");

            TreeListExControl.ParentFieldName = "UpperUserGroupId";
            TreeListExControl.KeyFieldName = "UserGroupId";

            TreeListExControl.SetTreeListOption(false);
            TreeListExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit()
            {
                ValueChecked = "Y",
                ValueUnchecked = "N"
            };

            TreeListExControl.TreeList.Columns["Active"].ColumnEdit = checkEdit;
            TreeListExControl.TreeList.Columns["UpdateTime"].ColumnEdit = HKInc.Service.Factory.RepositoryFactory.GetRepositoryItemDate(DateFormat.DateAndTime);
            TreeListExControl.TreeList.Columns["CreateTime"].ColumnEdit = HKInc.Service.Factory.RepositoryFactory.GetRepositoryItemDate(DateFormat.DateAndTime);
        }

        protected override void DataLoad()
        {
            UserGroupService.ReLoad();

            string radioValue = radioGroup.EditValue.GetNullToEmpty();
            TreeListBindingSource.DataSource = UserGroupService.GetList(p => (p.UserGroupName.Contains(textUserGroupName.Text) || p.UserGroupName2.Contains(textUserGroupName.Text) || p.UserGroupName3.Contains(textUserGroupName.Text)) &&
                                                                             (string.IsNullOrEmpty(radioValue) ? true : p.Active == radioValue))
                                                               .OrderBy(p=>p.UpperUserGroupId)
                                                               .OrderBy(p=>p.UserGroupId).ToList();
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