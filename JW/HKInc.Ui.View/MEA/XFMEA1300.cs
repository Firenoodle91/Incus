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
    public partial class XFMEA1300 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MEA1200> ModelService = (IService<TN_MEA1200>)ProductionFactory.GetDomainService("TN_MEA1200");
       
        public XFMEA1300()
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

                DateTime InstallDate = Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, View.Columns["PurcDate"]));
                object NextCheck = View.GetRowCellValue(e.RowHandle, View.Columns["NxcorDate"]);
                object checkturn= View.GetRowCellValue(e.RowHandle, View.Columns["CorTurn"]);
                object corDate = View.GetRowCellValue(e.RowHandle, View.Columns["CorDate"]);
                //if (corDate == null)
                //{
                //    View.SetRowCellValue(e.RowHandle, View.Columns["CorDate"], InstallDate);
                //}
                if (NextCheck == null)
                {
                    switch (checkturn.ToString())
                    {
                        case "18":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NxcorDate"], InstallDate.AddDays(1));
                            NextCheck = InstallDate.AddDays(1);
                            break;
                        case "19":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NxcorDate"], InstallDate.AddDays(7));
                            NextCheck = InstallDate.AddDays(7);
                            break;
                        case "20":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NxcorDate"], InstallDate.AddMonths(1));
                            NextCheck = InstallDate.AddMonths(1);
                            break;
                        case "21":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NxcorDate"], InstallDate.AddYears(1));
                            NextCheck = InstallDate.AddYears(1);
                            break;
                        case "22":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NxcorDate"], InstallDate.AddMonths(2));
                            NextCheck = InstallDate.AddMonths(2);
                            break;
                        case "23":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NxcorDate"], InstallDate.AddMonths(3));
                            NextCheck = InstallDate.AddMonths(3);
                            break;
                        case "24":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NxcorDate"], InstallDate.AddMonths(4));
                            NextCheck = InstallDate.AddMonths(4);
                            break;
                        case "25":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NxcorDate"], InstallDate.AddMonths(5));
                            NextCheck = InstallDate.AddMonths(5);
                            break;
                        case "26":
                            View.SetRowCellValue(e.RowHandle, View.Columns["NxcorDate"], InstallDate.AddMonths(6));
                            NextCheck = InstallDate.AddMonths(6);
                            break;

                    }
                }




                if (NextCheck.GetNullToEmpty() != "")
                {
                    if (Convert.ToDateTime(NextCheck).AddDays(-14) < DateTime.Today)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;

                    }
                }
            }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            
            TN_MEA1200 tn = MasterGridBindingSource.Current as TN_MEA1200;
            TN_MEA1300 tn1 = DetailGridBindingSource.Current as TN_MEA1300;
            DateTime dt=Convert.ToDateTime(tn1.CheckDate);
            if (e.Column.Name == "CheckDate")
            {
                switch (tn.CorTurn)
                {
                    case "18":
                        tn.NxcorDate = dt.AddDays(1);
                        break;
                    case "19":
                        tn.NxcorDate = dt.AddDays(7);
                        break;
                    case "20":
                        tn.NxcorDate = dt.AddMonths(1);
                        break;
                    case "21":
                        tn.NxcorDate = dt.AddYears(1);
                        break;
                    case "22":
                        tn.NxcorDate = dt.AddMonths(2);
                        break;
                    case "23":
                        tn.NxcorDate = dt.AddMonths(3);
                        break;
                    case "24":
                        tn.NxcorDate = dt.AddMonths(4);
                        break;
                    case "25":
                        tn.NxcorDate = dt.AddMonths(5);
                        break;
                    case "26":
                        tn.NxcorDate = dt.AddMonths(6);
                        break;

                }
                tn.CorDate = dt;
            }
            if (e.Column.Name== "CheckGu") {
                if (tn1.CheckGu == "A03")
                {
                    tn.UseYn = "N";
                }
                else {
                    tn.UseYn = "Y";
                }
            }
            MasterGridExControl.MainGrid.BestFitColumns();
        }
        private void chechkDate()
        {
           

            TN_MEA1200 tn = MasterGridBindingSource.Current as TN_MEA1200;
            TN_MEA1300 tn1 = DetailGridBindingSource.Current as TN_MEA1300;
            DateTime dt = Convert.ToDateTime(tn1.CheckDate);
         
                switch (tn.CorTurn)
                {
                    case "18":
                        tn.NxcorDate = dt.AddDays(1);
                        break;
                    case "19":
                        tn.NxcorDate = dt.AddDays(7);
                        break;
                    case "20":
                        tn.NxcorDate = dt.AddMonths(1);
                        break;
                    case "21":
                        tn.NxcorDate = dt.AddYears(1);
                        break;
                    case "22":
                        tn.NxcorDate = dt.AddMonths(2);
                        break;
                    case "23":
                        tn.NxcorDate = dt.AddMonths(3);
                        break;
                    case "24":
                        tn.NxcorDate = dt.AddMonths(4);
                        break;
                    case "25":
                        tn.NxcorDate = dt.AddMonths(5);
                        break;
                    case "26":
                        tn.NxcorDate = dt.AddMonths(6);
                        break;

                }
                tn.CorDate = dt;
           
        
            MasterGridExControl.MainGrid.BestFitColumns();


        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            MasterGridExControl.MainGrid.AddColumn("InstrNo", "설비코드");
            MasterGridExControl.MainGrid.AddColumn("InstrNm", "설비명");
            MasterGridExControl.MainGrid.AddColumn("Spec", "모델번호");
            MasterGridExControl.MainGrid.AddColumn("Maker", "제조회사");
            MasterGridExControl.MainGrid.AddColumn("PurcDate", "도입일");
            MasterGridExControl.MainGrid.AddColumn("SerialNo", "일련번호");
            MasterGridExControl.MainGrid.AddColumn("CorTurn", "검교정주기", DevExpress.Utils.HorzAlignment.Far, true);
            MasterGridExControl.MainGrid.AddColumn("CorDate", "검교정일");
            MasterGridExControl.MainGrid.AddColumn("NxcorDate", "다음검교정일", DevExpress.Utils.HorzAlignment.Far, true);
            MasterGridExControl.MainGrid.AddColumn("UseYn", false);

            DetailGridExControl.MainGrid.AddColumn("InstrSeq", false);
            DetailGridExControl.MainGrid.AddColumn("CheckDate", "점검일자", DevExpress.Utils.HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("InstrNo", false);
            DetailGridExControl.MainGrid.AddColumn("CheckGu","구분");
            DetailGridExControl.MainGrid.AddColumn("Memo", "점검내용");
            DetailGridExControl.MainGrid.AddColumn("CheckId", "점검자");


            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckDate", "CheckGu", "Memo", "CheckId");





        }
        protected override void InitBindingSource()
        {
            base.InitBindingSource();

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("CorDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("NxcorDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PurcDate");
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("Maker", MasterCode.GetMasterCode((int)MasterCodeEnum.Maker).ToList());
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("CorTurn", MasterCode.GetMasterCode((int)MasterCodeEnum.CheckTurn).ToList());
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("CheckDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].Width = 150;
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<UserView>(p => p.Active=="Y"), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckGu", DbRequesHandler.GetCommCode(MasterCodeSTR.CheckGu), "Mcode", "Codename");
        }
        protected override void DataLoad()
        {


            GridRowLocator.GetCurrentRow("InstrNo");
            DetailGridRowLocator.GetCurrentRow("InstrNo");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
         

            
            string lMachinecode = tx_MachineCode.Text;
            if (checkEdit1.Checked == true)
            {

                MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(lMachinecode) ? true : (p.InstrNm.Contains(lMachinecode) || p.InstrNo == lMachinecode))).ToList();

            }
            else {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.UseYn=="Y")&&(string.IsNullOrEmpty(lMachinecode) ? true : (p.InstrNm.Contains(lMachinecode) || p.InstrNo == lMachinecode))).ToList();

            }
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }
        protected override void MasterFocusedRowChanged()
        {

            TN_MEA1200 obj = MasterGridBindingSource.Current as TN_MEA1200;
            DetailGridBindingSource.DataSource = obj.TN_MEA1300List.OrderBy(o=>o.CheckDate).ToList();//.GetList(p => (p.MachineCode == obj.MachineCode)).OrderBy(p => p.MachineSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

        }
        protected override void DetailAddRowClicked()
        {
            //    base.DetailAddRowClicked();
            if (!UserRight.HasEdit) return;
            TN_MEA1200 obj1 = MasterGridBindingSource.Current as TN_MEA1200;
            if (obj1 != null)
            {
                TN_MEA1300 obj = new TN_MEA1300();
                obj.InstrSeq = obj1.TN_MEA1300List.Count == 0 ? 1 : obj1.TN_MEA1300List.Count + 1;
                obj.CheckDate = DateTime.Today;
                obj.InstrNo = obj1.InstrNo;


              
                DetailGridBindingSource.Add(obj);
                obj1.TN_MEA1300List.Add(obj);
                DetailGridBindingSource.MoveLast();
                chechkDate();


            }
        }
        protected override void DeleteDetailRow()
        {
            
            TN_MEA1200 obj1 = MasterGridBindingSource.Current as TN_MEA1200;
            TN_MEA1300 obj = DetailGridBindingSource.Current as TN_MEA1300;

            if (obj != null)
            {
                obj1.TN_MEA1300List.Remove(obj);
                DetailGridBindingSource.RemoveCurrent();
            }
        }

        protected override void DataSave()
        {
            foreach (var rowHandle in gridEx2.MainGrid.MainView.GetSelectedRows())
            {
                string _ProcessCode = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckDate").GetNullToEmpty());
                string _CheckMemo = gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "Memo").GetNullToEmpty();
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