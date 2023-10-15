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
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;

namespace HKInc.Ui.View.POP_Popup1
{
    public partial class XPFQTYIN : XtraForm
    {
        IService<TN_MPS1401> ModelService = (IService<TN_MPS1401>)ProductionFactory.GetDomainService("TN_MPS1401");
        IService<TN_MPS1404> ModelServiceDTL = (IService<TN_MPS1404>)ProductionFactory.GetDomainService("TN_MPS1404");
        protected BindingSource ModelBindingSource = new BindingSource();
        protected BindingSource StopBindingSource = new BindingSource();
        DateTime? dt;
        TP_POPJOBLIST lsobj;
        public class temp_qty {
            public temp_qty() { }
            public string fail_type { get; set; }
            public string fail_name { get; set; }
            public int fail_qty { get; set; }
            public string UpdateId { get; set; }        
            public Nullable<System.DateTime> UpdateTime { get; set; }

        }
        public XPFQTYIN()
        {
            InitializeComponent();
        }
        public XPFQTYIN(TP_POPJOBLIST obj,string lotno)
        {
            InitializeComponent();
            gridEx2.SetToolbarVisible(false);
            gridEx2.MainGrid.MainView.RowHeight = 30;
            gridEx2.MainGrid.SetGridFont(this.gridEx2.MainGrid.MainView, new Font(DefaultFont.FontFamily, 11, FontStyle.Bold));
            gridEx2.MainGrid.AddColumn("StopDate", "일자", HorzAlignment.Center, false);
            gridEx2.MainGrid.AddColumn("MachineCode", "설비", HorzAlignment.Center, false);
            gridEx2.MainGrid.AddColumn("StopCode", "Причина неработоспособности", HorzAlignment.Center, true);
            gridEx2.MainGrid.AddColumn("StopStarttime", "Время начала", HorzAlignment.Center, true);
            gridEx2.MainGrid.AddColumn("StopEnddate", "Время окончания", HorzAlignment.Center, true);
            gridEx2.MainGrid.AddColumn("DdifM", "время простоя", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");

            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MainView.RowHeight = 30;
            gridEx1.MainGrid.SetGridFont(this.gridEx1.MainGrid.MainView, new Font(DefaultFont.FontFamily, 11, FontStyle.Bold));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.AddColumn("fail_type", "Плохой код");
            gridEx1.MainGrid.AddColumn("fail_name", "Плохой тип");
            gridEx1.MainGrid.AddColumn("fail_qty", "Количества");
            lsobj = obj;
            if (lotno == "")
            {
                TN_MPS1401 lot = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.Process).OrderByDescending(o => o.Seq).FirstOrDefault();
                lotno = lot.LotNo;
                dt = lot.ResultDate;
            }
         
                tx_lot.Text = lotno;
            
            TN_MPS1401 tn = ModelService.GetList(p =>p.WorkNo==obj.WorkNo&&p.ProcessCode==obj.Process&& p.LotNo == lotno).OrderBy(o => o.LotNo).FirstOrDefault();

            tx_totqty.Text = tn.ResultQty.ToString();
            tx_totokqty.Text = tn.OkQty.ToString();
            tx_totbad.Text = tn.FailQty.ToString();
            lupWork.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
            lup_machine.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y"&&p.Temp==obj.Process).ToList());
            lup_fail.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.QCFAIL));
            lup_stop.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.STOPTYPE, "", "", ""));
            if (lsobj.MachineCode.GetNullToEmpty() != "")
            {
                lup_machine.EditValue = lsobj.MachineCode;
            }
            ModelBindingSource.DataSource = new List<temp_qty>();// ModelService.GetChildList<temp_qty>(p => 1 == 1).ToList();
            gridEx1.DataSource = ModelBindingSource;
            gridEx1.MainGrid.BestFitColumns();
            StopBindingSource.DataSource = ModelService.GetChildList<TN_MPS1600>(p => p.StopDate == DateTime.Today && p.MachineCode == lsobj.MachineCode).ToList();
            gridEx2.DataSource = StopBindingSource;
            gridEx2.MainGrid.BestFitColumns();
        }
        private void tx_qty_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCNUMPAD keypad = new XFCNUMPAD();
            keypad.ShowDialog();
            tx_qty.Text = keypad.returnval;
            
            //tx_totqty.Text = (Convert.ToInt32(tx_totqty.Text) + Convert.ToInt32(tx_qty.Text) + Convert.ToInt32(tx_bad.Text)).ToString();
        }

       

        //private void tx_bad_Click(object sender, EventArgs e)
        //{
        //    if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
        //    XFCKEYPAD keypad = new XFCKEYPAD();
        //    keypad.ShowDialog();
        //    tx_bad.Text = keypad.returnval;
        //}

        private void simpleButton2_Click(object sender, EventArgs e)
        {


            int qty = tx_qty.EditValue.ToString() == "" ? 0 : Convert.ToInt32(tx_qty.EditValue);

           int badqty = tx_sumbad.EditValue.GetIntNullToZero();
            if (lupWork.EditValue.GetNullToEmpty() == "")
            {
                MessageBox.Show("Пожалуйста, выберите работника.");
                return;
            }
            //if (badqty > 0 && lup_fail.EditValue.GetNullToEmpty() == "")
            //{
            //    MessageBox.Show("불량유형을 선택하세요.");
            //    return;

            //}

            TN_MPS1401 tn = ModelService.GetList(p =>p.WorkNo==lsobj.WorkNo&&p.ProcessCode==lsobj.Process && p.LotNo == tx_lot.EditValue.ToString()).OrderBy(o => o.LotNo).FirstOrDefault();
            tn.ResultQty = Convert.ToInt32(tn.ResultQty) + qty+ badqty;
            tn.OkQty =Convert.ToInt32(tn.OkQty) + qty;
            tn.FailQty =Convert.ToInt32(tn.FailQty)+ badqty;
            tn.EndDate = DateTime.Now;
            if (dt == null)
            {
                tn.ResultDate = DateTime.Today;
            }
            else
            {
                tn.ResultDate = dt;
            }
            tn.WorkId = lupWork.EditValue.ToString();
            ModelService.Update(tn);


            //TN_MPS1405 tn1405 = new TN_MPS1405();
            //tn1405.WorkDate = Convert.ToDateTime(lsobj.WorkDate);
            //tn1405.WorkNo = lsobj.WorkNo;
            //tn1405.Seq = DbRequesHandler.GetRowCount("exec SP_MPS1405_CNT '" + lsobj.WorkDate + "','" + lsobj.WorkNo + "','" + lsobj.Process + "'") == 0 ? 1 : DbRequesHandler.GetRowCount("exec SP_MPS1405_CNT '" + lsobj.WorkDate + "','" + lsobj.WorkNo + "','" + lsobj.Process + "'") + 1;
            //tn1405.ProcessCode = lsobj.Process;
            //tn1405.LotNo = tx_lot.EditValue.ToString();
            //if (dt == null)
            //{
            //    tn1405.ResultDate = DateTime.Today;
            //}
            //else { tn1405.ResultDate = Convert.ToDateTime(dt); }
            //tn1405.OkQty = qty;
            //tn1405.WorkId = lupWork.EditValue.GetNullToEmpty();
            //tn1405.WorkingTime = tx_minute.Text.GetNullToEmpty();
            //tn1405.MachineCode = lup_machine.EditValue.GetNullToEmpty();
            //tn1405.Pseq = lsobj.PSeq;
            //ModelService.InsertChild<TN_MPS1405>(tn1405);
            int SUMQTY = qty + badqty;
            int seq = DbRequesHandler.GetRowCount("exec SP_MPS1405_CNT '" + lsobj.WorkDate + "','" + lsobj.WorkNo + "','" + lsobj.Process + "'") == 0 ? 1 : DbRequesHandler.GetRowCount("exec SP_MPS1405_CNT '" + lsobj.WorkDate + "','" + lsobj.WorkNo + "','" + lsobj.Process + "'") + 1;
            string sql = "insert into tn_mps1405t( [WORK_DATE]      ,[WORK_NO]      ,[SEQ]      ,[PROCESS_CODE]      ,[P_SEQ] "
                       + "  ,[LOT_NO],[RESULT_DATE],[OK_QTY],[WORK_ID],[MACHINE_CODE]  "
                       + "  ,[WORKING_TIME],[RESULT_QTY],[FAIL_QTY],[INS_DATE],[INS_ID]) values(";
            string sql1 = "'" + lsobj.WorkDate.ToString().Substring(0, 10) + "','" + lsobj.WorkNo + "','" + seq + "','" + lsobj.Process + "','" + lsobj.PSeq + "','" + tx_lot.EditValue.ToString() + "','" + DateTime.Today.ToString().Substring(0, 10) + "','" + qty + "','" + lupWork.EditValue.GetNullToEmpty() + "', '" + lup_machine.EditValue.GetNullToEmpty() + "',";
            if (tx_minute.Text.GetNullToEmpty() == "") { sql1 += "'Null'"; }
            else { sql1 += "'" + tx_minute.Text.GetNullToEmpty() + "'";}


           
            string sql2= ",'" + SUMQTY + "', '" + badqty + "', getdate() ,'"+ lupWork.EditValue.GetNullToEmpty() + "') ";
               int s = DbRequesHandler.SetDataQury(sql+sql1+sql2);
            if (badqty > 0)
            {                
                GridView gv = gridEx1.MainGrid.MainView as GridView;
                for (int i = 0; i < gv.RowCount; i++)
                {
                    TN_MPS1404 nobj = new TN_MPS1404()
                    {
                        WorkDate = tn.WorkDate,
                        ResultDate = tn.ResultDate,
                        WorkNo = tn.WorkNo,
                        Seq = DbRequesHandler.GetRowCount("exec SP_MPS1404_CNT '" + tn.WorkDate + "','" + tn.WorkNo + "','" + tn.ProcessCode + "'") == 0 ? 1 : DbRequesHandler.GetRowCount("exec SP_MPS1404_CNT '" + tn.WorkDate + "','" + tn.WorkNo + "','" + tn.ProcessCode + "'") + 1,
                        ProcessCode = tn.ProcessCode,
                        LotNo = tn.LotNo,
                        FailQty = gv.GetRowCellValue(i, gv.Columns[2]).GetIntNullToZero(),
                       FaleType = gv.GetRowCellValue(i, gv.Columns[0]).GetNullToEmpty(),
                        ItemCode = lsobj.ItemCode

                    };
                    ModelServiceDTL.Insert(nobj);
                    ModelServiceDTL.Save();
                }
                   
                
            }
            ModelService.Save();
            

            this.Close();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            GridView gv = gridEx1.MainGrid.MainView as GridView;
            string fcode = "";
            int row = 0;

            for (int i = 0; i<gv.RowCount; i++)
            {
                fcode = gv.GetRowCellValue(i, gv.Columns[0]).GetNullToEmpty();
                if (fcode == lup_fail.EditValue.GetNullToEmpty())
                {
                    row = i+1;
                }

            }

            if (row == 0)
            {
                temp_qty tn = new temp_qty()
                { fail_type = lup_fail.EditValue.GetNullToEmpty(), fail_name = lup_fail.SelectedText, fail_qty = Convert.ToInt32(tx_bad.Text) };
                ModelBindingSource.Add(tn);
                gridEx1.MainGrid.BestFitColumns();
            }
            else {

                int qty = gv.GetRowCellValue(row-1, gv.Columns[2]).GetIntNullToZero();
                gv.SetRowCellValue(row-1, gv.Columns[2], qty + Convert.ToInt32(tx_bad.Text));

            }
            int qty2 = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                qty2 += gv.GetRowCellValue(i, gv.Columns[2]).GetIntNullToZero();

            }
            tx_sumbad.Text = qty2.ToString();
            tx_bad.Text = "0";
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ModelBindingSource.RemoveCurrent();
            GridView gv = gridEx1.MainGrid.MainView as GridView;
            int qty2 = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                qty2 += gv.GetRowCellValue(i, gv.Columns[2]).GetIntNullToZero();

            }
            tx_sumbad.Text = qty2.ToString();
        }

        private void tx_bad_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCKEYPAD keypad = new XFCKEYPAD();
            keypad.ShowDialog();
            tx_bad.Text = keypad.returnval;
        }

        private void tx_minute_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            //XFCKEYPAD keypad = new XFCKEYPAD();
            //keypad.ShowDialog();
            FWORKTIME keypad = new FWORKTIME("1");
            keypad.ShowDialog();
            tx_minute.Text = keypad.returnval;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (lup_machine.EditValue.GetNullToEmpty() == "") return;
            if (textEdit1.EditValue.GetNullToEmpty() == "") return;
            if (textEdit2.EditValue.GetNullToEmpty() == "") return;
            if (lup_stop.EditValue.GetNullToEmpty() == "") return;
            TN_MPS1600 obj = new TN_MPS1600();
            obj.StopDate = DateTime.Today;
            obj.StopSeq = DbRequesHandler.GetRequestNumber("STOP");
            obj.MachineCode = lup_machine.EditValue.GetNullToEmpty();
            obj.StopCode = lup_stop.EditValue.GetNullToEmpty();
            obj.StopStarttime = Convert.ToDateTime(textEdit1.EditValue.GetNullToEmpty());
            obj.StopEnddate = Convert.ToDateTime(textEdit2.EditValue.GetNullToEmpty());
            StopBindingSource.Add(obj);
            ModelService.InsertChild<TN_MPS1600>(obj);
            gridEx2.MainGrid.BestFitColumns();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            TN_MPS1600 obj = StopBindingSource.Current as TN_MPS1600;
            StopBindingSource.RemoveCurrent();
            ModelService.RemoveChild<TN_MPS1600>(obj);
        }

        private void textEdit1_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            FWORKTIME key = new FWORKTIME("1");
            key.ShowDialog();
            textEdit1.Text = DateTime.Today.ToString("yyyy-MM-dd") + " " + key.returnval1;
        }

        private void textEdit2_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            FWORKTIME key = new FWORKTIME("1");
            key.ShowDialog();
            textEdit2.Text = DateTime.Today.ToString("yyyy-MM-dd") + " " + key.returnval1;
        }
    }
}
