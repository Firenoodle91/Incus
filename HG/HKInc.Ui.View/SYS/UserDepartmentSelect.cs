using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using DevExpress.XtraGrid.Views.Grid;

using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;

namespace HKInc.Ui.View.SYS
{
    public partial class UserDepartmentSelect : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<UserDepartment> UserDepartmentService;
        BindingSource bindingSource = new BindingSource();
        bool IsmultiSelect = true;

        public UserDepartmentSelect()
        {
            InitializeComponent();
        }

        public UserDepartmentSelect(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;

            UserDepartmentService = (IService<UserDepartment>)ServiceFactory.GetDomainService("Department");

            if (this.PopupParam.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)PopupParam.GetValue(PopupParameter.IsMultiSelect);
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();

            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitControls()
        {
            base.InitControls();
            gridEx1.MainView.RowClick += MainView_RowClick;
            this.Text = "부서정보";
        }

        protected override void InitGrid()
        {
            gridEx1.Init();
            gridEx1.MultiSelect = IsmultiSelect;
            //gridEx1.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

          //  gridEx1.AddColumn("CompanySeq", false);
            gridEx1.AddColumn("DepartmentCode");
            gridEx1.AddColumn("DepartmentName");
            gridEx1.AddColumn("AbrDeptName");
            gridEx1.AddColumn("EngDeptName");
            gridEx1.AddColumn("AbrEngDeptName");

            gridEx1.AddColumn("BeginDate");
            gridEx1.AddColumn("EndDate");
            gridEx1.AddColumn("SMDeptType",false);
            gridEx1.AddColumn("SMDeptClass",false);
            gridEx1.AddColumn("UseFlag");

            gridEx1.BestFitColumns();
            gridEx1.GridLayoutRestore();
        }

        protected override void InitRepository()
        {
            gridEx1.SetRepositoryItemDateEdit("BeginDate");
            gridEx1.SetRepositoryItemDateEdit("EndDate");
            gridEx1.SetRepositoryItemCheckEdit("UseFlag", "N");
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            bindingSource.DataSource = UserDepartmentService.GetList(p => p.UseFlag == "Y")
                                                            .OrderBy(p => p.DepartmentCode).ToList();
            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();
            if (IsmultiSelect)
            {
                //List<UserDepartment> UserDepartmentList = new List<UserDepartment>();

                //foreach (var rowHandle in gridEx1.MainView.GetSelectedRows())
                //    UserDepartmentList.Add(gridEx1.MainView.GetRowCellValue(rowHandle, "UserId"));

                //DataParam map = new DataParam();
                //map.SetValue("UserIdList", userIdList);
                
                //param.SetValue(Utils.Enum.PopupParameter.DataParam, map);

                var returnList = new List<UserDepartment>();

                foreach (var rowHandle in gridEx1.MainView.GetSelectedRows())
                {
                    string DepartmentCode = gridEx1.MainView.GetRowCellValue(rowHandle, "DepartmentCode").GetNullToEmpty();//.GetIntNullToZero();
                   // int CompanySeq = gridEx1.MainView.GetRowCellValue(rowHandle, "CompanySeq").GetIntNullToZero();

                    var obj = UserDepartmentService.GetList(p => p.DepartmentCode == DepartmentCode  ).FirstOrDefault();
                    if (obj != null)
                        returnList.Add(UserDepartmentService.Detached(obj));
                }

                param.SetValue(PopupParameter.ReturnObject, returnList);

            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, UserDepartmentService.Detached((UserDepartment)bindingSource.Current));            
            }
            ReturnPopupArgument = new PopupArgument(param);

            base.ActClose();
        }


        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            if (e.Clicks == 2)
            {
                var obj = (UserDepartment)bindingSource.Current;

                if (IsmultiSelect)
                {
                    var returnList = new List<UserDepartment>();
                    if (obj != null)
                        returnList.Add(UserDepartmentService.Detached(obj));

                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, UserDepartmentService.Detached(obj));
                }
                ReturnPopupArgument = new PopupArgument(param);

                base.ActClose();
            }            
        }
    }
}
