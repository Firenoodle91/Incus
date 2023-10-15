using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.PUR
{
    public partial class XFPUR2000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {

        IService<VI_PUR_LIST> ModelService = (IService<VI_PUR_LIST>)ProductionFactory.GetDomainService("VI_PUR_LIST");
        public XFPUR2000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
          
         
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(+30);
            User ss = ModelService.GetChildList<User>(p => p.UserId == GlobalVariable.UserId).OrderBy(o => o.UserId).FirstOrDefault();
           
        }

    

     
        protected override void InitCombo()
        {
            lupCustCode.SetDefault(true, "CustomerCode", "CustomerName",ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lupUser.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
            lupItemCode.SetDefault(true, "ItemCode", "ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").ToList());
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("ReqNo", "발주번호");
            MasterGridExControl.MainGrid.AddColumn("ReqDate", "발주일");            
            MasterGridExControl.MainGrid.AddColumn("ReqCustcode", "발주거래처");
            MasterGridExControl.MainGrid.AddColumn("ReqitemCode", "발주품목");
            MasterGridExControl.MainGrid.AddColumn("ReqQty", "발주수량");
            MasterGridExControl.MainGrid.AddColumn("ReqId", "발주자");
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "입고거래처");
            MasterGridExControl.MainGrid.AddColumn("InputDate", "발주일");
            MasterGridExControl.MainGrid.AddColumn("InputItemcode", "입고품목");
            MasterGridExControl.MainGrid.AddColumn("Unit", "단위");
            MasterGridExControl.MainGrid.AddColumn("InputQty", "입고수량");
            MasterGridExControl.MainGrid.AddColumn("InputId", "입고자");



        }
        protected override void InitRepository()
        {
          
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("ReqDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InputDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReqId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReqCustcode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReqitemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InputItemcode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InputId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
        }
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion
            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string cust = lupCustCode.EditValue.GetNullToEmpty();
            string lsitem = lupItemCode.EditValue.GetNullToEmpty();
            string luser = lupUser.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => ((p.ReqDate >= dp_date.DateFrEdit.DateTime.Date && p.ReqDate <= dp_date.DateToEdit.DateTime.Date)||(p.InputDate >= dp_date.DateFrEdit.DateTime.Date && p.InputDate <= dp_date.DateToEdit.DateTime.Date))
                                                                       && (string.IsNullOrEmpty(luser) ? true : p.ReqId == luser)
                                                                       && (string.IsNullOrEmpty(cust) ? true : p.ReqCustcode == cust)
                                                                       && (string.IsNullOrEmpty(lsitem) ? true : (p.ReqitemCode == lsitem||p.InputItemcode==lsitem))
                                                                       ).OrderBy(o => o.ReqDate).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }
      
    }
}
