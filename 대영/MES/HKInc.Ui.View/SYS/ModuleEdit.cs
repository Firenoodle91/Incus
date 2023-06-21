using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using System.Linq;

namespace HKInc.Ui.View.SYS
{
    /// <summary>
    /// 모듈관리 팝업창
    /// </summary>
    public partial class ModuleEdit : HKInc.Service.Base.ListEditFormTemplate
    {                
        private IService<Module> ModuleService;
        
        public ModuleEdit(PopupDataParam param, PopupCallback callback)
        { 
            InitializeComponent();

            PopupParam = param;
            Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Name);
            ModelBindingSource = moduleBindingSource; // BindingSource설정            
        }
        
        protected override void AddControlList() // abstract함수 구현
        {
            ControlEnableList.Add("Assembly", textAssembly);
            ControlEnableList.Add("ModuleName", textModuleName);
            ControlEnableList.Add("ModuleName2", textModuleName2);
            ControlEnableList.Add("ModuleName3", textModuleName3);
            ControlEnableList.Add("Description", memoDescription);

            LayoutControlHandler.SetRequiredLabelText<Module>(new Module(), ControlEnableList, this.Controls);
        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();
            
            // Service설정 부모에게서 넘어온다
            ModuleService = (IService<Module>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            dateCreteTime.SetFormat(DateFormat.DateAndTimeSecond);
            dateUpdateTime.SetFormat(DateFormat.DateAndTimeSecond);
            var userList = ModuleService.GetChildList<User>(p => true).ToList();
            lupCreateId.SetDefault(false, "LoginId", "UserName", userList);
            lupUpdateId.SetDefault(false, "LoginId", "UserName", userList);
        }

        protected override void DataLoad()
        {
            if(EditMode == PopupEditMode.New) // 신규 추가
            {
                moduleBindingSource.Add(new Module());
                moduleBindingSource.MoveLast();
            }
            else
            {  // Update
                moduleBindingSource.DataSource = (Module)PopupParam.GetValue(PopupParameter.KeyValue);
            }            
        }

        protected override void DataSave()
        {
            moduleBindingSource.EndEdit(); //저장전 수정사항 Posting

            if(EditMode == PopupEditMode.New)
                moduleBindingSource.DataSource = ModuleService.Insert((Module)moduleBindingSource.Current);
            else
                ModuleService.Update((Module)moduleBindingSource.Current);
            
            ModuleService.Save();

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }    
    }
}