using System;
using System.Linq;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;

namespace HKInc.Ui.View.SYS
{
    public partial class SysLogList : HKInc.Service.Base.ListFormTemplate
    {
        IService<SystemLog> SystemLogService = (IService<SystemLog>)ServiceFactory.GetDomainService("SystemLog");

        public SysLogList()
        {
            InitializeComponent();

            GridExControl = gridEx1;            
        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();            
        }

        protected override void InitControls()
        {
            base.InitControls();
            dateLogdate.DateToEdit.EditValue = DateTime.Today;
            dateLogdate.DateFrEdit.EditValue = DateTime.Today.AddMonths(-1);
        }
        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("SystemLogId", false);
            GridExControl.MainGrid.AddColumn("LogType");
            GridExControl.MainGrid.AddColumn("LogTime");
            GridExControl.MainGrid.AddColumn("ClassName");
            GridExControl.MainGrid.AddColumn("ErrorCode");
            GridExControl.MainGrid.AddColumn("Message");
            GridExControl.MainGrid.AddColumn("Message2");
            GridExControl.MainGrid.AddColumn("Message3");
            GridExControl.MainGrid.AddColumn("Message4");
            GridExControl.MainGrid.AddColumn("Message5");
            GridExControl.MainGrid.AddColumn("UpdateId");
            GridExControl.MainGrid.AddColumn("UpdateTime");
            GridExControl.MainGrid.AddColumn("UpdateClass", false);
            GridExControl.MainGrid.AddColumn("CreateId");
            GridExControl.MainGrid.AddColumn("CreateTime");
            GridExControl.MainGrid.AddColumn("CreateClass", false);

            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("LogTime");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateTime");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateTime");
        }

        protected override void DataLoad()
        {
            SystemLogService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            DateTime toDate = ((DateTime)dateLogdate.EditToValue).AddDays(1);
            GridBindingSource.DataSource = SystemLogService.GetList(p => p.LogTime >= (DateTime)dateLogdate.EditFrValue && p.LogTime < toDate)
                                                           .OrderByDescending(p => p.LogTime).ToList();
            GridExControl.DataSource = GridBindingSource;

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);

            GridExControl.BestFitColumns();
        }        
    }
}