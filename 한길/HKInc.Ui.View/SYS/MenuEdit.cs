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
    public partial class MenuEdit : HKInc.Service.Base.ListEditFormTemplate
    {                
        private IService<HKInc.Ui.Model.Domain.Menu> MenuService;        
        private BindingSource GroupMenuBindingSource = new BindingSource();

        public MenuEdit(PopupDataParam param, PopupCallback callback)
        { 
            InitializeComponent();

            PopupParam = param;
            Callback = callback;

            GridExControl = gridEx1;  //  Grid설정
            ModelBindingSource = menuBindingSource; // BindingSource설정                      
        }
        
        protected override void AddControlList() // abstract함수 구현 InitControl에서 Call 한다
        {            
            ControlEnableList.Add("ScreenId", lupScreenId);
            ControlEnableList.Add("UpperMenuId", lupUpperMenuId);
            ControlEnableList.Add("MenuName", textMenuName);
            ControlEnableList.Add("MenuName2", textMenuName2);
            ControlEnableList.Add("MenuName3", textMenuName3);
            ControlEnableList.Add("SortOrder", textSortOrder);
            ControlEnableList.Add("MenuPath", textMenuPath);
            ControlEnableList.Add("Description", memoDescription);
            ControlEnableList.Add("IconIndex", textIconIndex);
            ControlEnableList.Add("Active", chkActive);

            LayoutControlHandler.SetRequiredLabelText<HKInc.Ui.Model.Domain.Menu>(new HKInc.Ui.Model.Domain.Menu(), ControlEnableList, this.Controls);
        }
        
        protected override void InitBindingSource()
        {            
            // Service설정 부모에게서 넘어온다
            MenuService = (IService<HKInc.Ui.Model.Domain.Menu>)PopupParam.GetValue(PopupParameter.Service);                 
        }

        protected override void InitCombo()
        {
            List<HKInc.Ui.Model.Domain.Menu> upperMenuList = MenuService.GetList(p => p.Active == "Y" || string.IsNullOrEmpty(p.Active));
            //lupUpperMenuId.SetDefault(true, "MenuId", HKInc.Service.Helper.LookUpFieldHelper.GetCultureFieldName("Menu"), upperMenuList);
            lupUpperMenuId.ParentFieldName = "UpperMenuId";
            lupUpperMenuId.KeyFieldName = "MenuId";
            lupUpperMenuId.DisplayMember = HKInc.Service.Helper.LookUpFieldHelper.GetCultureFieldName("Menu");
            lupUpperMenuId.ValueMember = "MenuId";
            lupUpperMenuId.ShowColumns = false;
            lupUpperMenuId.AddColumn(HKInc.Service.Helper.LookUpFieldHelper.GetCultureFieldName("Menu"));
            lupUpperMenuId.AddColumn("IconIndex", false);                        
            lupUpperMenuId.DataSource = upperMenuList;
            lupUpperMenuId.ExpandAll();

            List<HKInc.Ui.Model.Domain.Screen> screenList = MenuService.GetChildList<HKInc.Ui.Model.Domain.Screen>(p => p.Active == "Y" || string.IsNullOrEmpty(p.Active));
            lupScreenId.SetDefault(true, "ScreenId", HKInc.Service.Helper.LookUpFieldHelper.GetCultureFieldName("Screen"), screenList);

            HKInc.Utils.Images.IconImageCollection imageCollection = GlobalVariable.IconImageCollection;
            imageCollection.SetImageComboBoxEdit(textIconIndex);
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.Init();

            gridEx1.MainGrid.AddColumn("GroupMenuId", false);
            gridEx1.MainGrid.AddColumn("MenuId", false);
            gridEx1.MainGrid.AddColumn("UserGroupId");
            gridEx1.MainGrid.AddColumn("Read");
            gridEx1.MainGrid.AddColumn("Insert");
            gridEx1.MainGrid.AddColumn("Write");
            gridEx1.MainGrid.AddColumn("Export");
            gridEx1.MainGrid.AddColumn("Print");
            gridEx1.MainGrid.AddColumn("UpdateId");
            gridEx1.MainGrid.AddColumn("UpdateTime");
            gridEx1.MainGrid.AddColumn("UpdateClass");
            gridEx1.MainGrid.AddColumn("CreateId");
            gridEx1.MainGrid.AddColumn("CreateTime");
            gridEx1.MainGrid.AddColumn("CreateClass");

            gridEx1.MainGrid.SetEditable(UserRight.HasEdit);
            gridEx1.MainGrid.SetEditable("Read", "Insert", "Write", "Export", "Print");
            

            gridEx1.BestFitColumns();
        }

        protected override void InitRepository()
        {
            List<UserGroup> userGroupList = MenuService.GetChildList<UserGroup>(p => p.Active == "Y" || string.IsNullOrEmpty(p.Active));

            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("UserGroupId", userGroupList, "UserGroupId", "UserGroupName");
            gridEx1.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateTime");
            gridEx1.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateTime");
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("Read", "N");
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("Insert", "N");
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("Write", "N");
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("Export", "N");
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("Print", "N");

        }

        protected override void DataLoad()
        {
            if(EditMode == PopupEditMode.New) // 신규 추가
            {
                menuBindingSource.Add(new HKInc.Ui.Model.Domain.Menu() { Active = "Y", GroupMenuList = new List<GroupMenu>() });
                menuBindingSource.MoveLast();                
            }
            else
            {  // Update                                     
               menuBindingSource.DataSource = (HKInc.Ui.Model.Domain.Menu)PopupParam.GetValue(PopupParameter.KeyValue);                
            }

            // Grid Binding 설정     
            GroupMenuBindingSource.AllowNew = true;
            GroupMenuBindingSource.DataSource = MenuService.GetChildList<GroupMenu>(p => p.MenuId == ((HKInc.Ui.Model.Domain.Menu)menuBindingSource.Current).MenuId);            

            gridEx1.DataSource = GroupMenuBindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void DataSave()
        {
            menuBindingSource.EndEdit(); //저장전 수정사항 Posting

            HKInc.Ui.Model.Domain.Menu obj = (HKInc.Ui.Model.Domain.Menu)menuBindingSource.Current;
           
            if (EditMode == PopupEditMode.New)             
                menuBindingSource.DataSource = MenuService.Insert(obj);                             
            else            
                menuBindingSource.DataSource = MenuService.Update(obj);
                         
            MenuService.Save();
            
            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }

        #region Button click Function
        protected override void DeleteGridRow()
        {
            GroupMenu obj = (GroupMenu)GroupMenuBindingSource.Current;

            if (obj != null)
            {
                GroupMenuBindingSource.RemoveCurrent();
                ((HKInc.Ui.Model.Domain.Menu)menuBindingSource.Current).GroupMenuList.Remove(obj);

                if(obj.GroupMenuId > 0)  // DB에 존재하는 데이터 이면 
                    MenuService.RemoveChild<GroupMenu>(obj);

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
            List<decimal> userGroupIdList = new List<decimal>();
            // obj에 return된 UserGroup으로 값설정
            // 기존값과비교 중복이면 추가하지않는다.
            if (e != null)
                userGroupIdList = (List<decimal>)((DataParam)e.Map.GetValue(PopupParameter.DataParam)).GetValue("UserGroupIdList");
            
            foreach(var id in userGroupIdList)
            {                
                if (!((HKInc.Ui.Model.Domain.Menu)menuBindingSource.Current).GroupMenuList.Any(p=>p.UserGroupId == id))
                {
                    GroupMenu obj = (GroupMenu)GroupMenuBindingSource.AddNew();
                    obj.UserGroupId = id;
                    obj.Read = "N";
                    obj.Insert = "N";
                    obj.Write = "N";
                    obj.Export = "N";
                    obj.Print = "N";
                    obj.UpdateId = GlobalVariable.LoginId;
                    obj.UpdateClass = GlobalVariable.CurrentInstance;
                    obj.UpdateTime = DateTime.Now;
                    obj.CreateId = GlobalVariable.LoginId;
                    obj.CreateClass = GlobalVariable.CurrentInstance;
                    obj.CreateTime = DateTime.Now;

                    ((HKInc.Ui.Model.Domain.Menu)menuBindingSource.Current).GroupMenuList.Add(obj);
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
            param.SetValue(PopupParameter.Service, MenuService);
            return param;
        }
        #endregion
    }
}