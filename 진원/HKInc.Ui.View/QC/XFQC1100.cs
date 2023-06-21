﻿using System;
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
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using HKInc.Ui.View.REPORT;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.QC
{
    /// <summary>
    /// 2021-01-12 김진우 대리 생성
    /// 공정검사관리
    /// </summary>
    public partial class XFQC1100 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_QCT1200> ModelService = (IService<TN_QCT1200>)ProductionFactory.GetDomainService("TN_QCT1200");
        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;

        public XFQC1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            dp_procdate.DateFrEdit.DateTime = DateTime.Today.AddDays(-10);
            dp_procdate.DateToEdit.DateTime = DateTime.Today;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            repositoryItemGridLookUpEdit = new RepositoryItemGridLookUpEdit()
            {
                ValueMember = "Mcode",
                DisplayMember = "Codename"
            };
            repositoryItemGridLookUpEdit.View.OptionsView.ShowColumnHeaders = false;
            repositoryItemGridLookUpEdit.View.OptionsBehavior.AutoPopulateColumns = false;
            repositoryItemGridLookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            repositoryItemGridLookUpEdit.View.OptionsBehavior.AllowIncrementalSearch = true;
            repositoryItemGridLookUpEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            repositoryItemGridLookUpEdit.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            repositoryItemGridLookUpEdit.NullText = "";
            repositoryItemGridLookUpEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
            repositoryItemGridLookUpEdit.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            repositoryItemGridLookUpEdit.Appearance.BackColor = Color.White;
            repositoryItemGridLookUpEdit.DataSource = DbRequesHandler.GetCommCode("Q003").ToList();
            GridColumn col1 = repositoryItemGridLookUpEdit.View.Columns.AddField(repositoryItemGridLookUpEdit.DisplayMember);
            col1.VisibleIndex = 0;
        }

        private void MainView_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckProv").GetNullToEmpty();
                if (checkDataType == "QT1")
                {
                    //육안검사 
                    e.RepositoryItem = repositoryItemGridLookUpEdit;
                }
            }
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

            MasterGridExControl.MainGrid.AddColumn("No", "검사번호번호");
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            MasterGridExControl.MainGrid.AddColumn("LotNo", "LOTNO");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");            
            MasterGridExControl.MainGrid.AddColumn("CheckDate", "검사일");
            MasterGridExControl.MainGrid.AddColumn("FmeNo", "검사구분");
            MasterGridExControl.MainGrid.AddColumn("FmeDivision", "검사시기");
            MasterGridExControl.MainGrid.AddColumn("CheckResult", "검사결과");
            MasterGridExControl.MainGrid.AddColumn("CheckId", "검사자");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckDate", "CheckId", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, "추가");
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.DeleteRow, "삭제");
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "육안검사는 OK/NG 로 측정값을 입력하세요");

            DetailGridExControl.MainGrid.AddColumn("Seq",false);
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

            //var barInspPrint = new DevExpress.XtraBars.BarButtonItem();
            //barInspPrint.Id = 4;
            //barInspPrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/boreport2");
            //barInspPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            //barInspPrint.Name = "barPoDocumentPrint";
            //barInspPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            //barInspPrint.ShortcutKeyDisplayString = "Alt+P";
            //barInspPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            //barInspPrint.Caption = LabelConvert.GetLabelText("InspectionReportPrint") + "[Alt+P]";
            //barInspPrint.ItemClick += BarInspPrint_ItemClick;
            //barInspPrint.Enabled = UserRight.HasEdit;
            //barInspPrint.Alignment = BarItemLinkAlignment.Right;
            //MasterGridExControl.BarTools.AddItem(barInspPrint);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
           // MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("FmeDivision", DbRequesHandler.GetCommCode(MasterCodeSTR.QCSTEP), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("FmeNo", DbRequesHandler.GetCommCode(MasterCodeSTR.QCKIND), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckResult", DbRequesHandler.GetCommCode(MasterCodeSTR.QCOKNG), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("CheckDate");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("FmeDivision", DbRequesHandler.GetCommCode(MasterCodeSTR.QCSTEP), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckProv", DbRequesHandler.GetCommCode(MasterCodeSTR.QCTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ChaeckFlag", DbRequesHandler.GetCommCode(MasterCodeSTR.QCOKNG), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PoorType", DbRequesHandler.GetCommCode(MasterCodeSTR.QCFAIL), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckName", DbRequesHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("No");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string item = tx_item.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p =>(p.CheckDate>=dp_procdate.DateFrEdit.DateTime&&p.CheckDate<=dp_procdate.DateToEdit.DateTime)
                                                                       && (string.IsNullOrEmpty(item) ? true : (p.ItemCode.Contains(item) || p.TN_STD1100.ItemNm.Contains(item) || p.TN_STD1100.ItemNm1.Contains(item))) 
                                                                       && p.FmeNo == MasterCodeSTR.Process_Inspection).ToList();


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
            DetailGridBindingSource.DataSource = obj.QCT1201List.OrderBy(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");
            param.SetValue(PopupParameter.Value_1, MasterCodeSTR.Process_Inspection);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTMPS1401LIST, param, AddList);
            form.ShowPopup(true);

            //TN_QCT1200 obj = new TN_QCT1200();
            //MasterGridBindingSource.Add(obj);
            //ModelService.Insert(obj);
        }

        private void AddList(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;
            VI_MPS1401LIST partList = (VI_MPS1401LIST)e.Map.GetValue(PopupParameter.ReturnObject);

            TN_QCT1200 QCT1200 = new TN_QCT1200();

            QCT1200.No = DbRequesHandler.GetRequestNumber(MasterCodeSTR.Process_Inspection);
            QCT1200.FmeNo = MasterCodeSTR.Process_Inspection;
            QCT1200.WorkNo = partList.WorkNo;
            QCT1200.WorkDate = partList.WorkDate;
            QCT1200.CheckDate = DateTime.Today;
            QCT1200.FmeDivision = "01";
            //QCT1200.FmeDivision = DbRequesHandler.GetCellValue("exec SP_QCSTEP_STEP '" + MasterCodeSTR.Process_Inspection + "','" + partList.WorkDate + "','" + partList.WorkNo + "','" + partList.ProcessCode + "','" + DateTime.Today + "'", 0);
            QCT1200.ItemCode = partList.ItemCode;
            QCT1200.LotNo = partList.LotNo;
            QCT1200.ProcessCode = partList.ProcessCode;
            QCT1200.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == partList.ItemCode).FirstOrDefault();
            
            MasterGridBindingSource.Add(QCT1200);
            ModelService.Insert(QCT1200);

            MasterGridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            TN_QCT1200 obj = MasterGridBindingSource.Current as TN_QCT1200;
            if (obj == null) return;
            if (obj.QCT1201List.Count > 0) {

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

            List<TN_QCT1000> qc = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == obj.ItemCode && p.ProcessGu == MasterCodeSTR.Process_Inspection && p.ProcessCode == obj.ProcessCode && p.UseYn == "Y").ToList();

            for (int i = 0; i < qc.Count; i++)
            {
                TN_QCT1201 newobj = new TN_QCT1201()
                {
                    No = obj.No,
                    FmeNo=obj.FmeNo,
                    FmeDivision=obj.FmeDivision,
                    ItemCode = obj.ItemCode,
                    Seq = obj.QCT1201List.Count == 0 ? 1 : obj.QCT1201List.OrderBy(o => o.Seq).LastOrDefault().Seq + 1,
                    CheckProv=qc[i].CheckProv,
                    CheckName=qc[i].CheckName,
                    CheckStand=qc[i].CheckStand,
                    UpQuad=qc[i].UpQuad,
                    DownQuad=qc[i].DownQuad
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

        private void BarInspPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_QCT1200;

            if (masterObj == null) return;

            var tN_MPS1400 = ModelService.GetChildList<TN_MPS1400>(x => x.WorkNo == masterObj.WorkNo).FirstOrDefault();
            if (tN_MPS1400 == null) return;

            var tN_ORD1000 = ModelService.GetChildList<TN_ORD1000>(x => x.OrderNo == tN_MPS1400.OrderNo).FirstOrDefault();
            if (tN_ORD1000 == null) return;

            try
            {
                WaitHandler.ShowWait();

                //대영정밀
                if (tN_ORD1000.CustomerCode == "CUST-0001")
                {
                    XRQCT1100_A report = new XRQCT1100_A(masterObj, "CUST-0001");
                    report.CreateDocument();
                    report.ShowPrintStatusDialog = false;
                    report.ShowPreview();
                }
                //디디디
                else if (tN_ORD1000.CustomerCode == "CUST-0002")
                {
                    XRQCT1100_B_ report = new XRQCT1100_B_(masterObj);
                    report.CreateDocument();
                    report.ShowPrintStatusDialog = false;
                    report.ShowPreview();
                }
                //그외
                else
                {
                    XRQCT1100_C report = new XRQCT1100_C(masterObj, tN_ORD1000.CustomerCode);
                    report.CreateDocument();
                    report.ShowPrintStatusDialog = false;
                    report.ShowPreview();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                WaitHandler.CloseWait();
            }

            


        }
    }
}
