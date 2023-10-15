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
    /// <summary>
    /// 설비별 생산 실적
    /// </summary>
    public partial class XFMPS2200 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_MACHINE_PROCESS_QTY> ModelService = (IService<VI_MACHINE_PROCESS_QTY>)ProductionFactory.GetDomainService("VI_MACHINE_PROCESS_QTY");

        public XFMPS2200()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;

            dpdt.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            dpdt.DateToEdit.DateTime = DateTime.Today;
        }
     
        protected override void InitCombo()
        {
            lupmachine.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList());            
            lupitemcode.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y" && (p.TopCategory == "P01" || p.TopCategory == "P05")).ToList());
            lupProc.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process, "", "", ""));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.ShowFooter=true;
            MasterGridExControl.MainGrid.AddColumn("RowIndex", false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품목");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품명");
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            MasterGridExControl.MainGrid.AddColumn("ResultDate", "작업일");        
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비");
            MasterGridExControl.MainGrid.AddColumn("ResultQty", "생산량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("OkQty", "양품수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("FailQty", "불량수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.SummaryItemAddNew(7);
            MasterGridExControl.MainGrid.SummaryItemAddNew(8);
            MasterGridExControl.MainGrid.SummaryItemAddNew(9);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process, "", "", ""), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            MasterGridExControl.MainGrid.Clear();
       
            string itemcode = lupitemcode.EditValue.GetNullToEmpty();
            string machine = lupmachine.EditValue.GetNullToEmpty();
            string proc = lupProc.EditValue.GetNullToEmpty();

            // 2022-06-22 김진우 추가     시간이 12시가 넘으면 조회가 되지 않아서 추가           DateTime은 12시 기준
            DateTime FrDateTime = dpdt.DateFrEdit.DateTime;
            DateTime ToDateTime = dpdt.DateToEdit.DateTime;
            DateTime FrTime = new DateTime(FrDateTime.Year, FrDateTime.Month, FrDateTime.Day, 00, 00, 00);
            DateTime ToTime = new DateTime(ToDateTime.Year, ToDateTime.Month, ToDateTime.Day, 23, 59, 59);

            MasterGridBindingSource.DataSource = ModelService.GetList(p => p.ResultDate >= FrTime && p.ResultDate <= ToTime 
                                                                        && (string.IsNullOrEmpty(itemcode) ? true : p.ItemCode == itemcode) 
                                                                        && (string.IsNullOrEmpty(proc) ? true : p.ProcessCode == proc) 
                                                                        && (string.IsNullOrEmpty(machine) ? true : p.MachineCode == machine)
                                                                        ).OrderBy(o => o.MachineCode).OrderBy(s => s.ProcessCode).ToList();

            //MasterGridBindingSource.DataSource = ModelService.GetList(p => p.ResultDate >= dpdt.DateFrEdit.DateTime && p.ResultDate <= dpdt.DateToEdit.DateTime && (string.IsNullOrEmpty(itemcode) ? true : p.ItemCode == itemcode) && (string.IsNullOrEmpty(proc) ? true : p.ProcessCode == proc) && (string.IsNullOrEmpty(machine) ? true : p.MachineCode == machine)).OrderBy(o => o.MachineCode).OrderBy(s => s.ProcessCode).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
        }
      
    }
}
