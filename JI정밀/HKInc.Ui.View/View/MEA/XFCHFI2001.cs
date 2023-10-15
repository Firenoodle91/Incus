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
    /// 검사구이력관리
    /// </summary>
    public partial class XFCHFI2001 : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_CHF2000> ModelService = (IService<TN_CHF2000>)ProductionFactory.GetDomainService("TN_CHF2000");
        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        public XFCHFI2001()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
            MasterGridExControl.MainGrid.MainView.CustomUnboundColumnData += MainView_CustomUnboundColumnData;
            MasterGridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
            DetailGridExControl.MainGrid.MainView.RowCellStyle += DetailView_RowCellStyle;
            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += SubView_CellValueChanged;
            //SubDetailGridExControl.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
        }

        protected override void InitCombo()
        {
            lup_CheckerFixture.SetDefault(false, true, "CheckerFixCode", DataConvert.GetCultureDataFieldName("CheckerFixName"), ModelService.GetList(p => true).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddUnboundColumn("Image", LabelConvert.GetLabelText("ProductImage"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            MasterGridExControl.MainGrid.AddUnboundColumn("Image1", LabelConvert.GetLabelText("MaxImage"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            MasterGridExControl.MainGrid.AddUnboundColumn("Image2", LabelConvert.GetLabelText("LowImage"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            MasterGridExControl.MainGrid.AddUnboundColumn("Image3", LabelConvert.GetLabelText("AddImage"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);

            MasterGridExControl.MainGrid.AddColumn("CheckerFixCode", LabelConvert.GetLabelText("CheckerFixCode"));
            MasterGridExControl.MainGrid.AddColumn("CheckerFixName", LabelConvert.GetLabelText("CheckerFixName"));
            MasterGridExControl.MainGrid.AddColumn("CheckerFixNameENG", LabelConvert.GetLabelText("CheckerFixNameENG"), false);
            MasterGridExControl.MainGrid.AddColumn("CheckerFixNameCHN", LabelConvert.GetLabelText("CheckerFixNameCHN"), false);
            MasterGridExControl.MainGrid.AddColumn("CheckerFixKind", LabelConvert.GetLabelText("CheckerFixKind"), false);
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

            DetailGridExControl.MainGrid.AddColumn("CheckerFixCode", LabelConvert.GetLabelText("CheckerFixCode"), false);        // 설비코드
            DetailGridExControl.MainGrid.AddColumn("CheckNo", LabelConvert.GetLabelText("CheckerFixtureCheckNo"));            // 등급관리번호
            DetailGridExControl.MainGrid.AddColumn("CheckDate", LabelConvert.GetLabelText("QcDate"), HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("CheckId", LabelConvert.GetLabelText("WorkId"));                          // 평가자
            DetailGridExControl.MainGrid.AddColumn("Judgement", LabelConvert.GetLabelText("Judgement"));                  // 평가 값
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckDate", "CheckId");                    // 2021-10-14 김진우 주임 "CheckId" 추가

            SubDetailGridExControl.MainGrid.AddColumn("CheckNo", LabelConvert.GetLabelText("CheckNo"), false);
            SubDetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), false);              /// 2021-10-12 김진우 주임 추가
            SubDetailGridExControl.MainGrid.AddColumn("PointNo", LabelConvert.GetLabelText("PointNo"));
            SubDetailGridExControl.MainGrid.AddColumn("CheckType", LabelConvert.GetLabelText("CheckType"));            
            //SubDetailGridExControl.MainGrid.AddColumn("SpecDown", LabelConvert.GetLabelText("SpecDown"));
            //SubDetailGridExControl.MainGrid.AddColumn("SpecUp", LabelConvert.GetLabelText("SpecUp"));
            SubDetailGridExControl.MainGrid.AddColumn("Invalue", LabelConvert.GetLabelText("JudgeValue"));
            SubDetailGridExControl.MainGrid.AddColumn("Judgement", LabelConvert.GetLabelText("Judgement"));
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Invalue");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_CHF2002>(DetailGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_CHF2003>(SubDetailGridExControl);


            var barButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonPrint.Id = 6;
            barButtonPrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            barButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barButtonPrint.Name = "barButtonPrint";
            barButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonPrint.ShortcutKeyDisplayString = "Alt+P";
            barButtonPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonPrint.Caption = LabelConvert.GetLabelText("CheckerFixtureRecordReportPrint") + "[Alt+P]";
            barButtonPrint.ItemClick += BarButtonPrint_ItemClick;
            barButtonPrint.Alignment = BarItemLinkAlignment.Right;
            DetailGridExControl.BarTools.AddItem(barButtonPrint);


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
 
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemPictureEdit("Image");
            MasterGridExControl.MainGrid.SetRepositoryItemPictureEdit("Image1");
            MasterGridExControl.MainGrid.SetRepositoryItemPictureEdit("Image2");
            MasterGridExControl.MainGrid.SetRepositoryItemPictureEdit("Image3");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");           // 2021-10-14 김진우 주임 추가

            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Invalue", DbRequestHandler.GetCommCode(MasterCodeSTR.Judge_OKNG, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));             // 2021-10-14 김진우 주임 추가
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

            string MachineCode = lup_CheckerFixture.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => string.IsNullOrEmpty(MachineCode) ? true : p.CheckerFixCode == MachineCode
                                                                        && p.UseFlag == "Y")
                                                                        .OrderBy(p => p.CheckerFixName)
                                                                        .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            TN_CHF2000 masterObj = MasterGridBindingSource.Current as TN_CHF2000;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
        
            DetailGridBindingSource.DataSource = masterObj.TN_CHF2002List;
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            TN_CHF2000 MasterObj = MasterGridBindingSource.Current as TN_CHF2000;
            TN_CHF2002 NewObj = new TN_CHF2002();

            NewObj.CheckNo = DbRequestHandler.GetSeqMonth("CHFI");
            NewObj.CheckerFixCode = MasterObj.CheckerFixCode;
            NewObj.CheckDate = DateTime.Now;
            NewObj.CheckId = GlobalVariable.LoginId;

            DetailGridBindingSource.Add(NewObj);
        }

        protected override void DeleteDetailRow()
        {
            TN_CHF2000 MasterObj = MasterGridBindingSource.Current as TN_CHF2000;
            TN_CHF2002 DetailObj = DetailGridBindingSource.Current as TN_CHF2002;
            if (DetailObj == null) return;

            if (DetailObj.TN_CHF2003List.Count != 0)
            {
                MessageBoxHandler.Show(string.Format(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_137), HelperFactory.GetLabelConvert().GetLabelText("CheckerFixRecordDetail"), HelperFactory.GetLabelConvert().GetLabelText("CheckerFixRecordDetail")));
                return;
            }
            DetailGridBindingSource.RemoveCurrent();
  
            if (DetailObj.NewRowFlag == "N")
                ModelService.RemoveChild(DetailObj);
           
        }

        protected override void DetailFocusedRowChanged()
        {
            TN_CHF2002 DetailObj = DetailGridBindingSource.Current as TN_CHF2002;
            if (DetailObj == null) return;
            SubDetailGridExControl.MainGrid.Clear();
            SubDetailGridBindingSource.DataSource = DetailObj.TN_CHF2003List;
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;

            SubDetailGridExControl.BestFitColumns();
        }
      
        // SUB 추가
        protected override void SubDetailAddRowClicked()
        {
            TN_CHF2002 DetailObj = DetailGridBindingSource.Current as TN_CHF2002;
            if (DetailObj == null) return;

            if (DetailObj.TN_CHF2003List.Count >= 1)
            {
                MessageBoxHandler.Show(string.Format(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_138), HelperFactory.GetLabelConvert().GetLabelText("CheckerFixtureGradeJudgeDetail")));
                return;
            }

            List<TN_CHF2001> qclist = ModelService.GetChildList<TN_CHF2001>(p => p.CheckerFixCode == DetailObj.CheckerFixCode).OrderBy(o => o.Seq).ToList();
            if (qclist == null) { return; }

            for(int i=0;i< qclist.Count;i++)
            {
                TN_CHF2003 NewObj = new TN_CHF2003();

                NewObj.CheckNo = DetailObj.CheckNo;
                NewObj.Seq = DetailObj.TN_CHF2003List.Count == 0 ? 1 : DetailObj.TN_CHF2003List.Max(p => p.Seq) + 1;        // 2021-10-14 김진우 주임 추가
                NewObj.CheckType = qclist[i].CheckType;
                //NewObj.SpecDown = qclist[i].SpecDown;
                //NewObj.SpecUp = qclist[i].SpecUp;
                NewObj.PointNo = qclist[i].PointNo;
                NewObj.Invalue = "";
                NewObj.Judgement = "";      // 2021-10-14 김진우 주임 추가

                SubDetailGridBindingSource.Add(NewObj);
            }
            DetailObj.Judgement = "";       // 2021-10-14 김진우 주임 추가
        }

        // SUB 삭제
        protected override void DeleteSubDetailRow()
        {
            TN_CHF2002 DetailObj = DetailGridBindingSource.Current as TN_CHF2002;
            TN_CHF2003 SubdetailObj = SubDetailGridBindingSource.Current as TN_CHF2003;
            if (SubdetailObj == null) return;
          
            SubDetailGridBindingSource.RemoveCurrent();
        }

        private void SubView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TN_CHF2002 DetailObj = DetailGridBindingSource.Current as TN_CHF2002;
            List<TN_CHF2003> SubDetailList = SubDetailGridBindingSource.DataSource as List<TN_CHF2003>;
            TN_CHF2003 SubdetailObj = SubDetailGridBindingSource.Current as TN_CHF2003;

            //if (SubdetailObj.CheckType == "QT2")
            //{
            //    decimal up = Convert.ToDecimal(SubdetailObj.SpecUp.GetNullToZero());
            //    decimal down = Convert.ToDecimal(SubdetailObj.SpecDown.GetNullToZero());
            //    decimal inval = Convert.ToDecimal(SubdetailObj.Invalue.GetNullToZero());
            //    //reading1 >= checkDown && reading1 <= checkUp
            //    if (inval >= down && inval <= up)
            //    { SubdetailObj.Judgement = "OK"; }
            //    else { SubdetailObj.Judgement = "NG"; }
            //}
            //else
            //    SubdetailObj.Judgement = SubdetailObj.Invalue;

            SubdetailObj.Judgement = SubdetailObj.Invalue;

            if (DetailObj.TN_CHF2003List.Any(p => p.Judgement == "NG" || p.Judgement == ""))
                DetailObj.Judgement = "NG";
            else
                DetailObj.Judgement = "OK";
                       
            //if (DetailObj.TN_CHF2003List.Any(p => p.Judgement.ToUpper() == "NG" || p.Judgement == null || p.Judgement == "ng"))
            //    DetailObj.Judgement = "NG";
            //else if (DetailObj.TN_CHF2003List.Any(p => p.Judgement != "NG" && p.Judgement != null && p.Judgement != "ng"))
            //    DetailObj.Judgement = "OK";
            //else
            //    DetailObj.Judgement = null;

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

        /// <summary>
        /// 검사구이력서 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                TN_CHF2000 MasterObj = MasterGridBindingSource.Current as TN_CHF2000;
                TN_CHF2002 DetailObj = DetailGridBindingSource.Current as TN_CHF2002;       // 2021-10-14 김진우 주임 추가
                if (DetailObj == null)                                                      // 2021-10-14 김진우 주임 추가
                {
                    MessageBoxHandler.Show(string.Format(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_32), HelperFactory.GetLabelConvert().GetLabelText("WeldingJigRecordList")));
                    return;
                }
                List<TN_CHF2003> SubDetailList = DetailObj.TN_CHF2003List as List<TN_CHF2003>;  // 2021-10-14 김진우 주임 추가
                //List<TN_CHF2003> SubDetailList = SubDetailGridBindingSource.DataSource as List<TN_CHF2003>;

                var report = new REPORT.XRCHFI_WEL2002(MasterObj, SubDetailList);           // 2021-10-14 김진우 주임 추가
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

        /// <summary>
        /// 검사방법이 육안일경우 LOOKUP 형태로 표시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 이미지 더블클릭시 팝업으로 미리보기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

    }
}