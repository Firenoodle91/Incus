using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using DevExpress.XtraEditors.Controls;
using HKInc.Ui.Model.Domain;
using DevExpress.Utils;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;

namespace HKInc.Ui.View.POP_Popup
{
    /// <summary>
    /// POP 품질등록 팝업 창
    /// </summary>
    public partial class XPFINSPECTION_RUS : HKInc.Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_QCT1200> QcModelService = (IService<TN_QCT1200>)ProductionFactory.GetDomainService("TN_QCT1200");

        TP_XFPOP1000_V2_LIST TEMP_XFPOP1000_Obj;
        string ProductLotNo;
        int rowid;
        string qcType;

        bool Self_Check = false;
        bool FME_Check = false;
        bool Process_Check = false;

        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemSpinEdit repositoryItemSpinEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;
        #endregion

        public XPFINSPECTION_RUS(TP_XFPOP1000_V2_LIST obj, string productLotNo)
        {
            InitializeComponent();

            //this.Text = LabelConvert.GetLabelText("QualityAdd");
            this.Text = "Регистрация качества";         // 품질등록

            this.Size = new Size(this.Size.Width, this.Size.Height - 40);

            gridEx1.ViewType = GridViewType.POP_GridView;

            TEMP_XFPOP1000_Obj = obj;
            ProductLotNo = productLotNo;

            gridEx1.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            gridEx1.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;

            // 20220513 오세완 차장 최대시료수 체크 로직이 없어서 사용 안함 처리 
            //gridEx1.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            gridEx1.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;

            btn_InspFrequently.Click += Btn_InspFrequently_Click;
            btn_InspFME.Click += Btn_InspFME_Click;
            btn_InspProcess.Click += Btn_InspProcess_Click;
            btn_Save.Click += Btn_Save_Click;
            btn_InitRefresh.Click += Btn_InitRefresh_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

            pic_Limit.DoubleClick += Pic_Limit_DoubleClick;

            this.WindowState = FormWindowState.Maximized;
        }

        private void Pic_Limit_DoubleClick(object sender, EventArgs e)
        {
            // 20220315 오세완 차장 이미지 출력 맞는 지 확인해야 함 
            //if (pic_Limit.EditValue == null) return;
            //var imgForm = new POP_POPUP.XPFPOPIMG(LabelConvert.GetLabelText("ItemLimitImage"), pic_Limit.EditValue);
            //imgForm.ShowDialog();
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            lup_CheckId.SetDefaultPOP(false, "LoginId", "UserName", QcModelService.GetChildList<User>(p => p.Active == "Y").ToList(), TextEditStyles.DisableTextEditor);

            // 20220315 오세완 차장 기준을 입력하면 버튼을 출력하는 형태로, 내맘대로

            List<TN_QCT1000> qc_Arr = QcModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode && 
                                                                                   p.ProcessCode == TEMP_XFPOP1000_Obj.Process &&
                                                                                   p.UseYn == "Y");
            if(qc_Arr != null)
                if(qc_Arr.Count > 0)
                {
                    int iCount_Q03 = qc_Arr.Where(p => p.ProcessGu == MasterCodeSTR.QC_Process_FME).Count();
                    if(iCount_Q03 <= 0)
                        layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; // 20220315 오세완 차장 초중종검사 버튼 영역
                }

            layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            lc_FME_First.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lc_FME_Middle.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lc_FME_End.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            simpleSeparator2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            ControlSetting(false);
            
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
            repositoryItemGridLookUpEdit.DataSource = DbRequestHandler.GetCommCode(MasterCodeSTR.QCOKNG).ToList();
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

