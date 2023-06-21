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
using DevExpress.XtraGrid.Views.Base;

namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 용접지그관리
    /// </summary>
    public partial class XFWEL2000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_WEL2000> ModelService = (IService<TN_WEL2000>)ProductionFactory.GetDomainService("TN_WEL2000");

        public XFWEL2000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += View_CellValueChanged;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }
        

        /// <summary>
        /// 2021-07-05 김진우 주임 수정
        /// PointNo 중복체크 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TN_WEL2000 masterObj = MasterGridBindingSource.Current as TN_WEL2000;
            TN_WEL2001 DetailObj = DetailGridBindingSource.Current as TN_WEL2001;
            List<TN_WEL2001> DetailList = masterObj.TN_WEL2001List as List<TN_WEL2001>;
            List<TN_WEL2001> OtherList = DetailList.Where(x => x.Seq != DetailObj.Seq).ToList() as List<TN_WEL2001>;

            if (e.Column.Name.ToString() == "CheckType")
            {
                if (DetailObj.CheckType != "QT2" && DetailObj.CheckType != "QT1")
                {
                    DetailObj.SpecUp = null;
                    DetailObj.SpecDown = null;
                }
            }
            if (e.Column.Name.ToString() == "PointNo")
            {
                if (OtherList.Any(p => p.PointNo == DetailObj.PointNo))
                {
                    MessageBoxHandler.Show("검사포인트는 중복이 될수 없습니다.");
                    DetailObj.PointNo = OtherList.Max(m => m.PointNo) + 1;
                }
            }

        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("WeldingJigCode", LabelConvert.GetLabelText("WeldingJigCode"));
            MasterGridExControl.MainGrid.AddColumn("WeldingJigName", LabelConvert.GetLabelText("WeldingJigName"));
            MasterGridExControl.MainGrid.AddColumn("WeldingJigNameENG", LabelConvert.GetLabelText("WeldingJigNameENG"));
            MasterGridExControl.MainGrid.AddColumn("WeldingJigNameCHN", LabelConvert.GetLabelText("WeldingJigNameCHN"));
            MasterGridExControl.MainGrid.AddColumn("WeldingJigKind", LabelConvert.GetLabelText("WeldingJigKind"));
            MasterGridExControl.MainGrid.AddColumn("MapCo2", LabelConvert.GetLabelText("MapCo2"));
            MasterGridExControl.MainGrid.AddColumn("MapSpot", LabelConvert.GetLabelText("MapSpot"));
            MasterGridExControl.MainGrid.AddColumn("ProdCo2", LabelConvert.GetLabelText("ProdCo2"));
            MasterGridExControl.MainGrid.AddColumn("ProdSpot", LabelConvert.GetLabelText("ProdSpot"));
            MasterGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("FileName"));
            MasterGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"),false);
            MasterGridExControl.MainGrid.AddColumn("FileName1", LabelConvert.GetLabelText("FileName1"));
            MasterGridExControl.MainGrid.AddColumn("FileUrl1", LabelConvert.GetLabelText("FileUrl1"), false);
            MasterGridExControl.MainGrid.AddColumn("FileName2", LabelConvert.GetLabelText("FileName2"));
            MasterGridExControl.MainGrid.AddColumn("FileUrl2", LabelConvert.GetLabelText("FileUrl2"), false);
            MasterGridExControl.MainGrid.AddColumn("FileName3", LabelConvert.GetLabelText("FileName3"));
            MasterGridExControl.MainGrid.AddColumn("FileUrl3", LabelConvert.GetLabelText("FileUrl3"), false);
            MasterGridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            MasterGridExControl.MainGrid.Columns["FileName"].MaxWidth = 150;
            MasterGridExControl.MainGrid.Columns["FileName1"].MaxWidth = 150;
            MasterGridExControl.MainGrid.Columns["FileName2"].MaxWidth = 150;
            MasterGridExControl.MainGrid.Columns["FileName3"].MaxWidth = 150;

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WeldingJigName", "WeldingJigNameENG", "WeldingJigNameCHN",
                                                       "WeldingJigKind", "MapCo2", "MapSpot", "ProdCo2", "ProdSpot", "FileName", "FileUrl", "FileName1", "FileUrl1",
                                                       "FileName2", "FileUrl2", "FileName3", "FileUrl3", "UseFlag", "Memo");
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("WeldingJigCode", LabelConvert.GetLabelText("WeldingJigCode"));
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"));
            DetailGridExControl.MainGrid.AddColumn("PointNo", LabelConvert.GetLabelText("PointNo"));
            DetailGridExControl.MainGrid.AddColumn("CheckType", LabelConvert.GetLabelText("CheckType"));
            DetailGridExControl.MainGrid.AddColumn("SpecDown", LabelConvert.GetLabelText("SpecDown"));
            DetailGridExControl.MainGrid.AddColumn("SpecUp", LabelConvert.GetLabelText("SpecUp"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PointNo", "CheckType", "SpecUp", "SpecDown", "Memo");

            // 2021-06-25 김진우 주임 추가
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_WEL2000>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_WEL2001>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WeldingJigKind", DbRequestHandler.GetCommCode(MasterCodeSTR.WeldingJigKind, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);
            MasterGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, MasterGridExControl, MasterCodeSTR.FtpFolder_WeldingJig, "FileName", "FileUrl");
            MasterGridExControl.MainGrid.MainView.Columns["FileName1"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, MasterGridExControl, MasterCodeSTR.FtpFolder_WeldingJig, "FileName1", "FileUrl1");
            MasterGridExControl.MainGrid.MainView.Columns["FileName2"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, MasterGridExControl, MasterCodeSTR.FtpFolder_WeldingJig, "FileName2", "FileUrl2");
            MasterGridExControl.MainGrid.MainView.Columns["FileName3"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, MasterGridExControl, MasterCodeSTR.FtpFolder_WeldingJig, "FileName3", "FileUrl3");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckType", DbRequestHandler.GetCommCode(MasterCodeSTR.InspectionWay, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            DetailGridRowLocator.GetCurrentRow("Seq");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();
            InitCombo();

            var InstrCodeName = tx_InstrCodeName.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.WeldingJigCode.Contains(InstrCodeName) || p.WeldingJigName.Contains(InstrCodeName) || p.WeldingJigNameENG.Contains(InstrCodeName) || p.WeldingJigNameCHN.Contains(InstrCodeName))
                                                                    && (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                               )
                                                               .OrderBy(p => p.WeldingJigName)
                                                               .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);
            GridRowLocator.SetCurrentRow();
            DetailGridRowLocator.SetCurrentRow();
        }

        protected override void AddRowClicked()
        {
            TN_WEL2000 obj = new TN_WEL2000()
            {
                WeldingJigCode = DbRequestHandler.GetSeqStandard("WELD"),
                UseFlag = "Y"
            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);

        }
        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_WEL2000;
            if (masterObj == null) return;
            // 2021-06-25 김진우 주임 추가
            var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("WeldingJig")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                masterObj.UseFlag = "N";
            MasterGridExControl.BestFitColumns();
        
        }
        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_WEL2000;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_WEL2001List.OrderBy(p => p.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            #region 파일 CHECK
            var masterList = MasterGridBindingSource.List as List<TN_WEL2000>;

            if (masterList.Count > 0)
            {
                foreach (var d in masterList.Where(c => c.FileUrl != null && c.FileUrl.Contains("\\")).ToList())
                {
                    string[] filename = d.FileUrl.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = d.WeldingJigCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_WeldingJig + "/" + realFileName;

                        FileHandler.UploadFTP(d.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_WeldingJig + "/");

                        d.FileName = realFileName;
                        d.FileUrl = ftpFileUrl;
                    }
                }
                foreach (var d in masterList.Where(c => c.FileUrl != null && c.FileUrl.Contains("\\")).ToList())
                {
                    string[] filename = d.FileUrl.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = d.WeldingJigCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_WeldingJig + "/" + realFileName;

                        FileHandler.UploadFTP(d.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_WeldingJig + "/");

                        d.FileName = realFileName;
                        d.FileUrl = ftpFileUrl;
                    }
                }
                foreach (var d in masterList.Where(c => c.FileUrl1 != null && c.FileUrl1.Contains("\\")).ToList())
                {
                    string[] filename = d.FileUrl1.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = d.WeldingJigCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_WeldingJig + "/" + realFileName;

                        FileHandler.UploadFTP(d.FileUrl1, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_WeldingJig + "/");

                        d.FileName1 = realFileName;
                        d.FileUrl1 = ftpFileUrl;
                    }
                }
                foreach (var d in masterList.Where(c => c.FileUrl2 != null && c.FileUrl2.Contains("\\")).ToList())
                {
                    string[] filename = d.FileUrl2.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = d.WeldingJigCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_WeldingJig + "/" + realFileName;

                        FileHandler.UploadFTP(d.FileUrl2, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_WeldingJig + "/");

                        d.FileName2 = realFileName;
                        d.FileUrl2 = ftpFileUrl;
                    }
                }
                foreach (var d in masterList.Where(c => c.FileUrl3 != null && c.FileUrl3.Contains("\\")).ToList())
                {
                    string[] filename = d.FileUrl3.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = d.WeldingJigCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_WeldingJig + "/" + realFileName;

                        FileHandler.UploadFTP(d.FileUrl3, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_WeldingJig + "/");

                        d.FileName3 = realFileName;
                        d.FileUrl3 = ftpFileUrl;
                    }
                }

            }
            #endregion

            ModelService.Save();
            DataLoad();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_WEL2000;
            if (masterObj == null) return;

            var NewObj = new TN_WEL2001();

            NewObj.WeldingJigCode = masterObj.WeldingJigCode;
            NewObj.Seq = masterObj.TN_WEL2001List.Count == 0 ? 1 : masterObj.TN_WEL2001List.Max(p => p.Seq) + 1;
            NewObj.PointNo = masterObj.TN_WEL2001List.Count == 0 ? 1 : masterObj.TN_WEL2001List.Max(m => m.PointNo) + 1;
            NewObj.NewRowFlag = "Y";

            masterObj.EditRowFlag = "Y";
            masterObj.TN_WEL2001List.Add(NewObj);
            DetailGridBindingSource.Add(NewObj);
        }

        protected override void DeleteDetailRow()
        {
            TN_WEL2000 objMst = MasterGridBindingSource.Current as TN_WEL2000;
            TN_WEL2001 objDtl = DetailGridBindingSource.Current as TN_WEL2001;

            if (objDtl != null)
            {
                objMst.TN_WEL2001List.Remove(objDtl);
                DetailGridBindingSource.RemoveCurrent();
            }
        }

    }
}