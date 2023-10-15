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

namespace HKInc.Ui.View.POP
{
    public partial class XFPOP001 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1401> ModelService = (IService<TN_MPS1401>)ProductionFactory.GetDomainService("TN_MPS1401");
        private string srccode;
        public XFPOP001()
        {
            InitializeComponent();
            SetToolbarVisible(false);
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            btn_state(0);
            //byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + "logo.jpg"); //서버가 없으므로 주석처리
            //pictureEdit3.EditValue = img;
            pictureEdit1.ReadOnly = true;
            pictureEdit2.ReadOnly = true;
            pictureEdit3.ReadOnly = true;
        }

        protected override void InitCombo()
        {
            lup_Process.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.Process));
            lup_Mc.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList());
            lup_Item.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList());

            //            lupWork.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
        }
        protected override void GridRowDoubleClicked()
        {
           
        }
        protected override void InitGrid()
        {
            GridExControl.MainGrid.MainView.RowHeight = 50;
            GridExControl.MainGrid.SetGridFont(this.GridExControl.MainGrid.MainView, new Font(DefaultFont.FontFamily, 14, FontStyle.Bold));
            GridExControl.MainGrid.MainView.ColumnPanelRowHeight = 50;
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("JobStatus", "작업상태");
            GridExControl.MainGrid.AddColumn("WorkDate", false);
            GridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            GridExControl.MainGrid.AddColumn("Process", "공정");
            GridExControl.MainGrid.AddColumn("MachineCode", "설비");
            GridExControl.MainGrid.AddColumn("ItemCode", "품목");
            GridExControl.MainGrid.AddColumn("PlanQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Memo", "비고");
            GridExControl.MainGrid.AddColumn("WorkStantadNm", false);
            GridExControl.MainGrid.AddColumn("FileData", false);
            GridExControl.MainGrid.AddColumn("DesignFile", false);
            GridExControl.MainGrid.AddColumn("DesignMap", false);
            GridExControl.MainGrid.BestFitColumns();

        }

        protected override void InitRepository()
        {


            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Process", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");

            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStatus", MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList());
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataLoad();

        }

        private void DataLoad()
        {
            try
            {
                WaitHandler.ShowWait();
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {


                    var process = new SqlParameter("@process", lup_Process.EditValue.GetNullToEmpty());
                    var mccode = new SqlParameter("@mccode", lup_Mc.EditValue.GetNullToEmpty());
                    var itemcode = new SqlParameter("@itemcode", lup_Item.EditValue.GetNullToEmpty());
                    var wkno = new SqlParameter("@wkpno", tx_workno.EditValue.GetNullToEmpty());

                    var result = context.Database
                          .SqlQuery<TP_POPJOBLIST>("SP_POP_JOBLIST @process,@mccode ,@itemcode ,@wkpno", process, mccode, itemcode, wkno).OrderBy(p => p.WorkNo).ToList();
                    GridBindingSource.DataSource = result.OrderBy(o => o.PSeq).ToList();

                }
                GridExControl.DataSource = GridBindingSource;
                GridExControl.MainGrid.BestFitColumns();
                rowchange();
            }
            catch(Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
            finally { WaitHandler.CloseWait(); }
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
            pe_domap.EditValue = null;
            pe_domap.EditValue = obj.DesignMap;
        }

        private void pe_jobstd_Click(object sender, EventArgs e)
        {
            if (pe_jobstd.EditValue == null) return;
            POP_Popup.XPFPOPIMG fm = new POP_Popup.XPFPOPIMG("작업표준서", pe_jobstd.EditValue);
            fm.ShowDialog();
        }

        private void pe_domap_DoubleClick(object sender, EventArgs e)
        {
            if (pe_domap.EditValue == null) return;
            POP_Popup.XPFPOPIMG fm = new POP_Popup.XPFPOPIMG("도면", pe_domap.EditValue);
            fm.ShowDialog();
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
                    btn_stopin.Enabled = true;
                    btn_moveprt.Enabled = false;
                    btn_end.Enabled = false;
                    btn_exit.Enabled = true;

                    break;
                case (int)MasterCodeEnum.POP_Status_Start://생산중
                    btn_start.Enabled = false;
                    btn_srcchange.Enabled = true;
                    btn_qtyin.Enabled = true;
                    btn_qcin.Enabled = true;
                    btn_stopin.Enabled = true;
                    btn_moveprt.Enabled = true;
                    btn_end.Enabled = true;
                    btn_exit.Enabled = true;

                    break;
                case (int)MasterCodeEnum.POP_Status_StopWait://일시중지
                    btn_start.Enabled = true;
                    btn_srcchange.Enabled = false;
                    btn_qtyin.Enabled = false;
                    btn_qcin.Enabled = false;
                    btn_stopin.Enabled = true;
                    btn_moveprt.Enabled = false;
                    btn_end.Enabled = false;
                    btn_exit.Enabled = true;

                    break;
                case (int)MasterCodeEnum.POP_Status_Stop://비가동
                    btn_start.Enabled = true;
                    btn_srcchange.Enabled = false;
                    btn_qtyin.Enabled = false;
                    btn_qcin.Enabled = false;
                    btn_stopin.Enabled = true;
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
                    POP_Popup.XPFSRCIN fm = new POP_Popup.XPFSRCIN();
                    fm.ShowDialog();
                    if (fm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        DataSet ds1 = DbRequesHandler.GetDataQury("exec SP_LOTMAKE @workno='" + obj.WorkNo + "',@mccode='" + obj.MachineCode + "',@srccode='" + fm.returnval[0] + "',@item='" + obj.ItemCode + "',@date='" + DateTime.Today.ToString("yyyyMMdd") + "',@srclot='" + fm.returnval[1] + "'");
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
                    DataSet ds1 = DbRequesHandler.GetDataQury("exec SP_LOTMAKE @workno='" + obj.WorkNo + "',@date='" + DateTime.Today.ToString("yyyyMMdd") + "'");
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
              
                DataSet ds = DbRequesHandler.GetDataQury("exec SP_JOBSTATUS_UP '" + obj.WorkNo + "','" + obj.PSeq + "','" + obj.Process + "','" + obj.ItemCode + "','" + obj.JobStatus + "'");
                btn_state(Convert.ToInt32(obj.JobStatus));
                GridExControl.MainGrid.BestFitColumns();
            }
            else
            {
                //부품이동표 정보 조회 추가필요
                if (tx_lotno.EditValue.GetNullToEmpty() == "")
                {
                    POP_Popup.XPFITEMMOVESCAN fm = new POP_Popup.XPFITEMMOVESCAN(obj);
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
            DialogResult req = MessageBox.Show("종료하시겠습니까?", "경고", MessageBoxButtons.OKCancel);
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
                POP_Popup.XPFSRCIN fm = new POP_Popup.XPFSRCIN();
                fm.ShowDialog();
                if (fm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (fm.returnval[1] != DbRequesHandler.GetCellValue("select SRC_LOT from tn_lot_mst where lot_no='" + tx_lotno.Text + "'", 0))
                    {

                        DataSet ds = DbRequesHandler.GetDataQury("exec SP_LOTMAKE @workno='" + obj.WorkNo + "',@mccode='" + obj.MachineCode + "',@srccode='" + fm.returnval[0] + "',@item='" + obj.ItemCode + "',@date='" + DateTime.Today.ToString("yyyyMMdd") + "',@srclot='" + fm.returnval[1] + "'");
                        if (tx_lotno.Text != ds.Tables[0].Rows[0][0].ToString())
                        {
                            DialogResult = MessageBox.Show("알림", "LOTNO가 변경됩니다. 기존 LOTNO 실적을 등록하시겠습니까?", MessageBoxButtons.YesNo);
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
                        MessageBox.Show("기존에 투입된 원소재와 같은 LOTNO 입니다.");
                    }
                }
            }
            else
            {
                POP_Popup.XPFITEMMOVESCAN fm = new POP_Popup.XPFITEMMOVESCAN(obj);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    if (tx_lotno.Text != fm.retutnvalue)
                    {
                        DialogResult = MessageBox.Show("알림", "LOTNO가 변경됩니다. 기존 LOTNO 실적을 등록하시겠습니까?", MessageBoxButtons.YesNo);
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

        }

        private void btn_qtyin_Click(object sender, EventArgs e)
        {
            ProdQtyin();
        }

        private void ProdQtyin()
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            POP_Popup.XPFQTYIN fm = new POP_Popup.XPFQTYIN(obj, tx_lotno.Text, GridExControl.MainGrid.MainView.GetFocusedRowCellDisplayText("Process").ToString());
            fm.ShowDialog();
            rowchange();
        }

        private void btn_moveprt_Click(object sender, EventArgs e)
        {
            TP_POPJOBLIST tn = GridBindingSource.Current as TP_POPJOBLIST;

            POP_Popup.XPFITEMMOVE fm = new POP_Popup.XPFITEMMOVE(tn.WorkNo, tx_lotno.Text,tn.PSeq);
            fm.ShowDialog();
        }

        private void btn_end_Click(object sender, EventArgs e)
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;

            POP_Popup.XPFJOBEND efm = new POP_Popup.XPFJOBEND();
            efm.ShowDialog();
            switch (efm.lstatus)
            {
                case "qtytostop":
                    popupqtyin(obj);
                    DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                    if (DialogResult == DialogResult.Yes)
                    {
                        prtitemmove(obj);
                    }
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_StopWait).ToString();
                    break;
                case "exit":
                    DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                    if (DialogResult == DialogResult.Yes)
                    {
                        prtitemmove(obj);
                    }
                    break;
                case "qtytoend":
                    popupqtyin(obj);
                    DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                    if (DialogResult == DialogResult.Yes)
                    {
                        prtitemmove(obj);
                    }
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_End).ToString();
                    break;
                case "stop":
                    DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                    if (DialogResult == DialogResult.Yes)
                    {
                        prtitemmove(obj);
                    }
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_StopWait).ToString();
                    break;
                case "end":
                    DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                    if (DialogResult == DialogResult.Yes)
                    {
                        prtitemmove(obj);
                    }
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_End).ToString();
                    break;

            }
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
            POP_Popup.XPFITEMMOVE fm = new POP_Popup.XPFITEMMOVE(obj.WorkNo, tx_lotno.Text, obj.PSeq);
            fm.ShowDialog();
        }
        private void popupqtyin(TP_POPJOBLIST obj)
        {
            POP_Popup.XPFQTYIN fm = new POP_Popup.XPFQTYIN(obj, tx_lotno.Text, GridExControl.MainGrid.MainView.GetFocusedRowCellDisplayText("Process").ToString());
            fm.ShowDialog();
        }

        private void btn_stopin_Click(object sender, EventArgs e)
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            if (obj.MachineCode.GetNullToEmpty() == "") return;
            string cnt = DbRequesHandler.GetCellValue("exec SP_STOPCODE_UP '" + obj.MachineCode.ToString() + "'", 0);
            if (cnt == "0")
            {
                POP_Popup.XPFSTOP fm = new POP_Popup.XPFSTOP(obj.MachineCode);
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
            POP_Popup.XPFQCIN qc = new POP_Popup.XPFQCIN(obj,tx_lotno.Text.GetNullToEmpty());
            qc.ShowDialog();
        }

        private void XFPOP001_Load(object sender, EventArgs e)
        {
            InitGrid();
            DataLoad();
        }

        private void XFPOP001_ResizeEnd(object sender, EventArgs e)
        {
            GridExControl.MainGrid.BestFitColumns();
        }
    }
}

