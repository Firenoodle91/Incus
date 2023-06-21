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
    /// 20210603 오세완 차장
    /// 대영 재가동TO 입력 팝업
    /// </summary>
    public partial class XPFRESTART_TO : HKInc.Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_QCT1001> ModelService = (IService<TN_QCT1001>)ProductionFactory.GetDomainService("TN_QCT1001");
        IService<TN_QCT1100> QcModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");

        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;
        string ProductLotNo;
        int rowid;
        string qcType;
       
        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemSpinEdit repositoryItemSpinEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;
        #endregion

        public XPFRESTART_TO(TEMP_XFPOP1000 obj, string productLotNo)
        {
            InitializeComponent();
        
            this.Text = LabelConvert.GetLabelText("RestartTO");
            this.Size = new Size(this.Size.Width, this.Size.Height - 40);

            gridEx1.ViewType = Utils.Enum.GridViewType.POP_GridView;

            TEMP_XFPOP1000_Obj = obj;
            ProductLotNo = productLotNo;

            gridEx1.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            gridEx1.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            gridEx1.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            gridEx1.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            gridEx1.MainGrid.MainView.KeyDown += MainView_KeyDown;
            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle; // 20210629 오세완 차장 기존에 색상 구분이 없어서 추가

            btn_Save.Click += Btn_Save_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
            
            this.WindowState = FormWindowState.Maximized;

            // 20210630 오세완 차장 값에 따라서 측정값이 엎어지는 현상 제거처리
            //lup_CheckPoint.EditValueChanged += Lup_CheckPoint_EditValueChanged;
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

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            // 20210603 오세완 차장 재가동TO에서 검사지점을 입력하려면 Q007에서 앞에 J를 붙인 코드를 채번해야 한다. 
            lup_CheckPoint.SetDefaultPOP(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.CheckPoint).Where(p => p.CodeVal.Left(1) == "J").ToList(), TextEditStyles.DisableTextEditor);
            lup_CheckId.SetDefaultPOP(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            ControlSetting(false);

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

            repositoryItemGridLookUpEdit.Closed += RepositoryItemGridLookUpEdit_Closed;

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
        /// 20210606 오세완 차장 
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
            if (!GlobalVariable.KeyPad) return;
            var spinEidt = sender as SpinEdit;
            var keyPad = new XFCNUMPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                spinEidt.EditValue = keyPad.returnval;
            }

            // 20210606 오세완 차장 값을 입력하고 포커스를 이동해야만 판정이 나와서 아예 자동으로 이동처리 추가
            gridEx1.MainGrid.MainView.FocusedColumn = gridEx1.MainGrid.MainView.Columns["Judge"];
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

        protected override void InitDataLoad()
        {
            Search(MasterCodeSTR.Inspection_RestartTO);
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
                ScmYn = "N"
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
                    Judge = v.Judge,
                    ScmYn = TN_QCT1100_NewObj.ScmYn
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
        /// 버튼별 조회 함수.
        /// </summary> 
        private void Search(string checkDivision)
        {
            gridEx1.MainGrid.Clear();
            ModelService.ReLoad();

            qcType = null;
            rowid = 0;
            lup_CheckId.EditValue = null;

            // 20210606 오세완 차장 검사기준정보 리비전 가장 최근값을 가져오는 로직
            var qcRev = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode && 
                                                                   p.UseFlag == "Y").OrderBy(p => p.RowId).LastOrDefault();

            if (qcRev == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionItem")));
                ControlSetting(false);
                return;
            }
            
            var qualityList = ModelService.GetList(p => p.RevNo == qcRev.RevNo && 
                                                        p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode && 
                                                        p.CheckDivision == checkDivision && 
                                                        p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode && 
                                                        p.UseFlag == "Y").OrderBy(p => p.DisplayOrder).ToList();
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

            // 20210629 오세완 차장 셋팅이 J05일지, 셋팅인 경우 기존설정을 보여주는 행위인지 결정이 되지 않아서 생략처리
            //if (lup_CheckPoint.EditValue.GetNullToEmpty() == "J05") //셋팅
            //{
            //    var checkPoint = lup_CheckPoint.EditValue.GetNullToEmpty();
            //    var TN_QCT1100_OldObj = QcModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo
            //                                                                        && p.WorkSeq == TEMP_XFPOP1000_Obj.ProcessSeq
            //                                                                        && p.CheckDivision == checkDivision
            //                                                                        && p.CheckPoint == checkPoint
            //                                                                    )
            //                                                                    .OrderBy(p => p.RowId)
            //                                                                    .LastOrDefault();
            //    if (TN_QCT1100_OldObj != null)
            //    {
            //        var List = TN_QCT1100_OldObj.TN_QCT1101List.ToList();
            //        if (List.Count > 0)
            //        {
            //            foreach (var v in qualityList)
            //            {
            //                var Obj = List.Where(p => p.RevNo == v.RevNo && p.ItemCode == v.ItemCode && p.Seq == v.Seq).FirstOrDefault();
            //                if (Obj != null)
            //                {
            //                    v.Reading1 = Obj.Reading1;
            //                    v.Reading2 = Obj.Reading2;
            //                    v.Reading3 = Obj.Reading3;
            //                    v.Reading4 = Obj.Reading4;
            //                    v.Reading5 = Obj.Reading5;
            //                    v.Reading6 = Obj.Reading6;
            //                    v.Reading7 = Obj.Reading7;
            //                    v.Reading8 = Obj.Reading8;
            //                    v.Reading9 = Obj.Reading9;
            //                    v.Judge = Obj.Judge;
            //                }
            //            }
            //        }
            //    }
            //}

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
            lup_CheckPoint.ReadOnly = !editFlag;
        }

        /// <summary>
        /// 치수검사 체크
        /// </summary>
        private void CheckInput(TN_QCT1001 obj)
        {
            var checkSpec = obj.CheckSpec.GetDecimalNullToNull();
            if (checkSpec == null)
                return;

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

            // 20210629 오세완 차장 계산 로직 완전 수정
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
                reading1 = reading1.ToUpper(); // 20210603 오세완 차장 대문자 변화가 필요해보여서 추가
                if (reading1 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading2 != null)
            {
                reading2 = reading2.ToUpper(); // 20210603 오세완 차장 대문자 변화가 필요해보여서 추가
                if (reading2 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading3 != null)
            {
                reading3 = reading3.ToUpper(); // 20210603 오세완 차장 대문자 변화가 필요해보여서 추가
                if (reading3 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading4 != null)
            {
                reading4 = reading4.ToUpper(); // 20210603 오세완 차장 대문자 변화가 필요해보여서 추가
                if (reading4 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading5 != null)
            {
                reading5 = reading5.ToUpper(); // 20210603 오세완 차장 대문자 변화가 필요해보여서 추가
                if (reading5 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading6 != null)
            {
                reading6 = reading6.ToUpper(); // 20210603 오세완 차장 대문자 변화가 필요해보여서 추가
                if (reading6 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading7 != null)
            {
                reading7 = reading7.ToUpper(); // 20210603 오세완 차장 대문자 변화가 필요해보여서 추가
                if (reading7 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading8 != null)
            {
                reading8 = reading8.ToUpper(); // 20210603 오세완 차장 대문자 변화가 필요해보여서 추가
                if (reading8 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading9 != null)
            {
                reading9 = reading9.ToUpper(); // 20210603 오세완 차장 대문자 변화가 필요해보여서 추가
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
                Search(MasterCodeSTR.Inspection_RestartTO);
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

            // 20210629 오세완 차장 기존에 있던 로직 전면 수정
            TN_QCT1001 detailObj = GridBindingSource.Current as TN_QCT1001;
            if (detailObj == null) return;

            decimal maxReading = detailObj.MaxReading.GetDecimalNullToZero();
            if (maxReading == 0)
                maxReading = 9;

            if (gv.FocusedColumn.FieldName.Contains("Reading") && !gv.FocusedColumn.FieldName.Contains("MaxReading"))
            {
                if (gv.FocusedColumn.FieldName.Right(1).GetDecimalNullToNull() > maxReading )
                    e.Cancel = true;
            }
        }
    }
}
