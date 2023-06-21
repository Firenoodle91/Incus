using System;
using System.Linq;

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;

using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;

namespace HKInc.Ui.View.SYS
{
    public partial class MenuList : HKInc.Service.Base.TreeListFormTemplate
    {
        IService<Menu> MenuService = (IService<Menu>)ServiceFactory.GetDomainService("Menu");

        public MenuList()
        {
            InitializeComponent();

            TreeListExControl = treeList;
            OutPutRadioGroup = radioGroup1;
            RadioGroupType = RadioGroupType.ActiveAll;
        }

        protected override void InitGrid()
        {
            TreeListExControl.SetTreeListOption(false);
            TreeListExControl.TreeList.StateImageList = HKInc.Utils.Common.GlobalVariable.IconImageCollection;

            TreeListExControl.AddColumn("MenuId", false);            
            TreeListExControl.AddColumn("UpperMenuId", false);
            TreeListExControl.AddColumn("MenuName");
            TreeListExControl.AddColumn("MenuName2");
            TreeListExControl.AddColumn("MenuName3");
            TreeListExControl.AddColumn("SortOrder", DevExpress.Utils.HorzAlignment.Center);
            TreeListExControl.AddColumn("ScreenId", false);
            TreeListExControl.AddColumn("Screen.ScreenName", LabelConvert.GetLabelText("ScreenName"));
            TreeListExControl.AddColumn("MenuPath", false);
            TreeListExControl.AddColumn("Description");
            TreeListExControl.AddColumn("IconIndex", DevExpress.Utils.HorzAlignment.Center);
            TreeListExControl.AddColumn("Active");
            TreeListExControl.AddColumn("UpdateId");
            TreeListExControl.AddColumn("UpdateTime", DevExpress.Utils.HorzAlignment.Center);
            TreeListExControl.AddColumn("CreateId");
            TreeListExControl.AddColumn("CreateTime", DevExpress.Utils.HorzAlignment.Center);

            TreeListExControl.ParentFieldName = "UpperMenuId";
            TreeListExControl.KeyFieldName = "MenuId";
            
            TreeListExControl.BestFitColumns();

            TreeListExControl.TreeList.GetStateImage += TreeList_GetStateImage;
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
            MenuService.ReLoad();

            string radioValue = radioGroup1.EditValue.GetNullToEmpty();
            TreeListBindingSource.DataSource = MenuService.GetList(p=> (p.MenuName.Contains(textMenuName.Text) || p.MenuName2.Contains(textMenuName.Text) || p.MenuName3.Contains(textMenuName.Text)) &&
                                                                       (string.IsNullOrEmpty(radioValue) ? true : p.Active == radioValue))
                                                          .OrderBy(p=>p.UpperMenuId)
                                                          .OrderBy(p=>p.SortOrder).ToList();
            TreeListExControl.DataSource = TreeListBindingSource;

            SetRefreshMessage(TreeListBindingSource.Count);

            TreeListExControl.BestFitColumns();
            TreeListExControl.ExpandAll();
        }

        protected override void DataSave()
        {
            MenuService.Save();            
            DataLoad();
        }        

        #region 추상함수 구현
        protected override void DeleteRow()
        {
            Menu obj = TreeListBindingSource.Current as Menu;

            if (obj != null)
            {
                obj.Active = "N";
              //  MenuService.Delete(obj);
                TreeListBindingSource.RemoveCurrent();
            }
        }
        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            
            return HKInc.Ui.View.PopupFactory.ProductionPopupFactory.GetPopupForm(HKInc.Ui.View.PopupFactory.ProductionPopupView.Menu, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, MenuService);
            return param;
        }
        #endregion

        void TreeList_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            TreeList tree = sender as TreeList;
            if (tree == null) return;

            e.NodeImageIndex = e.Node["IconIndex"].GetIntNullToZero();
        }
    }
}