using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

using DevExpress.XtraBars;

using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Interface.Handler;
using HKInc.Utils.Interface.Forms;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using HKInc.Service.Forms;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Service;
using DevExpress.Utils;
using System.IO;

namespace HKInc.Ui.View.POP
{
    public partial class XFPOP003 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1401> ModelService = (IService<TN_MPS1401>)ProductionFactory.GetDomainService("TN_MPS1401");
        private string srccode;
        private string proc;
        public XFPOP003()
        {
            InitializeComponent();
            inicombo();
            SetToolbarVisible(false);
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            GridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            btn_state(0);
          //  lup_Process.settext = GlobalVariable.ProcessCode;
            //byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + "logo.jpg");
            //pictureEdit3.EditValue = img;

        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                object NextCheck = View.GetRowCellValue(e.RowHandle, View.Columns["EMType"]);
                if (NextCheck.GetNullToEmpty() == "Y")
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
            }
            if (e.RowHandle == View.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.Blue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        //  protected override void InitCombo()
        private void inicombo()
        {
         //   luptem.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.tem));
            lup_Process.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.Process));
           
            lup_Item.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList());
            // luptem.EditValue = DbRequesHandler.GetCellValue("SELECT [Property8]  FROM [VI_USER] where LoginId='"+ HKInc.Ui.Model.BaseDomain.GsValue.UserId + "'", 0);
            //lupWork.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
            lup_Process.SelectedIndex = GlobalVariable.ProcessCode;
            proc = lup_Process.EditValue.GetNullToEmpty();
            lup_Mc.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y" && (string.IsNullOrEmpty(proc)?true:p.Temp == proc)).ToList());
        }
        protected override void GridRowDoubleClicked()
        {
           
        }
        protected override void InitGrid()
        {
            GridExControl.MainGrid.MainView.RowHeight = 50;
            GridExControl.MainGrid.SetGridFont(this.GridExControl.MainGrid.MainView, new Font(DefaultFont.FontFamily, 11, FontStyle.Bold));
            GridExControl.MainGrid.MainView.ColumnPanelRowHeight = 50;
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("JobStatus", "состояние");
            GridExControl.MainGrid.AddColumn("EMType", "срочность");
            GridExControl.MainGrid.AddColumn("WorkDate", "Дата обучения");
            GridExControl.MainGrid.AddColumn("WorkNo", false);
            GridExControl.MainGrid.AddColumn("Cust", "Клиенты");
            GridExControl.MainGrid.AddColumn("ItemCode", "Предметы");
            GridExControl.MainGrid.AddColumn("ItemCode1", "спецификации");
            GridExControl.MainGrid.AddColumn("Process", "процесс");
            GridExControl.MainGrid.AddColumn("MachineCode", "оборудование");
       
            GridExControl.MainGrid.AddColumn("PlanQty", "Указав количество", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Memo", "напоминание");
            GridExControl.MainGrid.AddColumn("WorkStantadNm", false);
            GridExControl.MainGrid.AddColumn("FileData", false);
            //GridExControl.MainGrid.AddColumn("DesignFile", false);
            //GridExControl.MainGrid.AddColumn("DesignMap", false);
            GridExControl.MainGrid.BestFitColumns();

        }

        protected override void InitRepository()
        {


            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Process", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");

             GridExControl.MainGrid.SetRepositoryItemCheckEdit("EMType", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Cust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag=="Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "Spec1");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("JobStatus", MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList());
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataLoad();
            GridExControl.MainGrid.BestFitColumns();
        }

        private void DataLoad()
        {
            if (checkEdit1.EditValue.ToString() == "Y")
            {
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {


                    var process = new SqlParameter("@process", lup_Process.EditValue.GetNullToEmpty());
                    var mccode = new SqlParameter("@mccode", lup_Mc.EditValue.GetNullToEmpty());
                    var itemcode = new SqlParameter("@itemcode", lup_Item.EditValue.GetNullToEmpty());
                    var wkno = new SqlParameter("@wkpno", tx_workno.EditValue.GetNullToEmpty());
                    var tem = new SqlParameter("@tem", "");
                    var result = context.Database
                          .SqlQuery<TP_POPJOBLIST>("SP_POP_JOBLIST_NEW1 @process,@mccode ,@itemcode ,@wkpno,@tem", process, mccode, itemcode, wkno, tem).OrderBy(p => p.WorkNo).ToList();
                    GridBindingSource.DataSource = result.OrderBy(o => o.PSeq).OrderBy(o1 => o1.Eord).ToList();

                }
            }
            else
            {
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {


                    var process = new SqlParameter("@process", lup_Process.EditValue.GetNullToEmpty());
                    var mccode = new SqlParameter("@mccode", lup_Mc.EditValue.GetNullToEmpty());
                    var itemcode = new SqlParameter("@itemcode", lup_Item.EditValue.GetNullToEmpty());
                    var wkno = new SqlParameter("@wkpno", tx_workno.EditValue.GetNullToEmpty());
                    var tem = new SqlParameter("@tem", "");
                    var result = context.Database
                          .SqlQuery<TP_POPJOBLIST>("SP_POP_JOBLIST_NEW @process,@mccode ,@itemcode ,@wkpno,@tem", process, mccode, itemcode, wkno, tem).OrderBy(p => p.WorkNo).ToList();
                    GridBindingSource.DataSource = result.OrderBy(o => o.PSeq).OrderBy(o1 => o1.Eord).ToList();

                }
            }
            GridExControl.DataSource = GridBindingSource;
            GridExControl.MainGrid.BestFitColumns();
            rowchange();
            timer1.Start();
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            rowchange();
        }

        private void rowchange()
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            tx_lotno.Text = "";
            tx_makeqty.Text = "0";
            tx_okqty.Text = "0";
            tx_badqty.Text = "0";
            tx_totok.Text = "0";
            if (obj == null) return;
           
            btn_state(Convert.ToInt32(obj.JobStatus));
            ModelService.ReLoad();
        
            TN_MPS1401 tn1401 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.Process ).OrderBy(o => o.Seq).LastOrDefault();
            //TN_MPS1401 tn1401 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.Process && p.ResultDate == DateTime.Today).OrderBy(o => o.Seq).LastOrDefault();
            //            TN_MPS1401 tn1401 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.Process && p.LotNo==tx_lotno.Text).OrderBy(o => o.Seq).LastOrDefault();
            if (tn1401 != null)
            {
                tx_lotno.Text = tn1401.LotNo;
                string sql = "SELECT isnull(sum(isnull([OK_QTY],0)),0) OkQty  ,isnull(sum(isnull([RESULT_QTY],0)),0) ResultQty   ,isnull(sum(isnull([FAIL_QTY],0)),0) FailQty   "
                   + "  FROM [TN_MPS1405T] where RESULT_DATE = '" + DateTime.Today.ToString("yyyy-MM-dd") + "' and work_no='"+obj.WorkNo+"' and process_code='" + obj.Process + "'";
                DataSet ds = DbRequesHandler.GetDataQury(sql);
                //tx_makeqty.Text = tn1401.ResultQty.ToString();
                //tx_okqty.Text = tn1401.OkQty.ToString();
                //tx_badqty.Text = tn1401.FailQty.ToString();
                if (ds != null)
                {
                    tx_makeqty.Text = ds.Tables[0].Rows[0][1].ToString();
                    tx_okqty.Text = ds.Tables[0].Rows[0][0].ToString();
                    tx_badqty.Text = ds.Tables[0].Rows[0][2].ToString();
                }
                else {
                    tx_makeqty.Text = "0";
                    tx_okqty.Text = "0";
                    tx_badqty.Text = "0";

                }
            }
            else
            {
                tx_lotno.Text = "";
                tx_makeqty.Text = "0";
                tx_okqty.Text = "0";
                tx_badqty.Text = "0";
            }
            string sql1 = "SELECT isnull(sum(isnull([RESULT_QTY],0)),0) ResultQty   "
              + "  FROM [TN_MPS1405T] where RESULT_DATE = '" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + "' and work_no='" + obj.WorkNo + "' and process_code='" + obj.Process + "'";
            DataSet ds1 = DbRequesHandler.GetDataQury(sql1);
            tx_preqty.Text = Convert.ToString(ds1.Tables[0].Rows[0][0].GetIntNullToZero());
            tx_totok.Text = DbRequesHandler.GetCellValue("SELECT sum([OK_QTY]) qty FROM TN_MPS1401T where WORK_NO='" + obj.WorkNo + "' and PROCESS_CODE='" + obj.Process + "' ", 0).ToString();
            ////if (obj.JobStatus == "33" || obj.JobStatus == "35")
            ////{
            ////    if (tx_lotno.Text == "")
            ////    {
            ////        fload();
            ////    }

            ////}

            String filename = obj.WorkStantadNm.GetNullToEmpty();

            if (filename == "")
            { pe_jobstd.EditValue = null; }
            else
            {

                byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + filename);

                pe_jobstd.EditValue = null;
                pe_jobstd.EditValue = img;
            }
            //pe_domap.EditValue = null;
            //pe_domap.EditValue = obj.DesignMap;

            var DomapObj = ModelService.GetChildList<TN_STD1600>(p => p.ItemCode == obj.ItemCode).OrderBy(p => p.Seq).LastOrDefault();
            pe_domap.EditValue = DomapObj == null ? null : DomapObj.DesignMap;
        }

        private void pe_jobstd_Click(object sender, EventArgs e)
        {
            //if (pe_jobstd.EditValue == null) return;
            //POP_Popup.XPFPOPIMG fm = new POP_Popup.XPFPOPIMG("작업표준서", pe_jobstd.EditValue);
            //fm.ShowDialog();
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            String filename = obj.WorkStantadNm.GetNullToEmpty();
            if (filename != "")
            {
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

        private void pe_domap_DoubleClick(object sender, EventArgs e)
        {
            //if (pe_domap.EditValue == null) return;
            //POP_Popup.XPFPOPIMG fm = new POP_Popup.XPFPOPIMG("도면", pe_domap.EditValue);
            //fm.ShowDialog();
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            var DomapObj = ModelService.GetChildList<TN_STD1600>(p => p.ItemCode == obj.ItemCode).OrderBy(p => p.Seq).LastOrDefault();
            if (DomapObj == null) return;
            String filename = DomapObj.DesignFile.GetNullToEmpty();
            File.WriteAllBytes(filename, DomapObj.DesignMap);
        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            int irow = gv.FocusedRowHandle;
            irow++;
            gv.FocusedRowHandle = irow;

        }

        private void p_UP_Click(object sender, EventArgs e)
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            int irow = gv.FocusedRowHandle;
            irow--;
            gv.FocusedRowHandle = irow;
        }
        private void btn_state(int job)
        {

            switch (job)
            {
                case (int)MasterCodeEnum.POP_Status_Wait://대기
                    btn_start.Enabled = true;
                    btn_srcchange.Enabled = false;
                    btn_qtyin.Enabled = false;
                    btn_qcin.Enabled = false;
                    btn_stopin.Enabled = false;
                    btn_moveprt.Enabled = false;
                    btn_end.Enabled = true;
                    btn_exit.Enabled = true;

                    break;
                case (int)MasterCodeEnum.POP_Status_Start://생산중
                    btn_start.Enabled = false;
                    btn_srcchange.Enabled = true;
                    btn_qtyin.Enabled = true;
                    btn_qcin.Enabled = true;
                    btn_stopin.Enabled = false;
                    btn_moveprt.Enabled = true;
                    btn_end.Enabled = true;
                    btn_exit.Enabled = true;

                    break;
                case (int)MasterCodeEnum.POP_Status_StopWait://일시중지
                    btn_start.Enabled = true;
                    btn_srcchange.Enabled = false;
                    btn_qtyin.Enabled = false;
                    btn_qcin.Enabled = false;
                    btn_stopin.Enabled = false;
                    btn_moveprt.Enabled = false;
                    btn_end.Enabled = false;
                    btn_exit.Enabled = true;

                    break;
                case (int)MasterCodeEnum.POP_Status_Stop://비가동
                    btn_start.Enabled = true;
                    btn_srcchange.Enabled = false;
                    btn_qtyin.Enabled = false;
                    btn_qcin.Enabled = false;
                    btn_stopin.Enabled = false;
                    btn_moveprt.Enabled = false;
                    btn_end.Enabled = false;
                    btn_exit.Enabled = true;

                    break;
                case (int)MasterCodeEnum.POP_Status_End://작업종료
                    btn_start.Enabled = false;
                    btn_srcchange.Enabled = false;
                    btn_qtyin.Enabled = false;
                    btn_qcin.Enabled = false;
                    btn_stopin.Enabled = false;
                    btn_moveprt.Enabled = false;
                    btn_end.Enabled = false;
                    btn_exit.Enabled = true;
                    break;
                default:
                    btn_start.Enabled = false;
                    btn_srcchange.Enabled = false;
                    btn_qtyin.Enabled = false;
                    btn_qcin.Enabled = false;
                    btn_stopin.Enabled = false;
                    btn_moveprt.Enabled = false;
                    btn_end.Enabled = false;
                    btn_exit.Enabled = true;
                    break;
            }

     

        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            jobstart();
        }

        private void jobstart()
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            //obj.JobStatus = "33";
            //DataSet ds = DbRequesHandler.GetDataQury("exec SP_JOBSTATUS_UP '" + obj.WorkNo + "','" + obj.PSeq + "','" + obj.Process + "','" + obj.ItemCode + "','" + obj.JobStatus + "'");
            //btn_state(Convert.ToInt32(obj.JobStatus));

            if (obj.PSeq == 1)
            {
                if (obj.JobStatus == ((int)MasterCodeEnum.POP_Status_Wait).ToString())
                {
                    POP_Popup1.XPFSRCIN fm = new POP_Popup1.XPFSRCIN();
                    fm.ShowDialog();
                    if (fm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        DataSet ds1 = DbRequesHandler.GetDataQury("exec SP_LOTMAKE1 @workno='" + obj.WorkNo + "',@mccode='" + obj.MachineCode + "',@srccode='" + fm.returnval[0] + "',@item='" + obj.ItemCode + "',@date='" + DateTime.Today.ToString("yyyyMMdd") + "',@srclot='" + fm.returnval[1] + "'");
                        tx_lotno.Text = ds1.Tables[0].Rows[0][0].ToString();

                        int cnt = DbRequesHandler.GetRowCount("select count(*) from tn_mps1401t where work_no='" + obj.WorkNo + "' and process_code='" + obj.Process + "'");
                        TN_MPS1401 newobj = new TN_MPS1401();
                        newobj.WorkDate = obj.WorkDate;
                        newobj.WorkNo = obj.WorkNo;
                        newobj.Seq = cnt == 0 ? 1 : cnt + 1;
                        newobj.ProcessCode = obj.Process;
                        newobj.LotNo = tx_lotno.Text;
                        newobj.ProcessTurn = obj.PSeq;
                        newobj.OrderQty = obj.PlanQty;
                        newobj.ResultDate = DateTime.Today.Date;
                        newobj.StartDate = DateTime.Now;
                        ModelService.Insert(newobj);
                        ModelService.Save();
                        obj.JobStatus = ((int)MasterCodeEnum.POP_Status_Start).ToString();
                    }
                }
                else if (obj.JobStatus == ((int)MasterCodeEnum.POP_Status_StopWait).ToString() || obj.JobStatus == ((int)MasterCodeEnum.POP_Status_Stop).ToString())
                {
                    DataSet ds1 = DbRequesHandler.GetDataQury("exec SP_LOTMAKE1 @workno='" + obj.WorkNo + "',@date='" + DateTime.Today.ToString("yyyyMMdd") + "'");
                    if (ds1!=null)
                    {

                        tx_lotno.Text = ds1.Tables[0].Rows[0][0].ToString();
                        int cnt1 = DbRequesHandler.GetRowCount("select count(*) From tn_mps1401t where work_no='" + obj.WorkNo + "' and process_code='" + obj.Process + "' and lot_no='" + tx_lotno.Text + "'");
                        int cnt = DbRequesHandler.GetRowCount("select count(*) from tn_mps1401t where work_no='" + obj.WorkNo + "' and process_code='" + obj.Process + "'");
                        if (cnt1 == 0)
                        {
                            TN_MPS1401 newobj = new TN_MPS1401();
                            newobj.WorkDate = obj.WorkDate;
                            newobj.WorkNo = obj.WorkNo;
                            newobj.Seq = cnt == 0 ? 1 : cnt + 1;
                            newobj.ProcessCode = obj.Process;
                            newobj.LotNo = tx_lotno.Text;
                            newobj.ProcessTurn = obj.PSeq;
                            newobj.OrderQty = obj.PlanQty;
                            newobj.ResultDate = DateTime.Today.Date;
                            newobj.StartDate = DateTime.Now;
                            ModelService.Insert(newobj);
                            ModelService.Save();
                        }
                        obj.JobStatus = ((int)MasterCodeEnum.POP_Status_Start).ToString();
                    }
                    else
                    {
                        POP_Popup1.XPFSRCIN fm = new POP_Popup1.XPFSRCIN();
                        fm.ShowDialog();
                        if (fm.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            DataSet ds2 = DbRequesHandler.GetDataQury("exec SP_LOTMAKE1 @workno='" + obj.WorkNo + "',@mccode='" + obj.MachineCode + "',@srccode='" + fm.returnval[0] + "',@item='" + obj.ItemCode + "',@date='" + DateTime.Today.ToString("yyyyMMdd") + "',@srclot='" + fm.returnval[1] + "'");
                            tx_lotno.Text = ds2.Tables[0].Rows[0][0].ToString();

                            int cnt = DbRequesHandler.GetRowCount("select count(*) from tn_mps1401t where work_no='" + obj.WorkNo + "' and process_code='" + obj.Process + "'");
                            TN_MPS1401 newobj = new TN_MPS1401();
                            newobj.WorkDate = obj.WorkDate;
                            newobj.WorkNo = obj.WorkNo;
                            newobj.Seq = cnt == 0 ? 1 : cnt + 1;
                            newobj.ProcessCode = obj.Process;
                            newobj.LotNo = tx_lotno.Text;
                            newobj.ProcessTurn = obj.PSeq;
                            newobj.OrderQty = obj.PlanQty;
                            newobj.ResultDate = DateTime.Today.Date;
                            newobj.StartDate = DateTime.Now;
                            ModelService.Insert(newobj);
                            ModelService.Save();
                            obj.JobStatus = ((int)MasterCodeEnum.POP_Status_Start).ToString();
                        }
                    }
                  
                }
              
                DataSet ds = DbRequesHandler.GetDataQury("exec SP_JOBSTATUS_UP '" + obj.WorkNo + "','" + obj.PSeq + "','" + obj.Process + "','" + obj.ItemCode + "','" + obj.JobStatus + "'");
                btn_state(Convert.ToInt32(obj.JobStatus));
                GridExControl.MainGrid.BestFitColumns();
            }
            else
            {
                //부품이동표 정보 조회 추가필요
                if (tx_lotno.EditValue.GetNullToEmpty() == "")
                {
                    POP_Popup1.XPFITEMMOVESCAN fm = new POP_Popup1.XPFITEMMOVESCAN(obj);
                    fm.ShowDialog();

                    if (fm.DialogResult == DialogResult.OK)
                    {
                        tx_lotno.Text = fm.retutnvalue;
                        int cnt1 = DbRequesHandler.GetRowCount("select count(*) from tn_mps1401t where work_no='" + obj.WorkNo + "' and process_code='" + obj.Process + "' and lot_no='"+ tx_lotno.Text + "'");
                        int cnt = DbRequesHandler.GetRowCount("select count(*) from tn_mps1401t where work_no='" + obj.WorkNo + "' and process_code='" + obj.Process + "'");
                        if (cnt1 == 0)
                        {
                            TN_MPS1401 newobj = new TN_MPS1401();
                            newobj.WorkDate = obj.WorkDate;
                            newobj.WorkNo = obj.WorkNo;
                            newobj.Seq = cnt == 0 ? 1 : cnt + 1;
                            newobj.ProcessCode = obj.Process;
                            newobj.LotNo = tx_lotno.Text;
                            newobj.ProcessTurn = obj.PSeq;
                            newobj.OrderQty = obj.PlanQty;
                            newobj.ResultDate = DateTime.Today.Date;
                            newobj.StartDate = DateTime.Now;
                            newobj.Itemmoveno = fm.moveno;
                            ModelService.Insert(newobj);
                            ModelService.Save();
                        }
                        obj.JobStatus = ((int)MasterCodeEnum.POP_Status_Start).ToString();
                        DataSet ds = DbRequesHandler.GetDataQury("exec SP_JOBSTATUS_UP '" + obj.WorkNo + "','" + obj.PSeq + "','" + obj.Process + "','" + obj.ItemCode + "','" + obj.JobStatus + "'");
                        btn_state(Convert.ToInt32(obj.JobStatus));
                        GridExControl.MainGrid.BestFitColumns();
                    }
                }
                else
                {
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_Start).ToString();
                    DataSet ds = DbRequesHandler.GetDataQury("exec SP_JOBSTATUS_UP '" + obj.WorkNo + "','" + obj.PSeq + "','" + obj.Process + "','" + obj.ItemCode + "','" + obj.JobStatus + "'");
                    btn_state(Convert.ToInt32(obj.JobStatus));
                    GridExControl.MainGrid.BestFitColumns();
                }
            }

            DataLoad();
        }

        private void fload()
        {
            //사용안함 전일 실적조회
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            string lslotno = DbRequesHandler.GetCellValue("SELECT max([LOT_NO]) lotno FROM TN_LOT_MST where WORK_NO='" + obj.WorkNo + "'", 0);
            ModelService.ReLoad();
            TN_MPS1401 tn1401 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.Process && p.LotNo == lslotno).OrderBy(o => o.Seq).LastOrDefault();
            if (tn1401 != null)
            {
                tx_lotno.Text = tn1401.LotNo;
                tx_makeqty.Text = tn1401.ResultQty.ToString();
                tx_okqty.Text = tn1401.OkQty.ToString();
                tx_badqty.Text = tn1401.FailQty.ToString();
            }
            else
            {
                tx_lotno.Text = "";
                tx_makeqty.Text = "0";
                tx_okqty.Text = "0";
                tx_badqty.Text = "0";
            }


        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult req = MessageBox.Show("Ты уверен?", "경고", MessageBoxButtons.OKCancel);
            if (req == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btn_srcchange_Click(object sender, EventArgs e)
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            if (obj == null) return;
            if (obj.PSeq == 1)
            {
                POP_Popup1.XPFSRCIN fm = new POP_Popup1.XPFSRCIN();
                fm.ShowDialog();
                if (fm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (fm.returnval[1] != DbRequesHandler.GetCellValue("select SRC_LOT from tn_lot_mst where lot_no='" + tx_lotno.Text + "'", 0))
                    {

                        DataSet ds = DbRequesHandler.GetDataQury("exec SP_LOTMAKE1 @workno='" + obj.WorkNo + "',@mccode='" + obj.MachineCode + "',@srccode='" + fm.returnval[0] + "',@item='" + obj.ItemCode + "',@date='" + DateTime.Today.ToString("yyyyMMdd") + "',@srclot='" + fm.returnval[1] + "'");
                        if (tx_lotno.Text != ds.Tables[0].Rows[0][0].ToString())
                        {
                            DialogResult = MessageBox.Show("LЛОТНО меняется. Хотите ли вы зарегистрировать свое существующее выступление LOTNO?", "Уведомления", MessageBoxButtons.YesNo);
                            if (DialogResult == DialogResult.Yes)
                            {
                                ProdQtyin();
                            }
                        }

                        tx_lotno.Text = ds.Tables[0].Rows[0][0].ToString();

                        int cnt = DbRequesHandler.GetRowCount("select count(*) from tn_mps1401t where work_no='" + obj.WorkNo + "' and process_code='" + obj.Process + "'");
                        TN_MPS1401 newobj = new TN_MPS1401();
                        newobj.WorkDate = obj.WorkDate;
                        newobj.WorkNo = obj.WorkNo;
                        newobj.Seq = cnt == 0 ? 1 : cnt + 1;
                        newobj.ProcessCode = obj.Process;
                        newobj.LotNo = tx_lotno.Text;
                        newobj.ProcessTurn = obj.PSeq;
                        newobj.OrderQty = obj.PlanQty;
                        newobj.ResultDate = DateTime.Today.Date;

                        ModelService.Insert(newobj);
                        ModelService.Save();
                    }
                    else
                    {
                        MessageBox.Show("LOTNO такой же, как и оригинальное сырье");
                    }
                }
            }
            else
            {
                POP_Popup1.XPFITEMMOVESCAN fm = new POP_Popup1.XPFITEMMOVESCAN(obj);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    if (tx_lotno.Text != fm.retutnvalue)
                    {
                        DialogResult = MessageBox.Show("Уведомления", "ЛОТ НЕТ изменен. Хотите ли вы зарегистрировать свой существующий ЛОТ НЕТ результатов", MessageBoxButtons.YesNo);
                        if (DialogResult == DialogResult.Yes)
                        {
                            ProdQtyin();
                        }
                    }
                    tx_lotno.Text = fm.retutnvalue;
                  
                       int cnt = DbRequesHandler.GetRowCount("select count(*) from tn_mps1401t where work_no='" + obj.WorkNo + "' and process_code='" + obj.Process + "'");
                                          TN_MPS1401 newobj = new TN_MPS1401();
                        newobj.WorkDate = obj.WorkDate;
                        newobj.WorkNo = obj.WorkNo;
                        newobj.Seq = cnt == 0 ? 1 : cnt + 1;
                        newobj.ProcessCode = obj.Process;
                        newobj.LotNo = tx_lotno.Text;
                        newobj.ProcessTurn = obj.PSeq;
                        newobj.OrderQty = obj.PlanQty;
                        newobj.ResultDate = DateTime.Today.Date;
                        newobj.Itemmoveno = fm.moveno;
                        ModelService.Insert(newobj);
                        ModelService.Save();
                  

                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_Start).ToString();
                    DataSet ds = DbRequesHandler.GetDataQury("exec SP_JOBSTATUS_UP '" + obj.WorkNo + "','" + obj.PSeq + "','" + obj.Process + "','" + obj.ItemCode + "','" + obj.JobStatus + "'");
                    btn_state(Convert.ToInt32(obj.JobStatus));

                }
            }
            GridExControl.MainGrid.BestFitColumns();
            DataLoad();
        }

        private void btn_qtyin_Click(object sender, EventArgs e)
        {
            ProdQtyin();
        }

        private void ProdQtyin()
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            POP_Popup1.XPFQTYIN fm = new POP_Popup1.XPFQTYIN(obj, tx_lotno.Text);
            fm.ShowDialog();
            if (obj.PSeq.GetNullToEmpty() == "1")
            {
                DialogResult = MessageBox.Show("Хотите распечатать таблицу движения?", "알림", MessageBoxButtons.YesNo);
                if (DialogResult == DialogResult.Yes)
                {
                    prtitemmove(obj);
                }
            }
            rowchange();
        }

        private void btn_moveprt_Click(object sender, EventArgs e)
        {
            TP_POPJOBLIST tn = GridBindingSource.Current as TP_POPJOBLIST;

            POP_Popup1.XPFITEMMOVE fm = new POP_Popup1.XPFITEMMOVE(tn.WorkNo, tx_lotno.Text,tn.PSeq);
            fm.ShowDialog();
        }

        private void btn_end_Click(object sender, EventArgs e)
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;

           
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_End).ToString();
            
            //int k = DbRequesHandler.SetDataQury("update tn_mps1400t set JOB_STATES= '" + obj.JobStatus + "'"
            // + " where WORK_NO ='"+obj.WorkNo+"' and P_SEQ ="+obj.PSeq+" and PROCESS='"+obj.Process+"'"
            // +" and ITEM_CODE='"+obj.ItemCode+"' and MACHINE_CODE='"+obj.MachineCode+"'");
            DataSet ds = DbRequesHandler.GetDataQury("exec SP_JOBSTATUS_UP '" + obj.WorkNo + "','" + obj.PSeq + "','" + obj.Process + "','" + obj.ItemCode + "','" + obj.JobStatus + "'");
            //GridExControl.MainGrid.BestFitColumns();
            //rowchange();
            DataLoad();
        }
        private void prtitemmove(TP_POPJOBLIST obj)
        {
            POP_Popup1.XPFITEMMOVE fm = new POP_Popup1.XPFITEMMOVE(obj.WorkNo, tx_lotno.Text, obj.PSeq);
            fm.ShowDialog();
        }
        private void popupqtyin(TP_POPJOBLIST obj)
        {
            POP_Popup1.XPFQTYIN fm = new POP_Popup1.XPFQTYIN(obj, tx_lotno.Text);
            fm.ShowDialog();
        }

        private void btn_stopin_Click(object sender, EventArgs e)
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            String lsMachineCode= "";
            if (obj.MachineCode.GetNullToEmpty() == "") {

                POP_Popup1.XPFMACHINE POP = new POP_Popup1.XPFMACHINE();
                POP.ShowDialog();
                if (POP.DialogResult == DialogResult.OK)
                {
                    lsMachineCode = POP.machine;

                }
                else {

                    return;
                }


            }
            string cnt = DbRequesHandler.GetCellValue("exec SP_STOPCODE_UP '" + lsMachineCode + "'", 0);
            if (cnt == "0")
            {
                POP_Popup1.XPFSTOP fm = new POP_Popup1.XPFSTOP(lsMachineCode);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_Stop).ToString();
                    DataSet ds = DbRequesHandler.GetDataQury("exec SP_JOBSTATUS_UP '" + obj.WorkNo + "','" + obj.PSeq + "','" + obj.Process + "','" + obj.ItemCode + "','" + obj.JobStatus + "'");
                    DataLoad();
                }
            }
            else
            {

                jobstart();
            }

        }

        private void btn_qcin_Click(object sender, EventArgs e)
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            if (obj == null) return;
            POP_Popup1.XPFQCIN qc = new POP_Popup1.XPFQCIN(obj,tx_lotno.Text.GetNullToEmpty());
            qc.ShowDialog();
        }

        private void XFPOP003_Load(object sender, EventArgs e)
        {
            InitGrid();
            DataLoad();
        }

        private void XFPOP003_ResizeEnd(object sender, EventArgs e)
        {
            GridExControl.MainGrid.BestFitColumns();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            DataLoad();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            lup_Process.SelectedIndex = 0;
            proc = lup_Process.EditValue.GetNullToEmpty();
            lup_Mc.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y" && (string.IsNullOrEmpty(proc) ? true : p.Temp == proc)).ToList());
            DataLoad();
            GridExControl.MainGrid.BestFitColumns();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            lup_Process.SelectedIndex = 1;
            proc = lup_Process.EditValue.GetNullToEmpty();
            lup_Mc.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y" && (string.IsNullOrEmpty(proc) ? true : p.Temp == proc)).ToList());
            DataLoad();
            GridExControl.MainGrid.BestFitColumns();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            lup_Process.SelectedIndex = 2;
            proc = lup_Process.EditValue.GetNullToEmpty();
            lup_Mc.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y" && (string.IsNullOrEmpty(proc) ? true : p.Temp == proc)).ToList());
            DataLoad();
            GridExControl.MainGrid.BestFitColumns();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            lup_Process.SelectedIndex = 3;
            proc = lup_Process.EditValue.GetNullToEmpty();
            lup_Mc.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y" && (string.IsNullOrEmpty(proc) ? true : p.Temp == proc)).ToList());
            DataLoad();
            GridExControl.MainGrid.BestFitColumns();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (checkEdit1.EditValue.ToString() == "Y")
            {
                checkEdit1.Checked = false;
                simpleButton6.Text = "цельность";
                simpleButton6.ForeColor= System.Drawing.Color.Black;
                DataLoad();
                GridExControl.MainGrid.BestFitColumns();
            }
            else { checkEdit1.Checked = true;
                simpleButton6.Text = "Прогресс цель";
                simpleButton6.ForeColor = System.Drawing.Color.Blue;
                DataLoad();
                GridExControl.MainGrid.BestFitColumns();
            }
        }
    }
}

