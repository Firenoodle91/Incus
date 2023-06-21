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
using HKInc.Service.Handler;
using System.IO;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.MEA
{
    public partial class XFMEA1500 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MOLD001> ModelService = (IService<TN_MOLD001>)ProductionFactory.GetDomainService("TN_MOLD001");
       
        public XFMEA1500()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            MasterGridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
        }

        private void MainView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            try
            {

                if (e.Clicks == 1)
                {
                    if (e.Column.Name.ToString() == "Imgurl")
                    {

                        String filename = gv.GetRowCellValue(e.RowHandle, gv.Columns["Imgurl"]).ToString();


                        //      byte[] img = FileHandler.FtpImageToByte(filename);
                        byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + filename);




                        string[] lfileName = filename.Split('/');
                        if (lfileName.Length > 1)
                        {
                            File.WriteAllBytes(lfileName[lfileName.Length - 1], img);
                            HKInc.Service.Handler.FileHandler.StartProcess(lfileName[lfileName.Length - 1]);
                        }
                        else
                        {
                            File.WriteAllBytes(filename, img);
                            HKInc.Service.Handler.FileHandler.StartProcess(filename);
                        }
                    }

                }
            }
            catch (Exception ex) { }
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {

               
                object NextCheck = View.GetRowCellValue(e.RowHandle, View.Columns["RealShotcnt"]);
                object checkturn= View.GetRowCellValue(e.RowHandle, View.Columns["CheckPoint"]);
                if (NextCheck.GetDecimalNullToZero() != 0){
                    if (NextCheck.GetDecimalNullToZero() >=(checkturn.GetDecimalNullToZero()==0?0:(checkturn.GetDecimalNullToZero()/100*80)))
                    {
                        e.Appearance.BackColor = Color.Yellow;
                        e.Appearance.ForeColor = Color.Black;
                    }
                    if (NextCheck.GetDecimalNullToZero() >= (checkturn.GetDecimalNullToZero() == 0 ? 0 : (checkturn.GetDecimalNullToZero() / 100 * 90)))
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
            if (e.Column.Name != "EndDate") return;
            TN_MOLD001 tn = MasterGridBindingSource.Current as TN_MOLD001;
            TN_MOLD002 tn1 = DetailGridBindingSource.Current as TN_MOLD002;
            tn.RealShotcnt = 0;
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            //     DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("MoldMcode", "관리번호");
            MasterGridExControl.MainGrid.AddColumn("MoldCode", "금형코드");
            MasterGridExControl.MainGrid.AddColumn("MoldName", "금형명");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "제품코드");
            MasterGridExControl.MainGrid.AddColumn("MoldMakecust", "제작처");
            MasterGridExControl.MainGrid.AddColumn("InputDt", "이관일");
            MasterGridExControl.MainGrid.AddColumn("MastMc", "메인설비");
            MasterGridExControl.MainGrid.AddColumn("XCase", "캐비티");
            MasterGridExControl.MainGrid.AddColumn("Memo", "비고");
            MasterGridExControl.MainGrid.AddColumn("RealShotcnt", "현재타발수", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("SumShotcnt", "누적타발수", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("CheckPoint", "점검기준", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("CheckCycle", "점검주기");
            MasterGridExControl.MainGrid.AddColumn("NextCheckDate", "다음점검일");
            MasterGridExControl.MainGrid.AddColumn("MoldClass", "등급");
            MasterGridExControl.MainGrid.AddColumn("Imgurl", "이미지");


            DetailGridExControl.MainGrid.AddColumn("MoldMcode",false);
            DetailGridExControl.MainGrid.AddColumn("MoldCode",false);
            DetailGridExControl.MainGrid.AddColumn("Seq","순번");
            DetailGridExControl.MainGrid.AddColumn("ReqType","구분");
            DetailGridExControl.MainGrid.AddColumn("StartDate","작업시작일");
            DetailGridExControl.MainGrid.AddColumn("EndDate","작업종료일");
            DetailGridExControl.MainGrid.AddColumn("FaleNote","문제사항");
            DetailGridExControl.MainGrid.AddColumn("CommitNote","수리내역");
            DetailGridExControl.MainGrid.AddColumn("ReqNote","수리결과");
            DetailGridExControl.MainGrid.AddColumn("Commitucser","확인자");
            DetailGridExControl.MainGrid.AddColumn("ShotCnt","누적샷수", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("RealShotcnt","현재샷수", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("McCode","설비");
            DetailGridExControl.MainGrid.AddColumn("Worker","작업자");
            DetailGridExControl.MainGrid.AddColumn("StateYn","이상유무판정");
            DetailGridExControl.MainGrid.AddColumn("StateUser","판정자");
            DetailGridExControl.MainGrid.AddColumn("Memo1","비고1");
            DetailGridExControl.MainGrid.AddColumn("Memo2","비고2");
            DetailGridExControl.MainGrid.AddColumn("Memo3","비고3");
            DetailGridExControl.MainGrid.AddColumn("Memo4","비고4");
            DetailGridExControl.MainGrid.AddColumn("Memo5","비고5");

           


            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ReqType", "StartDate", "EndDate"
               ,"FaleNote"
               ,"CommitNote"
               ,"ReqNote"
               ,"Commitucser"
               ,"ShotCnt"
               ,"RealShotcnt"
               ,"McCode"
               ,"Worker"
               ,"StateYn"
               ,"StateUser"
               ,"Memo1"
               ,"Memo2"
               ,"Memo3"
               ,"Memo4"
               ,"Memo5"
                );





        }
        protected override void InitBindingSource()
        {
            base.InitBindingSource();

        }

        protected override void InitRepository()
        {
            
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InputDt");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("NextCheckDate");
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("MoldMakecust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("CheckCycle", DbRequestHandler.GetCommCode(MasterCodeSTR.CHECKCYCLE).ToList(), "Mcode", "Codename");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.MainView.Columns["Imgurl"].ColumnEdit = new HKInc.Service.Controls.FileGridButtonEdit(gridEx1, "Imgurl", "Imgurl");
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("MastMc", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("StartDate");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("EndDate");
            DetailGridExControl.MainGrid.MainView.Columns["FaleNote"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["FaleNote"].Width = 100;
            DetailGridExControl.MainGrid.MainView.Columns["CommitNote"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["CommitNote"].Width = 100;
            DetailGridExControl.MainGrid.MainView.Columns["ReqNote"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["ReqNote"].Width = 100;
            DetailGridExControl.MainGrid.MainView.Columns["Memo1"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["Memo1"].Width = 100;
            DetailGridExControl.MainGrid.MainView.Columns["Memo2"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["Memo2"].Width = 100;
            DetailGridExControl.MainGrid.MainView.Columns["Memo3"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["Memo3"].Width = 100;
            DetailGridExControl.MainGrid.MainView.Columns["Memo4"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["Memo4"].Width = 100;
            DetailGridExControl.MainGrid.MainView.Columns["Memo5"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["Memo5"].Width = 100;
            DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("ReqType", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDReqType).ToList(), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("McCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StateUser", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Worker", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Commitucser", ModelService.GetChildList<UserView>(p => p.Active=="Y"), "LoginId", "UserName");
        }
        protected override void DataLoad()
        {

            GridRowLocator.GetCurrentRow();
            DetailGridRowLocator.GetCurrentRow("MoldMcode");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
         

         
            string lMachinecode = tx_MachineCode.Text;
          
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(lMachinecode) ? true : (p.MoldMcode.Contains(lMachinecode) || p.MoldCode.Contains(lMachinecode)))).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }
        protected override void MasterFocusedRowChanged()
        {

            TN_MOLD001 obj = MasterGridBindingSource.Current as TN_MOLD001;
            DetailGridBindingSource.DataSource = obj.Mold002List.OrderBy(o=>o.StartDate).ToList();//.GetList(p => (p.MachineCode == obj.MachineCode)).OrderBy(p => p.MachineSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

        }
        protected override void DetailAddRowClicked()
        {
            //    base.DetailAddRowClicked();
            if (!UserRight.HasEdit) return;
            TN_MOLD001 obj1 = MasterGridBindingSource.Current as TN_MOLD001;
            if (obj1 != null)
            {
                TN_MOLD002 obj = new TN_MOLD002();
                obj.MoldMcode = obj1.MoldMcode;
                obj.MoldCode = obj1.MoldCode;
                string sql= "SELECT isnull(max(seq),0)+1 FROM [TN_MOLD002T] where MOLD_MCODE='" + obj1.MoldMcode + "'";
                obj.Seq= Convert.ToInt32(DbRequestHandler.GetCellValue(sql,0));
                obj.RealShotcnt = obj1.RealShotcnt.GetDoubleNullToZero();
                obj.ShotCnt = obj1.SumShotcnt.GetDoubleNullToZero();

                DetailGridBindingSource.Add(obj);
                obj1.Mold002List.Add(obj);
                DetailGridBindingSource.MoveLast();
              
            }
            //TN_MOLD001 tn = MasterGridBindingSource.Current as TN_MOLD001;
            //TN_MOLD002 tn1 = DetailGridBindingSource.Current as TN_MOLD002;
            //DateTime dt = Convert.ToDateTime(tn1.EndDate);
            //switch (tn.CheckCycle)
            //{
            //    case "18":
            //        tn.NextCheckDate = dt.AddDays(1);
            //        break;
            //    case "19":
            //        tn.NextCheckDate = dt.AddDays(7);
            //        break;
            //    case "20":
            //        tn.NextCheckDate = dt.AddMonths(1);
            //        break;
            //    case "21":
            //        tn.NextCheckDate = dt.AddYears(1);
            //        break;
            //    case "22":
            //        tn.NextCheckDate = dt.AddMonths(2);
            //        break;
            //    case "23":
            //        tn.NextCheckDate = dt.AddMonths(3);
            //        break;
            //    case "24":
            //        tn.NextCheckDate = dt.AddMonths(4);
            //        break;
            //    case "25":
            //        tn.NextCheckDate = dt.AddMonths(5);
            //        break;
            //    case "26":
            //        tn.NextCheckDate = dt.AddMonths(6);
            //        break;

            //}
            MasterGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DeleteDetailRow()
        {

            TN_MOLD001 obj1 = MasterGridBindingSource.Current as TN_MOLD001;
            TN_MOLD002 obj = DetailGridBindingSource.Current as TN_MOLD002;

            if (obj != null)
            {
                obj1.Mold002List.Remove(obj);
                DetailGridBindingSource.RemoveCurrent();
            }
        }

        protected override void DataSave()
        {
            //foreach (var rowHandle in gridEx2.MainGrid.MainView.GetSelectedRows())
            //{
            //    string _ProcessCode = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckDate").GetNullToEmpty());
            //    string _CheckMemo = gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckMemo").GetNullToEmpty();
            //    string _CheckId = gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckId").GetNullToEmpty();

            //    if (_ProcessCode == null || _ProcessCode == "")
            //    {
            //        HKInc.Service.Handler.MessageBoxHandler.Show("이력리스트" + Convert.ToInt32(rowHandle + 1) + "행의 점검일은 필수입력 사항입니다.");
            //        return;
            //    }

            //    if (_CheckMemo == null || _CheckMemo == "")
            //    {
            //        HKInc.Service.Handler.MessageBoxHandler.Show("이력리스트 " + Convert.ToInt32(rowHandle + 1) + "행의 점검내용은 필수입력 사항입니다.");
            //        return;
            //    }

            //    if (_CheckId == null || _CheckId == "")
            //    {
            //        HKInc.Service.Handler.MessageBoxHandler.Show("이력리스트 " + Convert.ToInt32(rowHandle + 1) + "행의 담당자는 필수입력 사항입니다.");
            //        return;
            //    }

            //}

            base.DataSave();
            ModelService.Save();
            DataLoad();
        }
    }
}