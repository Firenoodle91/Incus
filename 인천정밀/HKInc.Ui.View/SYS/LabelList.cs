using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using System.Linq;

namespace HKInc.Ui.View.SYS
{
    /// <summary>
    /// 라벨관리
    /// </summary>
    public partial class LabelList : HKInc.Service.Base.ListFormTemplate
    {
        IService<FieldLabel> LabelTextService = (IService<FieldLabel>)ServiceFactory.GetDomainService("FieldLabel");

        public LabelList()
        {
            InitializeComponent();

            GridExControl = gridEx1;
            //OutPutRadioGroup = radioGroup;
            //RadioGroupType = RadioGroupType.ActiveAll;
            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();          
        }
       
        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("FieldLabelId", LabelConvert.GetLabelText("FieldLabelId"));
            GridExControl.MainGrid.AddColumn("FieldName", LabelConvert.GetLabelText("FieldName"));
            GridExControl.MainGrid.AddColumn("LabelText", LabelConvert.GetLabelText("LabelText"));
            GridExControl.MainGrid.AddColumn("LabelText2", LabelConvert.GetLabelText("LabelTextENG"));
            GridExControl.MainGrid.AddColumn("LabelText3", LabelConvert.GetLabelText("LabelTextCHN"));
            GridExControl.MainGrid.AddColumn("Active", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.AddColumn("UpdateId");
            GridExControl.MainGrid.AddColumn("UpdateTime");
            GridExControl.MainGrid.AddColumn("UpdateClass", false);
            GridExControl.MainGrid.AddColumn("CreateId");
            GridExControl.MainGrid.AddColumn("CreateTime");
            GridExControl.MainGrid.AddColumn("CreateClass", false);

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit);
            GridExControl.MainGrid.SetEditable("FieldName", "LabelText", "LabelText2", "LabelText3", "Active");

            LayoutControlHandler.SetRequiredGridHeaderColor<FieldLabel>(GridExControl);
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            var userList = LabelTextService.GetChildList<User>(p => true).ToList();
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", userList, "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", userList, "LoginId", "UserName");

            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("FieldLabelId");
            LabelTextService.ReLoad();

            //string radioValue = radioGroup1.EditValue.GetNullToEmpty();
            string radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();
            string fieldName = textFieldName.EditValue.GetNullToEmpty();
            string labelText = textLabelText.EditValue.GetNullToEmpty();
            GridBindingSource.DataSource = LabelTextService.GetList(p =>  (p.FieldName.Contains(fieldName))
                                                                        && (p.LabelText.Contains(labelText) || p.LabelText2.Contains(labelText) || p.LabelText3.Contains(labelText))
                                                                        && (radioValue == "A" ? true : p.Active == radioValue)
                                                                    )
                                                                    .OrderBy(p=>p.FieldLabelId)
                                                                    .ToList();           
            GridExControl.DataSource = GridBindingSource;
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);

            GridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            gridEx1.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            LabelTextService.Save(true);            
            DataLoad();
        }        

        #region 추상함수 구현
        protected override void DeleteRow()
        {
            FieldLabel obj = GridBindingSource.Current as FieldLabel;

            if (obj != null)
            {
                LabelTextService.Delete(obj);
                GridBindingSource.RemoveCurrent();
            }
        }

        protected override void AddRowClicked()
        {
            FieldLabel obj = (FieldLabel)GridBindingSource.AddNew();
            obj.Active = "Y";
            LabelTextService.Insert(obj);
        }
        #endregion

        protected override void GridRowDoubleClicked(){}

    }
}