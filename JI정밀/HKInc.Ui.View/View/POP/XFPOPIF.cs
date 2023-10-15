using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraReports.UI;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.View.View.REPORT;
using System.IO;
using DevExpress.XtraLayout.Utils;

namespace HKInc.Ui.View.View.POP
{
    public partial class XFPOPIF : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");
        string aa = "C:\\jimes\\Serial.ini";
        string ab = "C:\\jimes";
        iniFile ini;
        public XFPOPIF()
        {
            InitializeComponent();
            ini = new iniFile(ab, aa);
            SetToolbarVisible(false);
            int tt = ini.IniReadValue("Serial", "TT", aa).GetIntNullToZero();
            tt = tt != 0 ? tt : 15;
            
            timer1.Interval = 1000 *  60 * tt;
            
            lup_Process.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process).Where(p => p.UseYN == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Machine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            //List<TN_STD1100> tempArr = ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList();
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_Item.Popup += Lup_Item_Popup;
            simpleButton2.Click += Btn_QualityAdd_Click;
            //   cb_combo.EditValue = ini.IniReadValue("Serial", "port", aa);
            try
            {
                lup_Machine.EditValue = ini.IniReadValue("Serial", "mc", aa).GetNullToEmpty();
                lup_Process.EditValue = ini.IniReadValue("Serial", "process", aa).GetNullToEmpty();
                lup_Item.EditValue = ini.IniReadValue("Serial", "item", aa).GetNullToEmpty();
                hkrS2321.lsACnt = ini.IniReadValue("Serial", "Acnt", aa).GetIntNullToZero();
                hkrS2321.lsBCnt = ini.IniReadValue("Serial", "Bcnt", aa).GetIntNullToZero();
                HKInc.Service.Controls.HKRS232.lsPort = ini.IniReadValue("Serial", "port", aa).GetNullToEmpty();
                HKInc.Service.Controls.HKRS232.lsmc = ini.IniReadValue("Serial", "mc", aa).GetNullToEmpty();
            }
            catch
            {
                POP_POPUP.XFPRSSET f = new POP_POPUP.XFPRSSET();
                if (DialogResult.OK == f.ShowDialog())
                {
                    lup_Machine.EditValue = ini.IniReadValue("Serial", "mc", aa).GetNullToEmpty();
                    lup_Process.EditValue = ini.IniReadValue("Serial", "process", aa).GetNullToEmpty();
                    lup_Item.EditValue = ini.IniReadValue("Serial", "item", aa).GetNullToEmpty();
                    hkrS2321.lsACnt = ini.IniReadValue("Serial", "Acnt", aa).GetIntNullToZero();
                    hkrS2321.lsBCnt = ini.IniReadValue("Serial", "Bcnt", aa).GetIntNullToZero();
                    HKInc.Service.Controls.HKRS232.lsPort = ini.IniReadValue("Serial", "port", aa).GetNullToEmpty();
                    HKInc.Service.Controls.HKRS232.lsmc = ini.IniReadValue("Serial", "mc", aa).GetNullToEmpty();
                }
            }
            dataload();
        }

