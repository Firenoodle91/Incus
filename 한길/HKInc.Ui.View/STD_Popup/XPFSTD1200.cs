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
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;

namespace HKInc.Ui.View.STD_Popup
{
    public partial class XPFSTD1200 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_STD1200> DepartmentService;

        public XPFSTD1200(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();

            PopupParam = param;
            Callback = callback;

            ModelBindingSource = tNSTD1200BindingSource; // BindingSource설정            
        }
        protected override void AddControlList() // abstract함수 구현
        {
            ControlEnableList.Add("DepartmentCode", textDepartmentCode);
            ControlEnableList.Add("DepartmentName", textDepartmentName);
            ControlEnableList.Add("DepartmentManager", lupDepartmentManager);
            ControlEnableList.Add("ParentDepartmentCode", treeListParentDepartment);
            ControlEnableList.Add("Level", textLevel);
            ControlEnableList.Add("SeqNumber", textSeqNo);
            ControlEnableList.Add("UseFlag", chkUseFlag);

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

            IService<HKInc.Ui.Model.Domain.User> UserService = (IService<HKInc.Ui.Model.Domain.User>)HKInc.Service.Factory.ServiceFactory.GetDomainService("User");
            lupDepartmentManager.SetDefault(true, "UserId", "UserName", UserService.GetList(p => p.Active == "Y" || string.IsNullOrEmpty(p.Active)));

            dateCreteTime.SetFormat(DateFormat.DateAndTimeSecond);
            dateUpdateTime.SetFormat(DateFormat.DateAndTimeSecond);
            dateCreteTime.Enabled = false;
            dateUpdateTime.Enabled = false;
        }

        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                tNSTD1200BindingSource.Add(new TN_STD1200
                {
                    //DepartmentCode = Service.Service.DbRequesHandler.GetCellValue("SELECT 'DEPT'+  REPLICATE('0', 2 - DATALENGTH(CAST( right(max([DEPARTMENT_CODE]),2)+1 AS VARCHAR(2)))) + CAST( right(max([DEPARTMENT_CODE]),2)+1 AS VARCHAR(2))  FROM TN_STD1200T", 0).ToString(), UseFlag = "Y"
                    DepartmentCode = Service.Service.DbRequesHandler.GetDepartmentCodeSeq(),
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