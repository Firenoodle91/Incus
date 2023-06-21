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
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Class;


namespace HKInc.Ui.View.POP_Popup1
{
    public partial class XPFQCIN : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1000> ModelService = (IService<TN_QCT1000>)ProductionFactory.GetDomainService("TN_QCT1000");
        IService<TN_QCT1200> QcModelService = (IService<TN_QCT1200>)ProductionFactory.GetDomainService("TN_QCT1200");
        TP_POPJOBLIST lsobj;
        string lsLotNo;
        int rowid;
        string qctype;
        public XPFQCIN(TP_POPJOBLIST obj,string lotno)
        {
            InitializeComponent();
            GridExControl = gridEx1;
            InitGrid();
            lsobj = obj;
            lsLotNo = lotno;
            SetToolbarVisible(false);
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            QcLoad("Q03");

        }
        protected override void GridRowDoubleClicked()
        {
            
        }
        protected override void InitCombo()
        {
            lup_qc.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.QCOKNG));
            lupXseq.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.XSEQ));
            lupXseq.SelectedIndex = 0;
            lupuser.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p=>p.Active=="Y").ToList());
        }
        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TN_QCT1000 obj = GridBindingSource.Current as TN_QCT1000;
            rowid = e.FocusedRowHandle;
            if (obj.CheckProv == "QT1")
            {
                lup_qc.Focus();

            }
            else
            {
                tx_qc.Focus();
            }
        }
        protected override void InitGrid()
        {
            GridExControl.MainGrid.MainView.RowHeight = 40;
            GridExControl.MainGrid.SetGridFont(this.GridExControl.MainGrid.MainView, new Font(DefaultFont.FontFamily, 10));
            GridExControl.MainGrid.MainView.ColumnPanelRowHeight = 40;
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("CheckName", "Осмотр пункт");
            GridExControl.MainGrid.AddColumn("CheckProv", "Метод проверки");
            GridExControl.MainGrid.AddColumn("CheckStand", "спецификации");
            GridExControl.MainGrid.AddColumn("UpQuad", "квадрант");
            GridExControl.MainGrid.AddColumn("DownQuad", "Нижний предел");
            GridExControl.MainGrid.AddColumn("ChaeckFlag", "определение");
            GridExControl.MainGrid.AddColumn("X1", "X1");
            GridExControl.MainGrid.AddColumn("X2", "X2");
            GridExControl.MainGrid.AddColumn("X3", "X3");
            GridExControl.MainGrid.AddColumn("X4", "X4");
            GridExControl.MainGrid.AddColumn("X5", "X5");
            GridExControl.MainGrid.AddColumn("X6", "X6");
            GridExControl.MainGrid.AddColumn("X7", "X7");
            GridExControl.MainGrid.AddColumn("X8", "X8");
         
        }
        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckName", DbRequesHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckProv", DbRequesHandler.GetCommCode(MasterCodeSTR.QCTYPE), "Mcode", "Codename");
        }
        private void btn_self_Click(object sender, EventArgs e)
        {
            //qctype = btn_self.Tag.ToString();
            //btn_proc.Enabled = false;
            //btn_step.Enabled = false;
            //QcLoad(qctype);

        }

        private void QcLoad(string qc)
        {
            GridBindingSource.DataSource = ModelService.GetList(p => p.ItemCode == lsobj.ItemCode && (p.ProcessGu == qc || p.ProcessGu == "Q06") && p.ProcessCode == lsobj.Process && p.UseYn == "Y").ToList();
            gridEx1.DataSource = GridBindingSource;
           //MessageBox.Show(lupXseq.SelectedIndex.ToString());
            gridEx1.MainGrid.BestFitColumns();
           // MessageBox.Show(lupXseq.SelectedIndex.ToString());
           
         //   MessageBox.Show(lupXseq.SelectedIndex.ToString());
            rowid = 0;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            SetIsFormControlChanged(false);
            this.Close();
        }

        private void btn_accept_Click(object sender, EventArgs e)
        {
            acceptrun();

        }

        private void acceptrun()
        {

            GridView gv = GridExControl.MainGrid.MainView as GridView;
            string xseq = lupXseq.EditValue.GetNullToEmpty();
            
            if (gv.GetRowCellValue(rowid, gv.Columns["CheckProv"]).ToString() == "QT1")
            {
                string val = lup_qc.EditValue.GetNullToEmpty();
                gv.SetRowCellValue(rowid, gv.Columns[xseq], val);
                rowid++;
                if (rowid >= gv.RowCount)
                {
                    MessageBox.Show("Последний товар.");
                 
                    lupXseq.SelectedIndex = lupXseq.SelectedIndex + 1;
                    rowid=0;
                }
                gv.FocusedRowHandle = rowid;
            }
            else
            {
                string val = tx_qc.EditValue.GetNullToEmpty();
                gv.SetRowCellValue(rowid, gv.Columns[xseq], val);
                tx_qc.Text = "";
                rowid++;
                if (rowid >= gv.RowCount)
                {
                    MessageBox.Show("Последний товар.");
                 
                    lupXseq.SelectedIndex = lupXseq.SelectedIndex + 1;
                    rowid = 0;
                }
                gv.FocusedRowHandle = rowid;
            }
        }

        private void btn_step_Click(object sender, EventArgs e)
        {

            //qctype = btn_step.Tag.ToString();
            //btn_proc.Enabled = false;
            //btn_self.Enabled = false;
            //QcLoad(qctype);
        }

        private void btn_proc_Click(object sender, EventArgs e)
        {

            //qctype = btn_proc.Tag.ToString();
            //btn_self.Enabled = false;
            //btn_step.Enabled = false;
            //QcLoad(qctype);
        }

        private void lup_qc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string lsval = lup_qc.EditValue.GetNullToEmpty();
            if (lsval == "") return;
            acceptrun();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            int ngcnt = 0;
            string checkid = lupuser.EditValue.GetNullToEmpty();
            if(checkid=="")
            {
                MessageBox.Show("Выбор инспектора");
                return;
            }
            for (int i = 0; i < gv.RowCount; i++)
            {
                if (gv.GetRowCellValue(i, gv.Columns["ChaeckFlag"]).ToString() == "NG")
                {
                    ngcnt++;
                }
            }
            TN_QCT1200 qc = new TN_QCT1200()
            {
                //No = DbRequesHandler.GetRequestNumber(qctype),
                No = DbRequesHandler.GetRequestNumber("Q03"),
                FmeNo = "Q03",// qctype,
                              // FmeDivision = DbRequesHandler.GetCellValue("exec SP_QCSTEP_STEP '"+ qctype+"','" + lsobj.WorkDate + "','" + lsobj.WorkNo + "','" + lsobj.Process + "','" + DateTime.Today + "'", 0),
                FmeDivision = DbRequesHandler.GetCellValue("exec SP_QCSTEP_STEP 'Q03','" + lsobj.WorkDate + "','" + lsobj.WorkNo + "','" + lsobj.Process + "','" + DateTime.Today + "'", 0),
                WorkDate = lsobj.WorkDate,
                WorkNo = lsobj.WorkNo,
                ItemCode = lsobj.ItemCode,
                ProcessCode = lsobj.Process,
                CheckDate = DateTime.Today,
                CheckId = checkid,
                CheckResult = ngcnt >= 1 ? "NG" : "OK",
                LotNo=lsLotNo
                
            };
            for (int i = 0; i < gv.RowCount; i++)
            {
                TN_QCT1201 dtl = new TN_QCT1201();
               // if (gv.GetRowCellValue(i, gv.Columns["CheckVal"]).GetNullToEmpty() == "") continue;
                dtl.No = qc.No;
                dtl.Seq = qc.QCT1201List.Count == 0 ? 1 : qc.QCT1201List.Count + 1;
                dtl.FmeNo = qc.FmeNo;
                dtl.FmeDivision = qc.FmeDivision;
                dtl.ItemCode = qc.ItemCode;
                dtl.CheckName = gv.GetRowCellValue(i, gv.Columns["CheckName"]).GetNullToEmpty();
                dtl.CheckProv = gv.GetRowCellValue(i, gv.Columns["CheckProv"]).GetNullToEmpty();
                dtl.CheckStand = gv.GetRowCellValue(i, gv.Columns["CheckStand"]).GetNullToEmpty();

                dtl.UpQuad = gv.GetRowCellValue(i, gv.Columns["UpQuad"]).GetDoubleNullToZero();
                dtl.DownQuad = gv.GetRowCellValue(i, gv.Columns["DownQuad"]).GetDoubleNullToZero();
                dtl.Reading1 = gv.GetRowCellValue(i, gv.Columns["X1"]).GetNullToEmpty();
                dtl.Reading2 = gv.GetRowCellValue(i, gv.Columns["X2"]).GetNullToEmpty();
                dtl.Reading3 = gv.GetRowCellValue(i, gv.Columns["X3"]).GetNullToEmpty();
                dtl.Reading4 = gv.GetRowCellValue(i, gv.Columns["X4"]).GetNullToEmpty();
                dtl.Reading5 = gv.GetRowCellValue(i, gv.Columns["X5"]).GetNullToEmpty();
                dtl.Reading6 = gv.GetRowCellValue(i, gv.Columns["X6"]).GetNullToEmpty();
                dtl.Reading7 = gv.GetRowCellValue(i, gv.Columns["X7"]).GetNullToEmpty();
                dtl.Reading8 = gv.GetRowCellValue(i, gv.Columns["X8"]).GetNullToEmpty();
                
                dtl.Judge = gv.GetRowCellValue(i, gv.Columns["ChaeckFlag"]).GetNullToEmpty();


                qc.QCT1201List.Add(dtl);
            }

            QcModelService.Insert(qc);
            QcModelService.Save();
            SetIsFormControlChanged(false);
            this.Close();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCKEYPAD keypad = new XFCKEYPAD();
            keypad.ShowDialog();
            tx_qc.Text = keypad.returnval;
        }

        private void tx_qc_DoubleClick(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCNUMPAD keypad = new XFCNUMPAD();
            keypad.ShowDialog();
            tx_qc.Text = keypad.returnval;
        }

        private void lupXseq_EditValueChanged(object sender, EventArgs e)
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            gv.FocusedRowHandle = 0;
        }

        private void XPFQCIN_Load(object sender, EventArgs e)
        {
            
        }
    }
}
