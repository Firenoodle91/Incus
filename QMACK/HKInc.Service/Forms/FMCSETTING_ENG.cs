using HKInc.Service.Factory;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Interface.Helper;
using System.Data.SqlClient;
using HKInc.Service.Forms;

namespace HKInc.Service.Controls
{
    public partial class FMCSETTING_ENG : Form
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
        string aa = "C:\\QMack\\Serial.ini";
        string ab = "C:\\QMack";
        //string aa = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Serial.ini";
        //string ab = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        iniFile ini;
        protected IMasterCode MasterCode = HelperFactory.GetMasterCode();
        public string lsCount;
        public FMCSETTING_ENG()
        {
            InitializeComponent();
            ini = new iniFile(ab, aa);
            cb_mcA.SetDefault(false, "MachineCode", "MachineName", ModelService.GetList(P => P.UseYn == "Y"));
            //cb_mcB.SetDefault(false, "MachineCode", "MachineName", ModelService.GetList(P => P.UseYn == "Y"));
            cb_mcA.EditValue = ini.IniReadValue("Serial", "mcA", aa);
            //cb_mcB.EditValue = ini.IniReadValue("Serial", "mcB", aa);
            List<string> com = new List<string>(SerialPort.GetPortNames());
            if (com.Count >= 1)
            {
                cb_combo.Properties.Items.AddRange(com);
                cb_combo.EditValue = ini.IniReadValue("Serial", "port", aa);
                cb_mcA.EditValue = ini.IniReadValue("Serial", "mcA", aa);
                //cb_mcB.EditValue = ini.IniReadValue("Serial", "mcB", aa);
                cb_combo.SelectedIndex = com.Count - 1;
            }
            Agrid.ActDeleteRowClicked += Agrid_ActDeleteRowClicked;
            //Bgrid.ActDeleteRowClicked += Bgrid_ActDeleteRowClicked;
            iniGrid(); DATALOAD();
        }
        public FMCSETTING_ENG(TP_POPJOBLIST_V2 obj)
        {
            InitializeComponent();
            ini = new iniFile(ab, aa);
            cb_mcA.SetDefault(false, "MachineCode", "MachineName", ModelService.GetList(P => P.UseYn == "Y"));
            //cb_mcB.SetDefault(false, "MachineCode", "MachineName", ModelService.GetList(P => P.UseYn == "Y"));
            FMessagebox msg = new FMessagebox();
            msg.Owner = this;
            msg.ShowDialog();
            cb_mcA.EditValue = ini.IniReadValue("Serial", "mcA", aa);
            //cb_mcB.EditValue = ini.IniReadValue("Serial", "mcB", aa);
            if (lsCount == "A")
            {
                ini.IniWriteValue("Serial", "Workno", obj.WorkNo.GetNullToEmpty(), aa);
                ini.IniWriteValue("Serial", "process", obj.Process.GetNullToEmpty(), aa);
            }
            //if (lsCount == "B")
            //{
            //    ini.IniWriteValue("Serial", "Workno1", obj.WorkNo.GetNullToEmpty(), aa);
            //    ini.IniWriteValue("Serial", "process1", obj.Process.GetNullToEmpty(), aa);
            //}

            List<string> com = new List<string>(SerialPort.GetPortNames());
            if (com.Count >= 1)
            {
                cb_combo.Properties.Items.AddRange(com);
                cb_combo.EditValue = ini.IniReadValue("Serial", "port", aa);
                cb_mcA.EditValue = ini.IniReadValue("Serial", "mcA", aa);
                //cb_mcB.EditValue = ini.IniReadValue("Serial", "mcB", aa);
                cb_combo.SelectedIndex = com.Count - 1;
            }
            Agrid.ActDeleteRowClicked += Agrid_ActDeleteRowClicked;
            //Bgrid.ActDeleteRowClicked += Bgrid_ActDeleteRowClicked;
            iniGrid(); DATALOAD();
        }

        //private void Bgrid_ActDeleteRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    TP_POPJOBLIST_V2 obj = bindingSourceB.Current as TP_POPJOBLIST_V2;
        //    if(obj == null) { return; }
        //    bindingSourceB.Remove(obj);
        //    Bgrid.MainGrid.BestFitColumns();
        //}

