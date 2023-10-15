using System.Linq;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;

namespace HKInc.Ui.View.SYS
{
    public partial class MessageList : HKInc.Service.Base.ListFormTemplate
    {
        IService<StandardMessage> StandardMessageService = (IService<StandardMessage>)ServiceFactory.GetDomainService("StandardMessage");

        public MessageList()
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
            GridExControl.MainGrid.AddColumn("MessageId");
            GridExControl.MainGrid.AddColumn("Message");
            GridExControl.MainGrid.AddColumn("Message2");
            GridExControl.MainGrid.AddColumn("Message3");
            GridExControl.MainGrid.AddColumn("Description");
            GridExControl.MainGrid.AddColumn("Active");
            GridExControl.MainGrid.AddColumn("UpdateId");
            GridExControl.MainGrid.AddColumn("UpdateTime");
            GridExControl.MainGrid.AddColumn("UpdateClass");
            GridExControl.MainGrid.AddColumn("CreateId");
            GridExControl.MainGrid.AddColumn("CreateTime");
            GridExControl.MainGrid.AddColumn("CreateClass");

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit);
            GridExControl.MainGrid.SetEditable("Message", "Message2", "Message3", "Active");
            

            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateTime");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateTime");
            GridExControl.MainGrid.MainView.Columns["Message"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.MainView.Columns["Message2"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.MainView.Columns["Message3"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MessageId");
            StandardMessageService.ReLoad();

            string radioValue = radioGroup.EditValue.GetNullToEmpty();
            GridBindingSource.DataSource = StandardMessageService.GetList(p => (p.Message.Contains(textMessage.Text) || p.Message2.Contains(textMessage.Text) || p.Message3.Contains(textMessage.Text)) &&
                                                                               (string.IsNullOrEmpty(radioValue) ? true : p.Active == radioValue))
                                                                 .OrderBy(p=>p.MessageId)
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

            StandardMessageService.Save(true);            
            DataLoad();
        }        

        #region 추상함수 구현
        protected override void DeleteRow()
        {
            StandardMessage obj = GridBindingSource.Current as StandardMessage;

            if (obj != null)
            {
                StandardMessageService.Delete(obj);
                GridBindingSource.RemoveCurrent();
            }
        }

        protected override void AddRowClicked()
        {
            StandardMessage obj = (StandardMessage)GridBindingSource.AddNew();
            obj.Active = "Y";
            StandardMessageService.Insert(obj);
        }
        #endregion

        private void GridBindingSource_CurrentItemChanged(object sender, System.EventArgs e)
        {
            IsFormControlChanged = true;
        }
    }
}