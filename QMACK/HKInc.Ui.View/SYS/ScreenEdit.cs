using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;

namespace HKInc.Ui.View.SYS
{
    public partial class ScreenEdit : HKInc.Service.Base.ListEditFormTemplate
    {                
        private IService<Screen> ScreenService;

        public ScreenEdit(PopupDataParam param, PopupCallback callback)
        { 
            InitializeComponent();

            PopupParam = param;
            Callback = callback;

            ModelBindingSource = screenBindingSource; // BindingSource설정
        }
        
        protected override void AddControlList() // abstract함수 구현
        {            
            ControlEnableList.Add("ModuleId", lupModuleId);
            ControlEnableList.Add("ScreenName", textScreenName);
            ControlEnableList.Add("ScreenName2", textScreenName2);
            ControlEnableList.Add("ScreenName3", textScreenName3);
            ControlEnableList.Add("NameSpace", textNameSpace);
            ControlEnableList.Add("ClassName", textClassName);
            ControlEnableList.Add("IconIndex", textIconIndex);
            ControlEnableList.Add("LargeIconIndex", textLargeIconIndex);
            ControlEnableList.Add("Description", memoDescription);
            ControlEnableList.Add("Active", chkActive);

            LayoutControlHandler.SetRequiredLabelText<Screen>(new Screen(), ControlEnableList, this.Controls);
        }
        
        protected override void InitBindingSource()
        {
            base.InitBindingSource();

            // Service설정 부모에게서 넘어온다
            ScreenService = (IService<Screen>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            IService<Module> moduleService = (IService<Module>)ServiceFactory.GetDomainService("Module");
            lupModuleId.SetDefault(false, "ModuleId", HKInc.Service.Helper.LookUpFieldHelper.GetCultureFieldName("Module"));
            lupModuleId.DataSource = moduleService.GetList();
        }

        protected override void DataLoad()
        {
            if(EditMode == PopupEditMode.New) // 신규 추가
            {
                screenBindingSource.Add(new Screen() { Active = "Y" });
                screenBindingSource.MoveLast();                
            }
            else
            {  // Update
                screenBindingSource.DataSource = (Screen)PopupParam.GetValue(PopupParameter.KeyValue);
            }            
        }

        protected override void DataSave()
        {
            screenBindingSource.EndEdit(); //저장전 수정사항 Posting

            if(EditMode == PopupEditMode.New)
                screenBindingSource.DataSource = ScreenService.Insert((Screen)screenBindingSource.Current);
            else
                ScreenService.Update((Screen)screenBindingSource.Current);

            ScreenService.Save();

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }    
    }
}