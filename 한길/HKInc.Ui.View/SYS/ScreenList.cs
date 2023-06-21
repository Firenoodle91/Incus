using System.Linq;
using DevExpress.XtraGrid.Columns;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;

namespace HKInc.Ui.View.SYS
{
    public partial class ScreenList : HKInc.Service.Base.ListFormTemplate
    {
        IService<Screen> ScreenService = (IService<Screen>)ServiceFactory.GetDomainService("Screen");

        public ScreenList()
        {
            InitializeComponent();

            GridExControl = gridEx1;
            OutPutRadioGroup = radioGroup;
            RadioGroupType = RadioGroupType.ActiveAll;
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("ScreenId");
            GridExControl.MainGrid.AddColumn("ModuleId");
            GridExControl.MainGrid.AddColumn("ScreenName");
            GridExControl.MainGrid.AddColumn("ScreenName2");
            GridExControl.MainGrid.AddColumn("ScreenName3");
            GridExControl.MainGrid.AddColumn("NameSpace");
            GridExControl.MainGrid.AddColumn("ClassName");
            GridExControl.MainGrid.AddColumn("IconIndex");
            GridExControl.MainGrid.AddColumn("LargeIconIndex");
            GridExControl.MainGrid.AddColumn("Description");
            GridExControl.MainGrid.AddColumn("Active");
            GridExControl.MainGrid.AddColumn("UpdateId");
            GridExControl.MainGrid.AddColumn("UpdateTime");
            GridExControl.MainGrid.AddColumn("UpdateClass");
            GridExControl.MainGrid.AddColumn("CreateId");
            GridExControl.MainGrid.AddColumn("CreateTime");
            GridExControl.MainGrid.AddColumn("CreateClass");

            GridExControl.MainGrid.SetEditable("Description");

            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateTime");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateTime");
            GridExControl.MainGrid.MainView.Columns["Description"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ModuleId", 
                                                               ScreenService.GetChildList<Module>(p => p.ModuleId == p.ModuleId),
                                                               "ModuleId", 
                                                               Service.Helper.LookUpFieldHelper.GetCultureFieldName("Module"));
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ScreenId");
            ScreenService.ReLoad();

            string radioValue = radioGroup.EditValue.GetNullToEmpty();
            GridBindingSource.DataSource = ScreenService.GetList(p => (p.ScreenName.Contains(textScreenName.Text) || p.ScreenName2.Contains(textScreenName.Text) || p.ScreenName3.Contains(textScreenName.Text)) &&
                                                                      (string.IsNullOrEmpty(radioValue) ? true : p.Active == radioValue))
                                                        .OrderBy(p => p.NameSpace).ThenBy(p => p.ScreenName).ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);            
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            ScreenService.Save();            
            DataLoad();
        }

        #region 추상함수 구현
        protected override void DeleteRow()
        {
            Screen obj = GridBindingSource.Current as Screen;

            if (obj != null)
            {
                ScreenService.Delete(obj);
                GridBindingSource.RemoveCurrent();
            }
        }
        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return PopupFactory.ProductionPopupFactory.GetPopupForm(PopupFactory.ProductionPopupView.Screen, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ScreenService);
            return param;
        }
        #endregion
    }
}