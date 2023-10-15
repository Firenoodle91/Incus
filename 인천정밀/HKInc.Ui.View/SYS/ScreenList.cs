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
    /// <summary>
    /// 화면관리
    /// </summary>
    public partial class ScreenList : HKInc.Service.Base.ListFormTemplate
    {
        IService<Screen> ScreenService = (IService<Screen>)ServiceFactory.GetDomainService("Screen");

        public ScreenList()
        {
            InitializeComponent();

            GridExControl = gridEx1;
            //OutPutRadioGroup = radioGroup;
            //RadioGroupType = RadioGroupType.ActiveAll;
            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));

            this.Text = LabelConvert.GetLabelText(this.Name);
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("ScreenId");
            GridExControl.MainGrid.AddColumn("ModuleId");
            GridExControl.MainGrid.AddColumn("ScreenName");
            GridExControl.MainGrid.AddColumn("ScreenName2", LabelConvert.GetLabelText("ScreenNameENG"));
            GridExControl.MainGrid.AddColumn("ScreenName3", LabelConvert.GetLabelText("ScreenNameCHN"));
            GridExControl.MainGrid.AddColumn("NameSpace");
            GridExControl.MainGrid.AddColumn("ClassName");
            GridExControl.MainGrid.AddColumn("Description", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.AddColumn("Active", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.AddColumn("CreateId");
            GridExControl.MainGrid.AddColumn("CreateTime");
            GridExControl.MainGrid.AddColumn("UpdateId");
            GridExControl.MainGrid.AddColumn("UpdateTime");

            GridExControl.MainGrid.SetEditable("Description");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            GridExControl.MainGrid.MainView.Columns["Description"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Description", UserRight.HasEdit);
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ModuleId", ScreenService.GetChildList<Module>(p => p.ModuleId == p.ModuleId), "ModuleId", Service.Helper.DataConvert.GetCultureDataFieldName("ModuleName", "ModuleName2", "ModuleName3"));

            var userList = ScreenService.GetChildList<User>(p => true).ToList();
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", userList, "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", userList, "LoginId", "UserName");

            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ScreenId");

            // 20210701 오세완 차장 하단에 inintReopsitory가 오류가 나지 않기 위해 추가
            GridExControl.MainGrid.Clear();
            ScreenService.ReLoad();
            
            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            //string radioValue = radioGroup1.EditValue.GetNullToEmpty();
            string radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();
            GridBindingSource.DataSource = ScreenService.GetList(p => (p.ScreenName.Contains(textScreenName.Text) || p.ScreenName2.Contains(textScreenName.Text) || p.ScreenName3.Contains(textScreenName.Text))
                                                                      && (radioValue == "A" ? true : p.Active == radioValue)
                                                                )
                                                                .OrderBy(p => p.ScreenId)
                                                                .ToList();
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