        private void Lup_Item_Popup(object sender, EventArgs e)
        {
            XFCNUMPAD keypad = new XFCNUMPAD();
            keypad.ShowDialog();
           // tx_qc.Text = keypad.returnval;
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (simpleButton1.Text != "시작") return;
                POP_POPUP.XFPRSSET f = new POP_POPUP.XFPRSSET();
            if (DialogResult.OK == f.ShowDialog())
            {
                lup_Machine.EditValue = ini.IniReadValue("Serial", "mc", aa).GetNullToEmpty();
                lup_Process.EditValue = ini.IniReadValue("Serial", "process", aa).GetNullToEmpty();
                lup_Item.EditValue = ini.IniReadValue("Serial", "item", aa).GetNullToEmpty();

                HKInc.Service.Controls.HKRS232.lsPort = ini.IniReadValue("Serial", "port", aa).GetNullToEmpty();
                HKInc.Service.Controls.HKRS232.lsmc = ini.IniReadValue("Serial", "mc", aa).GetNullToEmpty();
                int tt = ini.IniReadValue("Serial", "TT", aa).GetIntNullToZero();
                tt = tt != 0 ? tt : 15;

                timer1.Interval = 1000 * 60 * tt;
                dataload();
            }

        }
        /// <summary>
        /// 품질등록
        /// </summary>
        private void Btn_QualityAdd_Click(object sender, EventArgs e)
        {
            string item = lup_Item.EditValue.GetNullToEmpty();
            string mc = lup_Machine.EditValue.GetNullToEmpty();
            string process = lup_Process.EditValue.GetNullToEmpty();
            string llot = tx_ProductLotNo.EditValue.GetNullToEmpty();
            string workno = tx_workno.EditValue.GetNullToEmpty();

            //       string workno,string processcode,string item,string mc,string lotno)
            if (llot == "") { MessageBox.Show("LOT가 생성되지 않았습니다.");return; }

            //var inspectionForm = new POP_POPUP.XPFINSPECTION_V2(obj, tx_ProductLotNo.EditValue.GetNullToEmpty()); // 20210619 오세완 차장 초중종 로직 개선 버전
            var inspectionForm = new POP_POPUP.XPFQCIN(workno, process, item,  "",tx_ProductLotNo.EditValue.GetNullToEmpty(),"IF"); //JI버전

            if (inspectionForm.ShowDialog() != DialogResult.OK) { }
            else
                ActRefresh();

            LogFactory.GetMenuEventService().UpdateMenuEventLog(9999, "barButtonAdd");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (simpleButton1.Text == "시작")
            {
                if (tx_workno.EditValue.GetNullToEmpty() == "") { MessageBox.Show("작업지시 없이 시작할수 없습니다."); return; }
                hkrS2321.Open();
                if (hkrS2321.lsportstate == "Open")
                {
                    simpleButton1.ImageOptions.Image = IconImageList.GetIconImage("arrows/pause");
                    simpleButton1.Text = "중지";
                    lb_state.AppearanceItemCaption.BackColor = Color.Blue;
                    timer1.Start();
                }
            }
            else
            {
                hkrS2321.Close();
                if (hkrS2321.lsportstate == "Close")
                {
                    simpleButton1.ImageOptions.Image = IconImageList.GetIconImage("arrows/play");
                    simpleButton1.Text = "시작";
                    lb_state.AppearanceItemCaption.BackColor = Color.White;
                    timer1.Stop();
                }

            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            dataload();

        }

        private void dataload()
        {
            string item = lup_Item.EditValue.GetNullToEmpty();
            string mc = lup_Machine.EditValue.GetNullToEmpty();
            string process = lup_Process.EditValue.GetNullToEmpty();
            tx_workno.Text = "";
            tx_sumqty.Text = "";
            tx_ProductLotNo.Text = "";
            pic_DesignFileName.EditValue = null;
            pic_WorkStandardDocument.EditValue = null;
            if (item == "") { MessageBox.Show("품목은 필수조건입니다."); return; }
            if (mc == "") { MessageBox.Show("설비는 필수조건입니다."); return; }
            if (process == "") { MessageBox.Show("공정은 필수조건입니다."); return; }
            string dt = DateTime.Today.ToString("yyyy-MM-dd");

          
            string sql = "exec SP_GET_IFLIST '" + item + "','" + process + "','" + mc + "','" + dt + "'";
            DataSet ds = DbRequestHandler.GetDataQury(sql);
            if (ds== null) { MessageBox.Show("해당 작업지시가 없습니다."); return; }
            if (ds.Tables[0].Rows.Count == 0) { MessageBox.Show("해당 작업지시가 없습니다."); return; }
            tx_ProductLotNo.Text = ds.Tables[0].Rows[0][4].GetNullToEmpty();
            tx_workno.Text = ds.Tables[0].Rows[0][0].GetNullToEmpty();
            tx_sumqty.Text = ds.Tables[0].Rows[0][5].GetNullToEmpty();
            tx_seq.Text = ds.Tables[0].Rows[0][6].GetNullToEmpty();
            tx_cust.Text = ds.Tables[0].Rows[0][7].GetNullToEmpty();
            var Jobstd = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == item && p.ProcessCode == process).LastOrDefault();

            pic_WorkStandardDocument.EditValue = Jobstd == null ? null : Jobstd.FileData;

            //도면
            var DomapObj = ModelService.GetChildList<TN_STD1600>(p => p.ItemCode == item).OrderBy(p => p.Seq).LastOrDefault();

            pic_DesignFileName.EditValue = DomapObj == null ? null : DomapObj.DesignMap;
          
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (tx_ProductLotNo.EditValue.GetNullToEmpty() == "")
            {
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var WorkNo = new SqlParameter("@WorkNo", tx_workno.EditValue.GetNullToEmpty());
                    var MachineCode = new SqlParameter("@MachineCode", lup_Machine.EditValue.GetNullToEmpty());
                    var ItemCode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
                    var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                    //작업지시투입정보 INSERT

                    //productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO @WorkNo, @MachineCode, @ItemCode, @ProcessCode, @LoginId",
                    //    WorkNo, MachineCode, ItemCode, ProcessCode, LoginId).SingleOrDefault();

                    var productLotNo = context.Database.SqlQuery<string>("USP_INS_PRODUCT_LOT_NO_IF @WorkNo, @MachineCode, @ItemCode, @LoginId",
                           WorkNo, MachineCode, ItemCode, LoginId).SingleOrDefault();
                    tx_ProductLotNo.EditValue = productLotNo.ToString();
                }
            }
            string _plot = tx_ProductLotNo.EditValue.GetNullToEmpty();
            string _item = lup_Item.EditValue.GetNullToEmpty();
            string _proc = lup_Process.EditValue.GetNullToEmpty();
            string _mc = lup_Machine.EditValue.GetNullToEmpty();
            string _seq = tx_seq.EditValue.GetNullToEmpty();
            string _workno = tx_workno.EditValue.GetNullToEmpty();
            string _cust = tx_cust.EditValue.GetNullToEmpty();
            decimal? qty = hkrS2321.lsACnt.GetIntNullToZero();
            if (qty != 0)
            {
                hkrS2321.lsACnt = 0;
                ini.IniWriteValue("Serial", "ACnt", "0", aa);
                TN_MPS1201 tn = ModelService.GetChildList<TN_MPS1201>(p => p.ProductLotNo == _plot && p.ProcessCode == _proc && p.WorkNo == _workno).FirstOrDefault();
                if (tn == null)
                {
                    TN_MPS1201 ntn = new TN_MPS1201();
                    ntn.WorkNo = _workno;
                    ntn.ProcessCode = _proc;
                    ntn.ProcessSeq = _seq.GetIntNullToZero();
                    ntn.ItemCode = _item;
                    ntn.MachineCode = _mc;
                    ntn.ProductLotNo = _plot;
                    ntn.CustomerCode = _cust;
                    ntn.ResultDate = DateTime.Today;
                    ntn.ResultStartDate = DateTime.Now;
                    ntn.ResultEndDate = DateTime.Now;
                    ntn.ResultSumQty = qty;
                    ntn.OkSumQty = qty;
                    ModelService.Insert(ntn);
                    TN_MPS1202 ntnd = new TN_MPS1202();
                    ntnd.WorkNo = _workno;
                    ntnd.ResultSeq = 1;
                    ntnd.ProcessCode = _proc;
                    ntnd.ProcessSeq = _seq.GetIntNullToZero();
                    ntnd.ItemCode = _item;
                    ntnd.MachineCode = _mc;
                    ntnd.ProductLotNo = _plot;
                    ntnd.CustomerCode = _cust;
                    ntnd.ResultInsDate = DateTime.Today;
                    ntnd.ResultQty = qty;
                    ntnd.OkQty = qty;
                    ModelService.InsertChild<TN_MPS1202>(ntnd);

                }
                else
                {

                    tn.ResultEndDate = DateTime.Now;
                    tn.ResultSumQty = tn.ResultSumQty.GetDecimalNullToZero() + qty;
                    tn.OkSumQty = tn.OkSumQty.GetDecimalNullToZero() + qty;
                    ModelService.Update(tn);
                    TN_MPS1202 ntn = ModelService.GetChildList<TN_MPS1202>(p => p.ResultInsDate == tn.ResultDate && p.WorkNo == tn.WorkNo
                                          && p.ProcessCode == tn.ProcessCode && p.MachineCode == tn.MachineCode && p.ResultSeq == 1 && p.ItemCode == tn.ItemCode).FirstOrDefault();
                    if (ntn != null)
                    {
                        ntn.ResultQty = ntn.ResultQty.GetDecimalNullToZero() + qty;
                        ntn.OkQty = ntn.OkQty.GetDecimalNullToZero() + qty;
                        ModelService.UpdateChild<TN_MPS1202>(ntn);
                    }
                    else
                    {
                        TN_MPS1202 ntnd = new TN_MPS1202();
                        ntnd.WorkNo = _workno;
                        ntnd.ResultSeq = 1;
                        ntnd.ProcessCode = _proc;
                        ntnd.ProcessSeq = _seq.GetIntNullToZero();
                        ntnd.ItemCode = _item;
                        ntnd.MachineCode = _mc;
                        ntnd.ProductLotNo = _plot;
                        ntnd.CustomerCode = _cust;
                        ntnd.ResultInsDate = DateTime.Today;
                        ntnd.ResultQty = ntnd.ResultQty.GetDecimalNullToZero() + qty;
                        ntnd.OkQty = ntnd.OkQty.GetDecimalNullToZero() + qty;
                        ModelService.InsertChild<TN_MPS1202>(ntnd);
                    }

                }
                TN_MPS1200 pseq = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == _workno && p.ProcessCode == _proc).FirstOrDefault();
                if (pseq != null)
                {
                    if (pseq.ProcessSeq.GetIntNullToZero() == 1)
                    {
                        string MSG = string.Empty;
                        try
                        {
                            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                            {
                                var WorkNo = new SqlParameter("@WorkNo", _workno);
                                var MachineCode = new SqlParameter("@MachineCode", _mc);
                                var ItemCode = new SqlParameter("@ItemCode", _item);
                                var ProcessCode = new SqlParameter("@ProcessCode", _proc);
                                var ProductLotNo = new SqlParameter("@ProductLotNo", _plot);

                                var ResultQty = new SqlParameter("@ResultQty", qty);
                                var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                                MSG = context.Database.SqlQuery<string>("USP_INS_AUTO @WorkNo, @MachineCode, @ItemCode, @ProcessCode, @ProductLotNo, @ResultQty, @LoginId",
                                    WorkNo, MachineCode, ItemCode, ProcessCode, ProductLotNo, ResultQty, LoginId).SingleOrDefault();
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
                ModelService.Save();

                dataload();
            }
            timer1.Start();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            HKInc.Service.Controls.VKeybord.showKeyboard();
            HKInc.Service.Controls.VKeybord.moveWindow(0, 0, 1024, 350);
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            HKInc.Service.Controls.VKeybord.hideKeyboard();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            POP_POPUP.XPFANDON f = new POP_POPUP.XPFANDON(tx_workno.EditValue.GetNullToEmpty(),
                lup_Item.EditValue.GetNullToEmpty(), lup_Process.EditValue.GetNullToEmpty(), lup_Machine.EditValue.GetNullToEmpty());
            f.ShowDialog();
        }

        /// <summary>
        /// 작업표준서 이미지 더블클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_WorkStandardDocument_DoubleClick(object sender, EventArgs e)
        {
            if (pic_WorkStandardDocument.EditValue == null) return;
            var imgForm = new POP_POPUP.XPFPOPIMG(LabelConvert.GetLabelText("WorkStandardDocument"), pic_WorkStandardDocument.EditValue);
            imgForm.ShowDialog();
        }

        /// <summary>
        /// 도면 이미지 더블클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_DesignFileName_DoubleClick(object sender, EventArgs e)
        {
            if (pic_DesignFileName.EditValue == null) return;
            var imgForm = new POP_POPUP.XPFPOPIMG(LabelConvert.GetLabelText("DesignFileName"), pic_DesignFileName.EditValue);
            imgForm.ShowDialog();
        }

    }
}
