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
namespace HKInc.Ui.View.MPS
{
    public partial class XFMPS1600 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1600> ModelService = (IService<TN_MPS1600>)ProductionFactory.GetDomainService("TN_MPS1600");
        public XFMPS1600()
        {
            InitializeComponent();
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-5);
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today;
            GridExControl = gridEx1;
        }
        protected override void InitCombo()
        {
            lupMachine.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList());
            lupstoptype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.STOPTYPE, "", "", ""));

        }
        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("StopDate", "일자", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("MachineCode", "설비", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("StopCode", "비가동사유", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("StopStarttime", "비가동시작시간", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("StopEnddate", "비가동종료시간", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("DdifM", "비가동분", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");

        }
        protected override void GridRowDoubleClicked()
        {
            
        }
        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemDateEdit("StopDate");
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("StopStarttime");
            GridExControl.MainGrid.SetRepositoryItemDateTimeEdit("StopEnddate");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StopCode", DbRequesHandler.GetCommCode(MasterCodeSTR.STOPTYPE, "", "", ""), "Mcode", "Codename");
        }
        protected override void DataLoad()
        {
            ModelService.ReLoad();
            string mc = lupMachine.EditValue.GetNullToEmpty();
            string stoptype = lupstoptype.EditValue.GetNullToEmpty();
            GridBindingSource.DataSource = ModelService.GetList(p => (p.StopDate >= datePeriodEditEx1.DateFrEdit.DateTime && p.StopDate <= datePeriodEditEx1.DateToEdit.DateTime) &&
             (string.IsNullOrEmpty(mc) ? true : p.MachineCode == mc) && (string.IsNullOrEmpty(stoptype) ? true : p.StopCode == stoptype)).OrderBy(o => o.StopDate).ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.MainGrid.BestFitColumns();
        }
    }
}
