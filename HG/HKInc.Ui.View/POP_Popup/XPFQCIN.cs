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


namespace HKInc.Ui.View.POP_Popup
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
            //initGrid();
            lsobj = obj;
            lsLotNo = lotno;
            SetToolbarVisible(false);
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            tx_qc.KeyDown += Tx_qc_KeyDown;
        
        }

        protected override void InitCombo()
        {
            lup_qc.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.QCOKNG), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lupXseq.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.XSEQ), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lupuser.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            lup_qc.SetFontSize(new Font(DefaultFont.FontFamily, 13, FontStyle.Bold));
            lupXseq.SetFontSize(new Font(DefaultFont.FontFamily, 13, FontStyle.Bold));
            lupuser.SetFontSize(new Font(DefaultFont.FontFamily, 13, FontStyle.Bold));
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("CheckName", "검사항목");
            GridExControl.MainGrid.AddColumn("CheckProv", "검사방법");
            GridExControl.MainGrid.AddColumn("CheckStand", "규격");
            GridExControl.MainGrid.AddColumn("UpQuad", "상한");
            GridExControl.MainGrid.AddColumn("DownQuad", "하한");
            GridExControl.MainGrid.AddColumn("X1", "X1");
            GridExControl.MainGrid.AddColumn("X2", "X2");
            GridExControl.MainGrid.AddColumn("X3", "X3");
            GridExControl.MainGrid.AddColumn("X4", "X4", false);
            GridExControl.MainGrid.AddColumn("X5", "X5", false);
            GridExControl.MainGrid.AddColumn("X6", "X6", false);
            GridExControl.MainGrid.AddColumn("X7", "X7", false);
            GridExControl.MainGrid.AddColumn("X8", "X8", false);
            GridExControl.MainGrid.AddColumn("ChaeckFlag", "판정");
            GridExControl.MainGrid.SetGridFont(GridExControl.MainGrid.MainView, new Font(DefaultFont.FontFamily, 14, FontStyle.Bold));
            GridExControl.MainGrid.MainView.ColumnPanelRowHeight = 50;
            GridExControl.MainGrid.MainView.RowHeight = 50;
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckName", DbRequesHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckProv", DbRequesHandler.GetCommCode(MasterCodeSTR.QCTYPE), "Mcode", "Codename");
            GridExControl.BestFitColumns();
        }

        protected override void GridRowDoubleClicked() { }

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

        //자주검사 클릭이벤트
        private void btn_self_Click(object sender, EventArgs e)
        {
            qctype = btn_self.Tag.ToString();
            btn_proc.Enabled = false;
            btn_step.Enabled = false;
            QcLoad(qctype);
        }

        //검사버튼별 조회 함수.
        private void QcLoad(string qc)
        {
            GridBindingSource.DataSource = ModelService.GetList(p => p.ItemCode == lsobj.ItemCode && p.ProcessGu == qc && p.ProcessCode == lsobj.Process && p.UseYn == "Y").ToList();
            gridEx1.DataSource = GridBindingSource;
            gridEx1.MainGrid.BestFitColumns();
            rowid = 0;

            lupXseq.SelectedIndex = 0;

            lupuser.EditValue = Utils.Common.GlobalVariable.LoginId;

            if (qc == btn_step.Tag.ToString())
            {
                var QCStepList = DbRequesHandler.GetCommCode(MasterCodeSTR.QCSTEP);
                var StepValue = DbRequesHandler.GetCellValue("exec SP_QCSTEP_STEP '" + qctype + "','" + lsobj.WorkDate + "','" + lsobj.WorkNo + "','" + lsobj.Process + "','" + DateTime.Today + "'", 0);                
                SetMessage(string.Format("검사 시기 : {0}", QCStepList.Where(p => p.Mcode == StepValue).First().Codename));
            }
            else SetMessage("");
        }

        //취소 이벤트
        private void btn_exit_Click(object sender, EventArgs e)
        {
            SetIsFormControlChanged(false);
            this.Close();
        }

        //적용 이벤트
        private void btn_accept_Click(object sender, EventArgs e)
        {
            acceptrun();
        }

        //적용 함수
        private void acceptrun()
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            if(gv == null || gv.DataSource == null || GridExControl.MainGrid.RecordCount == 0)
            {
                MessageBox.Show("적용할 데이터가 없습니다.");
                return;
            }
            string xseq = lupXseq.EditValue.GetNullToEmpty();
            int XseqCount = ((List<TN_STD1000>)lupXseq.DataSource).Count-1;
            if (gv.GetRowCellValue(rowid, gv.Columns["CheckProv"]).ToString() == "QT1")
            {
                string val = lup_qc.EditValue.GetNullToEmpty();
                gv.SetRowCellValue(rowid, gv.Columns[xseq], val);
                rowid++;
                if (rowid >= gv.RowCount && lupXseq.SelectedIndex == XseqCount)
                {
                    MessageBox.Show("마지막 항목입니다.");
                    rowid--;
                }
                else if (rowid >= gv.RowCount)
                {
                    rowid = 0;
                    lupXseq.SelectedIndex = lupXseq.SelectedIndex + 1;
                }
                gv.FocusedRowHandle = rowid;
            }
            else
            {
                string val = tx_qc.EditValue.GetNullToEmpty();
                gv.SetRowCellValue(rowid, gv.Columns[xseq], val);
                tx_qc.Text = "";
                rowid++;
                if (rowid >= gv.RowCount && lupXseq.SelectedIndex == XseqCount)
                {
                    MessageBox.Show("마지막 항목입니다.");
                    rowid--;
                }
                else if (rowid >= gv.RowCount)
                {
                    rowid = 0;
                    lupXseq.SelectedIndex = lupXseq.SelectedIndex + 1;
                }
                gv.FocusedRowHandle = rowid;
            }
        }

        //초중종검사 클릭이벤트
        private void btn_step_Click(object sender, EventArgs e)
        {
            qctype = btn_step.Tag.ToString();
            btn_proc.Enabled = false;
            btn_self.Enabled = false;
            QcLoad(qctype);
        }

        //공정검사 클릭이벤트
        private void btn_proc_Click(object sender, EventArgs e)
        {
            qctype = btn_proc.Tag.ToString();
            btn_self.Enabled = false;
            btn_step.Enabled = false;
            QcLoad(qctype);
        }

        //육안검사 키 이벤트
        private void lup_qc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string lsval = lup_qc.EditValue.GetNullToEmpty();
            if (lsval == "") return;
            acceptrun();
        }

        //치수검사 키 이벤트
        private void Tx_qc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string lsval = tx_qc.EditValue.GetNullToEmpty();
            if (lsval == "") return;
            acceptrun();
        }

        //저장 클릭이벤트
        private void btn_save_Click(object sender, EventArgs e)
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            if (gv == null || gv.DataSource == null || GridExControl.MainGrid.RecordCount == 0)
            {
                MessageBox.Show("저장할 데이터가 없습니다.");
                return;
            }
            int ngcnt = 0;
            string checkid = lupuser.EditValue.GetNullToEmpty();
            if(checkid=="")
            {
                MessageBox.Show("검사자를 선택하세요");
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
                No = DbRequesHandler.GetRequestNumber(qctype),
                FmeNo = qctype,
                FmeDivision = DbRequesHandler.GetCellValue("exec SP_QCSTEP_STEP '"+ qctype+"','" + lsobj.WorkDate + "','" + lsobj.WorkNo + "','" + lsobj.Process + "','" + DateTime.Today + "'", 0),
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
                dtl.CheckName= gv.GetRowCellValue(i, gv.Columns["CheckName"]).GetNullToEmpty();
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
                
                dtl.Judge = gv.GetRowCellValue(i, gv.Columns["ChaeckFlag"]).ToString();


                qc.QCT1201List.Add(dtl);
            }

            QcModelService.Insert(qc);
            QcModelService.Save();
            SetIsFormControlChanged(false);
            this.Close();
        }

        //키패드(미사용)
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCKEYPAD keypad = new XFCKEYPAD();
            keypad.ShowDialog();
            tx_qc.Text = keypad.returnval;
            tx_qc.BeginInvoke(new MethodInvoker(delegate {
                //txtPassword.SelectionLength = txtPassword.Text.Length;
                tx_qc.SelectionStart = tx_qc.Text.Length;
            }));
        }

        //측정치 더블클릭 이벤트(키패드)
        private void tx_qc_DoubleClick(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCKEYPAD keypad = new XFCKEYPAD();
            keypad.ShowDialog();
            tx_qc.Text = keypad.returnval;
        }

        private void lupXseq_EditValueChanged(object sender, EventArgs e)
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            gv.FocusedRowHandle = 0;
        }
    }
}