        private void Agrid_ActDeleteRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TP_POPJOBLIST_V2 obj = bindingSourceA.Current as TP_POPJOBLIST_V2;
            if (obj == null) { return; }
            bindingSourceA.Remove(obj);
            Agrid.MainGrid.BestFitColumns();
        }

        private void iniGrid()
        {
            Agrid.MainGrid.Init();
            
            Agrid.SetToolbarButtonVisible(false);
            Agrid.SetToolbarButtonLargeIconVisible(true);
            Agrid.SetToolbarButtonFont(new Font(DefaultFont.FontFamily, 14, FontStyle.Bold));
            Agrid.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);           
            Agrid.MainGrid.AddColumn("RowIndex", false);
            Agrid.MainGrid.AddColumn("JobStatus", "JobStatus", HorzAlignment.Center, true);
            Agrid.MainGrid.AddColumn("DueDate", "DueDate", HorzAlignment.Center, false);
            Agrid.MainGrid.AddColumn("WorkNo", "WorkNo", HorzAlignment.Center, true);
            Agrid.MainGrid.AddColumn("Process", "공정코드", false);
            Agrid.MainGrid.AddColumn("ProcessName", "ProcessName");
            Agrid.MainGrid.AddColumn("MachineCode", "설비코드", false);
            Agrid.MainGrid.AddColumn("MachineName", "MachineName");
            Agrid.MainGrid.AddColumn("CustomerCode", "CustomerName");
            Agrid.MainGrid.AddColumn("ItemCode", "품목코드", false);
            Agrid.MainGrid.AddColumn("ItemNm1", "품번", false);
            Agrid.MainGrid.AddColumn("ItemNm", "ItemNm");
            Agrid.MainGrid.AddColumn("PlanQty", "PlanQty", HorzAlignment.Far, FormatType.Numeric, "n0");
            Agrid.MainGrid.AddColumn("StdPackQty", "StdPackQty", HorzAlignment.Far, FormatType.Numeric, "n0");
            Agrid.MainGrid.AddColumn("Memo", "비고", false);
            Agrid.MainGrid.AddColumn("WorkStantadNm", false);
            Agrid.MainGrid.AddColumn("FileData", false);
            Agrid.MainGrid.SetGridFont(Agrid.MainGrid.MainView, new Font("맑은 고딕", 13f, FontStyle.Bold));
            Agrid.MainGrid.MainView.ColumnPanelRowHeight = 50;
            Agrid.MainGrid.MainView.RowHeight = 50;
            Agrid.MainGrid.BestFitColumns();

            //Bgrid.MainGrid.Init();
            
            //Bgrid.SetToolbarButtonVisible(false);
            //Bgrid.SetToolbarButtonLargeIconVisible(true);
            //Bgrid.SetToolbarButtonFont(new Font(DefaultFont.FontFamily, 14, FontStyle.Bold));
            //Bgrid.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            //Bgrid.MainGrid.AddColumn("RowIndex", false);
            //Bgrid.MainGrid.AddColumn("JobStatus", "JobStatus", HorzAlignment.Center, true);
            //Bgrid.MainGrid.AddColumn("DueDate", "DueDate", HorzAlignment.Center, false);
            //Bgrid.MainGrid.AddColumn("WorkNo", "WorkNo", HorzAlignment.Center, true);
            //Bgrid.MainGrid.AddColumn("Process", "공정코드", false);
            //Bgrid.MainGrid.AddColumn("ProcessName", "ProcessName");
            //Bgrid.MainGrid.AddColumn("MachineCode", "설비코드", false);
            //Bgrid.MainGrid.AddColumn("MachineName", "MachineName");
            //Bgrid.MainGrid.AddColumn("CustomerCode", "CustomerName");
            //Bgrid.MainGrid.AddColumn("ItemCode", "품목코드", false);
            //Bgrid.MainGrid.AddColumn("ItemNm1", "품번", false);
            //Bgrid.MainGrid.AddColumn("ItemNm", "ItemNm");
            //Bgrid.MainGrid.AddColumn("PlanQty", "PlanQty", HorzAlignment.Far, FormatType.Numeric, "n0");
            //Bgrid.MainGrid.AddColumn("StdPackQty", "StdPackQty", HorzAlignment.Far, FormatType.Numeric, "n0");
            //Bgrid.MainGrid.AddColumn("Memo", "비고", false);
            //Bgrid.MainGrid.AddColumn("WorkStantadNm", false);
            //Bgrid.MainGrid.AddColumn("FileData", false);
            //Bgrid.MainGrid.SetGridFont(Bgrid.MainGrid.MainView, new Font("맑은 고딕", 13f, FontStyle.Bold));
            //Bgrid.MainGrid.MainView.ColumnPanelRowHeight = 50;
            //Bgrid.MainGrid.MainView.RowHeight = 50;
            //Agrid.MainGrid.BestFitColumns(); 

            Agrid.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStatus", MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList());
            Agrid.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag != "N"), "CustomerCode", "CustomerName");
            var repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            Agrid.MainGrid.MainView.OptionsView.ColumnAutoWidth = true;
            Agrid.MainGrid.MainView.OptionsView.RowAutoHeight = true;
            repositoryItemMemoEdit1.ScrollBars = ScrollBars.None;
            repositoryItemMemoEdit1.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            Agrid.MainGrid.MainView.Columns["ProcessName"].ColumnEdit = repositoryItemMemoEdit1;
            Agrid.MainGrid.MainView.Columns["ProcessName"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            Agrid.MainGrid.MainView.Columns["MachineName"].ColumnEdit = repositoryItemMemoEdit1;
            Agrid.MainGrid.MainView.Columns["MachineName"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            Agrid.MainGrid.MainView.Columns["ItemNm"].ColumnEdit = repositoryItemMemoEdit1;
            Agrid.MainGrid.MainView.Columns["ItemNm"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;

            //Bgrid.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStatus", MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList());
            //Bgrid.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag != "N"), "CustomerCode", "CustomerName");
            //Bgrid.MainGrid.MainView.Columns["ProcessName"].ColumnEdit = repositoryItemMemoEdit1;
            //Bgrid.MainGrid.MainView.Columns["ProcessName"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            //Bgrid.MainGrid.MainView.Columns["MachineName"].ColumnEdit = repositoryItemMemoEdit1;
            //Bgrid.MainGrid.MainView.Columns["MachineName"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            //Bgrid.MainGrid.MainView.Columns["ItemNm"].ColumnEdit = repositoryItemMemoEdit1;
            //Bgrid.MainGrid.MainView.Columns["ItemNm"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;

        }
        
        private void DATALOAD()
        {
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var wkno = new SqlParameter("@wkpno", ini.IniReadValue("Serial", "Workno", aa));
                var process = new SqlParameter("@process", ini.IniReadValue("Serial", "process", aa));
                var wkno1 = new SqlParameter("@wkpno1","");
                var process1 = new SqlParameter("@process1","");

                var result = context.Database
                      .SqlQuery<TP_POPJOBLIST_V2>("SP_POP_JOBLIST_MONITER @wkpno,@process ,@wkpno1,@process1", wkno, process, wkno1, process1).ToList();
                bindingSourceA.DataSource = result.Where(p => 1 == 1
                    //       //  && p.WorkNo.Contains(workno)

                    )
                    .OrderByDescending(p => p.JobStatus)/*.ThenBy(p => p.DueDate)*/.ThenBy(p => p.WorkNo).ThenBy(o => o.PSeq)/*.ThenBy(p => p.DisplayOrder)*/.ToList();
                Agrid.DataSource = bindingSourceA;
                Agrid.MainGrid.BestFitColumns();
            }

            //using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            //{
            //    var wkno = new SqlParameter("@wkpno", ini.IniReadValue("Serial", "Workno1", aa));
            //    var process = new SqlParameter("@process", ini.IniReadValue("Serial", "process1", aa));
            //    var wkno1 = new SqlParameter("@wkpno1", "");
            //    var process1 = new SqlParameter("@process1", "");

            //    var result = context.Database
            //          .SqlQuery<TP_POPJOBLIST_V2>("SP_POP_JOBLIST_MONITER @wkpno,@process ,@wkpno1,@process1", wkno, process, wkno1, process1).ToList();
            //    bindingSourceB.DataSource = result.Where(p => 1 == 1
            //        //       //  && p.WorkNo.Contains(workno)

            //        )
            //        .OrderByDescending(p => p.JobStatus)/*.ThenBy(p => p.DueDate)*/.ThenBy(p => p.WorkNo).ThenBy(o => o.PSeq)/*.ThenBy(p => p.DisplayOrder)*/.ToList();
            //    Bgrid.DataSource = bindingSourceB;
            //    Bgrid.MainGrid.BestFitColumns();
            //}
        }
        private void simpleButton12_Click(object sender, EventArgs e)
        {
           
            //ini = new iniFile(ab, aa);
            TP_POPJOBLIST_V2 objA = bindingSourceA.Current as TP_POPJOBLIST_V2;
            if (objA == null)
            {
                ini.IniWriteValue("Serial", "Workno","", aa);
                ini.IniWriteValue("Serial", "process", "", aa);

            }
            else {
                ini.IniWriteValue("Serial", "Workno", objA.WorkNo.GetNullToEmpty(), aa);
                ini.IniWriteValue("Serial", "process", objA.Process.GetNullToEmpty(), aa);

            }
            //TP_POPJOBLIST_V2 objB = bindingSourceB.Current as TP_POPJOBLIST_V2;
            //if (objB == null)
            //{
            //    ini.IniWriteValue("Serial", "Workno1", "", aa);
            //    ini.IniWriteValue("Serial", "process1", "", aa);

            //}
            //else {
            //    ini.IniWriteValue("Serial", "Workno1", objB.WorkNo.GetNullToEmpty(), aa);
            //    ini.IniWriteValue("Serial", "process1", objB.Process.GetNullToEmpty(), aa);


            //}


            ini.IniWriteValue("Serial", "port", cb_combo.SelectedItem.GetNullToEmpty(), aa);    
            ini.IniWriteValue("Serial", "mcA", cb_mcA.EditValue==null?"": cb_mcA.EditValue.ToString(), aa);
            //ini.IniWriteValue("Serial", "mcB", cb_mcB.EditValue == null ? "" : cb_mcB.EditValue.ToString(), aa);
            HKRS232.lsPort = cb_combo.SelectedItem.GetNullToEmpty();
            HKRS232.lsmcA = cb_mcA.EditValue == null ? "" : cb_mcA.EditValue.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton22_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
