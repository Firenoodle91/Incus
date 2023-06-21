﻿using System;
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
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 설비이력관리화면
    /// </summary>
    public partial class XFMEA1001 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");

        private BindingSource gridEx3_BindingSource = new BindingSource();
        private List<TN_MEA1003> gridEx3_DeleteList = new List<TN_MEA1003>();
        RepositoryItemSpinEdit repositorySpinEdit = new RepositoryItemSpinEdit();
        //RepositoryItemCheckEdit repositoryCheckEdit = new RepositoryItemCheckEdit() { ValueChecked = "Y", ValueUnchecked = "N" };
        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;
        private bool gridEx3_SaveCheck = false;

        public XFMEA1001()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;

            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;

            gridEx3.ActAddRowClicked += GridEx3_ActAddRowClicked;
            gridEx3.ActDeleteRowClicked += GridEx3_ActDeleteRowClicked;
            gridEx3.MainGrid.MainView.CellValueChanged += gridEx3_MainView_CellValueChanged;
            gridEx3.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitCombo()
        {
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
            DevExpress.XtraGrid.Columns.GridColumn col1 = repositoryItemGridLookUpEdit.View.Columns.AddField(repositoryItemGridLookUpEdit.DisplayMember);
            col1.VisibleIndex = 0;

            repositoryItemGridLookUpEdit.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton() { Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete });
            repositoryItemGridLookUpEdit.ButtonPressed += Lookup_ButtonPressed;

            repositoryItemTextEdit = new RepositoryItemTextEdit();
            repositoryItemTextEdit.Appearance.TextOptions.HAlignment = HorzAlignment.Default;

            repositorySpinEdit.Buttons[0].Visible = false;
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
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("MachineMCode", LabelConvert.GetLabelText("MachineMCode"), false);
            MasterGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MachineName"), LabelConvert.GetLabelText("MachineName"));
            MasterGridExControl.MainGrid.AddColumn("Model", LabelConvert.GetLabelText("Model"));
            MasterGridExControl.MainGrid.AddColumn("Maker", LabelConvert.GetLabelText("Maker"));
            MasterGridExControl.MainGrid.AddColumn("InstallDate", LabelConvert.GetLabelText("InstallDate"));
            MasterGridExControl.MainGrid.AddColumn("SerialNo", LabelConvert.GetLabelText("SerialNo"));
            MasterGridExControl.MainGrid.AddColumn("CheckTurn", LabelConvert.GetLabelText("CheckTurn"));
            MasterGridExControl.MainGrid.AddColumn("NextCheckDate", LabelConvert.GetLabelText("NextCheckDate"));
            MasterGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("FileName"));
            MasterGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn("FileName3", LabelConvert.GetLabelText("CheckMaintenanceFileName"));
            MasterGridExControl.MainGrid.AddColumn("FileUrl3", LabelConvert.GetLabelText("CheckMaintenanceFileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "NextCheckDate");

            DetailGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineMCode"), false);
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("CheckDate", LabelConvert.GetLabelText("CheckDate"));
            DetailGridExControl.MainGrid.AddColumn("CheckDivision", LabelConvert.GetLabelText("CheckDivision"));
            DetailGridExControl.MainGrid.AddColumn("RepairCode", LabelConvert.GetLabelText("RepairCode"));
            DetailGridExControl.MainGrid.AddColumn("Price", LabelConvert.GetLabelText("Price"));
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("CheckId", LabelConvert.GetLabelText("CheckId"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckDate", "CheckDivision", "RepairCode", "Price", "ItemCode", "Memo");

            gridEx3.SetToolbarButtonVisible(false);
            gridEx3.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
            gridEx3.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);

            gridEx3.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineMCode"), false);
            gridEx3.MainGrid.AddColumn("CheckSeq", LabelConvert.GetLabelText("CheckSeq"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            gridEx3.MainGrid.AddColumn("CheckDate", LabelConvert.GetLabelText("CheckDate"), false);
            gridEx3.MainGrid.AddColumn("TN_MEA1002.CheckPosition", LabelConvert.GetLabelText("CheckPosition"));
            gridEx3.MainGrid.AddColumn("TN_MEA1002.CheckList", LabelConvert.GetLabelText("CheckList"));
            gridEx3.MainGrid.AddColumn("TN_MEA1002.CheckWay", LabelConvert.GetLabelText("CheckWay"));
            gridEx3.MainGrid.AddColumn("TN_MEA1002.Temp", LabelConvert.GetLabelText("EyeCheckFlag"), false);
            gridEx3.MainGrid.AddColumn("TN_MEA1002.CheckCycle", LabelConvert.GetLabelText("CheckCycle"));
            gridEx3.MainGrid.AddColumn("TN_MEA1002.ManagementStandard", LabelConvert.GetLabelText("ManagementStandard"));
            gridEx3.MainGrid.AddColumn("CheckId", LabelConvert.GetLabelText("CheckId"), false);
            gridEx3.MainGrid.AddColumn("CheckValue", LabelConvert.GetLabelText("CheckValue"));
            gridEx3.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            gridEx3.MainGrid.SetEditable(UserRight.HasEdit, "CheckValue", "Memo");

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Model", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineModel), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineMaker, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckTurn", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckCycle, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InstallDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("NextCheckDate");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            MasterGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MachineImage, "FileName", "FileUrl", true);
            MasterGridExControl.MainGrid.MainView.Columns["FileName3"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MachineMaintenance, "FileName3", "FileUrl3");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");

           // DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("TN_MEA1002.Temp", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("CheckDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDivision", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckDivision, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("RepairCode", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineRepairHistory, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("Price", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => 1 == 1).ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);

            gridEx3.MainGrid.SetRepositoryItemDateEdit("CheckDate");
            gridEx3.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_MEA1002.CheckPosition", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckPosition), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx3.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_MEA1002.CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckList), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx3.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_MEA1002.CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckWay), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx3.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_MEA1002.CheckCycle", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckCycle), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx3.MainGrid.MainView.Columns["TN_MEA1002.ManagementStandard"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(gridEx3, "TN_MEA1002.ManagementStandard");
            gridEx3.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(gridEx3, "Memo", UserRight.HasEdit);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            gridEx3.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();

            gridEx3_SaveCheck = false;
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            gridEx3.MainGrid.Clear();
            gridEx3_DeleteList.Clear();

            ModelService.ReLoad();

            var machineCodeName = tx_MachineCodeName.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.MachineCode.Contains(machineCodeName) || (p.MachineName == machineCodeName) || p.MachineNameENG.Contains(machineCodeName) || p.MachineNameCHN.Contains(machineCodeName))
                                                                    && (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                               )
                                                               .OrderBy(p => p.MachineName)
                                                               .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1000;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                gridEx3.MainGrid.Clear();
                gridEx3_DeleteList.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_MEA1001List.OrderBy(p => p.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }
        
        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridEx3_SaveCheck)
            {
                DialogResult result = MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_1), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    ActSave();
                    return;
                }
                else
                {
                    ActRefresh();
                    return;
                }
            }

            var detailObj = DetailGridBindingSource.Current as TN_MEA1001;
            if (detailObj == null || detailObj.CheckDivision != MasterCodeSTR.MachineCheckDivision_Maintenance) 
            {
                gridEx3.MainGrid.Clear();
                gridEx3_DeleteList.Clear();
                return;
            }
            gridEx3_BindingSource.DataSource = ModelService.GetChildList<TN_MEA1003>(p => p.MachineCode == detailObj.MachineCode 
                                                                                        && p.CheckDate == detailObj.CheckDate
                                                                                        && p.TN_MEA1002.Division == MasterCodeSTR.MachineStandardCheckDivision_Maintenance).ToList();
            gridEx3.DataSource = gridEx3_BindingSource;
            gridEx3.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            if (gridEx3_BindingSource != null)
            {
                gridEx3_BindingSource.EndEdit();
                gridEx3.MainGrid.PostEditor();

                var subList = gridEx3_BindingSource.List as List<TN_MEA1003>;

                if (subList != null && (subList.Count > 0 && (subList.Any(p => p.NewRowFlag == "Y") || subList.Any(p => p.EditRowFlag == "Y"))))
                {
                    foreach (var v in subList.Where(p => p.NewRowFlag == "Y").ToList())
                    {
                        ModelService.InsertChild(v);
                    }

                    foreach (var v in subList.Where(p => p.EditRowFlag == "Y").ToList())
                    {
                        ModelService.UpdateChild(v);
                    }
                }
                if (gridEx3_DeleteList.Count > 0)
                {
                    foreach (var v in gridEx3_DeleteList)
                    {
                        ModelService.RemoveChild(v);
                    }
                }
            }

            ModelService.Save();
            DataLoad();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1000;
            if (masterObj == null) return;

            var newObjCheck = masterObj.TN_MEA1001List.Where(p => p.NewRowFlag == "Y").FirstOrDefault();
            if (newObjCheck != null)
            {
                DetailGridExControl.MainGrid.MainView.FocusedRowHandle = DetailGridExControl.MainGrid.MainView.LocateByValue("Seq", newObjCheck.Seq);
            }
            else
            {
                var newObj = new TN_MEA1001()
                {
                    MachineCode = masterObj.MachineMCode,
                    Seq = masterObj.TN_MEA1001List.Count == 0 ? 1 : masterObj.TN_MEA1001List.Max(p => p.Seq) + 1,
                    CheckId = GlobalVariable.LoginId,
                    NewRowFlag = "Y",
                };
                masterObj.TN_MEA1001List.Add(newObj);
                DetailGridBindingSource.Add(newObj);
            }
        }
        
        protected override void DeleteDetailRow()
        {
            TN_MEA1000 objMst = MasterGridBindingSource.Current as TN_MEA1000;
            TN_MEA1001 objDtl = DetailGridBindingSource.Current as TN_MEA1001;

            if (objDtl != null)
            {
                var subList = gridEx3_BindingSource.List as List<TN_MEA1003>;
                if (subList.Count > 0)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_10));
                    return;
                }
                objMst.TN_MEA1001List.Remove(objDtl);
                DetailGridBindingSource.RemoveCurrent();
            }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1000;
            var detailObj = DetailGridBindingSource.Current as TN_MEA1001;
            if (masterObj == null || detailObj == null) return;

            var maxSeq = masterObj.TN_MEA1001List.Max(p => p.Seq);
            if (detailObj.CheckDivision == MasterCodeSTR.MachineCheckDivision_Maintenance)
            {
                var subList = gridEx3_BindingSource.List as List<TN_MEA1003>;
                if (subList != null && subList.Count > 0)
                    e.Cancel = true;
                else
                {
                    if (detailObj.Seq != maxSeq)
                        e.Cancel = true;
                }
            }
            else
            {
                if (detailObj.Seq != maxSeq)
                    e.Cancel = true;
            }
            
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.Column.FieldName == "CheckDate")
            {
                var masterObj = MasterGridBindingSource.Current as TN_MEA1000;
                var detailObj = DetailGridBindingSource.Current as TN_MEA1001;
                if (masterObj == null || detailObj == null) return;

                if (masterObj.CheckTurn.IsNullOrEmpty()) return;

                object CheckDivision = gv.GetRowCellValue(e.RowHandle, gv.Columns["CheckDivision"]);

                if (CheckDivision.GetNullToEmpty() == MasterCodeSTR.MachineCheckDivision_Maintenance)
                {
                    DateTime dt = Convert.ToDateTime(detailObj.CheckDate);

                    masterObj.NextCheckDate = dt.AddMonths(masterObj.CheckTurn.GetIntNullToZero());

                    MasterGridExControl.MainGrid.BestFitColumns();
                }
            }
            else if (e.Column.FieldName == "CheckDivision")
            {
                if (e.Value.GetNullToEmpty() == MasterCodeSTR.MachineCheckDivision_Maintenance) 
                {
                    MainView_FocusedRowChanged(null, null);
                }
            }
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                object NextCheckDate = View.GetRowCellValue(e.RowHandle, View.Columns["NextCheckDate"]);

                if (NextCheckDate.GetNullToEmpty() != "")
                {
                    if (Convert.ToDateTime(NextCheckDate).AddDays(-7) <= DateTime.Today)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                    //점검예정일이거나 넘어섰을 경우
                   else if (Convert.ToDateTime(NextCheckDate).AddDays(-30) <= DateTime.Today)
                    {
                        e.Appearance.BackColor = Color.Yellow;
                        e.Appearance.ForeColor = Color.Black;
                    }
                    
                }
            }
        }

        private void GridEx3_ActAddRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1000;
            var detailObj = DetailGridBindingSource.Current as TN_MEA1001;
            if (masterObj == null || detailObj == null || detailObj.CheckDivision != MasterCodeSTR.MachineCheckDivision_Maintenance) return;

            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            if (detailObj.CheckDate == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("CheckDate")));
                return;
            }

            if (detailObj.CheckId == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("CheckId")));
                return;
            }

            var subList = gridEx3_BindingSource.List as List<TN_MEA1003>;
            var newList = ModelService.GetChildList<TN_MEA1002>(p => p.MachineCode == detailObj.MachineCode && p.Division == MasterCodeSTR.MachineStandardCheckDivision_Maintenance).ToList();
            foreach (var v in newList)
            {
                if (!subList.Any(p => p.MachineCode == masterObj.MachineMCode && p.CheckSeq == v.CheckSeq))
                {
                    var newObj = new TN_MEA1003()
                    {
                        MachineCode = masterObj.MachineMCode,
                        CheckSeq = v.CheckSeq,
                        CheckDate = (DateTime)detailObj.CheckDate,
                        CheckId = detailObj.CheckId,
                        TN_MEA1002 = v,
                        NewRowFlag = "Y",
                    };

                    gridEx3_SaveCheck = true;
                    gridEx3_BindingSource.Add(newObj);
                }
            }
        }

        private void GridEx3_ActDeleteRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1000;
            var detailObj = DetailGridBindingSource.Current as TN_MEA1001;
            var subObj = gridEx3_BindingSource.Current as TN_MEA1003;
            if (masterObj == null || detailObj == null || subObj == null) return;

            if (detailObj.CheckDivision == MasterCodeSTR.MachineCheckDivision_Maintenance)
            {
                if (subObj.NewRowFlag == "Y")
                    gridEx3_BindingSource.RemoveCurrent();
                else
                {
                    gridEx3_DeleteList.Add(subObj);
                    gridEx3_BindingSource.RemoveCurrent();
                    gridEx3_SaveCheck = true;
                }
            }
        }

        private void gridEx3_MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var obj = gridEx3_BindingSource.Current as TN_MEA1003;
            if (obj.NewRowFlag != "Y")
            {
                obj.EditRowFlag = "Y";
                gridEx3_SaveCheck = true;
            }
        }

        private void MainView_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                //if (e.Column.FieldName.Contains("CheckValue"))
                //{
                //    var checkWay = view.GetRowCellValue(e.RowHandle, view.Columns["TN_MEA1002.CheckWay"]).GetNullToEmpty();
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
                    var checkWay = view.GetRowCellValue(e.RowHandle, view.Columns["TN_MEA1002.CheckWay"]).GetNullToEmpty();
                    if (checkWay == MasterCodeSTR.MachineCheckWay_Memo)
                    {
                        //메모 입력
                        //e.Column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Default;
                        e.RepositoryItem = repositoryItemTextEdit;
                    }
                    else
                    {
                        var eyeCheckFlag = view.GetRowCellValue(e.RowHandle, view.Columns["TN_MEA1002.Temp"]).GetNullToEmpty();
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
    }
}