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
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// POP 품질등록 팝업 창
    /// </summary>
    public partial class XPFINSPECTION : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1100> QcModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;
        string ProductLotNo;
        int rowid;
        string qcType;
        string ProcTeamCode;

        bool Self_Check = false;
        bool FME_Check = false;
        bool Process_Check = false;

        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemSpinEdit repositoryItemSpinEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;

        public XPFINSPECTION(TEMP_XFPOP1000 obj, string productLotNo, string procTeamCode)
        {
            InitializeComponent();

            this.Text = LabelConvert.GetLabelText("QualityAdd");

            this.Size = new Size(this.Size.Width, this.Size.Height - 40);

            ProcTeamCode = procTeamCode;

            gridEx1.ViewType = GridViewType.POP_GridView;

            TEMP_XFPOP1000_Obj = obj;
            ProductLotNo = productLotNo;

            gridEx1.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            gridEx1.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            gridEx1.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            gridEx1.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            gridEx1.MainGrid.MainView.KeyDown += MainView_KeyDown;

            //tx_Reading.KeyDown += Tx_Reading_KeyDown;
            //tx_Reading.DoubleClick += Tx_Reading_DoubleClick;
            //tx_Reading.GotFocus += Tx_Reading_GotFocus;

            //lup_ReadingNumber.EditValueChanged += Lup_ReadingNumber_EditValueChanged;
            //lup_ReadingNumber.Popup += Lup_ReadingNumber_Popup;

            btn_InspFrequently.Click += Btn_InspFrequently_Click;
            btn_InspFME.Click += Btn_InspFME_Click;
            btn_InspProcess.Click += Btn_InspProcess_Click;
            btn_Save.Click += Btn_Save_Click;
            btn_InitRefresh.Click += Btn_InitRefresh_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
            //btn_Apply.Click += Btn_Apply_Click;

            pic_Limit.DoubleClick += Pic_Limit_DoubleClick;

            btn_FME_First.Click += Btn_FME_First_Click;
            btn_FME_Middle.Click += Btn_FME_Middle_Click;
            btn_FME_End.Click += Btn_FME_End_Click;

            this.WindowState = FormWindowState.Maximized;
        }

        private void Pic_Limit_DoubleClick(object sender, EventArgs e)
        {
            if (pic_Limit.EditValue == null) return;
            var imgForm = new POP_POPUP.XPFPOPIMG(LabelConvert.GetLabelText("ItemLimitImage"), pic_Limit.EditValue);
            imgForm.ShowDialog();
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            //lup_Eye.SetDefaultPOP(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), TextEditStyles.DisableTextEditor);
            //lup_ReadingNumber.SetDefaultPOP(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), TextEditStyles.DisableTextEditor);
            //lup_CheckId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p=>p.Active=="Y").ToList(), TextEditStyles.DisableTextEditor);
            //var procTeamCode = ProcTeamCode;
            //lup_CheckId.SetDefaultPOP(false, "LoginId", "UserName", QcModelService.GetChildList<User>(p => (string.IsNullOrEmpty(procTeamCode) ? true : p.ProductTeamCode == procTeamCode)
            //                                                                                        && p.Active == "Y"
            //                                                                                        && p.UserUserGroupList.Any(c => c.UserGroupId == MasterCodeSTR.UserGroup_ProcessInspection)
            //                                                                                    )
            //                                                                                    .ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            var procTeamCode = ProcTeamCode;
            var userList = QcModelService.GetChildList<User>(p => p.Active == "Y" && p.UserUserGroupList.Any(c => c.UserGroupId == MasterCodeSTR.UserGroup_ProcessInspection)).ToList();
            var loginId = GlobalVariable.LoginId;
            if (TEMP_XFPOP1000_Obj.ProcessSeq == 1)
            {
                lup_CheckId.SetDefaultPOP(false, "LoginId", "UserName", userList.Where(p => (string.IsNullOrEmpty(procTeamCode) ? true : p.ProductTeamCode == procTeamCode)).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
                if (userList.Where(p => (string.IsNullOrEmpty(procTeamCode) ? true : p.ProductTeamCode == procTeamCode)).ToList().Any(p => p.LoginId == loginId))
                    lup_CheckId.EditValue = loginId;
            }
            else
            {
                lup_CheckId.SetDefaultPOP(false, "LoginId", "UserName", userList, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
                if (userList.Any(p => p.LoginId == loginId))
                    lup_CheckId.EditValue = loginId;
            }

            //lup_CheckId.SetFontSize(new Font("맑은 고딕", 12));
            //lup_ReadingNumber.SetFontSize(new Font("맑은 고딕", 12));
            //lup_Eye.SetFontSize(new Font("맑은 고딕", 12));

            //lup_CheckId.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //lup_ReadingNumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //lup_Eye.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            var ProcessList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
            var ProcessObj = ProcessList.Where(p => p.CodeVal == TEMP_XFPOP1000_Obj.ProcessCode).FirstOrDefault();
            if (ProcessObj != null)
            {
                if (ProcessObj.Temp != "Y")
                {
                    Self_Check = true;
                    layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                if (ProcessObj.Temp1 != "Y")
                {                    
                    FME_Check = true;
                    layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                if (ProcessObj.Temp2 != "Y")
                {
                    Process_Check = true;
                    layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
            }

            lc_FME_First.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lc_FME_Middle.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lc_FME_End.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            simpleSeparator2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
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
            repositoryItemSpinEdit.Click += RepositorySpinEdit_Click;

            repositoryItemTextEdit = new RepositoryItemTextEdit();
        }

        private void InitButtonLabelConvert()
        {
            btn_InspFrequently.Text = LabelConvert.GetLabelText("InspFrequently");
            btn_InspFME.Text = LabelConvert.GetLabelText("InspFME");
            btn_InspProcess.Text = LabelConvert.GetLabelText("InspProcess");
            btn_Save.Text = LabelConvert.GetLabelText("Save");
            btn_InitRefresh.Text = LabelConvert.GetLabelText("InitRefresh");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");
            //btn_Apply.Text = LabelConvert.GetLabelText("Apply");
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MainView.OptionsView.ShowIndicator = false;

            gridEx1.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"), HorzAlignment.Center, true);
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

            gridEx1.MainGrid.Columns["Reading1"].MinWidth = 60;
            gridEx1.MainGrid.Columns["Reading2"].MinWidth = 60;
            gridEx1.MainGrid.Columns["Reading3"].MinWidth = 60;
            gridEx1.MainGrid.Columns["Reading4"].MinWidth = 60;
            gridEx1.MainGrid.Columns["Reading5"].MinWidth = 60;
            gridEx1.MainGrid.Columns["Reading6"].MinWidth = 60;
            gridEx1.MainGrid.Columns["Reading7"].MinWidth = 60;
            gridEx1.MainGrid.Columns["Reading8"].MinWidth = 60;
            gridEx1.MainGrid.Columns["Reading9"].MinWidth = 60;
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

        /// <summary>
        /// 자주검사 클릭이벤트
        /// </summary>
        private void Btn_InspFrequently_Click(object sender, EventArgs e)
        {
            Search(MasterCodeSTR.InspectionDivision_Frequently);
        }

        /// <summary>
        /// 초중종검사 클릭이벤트
        /// </summary>
        private void Btn_InspFME_Click(object sender, EventArgs e)
        {
            lc_FME_First.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lc_FME_Middle.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lc_FME_End.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            simpleSeparator2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            //Search(MasterCodeSTR.InspectionDivision_FME);
        }

        private void Btn_FME_First_Click(object sender, EventArgs e)
        {
            btn_FME_First.Enabled = false;
            btn_FME_Middle.Enabled = true;
            btn_FME_End.Enabled = true;
            ControlSetting(true);
            Search(MasterCodeSTR.InspectionDivision_FME, "1");
        }

        private void Btn_FME_Middle_Click(object sender, EventArgs e)
        {
            btn_FME_First.Enabled = true;
            btn_FME_Middle.Enabled = false;
            btn_FME_End.Enabled = true;
            ControlSetting(true);
            Search(MasterCodeSTR.InspectionDivision_FME, "2");
        }

        private void Btn_FME_End_Click(object sender, EventArgs e)
        {
            btn_FME_First.Enabled = true;
            btn_FME_Middle.Enabled = true;
            btn_FME_End.Enabled = false;
            ControlSetting(true);
            Search(MasterCodeSTR.InspectionDivision_FME, "3");
        }

        /// <summary>
        /// 공정검사 클릭이벤트
        /// </summary>
        private void Btn_InspProcess_Click(object sender, EventArgs e)
        {
            Search(MasterCodeSTR.InspectionDivision_Process);
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

            var masterCheckResult = list.Any(p=>p.Judge == "NG") ? "NG" : "OK";

            if (qcType == MasterCodeSTR.InspectionDivision_FME)
            {
                var TN_QCT1100_OldObj = QcModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo
                                                                                    && p.WorkSeq == TEMP_XFPOP1000_Obj.ProcessSeq
                                                                                    && p.WorkDate == TEMP_XFPOP1000_Obj.WorkDate
                                                                                    && p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode
                                                                                    && p.ProductLotNo == ProductLotNo 
                                                                                    && p.CheckDate == DateTime.Today
                                                                                    && p.CheckDivision == MasterCodeSTR.InspectionDivision_FME).FirstOrDefault();

                if (TN_QCT1100_OldObj != null)
                {
                    TN_QCT1100_OldObj.CheckId = checkId;
                    TN_QCT1100_OldObj.CheckResult = masterCheckResult;
                    var fmeList = TN_QCT1100_OldObj.TN_QCT1101List.ToList();
                    foreach (var v in list)
                    {
                        if (!btn_FME_First.Enabled)
                        {
                            TN_QCT1100_OldObj.CheckDateTime1 = DateTime.Now;
                            var fmeObj = fmeList.Where(p => p.RevNo == v.RevNo && p.ItemCode == v.ItemCode && p.Seq == v.Seq).FirstOrDefault();
                            if (fmeObj != null)
                            {
                                fmeObj.Reading1 = v.Reading1;
                                fmeObj.Reading2 = v.Reading2;
                                fmeObj.Reading3 = v.Reading3;
                                fmeObj.Judge = v.Judge;
                                fmeObj.UpdateId = checkId;
                                fmeObj.UpdateTime = DateTime.Now;
                            }
                            else
                            {
                                var TN_QCT1101_NewObj = new TN_QCT1101()
                                {
                                    InspNo = TN_QCT1100_OldObj.InspNo,
                                    InspSeq = TN_QCT1100_OldObj.TN_QCT1101List.Count == 0 ? 1 : TN_QCT1100_OldObj.TN_QCT1101List.Max(o => o.InspSeq) + 1,
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
                                    Judge = v.Judge
                                };
                                TN_QCT1100_OldObj.TN_QCT1101List.Add(TN_QCT1101_NewObj);
                            }
                        }
                        else if (!btn_FME_Middle.Enabled)
                        {
                            TN_QCT1100_OldObj.CheckDateTime2 = DateTime.Now;
                            var fmeObj = fmeList.Where(p => p.RevNo == v.RevNo && p.ItemCode == v.ItemCode && p.Seq == v.Seq).FirstOrDefault();
                            if (fmeObj != null)
                            {
                                fmeObj.Reading4 = v.Reading4;
                                fmeObj.Reading5 = v.Reading5;
                                fmeObj.Reading6 = v.Reading6;
                                fmeObj.Judge = v.Judge;
                                fmeObj.UpdateId = checkId;
                                fmeObj.UpdateTime = DateTime.Now;
                            }
                            else
                            {
                                var TN_QCT1101_NewObj = new TN_QCT1101()
                                {
                                    InspNo = TN_QCT1100_OldObj.InspNo,
                                    InspSeq = TN_QCT1100_OldObj.TN_QCT1101List.Count == 0 ? 1 : TN_QCT1100_OldObj.TN_QCT1101List.Max(o => o.InspSeq) + 1,
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
                                    Reading4 = v.Reading4,
                                    Reading5 = v.Reading5,
                                    Reading6 = v.Reading6,
                                    Judge = v.Judge
                                };
                                TN_QCT1100_OldObj.TN_QCT1101List.Add(TN_QCT1101_NewObj);
                            }
                        }
                        else
                        {
                            TN_QCT1100_OldObj.CheckDateTime3 = DateTime.Now;
                            var fmeObj = fmeList.Where(p => p.RevNo == v.RevNo && p.ItemCode == v.ItemCode && p.Seq == v.Seq).FirstOrDefault();
                            if (fmeObj != null)
                            {
                                fmeObj.Reading7 = v.Reading7;
                                fmeObj.Reading8 = v.Reading8;
                                fmeObj.Reading9 = v.Reading9;
                                fmeObj.Judge = v.Judge;
                                fmeObj.UpdateId = checkId;
                                fmeObj.UpdateTime = DateTime.Now;
                            }
                            else
                            {
                                var TN_QCT1101_NewObj = new TN_QCT1101()
                                {
                                    InspNo = TN_QCT1100_OldObj.InspNo,
                                    InspSeq = TN_QCT1100_OldObj.TN_QCT1101List.Count == 0 ? 1 : TN_QCT1100_OldObj.TN_QCT1101List.Max(o => o.InspSeq) + 1,
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
                                    Reading7 = v.Reading7,
                                    Reading8 = v.Reading8,
                                    Reading9 = v.Reading9,
                                    Judge = v.Judge
                                };
                                TN_QCT1100_OldObj.TN_QCT1101List.Add(TN_QCT1101_NewObj);
                            }
                        }
                    }
                    QcModelService.Update(TN_QCT1100_OldObj);
                }
                else
                {
                    var TN_QCT1100_NewObj = new TN_QCT1100()
                    {
                        InspNo = DbRequestHandler.GetSeqMonth(qcType),
                        CheckDivision = qcType,
                        CheckPoint = qcType == MasterCodeSTR.InspectionDivision_FME ? DbRequestHandler.GetCellValue("exec USP_GET_INSPECTION_FME_CHECK_POINT '" + TEMP_XFPOP1000_Obj.WorkNo + "','" + TEMP_XFPOP1000_Obj.ProcessSeq + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "'", 0)
                                                                               : MasterCodeSTR.CheckPoint_General,
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
                            CheckSpec = v.CheckSpec,
                            CheckMax = v.CheckMax,
                            CheckMin = v.CheckMin,
                            CheckUpQuad = v.CheckUpQuad,
                            CheckDownQuad = v.CheckDownQuad,
                            CheckDataType = v.CheckDataType,
                            Judge = v.Judge
                        };

                        if (!btn_FME_First.Enabled)
                        {
                            TN_QCT1100_NewObj.CheckDateTime1 = DateTime.Now;
                            TN_QCT1101_NewObj.Reading1 = v.Reading1;
                            TN_QCT1101_NewObj.Reading2 = v.Reading2;
                            TN_QCT1101_NewObj.Reading3 = v.Reading3;
                        }
                        else if (!btn_FME_Middle.Enabled)
                        {
                            TN_QCT1100_NewObj.CheckDateTime2 = DateTime.Now;
                            TN_QCT1101_NewObj.Reading4 = v.Reading4;
                            TN_QCT1101_NewObj.Reading5 = v.Reading5;
                            TN_QCT1101_NewObj.Reading6 = v.Reading6;
                        }
                        else
                        {
                            TN_QCT1100_NewObj.CheckDateTime3 = DateTime.Now;
                            TN_QCT1101_NewObj.Reading7 = v.Reading7;
                            TN_QCT1101_NewObj.Reading8 = v.Reading8;
                            TN_QCT1101_NewObj.Reading9 = v.Reading9;
                        }

                        TN_QCT1100_NewObj.TN_QCT1101List.Add(TN_QCT1101_NewObj);
                    }
                    QcModelService.Insert(TN_QCT1100_NewObj);
                }
            }
            else
            {
                var TN_QCT1100_NewObj = new TN_QCT1100()
                {
                    InspNo = DbRequestHandler.GetSeqMonth(qcType),
                    CheckDivision = qcType,
                    CheckPoint = qcType == MasterCodeSTR.InspectionDivision_FME ? DbRequestHandler.GetCellValue("exec USP_GET_INSPECTION_FME_CHECK_POINT '" + TEMP_XFPOP1000_Obj.WorkNo + "','" + TEMP_XFPOP1000_Obj.ProcessSeq + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "'", 0)
                                                                                : MasterCodeSTR.CheckPoint_General,
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

                if (qcType == MasterCodeSTR.InspectionDivision_Process)
                {
                    TN_QCT1100_NewObj.CheckDateTime1 = DateTime.Now;
                }

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
            }
            QcModelService.Save();
            SetIsFormControlChanged(false);
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 초기화 클릭이벤트
        /// </summary>
        private void Btn_InitRefresh_Click(object sender, EventArgs e)
        {
            gridEx1.MainGrid.Clear();
            gridEx1.BestFitColumns();

            qcType = null;
            rowid = 0;
            lup_CheckId.EditValue = null;//Utils.Common.GlobalVariable.LoginId;
            //lup_ReadingNumber.EditValue = 1;
            //lup_Eye.EditValue = null;
            //tx_Reading.EditValue = null;

            btn_InspProcess.Enabled = true;
            btn_InspFrequently.Enabled = true;
            btn_InspFME.Enabled = true;

            pic_Limit.EditValue = null;

            ControlSetting(false);

            SetIsFormControlChanged(false);

            lc_FME_First.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lc_FME_Middle.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lc_FME_End.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            simpleSeparator2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            btn_FME_First.Enabled = true;
            btn_FME_Middle.Enabled = true;
            btn_FME_End.Enabled = true;

            gridEx1.MainGrid.Columns["Reading1"].Visible = true;
            gridEx1.MainGrid.Columns["Reading2"].Visible = true;
            gridEx1.MainGrid.Columns["Reading3"].Visible = true;
            gridEx1.MainGrid.Columns["Reading4"].Visible = true;
            gridEx1.MainGrid.Columns["Reading5"].Visible = true;
            gridEx1.MainGrid.Columns["Reading6"].Visible = true;
            gridEx1.MainGrid.Columns["Reading7"].Visible = true;
            gridEx1.MainGrid.Columns["Reading8"].Visible = true;
            gridEx1.MainGrid.Columns["Reading9"].Visible = true;
            gridEx1.MainGrid.Columns["Judge"].Visible = false;
            gridEx1.MainGrid.Columns["Judge"].Visible = true;
        }

        /// <summary>
        /// 취소 클릭이벤트
        /// </summary>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            SetIsFormControlChanged(false);
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
            QcModelService.ReLoad();
            var TN_STD1100 = QcModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode).First();

            if (checkDivision == MasterCodeSTR.InspectionDivision_Frequently && TN_STD1100.SelfInspFlag != "Y")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_67), LabelConvert.GetLabelText("StockInspFlag")));
                return;
            }

            if (checkDivision == MasterCodeSTR.InspectionDivision_Process && TN_STD1100.ProcInspFlag != "Y")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_67), LabelConvert.GetLabelText("ProcInspFlag")));
                return;
            }

            gridEx1.MainGrid.Clear();

            qcType = null;
            rowid = 0;
            lup_CheckId.EditValue = null;//Utils.Common.GlobalVariable.LoginId;
            //lup_ReadingNumber.EditValue = 1;
            //lup_Eye.EditValue = null;
            //tx_Reading.EditValue = null;

            var qcRev = QcModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode && p.UseFlag == "Y").OrderBy(p => p.RowId).LastOrDefault();
            if (qcRev == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionItem")));
                Btn_InitRefresh_Click(null, null);
                return;
            }
            
            var qualityList = QcModelService.GetChildList<TN_QCT1001>(p => p.RevNo == qcRev.RevNo
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
                Btn_InitRefresh_Click(null, null);
                return;
            }


            ControlSetting(true);

            if (checkDivision == MasterCodeSTR.InspectionDivision_FME)
            {
                var TN_QCT1100_OldObj = QcModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo
                                                                                    && p.WorkSeq == TEMP_XFPOP1000_Obj.ProcessSeq
                                                                                    && p.WorkDate == TEMP_XFPOP1000_Obj.WorkDate
                                                                                    && p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode
                                                                                    && p.ProductLotNo == ProductLotNo
                                                                                    && p.CheckDivision == checkDivision
                                                                                    && p.CheckDate == DateTime.Today).FirstOrDefault();
                if (TN_QCT1100_OldObj != null)
                {
                    var fmeList = TN_QCT1100_OldObj.TN_QCT1101List.ToList();
                    if (fmeList.Count > 0)
                    {
                        foreach (var v in qualityList)
                        {
                            var fmeObj = fmeList.Where(p => p.RevNo == v.RevNo && p.ItemCode == v.ItemCode && p.Seq == v.Seq).FirstOrDefault();
                            if (fmeObj != null)
                            {
                                v.Reading1 = fmeObj.Reading1;
                                v.Reading2 = fmeObj.Reading2;
                                v.Reading3 = fmeObj.Reading3;
                                v.Reading4 = fmeObj.Reading4;
                                v.Reading5 = fmeObj.Reading5;
                                v.Reading6 = fmeObj.Reading6;
                                v.Reading7 = fmeObj.Reading7;
                                v.Reading8 = fmeObj.Reading8;
                                v.Reading9 = fmeObj.Reading9;
                                v.Judge = fmeObj.Judge;
                            }
                        }
                    }
                }
            }

            GridBindingSource.DataSource = qualityList;
            gridEx1.DataSource = GridBindingSource;
            gridEx1.MainGrid.BestFitColumns();

            qcType = checkDivision;
        }

        /// <summary>
        /// 버튼별 조회 함수.
        /// </summary> 
        private void Search(string checkDivision, string FME_Division)
        {
            QcModelService.ReLoad();
            var TN_STD1100 = QcModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode).First();

            if (checkDivision == MasterCodeSTR.InspectionDivision_Frequently && TN_STD1100.SelfInspFlag != "Y")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_67), LabelConvert.GetLabelText("StockInspFlag")));
                return;
            }

            if (checkDivision == MasterCodeSTR.InspectionDivision_Process && TN_STD1100.ProcInspFlag != "Y")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_67), LabelConvert.GetLabelText("ProcInspFlag")));
                return;
            }

            gridEx1.MainGrid.Clear();

            qcType = null;
            rowid = 0;
            lup_CheckId.EditValue = null;//Utils.Common.GlobalVariable.LoginId;
            //lup_ReadingNumber.EditValue = 1;
            //lup_Eye.EditValue = null;
            //tx_Reading.EditValue = null;

            var qcRev = QcModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode && p.UseFlag == "Y").OrderBy(p => p.RowId).LastOrDefault();
            if (qcRev == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionItem")));
                Btn_InitRefresh_Click(null, null);
                return;
            }

            var qualityList = QcModelService.GetChildList<TN_QCT1001>(p => p.RevNo == qcRev.RevNo
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
                Btn_InitRefresh_Click(null, null);
                return;
            }


            ControlSetting(true);

            if (checkDivision == MasterCodeSTR.InspectionDivision_FME)
            {
                var TN_QCT1100_OldObj = QcModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo
                                                                                    && p.WorkSeq == TEMP_XFPOP1000_Obj.ProcessSeq
                                                                                    && p.WorkDate == TEMP_XFPOP1000_Obj.WorkDate
                                                                                    && p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode
                                                                                    && p.ProductLotNo == ProductLotNo
                                                                                    && p.CheckDivision == checkDivision
                                                                                    && p.CheckDate == DateTime.Today).FirstOrDefault();
                if (TN_QCT1100_OldObj != null)
                {
                    var fmeList = TN_QCT1100_OldObj.TN_QCT1101List.ToList();
                    if (fmeList.Count > 0)
                    {
                        foreach (var v in qualityList)
                        {
                            var fmeObj = fmeList.Where(p => p.RevNo == v.RevNo && p.ItemCode == v.ItemCode && p.Seq == v.Seq).FirstOrDefault();
                            if (fmeObj != null)
                            {
                                v.Reading1 = fmeObj.Reading1;
                                v.Reading2 = fmeObj.Reading2;
                                v.Reading3 = fmeObj.Reading3;
                                v.Reading4 = fmeObj.Reading4;
                                v.Reading5 = fmeObj.Reading5;
                                v.Reading6 = fmeObj.Reading6;
                                v.Reading7 = fmeObj.Reading7;
                                v.Reading8 = fmeObj.Reading8;
                                v.Reading9 = fmeObj.Reading9;
                                v.Judge = fmeObj.Judge;
                            }
                        }
                    }
                }
            }

            if (FME_Division == "1")
            {
                gridEx1.MainGrid.Columns["Reading1"].Visible = true;
                gridEx1.MainGrid.Columns["Reading2"].Visible = true;
                gridEx1.MainGrid.Columns["Reading3"].Visible = true;
                gridEx1.MainGrid.Columns["Reading4"].Visible = false;
                gridEx1.MainGrid.Columns["Reading5"].Visible = false;
                gridEx1.MainGrid.Columns["Reading6"].Visible = false;
                gridEx1.MainGrid.Columns["Reading7"].Visible = false;
                gridEx1.MainGrid.Columns["Reading8"].Visible = false;
                gridEx1.MainGrid.Columns["Reading9"].Visible = false;
                gridEx1.MainGrid.Columns["Judge"].Visible = false;
                gridEx1.MainGrid.Columns["Judge"].Visible = true;

                gridEx1.MainGrid.Columns["Reading1"].VisibleIndex = gridEx1.MainGrid.Columns["MaxReading"].VisibleIndex + 1;
                gridEx1.MainGrid.Columns["Reading2"].VisibleIndex = gridEx1.MainGrid.Columns["Reading1"].VisibleIndex + 1;
                gridEx1.MainGrid.Columns["Reading3"].VisibleIndex = gridEx1.MainGrid.Columns["Reading2"].VisibleIndex + 1;
                gridEx1.MainGrid.Columns["Judge"].VisibleIndex = gridEx1.MainGrid.Columns["Reading3"].VisibleIndex + 1;

                //lup_ReadingNumber.EditValue = 1;
            }
            else if (FME_Division == "2")
            {
                gridEx1.MainGrid.Columns["Reading1"].Visible = false;
                gridEx1.MainGrid.Columns["Reading2"].Visible = false;
                gridEx1.MainGrid.Columns["Reading3"].Visible = false;
                gridEx1.MainGrid.Columns["Reading4"].Visible = true;
                gridEx1.MainGrid.Columns["Reading5"].Visible = true;
                gridEx1.MainGrid.Columns["Reading6"].Visible = true;
                gridEx1.MainGrid.Columns["Reading7"].Visible = false;
                gridEx1.MainGrid.Columns["Reading8"].Visible = false;
                gridEx1.MainGrid.Columns["Reading9"].Visible = false;
                gridEx1.MainGrid.Columns["Judge"].Visible = false;
                gridEx1.MainGrid.Columns["Judge"].Visible = true;

                gridEx1.MainGrid.Columns["Reading4"].VisibleIndex = gridEx1.MainGrid.Columns["MaxReading"].VisibleIndex + 1;
                gridEx1.MainGrid.Columns["Reading5"].VisibleIndex = gridEx1.MainGrid.Columns["Reading4"].VisibleIndex + 1;
                gridEx1.MainGrid.Columns["Reading6"].VisibleIndex = gridEx1.MainGrid.Columns["Reading5"].VisibleIndex + 1;
                gridEx1.MainGrid.Columns["Judge"].VisibleIndex = gridEx1.MainGrid.Columns["Reading6"].VisibleIndex + 1;

                //lup_ReadingNumber.EditValue = 4;
            }
            else
            {
                gridEx1.MainGrid.Columns["Reading1"].Visible = false;
                gridEx1.MainGrid.Columns["Reading2"].Visible = false;
                gridEx1.MainGrid.Columns["Reading3"].Visible = false;
                gridEx1.MainGrid.Columns["Reading4"].Visible = false;
                gridEx1.MainGrid.Columns["Reading5"].Visible = false;
                gridEx1.MainGrid.Columns["Reading6"].Visible = false;
                gridEx1.MainGrid.Columns["Reading7"].Visible = true;
                gridEx1.MainGrid.Columns["Reading8"].Visible = true;
                gridEx1.MainGrid.Columns["Reading9"].Visible = true;
                gridEx1.MainGrid.Columns["Judge"].Visible = false;
                gridEx1.MainGrid.Columns["Judge"].Visible = true;

                gridEx1.MainGrid.Columns["Reading7"].VisibleIndex = gridEx1.MainGrid.Columns["MaxReading"].VisibleIndex + 1;
                gridEx1.MainGrid.Columns["Reading8"].VisibleIndex = gridEx1.MainGrid.Columns["Reading7"].VisibleIndex + 1;
                gridEx1.MainGrid.Columns["Reading9"].VisibleIndex = gridEx1.MainGrid.Columns["Reading8"].VisibleIndex + 1;
                gridEx1.MainGrid.Columns["Judge"].VisibleIndex = gridEx1.MainGrid.Columns["Reading9"].VisibleIndex + 1;

                //lup_ReadingNumber.EditValue = 7;
            }

            GridBindingSource.DataSource = qualityList;
            gridEx1.DataSource = GridBindingSource;
            gridEx1.MainGrid.BestFitColumns();

            qcType = checkDivision;
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var obj = GridBindingSource.Current as TN_QCT1001;
            if (obj == null) return;

            rowid = e.FocusedRowHandle;

            var TN_STD1104 = obj.TN_QCT1000.TN_STD1100.TN_STD1104List.Where(p => p.CheckList == obj.CheckList).OrderBy(p => p.Seq).LastOrDefault();
            if (TN_STD1104 == null || TN_STD1104.FileUrl.IsNullOrEmpty())
                pic_Limit.EditValue = null;
            else
                pic_Limit.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1104.FileUrl);

            //if (obj.CheckWay == MasterCodeSTR.InspectionWay_Eye)
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

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("Reading") && !e.Column.FieldName.Contains("MaxReading"))
                {
                    //var inspectionWay = view.GetRowCellValue(e.RowHandle, "CheckWay").GetNullToEmpty();
                    //if (inspectionWay == MasterCodeSTR.InspectionWay_Input)
                    //{
                    //    //검사방법이 치수검사일 때 검사데이터타입이 없을 경우 
                    //    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    //    if (checkDataType.IsNullOrEmpty())
                    //    {
                    //        e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                    //    }
                    //    else
                    //    {
                    //        e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                    //    }

                    //    var checkSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                    //    var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                    //    var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                    //    var readingValue = e.CellValue.GetDecimalNullToNull();
                    //    e.Appearance.ForeColor = DetailCheckInputColor(checkSpec, checkUpQuad, checkDownQuad, readingValue);
                    //}
                    //else if (inspectionWay == MasterCodeSTR.InspectionWay_Eye)
                    //{
                    //    e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    //    var readingValue = e.CellValue.GetNullToEmpty();
                    //    e.Appearance.ForeColor = DetailCheckInputColor(readingValue);
                    //}
                    //else
                    //{
                    //    e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                    //    var checkSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                    //    var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                    //    var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                    //    var readingValue = e.CellValue.GetDecimalNullToNull();
                    //    e.Appearance.ForeColor = DetailCheckInputColor(checkSpec, checkUpQuad, checkDownQuad, readingValue);
                    //}

                    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    if (checkDataType == MasterCodeSTR.CheckDataType_C)
                    {
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                        var readingValue = e.CellValue.GetNullToEmpty();
                        e.Appearance.ForeColor = DetailCheckInputColor(readingValue);
                    }
                    else
                    {
                        //검사방법이 치수검사일 때 검사데이터타입이 없을 경우 
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                        var checkSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                        var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                        var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                        var readingValue = e.CellValue.GetDecimalNullToNull();
                        e.Appearance.ForeColor = DetailCheckInputColor(checkSpec, checkUpQuad, checkDownQuad, readingValue);
                    }
                }
                else if (e.Column.FieldName == "Judge")
                {
                    var judgeValue = view.GetRowCellValue(e.RowHandle, "Judge").GetNullToEmpty();
                    if (judgeValue == "NG")
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
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
        //private void Lup_ReadingNumber_EditValueChanged(object sender, EventArgs e)
        //{
        //    var view = gridEx1.MainGrid.MainView as GridView;
        //    view.FocusedRowHandle = 0;
        //}

        /// <summary>
        /// 컨트롤 셋팅
        /// </summary>
        private void ControlSetting(bool editFlag)
        {
            btn_InspFrequently.Enabled = !editFlag;
            btn_InspFME.Enabled = !editFlag;
            btn_InspProcess.Enabled = !editFlag;
            btn_Save.Enabled = editFlag;
            btn_InitRefresh.Enabled = editFlag;
            lup_CheckId.ReadOnly = !editFlag;
            //lup_ReadingNumber.ReadOnly = !editFlag;
            //lup_Eye.ReadOnly = true;
            //tx_Reading.ReadOnly = true;
            //btn_Apply.Enabled = editFlag;

            //if (editFlag)
            //{
            //    btn_InspFrequently.Enabled = !editFlag;
            //    btn_InspFME.Enabled = !editFlag;
            //    btn_InspProcess.Enabled = !editFlag;
            //    btn_Save.Enabled = editFlag;
            //    btn_InitRefresh.Enabled = editFlag;
            //    lup_CheckId.ReadOnly = !editFlag;
            //    lup_ReadingNumber.ReadOnly = !editFlag;
            //    lup_Eye.ReadOnly = !editFlag;
            //    tx_Reading.ReadOnly = !editFlag;
            //    btn_Apply.Enabled = editFlag;
            //}
            //else
            //{
            //    btn_InspFrequently.Enabled = !editFlag;
            //    btn_InspFME.Enabled = !editFlag;
            //    btn_InspProcess.Enabled = !editFlag;
            //    btn_Save.Enabled = editFlag;
            //    btn_InitRefresh.Enabled = editFlag;
            //    lup_CheckId.ReadOnly = !editFlag;
            //    lup_ReadingNumber.ReadOnly = !editFlag;
            //    lup_Eye.ReadOnly = !editFlag;
            //    tx_Reading.ReadOnly = !editFlag;
            //    btn_Apply.Enabled = editFlag;
            //}

            //if (!Self_Check)
            //    btn_InspFrequently.Enabled = false;
            //if (!FME_Check)
            //    btn_InspFME.Enabled = false;
            //if (!Process_Check)
            //    btn_InspProcess.Enabled = false;

            //if (!btn_InitRefresh.Enabled)
            //{
            //    btn_InspFrequently.AppearanceDisabled.ForeColor = btn_InitRefresh.ForeColor;
            //    btn_InspFME.AppearanceDisabled.ForeColor = Color.FromArgb(235, 235, 228);
            //    btn_InspProcess.AppearanceDisabled.ForeColor = btn_InitRefresh.ForeColor;
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

        //private void Tx_Reading_GotFocus(object sender, EventArgs e)
        //{
        //    tx_Reading.SelectAll();
        //}

        private Color DetailCheckInputColor(decimal? checkSpec, decimal checkUpQuad, decimal checkDownQuad, decimal? readingValue)
        {
            //var checkSpec = detailObj.CheckSpec.GetDecimalNullToNull();
            if (checkSpec == null)
                return Color.Black;
            else
            {
                //var checkUpQuad = detailObj.CheckUpQuad.GetDecimalNullToZero();
                //var checkDownQuad = detailObj.CheckDownQuad.GetDecimalNullToZero();
                var checkUp = checkSpec + checkUpQuad;
                var checkDown = checkSpec - checkDownQuad;

                if (readingValue != null)
                {
                    if (readingValue >= checkDown && readingValue <= checkUp)
                        return Color.Black;
                    else
                        return Color.Red;
                }
                else
                    return Color.Black;
            }
        }

        private Color DetailCheckInputColor(string readingValue)
        {
            if (!readingValue.IsNullOrEmpty())
            {
                if (readingValue == "OK")
                    return Color.Black;
                else
                    return Color.Red;
            }
            else
                return Color.Black;
        }

        //private void Lup_ReadingNumber_Popup(object sender, EventArgs e)
        //{
        //    var lookup = sender as SearchLookUpEdit;
        //    if (lookup == null) return;

        //    if (lc_FME_First.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always) 
        //    {
        //        if (!btn_FME_First.Enabled)
        //        {
        //            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[CodeVal] = '1' OR [CodeVal] = '2' OR [CodeVal] = '3'";
        //        }
        //        else if (!btn_FME_Middle.Enabled)
        //        {
        //            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[CodeVal] = '4' OR [CodeVal] = '5' OR [CodeVal] = '6'";
        //        }
        //        else
        //        {
        //            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[CodeVal] = '7' OR [CodeVal] = '8' OR [CodeVal] = '9'";
        //        }
        //    }
        //    else
        //    {
        //        lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
        //    }
        //}

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

        private void RepositorySpinEdit_Click(object sender, EventArgs e)
        {
            var spinEdit = sender as SpinEdit;
            spinEdit.SelectAll();
            if (!GlobalVariable.KeyPad) return;
            var keyPad = new XFCKEYPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                spinEdit.EditValue = keyPad.returnval;
            }
        }
    }
}
