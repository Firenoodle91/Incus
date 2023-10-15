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
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Columns;          // 2022-03-08 김진우 추가
using DevExpress.XtraEditors.Controls;      // 2022-03-08 김진우 추가

namespace HKInc.Ui.View.QC
{
    /// <summary>
    /// 수입검사관리
    /// </summary>
    public partial class XFQC1800 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_QCT1200> ModelService = (IService<TN_QCT1200>)ProductionFactory.GetDomainService("TN_QCT1200");
        //string qctype = "Q01";//수입검사
        RepositoryItemGridLookUpEdit GridLookUpEdit;        // 2022-03-08 김진우 추가

        public XFQC1800()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dp_procdate.DateFrEdit.DateTime = DateTime.Today.AddDays(-10);
            dp_procdate.DateToEdit.DateTime = DateTime.Today;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CustomRowCellEditForEditing += DetailView_CustomRowCellEditForEditing;        // 2022-03-08 김진우 추가

            #region OK_NG LOOKUPEDIT
            // 2022-03-08 김진우 추가
            GridLookUpEdit = new RepositoryItemGridLookUpEdit();

            GridLookUpEdit.ValueMember = "Mcode";
            GridLookUpEdit.DisplayMember = "Codename";
            GridLookUpEdit.View.OptionsView.ShowColumnHeaders = false;
            GridLookUpEdit.View.OptionsBehavior.AutoPopulateColumns = false;
            GridLookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            GridLookUpEdit.View.OptionsBehavior.AllowIncrementalSearch = true;
            GridLookUpEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            GridLookUpEdit.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            GridLookUpEdit.NullText = "";
            GridLookUpEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
            GridLookUpEdit.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            GridLookUpEdit.Appearance.BackColor = Color.White;
            GridLookUpEdit.DataSource = DbRequestHandler.GetCommCode(MasterCodeSTR.QCOKNG).ToList();
            GridColumn col1 = GridLookUpEdit.View.Columns.AddField(GridLookUpEdit.DisplayMember);
            col1.VisibleIndex = 0;
            #endregion
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TN_QCT1200 obj = MasterGridBindingSource.Current as TN_QCT1200;
            GridView gv = sender as GridView;

            string CheckProv = gv.GetFocusedRowCellValue("CheckProv").GetNullToEmpty();
            string val1 = gv.GetFocusedValue().GetNullToEmpty();
            if (CheckProv == "QT1")
            {
                if (val1 != "")
                {
                    if (val1.Substring(0, 1).ToUpper() != "O" && val1.Substring(0, 1).ToUpper() != "N")
                    {
                        MessageBox.Show(val1 + "입력값이 잘못되었습니다.");
                        gv.SetFocusedValue("");
                        return;
                    }
                }
            }
            if (CheckProv == "QT2")
            {
                if (val1 != "")
                {
                    if (val1.Substring(0, 1).ToUpper() == "O" || val1.Substring(0, 1).ToUpper() == "N")
                    {

                        MessageBox.Show(val1 + "입력값이 잘못되었습니다.");
                        gv.SetFocusedValue("");
                        return;
                    }
                }
            }
            if (e.Column.Name.ToString() == "Reading1") { Checkval(obj, gv); }
            if (e.Column.Name.ToString() == "Reading2") { Checkval(obj, gv); }
            if (e.Column.Name.ToString() == "Reading3") { Checkval(obj, gv); }
            if (e.Column.Name.ToString() == "Reading4") { Checkval(obj, gv); }
            if (e.Column.Name.ToString() == "Reading5") { Checkval(obj, gv); }
            if (e.Column.Name.ToString() == "Reading6") { Checkval(obj, gv); }
            if (e.Column.Name.ToString() == "Reading7") { Checkval(obj, gv); }
            if (e.Column.Name.ToString() == "Reading8") { Checkval(obj, gv); }