        private void RepositoryItemSpinEdit_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad)
                return;

            var spinEidt = sender as SpinEdit;
            var keyPad = new XFCNUMPAD();
            keyPad.ShowDialog();
            spinEidt.EditValue = keyPad.returnval;

            // 20220513 오세완 차장 값을 입력하고 포커스를 이동해야만 판정이 나와서 아예 자동으로 이동처리 추가
            gridEx1.MainGrid.MainView.FocusedColumn = gridEx1.MainGrid.MainView.Columns["CheckFlag"];
        }

        /// <summary>
        /// 20220513 오세완 차장 
        /// 값을 입력하고 포커스를 이동해야만 판정이 나와서 아예 자동으로 이동처리 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepositoryItemGridLookUpEdit_Closed(object sender, ClosedEventArgs e)
        {
            gridEx1.MainGrid.MainView.FocusedColumn = gridEx1.MainGrid.MainView.Columns["CheckFlag"];
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MainView.OptionsView.ShowIndicator = false;

            gridEx1.MainGrid.AddColumn("ProcessGu", "Тип проверки");                                  // 검사종류
            gridEx1.MainGrid.AddColumn("CheckName", "Пункты проверки");                            // 검사항목
            gridEx1.MainGrid.AddColumn("CheckProv", "Метод проверки");                              // 검사방법
            gridEx1.MainGrid.AddColumn("CheckStand", "стандартный");                                  // 규격
            gridEx1.MainGrid.AddColumn("UpQuad", "верхний предел", HorzAlignment.Center, true);     // 상한
            gridEx1.MainGrid.AddColumn("DownQuad", "Нижний предел", HorzAlignment.Center, true);     // 하한       
            gridEx1.MainGrid.AddColumn("X1", "X1", HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("X2", "X2", HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("X3", "X3", HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("X4", "X4", HorzAlignment.Center, true,70);

            gridEx1.MainGrid.AddColumn("X5", "X5", HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("X6", "X6", HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("X7", "X7", HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("X8", "X8", HorzAlignment.Center, true,70);
            gridEx1.MainGrid.AddColumn("CheckFlag", "определение");                                   // 판정

            gridEx1.MainGrid.SetEditable(true, "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8");
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 15f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessGu", DbRequestHandler.GetCommCode(MasterCodeSTR.QCKIND), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckName", DbRequestHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckProv", DbRequestHandler.GetCommCode(MasterCodeSTR.QCTYPE), "Mcode", "Codename");

            gridEx1.BestFitColumns();
        }

        protected override void GridRowDoubleClicked() { }

        /// <summary>
        /// 자주검사 클릭이벤트
        /// </summary>
        private void Btn_InspFrequently_Click(object sender, EventArgs e)
        {
            //Search(MasterCodeSTR.InspectionDivision_Frequently);
        }

        /// <summary>
        /// 초중종검사 클릭이벤트
        /// </summary>
        private void Btn_InspFME_Click(object sender, EventArgs e)
        {
            Search(MasterCodeSTR.FMECheckManagement);
        }

        /// <summary>
        /// 공정검사 클릭이벤트
        /// </summary>
        private void Btn_InspProcess_Click(object sender, EventArgs e)
        {
            //Search(MasterCodeSTR.InspectionDivision_Process);
        }

        /// <summary>
        /// 저장 클릭이벤트
        /// </summary>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            GridView gv = gridEx1.MainGrid.MainView as GridView;
            List<TN_QCT1000> qc_Arr = GridBindingSource.List as List<TN_QCT1000>;
            if (qc_Arr == null || qc_Arr.Count == 0) return;

            int iCnt_Check = qc_Arr.Where(p => p.CheckFlag.IsNullOrEmpty()).Count();
            if (qc_Arr.Count == iCnt_Check)
            {
                MessageBoxHandler.Show("Нет проведенных проверок");             // 진행한 검사가 없습니다
                return;
            }

            string sCheckId = lup_CheckId.EditValue.GetNullToEmpty();
            if (sCheckId.IsNullOrEmpty())
            {
                MessageBoxHandler.Show("Проверка обязательна");             // 검사자는 필수입니다
                return;
            }

            string sMasterCheckResult = qc_Arr.Any(p=>p.CheckFlag == "NG") ? "NG" : "OK";

            if (qcType == MasterCodeSTR.QC_Process_FME)
            {
                string sSql = "exec SP_QCSTEP_STEP '" + MasterCodeSTR.QC_Process_FME + "','" + TEMP_XFPOP1000_Obj.WorkDate.GetNullToEmpty() + "', '" + TEMP_XFPOP1000_Obj.WorkNo + "', '" + 
                    TEMP_XFPOP1000_Obj.Process + "', '" + DateTime.Today.ToString() + "' ";

                TN_QCT1200 insertObj = new TN_QCT1200()
                {
                    No = DbRequestHandler.GetRequestNumber(MasterCodeSTR.QC_Process_FME),
                    FmeNo = MasterCodeSTR.QC_Process_FME,
                    FmeDivision = DbRequestHandler.GetCellValue(sSql, 0),
                    WorkDate = TEMP_XFPOP1000_Obj.WorkDate,
                    WorkNo = TEMP_XFPOP1000_Obj.WorkNo,
                    ItemCode = TEMP_XFPOP1000_Obj.ItemCode,
                    ProcessCode = TEMP_XFPOP1000_Obj.Process,
                    CheckId = sCheckId,
                    CheckResult = sMasterCheckResult,
                    LotNo = ProductLotNo,
                    CheckDate = DateTime.Today
                };

                foreach(TN_QCT1000 each in qc_Arr)
                {
                    TN_QCT1201 insertObj_Det = new TN_QCT1201()
                    {
                        No = insertObj.No,
                        Seq = insertObj.QCT1201List.Count == 0 ? 1 : insertObj.QCT1201List.Count + 1,
                        FmeNo = insertObj.FmeNo,
                        FmeDivision = insertObj.FmeDivision,
                        ItemCode = insertObj.ItemCode,
                        CheckName = each.CheckName,
                        CheckProv = each.CheckProv,
                        CheckStand = each.CheckStand,
                        UpQuad = each.UpQuad,
                        DownQuad = each.DownQuad,
                        Reading1 = each.X1,
                        Reading2 = each.X2,
                        Reading3 = each.X3,
                        Reading4 = each.X4,
                        Reading5 = each.X5,
                        Reading6 = each.X6,
                        Reading7 = each.X7,
                        Reading8 = each.X8,
                        Judge = each.CheckFlag
                    };

                    insertObj.QCT1201List.Add(insertObj_Det);
                }

                QcModelService.Insert(insertObj);
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
            gridEx1.MainGrid.Clear();

            qcType = null;
            rowid = 0;
            lup_CheckId.EditValue = null;

            List<TN_QCT1000> qc_Arr = QcModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode && 
                                                                                   (p.ProcessGu == checkDivision || p.ProcessGu == MasterCodeSTR.QC_Process_100) &&
                                                                                   p.ProcessCode == TEMP_XFPOP1000_Obj.Process &&
                                                                                   p.UseYn == "Y").OrderBy(o => o.Seq).ToList();

            bool bNotFound = false;
            if (qc_Arr == null)
                bNotFound = true;
            else if (qc_Arr.Count == 0)
                bNotFound = true;

            if (bNotFound)
            {
                MessageBoxHandler.Show("Проверка не существует");           // 검사항목이 존재하지 않습니다
                Btn_InitRefresh_Click(null, null);
            }
            else
            {
                ControlSetting(true);

                GridBindingSource.DataSource = qc_Arr;
                gridEx1.DataSource = GridBindingSource;
                gridEx1.MainGrid.BestFitColumns();

                qcType = checkDivision;
                rowid = 0;
            }
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var obj = GridBindingSource.Current as TN_QCT1001;
            if (obj == null) return;

            rowid = e.FocusedRowHandle;

            // 20220316 오세오나 차장 image출력 확인
            // 20220512 오세완 차장 품목한도 이력관리가 없음으로 출력안함
            //var TN_STD1104 = obj.TN_QCT1000.TN_STD1100.TN_STD1104List.Where(p => p.CheckList == obj.CheckList).OrderBy(p => p.Seq).LastOrDefault();
            //if (TN_STD1104 == null || TN_STD1104.FileUrl.IsNullOrEmpty())
            //    pic_Limit.EditValue = null;
            //else
            //    pic_Limit.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1104.FileUrl);
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var view = sender as GridView;

            var obj = GridBindingSource.Current as TN_QCT1000;
            if (obj == null)
                return;

            if (e.Column.FieldName.Contains("X"))
            {
                var checkDataType = obj.CheckProv.GetNullToEmpty();
                if (checkDataType == MasterCodeSTR.VisualInspection)
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
                if (e.Column.FieldName.Contains("X"))
                {
                    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckProv").GetNullToEmpty();
                    if (checkDataType == MasterCodeSTR.VisualInspection)
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
                else if (e.Column.FieldName == "CheckFlag")
                {
                    var judgeValue = view.GetRowCellValue(e.RowHandle, "CheckFlag").GetNullToEmpty();
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
        }

        /// <summary>
        /// 치수검사 체크
        /// </summary>
        private void CheckInput(TN_QCT1000 obj)
        {
            decimal dCheckSpec;
            string sCheckSpec = obj.CheckStand.GetNullToEmpty();
            decimal.TryParse(sCheckSpec, out dCheckSpec);

            if (sCheckSpec == "")
                return;

            decimal dCheckUpQuad = obj.UpQuad.GetDecimalNullToZero();
            decimal dCheckDownQuad = obj.DownQuad.GetDecimalNullToZero();
            decimal dCheckUp = dCheckSpec + dCheckUpQuad;
            decimal dCheckDown = dCheckSpec - dCheckDownQuad;

            int NgQty = 0;
            int OkQty = 0;

            // 20220513 오세완 차장 큐백은 최대시료수가 없어서 입력한 대로 확인하여 처리해야 해서 추가 
            string sReading1 = obj.X1.GetNullToEmpty();
            string sReading2 = obj.X2.GetNullToEmpty();
            string sReading3 = obj.X3.GetNullToEmpty();
            string sReading4 = obj.X4.GetNullToEmpty();
            string sReading5 = obj.X5.GetNullToEmpty();
            string sReading6 = obj.X6.GetNullToEmpty();
            string sReading7 = obj.X7.GetNullToEmpty();
            string sReading8 = obj.X8.GetNullToEmpty();

            decimal dReading1 = obj.X1.GetDecimalNullToZero();
            decimal dReading2 = obj.X2.GetDecimalNullToZero();
            decimal dReading3 = obj.X3.GetDecimalNullToZero();
            decimal dReading4 = obj.X4.GetDecimalNullToZero();
            decimal dReading5 = obj.X5.GetDecimalNullToZero();
            decimal dReading6 = obj.X6.GetDecimalNullToZero();
            decimal dReading7 = obj.X7.GetDecimalNullToZero();
            decimal dReading8 = obj.X8.GetDecimalNullToZero();

            if(sReading1 != "")
                if (dReading1 >= dCheckDown && dReading1 <= dCheckUp)
                    OkQty++;
                else
                    NgQty++;

            if (sReading2 != "")
                if (dReading2 >= dCheckDown && dReading2 <= dCheckUp)
                    OkQty++;
                else
                    NgQty++;

            if (sReading3 != "")
                if (dReading3 >= dCheckDown && dReading3 <= dCheckUp)
                    OkQty++;
                else
                    NgQty++;

            if (sReading4 != "")
                if (dReading4 >= dCheckDown && dReading4 <= dCheckUp)
                    OkQty++;
                else
                    NgQty++;

            if (sReading5 != "")
                if (dReading5 >= dCheckDown && dReading5 <= dCheckUp)
                    OkQty++;
                else
                    NgQty++;

            if (sReading6 != "")
                if (dReading6 >= dCheckDown && dReading6 <= dCheckUp)
                    OkQty++;
                else
                    NgQty++;

            if (sReading7 != "")
                if (dReading7 >= dCheckDown && dReading7 <= dCheckUp)
                    OkQty++;
                else
                    NgQty++;

            if (sReading8 != "")
                if (dReading8 >= dCheckDown && dReading8 <= dCheckUp)
                    OkQty++;
                else
                    NgQty++;

            if (NgQty == 0 && OkQty == 0)
            {
                obj.CheckFlag = null;
            }
            else if (NgQty > 0)
            {
                obj.CheckFlag = "NG";
            }
            else
            {
                obj.CheckFlag = "OK";
            }

            gridEx1.BestFitColumns();
        }

        /// <summary>
        /// 육안검사 체크
        /// </summary>
        /// <param name="detailObj"></param>
        private void CheckEye(TN_QCT1000 obj)
        {
            int NgQty = 0;
            int OkQty = 0;

            string sX1 = obj.X1.GetNullToEmpty();
            string sX2 = obj.X2.GetNullToEmpty();
            string sX3 = obj.X3.GetNullToEmpty();
            string sX4 = obj.X4.GetNullToEmpty();
            string sX5 = obj.X5.GetNullToEmpty();
            string sX6 = obj.X6.GetNullToEmpty();
            string sX7 = obj.X7.GetNullToEmpty();
            string sX8 = obj.X8.GetNullToEmpty();

            if (sX1 != "")
            {
                if (sX1 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (sX2 != "")
            {
                if (sX2 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (sX3 != "")
            {
                if (sX3 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (sX4 != "")
            {
                if (sX4 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (sX5 != "")
            {
                if (sX5 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (sX6 != "")
            {
                if (sX6 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (sX7 != "")
            {
                if (sX7 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (sX8 != "")
            {
                if (sX8 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (NgQty == 0 && OkQty == 0)
            {
                obj.CheckFlag = "";
            }
            else if (NgQty > 0)
            {
                obj.CheckFlag = "NG";
            }
            else
            {
                obj.CheckFlag = "OK";
            }

            gridEx1.BestFitColumns();
        }

        private Color DetailCheckInputColor(decimal? checkSpec, decimal checkUpQuad, decimal checkDownQuad, decimal? readingValue)
        {
            if (checkSpec == null)
                return Color.Black;
            else
            {
                decimal dCheckUp = checkSpec.GetDecimalNullToZero() + checkUpQuad;
                decimal dCheckDown = checkSpec.GetDecimalNullToZero() - checkDownQuad;

                if (readingValue != null)
                {
                    if (readingValue >= dCheckDown && readingValue <= dCheckUp)
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

        private void MainView_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("X") )
                {
                    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckProv").GetNullToEmpty();
                    if (checkDataType == MasterCodeSTR.VisualInspection)
                    {
                        //육안검사 
                        e.RepositoryItem = repositoryItemGridLookUpEdit;
                    }
                    else
                    {
                        // 20220513 오세완 차장 이게 없어야 제대로 숫자가 보인다. 그리고 검사타입이 육안하고 수치밖에 없어서 이게 가능 
                        //if (!string.IsNullOrEmpty(checkDataType))
                        //    repositoryItemSpinEdit.Mask.EditMask = string.Format("N{0}", checkDataType);

                        e.RepositoryItem = repositoryItemSpinEdit;
                    }
                }
            }
        }

        /// <summary>
        /// 20220513 오세완 차장
        /// 최대시료수 체크 로직이 없어도 되어서 사용 안함 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView gv = sender as GridView;

            TN_QCT1000 detailObj = GridBindingSource.Current as TN_QCT1000;
            if (detailObj == null)
                return;

            // 20220317 오세완 차장 최대 시료수를 확인하여 그 이상 입력하면 못넘어가게 막는 로직인데 큐맥에는 그게 필요 없어서 생략처리. 
            //var maxReading = detailObj.TN_QCT1001.MaxReading.GetNullToEmpty();
            //if (gv.FocusedColumn.FieldName.Contains("Reading") && !gv.FocusedColumn.FieldName.Contains("MaxReading"))
            //{
            //    if (gv.FocusedColumn.FieldName.Right(1).GetDecimalNullToNull() > maxReading.GetDecimalNullToNull())
            //        e.Cancel = true;
            //}
        }
    }
}
