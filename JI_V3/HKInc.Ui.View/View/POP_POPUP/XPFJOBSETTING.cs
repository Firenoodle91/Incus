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
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain.VIEW;
using DevExpress.XtraEditors.Controls;
using HKInc.Ui.Model.Domain;
using DevExpress.Utils;
using HKInc.Service.Helper;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using System.IO;
using System.Collections.Specialized;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// POP 작업설정 창
    /// </summary>
    public partial class XPFJOBSETTING : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1001> ModelService = (IService<TN_QCT1001>)ProductionFactory.GetDomainService("TN_QCT1001");
        IService<TN_QCT1100> QcModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;
        string ProductLotNo;
        int rowid;
        string qcType;
       
        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemSpinEdit repositoryItemSpinEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;

        public XPFJOBSETTING(TEMP_XFPOP1000 obj, string productLotNo)
        {
            InitializeComponent();
        
            this.Text = LabelConvert.GetLabelText("InspJobSetting");

            this.Size = new Size(this.Size.Width, this.Size.Height - 40);

            gridEx1.ViewType = Utils.Enum.GridViewType.POP_GridView;

            TEMP_XFPOP1000_Obj = obj;
            ProductLotNo = productLotNo;

            gridEx1.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            gridEx1.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            gridEx1.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            gridEx1.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            gridEx1.MainGrid.MainView.KeyDown += MainView_KeyDown;

            //tx_Reading.KeyDown += Tx_Reading_KeyDown;
            //tx_Reading.DoubleClick += Tx_Reading_DoubleClick;

            //lup_ReadingNumber.EditValueChanged += Lup_ReadingNumber_EditValueChanged;

            btn_Save.Click += Btn_Save_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
            //btn_Apply.Click += Btn_Apply_Click;

            this.WindowState = FormWindowState.Maximized;

            lup_CheckPoint.EditValueChanged += Lup_CheckPoint_EditValueChanged;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            empty_JobSettingTitle.Text = LabelConvert.GetLabelText("InspJobSetting");

            //lup_Eye.SetDefaultPOP(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), TextEditStyles.DisableTextEditor);
            //lup_ReadingNumber.SetDefaultPOP(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), TextEditStyles.DisableTextEditor);
            //lup_CheckId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p=>p.Active=="Y").ToList(), TextEditStyles.DisableTextEditor);
            lup_CheckPoint.SetDefaultPOP(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.CheckPoint).Where(p => p.CodeVal.Left(1) == "J").ToList(), TextEditStyles.DisableTextEditor);

            //var procTeamCode = TEMP_XFPOP1000_Obj.ProcTeamCode.GetNullToNull();
            //lup_CheckId.SetDefaultPOP(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => (string.IsNullOrEmpty(procTeamCode) ? true : p.ProductTeamCode == procTeamCode)
            //                                                                                                                                            && p.Active == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_CheckId.SetDefaultPOP(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.DepartmentCode == "DEPT-00003"
                                                                       && p.Active == "Y"

                                                                   )
                                                                   .ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //searchLookUpEditEx_POP1.SetDefaultPOP(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => (string.IsNullOrEmpty(procTeamCode) ? true : p.ProductTeamCode == procTeamCode)
            //&& p.Active == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lup_CheckId.SetFontSize(new Font("맑은 고딕", 15));
            //lup_ReadingNumber.SetFontSize(new Font("맑은 고딕", 15));
            //lup_Eye.SetFontSize(new Font("맑은 고딕", 15));
            //lup_CheckPoint.SetFontSize(new Font("맑은 고딕", 15));

            //lup_CheckId.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //lup_ReadingNumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //lup_Eye.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //lup_CheckPoint.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            ControlSetting(false);

            repositoryItemGridLookUpEdit = new RepositoryItemGridLookUpEdit()
            {
                ValueMember = "CodeVal"
                ,
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

            repositoryItemSpinEdit = new RepositoryItemSpinEdit();
            repositoryItemSpinEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            repositoryItemSpinEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repositoryItemSpinEdit.Mask.UseMaskAsDisplayFormat = true;
            repositoryItemSpinEdit.AllowMouseWheel = true;
            repositoryItemSpinEdit.Appearance.BackColor = Color.White;
            repositoryItemSpinEdit.Buttons[0].Visible = false;
            repositoryItemSpinEdit.Click += RepositoryItemSpinEdit_Click;
            repositoryItemTextEdit = new RepositoryItemTextEdit();
        }

        private void RepositoryItemSpinEdit_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad) return;
            var spinEidt = sender as SpinEdit;
            var keyPad = new XFCNUMPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                spinEidt.EditValue = keyPad.returnval;
            }
        }

        private void InitButtonLabelConvert()
        {
            btn_Save.Text = LabelConvert.GetLabelText("Save");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
            //btn_Apply.Text = LabelConvert.GetLabelText("Apply");
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MainView.OptionsView.ShowIndicator = false;

            gridEx1.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"));
            gridEx1.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));
            gridEx1.MainGrid.AddColumn("CheckDataType", LabelConvert.GetLabelText("CheckDataType"), false);
            gridEx1.MainGrid.AddColumn("InspectionReportMemo", LabelConvert.GetLabelText("InspectionReportMemo"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("CheckMin", LabelConvert.GetLabelText("CheckMin"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("CheckMax", LabelConvert.GetLabelText("CheckMax"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("CheckSpec", LabelConvert.GetLabelText("CheckSpec"), HorzAlignment.Center, false);
            gridEx1.MainGrid.AddColumn("CheckUpQuad", LabelConvert.GetLabelText("CheckUpQuad"), HorzAlignment.Center, false);
            gridEx1.MainGrid.AddColumn("CheckDownQuad", LabelConvert.GetLabelText("CheckDownQuad"), HorzAlignment.Center, false);
            gridEx1.MainGrid.AddColumn("MaxReading", LabelConvert.GetLabelText("MaxReading"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("Reading1", LabelConvert.GetLabelText("Reading1"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("Reading2", LabelConvert.GetLabelText("Reading2"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("Reading3", LabelConvert.GetLabelText("Reading3"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("Reading4", LabelConvert.GetLabelText("Reading4"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("Reading5", LabelConvert.GetLabelText("Reading5"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("Reading6", LabelConvert.GetLabelText("Reading6"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("Reading7", LabelConvert.GetLabelText("Reading7"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("Reading8", LabelConvert.GetLabelText("Reading8"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("Reading9", LabelConvert.GetLabelText("Reading9"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("Judge", LabelConvert.GetLabelText("Judge"));
            gridEx1.MainGrid.SetEditable(true, "Reading1", "Reading2", "Reading3", "Reading4", "Reading5", "Reading6", "Reading7", "Reading8", "Reading9");

            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 15f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
            //gridEx1.MainGrid.MainView.OptionsView.ColumnAutoWidth = true;
            //gridEx1.MainGrid.Columns["CheckList"].MinWidth = 100;
            //gridEx1.MainGrid.Columns["CheckWay"].MinWidth = 100;
            //gridEx1.MainGrid.Columns["CheckDataType"].MinWidth = 100;
            //gridEx1.MainGrid.Columns["InspectionReportMemo"].MinWidth = 100;
            //gridEx1.MainGrid.Columns["CheckMin"].MinWidth = 100;
            //gridEx1.MainGrid.Columns["CheckMax"].MinWidth = 100;
            //gridEx1.MainGrid.Columns["CheckSpec"].MinWidth = 100;
            //gridEx1.MainGrid.Columns["Reading1"].MinWidth = 80;
            //gridEx1.MainGrid.Columns["Reading2"].MinWidth = 80;
            //gridEx1.MainGrid.Columns["Reading3"].MinWidth = 80;
            //gridEx1.MainGrid.Columns["Reading4"].MinWidth = 80;
            //gridEx1.MainGrid.Columns["Reading5"].MinWidth = 80;
            //gridEx1.MainGrid.Columns["Reading6"].MinWidth = 80;
            //gridEx1.MainGrid.Columns["Reading7"].MinWidth = 80;
            //gridEx1.MainGrid.Columns["Reading8"].MinWidth = 80;
            //gridEx1.MainGrid.Columns["Reading9"].MinWidth = 80;
            //gridEx1.MainGrid.Columns["Judge"].MinWidth = 80;
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionItem), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDataType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDataType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("Judge", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MaxReading", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
      
            gridEx1.BestFitColumns();
        }
     
       
        protected override void GridRowDoubleClicked() { }

        protected override void InitDataLoad()
        {
            Search(MasterCodeSTR.InspectionDivision_Setting);
        }

        /// <summary>
        /// 저장 클릭이벤트
        /// </summary>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            var view = gridEx1.MainGrid.MainView as GridView;
            var list = GridBindingSource.List as List<TN_QCT1001>;
            if (list == null || list.Count == 0) return;

            if (list.Count == list.Where(p => p.Judge.IsNullOrEmpty()).Count())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("CheckingInspection")));
                return;
            }

            var checkId = lup_CheckId.EditValue.GetNullToEmpty();
            if (checkId.IsNullOrEmpty())
            {                
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("CheckId2")));
                return;
            }

            var checkPoint = lup_CheckPoint.EditValue.GetNullToEmpty();
            if (checkPoint.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("CheckPoint")));
                return;
            }

            var masterCheckResult = list.Any(p=>p.Judge == "NG") ? "NG" : "OK";
                                    
            var TN_QCT1100_NewObj = new TN_QCT1100()
            {
                InspNo = DbRequestHandler.GetSeqMonth(qcType),
                CheckDivision = qcType,
                CheckPoint = checkPoint,
                WorkNo = TEMP_XFPOP1000_Obj.WorkNo,
                WorkSeq = TEMP_XFPOP1000_Obj.ProcessSeq,
                WorkDate = TEMP_XFPOP1000_Obj.WorkDate,
                ItemCode = TEMP_XFPOP1000_Obj.ItemCode,
                CustomerCode = TEMP_XFPOP1000_Obj.CustomerCode,
                ProcessCode = TEMP_XFPOP1000_Obj.ProcessCode,
                ProductLotNo = ProductLotNo,
                CheckDate = DateTime.Today,
                CheckId = checkId,
                CheckResult = masterCheckResult,
            };

            foreach (var v in list)
            {
                var TN_QCT1101_NewObj = new TN_QCT1101()
                {
                    InspNo = TN_QCT1100_NewObj.InspNo,
                    InspSeq = TN_QCT1100_NewObj.TN_QCT1101List.Count == 0 ? 1 : TN_QCT1100_NewObj.TN_QCT1101List.Max(o => o.InspSeq) + 1,
                    RevNo = v.RevNo,
                    ItemCode = v.ItemCode,
                    Seq = v.Seq,
                    CheckWay = v.CheckWay,
                    CheckList = v.CheckList,
                    CheckMax = v.CheckMax,
                    CheckMin = v.CheckMin,
                    CheckSpec = v.CheckSpec,
                    CheckUpQuad = v.CheckUpQuad,
                    CheckDownQuad = v.CheckDownQuad,
                    CheckDataType = v.CheckDataType,
                    Reading1 = v.Reading1,
                    Reading2 = v.Reading2,
                    Reading3 = v.Reading3,
                    Reading4 = v.Reading4,
                    Reading5 = v.Reading5,
                    Reading6 = v.Reading6,
                    Reading7 = v.Reading7,
                    Reading8 = v.Reading8,
                    Reading9 = v.Reading9,
                    Judge = v.Judge
                };
                TN_QCT1100_NewObj.TN_QCT1101List.Add(TN_QCT1101_NewObj);
            }
            QcModelService.Insert(TN_QCT1100_NewObj);
            QcModelService.Save();
            SetIsFormControlChanged(false);

            DialogResult = masterCheckResult == "OK" ? DialogResult.OK : DialogResult.No;

            Close();
        }

        /// <summary>
        /// 취소 클릭이벤트
        /// </summary>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            SetIsFormControlChanged(false);
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 적용 클릭이벤트
        /// </summary>
        //private void Btn_Apply_Click(object sender, EventArgs e)
        //{
        //    var view = gridEx1.MainGrid.MainView as GridView;
        //    var readingNumber = "Reading" + lup_ReadingNumber.EditValue.GetNullToEmpty();
        //    int readingNumberCount = ((List<TN_STD1000>)lup_ReadingNumber.DataSource).Count;

        //    //if (view.GetRowCellValue(rowid, view.Columns["CheckWay"]).ToString() == MasterCodeSTR.InspectionWay_Eye)
        //    //{
        //    //    string val = lup_Eye.EditValue.GetNullToEmpty();
        //    //    if (val.IsNullOrEmpty())
        //    //    {
        //    //        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspEye")));
        //    //        return;
        //    //    }
        //    //    view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
        //    //    rowid++;
        //    //    if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //    //    {
        //    //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //    //        rowid--;
        //    //    }
        //    //    else if (rowid >= view.RowCount)
        //    //    {
        //    //        rowid = 0;
        //    //        lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //    //    }
        //    //    view.FocusedRowHandle = rowid;
        //    //}
        //    //else
        //    //{
        //    //    string val = tx_Reading.EditValue.GetNullToEmpty();
        //    //    if (val.IsNullOrEmpty())
        //    //    {
        //    //        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("Reading")));
        //    //        return;
        //    //    }
        //    //    view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
        //    //    tx_Reading.Text = "";
        //    //    rowid++;
        //    //    if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //    //    {
        //    //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //    //        rowid--;
        //    //    }
        //    //    else if (rowid >= view.RowCount)
        //    //    {
        //    //        rowid = 0;
        //    //        lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //    //    }
        //    //    view.FocusedRowHandle = rowid;
        //    //}

        //    var maxReading = view.GetRowCellValue(rowid, view.Columns["MaxReading"]).GetNullToEmpty();
        //    if (!maxReading.IsNullOrEmpty() && maxReading.GetDecimalNullToZero() < lup_ReadingNumber.EditValue.GetDecimalNullToZero())
        //    {
        //        rowid++;
        //        if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //        {
        //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //            rowid--;
        //        }
        //        else if (rowid >= view.RowCount)
        //        {
        //            rowid = 0;
        //            lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //        }
        //        view.FocusedRowHandle = rowid;
        //        return;
        //    }

        //    if (view.GetRowCellValue(rowid, view.Columns["CheckDataType"]).ToString() == MasterCodeSTR.CheckDataType_C)
        //    {
        //        string val = lup_Eye.EditValue.GetNullToEmpty();
        //        if (val.IsNullOrEmpty())
        //        {
        //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspEye")));
        //            return;
        //        }

        //        view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
        //        rowid++;
        //        if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //        {
        //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //            rowid--;
        //        }
        //        else if (rowid >= view.RowCount)
        //        {
        //            rowid = 0;
        //            lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //        }
        //        view.FocusedRowHandle = rowid;
        //    }
        //    else
        //    {
        //        string val = tx_Reading.EditValue.GetNullToEmpty();
        //        if (val.IsNullOrEmpty())
        //        {
        //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("Reading")));
        //            return;
        //        }

        //        view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
        //        tx_Reading.Text = "";
        //        rowid++;
        //        if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //        {
        //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //            rowid--;
        //        }
        //        else if (rowid >= view.RowCount)
        //        {
        //            rowid = 0;
        //            lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //        }
        //        view.FocusedRowHandle = rowid;
        //    }
        //}
        
        /// <summary>
        /// 버튼별 조회 함수.
        /// </summary> 
        private void Search(string checkDivision)
        {
            gridEx1.MainGrid.Clear();
            ModelService.ReLoad();

            qcType = null;
            rowid = 0;
            lup_CheckId.EditValue = null;//Utils.Common.GlobalVariable.LoginId;
            //lup_ReadingNumber.EditValue = 1;
            //lup_Eye.EditValue = null;
            //tx_Reading.EditValue = null;

            var qcRev = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode && p.UseFlag == "Y").OrderBy(p => p.RowId).LastOrDefault();
            if (qcRev == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionItem")));
                ControlSetting(false);
                return;
            }
            
            var qualityList = ModelService.GetList(p => p.RevNo == qcRev.RevNo
                                                                  && p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode
                                                                  && p.CheckDivision == checkDivision
                                                                  && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode
                                                                  && p.UseFlag == "Y"
                                                               )
                                                               .OrderBy(p => p.DisplayOrder)
                                                               .ToList();
            if (qualityList.Count == 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionItem")));
                ControlSetting(false);
                return;
            }

            GridBindingSource.DataSource = qualityList;
            gridEx1.DataSource = GridBindingSource;
            gridEx1.MainGrid.BestFitColumns();
                       
            ControlSetting(true);

            if (lup_CheckPoint.EditValue.GetNullToEmpty() == "J05") //셋팅
            {
                var checkPoint = lup_CheckPoint.EditValue.GetNullToEmpty();
                var TN_QCT1100_OldObj = QcModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo
                                                                                    && p.WorkSeq == TEMP_XFPOP1000_Obj.ProcessSeq
                                                                                    && p.CheckDivision == checkDivision
                                                                                    && p.CheckPoint == checkPoint
                                                                                )
                                                                                .OrderBy(p => p.RowId)
                                                                                .LastOrDefault();
                if (TN_QCT1100_OldObj != null)
                {
                    var List = TN_QCT1100_OldObj.TN_QCT1101List.ToList();
                    if (List.Count > 0)
                    {
                        foreach (var v in qualityList)
                        {
                            var Obj = List.Where(p => p.RevNo == v.RevNo && p.ItemCode == v.ItemCode && p.Seq == v.Seq).FirstOrDefault();
                            if (Obj != null)
                            {
                                v.Reading1 = Obj.Reading1;
                                v.Reading2 = Obj.Reading2;
                                v.Reading3 = Obj.Reading3;
                                v.Reading4 = Obj.Reading4;
                                v.Reading5 = Obj.Reading5;
                                v.Reading6 = Obj.Reading6;
                                v.Reading7 = Obj.Reading7;
                                v.Reading8 = Obj.Reading8;
                                v.Reading9 = Obj.Reading9;
                                v.Judge = Obj.Judge;
                            }
                        }
                    }
                }
            }

            qcType = checkDivision;

            MainView_FocusedRowChanged(null, null);
            gridEx1.BestFitColumns();
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var obj = GridBindingSource.Current as TN_QCT1001;
            if (obj == null) return;

            if(e != null)
                rowid = e.FocusedRowHandle;
            //if (obj.CheckWay == MasterCodeSTR.InspectionWay_Eye)
            //{
            //    lup_Eye.Focus();
            //}
            //else
            //{
            //    tx_Reading.Focus();
            //}

            //if (obj.CheckDataType == MasterCodeSTR.CheckDataType_C)
            //{
            //    lup_Eye.Focus();
            //    tx_Reading.ReadOnly = true;
            //    lup_Eye.ReadOnly = false;
            //}
            //else
            //{
            //    tx_Reading.Focus();
            //    tx_Reading.ReadOnly = false;
            //    lup_Eye.ReadOnly = true;

            //    var CheckDataType = obj.CheckDataType;
            //    if (!CheckDataType.IsNullOrEmpty())
            //    {
            //        tx_Reading.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //        tx_Reading.Properties.Mask.EditMask = string.Format("N{0}", CheckDataType);
            //        tx_Reading.Properties.Mask.UseMaskAsDisplayFormat = true;
            //    }
            //    else
            //    {
            //        tx_Reading.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //        tx_Reading.Properties.Mask.EditMask = string.Format("N{0}", 0);
            //    }
            //}
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var view = sender as GridView;

            var obj = GridBindingSource.Current as TN_QCT1001;
            if (obj == null) return;

            if (e.Column.FieldName.Contains("Reading"))
            {
                //var inspectionWay = obj.CheckWay.GetNullToEmpty();
                //if (inspectionWay == MasterCodeSTR.InspectionWay_Input)
                //{
                //    CheckInput(obj);
                //}
                //else if (inspectionWay == MasterCodeSTR.InspectionWay_Eye)
                //{
                //    CheckEye(obj);
                //}
                //else
                //{
                //    CheckInput(obj);
                //}

                var checkDataType = obj.CheckDataType.GetNullToEmpty();
                if (checkDataType == MasterCodeSTR.CheckDataType_C)
                {
                    CheckEye(obj);
                }
                else
                {
                    CheckInput(obj);
                }
            }
        }

        /// <summary>
        /// 측정치 키 이벤트
        /// </summary> 
        //private void Tx_Reading_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (!tx_Reading.EditValue.IsNullOrEmpty())
        //            Btn_Apply_Click(null, null);
        //    }
        //}

        /// <summary>
        /// 측정치 더블클릭 이벤트(키패드)
        /// </summary> 
        //private void Tx_Reading_DoubleClick(object sender, EventArgs e)
        //{
        //    if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
        //    XFCKEYPAD keyPad = new XFCKEYPAD();
        //    if (keyPad.ShowDialog() != DialogResult.Cancel)
        //        tx_Reading.EditValue = keyPad.returnval;
        //}

        /// <summary>
        /// 시료수 변경 시 이벤트
        /// </summary>
        private void Lup_ReadingNumber_EditValueChanged(object sender, EventArgs e)
        {
            var view = gridEx1.MainGrid.MainView as GridView;
            view.FocusedRowHandle = 0;
        }

        /// <summary>
        /// 컨트롤 셋팅
        /// </summary>
        private void ControlSetting(bool editFlag)
        {
            btn_Save.Enabled = editFlag;
            lup_CheckId.ReadOnly = !editFlag;
            //lup_ReadingNumber.ReadOnly = !editFlag;
            //lup_Eye.ReadOnly = !editFlag;
            //tx_Reading.ReadOnly = !editFlag;
            //btn_Apply.Enabled = editFlag;
            lup_CheckPoint.ReadOnly = !editFlag;
            //if (editFlag)
            //{
            //    btn_Save.Enabled = editFlag;
            //    lup_CheckId.ReadOnly = !editFlag;
            //    lup_ReadingNumber.ReadOnly = !editFlag;
            //    lup_Eye.ReadOnly = !editFlag;
            //    tx_Reading.ReadOnly = !editFlag;
            //    btn_Apply.Enabled = editFlag;
            //}
            //else
            //{
            //    btn_Save.Enabled = editFlag;
            //    lup_CheckId.ReadOnly = !editFlag;
            //    lup_ReadingNumber.ReadOnly = !editFlag;
            //    lup_Eye.ReadOnly = !editFlag;
            //    tx_Reading.ReadOnly = !editFlag;
            //    btn_Apply.Enabled = editFlag;
            //}
        }

        /// <summary>
        /// 치수검사 체크
        /// </summary>
        private void CheckInput(TN_QCT1001 obj)
        {
            var checkSpec = obj.CheckSpec.GetDecimalNullToNull();
            if (checkSpec == null) return;
            var checkUpQuad = obj.CheckUpQuad.GetDecimalNullToZero();
            var checkDownQuad = obj.CheckDownQuad.GetDecimalNullToZero();
            var checkUp = checkSpec + checkUpQuad;
            var checkDown = checkSpec - checkDownQuad;

            int NgQty = 0;
            int OkQty = 0;

            var reading1 = obj.Reading1.GetDecimalNullToNull();
            var reading2 = obj.Reading2.GetDecimalNullToNull();
            var reading3 = obj.Reading3.GetDecimalNullToNull();
            var reading4 = obj.Reading4.GetDecimalNullToNull();
            var reading5 = obj.Reading5.GetDecimalNullToNull();
            var reading6 = obj.Reading6.GetDecimalNullToNull();
            var reading7 = obj.Reading7.GetDecimalNullToNull();
            var reading8 = obj.Reading8.GetDecimalNullToNull();
            var reading9 = obj.Reading9.GetDecimalNullToNull();

            if (reading1 != null)
            {
                if (reading1 >= checkDown && reading1 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading2 != null)
            {
                if (reading2 >= checkDown && reading2 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading3 != null)
            {
                if (reading3 >= checkDown && reading3 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading4 != null)
            {
                if (reading4 >= checkDown && reading4 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading5 != null)
            {
                if (reading5 >= checkDown && reading5 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading6 != null)
            {
                if (reading6 >= checkDown && reading6 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading7 != null)
            {
                if (reading7 >= checkDown && reading7 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading8 != null)
            {
                if (reading8 >= checkDown && reading8 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading9 != null)
            {
                if (reading9 >= checkDown && reading9 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (NgQty == 0 && OkQty == 0)
            {
                obj.Judge = null;
            }
            else if (NgQty > 0)
            {
                obj.Judge = "NG";
            }
            else
            {
                obj.Judge = "OK";
            }

            gridEx1.BestFitColumns();
        }

        /// <summary>
        /// 육안검사 체크
        /// </summary>
        /// <param name="detailObj"></param>
        private void CheckEye(TN_QCT1001 obj)
        {
            int NgQty = 0;
            int OkQty = 0;

            var reading1 = obj.Reading1.GetNullToNull();
            var reading2 = obj.Reading2.GetNullToNull();
            var reading3 = obj.Reading3.GetNullToNull();
            var reading4 = obj.Reading4.GetNullToNull();
            var reading5 = obj.Reading5.GetNullToNull();
            var reading6 = obj.Reading6.GetNullToNull();
            var reading7 = obj.Reading7.GetNullToNull();
            var reading8 = obj.Reading8.GetNullToNull();
            var reading9 = obj.Reading9.GetNullToNull();

            if (reading1 != null)
            {
                if (reading1 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading2 != null)
            {
                if (reading2 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading3 != null)
            {
                if (reading3 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading4 != null)
            {
                if (reading4 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading5 != null)
            {
                if (reading5 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading6 != null)
            {
                if (reading6 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading7 != null)
            {
                if (reading7 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading8 != null)
            {
                if (reading8 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading9 != null)
            {
                if (reading9 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (NgQty == 0 && OkQty == 0)
            {
                obj.Judge = null;
            }
            else if (NgQty > 0)
            {
                obj.Judge = "NG";
            }
            else
            {
                obj.Judge = "OK";
            }

            gridEx1.BestFitColumns();
        }

        private void Lup_CheckPoint_EditValueChanged(object sender, EventArgs e)
        {
            var value = lup_CheckPoint.EditValue.GetNullToEmpty();
            if (!value.IsNullOrEmpty())
            {
                Search(MasterCodeSTR.InspectionDivision_Setting);
            }
        }

        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            var view = sender as GridView;
            if (e.KeyCode == Keys.Enter)
            {
                if (view.FocusedColumn.FieldName.Contains("Reading") && !view.FocusedColumn.FieldName.Contains("MaxReading"))
                {
                    var maxReading = view.GetFocusedRowCellValue("MaxReading").GetNullToEmpty();
                    if (view.FocusedColumn.FieldName.Right(1).GetDecimalNullToNull() == maxReading.GetDecimalNullToNull())
                    {
                        if (view.RowCount == view.FocusedRowHandle + 1)
                        {
                            view.FocusedRowHandle = 0;
                        }
                        else
                        {
                            view.FocusedRowHandle = view.FocusedRowHandle + 1;
                        }

                        view.FocusedColumn = view.Columns["Reading1"];
                    }
                    else
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
                else
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
        }

        private void MainView_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("Reading") && !e.Column.FieldName.Contains("MaxReading"))
                {
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
                    //else if(inspectionWay == MasterCodeSTR.InspectionWay_Eye)
                    //{
                    //    e.RepositoryItem = repositoryItemGridLookUpEdit;
                    //}
                    //else
                    //{
                    //    e.RepositoryItem = repositoryItemTextEdit;
                    //}

                    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    if (checkDataType == MasterCodeSTR.CheckDataType_C)
                    {
                        //육안검사 
                        e.RepositoryItem = repositoryItemGridLookUpEdit;
                    }
                    else if (checkDataType.IsNullOrEmpty())
                    {
                        e.RepositoryItem = repositoryItemTextEdit;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(checkDataType)) repositoryItemSpinEdit.Mask.EditMask = string.Format("N{0}", checkDataType);
                        e.RepositoryItem = repositoryItemSpinEdit;
                    }
                }
            }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView gv = sender as GridView;

            var detailObj = GridBindingSource.Current as TN_QCT1101;
            if (detailObj == null) return;

            var maxReading = detailObj.TN_QCT1001.MaxReading.GetNullToEmpty();
            if (gv.FocusedColumn.FieldName.Contains("Reading") && !gv.FocusedColumn.FieldName.Contains("MaxReading"))
            {
                if (gv.FocusedColumn.FieldName.Right(1).GetDecimalNullToNull() > maxReading.GetDecimalNullToNull())
                    e.Cancel = true;
            }
        }
    }
}
