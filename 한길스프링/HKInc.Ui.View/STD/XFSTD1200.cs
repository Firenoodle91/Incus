using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;

namespace HKInc.Ui.View.STD
{
    //부서관리화면
    public partial class XFSTD1200 : HKInc.Service.Base.TreeListMasterDetailFormTemplate
    {
        IService<TN_STD1200> DepartmentService = (IService<TN_STD1200>)ProductionFactory.GetDomainService("TN_STD1200");
        IService<HKInc.Ui.Model.Domain.User> UserService = (IService<HKInc.Ui.Model.Domain.User>)ServiceFactory.GetDomainService("User");
        public XFSTD1200()
        {
            InitializeComponent();
            TreeListExControl = treeList;
            DetailGridExControl = gridEx;
            OutPutRadioGroup = radioGroup1;
            RadioGroupType = RadioGroupType.ActiveAll;
        }
        protected override void InitGrid()
        {
            TreeListExControl.SetTreeListOption(false);
            TreeListExControl.TreeList.StateImageList = HKInc.Utils.Common.GlobalVariable.IconImageCollection;
            IsTreeListButtonExportEnabled = true;

            TreeListExControl.AddColumn("DepartmentCode");
            TreeListExControl.AddColumn("DepartmentName");
            TreeListExControl.AddColumn("DepartmentManager");
            TreeListExControl.AddColumn("ParentDepartmentCode", false);

            TreeListExControl.AddColumn("UseFlag");
            TreeListExControl.AddColumn("UpdateId", false);
            TreeListExControl.AddColumn("UpdateTime", false);
            TreeListExControl.AddColumn("CreateId", false);
            TreeListExControl.AddColumn("CreateTime", false);

            TreeListExControl.ParentFieldName = "ParentDepartmentCode";
            TreeListExControl.KeyFieldName = "DepartmentCode";

            TreeListExControl.BestFitColumns();

            DetailGridExControl.MainGrid.AddColumn("LoginId");
            DetailGridExControl.MainGrid.AddColumn("UserName");
            DetailGridExControl.MainGrid.AddColumn("EmployeeNo");
            DetailGridExControl.MainGrid.AddColumn("Rank");
            DetailGridExControl.MainGrid.AddColumn("GroupCode");
            //DetailGridExControl.MainGrid.AddColumn("DepartmentCode");
            DetailGridExControl.MainGrid.AddColumn("Email");
            DetailGridExControl.MainGrid.AddColumn("CellPhone");
            //DetailGridExControl.MainGrid.AddColumn("SequenceNumber");
            DetailGridExControl.MainGrid.AddColumn("HireDate", HorzAlignment.Far, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("DischargeDate", HorzAlignment.Far, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("Active");

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
            TreeListExControl.TreeList.Columns["DepartmentManager"].ColumnEdit = HKInc.Service.Factory.RepositoryFactory.GetRepositoryItemLookUpEdit(UserService.GetList(), "UserId", "UserName");
            TreeListExControl.TreeList.Columns["UpdateTime"].ColumnEdit = HKInc.Service.Factory.RepositoryFactory.GetRepositoryItemDate(DateFormat.DateAndTime);
            TreeListExControl.TreeList.Columns["CreateTime"].ColumnEdit = HKInc.Service.Factory.RepositoryFactory.GetRepositoryItemDate(DateFormat.DateAndTime);

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DepartmentCode", DepartmentService.GetList(), "DepartmentCode", "DepartmentName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Rank", MasterCode.GetMasterCode((int)MasterCodeEnum.UserRankCode).ToList());
        }

        protected override void DataLoad()
        {            
            DetailGridExControl.MainGrid.Clear();

            DepartmentService.ReLoad();

            string radioValue = radioGroup1.EditValue.GetNullToEmpty();
            
            TreeListBindingSource.DataSource = DepartmentService.GetList(p => (p.DepartmentName.Contains(textDepartmentName.Text)) &&
                                                                              (string.IsNullOrEmpty(radioValue) ? true : p.UseFlag == radioValue))
                                                           .OrderBy(p => p.ParentDepartment).ThenBy(p=>p.DepartmentCode)                                                          
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
                    HKInc.Service.Handler.MessageBoxHandler.Show(MessageHelper.GetStandardMessage(10), HKInc.Service.Factory.HelperFactory.GetLabelConvert().GetLabelText("Confirm"));
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
            HKInc.Ui.Model.Domain.User user = (HKInc.Ui.Model.Domain.User)DetailGridBindingSource.Current;

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

            IPopupForm form = HKInc.Ui.View.PopupFactory.ProductionPopupFactory.GetPopupForm(HKInc.Ui.View.PopupFactory.ProductionPopupView.UserSelectList, param, AddNewUserGroup);
            
            form.ShowPopup(true);
        }

        private void AddNewUserGroup(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            List<decimal> userIdList = (List<decimal>)((DataParam)e.Map.GetValue(PopupParameter.DataParam)).GetValue("UserIdList");
            TN_STD1200 department = TreeListBindingSource.Current as TN_STD1200;

            foreach (var userId in userIdList)
            {
                // 기존값과비교 중복이면 추가하지않는다.
                List<HKInc.Ui.Model.Domain.User> userList = (List<HKInc.Ui.Model.Domain.User>)DetailGridBindingSource.DataSource;
                if (!userList.Any(p => p.UserId == userId))
                {
                    HKInc.Ui.Model.Domain.User userToAdd = UserService.GetList(p => p.UserId == userId).FirstOrDefault();
                    userToAdd.DepartmentCode =department.DepartmentCode;

                    DetailGridBindingSource.Add(userToAdd);
                    UserService.Update(userToAdd);
                }
            }
            if (userIdList.Count > 0) IsFormControlChanged = true;
            DetailGridExControl.BestFitColumns();
        }
    }
}