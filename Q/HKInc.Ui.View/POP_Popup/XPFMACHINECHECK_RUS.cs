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
using HKInc.Utils.Common;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

namespace HKInc.Ui.View.POP_POPUP
{
    /// <summary>
    /// 설비점검 팝업 창
    /// </summary>
    public partial class XPFMACHINECHECK_RUS : Service.Base.ListFormTemplate
    {
        RepositoryItemSpinEdit repositorySpinEdit = new RepositoryItemSpinEdit();
        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;

        IService<TN_MEA1003> ModelService = (IService<TN_MEA1003>)ProductionFactory.GetDomainService("TN_MEA1003");

        public XPFMACHINECHECK_RUS()
        {
            InitializeComponent();

            this.Text = LabelConvert.GetLabelText("MachineCheck");

            this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            this.Size = new Size(this.Size.Width, this.Size.Height - 40);

            gridEx1.ViewType = Utils.Enum.GridViewType.POP_GridView;
            gridEx1.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            gridEx1.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;

            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;

            btn_Save.Click += Btn_Save_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

            // 20220313 오세완 차장 화면설계서에 이미지가 없어서 일단 제외처리 
            //pic_CheckPointImage.DoubleClick += Pic_CheckPointImage_DoubleClick;
        }

        private void Pic_CheckPointImage_DoubleClick(object sender, EventArgs e)
        {
            if (pic_CheckPointImage.EditValue == null) return;
            //var imgForm = new POP_POPUP.XPFPOPIMG(LabelConvert.GetLabelText("CheckPointImage"), pic_CheckPointImage.EditValue);
            //imgForm.ShowDialog();
        }

        private void Lup_CheckId_Popup(object sender, EventArgs e)
        {
            var machineValue = lup_Machine.EditValue.GetNullToEmpty();
            if (machineValue.IsNullOrEmpty())
            {
                lup_CheckId.Properties.View.ActiveFilter.NonColumnFilter = "[Active] = '" + "Y" + "'";
            }
            else
            {
                var machineObj = lup_Machine.GetSelectedDataRow() as TN_MEA1000;
                lup_CheckId.Properties.View.ActiveFilter.NonColumnFilter = "[Active] = '" + "Y" + "'";
            }
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            lup_Machine.SetDefault(false, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.DailyCheckFlag == "Y" && 
                                                                                                                    p.UseYn == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            

            lup_Machine.SetFontSize(new Font("맑은 고딕", 12f));
            //lup_CheckId.SetDefaultPOP(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.DepartmentCode == "DEPT-00003" && 
            lup_CheckId.SetDefaultPOP(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor); // 20220510 오세완 차장 부서가 걸려있었는데 일단 제거 
            lup_CheckId.SetFontSize(new Font("맑은 고딕", 12f));

            lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_CheckId.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            repositorySpinEdit.Buttons[0].Visible = false;
            repositorySpinEdit.Click += RepositorySpinEdit_Click;

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
            repositoryItemGridLookUpEdit.DataSource = DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckOX).ToList();

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
            gridEx1.MainGrid.AddColumn("CheckPosition", "инспекционная позиция");                   // 점검위치
            gridEx1.MainGrid.AddColumn("CheckList", "Список проверок");                                   // 점검항목
            gridEx1.MainGrid.AddColumn("CheckWay", "метод проверки");                                      // 점검방법
            gridEx1.MainGrid.AddColumn("Temp", false);
            gridEx1.MainGrid.AddColumn("CheckCycle", "цикл проверки");                                      // 점검주기
            gridEx1.MainGrid.AddColumn("CheckStandardDate", "дата осмотра");                                 // 점검기준일
            gridEx1.MainGrid.AddColumn("ManagementStandard", "стандарты управления");                // 관리기준
            gridEx1.MainGrid.AddColumn("LastCheckDate", "Дата последней проверки", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");    // 최종점검일
            gridEx1.MainGrid.AddColumn("CheckValue", "осмотрен", HorzAlignment.Center, true);                   // 점검
            gridEx1.MainGrid.AddColumn("Temp2", "последний инспекционный работник", HorzAlignment.Center, true);            // 최종점검자
            gridEx1.MainGrid.AddColumn("CheckSeq", false); // 20220510 오세완 차장 key값이라 있어야 함
            gridEx1.MainGrid.SetEditable(true, "CheckValue");
            gridEx1.MainGrid.Columns["CheckValue"].MinWidth = 100;
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("Temp", "N");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPosition", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckPosition), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckList), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckWay), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckCycle), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckStandardDate", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckStandardDate), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp2", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName");
            gridEx1.MainGrid.MainView.Columns["ManagementStandard"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, false, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            gridEx1.BestFitColumns();
        }

        protected override void GridRowDoubleClicked(){}

        /// <summary>
        /// 설비 변경 이벤트
        /// </summary>
        private void Lup_Machine_EditValueChanged(object sender, EventArgs e)
        {
            var edit = sender as SearchLookUpEdit;
            var value = edit.EditValue.GetNullToEmpty();

            lup_CheckId.EditValue = null;
            pic_CheckPointImage.EditValue = null;
            Search(value);
        }

