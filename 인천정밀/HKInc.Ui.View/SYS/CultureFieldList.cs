using System.Linq;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;

namespace HKInc.Ui.View.SYS
{
    /// <summary>
    /// 메뉴미사용
    /// </summary>
    public partial class CultureFieldList : HKInc.Service.Base.ListFormTemplate
    {
        IService<CultureField> CultureFieldService = (IService<CultureField>)ServiceFactory.GetDomainService("CultureField");

        public CultureFieldList()
        {
            InitializeComponent();

            GridExControl = gridEx1;
        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();
            
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("CultureFieldId");
            GridExControl.MainGrid.AddColumn("EntityName");
            GridExControl.MainGrid.AddColumn("DefaultField");
            GridExControl.MainGrid.AddColumn("SecondField");
            GridExControl.MainGrid.AddColumn("ThirdField");
            GridExControl.MainGrid.AddColumn("UpdateId");
            GridExControl.MainGrid.AddColumn("UpdateTime");
            GridExControl.MainGrid.AddColumn("UpdateClass");
            GridExControl.MainGrid.AddColumn("CreateId");
            GridExControl.MainGrid.AddColumn("CreateTime");
            GridExControl.MainGrid.AddColumn("CreateClass");

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit);
            GridExControl.MainGrid.SetEditable("EntityName", "DefaultField", "SecondField", "ThirdField");            

            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {            
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateTime");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateTime");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("CultureFieldId");
            CultureFieldService.ReLoad();

            GridBindingSource.DataSource = CultureFieldService.GetList(p => p.EntityName.Contains(textEntityName.Text)).OrderBy(p=>p.EntityName).ToList();
            GridBindingSource.Sort = "EntityName";

            GridExControl.DataSource = GridBindingSource;

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
            GridRowLocator.SetCurrentRow();

            GridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            gridEx1.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            CultureFieldService.Save();            
            DataLoad();
        }        

        #region 추상함수 구현
        protected override void DeleteRow()
        {
            CultureField obj = GridBindingSource.Current as CultureField;

            if (obj != null)
            {
                CultureFieldService.Delete(obj);
                GridBindingSource.RemoveCurrent();
            }
        }

        protected override void AddRowClicked()
        {
            CultureField obj = (CultureField)GridBindingSource.AddNew();
            CultureFieldService.Insert(obj);
        }
        #endregion

        private void GridBindingSource_CurrentItemChanged(object sender, System.EventArgs e)
        {
            IsFormControlChanged = true;
        }

        protected override void GridRowDoubleClicked() { }
    }
}