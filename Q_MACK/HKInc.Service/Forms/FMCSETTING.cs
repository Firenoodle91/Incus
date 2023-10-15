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
using HKInc.Service.Handler;

namespace HKInc.Service.Controls
{
    public partial class FMCSETTING : Form
    {
        #region 전역변수
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
        string aa = "C:\\qmack\\Serial.ini";
        string ab = "C:\\qmack";
        //string aa = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Serial.ini";
        //string ab = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        iniFile ini;
        TP_XFPOP1000_V2_LIST POP_Data;
        protected IMasterCode MasterCode = HelperFactory.GetMasterCode();
        public string lsCount;
        bool Check;
        #endregion


        public FMCSETTING(TP_XFPOP1000_V2_LIST obj, iniFile INI)
        {
            InitializeComponent();
            ini = INI;
            POP_Data = obj;
            Check = false;
            //ini = new iniFile(ab, aa);
            cb_mcA.SetDefault(false, "MachineCode", "MachineName", ModelService.GetList(P => P.UseYn == "Y" && P.MonitorLocation != null));
            cb_mcA.EditValue = ini.IniReadValue("Serial", "mcA", aa);

            List<string> com = new List<string>(SerialPort.GetPortNames());

            if (com.Count >= 1)
            {
                cb_combo.Properties.Items.AddRange(com);
                cb_combo.EditValue = ini.IniReadValue("Serial", "port", aa);
                cb_combo.SelectedIndex = com.Count - 1;
            }
            Agrid.ActDeleteRowClicked += Agrid_ActDeleteRowClicked;
            Agrid.ActAddRowClicked += Agrid_ActAddRowClicked;
            iniGrid();
            DATALOAD();
        }

        public FMCSETTING()
        {
            InitializeComponent();
            ini = new iniFile(ab, aa);
            cb_mcA.SetDefault(false, "MachineCode", "MachineName", ModelService.GetList(P => P.UseYn == "Y" && P.MonitorLocation != null));
            cb_mcA.EditValue = ini.IniReadValue("Serial", "mcA", aa);
            List<string> com = new List<string>(SerialPort.GetPortNames());
            if (com.Count >= 1)
            {
                cb_combo.Properties.Items.AddRange(com);
                cb_combo.EditValue = ini.IniReadValue("Serial", "port", aa);
                cb_combo.SelectedIndex = com.Count - 1;
            }
            Agrid.ActDeleteRowClicked += Agrid_ActDeleteRowClicked;
            Agrid.ActAddRowClicked += Agrid_ActAddRowClicked;
            iniGrid();
            DATALOAD();
        }

        #region 기존

        //public FMCSETTING()
        //{
        //    InitializeComponent();
        //    ini = new iniFile(ab, aa);
        //    cb_mcA.SetDefault(false, "MachineCode", "MachineName", ModelService.GetList(P => P.UseYn == "Y"));
        //    //cb_mcB.SetDefault(false, "MachineCode", "MachineName", ModelService.GetList(P => P.UseYn == "Y"));
        //    cb_mcA.EditValue = ini.IniReadValue("Serial", "mcA", aa);
        //    //cb_mcB.EditValue = ini.IniReadValue("Serial", "mcB", aa);
        //    List<string> com = new List<string>(SerialPort.GetPortNames());
        //    if (com.Count >= 1)
        //    {
        //        cb_combo.Properties.Items.AddRange(com);
        //        cb_combo.EditValue = ini.IniReadValue("Serial", "port", aa);
        //        cb_combo.SelectedIndex = com.Count - 1;
        //    }
        //    Agrid.ActDeleteRowClicked += Agrid_ActDeleteRowClicked;
        //    //Bgrid.ActDeleteRowClicked += Bgrid_ActDeleteRowClicked;
        //    iniGrid();
        //    DATALOAD();
        //}

        //public FMCSETTING(TP_POPJOBLIST_V2 obj)
        //{
        //InitializeComponent();
        //ini = new iniFile(ab, aa);
        //cb_mcA.SetDefault(false, "MachineCode", "MachineName", ModelService.GetList(P => P.UseYn == "Y"));
        ////cb_mcB.SetDefault(false, "MachineCode", "MachineName", ModelService.GetList(P => P.UseYn == "Y"));
        //FMessagebox msg = new FMessagebox();
        //msg.Owner = this;
        //msg.ShowDialog();
        //cb_mcA.EditValue = ini.IniReadValue("Serial", "mcA", aa);
        ////cb_mcB.EditValue = ini.IniReadValue("Serial", "mcB", aa);
        //if (lsCount == "A")
        //{
        //    ini.IniWriteValue("Serial", "Workno", obj.WorkNo.GetNullToEmpty(), aa);
        //    ini.IniWriteValue("Serial", "process", obj.Process.GetNullToEmpty(), aa);
        //}
        ////if (lsCount == "B")
        ////{
        ////    ini.IniWriteValue("Serial", "Workno1", obj.WorkNo.GetNullToEmpty(), aa);
        ////    ini.IniWriteValue("Serial", "process1", obj.Process.GetNullToEmpty(), aa);
        ////}

