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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Utils.Common;
using DevExpress.Utils;
using HKInc.Utils.Enum;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;

namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 용접지그이력관리
    /// </summary>
    public partial class XFWEL2001 : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_WEL2000> ModelService = (IService<TN_WEL2000>)ProductionFactory.GetDomainService("TN_WEL2000");
        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        public XFWEL2001()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
            MasterGridExControl.MainGrid.MainView.CustomUnboundColumnData += MainView_CustomUnboundColumnData;
            MasterGridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
            DetailGridExControl.MainGrid.MainView.RowCellStyle += DetailView_RowCellStyle;
            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += SubView_CellValueChanged;
            SubDetailGridExControl.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            

            repositoryItemGridLookUpEdit = new RepositoryItemGridLookUpEdit()
            {
                ValueMember = "CodeVal",
                DisplayMember = DataConvert.GetCultureDataFieldName("CodeName")
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
            repositoryItemGridLookUpEdit.DataSource = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG).ToList();
            repositoryItemGridLookUpEdit.View.RowHeight = 50;
            foreach (AppearanceObject ap in repositoryItemGridLookUpEdit.View.Appearance)
                ap.Font = new Font("맑은 고딕", 15f);
            GridColumn col1 = repositoryItemGridLookUpEdit.View.Columns.AddField(repositoryItemGridLookUpEdit.DisplayMember);
            col1.VisibleIndex = 0;

        }

        private void MainView_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckType").GetNullToEmpty();
                if (checkDataType == "QT1")
                    e.RepositoryItem = repositoryItemGridLookUpEdit;

            }
        }
        private void MainView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks != 2) return;
            if (e.Column.Name.ToString() == "colImage")
            {
                HKInc.Ui.View.View.POP_POPUP.XPFPOPIMG fm = new POP_POPUP.XPFPOPIMG("", e.CellValue);
                fm.Show();
            }
            if (e.Column.Name.ToString() == "colImage1")
            {
                HKInc.Ui.View.View.POP_POPUP.XPFPOPIMG fm = new POP_POPUP.XPFPOPIMG("", e.CellValue);
                fm.Show();
            }
            if (e.Column.Name.ToString() == "colImage2")
            {
                HKInc.Ui.View.View.POP_POPUP.XPFPOPIMG fm = new POP_POPUP.XPFPOPIMG("", e.CellValue);
                fm.Show();
            }
            if (e.Column.Name.ToString() == "colImage3")
            {
                HKInc.Ui.View.View.POP_POPUP.XPFPOPIMG fm = new POP_POPUP.XPFPOPIMG("", e.CellValue);
                fm.Show();
            }
            //   HKInc.Ui.View.View.POP_POPUP
        }

        protected override void InitCombo()
        {
            lup_WeldingJig.SetDefault(false, true, "WeldingJigCode", DataConvert.GetCultureDataFieldName("WeldingJigName"), ModelService.GetList(p => true).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddUnboundColumn("Image", LabelConvert.GetLabelText("Image"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            MasterGridExControl.MainGrid.AddUnboundColumn("Image1", LabelConvert.GetLabelText("Image"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            MasterGridExControl.MainGrid.AddUnboundColumn("Image2", LabelConvert.GetLabelText("Image"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            MasterGridExControl.MainGrid.AddUnboundColumn("Image3", LabelConvert.GetLabelText("Image"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);

            MasterGridExControl.MainGrid.AddColumn("WeldingJigCode", LabelConvert.GetLabelText("WeldingJigCode"));
            MasterGridExControl.MainGrid.AddColumn("WeldingJigName", LabelConvert.GetLabelText("WeldingJigName"));
            MasterGridExControl.MainGrid.AddColumn("WeldingJigNameENG", LabelConvert.GetLabelText("WeldingJigNameENG"), false);
            MasterGridExControl.MainGrid.AddColumn("WeldingJigNameCHN", LabelConvert.GetLabelText("WeldingJigNameCHN"), false);
            MasterGridExControl.MainGrid.AddColumn("WeldingJigKind", LabelConvert.GetLabelText("WeldingJigKind"), false);
            MasterGridExControl.MainGrid.AddColumn("MapCo2", LabelConvert.GetLabelText("MapCo2"));
            MasterGridExControl.MainGrid.AddColumn("MapSpot", LabelConvert.GetLabelText("MapSpot"));
            MasterGridExControl.MainGrid.AddColumn("ProdCo2", LabelConvert.GetLabelText("ProdCo2"));
            MasterGridExControl.MainGrid.AddColumn("ProdSpot", LabelConvert.GetLabelText("ProdSpot"));
            MasterGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("FileName"), false);
            MasterGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn("FileName1", LabelConvert.GetLabelText("FileName1"), false);
            MasterGridExControl.MainGrid.AddColumn("FileUrl1", LabelConvert.GetLabelText("FileUrl1"), false);
            MasterGridExControl.MainGrid.AddColumn("FileName2", LabelConvert.GetLabelText("FileName2"), false);
            MasterGridExControl.MainGrid.AddColumn("FileUrl2", LabelConvert.GetLabelText("FileUrl2"), false);
            MasterGridExControl.MainGrid.AddColumn("FileName3", LabelConvert.GetLabelText("FileName3"), false);
            MasterGridExControl.MainGrid.AddColumn("FileUrl3", LabelConvert.GetLabelText("FileUrl3"), false);
            MasterGridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"), false);
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.Columns["Image"].MinWidth = 100;
            MasterGridExControl.MainGrid.Columns["Image"].MaxWidth = 100;
            MasterGridExControl.MainGrid.Columns["Image"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            MasterGridExControl.MainGrid.Columns["Image1"].MinWidth = 100;
            MasterGridExControl.MainGrid.Columns["Image1"].MaxWidth = 100;
            MasterGridExControl.MainGrid.Columns["Image1"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            MasterGridExControl.MainGrid.Columns["Image2"].MinWidth = 100;
            MasterGridExControl.MainGrid.Columns["Image2"].MaxWidth = 100;
            MasterGridExControl.MainGrid.Columns["Image2"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            MasterGridExControl.MainGrid.Columns["Image3"].MinWidth = 100;
            MasterGridExControl.MainGrid.Columns["Image3"].MaxWidth = 100;
            MasterGridExControl.MainGrid.Columns["Image3"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            DetailGridExControl.MainGrid.AddColumn("WeldingJigCode", LabelConvert.GetLabelText("WeldingJigCode"), false);
            DetailGridExControl.MainGrid.AddColumn("CheckNo", LabelConvert.GetLabelText("WeldingJigCheckNo"));
            DetailGridExControl.MainGrid.AddColumn("CheckDate", LabelConvert.GetLabelText("QcDate"), HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("CheckId", LabelConvert.GetLabelText("WorkId"));
            DetailGridExControl.MainGrid.AddColumn("Judgement", LabelConvert.GetLabelText("Judgement"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckDate");

            SubDetailGridExControl.MainGrid.AddColumn("CheckNo", LabelConvert.GetLabelText("CheckNo"), false);
            SubDetailGridExControl.MainGrid.AddColumn("PointNo", LabelConvert.GetLabelText("PointNo"));
            SubDetailGridExControl.MainGrid.AddColumn("CheckType", LabelConvert.GetLabelText("CheckType"));
            SubDetailGridExControl.MainGrid.AddColumn("SpecDown", LabelConvert.GetLabelText("SpecDown"));
            SubDetailGridExControl.MainGrid.AddColumn("SpecUp", LabelConvert.GetLabelText("SpecUp"));
            SubDetailGridExControl.MainGrid.AddColumn("Invalue", LabelConvert.GetLabelText("JudgeValue"));
            SubDetailGridExControl.MainGrid.AddColumn("Judgement", LabelConvert.GetLabelText("Judgement"));
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Invalue");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_WEL2002>(DetailGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_WEL2003>(SubDetailGridExControl);

            var barButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonPrint.Id = 6;
            barButtonPrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            barButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barButtonPrint.Name = "barButtonPrint";
            barButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonPrint.ShortcutKeyDisplayString = "Alt+P";
            barButtonPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonPrint.Caption = LabelConvert.GetLabelText("WeldingJigRecordReportPrint") + "[Alt+P]";
            barButtonPrint.ItemClick += BarButtonPrint_ItemClick;
            barButtonPrint.Alignment = BarItemLinkAlignment.Right;
            DetailGridExControl.BarTools.AddItem(barButtonPrint);
        }

        private void MainView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                var ProdFileUrl = MasterGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "FileUrl").GetNullToEmpty();

                if (ProdFileUrl.IsNullOrEmpty()) return;
                byte[] img = FileHandler.FtpToByte(Utils.Common.GlobalVariable.HTTP_SERVER + ProdFileUrl);
                e.Value = img;
            }
            else if (e.Column.FieldName == "Image1" && e.IsGetData)
            {
                var ProdFileUrl = MasterGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "FileUrl1").GetNullToEmpty();

                if (ProdFileUrl.IsNullOrEmpty()) return;
                byte[] img = FileHandler.FtpToByte(Utils.Common.GlobalVariable.HTTP_SERVER + ProdFileUrl);
                e.Value = img;
            }
            else if (e.Column.FieldName == "Image2" && e.IsGetData)
            {
                var ProdFileUrl = MasterGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "FileUrl2").GetNullToEmpty();

                if (ProdFileUrl.IsNullOrEmpty()) return;
                byte[] img = FileHandler.FtpToByte(Utils.Common.GlobalVariable.HTTP_SERVER + ProdFileUrl);
                e.Value = img;
            }
            else if (e.Column.FieldName == "Image3" && e.IsGetData)
            {
                var ProdFileUrl = MasterGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "FileUrl3").GetNullToEmpty();

                if (ProdFileUrl.IsNullOrEmpty()) return;
                byte[] img = FileHandler.FtpToByte(Utils.Common.GlobalVariable.HTTP_SERVER + ProdFileUrl);
                e.Value = img;
            }
        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemPictureEdit("Image");
            MasterGridExControl.MainGrid.SetRepositoryItemPictureEdit("Image1");
            MasterGridExControl.MainGrid.SetRepositoryItemPictureEdit("Image2");
            MasterGridExControl.MainGrid.SetRepositoryItemPictureEdit("Image3");

            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckType", DbRequestHandler.GetCommCode(MasterCodeSTR.InspectionWay, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();
            InitCombo();

            string MachineCode = lup_WeldingJig.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => string.IsNullOrEmpty(MachineCode) ? true : p.WeldingJigCode == MachineCode
                                                                        && p.UseFlag == "Y")
                                                                        .OrderBy(p => p.WeldingJigName)
                                                                        .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            var masterObj = MasterGridBindingSource.Current as TN_WEL2000;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_WEL2002List;
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            TN_WEL2000 MasterObj = MasterGridBindingSource.Current as TN_WEL2000;
            TN_WEL2002 NewObj = new TN_WEL2002();

            NewObj.CheckNo = DbRequestHandler.GetSeqMonth("WELD");
            NewObj.WeldingJigCode = MasterObj.WeldingJigCode;
            NewObj.CheckDate = DateTime.Now;
            NewObj.CheckId = GlobalVariable.LoginId;

            DetailGridBindingSource.Add(NewObj);
        }

        protected override void DeleteDetailRow()
        {
            TN_WEL2000 MasterObj = MasterGridBindingSource.Current as TN_WEL2000;
            TN_WEL2002 DetailObj = DetailGridBindingSource.Current as TN_WEL2002;
            if (DetailObj == null) return;

            if (DetailObj.TN_WEL2003List.Count != 0)
            {
                MessageBoxHandler.Show(string.Format(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_137), HelperFactory.GetLabelConvert().GetLabelText("WeldingJigGradeJudgeDetail"), HelperFactory.GetLabelConvert().GetLabelText("WeldingJigGradeJudgeList")));
                return;
            }

            DetailGridBindingSource.RemoveCurrent();

            if (DetailObj.NewRowFlag == "N")
                ModelService.RemoveChild(DetailObj);
        }

        protected override void DetailFocusedRowChanged()
        {
            TN_WEL2002 DetailObj = DetailGridBindingSource.Current as TN_WEL2002;
            if (DetailObj == null) return;
            SubDetailGridExControl.MainGrid.Clear();
            SubDetailGridBindingSource.DataSource = DetailObj.TN_WEL2003List;
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;

            SubDetailGridExControl.BestFitColumns();
        }

        // SUB 추가
        protected override void SubDetailAddRowClicked()
        {
            TN_WEL2002 DetailObj = DetailGridBindingSource.Current as TN_WEL2002;
            if (DetailObj == null) return;

            if (DetailObj.TN_WEL2003List.Count >= 1)
            {
                MessageBoxHandler.Show(string.Format(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_138), HelperFactory.GetLabelConvert().GetLabelText("WeldingJigGradeJudgeDetail")));
                return;
            }

            List<TN_WEL2001> qclist = ModelService.GetChildList<TN_WEL2001>(p => p.WeldingJigCode == DetailObj.WeldingJigCode).OrderBy(o => o.Seq).ToList();
            if (qclist == null) { return; }

            for (int i = 0; i < qclist.Count; i++)
            {
                TN_WEL2003 NewObj = new TN_WEL2003();

                NewObj.CheckNo = DetailObj.CheckNo;
                NewObj.CheckType = qclist[i].CheckType;
                NewObj.SpecDown = qclist[i].SpecDown;
                NewObj.SpecUp = qclist[i].SpecUp;
                NewObj.PointNo = qclist[i].PointNo;
                NewObj.Invalue = "0";

                SubDetailGridBindingSource.Add(NewObj);
            }
        }

        // SUB 삭제
        protected override void DeleteSubDetailRow()
        {
            TN_WEL2002 DetailObj = DetailGridBindingSource.Current as TN_WEL2002;
            TN_WEL2003 SubdetailObj = SubDetailGridBindingSource.Current as TN_WEL2003;
            if (SubdetailObj == null) return;

            SubDetailGridBindingSource.RemoveCurrent();
        }

        private void SubView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TN_WEL2002 DetailObj = DetailGridBindingSource.Current as TN_WEL2002;
            List<TN_WEL2003> SubDetailList = SubDetailGridBindingSource.DataSource as List<TN_WEL2003>;
            TN_WEL2003 SubdetailObj = SubDetailGridBindingSource.Current as TN_WEL2003;

            if (SubdetailObj.CheckType == "QT2")
            {
                decimal up = Convert.ToDecimal(SubdetailObj.SpecUp.GetNullToZero());
                decimal down = Convert.ToDecimal(SubdetailObj.SpecDown.GetNullToZero());
                decimal inval = Convert.ToDecimal(SubdetailObj.Invalue.GetNullToZero());
                //reading1 >= checkDown && reading1 <= checkUp
                if (inval >= down && inval <= up)
                { SubdetailObj.Judgement = "OK"; }
                else { SubdetailObj.Judgement = "NG"; }
            }
            else
                SubdetailObj.Judgement = SubdetailObj.Invalue;

            if (DetailObj.TN_WEL2003List.Any(p => p.Judgement == "NG" || p.Judgement == null || p.Judgement == "ng"))
                DetailObj.Judgement = "NG";
            else if (DetailObj.TN_WEL2003List.Any(p => p.Judgement != "NG" && p.Judgement != null && p.Judgement != "ng"))
                DetailObj.Judgement = "OK";
            else
                DetailObj.Judgement = null;

            DetailGridExControl.MainGrid.BestFitColumns();
            SubDetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();
     
            ModelService.Save();
            DataLoad();
        }

        private void BarButtonPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (DetailGridBindingSource == null) return;
                if (MasterGridBindingSource.DataSource == null) return;
                if (SubDetailGridBindingSource.DataSource == null) return;
                MasterGridExControl.MainGrid.PostEditor();
                DetailGridExControl.MainGrid.PostEditor();
                SubDetailGridExControl.MainGrid.PostEditor();

                WaitHandler.ShowWait();

                TN_WEL2000 MasterObj = MasterGridBindingSource.Current as TN_WEL2000;
                TN_WEL2002 DetailObj = DetailGridBindingSource.Current as TN_WEL2002;       // 2021-10-14 김진우 주임 추가
                if (DetailObj == null)                                                      // 2021-10-14 김진우 주임 추가
                {
                    MessageBoxHandler.Show(string.Format(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_32), HelperFactory.GetLabelConvert().GetLabelText("WeldingJigRecordDetail")));
                    return;
                }
                List<TN_WEL2003> SubDetailList = DetailObj.TN_WEL2003List as List<TN_WEL2003>;  // 2021-10-14 김진우 주임 추가
                var report = new REPORT.XRCHFI_WEL2001(MasterObj, SubDetailList);

                report.CreateDocument();
                report.PrintingSystem.ShowMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                report.ShowPreview();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally
            {
                WaitHandler.CloseWait();
            }
        }

        private void DetailView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.Column.ToString() == "평가")
            {
                switch (e.CellValue)
                {
                    case "NG":
                        e.Appearance.ForeColor = Color.Red;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}