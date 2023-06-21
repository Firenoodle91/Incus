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
    /// 20210619 오세완 차장
    /// 대영 POP 품질등록 팝업 창, 기존 버전하고 다른점은 사용하지 않는 코드 정리, 초중종 검사 방법 변경
    /// </summary>
    public partial class XPFINSPECTION_V2 : HKInc.Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_QCT1100> QcModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;
        string ProductLotNo;
        int rowid;
        string qcType;

        /// <summary>
        /// 20210621 오세완 차장
        /// 초중종검사시 초/중/종 구분값 설정
        /// </summary>
        private string FME_checkpoint = "";

        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemSpinEdit repositoryItemSpinEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;
        #endregion

        public XPFINSPECTION_V2(TEMP_XFPOP1000 obj, string productLotNo)
        {
            InitializeComponent();

            this.Text = LabelConvert.GetLabelText("QualityAdd");

            this.Size = new Size(this.Size.Width, this.Size.Height - 40);

            gridEx1.ViewType = GridViewType.POP_GridView;

            TEMP_XFPOP1000_Obj = obj;
            ProductLotNo = productLotNo;

            gridEx1.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            gridEx1.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            gridEx1.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            gridEx1.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            gridEx1.MainGrid.MainView.KeyDown += MainView_KeyDown;

            btn_InspFrequently.Click += Btn_InspFrequently_Click;
            btn_InspFME.Click += Btn_InspFME_Click;
            btn_InspProcess.Click += Btn_InspProcess_Click;
            btn_Save.Click += Btn_Save_Click;
            btn_InitRefresh.Click += Btn_InitRefresh_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
            
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
            // 20210819 오세완 차장 생산부, 품질부를 같이 나오게 해달라는 요청으로 수정
            //lup_CheckId.SetDefaultPOP(false, "LoginId", "UserName", QcModelService.GetChildList<User>(p => p.DepartmentCode== "DEPT-00003"
            //                                                                && p.Active == "Y"

            //                                                            )
            //                                                            .ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            List<User> qcUser_Arr = QcModelService.GetChildList<User>(p => (p.DepartmentCode == "DEPT-00002" || p.DepartmentCode == "DEPT-00003") &&
                                                                            p.Active == "Y").ToList();
            lup_CheckId.SetDefaultPOP(false, "LoginId", "UserName", qcUser_Arr, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            var ProcessList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
            var ProcessObj = ProcessList.Where(p => p.CodeVal == TEMP_XFPOP1000_Obj.ProcessCode).FirstOrDefault();
            if (ProcessObj != null)
            {
                if (ProcessObj.Temp != "Y")
                {
                    layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                if (ProcessObj.Temp1 != "Y")
                {                    
                    layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                if (ProcessObj.Temp2 != "Y")
                {
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
            repositoryItemGridLookUpEdit.Closed += RepositoryItemGridLookUpEdit_Closed;
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

        /// <summary>
        /// 20210619 오세완 차장 
        /// 값을 입력하고 포커스를 이동해야만 판정이 나와서 아예 자동으로 이동처리 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepositoryItemGridLookUpEdit_Closed(object sender, ClosedEventArgs e)
        {
            gridEx1.MainGrid.MainView.FocusedColumn = gridEx1.MainGrid.MainView.Columns["Judge"];
        }

        private void RepositoryItemSpinEdit_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad)
                return;

            var spinEidt = sender as SpinEdit;
            var keyPad = new XFCNUMPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                spinEidt.EditValue = keyPad.returnval;
            }

            // 20210619 오세완 차장 값을 입력하고 포커스를 이동해야만 판정이 나와서 아예 자동으로 이동처리 추가
            gridEx1.MainGrid.MainView.FocusedColumn = gridEx1.MainGrid.MainView.Columns["Judge"];
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MainView.OptionsView.ShowIndicator = false;

            gridEx1.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"));
            gridEx1.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));
            gridEx1.MainGrid.AddColumn("CheckDataType", LabelConvert.GetLabelText("CheckDataType"));
            gridEx1.MainGrid.AddColumn("InspectionReportMemo", LabelConvert.GetLabelText("InspectionReportMemo"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("CheckMin", LabelConvert.GetLabelText("CheckMin"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("CheckMax", LabelConvert.GetLabelText("CheckMax"), HorzAlignment.Center, true);            
            gridEx1.MainGrid.AddColumn("CheckSpec", LabelConvert.GetLabelText("CheckSpec"), HorzAlignment.Center, false);
            gridEx1.MainGrid.AddColumn("CheckUpQuad", LabelConvert.GetLabelText("CheckUpQuad"), HorzAlignment.Center, false);
            gridEx1.MainGrid.AddColumn("CheckDownQuad", LabelConvert.GetLabelText("CheckDownQuad"), HorzAlignment.Center, false);
            gridEx1.MainGrid.AddColumn("MaxReading", LabelConvert.GetLabelText("MaxReading"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("Reading1", LabelConvert.GetLabelText("Reading1"), HorzAlignment.Center,true,70);
            gridEx1.MainGrid.AddColumn("Reading2", LabelConvert.GetLabelText("Reading2"), HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("Reading3", LabelConvert.GetLabelText("Reading3"), HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("Reading4", LabelConvert.GetLabelText("Reading4"), HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("Reading5", LabelConvert.GetLabelText("Reading5"), HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("Reading6", LabelConvert.GetLabelText("Reading6"), HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("Reading7", LabelConvert.GetLabelText("Reading7"), HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("Reading8", LabelConvert.GetLabelText("Reading8"), HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("Reading9", LabelConvert.GetLabelText("Reading9"), HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("Judge", LabelConvert.GetLabelText("Judge"));
            gridEx1.MainGrid.SetEditable(true, "Reading1", "Reading2", "Reading3", "Reading4", "Reading5", "Reading6", "Reading7", "Reading8", "Reading9");

            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 15f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
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
        }

        private void Btn_FME_First_Click(object sender, EventArgs e)
        {
            btn_FME_First.Enabled = false;
            btn_FME_Middle.Enabled = true;
            btn_FME_End.Enabled = true;
            ControlSetting(true);
            FME_checkpoint = Set_FME_CHECKPONT_VALUE("초물");
            Search_FME(MasterCodeSTR.InspectionDivision_FME, MasterCodeSTR.CheckPoint_First);
        }

        private void Btn_FME_Middle_Click(object sender, EventArgs e)
        {
            btn_FME_First.Enabled = true;
            btn_FME_Middle.Enabled = false;
            btn_FME_End.Enabled = true;
            ControlSetting(true);
            FME_checkpoint = Set_FME_CHECKPONT_VALUE("중물");
            Search_FME(MasterCodeSTR.InspectionDivision_FME, MasterCodeSTR.CheckPoint_Middle);
        }

        private void Btn_FME_End_Click(object sender, EventArgs e)
        {
            btn_FME_First.Enabled = true;
            btn_FME_Middle.Enabled = true;
            btn_FME_End.Enabled = false;
            ControlSetting(true);
            FME_checkpoint = Set_FME_CHECKPONT_VALUE("종물");
            Search_FME(MasterCodeSTR.InspectionDivision_FME, MasterCodeSTR.CheckPoint_End);
        }

        /// <summary>
        /// 20210621 오세완 차장 
        /// 초중종 검사 checkpoint 값 공통코드 조회
        /// </summary>
        /// <returns>코드값</returns>
        private string Set_FME_CHECKPONT_VALUE(string sPoint)
        {
            string sResult = "";

            List<TN_STD1000> tempArr = QcModelService.GetChildList<TN_STD1000>(p => p.CodeMain == MasterCodeSTR.InspectionFME_POP &&
                                                                                    p.UseYN == "Y").ToList();
            if(tempArr != null)
                if(tempArr.Count > 0)
                {
                    TN_STD1000 each = tempArr.Where(p => p.CodeName == sPoint).FirstOrDefault();
                    sResult = each.CodeVal;
                }

            return sResult;
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

            // 20210619 오세완 차장 초중종검사도 일반 검사와 동일하게 처리
            var TN_QCT1100_NewObj = new TN_QCT1100()
            {
                InspNo = DbRequestHandler.GetSeqMonth(qcType),
                CheckDivision = qcType,
                CheckPoint = qcType == MasterCodeSTR.InspectionDivision_FME ? FME_checkpoint : MasterCodeSTR.CheckPoint_General,
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
                CheckDateTime1 = DateTime.Now
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
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 초기화 클릭이벤트
        /// </summary>
        private void Btn_InitRefresh_Click(object sender, EventArgs e)
        {
            gridEx1.MainGrid.Clear();
            gridEx1.MainGrid.Columns.Clear();
         
         
            InitGrid();
            InitRepository();
            qcType = null;
            rowid = 0;
            lup_CheckId.EditValue = null;

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
        /// 버튼별 조회 함수.
        /// </summary> 
        private void Search(string checkDivision)
        {
            QcModelService.ReLoad();
            var TN_STD1100 = QcModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode).First();

            if (checkDivision == MasterCodeSTR.InspectionDivision_Frequently && TN_STD1100.SelfInspFlag != "Y")
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_67), LabelConvert.GetLabelText("SelfInspFlag")));
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
            lup_CheckId.EditValue = null;

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
            else
            {
                // 20210817 오세완 차장 검사 규격이 숫자인 경우 규격에 숫자 아닌 다른 값을 넣었을 때 오류가 발생해서 회피하는 코드 추가
                // 22010818 오세완 차장 foreach문에서 loop를 도는 list를 remove하게되면 오류가 발생해서 비교형태로 변경
                List<TN_QCT1001> tempList = new List<TN_QCT1001>();
                foreach (TN_QCT1001 each in qualityList)
                {
                    if (each.CheckWay.GetNullToEmpty() == MasterCodeSTR.InspectionWay_Input)
                    {
                        if (each.CheckSpec.GetNullToEmpty() != "")
                        {
                            decimal dTemp;
                            bool bResult = decimal.TryParse(each.CheckSpec.ToString(), out dTemp);
                            if (bResult)
                                tempList.Add(each);
                        }
                    }
                    else
                        tempList.Add(each);
                }

                if (qualityList.Count > tempList.Count)
                    qualityList = tempList;
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
        private void Search_FME(string checkDivision, string checkPoint)
        {
            QcModelService.ReLoad();
            gridEx1.MainGrid.Clear();

            qcType = null;
            rowid = 0;
            lup_CheckId.EditValue = null;

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
            else
            {
                // 20210817 오세완 차장 검사 규격이 숫자인 경우 규격에 숫자 아닌 다른 값을 넣었을 때 오류가 발생해서 회피하는 코드 추가
                // 22010818 오세완 차장 foreach문에서 loop를 도는 list를 remove하게되면 오류가 발생해서 비교형태로 변경
                List<TN_QCT1001> tempList = new List<TN_QCT1001>();
                foreach (TN_QCT1001 each in qualityList)
                {
                    if (each.CheckWay.GetNullToEmpty() == MasterCodeSTR.InspectionWay_Input)
                    {
                        if (each.CheckSpec.GetNullToEmpty() != "")
                        {
                            decimal dTemp;
                            bool bResult = decimal.TryParse(each.CheckSpec.ToString(), out dTemp);
                            if (bResult)
                                tempList.Add(each);
                        }
                    }
                    else
                        tempList.Add(each);
                }

                if (qualityList.Count > tempList.Count)
                    qualityList = tempList;
            }


            ControlSetting(true);

            //20210623 오세완 차장 기존에 실시한 검사 내역을 보여주는 로직인데 대영은 필요가 없어 보여서 임시 생략
            if (checkDivision == MasterCodeSTR.InspectionDivision_FME)
            {
                var TN_QCT1100_OldObj = QcModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo
                                                                                    && p.WorkSeq == TEMP_XFPOP1000_Obj.ProcessSeq
                                                                                    && p.WorkDate == TEMP_XFPOP1000_Obj.WorkDate
                                                                                    && p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode
                                                                                    && p.ProductLotNo == ProductLotNo
                                                                                    && p.CheckDivision == checkDivision
                                                                                    && p.CheckPoint == checkPoint
                                                                                    && p.CheckDate == DateTime.Today).OrderByDescending(o => o.InspNo).FirstOrDefault();
                if (TN_QCT1100_OldObj != null)
                {
                    var fmeList = TN_QCT1100_OldObj.TN_QCT1101List.ToList();
                    if (fmeList.Count > 0)
                    {
                        lup_CheckId.EditValue = TN_QCT1100_OldObj.CheckId;
                        lup_CheckId.ReadOnly = true;
                        GridColumnReadOnlyYN(true);

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

            // 20210619 오세완 차장 초중종검사 최대 시료수 설정
            int iMaxReading = 0;
            if(qualityList != null)
                if(qualityList.Count > 0)
                {
                    foreach(TN_QCT1001 each in qualityList)
                    {
                        iMaxReading = each.MaxReading.GetIntNullToZero();
                        if (iMaxReading == 0)
                            iMaxReading = 9;

                        for (int i = 1; i <= iMaxReading; i++)
                        {
                            string sIndex = "Reading" + i.ToString();
                            gridEx1.MainGrid.Columns[sIndex].Visible = true;
                            if (i == 1)
                                gridEx1.MainGrid.Columns[sIndex].VisibleIndex = gridEx1.MainGrid.Columns["MaxReading"].VisibleIndex + 1;
                        }

                        if (iMaxReading < 9)
                        {
                            for (int i = iMaxReading + 1; i <= 9; i++)
                            {
                                string sIndex = "Reading" + i.ToString();
                                gridEx1.MainGrid.Columns[sIndex].Visible = false;
                            }
                        }

                        gridEx1.MainGrid.Columns["Judge"].Visible = false;
                        gridEx1.MainGrid.Columns["Judge"].Visible = true;
                        gridEx1.MainGrid.Columns["Judge"].VisibleIndex = gridEx1.MainGrid.Columns["MaxReading"].VisibleIndex + 1;
                    }

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
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var view = sender as GridView;

            var obj = GridBindingSource.Current as TN_QCT1001;
            if (obj == null) return;

            if (e.Column.FieldName.Contains("Reading"))
            {
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

                        // 20210817 오세완 차장 검사 규격이 숫자인 경우 규격에 숫자 아닌 다른 값을 넣었을 때 오류가 발생해서 회피하는 코드 추가
                        //object oSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec");
                        //if(oSpec != null)
                        //{
                        //    decimal dTemp;
                        //    if(decimal.TryParse(oSpec.ToString(), out dTemp))
                        //    {
                        //        var checkSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                        //        var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                        //        var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                        //        var readingValue = e.CellValue.GetDecimalNullToNull();
                        //        e.Appearance.ForeColor = DetailCheckInputColor(checkSpec, checkUpQuad, checkDownQuad, readingValue);
                        //    }
                        //}
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

        private void GridColumnReadOnlyYN(bool ynCheck)
        {
            gridEx1.MainGrid.Columns["Reading1"].OptionsColumn.ReadOnly = ynCheck;
            gridEx1.MainGrid.Columns["Reading2"].OptionsColumn.ReadOnly = ynCheck;
            gridEx1.MainGrid.Columns["Reading3"].OptionsColumn.ReadOnly = ynCheck;
            gridEx1.MainGrid.Columns["Reading4"].OptionsColumn.ReadOnly = ynCheck;
            gridEx1.MainGrid.Columns["Reading5"].OptionsColumn.ReadOnly = ynCheck;
            gridEx1.MainGrid.Columns["Reading6"].OptionsColumn.ReadOnly = ynCheck;
            gridEx1.MainGrid.Columns["Reading7"].OptionsColumn.ReadOnly = ynCheck;
            gridEx1.MainGrid.Columns["Reading8"].OptionsColumn.ReadOnly = ynCheck;
            gridEx1.MainGrid.Columns["Reading9"].OptionsColumn.ReadOnly = ynCheck;
        }

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

            if (gridEx1.MainGrid.Columns.Count > 0)
            {
                GridColumnReadOnlyYN(false);
            }
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

        private Color DetailCheckInputColor(decimal? checkSpec, decimal checkUpQuad, decimal checkDownQuad, decimal? readingValue)
        {
            if (checkSpec == null)
                return Color.Black;
            else
            {
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
                    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    if (checkDataType == MasterCodeSTR.CheckDataType_C)
                    {
                        //육안검사 
                        e.RepositoryItem = repositoryItemGridLookUpEdit;
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

            // 20210629 오세완 차장 기존에 있던 로직 전면 수정
            TN_QCT1001 detailObj = GridBindingSource.Current as TN_QCT1001; 
            if (detailObj == null) return;

            decimal maxReading = detailObj.MaxReading.GetDecimalNullToZero();
            if (maxReading == 0)
                maxReading = 9;

            if (gv.FocusedColumn.FieldName.Contains("Reading") && !gv.FocusedColumn.FieldName.Contains("MaxReading"))
            {
                if (gv.FocusedColumn.FieldName.Right(1).GetDecimalNullToNull() > maxReading)
                    e.Cancel = true;
            }
        }
    }
}
