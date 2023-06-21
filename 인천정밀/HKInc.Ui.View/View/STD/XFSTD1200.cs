using System.Collections.Generic;
using System.Data;
using System.Linq;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// 부서관리화면
    /// </summary>
    public partial class XFSTD1200 : HKInc.Service.Base.TreeListMasterDetailFormTemplate
    {
        IService<TN_STD1200> DepartmentService = (IService<TN_STD1200>)ProductionFactory.GetDomainService("TN_STD1200");
        IService<HKInc.Ui.Model.Domain.User> UserService = (IService<HKInc.Ui.Model.Domain.User>)ServiceFactory.GetDomainService("User");

        public XFSTD1200()
        {
            InitializeComponent();
            TreeListExControl = treeList;
            DetailGridExControl = gridEx;
            //OutPutRadioGroup = radioGroup1;
            //RadioGroupType = RadioGroupType.ActiveAll;
            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }
        protected override void InitGrid()
        {
            TreeListExControl.SetTreeListOption(false);
            TreeListExControl.TreeList.StateImageList = HKInc.Utils.Common.GlobalVariable.IconImageCollection;
            //IsTreeListButtonExportEnabled = true;
            //TreeListExControl.SetToolbarButtonCaption(GridToolbarButton.Export, LabelConvert.GetLabelText("Export"));

            TreeListExControl.AddColumn("DepartmentCode", LabelConvert.GetLabelText("DepartmentCode"));
            TreeListExControl.AddColumn("DepartmentName", LabelConvert.GetLabelText("DepartmentName"));
            TreeListExControl.AddColumn("DepartmentManager", LabelConvert.GetLabelText("DepartmentManager"));
            TreeListExControl.AddColumn("ParentDepartmentCode", LabelConvert.GetLabelText("ParentDepartmentCode"), false);
            TreeListExControl.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            TreeListExControl.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            TreeListExControl.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            TreeListExControl.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            TreeListExControl.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);

            TreeListExControl.ParentFieldName = "ParentDepartmentCode";
            TreeListExControl.KeyFieldName = "DepartmentCode";

            TreeListExControl.BestFitColumns();

            DetailGridExControl.MainGrid.AddColumn("LoginId");
            DetailGridExControl.MainGrid.AddColumn("UserName");
            DetailGridExControl.MainGrid.AddColumn("EmployeeNo");
            DetailGridExControl.MainGrid.AddColumn("RankCode", LabelConvert.GetLabelText("Rank"));
            DetailGridExControl.MainGrid.AddColumn("ProductTeamCode", LabelConvert.GetLabelText("ProductTeam"));
            DetailGridExControl.MainGrid.AddColumn("Email");
            DetailGridExControl.MainGrid.AddColumn("CellPhone", LabelConvert.GetLabelText("PhoneNumber"));
            DetailGridExControl.MainGrid.AddColumn("HireDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("DischargeDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            DetailGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            DetailGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            DetailGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);

            DetailGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit()
            {
                ValueChecked = "Y",
                ValueUnchecked = "N"
            };
            TreeListExControl.TreeList.Columns["UseFlag"].ColumnEdit = checkEdit;
            TreeListExControl.TreeList.Columns["DepartmentManager"].ColumnEdit = RepositoryFactory.GetRepositoryItemLookUpEdit(UserService.GetList(), "LoginId", "UserName");
            TreeListExControl.TreeList.Columns["CreateId"].ColumnEdit = RepositoryFactory.GetRepositoryItemLookUpEdit(DepartmentService.GetChildList<User>(p => true), "LoginId", "UserName");
            TreeListExControl.TreeList.Columns["UpdateId"].ColumnEdit = RepositoryFactory.GetRepositoryItemLookUpEdit(DepartmentService.GetChildList<User>(p => true), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("ProductTeamCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("RankCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.RankCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", DepartmentService.GetChildList<User>(p => true), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", DepartmentService.GetChildList<User>(p => true), "LoginId", "UserName");

        }

        protected override void DataLoad()
        {
            DetailGridExControl.MainGrid.Clear();

            DepartmentService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624


            //string radioValue = radioGroup1.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();
            TreeListBindingSource.DataSource = DepartmentService.GetList(p => (p.DepartmentName.Contains(textDepartmentName.Text)) &&
                                                                              (radioValue == "A" ? true : p.UseFlag == radioValue))
                                                           .OrderBy(p => p.ParentDepartment).ThenBy(p => p.DepartmentCode)
                                                          .ToList();

            TreeListExControl.DataSource = TreeListBindingSource;
            TreeListExControl.BestFitColumns();

            SetRefreshMessage(TreeListBindingSource.Count);

            DetailGridExControl.BestFitColumns();
            TreeListExControl.ExpandAll();
        }
        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            DepartmentService.Save();
            UserService.Save();
            DataLoad();
        }


        protected override void MasterFocusedRowChanged()
        {
            TN_STD1200 obj = TreeListBindingSource.Current as TN_STD1200;
            if (obj != null)
            {
                UserService.ReLoad();
                DetailGridBindingSource.DataSource = UserService.GetList(p => p.DepartmentCode == obj.DepartmentCode);
                DetailGridExControl.DataSource = DetailGridBindingSource;
            }
            else
            {
                DetailGridExControl.MainGrid.Clear();
            }
            DetailGridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            TN_STD1200 obj = TreeListBindingSource.Current as TN_STD1200;

            if (obj != null)
            {
                if (DetailGridBindingSource.Count > 0)
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_10));
                    return;
                }
                DepartmentService.Delete(obj);
                TreeListBindingSource.RemoveCurrent();
            }
        }
        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFSTD1200, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, DepartmentService);
            return param;
        }

        protected override void DeleteDetailRow()
        {
            var user = (Model.Domain.User)DetailGridBindingSource.Current;

            if (user != null)
            {
                user.DepartmentCode = null;
                UserService.Update(user);

                DetailGridBindingSource.RemoveCurrent();
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_STD1200 department = TreeListBindingSource.Current as TN_STD1200;
            if (department == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, "LoginId");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.UserSelectList, param, AddNewUserGroup);

            form.ShowPopup(true);
        }

        private void AddNewUserGroup(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            List<object> returnList = (List<object>)((DataParam)e.Map.GetValue(PopupParameter.DataParam)).GetValue("returnList");
            TN_STD1200 department = TreeListBindingSource.Current as TN_STD1200;

            foreach (var loginId in returnList)
            {
                // 기존값과비교 중복이면 추가하지않는다.
                List<HKInc.Ui.Model.Domain.User> userList = (List<HKInc.Ui.Model.Domain.User>)DetailGridBindingSource.DataSource;
                if (!userList.Any(p => p.LoginId == loginId.ToString()))
                {
                    HKInc.Ui.Model.Domain.User userToAdd = UserService.GetList(p => p.LoginId == loginId.ToString()).FirstOrDefault();
                    userToAdd.DepartmentCode = department.DepartmentCode;

                    DetailGridBindingSource.Add(userToAdd);
                    UserService.Update(userToAdd);
                }
            }
            if (returnList.Count > 0) IsFormControlChanged = true;
            DetailGridExControl.BestFitColumns();
        }
    }
}