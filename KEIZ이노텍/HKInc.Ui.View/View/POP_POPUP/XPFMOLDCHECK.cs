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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.XtraReports.UI;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using DevExpress.XtraLayout;
using DevExpress.Utils;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Common;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 20210603 오세완 차장
    /// 금형일상점검 팝업 창
    /// </summary>
    public partial class XPFMOLDCHECK : Service.Base.ListFormTemplate
    {
        #region 전역변수
        RepositoryItemSpinEdit repositorySpinEdit = new RepositoryItemSpinEdit();
        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;
        IService<TN_MOLD1500> ModelService = (IService<TN_MOLD1500>)ProductionFactory.GetDomainService("TN_MOLD1500");
        #endregion

        public XPFMOLDCHECK()
        {
            InitializeComponent();

            this.Text = LabelConvert.GetLabelText("MoldDailyCheck");

            this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            this.Size = new Size(this.Size.Width, this.Size.Height - 40);

            gridEx1.ViewType = Utils.Enum.GridViewType.POP_GridView;
            gridEx1.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            gridEx1.MainGrid.MainView.RowCellClick += MainView_RowCellClick;

            btn_Save.Click += Btn_Save_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
            tx_Mold.EditValueChanged += Tx_Mold_EditValueChanged;
            tx_Mold.Click += Tx_Mold_Click;
            tx_Mold.KeyDown += Tx_Mold_KeyDown;

            pic_CheckPointImage.DoubleClick += Pic_CheckPointImage_DoubleClick;
        }

        /// <summary>
        /// 20210618 오세완 차장 key event 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tx_Mold_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string sMoldcode = tx_Mold.EditValue.GetNullToEmpty();
                Search(sMoldcode);
            }
        }

        /// <summary>
        /// 20210604 오세완 차장
        /// 금혐일상점검 점검부위 사진 교체
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            TEMP_MOLDCHECK_DAILY tObj = GridBindingSource.Current as TEMP_MOLDCHECK_DAILY;
            if (tObj == null)
                return;

            pic_CheckPointImage.EditValue = null;
            if (tObj.PicUrl == null)
                return;
            else if (tObj.PicUrl != "")
            {
                string sTotalUrl = GlobalVariable.HTTP_SERVER + tObj.PicUrl;
                pic_CheckPointImage.EditValue = FileHandler.FtpToByte(sTotalUrl);
            }
        }

        /// <summary>
        /// 20210604 오세완 차장 
        /// 금형 바코드 수기입력할 수 있게 키패드 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tx_Mold_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad)
                return;

            var vKeypad = new XFCKEYPAD();
            if (vKeypad.ShowDialog() != DialogResult.Cancel)
            {
                tx_Mold.EditValue = vKeypad.returnval;
                Search(tx_Mold.EditValue.ToString());
            }
        }

        private void Tx_Mold_EditValueChanged(object sender, EventArgs e)
        {
            var edit = sender as TextEdit;
            var value = edit.EditValue.GetNullToEmpty();
            if (value.IsNullOrEmpty())
            {
                lup_CheckId.EditValue = null;
                pic_CheckPointImage.EditValue = null;
            }

            Search(value);
        }

        private void Pic_CheckPointImage_DoubleClick(object sender, EventArgs e)
        {
            if (pic_CheckPointImage.EditValue == null) return;
            var imgForm = new POP_POPUP.XPFPOPIMG(LabelConvert.GetLabelText("CheckPointImage"), pic_CheckPointImage.EditValue);
            imgForm.ShowDialog();
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            var userObj = ModelService.GetChildList<User>(p => p.LoginId == GlobalVariable.LoginId).FirstOrDefault();
        
            lup_CheckId.SetDefaultPOP(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_CheckId.SetFontSize(new Font("맑은 고딕", 12f));

            lup_CheckId.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            repositorySpinEdit.Buttons[0].Visible = false;
            repositorySpinEdit.Click += RepositorySpinEdit_Click;

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
            repositoryItemGridLookUpEdit.DataSource = DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckOX).ToList();

            repositoryItemGridLookUpEdit.View.Appearance.Row.Font = new Font("맑은 고딕", 12f);
            DevExpress.XtraGrid.Columns.GridColumn col1 = repositoryItemGridLookUpEdit.View.Columns.AddField(repositoryItemGridLookUpEdit.DisplayMember);
            col1.VisibleIndex = 0;

            repositoryItemGridLookUpEdit.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton() { Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete });
            repositoryItemGridLookUpEdit.ButtonPressed += Lookup_ButtonPressed;

            repositoryItemTextEdit = new RepositoryItemTextEdit();
            repositoryItemTextEdit.Appearance.TextOptions.HAlignment = HorzAlignment.Default;

            lcCheckId.AppearanceItemCaption.ForeColor = Color.Red;
        }

        private void Lookup_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            if (edit == null) return;

            if (e.Button.Kind == ButtonPredefines.Delete)
                edit.EditValue = null;
        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("CheckPosition", LabelConvert.GetLabelText("CheckPosition"));
            gridEx1.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("CheckList"));
            gridEx1.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("CheckWay"));
            gridEx1.MainGrid.AddColumn("Temp", LabelConvert.GetLabelText("EyeCheckFlag"), false);
            gridEx1.MainGrid.AddColumn("CheckCycle", LabelConvert.GetLabelText("CheckCycle"));

            gridEx1.MainGrid.AddColumn("CheckStandardDate", LabelConvert.GetLabelText("CheckStandardDate"));
            gridEx1.MainGrid.AddColumn("ManagementStandard", LabelConvert.GetLabelText("ManagementStandard"));
            gridEx1.MainGrid.AddColumn("LastCheckDate", LabelConvert.GetLabelText("LastCheckDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            gridEx1.MainGrid.AddColumn("CheckValue", LabelConvert.GetLabelText("Check2"), HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("CheckId", LabelConvert.GetLabelText("LastCheckId"), HorzAlignment.Center, true);

            gridEx1.MainGrid.AddColumn("PicUrl", "점검부위 사진경로", false);

            gridEx1.MainGrid.SetEditable(true, "CheckValue");
            gridEx1.MainGrid.Columns["CheckValue"].MinWidth = 100;
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("Temp", "N");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPosition", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckPosition), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckList), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckWay), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckCycle), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckStandardDate", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckStandardDate), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName");
            gridEx1.MainGrid.MainView.Columns["ManagementStandard"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(gridEx1, "ManagementStandard", false, 200);

            gridEx1.BestFitColumns();
        }

        protected override void GridRowDoubleClicked(){}

        private void Search(string moldMCode)
        {
            gridEx1.MainGrid.Clear();
            ModelService.ReLoad();

            if (moldMCode.IsNullOrEmpty())
                return;

            using (var context = new Model.Context.ProductionContext(Utils.Common.ServerInfo.GetConnectString(Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var MoldMcode = new SqlParameter("@MOLD_MCODE", moldMCode);
                var CheckDate = new SqlParameter("@DATE", DateTime.Today.ToString("yyyy-MM-dd"));
                var vResult = context.Database.SqlQuery<TEMP_MOLDCHECK_DAILY>("SP_GET_MOLDCHECK_DAILY @MOLD_MCODE, @DATE", MoldMcode, CheckDate).ToList();
                if(vResult != null)
                {
                    if(vResult.Count > 0)
                    {
                        bool bNothing_Checklist = false;
                        foreach(TEMP_MOLDCHECK_DAILY each in vResult)
                        {
                            if(each.CheckList.GetNullToEmpty() == "")
                            {
                                bNothing_Checklist = true;
                                break;
                            }
                        }

                        if(bNothing_Checklist)
                        {
                            // 20210701 오세완 차장 점검항목이 없는 경우 입력을 못하게 처리한다. 
                            //MessageBoxHandler.Show("점검항목이 없어서 일상점검을 진행할 수 없습니다.");
                            MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_166));
                            return;
                        }
                    }

                    GridBindingSource.DataSource = vResult;

                    if(vResult.Count > 0)
                    {
                        // 20210618 오세완 차장 처음 선택된 녀석을 간주하여 이미지를 출력처리
                        TEMP_MOLDCHECK_DAILY each_Top = vResult.FirstOrDefault();
                        pic_CheckPointImage.EditValue = null;
                        if (each_Top.PicUrl == null)
                            return;
                        else if (each_Top.PicUrl != "")
                        {
                            string sTotalUrl = GlobalVariable.HTTP_SERVER + each_Top.PicUrl;
                            pic_CheckPointImage.EditValue = FileHandler.FtpToByte(sTotalUrl);
                        }
                    }
                }
                else
                {
                    // 20210618 오세완 차장 조회 안되면 바로 이미지 날려버리기
                    pic_CheckPointImage.EditValue = null;
                }
            }

            gridEx1.DataSource = GridBindingSource;
            gridEx1.BestFitColumns();
        }

        /// <summary>
        /// 저장 클릭 이벤트
        /// </summary>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            var moldmcode = tx_Mold.EditValue;
            bool bCheck = false;
            if (moldmcode == null)
                bCheck = true;
            else if (moldmcode.ToString() == "")
                bCheck = true;

            string sMessage = "";
            if (bCheck)
            {
                sMessage = string.Format(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_116), LabelConvert.GetLabelText("MoldMCode"));
                MessageBoxHandler.Show(sMessage); // 20210702 오세완 차장 메시지 교체
                //MessageBoxHandler.Show("금형관리코드 바코드 리딩은 필수입니다.", "금형관리코드");
                return;
            }

            var checkId = lup_CheckId.EditValue;
            if (checkId == null)
                bCheck = true;
            else if (checkId.ToString() == "")
                bCheck = true;

            if (bCheck)
            {
                sMessage = string.Format(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_84), LabelConvert.GetLabelText("CheckId"));
                MessageBoxHandler.Show(sMessage); // 20210702 오세완 차장 메시지 교체
                //MessageBoxHandler.Show("점검자 선택은 필수입니다.", "점검자");
                return;
            }

            if (GridBindingSource == null || GridBindingSource.DataSource == null) return;

            var list = GridBindingSource.List as List<TEMP_MOLDCHECK_DAILY>;
            if (list == null || list.Count == 0) return;

            int iCnt_Moldmcode_error = 0;
            int iCnt_Checkvalue_error = 0;
            foreach (var v in list)
            {
                if (v.MoldMcode != moldmcode.ToString())
                    iCnt_Moldmcode_error++;

                if (v.CheckValue == null)
                    iCnt_Checkvalue_error++;
                else if (v.CheckValue == "")
                    iCnt_Checkvalue_error++;
            }

            if (iCnt_Checkvalue_error > 0 || iCnt_Moldmcode_error > 0)
                bCheck = true;

            if (bCheck)
            {
                sMessage = HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_166);
                MessageBoxHandler.Show(sMessage, LabelConvert.GetLabelText("Confirm")); // 20210702 오세완 차장 메시지 교체
                //MessageBoxHandler.Show("점검값에 오류가 있습니다.", "확인");
                return;
            }

            List<TN_MOLD1500> mold1500List = ModelService.GetChildList<TN_MOLD1500>(p => p.MoldMCode.Equals(moldmcode.ToString())).ToList();
            foreach (var v in list)
            {
                DateTime dtNow = DateTime.Today;
                string sCheckValue = string.Empty;
                if (v.CheckWay == "01")
                {
                    sCheckValue = v.CheckValue; 
                }
                else
                {
                    List<TN_STD1000> tempList = DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckOX, 1);
                    var tempObj = (from c in tempList where c.CodeName.Equals(v.CheckValue) select c).ToList().FirstOrDefault();
                    if (tempObj != null)
                    {
                        sCheckValue = tempObj.CodeVal;
                    }
                }

                if (v.CheckDate == dtNow)
                {
                    //Update
                    var updateObj = mold1500List.Where(p => p.Seq == v.CheckSeq).FirstOrDefault();
                    if(updateObj != null)
                    {
                        // 20210701 오세완 차장 KEY라서 바꾸면 오류
                        //updateObj.CheckDate = dtNow;
                        updateObj.CheckId = checkId.ToString();
                        updateObj.CheckValue = sCheckValue;
                        ModelService.Update(updateObj);
                    }
                }
                else
                {
                    //Insert
                    var newObj = new TN_MOLD1500();
                    newObj.MoldMCode = v.MoldMcode;
                    newObj.Seq = v.CheckSeq;
                    newObj.CheckDate = dtNow;
                    newObj.CheckId = checkId.ToString();
                    newObj.CheckValue = sCheckValue;
                    ModelService.Insert(newObj);
                }
            }

            ModelService.Save();
            DialogResult = DialogResult.OK;
            IsFormControlChanged = false;
            ActClose();
        }

        /// <summary>
        /// 취소 클릭 이벤트
        /// </summary>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            IsFormControlChanged = false;
            ActClose();
        }

        private void MainView_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("CheckValue"))
                {
                    var checkWay = view.GetRowCellValue(e.RowHandle, view.Columns["CheckWay"]).GetNullToEmpty();
                    if (checkWay == null)
                        return;
                    else if (checkWay == "")
                        return;
                    else
                    {
                        if(checkWay == "01")
                            e.RepositoryItem = repositorySpinEdit; // 치수검사
                        else
                            e.RepositoryItem = repositoryItemGridLookUpEdit; // 육안검사
                    }
                }
            }
        }
        
        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "CheckValue")
                {
                    var checkPosition = view.GetRowCellValue(e.RowHandle, view.Columns["CheckPosition"]).GetNullToEmpty();
                    if (checkPosition == MasterCodeSTR.MachineCheckPosition_ConfirmFlag)
                        e.Appearance.BackColor = Color.Empty;
                    else
                        e.Appearance.BackColor = GlobalVariable.GridEditableColumnColor;
                }
            }
        }

        private void RepositorySpinEdit_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.KeyPad) return;
            var spinEidt = sender as SpinEdit;
            var keyPad = new XFCNUMPAD();
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                spinEidt.EditValue = keyPad.returnval;
            }
        }

    }
}
