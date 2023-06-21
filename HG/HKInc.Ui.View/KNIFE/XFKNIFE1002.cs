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

namespace HKInc.Ui.View.KNIFE
{
    public partial class XFKNIFE1002 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_KNIFE001> ModelService = (IService<TN_KNIFE001>)ProductionFactory.GetDomainService("TN_KNIFE001");
       
        public XFKNIFE1002()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitControls()
        {
            base.InitControls();
            MasterGridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }
        
        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("KnifeMcode", "관리번호");
            MasterGridExControl.MainGrid.AddColumn("KnifeCode", "칼 코드");
            MasterGridExControl.MainGrid.AddColumn("KnifeName", "칼 명");          
            MasterGridExControl.MainGrid.AddColumn("MakeCust", "거래처");
            MasterGridExControl.MainGrid.AddColumn("StPostion1", "위치1");
            MasterGridExControl.MainGrid.AddColumn("StPostion2", "위치2");
            MasterGridExControl.MainGrid.AddColumn("StPostion3", "위치3");
            MasterGridExControl.MainGrid.AddColumn("Memo", "비고");
            MasterGridExControl.MainGrid.AddColumn("Class", "등급");
            MasterGridExControl.MainGrid.AddColumn("Imgurl", "이미지");

            DetailGridExControl.MainGrid.Init();
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("KnifeMcode", false);
            DetailGridExControl.MainGrid.AddColumn("KnifeCode", false);
            DetailGridExControl.MainGrid.AddColumn("Seq","순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InoutDt", "입출고일");
            DetailGridExControl.MainGrid.AddColumn("StOutpostion1", "출고위치1");
            DetailGridExControl.MainGrid.AddColumn("StOutpostion2", "출고위치1");
            DetailGridExControl.MainGrid.AddColumn("StOutpostion3", "출고위치1");
            DetailGridExControl.MainGrid.AddColumn("StInpostion1", "입고위치1");
            DetailGridExControl.MainGrid.AddColumn("StInpostion2", "입고위치2");
            DetailGridExControl.MainGrid.AddColumn("StInpostion3", "입고위치3");
            DetailGridExControl.MainGrid.AddColumn("InoutId", "입출고자");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InoutDt", "StOutpostion1", "StOutpostion2", "StOutpostion3", "StInpostion1"
                                                                        , "StInpostion2", "StInpostion3", "InoutId");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MakeCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");        
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.MainView.Columns["Imgurl"].ColumnEdit = new HKInc.Service.Controls.FileGridButtonEdit(MasterGridExControl, "Imgurl", "Imgurl");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Class", DbRequesHandler.GetCommCode(MasterCodeSTR.MOLDCLASS), "Mcode", "Codename");

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InoutDt");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StOutpostion1", DbRequesHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StOutpostion2",  DbRequesHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StOutpostion3", DbRequesHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 3), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StInpostion1",  DbRequesHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StInpostion2",  DbRequesHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StInpostion3", DbRequesHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 3), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InoutId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");          
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("KnifeMcode");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string KnifeCodeName = tx_MachineCode.Text;

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.KnifeMcode.Contains(KnifeCodeName) || p.KnifeName.Contains(KnifeCodeName) || p.KnifeCode.Contains(KnifeCodeName))).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_KNIFE001 obj = MasterGridBindingSource.Current as TN_KNIFE001;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.Knife003List.OrderBy(o => o.InoutDt).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();
        }

        protected override void DetailAddRowClicked()
        {
            if (!UserRight.HasEdit) return;
            TN_KNIFE001 obj1 = MasterGridBindingSource.Current as TN_KNIFE001;
            if (obj1 != null)
            {
                TN_KNIFE003 obj = new TN_KNIFE003();
                obj.KnifeMcode = obj1.KnifeMcode;
                obj.KnifeCode = obj1.KnifeCode;
                obj.KnifeName = obj1.KnifeName;
                string sql = "SELECT isnull(max(seq),0)+1 FROM TN_KNIFE003T where KNIFE_MCODE='" + obj1.KnifeMcode + "'";
                obj.Seq = Convert.ToInt32(DbRequesHandler.GetCellValue(sql, 0));
                obj.StOutpostion1 = obj1.StPostion1;
                obj.StOutpostion2 = obj1.StPostion2;
                obj.StOutpostion3 = obj1.StPostion3;
                obj1.Knife003List.Add(obj);
                DetailGridBindingSource.Add(obj);
                DetailGridBindingSource.MoveLast();
                DetailGridExControl.MainGrid.BestFitColumns();
            }
        }
        
        protected override void DeleteDetailRow()
        {
            TN_KNIFE001 obj1 = MasterGridBindingSource.Current as TN_KNIFE001;
            TN_KNIFE003 obj = DetailGridBindingSource.Current as TN_KNIFE003;

            if (obj != null)
            {
                obj1.StPostion1 = obj.StOutpostion1;
                obj1.StPostion2 = obj.StOutpostion2;
                obj1.StPostion3 = obj.StOutpostion3;
                obj1.Knife003List.Remove(obj);
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

        private void MainView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            try
            {
                if (e.Clicks == 1)
                {
                    if (e.Column.Name.ToString() == "Imgurl")
                    {
                        WaitHandler.ShowWait();
                        String filename = gv.GetRowCellValue(e.RowHandle, gv.Columns["Imgurl"]).ToString();
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
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
            finally { WaitHandler.CloseWait(); }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TN_KNIFE001 obj = MasterGridBindingSource.Current as TN_KNIFE001;
            TN_KNIFE003 obj3 = DetailGridBindingSource.Current as TN_KNIFE003;
            if (e.Column.Name == "StInpostion1") { obj.StPostion1 = obj3.StInpostion1; }
            if (e.Column.Name == "StInpostion2") { obj.StPostion2 = obj3.StInpostion2; }
            if (e.Column.Name == "StInpostion3") { obj.StPostion3 = obj3.StInpostion3; }
            MasterGridExControl.BestFitColumns();
        }

    }
}