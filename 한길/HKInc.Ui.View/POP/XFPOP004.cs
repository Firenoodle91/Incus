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
    /// <summary>
    /// POP 한길 VERSION UI 변경건
    /// </summary>
    public partial class XFPOP004 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1401> ModelService = (IService<TN_MPS1401>)ProductionFactory.GetDomainService("TN_MPS1401");
        private string srccode;
        //private List<TN_MEA1000> MachineList;
        List<TN_STD1000> materialList = DbRequesHandler.GetCommCode(MasterCodeSTR.ITEM_MATERIAL);

        public XFPOP004()
        {
            InitializeComponent();
            GridExControl = gridEx1;
        }

        protected override void InitControls()
        {
            base.InitControls();
            SetToolbarVisible(false);
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            btn_state(0);
            pictureEdit1.ReadOnly = true;
            pictureEdit2.ReadOnly = true;
            //pictureEdit3.ReadOnly = true;
            dateWorkDate.Properties.ShowClear = true;
            dateWorkDate.SetFontSize(dateWorkDate.Font);
        }

        protected override void InitCombo()
        {
            lup_Process.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Process.SetFontSize(new Font("Tahoma", 13f, FontStyle.Bold));
            //using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            //{
            //    MachineList = context.TN_MEA1000.Where(p => p.UseYn == "Y").Select(p => new { p.MachineCode, p.MachineName, p.UseYn }).ToList().Select(p => new TN_MEA1000 { MachineCode = p.MachineCode, MachineName = p.MachineName, UseYn = p.UseYn }).ToList();
            //}
            lup_Mc.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Mc.SetFontSize(new Font("Tahoma", 13f, FontStyle.Bold));
            lup_Mc.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            //lup_Mc.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lup_Item.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //dateWorkDate.EditValue = DateTime.Today;
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.Init();
            GridExControl.SetToolbarVisible(false);
            //GridExControl.SetToolbarButtonVisible(false);
            //GridExControl.SetToolbarButtonLargeIconVisible(true);
            //GridExControl.SetToolbarButtonFont(new Font(DefaultFont.FontFamily, 14, FontStyle.Bold));
            //GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            //GridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, "포장라벨출력[F3]", DevExpress.Images.ImageResourceCache.Default.GetImage("images/print/printer_32x32.png"));
            GridExControl.MainGrid.AddColumn("RowIndex", false);
            GridExControl.MainGrid.AddColumn("JobStatus", "작업상태", HorzAlignment.Center, true);
            //GridExControl.MainGrid.AddColumn("WorkDate", "작업지시일", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("DueDate", "납기일", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호", HorzAlignment.Center, false);
            GridExControl.MainGrid.AddColumn("Process", "공정코드", false);
            GridExControl.MainGrid.AddColumn("ProcessName", "공정");
            GridExControl.MainGrid.AddColumn("MachineCode", "설비코드",false);
            GridExControl.MainGrid.AddColumn("MachineName", "설비");
            GridExControl.MainGrid.AddColumn("CustomerCode", "거래처명");
            GridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            GridExControl.MainGrid.AddColumn("ItemNm1", "품번", false);
            GridExControl.MainGrid.AddColumn("ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("PlanQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("StdPackQty", "박스당수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Memo", "비고", false);
            GridExControl.MainGrid.AddColumn("WorkStantadNm", false);
            GridExControl.MainGrid.AddColumn("FileData", false);
            //GridExControl.MainGrid.AddColumn("ItemDesignName", false); //도면은 메모리 관련하여 row change 마다 불러오도록 변경
            //GridExControl.MainGrid.AddColumn("ItemDesignFile", false);
            GridExControl.MainGrid.SetGridFont(GridExControl.MainGrid.MainView, new Font("맑은 고딕", 13f, FontStyle.Bold));
            GridExControl.MainGrid.MainView.ColumnPanelRowHeight = 50;
            GridExControl.MainGrid.MainView.RowHeight = 50;
            GridExControl.MainGrid.MainView.Columns["JobStatus"].MinWidth = 90;
            GridExControl.MainGrid.MainView.Columns["JobStatus"].MaxWidth = 90;
            GridExControl.MainGrid.MainView.Columns["DueDate"].MinWidth = 120;
            GridExControl.MainGrid.MainView.Columns["DueDate"].MaxWidth = 120;
            GridExControl.MainGrid.MainView.Columns["WorkNo"].MinWidth = 180;
            GridExControl.MainGrid.MainView.Columns["WorkNo"].MaxWidth = 180;
            GridExControl.MainGrid.MainView.Columns["PlanQty"].MinWidth = 90;
            GridExControl.MainGrid.MainView.Columns["PlanQty"].MaxWidth = 90;
            GridExControl.MainGrid.MainView.Columns["StdPackQty"].MinWidth = 107;
            GridExControl.MainGrid.MainView.Columns["StdPackQty"].MaxWidth = 107;

            GridExControl.MainGrid.MainView.Columns["ProcessName"].MinWidth = 110;
            GridExControl.MainGrid.MainView.Columns["ProcessName"].MaxWidth = 110;
            GridExControl.MainGrid.MainView.Columns["MachineName"].MinWidth = 110;
            GridExControl.MainGrid.MainView.Columns["MachineName"].MaxWidth = 110;
            GridExControl.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Process", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStatus", MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList());
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag != "N"), "CustomerCode", "CustomerName");

            var repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            GridExControl.MainGrid.MainView.OptionsView.ColumnAutoWidth = true;
            GridExControl.MainGrid.MainView.OptionsView.RowAutoHeight = true;
            repositoryItemMemoEdit1.ScrollBars = ScrollBars.None;
            //GridExControl.MainGrid.MainView.Columns["WorkNo"].ColumnEdit = repositoryItemMemoEdit1;
            repositoryItemMemoEdit1.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            GridExControl.MainGrid.MainView.Columns["ProcessName"].ColumnEdit = repositoryItemMemoEdit1;
            GridExControl.MainGrid.MainView.Columns["ProcessName"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            GridExControl.MainGrid.MainView.Columns["MachineName"].ColumnEdit = repositoryItemMemoEdit1;
            GridExControl.MainGrid.MainView.Columns["MachineName"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            GridExControl.MainGrid.MainView.Columns["ItemNm"].ColumnEdit = repositoryItemMemoEdit1;
            GridExControl.MainGrid.MainView.Columns["ItemNm"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
        }

        protected override void GridRowDoubleClicked(){}

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        //포장라벨출력
        private bool PackLabelPrint()
        {
            // 포장 로직 변경 19.09.23
            //TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            //if (obj == null) return false;

            //IService<TN_MPS1401> Service = (IService<TN_MPS1401>)ProductionFactory.GetDomainService("TN_MPS1401");
            //var tn1401 = Service.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.Process).OrderBy(o => o.Seq).LastOrDefault();
            //if (tn1401 == null)
            //{
            //    MessageBoxHandler.Show("출력할 실적이 없습니다.", "경고");
            //    return false;
            //}

            //if (tn1401.LotNo.GetNullToEmpty() == "")
            //{
            //    MessageBoxHandler.Show("출력할 LOT NO가 없습니다.", "경고");
            //    return false;
            //}
            //else if(tn1401.ResultQty.GetIntNullToZero() == 0)
            //{
            //    MessageBoxHandler.Show("출력할 생산 수량이 없습니다.", "경고");
            //    return false;
            //}
            //else
            //{
            //    var ItemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == obj.ItemCode).First();

            //    //TN_MPS1401 tn = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.Process && p.LotNo == tn1401.LotNo).FirstOrDefault();

            //    POP_Popup.XPFPACKLABEL form = new POP_Popup.XPFPACKLABEL(tn1401.ResultQty.GetIntNullToZero(), tn1401.LotNo, ItemObj.ItemNm1, ItemObj.ItemNm, tn1401, ItemObj.StdPackQty.GetIntNullToZero());
            //    var value = form.ShowDialog();
            //    if (value == DialogResult.OK) return true;
            //    else return false;
            //}

            return true;
        }

        //조회
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private new void DataLoad()
        {
            try
            {
                WaitHandler.ShowWait();
                GridRowLocator.GetCurrentRow("RowIndex");
                GridExControl.MainGrid.Clear();
                var ItemCodeName = textItemCodeName.EditValue.GetNullToEmpty();
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    var process = new SqlParameter("@process", lup_Process.EditValue.GetNullToEmpty());
                    var mccode = new SqlParameter("@mccode", lup_Mc.EditValue.GetNullToEmpty());
                    var itemcode = new SqlParameter("@itemcode", "");
                    var wkno = new SqlParameter("@wkpno", tx_workno.EditValue.GetNullToEmpty());
                    var workDate = new SqlParameter("@WorkDate", dateWorkDate.EditValue == null ? new DateTime(9999,1,1) : dateWorkDate.DateTime);
                    var result = context.Database
                          .SqlQuery<TP_POPJOBLIST>("SP_POP_JOBLIST @process,@mccode ,@itemcode ,@wkpno, @WorkDate", process, mccode, itemcode, wkno, workDate).ToList();
                    GridBindingSource.DataSource = result.Where(p=>p.ItemNm1.Contains(ItemCodeName) || p.ItemNm.Contains(ItemCodeName))
                        .OrderByDescending(p=>p.JobStatus).ThenBy(p=>p.DueDate).ThenBy(p=>p.WorkNo).ThenBy(o => o.PSeq).ThenBy(p=>p.DisplayOrder).ToList();
                    if(result.Count == 0)
                    {
                        tx_lotno.Text = "";
                        tx_makeqty.Text = "0";
                        tx_okqty.Text = "0";
                        tx_badqty.Text = "0";
                        tx_totok.Text = "0";
                        memoEdit1.EditValue = null;
                        pe_domap.EditValue = null;

                        textWorkOrderNo.EditValue = null;
                        textMatStock.EditValue = "0";
                    }

                }
                GridExControl.DataSource = GridBindingSource;                
                GridRowLocator.SetCurrentRow();
                GridExControl.MainGrid.BestFitColumns();
                
                //rowchange();
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
            finally { WaitHandler.CloseWait(); }
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                rowchange();
            }
            finally { WaitHandler.CloseWait(); }
        }

        private void rowchange()
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            
            tx_lotno.Text = "";
            tx_makeqty.Text = "0";
            tx_okqty.Text = "0";
            tx_badqty.Text = "0";
            tx_totok.Text = "0";
            memoEdit1.EditValue = null;
            pe_domap.EditValue = null;

            GridExControl.SetToolbarButtonEnable(GridToolbarButton.AddRow, false);
            if (obj == null)  return; 
            
            if(GridExControl.MainGrid.MainView.GetFocusedRowCellDisplayText("Process") == "포장")
                GridExControl.SetToolbarButtonEnable(GridToolbarButton.AddRow, true);

            btn_state(Convert.ToInt32(obj.JobStatus));
            ModelService.ReLoad();
        
            TN_MPS1401 tn1401 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.Process ).OrderBy(o => o.Seq).LastOrDefault();
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
           
            String filename = obj.WorkStantadNm.GetNullToEmpty();

            if (filename == "")
            {
                memoEdit1.EditValue = null;
            }
            else
            {

                //byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + filename);

                //pe_jobstd.EditValue = null;
                //pe_jobstd.EditValue = img;
                memoEdit1.EditValue = filename;
            }

            //var DomapObj = ModelService.GetChildList<TN_STD1600>(p => p.ItemCode == obj.ItemCode).OrderBy(p => p.Seq).LastOrDefault();
            //pe_domap.EditValue = DomapObj == null ? null : DomapObj.DesignMap;
            //pe_domap.EditValue = null;
            //pe_domap.EditValue = obj.DesignMap;


            String filename2 = obj.DesignFile.GetNullToEmpty();

            if (filename2 == "")
            {
                pe_domap.EditValue = null;
            }
            else
            {
                byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + filename2);
                pe_domap.EditValue = null;
                pe_domap.EditValue = img;
            }

            textWorkOrderNo.EditValue = obj.WorkNo;

            var productItemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == obj.ItemCode).First();
            var materialCode = materialList.Where(p => p.Mcode == productItemObj.Spec1).FirstOrDefault() == null ? string.Empty : materialList.Where(p => p.Mcode == productItemObj.Spec1).First().Codename;
            var materialConstraint = string.Format("{0}_{1}_{2}", materialCode, productItemObj.Spec2, productItemObj.Spec3);
            var materialItemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemNm.Contains(materialConstraint)).FirstOrDefault();
            if (materialItemObj != null)
            {
                textMatItemName.EditValue = materialItemObj.ItemNm;
                //원자재품명, 재고량
                var MatStockObj = ModelService.GetChildList<VI_PURSTOCK>(p => p.ItemCode == materialItemObj.ItemCode).FirstOrDefault();
                if (MatStockObj != null)
                {
                    textMatStock.EditValue = MatStockObj.StockQty.GetIntNullToZero();
                }
                else
                {
                    //textMatItemName.EditValue = null;
                    textMatStock.EditValue = 0;
                }

                //원자재위치
                var materialStockObj = ModelService.GetChildList<TN_PUR1301>(p => p.ItemCode == materialItemObj.ItemCode).OrderBy(p => p.CreateTime).LastOrDefault();
                if (materialStockObj != null)
                {
                    var positionObj = ModelService.GetChildList<TN_WMS2000>(p => p.PosionCode == materialStockObj.WhPosition).FirstOrDefault();
                    textMaterialPosition.EditValue = positionObj != null ? positionObj.PosionName : null;
                }
                else
                    textMaterialPosition.EditValue = null;
            }
            else
            {
                textMatItemName.EditValue = null;
                textMatStock.EditValue = 0;
                textMaterialPosition.EditValue = null;
            }

            //금형,칼 위치
            var moldObj = ModelService.GetChildList<TN_MOLD001>(p => p.MoldCode == obj.MoldCode).FirstOrDefault();
            var knifeObj = ModelService.GetChildList<TN_KNIFE001>(p => p.KnifeCode == obj.KnifeCode).FirstOrDefault();
            if (moldObj != null)
            {
                var positionObj = ModelService.GetChildList<TN_WMS2000>(p => p.PosionCode == moldObj.StPostion2).FirstOrDefault();
                textMoldPosition.EditValue = positionObj != null ? positionObj.PosionName : null;
            }
            else
                textMoldPosition.EditValue = null;

            if (knifeObj != null)
            {
                var positionObj = ModelService.GetChildList<TN_WMS2000>(p => p.PosionCode == knifeObj.StPostion2).FirstOrDefault();
                textKnifePosition.EditValue = positionObj != null ? positionObj.PosionName : null;
            }
            else
                textKnifePosition.EditValue = null;
        }

        //도면 클릭
        private void pe_domap_DoubleClick(object sender, EventArgs e)
        {
            if (pe_domap.EditValue == null) return;
            POP_Popup.XPFPOPIMG fm = new POP_Popup.XPFPOPIMG("제품사진", pe_domap.EditValue);
            fm.ShowDialog();
        }
        
        //▲ 클릭
        private void p_UP_Click(object sender, EventArgs e)
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            int irow = gv.FocusedRowHandle;
            irow--;
            gv.FocusedRowHandle = irow;
        }

        //▼ 클릭
        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            int irow = gv.FocusedRowHandle;
            irow++;
            gv.FocusedRowHandle = irow;
        }

        //버튼 상태값 CHECK
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
                    btn_start.Enabled = false;
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

        //작업시작
        private void btn_start_Click(object sender, EventArgs e)
        {
            jobstart();
        }

        private void jobstart()
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;

            if (obj.PSeq == 1)
            {
                if (obj.JobStatus == ((int)MasterCodeEnum.POP_Status_Wait).ToString())
                {
                    //POP_Popup.XPFSRCIN2 fm = new POP_Popup.XPFSRCIN2();
                    POP_Popup.XPFSRCIN3 fm = new POP_Popup.XPFSRCIN3();
                    fm.TP_POPJOB = obj;
                    fm.ShowDialog();
                    if (fm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        string sql = "exec SP_LOTMAKE_ETC @workno='" + obj.WorkNo + "'" +
                                                         ",@mccode='" + obj.MachineCode + "'" +
                                                         ",@item='" + obj.ItemCode + "'" +
                                                         ",@date='" + DateTime.Today.ToString("yyyyMMdd") + "'" +
                                                         ",@srccode='" + fm.returnval[0] + "'" +
                                                         ",@srclot='" + fm.returnval[1] + "'" +
                                                         //",@srccode1='" + fm.returnval[2] + "'" +
                                                         //",@srclot1='" + fm.returnval[3] + "'" +
                                                         //",@srccode2='" + fm.returnval[4] + "'" +
                                                         //",@srclot2='" + fm.returnval[5] + "'" +
                                                         //",@srccode3='" + fm.returnval[6] + "'" +
                                                         //",@srclot3='" + fm.returnval[7] + "'" +
                                                         ",@knife1='" + fm.returnval[8] + "'";
                                                         //",@knife2='" + fm.returnval[9] + "'" +
                                                         //",@knife3='" + fm.returnval[10] + "'" +
                                                         //",@knife4='" + fm.returnval[11] + "'";
                                                        //",@srccode4='" + fm.returnval[8] + "'" +
                                                        //",@srclot4='" + fm.returnval[9] + "'" +
                                                        //",@srccode5='" + fm.returnval[10] + "'" +
                                                        //",@srclot5='" + fm.returnval[11] + "'" +
                                                        //",@srccode4='" + fm.returnval[8] + "'" +
                                                        //",@srclot4='" + fm.returnval[9] + "'" +
                                                        //",@srccode5='" + fm.returnval[10] + "'" +
                                                        //",@srclot5='" + fm.returnval[11] + "'" +
                                                        //",@srccode6='" + fm.returnval[12] + "'" +
                                                        //",@srclot6='" + fm.returnval[13] + "'" +
                                                        //",@srccode7='" + fm.returnval[14] + "'" +
                                                        //",@srclot7='" + fm.returnval[15] + "'";
                                                        //",@srccode4='" + fm.returnval[8] + "'" +
                                                        //",@srclot4='" + fm.returnval[9] + "'" +
                                                        //",@srccode5='" + fm.returnval[10] + "'" +
                                                        //",@srclot5='" + fm.returnval[11] + "'" +

                        string slotno = DbRequesHandler.GetCellValue(sql, 0);
                        tx_lotno.Text = slotno;
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

            DataLoad();
        }

        //사용안함 전일 실적조회
        private void fload()
        {
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

        //종료
        private void btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult req = MessageBox.Show("종료하시겠습니까?", "경고", MessageBoxButtons.OKCancel);
            if (req == DialogResult.OK)
            {
                this.Close();
            }
        }

        //원소재투입
        private void btn_srcchange_Click(object sender, EventArgs e)
        {
			TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            if (obj == null) return;
         
            POP_Popup.XPFSRCIN3 fm = new POP_Popup.XPFSRCIN3();
            fm.TP_POPJOB = obj;
            fm.ShowDialog();
            if (fm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                string sql = "exec SP_LOTMAKE_ETC @workno='" + obj.WorkNo + "'" +
                                                ",@mccode='" + obj.MachineCode + "'" +
                                                ",@item='" + obj.ItemCode + "'" +
                                                ",@date='" + DateTime.Today.ToString("yyyyMMdd") + "'" +
                                                ",@srccode='" + fm.returnval[0] + "'" +
                                                ",@srclot='" + fm.returnval[1] + "'" +
                                                //",@srccode1='" + fm.returnval[2] + "'" +
                                                //",@srclot1='" + fm.returnval[3] + "'" +
                                                //",@srccode2='" + fm.returnval[4] + "'" +
                                                //",@srclot2='" + fm.returnval[5] + "'" +
                                                //",@srccode3='" + fm.returnval[6] + "'" +
                                                //",@srclot3='" + fm.returnval[7] + "'" +
                                                ",@knife1='" + fm.returnval[8] + "'";
                                                //",@knife2='" + fm.returnval[9] + "'" +
                                                //",@knife3='" + fm.returnval[10] + "'" +
                                                //",@knife4='" + fm.returnval[11] + "'";
                                                //",@srccode4='" + fm.returnval[8] + "'" +
                                                //",@srclot4='" + fm.returnval[9] + "'" +
                                                //",@srccode5='" + fm.returnval[10] + "'" +
                                                //",@srclot5='" + fm.returnval[11] + "'" +
                                                //",@srccode4='" + fm.returnval[8] + "'" +
                                                //",@srclot4='" + fm.returnval[9] + "'" +
                                                //",@srccode5='" + fm.returnval[10] + "'" +
                                                //",@srclot5='" + fm.returnval[11] + "'" +
                                                //",@srccode6='" + fm.returnval[12] + "'" +
                                                //",@srclot6='" + fm.returnval[13] + "'" +
                                                //",@srccode7='" + fm.returnval[14] + "'" +
                                                //",@srclot7='" + fm.returnval[15] + "'";
                                                //",@srccode4='" + fm.returnval[8] + "'" +
                                                //",@srclot4='" + fm.returnval[9] + "'" +
                                                //",@srccode5='" + fm.returnval[10] + "'" +
                                                //",@srclot5='" + fm.returnval[11] + "'" +

                string slotno = DbRequesHandler.GetCellValue(sql, 0);
                if (tx_lotno.Text != "")
                {
                    if (tx_lotno.Text != slotno)
                    {
                        DialogResult = MessageBox.Show( "LOTNO가 변경됩니다. 기존 LOTNO 실적을 등록하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                        if (DialogResult == DialogResult.Yes)
                        {
                            ProdQtyin();
                        }

                        tx_lotno.Text = slotno;

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

                        tx_makeqty.Text = "0";
                        tx_okqty.Text = "0";
                        tx_badqty.Text = "0";

                        ModelService.Insert(newobj);
                        ModelService.Save();

                        
                    }
                    else
                    {
                        MessageBox.Show("기존에 투입된 원소재와 같은 LOTNO 입니다.");
                    }
                }
                else
                {
                    tx_lotno.Text = slotno;

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


                GridExControl.MainGrid.BestFitColumns();
            }
        }

        /// <summary>
        /// 20220331 오세완 차장 
        /// 오류 수정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_srcchange_Click_V2(object sender, EventArgs e)
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            if (obj == null)
                return;

            POP_Popup.XPFSRCIN3 fm = new POP_Popup.XPFSRCIN3();
            fm.TP_POPJOB = obj;
            fm.ShowDialog();
            if (fm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                string sql = "exec SP_LOTMAKE_ETC_V2 @workno='" + obj.WorkNo + "'" +
                                                ",@mccode='" + obj.MachineCode + "'" +
                                                ",@item='" + obj.ItemCode + "'" +
                                                ",@date='" + DateTime.Today.ToString("yyyyMMdd") + "'" +
                                                ",@srccode='" + fm.returnval[0] + "'" +
                                                ",@srclot='" + fm.returnval[1] + "'" +
                                                ",@knife1='" + fm.returnval[8] + "'";

                string slotno = DbRequesHandler.GetCellValue(sql, 0);
                if (tx_lotno.Text != "")
                {
                    if(slotno.GetNullToEmpty() == "")
                    {
                        MessageBox.Show("생산 LOTNO 채번 실패입니다. 다시 시도해 주세요.");
                    }
                    else
                    {
                        if (tx_lotno.Text != slotno)
                        {
                            DialogResult = MessageBox.Show("LOTNO가 변경됩니다. 기존 LOTNO 실적을 등록하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                            if (DialogResult == DialogResult.Yes)
                            {
                                ProdQtyin();
                            }

                            tx_lotno.Text = slotno;

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

                            tx_makeqty.Text = "0";
                            tx_okqty.Text = "0";
                            tx_badqty.Text = "0";

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
                    tx_lotno.Text = slotno;

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
            }
        }

        //실적등록
        private void btn_qtyin_Click(object sender, EventArgs e)
        {
            ProdQtyin();
        }

        private void ProdQtyin()
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            if (popupqtyin(obj) == DialogResult.Cancel) return;
            //POP_Popup.XPFQTYIN fm = new POP_Popup.XPFQTYIN(obj, tx_lotno.Text, GridExControl.MainGrid.MainView.GetFocusedRowCellDisplayText("Process").ToString());
            //fm.ShowDialog();
            //rowchange();

            try
            {
                WaitHandler.ShowWait();
                rowchange();
            }
            finally { WaitHandler.CloseWait(); }
        }

        //이동표출력
        private void btn_moveprt_Click(object sender, EventArgs e)
        {
            TP_POPJOBLIST tn = GridBindingSource.Current as TP_POPJOBLIST;
            POP_Popup.XPFITEMMOVE fm = new POP_Popup.XPFITEMMOVE(tn.WorkNo, tx_lotno.Text,tn.PSeq);
            fm.ShowDialog();
        }

        //작업완료
        private void btn_end_Click(object sender, EventArgs e)
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;

            POP_Popup.XPFJOBEND efm = new POP_Popup.XPFJOBEND();
            efm.ShowDialog();
            switch (efm.lstatus)
            {
                case "qtytostop":
                    if (popupqtyin(obj) == DialogResult.Cancel) return;
                    if (obj.ProcessName.Trim() == "성형")
                    {
                        DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                        if (DialogResult == DialogResult.Yes)
                        {
                            prtitemmove(obj);
                        }
                    }
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_StopWait).ToString();
                    break;
                case "exit":
                    //DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                    //if (DialogResult == DialogResult.Yes)
                    //{
                    //    prtitemmove(obj);
                    //}
                    return;
                    //break;
                case "qtytoend":
                    if (popupqtyin(obj) == DialogResult.Cancel) return;
                    // 포장 로직 변경 19.09.23
                    //if (obj.Process == MasterCodeSTR.ProcessPacking
                    //    || obj.Process == MasterCodeSTR.ProcessCutToPacking
                    //    || obj.Process == MasterCodeSTR.ProcessMakeToCutToPacking)
                    //{
                    //    if (PackLabelPrint())
                    //    {
                    //        DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                    //        if (DialogResult == DialogResult.Yes)
                    //        {
                    //            prtitemmove(obj);
                    //        }
                    //        else
                    //        {
                    //            if (obj.PSeq == 1)
                    //            {
                    //                int k = DbRequesHandler.SetDataQury("EXEC SP_ITEM_MOVE_INSERT '" + obj.WorkNo + "', '" + tx_lotno.Text + "'");
                    //            }
                    //        }
                    //        obj.JobStatus = ((int)MasterCodeEnum.POP_Status_End).ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                    //    if (DialogResult == DialogResult.Yes)
                    //    {
                    //        prtitemmove(obj);
                    //    }
                    //    else
                    //    {
                    //        if (obj.PSeq == 1)
                    //        {
                    //            int k = DbRequesHandler.SetDataQury("EXEC SP_ITEM_MOVE_INSERT '" + obj.WorkNo + "', '" + tx_lotno.Text + "'");
                    //        }
                    //    }
                    //    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_End).ToString();
                    //}
                    if (obj.ProcessName.Trim() == "성형")
                    {
                        DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                        if (DialogResult == DialogResult.Yes)
                        {
                            prtitemmove(obj);
                        }
                        else
                        {
                            if (obj.PSeq == 1)
                            {
                                int k = DbRequesHandler.SetDataQury("EXEC SP_ITEM_MOVE_INSERT '" + obj.WorkNo + "', '" + tx_lotno.Text + "'");
                            }
                        }
                    }
                    else
                    {
                        if (obj.PSeq == 1)
                        {
                            int k = DbRequesHandler.SetDataQury("EXEC SP_ITEM_MOVE_INSERT '" + obj.WorkNo + "', '" + tx_lotno.Text + "'");
                        }
                    }
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_End).ToString();
                    break;
                case "stop":
                    if (obj.ProcessName.Trim() == "성형")
                    {
                        DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                        if (DialogResult == DialogResult.Yes)
                        {
                            prtitemmove(obj);
                        }
                    }
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_StopWait).ToString();
                    break;
                case "end":
                    //if (GridExControl.MainGrid.MainView.GetFocusedRowCellDisplayText("Process").ToString() == "포장")
                    //포장 로직 변경 19.09.23
                    //if (obj.Process == MasterCodeSTR.ProcessPacking
                    //    || obj.Process == MasterCodeSTR.ProcessCutToPacking
                    //    || obj.Process == MasterCodeSTR.ProcessMakeToCutToPacking)
                    //{
                    //    if (PackLabelPrint())
                    //    {
                    //        DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                    //        if (DialogResult == DialogResult.Yes)
                    //        {
                    //            prtitemmove(obj);
                    //        }
                    //        else
                    //        {
                    //            if (obj.PSeq == 1)
                    //            {
                    //                int k = DbRequesHandler.SetDataQury("EXEC SP_ITEM_MOVE_INSERT '" + obj.WorkNo + "', '" + tx_lotno.Text + "'");
                    //            }
                    //        }
                    //        obj.JobStatus = ((int)MasterCodeEnum.POP_Status_End).ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                    //    if (DialogResult == DialogResult.Yes)
                    //    {
                    //        prtitemmove(obj);
                    //    }
                    //    else
                    //    {
                    //        if (obj.PSeq == 1)
                    //        {
                    //            int k = DbRequesHandler.SetDataQury("EXEC SP_ITEM_MOVE_INSERT '" + obj.WorkNo + "', '" + tx_lotno.Text + "'");
                    //        }
                    //    }
                    //    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_End).ToString();
                    //}
                    if (obj.ProcessName.Trim() == "성형")
                    {
                        DialogResult = MessageBox.Show("부품이동표를 출력 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                        if (DialogResult == DialogResult.Yes)
                        {
                            prtitemmove(obj);
                        }
                        else
                        {
                            if (obj.PSeq == 1)
                            {
                                int k = DbRequesHandler.SetDataQury("EXEC SP_ITEM_MOVE_INSERT '" + obj.WorkNo + "', '" + tx_lotno.Text + "'");
                            }
                        }
                    }
                    else
                    {
                        if (obj.PSeq == 1)
                        {
                            int k = DbRequesHandler.SetDataQury("EXEC SP_ITEM_MOVE_INSERT '" + obj.WorkNo + "', '" + tx_lotno.Text + "'");
                        }
                    }
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_End).ToString();
                    break;
            }
            DataSet ds = DbRequesHandler.GetDataQury("exec SP_JOBSTATUS_UP '" + obj.WorkNo + "','" + obj.PSeq + "','" + obj.Process + "','" + obj.ItemCode + "','" + obj.JobStatus + "'");            
            DataLoad();
        }

        private void prtitemmove(TP_POPJOBLIST obj)
        {
            POP_Popup.XPFITEMMOVE fm = new POP_Popup.XPFITEMMOVE(obj.WorkNo, tx_lotno.Text, obj.PSeq);
            if (fm.ShowDialog() != DialogResult.OK)
            {
                if(obj.PSeq == 1)
                {
                    int k = DbRequesHandler.SetDataQury("EXEC SP_ITEM_MOVE_INSERT '" + obj.WorkNo + "', '" + tx_lotno.Text + "'");
                }
            }
        }

        private DialogResult popupqtyin(TP_POPJOBLIST obj)
        {
            POP_Popup.XPFQTYIN2 fm = new POP_Popup.XPFQTYIN2(obj, tx_lotno.Text, GridExControl.MainGrid.MainView.GetFocusedRowCellDisplayText("Process").ToString());
            return fm.ShowDialog();
        }

        //비가동 버튼 이벤트
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

        //품질등록 이벤트
        private void btn_qcin_Click(object sender, EventArgs e)
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            if (obj == null) return;
            POP_Popup.XPFQCIN qc = new POP_Popup.XPFQCIN(obj,tx_lotno.Text.GetNullToEmpty());
            qc.ShowDialog();
        }

        private void XFPOP001_ResizeEnd(object sender, EventArgs e)
        {
            GridExControl.MainGrid.BestFitColumns();
        }

        //이동표입력
        private void btn_itemmove_Click(object sender, EventArgs e)
        {
            TP_POPJOBLIST obj = GridBindingSource.Current as TP_POPJOBLIST;
            if (obj == null) return;
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
                newobj.StartDate = DateTime.Now;
                newobj.Itemmoveno = fm.moveno;
                ModelService.Insert(newobj);
                ModelService.Save();


                obj.JobStatus = ((int)MasterCodeEnum.POP_Status_Start).ToString();
                DataSet ds = DbRequesHandler.GetDataQury("exec SP_JOBSTATUS_UP '" + obj.WorkNo + "','" + obj.PSeq + "','" + obj.Process + "','" + obj.ItemCode + "','" + obj.JobStatus + "'");
                btn_state(Convert.ToInt32(obj.JobStatus));
            }
            GridExControl.MainGrid.BestFitColumns();
        }
    }
}

