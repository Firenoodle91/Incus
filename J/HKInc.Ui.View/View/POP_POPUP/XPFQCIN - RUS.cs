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
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.Model.Domain.VIEW;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using HKInc.Utils.Common;
using System.Data.SqlClient;

/// <summary>
/// POP 품질검사 팝업
/// </summary>
namespace HKInc.Ui.View.View.POP_POPUP
{
    public partial class XPFQCIN_RUS : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1001> ModelService = (IService<TN_QCT1001>)ProductionFactory.GetDomainService("TN_QCT1001");
        IService<TN_QCT1100> QcModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");
        IService<TN_MPS1000> ModelService2 = (IService<TN_MPS1000>)ProductionFactory.GetDomainService("TN_MPS1000");

        TEMP_XFPOP1000 lsobj;

        string lsLotNo;
        int rowid;
        string qctype;

        //TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        bool Self_Check = false;
        bool FME_Check = false;
        bool Process_Check = false;


        public XPFQCIN_RUS(TEMP_XFPOP1000 obj, string lotno, object objimg)
        {
            InitializeComponent();
            GridExControl = gridEx1;
            //initGrid();
            lsobj = obj;
            lsLotNo = lotno;
            SetToolbarVisible(false);
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            GridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            btn_proc.Enabled = true;
            // btn_self.Enabled = false;
            pictureEdit1.EditValue = objimg;
            //this.Location = new Point(x - this.Width, 100);
        }
        public XPFQCIN_RUS(string workno, string processcode, string item, string mc, string lotno)
        {
            InitializeComponent();
            GridExControl = gridEx1;
            //initGrid();
            //   lsobj = obj;
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                var ProcessCode = new SqlParameter("@ProcessCode", processcode);
                var MachineCode = new SqlParameter("@MachineCode", mc);
                var ItemCode = new SqlParameter("@ItemCode", item);
                var WorkNo = new SqlParameter("@WorkNo", workno);

                TEMP_XFPOP1000 result = context.Database.SqlQuery<TEMP_XFPOP1000>("USP_GET_XFPOP1000_LIST @ProcessCode, @MachineCode, @ItemCode, @WorkNo", ProcessCode, MachineCode, ItemCode, WorkNo).FirstOrDefault();
                if (result == null) { return; }
                lsobj = result;
            }
            lsLotNo = lotno;
            SetToolbarVisible(false);
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            GridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            btn_proc.Enabled = true;
            // btn_self.Enabled = false;
            var Jobstd = ModelService2.GetChildList<TN_MPS1000>(p => p.ItemCode == lsobj.ItemCode && p.ProcessCode == lsobj.ProcessCode).LastOrDefault();
            pictureEdit1.EditValue = Jobstd == null ? null : Jobstd.FileData;
            //this.Location = new Point(x - this.Width, 100);
        }
        public XPFQCIN_RUS(string workno, string processcode, string item, string mc, string lotno, string lsif)
        {
            InitializeComponent();
            GridExControl = gridEx1;
            //initGrid();
            //   lsobj = obj;
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                var ProcessCode = new SqlParameter("@ProcessCode", processcode);
                var MachineCode = new SqlParameter("@MachineCode", mc);
                var ItemCode = new SqlParameter("@ItemCode", item);
                var WorkNo = new SqlParameter("@WorkNo", workno);

                TEMP_XFPOP1000 result = context.Database.SqlQuery<TEMP_XFPOP1000>("USP_GET_XFPOP1000_LIST_1 @ProcessCode, @MachineCode, @ItemCode, @WorkNo", ProcessCode, MachineCode, ItemCode, WorkNo).FirstOrDefault();
                if (result == null) { return; }
                lsobj = result;
            }
            lsLotNo = lotno;
            SetToolbarVisible(false);
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            GridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            btn_proc.Enabled = true;
            // btn_self.Enabled = false;
            var Jobstd = ModelService2.GetChildList<TN_MPS1000>(p => p.ItemCode == lsobj.ItemCode && p.ProcessCode == lsobj.ProcessCode).LastOrDefault();
            pictureEdit1.EditValue = Jobstd == null ? null : Jobstd.FileData;
            //this.Location = new Point(x - this.Width, 100);
        }
        public XPFQCIN_RUS(TEMP_XFPOP1000 obj, string lotno)
        {
            InitializeComponent();
            GridExControl = gridEx1;
            //initGrid();
            lsobj = obj;
            lsLotNo = lotno;
            SetToolbarVisible(false);
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            GridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            //btn_proc.Enabled = false;

            //+ 작업표준서
            var Jobstd = ModelService2.GetChildList<TN_MPS1000>(p => p.ItemCode == obj.ItemCode && p.ProcessCode == obj.ProcessCode).LastOrDefault();
            pictureEdit1.EditValue = Jobstd == null ? null : Jobstd.FileData;
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Judge"]); //JI원 소스는 됨, 여기는 안됨
                if (category == "NG")
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                    e.Appearance.ForeColor = Color.White;
                    //e.Appearance.BackColor2 = Color.SeaShell;

                }

                //string val = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Reading1"]);
                //string checkmin = View.GetRowCellDisplayText(e.RowHandle, View.Columns["CheckMin"]);
                //string checkmax = View.GetRowCellDisplayText(e.RowHandle, View.Columns["CheckMax"]);
                ////string checkway = View.GetRowCellDisplayText(e.RowHandle, View.Columns["CheckWay"]);
                //string checkway = View.GetRowCellValue(e.RowHandle, View.Columns["CheckWay"]).ToString();
                //string judge = "";

                //if (val.Length <= 0) return;

                //if (checkway == "QT1")
                //{
                //    if (val == "NG")
                //    {
                //        e.Appearance.BackColor = Color.OrangeRed;
                //        e.Appearance.ForeColor = Color.White;
                //        //e.Appearance.BackColor2 = Color.SeaShell;
                //        judge = "NG";
                //    }
                //    else
                //        judge = "OK";
                //}

                //else
                //{
                //    if (Convert.ToDecimal(val) > Convert.ToDecimal(checkmax) || Convert.ToDecimal(val) < Convert.ToDecimal(checkmin))
                //    {
                //        e.Appearance.BackColor = Color.OrangeRed;
                //        e.Appearance.ForeColor = Color.White;
                //        //e.Appearance.BackColor2 = Color.SeaShell;
                //        judge = "NG";

                //    }
                //    else
                //        judge = "OK";
                //}

                //View.SetRowCellValue(rowid, View.Columns["Judge"], judge);

            }

        }

        protected override void GridRowDoubleClicked()
        {

        }

        protected override void InitCombo()
        {
            lup_qc.SetDefault(true, "CodeVal", "CodeName", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG));
            lupuser.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TN_QCT1001 obj = GridBindingSource.Current as TN_QCT1001;

            if (obj == null) return;

            rowid = e.FocusedRowHandle;
            if (obj.CheckWay == "QT1")
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
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.MainView.OptionsView.ShowIndicator = false;

            //GridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"));
            //GridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));
            //GridExControl.MainGrid.AddColumn("CheckDataType", LabelConvert.GetLabelText("CheckDataType"), false);
            //GridExControl.MainGrid.AddColumn("InspectionReportMemo", LabelConvert.GetLabelText("InspectionReportMemo"), HorzAlignment.Center, false);
            //GridExControl.MainGrid.AddColumn("CheckMin", LabelConvert.GetLabelText("CheckMin"), HorzAlignment.Center, true);
            //GridExControl.MainGrid.AddColumn("CheckMax", LabelConvert.GetLabelText("CheckMax"), HorzAlignment.Center, true);
            //GridExControl.MainGrid.AddColumn("CheckSpec", LabelConvert.GetLabelText("CheckSpec"), HorzAlignment.Center, true);
            //GridExControl.MainGrid.AddColumn("CheckUpQuad", LabelConvert.GetLabelText("CheckUpQuad"), HorzAlignment.Center, false);
            //GridExControl.MainGrid.AddColumn("CheckDownQuad", LabelConvert.GetLabelText("CheckDownQuad"), HorzAlignment.Center, false);
            //GridExControl.MainGrid.AddColumn("Reading1",   LabelConvert.GetLabelText("Reading1"),   HorzAlignment.Center, true);
            //GridExControl.MainGrid.AddColumn("Judge", LabelConvert.GetLabelText("Judge"));

            GridExControl.MainGrid.AddColumn("RevNo", "RevNo", false);
            GridExControl.MainGrid.AddColumn("Seq", "Seq", false);
            GridExControl.MainGrid.AddColumn("CheckUpQuad", "CheckUpQuad", false);
            GridExControl.MainGrid.AddColumn("CheckDownQuad", "CheckDownQuad", false);

            GridExControl.MainGrid.AddColumn("CheckDataType", LabelConvert.GetLabelText("CheckDataType"), false);
            GridExControl.MainGrid.AddColumn("CheckList", "Пункты проверки");       // 검사항목
            GridExControl.MainGrid.AddColumn("CheckWay", "метод осмотра");      // 검사방법
            GridExControl.MainGrid.AddColumn("CheckSpec", "стандартный", HorzAlignment.Near, true);     // 규격
            GridExControl.MainGrid.AddColumn("CheckMin", "Нижний предел", HorzAlignment.Center, true);      // 하한
            GridExControl.MainGrid.AddColumn("CheckMax", "верхний предел", HorzAlignment.Center, true);     // 상한
            GridExControl.MainGrid.AddColumn("Reading1", "Проверить", HorzAlignment.Center, true);      // 검사값
            GridExControl.MainGrid.AddColumn("Judge", "Значение решения");      // 판정

            //GridExControl.MainGrid.SetEditable(true, "Reading1", "Reading2", "Reading3", "Reading4", "Reading5", "Reading6", "Reading7", "Reading8", "Reading9");
        }
        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDataType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDataType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("Judge", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MaxReading", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            //GridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("CheckProv", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "Mcode", "Codename");

            GridExControl.MainGrid.MainView.Columns["Reading1"].Width = 100; //너비 테스트
        }

        private void QcLoad(string qc)
        {
            //GridExControl.MainGrid.MainView.OptionsView.RowAutoHeight = true;
            //GridExControl.MainGrid.MainView.Columns["CheckStand"].AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            var qcRev = QcModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == lsobj.ItemCode && p.UseFlag == "Y").OrderBy(p => p.RowId).LastOrDefault();
            if (qcRev == null) return;      // RevNo를 불러올수 없을때 오류나서 추가  2022-10-06 김진우 추가

            GridBindingSource.DataSource =
                ModelService.GetList(p => p.ItemCode == lsobj.ItemCode
                                  && p.CheckDivision == qc
                                  && p.ProcessCode == lsobj.ProcessCode
                                  && p.UseFlag == "Y"
                                  && p.RevNo == qcRev.RevNo
                                  )
                                    .OrderBy(p => p.DisplayOrder).ToList();

            gridEx1.DataSource = GridBindingSource;
            gridEx1.MainGrid.BestFitColumns();
            rowid = 0;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            SetIsFormControlChanged(false);
            this.Close();
        }

        /// <summary>
        /// 적용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_accept_Click(object sender, EventArgs e)
        {
            acceptrun();
        }

        private void acceptrun()
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            string val = string.Empty;
            string checkmin = gv.GetRowCellDisplayText(rowid, gv.Columns["CheckMin"]);
            string checkmax = gv.GetRowCellDisplayText(rowid, gv.Columns["CheckMax"]);

            if (gv.GetRowCellValue(rowid, gv.Columns["CheckWay"]).ToString() == "QT1")
            {
                val = lup_qc.EditValue.GetNullToEmpty();
                gv.SetRowCellValue(rowid, gv.Columns["Judge"], val);
            }
            else
            {
                // 2022-10-19 김진우 추가
                if (tx_qc.EditValue.GetNullToEmpty() == "")
                    val = "0";
                else
                    val = tx_qc.EditValue.GetNullToEmpty();

                if (Convert.ToDecimal(val) > Convert.ToDecimal(checkmax) || Convert.ToDecimal(val) < Convert.ToDecimal(checkmin))
                {
                    gv.SetRowCellValue(rowid, gv.Columns["Judge"], "NG");
                    //e.Appearance.BackColor = Color.OrangeRed;
                    //e.Appearance.ForeColor = Color.White;
                }
                else
                {
                    gv.SetRowCellValue(rowid, gv.Columns["Judge"], "OK");
                }
            }

            gv.SetRowCellValue(rowid, gv.Columns["Reading1"], val);
            tx_qc.Text = "";
            rowid++;
            if (rowid >= gv.RowCount)
            {
                MessageBox.Show("Это последняя запись.");       // 마지막 항목입니다.
                rowid--;
            }
            gv.FocusedRowHandle = rowid;

            //if (gv.GetRowCellValue(rowid, gv.Columns["CheckWay"]).ToString() == "QT1")
            //{
            //    string val = lup_qc.EditValue.GetNullToEmpty();
            //    gv.SetRowCellValue(rowid, gv.Columns["Reading1"], val);
            //    rowid++;
            //    if (rowid >= gv.RowCount)
            //    {
            //        MessageBox.Show("마지막 항목입니다.");
            //        rowid--;
            //    }
            //    gv.FocusedRowHandle = rowid;
            //}
            //else
            //{
            //    string val = tx_qc.EditValue.GetNullToEmpty();
            //    gv.SetRowCellValue(rowid, gv.Columns["Reading1"], val);
            //    tx_qc.Text = "";
            //    rowid++;
            //    if (rowid >= gv.RowCount)
            //    {
            //        MessageBox.Show("마지막 항목입니다.");
            //        rowid--;
            //    }
            //    gv.FocusedRowHandle = rowid;
            //}
        }

        /// <summary>
        /// 초중종 검사
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_step_Click(object sender, EventArgs e)
        {

            qctype = btn_step.Tag.ToString();
            btn_proc.Enabled = false;
            btn_self.Enabled = false;
            //초중종
            //lupqcstep.SetDefault(true, "CodeVal", "CodeName", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionFME_POP));
            lupqcstep.SetDefault(true, "CodeVal", "CodeName", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionFME_POP));        // 2022-10-11 김진우 숫자로 표시되도록 수정
            QcLoad(qctype);
        }

        /// <summary>
        /// 공정검사
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_proc_Click(object sender, EventArgs e)
        {
            qctype = btn_proc.Tag.ToString();
            btn_self.Enabled = false;
            btn_step.Enabled = false;
            QcLoad(qctype);
        }

        /// <summary>
        /// 초품검사
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_self_Click(object sender, EventArgs e)
        {
            qctype = btn_self.Tag.ToString();
            btn_proc.Enabled = false;
            btn_step.Enabled = false;
            //초품검사
            lupqcstep.SetDefault(true, "CodeVal", "CodeName", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionF));
            QcLoad(qctype);
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

            var list = GridBindingSource.List as List<TN_QCT1001>;

            var list2 = gridEx1.DataSource as List<TN_QCT1001>;

            int ngcnt = 0;
            string checkid = lupuser.EditValue.GetNullToEmpty();
            string checkpoint = lupqcstep.EditValue.GetNullToEmpty();

            if (checkid == "")
            {
                MessageBox.Show("Выберите инспектора");      // 검사자를 선택하세요
                return;
            }

            for (int i = 0; i < gv.RowCount; i++)
            {
                if (gv.GetRowCellValue(i, gv.Columns["Judge"]).GetNullToEmpty() == "NG")
                {
                    ngcnt++;
                }
            }

            //var masterCheckResult = list.Any(p => p.Judge == "NG") ? "NG" : "OK";

            string InspNo_Seq = DbRequestHandler.GetSeqMonth(qctype);

            // 2022-10-16 김진우 추가
            int a = Convert.ToInt32(DateTime.Now.ToString("HH"));
            DateTime DateNow = DateTime.Now;
            if (a >= 0 && a < 8)
                DateNow = DateTime.Now.AddDays(-1);

            //검사 마스터    ModelService
            TN_QCT1100 qc = new TN_QCT1100()
            {
                InspNo = InspNo_Seq,
                //InspNo = DbRequestHandler.GetSeqMonth(qctype),
                CheckDivision = qctype,
                CheckPoint = checkpoint.GetNullToEmpty() == "" ? "##" : checkpoint,
                WorkNo = lsobj.WorkNo,
                WorkSeq = lsobj.ProcessSeq,
                WorkDate = lsobj.WorkDate,
                ItemCode = lsobj.ItemCode,
                CustomerCode = lsobj.CustomerCode,
                ProcessCode = lsobj.ProcessCode,
                ProductLotNo = lsLotNo,
                //CheckDate = DateTime.Today,                    // 2022-10-14 김진우 수정
                CheckDate = DateNow,                    // 2022-10-14 김진우 수정
                CheckId = checkid,
                //CheckResult = masterCheckResult,
                CheckResult = ngcnt >= 1 ? "NG" : "OK",
                CheckDateTime1 = DateTime.Now,
                //ProdDate = DateTime.Now,                // 2022-09-27 김진우 추가
                ProdDateTime = DateTime.Now,       // 2022-09-27 김진우 추가
                ProdId = checkid,                        // 2022-09-27 김진우 추가
                ProdOKNG = ngcnt >= 1 ? "NG" : "OK",     // 2022-10-05 김진우 추가
                Temp = qctype == "Q03" ? "Y" : null,                                // 2022-10-12 김진우 추가
            };

            //검사 디테일    QcModelService
            #region JI 버전
            for (int i = 0; i < gv.RowCount; i++)
            {
                TN_QCT1101 dtl = new TN_QCT1101();

                if (gv.GetRowCellValue(i, gv.Columns["Reading1"]).GetNullToEmpty() == "") continue;
                //string checkmin = gv.GetRowCellDisplayText(rowid, gv.Columns["CheckMin"]);
                //string checkmax = gv.GetRowCellDisplayText(rowid, gv.Columns["CheckMax"]);

                try
                {
                    dtl.InspNo = qc.InspNo;
                    dtl.InspSeq = qc.TN_QCT1101List.Count == 0 ? 1 : qc.TN_QCT1101List.Max(o => o.InspSeq) + 1;
                    dtl.RevNo = gv.GetRowCellValue(i, gv.Columns["RevNo"]).GetNullToEmpty();
                    dtl.ItemCode = qc.ItemCode;
                    dtl.Seq = gv.GetRowCellValue(i, gv.Columns["Seq"]).GetIntNullToZero();
                    dtl.CheckWay = gv.GetRowCellValue(i, gv.Columns["CheckWay"]).GetNullToEmpty();
                    dtl.CheckList = gv.GetRowCellValue(i, gv.Columns["CheckList"]).GetNullToEmpty();
                    dtl.FME_DIVISION = checkpoint.GetNullToEmpty() == "" ? "##" : checkpoint;
                    dtl.CheckMax = gv.GetRowCellValue(i, gv.Columns["CheckMax"]).GetNullToEmpty();
                    dtl.CheckMin = gv.GetRowCellValue(i, gv.Columns["CheckMin"]).GetNullToEmpty();
                    dtl.CheckSpec = gv.GetRowCellValue(i, gv.Columns["CheckSpec"]).GetNullToEmpty();
                    dtl.CheckUpQuad = gv.GetRowCellValue(i, gv.Columns["CheckUpQuad"]).GetNullToEmpty();
                    dtl.CheckDownQuad = gv.GetRowCellValue(i, gv.Columns["CheckDownQuad"]).GetNullToEmpty();
                    dtl.CheckDataType = gv.GetRowCellValue(i, gv.Columns["CheckDataType"]).GetNullToEmpty();
                    dtl.Reading1 = gv.GetRowCellValue(i, gv.Columns["Reading1"]).GetNullToEmpty();
                    dtl.Judge = gv.GetRowCellValue(i, gv.Columns["Judge"]).GetNullToEmpty();
                    dtl.CreateId = checkid;

                    qc.TN_QCT1101List.Add(dtl);
                }
                catch
                { }
            }
            #endregion

            // 모니터링용 컬럼 temp에 추가된 값 외에 같은 목록은 N으로 변경 2022-10-12 김진우 추가
            //List<TN_QCT1100> QCT1100List = QcModelService.GetList(p => p.WorkNo == lsobj.WorkNo && p.CheckPoint == checkpoint && p.ProcessCode == lsobj.ProcessCode && p.CheckDivision == "Q03").ToList();
            //if (QCT1100List != null && qctype == "Q03")
            //{
            //    foreach (var v in QCT1100List)
            //    {
            //        v.Temp = "N";
            //        QcModelService.Update(v);
            //    }
            //}

            QcModelService.Insert(qc);
            QcModelService.Save();
            SetIsFormControlChanged(false);
            this.Close();
        }

        private void tx_qc_DoubleClick(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;

            //XFCKEYPAD keypad = new XFCKEYPAD();
            XFCNUMPAD keypad = new XFCNUMPAD();
            keypad.ShowDialog();
            tx_qc.Text = keypad.returnval;
        }

        private void tx_qc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            acceptrun();
        }

        ////보류
        //private void Pic_WorkStandardDocument_DoubleClick(object sender, EventArgs e)
        //{
        //    if (pic_WorkStandardDocument.EditValue == null) return;
        //    var imgForm = new POP_POPUP.XPFPOPIMG(LabelConvert.GetLabelText("WorkStandardDocument"), pic_WorkStandardDocument.EditValue);
        //    imgForm.ShowDialog();
        //}
    }
}