        private void Search(string machineCode)
        {
            gridEx1.MainGrid.Clear();
            
            ModelService.ReLoad();

            if (machineCode.IsNullOrEmpty())
                return;

            // 20220313 오세완 차장 비가동 설비 설비점검 여부 확인해 볼것
            //var TN_MEA1004 = ModelService.GetChildList<TN_MEA1004>(p => p.MachineCode == machineCode && p.StopEndTime == null).FirstOrDefault();
            //if (TN_MEA1004 != null)
            //{
            //    MessageBoxHandler.Show("해당 설비는 비가동상태입니다. 확인 부탁드립니다.");
            //    lup_Machine.EditValue = null;
            //    return;
            //}

            using (var context = new Model.Context.ProductionContext(Utils.Common.ServerInfo.GetConnectString(Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var MachineCode = new SqlParameter("@MachineCode", machineCode);
                var CheckDate = new SqlParameter("@CheckDate", DateTime.Today);
                GridBindingSource.DataSource = context.Database.SqlQuery<TP_XPFMACHINECHECK_LIST>("USP_GET_XPFMACHINECHECK_LIST @MachineCode,@CheckDate", MachineCode, CheckDate).OrderBy(p => p.DisplayOrder).ToList();

            }

            // 20220313 오세완 차장 이미지 출력 여부 확인해봐야 함
            //var TN_MEA1000 = lup_Machine.GetSelectedDataRow() as TN_MEA1000;
            //if (TN_MEA1000 != null)
            //{
            //    if (!TN_MEA1000.FileUrl2.IsNullOrEmpty())
            //        pic_CheckPointImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_MEA1000.FileUrl2);
            //}

            gridEx1.DataSource = GridBindingSource;
            gridEx1.BestFitColumns();
        }

        /// <summary>
        /// 저장 클릭 이벤트
        /// </summary>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if (GridBindingSource == null || GridBindingSource.DataSource == null)
                return;

            var list = GridBindingSource.List as List<TP_XPFMACHINECHECK_LIST>;
            if (list == null || list.Count == 0)
                return;

            var machineCode = lup_Machine.EditValue.GetNullToEmpty();
            string sMessage = "";
            bool bCheck = false;
            if (machineCode.IsNullOrEmpty())
            {
                sMessage = "Установка обязательна для заполнения.";         // 설비는 필수 입력항목입니다
                bCheck = true;
            }

            var checkId = lup_CheckId.EditValue.GetNullToEmpty();
            if (checkId.IsNullOrEmpty())
            {
                sMessage = "Инспектор - обязательная запись.";                     // 점검자는 필수 입력항목입니다
                bCheck = true;
            }

            if(bCheck)
            {
                MessageBoxHandler.Show(sMessage);
                return;
            }

            var TN_MEA1002List = ModelService.GetChildList<TN_MEA1002>(p => p.MachineCode == machineCode).ToList();

            foreach (var v in list)
            {
                if (v.MachineCode != machineCode)
                    return;

                if (v.CheckValue.IsNullOrEmpty())
                    continue;

                if (v.LastCheckDate == DateTime.Today)
                {
                    //Update
                    TN_MEA1003 update_Obj = TN_MEA1002List.Where(p => p.CheckSeq == v.CheckSeq).First().TN_MEA1003List.Where(p => p.CheckDate == DateTime.Today).Last();
                    if(update_Obj != null)
                    {
                        update_Obj.CheckDate = DateTime.Today;
                        update_Obj.CheckId = checkId;
                        update_Obj.CheckValue = v.CheckValue;
                        ModelService.Update(update_Obj);
                    }
                }
                else
                {
                    //Insert
                    TN_MEA1003 insert_Obj = new TN_MEA1003()
                    {
                        MachineCode = v.MachineCode,
                        CheckSeq = v.CheckSeq,
                        CheckDate = DateTime.Today,
                        CheckId = checkId,
                        CheckValue = v.CheckValue
                    };
                    ModelService.Insert(insert_Obj);

                    // 20220510 오세완 차장 괜히 foregin key를 의식해서 이렇게 저장하면 오류가 발생한다. 
                    //TN_MEA1002 insert_1002 = TN_MEA1002List.Where(p => p.CheckSeq == v.CheckSeq).FirstOrDefault();
                    //if (insert_1002 == null)
                    //    ModelService.Insert(insert_Obj);
                    //else
                    //{
                    //    insert_Obj.TN_MEA1002 = insert_1002;
                    //    insert_1002.TN_MEA1003List.Add(insert_Obj);
                    //    ModelService.InsertChild<TN_MEA1002>(insert_1002);
                    //}
                }
            }
            ModelService.Save();
            DialogResult = DialogResult.OK;
            IsFormControlChanged = false;
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
                    if (checkWay == MasterCodeSTR.MachineCheckWay_Memo)
                    {
                        //메모 입력
                        //e.Column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Default;
                        e.RepositoryItem = repositoryItemTextEdit;
                    }
                    else
                    {
                        var eyeCheckFlag = view.GetRowCellValue(e.RowHandle, view.Columns["Temp"]).GetNullToEmpty();
                        if (eyeCheckFlag == "N")
                        {
                            //치수 입력
                            e.RepositoryItem = repositorySpinEdit;
                        }
                        else
                        {
                            //O,X 입력
                            //e.Column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                            e.RepositoryItem = repositoryItemGridLookUpEdit;
                        }
                    }
                }
            }
        }
        
        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var obj = GridBindingSource.Current as TP_XPFMACHINECHECK_LIST;
            if (obj.CheckPosition == MasterCodeSTR.MachineCheckPosition_ConfirmFlag)
            {
                e.Cancel = true;
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
            keyPad.ShowDialog();
            spinEidt.EditValue = keyPad.returnval;
        }

    }
}
