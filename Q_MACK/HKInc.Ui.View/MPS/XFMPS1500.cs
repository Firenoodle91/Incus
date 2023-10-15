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
using DevExpress.XtraGrid;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 생산실적관리
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
            lupMachine.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList());
            lupWorkId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
            // 20220219 오세완 차장 품목 / 품명이 동일하게 나오는 오류가 있어서 생략                     2022-02-23 김진우 ItemCodeColumnSetting 수정하여 원복            
            lupItem.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == MasterCodeSTR.Topcategory_Final_Product || p.TopCategory == MasterCodeSTR.Topcategory_Half_Product)).OrderBy(o => o.ItemNm1).ToList());
            lupProc.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process, "", "", ""));
            //  luptem.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.tem));
        }
        //protected override void GridRowDoubleClicked()
        //{
        //    base.GridRowDoubleClicked();
        //}
        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.ShowFooter = true;
            GridExControl.MainGrid.AddColumn("ItemCode", "품목", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("ItemNm", "품명", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("ItemNm1", "품번", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("BOTTOM_CATEGORY", "차종", HorzAlignment.Center, true);
        //    GridExControl.MainGrid.AddColumn("LCTYPE", "기종", HorzAlignment.Center, true);
         //   GridExControl.MainGrid.AddColumn("TEMP5", "팀", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("MachineCode", "설비", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("ProcessCode", "공정", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("WorkId", "작업자", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("asum", "계", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a01", "01", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a02", "02", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a03", "03", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a04", "04", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a05", "05", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a06", "06", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a07", "07", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a08", "08", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a09", "09", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a10", "10", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a11", "11", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a12", "12", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a13", "13", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a14", "14", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a15", "15", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a16", "16", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a17", "17", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a18", "18", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a19", "19", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a20", "20", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a21", "21", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a22", "22", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a23", "23", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a24", "24", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a25", "25", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a26", "26", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a27", "27", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a28", "28", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a29", "29", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a30", "30", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("a31", "31", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");

            //GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "a01", "{0}");
            //GridExControl.MainGrid.Columns["a01"].Summary.Add(item1);
            //GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "a02", "{0}");
            //GridExControl.MainGrid.Columns["a02"].Summary.Add(item2);

            //GridExControl.MainGrid.Columns["a01"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "a01", "Sum={0}");
            GridExControl.MainGrid.SummaryItemAddNew(7);
            GridExControl.MainGrid.SummaryItemAddNew(8);
            GridExControl.MainGrid.SummaryItemAddNew(9);
            GridExControl.MainGrid.SummaryItemAddNew(10);
            GridExControl.MainGrid.SummaryItemAddNew(11);
            GridExControl.MainGrid.SummaryItemAddNew(12);
            GridExControl.MainGrid.SummaryItemAddNew(13);
            GridExControl.MainGrid.SummaryItemAddNew(14);
            GridExControl.MainGrid.SummaryItemAddNew(15);
            GridExControl.MainGrid.SummaryItemAddNew(16);
            GridExControl.MainGrid.SummaryItemAddNew(17);
            GridExControl.MainGrid.SummaryItemAddNew(18);
            GridExControl.MainGrid.SummaryItemAddNew(19);
            GridExControl.MainGrid.SummaryItemAddNew(20);
            GridExControl.MainGrid.SummaryItemAddNew(21);
            GridExControl.MainGrid.SummaryItemAddNew(22);
            GridExControl.MainGrid.SummaryItemAddNew(23);
            GridExControl.MainGrid.SummaryItemAddNew(24);
            GridExControl.MainGrid.SummaryItemAddNew(25);
            GridExControl.MainGrid.SummaryItemAddNew(26);
            GridExControl.MainGrid.SummaryItemAddNew(27);
            GridExControl.MainGrid.SummaryItemAddNew(28);
            GridExControl.MainGrid.SummaryItemAddNew(29);
            GridExControl.MainGrid.SummaryItemAddNew(30);
            GridExControl.MainGrid.SummaryItemAddNew(31);
            GridExControl.MainGrid.SummaryItemAddNew(32);
            GridExControl.MainGrid.SummaryItemAddNew(33);
            GridExControl.MainGrid.SummaryItemAddNew(34);
            GridExControl.MainGrid.SummaryItemAddNew(35);
            GridExControl.MainGrid.SummaryItemAddNew(36);
            GridExControl.MainGrid.SummaryItemAddNew(37);
            GridExControl.MainGrid.SummaryItemAddNew(38);
      
           
          //  GridExControl.MainGrid.SummaryItemAddNew(41);


        }
        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BOTTOM_CATEGORY", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
          //  GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TEMP5", DbRequesHandler.GetCommCode(MasterCodeSTR.tem), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process, "", "", ""), "Mcode", "Codename");
           // GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
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

                string item = lupItem.EditValue.GetNullToEmpty();
                string mc = lupMachine.EditValue.GetNullToEmpty();
                string worker = lupWorkId.EditValue.GetNullToEmpty();
                string proc = lupProc.EditValue.GetNullToEmpty();
              //  string tem = luptem.EditValue.GetNullToEmpty();
                var date = new SqlParameter("@fdt", dateEditEx1.DateTime);

                var result = context.Database
                      .SqlQuery<TP_MPS1500>("SP_WORK_RESULT @fdt", date).ToList();
            
                GridBindingSource.DataSource = result.Where(p=>(string.IsNullOrEmpty(item)?true:p.ItemCode==item)
                &&(string.IsNullOrEmpty(mc)?true:p.MachineCode==mc)&&(string.IsNullOrEmpty(proc)?true:p.ProcessCode==proc )&&(string.IsNullOrEmpty(worker)?true:p.WorkId==worker)      );

            }
            GridExControl.DataSource = GridBindingSource;
            GridExControl.MainGrid.BestFitColumns();
            //GridExControl.MainGrid.SummaryItemAdd(10);
           // GridExControl.MainGrid.SummaryItemAdd(12);
            
          
        }
    }
}
