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

namespace HKInc.Ui.View.MPS
{
    public partial class XFMPS1800 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {

        IService<VI_RESULT_QTY> ModelService = (IService<VI_RESULT_QTY>)ProductionFactory.GetDomainService("VI_RESULT_QTY");
        public XFMPS1800()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;

            dpdt.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            dpdt.DateToEdit.DateTime = DateTime.Today;

        }

     
        protected override void InitCombo()
        {
            lupmachine.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList());
            //lupitemcode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y" && (p.TopCategory == "P01" || p.TopCategory == "P05")).ToList());
            lupitemcode.SetDefault(true, "ItemCode", "ItemCode", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y" && (p.TopCategory == "P01" || p.TopCategory == "P05")).ToList());
            lupProc.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.Process, "", "", ""));
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.ShowFooter=true;
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");                      
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            MasterGridExControl.MainGrid.AddColumn("ResultDate", "작업일");
            MasterGridExControl.MainGrid.AddColumn("WorkId", "작업자");
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비");
            MasterGridExControl.MainGrid.AddColumn("OrderQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("Resultqty", "생산량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("Okqty", "양품수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("Failqty", "불량수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.SummaryItemAddNew(7);
            MasterGridExControl.MainGrid.SummaryItemAddNew(8);
            MasterGridExControl.MainGrid.SummaryItemAddNew(9);
            MasterGridExControl.MainGrid.SummaryItemAddNew(10);




        }
        protected override void InitRepository()
        {

            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcNm", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y"&&(p.TopCategory=="P03"||p.TopCategory == "P02")).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process, "", "", ""), "Mcode", "Codename");
            // GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
        }
        protected override void DataLoad()
        {
            ModelService.ReLoad();
            //  DetailGridExControl.DataSource = null;
            MasterGridExControl.MainGrid.Clear();
       
            string itemcode = lupitemcode.EditValue.GetNullToEmpty();
            string machine = lupmachine.EditValue.GetNullToEmpty();
            string proc = lupProc.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p =>p.ResultDate>=dpdt.DateFrEdit.DateTime&&p.ResultDate<=dpdt.DateToEdit.DateTime&&(string.IsNullOrEmpty(itemcode)?true:p.ItemCode==itemcode )&&(string.IsNullOrEmpty(proc)?true:p.ProcessCode==proc)&&(string.IsNullOrEmpty(machine)?true:p.MachineCode==machine)).OrderBy(o=>o.WorkNo).OrderBy(s=>s.Pseq).ToList();                                                  
            //MasterGridBindingSource.DataSource = ModelService.GetList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
        }


      
    }
}
