using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using DevExpress.Utils;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Service;

namespace HKInc.Ui.View.SYS
{
    /// <summary>
    /// 사용자접속기록
    /// </summary>
    public partial class LoginLogList : HKInc.Service.Base.ListFormTemplate
    {
        IService<LoginLog> LoginLogService = (IService<LoginLog>)ServiceFactory.GetDomainService("LoginLog");
        
        public LoginLogList()
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
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("LoginLogId",false);
            GridExControl.MainGrid.AddColumn("UserId");
            GridExControl.MainGrid.AddColumn("User.LoginId", LabelConvert.GetLabelText("LoginId"));
            GridExControl.MainGrid.AddColumn("User.RankCode", LabelConvert.GetLabelText("Rank"));
            GridExControl.MainGrid.AddColumn("User.UserName", LabelConvert.GetLabelText("UserName"));
            GridExControl.MainGrid.AddColumn("User.EmployeeNo", LabelConvert.GetLabelText("EmployeeNo"));
            GridExControl.MainGrid.AddColumn("User.TN_STD1200.DepartmentName", LabelConvert.GetLabelText("Department"));
            GridExControl.MainGrid.AddColumn("User.HireDate", LabelConvert.GetLabelText("HireDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("User.DischargeDate", LabelConvert.GetLabelText("DischargeDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("User.Description", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.AddColumn("LoginTime", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm:ss");
            GridExControl.MainGrid.AddColumn("LogoutTime", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm:ss");
            GridExControl.MainGrid.AddColumn("IpAddress");
            GridExControl.MainGrid.AddColumn("PcName");
            
            GridExControl.BestFitColumns();            
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("User.RankCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.RankCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            LoginLogService.ReLoad();

            DateTime toDate = ((DateTime)dateLogdate.EditToValue).AddDays(1);            
            
            GridBindingSource.DataSource = LoginLogService.GetList(p => p.LoginTime >= (DateTime)dateLogdate.EditFrValue && p.LoginTime < toDate)
                                                          .OrderByDescending(p=>p.LoginTime).ToList();
            GridExControl.DataSource = GridBindingSource;            
            SetRefreshMessage(GridExControl.MainGrid.RecordCount);

            GridExControl.BestFitColumns();
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            
            return HKInc.Ui.View.PopupFactory.ProductionPopupFactory.GetPopupForm(HKInc.Ui.View.PopupFactory.ProductionPopupView.MenuOpenLog, param, DummyCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            LoginLog LoginLog = GridBindingSource.Current as LoginLog;
            
            param.SetValue(PopupParameter.Service, LoginLogService);
            param.SetValue(PopupParameter.DataParam, LoginLog.MenuLogList.ToList());
            return param;
        }

        protected virtual void DummyCallback(object sender, HKInc.Utils.Common.PopupArgument e) { }
    }
}
