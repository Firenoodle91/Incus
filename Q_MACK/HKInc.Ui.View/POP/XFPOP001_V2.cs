using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Service;
using DevExpress.Utils;
using System.IO;
using System.Collections.Generic;
using HKInc.Utils.Enum;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Helper;
using HKInc.Service.Controls;

namespace HKInc.Ui.View.POP
{
    public partial class XFPOP001_V2 : HKInc.Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1401> ModelService = (IService<TN_MPS1401>)ProductionFactory.GetDomainService("TN_MPS1401");
        private string proc;
        iniFile ini;
        string aa = "C:\\qmack\\Serial.ini";
        string ab = "C:\\qmack";
        //string aa = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Serial.ini";
        //string ab = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        HKRS232 rsobj;
        #endregion

        public XFPOP001_V2()
        {
            InitializeComponent();
            #region tp보고용 로그 기록
            LogFactory.GetLoginLogService().SetLoginLog(DateTime.Now);
            MenuOpenLogService.SetOpenMenuLog(DateTime.Now, 9999);
            #endregion

            inicombo();
            SetToolbarVisible(false);
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            GridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            btn_state(0);

            ini = new iniFile(aa, ab);
            rsobj = hkrS2321;
            //rsobj.lsACnt = ini.IniReadValue("Serial", "ACnt", aa).GetIntNullToZero();
            //rsobj.lsBCnt = ini.IniReadValue("Serial", "BCnt", aa).GetIntNullToZero();

            timer1.Interval = 10000;
            timer1.Start();
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

        private void inicombo()
        {
            lup_Process.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process));
            lup_Item.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != MasterCodeSTR.Topcategory_Material && 
                                                                                                        p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList());
            lup_Process.SelectedIndex = GlobalVariable.ProcessCode;
            lup_Mc.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList());
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

            GridExControl.MainGrid.AddColumn("JobStatusName", "작업상태");
            GridExControl.MainGrid.AddColumn("EMType", LabelConvert.GetLabelText("EMType"));
            GridExControl.MainGrid.AddColumn("WorkDate","지시일");
            GridExControl.MainGrid.AddColumn("WorkNo", "지시번호");
            GridExControl.MainGrid.AddColumn("CustomerName", "고객사");
            GridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            GridExControl.MainGrid.AddColumn("ItemNm", "품번");
            GridExControl.MainGrid.AddColumn("ItemNm1", "품명");
            GridExControl.MainGrid.AddColumn("ProcessName", "공정");
            GridExControl.MainGrid.AddColumn("MachineName", "설비");
            //GridExControl.MainGrid.AddColumn("MachineCodeName", "설비");        // 오타 수정         2022-07-07 김진우
            GridExControl.MainGrid.AddColumn("PlanQty", "계획수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Memo", "비고");
            GridExControl.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("EMType", "N");
        }

        /// <summary>
        /// 20220222 오세완 차장 작업지시 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataLoad();
            GridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataLoad()
        {
            #region grid focus 추가
            var keyFieldName = "RowId";
            object keyValue = null;
            int currentRow = 0;
            if (gridEx1.MainGrid.MainView.RowCount > 0)
            {
                currentRow = gridEx1.MainGrid.MainView.FocusedRowHandle > 0 ? gridEx1.MainGrid.MainView.FocusedRowHandle : 0;
                keyValue = gridEx1.MainGrid.MainView.GetRowCellValue(currentRow, keyFieldName);
            }
            #endregion
            GridExControl.MainGrid.Clear();
            InitCombo();
            InitRepository();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Process = new SqlParameter("@PROCESS", lup_Process.EditValue.GetNullToEmpty());
                SqlParameter sp_Machinecode = new SqlParameter("@MACHINE_CODE", lup_Mc.EditValue.GetNullToEmpty());
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", lup_Item.EditValue.GetNullToEmpty());
                SqlParameter sp_Workno = new SqlParameter("@WORK_NO", tx_workno.EditValue.GetNullToEmpty());

                var vResult = context.Database.SqlQuery<TP_XFPOP1000_V2_LIST>("USP_GET_XFPOP1000_V2_LIST @PROCESS, @MACHINE_CODE, @ITEM_CODE, @WORK_NO",
                    sp_Process, sp_Machinecode, sp_Itemcode, sp_Workno).ToList();

                if (vResult != null)
                    GridBindingSource.DataSource = vResult.OrderBy(o => o.PSeq).OrderBy(o1 => o1.Eord).ToList();
                else
                    GridBindingSource.Clear();
            }
            
            GridExControl.DataSource = GridBindingSource;

            #region grid foucs 불러오기
            if (string.IsNullOrEmpty(keyFieldName) || keyValue == null)
                gridEx1.MainGrid.MainView.FocusedRowHandle = currentRow;
            else
                gridEx1.MainGrid.MainView.FocusedRowHandle = gridEx1.MainGrid.MainView.LocateByValue(keyFieldName, keyValue);
            #endregion

            GridExControl.MainGrid.BestFitColumns();
            rowchange();
            // 20220428 오세완 차장 timer가 DbUpdateConcurrencyException
            //timer1.Start();

            // 20220224 오세완 차장 TP보고용 로그 기록 추가 
            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "Refresh");
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            rowchange();
        }

        private void rowchange()
        {
            TP_XFPOP1000_V2_LIST obj = GridBindingSource.Current as TP_XFPOP1000_V2_LIST;
            if (obj == null)
                return;

            tx_lotno.Text = "";
            tx_makeqty.Text = "0";
            tx_okqty.Text = "0";
            tx_badqty.Text = "0";
            tx_totok.Text = "0";
           
            btn_state(Convert.ToInt32(obj.JobStatus));
            ModelService.ReLoad();
        
            TN_MPS1401 tn1401 = ModelService.GetList(p => p.WorkNo == obj.WorkNo && 
                                                          p.ProcessCode == obj.Process ).OrderBy(o => o.Seq).LastOrDefault();
            if (tn1401 != null)
            {
                tx_lotno.Text = tn1401.LotNo;
                tx_makeqty.Text = tn1401.ResultQty.GetNullToEmpty();
                tx_okqty.Text = tn1401.OkQty.GetNullToEmpty();
                tx_badqty.Text = tn1401.FailQty.GetNullToEmpty();

                DateTime dt_Yesterday = DateTime.Today.AddDays(-1); // 20220314 오세완 차장 안하면 오류 생김
                List<TN_MPS1405> tempArr = ModelService.GetChildList<TN_MPS1405>(p => p.ResultDate == dt_Yesterday &&
                                                                                      p.WorkNo == tn1401.WorkNo &&
                                                                                      p.ProcessCode == tn1401.ProcessCode).ToList();

                if (tempArr != null)
                    if (tempArr.Count > 0)
                    {
                        int iPrev_Qty = tempArr.Sum(p => p.ResultQty).GetIntNullToZero();
                        string sPrev_Qty = "전일실적 : " + iPrev_Qty.ToString("#,###");
                        tx_preqty.Text = sPrev_Qty;
                    }

                List<TN_MPS1401> tempArr1 = ModelService.GetList(p => p.WorkNo == tn1401.WorkNo &&
                                                                      p.ProcessCode == tn1401.ProcessCode);
                if (tempArr1 != null)
                    if (tempArr1.Count > 0)
                    {
                        int iTot_Ok = (int)tempArr1.Sum(s => s.OkQty);
                        tx_totok.Text = iTot_Ok.ToString("#,###");
                    }
            }
            else
            {
                tx_lotno.Text = "";
                tx_makeqty.Text = "0";
                tx_okqty.Text = "0";
                tx_badqty.Text = "0";
            }

            pe_jobstd.EditValue = null;
            TN_MPS1000 mps1000 = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == obj.ItemCode &&
                                                                            p.ProcessCode == obj.Process &&
                                                                            p.UseYn == "Y").FirstOrDefault();
            if(mps1000 != null)
            {
                string sFilename = mps1000.WorkStandardUrl.GetNullToEmpty();
                if(sFilename != "")
                {
                    byte[] bImg_Arr = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + sFilename);
                    if(bImg_Arr != null)
                        if(bImg_Arr.Count() > 0)
                        {
                            pe_jobstd.EditValue = bImg_Arr;
                        }
                }
            }

            pe_domap.EditValue = null;
            TN_STD1600 std1600 = ModelService.GetChildList<TN_STD1600>(p => p.ItemCode == obj.ItemCode).OrderBy(p => p.Seq).LastOrDefault();
            if(std1600 != null)
            {
                string sDesign_filename = std1600.DesignFileUrl.GetNullToEmpty();
                if(sDesign_filename != "")
                {
                    byte[] bDesign_imgarr = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + sDesign_filename);
                    if (bDesign_imgarr != null)
                        if (bDesign_imgarr.Count() > 0)
                            pe_domap.EditValue = bDesign_imgarr;
                }
            }
        }

        /// <summary>
        /// 20220222 오세완 차장 작업표준서 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pe_jobstd_Click(object sender, EventArgs e)
        {
            TP_XFPOP1000_V2_LIST obj = GridBindingSource.Current as TP_XFPOP1000_V2_LIST;
            if (obj == null)
                return;

            if (pe_jobstd.EditValue == null)
                return;

            TN_MPS1000 mps1000 = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == obj.ItemCode &&
                                                                            p.ProcessCode == obj.Process &&
                                                                            p.UseYn == "Y").FirstOrDefault();
            if (mps1000 != null)
            {
                if(mps1000.WorkStantadnm.IndexOf("pdf") > -1)
                {
                    byte[] bArr = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + mps1000.WorkStandardUrl);
                    if(bArr != null)
                        if(bArr.Count() > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(bArr))
                            {
                                POP_Popup.XPFPOPPDF form = new POP_Popup.XPFPOPPDF(ms);
                                form.ShowDialog();
                            }
                        }
                }
                else
                {
                    POP_Popup.XPFPOPIMG_V2 form = new POP_Popup.XPFPOPIMG_V2("작업표준서", pe_jobstd.EditValue);
                    form.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 20220222 오세완 차장 도면 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pe_domap_DoubleClick(object sender, EventArgs e)
        {
            TP_XFPOP1000_V2_LIST obj = GridBindingSource.Current as TP_XFPOP1000_V2_LIST;
            if (obj == null)
                return;

            List<TN_STD1600> tempArr = ModelService.GetChildList<TN_STD1600>(p => p.ItemCode == obj.ItemCode).ToList();
            if(tempArr != null)
                if(tempArr.Count > 0)
                {
                    TN_STD1600 std1600 = tempArr.OrderByDescending(p => p.Seq).FirstOrDefault();
                    if(std1600 != null)
                    {
                        String filename = std1600.DesignFile.GetNullToEmpty();
                        if(std1600.DesignFile.IndexOf("pdf") > -1)
                        {
                            byte[] bArr = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + std1600.DesignFileUrl);
                            if (bArr != null)
                                if (bArr.Count() > 0)
                                {
                                    using (MemoryStream ms = new MemoryStream(bArr))
                                    {
                                        POP_Popup.XPFPOPPDF form = new POP_Popup.XPFPOPPDF(ms);
                                        form.ShowDialog();
                                    }
                                }
                        }
                        else
                        {
                            POP_Popup.XPFPOPIMG_V2 form = new POP_Popup.XPFPOPIMG_V2("도면", pe_domap.EditValue);
                            form.ShowDialog();
                        }
                    }
                }
        }

        /// <summary>
        /// 20220222 오세완 차장 작업지시 그리드 1칸 하위 이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            int irow = gv.FocusedRowHandle;
            irow++;
            gv.FocusedRowHandle = irow;

        }

        /// <summary>
        /// 20220222 오세완 차장 작업지시 그리드 1칸 상위 이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    btn_qtyin.Enabled = false;
                    btn_qcin.Enabled = false;
                    btn_stopin.Enabled = true;
                    btn_end.Enabled = true;
                    btn_exit.Enabled = true;
                    break;

                case (int)MasterCodeEnum.POP_Status_Start://생산중
                    btn_start.Enabled = false;
                    btn_qtyin.Enabled = true;
                    btn_qcin.Enabled = true;
                    btn_stopin.Enabled = true;
                    btn_end.Enabled = true;
                    btn_exit.Enabled = true;
                    break;

                case (int)MasterCodeEnum.POP_Status_StopWait://일시중지
                    btn_start.Enabled = true;
                    btn_qtyin.Enabled = false;
                    btn_qcin.Enabled = false;
                    btn_stopin.Enabled = true;
                    btn_end.Enabled = false;
                    btn_exit.Enabled = true;
                    break;

                case (int)MasterCodeEnum.POP_Status_Stop://비가동
                    btn_start.Enabled = true;
                    btn_qtyin.Enabled = false;
                    btn_qcin.Enabled = false;
                    btn_stopin.Enabled = true;
                    btn_end.Enabled = false;
                    btn_exit.Enabled = true;
                    break;

                case (int)MasterCodeEnum.POP_Status_End://작업종료
                    btn_start.Enabled = false;
                    btn_qtyin.Enabled = false;
                    btn_qcin.Enabled = false;
                    btn_stopin.Enabled = false;
                    btn_end.Enabled = false;
                    btn_exit.Enabled = true;
                    break;

                default:
                    btn_start.Enabled = false;
                    btn_qtyin.Enabled = false;
                    btn_qcin.Enabled = false;
                    btn_stopin.Enabled = false;
                    btn_end.Enabled = false;
                    btn_exit.Enabled = true;
                    btn_Machinecheck.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// 작업시작
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_start_Click(object sender, EventArgs e)
        {
            // 20220224 오세완 차장 TP보고용 로그 기록 추가 
            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
            //jobstart();
            Work_Start();
        }

        /// <summary>
        /// 20220328 오세완 차장 작업시작
        /// </summary>
        private void jobstart()
        {
            TP_XFPOP1000_V2_LIST obj = GridBindingSource.Current as TP_XFPOP1000_V2_LIST;
            if (obj == null)
                return;

            if (obj.PSeq == 1)
            {
                bool bShow_Start = false;
                if (obj.JobStatus == ((int)MasterCodeEnum.POP_Status_Wait).ToString())
                {
                    // 대기 -> 시작
                    bShow_Start = true;
                }
                else if (obj.JobStatus == ((int)MasterCodeEnum.POP_Status_StopWait).ToString() || obj.JobStatus == ((int)MasterCodeEnum.POP_Status_Stop).ToString())
                {
                    // 일시정지 / 비가동 -> 시작
                    //string sSql_Current_Lotno = "exec SP_LOTMAKE1 @workno='" + obj.WorkNo + "',@date='" + DateTime.Today.ToString("yyyyMMdd") + "'";
                    // 20220425 오세완 차장 LOT추적 및 자동차감 때문에 추적 마스터 테이블을 변경하여 방법을 변경
                    string sSql_Current_Lotno = "exec USP_INS_PRODUCT_LOT_NO_MST '" + DateTime.Today.ToString("yyyy-MM-dd") + "', '" + tx_lotno.EditValue.GetNullToEmpty() + "', '" + obj.WorkNo +
                        "', '" + obj.ItemCode + "', '" + GlobalVariable.LoginId + "' ";
                    string sCurrent_Lotno = DbRequestHandler.GetCellValue(sSql_Current_Lotno, 0);
                    if(sCurrent_Lotno.GetNullToEmpty() != "")
                    {
                        string sSql_ResultMaster = "exec USP_INS_MPS1401 '" + obj.WorkNo + "', '" + obj.Process + "', '" + sCurrent_Lotno + "', " + obj.PSeq.ToString() + ", " + obj.PlanQty.ToString() + 
                            ", '" + obj.MachineCode + "', '" + GlobalVariable.LoginId + "' ";
                        int iResult = DbRequestHandler.SetDataQury(sSql_ResultMaster);

                        obj.JobStatus = ((int)MasterCodeEnum.POP_Status_Start).ToString();
                        string sSql_OrderUdate = "exec SP_JOBSTATUS_UP '" + obj.WorkNo + "', " + obj.PSeq.ToString() + ", '" + obj.Process + "', '" + obj.ItemCode + "', '" + obj.JobStatus + "'";
                        int iResult_Upd = DbRequestHandler.SetDataQury(sSql_OrderUdate);
                        btn_state(Convert.ToInt32(obj.JobStatus));
                    }
                    else
                        bShow_Start = true;
                  
                }

                if(bShow_Start)
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.Value_1, obj);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFWORK_START, param, WorkStartCallback);
                    form.ShowPopup(true);
                }
            }
            else
            {
                //부품이동표 없이 진행하는 케이즈이노텍 스타일로 변경해야 함
                if (tx_lotno.EditValue.GetNullToEmpty() == "")
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.Value_1, obj);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFWORK_START, param, WorkStartCallback);
                    form.ShowPopup(true);
                }
                else
                {
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_Start).ToString();
                    int iResult = DbRequestHandler.SetDataQury("exec SP_JOBSTATUS_UP '" + obj.WorkNo + "','" + obj.PSeq + "','" + obj.Process + "','" + obj.ItemCode + "','" + obj.JobStatus + "'");
                    btn_state(Convert.ToInt32(obj.JobStatus));
                    DataLoad();
                }
            }
        }

        /// <summary>
        /// 20220502 오세완 차장 
        /// job_start랄 다르게 최초 시작이던 일시정지 후 다음 시작이던 시작하는 설비를 기록하는 형태 
        /// </summary>
        private void Work_Start()
        {
            TP_XFPOP1000_V2_LIST obj = GridBindingSource.Current as TP_XFPOP1000_V2_LIST;
            if (obj == null)
                return;

            if (obj.PSeq == 1)
            {
                bool bShow_Start = false;
                if (obj.JobStatus == ((int)MasterCodeEnum.POP_Status_Wait).ToString())
                {
                    // 대기 -> 시작
                    bShow_Start = true;
                }
                else if (obj.JobStatus == ((int)MasterCodeEnum.POP_Status_StopWait).ToString() || obj.JobStatus == ((int)MasterCodeEnum.POP_Status_Stop).ToString())
                {
                    // 일시정지 / 비가동 -> 시작
                    bShow_Start = true;
                }

                if (bShow_Start)
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.Value_1, obj);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFWORK_START, param, WorkStartCallback);
                    form.ShowPopup(true);
                }
            }
            else
            {
                //부품이동표 없이 진행하는 케이즈이노텍 스타일로 변경해야 함
                if (tx_lotno.EditValue.GetNullToEmpty() == "")
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.Value_1, obj);
                    IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFWORK_START, param, WorkStartCallback);
                    form.ShowPopup(true);
                }
                else
                {
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_Start).ToString();
                    int iResult = DbRequestHandler.SetDataQury("exec SP_JOBSTATUS_UP '" + obj.WorkNo + "', " + obj.PSeq.ToString() + ", '" + obj.Process + "', '" + obj.ItemCode + "', '" + obj.JobStatus + "'");
                    btn_state(Convert.ToInt32(obj.JobStatus));
                    DataLoad();
                }
            }
        }

        private void WorkStartCallback(object sender, PopupArgument e)
        {
            if (e == null)
                return;

            if (!e.Map.ContainsKey(PopupParameter.Value_1))
                return;

            DialogResult dialogResult = (DialogResult)e.Map.GetValue(PopupParameter.Value_1);
            if(dialogResult == DialogResult.OK)
            {
                TP_XFPOP1000_V2_LIST prev_WorkObj = (TP_XFPOP1000_V2_LIST)e.Map.GetValue(PopupParameter.Value_2);
                if(prev_WorkObj != null)
                {
                    bool bInsert_Mps1401 = false;
                    string sLotNo1 = "";
                    string sMessage = "";
                    if (prev_WorkObj.PSeq == 1)
                    {
                        //string sSql_LotMake = "exec SP_LOTMAKE1 @workno='" + prev_WorkObj.WorkNo + "',@mccode='" + prev_WorkObj.MachineCode + "',@item='" + prev_WorkObj + "',@date='" + DateTime.Today.ToString("yyyyMMdd") + "'";
                        // 20220425 오세완 차장 LOT추적 및 자동차감 때문에 추적 마스터 테이블을 변경하여 방법을 변경
                        string sSql_LotMake = "exec USP_INS_PRODUCT_LOT_NO_MST '" + DateTime.Today.ToString("yyyy-MM-dd") + "', '" + tx_lotno.EditValue.GetNullToEmpty() + "', '" + prev_WorkObj.WorkNo +
                            "', '" + prev_WorkObj.ItemCode + "', '" + GlobalVariable.LoginId + "' ";
                        string sLot_no = DbRequestHandler.GetCellValue(sSql_LotMake, 0);
                        if (sLot_no != "")
                        {
                            bInsert_Mps1401 = true;
                            sLotNo1 = sLot_no;   
                        }
                    }
                    else
                    {
                        // 20220513 오세완 차장 선택한 작업지시에 한번도 해당 공정에서 진행하지 않은 최상위 선입선출 lotno를 이전공정을 조회하여 반환
                        string sSql_PrevLotno = "exec USP_GET_LOTNO_PREV_PROCESS '" + prev_WorkObj.WorkNo + "', '" + prev_WorkObj.Process + "', " + prev_WorkObj.PSeq.ToString();
                        string sPre_Lotno = DbRequestHandler.GetCellValue(sSql_PrevLotno, 0);
                        if(sPre_Lotno != "")
                        {
                            bInsert_Mps1401 = true;
                            sLotNo1 = sPre_Lotno;
                        }
                        else
                        {
                            sMessage = "이전 공정에서 실적이 없습니다.";
                            MessageBoxHandler.Show(sMessage);
                        }
                    }

                    if(bInsert_Mps1401)
                    {
                        string sSql_ResultMaster = "exec USP_INS_MPS1401 '" + prev_WorkObj.WorkNo + "', '" + prev_WorkObj.Process + "', '" + sLotNo1 + "', " + prev_WorkObj.PSeq.ToString() + ", " +
                                prev_WorkObj.PlanQty.ToString() + ", '" + prev_WorkObj.MachineCode + "', '" + GlobalVariable.LoginId + "' ";
                        int iResult = DbRequestHandler.SetDataQury(sSql_ResultMaster);

                        prev_WorkObj.JobStatus = ((int)MasterCodeEnum.POP_Status_Start).ToString();
                        string sSql_OrderUpdate = "exec SP_JOBSTATUS_UP '" + prev_WorkObj.WorkNo + "', " + prev_WorkObj.PSeq.ToString() + ", '" + prev_WorkObj.Process + "', '" +
                            prev_WorkObj.ItemCode + "', '" + prev_WorkObj.JobStatus + "' ";
                        int iResult_order = DbRequestHandler.SetDataQury(sSql_OrderUpdate);

                        btn_state(Convert.ToInt32(prev_WorkObj.JobStatus));
                        DataLoad();
                    }
                }
            }
        }

        /// <summary>
        /// 종료
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult req = MessageBox.Show("종료하시겠습니까?(Do you want to complete?", "경고", MessageBoxButtons.OKCancel);
            if (req == DialogResult.OK)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 실적등록
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_qtyin_Click(object sender, EventArgs e)
        {
            ProdQtyIn_V2();
            //ProdQtyin();
        }

        /// <summary>
        /// 20220310 오세완 차장 새로운 팝업 화면으로 교체
        /// </summary>
        private void ProdQtyIn_V2()
        {
            TP_XFPOP1000_V2_LIST tObj = GridBindingSource.Current as TP_XFPOP1000_V2_LIST;
            if (tObj == null)
                return;

            // 20220428 오세완 차장 DbUpdateConcurrencyException때문에 생략처리
            //timer1.Stop();
            string sProductLotNo = tx_lotno.EditValue.GetNullToEmpty();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, tObj);
            param.SetValue(PopupParameter.Value_1, sProductLotNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_DEFAULT, param, ResultAddCallback);
            form.ShowPopup(true);

            // 20220224 오세완 차장 TP보고용 로그 기록 추가 
            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
        }

        private void ResultAddCallback(object sender, PopupArgument e)
        {
            if (e == null)
                return;

            // 02000517 오세완 차장 이동표 없이 전공에서 있는 실적을 선입선출 로트번호 순대로 처리하는 로직 추가 
            TP_XFPOP1000_V2_LIST obj = GridBindingSource.Current as TP_XFPOP1000_V2_LIST;
            if(obj != null)
            {
                if(obj.PSeq > 1)
                {
                    string sSql = "exec USP_GET_CHANGE_LOTNO_BY_RESULT '" + obj.WorkNo + "', '" + obj.Process + "', " + obj.PSeq.ToString() + ", '" + tx_lotno.EditValue.GetNullToEmpty() + "' ";
                    string sResult = DbRequestHandler.GetCellValue(sSql, 0);
                    if(sResult != null)
                        if(sResult == "Y")
                        {
                            string sSql_PrevLotno = "exec USP_GET_LOTNO_PREV_PROCESS '" + obj.WorkNo + "', '" + obj.Process + "', " + obj.PSeq.ToString();
                            string sPre_Lotno = DbRequestHandler.GetCellValue(sSql_PrevLotno, 0);
                            if(sPre_Lotno != null)
                            {
                                string sSql_ResultMaster = "exec USP_INS_MPS1401 '" + obj.WorkNo + "', '" + obj.Process + "', '" + sPre_Lotno + "', " + obj.PSeq.ToString() + ", " +
                                    obj.PlanQty.ToString() + ", '" + obj.MachineCode + "', '" + GlobalVariable.LoginId + "' ";
                                int iResult = DbRequestHandler.SetDataQury(sSql_ResultMaster);
                            }
                        }
                }
            }

            rowchange();
            // 20220428 오세완 차장 DbUpdateConcurrencyException때문에 생략처리
            //timer1.Start();
        }

        /// <summary>
        /// 작업종료
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_end_Click(object sender, EventArgs e)
        {
            TP_XFPOP1000_V2_LIST obj = GridBindingSource.Current as TP_XFPOP1000_V2_LIST;
            if (obj == null)
                return;

            string sProductLotNo = tx_lotno.EditValue.GetNullToEmpty();
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.Value_1, sProductLotNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFWORKEND, param, WorkEndCallback);
            form.ShowPopup(true);
        }

        private void WorkEndCallback(object sender, PopupArgument e)
        {
            if (e == null)
                return;
            else if (!e.Map.ContainsKey(PopupParameter.Value_2)) //종료하는 작업지시 정보
                return;

            DataLoad();
            rowchange();
        }

        /// <summary>
        /// 비가동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_stopin_Click(object sender, EventArgs e)
        {
            //WorkStop_Before();
            WorkStop_V2();
        }

        /// <summary>
        /// 20220310 오세완 차장
        /// 기존 스타일
        /// </summary>
        private void WorkStop_Before()
        {
            TP_XFPOP1000_V2_LIST obj = GridBindingSource.Current as TP_XFPOP1000_V2_LIST;
            if (obj == null)
                return;

            string lsMachineCode = "";
            if (obj.MachineCode.GetNullToEmpty() == "")
            {

                POP_Popup.XPFMACHINE POP = new POP_Popup.XPFMACHINE();
                POP.ShowDialog();
                if (POP.DialogResult == DialogResult.OK)
                    lsMachineCode = POP.machine;
                else
                    return;
            }

            string cnt = DbRequestHandler.GetCellValue("exec SP_STOPCODE_UP '" + lsMachineCode + "'", 0);
            if (cnt == "0")
            {
                POP_Popup.XPFSTOP fm = new POP_Popup.XPFSTOP(lsMachineCode);
                fm.ShowDialog();
                if (fm.DialogResult == DialogResult.OK)
                {
                    obj.JobStatus = ((int)MasterCodeEnum.POP_Status_Stop).ToString();
                    DataSet ds = DbRequestHandler.GetDataQury("exec SP_JOBSTATUS_UP '" + obj.WorkNo + "','" + obj.PSeq + "','" + obj.Process + "','" + obj.ItemCode + "','" + obj.JobStatus + "'");
                    DataLoad();
                }
            }
            else
            {
                jobstart();
            }
        }

        /// <summary>
        /// 20220509 오세완 차장 
        /// 변경된 비가동 팝업 
        /// </summary>
        private void WorkStop_V2()
        {
            TP_XFPOP1000_V2_LIST WorkObj = GridBindingSource.Current as TP_XFPOP1000_V2_LIST;
            if (WorkObj != null)
            {
                var stopForm = new POP_POPUP.XPFMACHINESTOP_V2(WorkObj);
                stopForm.ShowDialog();

                DataLoad();
                rowchange();

                LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
            }
        }

        private void btn_qcin_Click(object sender, EventArgs e)
        {
            // 20220224 오세완 차장 TP보고용 로그 기록 추가 
            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
            TP_XFPOP1000_V2_LIST obj = GridBindingSource.Current as TP_XFPOP1000_V2_LIST;
            if (obj == null)
                return;

            POP_Popup.XPFINSPECTION qc = new POP_Popup.XPFINSPECTION(obj, tx_lotno.Text.GetNullToEmpty());
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

        /// <summary>
        /// 설비 I/F 추가
        /// 2022-08-23 김진우
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            try
            {
                string workno = ini.IniReadValue("Serial", "Workno", aa).GetNullToEmpty();
                string process = ini.IniReadValue("Serial", "process", aa).GetNullToEmpty();
                if (workno == "" || process == "")
                {
                    timer1.Start();
                    return;
                }

                #region 주석
                //List<TP_XFPOP1000_V2_LIST> obj = GridBindingSource.DataSource as List<TP_XFPOP1000_V2_LIST>;
                //if (obj == null) { timer1.Start(); return; }

                //string MCA = HKRS232.lsmcA;
                //string MCB = HKRS232.lsmcB;
                //string item = null;
                //int lscavity = 1, xcase = 1;

                //for (int i = 0; i < obj.Count; i++)
                //{
                //if (obj[i].JobStatus == ((int)MasterCodeEnum.POP_Status_Start).ToString())
                //{
                //  workno = obj[i].WorkNo.GetNullToEmpty();
                // process= obj[i].Process.GetNullToEmpty();
                //item = obj[i].ItemCode.GetNullToEmpty();
                //var ItemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == item
                //                                                  && string.IsNullOrEmpty(p.MoldCode) == false).FirstOrDefault();
                //if (ItemObj != null)
                //{
                //    var MoldList = ModelService.GetChildList<TN_MOLD001>(p => p.MoldCode == ItemObj.MoldCode).FirstOrDefault();
                //    if (MoldList.XCase.GetIntNullToZero() != 0)
                //    {
                //        lscavity = MoldList.XCase.GetIntNullToZero();
                //    }
                //    var KnifeList = ModelService.GetChildList<TN_KNIFE001>(p => p.KnifeCode == ItemObj.KnifeCode).FirstOrDefault();
                //    if (KnifeList.XCase.GetIntNullToZero() != 0)
                //    {
                //        xcase = KnifeList.XCase.GetIntNullToZero();
                //    }
                //}
                //string wkdt = DateTime.Today.ToString("yyyy-MM-dd");
                //DateTime resultdt = Convert.ToDateTime(wkdt);

                #region Acount

                //if (workno != "")
                //{
                //    if (rsobj.lsACnt.GetIntNullToZero() > 0)
                //    {
                //        TN_MPS1401 tn = ModelService.GetList(p => p.WorkNo == workno
                //                                               && p.ProcessCode == process)
                //                                           .OrderBy(o => o.Seq).LastOrDefault();

                //        tn.ResultQty = tn.ResultQty.GetIntNullToZero() + (rsobj.lsACnt.GetIntNullToZero());
                //        tn.OkQty = tn.OkQty.GetIntNullToZero() + (rsobj.lsACnt.GetIntNullToZero());
                //        //tn.FailQty = tn.FailQty.GetIntNullToZero() + BadList.Sum(p => p.FailQty).GetIntNullToZero();
                //        tn.EndDate = DateTime.Now;


                //        tn.WorkId = "Admin";// lupWork.EditValue.ToString();
                //        ModelService.Update(tn);
                //        //MaterialUsingQty(tn, rsobj.lsACnt.GetIntNullToZero(), 1);
                //        rsobj.lsACnt = 0;
                //        try
                //        {
                //            //string sql = "exec SP_IF_INSERT '" + wkdt + "','" + MCA + "','" + workno + "','" + process + "'";
                //            //int ii = DbRequestHandler.SetDataQury(sql);
                //        }
                //        catch { }
                //    }
                //}
                #endregion
                #region Bcount
                ////  }
                ////if (obj[i].MachineCode.GetNullToEmpty() == MCB)
                //// {
                //if (workno1 != "")
                //{
                //    if (rsobj.lsBCnt.GetIntNullToZero() > 0)
                //    {
                //        TN_MPS1401 tn1 = ModelService.GetList(p => p.WorkNo == workno1
                //                                           && p.ProcessCode == process1

                //                                       )
                //                                          .OrderBy(o => o.Seq).LastOrDefault();

                //        tn1.ResultQty = tn1.ResultQty.GetIntNullToZero() + (rsobj.lsBCnt.GetIntNullToZero() * xcase);
                //        tn1.OkQty = tn1.OkQty.GetIntNullToZero() + (rsobj.lsBCnt.GetIntNullToZero() * xcase);
                //        //tn.FailQty = tn.FailQty.GetIntNullToZero() + BadList.Sum(p => p.FailQty).GetIntNullToZero();
                //        tn1.EndDate = DateTime.Now;


                //        tn1.WorkId = "Admin";// lupWork.EditValue.ToString();
                //        ModelService.Update(tn1);
                //        MaterialUsingQty(tn1, rsobj.lsACnt.GetIntNullToZero(), 2);
                //        rsobj.lsBCnt = 0;
                //        try
                //        {
                //            string sql = "exec SP_IF_INSERT '" + wkdt + "','" + MCB + "','" + workno1 + "','" + process1 + "'";
                //            int ii = DbRequestHandler.SetDataQury(sql);
                //        }
                //        catch { }
                //    }
                //}
                #endregion
                //}
                //}
                //ModelService.Save();
                #endregion

                if (rsobj.lsACnt.GetIntNullToZero() > 0)
                {
                    TN_MPS1401 MPS1401 = ModelService.GetList(p => p.WorkNo == workno && p.ProcessCode == process).OrderBy(o => o.Seq).LastOrDefault();
                    if (MPS1401 == null) return;
                    TN_MPS1400 MPS1400 = ModelService.GetChildList<TN_MPS1400>(p => p.WorkNo == MPS1401.WorkNo && p.Process == MPS1401.ProcessCode).FirstOrDefault();
                    if (MPS1400 == null) return;
                    if (MPS1400.JobStates == ((int)MasterCodeEnum.POP_Status_Start).ToString())
                    {
                        String EndDate = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff");
                        int QTY = rsobj.lsACnt.GetIntNullToZero();                  // RS232 카운트

                        string sql = "exec USP_UPD_MPS1401_RS232 '" + workno + "','" + process + "','" + QTY + "','" + QTY + "','" + EndDate + "','" + "Admin" + "'";
                        int ii = DbRequestHandler.SetDataQury(sql);                 // MPS1401 수량변경

                        TN_MPS1405 MPS1405 = ModelService.GetChildList<TN_MPS1405>(p => p.WorkNo == MPS1401.WorkNo && p.ProcessCode == MPS1401.ProcessCode
                                             && p.LotNo == MPS1401.LotNo).OrderBy(o => o.Seq).LastOrDefault();

                        if (MPS1405 == null)                            // MPS1405 없을시 추가
                        {
                            TN_MPS1405 AddObj = new TN_MPS1405();

                            AddObj.WorkDate = DateTime.Now;
                            AddObj.WorkNo = MPS1401.WorkNo;
                            AddObj.Seq = 1;
                            AddObj.ProcessCode = MPS1401.ProcessCode;
                            AddObj.Pseq = MPS1401.ProcessTurn;
                            AddObj.LotNo = MPS1401.LotNo;
                            AddObj.ResultDate = DateTime.Now;
                            AddObj.OkQty = Convert.ToInt32(rsobj.lsACnt);
                            AddObj.ResultQty = Convert.ToInt32(rsobj.lsACnt);
                            AddObj.FailQty = 0;
                            AddObj.WorkId = "Admin";
                            AddObj.MachineCode = ini.IniReadValue("Serial", "mcA", aa).GetNullToEmpty();
                            AddObj.WorkingTime = "0";

                            ModelService.InsertChild(AddObj);
                        }
                        else                                                // MPS1405 존재시 업데이트
                        {
                            MPS1405.ResultQty = MPS1405.ResultQty + QTY;
                            MPS1405.OkQty = MPS1405.OkQty + QTY;
                            MPS1405.ResultDate = DateTime.Now;

                            ModelService.UpdateChild(MPS1405);                          // mps1405 수량변경
                        }
                        #region 주석
                        //TN_MPS1405 detailNewObj = new TN_MPS1405()
                        //{
                        //    WorkDate = MPS1401.WorkDate,
                        //    WorkNo = MPS1401.WorkNo,
                        //    ProcessCode = MPS1401.ProcessCode,
                        //    Pseq = MPS1401.ProcessTurn,
                        //    LotNo = MPS1401.LotNo,
                        //    ResultDate = DateTime.Now,
                        //    ResultQty = QTY,
                        //    OkQty = QTY,
                        //    FailQty = 0,
                        //    WorkId = "Admin",
                        //    MachineCode = MPS1401.MachineCode,
                        //    WorkingTime = "0",
                        //    //WorkingTime = spin_WorkTime.EditValue.GetNullToEmpty(),
                        //};

                        //string sSql = "exec SP_MPS1405_CNT '" + MPS1401.WorkDate.ToString() + "', '" + MPS1401.WorkNo + "', '" + MPS1401.ProcessCode + "' ";
                        //int iMaxSeq = DbRequestHandler.GetRowCount(sSql);
                        //if (iMaxSeq == 0)
                        //    detailNewObj.Seq = 1;
                        //else
                        //    detailNewObj.Seq = iMaxSeq + 1;

                        //ModelService.InsertChild<TN_MPS1405>(detailNewObj);
                        //ModelService.Save();
                        #endregion
                        rsobj.lsACnt = 0;
                        ModelService.Save();
                    }
                }
                rowchange();
                timer1.Start();
            }
            catch (Exception ex)
            {
                timer1.Start();
                rsobj.lsACnt = 0;
                MessageBoxHandler.Show(ex.ToString());
            }

            // 20220428 오세완 차장 DbUpdateConcurrencyException때문에 생략처리
            //timer1.Stop();
            //DataLoad();           // 2022-08-18 김진우 미사용 주석

        }

        /// <summary>
        /// 20220224 오세완 차장
        /// 컷팅 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            lup_Process.SelectedIndex = 0;
            proc = lup_Process.EditValue.GetNullToEmpty();
            DataLoad();
            GridExControl.MainGrid.BestFitColumns();
        }

        /// <summary>
        /// 20220224 오세완 차장
        /// 면취 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            lup_Process.SelectedIndex = 1;
            proc = lup_Process.EditValue.GetNullToEmpty();
            lup_Mc.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y" && (string.IsNullOrEmpty(proc) ? true : p.Temp == proc)).ToList());
            DataLoad();
            GridExControl.MainGrid.BestFitColumns();
        }

        
        /// <summary>
        /// 20220224 오세완 차장
        /// 검사포장 버튼 클릭 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            lup_Process.SelectedIndex = 3;
            proc = lup_Process.EditValue.GetNullToEmpty();
            lup_Mc.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y" && (string.IsNullOrEmpty(proc) ? true : p.Temp == proc)).ToList());
            DataLoad();
            GridExControl.MainGrid.BestFitColumns();
        }

        private void XFPOP001_V2_FormClosed(object sender, FormClosedEventArgs e)
        {
            #region TP보고용 로그 기록
            MenuOpenLogService.SetCloseMenuLog(DateTime.Now);
            LogFactory.GetLoginLogService().SetLogoutLog(DateTime.Now);
            #endregion
        }

        /// <summary>
        /// 20220224 오세완 차장
        /// 설비점검 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Machinecheck_Click(object sender, EventArgs e)
        {
            POP_POPUP.XPFMACHINECHECK pop = new POP_POPUP.XPFMACHINECHECK();
            pop.ShowDialog();

            // 20220224 오세완 차장 TP보고용 로그 기록 추가 
            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
        }

        /// <summary>
        /// 20220427 오세완 차장
        /// 작업지시번호 클릭 이벤트, 키오스크인 경우는 키패드 출력이 필요
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tx_workno_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad)
                return;

            var keyPad = new XFCKEYPAD();
            keyPad.ShowDialog();
            tx_workno.EditValue = keyPad.returnval;
            DataLoad();
        }

        /// <summary>
        /// 안돈
        /// 2022-07-15 김진우
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Andon_Click(object sender, EventArgs e)
        {
            TP_XFPOP1000_V2_LIST Obj = GridBindingSource.Current as TP_XFPOP1000_V2_LIST;
            if (Obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, Obj);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFANDON, param, AndonCallBack);
            form.ShowPopup(true);
        }

        private void AndonCallBack(object sender, PopupArgument e)
        {
            // X
        }

        /// <summary>
        /// 인터페이스 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_RS232_Click(object sender, EventArgs e)
        {
            TP_XFPOP1000_V2_LIST data = GridBindingSource.Current as TP_XFPOP1000_V2_LIST;

            FMCSETTING fm = new FMCSETTING(data, ini);
            fm.ShowDialog();
        }
    }
}

