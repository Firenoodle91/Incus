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
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;

using HKInc.Service.Factory;
using HKInc.Service.Helper;
using DevExpress.Utils;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using HKInc.Service.Handler;
using HKInc.Service.Service;
using System.Data.SqlClient;

namespace HKInc.Ui.View.View.MEA
{
    public partial class XFUPH1100 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_DEV1000> ModelService = (IService<TN_DEV1000>)ProductionFactory.GetDomainService("TN_DEV1000");

        public XFUPH1100()
        {
            InitializeComponent();

            GridExControl = gridEx1;
        }

        protected override void InitCombo()
        {
            dt_ReqDate.SetTodayIsMonth();
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            lup_Machine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.Init();
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("ResultDate", LabelConvert.GetLabelText("ResultDate"));
            GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
            GridExControl.MainGrid.AddColumn("MachineName", LabelConvert.GetLabelText("MachineName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemNm", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessCode"));
            GridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));
            GridExControl.MainGrid.AddColumn("QTY", LabelConvert.GetLabelText("ResultQty"), HorzAlignment.Far, FormatType.Numeric, "{ 0:#,###,###,##0.##}");
            GridExControl.MainGrid.AddColumn("WorkTime", LabelConvert.GetLabelText("WorkTime"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("UPH", LabelConvert.GetLabelText("UPH"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("SpendUPH", LabelConvert.GetLabelText("SpendUPH"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("SpendUPH_Rate", LabelConvert.GetLabelText("SpendUPH_Rate"), HorzAlignment.Far, FormatType.Numeric, "{0:#,###,###,##0}%");
            GridExControl.BestFitColumns();





        }

        protected override void InitRepository()
        {
            var ItemList = ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList();
            var MachineList = ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList();
         
           GridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("WorkId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
           // GridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ItemNm", ItemList, "ItemCode", "ItemNm");
           //GridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("MachineName", MachineList, "MachineCode", "MachineName");


            // lup_Item.SetDefault(true, "ItemCode", "ItemNm", ItemList);
            // lup_Machine.SetDefault(true, "MachineCode", "MachineName", MachineList);
            //    lupWorkId.SetDefault(true, "LoginId", "UserName", UserList);
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {

            GridRowLocator.GetCurrentRow();
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();
            var FromDate = dt_ReqDate.DateFrEdit.DateTime.Date;
            var ToDate = dt_ReqDate.DateToEdit.DateTime.Date;
            var MachineCode = lup_Machine.EditValue.GetNullToEmpty();
            var ItemCode = lup_Item.EditValue.GetNullToEmpty();
            var result =DbRequestHandler.GetDataQury("exec SP_GET_UPH1100 '"+ FromDate+"','"+ ToDate+"','"+ MachineCode + "','"+ ItemCode + "'");
            GridBindingSource.DataSource = result.Tables[0];
         

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(GridExControl.MainGrid.RecordCount);

          
        }

      

     
    }
}