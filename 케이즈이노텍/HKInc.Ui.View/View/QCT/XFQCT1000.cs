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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Service.Helper;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Common;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using HKInc.Utils.Enum;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;

namespace HKInc.Ui.View.View.QCT
{
    /// <summary>
    /// 검사규격관리
    /// </summary>
    public partial class XFQCT1000 : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        RepositoryItemSpinEdit repositoryItemSpinEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;

        public XFQCT1000()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellValueChanged;

            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += SubDetailMainView_CellValueChanged;
            SubDetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            SubDetailGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            SubDetailGridExControl.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            SubDetailGridExControl.MainGrid.MainView.KeyDown += MainView_KeyDown;

            repositoryItemSpinEdit = new RepositoryItemSpinEdit();
            repositoryItemSpinEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            repositoryItemSpinEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repositoryItemSpinEdit.Mask.UseMaskAsDisplayFormat = true;
            repositoryItemSpinEdit.AllowMouseWheel = true;
            repositoryItemSpinEdit.Buttons[0].Visible = false;

            repositoryItemTextEdit = new RepositoryItemTextEdit();

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        private void DetailMainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_QCT1000;
            if (detailObj == null) return;

            detailObj.EditRowFlag = "Y";
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetList(p => p.UseFlag == "Y" && (p.TopCategory != MasterCodeSTR.TopCategory_SPARE && p.TopCategory != MasterCodeSTR.TopCategory_TOOL)).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            MasterGridExControl.MainGrid.AddColumn("CustomerItemCode", LabelConvert.GetLabelText("CustomerItemCode"));
            MasterGridExControl.MainGrid.AddColumn("CustomerItemName", LabelConvert.GetLabelText("CustomerItemName"));
            MasterGridExControl.MainGrid.AddColumn("CombineSpec", LabelConvert.GetLabelText("Spec"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.MainGrid.AddColumn("RevNo", LabelConvert.GetLabelText("RevNo"));
            DetailGridExControl.MainGrid.AddColumn("RevDate", LabelConvert.GetLabelText("RevDate"));
            DetailGridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"));
            DetailGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("CheckReport"), false);
            DetailGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            DetailGridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            DetailGridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "RevDate", "UseFlag", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_QCT1000>(DetailGridExControl);

            var barButtonItemCopy = new DevExpress.XtraBars.BarButtonItem();
            barButtonItemCopy.Id = 4;
            barButtonItemCopy.ImageOptions.Image = IconImageList.GetIconImage("miscellaneous/wizard");
            barButtonItemCopy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.I));
            barButtonItemCopy.Name = "barButtonItemCopy";
            barButtonItemCopy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonItemCopy.ShortcutKeyDisplayString = "Alt+I";
            barButtonItemCopy.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonItemCopy.Caption = LabelConvert.GetLabelText("OtherItemCopy") + "[Alt+I]";
            barButtonItemCopy.ItemClick += BarButtonItemCopy_ItemClick;
            DetailGridExControl.BarTools.AddItem(barButtonItemCopy);

            SubDetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "Seq", true);
            SubDetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            SubDetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), false);
            SubDetailGridExControl.MainGrid.AddColumn("DisplayOrder", LabelConvert.GetLabelText("DisplayOrder"));                       //표시순서
            SubDetailGridExControl.MainGrid.AddColumn("CheckDivision", LabelConvert.GetLabelText("InspectionDivision"));                //검사구분
            SubDetailGridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));                          //검사방법
            SubDetailGridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"));                        //검사항목
            SubDetailGridExControl.MainGrid.AddColumn("InspCheckPosition", LabelConvert.GetLabelText("InspCheckPosition"));             //검사위치
            SubDetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessCode"));                         //공정코드
            SubDetailGridExControl.MainGrid.AddColumn("InspectionReportFlag", LabelConvert.GetLabelText("InspectionReportFlag"));       //성적서사용여부
            SubDetailGridExControl.MainGrid.AddColumn("InspectionReportMemo", LabelConvert.GetLabelText("InspectionReportMemo"));       //규격?
            SubDetailGridExControl.MainGrid.AddColumn("CheckDataType", LabelConvert.GetLabelText("CheckDataType"));                     //검사데이터타입
            SubDetailGridExControl.MainGrid.AddColumn("CheckMin", LabelConvert.GetLabelText("CheckMin"), false);                        //하한값
            SubDetailGridExControl.MainGrid.AddColumn("CheckMax", LabelConvert.GetLabelText("CheckMax"), false);                        //상한값
            SubDetailGridExControl.MainGrid.AddColumn("CheckUpQuad", LabelConvert.GetLabelText("CheckUpQuad"), true);                   //상한공차
            SubDetailGridExControl.MainGrid.AddColumn("CheckDownQuad", LabelConvert.GetLabelText("CheckDownQuad"), true);               //하한공차
            SubDetailGridExControl.MainGrid.AddColumn("MaxReading", LabelConvert.GetLabelText("MaxReading"));                           //최대시료수
            SubDetailGridExControl.MainGrid.AddColumn("InstrumentCode", LabelConvert.GetLabelText("InstrName"));                        //계측기명
            SubDetailGridExControl.MainGrid.AddColumn("InstrumentNo", LabelConvert.GetLabelText("InstrumentNo"));                       //계측기NO
            SubDetailGridExControl.MainGrid.AddColumn("CheckSpec", LabelConvert.GetLabelText("CheckSpec"), false);                      //규격
            //SubDetailGridExControl.MainGrid.AddColumn("SpcFlag", LabelConvert.GetLabelText("SpcFlag"));                               //SPC사용여부
            //SubDetailGridExControl.MainGrid.AddColumn("SpcDivision", LabelConvert.GetLabelText("SpcDivision"));                       //SPC구분
            SubDetailGridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));                                 //사용여부
            SubDetailGridExControl.MainGrid.AddColumn("InstrMemo", LabelConvert.GetLabelText("InstrMemo"));                             //검사기준항목
            SubDetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));                                       //메모

            var barButtonCheckDivisionCopy = new DevExpress.XtraBars.BarButtonItem();
            barButtonCheckDivisionCopy.Id = 4;
            barButtonCheckDivisionCopy.ImageOptions.Image = IconImageList.GetIconImage("miscellaneous/wizard");
            barButtonCheckDivisionCopy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.C));
            barButtonCheckDivisionCopy.Name = "barButtonCheckDivisionCopy";
            barButtonCheckDivisionCopy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonCheckDivisionCopy.ShortcutKeyDisplayString = "Alt+C";
            barButtonCheckDivisionCopy.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonCheckDivisionCopy.Caption = LabelConvert.GetLabelText("CheckDivisionCopy") + "[Alt+C]";
            barButtonCheckDivisionCopy.ItemClick += BarButtonCheckDivisionCopy_ItemClick;
            SubDetailGridExControl.BarTools.AddItem(barButtonCheckDivisionCopy);

            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "DisplayOrder", "ProcessCode", "InspectionReportFlag", "InspectionReportMemo",
                "InspCheckPosition", "CheckDivision", "CheckWay", "CheckList", "InstrumentCode", "CheckDataType", "CheckDownQuad", "CheckUpQuad", "UseFlag", "Memo", "InstrumentNo", "MaxReading");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_QCT1001>(SubDetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("RevDate", true);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, DetailGridExControl, MasterCodeSTR.FtpFolder_Inspection_IN_File, "FileName", "FileUrl");

            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MaxReading", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("DisplayOrder");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("InspectionReportFlag", "N");
            //SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("SpcFlag", "N");
            //SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SpcDivision", DbRequestHandler.GetCommTopCode(MasterCodeSTR.SpcDivision), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDivision", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDivision), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionItem), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InstrumentCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InstrumentCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InstrumentCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDataType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDataType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InspCheckPosition", DbRequestHandler.GetCommCode(MasterCodeSTR.InspectionCheckPosition, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            SubDetailGridExControl.MainGrid.MainView.Columns["InstrMemo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(SubDetailGridExControl, "InstrMemo", UserRight.HasEdit);
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(SubDetailGridExControl, "Memo", UserRight.HasEdit);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");
            DetailGridRowLocator.GetCurrentRow("RevNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();
            InitCombo();

            var itemCode = lup_Item.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                        && (p.UseFlag == "Y")
                                                                        && (p.TopCategory != MasterCodeSTR.TopCategory_SPARE && p.TopCategory != MasterCodeSTR.TopCategory_TOOL)
                                                                     )
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
            {
                return;
            }

            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            DetailGridBindingSource.DataSource = masterObj.TN_QCT1000List.Where(p => (radioValue == "A" ? true : p.UseFlag == radioValue)).OrderBy(p => p.RevNo).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();
        }

        protected override void DetailFocusedRowChanged()
        {
            SubDetailGridExControl.MainGrid.Clear();

            var detailObj = DetailGridBindingSource.Current as TN_QCT1000;
            if (detailObj == null)
            {
                return;
            }

            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            SubDetailGridBindingSource.DataSource = detailObj.TN_QCT1001List.Where(p => (radioValue == "A" ? true : p.UseFlag == radioValue)).OrderBy(p => p.CheckDivision).ThenBy(p => p.ProcessCode).ThenBy(p => p.DisplayOrder).ToList();
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;

            var newObjCheck = masterObj.TN_QCT1000List.Where(p => p.NewRowFlag == "Y").FirstOrDefault();
            if (newObjCheck != null)
            {
                DetailGridExControl.MainGrid.MainView.FocusedRowHandle = DetailGridExControl.MainGrid.MainView.LocateByValue("RevNo", newObjCheck.RevNo);
            }
            else
            {
                var newObj = new TN_QCT1000()
                {
                    RevNo = masterObj.TN_QCT1000List.Count == 0 ? "Rev00" : "Rev" + masterObj.TN_QCT1000List.Count.ToString().PadLeft(2, '0'),
                    RevDate = DateTime.Today,
                    ItemCode = masterObj.ItemCode,
                    UseFlag = "Y",
                    NewRowFlag = "Y"
                };

                DetailGridBindingSource.Add(newObj);
                masterObj.TN_QCT1000List.Add(newObj);
                DetailGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;

            var detailObj = DetailGridBindingSource.Current as TN_QCT1000;
            if (detailObj == null)
                return;

            var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("RevInfo")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                detailObj.UseFlag = "N";
                DetailGridExControl.BestFitColumns();
            }

            //if (detailObj.TN_QCT1001List.Count > 0)
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("RevInfo"), LabelConvert.GetLabelText("InspectionInfo"), LabelConvert.GetLabelText("InspectionInfo")));
            //    return;
            //}

            //DetailGridBindingSource.RemoveCurrent(); ;
            //masterObj.TN_QCT1000List.Remove(detailObj);
            //DetailGridExControl.BestFitColumns();
        }

        protected override void SubDetailAddRowClicked()
        {
            var detailObj = DetailGridBindingSource.Current as TN_QCT1000;
            if (detailObj == null)
                return;

            var inspchkp = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == detailObj.ItemCode && p.UseFlag == "Y" && p.StockInspFlag == "Y").LastOrDefault();

            //등록된 ROW가 한개도 없을 경우 전 버전 불러오기 (사용여부 Y로)
            //if (detailObj.TN_QCT1001List.Count == 0)
            //{
            //    var RevNoSeq = detailObj.RevNo.Substring(3);
            //    var PreRevNo = "Rev" + (RevNoSeq.GetIntNullToZero() - 1).GetNullToEmpty();
            //    var PreObj = ModelService.GetChildList<TN_QCT1000>(p => p.UseFlag == "Y").Where(p => (p.RevNo.Substring(3).GetIntNullToZero()) < (detailObj.RevNo.Substring(3).GetIntNullToZero())).OrderBy(p => p.RowId).LastOrDefault();

            //    if (PreObj == null)
            //    {

            //        if (inspchkp.MiddleCategory.GetNullToEmpty() != "")
            //        {

            //            if (inspchkp.MiddleCategory.Substring(3, 2).ToString() == "03")
            //            {

            //                int seq = 0;

            //                for (int i = 0; i < 2; i++)
            //                {
            //                    var newObj = new TN_QCT1001()
            //                    {
            //                        RevNo = detailObj.RevNo,
            //                        Seq = detailObj.TN_QCT1001List.Count == 0 ? 1 : detailObj.TN_QCT1001List.Max(p => p.Seq) + 1,
            //                        ItemCode = detailObj.ItemCode,
            //                        InspectionReportFlag = "N",
            //                        SpcFlag = "N",
            //                        UseFlag = "Y",
            //                        NewRowFlag = "Y"
            //                    };
            //                    SubDetailGridBindingSource.Add(newObj);
            //                    detailObj.TN_QCT1001List.Add(newObj);
            //                    seq++;
            //                }
            //            }
            //            else
            //            {
            //                var newObj = new TN_QCT1001()
            //                {
            //                    RevNo = detailObj.RevNo,
            //                    Seq = detailObj.TN_QCT1001List.Count == 0 ? 1 : detailObj.TN_QCT1001List.Max(p => p.Seq) + 1,
            //                    ItemCode = detailObj.ItemCode,
            //                    InspectionReportFlag = "N",
            //                    SpcFlag = "N",
            //                    UseFlag = "Y",
            //                    NewRowFlag = "Y"
            //                };
            //                SubDetailGridBindingSource.Add(newObj);
            //                detailObj.TN_QCT1001List.Add(newObj);
            //            }

            //        }
            //        else
            //        {
            //            var newObj = new TN_QCT1001()
            //            {
            //                RevNo = detailObj.RevNo,
            //                Seq = detailObj.TN_QCT1001List.Count == 0 ? 1 : detailObj.TN_QCT1001List.Max(p => p.Seq) + 1,
            //                ItemCode = detailObj.ItemCode,
            //                InspectionReportFlag = "N",
            //                SpcFlag = "N",
            //                UseFlag = "Y",
            //                NewRowFlag = "Y"
            //            };
            //            SubDetailGridBindingSource.Add(newObj);
            //            detailObj.TN_QCT1001List.Add(newObj);
            //        }


            //        //var newObj = new TN_QCT1001()
            //        //{
            //        //    RevNo = detailObj.RevNo,
            //        //    Seq = detailObj.TN_QCT1001List.Count == 0 ? 1 : detailObj.TN_QCT1001List.Max(p => p.Seq) + 1,
            //        //    ItemCode = detailObj.ItemCode,
            //        //    InspectionReportFlag = "N",
            //        //    SpcFlag = "N",
            //        //    UseFlag = "Y",
            //        //    NewRowFlag = "Y"
            //        //};
            //        //SubDetailGridBindingSource.Add(newObj);
            //        //detailObj.TN_QCT1001List.Add(newObj);
            //    }
            //    else
            //    {
            //        foreach (var v in PreObj.TN_QCT1001List.Where(p => p.UseFlag == "Y").OrderBy(p => p.Seq).ToList())
            //        {
            //            var newObj = new TN_QCT1001()
            //            {
            //                RevNo = detailObj.RevNo,
            //                Seq = detailObj.TN_QCT1001List.Count == 0 ? 1 : detailObj.TN_QCT1001List.Max(p => p.Seq) + 1,
            //                ItemCode = detailObj.ItemCode,
            //                DisplayOrder = v.DisplayOrder,
            //                ProcessCode = v.ProcessCode,
            //                InspectionReportFlag = v.InspectionReportFlag,
            //                InspectionReportMemo = v.InspectionReportMemo,
            //                SpcFlag = v.SpcFlag,
            //                SpcDivision = v.SpcDivision,
            //                CheckDivision = v.CheckDivision,
            //                CheckWay = v.CheckWay,
            //                CheckList = v.CheckList,
            //                CheckMax = v.CheckMax,
            //                CheckMin = v.CheckMin,
            //                CheckSpec = v.CheckSpec,
            //                CheckUpQuad = v.CheckUpQuad,
            //                CheckDownQuad = v.CheckDownQuad,
            //                InstrumentCode = v.InstrumentCode,
            //                CheckDataType = v.CheckDataType,
            //                UseFlag = v.UseFlag,
            //                Memo = v.Memo,
            //                Temp = v.Temp,
            //                Temp1 = v.Temp1,
            //                Temp2 = v.Temp2,
            //                NewRowFlag = "Y"
            //            };
            //            SubDetailGridBindingSource.Add(newObj);
            //            detailObj.TN_QCT1001List.Add(newObj);
            //        }
            //    }
            //}
            //else
            //{
                var newObj = new TN_QCT1001()
                {
                    RevNo = detailObj.RevNo,
                    Seq = detailObj.TN_QCT1001List.Count == 0 ? 1 : detailObj.TN_QCT1001List.Max(p => p.Seq) + 1,
                    ItemCode = detailObj.ItemCode,
                    InspectionReportFlag = "N",
                    SpcFlag = "N",
                    UseFlag = "Y",
                    NewRowFlag = "Y"
                };
                SubDetailGridBindingSource.Add(newObj);
                detailObj.TN_QCT1001List.Add(newObj);
            //}
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void DeleteSubDetailRow()
        {
            var detailObj = DetailGridBindingSource.Current as TN_QCT1000;
            if (detailObj == null)
                return;

            var subObj = SubDetailGridBindingSource.Current as TN_QCT1001;
            if (subObj == null)
                return;

            var subList = SubDetailGridBindingSource.List as List<TN_QCT1001>;
            if (subList == null || subList.Count == 0) return;

            if (subObj.NewRowFlag == "Y")
            {
                detailObj.TN_QCT1001List.Remove(subObj);
                SubDetailGridBindingSource.RemoveCurrent();
                SubDetailGridExControl.BestFitColumns();
                return;
            }

            var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("InspectionInfo")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var checkList = subList.Where(p => p._Check == "Y").ToList();
                if (checkList.Count == 0)
                {
                    subObj.UseFlag = "N";
                }
                else
                {
                    foreach (var v in checkList)
                    {
                        v.UseFlag = "N";
                        v._Check = "N";
                    }
                }
                SubDetailGridExControl.BestFitColumns();
            }
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();
            SubDetailGridExControl.MainGrid.PostEditor();

            if (DetailGridBindingSource != null && DetailGridBindingSource.DataSource != null)
            {
                var detailList = DetailGridBindingSource.List as List<TN_QCT1000>;
                var editList = detailList.Where(p => p.EditRowFlag == "Y").ToList();
                if (editList.Count > 0)
                {
                    foreach (var v in editList.Where(c => c.FileUrl != null && c.FileUrl.Contains("\\")).ToList())
                    {
                        string[] filename = v.FileUrl.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = v.RevNo + "_" + v.ItemCode + "_" + filename[filename.Length - 1];
                            realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 특수문자 있는 경우 오류가 있어서 그걸 제거처리
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_Inspection_IN_File + "/" + realFileName;

                            FileHandler.UploadFTP(v.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_Inspection_IN_File + "/");

                            v.FileName = realFileName;
                            v.FileUrl = ftpFileUrl;
                        }
                    }
                    //foreach (var d in masterList.Where(p => !p.DeleteFilePath.IsNullOrEmpty()))
                    //{
                    //    try
                    //    {
                    //        FileHandler.PathDeleteFTP(GlobalVariable.FTP_SERVER, d.DeleteFilePath);
                    //    }
                    //    catch { }
                    //}

                    //foreach (var d in masterList.Where(p => !p.UploadFilePath.IsNullOrEmpty()))
                    //{
                    //    try
                    //    {
                    //        FileHandler.UploadFTP(d.UploadFilePath, string.Format("{0}{1}/{2}/", GlobalVariable.FTP_SERVER, MasterCodeSTR.FtpFolder_Inspection_IN_File, d.InspNo));
                    //        d.FileUrl = string.Format("{0}/{1}/{2}", MasterCodeSTR.FtpFolder_Inspection_IN_File, d.InspNo, d.FileName);
                    //    }
                    //    catch { }
                    //}
                }
            }

            ModelService.Save();
            DataLoad();
        }

        /// <summary>검사구분복사</summary>
        private void BarButtonCheckDivisionCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!UserRight.HasEdit) return;

            var detailObj = DetailGridBindingSource.Current as TN_QCT1000;
            if (detailObj == null)
                return;

            var subList = SubDetailGridBindingSource.List as List<TN_QCT1001>;
            if (subList == null || subList.Count == 0) return;

            var checkList = subList.Where(p => p._Check == "Y").ToList();
            if (checkList.Count == 0) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_QCT_CHECK_DIVISION_COPY, param, CheckDivisionCopyPopupCallback);

            form.ShowPopup(true);
        }

        private void CheckDivisionCopyPopupCallback(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var returnValue = e.Map.GetValue(PopupParameter.ReturnObject).GetNullToEmpty();

            var detailObj = DetailGridBindingSource.Current as TN_QCT1000;
            if (detailObj == null)
                return;

            var subList = SubDetailGridBindingSource.List as List<TN_QCT1001>;
            if (subList == null || subList.Count == 0) return;

            var checkList = subList.Where(p => p._Check == "Y").ToList();
            if (checkList.Count == 0) return;

            foreach (var v in checkList)
            {
                var newObj = new TN_QCT1001();
                newObj.RevNo = detailObj.RevNo;
                newObj.ItemCode = detailObj.ItemCode;
                newObj.Seq = detailObj.TN_QCT1001List.Count == 0 ? 1 : detailObj.TN_QCT1001List.Max(p => p.Seq) + 1;
                newObj.DisplayOrder = v.DisplayOrder;
                newObj.ProcessCode = v.ProcessCode;
                newObj.SpcFlag = v.SpcFlag;
                newObj.SpcDivision = v.SpcDivision;
                newObj.CheckDivision = returnValue;
                newObj.CheckWay = v.CheckWay;
                newObj.CheckList = v.CheckList;
                newObj.CheckMax = v.CheckMax;
                newObj.CheckMin = v.CheckMin;
                newObj.CheckSpec = v.CheckSpec;
                newObj.CheckUpQuad = v.CheckUpQuad;
                newObj.CheckDownQuad = v.CheckDownQuad;
                newObj.InstrumentCode = v.InstrumentCode;
                newObj.CheckDataType = v.CheckDataType;
                newObj.MaxReading = v.MaxReading;
                newObj.UseFlag = "Y";
                newObj.Memo = v.Memo;
                newObj.InstrMemo = v.InstrMemo;
                newObj.InstrumentNo = v.InstrumentNo;
                newObj.InspectionReportFlag = v.InspectionReportFlag;
                newObj.InspectionReportMemo = v.InspectionReportMemo;
                newObj.NewRowFlag = "Y";

                //if (returnValue == MasterCodeSTR.InspectionDivision_Shipment)
                //{
                //    newObj.InspectionReportFlag = v.InspectionReportFlag;
                //    newObj.InspectionReportMemo = v.InspectionReportMemo;
                //}

                v._Check = "N";
                SubDetailGridBindingSource.Add(newObj);
                detailObj.TN_QCT1001List.Add(newObj);
            }

            SetIsFormControlChanged(true);
            SubDetailGridExControl.BestFitColumns();
        }

        /// <summary> 다른품목복사</summary>
        private void BarButtonItemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!UserRight.HasEdit) return;

            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_QCT_ITEM_COPY, param, ItemCopyPopupCallback);

            form.ShowPopup(true);
        }

        private void ItemCopyPopupCallback(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var returnList = (List<TN_QCT1001>)e.Map.GetValue(PopupParameter.ReturnObject);

            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;

            var newObjCheck = masterObj.TN_QCT1000List.Where(p => p.NewRowFlag == "Y").FirstOrDefault();
            if (newObjCheck != null)
            {
                DetailGridExControl.MainGrid.MainView.FocusedRowHandle = DetailGridExControl.MainGrid.MainView.LocateByValue("RevNo", newObjCheck.RevNo);
                foreach (var v in returnList)
                {
                    var newObj2 = new TN_QCT1001()
                    {
                        RevNo = newObjCheck.RevNo,
                        ItemCode = newObjCheck.ItemCode,
                        Seq = newObjCheck.TN_QCT1001List.Count == 0 ? 1 : newObjCheck.TN_QCT1001List.Max(p => p.Seq) + 1,
                        DisplayOrder = v.DisplayOrder,
                        ProcessCode = v.ProcessCode,
                        InspectionReportFlag = v.InspectionReportFlag,
                        InspectionReportMemo = v.InspectionReportMemo,
                        SpcFlag = v.SpcFlag,
                        SpcDivision = v.SpcDivision,
                        CheckDivision = v.CheckDivision,
                        CheckWay = v.CheckWay,
                        CheckList = v.CheckList,
                        CheckMax = v.CheckMax,
                        CheckMin = v.CheckMin,
                        CheckSpec = v.CheckSpec,
                        CheckUpQuad = v.CheckUpQuad,
                        CheckDownQuad = v.CheckDownQuad,
                        InstrumentCode = v.InstrumentCode,
                        CheckDataType = v.CheckDataType,
                        UseFlag = "Y",
                        NewRowFlag = "Y",
                        Memo = v.Memo
                    };
                    SubDetailGridBindingSource.Add(newObj2);
                    newObjCheck.TN_QCT1001List.Add(newObj2);
                }

                if (returnList.Count > 0)
                {
                    SetIsFormControlChanged(true);
                    SubDetailGridExControl.BestFitColumns();
                }
            }
            else
            {
                var newObj = new TN_QCT1000()
                {
                    RevNo = masterObj.TN_QCT1000List.Count == 0 ? "Rev00" : "Rev" + masterObj.TN_QCT1000List.Count.ToString().PadLeft(2, '0'),
                    RevDate = DateTime.Today,
                    ItemCode = masterObj.ItemCode,
                    UseFlag = "Y",
                    NewRowFlag = "Y"
                };

                DetailGridBindingSource.Add(newObj);
                masterObj.TN_QCT1000List.Add(newObj);
                DetailGridExControl.BestFitColumns();

                foreach (var v in returnList)
                {
                    var newObj2 = new TN_QCT1001()
                    {
                        RevNo = newObj.RevNo,
                        ItemCode = newObj.ItemCode,
                        Seq = newObj.TN_QCT1001List.Count == 0 ? 1 : newObj.TN_QCT1001List.Max(p => p.Seq) + 1,
                        DisplayOrder = v.DisplayOrder,
                        ProcessCode = v.ProcessCode,
                        InspectionReportFlag = v.InspectionReportFlag,
                        InspectionReportMemo = v.InspectionReportMemo,
                        SpcFlag = v.SpcFlag,
                        SpcDivision = v.SpcDivision,
                        CheckDivision = v.CheckDivision,
                        CheckWay = v.CheckWay,
                        CheckList = v.CheckList,
                        CheckMax = v.CheckMax,
                        CheckMin = v.CheckMin,
                        CheckSpec = v.CheckSpec,
                        CheckUpQuad = v.CheckUpQuad,
                        CheckDownQuad = v.CheckDownQuad,
                        InstrumentCode = v.InstrumentCode,
                        CheckDataType = v.CheckDataType,
                        UseFlag = "Y",
                        NewRowFlag = "Y",
                        Memo = v.Memo
                    };
                    SubDetailGridBindingSource.Add(newObj2);
                    newObj.TN_QCT1001List.Add(newObj2);
                }

                if (returnList.Count > 0)
                {
                    SetIsFormControlChanged(true);
                    SubDetailGridExControl.BestFitColumns();
                }
            }
        }

        private void MainView_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "CheckMax" || e.Column.FieldName == "CheckMin") 
                //(e.Column.FieldName == "CheckSpec" || e.Column.FieldName == "CheckUpQuad" || e.Column.FieldName == "CheckDownQuad")
                {
                    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    if (checkDataType.IsNullOrEmpty())
                    {
                        e.RepositoryItem = repositoryItemTextEdit;
                    }
                    else
                    {
                        if (checkDataType == MasterCodeSTR.CheckDataType_C)
                        {
                            //육안검사 C
                            e.RepositoryItem = repositoryItemTextEdit;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(checkDataType)) repositoryItemSpinEdit.Mask.EditMask = string.Format("N{0}", checkDataType);
                            e.RepositoryItem = repositoryItemSpinEdit;
                        }
                    }

                    //var inspectionWay = view.GetRowCellValue(e.RowHandle, "CheckWay").GetNullToEmpty();
                    //if (inspectionWay == MasterCodeSTR.InspectionWay_Input)
                    //{
                    //    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    //    if (!checkDataType.IsNullOrEmpty())
                    //    {
                    //        if (!string.IsNullOrEmpty(checkDataType)) repositoryItemSpinEdit.Mask.EditMask = string.Format("N{0}", checkDataType);
                    //        e.RepositoryItem = repositoryItemSpinEdit;
                    //    }
                    //    else
                    //    {
                    //        e.RepositoryItem = repositoryItemTextEdit;
                    //    }
                    //}
                    //else
                    //{
                    //    e.RepositoryItem = repositoryItemTextEdit;
                    //}
                }
            }
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "ProcessCode")
                {
                    var inspectionDivision = view.GetRowCellValue(e.RowHandle, "CheckDivision").GetNullToEmpty();
                    if (inspectionDivision.IsNullOrEmpty() || inspectionDivision == MasterCodeSTR.InspectionDivision_IN)
                    {
                        e.Appearance.BackColor = Color.Empty;
                    }
                    else
                    {
                        e.Appearance.BackColor = GlobalVariable.GridEditableColumnColor;
                    }
                }
                else if (e.Column.FieldName == "CheckDataType")
                {
                    //var inspectionWay = view.GetRowCellValue(e.RowHandle, "CheckWay").GetNullToEmpty();
                    //if (inspectionWay.IsNullOrEmpty() || inspectionWay == MasterCodeSTR.InspectionWay_Eye)
                    //{
                    //    e.Appearance.BackColor = Color.Empty;
                    //}
                    //else
                    //{
                    //    e.Appearance.BackColor = GlobalVariable.GridEditableColumnColor;
                    //}
                }
                else if (e.Column.FieldName == "CheckMax" || e.Column.FieldName == "CheckMin")
                //(e.Column.FieldName == "CheckSpec" || e.Column.FieldName == "CheckUpQuad" || e.Column.FieldName == "CheckDownQuad")
                {
                    //var inspectionWay = view.GetRowCellValue(e.RowHandle, "CheckWay").GetNullToEmpty();
                    //if (inspectionWay.IsNullOrEmpty() || inspectionWay == MasterCodeSTR.InspectionWay_Input)
                    //{
                    //    //검사방법이 치수검사일 때 검사데이터타입이 없을 경우 
                    //    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    //    if (checkDataType.IsNullOrEmpty())
                    //    {
                    //        e.Appearance.BackColor = Color.Empty;
                    //        e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                    //    }
                    //    else
                    //    {
                    //        e.Appearance.BackColor = GlobalVariable.GridEditableColumnColor;
                    //        e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                    //    }
                    //}

                    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    if (checkDataType.IsNullOrEmpty())
                    {
                        e.Appearance.BackColor = Color.Empty;
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                    }
                    else
                    {
                        if (checkDataType == MasterCodeSTR.CheckDataType_C)
                        {
                            //육안검사 C
                            e.Appearance.BackColor = Color.LightGray;
                            e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                            view.Columns["CheckSpec"].OptionsColumn.ReadOnly = true;
                            view.Columns["CheckMax"].OptionsColumn.ReadOnly = true;
                            view.Columns["CheckMin"].OptionsColumn.ReadOnly = true;
                        }
                        else
                        {
                            e.Appearance.BackColor = GlobalVariable.GridEditableColumnColor;
                            e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                            view.Columns["CheckSpec"].OptionsColumn.ReadOnly = false;
                            view.Columns["CheckMax"].OptionsColumn.ReadOnly = false;
                            view.Columns["CheckMin"].OptionsColumn.ReadOnly = false;
                        }
                    }
                }
            }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            var fieldName = view.FocusedColumn.FieldName;
            if (fieldName == "ProcessCode")
            {
                var inspectionDivision = view.GetFocusedRowCellValue("CheckDivision").GetNullToEmpty();
                if (inspectionDivision.IsNullOrEmpty() || inspectionDivision == MasterCodeSTR.InspectionDivision_IN)
                {
                    //공정이 필요없는 검사일 경우
                    e.Cancel = true;
                }
            }
            else if (fieldName == "CheckSpec" || fieldName == "CheckUpQuad" || fieldName == "CheckDownQuad")
            {
                var checkDataType = view.GetFocusedRowCellValue("CheckDataType").GetNullToEmpty();
                if (checkDataType.IsNullOrEmpty())
                {
                    e.Cancel = true;
                }
                //var inspectionWay = view.GetFocusedRowCellValue("CheckWay").GetNullToEmpty();
                //if (inspectionWay.IsNullOrEmpty() || inspectionWay == MasterCodeSTR.InspectionWay_Input)
                //{
                //    //검사방법이 치수검사일 때 검사데이터타입이 없을 경우 
                //    var checkDataType = view.GetFocusedRowCellValue("CheckDataType").GetNullToEmpty();
                //    if (checkDataType.IsNullOrEmpty())
                //        e.Cancel = true;
                //}
            }
            else if (fieldName == "InspectionReportFlag") //검사구분이 출하검사일 때만 성적서사용여부 체크 가능
            {
                var inspectionDivision = view.GetFocusedRowCellValue("CheckDivision").GetNullToEmpty();
                if (inspectionDivision != MasterCodeSTR.InspectionDivision_Shipment && inspectionDivision != MasterCodeSTR.InspectionDivision_IN)
                {
                    e.Cancel = true;
                }
            }
        }

        private void SubDetailMainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = sender as GridView;

            if (e.Column.FieldName == "InstrumentCode")
            {
                var instrCode = e.Value.GetNullToEmpty();
                if (!instrCode.IsNullOrEmpty())
                {
                    var TN_MEA1100 = ModelService.GetChildList<TN_MEA1100>(p => p.InstrCode == instrCode).FirstOrDefault();
                    if (TN_MEA1100 != null && !TN_MEA1100.SerialNo.IsNullOrEmpty())
                    {
                        view.SetFocusedRowCellValue("InstrumentNo", TN_MEA1100.SerialNo);
                    }
                }
            }
            else if (e.Column.FieldName == "InspectionReportMemo" || e.Column.FieldName == "CheckUpQuad" || e.Column.FieldName == "CheckDownQuad")
            {
                var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                if (!checkDataType.IsNullOrEmpty() && checkDataType != MasterCodeSTR.CheckDataType_C)
                {
                    var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDoubleNullToNull();
                    var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDoubleNullToNull();
                    var checkSpec = view.GetRowCellValue(e.RowHandle, "InspectionReportMemo").GetNullToNull();

                    double checkSpecDecimal = 0;
                    
                    if (!double.TryParse(checkSpec, out checkSpecDecimal))
                    {
                        view.SetFocusedRowCellValue("InspectionReportMemo", checkSpecDecimal);
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_171), LabelConvert.GetLabelText("CheckDataType")));
                        
                        return;
                    }

                    if (checkUpQuad != null && checkDownQuad != null)
                    {
                        //view.SetFocusedRowCellValue("CheckSpec", checkSpec);
                        view.SetFocusedRowCellValue("CheckSpec", checkSpecDecimal);
                        view.SetFocusedRowCellValue("CheckMax", checkSpecDecimal + checkUpQuad);
                        view.SetFocusedRowCellValue("CheckMin", checkSpecDecimal - checkDownQuad);
                    }
                    else
                    {
                        view.SetFocusedRowCellValue("CheckSpec", null);
                        view.SetFocusedRowCellValue("CheckMax", null);
                        view.SetFocusedRowCellValue("CheckMin", null);
                    }
                }
            }
            else if (e.Column.FieldName == "CheckWay")
            {
                var checkWay = view.GetRowCellValue(e.RowHandle, "CheckWay").GetNullToNull();
                if (checkWay != null && checkWay == MasterCodeSTR.InspectionWay_Eye)
                {
                    view.SetFocusedRowCellValue("CheckList", "외관");
                }
                else
                {
                    view.SetFocusedRowCellValue("CheckList", "");
                }
            }
            else if (e.Column.FieldName == "CheckDataType")
            {
                var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToNull();
                if (checkDataType != null && checkDataType == MasterCodeSTR.CheckDataType_C)
                {
                    view.SetFocusedRowCellValue("CheckSpec", null);
                    view.SetFocusedRowCellValue("CheckMax", null);
                    view.SetFocusedRowCellValue("CheckMin", null);
                }
                else if (checkDataType != null && checkDataType != MasterCodeSTR.CheckDataType_C)
                {
                    double checkSpecInt = 0;

                    var checkSpec = view.GetRowCellValue(e.RowHandle, "InspectionReportMemo").GetNullToNull();
                    if (checkSpec == null)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_84), LabelConvert.GetLabelText("Spec")));
                    }
                    else if (checkSpec != null && !double.TryParse(checkSpec, out checkSpecInt))
                    {
                        view.SetFocusedRowCellValue("InspectionReportMemo", null);
                    }
                }
            }
        }

        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            var view = sender as GridView;
            if (e.KeyCode == Keys.Enter)
            {
                if (view.FocusedColumn.VisibleIndex + 1 == view.VisibleColumns.Count)
                {
                    view.FocusedColumn = view.VisibleColumns[0];
                    if (view.RowCount == view.FocusedRowHandle + 1)
                        view.FocusedRowHandle = 0;
                    else
                        view.FocusedRowHandle = view.FocusedRowHandle + 1;
                }
                else
                {
                    view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex + 1];
                }
            }
        }

        private void InspCheckPositionEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_QCT1001;

            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.InspCheckPosition + "'";
        }
    }
}