            MasterGridExControl.MainGrid.BestFitColumns();
        }

        private static void Checkval(TN_QCT1200 obj, GridView gv)
        {
            int Ncnt = 0;
            int Ocnt = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                switch (gv.GetRowCellValue(i, gv.Columns["ChaeckFlag"]).GetNullToEmpty())
                {
                    case "OK": Ocnt++; break;
                    case "NG": Ncnt++; break;
                }
            }
            if (Ncnt > 0)
            {
                obj.CheckResult = "NG";
            }
            else if (Ocnt > 0)
            { obj.CheckResult = "OK"; }
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("No", "검사번호");
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            MasterGridExControl.MainGrid.AddColumn("LotNo", "LOTNO");
            MasterGridExControl.MainGrid.AddColumn("Temp1", "INLOTNO");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품목");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");         // 2022-02-15 김진우 추가
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");            
            MasterGridExControl.MainGrid.AddColumn("CheckDate", "검사일");
            MasterGridExControl.MainGrid.AddColumn("FmeNo", "검사구분");
            MasterGridExControl.MainGrid.AddColumn("FmeDivision", "검사시기");
            MasterGridExControl.MainGrid.AddColumn("CheckResult", "검사결과");
            MasterGridExControl.MainGrid.AddColumn("CheckId", "검사자");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckDate", "CheckId", "CheckResult", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            //DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, "추가");
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            //DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.DeleteRow, "삭제");
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "육안검사는 OK/NG 로 측정값을 입력하세요");
            DetailGridExControl.MainGrid.AddColumn("Seq", false);
            DetailGridExControl.MainGrid.AddColumn("FmeDivision", "검사구분");
            DetailGridExControl.MainGrid.AddColumn("CheckName", "검사항목");            
            DetailGridExControl.MainGrid.AddColumn("CheckProv", "검사방법");
            DetailGridExControl.MainGrid.AddColumn("CheckStand", "기준",true,HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("UpQuad", "상한공차", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("DownQuad", "하한공차", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("Reading1", "측정치1", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("Reading2", "측정치2", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("Reading3", "측정치3", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("Reading4", "측정치4", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("Reading5", "측정치5", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("Reading6", "측정치6", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("Reading7", "측정치7", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("Reading8", "측정치8", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("ChaeckFlag", "판정", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("PoorType", "불량유형", false, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Reading1", "Reading2", "Reading3", "Reading4", "Reading5", "Reading6", "Reading7", "Reading8", "Memo");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("FmeDivision", DbRequestHandler.GetCommCode(MasterCodeSTR.QCSTEP), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("FmeNo", DbRequestHandler.GetCommCode(MasterCodeSTR.QCKIND), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckResult", DbRequestHandler.GetCommCode(MasterCodeSTR.QCOKNG), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("CheckDate");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("FmeDivision", DbRequestHandler.GetCommCode(MasterCodeSTR.QCSTEP), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckProv", DbRequestHandler.GetCommCode(MasterCodeSTR.QCTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ChaeckFlag", DbRequestHandler.GetCommCode(MasterCodeSTR.QCOKNG), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoorType", DbRequestHandler.GetCommCode(MasterCodeSTR.QCFAIL), "Mcode", "Codename");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckName", DbRequestHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("No");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string item = tx_item.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.CheckDate >= dp_procdate.DateFrEdit.DateTime && p.CheckDate <= dp_procdate.DateToEdit.DateTime)
                                                                        && (string.IsNullOrEmpty(item) ? true : (p.ItemCode.Contains(item)||p.TN_STD1100.ItemNm.Contains(item)||p.TN_STD1100.ItemNm1.Contains(item)))
                                                                        && p.FmeNo == MasterCodeSTR.ReceivingInspectionManagement)  // 2022-04-20 김진우  MAsterCodeSTR 추가
                                                                        .OrderBy(o => o.No)        // 2022-02-08 김진우 대리 추가
                                                                        .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_QCT1200 obj = MasterGridBindingSource.Current as TN_QCT1200;
            if (obj == null) return;
            if (obj.WorkNo.Contains("OIN"))
            {
                simpleButton1.Enabled = false;
            }
            else { simpleButton1.Enabled = true; }
            DetailGridBindingSource.DataSource = obj.QCT1201List.OrderBy(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void AddRowClicked()
        {

            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTINSPLIST, param, AddList);
            form.ShowPopup(true);
        }

        private void AddList(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;
            VI_INSPLIST partList = (VI_INSPLIST)e.Map.GetValue(PopupParameter.ReturnObject);

            TN_QCT1200 obj = new TN_QCT1200();

            obj.No = DbRequestHandler.GetRequestNumber(MasterCodeSTR.ReceivingInspectionManagement);        // 2022-04-20 김진우  MAsterCodeSTR 추가
            obj.FmeNo = MasterCodeSTR.ReceivingInspectionManagement;                                        // 2022-04-20 김진우  MAsterCodeSTR 추가
            obj.WorkNo = partList.InputNo;
            obj.WorkDate = partList.InputDate;
            obj.CheckDate = DateTime.Today;
            obj.FmeDivision = "01";
            obj.ItemCode = partList.ItemCode;
            obj.LotNo = partList.LotNo;
            obj.Temp1 = partList.Inlot;
            obj.Temp2 = partList.InputSeq.ToString();
            obj.CheckResult = "NG";                             // 2022-02-15 김진우 추가
            obj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == partList.ItemCode).FirstOrDefault();          // 2022-02-15 김진우 추가

            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
            MasterGridExControl.BestFitColumns();       // 2022-03-08 김진우 추가
        }

        protected override void DeleteRow()
        {
            TN_QCT1200 obj = MasterGridBindingSource.Current as TN_QCT1200;
            if (obj == null) return;
            if (obj.QCT1201List.Count > 0)
            {
                MessageBox.Show("품질데이터가 있어서 삭제할수 없습니다.");
            }
            else
            {
                MasterGridBindingSource.Remove(obj);
                ModelService.Delete(obj);
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_QCT1200 obj = MasterGridBindingSource.Current as TN_QCT1200;

            List<TN_QCT1000> qc = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == obj.ItemCode && p.ProcessGu == MasterCodeSTR.ReceivingInspectionManagement && p.UseYn == "Y").ToList();      // 2022-04-20 김진우  MAsterCodeSTR 추가

            for (int i = 0; i < qc.Count; i++)
            {
                TN_QCT1201 newobj = new TN_QCT1201()
                {
                    No = obj.No,
                    FmeNo = obj.FmeNo,
                    FmeDivision = obj.FmeDivision,
                    ItemCode = obj.ItemCode,
                    Seq = obj.QCT1201List.Count == 0 ? 1 : obj.QCT1201List.OrderBy(o => o.Seq).LastOrDefault().Seq + 1,
                    CheckProv = qc[i].CheckProv,
                    CheckName = qc[i].CheckName,
                    CheckStand = qc[i].CheckStand,
                    UpQuad = qc[i].UpQuad,
                    DownQuad = qc[i].DownQuad
                };
                DetailGridBindingSource.Add(newobj);
                obj.QCT1201List.Add(newobj);
            }
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            TN_QCT1200 obj = MasterGridBindingSource.Current as TN_QCT1200;
           
            if (obj == null) return;
            TN_QCT1201 delobj = DetailGridBindingSource.Current as TN_QCT1201;
            obj.QCT1201List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);

            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            ModelService.Save();
            DataLoad();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            TN_QCT1200 v = MasterGridBindingSource.Current as TN_QCT1200;
            if (v == null) return;      // 2022-02-14 김진우 추가
            var FirstReport = new REPORT.RQCLABLE();

            TN_PUR1301 obj = ModelService.GetChildList<TN_PUR1301>(p => p.Temp2 == v.Temp1).FirstOrDefault();

            obj.InputQty = Convert.ToDecimal(obj.InputQty / (obj.Lqty == 0 ? 1 : obj.Lqty));
            for (int j = 0; j < (obj.Lqty == 0 ? 1 : obj.Lqty); j++)
            {
                var report = new REPORT.RQCLABLE(obj);
                report.CreateDocument();
                FirstReport.Pages.AddRange(report.Pages);
            }
            FirstReport.ShowPrintStatusDialog = false;
            FirstReport.ShowPreview();//.Print();
        }

        /// <summary>
        /// 2022-03-08 김진우 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailView_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            GridView GV = sender as GridView;

            if (e.RowHandle >= 0)
                if (e.Column.FieldName.Contains("Reading"))
                {
                    var CheckType = GV.GetRowCellValue(e.RowHandle, "CheckProv").GetNullToEmpty();
                    if (CheckType == MasterCodeSTR.VisualInspection)
                        e.RepositoryItem = GridLookUpEdit;
                }
        }

    }
}
