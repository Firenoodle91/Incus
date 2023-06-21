using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 용접지그이력관리화면
    /// </summary>
    public partial class XFMEA1501 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MEA1500> ModelService = (IService<TN_MEA1500>)ProductionFactory.GetDomainService("TN_MEA1500");

        public XFMEA1501()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("RBIDCode", LabelConvert.GetLabelText("RBIDCode"));
            MasterGridExControl.MainGrid.AddColumn("RBIDKind", LabelConvert.GetLabelText("RBIDKind"));
            MasterGridExControl.MainGrid.AddColumn("RBIDName", LabelConvert.GetLabelText("RBIDName"));
            MasterGridExControl.MainGrid.AddColumn("RBIDNameENG", LabelConvert.GetLabelText("RBIDNameENG"));
            MasterGridExControl.MainGrid.AddColumn("RBIDNameCHN", LabelConvert.GetLabelText("RBIDNameCHN"));
            MasterGridExControl.MainGrid.AddColumn("Spec", LabelConvert.GetLabelText("Spec"));
            MasterGridExControl.MainGrid.AddColumn("Maker", LabelConvert.GetLabelText("Maker"));
            MasterGridExControl.MainGrid.AddColumn("IntroductionDate", LabelConvert.GetLabelText("IntroductionDate"));
            MasterGridExControl.MainGrid.AddColumn("SerialNo", LabelConvert.GetLabelText("SerialNo"));
            MasterGridExControl.MainGrid.AddColumn("CorTurn", LabelConvert.GetLabelText("CorTurn"));
            MasterGridExControl.MainGrid.AddColumn("CorDate", LabelConvert.GetLabelText("CorDate"));
            MasterGridExControl.MainGrid.AddColumn("PredictionCorDate", LabelConvert.GetLabelText("PredictionCorDate"));
            MasterGridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));
            MasterGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("FileName"), false);     // 2021-05-26 김진우 주임 false 추가
            MasterGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("InstrCode", LabelConvert.GetLabelText("InstrCode"), false);
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("CheckDate", LabelConvert.GetLabelText("CorDate"));
            DetailGridExControl.MainGrid.AddColumn("CheckDivision", LabelConvert.GetLabelText("CheckDivision"));
            DetailGridExControl.MainGrid.AddColumn("CheckContent", LabelConvert.GetLabelText("CheckContent"));
            DetailGridExControl.MainGrid.AddColumn("CheckId", LabelConvert.GetLabelText("CheckId"));
            DetailGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("FileName"));
            DetailGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            DetailGridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            DetailGridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckDate", "CheckDivision", "CheckContent", "Memo", "FileName");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1500>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1501>(DetailGridExControl);

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequestHandler.GetCommCode(MasterCodeSTR.InstrMaker, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CorTurn", DbRequestHandler.GetCommCode(MasterCodeSTR.InstrCorTurn, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("IntroductionDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("CorDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PredictionCorDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            //GridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, MasterCodeSTR.FtpFolder_InstrImage, "FileName", "FileUrl", true);     // 2021-05-26 김진우 주임 주석처리
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("CheckDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDivision", DbRequestHandler.GetCommCode(MasterCodeSTR.InstrCheckDivision, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, DetailGridExControl, MasterCodeSTR.FtpFolder_InstrCheckHistory, "FileName", "FileUrl");
            //DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit_BAK(UserRight.HasEdit, DetailGridExControl, "FileName", "FileUrl", false);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            DetailGridRowLocator.GetCurrentRow("Seq");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var InstrCodeName = tx_RBIDCodeName.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.RBIDCode.Contains(InstrCodeName) || (p.RBIDName == InstrCodeName) || p.RBIDNameENG.Contains(InstrCodeName) || p.RBIDNameCHN.Contains(InstrCodeName))
                                                                    && (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                               )
                                                               .OrderBy(p => p.RBIDName)
                                                               .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);
            GridRowLocator.SetCurrentRow();
            DetailGridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1500;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_MEA1501List.OrderBy(p => p.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            #region 파일 CHECK
            var masterList = MasterGridBindingSource.List as List<TN_MEA1500>;
            var editList = masterList.Where(p => p.EditRowFlag == "Y").ToList();
            if (editList.Count > 0)
            {
                foreach (var v in editList.Where(p => p.TN_MEA1501List.Any(c => c.FileUrl != null && c.FileUrl.Contains("\\"))).ToList())
                {
                    foreach (var d in v.TN_MEA1501List.Where(c => c.FileUrl != null && c.FileUrl.Contains("\\")).ToList())
                    {
                        string[] filename = d.FileUrl.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = d.RBIDCode + "_" + filename[filename.Length - 1];
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_InstrCheckHistory + "/" + realFileName;

                            FileHandler.UploadFTP(d.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_InstrCheckHistory + "/");

                            d.FileName = realFileName;
                            d.FileUrl = ftpFileUrl;
                        }
                    }
                }
                //foreach (var v in masterList.Where(p => p.TN_MEA1101List.Any(c => !c.DeleteFilePath.IsNullOrEmpty())).ToList())
                //{
                //    foreach (var d in v.TN_MEA1101List.Where(p => !p.DeleteFilePath.IsNullOrEmpty()))
                //    {
                //        try
                //        {
                //            FileHandler.PathDeleteFTP(GlobalVariable.FTP_SERVER, d.DeleteFilePath);
                //        }
                //        catch { }
                //    }
                //}

                //foreach (var v in masterList.Where(p => p.TN_MEA1101List.Any(c => !c.UploadFilePath.IsNullOrEmpty())).ToList())
                //{
                //    foreach (var d in v.TN_MEA1101List.Where(p => !p.UploadFilePath.IsNullOrEmpty()))
                //    {
                //        try
                //        {
                //            FileHandler.UploadFTP(d.UploadFilePath, string.Format("{0}{1}/{2}/{3}/", GlobalVariable.FTP_SERVER, MasterCodeSTR.FtpFolder_InstrCheckHistory, d.InstrCode, d.Seq));
                //            d.FileUrl = string.Format("{0}/{1}/{2}/{3}", MasterCodeSTR.FtpFolder_InstrCheckHistory, d.InstrCode, d.Seq, d.FileName);
                //        }
                //        catch { }
                //    }
                //}
            }
            #endregion

            ModelService.Save();
            DataLoad();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1500;
            if (masterObj == null) return;

            var newObjCheck = masterObj.TN_MEA1501List.Where(p => p.NewRowFlag == "Y").FirstOrDefault();
            if (newObjCheck != null)
            {
                DetailGridExControl.MainGrid.MainView.FocusedRowHandle = DetailGridExControl.MainGrid.MainView.LocateByValue("Seq", newObjCheck.Seq);
            }
            else
            {
                var newObj = new TN_MEA1501()
                {
                    RBIDCode = masterObj.RBIDCode,
                    Seq = masterObj.TN_MEA1501List.Count == 0 ? 1 : masterObj.TN_MEA1501List.Max(p => p.Seq) + 1,
                    CheckId = GlobalVariable.LoginId,
                    NewRowFlag = "Y"
                    //CheckDate = DateTime.Now
                };

                masterObj.EditRowFlag = "Y";
                masterObj.TN_MEA1501List.Add(newObj);
                DetailGridBindingSource.Add(newObj);
            }
        }

        protected override void DeleteDetailRow()
        {
            TN_MEA1500 objMst = MasterGridBindingSource.Current as TN_MEA1500;
            TN_MEA1501 objDtl = DetailGridBindingSource.Current as TN_MEA1501;

            if (objDtl != null)
            {
                objMst.TN_MEA1501List.Remove(objDtl);
                DetailGridBindingSource.RemoveCurrent();
            }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1500;
            var detailObj = DetailGridBindingSource.Current as TN_MEA1501;
            if (masterObj == null || detailObj == null) return;

            var maxSeq = masterObj.TN_MEA1501List.Max(p => p.Seq);
            if (detailObj.Seq != maxSeq)
                e.Cancel = true;
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1500;
            if (masterObj == null) return;

            masterObj.EditRowFlag = "Y";

            if (e.Column.FieldName == "CheckDate")
            {
                var detailObj = DetailGridBindingSource.Current as TN_MEA1501;
                if (detailObj == null) return;

                if (masterObj.CorTurn.IsNullOrEmpty()) return;

                DateTime dt = Convert.ToDateTime(detailObj.CheckDate);

                masterObj.CorDate = dt;
                masterObj.PredictionCorDate = dt.AddMonths(masterObj.CorTurn.GetIntNullToZero());

                MasterGridExControl.MainGrid.BestFitColumns();
            }
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                object PredictionCorDate = View.GetRowCellValue(e.RowHandle, View.Columns["PredictionCorDate"]);

                if (PredictionCorDate.GetNullToEmpty() != "")
                {
                    //교정예정일이 14일 이내로 도래하면 계측기 목록에서 해당 계측기 1행이 빨간색으로 변경됨
                    if (Convert.ToDateTime(PredictionCorDate).AddDays(-14) <= DateTime.Today)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }
    }
}