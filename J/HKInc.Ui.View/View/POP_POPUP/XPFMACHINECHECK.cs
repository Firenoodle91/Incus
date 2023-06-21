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
    /// 설비점검 팝업 창
    /// </summary>
    public partial class XPFMACHINECHECK : Service.Base.ListFormTemplate
    {
        RepositoryItemSpinEdit repositorySpinEdit = new RepositoryItemSpinEdit();
        //RepositoryItemCheckEdit repositoryCheckEdit = new RepositoryItemCheckEdit() { ValueChecked = "Y", ValueUnchecked = "N" };
        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;

        IService<TN_MEA1003> ModelService = (IService<TN_MEA1003>)ProductionFactory.GetDomainService("TN_MEA1003");

        public XPFMACHINECHECK()
        {
            InitializeComponent();

            this.Text = LabelConvert.GetLabelText("MachineCheck");

            this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            this.Size = new Size(this.Size.Width, this.Size.Height - 40);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);

            gridEx1.ViewType = Utils.Enum.GridViewType.POP_GridView;
            //gridEx1.MainGrid.MainView.CustomRowCellEdit += MainView_CustomRowCellEdit;
            gridEx1.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            gridEx1.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;

            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;
            //lup_CheckId.Popup += Lup_CheckId_Popup;

            btn_Save.Click += Btn_Save_Click;
            btn_Cancel.Click += Btn_Cancel_Click;

            pic_CheckPointImage.DoubleClick += Pic_CheckPointImage_DoubleClick;
        }

        private void Pic_CheckPointImage_DoubleClick(object sender, EventArgs e)
        {
            if (pic_CheckPointImage.EditValue == null) return;
            var imgForm = new POP_POPUP.XPFPOPIMG(LabelConvert.GetLabelText("CheckPointImage"), pic_CheckPointImage.EditValue);
            imgForm.ShowDialog();
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
                //if (machineObj.ProcTeamCode.IsNullOrEmpty())
                //{
                    lup_CheckId.Properties.View.ActiveFilter.NonColumnFilter = "[Active] = '" + "Y" + "'";
                //}
                //else
                //{
                //    lup_CheckId.Properties.View.ActiveFilter.NonColumnFilter = "[ProductTeamCode] = '" + machineObj.ProcTeamCode + "'";
                //}
            }
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            var userObj = ModelService.GetChildList<User>(p => p.LoginId == GlobalVariable.LoginId).FirstOrDefault();
            if (userObj == null) 
                lup_Machine.SetDefault(false, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.DailyCheckFlag == "Y" && p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            else
                //   lup_Machine.SetDefault(false, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.DailyCheckFlag == "Y" && p.UseFlag == "Y" && p.ProcTeamCode == userObj.ProductTeamCode).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
                lup_Machine.SetDefault(false, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.DailyCheckFlag == "Y" && p.UseFlag == "Y" ).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);

            lup_Machine.SetFontSize(new Font("맑은 고딕", 12f));
            lup_CheckId.SetDefaultPOP(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.DepartmentCode == "DEPT-00003"
                                                                       && p.Active == "Y"

                                                                   )
                                                                   .ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_CheckId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_CheckId.SetFontSize(new Font("맑은 고딕", 12f));

            lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_CheckId.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            repositorySpinEdit.Buttons[0].Visible = false;
            //repositoryCheckEdit.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            //repositoryCheckEdit.NullText = "N";
            repositorySpinEdit.Click += RepositorySpinEdit_Click;

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
            repositoryItemGridLookUpEdit.DataSource = DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckOX).ToList();

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
            gridEx1.MainGrid.AddColumn("Temp2", LabelConvert.GetLabelText("LastCheckId"), HorzAlignment.Center, true);
            gridEx1.MainGrid.SetEditable(true, "CheckValue");
            gridEx1.MainGrid.Columns["CheckValue"].MinWidth = 100;
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 30;
            gridEx1.MainGrid.MainView.RowHeight = 50;
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("Temp", "N");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPosition", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckPosition), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckList), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckWay), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckCycle), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckStandardDate", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckStandardDate), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp2", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName");
            gridEx1.MainGrid.MainView.Columns["ManagementStandard"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(gridEx1, "ManagementStandard", false, 200);

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

            // 20211108 오세완 차장 설비코드가 존재해도 이미지가 없는 경우가 존재하기 때문에 무조건 초기화로 변경
            //if (value.IsNullOrEmpty())
            //{
                lup_CheckId.EditValue = null;
                pic_CheckPointImage.EditValue = null;
            //}
            Search(value);
        }

        private void Search(string machineCode)
        {
            gridEx1.MainGrid.Clear();
            
            ModelService.ReLoad();

            if (machineCode.IsNullOrEmpty()) return;

            var TN_MEA1004 = ModelService.GetChildList<TN_MEA1004>(p => p.MachineCode == machineCode && p.StopEndTime == null).FirstOrDefault();
            if (TN_MEA1004 != null)
            {
                MessageBoxHandler.Show("해당 설비는 비가동상태입니다. 확인 부탁드립니다.");
                lup_Machine.EditValue = null;
                return;
            }

            using (var context = new Model.Context.ProductionContext(Utils.Common.ServerInfo.GetConnectString(Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var MachineCode = new SqlParameter("@MachineCode", machineCode);
                var CheckDate = new SqlParameter("@CheckDate", DateTime.Today);
                GridBindingSource.DataSource = context.Database.SqlQuery<TEMP_XPFMACHINECHECK_LIST>("USP_GET_XPFMACHINECHECK_LIST @MachineCode,@CheckDate", MachineCode, CheckDate).OrderBy(p => p.DisplayOrder).ToList();

            }

            var TN_MEA1000 = lup_Machine.GetSelectedDataRow() as TN_MEA1000;
            if (TN_MEA1000 != null)
            {
                if (!TN_MEA1000.FileUrl2.IsNullOrEmpty())
                    pic_CheckPointImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_MEA1000.FileUrl2);
            }

            gridEx1.DataSource = GridBindingSource;
            gridEx1.BestFitColumns();
        }

        /// <summary>
        /// 저장 클릭 이벤트
        /// </summary>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if (GridBindingSource == null || GridBindingSource.DataSource == null) return;

            var list = GridBindingSource.List as List<TEMP_XPFMACHINECHECK_LIST>;
            if (list == null || list.Count == 0) return;

            var machineCode = lup_Machine.EditValue.GetNullToEmpty();
            if (machineCode.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("Machine")));
                return;
            }

            var checkId = lup_CheckId.EditValue.GetNullToEmpty();
            if (checkId.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("CheckId")));
                return;
            }

            var TN_MEA1002List = ModelService.GetChildList<TN_MEA1002>(p => p.MachineCode == machineCode).ToList();

            foreach (var v in list)
            {
                if (v.MachineCode != machineCode) return;
                if (v.CheckValue.IsNullOrEmpty()) continue;

                if (v.LastCheckDate == DateTime.Today)
                {
                    //Update
                    var updateObj = TN_MEA1002List.Where(p => p.CheckSeq == v.CheckSeq).First().TN_MEA1003List.Where(p => p.CheckDate == DateTime.Today).Last();
                    updateObj.CheckDate = DateTime.Today;
                    updateObj.CheckId = checkId;
                    updateObj.CheckValue = v.CheckValue;
                    ModelService.Update(updateObj);
                }
                else
                {
                    //Insert
                    var newObj = new TN_MEA1003();
                    newObj.MachineCode = v.MachineCode;
                    newObj.CheckSeq = v.CheckSeq;
                    newObj.CheckDate = DateTime.Today;
                    newObj.CheckId = checkId;
                    newObj.CheckValue = v.CheckValue;
                    ModelService.Insert(newObj);
                }
            }
            ModelService.Save();
            DialogResult = DialogResult.OK;
            IsFormControlChanged = false;
            //ActClose();
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

        /// <summary>
        /// 점검 컨트롤 변경을 위함
        /// </summary>
        //private void MainView_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        //{
        //    var view = sender as GridView;
        //    if (e.Column.FieldName == "CheckValue" && e.RowHandle >= 0)
        //    {
        //        var checkWay = view.GetRowCellValue(e.RowHandle, view.Columns["CheckWay"]).GetNullToEmpty();
        //        if (checkWay == MasterCodeSTR.MachineCheckWay_CheckFlag) 
        //        {
        //            //치수 입력
        //            e.RepositoryItem = repositorySpinEdit;
        //        }
        //        else
        //        {
        //            //체크 입력
        //            e.RepositoryItem = repositoryCheckEdit;
        //        }
        //    }
        //}

        private void MainView_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                //if (e.Column.FieldName == "CheckValue")
                //{
                //    var checkWay = view.GetRowCellValue(e.RowHandle, view.Columns["CheckWay"]).GetNullToEmpty();
                //    if (checkWay == MasterCodeSTR.MachineCheckWay_CheckFlag)
                //    {
                //        //치수 입력
                //        e.RepositoryItem = repositorySpinEdit;
                //    }
                //    else if (checkWay == MasterCodeSTR.MachineCheckWay_Memo)
                //    {
                //        //메모 입력
                //        //e.Column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Default;
                //        e.RepositoryItem = repositoryItemTextEdit;
                //    }
                //    else
                //    {
                //        //O,X 입력
                //        //e.Column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                //        e.RepositoryItem = repositoryItemGridLookUpEdit;
                //    }
                //}

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
            var obj = GridBindingSource.Current as TEMP_XPFMACHINECHECK_LIST;
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
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                spinEidt.EditValue = keyPad.returnval;
            }
        }

    }
}
