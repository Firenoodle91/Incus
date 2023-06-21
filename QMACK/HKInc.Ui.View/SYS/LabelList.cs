using System.Linq;
using System.Windows.Forms;

using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;

namespace HKInc.Ui.View.SYS
{
    public partial class LabelList : HKInc.Service.Base.ListFormTemplate
    {
        IService<FieldLabel> LabelTextService = (IService<FieldLabel>)ServiceFactory.GetDomainService("FieldLabel");

        public LabelList()
        {
            InitializeComponent();

            GridExControl = gridEx1;
            OutPutRadioGroup = radioGroup;
            RadioGroupType = RadioGroupType.ActiveAll;
        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();          
        }
       
        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("FieldLabelId");
            GridExControl.MainGrid.AddColumn("FieldName");
            GridExControl.MainGrid.AddColumn("LabelText");
            GridExControl.MainGrid.AddColumn("LabelText2");
            GridExControl.MainGrid.AddColumn("LabelText3");
            GridExControl.MainGrid.AddColumn("Active");
            GridExControl.MainGrid.AddColumn("UpdateId");
            GridExControl.MainGrid.AddColumn("UpdateTime");
            GridExControl.MainGrid.AddColumn("UpdateClass", false);
            GridExControl.MainGrid.AddColumn("CreateId");
            GridExControl.MainGrid.AddColumn("CreateTime");
            GridExControl.MainGrid.AddColumn("CreateClass", false);

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit);
            GridExControl.MainGrid.SetEditable("FieldName", "LabelText", "LabelText2", "LabelText3", "Active");
            

            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateTime");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateTime");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("FieldLabelId");
            LabelTextService.ReLoad();

            string radioValue = radioGroup.EditValue.GetNullToEmpty();
            string fieldName = textFieldName.EditValue.GetNullToEmpty();
            string labelText = textLabelText.EditValue.GetNullToEmpty();
            GridBindingSource.DataSource = LabelTextService.GetList(p => ((p.FieldName.Contains(fieldName) &&
                                                                          (p.LabelText.Contains(labelText) || p.LabelText2.Contains(labelText) || p.LabelText3.Contains(labelText))) &&
                                                                          (string.IsNullOrEmpty(radioValue) ? true : p.Active == radioValue)))
                                                           .OrderBy(p=>p.FieldName).ToList();           
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