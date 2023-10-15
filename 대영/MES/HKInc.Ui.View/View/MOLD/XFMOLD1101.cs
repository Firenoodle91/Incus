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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;


namespace HKInc.Ui.View.View.MOLD
{

    //금형이력관리

    public partial class XFMOLD1101 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MOLD1100> ModelService = (IService<TN_MOLD1100>)ProductionFactory.GetDomainService("TN_MOLD1100");

        public XFMOLD1101()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            MasterGridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
        }


        protected override void InitCombo()
        {
            lup_MoldCode.SetDefault(true, "MoldMCode", DataConvert.GetCultureDataFieldName("MoldName"), ModelService.GetChildList<TN_MOLD1100>(p => p.UseYN == "Y").ToList());            
        }

        private void MainView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //GridView gv = sender as GridView;
            //try
            //{

            //    if (e.Clicks == 1)
            //    {
            //        if (e.Column.Name.ToString() == "Imgurl")
            //        {

            //            String filename = gv.GetRowCellValue(e.RowHandle, gv.Columns["Imgurl"]).ToString();


            //            //      byte[] img = FileHandler.FtpImageToByte(filename);
            //            byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + filename);




            //            string[] lfileName = filename.Split('/');
            //            if (lfileName.Length > 1)
            //            {
            //                File.WriteAllBytes(lfileName[lfileName.Length - 1], img);
            //                HKInc.Service.Handler.FileHandler.StartProcess(lfileName[lfileName.Length - 1]);
            //            }
            //            else
            //            {
            //                File.WriteAllBytes(filename, img);
            //                HKInc.Service.Handler.FileHandler.StartProcess(filename);
            //            }
            //        }

            //    }
            //}
            //catch (Exception ex) { }
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {


                object NextCheck = View.GetRowCellValue(e.RowHandle, View.Columns["RealShotcnt"]);
                object checkturn = View.GetRowCellValue(e.RowHandle, View.Columns["CheckShotcnt"]);
                if (NextCheck.GetDecimalNullToZero() != 0)
                {
                    if (NextCheck.GetDecimalNullToZero() >= (checkturn.GetDecimalNullToZero() == 0 ? 0 : (checkturn.GetDecimalNullToZero() / 100 * 80)))
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

            var masterObj = MasterGridBindingSource.Current as TN_MOLD1100;

            var detailObj = DetailGridBindingSource.Current as TN_MOLD1101;

           

            if (e.Column.Name != "EndDate")
            {
                return;
            }
            else
            {
                int checkcycle = Convert.ToInt32(masterObj.CheckCycle.GetNullToZero());

                DateTime transferdate = Convert.ToDateTime(detailObj.EndDate.GetNullToNull());

                if (checkcycle != 0 && transferdate != null)
                {
                    masterObj.NextCheckDate = transferdate.AddMonths(checkcycle.GetIntNullToZero());
                }

            }

            masterObj.RealShotcnt = 0;
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();
            MasterGridExControl.MainGrid.BestFitColumns();
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            
            
            //DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);


            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMCode"), LabelConvert.GetLabelText("MoldMCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCode"), LabelConvert.GetLabelText("MoldCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldName"), LabelConvert.GetLabelText("MoldName"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemCode"), LabelConvert.GetLabelText("ItemCode"),false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMakerCust"), LabelConvert.GetLabelText("MoldMakerCust"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("TransferDate"), LabelConvert.GetLabelText("TransferDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Cavity"), LabelConvert.GetLabelText("Cavity"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MainMachineCode"), LabelConvert.GetLabelText("MainMachineCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldWhCode"), LabelConvert.GetLabelText("MoldWhCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldWhPosition"), LabelConvert.GetLabelText("MoldWhPosition"));
            //GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("StPostion1"), LabelConvert.GetLabelText("StPostion1"));
            //GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("StPostion2"), LabelConvert.GetLabelText("StPostion2"));
            //GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("StPostion3"), LabelConvert.GetLabelText("StPostion3"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("StdShotcnt"), LabelConvert.GetLabelText("StdShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Memo"), LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckCycle"), LabelConvert.GetLabelText("CheckCycle"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("NextCheckDate"), LabelConvert.GetLabelText("NextMoldCheckDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldClass"), LabelConvert.GetLabelText("MoldClass"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("RealShotcnt"), LabelConvert.GetLabelText("RealShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("BaseShotcnt"), LabelConvert.GetLabelText("BaseShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckShotcnt"), LabelConvert.GetLabelText("CheckShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldFileName"), LabelConvert.GetLabelText("MoldFileName"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldFileUrl"), LabelConvert.GetLabelText("MoldFileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCheckFileName"), LabelConvert.GetLabelText("MoldCheckFileName"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCheckFileUrl"), LabelConvert.GetLabelText("MoldCheckFileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("UploadFilePath"), LabelConvert.GetLabelText("UploadFilePath"), false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("DeleteFilePath"), LabelConvert.GetLabelText("DeleteFilePath"), false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("DisuseDate"), LabelConvert.GetLabelText("DisuseDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("UseYN"), LabelConvert.GetLabelText("UseFlag"), HorzAlignment.Center, true);

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("MoldMcode", LabelConvert.GetLabelText("MoldMcode"), false);
            DetailGridExControl.MainGrid.AddColumn("MoldCode", LabelConvert.GetLabelText("MoldCode"), false);
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"));
            DetailGridExControl.MainGrid.AddColumn("ReqType", LabelConvert.GetLabelText("ReqType"));
            DetailGridExControl.MainGrid.AddColumn("StartDate", LabelConvert.GetLabelText("StartDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("EndDate", LabelConvert.GetLabelText("EndDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("FaleNote", LabelConvert.GetLabelText("FaleNote"));
            DetailGridExControl.MainGrid.AddColumn("CommitNote", LabelConvert.GetLabelText("CommitNote"));
            DetailGridExControl.MainGrid.AddColumn("ReqNote", LabelConvert.GetLabelText("ReqNote"));
            DetailGridExControl.MainGrid.AddColumn("CommitId", LabelConvert.GetLabelText("CommitId"));
            DetailGridExControl.MainGrid.AddColumn("MainMachineCode", LabelConvert.GetLabelText("MachineName"));
            DetailGridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));
            DetailGridExControl.MainGrid.AddColumn("StateYn", LabelConvert.GetLabelText("StateYn"));
            DetailGridExControl.MainGrid.AddColumn("StateId", LabelConvert.GetLabelText("StateId"));
            DetailGridExControl.MainGrid.AddColumn("Memo1", LabelConvert.GetLabelText("Memo1"));
            DetailGridExControl.MainGrid.AddColumn("Memo2", LabelConvert.GetLabelText("Memo2"));
            DetailGridExControl.MainGrid.AddColumn("Memo3", LabelConvert.GetLabelText("Memo3"));
            DetailGridExControl.MainGrid.AddColumn("Memo4", LabelConvert.GetLabelText("Memo4"));
            DetailGridExControl.MainGrid.AddColumn("Memo5", LabelConvert.GetLabelText("Memo5"));

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ReqType", "StartDate", "EndDate", "FaleNote", "CommitNote", "ReqNote", "CommitId", "MainMachineCode", "WorkId", "StateYn", "StateId", "Memo1", "Memo2", "Memo3", "Memo4", "Memo5");

        }


        protected override void InitBindingSource()
        {
            base.InitBindingSource();

        }

        protected override void InitRepository()
        {


            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldMakerCust", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldMakercust), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckCycle), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", false);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldClass", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldClass), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.MainView.Columns["MoldFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MoldImage, "MoldFileName", "MoldFileUrl", true);
            MasterGridExControl.MainGrid.MainView.Columns["MoldCheckFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MoldCheckImage, "MoldCheckFileName", "MoldCheckFileUrl", true);
            MasterGridExControl.MainGrid.SetRepositoryItemCodeLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());    // 2021-07-15 김진우 주임 SetRepositoryItemSearchLookUpEdit 에서 SetRepositoryItemCodeLookUpEdit 로 변경




            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReqType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldReqType), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.MainView.Columns["FaleNote"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "FaleNote", UserRight.HasEdit);
            DetailGridExControl.MainGrid.MainView.Columns["CommitNote"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "CommitNote", UserRight.HasEdit);
            DetailGridExControl.MainGrid.MainView.Columns["ReqNote"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "ReqNote", UserRight.HasEdit);
            DetailGridExControl.MainGrid.MainView.Columns["StateYn"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "StateYn", UserRight.HasEdit);
            DetailGridExControl.MainGrid.MainView.Columns["Memo1"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo1", UserRight.HasEdit);
            DetailGridExControl.MainGrid.MainView.Columns["Memo2"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo1", UserRight.HasEdit);
            DetailGridExControl.MainGrid.MainView.Columns["Memo3"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo1", UserRight.HasEdit);
            DetailGridExControl.MainGrid.MainView.Columns["Memo4"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo1", UserRight.HasEdit);
            DetailGridExControl.MainGrid.MainView.Columns["Memo5"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo1", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("MainMachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineCode", "MachineName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StateId", ModelService.GetChildList<User>(p => p.ScmYn == "N" && p.Active == "Y"), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => p.ScmYn == "N" && p.Active == "Y"), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CommitId", ModelService.GetChildList<User>(p => p.ScmYn == "N" && p.Active == "Y"), "LoginId", "UserName");


            ////주기
            //MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("CheckCycle", DbRequestHandler.GetCommCode(MasterCodeSTR.CHECKCYCLE).ToList(), "Mcode", "Codename");



            //MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("NextCheckDate");
            //MasterGridExControl.MainGrid.MainView.Columns["Imgurl"].ColumnEdit = new HKInc.Service.Controls.FileGridButtonEdit(gridEx1, "Imgurl", "Imgurl");
            //DetailGridExControl.MainGrid.MainView.Columns["FaleNote"].Width = 100;
            //DetailGridExControl.MainGrid.MainView.Columns["CommitNote"].Width = 100;
            //DetailGridExControl.MainGrid.MainView.Columns["ReqNote"].Width = 100;
            //DetailGridExControl.MainGrid.MainView.Columns["Memo1"].Width = 100;

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }


        protected override void DataLoad()
        {

            
            GridRowLocator.GetCurrentRow();
            DetailGridRowLocator.GetCurrentRow("MoldMcode");

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            MasterGridExControl.DataSource = null;
            DetailGridExControl.DataSource = null;
            var MoldMCode = lup_MoldCode.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(MoldMCode) ? true : p.MoldMCode == MoldMCode)
                                                             && (p.UseYN == "Y"))
                                                            .OrderBy(p => p.MoldMCode)
                                                          .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }


        protected override void MasterFocusedRowChanged()
        {

            TN_MOLD1100 obj = MasterGridBindingSource.Current as TN_MOLD1100;
            DetailGridBindingSource.DataSource = obj.TN_MOLD1101List.OrderBy(o => o.StartDate).ToList();//.GetList(p => (p.MachineCode == obj.MachineCode)).OrderBy(p => p.MachineSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

        }
        protected override void DetailAddRowClicked()
        {
            //base.DetailAddRowClicked();
            if (!UserRight.HasEdit) return;
            TN_MOLD1100 MasterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            if (MasterObj != null)
            {
                TN_MOLD1101 newobj = new TN_MOLD1101();
                newobj.MoldMCode = MasterObj.MoldMCode;
                newobj.MoldCode = MasterObj.MoldCode;
                //string sql = "SELECT isnull(max(seq),0)+1 FROM [TN_MOLD002T] where MOLD_MCODE='" + obj1.MoldMcode + "'";
                //obj.Seq = Convert.ToInt32(DbRequestHandler.GetCellValue(sql, 0));
                newobj.Seq = MasterObj.TN_MOLD1101List.Count == 0 ? 1 : MasterObj.TN_MOLD1101List.Max(o => o.Seq) + 1;
                newobj.RealShotcnt = MasterObj.RealShotcnt.GetDoubleNullToZero();
                newobj.ShotCnt = MasterObj.SumShotcnt.GetDoubleNullToZero();

                DetailGridBindingSource.Add(newobj);
                MasterObj.TN_MOLD1101List.Add(newobj);
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

            TN_MOLD1100 MasterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            TN_MOLD1101 delObj = DetailGridBindingSource.Current as TN_MOLD1101;

            if (MasterObj != null)
            {
                MasterObj.TN_MOLD1101List.Remove(delObj);
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

            DetailGridExControl.MainGrid.PostEditor();
            ModelService.Save();
            DataLoad();
        }
    }
}