        //List<string> com = new List<string>(SerialPort.GetPortNames());
        //if (com.Count >= 1)
        //{
        //    cb_combo.Properties.Items.AddRange(com);
        //    cb_combo.EditValue = ini.IniReadValue("Serial", "port", aa);
        //    cb_mcA.EditValue = ini.IniReadValue("Serial", "mcA", aa);
        //    //cb_mcB.EditValue = ini.IniReadValue("Serial", "mcB", aa);
        //    cb_combo.SelectedIndex = com.Count - 1;
        //}
        //Agrid.ActDeleteRowClicked += Agrid_ActDeleteRowClicked;
        ////Bgrid.ActDeleteRowClicked += Bgrid_ActDeleteRowClicked;
        //iniGrid(); DATALOAD();
        //}

        //private void Bgrid_ActDeleteRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    TP_POPJOBLIST_V2 obj = bindingSourceB.Current as TP_POPJOBLIST_V2;
        //    if(obj == null) { return; }
        //    bindingSourceB.Remove(obj);
        //    Bgrid.MainGrid.BestFitColumns();
        //}
        #endregion

        private void Agrid_ActAddRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cb_mcA.EditValue.GetNullToEmpty() == "")
            {
                MessageBoxHandler.Show("설비를 선택하세요.");
                return;
            }

            ini.IniWriteValue("Serial", "Workno", POP_Data.WorkNo, aa);
            ini.IniWriteValue("Serial", "process", POP_Data.Process, aa);

