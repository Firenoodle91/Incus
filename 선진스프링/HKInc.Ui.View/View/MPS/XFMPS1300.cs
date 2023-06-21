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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using DevExpress.Utils;
using HKInc.Utils.Enum;
using DevExpress.XtraEditors.Repository;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 생산실적관리화면
    /// </summary>
    public partial class XFMPS1300 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MEA1004> ModelService = (IService<TN_MEA1004>)ProductionFactory.GetDomainService("TN_MEA1004");

        public XFMPS1300()
        {
            InitializeComponent();

            dateEditEx1.DateTime = DateTime.Today;
            GridExControl = gridEx1;
        }

        protected override void InitCombo()
        {
            lupMachine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
            lupWorkId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
            lupItem.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.TopCategory_WAN && p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"));
            GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            GridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));
            GridExControl.MainGrid.AddColumn("a01", "01", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a02", "02", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a03", "03", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a04", "04", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a05", "05", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a06", "06", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a07", "07", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a08", "08", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a09", "09", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a10", "10", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a11", "11", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a12", "12", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a13", "13", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a14", "14", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a15", "15", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a16", "16", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a17", "17", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a18", "18", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a19", "19", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a20", "20", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a21", "21", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a22", "22", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a23", "23", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a24", "24", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a25", "25", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a26", "26", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a27", "27", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a28", "28", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a29", "29", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a30", "30", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
            GridExControl.MainGrid.AddColumn("a31", "31", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "#,###,###,##0.##");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemName", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                string item = lupItem.EditValue.GetNullToEmpty();
                string mc = lupMachine.EditValue.GetNullToEmpty();
                string worker = lupWorkId.EditValue.GetNullToEmpty();
                var date = new SqlParameter("@fdt", dateEditEx1.DateTime);

                var result = context.Database
                      .SqlQuery<TEMP_XFMPS1300>("USP_GET_MPS1300_LIST @fdt", date).ToList();
            
                GridBindingSource.DataSource = result.Where(p=> (string.IsNullOrEmpty(item)?true:p.ItemCode==item)
                                                            &&  (string.IsNullOrEmpty(mc)?true:p.MachineCode==mc)
                                                            &&  (string.IsNullOrEmpty(worker)?true:p.WorkId==worker)).ToList();
            }
            GridExControl.DataSource = GridBindingSource;            
            GridExControl.MainGrid.BestFitColumns();
            SetRefreshMessage(GridExControl);
        }
    }
}
