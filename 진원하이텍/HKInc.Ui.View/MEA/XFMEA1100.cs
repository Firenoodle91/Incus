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
    public partial class XFMEA1100 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
       
        public XFMEA1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {

                DateTime InstallDate = Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, View.Columns["InstallDate"]));
                object NextCheck = View.GetRowCellValue(e.RowHandle, View.Columns["NextCheck"]);
                object checkturn= View.GetRowCellValue(e.RowHandle, View.Columns["CheckTurn"]).GetNullToEmpty();
                if (NextCheck == null)
                {
                    switch (checkturn.ToString())
                    {
                        case "18":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddDays(1));
                            NextCheck = InstallDate.AddDays(1);
                            break;
                        case "19":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddDays(7));
                            NextCheck = InstallDate.AddDays(7);
                            break;
                        case "20":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddMonths(1));
                            NextCheck = InstallDate.AddMonths(1);
                            break;
                        case "21":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddYears(1));
                            NextCheck = InstallDate.AddYears(1);
                            break;
                        case "22":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddMonths(2));
                            NextCheck = InstallDate.AddMonths(2);
                            break;
                        case "23":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddMonths(3));
                            NextCheck = InstallDate.AddMonths(3);
                            break;
                        case "24":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddMonths(4));
                            NextCheck = InstallDate.AddMonths(4);
                            break;
                        case "25":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddMonths(5));
                            NextCheck = InstallDate.AddMonths(5);
                            break;
                        case "26":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NextCheck"], InstallDate.AddMonths(6));
                            NextCheck = InstallDate.AddMonths(6);
                            break;
                    }
                }
                if (NextCheck.GetNullToEmpty() != "")
                {
                    try
                    {
                        if (Convert.ToDateTime(NextCheck).AddDays(-14) <= DateTime.Today)
                        {
                            e.Appearance.BackColor = Color.Red;
                            e.Appearance.ForeColor = Color.White;
                        }
                    }
                    catch { }
                }
            }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.Column.Name != "CheckDate") return;
            TN_MEA1000 tn = MasterGridBindingSource.Current as TN_MEA1000;
            TN_MEA1100 tn1 = DetailGridBindingSource.Current as TN_MEA1100;
            DateTime dt=Convert.ToDateTime(tn1.CheckDate);
            switch (tn.CheckTurn)
            {
                case "18":
                    tn.NextCheck = dt.AddDays(1);
                    break;
                case "19":
                    tn.NextCheck = dt.AddDays(7);
                    break;
                case "20":
                    tn.NextCheck = dt.AddMonths(1);
                    break;
                case "21":
                    tn.NextCheck = dt.AddYears(1);
                    break;
                case "22":
                    tn.NextCheck = dt.AddMonths(2);
                    break;
                case "23":
                    tn.NextCheck = dt.AddMonths(3);
                    break;
                case "24":
                    tn.NextCheck = dt.AddMonths(4);
                    break;
                case "25":
                    tn.NextCheck = dt.AddMonths(5);
                    break;
                case "26":
                    tn.NextCheck = dt.AddMonths(6);
                    break;
              
            }
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
       //     DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비코드");
            MasterGridExControl.MainGrid.AddColumn("MachineName", "설비명");
            MasterGridExControl.MainGrid.AddColumn("ModelNo", "모델번호");
            MasterGridExControl.MainGrid.AddColumn("Maker", "제조회사");
            MasterGridExControl.MainGrid.AddColumn("InstallDate", "설치일");
            MasterGridExControl.MainGrid.AddColumn("SerialNo", "일련번호");
            MasterGridExControl.MainGrid.AddColumn("CheckTurn", "점검주기", DevExpress.Utils.HorzAlignment.Far, true);
            MasterGridExControl.MainGrid.AddColumn("NextCheck", "다음점검일", DevExpress.Utils.HorzAlignment.Far, true);
            

            DetailGridExControl.MainGrid.AddColumn("MachineSeq", false);
            DetailGridExControl.MainGrid.AddColumn("CheckDate", "점검일자", DevExpress.Utils.HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("MachineCode", false);
            DetailGridExControl.MainGrid.AddColumn("CheckMemo", "점검내용");
            DetailGridExControl.MainGrid.AddColumn("CheckId", "점검자");


            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckDate", "CheckMemo", "CheckId");





        }
        protected override void InitBindingSource()
        {
            base.InitBindingSource();

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("NextCheck");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InstallDate");
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("Maker", MasterCode.GetMasterCode((int)MasterCodeEnum.Maker).ToList());
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("CheckTurn", MasterCode.GetMasterCode((int)MasterCodeEnum.CheckTurn).ToList());
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("CheckDate");
            DetailGridExControl.MainGrid.MainView.Columns["CheckMemo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["CheckMemo"].Width = 150;
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<UserView>(p => p.Active=="Y"), "LoginId", "UserName");
        }
        protected override void DataLoad()
        {

            GridRowLocator.GetCurrentRow();
            DetailGridRowLocator.GetCurrentRow("MachineSeq");
            MasterGridExControl.MainGrid.Clear();

            ModelService.ReLoad();
          

            MasterGridExControl.DataSource = null;
            DetailGridExControl.DataSource = null;
            string lMachinecode = tx_MachineCode.Text;
          
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(lMachinecode) ? true : (p.MachineName.Contains(lMachinecode) ||    p.MachineCode==lMachinecode))&&p.SerialNo!="ETC").ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }
        protected override void MasterFocusedRowChanged()
        {

            TN_MEA1000 obj = MasterGridBindingSource.Current as TN_MEA1000;
            DetailGridBindingSource.DataSource = obj.TN_MEA1100List.OrderBy(o=>o.CheckDate).ToList();//.GetList(p => (p.MachineCode == obj.MachineCode)).OrderBy(p => p.MachineSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

        }
        protected override void DetailAddRowClicked()
        {
            //    base.DetailAddRowClicked();
            if (!UserRight.HasEdit) return;
            TN_MEA1000 obj1 = MasterGridBindingSource.Current as TN_MEA1000;
            if (obj1 != null)
            {
                TN_MEA1100 obj = new TN_MEA1100();
                obj.MachineSeq = obj1.TN_MEA1100List.Count == 0 ? 1 : obj1.TN_MEA1100List.Count + 1;
                obj.CheckDate = DateTime.Today;
                obj.MachineCode = obj1.MachineCode;


              
                DetailGridBindingSource.Add(obj);
                obj1.TN_MEA1100List.Add(obj);
                DetailGridBindingSource.MoveLast();
              
            }
            TN_MEA1000 tn = MasterGridBindingSource.Current as TN_MEA1000;
            TN_MEA1100 tn1 = DetailGridBindingSource.Current as TN_MEA1100;
            DateTime dt = Convert.ToDateTime(tn1.CheckDate);
            switch (tn.CheckTurn)
            {
                case "18":
                    tn.NextCheck = dt.AddDays(1);
                    break;
                case "19":
                    tn.NextCheck = dt.AddDays(7);
                    break;
                case "20":
                    tn.NextCheck = dt.AddMonths(1);
                    break;
                case "21":
                    tn.NextCheck = dt.AddYears(1);
                    break;
                case "22":
                    tn.NextCheck = dt.AddMonths(2);
                    break;
                case "23":
                    tn.NextCheck = dt.AddMonths(3);
                    break;
                case "24":
                    tn.NextCheck = dt.AddMonths(4);
                    break;
                case "25":
                    tn.NextCheck = dt.AddMonths(5);
                    break;
                case "26":
                    tn.NextCheck = dt.AddMonths(6);
                    break;

            }
            MasterGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DeleteDetailRow()
        {
            
            TN_MEA1000 obj1 = MasterGridBindingSource.Current as TN_MEA1000;
            TN_MEA1100 obj = DetailGridBindingSource.Current as TN_MEA1100;

            if (obj != null)
            {
                obj1.TN_MEA1100List.Remove(obj);
                DetailGridBindingSource.RemoveCurrent();
            }
        }

        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            foreach (var rowHandle in gridEx2.MainGrid.MainView.GetSelectedRows())
            {
                string _ProcessCode = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckDate").GetNullToEmpty());
                string _CheckMemo = gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckMemo").GetNullToEmpty();
                string _CheckId = gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckId").GetNullToEmpty();

                if (_ProcessCode == null || _ProcessCode == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("이력리스트" + Convert.ToInt32(rowHandle + 1) + "행의 점검일은 필수입력 사항입니다.");
                    return;
                }

                if (_CheckMemo == null || _CheckMemo == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("이력리스트 " + Convert.ToInt32(rowHandle + 1) + "행의 점검내용은 필수입력 사항입니다.");
                    return;
                }

                if (_CheckId == null || _CheckId == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("이력리스트 " + Convert.ToInt32(rowHandle + 1) + "행의 담당자는 필수입력 사항입니다.");
                    return;
                }

            }

            base.DataSave();
            ModelService.Save();
            DataLoad();
        }
    }
}