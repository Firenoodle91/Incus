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
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Enum;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;

namespace HKInc.Ui.View.View.STD_POPUP
{
    public partial class XPFSTD1200 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_STD1200> DepartmentService;

        public XPFSTD1200()
        {
            InitializeComponent();
        }

        public XPFSTD1200(PopupDataParam param, PopupCallback callback) : this()
        {
            InitializeComponent();

            PopupParam = param;
            Callback = callback;

            ModelBindingSource = tNSTD1200BindingSource; // BindingSource설정           

            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
        }

        protected override void AddControlList() // abstract함수 구현
        {
            ControlEnableList.Add("DepartmentCode", textDepartmentCode);
            ControlEnableList.Add("DepartmentName", textDepartmentName);
            ControlEnableList.Add("DepartmentManager", lupDepartmentManager);
            ControlEnableList.Add("ParentDepartmentCode", treeListParentDepartment);
            ControlEnableList.Add("UseFlag", chkUseFlag);

            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
                control.Value.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ModelBindingSource, control.Key, true));
            }
            #endregion

            LayoutControlHandler.SetRequiredLabelText<TN_STD1200>(new TN_STD1200(), ControlEnableList, this.Controls);
        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();

            // Service설정 부모에게서 넘어온다
            DepartmentService = (IService<TN_STD1200>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            List<TN_STD1200> upperDepartmentList = DepartmentService.GetList(p => p.UseFlag == "Y" || string.IsNullOrEmpty(p.UseFlag));

            treeListParentDepartment.ParentFieldName = "ParentDepartmentCode";
            treeListParentDepartment.KeyFieldName = "DepartmentCode";
            treeListParentDepartment.DisplayMember = "DepartmentName";
            treeListParentDepartment.ValueMember = "DepartmentCode";
            treeListParentDepartment.ShowColumns = false;
            treeListParentDepartment.AddColumn("DepartmentName");
            treeListParentDepartment.AddColumn("DepartmentCode", false);
            treeListParentDepartment.DataSource = upperDepartmentList;
            treeListParentDepartment.ExpandAll();

            lupDepartmentManager.SetDefault(true, "LoginId", "UserName", DepartmentService.GetChildList<User>(p => p.Active == "Y" || string.IsNullOrEmpty(p.Active)).ToList());
        }

        protected override void DataLoad()
        {
            textDepartmentCode.ReadOnly = true;

            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                tNSTD1200BindingSource.Add(new TN_STD1200
                {
                    DepartmentCode = DbRequestHandler.GetSeqStandard("DEPT"),
                    UseFlag = "Y"
                });
                tNSTD1200BindingSource.MoveLast();
            }
            else
            {  // Update
                tNSTD1200BindingSource.DataSource = (TN_STD1200)PopupParam.GetValue(PopupParameter.KeyValue);
                textDepartmentCode.ReadOnly = true;
            }
        }

        protected override void DataSave()
        {
            tNSTD1200BindingSource.EndEdit(); //저장전 수정사항 Posting

            if (EditMode == PopupEditMode.New)
                tNSTD1200BindingSource.DataSource = DepartmentService.Insert((TN_STD1200)tNSTD1200BindingSource.Current);
            else
                DepartmentService.Update((TN_STD1200)tNSTD1200BindingSource.Current);

            DepartmentService.Save();

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }
    }
}