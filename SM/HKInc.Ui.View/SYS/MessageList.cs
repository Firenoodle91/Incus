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
    /// 메시지관리
    /// </summary>
    public partial class MessageList : HKInc.Service.Base.ListFormTemplate
    {
        IService<StandardMessage> StandardMessageService = (IService<StandardMessage>)ServiceFactory.GetDomainService("StandardMessage");

        public MessageList()
        {
            InitializeComponent();

            GridExControl = gridEx1;
            //OutPutRadioGroup = radioGroup;
            //RadioGroupType = RadioGroupType.ActiveAll;
            //rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, false);
            GridExControl.MainGrid.AddColumn("MessageId");
            GridExControl.MainGrid.AddColumn("Message");
            GridExControl.MainGrid.AddColumn("Message2", LabelConvert.GetLabelText("MessageENG"));
            GridExControl.MainGrid.AddColumn("Message3", LabelConvert.GetLabelText("MessageCHN"));
            //GridExControl.MainGrid.AddColumn("Active", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.AddColumn("CreateId");
            GridExControl.MainGrid.AddColumn("CreateTime");
            GridExControl.MainGrid.AddColumn("UpdateId");
            GridExControl.MainGrid.AddColumn("UpdateTime");

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit);
            GridExControl.MainGrid.SetEditable("Message", "Message2", "Message3");

            LayoutControlHandler.SetRequiredGridHeaderColor<StandardMessage>(GridExControl);
        }

        protected override void InitRepository()
        {
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            GridExControl.MainGrid.MainView.Columns["Message"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Message", UserRight.HasEdit);
            GridExControl.MainGrid.MainView.Columns["Message2"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Message2", UserRight.HasEdit);
            GridExControl.MainGrid.MainView.Columns["Message3"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Message3", UserRight.HasEdit);

            var userList = StandardMessageService.GetChildList<User>(p => true).ToList();
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", userList, "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", userList, "LoginId", "UserName");

            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MessageId");
            StandardMessageService.ReLoad();

            //string radioValue = radioGroup1.EditValue.GetNullToEmpty();
            //string radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();
            GridBindingSource.DataSource = StandardMessageService.GetList(p => (p.Message.Contains(textMessage.Text) || p.Message2.Contains(textMessage.Text) || p.Message3.Contains(textMessage.Text))
                                                                               //&& (radioValue == "A" ? true : p.Active == radioValue)
                                                                         )
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