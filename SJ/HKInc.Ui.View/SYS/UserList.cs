using System.Linq;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Service;
using DevExpress.Utils;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.SYS
{
    /// <summary>
    /// 사용자관리
    /// </summary>
    public partial class UserList : HKInc.Service.Base.ListFormTemplate
    {
        IService<User> UserService = (IService<User>)ServiceFactory.GetDomainService("User");

        public UserList()
        {
            InitializeComponent();

            GridExControl = gridEx1;
            //OutPutRadioGroup = radioGroup;
            //RadioGroupType = RadioGroupType.ActiveAll;
            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("UserId");
            GridExControl.MainGrid.AddColumn("LoginId");
            GridExControl.MainGrid.AddColumn("UserName");
            GridExControl.MainGrid.AddColumn("EmployeeNo");
            GridExControl.MainGrid.AddColumn("ProductTeamCode", LabelConvert.GetLabelText("ProductTeam"));
            GridExControl.MainGrid.AddColumn("RankCode", LabelConvert.GetLabelText("Rank"));
            GridExControl.MainGrid.AddColumn("Email");
            GridExControl.MainGrid.AddColumn("CellPhone", LabelConvert.GetLabelText("PhoneNumber"));
            GridExControl.MainGrid.AddColumn("HireDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("DischargeDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("Description", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.AddColumn("Active", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Description");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ProductTeamCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("RankCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.RankCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));            
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            GridExControl.MainGrid.MainView.Columns["Description"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Description", UserRight.HasEdit);
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            UserService.ReLoad();

            //string radioValue = radioGroup.EditValue.GetNullToEmpty();
            string radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();
            GridBindingSource.DataSource = UserService.GetList(p => p.UserName.Contains(textUserName.Text) &&
                                                                   (radioValue == "A" ? true : p.Active == radioValue)
                                                              )
                                                              .OrderBy(p => p.UserName)
                                                              .ToList();
            GridExControl.DataSource = GridBindingSource;
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(GridExControl.MainGrid.RecordCount);

            GridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            UserService.Save();            
            DataLoad();
        }        

        #region 추상함수 구현
        protected override void DeleteRow()
        {
            User obj = GridBindingSource.Current as User;
            if (obj == null) return;

            var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("UserInfo"))
                                                    , LabelConvert.GetLabelText("Warning")
                                                    , System.Windows.Forms.MessageBoxButtons.YesNo);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                obj.Active = "N";
                GridExControl.BestFitColumns();
            }
        }
        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return HKInc.Ui.View.PopupFactory.ProductionPopupFactory.GetPopupForm(HKInc.Ui.View.PopupFactory.ProductionPopupView.User, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, UserService);
            return param;
        }
        #endregion
    }
}