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
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.MEA
{
    /// <summary>
    /// 설비가동정보등록화면
    /// </summary>
    public partial class XFMTTF1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
       
        public XFMTTF1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            
        }

        protected override void InitCombo()
        {
            lupmc.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList());
        }    

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비코드");
            MasterGridExControl.MainGrid.AddColumn("MachineName", "설비명");
            MasterGridExControl.MainGrid.AddColumn("ModelNo", "모델번호");
            MasterGridExControl.MainGrid.AddColumn("Maker", "제조회사");
            MasterGridExControl.MainGrid.AddColumn("InstallDate", "설치일");
            MasterGridExControl.MainGrid.AddColumn("SerialNo", "일련번호");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("MachineCode", false);
            DetailGridExControl.MainGrid.AddColumn("YYYY", "년도", DevExpress.Utils.HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("MM", "월");
            DetailGridExControl.MainGrid.AddColumn("RunTime", "작업시간(분)", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("StopTime", "정지시간(분)", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("StopCnt", "정지횟수", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "MM", "RunTime", "StopTime", "StopCnt");
            DetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "MM");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InstallDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker",  DbRequesHandler.GetCommCode(MasterCodeSTR.MCMAKER), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MM", DbRequesHandler.GetCommCode(MasterCodeSTR.MM), "Mcode", "Codename");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MachineCode");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string lMachinecode = lupmc.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(lMachinecode) ? true : (p.MachineName.Contains(lMachinecode) || p.MachineCode == lMachinecode))).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_MEA1000 obj = MasterGridBindingSource.Current as TN_MEA1000;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_MTTF1000>(p => p.MachineCode == obj.MachineCode).OrderBy(o => o.YYYY).OrderBy(o => o.MM).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            if (!UserRight.HasEdit) return;
            TN_MEA1000 obj1 = MasterGridBindingSource.Current as TN_MEA1000;
            if (obj1 != null)
            {
                TN_MTTF1000 obj = new TN_MTTF1000();
                obj.MachineCode = obj1.MachineCode;
                obj.YYYY = DateTime.Today.ToString("yyyy");

                DetailGridBindingSource.Add(obj);
                ModelService.InsertChild<TN_MTTF1000>(obj);
                DetailGridBindingSource.MoveLast();              
            }            
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {            
            TN_MEA1000 obj1 = MasterGridBindingSource.Current as TN_MEA1000;
            if (obj1 == null) return;
            TN_MTTF1000 obj = DetailGridBindingSource.Current as TN_MTTF1000;

            if (obj != null)
            {
                ModelService.RemoveChild<TN_MTTF1000>(obj);
                DetailGridBindingSource.RemoveCurrent();
            }
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.Column.Name != "MM") return;
            TN_MTTF1000 tn1 = DetailGridBindingSource.Current as TN_MTTF1000;
            DataTable dt = DbRequesHandler.GetDTselect("exec SP_MTTF_DATA '" + tn1.YYYY + "','" + tn1.MM + "','" + tn1.MachineCode + "'");
            if (dt != null)
            {
                tn1.StopTime = Convert.ToInt32(dt.Rows[0][0].ToString());
                tn1.StopCnt = Convert.ToInt32(dt.Rows[0][1].ToString());
            }
            else
            {
                tn1.StopTime = 0;
                tn1.StopCnt = 0;
            }
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;
            tx_filename.Text = dlg.FileName.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (tx_filename.Text == "")
            {
                MessageBox.Show("업로드할 엑셀 파일을 선택하여 주십시오.");
                return;
            }

            DataTable dt = HKInc.Service.Helper.ExcelImport.GetTableFromExcel(tx_filename.Text, 0);
            if (dt.Columns[0].ColumnName != "MACHINE_CODE"
               || dt.Columns[2].ColumnName != "YYYY"
               || dt.Columns[3].ColumnName != "MM"
               || dt.Columns[4].ColumnName != "RUN_TIME"
               || dt.Columns[5].ColumnName != "STOP_TIME"
               || dt.Columns[6].ColumnName != "STOP_CNT")
            {
                MessageBox.Show("파일형태가 잘못되었습니다.");
                return;
            }

            foreach (DataRow dtrow in dt.Rows)
            {
                TN_MTTF1000 row = new TN_MTTF1000();       
                row.MachineCode = dtrow[0].GetNullToEmpty().Trim();
                row.YYYY = dtrow[2].GetNullToEmpty().Trim();
                row.MM = dtrow[3].GetNullToEmpty().Trim();
                row.RunTime = dtrow[4].GetIntNullToZero();
                row.StopTime = dtrow[5].GetIntNullToZero();
                row.StopCnt = dtrow[6].GetIntNullToZero();
                DetailGridBindingSource.Add(row);
                ModelService.InsertChild<TN_MTTF1000>(row);
            }
        }
    }
}