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
    /// 계측기점검관리화면
    /// </summary>
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

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("InstrNo", "계측기코드");
            MasterGridExControl.MainGrid.AddColumn("InstrNm", "계측기명");
            MasterGridExControl.MainGrid.AddColumn("Maker", "제작사");
            MasterGridExControl.MainGrid.AddColumn("Spec", "규격");
            MasterGridExControl.MainGrid.AddColumn("PurcDate", "설치일자");
            MasterGridExControl.MainGrid.AddColumn("SerialNo", "S/N");
            MasterGridExControl.MainGrid.AddColumn("CorTurn", "검교정주기");
            MasterGridExControl.MainGrid.AddColumn("CorDate", "검교정일");
            MasterGridExControl.MainGrid.AddColumn("NxcorDate", "다음검교정일");
            MasterGridExControl.MainGrid.AddColumn("UseYn", false);

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("InstrSeq", false);
            DetailGridExControl.MainGrid.AddColumn("CheckDate", "점검일자", DevExpress.Utils.HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("InstrNo", false);
            DetailGridExControl.MainGrid.AddColumn("CheckGu","점검구분");
            DetailGridExControl.MainGrid.AddColumn("Memo", "점검내용");
            DetailGridExControl.MainGrid.AddColumn("CheckId", "점검자");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckDate", "CheckGu", "Memo", "CheckId");
            DetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "CheckDate", "CheckGu", "Memo", "CheckId");
        }


        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("CorDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("NxcorDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PurcDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequesHandler.GetCommCode(MasterCodeSTR.TOOLMAKER), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CorTurn", MasterCode.GetMasterCode((int)MasterCodeEnum.CheckTurn).ToList());

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("CheckDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].Width = 150;
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<UserView>(p => p.Active=="Y"), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckGu", DbRequesHandler.GetCommCode(MasterCodeSTR.CheckGu), "Mcode", "Codename");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InstrNo");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string lMachinecode = tx_MachineCode.Text;
            if (checkEdit1.Checked == true)
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(lMachinecode) ? true : (p.InstrNm.Contains(lMachinecode) || p.InstrNo == lMachinecode))).ToList();
            }
            else
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.UseYn=="Y") && (string.IsNullOrEmpty(lMachinecode) ? true : (p.InstrNm.Contains(lMachinecode) || p.InstrNo == lMachinecode))).ToList();
            }
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {

            TN_MEA1200 obj = MasterGridBindingSource.Current as TN_MEA1200;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.TN_MEA1300List.OrderBy(o => o.CheckDate).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();
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

        protected override void DetailAddRowClicked()
        {
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
            }
        }

        protected override void DeleteDetailRow()
        {            
            TN_MEA1200 obj1 = MasterGridBindingSource.Current as TN_MEA1200;
            if (obj1 == null) return;
            TN_MEA1300 obj = DetailGridBindingSource.Current as TN_MEA1300;

            if (obj != null)
            {
                obj1.TN_MEA1300List.Remove(obj);
                DetailGridBindingSource.RemoveCurrent();
            }
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {                
                if (View.GetRowCellValue(e.RowHandle, View.Columns["PurcDate"]) == null) return;
                DateTime InstallDate = Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, View.Columns["PurcDate"]));
                object NextCheck = View.GetRowCellValue(e.RowHandle, View.Columns["NxcorDate"]);
                object checkturn = View.GetRowCellValue(e.RowHandle, View.Columns["CorTurn"]);
                object corDate = View.GetRowCellValue(e.RowHandle, View.Columns["CorDate"]);
                if (corDate == null)
                {
                    View.SetRowCellValue(e.RowHandle, View.Columns["CorDate"], InstallDate);
                }
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
            DateTime dt = Convert.ToDateTime(tn1.CheckDate);
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
            if (e.Column.Name == "CheckGu")
            {
                if (tn1.CheckGu == "A03")
                {
                    tn.UseYn = "N";
                }
                else
                {
                    tn.UseYn = "Y";
                }
            }
            MasterGridExControl.MainGrid.BestFitColumns();
        }

    }
}