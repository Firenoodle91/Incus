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
using HKInc.Ui.Model.Domain.VIEW;

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 부서 선택창
    /// </summary>
    public partial class DepartmentSelect : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_STD1200> UserDepartmentService;
        BindingSource bindingSource = new BindingSource();
        bool IsmultiSelect = true;

        public DepartmentSelect()
        {
            InitializeComponent();
        }

        public DepartmentSelect(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;

            UserDepartmentService = (IService<TN_STD1200>)ServiceFactory.GetDomainService("TN_STD1200");

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

            this.Text = LabelConvert.GetLabelText("DepartmentList");            
        }

        protected override void InitGrid()
        {
            gridEx1.Init();
            gridEx1.MultiSelect = IsmultiSelect;

            gridEx1.AddColumn("DepartmentCode", LabelConvert.GetLabelText("DepartmentCode"));
            gridEx1.AddColumn("DepartmentName", LabelConvert.GetLabelText("DepartmentName"));
            gridEx1.AddColumn("DepartmentManager", LabelConvert.GetLabelText("DepartmentManager"));
            gridEx1.AddColumn("ParentDepartmentCode", LabelConvert.GetLabelText("ParentDepartmentCode"), false);

            gridEx1.GridLayoutRestore();
        }

        protected override void InitRepository()
        {
            gridEx1.SetRepositoryItemSearchLookUpEdit("DepartmentManager", UserDepartmentService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");

            gridEx1.BestFitColumns();
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
                var returnList = new List<TN_STD1200>();

                foreach (var rowHandle in gridEx1.MainView.GetSelectedRows())
                {
                    string DepartmentCode = gridEx1.MainView.GetRowCellValue(rowHandle, "DepartmentCode").GetNullToEmpty();
                    var obj = UserDepartmentService.GetList(p => p.DepartmentCode == DepartmentCode).FirstOrDefault();
                    if (obj != null)
                        returnList.Add(UserDepartmentService.Detached(obj));
                }

                param.SetValue(PopupParameter.ReturnObject, returnList);

            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, UserDepartmentService.Detached((TN_STD1200)bindingSource.Current));            
            }
            ReturnPopupArgument = new PopupArgument(param);

            base.ActClose();
        }


        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            if (e.Clicks == 2)
            {
                var obj = (TN_STD1200)bindingSource.Current;

                if (IsmultiSelect)
                {
                    var returnList = new List<TN_STD1200>();
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
