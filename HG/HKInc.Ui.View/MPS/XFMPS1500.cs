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
using System.Data.SqlClient;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 생산실적관리화면
    /// </summary>
    public partial class XFMPS1500 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1600> ModelService = (IService<TN_MPS1600>)ProductionFactory.GetDomainService("TN_MPS1600");

        public XFMPS1500()
        {
            InitializeComponent();

            dateEditEx1.DateTime = DateTime.Today;
            GridExControl = gridEx1;
        }

        protected override void InitCombo()
        {
            lupMachine.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList());
            lupWorkId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
            lupItem.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            GridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            GridExControl.MainGrid.AddColumn("ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("MachineCode", "설비", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("ProcessCode", "공정", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("WorkId", "작업자", HorzAlignment.Center, true);

            var TodayDays = DateTime.Today.Day;
            GridExControl.MainGrid.AddColumn(string.Format("{0}{1:D2}", "a", TodayDays), string.Format("{0:D2}", TodayDays), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            for (int i = 1; i <= 31; i++)
            {
                if(i != TodayDays)
                {
                    var FieldName = string.Format("{0}{1:D2}", "a", i);
                    var Caption = string.Format("{0:D2}", i);
                    GridExControl.MainGrid.AddColumn(FieldName, Caption, HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
                }
            }

            //GridExControl.MainGrid.AddColumn("a01", "01", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a02", "02", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a03", "03", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a04", "04", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a05", "05", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a06", "06", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a07", "07", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a08", "08", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a09", "09", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a10", "10", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a11", "11", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a12", "12", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a13", "13", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a14", "14", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a15", "15", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a16", "16", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a17", "17", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a18", "18", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a19", "19", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a20", "20", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a21", "21", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a22", "22", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a23", "23", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a24", "24", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a25", "25", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a26", "26", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a27", "27", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a28", "28", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a29", "29", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a30", "30", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("a31", "31", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            

        }

        protected override void InitRepository()
        {

            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process, "", "", ""), "Mcode", "Codename");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
        }

        protected override void DataLoad()
        {
            //ModelService.ReLoad();
            //string mc = lupMachine.EditValue.GetNullToEmpty();
            //string stoptype = lupstoptype.EditValue.GetNullToEmpty();
            //GridBindingSource.DataSource = ModelService.GetList(p => (p.StopDate >= datePeriodEditEx1.DateFrEdit.DateTime && p.StopDate <= datePeriodEditEx1.DateToEdit.DateTime) &&
            // (string.IsNullOrEmpty(mc) ? true : p.MachineCode == mc) && (string.IsNullOrEmpty(stoptype) ? true : p.StopCode == stoptype)).OrderBy(o => o.StopDate).ToList();
            //GridExControl.DataSource = GridBindingSource;
            //GridExControl.MainGrid.BestFitColumns();
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {

                //string item = lupItem.EditValue.GetNullToEmpty();
                string item = textItemCodeName.EditValue.GetNullToEmpty();
                string mc = lupMachine.EditValue.GetNullToEmpty();
                string worker = lupWorkId.EditValue.GetNullToEmpty();
                var date = new SqlParameter("@fdt", dateEditEx1.DateTime);

                var result = context.Database
                      .SqlQuery<TP_MPS1500>("SP_WORK_RESULT @fdt", date).ToList();

                GridBindingSource.DataSource = result.Where(p => (p.ItemNm1.Contains(item) || p.ItemNm.Contains(item))
                                                              && (string.IsNullOrEmpty(mc) ? true : p.MachineCode == mc) 
                                                              && (string.IsNullOrEmpty(worker) ? true : p.WorkId == worker)
                                                           ).ToList();
            }
            GridExControl.DataSource = GridBindingSource;
            GridExControl.MainGrid.BestFitColumns();
        }
    }
}
