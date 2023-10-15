using System.Linq;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Domain.VIEW;
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
            GridExControl.MainGrid.AddColumn("DepartmentCode", LabelConvert.GetLabelText("DepartmentCode"));
            GridExControl.MainGrid.AddColumn("RankCode", LabelConvert.GetLabelText("Rank"));
            GridExControl.MainGrid.AddColumn("Email");
            GridExControl.MainGrid.AddColumn("CellPhone", LabelConvert.GetLabelText("PhoneNumber"));
            GridExControl.MainGrid.AddColumn("HireDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("DischargeDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("Description", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.AddColumn("Active", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.AddColumn("MainYn", LabelConvert.GetLabelText("MainYn"), false);
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"), false);
            GridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            GridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            GridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            GridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Description");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("DepartmentCode", UserService.GetChildList<TN_STD1200>(p => p.UseFlag != "N"), "DepartmentCode", "DepartmentName");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("RankCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.RankCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("MainYn", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MainYn), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));            
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", UserService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.MainView.Columns["Description"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Description", UserRight.HasEdit);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", UserService.GetChildList<User>(p => true), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", UserService.GetChildList<User>(p => true), "LoginId", "UserName");

            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            //GridRowLocator.GetCurrentRow();
            // 20210810 오세완 차장 grid focus기능 없어서 추가
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("UserId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear(); // 20210810 오세완 차장 이게 없으면 하단에 InitRepository가 팝업창을 닫은 후에 오류가 발생한다. 
            UserService.ReLoad();

            //InitCombo(); //phs20210624 20210810 오세완 차장 initcombo를 재정의한게 없어서 생략처리
            InitRepository();//phs20210624

            //string radioValue = radioGroup.EditValue.GetNullToEmpty();
            string radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();
            GridBindingSource.DataSource = UserService.GetList(p => p.UserName.Contains(textUserName.Text) &&
                                                                   (radioValue == "A" ? true : p.Active == radioValue)
                                                              )
                                                              .OrderBy(p => p.UserName)
                                                              .ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(GridExControl.MainGrid.RecordCount);

            //GridExControl.BestFitColumns(); // 20210810 오세완 차장 순서 변경, 
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