            TN_MEA1000 MEA1000 = ModelService.GetList(p => p.MachineCode == cb_mcA.EditValue.ToString()).FirstOrDefault();
            if (MEA1000 == null) return;
            TN_MEA1700 MEA1700 = ModelService.GetChildList<TN_MEA1700>(p => p.Connection == MEA1000.MonitorLocation).FirstOrDefault();
            if (MEA1700 != null)
            {
                MEA1700.MachineCode = cb_mcA.EditValue == null ? "" : cb_mcA.EditValue.ToString();
                MEA1700.RunStatus = "RUN";
                MEA1700.WorkNo = POP_Data.WorkNo;
                MEA1700.PrevCountTime = MEA1700.CountTime;
                MEA1700.CountTime = DateTime.Now;

                ModelService.UpdateChild<TN_MEA1700>(MEA1700);
            }
            DATALOAD();
        }

        private void Agrid_ActDeleteRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TP_POPJOBLIST_V2 obj = bindingSourceA.Current as TP_POPJOBLIST_V2;
            if (obj == null) { return; }
            bindingSourceA.Remove(obj);
            ini.IniWriteValue("Serial", "Workno", "", aa);
            ini.IniWriteValue("Serial", "process", "", aa);

            TN_MEA1000 MEA1000 = ModelService.GetList(p => p.MachineCode == cb_mcA.EditValue.ToString()).FirstOrDefault();
            if (MEA1000 == null) return;
            TN_MEA1700 MEA1700 = ModelService.GetChildList<TN_MEA1700>(p => p.Connection == MEA1000.MonitorLocation).FirstOrDefault();
            if (MEA1700 != null)
            {
                MEA1700.RunStatus = "STOP";
                MEA1700.PrevCountTime = MEA1700.CountTime;
                MEA1700.CountTime = DateTime.Now;

                ModelService.UpdateChild<TN_MEA1700>(MEA1700);
            }

            cb_mcA.EditValue = null;
            Agrid.MainGrid.BestFitColumns();
        }

        private void iniGrid()
        {
            Agrid.MainGrid.Init();

            Agrid.SetToolbarButtonVisible(false);
            Agrid.SetToolbarButtonLargeIconVisible(true);
            Agrid.SetToolbarButtonFont(new Font(DefaultFont.FontFamily, 14, FontStyle.Bold));
            Agrid.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            Agrid.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            Agrid.MainGrid.AddColumn("RowIndex", false);
            Agrid.MainGrid.AddColumn("JobStatus", "작업상태", HorzAlignment.Center, true);
            Agrid.MainGrid.AddColumn("DueDate", "납기일", HorzAlignment.Center, false);
            Agrid.MainGrid.AddColumn("WorkNo", "작업지시번호", HorzAlignment.Center, true);
            Agrid.MainGrid.AddColumn("Process", "공정코드", false);
            Agrid.MainGrid.AddColumn("ProcessName", "공정");
            Agrid.MainGrid.AddColumn("MachineCode", "설비코드", false);
            Agrid.MainGrid.AddColumn("MachineName", "설비");
            Agrid.MainGrid.AddColumn("CustomerCode", "거래처명");
            Agrid.MainGrid.AddColumn("ItemCode", "품목코드", false);
            Agrid.MainGrid.AddColumn("ItemNm1", "품번", false);
            Agrid.MainGrid.AddColumn("ItemNm", "품명");
            Agrid.MainGrid.AddColumn("PlanQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            Agrid.MainGrid.AddColumn("StdPackQty", "박스당수량", HorzAlignment.Far, FormatType.Numeric, "n0", false);
            Agrid.MainGrid.AddColumn("Memo", "비고", false);
            Agrid.MainGrid.AddColumn("WorkStantadNm", false);
            Agrid.MainGrid.AddColumn("FileData", false);
            Agrid.MainGrid.SetGridFont(Agrid.MainGrid.MainView, new Font("맑은 고딕", 13f, FontStyle.Bold));
            Agrid.MainGrid.MainView.ColumnPanelRowHeight = 50;
            Agrid.MainGrid.MainView.RowHeight = 50;
            Agrid.MainGrid.BestFitColumns();

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

            #region 제거
            //Bgrid.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStatus", MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList());
            //Bgrid.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag != "N"), "CustomerCode", "CustomerName");
            //Bgrid.MainGrid.MainView.Columns["ProcessName"].ColumnEdit = repositoryItemMemoEdit1;
            //Bgrid.MainGrid.MainView.Columns["ProcessName"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            //Bgrid.MainGrid.MainView.Columns["MachineName"].ColumnEdit = repositoryItemMemoEdit1;
            //Bgrid.MainGrid.MainView.Columns["MachineName"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            //Bgrid.MainGrid.MainView.Columns["ItemNm"].ColumnEdit = repositoryItemMemoEdit1;
            //Bgrid.MainGrid.MainView.Columns["ItemNm"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            #endregion
        }

        private void DATALOAD()
        {
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                //var wkno = new SqlParameter("@wkpno", POP_Data.WorkNo);
                //var process = new SqlParameter("@process", POP_Data.Process);
                var wkno = new SqlParameter("@wkpno", ini.IniReadValue("Serial", "Workno", aa));
                var process = new SqlParameter("@process", ini.IniReadValue("Serial", "process", aa));
                var wkno1 = new SqlParameter("@wkpno1", "");
                var process1 = new SqlParameter("@process1", "");

                var result = context.Database
                      .SqlQuery<TP_POPJOBLIST_V2>("SP_POP_JOBLIST_MONITER @wkpno,@process ,@wkpno1,@process1", wkno, process, wkno1, process1).ToList();
                bindingSourceA.DataSource = result.Where(p => true)
                    .OrderByDescending(p => p.JobStatus).ThenBy(p => p.DueDate).ThenBy(p => p.WorkNo).ThenBy(o => o.PSeq)./*ThenBy(p => p.DisplayOrder).*/ToList();
                Agrid.DataSource = bindingSourceA;
                Agrid.MainGrid.BestFitColumns();
            }
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            ini.IniWriteValue("Serial", "port", cb_combo.SelectedItem.GetNullToEmpty(), aa);
            ini.IniWriteValue("Serial", "mcA", cb_mcA.EditValue == null ? "" : cb_mcA.EditValue.ToString(), aa);
            HKRS232.lsPort = cb_combo.SelectedItem.GetNullToEmpty();
            HKRS232.lsmcA = cb_mcA.EditValue == null ? "" : cb_mcA.EditValue.ToString();

            ModelService.Save();
            Check = true;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton22_Click(object sender, EventArgs e)
        {
            if (Check)
            {
                ini.IniWriteValue("Serial", "Workno", POP_Data.WorkNo, aa);
                ini.IniWriteValue("Serial", "process", POP_Data.Process, aa);
            }
            else
            {
                ini.IniWriteValue("Serial", "Workno", "", aa);
                ini.IniWriteValue("Serial", "process", "", aa);
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
